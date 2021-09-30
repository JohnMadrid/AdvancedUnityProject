using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngineInternal;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    // speed and shield Icons on the screen menu
    public Image SpeedIconColor;
    public Image ShieldIconColor;
    public Image GrenadeIconColor;
    
    //shield
    [SerializeField] private GameObject PlayerShield;
    
    [SerializeField] private GameObject FinishedCanvas;
    
    //how much time we want the shield power to last. 
    [SerializeField] private float _shieldPowerTimeout = 5f;
    
    // This variable is modified from the ShieldPower script to know if the shield power is on.
    private bool _shieldPowerON = false;
    // Jumping settings
    //private float _jumpSpeed = 7f;
    public Rigidbody rb;
    private bool _playerOnGround = true;

    public int _lives = 3;

    public int _coinRewards = 0;
    
    
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
    //how much force to add to the ejection of the projectile. 
    [SerializeField] private float thrust = 70f;

    //projectile prefab
    [SerializeField] private GameObject projectilePrefab;

    //projectile ammunition speed
    [SerializeField] private float projectileSpeed = 20f;

    // motor that drives the player
    public CharacterController controller;
    public Transform cam;

   //player speed
    private float _speed = 9f;

    private float _turnSmoothTime = 0.1f;

    private float _turnSmoothVelocity;


    private Vector3 direction;
    private Vector3 moveDirection;
    private float gravity = 9f;
    
    // animation
    Animator anim;
    private GameObject spanM;
    
    
    private static readonly int Condition = Animator.StringToHash("condition");

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
        

    // Update is called once per frame
    void Update()

    {
        PlayerMoves();
        getInput2();

    }
    
    

    // player movement
    void PlayerMoves()
    {
        if (anim.GetBool("dead"))
        {
            return;
        }
        
        // read player inputs on both x and y axis
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        
        if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.D))
        {
            if (anim.GetBool("attacking"))
            {
                return;
            }
        
            anim.SetBool("running", true); 
            anim.SetInteger("condition", 1);
        
        }
        
        if (Input.GetKeyUp(KeyCode.W) | Input.GetKeyUp(KeyCode.A) | Input.GetKeyUp(KeyCode.S) | Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("running", false);
            anim.SetInteger("condition", 0);
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

        }
        
    }

  

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attacking();
        }
    }

    void getInput2()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (anim.GetBool("running") == true)
            {
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);
            }
            if (anim.GetBool("running") == false)
            {
                Attacking();
            }
        }
    }

    

    IEnumerator AttackRoutine()
    {
        anim.SetBool("attacking", true);
        anim.SetInteger("condition", 2);
        yield return new WaitForSeconds(0.3f);
        if (_bombPower)
        {
            fireBomb();
        }
        else
        {
            fireProjectile();
        }
        anim.SetInteger("condition", 0);
        anim.SetBool("attacking", false);
    }

    void Attacking()
    {
        StartCoroutine(AttackRoutine());

    }

    void fireProjectile()
    {

        // spawn projectiles
        GameObject bullet = Instantiate(projectilePrefab) as GameObject;
        //places the bullet in player position.
        bullet.transform.position = this.transform.position;
        bullet.transform.rotation = this.transform.rotation;
        // applies a force, in the direction of the player, to the bullet rigidbody (Unity API)
        bullet.GetComponent<Rigidbody>().AddForce(this.transform.forward * 80f);
       

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
        GrenadeIconColor.color = new Color32(135, 140, 92,255);
        // Debug.Log("Player collided with the BombPower");
        StartCoroutine(DeactivateBomb());
    }

    IEnumerator DeactivateBomb()
    { 
        yield return new WaitForSeconds(_bombTimeout);
        _bombPower = false;
        GrenadeIconColor.color = new Color32(130,146,67,100);
    }
    
    // Player Damage
    public void Damage()

    {
        if (!_shieldPowerON)
        {
            // reduce lives by 1
            _lives -= 1;
        }
      

        // player death
        if (_lives == 0)
        {
            //Debug.Log("player is dying");
            anim.SetBool("dead", true);
            SpawnM.GetComponent<SpawnManager>()._spawningEnemy1ON = false;
            StartCoroutine(DestroyRoutine());
        }
        
        IEnumerator DestroyRoutine()
        {
            //Debug.Log("Player's destroy routine called");
            anim.SetInteger("condition", 3);
            yield return new WaitForSeconds(4.2f);
            //Debug.Log("Now destroy player");
            Destroy(gameObject);

            // Destroy all other enemies
            GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>().DestroyAllEnemies();
        }
        


    }

    public void ActivateSpeed()
    {
        SpeedIconColor.color = new Color32(1,94, 255, 255);
        _speed = 18f;
        StartCoroutine(DeactivateSpeed());
    }
    
    IEnumerator DeactivateSpeed()
    { 
        yield return new WaitForSeconds(_speedHelpTimeout);
        SpeedIconColor.color = new Color32(57,170, 236, 100);
        _speed = 6f;
        
    }
    
    
    
    public void ActivateShieldPower()
    {
        _shieldPowerON = true;
        PlayerShield.SetActive(true);
        ShieldIconColor.color = new Color32(255, 97, 0,255);
        
        StartCoroutine(DeactivateShieldPower());
    }
    
    IEnumerator DeactivateShieldPower()
    {
        yield return new WaitForSeconds(_shieldPowerTimeout);
        ShieldIconColor.color = new Color32(224, 108, 37,100);
        _shieldPowerON = false;
        PlayerShield.SetActive(false);
        
    }

    public void DeactivateShieldOnDoor()
    {
        PlayerShield.SetActive(false);
        _shieldPowerON = false;
    }
    
    // Pause game after reaching the goal
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            FinishedCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
    