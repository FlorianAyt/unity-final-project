using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title_Script : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioSource hautParleur;


    public AudioClip[] sons;
    void Start()
    {
        hautParleur = GetComponent<AudioSource>();
        hautParleur.volume = 0.6f;
        hautParleur.playOnAwake = true;
        hautParleur.loop = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            StartCoroutine(LoadGame());
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            StartCoroutine(ReturnToMenu());
        }
    }

    IEnumerator LoadGame(){
    
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("AppleCatcher");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    IEnumerator ReturnToMenu()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MenuPrincipal");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
