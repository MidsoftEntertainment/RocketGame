using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    // bool pressQ = false;
    // float currentTime = 0f;
    [SerializeField] float timeToWait = 5f;
    MeshRenderer render;
    Rigidbody physics;
    // Start is called before the first frame update
    void Start()
    {
        float RandAngle = Random.Range(5f, 15f);
        render = GetComponent<MeshRenderer>();
        physics = GetComponent<Rigidbody>();
        transform.Rotate(RandAngle, RandAngle, RandAngle);
        render.enabled = false;
        physics.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Time.time >= 10)
            print("10 seconds");

        if (Input.GetKeyDown("q"))
        {
            currentTime = Mathf.Floor(Time.time * 100f) / 100f;

            pressQ = true;
        }

        if (pressQ && Mathf.Approximately((Mathf.Floor(Time.time * 100f) / 100f - currentTime), 3f))
        {
            print("3 seconds since Q was pressed!");
            pressQ = false;
        }
        */

        if (Time.time >= timeToWait)
        {
            render.enabled = true;
            physics.useGravity = true;
        }
    }
}
