using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class EnemyPool
    {
        public string tag;
        public GameObject enemyPrefab;
        public int size;
    }

    public List<EnemyPool> enemyPools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public static ObjectPool Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (EnemyPool enemyPool in enemyPools)
        {
            Queue<GameObject> bulletObject = new Queue<GameObject>();

            for (int i = 0; i < enemyPool.size; i++)
            {
                GameObject obj = Instantiate(enemyPool.enemyPrefab, transform.position, Quaternion.Euler(0, 180, 0));
                obj.SetActive(false);
                bulletObject.Enqueue(obj);
            }
            poolDictionary.Add(enemyPool.tag, bulletObject);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IEnemyObject bulletObj = objectToSpawn.GetComponent<IEnemyObject>();

        if (bulletObj != null)
        {
            bulletObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}