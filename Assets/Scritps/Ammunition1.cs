using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition1 : MonoBehaviour
{
    // shooting speed
    [SerializeField]private float _shootingSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // shoots forward
        transform.Translate(Vector3.forward  *_shootingSpeed  * Time.deltaTime);
    }
}
