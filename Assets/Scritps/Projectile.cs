using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] private float _shootingSpeed= 10f;

    void OnTriggerEnter(Collider other)
    {  // if the projectile collides with the enemy1
        //Debug.Log(other.name);
        if(other.CompareTag("Enemy1")) 
        {
           
            // if the virus-- which is the current class we are in--- hits the Player we want to kill one of the Player's lives
            // we want to access the Damage function of the Player script
            //use GetComponet, the script --- in this case player--- is also a component. That you can tell if you look at
            // inspect tab of the Player object, the cube.
            //other.GetComponent<Player>().Damage();
            
            other.GetComponent<Enemy1>().EnemyDiesEffect();
            //Debug.Log("Enemy DYING Effect ON");
            Destroy(gameObject);

            // Destroy(other);
        }
        // projectile destroys when hits walls and other objects
        else if (other.CompareTag("Walls") | other.CompareTag("BombPower") | other.CompareTag("BoxObstacle"))
        {
            Destroy(gameObject);
        }
        
        
       
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // shoots forward
        transform.Translate(Vector3.forward * (_shootingSpeed * Time.deltaTime));
    }
    
    
}
