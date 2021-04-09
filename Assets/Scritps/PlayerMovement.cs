using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [SerializeField]

public class PlayerMovement : MonoBehaviour
{
    //private float angle;
    //projectile prefab
    [SerializeField] private GameObject projectilePrefab;
    //projectile ammunition speed
    [SerializeField]private float projectileSpeed = 10f;
    // motor that drives the player
    public CharacterController controller;
    public Transform cam;
    
    public float speed = 6f;

    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;
    // public
    
    
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f,vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            
        }
        // shoot with left button mouse
        // if (Input.GetMouseButtonDown(0))
        // {
        //     //firing the projectile
        //     fireProjectile();
        //
        // }
    }

    // void fireProjectile()
    // {
    //     GameObject bullet = Instantiate(projectilePrefab) as GameObject;
    //     bullet.transform.position = this.transform.position;
    //     bullet.transform.rotation = Quaternion.Euler(0f,angle,0f);
    // }
}

// TODO: fixate camara to the back of the player so that it does not need mouse movement