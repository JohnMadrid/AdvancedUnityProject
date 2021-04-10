using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{   
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
        if (countdown <= 0f && !_explosionOcurred)
        {
            explode();
            _explosionOcurred = true;
        }
        
    }

    void explode()
    {
        
    }
}
