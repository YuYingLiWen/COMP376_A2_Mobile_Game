using UnityEngine;

public class Health :MonoBehaviour
{
    private int maxHealth;
    private int points;

    public void SetPoints(int points) 
    { 
        this.points = points;
        maxHealth = points;
    }

    public int GetPoints() => points;
    public void TakeDamage(int damage)
    {
        points -= damage;
    }

    public float GetHealthPercent() => (float)points / (float)maxHealth;

    public bool IsAlive() => points > 0;

}
