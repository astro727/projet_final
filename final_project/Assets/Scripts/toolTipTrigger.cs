using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class toolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string header;

    public string content;

// Permet de choisir l'action lorsque l'utilisateur mets son curseur sur le texte. Dans notre cas, nous avons d�cid� d'afficher un tooltip
    private bool time = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(delayedCall());
    }

    // Permet de choisir l'action lorsque l'utilisateur enl�ve son curseur sur le texte. Dans notre cas, nous avons d�cid� d'enlever le tooltip
    public void OnPointerExit(PointerEventData eventData)
    {
        //d�sactive la variable bool pour annuler l'affichage si l'utilisateur quite la zone
        time = false;
        tooltipSystem.Hide();
    }
<<<<<<< HEAD

    // Permet de choisir le delai d'affichage apr�s avoir mis notre curseur
=======
     
>>>>>>> d05c401c552a097e5c7284f6e090505cc6ade832
    IEnumerator delayedCall()
    {
        //ajoute un d�lai court avant l'affichage
        time = true;
        yield return new WaitForSeconds(0.5f);
        if(time == true)
            tooltipSystem.Show(content, header);

        yield return null;
    }
}
