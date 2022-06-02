using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ecranAccueil : MonoBehaviour
{
   

    public void play()
    {
        //changement de scène
        SceneManager.LoadScene("Simulateur");
    }

    public void quit()
    {
        //sortie de l'application
        Application.Quit();
    }
}
