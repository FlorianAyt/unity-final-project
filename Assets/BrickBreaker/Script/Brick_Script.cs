using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick_Script : MonoBehaviour
{
    public int sco;
    public int Toucher;
    public Sprite SpriteCasser;

    public void CasserBrick()
    {
        Toucher--;
        GetComponent<SpriteRenderer>().sprite = SpriteCasser;
    }
   
}
