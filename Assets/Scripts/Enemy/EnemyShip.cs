using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using Ship;
using UnityEngine;

/// <summary>
/// Class which manages enemy ships.
/// </summary>
public class EnemyShip : SpaceObject
{
    [SerializeField] private GameObject gunsHolder;
    [SerializeField] protected List<GameObject> particlesToDisable;
    
    private List<Gun> _guns;

    /// <summary>
    /// Unity event inherited from Unity MonoBehavior class
    /// Is called after "Awake" once when Monobehavior object is created.
    /// Finds all attached to ship guns and moves them to list.
    /// </summary>
    private void Start()
    {
        _guns = gunsHolder.GetComponentsInChildren<Gun>().ToList();
    }

    /// <summary>
    /// Override of Die method, additionaly to base logic disables some particles.
    /// </summary>
    public override void Die()
    {
        base.Die();
        foreach (var particle in particlesToDisable)
            particle.SetActive(false);
    }

    /// <summary>
    /// Unity event inherited from Unity MonoBehavior class
    /// Is called every fixed "physics" frame after "Start".
    /// Processes moving and shooting logic.
    /// </summary>
    private void FixedUpdate()
    {
        transform.Translate(Vector3.back * speed);
        if (!IsDestroyed) foreach (var gun in _guns) gun.Shoot();
    }
}
