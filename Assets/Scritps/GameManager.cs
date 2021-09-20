using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this GameManager can be accessed from any other script without any need to reference it.
//So I can, for example, place functions here and called them from any other script using Gamemanager.Instance.my_function 

//TODO: move gameOver functionality to this  script and turn the whole thing into a an event
//TODO: Implement object pooling: instead of destroying the enimies(or any other object) keep them in memory and use them when needed again.
public class GameManager : MonoBehaviour
    //this sort of architecture is called singleton
{
    private static GameManager _instance;

    public static GameManager Instance
    {    //it means that you can only get the Value of _instance you cannot set it. And when you access Instance it returns _instance
        get
        {
            return _instance;
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {   //ensure there's only one GameManager in the entire game. 
        if (_instance !=null && _instance!=this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}