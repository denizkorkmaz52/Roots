using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoot : MonoBehaviour
{
    [SerializeField] private GameObject[] points;
    [SerializeField] private GameObject building;
    private void Start()
    {
        CreateRoots();
    }
    private void CreateRoots()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 pos = points[i].transform.position;
            Vector3 direction = pos - transform.position;
            Vector3 adjustedDirection = direction / 60;
            GameObject newRoot = Instantiate(GameResources.Instance.rootPrefab, pos, Quaternion.identity);
            newRoot.transform.parent = points[i].transform;
            newRoot.GetComponent<Roots>().SetPositionFirstNode(pos);
            newRoot.GetComponent<Roots>().SetBuilding(building);
            newRoot.GetComponent<Roots>().SetGrowthDirection(adjustedDirection, 0);

        }
    }
}
