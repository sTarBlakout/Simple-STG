using System;
using Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Class which manages Asteroid behavior.
/// </summary>
public class Asteroid : SpaceObject
{
    private float _xSpin, _ySpin, _zSpin;
    private float _rotationSpeed;

    /// <summary>
    /// Unity event inherited from Unity MonoBehavior class.
    /// Is called after "Awake" once when Monobehavior object is created.
    /// Does initialization logic for spinning.
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
    /// Moves and spins Asteroid.
    /// </summary>
    private void FixedUpdate()
    {
        transform.Translate(Vector3.back * speed);
        if (!IsDestroyed)
            graphics.transform.rotation = Quaternion.Euler(_xSpin, _ySpin, _zSpin);
        _xSpin += _rotationSpeed;
    }
}
