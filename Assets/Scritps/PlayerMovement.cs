using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;


public class PlayerMovement : MonoBehaviour
{
    // how much time BombPower lasts
    [SerializeField]private float _bombTimeout = 5f;
    // true if player caught bomb help.
    private bool _bombPower = false;
    //how much fore to add to the ejection of the projectile. 
    [SerializeField] private float thrust = 70f;

    //projectile prefab
    [SerializeField] private GameObject projectilePrefab;

    //projectile ammunition speed
    [SerializeField] private float projectileSpeed = 10f;

    // motor that drives the player
    public CharacterController controller;
    public Transform cam;

    [SerializeField] private float _speed = 6f;

    private float _turnSmoothTime = 0.1f;

    private float _turnSmoothVelocity;
    // public

    void Start()
    {
        // if  lives == 0 
        // // reset player position
        // transform.position = new Vector3(0f,0f,0f)
        //returns player camera


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

        //shoot with left button mouse
        if (Input.GetMouseButtonDown(0))
        {
            if (_bombPower)
            {
                //spawn bombs
            }
            else
            {
                //firing the projectile
                fireProjectile();
            }
        

        }
    }

    void fireProjectile()
    {
        // spawn projeciles
        //GameObject bullet = Instantiate(projectilePrefab, transform.position + new Vector3(0f,0.7f,0f));
        GameObject bullet = Instantiate(projectilePrefab) as GameObject;
        //places the bullet in player position.
        bullet.transform.position = this.transform.position + new Vector3(0f, 0.4f, 0f);
        bullet.transform.rotation = this.transform.rotation;
        // aplies a force, in the direction of the player, to the bullet rigidbody (Unity API)
        bullet.GetComponent<Rigidbody>().AddForce(this.transform.forward * 20f);
        //bullet.GetComponent<Rigidbody>().AddForce(this.transform.position * thrust);

    }
    
    
    
    public void ActivateBomb()
    {   // if one of the crate objects is captured then the uv light power is activated. The function
        // DeactivatePowerUp() manages how much time the uv light power is allowed  in the game. 
        _bombPower = true;
        Debug.Log("Player collided with the BombPower");
        StartCoroutine(DeactivateBomb());
    }

    IEnumerator DeactivateBomb()
    { 
        yield return new WaitForSeconds(_bombTimeout);
        _bombPower = false;
    }
    
    
}


// TODO: decrease Player lives, create player damage