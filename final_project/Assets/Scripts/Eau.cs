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
    

    // Start is called before the first frame update
    void Start()
    {
        spawnObject();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnObject()
    {
        Vector3 position = transform.position;
        position.x = position_X;
        position.y = position_Y;
        position.z = position_Z;

        Instantiate(tuyau, position, Quaternion.Euler(0f,0f,90f));
    }

    void moveObject()
    {
        Vector3 position = transform.position;
        position.x -= speed * Time.deltaTime;
        transform.position = position;
    }
}
