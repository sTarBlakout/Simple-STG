using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using Ship;
using UnityEngine;

public class EnemyShip : SpaceObject
{
    [SerializeField] private GameObject gunsHolder;
    [SerializeField] protected List<GameObject> particlesToDisable;
    
    private List<Gun> _guns;

    private void Start()
    {
        _guns = gunsHolder.GetComponentsInChildren<Gun>().ToList();
    }

    public override void Die()
    {
        base.Die();
        foreach (var particle in particlesToDisable)
            particle.SetActive(false);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.back * speed);
        if (!IsDestroyed) foreach (var gun in _guns) gun.Shoot();
    }
}
