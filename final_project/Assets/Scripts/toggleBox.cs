using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleBox : MonoBehaviour
{
    public GameObject hemisphere;
    bool actif = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void vueCoup()
    {
        if (actif)
        {
            hemisphere.SetActive(false);
            actif = false;
        }
        else
        {
            hemisphere.SetActive(true);
            actif = true;
        }
    }
}
