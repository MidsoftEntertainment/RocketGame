using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    int score = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Hit")
        {
            score++;
            Debug.Log("You've bumped into a thing " + score + " times");
        }
    }

    /*
    bool hitOnce = true;
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hitOnce && hit.gameObject.GetComponent<ObjectHit>())
        {
            score++;
            Debug.Log(score);
            hit.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            hitOnce = false;
        }
    }
    */
}
