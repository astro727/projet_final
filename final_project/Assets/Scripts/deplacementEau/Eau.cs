using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eau : MonoBehaviour
{
    // Ce script doit �tre plac� dans chaque cube de d�part.,
    // On doit ajouter le bon prefab qui correspond au type de tuyau, soit chaud ou froid.
    public GameObject tuyau;
    [SerializeField] Vector3[] startPosition;
    [SerializeField] float[] rotation;

    int pos = 0;
    float t = 0f;
    float elapsed = 0f;
    public float timeIntervalle = 0.3f;


    // Update is called once per frame
    // Cette fontion permet de cr�er des objets avec un intervalle donn�
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= timeIntervalle)
        {
            elapsed = elapsed % timeIntervalle;
            spawnObject(pos);
        }
           
    }

    // Cette fonction permet de cr�er les cylindres pour repr�senter le d�placement de l'eau du r�acteur.
    void spawnObject(int pos)
    {
        // lenght = deplacement.Length;
        Vector3 position = startPosition[pos];
        //Vector3 position = transform.position;
        //Quaternion rotation = transform.rotation;


        //if(tuyau.name == "eau_FDG")
        //{
        //    Instantiate(tuyau, position, Quaternion.Euler(rotation[pos], rotation[pos], rotation[pos]));
        //}

        Instantiate(tuyau, position, Quaternion.Euler(rotation[0], rotation[1], rotation[2]));
        //Instantiate(tuyau, position, Quaternion.Euler(-90f,0f,48f));
        //Instantiate(tuyau, boxPosition, Quaternion.Euler(-90f,0f,48f));
        //Instantiate(tuyau, position, Quaternion.Euler(rotation.x, 0f, rotation.z));
    }
}
