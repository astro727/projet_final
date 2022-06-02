using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class tooltip : MonoBehaviour
{

    public TextMeshProUGUI headerField;

    public TextMeshProUGUI contentField;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    public RectTransform rectTransform;

    private void Awake()
    {
        //optien la composante de position du tooltip
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string content, string header = "")
    {
        //assigne l'entête si il y alieu
        if(string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        //assigne le texte
        contentField.text = content;

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;
        //vérifie si le texte doit modifier la largeur du tooltip, ou s'il doit suivre la largeur assigner 
        layoutElement.enabled = (headerLength > (characterWrapLimit / 1.5) || contentLength > characterWrapLimit) ? true : false;
    }

    private void Update()
    {
        //permet l'affichage in editor
        if (Application.isEditor)
        {
            int headerLength = headerField.text.Length;
            int contentLength = contentField.text.Length;

            layoutElement.enabled = (headerLength > (characterWrapLimit / 1.5) || contentLength > characterWrapLimit) ? true : false;
        }
        //obtien la position du curseur
        Vector2 position = Input.mousePosition;

        //position pivot pour rester sur l'écran
        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        //assigner position
        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }
}
