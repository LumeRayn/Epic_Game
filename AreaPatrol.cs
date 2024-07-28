using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPatrol : Entity
{
    public float speed;
    private Vector3 dir;
   

    [SerializeField] private int lives = 2;
    private float waitTime;
    public float startWaitTime;

    public Transform[] moveSpots;
    private int randomSpot;
    
    void Start()
    {
        randomSpot = Random.Range(0, moveSpots.Length);
        waitTime = startWaitTime;
    }

    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) // Метод, который вызывается при столкновении с другим коллайдером
    {
        if (collision.gameObject == PlayerController.Instance.gameObject) // Если объект столкновения является героем
        {
            PlayerController.Instance.GetDamage(); // Вызывает метод GetDamage у героя
            lives--;
            HealthController.Health -=1;
            Debug.Log("BEES Health " + lives);
        }
        if (lives < 1)
        {
            Die();
        }
    }
    
}
