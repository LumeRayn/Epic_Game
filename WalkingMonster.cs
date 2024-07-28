using System;
using UnityEngine;

public class WalkingMonster : Entity
{   
    [SerializeField] private int lives = 2;
    private float speed = 2f;
    private Vector3 dir;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start ()
    {
        dir = transform.right;
    }

    private void Move() // Метод, который отвечает за перемещение врага
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + transform.right * dir.x * 0.7f,  -0.1f);  // Создает массив colliders и заполняет его всеми коллайдерами, которые пересекаются с кругом заданного радиуса и центра. Центр круга находится немного выше и впереди относительно объекта.

        if (colliders.Length > 1) dir *= -1f; // Если массив colliders не пустой, то меняет направление врага на противоположное
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);// Перемещает объект на заданное расстояние в заданном направлении с учетом скорости и времени кадра
            sprite.flipX = dir.x < 0.0f; // Отражает спрайт по горизонтали в зависимости от направления
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) // Метод, который вызывается при столкновении с другим коллайдером
    {
        if (collision.gameObject == PlayerController.Instance.gameObject) // Если объект столкновения является героем
        {
            PlayerController.Instance.GetDamage(); // Вызывает метод GetDamage у героя
            lives--;
            HealthController.Health -=1;
            Debug.Log("PIG Health " + lives);
        }
        if (lives < 1)
        {
            Die();
        }
    }

    private void Update()
    {
        Move();
    }
}
