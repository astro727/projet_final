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
    bool actif = true;
    bool therm = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vague.lerpTime = 2 - 1.5f*(float)reacteur.GetComponent<Cinétique>().forcePompe / 44000000;
        couleur.color = new Color((42+ 213* ((float) reacteur.GetComponent<Cinétique>().deltaTemp / 60)) , 71, (255*(1-(float) (reacteur.GetComponent<Cinétique>().deltaTemp/60))));
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
