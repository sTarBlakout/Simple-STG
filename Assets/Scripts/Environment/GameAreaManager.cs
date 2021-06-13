using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAreaManager : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
