using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Boutton : MonoBehaviour
{
    public GameObject setup;
    public GameObject fonctionnement;
    public GameObject boxes;
    public GameObject reacteur;

    Formule4facteurs f4f;
    Cin�tique cin;
    // Start is called before the first frame update
    void Start()
    {
        f4f = reacteur.GetComponent<Formule4facteurs>();
        cin = reacteur.GetComponent<Cin�tique>();

    }

    public void lancer()
    {
        //v�rifie qu'une source de neutrons est sp�cifi� avant de d�marer le r�acteur;
        GameObject temp;
        temp = GameObject.Find("source");
        if (Convert.ToDouble(temp.GetComponent<TMP_InputField>().text) > 0)
        {
            cin.start = true;
            temp.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
    public void confirmer()
    {
        //ins�re les barres de contr�le
        cin.barreControle();
    }

    public void swap()
    {
        //changement de canvas � l'appuie du bouton
        if(f4f.keff > 0)
        {
            fonctionnement.SetActive(true);
            setup.SetActive(false);
            boxes.SetActive(true);
            vague.start = true;
        }
    }

    public void quitter()
    {
        //retour au menu
        vague.start = false;
        SceneManager.LoadScene("Main");
    }
}
