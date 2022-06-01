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
        time = false;
        tooltipSystem.Hide();
    }

    IEnumerator delayedCall()
    {
        time = true;
        yield return new WaitForSeconds(0.5f);
        if(time == true)
            tooltipSystem.Show(content, header);

        yield return null;
    }
}
