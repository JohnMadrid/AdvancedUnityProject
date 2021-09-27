using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinReward : MonoBehaviour
{
    //[SerializeField] public GameObject coinRewardCount;
    //private int coinsValue;
    
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
        // if the player collides with coin
        
        if (other.CompareTag("Player"))
        {
            // Increase the coins count
            other.GetComponent<PlayerMovement>()._coinRewards += Random.Range(1,12);
            
            Destroy(gameObject);

        }
    }
}
