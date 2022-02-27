using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{

    public float vitesse;
    public float limit_D;
    public float limit_G;
    public GameManager_Script gm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.gameOver)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * horizontal * Time.deltaTime * vitesse);

        if(transform.position.x < limit_G) 
        {
            transform.position = new Vector2 (limit_G, transform.position.y);
        }

        if (transform.position.x > limit_D)
        {
            transform.position = new Vector2(limit_D, transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("bonus"))
        {

        gm.UpdateScore(5);
        Destroy(other.gameObject);

        }

        if(other.CompareTag("vie"))
        {

            gm.UpdateLives(1);
            Destroy(other.gameObject);

        }
    }
}
