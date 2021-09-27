using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _coinLocationPrefab;
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

        PlaceCoins(_coinLocationPrefab);
        
        coroutine = waitAndSpawnEnemy1();
        StartCoroutine(coroutine);
    }

    void Update()
    {
        PauseResume();
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
        // front left door (with respect to the player)
        var position_door = new Vector3(-32.8899994f,-1.38066602f,34.9647064f) ;
        
        GameObject prefab_door=Instantiate(doorPrefab, position_door, Quaternion.Euler(1.24822605f,270.8508f,0.619182825f));
        prefab_door.transform.localScale= new Vector3(1.43771684f,1.2507f,1f)  ;
        
        // back door
        var position_door_2 = new Vector3(0.4f,-0.585235596f,-13.78f);
        GameObject prefab_door_2=Instantiate(doorPrefab, position_door_2, Quaternion.Euler(0.468579382f,359.080872f,0.958902121f) );
        prefab_door_2.transform.localScale= new Vector3(1.515264f,1.11842f,4.8f);
        
        // right behind wall door
        var position_door_3 = new Vector3(11.8400002f,-0.415545702f,-2.67000008f);
        GameObject prefab_door_3=Instantiate(doorPrefab, position_door_3, Quaternion.Euler(358.063354f,269.079468f,359.320801f));
        prefab_door_3.transform.localScale= new Vector3(1.39439583f,1.08769f,4.80000019f);
        
    }

    void PauseResume()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
        }
        
    }
   

    void PlaceCoins(GameObject coin_prefab)
    {
   
        List<Tuple<float, float, float>> positionList = new List<Tuple<float, float, float>>()
        {
            Tuple.Create(-3.41000009f,-0.994000018f,40.0200005f),
            Tuple.Create(-23.8299999f,-0.994000018f,33.8699989f),
            Tuple.Create(-46.6800003f,-0.994000018f,33.9300003f),
            Tuple.Create(-37.2299995f,-0.994000018f,56.6800003f),
            Tuple.Create (-54.7000008f,-0.994000018f,73.3000031f),
            Tuple.Create (-64.0400009f,-0.994000018f,73.1999969f),
            Tuple.Create (-85f,-0.994000018f,111.900002f)
          
        };
        
        foreach (var item in positionList)
        {
            float x = item.Item1;
            float y = item.Item2;
            float z = item.Item3;
            Instantiate(coin_prefab, new Vector3(x,y,z),Quaternion.Euler(0,0,0));
        }
        
        
    }
}
