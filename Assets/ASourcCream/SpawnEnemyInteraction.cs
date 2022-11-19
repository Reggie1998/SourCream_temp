using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemyInteraction : MonoBehaviour
{
    [SerializeField]
    GameObject monsterPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter()
    {
        var spawner = new MonsterSpawner();
        spawner.SpawnEnemy(monsterPrefab, transform.position, 5.0f);
    }
}
