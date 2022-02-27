using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pomme : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.name == "Catchboy"){
            Destroy(gameObject);
        }
    }
}
