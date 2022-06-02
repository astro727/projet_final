using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tooltipSystem : MonoBehaviour
{
    private static tooltipSystem current;

    public tooltip Tooltip;

    // Permet de déterminier la souris est sur quel tooltip
    public void Awake()
    {
        current = this;
    }

    //Cette fonction permet d'afficher le texte quand le curseur survole par dessus.
    public static void Show(string content, string header = "")
    {
        current.Tooltip.SetText(content, header);
        current.Tooltip.gameObject.SetActive(true);
    }

    // Cette fonction permet de cacher le texte quand le curseur n'est plus sur le texte
    public static void Hide()
    {
        current.Tooltip.gameObject.SetActive(false);
    }
}
