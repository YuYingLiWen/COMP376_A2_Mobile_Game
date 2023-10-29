using UnityEngine;

public class Health :MonoBehaviour
{
    [SerializeField] private int points;

    public void SetPoints(int points) { this.points = points; }

    public int GetPoints() => points;
    public void TakeDamage(int damage)
    {
        points -= damage;
    }
        
    public bool IsAlive() => points > 0;

}
