using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    public float LookRate = 200f;
    float TurnRotation = 0f;
    public Transform PlayerTransform;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float LookUp = Input.GetAxis("Mouse Y") * LookRate * Time.deltaTime;
        float Turn = Input.GetAxis("Mouse X") * LookRate * Time.deltaTime;

        TurnRotation -= LookUp;
        TurnRotation = Mathf.Clamp(TurnRotation, -85f, 85f);

        transform.localRotation = Quaternion.Euler(TurnRotation, 0f, 0f);
        PlayerTransform.Rotate(Vector3.up * Turn);
    }
}
