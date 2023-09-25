using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelDelay = 3f;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip finishSound;
    [SerializeField] ParticleSystem explosionEffect;
    [SerializeField] ParticleSystem finishEffect;
    [SerializeField] GameObject destroyedRocket;
    AudioSource sfx;
    GameObject landingPad;
    RocketMovement movement;
    bool isTransitioning = false;

    void Start()
    {
        movement = GetComponent<RocketMovement>();
        sfx = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (landingPad)
        {
            float xPos = Mathf.Lerp(transform.position.x, landingPad.transform.position.x, Time.deltaTime * 5f);
            float yPos = Mathf.Lerp(transform.position.y, landingPad.transform.position.y + landingPad.transform.localScale.y / 2f, Time.deltaTime * 5f);
            float zPos = Mathf.Lerp(transform.position.z, landingPad.transform.position.z, Time.deltaTime * 5f);
            transform.position = new Vector3(xPos, yPos, zPos);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 5f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning)
            return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Friendly");
                break;
            case "Finish":
                LandingPad(collision.gameObject);
                break;
            case "Fuel":
                print("Fuel");
                break;
            default:
                CrashRocket(collision.GetContact(0).point);
                break;
        }
    }

    void CrashRocket(Vector3 impactPoint)
    {
        isTransitioning = true;
        movement.enabled = false;

        movement.physics.constraints = RigidbodyConstraints.None;

        if (movement.engineJetEffect)
            movement.engineJetEffect.Stop();

        if (movement.engineLeftJetEffect)
            movement.engineJetEffect.Stop();

        if (movement.engineRightJetEffect)
            movement.engineJetEffect.Stop();

        if (explosionEffect)
        {
            explosionEffect = Instantiate(explosionEffect, impactPoint, transform.rotation);
            explosionEffect.Play();
        }

        if (explosionSound)
        {
            if (sfx.isPlaying)
                sfx.Stop();

            sfx.PlayOneShot(explosionSound , 0.5f);
            explosionSound = null;
        }

        destroyedRocket = Instantiate(destroyedRocket, transform.position, transform.rotation);
        destroyedRocket.GetComponent<DestroyedRocket>().xVelocity = movement.physics.velocity.x;
        print(movement.physics.velocity.x);
        
        foreach (Transform childObject in transform)
        {
            childObject.gameObject.SetActive(false);
        }

        Invoke("Respawn", levelDelay);
    }

    void LandingPad(GameObject pad)
    {
        isTransitioning = true;

        if (movement.physics)
        {
            movement.physics.detectCollisions = false;
            movement.physics.isKinematic = true;
        }

        movement.enabled = false;

        if (finishEffect)
        {
            finishEffect = Instantiate(finishEffect, pad.transform.position, pad.transform.rotation);
            finishEffect.Play();
        }

        if (finishSound)
        {
            if (sfx.isPlaying)
                sfx.Stop();

            sfx.PlayOneShot(finishSound);
            finishSound = null;
        }

        landingPad = pad;
        Invoke("Finish", levelDelay);
    }

    void Respawn()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void Finish()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
