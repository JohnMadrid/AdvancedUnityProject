using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseResume : MonoBehaviour
{
    //public GameObject pausedCanvas;
    
    public void ResumeGame()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
    
}
