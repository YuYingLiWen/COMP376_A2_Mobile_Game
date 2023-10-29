
using UnityEngine;

public interface IActor
{
    void TakeDamage(int damage);
    void SpawnBlood(Vector3 at, Vector3 up);
}