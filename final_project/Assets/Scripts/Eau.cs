using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eau : MonoBehaviour
{

    public GameObject tuyau;
    public float speed = 10.0f;
    public float position_X = 0f;
    public float position_Y = 80.7f;
    public float position_Z = -245.1f;
    [SerializeField] Vector3[] startPosition;
    [SerializeField] Quaternion[] rotation;
    [SerializeField] float spawnTime = 2f;

    int pos = 0;
    int lenght;
    float t = 0f;
    float elapsed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //spawnObject(0);

        
    }

    // Update is called once per frame
    void Update()
    {
        
        elapsed += Time.deltaTime;
        if (elapsed >=0.3f)
        {
            elapsed = elapsed % 0.3f;
            spawnObject(0);
            spawnObject(1);

        }
           
    }

    void spawnObject(int pos)
    {
        // lenght = deplacement.Length;
        Vector3 position = startPosition[pos];
        //Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
       // position.x = position_X;
        //position.y = position_Y;
        //position.z = position_Z;

        Instantiate(tuyau, position, Quaternion.Euler(-90f,0f,48f));
        //Instantiate(tuyau, position, Quaternion.Euler(rotation.x, 0f, rotation.z));
    }

    void moveObject()
    {
        Vector3 position = transform.position;
        position.x -= speed * Time.deltaTime;
        transform.position = position;
    }
}
