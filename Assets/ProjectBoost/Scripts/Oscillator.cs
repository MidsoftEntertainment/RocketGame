using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] Vector3 movementVector;
    /*[SerializeField] [Range(0f, 1f)]*/ float movementFactor;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // if (period == 0f) return;
        if (period <= Mathf.Epsilon) return;
        float cycles = Time.time / period;
        const float pi = Mathf.PI;

        float rawSineWave = Mathf.Sin(cycles * 2f * pi);
        movementFactor = (rawSineWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startPos + offset;
    }
}
