using System.Collections;
using System.Collections.Generic;
using Enemy;
using Environment;
using Player;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Global
{
    /// <summary>
    /// Class which controls gameplay workflow.
    /// </summary>
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
        [SerializeField] private Text scoreText;
        [SerializeField] private Text damageText;

        private static GameController _instance;
        public static GameController instance => _instance;
        
        [HideInInspector] public bool IsGameOver;
        [HideInInspector] public PlayerController player;

        private Coroutine _waveSpawner;
        public int score;

        /// <summary>
        /// Unity event inherited from Unity MonoBehavior class.
        /// Is called first once when Monobehavior object is created.
        /// Initializes Singleton of this class
        /// </summary>
        private void Awake()
        {
            _instance = this;
        }

        /// <summary>
        /// Unity event inherited from Unity MonoBehavior class
        /// Is called after "Awake" once when Monobehavior object is created.
        /// Does initialization logic
        /// </summary>
        private void Start()
        {
            if (healthBar != null) healthBar.gameObject.SetActive(false);
            if (damageText != null) damageText.gameObject.SetActive(false);
            score = 0;
            AddScore(0);
        }

        /// <summary>
        /// Unity event inherited from Unity MonoBehavior class
        /// Is called every frame after "Start".
        /// Updates player damage UI text
        /// </summary>
        private void Update()
        {
            if (player != null && !IsGameOver)
                damageText.text = "Damage: " + player.CurrDamage;
        }

        /// <summary>
        /// Game start method, called by "START" button.
        /// </summary>
        public void StartGame()
        {
            if (startButton != null) startButton.SetActive(false);
            if (healthBar != null) healthBar.gameObject.SetActive(true);
            if (damageText != null) damageText.gameObject.SetActive(true);
            score = 0;
            AddScore(0);
            IsGameOver = false;

            if (player != null) Destroy(player.gameObject);
            player = Instantiate(playerPrefab, playerSpawnPosition.position, Quaternion.identity).GetComponent<PlayerController>();
            player.Init();
            healthBar.Init(player);
            
            _waveSpawner = StartCoroutine(WaveSpawner());
        }

        /// <summary>
        /// Method for getting SpaceObject component from far parent of passed object.
        /// </summary>
        /// <param name="obj">Object which parents may have SpaceObject component.</param>
        /// <returns>Returns null if didn't find anything or SpaceObject component of the parent.</returns>
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

        /// <summary>
        /// Adds score to current play session.
        /// </summary>
        /// <param name="value">Amount of points to add.</param>
        public void AddScore(int value)
        {
            score += value;
            if (scoreText != null) scoreText.text = "Score: " + score;
        }

        /// <summary>
        /// Finishes current game session.
        /// </summary>
        public void FinishGame()
        {
            IsGameOver = true;
            if (startButton != null) startButton.SetActive(true);
            if (healthBar != null) healthBar.gameObject.SetActive(false);
            if (damageText != null) damageText.gameObject.SetActive(false);
            if (_waveSpawner != null) StopCoroutine(_waveSpawner);
        }

        /// <summary>
        /// Wave spawning coroutine method, should be called with StartCoroutine().
        /// </summary>
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

        /// <summary>
        /// Gets list of GameObjects to spawn in next wave using passed value and random.
        /// </summary>
        /// <param name="waveNumber">Current wave number.</param>
        /// <returns>List of GameObjects to spawn in next wave</returns>
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
