using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngineInternal;


public class PlayerMovement : MonoBehaviour
{
    
    //shield
    [SerializeField] private GameObject PlayerShield;
    
    
    //how much time we want the shield power to last. 
    [SerializeField] private float _shieldPowerTimeout = 5f;
    
    // to know if the shield power is on. This variable is modified from the ShieldPower script
    private bool _shieldPowerON = false;
    // Jumping settings
    private float _jumpSpeed = 7f;
    public Rigidbody rb;
    private bool _playerOnGround = true;

     
    public int _lives = 3;
    
    
    // keep track of direction
    private float _directionY;
    
    
    //reference to the SpawnManager
    [SerializeField] private GameObject SpawnM;
    
    // bomb prefab
    [SerializeField] private GameObject _bombPrefab;
    // how much time BombPower lasts
    [SerializeField]private float _bombTimeout = 20f;
    
    // how much time SpeedHelp lasts
    [SerializeField] private float _speedHelpTimeout = 10f;
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

   //player speed
    private float _speed = 6f;

    private float _turnSmoothTime = 0.1f;

    private float _turnSmoothVelocity;


    private Vector3 direction;
    private Vector3 moveDirection;
    private float gravity = 9f;
    
    // animation
    Animator anim;
    
    private static readonly int Condition = Animator.StringToHash("condition");

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
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
        
        if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("condition", 1);
            
        }

        if (Input.GetKeyUp(KeyCode.W) | Input.GetKeyUp(KeyCode.A) | Input.GetKeyUp(KeyCode.S) | Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("condition", 0);
            Debug.Log("NOT moving");
        }

        // player and camera move together
        if (direction.magnitude >= 0.1f)
        {
            // Player rotates and faces the direction in which it is moving and moves where camara is pointing
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            // smooth player rotation movement
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                _turnSmoothTime);

            // Calculate the desired direction of movement depending on the camera movement
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection.normalized * (_speed * Time.deltaTime));
            
            // if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.D))
            // {
            //     anim.SetInteger(Condition, 1);
            //     Calculate the desired direction of movement depending on the camera movement
            //     moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //     controller.Move(moveDirection.normalized * (_speed * Time.deltaTime));
            // }

            // if (Input.GetKeyUp(KeyCode.W) | Input.GetKeyUp(KeyCode.A) | Input.GetKeyUp(KeyCode.S) |
            //     Input.GetKeyUp(KeyCode.D))
            // {
            //     // anim.SetInteger(Condition, 0);
            //     moveDirection = new Vector3(0, 0, 0);
            //     // controller.Move(moveDirection.normalized * (_speed * Time.deltaTime * 0)); 
            // }

        }




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
        if (!_shieldPowerON)
        {
            // reduce lives by 1
            _lives -= 1;
            Debug.Log("1 life reduced");
        }
      

        // player death
        if (_lives == 0)
            
        {
            //stop the spawning of  Enemy1
            SpawnM.GetComponent<SpawnManager>()._spawningEnemy1ON = false;
            
            Destroy(gameObject);
        }


    }

    public void ActivateSpeed()
    {
        _speed = 20f;
        StartCoroutine(DeactivateSpeed());
    }
    
    IEnumerator DeactivateSpeed()
    { 
        yield return new WaitForSeconds(_speedHelpTimeout);
        _speed = 6f;
    }
    
    
    
    public void ActivateShieldPower()
    {
        _shieldPowerON = true;
        PlayerShield.SetActive(true);
        Debug.Log("the shieldPower has been activated");
        StartCoroutine(DeactivateShieldPower());
    }
    
    IEnumerator DeactivateShieldPower()
    { 
        yield return new WaitForSeconds(_shieldPowerTimeout);
        _shieldPowerON = false;
        PlayerShield.SetActive(false);
        Debug.Log("The shieldPower has been deactivated");
        
    }
    
    
}
    
    



// TODO: player jump