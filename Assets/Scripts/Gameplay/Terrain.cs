using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] private GameObject floorTile;
    [SerializeField] private float tileScale = 1.0f;

    [SerializeField] int sizeX = 20;
    [SerializeField] int sizeY = 20;
    private List<GameObject> tiles;

    private void Awake()
    {
        MakeTerrain();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void MakeTerrain()
    {
        for(int i = 0; i < sizeY; i ++)
        {
            for(int j = 0; j < sizeX; j++)
            {
                var tile = Instantiate(floorTile);
                tile.transform.position += Vector3.right * (i * tileScale) + Vector3.forward * (j * tileScale);
                tile.transform.parent = transform;
                tile.transform.localScale = Vector3.one * tileScale;
            }
        }
    }
}
