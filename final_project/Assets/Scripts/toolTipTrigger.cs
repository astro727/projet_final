using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class toolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string header;

    public string content;

// Permet de choisir l'action lorsque l'utilisateur mets son curseur sur le texte. Dans notre cas, nous avons décidé d'afficher un tooltip
    private bool time = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(delayedCall());
    }

    // Permet de choisir l'action lorsque l'utilisateur enlève son curseur sur le texte. Dans notre cas, nous avons décidé d'enlever le tooltip
    public void OnPointerExit(PointerEventData eventData)
    {
        time = false;
        tooltipSystem.Hide();
    }

    // Permet de choisir le delai d'affichage après avoir mis notre curseur
    IEnumerator delayedCall()
    {
        time = true;
        yield return new WaitForSeconds(0.5f);
        if(time == true)
            tooltipSystem.Show(content, header);

        yield return null;
    }
}
