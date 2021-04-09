using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;


public class PlayerMovement : MonoBehaviour
{   
    // motor that drives the player
    public CharacterController controller;
    public Transform cam;
    
    [SerializeField]
    private float _speed = 6f;

    private float _turnSmoothTime = 0.1f;

    private float _turnSmoothVelocity;
    // public

    void Start()
    {
        // if  lives == 0 
        // // reset player position
        // transform.position = new Vector3(0f,0f,0f)
        
    }
 
    // Update is called once per frame
    void Update()
    {
        PlayerMoves();
       
    }
    
    // player movement
    void PlayerMoves()
    {
        // read player inputs on both x and y axis
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        
        // player and camera move together
        if (direction.magnitude >= 0.1f)
        {
            // Player rotates and faces the direction in which it is moving and moves where camara is pointing
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            
            // smooth player rotation movement
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            // Calculate the desired direction of movement depending on the camera movement
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * (_speed * Time.deltaTime));
        }
    }
}

// TODO: decrease Player lives, create player damage