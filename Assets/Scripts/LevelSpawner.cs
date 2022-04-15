using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject level;
    [SerializeField] private int groundCount;
    private Vector3 spawnVector;

    [SerializeField] private int index;

    private void Start()
    {
        spawnVector = transform.position;
        
        for (int i = 0; i < groundCount; i++)
        {
            Instantiate(ground, spawnVector, Quaternion.identity, level.transform);
            spawnVector.x += 1.01f;
            if (i % 9 == 0)
            {
                spawnVector.x = -4.04f;
                spawnVector.z += 1.01f;
            }

        }
        
        /*
        for (int i = 0; i < groundCount; i++)
        {
            spawnVector.x += 1.01f;
            if (i % 7 == 0)
            {
                spawnVector.x = spawnVector.x - 6.06f;
                spawnVector.z += 1.01f;
            }
            Instantiate(ground, spawnVector, Quaternion.identity, level.transform);
        }
        
        */
    }
}
