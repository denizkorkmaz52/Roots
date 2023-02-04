using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int bombCount = 3;
    [SerializeField] private Transform throwPosition;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (bombCount > 0)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.nearClipPlane;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

                GameObject bomb = Instantiate(GameResources.Instance.bombPrefab, throwPosition.position, Quaternion.identity);
                bomb.GetComponent<Bomb>().SetDest(worldPosition);
            }
        }
    }
}
