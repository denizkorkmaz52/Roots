using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private float damage = 15;

    [SerializeField] private int bombCount = 3;
    [SerializeField] private Transform throwPosition;

    [SerializeField] private GameManager gm;
    [SerializeField] private SpriteRenderer[] sprites;
    private PlayerMovement playerMovement;


    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
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
        if (Input.GetMouseButtonDown(0))
        {
            PlayerAnim(0);
        }
    }

    private void PlayerAnim(int type)
    {
        if (type == 0)
        {
            //Attack anim
        }
        else if (type == 1)
        {
            //Hit anim
            playerMovement.PlayerHit();
        }
    }
    public void TakeDamage(float damage)
    {
        if (playerMovement.canTakeDamage)
        {
            health -= damage;
            StartCoroutine(HitEffect());
            PlayerAnim(1);
            if (health <= 0)
            {
                Debug.Log("Öldün");
                gm.SetGameState(GameState.lose);
            }
        }
       
    }
    public IEnumerator HitEffect()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = Color.red;
        }
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = Color.white;
        }
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = Color.red;
        }
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = Color.white;
        }

    }
    public float HitDamage()
    {
        return damage;
    }
    public void EnemyKilled()
    {
        gm.EnemyKilled();
    }

  

}
