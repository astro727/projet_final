using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class toggleBox : MonoBehaviour
{
    public GameObject hemisphere;
    public GameObject reacteur;
    public GameObject derriere;
    public GameObject avant;
    public Material couleur;
    public Material comb;
    public Color cherenkov;
    public float vitesseFluide;
    public float chaleurFluide;
    bool actif = true;
    bool therm = false;
    // Start is called before the first frame update
    void Start()
    {
        cherenkov = comb.color;
    }

    // Update is called once per frame
    void Update()
    {
        vitesseFluide = 1.5f * (float)reacteur.GetComponent<Cin�tique>().forcePompe / 4400000000;
        chaleurFluide = ((float)reacteur.GetComponent<Cin�tique>().deltaTemp / 60);
        vague.lerpTime = 2 - vitesseFluide;
        if(reacteur.GetComponent<Cin�tique>().deltaTemp >= 0)
            couleur.color = new Color(( 0.165f + (0.84f* chaleurFluide)) , 0.278f, (1-chaleurFluide));
        if (reacteur.GetComponent<Cin�tique>().deltaTemp >= 0)
            comb.color = (cherenkov * (float) (-5 + (733333333.3 / reacteur.GetComponent<Cin�tique>().puissance)));
            
    }

    public void vueThermique()
    {
        if(therm == false)
        {
            if(actif == true)
            {
                avant.SetActive(true);
                derriere.SetActive(true);
                therm = true;
            }
            else
            {
                derriere.SetActive(true);
                therm = true;
            }
        }
        else
        {
            if (actif == true)
            {
                avant.SetActive(false);
                derriere.SetActive(false);
                therm = false;
            }
            else
            {
                derriere.SetActive(false);
                therm = false;
            }
        }
    }

    public void vueCoup()
    {
        if (actif)
        {
            hemisphere.SetActive(false);
            actif = false;
            if (therm == true)
            {
                avant.SetActive(false);
            }
        }
        else
        {
            hemisphere.SetActive(true);
            if(therm == true)
            {
                avant.SetActive(true);
            }
            actif = true;
        }
    }
}
