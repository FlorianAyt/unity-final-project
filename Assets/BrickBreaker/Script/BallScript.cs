using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{

    public Rigidbody2D rigid;
    public bool debut;
    public Transform paddle;
    public float vitesse;
    public Transform impact;
    public GameManager_Script gm;
    public Transform coin;
    public Transform coeur;
    public AudioSource audio;
    public AudioClip[] audios;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D> ();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.gameOver)
        {
            return;
        }
        if(!debut)
        {
            transform.position = paddle.position;
        }

        if(Input.GetButtonDown ("Jump") && !debut)
        {
            debut = true;
            rigid.AddForce(Vector2.up * vitesse);
        }

      /*  if(gm.LoadLevel)
        {
         //   vitesse += 20;
        }*/
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag ("bas"))
        {
            rigid.velocity = Vector2.zero;
            debut = false;
            gm.UpdateLives(-1);

            PlayAudio(3);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.CompareTag("brick"))
        {
            Brick_Script brickscript = other.gameObject.GetComponent<Brick_Script> ();
            
            if (brickscript.Toucher > 1 )
            {
                brickscript.CasserBrick();
            } else {

            float randChance = Random.Range(0,1f);
            float Aide = Random.Range(0,0.1f);
            if(randChance == 0.2f)
            {
                Instantiate(coin, other.transform.position, other.transform.rotation);
            }

            if (Aide == 0.08f)
            {
                Instantiate(coeur, other.transform.position, other.transform.rotation);
            }

                Transform newImpact = Instantiate(impact, other.transform.position, other.transform.rotation);
            Destroy(newImpact.gameObject, 2.5f);

            gm.UpdateScore (brickscript.sco);
            gm.UpdateNbreBricks ();

            Destroy(other.gameObject);


            }

            PlayAudio(0);

        }
        if(other.transform.CompareTag("wall"))
        {
            /**/
            PlayAudio(1);
           // Debug.Log("son joue");
        }
        
    }

    void PlayAudio(int index)
    {
        audio.volume = 1f;
        audio.clip = audios[index];
        audio.loop = false;
        audio.Play();
    }
}
