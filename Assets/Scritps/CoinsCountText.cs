using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCountText : MonoBehaviour
{
    // for Lives text increase
    [SerializeField] public GameObject playerMovementScript;
    // public Image CoinsIconColor;
    private TextMeshProUGUI TextCoinsCount;
    private int CoinsTotal;
    
    // Start is called before the first frame update
    void Start()
    {
        TextCoinsCount = gameObject.GetComponent<TextMeshProUGUI>();
        CoinsTotal = playerMovementScript.GetComponent<PlayerMovement>()._coinRewards;
    }

    // Update is called once per frame
    void Update()
    {
        // set total coins to the current value
        CoinsTotal = playerMovementScript.GetComponent<PlayerMovement>()._coinRewards;
        // rewrite total coins when collected
        TextCoinsCount.text = CoinsTotal.ToString();
        GainedLives();
    }

    void GainedLives()
    {
        if (playerMovementScript.GetComponent<PlayerMovement>()._coinRewards == 5)
        { 
            // increase lives by one 
            playerMovementScript.GetComponent<PlayerMovement>()._lives += 1;
            
        }
        
    }
}
