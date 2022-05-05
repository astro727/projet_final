using System.Collections;
using System.Collections.Generic;
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
    }
}
