using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform Player;

    int moveSpeed = 4;
    int maxDistance = 10;
    int minDistance = 5;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.LookAt(Player);
        if(Vector3.Distance(transform.position, Player.position) >= minDistance)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            if(Vector3.Distance(transform.position, Player.position) <= maxDistance)
            {

            }
        }
    }

}
