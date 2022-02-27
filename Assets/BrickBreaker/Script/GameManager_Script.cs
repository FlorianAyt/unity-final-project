using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_Script : MonoBehaviour
{

    private float min_x = -8.5f;
    private float min_y = -3f;
    private float max_x = 8.5f;
    private float max_y = 3.5f;
    private float brick_size_x = 1.76f;
    private float brick_size_y = 0.9f;
    private int rows;
    private int columns;
    // private float spacing_x = 0.5f;
    // private float spacing_y = 0.5f;
    private System.Random random = new System.Random();

    public BallScript bs;

    public int lives;
    public int score;

    public Text Tentative;
    public Text scoreText;
    public Text recordText;
    public Text niv;
    public InputField recordInput;
    public bool gameOver;
    public GameObject gameOverPanel;
    public GameObject loadLevelPanel;
    public GameObject brick_Prefab;
    public int nbreBricks;
    public Transform[] levels;
    public int niveauActIndex = 0;
    public int nive;

    //   public GameObject brickPrefab_2;


    // Start is called before the first frame update
    void Start()
    {
        nive = niveauActIndex + 1;
        Tentative.text = "Vies : " + lives;
        scoreText.text = "Score : " + score;
        niv.text = "Niveau : " + nive;
        GenererBriques();
        nbreBricks = GameObject.FindGameObjectsWithTag("brick").Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("Accueil Brick");
        }
    }

    public void GenererBriques()
    {
        RéinitialiserBricks();

        for (float y = min_y; y < max_y; y += brick_size_y)
        {
            for (float x = 0; x < max_x; x += brick_size_x)
            {
                float tmp = Random.Range(0, 1.1f);
                if (tmp >= 0.4f)
                {
                    if (tmp >= 0.5f)
                    {
                        Vector2 spawnPosition = new Vector2(x, y);
                        GameObject newBrick = Instantiate(brick_Prefab, spawnPosition, Quaternion.identity);
                        newBrick.GetComponent<Renderer>().material.color = new Color(4 * Random.value, 4 * Random.value, Random.value);
                        newBrick.name = "Brick";

                        Vector2 spawnPosition2 = new Vector2(-x, y);
                        GameObject newBrick2 = Instantiate(brick_Prefab, spawnPosition2, Quaternion.identity);
                        newBrick2.GetComponent<Renderer>().material.color = new Color(4 * Random.value, 4 * Random.value, Random.value);
                        newBrick2.name = "Brick";

                        nbreBricks += 2;
                    }
                    else if(tmp>=0.8f)
                    {
                        Vector2 spawnPosition = new Vector2(x, y);
                        GameObject newBrick = Instantiate(brick_Prefab, spawnPosition, Quaternion.identity);
                        newBrick.GetComponent<Renderer>().material.color = new Color(1 * Random.value, 1 * Random.value, Random.value);
                        newBrick.name = "Brick";

                        Vector2 spawnPosition2 = new Vector2(-x, y);
                        GameObject newBrick2 = Instantiate(brick_Prefab, spawnPosition2, Quaternion.identity);
                        newBrick2.GetComponent<Renderer>().material.color = new Color(1 * Random.value, 1 * Random.value, Random.value);
                        newBrick2.name = "Brick";

                        nbreBricks += 2;
                    }
                    else
                    {
                        Vector2 spawnPosition = new Vector2(x, y);
                        GameObject newBrick = Instantiate(brick_Prefab, spawnPosition, Quaternion.identity);
                        newBrick.GetComponent<Renderer>().material.color = new Color(1 * Random.value, 1 * Random.value, Random.value);
                        newBrick.name = "Brick";

                        Vector2 spawnPosition2 = new Vector2(-x, y);
                        GameObject newBrick2 = Instantiate(brick_Prefab, spawnPosition2, Quaternion.identity);
                        newBrick2.GetComponent<Renderer>().material.color = new Color(1 * Random.value, 1 * Random.value, Random.value);
                        newBrick2.name = "Brick";

                        nbreBricks += 2;
                    }
                }
            }
        }
    }

    private void RéinitialiserBricks()
    {
        nbreBricks = 0;
    }
    public void UpdateLives(int changeInLives)
    {
        lives += changeInLives;

        //Vérifier s'il y'a une vie restante sinon signaler game Over

        if (lives <= 0)
        {
            lives = 0;
            GameOver();
        }

        Tentative.text = "Vies : " + lives;
    }

    public void UpdateScore(int sco)
    {
        score += sco;
        scoreText.text = "Score : " + score;
    }

    public void UpdateNiveau(int aug)
    {
        nive += aug;
        niv.text = "Niveau : " + nive;
    }

    public void UpdateNbreBricks()
    {
        nbreBricks--;
        if (nbreBricks <= 0)
        {
            if (niveauActIndex >= levels.Length - 1)
            {
                GameOver();
            } else
            {
                loadLevelPanel.SetActive(true);
                loadLevelPanel.GetComponentInChildren<Text>().text = "Niveau " + (niveauActIndex + 2);
                gameOver = true;
                bs.transform.position = bs.paddle.position;
                bs.rigid.velocity = Vector2.zero;
                bs.debut = false;

                //  bille = gameObjectWithTag("ball");
                // Destroy(bs.gameObject);
                Invoke("LoadLevel", 0.75f);
            }
        }
    }


    void LoadLevel()
    {
        niveauActIndex++;
        Instantiate(levels[niveauActIndex], Vector2.zero, Quaternion.identity);
        nbreBricks = GameObject.FindGameObjectsWithTag("brick").Length;
        gameOver = false;
        loadLevelPanel.SetActive(false);
        //GenererBriques();
        bs.vitesse += 40;

        if (Input.GetButtonDown("Jump") && !bs.debut)
        {
            bs.debut = true;
            bs.rigid.AddForce(Vector2.up * bs.vitesse);
        }
        UpdateNiveau(1);
    }

    void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        int highscore = PlayerPrefs.GetInt("HIGHSCORE");
        if (score > highscore)
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);
            recordText.text = "Nouveau Record!! " + "\n" + "Entrer votre Pseudo";

            recordInput.gameObject.SetActive(true);
        }
        else
        {
            recordText.text = "Le record de " + PlayerPrefs.GetString("HIGHSCORENAME") + " était : " + highscore + "\n" + "A vous de faire mieux ^_~ ";
            recordInput.gameObject.SetActive(false);
        }
    }

    public void NouveauRecord()
    {
        string recordman = recordInput.text;
        PlayerPrefs.SetString("HIGHSCORENAME", recordman);
        recordInput.gameObject.SetActive(false);
        recordText.text = "Félicitations!!! " + recordman + "\n" + "Votre Nouveau Record est : " + score;
    }

    public void NouvellePartie()
    {
        SceneManager.LoadScene("Breaker");
    }

    public void Quitter()
    {
        SceneManager.LoadScene("Accueil Brick");
    }

   
}
