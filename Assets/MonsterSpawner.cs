using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterSpawner
{
    public void SpawnEnemy(GameObject gameObject, Vector3 location, float maxDistance) {
        var spawner = new MonsterSpawner();
        var adjustedLocation = new Vector3(location.x + Random.Range(0.01f, 0.03f), location.y, location.z + Random.Range(2.9f, 3.0f));
        NavMeshHit navMeshHit;
        if(NavMesh.SamplePosition(adjustedLocation, out navMeshHit, maxDistance, NavMesh.AllAreas)) {
            GameObject newEnemy = Object.Instantiate(gameObject, adjustedLocation, Quaternion.identity);
        }
    }
}
