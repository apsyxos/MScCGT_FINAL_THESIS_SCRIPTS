using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is responsible for spawning extra zombies in points accross the map
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager manager;
    [SerializeField]
    private GameObject zombiePrefab;
    public Transform[] zombieSpawnPoints;
    [SerializeField]
    private int zombieCount;
    private int initialZombieCount;
    public float waitBeforeSpawn = 60f;

    void Awake()
    {
        MakeManager();
        waitBeforeSpawn *= Time.timeScale;
    }
    void Start()
    {
        initialZombieCount = zombieCount;
        StartCoroutine("CheckToSpawnEnemies");
    }
    void MakeManager()
    {
        if(manager == null)
        {
            manager = this;
        }
    }

    //spawn zombies every few seconds
    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(waitBeforeSpawn);
        SpawnZombies();
        //looping the coroutine
        StartCoroutine("CheckToSpawnEnemies");
    }

    //spawn one zombie on each spawn point on the map
    void SpawnZombies()
    {
        int index = 0;
        for(int i = 0; i<zombieCount;++i)
        {
            if (index >= zombieSpawnPoints.Length)
                index = 0;

            Instantiate(zombiePrefab, zombieSpawnPoints[index].position, Quaternion.identity);
            ++index;
        }
    }
}