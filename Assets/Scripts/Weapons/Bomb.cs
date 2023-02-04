using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bomb : MonoBehaviour
{

    [SerializeField] private float speed = 1.5f;
    private ParticleSystem particleSystem;
    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    // Update is called once per frame
    public void SetDest(Vector3 targetPos)
    {
        transform.DOMove(targetPos, speed).SetEase(Ease.Linear).OnComplete(() => { PlayParticle(); });
    }
    
    private void PlayParticle()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        particleSystem.Play();

        StartCoroutine(DestroyBomb());
    }
    private IEnumerator DestroyBomb()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
