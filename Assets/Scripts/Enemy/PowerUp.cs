using Enemy;
using Player;
using UnityEngine;

/// <summary>
/// Class which manages PowerUp.
/// PowerUp gives player health and damage boost on touch.
/// </summary>
public class PowerUp : SpaceObject
{
    [SerializeField] private float damageBoost;
    [SerializeField] private float healthBoost;
    
    private float _xSpin, _ySpin, _zSpin;
    private float _rotationSpeed;

    /// <summary>
    /// Unity event inherited from Unity MonoBehavior class
    /// Is called after "Awake" once when Monobehavior object is created.
    /// Does initialization logic for spins.
    /// </summary>
    private void Start()
    {
        _xSpin = Random.Range(0,360);
        _ySpin = Random.Range(0,360);
        _zSpin = Random.Range(0,360);
        _rotationSpeed = Random.Range(1, 5);
    }

    /// <summary>
    /// Unity event inherited from Unity MonoBehavior class.
    /// Is called every fixed "physics" frame after "Start".
    /// Moves and spins PowerUp.
    /// </summary>
    private void FixedUpdate()
    {
        transform.Translate(Vector3.back * speed);
        if (!IsDestroyed)
            graphics.transform.rotation = Quaternion.Euler(_xSpin, _ySpin, _zSpin);
        _xSpin += _rotationSpeed;
    }

    /// <summary>
    /// Unity event for checking if any collider touches this object.
    /// Checks if PlayerShip touched it, if so die and boost PlayerShip.
    /// </summary>
    /// <param name="other">Object with collider component which touched current object.</param>
    protected override void OnTriggerEnter(Collider other)
    {
        if (IsDestroyed) return;
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().Boost(damageBoost, healthBoost);
            Die();
        }
    }
}
