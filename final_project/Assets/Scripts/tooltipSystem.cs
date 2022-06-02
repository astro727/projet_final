using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tooltipSystem : MonoBehaviour
{
    private static tooltipSystem current;

    public tooltip Tooltip;

    public void Awake()
    {
        current = this;
    }

    public static void Show(string content, string header = "")
    {
        //control l'affichage
        current.Tooltip.SetText(content, header);
        current.Tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        //controle la fereture
        current.Tooltip.gameObject.SetActive(false);
    }
}
