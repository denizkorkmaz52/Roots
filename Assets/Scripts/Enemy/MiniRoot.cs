using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniRoot : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private Gradient color;
    [SerializeField] private GameObject[] roots;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        for (int i = 0; i < roots.Length; i++)
        {
            Vector3 pos = roots[i].transform.position;
            Vector3 direction = pos - transform.position;
            direction.z = -1;
            Vector3 adjustedDirection = direction / 60;
            GameObject newRoot = Instantiate(GameResources.Instance.rootPrefab, pos, Quaternion.identity);
            newRoot.transform.parent = roots[i].transform;
            newRoot.GetComponent<Roots>().SetPositionFirstNode(pos);
            newRoot.GetComponent<Roots>().SetGrowthDirection(adjustedDirection, 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Hit(other.gameObject);
        }
    }
    private void Hit(GameObject player)
    {
        player.GetComponent<Player>().TakeDamage(damage);
    }
    public void TakeDamage(GameObject player)
    {
        Player playerSC = player.GetComponent<Player>();
        health -= playerSC.HitDamage();
        if (health <= 0)
        {
            playerSC.EnemyKilled();
            Destroy(gameObject);
        }
    }
}
