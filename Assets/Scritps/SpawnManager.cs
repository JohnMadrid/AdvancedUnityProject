using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _speedHelpPrefab;
    [SerializeField] private GameObject _oneLifePrefab;
    [SerializeField] private GameObject _bombPowerPrefab;
    
    
    // we want to make this false when the Player is destroyed so that the spawning of the virus stops. 
    public bool _spawningEnemy1ON = true;
    private IEnumerator coroutine;
    // Enemy1 spawns with an interval ranging from 3 to 15 seconds
    //private float _timeForNextEnemy1 = Random.Range(3.0f, 15.0f);

    [SerializeField] private GameObject enemy1Prefab;
    // Start is called before the first frame update
    void Start()
    {
        //place some _bombPower throughout the maze
        PlaceObjects(_bombPowerPrefab, 2);
        
        //place some _speedHelpPrefab throughout the maze
        PlaceObjects(_speedHelpPrefab, 2);
        
        //place some _oneLifePrefab throughout the maze
        PlaceObjects(_oneLifePrefab, 2);
        
        coroutine = waitAndSpawnEnemy1();
        StartCoroutine(coroutine);
    }
    
    private IEnumerator waitAndSpawnEnemy1()
    {
        while (_spawningEnemy1ON)

        {
            yield return new WaitForSeconds(Random.Range(3.0f, 15.0f));
            GameObject enemy1Clone = Instantiate(enemy1Prefab) as GameObject;
            //Instantiate(enemy1Prefab);

        }
    }

    public void DestroyEnemy(GameObject _enemy1Clone)
    {
        Destroy(_enemy1Clone);
    }

    void PlaceObjects(GameObject prefabObject, int howManyObjects)
    {
        for (int i = 0; i <= howManyObjects; i++)
        {
            var position1 = new Vector3(Random.Range(0f, 25f), 0f, Random.Range(0f, 25f));
            Instantiate(prefabObject, position1, Quaternion.identity );
        }
    }
}
