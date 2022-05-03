using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vague : MonoBehaviour
{
    [SerializeField] Vector3[] deplacement;
    [SerializeField] [Range(0f, 100f)] float lerpTime;
    public float elapsedTime;
    private Vector3 startPosition;
    [SerializeField] Vector3[] destroyPosition;
    [SerializeField] GameObject deleteBox;
    [SerializeField] Vector3 boxPosition;

    int pos = 0;
    int lenght;
    float t = 0f;

    // Start is called before the first frame update
    void Start()
    {
        lenght = deplacement.Length;
        startPosition = transform.position;
        boxPosition = deleteBox.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveObject(pos);
        destroyGameObject();
    }

    void moveObject(int indice)
    {
        elapsedTime += Time.deltaTime;
        float percentageChange = elapsedTime / lerpTime;
        //transform.position = Vector3.Lerp(startPosition, deplacement[indice], percentageChange);
        transform.position = Vector3.Lerp(startPosition, boxPosition, percentageChange);
 
        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if (t > .9f)
        {
            t = 0f;
            pos++;
            pos = (pos >= lenght) ? 0 : pos;
        }
    }

    void destroyGameObject()
    {
        Vector3 position1 = transform.position;
        //if (position1.x == deplacement[pos].x && position1.y == deplacement[pos].y && position1.z == deplacement[pos].z)
        if (position1.x == boxPosition.x && position1.y == boxPosition.y && position1.z == boxPosition.z)
        {
            Destroy(this.gameObject);
        }       
    }
}
