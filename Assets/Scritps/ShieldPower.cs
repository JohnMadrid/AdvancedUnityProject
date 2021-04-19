using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPower : MonoBehaviour
{
    // Start is called before the first frame update
    //Reference to the player
    //public static GameObject PlayerViewReference;
    
    //referecne to playerView
    private Collider PlayerViewC;
    
    //how much time we want the shield power to last. 
    [SerializeField] private float _shieldPowerTimeout = 5f;
    
    void Start()
    {
       //PlayerViewReference = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ActivateShieldPower()
    {
        PlayerViewC.GetComponent<PlayerMovement>()._shieldPowerON = true;
        Debug.Log("the shieldPower has been activated");
        StartCoroutine(DeactivateShieldPower());
    }
    
    IEnumerator DeactivateShieldPower()
    { 
        yield return new WaitForSeconds(_shieldPowerTimeout);
        PlayerViewC.GetComponent<PlayerMovement>()._shieldPowerON = false;
        Debug.Log("The shieldPower has been deactivated");
        
    }
    
    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            
            //other.GetComponent<PlayerMovement>()._shieldPowerON = true;
            PlayerViewC =  other;
            ActivateShieldPower();

            Destroy(this.gameObject);


        }

    }
}
