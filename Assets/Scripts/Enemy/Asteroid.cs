using System;
using Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : SpaceObject
{
    [SerializeField] private GameObject graphics;
    
    private float _xSpin, _ySpin, _zSpin;
    private float _rotationSpeed;

    private void Start()
    {
        _xSpin = Random.Range(0,360);
        _ySpin = Random.Range(0,360);
        _zSpin = Random.Range(0,360);
        _rotationSpeed = Random.Range(1, 5);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.back * speed);
        graphics.transform.rotation = Quaternion.Euler(_xSpin, _ySpin, _zSpin);
        _xSpin += _rotationSpeed;
    }
}
