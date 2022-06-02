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
        //valeurs initiale de l'�mission
        cherenkov = new Color (0.176f, 0.867f, 0.796f);
        comb.SetColor("_EmissionColor", (cherenkov * 0.01f));
    }

    // Update is called once per frame
    void Update()
    {
        //ajustement de l'affichage de la vitesse du fluide en d�placement
        vitesseFluide = 1.5f * (float)reacteur.GetComponent<Cin�tique>().forcePompe / 4400000000;
        chaleurFluide = ((float)reacteur.GetComponent<Cin�tique>().deltaTemp / 60);
        vague.lerpTime = 2 - vitesseFluide;
        //ajustement de la couleur du fluide en fonction de la temp�rature
        if(reacteur.GetComponent<Cin�tique>().deltaTemp >= 0)
            couleur.color = new Color(( 0.165f + (0.84f* chaleurFluide)) , 0.278f, (1-chaleurFluide));
        //ajustement de la couleur de l'�mission de cherenkov en fonction de la puissance
        if (reacteur.GetComponent<Cin�tique>().puissance > 1)
            comb.SetColor("_EmissionColor", (cherenkov * (0.0706f +(0.1294f*Mathf.Log((float) reacteur.GetComponent<Cin�tique>().puissance)))));

    }

    public void vueThermique()
    {
        // simple v�rification pour qu'il n'y ait pas de conflits entre la vue coup�e et la vue thermique.
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
        // simple v�rification pour qu'il n'y ait pas de conflits entre la vue coup�e et la vue thermique.
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
