using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LivesCount : MonoBehaviour
{

    public static int livesValue = 3;
    TextMeshPro lives;
    
    // Start is called before the first frame update
    void Start()
    {
        lives = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        lives.text = "L" + livesValue;
    }
}
