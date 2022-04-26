using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class valeurKinf : MonoBehaviour
{
    public GameObject reacteur;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<TMP_Text>().text = " = " + Math.Round(reacteur.GetComponent<Formule4facteurs>().Kinf, 5);
    }
}
