using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public GameObject[] playerRef;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    public bool PlayerVisible;


    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    private void Update()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisableTargets();
        }
    }

    void FindVisableTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (dstToTarget <= viewRadius)
                {
                    if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                    {
                        PlayerVisible = true;
                    }
                    else
                    {
                        PlayerVisible = false;
                    }
                }else
                {
                    PlayerVisible = false;
                }

            }else
            {
                PlayerVisible = false;
            }
        }

        if (targetsInViewRadius.Length == 0)
        {
            PlayerVisible = false;
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
