using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] float thrustSpeed = 100f;
    [SerializeField] float rotateSpeed = 55f;
    [SerializeField] AudioClip idleSound;
    [SerializeField] AudioClip thrustSound;
    [HideInInspector] public Rigidbody physics;
    public ParticleSystem engineJetEffect;
    public ParticleSystem engineLeftJetEffect;
    public ParticleSystem engineRightJetEffect;
    AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<Rigidbody>();
        sfx = GetComponent<AudioSource>();

        physics.centerOfMass = Vector3.zero;
        physics.inertiaTensorRotation = Quaternion.identity;

        if (idleSound)
        {
            sfx.clip = idleSound;
            sfx.Play();
        }
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrustEffects();
        // AdjustRotation();
        ProcessRotationEffects();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
            physics.AddRelativeForce(Vector3.up * physics.mass * Time.deltaTime * thrustSpeed * 10f);
    }

    void ProcessThrustEffects()
    {
        if (Input.GetKeyDown(KeyCode.Space) && thrustSound)
        {
            if (engineJetEffect)
                engineJetEffect.Play();

            sfx.clip = thrustSound;
            sfx.Play();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (engineJetEffect)
                engineJetEffect.Stop();

            if (idleSound)
            {
                sfx.clip = idleSound;
                sfx.Play();
            }
            else if (sfx.isPlaying)
                sfx.Stop();
        }
    }

    void ProcessRotation()
    {
        float right = -Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed;

        // transform.Rotate(Vector3.forward * right);
        physics.AddTorque(Vector3.forward * right * 30f, ForceMode.Impulse);
    }

    void AdjustRotation()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        if (rot != Vector3.zero && (/*physics.freezeRotation || */Input.GetKey(KeyCode.Space)))
        {
            float newX = Mathf.LerpAngle(rot.x, 0, Time.deltaTime * 5f);
            float newY = Mathf.LerpAngle(rot.y, 0, Time.deltaTime * 5f);

            transform.rotation = Quaternion.Euler(newX, newY, rot.z);
        }
    }

    void ProcessRotationEffects()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            // physics.freezeRotation = true;

            if (thrustSound && !sfx.isPlaying)
            {
                sfx.clip = thrustSound;
                // sfx.Play();
            }
        }
        else
        {
            // physics.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

            if (idleSound && !sfx.isPlaying)
            {
                sfx.clip = idleSound;
                sfx.Play();
            }
            else if (sfx.isPlaying && !Input.GetKey(KeyCode.Space))
                sfx.Stop();
        }

        if (!engineLeftJetEffect || !engineRightJetEffect)
            return;

        if (Input.GetAxis("Horizontal") > 0f && !Input.GetKey(KeyCode.Space))
        {
            if (engineRightJetEffect.isPlaying)
                engineRightJetEffect.Stop();

            if (!engineLeftJetEffect.isPlaying)
                engineLeftJetEffect.Play();
        }
        else if (Input.GetAxis("Horizontal") < 0f && !Input.GetKey(KeyCode.Space))
        {
            if (engineLeftJetEffect.isPlaying)
                engineLeftJetEffect.Stop();

            if (!engineRightJetEffect.isPlaying)
                engineRightJetEffect.Play();
        }
        else
        {
            engineRightJetEffect.Stop();
            engineLeftJetEffect.Stop();
        }
    }
}
