using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    
    void OnTriggerEnter(Collider other)
    {
        // if the player collides with BombPower
        Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            
            other.GetComponent<PlayerMovement>().ActivateShieldPower();

            Destroy(gameObject);
            
        }
    }
}
