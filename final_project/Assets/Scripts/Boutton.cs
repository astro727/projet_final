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
    Cinétique cin;
    // Start is called before the first frame update
    void Start()
    {
        f4f = reacteur.GetComponent<Formule4facteurs>();
        cin = reacteur.GetComponent<Cinétique>();

    }

    public void lancer()
    {
        //vérifie qu'une source de neutrons est spécifié avant de démarer le réacteur;
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
        //insère les barres de contrôle
        cin.barreControle();
    }

    public void swap()
    {
        //changement de canvas à l'appuie du bouton
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
