using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    CharacterController playerController;
    [SerializeField] float rageVelocity = 0.01f;
    [SerializeField] float rightStutter = 0.05f;
    [SerializeField] float turnSpeed = 1f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] Transform meshTransform;
    float initMeshTurn = 0f;
    bool isRaging = false;
    int turnScale = 1;

    // Start is called before the first frame update
    void Start()
    {
        // StartRaging();
        initMeshTurn = meshTransform.localEulerAngles.y;
        playerController = GetComponent<CharacterController>();
        // PrintString("Hello world!");
    }

    // Update is called once per frame
    void Update()
    {
        ToggleRage();

        MovePlayer(playerController);
    }

    void ToggleRage()
    {
        float meshTurn = meshTransform.localEulerAngles.y;

        if (Input.GetKeyDown("r"))
            isRaging = !isRaging;

        if (isRaging)
        {
            // transform.Translate(0f, rageVelocity * Input.GetAxis("Rage"), Random.Range(-rightStutter, rightStutter));
            meshTransform.Translate(0f, 0f, Random.Range(-rightStutter, rightStutter));

            if (Mathf.Floor(meshTurn) == Mathf.Floor(initMeshTurn + 30f))
                turnScale = -1;

            if (Mathf.Floor(meshTurn) == Mathf.Floor(initMeshTurn - 30f))
                turnScale = 1;

            meshTransform.Rotate(0f, 0.1f * turnScale * turnSpeed, 0f);
        }
        else if (Mathf.Approximately(meshTurn, initMeshTurn))
            meshTransform.localRotation = Quaternion.Euler(0f, initMeshTurn, 0f);
        else
            meshTransform.localRotation = Quaternion.Euler(0f, Mathf.LerpAngle(meshTurn, initMeshTurn, Time.deltaTime * 5f), 0f);

        if (!isRaging)
        {
            if (meshTransform.localPosition == Vector3.zero)
                meshTransform.localPosition = Vector3.zero;
            else
                meshTransform.localPosition = Vector3.Lerp(meshTransform.localPosition, Vector3.zero, Time.deltaTime * 5f);
        }
    }

    void MovePlayer(CharacterController controller)
    {
        float forwardAxis = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        float rightAxis = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        if (controller && controller.enabled)
            controller.Move(new Vector3(rightAxis, 0f, forwardAxis));
        else
            transform.Translate(forwardAxis, 0f, -rightAxis);
    }

    void PrintString(object text)
    {
        Debug.Log(text);
    }
}
