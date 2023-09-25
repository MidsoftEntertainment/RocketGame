using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedRocket : MonoBehaviour
{
    [SerializeField] float explosionForce = 25f; // 40f
    [SerializeField] float explosionRadius = 25f; // 75f
    [HideInInspector] public float xVelocity = 0f;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - Vector3.right * xVelocity * 2f + Vector3.up * 10f, 75f);
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (Rigidbody physics in GetComponentsInChildren<Rigidbody>())
        {
            if (physics)
            {
                // physics.AddExplosionForce(physics.mass * 100f * explosionForce, transform.position - Vector3.right * xVelocity * 2f + Vector3.up * 10f, explosionRadius);
                // physics.AddExplosionForce(physics.mass * 25f * explosionForce, transform.position, explosionRadius);
                physics.AddForce(Vector3.up * explosionForce * Time.deltaTime * 1000f + Vector3.right * xVelocity * physics.mass * explosionForce * 100f * Time.deltaTime);
                print(physics.gameObject.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
