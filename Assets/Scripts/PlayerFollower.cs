using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that follows the Player prefab
/// </summary>
public class PlayerFollower : MonoBehaviour
{
    private Transform player;
    private Vector3 defaultPosition;
    private Vector3 defaultPlayerPosition;
    private Vector3 playerGravity;
    void Start()
    {
        player = GameObject.Find("Player").transform;
        defaultPosition = transform.position;
        defaultPlayerPosition = player.position;
    }

    
    void Update()
    {
        transform.position = -(defaultPlayerPosition - player.position) + defaultPosition;
    }
}
