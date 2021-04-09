using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _enemy1Speed = 2f;

    [SerializeField] private float _rotationSpeed = 3f;
    //reference to the Enemy1 rigid body
    private Rigidbody _enemyRigidB;

    private Vector3 towardsPlayer;
    //  Create reference to the player
    public Transform PlayerView;
    
    void Start()

    {
        _enemyRigidB = this.GetComponent<Rigidbody>();
        transform.position= new Vector3(3f,3f,3f);
    }

    // Update is called once per frame
    void Update()
    {   
        //where the player is
        Vector3 direction = PlayerView.position - transform.position;
        
        
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
        _enemyRigidB.MovePosition(transform.position + (direction * _enemy1Speed * Time.deltaTime));
    }
}
