
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == PlayerController.Instance.gameObject)
        {
            PlayerController.Instance.GetDamage();
            HealthController.Health -=1;
        }
    }
}
