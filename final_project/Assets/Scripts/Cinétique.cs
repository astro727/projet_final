using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinétique : MonoBehaviour
{
    public double nbNeutronsI = 0;
    public double nbNeutronsC = 0;
    public double inteSource = 0;
    public double puissance = 0;
    public double tempsSous = 0;
    public double tempsSur = 0;
    public double tempsPrompt = 0;
    public double kEff = 0;
    public double rho = 0;
    public double T = 0;
    public double Tr = 0;
    public double Lambda = 0;
    public double Ts = 0;
    public double beta = 0.0065;
    public float e = 2.71828f;
    private bool wasPrompt = false;
    private bool wasDelayed = false;
    private bool wasSub = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rho = (kEff - 1) / kEff;

        if (rho < 0 && nbNeutronsC <= inteSource && kEff != 0)
        {
            tempsSous += Time.deltaTime;
            tempsSur = 0;
            tempsPrompt = 0;
            Lambda = (2.1 * 0.0001) / kEff;
            T = ((-rho + beta) / -rho) * 9.03;
            Ts = (Lambda / (-rho + beta));
            nbNeutronsI =(inteSource * Lambda / -rho * ((rho / (-rho + beta) * Mathf.Pow(e, (float)(-tempsSous / Ts))) + (1 - (beta / (-rho + beta) * Mathf.Pow(e, (float)(-tempsSous / T))))));
            puissance = 200 * 1000000 * 1.602 * Mathf.Pow(10, -21)*0.6*nbNeutronsI;
            wasSub = true;
            wasPrompt = false;
            wasDelayed = false;
        }
        if (rho == 0)
        {
            if (wasPrompt == true || wasDelayed == true)
            {
                nbNeutronsI = nbNeutronsC;
            }
            if(wasSub == true)
            {
                nbNeutronsC = nbNeutronsI;
            }
            puissance = 200 * 1000000 * 1.602 * Mathf.Pow(10, -21) * 0.6 * nbNeutronsC;
            tempsSous = 0;
            tempsPrompt = 0;
            tempsSur = 0;
            wasDelayed = false;
            wasPrompt = false;
            wasSub = false;
        }
        if (rho > 0 && rho < beta)
        {
            if (wasPrompt == true)
            {
                nbNeutronsI = nbNeutronsC;
            }
            tempsSous = 0;
            tempsPrompt = 0;
            tempsSur += Time.deltaTime;
            Tr = 0.085 / rho;
            nbNeutronsC = (nbNeutronsI *(beta/(beta-rho)*Mathf.Pow(e, (float)(tempsSur / Tr))));
            puissance = 200 * 1000000 * 1.602 * Mathf.Pow(10, -21) * 0.6 * nbNeutronsC;
            wasSub = false;
            wasPrompt = false;
            wasDelayed = true;
        }
        if (rho > 0.0002 && rho > beta)
        {
            if(wasDelayed == true)
            {
                nbNeutronsI = nbNeutronsC;
            }
            tempsSous = 0;
            tempsSur = 0;
            Lambda = (2.1 * 0.0001) / kEff;
            tempsPrompt += Time.deltaTime;
            Tr =  Lambda/ (rho-beta);
            nbNeutronsC = (nbNeutronsI * (Mathf.Pow(e, (float)(tempsPrompt / Tr))));
            puissance = 200 * 1000000 * 1.602 * Mathf.Pow(10, -21) * 0.6 * nbNeutronsC;
            wasSub = false;
            wasPrompt = true;
            wasDelayed = false;
        }

    }
}
