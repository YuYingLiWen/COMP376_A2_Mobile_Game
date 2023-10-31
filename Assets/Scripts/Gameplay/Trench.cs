
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;


public class Trench: MonoBehaviour
{
    private static List<IActor> actors;

    public static Player player;

    const int takenOverNum = 5;
    int currentCount = 0;

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
        else if (collision.CompareTag("Enemy"))
        {
            currentCount += 1;
            if (currentCount >= takenOverNum) FindAnyObjectByType<LevelManager>().GameOver();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            currentCount = Mathf.Max(0, currentCount - 1);
        }
    }
}
