using System.Collections;
using System.Collections.Generic;
using Enemy;
using Player;
using UnityEngine;

public class PowerUp : SpaceObject
{
    [SerializeField] private float damageBoost;
    [SerializeField] private float healthBoost;
    
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
        if (!IsDestroyed)
            graphics.transform.rotation = Quaternion.Euler(_xSpin, _ySpin, _zSpin);
        _xSpin += _rotationSpeed;
    }

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
