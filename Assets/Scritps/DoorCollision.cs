using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Wall is on!" + other.name);
        other.GetComponentInChildren<ShieldPower>().GetComponent<PlayerMovement>().DeactivateShieldOnDoor();
        
            
    }
}
