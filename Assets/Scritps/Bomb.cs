using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float explosionForce = 300f;
    [SerializeField] private float radius = 10f;
    //effects of the explosion
    private GameObject effects;
    //time remaining for the explostion to occur. 
    [SerializeField] private  float delay = 3f;

    private float countdown;

    private bool _explosionOcurred = false;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0.0f && !_explosionOcurred)
        {
            explode();
            _explosionOcurred = true;
        }
        
    }

    void explode()
    {
        //Code here mainly taken from Unity API
        //Instantiate(effects, transform.position, transform.rotation);
        // add damage to the sorrounding objects
        // returns all objects colliding with this sphere
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider i in colliders )
        {
            //get the rigidbody of the i object
            Rigidbody rb =i.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //add force
                rb.AddExplosionForce(explosionForce, transform.position,radius);
                
                
            }

            if (i.CompareTag("BoxObstacle"))
            {
                Destroy(i);
                Debug.Log("Box's been destroyed");
            } 
            // if (i.CompareTag("Enemy1"))
            // {
            //     //i.GetComponent<Enemy1>().Destroy();
            //     Destroy(i);
            //     Debug.Log("Enemy's been destroyed");
            //     
            // }
        }

        //destroy bomb object once the explostion takes place
        Debug.Log("the bomb's been destroyed");
        Destroy(this.gameObject);
    }
}
