using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Roots : MonoBehaviour
{
    [SerializeField] private float growthSpeed = 0.3f;
    [SerializeField] private Vector3 growthDirection;
    [SerializeField] private int newRootInitAt = 100;
    [SerializeField] private float noiseX = 0.01f;
    [SerializeField] private float noiseY = 0.01f;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private int childRootLimit = 2;
    [SerializeField] private GameObject building;
    [SerializeField] private GameManager gm;
    private int createdChildRoot = 0;

    private LineRenderer lineRenderer;
    private bool isDirectionDangerous = false;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //edgeCollider = GetComponent<EdgeCollider2D>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        //transform.localScale += growthDirection * Time.deltaTime / growthSpeed;
        //SetEdgeCollider();
        if (isDirectionDangerous)
        {
            Vector3 pos = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
            Vector3 buildingPos = building.transform.position;
            if (pos.x <= buildingPos.x && (pos.y <= (buildingPos.y + 0.2f) && pos.y >= (buildingPos.y - 0.2f)))
            {
                Debug.Log("Eve Ulaþtý");
                gm.SetGameState(GameState.lose);
            }
        }
    }

    private IEnumerator EnlargeRoot(int type)
    {
        if (type == 0)
        {
            while (lineRenderer.positionCount < 301)
            {
                Vector3 lastPos = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

                float rndNoiseX = Random.Range(-noiseX, noiseX);
                float rndNoiseY = Random.Range(-noiseY, noiseY);
                Vector3 noiseAdded = new Vector3(growthDirection.x + rndNoiseX, growthDirection.y + rndNoiseY, 0);
                Vector3 newPos = lastPos + noiseAdded;

                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPos);

                //GenerateMeshCollider();

                if (lineRenderer.positionCount % newRootInitAt == 0 && createdChildRoot < childRootLimit)
                {
                    float rangeNoise = Random.Range(-10, 10);
                    var rnd = new System.Random();
                    int rndValue = rnd.Next(0, 2);
                    int sign = 0;
                    if (rndValue == 0)
                        sign = -1;
                    else if (rndValue == 1)
                        sign = 1;
                    float newRange = (45 * sign) + rangeNoise;
                    //Debug.Log(newRange);
                    //Vector3 direction = lineRenderer.GetPosition(lineRenderer.positionCount - 2) - lineRenderer.GetPosition(lineRenderer.positionCount - 3);

                    Vector3 rotatedVector = Quaternion.AngleAxis(newRange, Vector3.forward) * growthDirection;

                    GameObject newRoot = Instantiate(GameResources.Instance.rootPrefab, lineRenderer.GetPosition(lineRenderer.positionCount - 80), Quaternion.identity);
                    newRoot.transform.parent = transform;
                    newRoot.GetComponent<Roots>().SetPositionFirstNode(lineRenderer.GetPosition(lineRenderer.positionCount - 80));
                    newRoot.GetComponent<Roots>().SetBuilding(building);
                    newRoot.GetComponent<Roots>().SetGrowthDirection(rotatedVector, 0);
                    createdChildRoot++;
                }
                yield return new WaitForSeconds(growthSpeed);
            }
        }
        else if (type == 1)
        {
            while (lineRenderer.positionCount < 20)
            {
                Vector3 lastPos = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

                float rndNoiseX = Random.Range(-noiseX, noiseX);
                float rndNoiseY = Random.Range(-noiseY, noiseY);
                Vector3 noiseAdded = new Vector3(growthDirection.x + rndNoiseX, growthDirection.y + rndNoiseY, 0);
                Vector3 newPos = lastPos + noiseAdded;
                newPos.z = -1;
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPos);

                yield return new WaitForSeconds(growthSpeed);
            }
        }
        
    }
    public void SetGrowthDirection(Vector3 newDirection, int type)
    {
        growthDirection = newDirection;
        StartCoroutine(EnlargeRoot(type));
    }
    public void SetPositionFirstNode(Vector3 newPos)
    {
        lineRenderer.SetPosition(0, newPos);
    }
    public void SetBuilding(GameObject building)
    {
        this.building = building;
        if (building != null)
        {
            Vector3 buildingDirection = building.transform.position - transform.position;
            if (Mathf.Abs(Vector3.Angle(growthDirection, buildingDirection)) < 5)
            {
                isDirectionDangerous = true;
            }
        }
    }

    /*private void SetEdgeCollider()
    {
        List<Vector2> edges = new List<Vector2>();

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 point = lineRenderer.GetPosition(i);
            edges.Add(new Vector2(point.x, point.y));
        }

        edgeCollider.SetPoints(edges);
    }*/
}
