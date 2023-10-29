
using UnityEngine;

[CreateAssetMenu(fileName = "Actor", menuName ="ScriptableObjects/Actors")]
public class SOActor : ScriptableObject
{
    public string Name;
    public string Tag;
    public string Layer;

    public int HitPoints;
    public int AttackPoints;
    public int Speed;
    public Sprite sprite;
    public Color color;
}
