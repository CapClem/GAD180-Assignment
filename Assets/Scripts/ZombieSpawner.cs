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
    public float delay = 2.0f;
    public float roundDelay;
    private float timeSinceSpawn =0;
    public int maxZombies = 150;
    public List<GameObject> currentZombies;
    public List<GameObject> weaponDrops;
    public GameObject medKitDrop;
    public GameObject ammoDrop;
    public float weaponDropChance;
    public float utilityDropChance;
    public int roundCounter = 1;
    public int baseZombiesPerRound;
    public int zombiesThisRound = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void SpawnZombie()
    {

        GameObject zomb = Instantiate(Zombie, SpawnLocations[currentSpawnLocationIndex].position, SpawnLocations[currentSpawnLocationIndex].rotation);
        currentZombies.Add(zomb);
        ZombieControl zCon = zomb.GetComponent<ZombieControl>();
        zCon.player1 = PlayerOne;
        zCon.player2 = PlayerTwo;
        zCon.spawner = this;

        float curChance = Random.Range(0.0f, 1.0f);
        if (curChance <= weaponDropChance)
        {
            zCon.drop = weaponDrops[Mathf.RoundToInt(Random.Range(0, weaponDrops.Count - 1))];
        }
        else if (curChance <= utilityDropChance)
        {
            if (Random.Range(0, 3) <= 0)
            {
                zCon.drop = medKitDrop;
                zCon.drop.GetComponent<PickUpScript>().Value = Random.Range(30, 75);
            }
            else
            {
                zCon.drop = ammoDrop;
                zCon.drop.GetComponent<PickUpScript>().Value = Random.Range(10, 40);
            }

        }
    }
    void Update()
    {
        if (currentZombies.Count < maxZombies)
        {
            if ( zombiesThisRound < baseZombiesPerRound * roundCounter)
            {
                if (currentSpawnLocationIndex < SpawnLocations.Length)
                {

                    if (timeSinceSpawn <= 0)
                    {

                        SpawnZombie();
                        currentSpawnLocationIndex += 1;
                        timeSinceSpawn = delay;
                        zombiesThisRound++;
                    }
                }
                else
                {
                    currentSpawnLocationIndex = 0;
                }
            }
            else if (currentZombies.Count == 0)
            {
                PlayerOne.GetComponent<Stats>().Revive();
                PlayerTwo.GetComponent<Stats>().Revive();
                roundCounter += 1;
                zombiesThisRound = 0;
                timeSinceSpawn = roundDelay;
            }

                    
            timeSinceSpawn -= Time.deltaTime;
        }
    }
}
