using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{   
    public Transform player;
    public int index;
   
    void Awake()
    {   
        if (DataContainer.checkpointIndex == index)
        {
            player.position = transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.name == "Player")
        {
            DataContainer.checkpointIndex = index;
        }
    }
}
