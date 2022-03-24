using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> _wayConfig;
    [SerializeField] private int _startingWave = 0;
    [SerializeField] private bool _looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (_looping);
    }

    IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = _startingWave; waveIndex < _wayConfig.Count; waveIndex++)
        {
            var currentWave = _wayConfig[_startingWave];
            yield return StartCoroutine(SpawnAllEnemiesInWaves(currentWave));

        }
    }

    IEnumerator SpawnAllEnemiesInWaves(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumbersOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWayPoints()[0].transform.position, Quaternion.identity);

            newEnemy.GetComponent<Enemy>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenApawns());
        }
    }
}
