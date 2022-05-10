using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boutton : MonoBehaviour
{
    public GameObject setup;
    public GameObject fonctionnement;
    public GameObject reacteur;

    Formule4facteurs f4f;
    Cinétique cin;
    // Start is called before the first frame update
    void Start()
    {
        f4f = reacteur.GetComponent<Formule4facteurs>();
        cin = reacteur.GetComponent<Cinétique>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lancer()
    {
        cin.start = true;
        GameObject temp;
        temp = GameObject.Find("Source");
        if (Convert.ToDouble(temp.GetComponent<TMP_InputField>().text) > 0)
        {
            temp.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
    public void confirmer()
    {
        cin.barreControle();
    }

    public void swap()
    {
        if(f4f.keff > 0)
        {
            fonctionnement.SetActive(true);
            setup.SetActive(false);
        }

        vague.start = true;
    }
}
