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
        Vector3 position = startPosition[pos];
        Instantiate(tuyau, position, Quaternion.Euler(rotation[0], rotation[1], rotation[2]));
    }
}
