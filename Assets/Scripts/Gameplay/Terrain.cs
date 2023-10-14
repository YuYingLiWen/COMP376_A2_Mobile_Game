using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] private GameObject floorTile;

    private List<GameObject> tiles;

    private void Awake()
    {
        MakeTerrain();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MakeTerrain()
    {
        for(int i = 0; i < 20; i ++)
        {
            for(int j = 0; j < 20; j++)
            {
                var tile = Instantiate(floorTile);
                tile.transform.position += Vector3.right * i + Vector3.forward * j;
                tile.transform.parent = transform;
            }
        }
    }
}
