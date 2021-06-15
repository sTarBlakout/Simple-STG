using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField] private float width;

    public Vector3 GetSpawnPosition()
    {
        return new Vector3(Random.Range(-width, width), transform.position.y, transform.position.z);
    }
}
