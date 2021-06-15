using System.Collections;
using System.Collections.Generic;
using Enemy;
using Environment;
using UnityEngine;

namespace Global
{
    public class GameController : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private float timeBetwWaves;
        [SerializeField] private Vector2 timeBetwSpawns;
        [SerializeField] private float waveHpMod;
        [SerializeField] private float waveNumberMod;
        [SerializeField] private List<GameObject> spaceObjectsForWaves = new List<GameObject>();
    
        [Header("Components")]
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform playerSpawnPosition;
        [SerializeField] private SpawnArea spawnArea;
    
        private bool _isGameOver;

        void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            _isGameOver = false;

            Instantiate(playerPrefab, playerSpawnPosition.position, Quaternion.identity);
            StartCoroutine(WaveSpawner());
        }

        private IEnumerator WaveSpawner()
        {
            var waveCounter = 1;

            while (!_isGameOver)
            {
                yield return new WaitForSeconds(timeBetwWaves);
                var spaceObjects = GetSpaceObjectsThisWave(waveCounter);
                foreach (var spaceObject in spaceObjects)
                {
                    var spaceObj = Instantiate(spaceObject, spawnArea.GetSpawnPosition(), Quaternion.identity).GetComponent<SpaceObject>();
                    spaceObj.Init(waveHpMod * waveCounter);
                    yield return new WaitForSeconds(Random.Range(timeBetwSpawns.x, timeBetwSpawns.y));
                }

                waveCounter++;
            }
        }

        private List<GameObject> GetSpaceObjectsThisWave(int waveNumber)
        {
            var spaceObjects = new List<GameObject>();
            var numberOfEnemies = Mathf.CeilToInt(waveNumber * waveNumberMod);

            while (spaceObjects.Count != numberOfEnemies)
            {
                var spaceObject = spaceObjectsForWaves[Random.Range(0, spaceObjectsForWaves.Count)];
                spaceObjects.Add(spaceObject);
            }

            return spaceObjects;
        }
    }
}
