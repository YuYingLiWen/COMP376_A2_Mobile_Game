using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// In this game, the player will be part of DDOL since not multiplayer game
public class Player : MonoBehaviour
{
    private int attack;

    private Health health;

    private void Awake()
    {
        health = new Health(10);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
