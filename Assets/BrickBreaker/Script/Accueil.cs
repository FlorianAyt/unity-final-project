using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Accueil : MonoBehaviour
{

    public Text recordgame;

    void Start()
    {
        if (PlayerPrefs.GetString("HIGHSCORENAME") != "")
        {
            recordgame.text = PlayerPrefs.GetString("HIGHSCORENAME") + " détient le record du Jeu : " + PlayerPrefs.GetInt("HIGHSCORE");
        }

        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
           GoToMenuPrincippal();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            GoToInstructions();
        }

    }

    public void GoToInstructions()
    {
        SceneManager.LoadScene("Accueil Brick");
    }
   /**/ public void GoToMenuPrincippal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    } 

    public void LancerJeu()
    {
        SceneManager.LoadScene("Breaker");
    }
    
    IEnumerator ReturnToLauncher()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("BrickBreakerLaunchScreen");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    
}
