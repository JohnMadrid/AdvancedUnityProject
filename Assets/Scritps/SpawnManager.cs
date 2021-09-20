using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _speedHelpPrefab;
    [SerializeField] private GameObject _oneLifePrefab;
    [SerializeField] private GameObject _bombPowerPrefab;
    [SerializeField] private GameObject _shieldPrefab;
    
    
    // we want to make this false when the Player is destroyed so that the spawning of the virus stops. 
    public bool _spawningEnemy1ON = true;
    private IEnumerator coroutine;

    private GameObject[] enemies;
    // Enemy1 spawns with an interval ranging from 3 to 15 seconds
    //private float _timeForNextEnemy1 = Random.Range(3.0f, 15.0f);
    
    public static GameObject PlayerView;

    [SerializeField] private GameObject enemy1Prefab;
    // Start is called before the first frame update
    void Start()
    {
        PlayerView = GameObject.FindWithTag("Player");
        
        //place some _bombPower throughout the maze
        PlaceObjects(_bombPowerPrefab, 2);
        
        //place some _speedHelpPrefab throughout the maze
        PlaceObjects(_speedHelpPrefab, 2);
        
        //place some _oneLifePrefab throughout the maze
        PlaceObjects(_oneLifePrefab, 3);
        
        //place some shields around the maze
        PlaceObjects(_shieldPrefab, 3);
        
        coroutine = waitAndSpawnEnemy1();
        StartCoroutine(coroutine);
    }
    
    private IEnumerator waitAndSpawnEnemy1()
    {
        while (_spawningEnemy1ON)
        {
            yield return new WaitForSeconds(Random.Range(6.0f, 15.0f));
            if (_spawningEnemy1ON)
            {
                GameObject enemy1Clone = Instantiate(enemy1Prefab) as GameObject;
            }
        }
    }

    public void DestroyAllEnemies()
    {
        Debug.Log("Destroying remaining enemies");
        enemies = GameObject.FindGameObjectsWithTag("Enemy1");
        foreach (var enemy in enemies){
            Destroy(enemy);
        }
    }

    void PlaceObjects(GameObject prefabObject, int howManyObjects)
    {
        for (int i = 0; i <= howManyObjects; i++)
        {
            var position1 = new Vector3(Random.Range(0, 25f), -1.06f, Random.Range(0, 25f));
            Instantiate(prefabObject, position1, Quaternion.identity );
        }
    }
}
