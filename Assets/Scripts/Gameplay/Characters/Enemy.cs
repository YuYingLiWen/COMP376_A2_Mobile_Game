using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IActor
{
    [SerializeField] private SOEnemies so;

    private Health health;


    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void TakeDamage(int damage)
    {
        //health.TakeDamage(damage);


        /*if(!health.IsAlive())
        {

        }
        else
        {

        }*/
    }

    public void SpawnBlood(Vector3 at, Vector3 up)
    {
        GameObject blood = BloodPooler.Instance.Pool.Get();
        blood.transform.up = at - up;
        blood.transform.position = at;
    }
}
