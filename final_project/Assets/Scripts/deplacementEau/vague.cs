using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vague : MonoBehaviour
{
    // Pour que le script fonctionne, il faut trouver la position du cube d'arriv�
    // Pour ce faire, il faut juste placer le cube � l'endroit souhaiter, et ensuite
    // Mettre la postion dans la liste de vecteur du script qui va �tre sur chaque objet.

    [SerializeField] Vector3[] deplacement;
    [SerializeField] [Range(0f, 100f)] float lerpTime;
    public float elapsedTime;
    private Vector3 startPosition;
    
    int pos = 0;
    int lenght;
    float t = 0f;

    // Start is called before the first frame update
    // Cette fonction permet r�cup�rer la distance que le cylindre doit parcourir ainsi 
    // que �a position de d�part
    void Start()
    {
        lenght = deplacement.Length;
        startPosition = transform.position;
    }

    // Update is called once per frame
    // Cette fonction permet de faire fonctionner le d�placement de l'eau dans le r�acteur
    // en fessant sorte que le cylindre se d�place et se d�truise quand il arrive � l'arriv�e
    void Update()
    {
        moveObject(pos);
        destroyGameObject();
    }

    // Cette fonction permet de faire bouger les cylindres pour simuler le d�placement d'eau
    void moveObject(int indice)
    {
        // Les deux lignes suivantes permettent de d�terminer la dur�e du d�placement totale du cylindre pour qu'elle corresponde
        // � la vitesse souhaiter.
        elapsedTime += Time.deltaTime;
        float percentageChange = elapsedTime / lerpTime;

        // La fonction Lerp des Vecteurs est utilis�e pour d�placer l'objet � sa position de d�part ainsi que
        // que de d�placer l'objet � sa position finale selon une vitesse prescrit
        transform.position = Vector3.Lerp(startPosition, deplacement[indice], percentageChange);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if (t > .9f)
        {
            t = 0f;
            pos++;
            pos = (pos >= lenght) ? 0 : pos;
        }
    }

    // Cette fonction permet de supprimer le clone cr�� lorsque l'objet est arriv� � sa position finale
    void destroyGameObject()
    {
        // permet d'obtenir l'emplacement de l'objet
        Vector3 position1 = transform.position;
        
        // permet de d�truire l,
        if (position1.x == deplacement[pos].x && position1.y == deplacement[pos].y && position1.z == deplacement[pos].z)
        {
            Destroy(this.gameObject);
        }       
    }
}
