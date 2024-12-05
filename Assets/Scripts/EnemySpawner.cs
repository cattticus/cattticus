using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public Enemy enemyPrefab;

    public float trajectoryVariance = 15.0f;

    public float spawnRate = 2.0f;

    public float spawnDistance = 15.0f;

    public float spawnAmount = 1.0f;
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);//spawns at a regular rate (every 2 seconds)
    }
    private void Spawn()
    {
        for (int i = 0; i < this.spawnAmount; i++) 
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;//spawns enemies on the edge of the circle
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward); //changing trajectory

            Enemy enemy = Instantiate(this.enemyPrefab, spawnPoint, rotation);
            enemy.size = Random.Range(enemy.minSize, enemy.maxSize);
            enemy.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
