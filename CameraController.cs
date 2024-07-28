using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;

    private void Awake()
    {
        if(!player)
        {
            player = FindAnyObjectByType<PlayerController>().transform;
        }
            
    }

    private void Update()
    {
        pos = player.position;
        pos.z = -10f; // привязал жестко расстояние от камеры до игрока, тк иначе через три секунды моделька игрока пропадает 
        pos.y += 3f;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime); // Лерп делает плавные передвижение 
    }
}
