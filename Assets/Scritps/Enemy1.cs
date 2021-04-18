using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    //used to grab the PlayerView from the scene 
    public static GameObject PlayerView;
    
    //private float _enemy1Speed = Random.Range(5.0f, 15.0f);

    [SerializeField] private float _rotationSpeed = 3f;
    //reference to the Enemy1 rigid body
    private Rigidbody _enemyRigidB;

    private Vector3 towardsPlayer;
   
    
    void Start()

    {
        // search for the PlayerView
        PlayerView = GameObject.FindWithTag("Player");
        
        
        _enemyRigidB = this.GetComponent<Rigidbody>();
        transform.position= PlayerView.transform.position +   new Vector3(10f,0f,10f);
    }

    // Update is called once per frame
    void Update()
    {   
        //where the player is
        Vector3 direction = PlayerView.transform.position - transform.position;
        
        
        // rotate Enemy1 in the direction of the player, so that it looks at the player
         Quaternion rotation = Quaternion.LookRotation(direction);
         transform.rotation = rotation;
         transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
       
         direction.Normalize();
         towardsPlayer = direction;
         //Enemy1 moves towards the player
         moveEnemy(towardsPlayer);

    }

    void moveEnemy(Vector3 direction)
    {
        // not all Enemy1 instances travel at the same speed. They travel at a speed ranging from 3  to 8
        _enemyRigidB.MovePosition(transform.position + (direction * Random.Range(3.0f, 8.0f) * Time.deltaTime));
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    
    // Enemy takes lives from Player
    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            other.GetComponent<PlayerMovement>().Damage();
            
        }

    }
}
