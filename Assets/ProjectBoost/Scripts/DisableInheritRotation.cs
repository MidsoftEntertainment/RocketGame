using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableInheritRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
