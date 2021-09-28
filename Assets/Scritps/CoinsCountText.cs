using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCountText : MonoBehaviour
{
    // for Lives text increase
    [SerializeField] public GameObject playerMovementScript;
    public Image CoinsIconColor;
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
        ChangeCoinsIconColor();
    }

    void GainedLives()
    {
        if (CoinsTotal >= 20)
        {
            // increase lives for 20 coins collected
            playerMovementScript.GetComponent<PlayerMovement>()._lives += 1;
            // set coins to zero
            playerMovementScript.GetComponent<PlayerMovement>()._coinRewards = 0;
        }
        
    }

    void ChangeCoinsIconColor()
    {
        if (CoinsTotal == 0)
        {
            CoinsIconColor.color = new Color32(178, 176, 93, 100);
        }

        else if (CoinsTotal >= 1 && CoinsTotal < 5)
        {
            CoinsIconColor.color = new Color32(215,190, 101, 198);
        }

        else if (CoinsTotal >= 5 & CoinsTotal < 9)
        {
            CoinsIconColor.color = new Color32(229, 206, 80, 225);
        }
        else if (CoinsTotal >= 9 & CoinsTotal < 16)
        {
            CoinsIconColor.color = new Color32(241, 196, 38, 240);
        }
        else if (CoinsTotal >= 16)
        {
            CoinsIconColor.color = new Color32(255, 180, 0, 255);
        }

    }
}
