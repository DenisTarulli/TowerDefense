using System.Collections;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    [SerializeField] private WaveScriptableObject[] waves;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private float countdown;

    [SerializeField] private TextMeshProUGUI waveCountdownText;

    private int waveIndex;

    private void Start()
    {
        waveIndex = 0;
        EnemiesAlive = 0;
    }

    private void Update()
    {
        if (EnemiesAlive > 0)
            return;

        if (waveIndex == waves.Length && EnemiesAlive == 0)
        {
            Debug.Log("LEVEL COMPLETED");
            this.enabled = false;
            return;
        }

        if (countdown <= 0f)
        {            
            waveIndex++;
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        if (countdown >= 0f)
            waveCountdownText.text = $"Next wave: {string.Format("{0:00.00}", countdown)}";
    }

    private IEnumerator SpawnWave()
    {
        PlayerStats.Waves++;

        WaveScriptableObject wave = waves[waveIndex - 1];

        EnemiesAlive = wave.enemiesAmount;

        for (int i = 0; i < wave.enemiesAmount; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }        
    }

    private void SpawnEnemy(GameObject enemyToSpawn)
    {
        Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
    }
}
