using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoPlayerScript : MonoBehaviour
{
    public AudioSource highSpeaker;

    public AudioClip[] audios;
    // Start is called before the first frame update
    void Start()
    {
        highSpeaker = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadFurapiBirdGame()
    {
        PlayGameSelectionAudioClip();
        StartCoroutine(LoadMyScene("FurapiBirdLaunchScreen"));
    }

    public void LoadBrickBreakerGame()
    {
        PlayGameSelectionAudioClip();
        StartCoroutine(LoadMyScene("BrickBreakerLaunchScreen"));
    }

    public void LoadAppleCatcherGame()
    {
        PlayGameSelectionAudioClip();
        StartCoroutine(LoadMyScene("AppleCatcherLaunchScreen"));
    }

    IEnumerator LoadMyScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void PlayGameSelectionAudioClip()
    {
        highSpeaker.volume = 0.5f;
        highSpeaker.clip = audios[0];
        highSpeaker.loop = false;
        highSpeaker.Play();
    }
}
