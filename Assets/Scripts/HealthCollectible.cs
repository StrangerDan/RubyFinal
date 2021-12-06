using UnityEngine;
using System;

/// <summary>
/// Will handle giving health to the character when they enter the trigger.
/// </summary>
public class HealthCollectible : MonoBehaviour 
{
    AudioSource audioSource;
 public AudioClip pickupnoise;
    void Start(){
                        audioSource = GetComponent<AudioSource>();

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyControllerCurrent controller = other.GetComponent<RubyControllerCurrent>();

        if (controller != null)
        {
                    audioSource.PlayOneShot(pickupnoise);

            controller.ChangeHealth(1);
            Destroy(gameObject);
        }
    }
}
