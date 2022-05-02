using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vague : MonoBehaviour
{
    public GameObject tuyau;
    public GameObject deleteBox;
    public float speed = 10.0f;
    public float position_X = 0f;
    public float position_Y = 96f;
    public float position_Z = -245.1f;
    public float destroy_X = 0f;
    public float destroy_Y = 0f;
    public float destroy_Z = 0f;



    [SerializeField] Vector3[] deplacement;
    [SerializeField] [Range(0f, 100f)] float lerpTime;
    public float elapsedTime;
    public float desiredDuration = 5f;
    private Vector3 startPosition;

    int pos = 0;
    int lenght;
    float t = 0f;


    // public Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        lenght = deplacement.Length;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveObject();
        //destroyGameObject();
    }

    void moveObject()
    {
        elapsedTime += Time.deltaTime;
        float percentageChange = elapsedTime / lerpTime;
        transform.position = Vector3.Lerp(startPosition, deplacement[pos], percentageChange);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if(t>.9f)
        {
            t = 0f;
            pos++;
            pos = (pos >= lenght) ? 0 : pos;
        }
       
        
        
        Vector3 position = transform.position;
        float speed_x = Mathf.Abs(position_X / position_Y);
        float speed_y = Mathf.Abs(position_Y / position_X);

        //position.x -= speed_x * speed * Time.deltaTime;
       // position.z -= speed_y * speed * Time.deltaTime;

        
//position.x -= speed * Time.deltaTime;
  //position.z -= speed * Time.deltaTime;


        //transform.position = position;
    }

    void destroyGameObject()
    {
        Vector3 position1 = transform.position;
        if (position1.x < destroy_X)
        {
            Destroy(this.gameObject);
        }
            
    }

}
