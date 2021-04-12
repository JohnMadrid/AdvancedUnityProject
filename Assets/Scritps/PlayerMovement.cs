using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;


public class PlayerMovement : MonoBehaviour
{

    // [Header("Jumping")]
    

    [SerializeField]
    private float _gravity = 9.5f;
    
    //how hight player jumps
    // [SerializeField] private float _jumpS = 3.5f;

    [SerializeField] 
    private int _lives = 3;

    // [SerializeField] private float _colorChannel = 1f;
    //
    // private MaterialPropertyBlock _mpb;
    
    // keep track of direction
    private float _directionY;
    
    
    // bomb prefab
    [SerializeField] private GameObject _bombPrefab;
    // how much time BombPower lasts
    [SerializeField]private float _bombTimeout = 20f;
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


    private Vector3 direction;
    private Vector3 moveDirection;
    
    
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
        direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        
        

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
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
             
            controller.Move(moveDirection.normalized * (_speed * Time.deltaTime));

        }
        

        //JUMPING
        // if (Input.GetButtonDown("Jump"))
        // {
        //     Debug.Log("Space bar pressed");
        //    // _directionY = _jumpS;
        //   
        //    
        //
        // }
        // _directionY -= _gravity;
        // direction.y = _directionY;

        //controller.Move(direction  * Time.deltaTime);
        
        
       

        //shoot with left button mouse
        if (Input.GetMouseButtonDown(0))
        {
            if (_bombPower)
            {
                //fire bombs
                fireBomb();
            }
            else
            {
                //fire  projectiles
                fireProjectile();
            }
        

        }
    }
    //Code based on this video: https://www.youtube.com/watch?v=59No0ybIoxg
    // void jump()
    // {
    //     _directionY = _jumpS;
    //     _directionY -= _gravity;
    //     direction.y = _directionY;
    //     controller.Move(direction * _speed * Time.deltaTime);
    // }
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
       

    }
    
    void fireBomb()
    {
        // spawn bombs
      
        GameObject bomb = Instantiate(_bombPrefab) as GameObject;
        //places bomb in player position.
        bomb.transform.position = this.transform.position + new Vector3(0f, 0.4f, 0f);
        bomb.transform.rotation = this.transform.rotation;
        // aplies a force, in the direction of the player, to the bomb rigidbody (Unity API)
        bomb.GetComponent<Rigidbody>().AddForce(this.transform.forward * 20f);
        

    }
    
    
    
    public void ActivateBomb()
    {   
        // when the player stumbles with BombPower this  function is called and starts the Coroutine
        _bombPower = true;
        Debug.Log("Player collided with the BombPower");
        StartCoroutine(DeactivateBomb());
    }

    IEnumerator DeactivateBomb()
    { 
        yield return new WaitForSeconds(_bombTimeout);
        _bombPower = false;
    }
    
    // Player Damage
    public void Damage()

    {
        // reduce lives by 1
        _lives -= 1;
        Debug.Log("1 life reduced");
        
        // reduce player color channel after damage
        // _colorChannel -= 0.5f;
        
        // _mpb.SetColor("_Color",new Color(_colorChannel, 0, _colorChannel,1f));
        // this.GetComponent<Renderer>().SetPropertyBlock(_mpb);
        
        // player death
        if (_lives == 0)
            
        {
            Destroy(gameObject);
        }


    }
}
    
    



// TODO: decrease Player lives, create player damage