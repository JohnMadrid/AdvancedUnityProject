using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private IEnumerator coroutine;
    [SerializeField] private float _timeForNextEnemy1 = 20f;

    [SerializeField] private GameObject enemy1Prefab;
    // Start is called before the first frame update
    void Start()
    {
        coroutine = waitAndSpawnEnemy1();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator waitAndSpawnEnemy1()
    {
        while (true)

        {
            yield return new WaitForSeconds(_timeForNextEnemy1);
            Instantiate(enemy1Prefab);

        }
    }
}
