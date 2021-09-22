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
    [SerializeField] private GameObject _door;
    
    
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
        PlaceDoors(_door);
        
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
    
    void PlaceDoors(GameObject doorPrefab)
    {
        
        var position_door = new Vector3(-32.9099998f,-1.32000005f,35.0499992f);
        
        GameObject prefab_door=Instantiate(doorPrefab, position_door, Quaternion.Euler(-1.764f,-85.179f,-0.138f));
        prefab_door.transform.localScale= new Vector3(1.43771684f,1.2507f,1) ;
        
        var position_door_2 = new Vector3(-2.34f,-1.48f,-10.05f);
        GameObject prefab_door_2=Instantiate(doorPrefab, position_door_2, Quaternion.Euler(-1.764f,-85.179f,-0.138f));
        prefab_door_2.transform.localScale= new Vector3(1.496409f,1.261857f,1);
        
        var position_door_3 = new Vector3(10.55f,-0.63f,-2.58f);
        GameObject prefab_door_3=Instantiate(doorPrefab, position_door_3, Quaternion.Euler(-1.764f,-85.179f,-0.138f));
        prefab_door_3.transform.localScale= new Vector3(1.4964409f,1.08769f,1);
        
    }
}
