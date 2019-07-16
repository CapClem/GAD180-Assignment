using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject PlayerOne;
    public GameObject PlayerTwo;

    public GameObject Zombie; // The prefab

    public Transform[] SpawnLocations;
    public int currentSpawnLocationIndex = 0;
    public float Delay = 5.0f;
    private float timeSinceSpawn =0;
    public int maxZombies = 10;
    public List<GameObject> CurrentZombies; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentZombies.Count < maxZombies)
        {
            if (currentSpawnLocationIndex < SpawnLocations.Length)
            {
                if (timeSinceSpawn <= 0)
                { 
                    GameObject zomb = Instantiate(Zombie, SpawnLocations[currentSpawnLocationIndex].position, SpawnLocations[currentSpawnLocationIndex].rotation);
                    CurrentZombies.Add(zomb);
                    ZombieControl zCon = zomb.GetComponent<ZombieControl>();
                    zCon.player1 = PlayerOne;
                    zCon.player2 = PlayerTwo;
                    zCon.spawner = this;
                    currentSpawnLocationIndex += 1;
                    timeSinceSpawn = Delay;
                }

            }
            else
            {
                currentSpawnLocationIndex = 0;
            }
        }
        timeSinceSpawn -= Time.deltaTime;
        
    }
}
