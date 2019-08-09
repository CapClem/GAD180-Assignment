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
    public float Delay = 2.0f;
    private float timeSinceSpawn =0;
    public int maxZombies = 50;
    public List<GameObject> currentZombies;
    public List<GameObject> weaponDrops;
    public GameObject medKitDrop;
    public GameObject ammoDrop;
    public float weaponDropChance;
    public float utilityDropChance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentZombies.Count < maxZombies)
        {
            if (currentSpawnLocationIndex < SpawnLocations.Length)
            {
                if (timeSinceSpawn <= 0)
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
                        if (Random.Range(0, 2) <= 0)
                        {
                            zCon.drop = medKitDrop;
                            zCon.drop.GetComponent<PickUpScript>().Value = Random.Range(30,75);
                        }
                        else
                        {
                            zCon.drop = ammoDrop;
                            zCon.drop.GetComponent<PickUpScript>().Value = Random.Range(4,10);
                        }

                    }
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
