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

    public double epsilon = 0;
    public double fuite = 0;
    public double eta = 0;
    public double p = 0;
    public double f = 0;
    public double fI = 0;
    public double pI = 0;
    public double insertionP = 0;
    public double insertionS = 0;
    
    public double deltaTemp = 0;
    public double nbNeutronsI = 0;
    public double nbNeutronsC = 0;
    public double inteSource = 0;
    public double puissance = 0;
    public double tempsSous = 0;
    public double tempsSur = 0;
    public double tempsPrompt = 0;
    public double tempsRed = 0;
    public double kEffF = 0;
    public double kEff = 0;
    public double rho = 0;
    public double pcm = 0;
    public double pcmT = 0;
    public double T = 0;
    public double Tr = 0;
    public double Lambda = 0;
    public double Ts = 0;

    private double beta = 0.0065;
    private float e = 2.71828f;

    public bool start = false;
    private int etat = 0;

    // Start is called before the first frame update
    void Start()
    {
        f4f = this.gameObject.GetComponent<Formule4facteurs>();
    }

    // Update is called once per frame
    void Update()
    {
        reactivite();

        if (start == true)
        {
            etatPrecedent();
            puissanceInstantane();
            if(deltaTemp > 50)
            {
                SCRAM();
            }

            if (rho < -0.0001 && nbNeutronsI == 0)
            {
                sousCritique();
            }
            if (rho < -0.0001 && nbNeutronsI != 0)
            {
                reduction();
            }
            if (rho < 0.0001 && rho > -0.0001)
            {
                critique();
            }
            if (rho > 0.0001 && rho < beta)
            {
                superCritiqueRetarde();
            }
            if (rho > 0.0002 && rho > beta)
            {
                superCritiquePrompt();
            }
        }
    }

    void reactivite()
    {
        epsilon = f4f.epsilon;
        eta = f4f.eta;
        fI = f4f.f;
        pI = f4f.p;
        fuite = f4f.fuite;
        kEffF = epsilon * eta * pI * fI * fuite;
        pcm = ((kEffF - 1) / kEffF) * 100000;
        deltaTemp = 40 * (puissance / (4.4 * Mathf.Pow(10, 9)));
        pcmT = pcm + (273 + deltaTemp) * -3 + (273 + deltaTemp) * -31.62647;
        kEff = 1 / (1 - pcmT / 100000);
        p = kEff / (eta * epsilon * fI);

        kEff = p * f * epsilon * eta;
        rho = (kEff - 1) / kEff;
    }

    void etatPrecedent()
    {
        switch(etat)
        {
            case 1:
                tempsSous += Time.deltaTime;
                tempsSur = 0;
                tempsPrompt = 0;
                tempsRed = 0;
                break;
            case 2:
                tempsSous = 0;
                tempsPrompt = 0;
                tempsSur += Time.deltaTime;
                tempsRed = 0;
                break;
            case 3:
                tempsSous = 0;
                tempsSur = 0;
                tempsPrompt += Time.deltaTime;
                tempsRed = 0;
                break;
            case 4:
                tempsSous = 0;
                tempsSur = 0;
                tempsPrompt = 0;
                tempsRed += Time.deltaTime;
                break;
            default:
                tempsSous = 0;
                tempsPrompt = 0;
                tempsSur = 0;
                tempsRed = 0;
                break;
        }
    }

    public void barreControle()
    {
        f = fI * (1 - (Convert.ToDouble(positionC.GetComponent<TMP_InputField>().text) / 100) + 0.1 - (Convert.ToDouble(positionI.GetComponent<TMP_InputField>().text) / 1000) + 0.01- (Convert.ToDouble(positionP.GetComponent<TMP_InputField>().text) / 10000));
    }

    void sousCritique()
    {
        Lambda = (2.1 * 0.0001) / kEff;
        T = ((-rho + beta) / -rho) * 9.03;
        Ts = (Lambda / (-rho + beta));
        nbNeutronsC = (inteSource * Lambda / -rho * ((rho / (-rho + beta) * Mathf.Pow(e, (float)(-tempsSous / Ts))) + (1 - (beta / (-rho + beta) * Mathf.Pow(e, (float)(-tempsSous / T))))));
        etat = 1;
    }

    void critique()
    {
        nbNeutronsI = nbNeutronsC;
        etat = 0;
    }

    void superCritiqueRetarde()
    {
        if (etat != 2)
        {
            nbNeutronsI = nbNeutronsC;
        }
        Tr = 0.085 / rho;
        nbNeutronsC = (nbNeutronsI * (beta / (beta - rho) * Mathf.Pow(e, (float)(tempsSur / Tr))));
        etat = 2;
    }

    void superCritiquePrompt()
    {
        if (etat != 3)
        {
            nbNeutronsI = nbNeutronsC;
        }
        Lambda = (2.1 * 0.0001) / kEff;
        Tr = Lambda / (rho - beta);
        nbNeutronsC = (nbNeutronsI * (Mathf.Pow(e, (float)(tempsPrompt / Tr))));
        etat = 3;
    }

    void reduction()
    {
        if(etat != 4)
        {
            nbNeutronsI = nbNeutronsC;
        }
        nbNeutronsC = nbNeutronsI * ((1/(1+ (-rho)))*MathF.Pow(e,(float)-tempsRed/80));
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
        barreControle();
    }

}

