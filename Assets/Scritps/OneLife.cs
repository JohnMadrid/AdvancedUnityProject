using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneLife : MonoBehaviour
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
        
        if (other.CompareTag("Player"))
        {
            Debug.Log( other.GetComponent<PlayerMovement>()._lives);
            other.GetComponent<PlayerMovement>()._lives += 1;
            Debug.Log("Ive given it a life");
            Debug.Log( other.GetComponent<PlayerMovement>()._lives);

            Destroy(gameObject);
            
        }
    }
}
