using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class LivesCount : MonoBehaviour
{
    [SerializeField] public GameObject player_movement;
    public Image LivesIconColor;
    private TextMeshProUGUI TextmesH;
    private int livesValue;



    // Start is called before the first frame update
    void Start()
    {
        TextmesH = gameObject.GetComponent<TextMeshProUGUI>();
        livesValue = player_movement.GetComponent<PlayerMovement>()._lives;
        
    }

    // Update is called once per frame
    void Update()
    {
        livesValue = player_movement.GetComponent<PlayerMovement>()._lives;
        
        // set lives values as new text
        TextmesH.text = livesValue.ToString();
        
        // change colors
        ChangeLivesIconColor();

    }

    void ChangeLivesIconColor()
    {
        if (livesValue == 2)
        {
            LivesIconColor.color = new Color32(245, 144, 0, 255);
        }
        else if (livesValue == 1)
        {
            LivesIconColor.color = new Color32(255, 64, 64, 255);
        }
        else if (livesValue == 0)
        {
            LivesIconColor.color = new Color32(255, 0, 0, 255);
        }
    }
}
