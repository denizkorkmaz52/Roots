using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameResources : MonoBehaviour
{
    private static GameResources instance;

    public static GameResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources");
            }
            return instance;
        }
    }


    public GameObject rootPrefab;
    public GameObject bombPrefab;
    public GameObject level1EnemyPrefab;
    public GameObject level2EnemyPrefab;
    public GameObject level3EnemyPrefab;
}
