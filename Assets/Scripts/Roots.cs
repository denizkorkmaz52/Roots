using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Roots : MonoBehaviour
{
    [SerializeField] private float growthSpeed = 0.02f;
    [SerializeField] private Vector3 growthDirection = new Vector3(0.1f, 0.05f, 0);
    [SerializeField] private int newRootInitAt = 10;
    [SerializeField] private float noiseX = 0.05f;
    [SerializeField] private float noiseY = 0.05f;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private int childRootLimit = 2;
    private int createdChildRoot = 0;

    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        //transform.localScale += growthDirection * Time.deltaTime / growthSpeed;
        //SetEdgeCollider();
    }

    private IEnumerator EnlargeRoot()
    {
        while (true)
        {
            Vector3 lastPos = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

            float rndNoiseX = Random.Range(-noiseX, noiseX);
            float rndNoiseY = Random.Range(-noiseY, noiseY);
            Vector3 noiseAdded = new Vector3(growthDirection.x + rndNoiseX, growthDirection.y + rndNoiseY);
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

                GameObject newRoot = Instantiate(GameResources.Instance.rootPrefab, lineRenderer.GetPosition(lineRenderer.positionCount - 8), Quaternion.identity);
                newRoot.transform.parent = transform;               
                newRoot.GetComponent<Roots>().SetPositionFirstNode(lineRenderer.GetPosition(lineRenderer.positionCount - 8));
                newRoot.GetComponent<Roots>().SetGrowthDirection(rotatedVector);
                createdChildRoot++;
            }
            yield return new WaitForSeconds(growthSpeed);
        }        
    }
    private IEnumerator AdjustGrowth(Vector3 startPos, Vector3 endPos)
    {
        Vector3 nextPos = startPos;
        Vector3 length = (endPos - startPos) / 30;

        while (nextPos != endPos)
        {


            yield return new WaitForSeconds(0.02f);
        }
    }
    public void SetGrowthDirection(Vector3 newDirection)
    {
        growthDirection = newDirection;
        StartCoroutine(EnlargeRoot());
    }
    public void SetPositionFirstNode(Vector3 newPos)
    {
        lineRenderer.SetPosition(0, newPos);
    }

    /*private void GenerateMeshCollider()
    {
        MeshCollider collider = GetComponent<MeshCollider>();

        if (collider == null)
        {
            collider = gameObject.AddComponent<MeshCollider>(); 
        }

        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh);
        collider.sharedMesh = mesh;
    }
    */
   /* private void SetEdgeCollider()
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
