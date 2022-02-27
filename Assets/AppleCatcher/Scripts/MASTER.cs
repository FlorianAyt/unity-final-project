using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MASTER : MonoBehaviour
{
    public GameObject pomme_prefab;
    public TextMeshPro text_TMP;


    private float MIN_X = -8.5f;
    private float MAX_X = 8.5f;
    private int score = 0;
    private float gameTimer = 20f;
    private float timer = 1.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GenerateApples();
        CheckTimer();
    }

    //Méthode pour augmenter le score
    public void IncreaseScore()
    {
        score++;
        text_TMP.SetText("Score:" + score);
    }



    //Méthode pour générer des pommes de façon aléatoire
    private void GenerateApples()
    {
        float randomValue = Random.Range(0, 1f);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (randomValue <= 0.5)
            {
                float interval = MAX_X - MIN_X;
                float posX = (MIN_X) + (interval * Random.value);
                GameObject newApple = Instantiate(pomme_prefab);
                newApple.transform.position = new Vector3(posX, 6.0f, 0);
                newApple.name = "pomme";
                timer = 1.5f;
            }
        }
    }

    //Permet de retourner à la page d'accueil du jeu.
    IEnumerator ReturnToTitle()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Title");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    //Vérifies le temps écoulé et retourne au titre après 120 secondes
    private void CheckTimer()
    {
        gameTimer -= Time.deltaTime;
        if (gameTimer == 0)
        {
            StartCoroutine(ReturnToTitle());
        }
    }
}
