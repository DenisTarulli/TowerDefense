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

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        if (countdown >= 0f)
            waveCountdownText.text = $"Next wave: {string.Format("{0:00.00}", countdown)}";
    }

    private IEnumerator SpawnWave()
    {
        waveIndex++;
        PlayerStats.Waves++;

        for (int i = 0; i < waveIndex; i++)
        {
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(spawnDelay);
        }        
    }
}
