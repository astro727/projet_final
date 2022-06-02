using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class valeurCinetique : MonoBehaviour

{
    public GameObject K;
    public GameObject rho;
    public GameObject f;
    public GameObject T;
    public GameObject P;
    public GameObject positionC;
    public GameObject positionP;
    public GameObject positionI;
    public GameObject Pe;
    double finst;

    public GameObject reacteur;
    Cin�tique cin;
    // Start is called before the first frame update
    void Start()
    {
        cin = reacteur.GetComponent<Cin�tique>();
    }

    // Update is called once per frame
    void Update()
    {
        //assignation des valeurs � leurs �l�ments UI
        K.GetComponent<TMP_Text>().text = "K = " + cin.kEff.ToString();
        f.GetComponent<TMP_Text>().text = "F = " + cin.f.ToString();
        T.GetComponent<TMP_Text>().text = "T = " + (Math.Round(cin.deltaTemp, 2) + 273).ToString() + "�C";
        //v�rification de l'ordre de magnitude de la puissance, puis assignation de l'unit� appropri�e
        if (cin.puissance > 1)
        {
            if (cin.puissance < 1000)
            {
                P.GetComponent<TMP_Text>().text = "Puissance = " + (Math.Round(cin.puissance, 1)).ToString() + "W";
            }
            else
            {
                if(cin.puissance < 1000000)
                {
                    P.GetComponent<TMP_Text>().text = "Puissance = " + (Math.Round(cin.puissance/1000, 1)).ToString() + "kW";
                }
                else
                {
                    if(cin.puissance < 1000000000)
                    {
                        P.GetComponent<TMP_Text>().text = "Puissance = " + (Math.Round(cin.puissance / 1000000, 1)).ToString() + "MW";
                    }
                    else
                    {
                        P.GetComponent<TMP_Text>().text = "Puissance = " + (Math.Round(cin.puissance / 1000000000, 1)).ToString() + "GW";
                    }
                }

            }
        }
        else 
        {
            P.GetComponent<TMP_Text>().text = "Puissance = 0W";
        }


        //m�me principe que pour la puissance
        if (cin.puissanceElectric > 1)
        {
            if (cin.puissanceElectric < 1000)
            {
                Pe.GetComponent<TMP_Text>().text = "�lectricit� = " + (Math.Round(cin.puissanceElectric, 1)).ToString() + "W";
            }
            else
            {
                if (cin.puissanceElectric < 1000000)
                {
                    Pe.GetComponent<TMP_Text>().text = "�lectricit� = " + (Math.Round(cin.puissanceElectric / 1000, 1)).ToString() + "kW";
                }
                else
                {
                    if (cin.puissanceElectric < 1000000000)
                    {
                        Pe.GetComponent<TMP_Text>().text = "�lectricit� = " + (Math.Round(cin.puissanceElectric / 1000000, 1)).ToString() + "MW";
                    }
                    else
                    {
                        Pe.GetComponent<TMP_Text>().text = "�lectricit� = " + (Math.Round(cin.puissanceElectric / 1000000000, 1)).ToString() + "GW";
                    }
                }

            }
        }
        else
        {
            Pe.GetComponent<TMP_Text>().text = "�lectricit� = 0W";
        }
        //valeur de la r�activit� pour voir la valeur �quivalente � la position voulue des barres ainsi que la valeur acuelle
        finst = cin.fI* (1 - (Convert.ToDouble(positionC.GetComponent<TMP_InputField>().text) / 100) + 0.1 - (Convert.ToDouble(positionI.GetComponent<TMP_InputField>().text) / 1000) + 0.01 - (Convert.ToDouble(positionP.GetComponent<TMP_InputField>().text) / 10000));
        rho.GetComponent<TMP_Text>().text = "r�activit� = " + (Math.Round(cin.rho / 0.0065, 2)).ToString() + "$ (" + Math.Round(((cin.p*cin.epsilon*cin.eta*finst-1)/ (cin.p * cin.epsilon * cin.eta * finst))/0.0065, 2) + ")";
        //couleur pour indiqu� l'�taat du r�acteur
        if(cin.rho > 0.0065)
        {
            rho.GetComponent<TMP_Text>().color = Color.red;
        }
        else
        {
            if(cin.rho < 0.0065 && cin.rho > 0)
            {
                rho.GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                if(cin.rho < 0)
                rho.GetComponent<TMP_Text>().color = Color.blue;
            }
        }
    }
        
}
