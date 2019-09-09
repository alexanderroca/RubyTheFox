using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController rc = other.GetComponent<RubyController>();

        if (rc != null)
        {
            if (rc.getCurrentHealth < rc.maxHealth) {
                rc.changeHealth(1);
                Destroy(gameObject);

                rc.PlaySound(collectedClip);
            }   //if
        }   //if
    }
}
