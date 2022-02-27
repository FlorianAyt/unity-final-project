using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panier : MonoBehaviour
{
    //CONSTANT
    private const float SPEED = 8.0f;
    private float MAX_X = 7.6f;
    private float MIN_X = -7.6f;

    public Animator ref_animator;

    //Récupération d'une source audio
    private AudioSource speaker;


    public AudioClip[] audios;

    //Reference au master pour pouvoir en utiliser les méthodes
    public MASTER refMaster;

    private float previousSpeed = 0;


    // Start is called before the first frame update
    void Start()
    {
        speaker = GetComponent<AudioSource>();
        ref_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBasket();
        CheckBasketPosition();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.name == "pomme")
        {
            speaker.volume = 0.6f;
            speaker.clip = audios[0];
            speaker.loop = false;
            speaker.Play();
            refMaster.IncreaseScore();
        }

    }

    //Méthode pour déplacer le panier de gauche à droite et vice-versa
    private void MoveBasket()
    {
        float newSpeed = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            newSpeed = SPEED;
            if (newSpeed != previousSpeed)
            {
                ref_animator.SetTrigger("Droite");
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            newSpeed = -SPEED;
            if (newSpeed != previousSpeed)
            {
                ref_animator.SetTrigger("Gauche");
            }
        }
        else
        {
            if (newSpeed != previousSpeed)
            {
                ref_animator.SetTrigger("Immobile");
            }
        }
        transform.Translate(newSpeed * Time.deltaTime, 0, 0);
    }
    
    //Méthode vérifiant la position du panier pour empêcher qu'il ne sorte de la scène de jeu

    private void CheckBasketPosition()
    {
        if (gameObject.transform.position.x < MIN_X)
        {
            float pos_y = transform.position.y;
            transform.position = new Vector2(MIN_X, pos_y);
        }
        else if (gameObject.transform.position.x > MAX_X)
        {
            float pos_y = transform.position.y;
            transform.position = new Vector2(MAX_X, pos_y);
        }
    }
}
