
using UnityEngine;

public interface IActor
{
    void TakeDamage(int damage, Vector3 at, Vector3 up);
    void SpawnBlood(Vector3 at, Vector3 up);
    Transform GetTransform();
}