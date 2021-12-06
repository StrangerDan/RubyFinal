using UnityEngine;
using System;

/// <summary>
/// Will handle temporarily double speed to the character when they enter the trigger.
/// </summary>
public class SpeedCollectible : MonoBehaviour 
{
  

    void OnTriggerEnter2D(Collider2D other)
    {

        RubyControllerCurrent controller = other.GetComponent<RubyControllerCurrent>();

        if (controller != null)
        {
            controller.ChangeSpeed();
            Destroy(gameObject);
        }
    }
}
