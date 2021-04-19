using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPower : MonoBehaviour
{
    // Start is called before the first frame update
    //Reference to the player
    //public static GameObject PlayerViewReference;
    
    //referecne to playerView
    //private Collider PlayerViewC;
    
    
    
    void Start()
    {
       //PlayerViewReference = GameObject.FindWithTag("Player");
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
