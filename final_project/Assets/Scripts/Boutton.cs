using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boutton : MonoBehaviour
{
    public GameObject setup;
    public GameObject fonctionnement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void swap()
    {
        fonctionnement.SetActive(true);
        setup.SetActive(false);
    }
}
