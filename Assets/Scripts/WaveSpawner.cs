using System.Collections;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private float countdown;
    [SerializeField] private float spawnDelay;

    [SerializeField] private TextMeshProUGUI waveCountdownText;

    private int waveIndex;

    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(nameof(SpawnWave));
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        if (countdown >= 0f)
            waveCountdownText.text = $"{Mathf.CeilToInt(countdown):D2}";
    }

    private IEnumerator SpawnWave()
    {
        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(spawnDelay);
        }        
    }
}
