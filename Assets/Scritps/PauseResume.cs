using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseResume : MonoBehaviour
{

    public void ResumeGame()
    {
        // Resume the game
        Time.timeScale = 1;
        // Set PauseResume canvas inactive
        this.gameObject.SetActive(false);
        // Call the PauseResume function from the SpawnManager 
        GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>().PauseResume();
    }
    
}
