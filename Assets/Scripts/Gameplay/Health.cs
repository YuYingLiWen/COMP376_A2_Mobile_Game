
public class Health 
{
    private int hp;

    public Health() { hp = 1; }
    public Health(int health) { hp = health; }

    public int GetHP() => hp;
    public void TakeDamage(int damage) => hp -= damage;
    public bool IsAlive() => hp <= 0;

}
