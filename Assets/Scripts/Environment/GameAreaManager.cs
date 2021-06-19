using System;
using System.Collections;
using System.Collections.Generic;
using Global;
using UnityEngine;

public class GameAreaManager : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        var spaceObject = GameController.TryGetParentSpaceObject(other.transform);
        Destroy(spaceObject != null ? spaceObject.gameObject : other.gameObject);
    }
}
