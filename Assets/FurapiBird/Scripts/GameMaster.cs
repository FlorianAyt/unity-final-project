using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public TextMeshPro scoreText;
    public GameObject gameOverObject;
    public GameObject PipeObstacle;
    public GameObject PipeObstacleRed;
    public GameObject PipeObstcalePurple;
    public GameObject PipeObstacleYellow;


    public bool gameIsOver = false;

    private float _score = 0;
    private float _timer = 2f;
    private float _maxY = 4f;
    private float _minY = -4f;
    private float _timerOldValue = 2f;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;

        float randomNumber = Random.Range(0, 0.4f);
        float randomNumberForObstacleGeneration = Random.Range(0, 1.1f);

        if (_timer <= 0 && gameIsOver == false)
        {
            if (randomNumberForObstacleGeneration <= 0.25)
            {
                GenerateObstacles(randomNumber, PipeObstacle);
            }
            else if (randomNumberForObstacleGeneration <= 0.5f)
            {
                GenerateObstacles(randomNumber, PipeObstacleRed);
            }
            else if (randomNumberForObstacleGeneration <= 0.75f)
            {
                GenerateObstacles(randomNumber, PipeObstacleYellow);
            }
            else if (randomNumberForObstacleGeneration <= 1f)
            {
                GenerateObstacles(randomNumber, PipeObstcalePurple);
            }
            IncreaseScore();
            _timer = _timerOldValue;
        }
        RestartGame();
        ReturnToLaunchScreen();

    }

    public void BirdDied()
    {
        _score = 0;
        SetScore(_score);
        gameOverObject.SetActive(true);
        gameIsOver = true;
    }

    //Méthode pour augmenter le score
    private void IncreaseScore()
    {
        if (gameIsOver == false)
        {
            _score++;
            SetScore(_score);
        }

    }

    private void SetScore(float score)
    {
        scoreText.SetText("Score: " + score);
    }

    IEnumerator ReturnToFurapiBirdLaunchScreen()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("FurapiBirdLaunchScreen");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    //Cette méthode permet de relancer le jeu 
    private void RestartGame()
    {
        if (gameIsOver == true)
        {
            if (Input.GetKey(KeyCode.R) || Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void ReturnToLaunchScreen()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            StartCoroutine(ReturnToFurapiBirdLaunchScreen());
        }
    }

    
    // A ce niveau on voulait augmenter la vitesse d'apparition
    // des obstacles mais on a été confronté à des erreurs avec l'utilisation de Time.deltaTime et du Timer utilisé au début du code
    private void IncreaseObstacleSpeed()
    {
        if (_timerOldValue <= 1.5f)
        {
            _timerOldValue = 1.5f;
        }
        else
        {
            _timerOldValue -= 0.1f;
        }
    }

    //Méthode permettant de générer un obstacle de façon aléatoire
    private void GenerateObstacles(float random_number, GameObject Obstacle)
    {
        float obstacle_position_x = 9.041094f;
        float interval = _maxY - _minY;

        float obsatcle_position_y = interval * random_number;

        GameObject newPipeObstacle = Instantiate(Obstacle);
        newPipeObstacle.transform.position = new Vector3(obstacle_position_x, obsatcle_position_y, 0);
        newPipeObstacle.name = "Obstacle";
    }

}
