using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public partial class GameObjectPooler : MonoBehaviour
{
    private GameObjectPooler instance = null;


    private ObjectPool<GameObject> poolObject1;

    private void Awake()
    {
        if(!instance) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        poolObject1 = new ObjectPool<GameObject>(OnCreateObj1, OnGetObj1, OnReleaseObj1,OnDestroyObj1,true,90) ;

    }

    
}

