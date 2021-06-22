using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Enemy;
using Environment;
using Player;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

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
        [SerializeField] private HealthBarHandler healthBar;
        [SerializeField] private GameObject startButton;

        private static GameController _instance;
        public static GameController instance => _instance;
        
        [HideInInspector] public bool IsGameOver;
        [HideInInspector] public PlayerController player;

        private Coroutine _waveSpawner;

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            healthBar.gameObject.SetActive(false);
        }

        public void StartGame()
        {
            startButton.SetActive(false);
            healthBar.gameObject.SetActive(true);
            IsGameOver = false;

            if (player != null) Destroy(player.gameObject);
            player = Instantiate(playerPrefab, playerSpawnPosition.position, Quaternion.identity).GetComponent<PlayerController>();
            healthBar.Init(player);
            
            _waveSpawner = StartCoroutine(WaveSpawner());
        }

        public static SpaceObject TryGetParentSpaceObject(Transform obj)
        {
            var spaceObject = obj.GetComponent<SpaceObject>();
            
            while (spaceObject == null)
            {
                if (obj.parent != null)
                {
                    obj = obj.parent;
                    spaceObject = obj.GetComponent<SpaceObject>();
                }
                else break;
            }

            return spaceObject;
        }

        public void FinishGame()
        {
            IsGameOver = true;
            startButton.SetActive(true);
            healthBar.gameObject.SetActive(false);
            StopCoroutine(_waveSpawner);
        }

        private IEnumerator WaveSpawner()
        {
            var waveCounter = 1;

            while (!IsGameOver)
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
