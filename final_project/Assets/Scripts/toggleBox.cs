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
        cherenkov = new Color (0.176f, 0.867f, 0.796f);
        comb.SetColor("_EmissionColor", (cherenkov * 0.2f));
    }

    // Update is called once per frame
    void Update()
    {
        vitesseFluide = 1.5f * (float)reacteur.GetComponent<Cinétique>().forcePompe / 4400000000;
        chaleurFluide = ((float)reacteur.GetComponent<Cinétique>().deltaTemp / 60);
        vague.lerpTime = 2 - vitesseFluide;
        if(reacteur.GetComponent<Cinétique>().deltaTemp >= 0)
            couleur.color = new Color(( 0.165f + (0.84f* chaleurFluide)) , 0.278f, (1-chaleurFluide));
        if (reacteur.GetComponent<Cinétique>().puissance > 1)
            comb.SetColor("_EmissionColor", (cherenkov * (0.0706f +(0.1294f*Mathf.Log((float) reacteur.GetComponent<Cinétique>().puissance)))));

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
