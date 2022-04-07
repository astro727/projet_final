using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formule4facteurs : MonoBehaviour
{
    public double epsilon = 0;
    public double nu = 0;
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
        Kinf = calculNu() * calculF() * calculP() * calculEpsilon();

        keff = Kinf * calculFuite();
        rhoPCM = ((keff - 1) / keff) * 100000;

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
        N = (float)(enrichissement *1.42857* Mathf.Pow(10, 23));
        z = (float)(5.104 * Mathf.Pow(10, -12) * Mathf.Pow(N, (float)0.522) * Mathf.Pow((float)(moderation + 0.01), -1));
        p = (Mathf.Pow((float)2.71828, -z));

        return p;
    }

    double calculNu()
    {
        nu = (2.082 / (1 + 3.957*Mathf.Pow(10, -3) / enrichissement));

        return nu;
    }

    double calculEpsilon()
    {
        float temp = 0f;
        temp = (float)(-0.0546 * moderation);
        epsilon = (1 + ((1 - p) / p) * (1 / f) * (0.137*Mathf.Pow((float)2.71828, temp)));

        return epsilon;
    }
}
