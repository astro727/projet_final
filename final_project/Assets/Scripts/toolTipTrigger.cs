using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class toolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string header;

    public string content;

    private bool time = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(delayedCall());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //désactive la variable bool pour annuler l'affichage si l'utilisateur quite la zone
        time = false;
        tooltipSystem.Hide();
    }
     
    IEnumerator delayedCall()
    {
        //ajoute un délai court avant l'affichage
        time = true;
        yield return new WaitForSeconds(0.5f);
        if(time == true)
            tooltipSystem.Show(content, header);

        yield return null;
    }
}
