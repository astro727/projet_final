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
    
    public double deltaTemp = 0;
    public double nbNeutronsI = 0;
    public double nbNeutronsC = 0;
    public double inteSource = 0;
    public double puissance = 0;
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
    private bool scram = false;
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
        if (deltaTemp > 60)
        {
            SCRAM();
        }
        temperature();
        reactivite();

        if (moveBarre && tpsMove <10)
        {
            tpsMove += Time.deltaTime;
            double deltaV = (fTemp-fPre);
            f = fPre + (deltaV*tpsMove/10);
        }
        else
        {
            f = fTemp;
            moveBarre = false;
            tpsMove = 0;
        }

        if (start == true)
        {
            
            puissanceInstantane();

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
        forcePompe = Convert.ToDouble(pompes.GetComponent<TMP_InputField>().text)*1000000;
    }

    public void setIntensiteSource()
    {
        inteSource = Convert.ToDouble(neutrons.GetComponent<TMP_InputField>().text);
    }

    void reactivite()
    {
        if(start == false)
        {
            epsilon = f4f.epsilon;
            eta = f4f.eta;
            fI = f4f.f;
            pI = f4f.p;
            fuite = f4f.fuite;
            kEffF = epsilon * eta * pI * fI * fuite;
            pcm = ((kEffF - 1) / kEffF) * 100000;
        }

        pcmT = pcm + (273 + deltaTemp) * -3 + (273 + deltaTemp) * -31.62647;
        keffI = 1 / (1 - pcmT / 100000);
        p = keffI / (eta * epsilon * fI);

        kEff = p * f * epsilon * eta;
        rho = (kEff - 1) / kEff;
    }

    void temperature()
    {
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
        if(moveBarre == false)
        {
            fTemp = fI * (1 - (Convert.ToDouble(positionC.GetComponent<TMP_InputField>().text) / 100) + 0.1 - (Convert.ToDouble(positionI.GetComponent<TMP_InputField>().text) / 1000) + 0.01 - (Convert.ToDouble(positionP.GetComponent<TMP_InputField>().text) / 10000));
            moveBarre = true;
            fPre = f;
        }
    }

    void sousCritique()
    {
        temps += Time.deltaTime;
        Lambda = (2.1 * 0.0001) / kEff;
        T = ((-rho + beta) / -rho) * 9.03;
        Ts = (Lambda / (-rho + beta));
        nbNeutronsC = (inteSource * Lambda / -rho * ((rho / (-rho + beta) * Mathf.Pow(e, (float)(-temps / Ts))) + (1 - (beta / (-rho + beta) * Mathf.Pow(e, (float)(-temps / T))))));
        etat = 1;
    }

    void critique()
    {
        if(etat != 0)
        {
            temps = 0;
        }
        nbNeutronsI = nbNeutronsC;
        etat = 0;
    }

    void superCritiqueRetarde()
    {
        if (etat != 2)
        {
            nbNeutronsI = nbNeutronsC;
            temps = 0;
        }
        temps += Time.deltaTime;
        T = 0.085 / rho;
        nbNeutronsC = (nbNeutronsI * (beta / (beta - rho) * Mathf.Pow(e, (float)(temps / T))));
        etat = 2;
    }

    void superCritiquePrompt()
    {
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
        puissance = 200 * 1000000 * 1.602 * Mathf.Pow(10, -21) * p * f * nbNeutronsC;
    }

    void SCRAM()
    {
        positionC.GetComponent<TMP_InputField>().text = "100";
        positionI.GetComponent<TMP_InputField>().text = "100";
        pompes.GetComponent<TMP_InputField>().text = "4400";
        scram = true;
        setPuissancePompe();
        barreControle();
    }

}

