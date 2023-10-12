using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName ="ScriptableObjects/Actors")]
public class SOEnemies : ScriptableObject
{
    public string Name;
    public int Hp;
    public int Speed;
    public Mesh Model;
}
