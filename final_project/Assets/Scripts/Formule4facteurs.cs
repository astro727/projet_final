using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Formule4facteurs : MonoBehaviour
{
    public GameObject E235;
    public GameObject Rn;
    public GameObject dia;
    public GameObject haut;

    public double epsilon = 0;
    public double eta = 0;
    public double p = 0;
    public double f = 0;
    public double Kinf = 0;
    public double keff = 0;
    public double enrichissement = 0;
    public double moderation = 0;
    public double fuite = 0;
    public double D = 0;
    public double H = 0;
    public double rhoPCM = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Kinf = calculEta() * calculF() * calculP() * calculEpsilon();
        
        keff = Kinf * calculFuite();
        rhoPCM = ((keff - 1) / keff) * 100000;

    }
    public void setModeration()
    {
        string temp2 = Rn.GetComponent<TMP_InputField>().text;
        
        moderation = Convert.ToDouble(temp2);
        if (moderation > 10)
        {
            moderation = 10;
            E235.GetComponent<TMP_InputField>().text = "10";
        }
        if (moderation < 2)
        {
            moderation = 2;
            E235.GetComponent<TMP_InputField>().text = "2";
        }

    }


    public void setEnrichissement()
    {
        string temp = E235.GetComponent<TMP_InputField>().text;
        enrichissement = Convert.ToDouble(temp) / 100;
        if (enrichissement > 0.07)
        {
            enrichissement = 0.07;
            E235.GetComponent<TMP_InputField>().text = "7";
        }
        if (enrichissement < 0.007)
        {
            enrichissement = 0.007;
            E235.GetComponent<TMP_InputField>().text = "0.7";
        }
    }

    public void setDimensions()
    {
        string temp = dia.GetComponent<TMP_InputField>().text;
        D = Convert.ToDouble(temp);
        if (D > 500)
        {
            D = 500;
            dia.GetComponent<TMP_InputField>().text = "500";
        }
        if (D < 100)
        {
            D = 100;
            dia.GetComponent<TMP_InputField>().text = "100";
        }

        string temp2 = haut.GetComponent<TMP_InputField>().text;
        H = Convert.ToDouble(temp2);
        if (H > 500)
        {
            H = 500;
            haut.GetComponent<TMP_InputField>().text = "500";
        }
        if (H < 100)
        {
            H = 100;
            haut.GetComponent<TMP_InputField>().text = "100";
        }
    }

    double calculFuite()
    {
        double M = 35.12;
        double B = 0;
        B = Mathf.Pow((float)(Mathf.PI / H), 2) + Mathf.Pow((float)(2.405 / D), 2);
        fuite = 1 / (1 + B * M);
        return fuite;
    }

    double calculF()
    {
        double phi = 0;
        phi = 4.901 * Mathf.Pow((float)10, -4) * ((moderation + 0.3624) / (enrichissement + 3.957 * Mathf.Pow((float)10, -3)));
        f = 1 / (1 + phi);

        return f;
    }

    double calculP()
    {
        float z = 0f;
        float N = 0f;
        N = (float)(2*Mathf.Pow(10,22)*Mathf.Pow(2.71828f, (float)(-51.44*enrichissement)));
        z = (float)(5.104 * Mathf.Pow(10, -12) * Mathf.Pow(N, (float)0.522) * Mathf.Pow((float)(moderation + 0.01), -1));
        p = (Mathf.Pow((float)2.71828, -z));

        return p;
    }

    double calculEta()
    {
        eta = (2.082 / (1 + 3.957*Mathf.Pow(10, -3) / enrichissement));

        return eta;
    }

    double calculEpsilon()
    {
        float temp = 0f;
        temp = (float)(-0.0546 * moderation);
        epsilon = (1 + ((1 - p) / p) * (1 / f) * (0.137*Mathf.Pow((float)2.71828, temp)));

        return epsilon;
    }
}
