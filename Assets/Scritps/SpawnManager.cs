using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // we want to make this false when the Player is destroyed so that the spawning of the virus stops. 
    private bool _spawningEnemy1ON = true;
    private IEnumerator coroutine;
    // Enemy1 spawns with an interval ranging from 3 to 15 seconds
    //private float _timeForNextEnemy1 = Random.Range(3.0f, 15.0f);

    [SerializeField] private GameObject enemy1Prefab;
    // Start is called before the first frame update
    void Start()
    {
        coroutine = waitAndSpawnEnemy1();
        StartCoroutine(coroutine);
    }
    
    private IEnumerator waitAndSpawnEnemy1()
    {
        while (_spawningEnemy1ON)

        {
            yield return new WaitForSeconds(Random.Range(3.0f, 15.0f));
            //GameObject enemy1Clone = Instantiate(enemy1Prefab) as GameObject;
            Instantiate(enemy1Prefab);

        }
    }
}
