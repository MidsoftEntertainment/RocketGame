using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintToConsole : MonoBehaviour
{
    public bool bDebug = false;
    public bool bDebugOnTick = false;
    public int DebugCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (bDebug)
            for (int i = 0; i < DebugCount; i++)
                Debug.Log("Hello world!");
    }

    // Update is called once per frame
    void Update()
    {
        if (bDebug && bDebugOnTick)
            Debug.Log("Hello world!");
    }
}
