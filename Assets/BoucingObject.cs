using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BoucingObject : MonoBehaviour
{
    Vector3 bounceUp = new Vector3(0, 0.2f, 0);
    Vector3 bounceDown = new Vector3(0, -0.2f, 0);
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        transform.DOMove(startPos + bounceUp, 1).SetEase(Ease.Linear).SetLoops(100, LoopType.Yoyo);

    }
    

}
