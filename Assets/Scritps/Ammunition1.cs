using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition1 : MonoBehaviour
{
    //this is the speed at which the ammuntion travles towards the player
    [SerializeField] private float ammu1_speed = 8f;
    [SerializeField] private GameObject _enemyone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * ammu1_speed * Time.deltaTime);
    }
}