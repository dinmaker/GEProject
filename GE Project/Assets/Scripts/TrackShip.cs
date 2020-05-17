using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackShip : MonoBehaviour
{
    public Transform target;

    public float speed;

    void Update()
    {
        Vector3 targetDirection = target.position - transform.position;

        float singleStep = speed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
