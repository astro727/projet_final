using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cinétique : MonoBehaviour
{
    private Formule4facteurs f4f;
    public GameObject positionC;
    public GameObject positionP;
    public GameObject positionI;
    public GameObject pompes;
    public GameObject neutrons;

    public double epsilon = 0;
    public double fuite = 0;
    public double eta = 0;
    public double p = 0;
    public double f = 0;
    public double fPre = 0;
    public double fTemp = 0;
    public double fI = 0;
    public double pI = 0;
    public double insertionP = 0;
    public double insertionS = 0;
    public double forcePompe = 24000000;
    public double chaleurPompe = 24000000;
    public double termeInitial = 1;
    public double exposantP = 0;
    
    public double deltaTemp = 0;
    public double nbNeutronsI = 0;
    public double nbNeutronsC = 0;
    public double inteSource = 0;
    public double puissance = 0;
    public double puissanceElectric = 0;
    public double temps = 0;
    public double tpsMove = 0;
    public double kEffF = 0;
    public double kEff = 0;
    public double rho = 0;
    public double pcm = 0;
    public double pcmT = 0;
    public double T = 0;
    public double Lambda = 0;
    public double Ts = 0;

    private double beta = 0.0065;
    private float e = 2.71828f;
    public bool scram = false;
    private bool moveBarre = false;
    private double keffI = 0;

    public bool start = false;
    private int etat = 0;
    private int masseEau = 26000;

    // Start is called before the first frame update
    void Start()
    {
        f4f = this.gameObject.GetComponent<Formule4facteurs>();
    }

    // Update is called once per frame
    void Update()
    {
        //vérification de la nécessité d'un SCRAM
        if (deltaTemp > 60 && scram == false)
        {
            SCRAM();
        }
        //update des valeurs importantes
        temperature();
        reactivite();
        mouvementControle();

        if (start == true)
        {
            
            puissanceInstantane();

            //cette série de if vérifie dans quel état se trouve le réacteur par rapport à sa réactivité

            if (rho < -0.00005 && nbNeutronsI == 0)
            {
                sousCritique();
            }
            if (rho < -0.00005 && nbNeutronsI != 0)
            {
                reduction();
            }
            if (rho < 0.00005 && rho > -0.00005)
            {
                critique();
            }
            if (rho > 0.00005 && rho < beta)
            {
                superCritiqueRetarde();
            }
            if (rho > 0.0002 && rho > beta)
            {
                superCritiquePrompt();
            }
        }
    }

    public void setPuissancePompe()
    {
        //permet aux autres scripts de set la puissance des pompes
        forcePompe = Convert.ToDouble(pompes.GetComponent<TMP_InputField>().text)*1000000;
    }

    public void setIntensiteSource()
    {
        //permet aux autres scripts de set l'intensité de la source neutronique
        inteSource = Convert.ToDouble(neutrons.GetComponent<TMP_InputField>().text);
    }

    void reactivite()
    {
        if(start == false)
        {
            //calcul la réactivité en part par cent mille en utilisant es valeurs du script f4f
            epsilon = f4f.epsilon;
            eta = f4f.eta;
            fI = f4f.f;
            pI = f4f.p;
            fuite = f4f.fuite;
            kEffF = epsilon * eta * pI * fI * fuite;
            pcm = ((kEffF - 1) / kEffF) * 100000;
        }
        //déduit la valeur p modifié pour la température
        pcmT = pcm + (273 + deltaTemp) * -3 + (273 + deltaTemp) * -25;
        keffI = 1 / (1 - pcmT / 100000);
        p = keffI / (eta * epsilon * fI);

        //set la réactivité en fonction de f
        kEff = p * f * epsilon * eta;
        rho = (kEff - 1) / kEff;
    }

    void temperature()
    {
        //calcul la variation de la température en fonction de la puissance et de la puissance des pompes
        if(deltaTemp > -253)
        {
            deltaTemp += Time.deltaTime * ((forcePompe/(-4184 * masseEau)) + (chaleurPompe/(4184*masseEau)) + (puissance/(4184*masseEau)));
        }
        else
        {
            deltaTemp = -253;
        }
    }


    public void barreControle()
    {
        // sélectione les valeurs à assigner aux barres de controles à partir des éléments UI
        if(moveBarre == false || scram == true)
        {
            fTemp = fI * (1 - (Convert.ToDouble(positionC.GetComponent<TMP_InputField>().text) / 100) + 0.1 - (Convert.ToDouble(positionI.GetComponent<TMP_InputField>().text) / 1000) + 0.01 - (Convert.ToDouble(positionP.GetComponent<TMP_InputField>().text) / 10000));
            moveBarre = true;
            fPre = f;
        }
    }

    void sousCritique()
    {
        //modélisation de l'état sous critique initial du réacteur
        temps += Time.deltaTime;
        Lambda = (2.1 * 0.0001) / kEff;
        T = ((-rho + beta) / -rho) * 9.03;
        Ts = (Lambda / (-rho + beta));
        nbNeutronsC = (inteSource * Lambda / -rho * ((rho / (-rho + beta) * Mathf.Pow(e, (float)(-temps / Ts))) + (1 - (beta / (-rho + beta) * Mathf.Pow(e, (float)(-temps / T))))));
        etat = 1;
    }

    void critique()
    {
        //modélise l'état stable du réacteur
        if(etat != 0)
        {
            temps = 0;
        }
        if(puissance < 4400000000)
            forcePompe = puissance;

        nbNeutronsI = nbNeutronsC;
        etat = 0;
    }

    void superCritiqueRetarde()
    {
        //modélise la croissance retardé du racteur
        if (etat != 2)
        {
            nbNeutronsI = nbNeutronsC;
            temps = 0;
            etat = 2;
        }
        if(moveBarre == true)
        {
            termeInitial = (beta / (beta - rho));
        }
        T = 0.085 / rho;
        temps += Time.deltaTime / T;
        nbNeutronsC = (nbNeutronsI * termeInitial * Mathf.Pow(e, (float) temps));
        exposantP = temps / T;
    }

    void superCritiquePrompt()
    {
        //modélise la croissance prompt du réacteur
        if (etat != 3)
        {
            nbNeutronsI = nbNeutronsC;
            temps = 0;
        }
        temps += Time.deltaTime;
        Lambda = (2.1 * 0.0001) / kEff;
        T = Lambda / (rho - beta);
        nbNeutronsC = (nbNeutronsI * (Mathf.Pow(e, (float)(temps / T))));
        etat = 3;
    }

    void reduction()
    {
        //modélise la réduction de la puissance du réacteur
        if(etat != 4)
        {
            nbNeutronsI = nbNeutronsC;
            temps = 0;
        }
        temps += Time.deltaTime;
        nbNeutronsC = nbNeutronsI * ((1/(1+ (-rho)))*MathF.Pow(e,(float)-temps/80));
        etat = 4;
    }

    void puissanceInstantane()
    {
        //calcul al puissance à un moment donné ainsi que la puissance électrique
        puissance = 200 * 1000000 * 1.602 * Mathf.Pow(10, -21) * p * f * nbNeutronsC;
        if(deltaTemp > 0 && forcePompe >= 24000000)
        {
            if(deltaTemp < 10)
            {
                puissanceElectric = ((forcePompe - 24000000) / (30 - deltaTemp)) * 0.32;
            }
            else
            {
                if(deltaTemp < 20)
                {
                    puissanceElectric = ((forcePompe - 24000000) / (30 - (1.25 * deltaTemp))) * 0.32;
                }
                else
                {
                    puissanceElectric = (forcePompe - 24000000) * 0.32;
                }
            }
        }
        else
        {
            puissanceElectric = 0;
        }
        
    }

    void SCRAM()
    {
        //arrêt d'urgence du réacteur, force les valeurs des barres de controles à leur maximum
        positionC.GetComponent<TMP_InputField>().text = "100";
        positionI.GetComponent<TMP_InputField>().text = "100";
        pompes.GetComponent<TMP_InputField>().text = "4400";
        scram = true;
        tpsMove = 0;
        setPuissancePompe();
        barreControle();
    }

    void mouvementControle()
    {
        if (scram == false)
        {
            //opération normale: les barres de controles prennent 10 secondes à atteindre leurs valeurs désirés
            if (moveBarre && tpsMove < 10)
            {
                tpsMove += Time.deltaTime;
                double deltaV = (fTemp - fPre);
                f = fPre + (deltaV * tpsMove / 10);
            }
            else
            {
                f = fTemp;
                moveBarre = false;
                tpsMove = 0;
            }
        }
        else
        {
            //opération d'urgence: les barres de controles sont relaché et forcé très rapidement par la gravité à leur valeur maximale, prend 1 seconde
            if (moveBarre && tpsMove < 1)
            {
                tpsMove += Time.deltaTime;
                double deltaV = (fTemp - fPre);
                f = fPre + (deltaV * tpsMove);
            }
            else
            {
                f = fTemp;
                moveBarre = false;
                tpsMove = 0;
            }
        }
    }

}

