using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableEvenetSystem;

public class Initializer : MonoBehaviour
{
    public OnArraySizeChanged onArray;

    private void Start()
    {
        onArray = OnArraySizeChanged.Instance;
    }
}