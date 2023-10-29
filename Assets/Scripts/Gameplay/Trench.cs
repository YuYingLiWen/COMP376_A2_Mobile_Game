
using System.Collections.Generic;

using UnityEngine;


public class Trench: MonoBehaviour
{
    private static List<IActor> actors;

    public static Player player;

    public static IActor GetRandomActor()
    {
        return actors[Random.Range(0, actors.Count - 1)];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ally"))
        {
            actors.Add(collision.GetComponent<Ally>());
            Debug.Log("New Ally join the trench");
        }
        else if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            Debug.Log("New Ally join the trench");
        }
    }
}
