using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public float distanceDepart = 5f;
    public GameObject johnsonPrefab;
    public int numberOfWaves = 5;
    public int enemiesPerWave = 10;
    public float waveInterval = 10f;
    public float minY = 0.5f;
    public float maxY = 1f;
    private int currentWave = 0;
    public string johnsonTag = "johnson";

    void Start()
    {
        StartCoroutine(WaveRoutine());
    }

    IEnumerator WaveRoutine()
    {
        while (currentWave < numberOfWaves)
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnJohnsonAtRandomPoint();
                yield return new WaitForSeconds(1f);
            }
            currentWave++;
            yield return new WaitForSeconds(waveInterval);
        }
    }

    void SpawnJohnsonAtRandomPoint()
    {
        float angle = Random.Range(0, 360);
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * distanceDepart;
        float z = Mathf.Sin(angle * Mathf.Deg2Rad) * distanceDepart;
        float y = Random.Range(minY, maxY);

        Vector3 spawnPosition = transform.position + new Vector3(x, y, z);
        Instantiate(johnsonPrefab, spawnPosition, Quaternion.identity);
    }

    
}
