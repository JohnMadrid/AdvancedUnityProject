using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{
    
    // create reference to the SpawnManager so that I can grab its DestroyEnemy method.
    //public static GameObject SpawnManagerReference;
    //used to grab the PlayerView from the scene 
    public static GameObject PlayerView;
    
    //private float _enemy1Speed = Random.Range(5.0f, 15.0f);

    [SerializeField] private float _rotationSpeed = 3f;
    //reference to the Enemy1 rigid body
    private Rigidbody _enemyRigidB;

    private Vector3 towardsPlayer;
    Animator anim;
   
    
    void Start()

    {
        anim = GetComponent<Animator>();
        // search for the SpawnManager
        //SpawnManagerReference = GameObject.FindWithTag("SpawnManager");
        // search for the PlayerView
        PlayerView = GameObject.FindWithTag("Player");
        
        _enemyRigidB = this.GetComponent<Rigidbody>();
        Vector3 randomizedPosition = new Vector3(Random.Range(8f, 15f), 0.2f, Random.Range(8f, 15f));
        transform.position = PlayerView.transform.position + randomizedPosition;
        Debug.Log("New enemy spawned at relative position ("+randomizedPosition.x+","+randomizedPosition.z+") to PlayerView");
        StartCoroutine(ChaseRoutine());

    }
    private void Reset()
    {
        if (gameObject.GetComponent<CapsuleCollider>() == null)
        {
            Debug.Log("Enemy1 has no collider and must DIEEEEE");
            CapsuleCollider capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<CapsuleCollider>() == null)
        {
            Destroy(gameObject);
        }

        if (!(null == PlayerView))
        {
            //where the player is
            Vector3 direction = PlayerView.transform.position - transform.position;
        
        
            // rotate Enemy1 in the direction of the player, so that it looks at the player
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
        
            direction.Normalize();
           
            if (anim.GetInteger("condition") == 1)
            {
                _enemyRigidB.MovePosition(transform.position + (direction * Random.Range(3.0f, 8.0f) * Time.deltaTime));
            }
        }

    }
    

    IEnumerator ChaseRoutine()
    {
        anim.SetInteger("condition", 0);
        yield return new WaitForSeconds(5f);
        // Debug.Log("condition 0 is ON");
        anim.SetInteger("condition",1);
        // Debug.Log("condition 1 is ON");
        // anim.SetBool("spawn", true);
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
            Debug.Log("Enemy hit player");
            Destroy(this.gameObject);
            other.GetComponent<PlayerMovement>().Damage();
            
        }

    }
}
