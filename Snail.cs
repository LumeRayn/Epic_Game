using UnityEngine;

//public HealtharBehaviourScript Healthbar;

public class Snail : Entity
{
    [SerializeField] private int lives = 2;

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == PlayerController.Instance.gameObject)
        {
            PlayerController.Instance.GetDamage();
            lives--;
            HealthController.Health -=1;
            Debug.Log("Snail Health " + lives);
        }
        if (lives < 1)
        {
            Die();
        }
    }
}
