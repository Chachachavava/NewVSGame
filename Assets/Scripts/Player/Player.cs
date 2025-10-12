using System.Diagnostics;

public class Player : Entity
{
    protected override void Die()
    {
        Debug.Print("Die");
        health = healthMax;
    }

    void Start()
    {
        healthMax = 100;
        health = healthMax;
    }
    void Update()
    {
        
    }
}
