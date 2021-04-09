using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    // Start is called before the first frame update
    // stores mouse position
    private Vector3 directionFire;
    //reference to the gun
    //[SerializeField] private GameObject playerView;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   //get mouse position world view. We want to shoot where the player is lookig at 
        directionFire = transform.GetComponent<Camera>()
            .ScreenToViewportPoint(new Vector3(Input.mousePosition.x, 
                Input.mousePosition.y, 
                transform.position.z));
        // difference between player and where the mouse is

        //Vector3 diff = directionFire - playerView.transform.position;
        //converts radians to degrees 
        //float rotatez = Mathf.Atan2(diff.y , diff.x) * Mathf.Rad2Deg;
        //rotate in rotatez degree radious
        //playerView.transform.rotation = Quaternion.Euler(0.0f, 0.0f,rotatez);
        
    }
}


/*void fireProjectile()
{
    GameObject bullet = Instantiate(projectilePrefab) as GameObject;
    bullet.transform.position = this.transform.position;
    Vector3 diff = directionFire - this.transform.position;
    float rotatez = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
    bullet.transform.rotation = Quaternion.Euler(0f,0f,rotatez);
    float distance = diff.magnitude;
    Vector3 direction = diff / distance;
    direction.Normalize();
    bullet.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
}*/