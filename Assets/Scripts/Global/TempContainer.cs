using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TempContainer : MonoBehaviour
{
    private static TempContainer _instance;
    public static TempContainer Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }

    public void MoveToContainer(GameObject smth)
    {
        smth.transform.parent = transform;
    }
}
