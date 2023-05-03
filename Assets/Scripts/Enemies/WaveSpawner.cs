using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // A list of all the possible spawns
    public SpawnType[] spawns;

    // Time between each wave
    public int waveDelay;

    // Time between each spawn
    public float spawnDelay;

    // The initial amount of enemies for the first wave
    // Will be casted to an int
    public float initialWaveCount = 1;

    // How many waves it takes for more enemies to arrive per wave
    public int waveGrowthRate = 5;
    
    // How much to increase by
    public float waveGrowthFactor = 1.5f;

    // Helper array/float for spawning
    float[] weights; 
    float totalWeight = 0;

    int waveNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        weights = new float[spawns.Length];
        for(int i = 0; i < spawns.Length; i ++) {
            SpawnType spawn = spawns[i];
            totalWeight += spawn.weight;
            weights[i] = totalWeight;
        }

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves() {
        // Check that there are spawns
        int length = spawns.Length;
        while(length > 0) {
            // Wait for next wave
            yield return new WaitForSeconds(waveDelay);

            // Spawn enemies
            for(int i = 0; i < (int)initialWaveCount; i ++) {
                // Randomly choose a spawn type
                float choice = Random.Range(0, totalWeight);
                int ndx = 0;
                while(choice > weights[ndx])
                    ndx += 1;
                
                // Instantiate the spawn
                SpawnType spawn = spawns[ndx];
                Instantiate(
                    spawn.enemyPrefab,
                    spawn.spawnPoint.position,
                    Quaternion.identity
                );

                // Delay between spawns
                yield return new WaitForSeconds(spawnDelay);
            }

            // Check if the waves should grow
            waveNumber += 1;    
            if(waveNumber >= waveGrowthRate) {
                waveNumber = 0;
                initialWaveCount *= waveGrowthFactor;
            }

        }
    }
}

[System.Serializable]
public class SpawnType {
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    // Chance of being spawned
    public float weight; 
}
