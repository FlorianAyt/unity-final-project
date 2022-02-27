using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
    public GameMaster refGameMaster;
    public AudioSource speaker;
    public AudioClip[] audios;

    private Rigidbody2D _rigidbody;

    private const float JumpAmount = 200f;
    private float _minimalYValue = -4.5f;
    private float _maximalYValue = 4.5f;
    private float _gameTimer = 5f;

    private bool _isAlive = true;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speaker = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAlive == true)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.AddForce(new Vector2(0, JumpAmount));
                animator.SetTrigger("Fly");
            }
        }
        ReturnBirdOnScene();

    }

    //Méthode pour garder l'oiseau sur la scène et vérifier sa position
    private void ReturnBirdOnScene()
    {
        float birdPositionX = gameObject.transform.position.x;
        if (gameObject.transform.position.y >= _maximalYValue)
        {
            transform.position = new Vector2(birdPositionX, _maximalYValue);
        }
        else if (gameObject.transform.position.y <= _minimalYValue)
        {
            transform.position = new Vector2(birdPositionX, _minimalYValue);
            BirdDiedActions();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Obstacle")
        {
           BirdDiedActions();
        }
    }

    //Les actions lancées quand l'oiseau meurt
    void BirdDiedActions()
    {
        _isAlive = false;
        if (_isAlive == false)
        {
            refGameMaster.BirdDied();
        }
        animator.SetTrigger("Die");
        PlayAudiosOnCollisionWithObstacles();
      
    }

    //Méthode pour jouer les jingles de collision ensuite de mort
    //Mais on a rencontré un souci à ce  niveau vous l'aurez remarqué il crée un bug quand on perd.
    void PlayAudiosOnCollisionWithObstacles()
    {
        AudioClip impactSound = audios[0];
        speaker.volume = 1f;
        speaker.PlayOneShot(impactSound);
        if (speaker.isPlaying)
        {
            speaker.clip = audios[1];
            speaker.Play();
        }
    }

}
