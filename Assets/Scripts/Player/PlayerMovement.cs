using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Animator animator;
    private float movementX;
    private float movementY;
    private bool hit = false;
    private bool attack = false;
    public bool canTakeDamage = true;
    public bool canAttack = true;
    private int lastDirection = 0;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        TakeInput();
    }

    private void TakeInput()
    {
        movementX = Input.GetAxis("Horizontal");
        movementY = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            attack = true;

        }
            
        transform.position += new Vector3(movementX, movementY, 0f) * Time.deltaTime * moveSpeed;

        PlayAnimation();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && attack && canAttack)
        {
            Debug.Log("Saldýr pikaçu");
            other.GetComponent<MiniRoot>().TakeDamage(gameObject);
            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown() 
    {
        canAttack = false;
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }
    private void PlayAnimation()
    {
        if (hit)
        {
            StartCoroutine(HitCooldown());
        }
        if (attack)
        {
            Debug.Log(lastDirection);
            if (lastDirection == 0)
            {

                animator.SetBool("HitUp", false);
                animator.SetBool("HitLeft", false);
                animator.SetBool("HitRight", false);
                animator.SetBool("HitDown", true);
            }
            else if (lastDirection == 1)
            {
                animator.SetBool("HitDown", false);
                animator.SetBool("HitUp", false);
                animator.SetBool("HitRight", false);
                animator.SetBool("HitLeft", true);
            }
            else if (lastDirection == 2)
            {
                animator.SetBool("HitDown", false);
                animator.SetBool("HitLeft", false);
                animator.SetBool("HitRight", false);
                animator.SetBool("HitUp", true);
            }
            else if (lastDirection == 3)
            {
                animator.SetBool("HitDown", false);
                animator.SetBool("HitUp", false);
                animator.SetBool("HitLeft", false);
                animator.SetBool("HitRight", true);
            }
            StartCoroutine(WaitAttackAnimation());
        }
        if (movementX != 0 && movementY == 0)
        {
            if (movementX > 0)
            {
                animator.SetBool("WalkRight", true);
                animator.SetBool("Idle", false);
                animator.SetBool("IdleLeft", false);
                animator.SetBool("IdleRight", false);
                animator.SetBool("IdleUpward", false);
                animator.SetBool("WalkLeft", false);
                animator.SetBool("WalkUpwards", false);
                animator.SetBool("WalkDownwards", false);
                animator.SetBool("HitUp", false);
                animator.SetBool("HitDown", false);
                animator.SetBool("HitLeft", false);
                animator.SetBool("HitRight", false);

                lastDirection = 3;
            }
            else
            {
                animator.SetBool("WalkRight", false);
                animator.SetBool("Idle", false);
                animator.SetBool("IdleLeft", false);
                animator.SetBool("IdleRight", false);
                animator.SetBool("IdleUpward", false);
                animator.SetBool("WalkLeft", true);
                animator.SetBool("WalkUpwards", false);
                animator.SetBool("WalkDownwards", false);
                animator.SetBool("HitUp", false);
                animator.SetBool("HitDown", false);
                animator.SetBool("HitLeft", false);
                animator.SetBool("HitRight", false);
                lastDirection = 1;
            }
            attack = false;
        }
        else if ((movementX == 0 && movementY != 0) || (movementX != 0 && movementY != 0))
        {
            if (movementY > 0)
            {
                animator.SetBool("WalkRight", false);
                animator.SetBool("Idle", false);
                animator.SetBool("IdleLeft", false);
                animator.SetBool("IdleRight", false);
                animator.SetBool("IdleUpward", false);
                animator.SetBool("WalkLeft", false);
                animator.SetBool("WalkUpwards", true);
                animator.SetBool("WalkDownwards", false);
                animator.SetBool("HitUp", false);
                animator.SetBool("HitDown", false);
                animator.SetBool("HitLeft", false);
                animator.SetBool("HitRight", false);
                lastDirection = 2;
            }
            else
            {
                animator.SetBool("WalkRight", false);
                animator.SetBool("Idle", false);
                animator.SetBool("IdleLeft", false);
                animator.SetBool("IdleRight", false);
                animator.SetBool("IdleUpward", false);
                animator.SetBool("WalkLeft", false);
                animator.SetBool("WalkUpwards", false);
                animator.SetBool("WalkDownwards", true);
                animator.SetBool("HitUp", false);
                animator.SetBool("HitDown", false);
                animator.SetBool("HitLeft", false);
                animator.SetBool("HitRight", false);
                lastDirection = 0;
            }
            attack = false;
        }
        else if (movementX == 0 && movementY == 0)
        {
            animator.SetBool("WalkRight", false);
            animator.SetBool("WalkLeft", false);
            animator.SetBool("WalkUpwards", false);
            animator.SetBool("WalkDownwards", false);
            Debug.Log("Last direction : " + lastDirection);

            if (lastDirection == 0)
            {
                animator.SetBool("Idle", true);
                animator.SetBool("IdleLeft", false);
                animator.SetBool("IdleRight", false);
                animator.SetBool("IdleUpward", false);
            }
            else if (lastDirection == 1)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("IdleLeft", true);
                animator.SetBool("IdleRight", false);
                animator.SetBool("IdleUpward", false);
            }
            else if (lastDirection == 2)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("IdleLeft", false);
                animator.SetBool("IdleRight", false);
                animator.SetBool("IdleUpward", true);
            }
            else if (lastDirection == 3)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("IdleLeft", false);
                animator.SetBool("IdleRight", true);
                animator.SetBool("IdleUpward", false);
            }

        }
    }
    private IEnumerator WaitAttackAnimation()
    {
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.41f);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("HitRight") && !animator.GetCurrentAnimatorStateInfo(0).IsName("HitLeft") &&
        !animator.GetCurrentAnimatorStateInfo(0).IsName("HitUp") && !animator.GetCurrentAnimatorStateInfo(0).IsName("HitDown"))
        {
            animator.SetBool("HitUp", false);
            animator.SetBool("HitDown", false);
            animator.SetBool("HitLeft", false);
            animator.SetBool("HitRight", false);
        }
        animator.SetBool("Attack", false);
    }
    public void PlayerHit()
    {
        hit = true;
    }
    private IEnumerator HitCooldown()
    {
        animator.SetBool("PlayerHit", true);
        canTakeDamage = false;
        yield return new WaitForSeconds(0.46f);
        animator.SetBool("PlayerHit", false);
        canTakeDamage = true;
    }
}
