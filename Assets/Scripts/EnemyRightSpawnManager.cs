using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRightSpawnManager : MonoBehaviour
{
    [SerializeField] private int secondEnemy;
    [SerializeField] private List<GameObject> rightEnemy;


    private void Update()
    {
        for (int i = 0; i < rightEnemy.Count; i++)
        {
            if (rightEnemy[i].gameObject.layer != 9)
            {
                rightEnemy.RemoveAt(i);
            }
        }
    }
    private void Start()
    {
        InvokeRepeating(nameof(EnemySpawn), 0f, 2f);
    }

    void EnemySpawn()
    {
        for (int i = 0; i < secondEnemy; i++)
        {
            Vector3 vec = transform.position;
            if (GameManager.instance.isGameStart && !GameManager.instance.isGameOver && !GameManager.instance.isGameWin)
            {

                rightEnemy.Add(ObjectPool.Instance.SpawnFromPool("EnemyRight", vec, Quaternion.Euler(Vector3.up * 90)));
                foreach (GameObject gameObject in rightEnemy)
                {
                    gameObject.layer = 8;
                }
            }
        }
    }
}
