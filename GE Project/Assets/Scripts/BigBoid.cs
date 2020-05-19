using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoid : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 force = Vector3.zero;

    public float mass = 1.0f;

    public float maxSpeed = 5;
    public float maxForce = 10;

    public float speed = 0;

    public bool seekEnabled = false;
    public Vector3 target;
    public Transform targetTransform;

    public bool arriveEnabled = false;
    public float slowingDistance = 10;

    [Range(0.0f, 10.0f)]
    public float banking = 0.1f;
    

    public float damping = 0.1f;

    public bool pathFollowingEnabled = false;
    public float waypointDistance = 3;
    public Path path;

    public BigBoid pursueTarget;
    public bool pursueEnabled;
    public Vector3 pursueTargetPos;
    public bool randomOffset;
    public Vector3 offset;

    public Vector3 Pursue(BigBoid pursueTarget)
    {
        float dist = Vector3.Distance(pursueTarget.transform.position, transform.position);
        float time = dist / maxSpeed;


        pursueTargetPos = pursueTarget.transform.position + pursueTarget.velocity * time;

        return Seek(pursueTargetPos);
    }



    public Vector3 FollowPath()
    {
        Vector3 nextWaypoint = path.NextWaypoint();

        if (path.next == path.waypoints.Count-1)
        {
            return Seek(pursueTargetPos);
        }

        if (!path.looped && path.IsLast())
        {
            return Seek(nextWaypoint);
        }
        else
        {
            if (Vector3.Distance(transform.position, nextWaypoint) < waypointDistance)
            {
                path.AdvanceToNext();
            }
            return Seek(nextWaypoint);
        }
    }


    public void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(target, 0.1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + acceleration);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + velocity);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(targetTransform.position, slowingDistance);

        if (pursueEnabled)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pursueTargetPos, 0.1f);
        }
    }

    Vector3 Arrive(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;
        float dist = toTarget.magnitude;

        float ramped = (dist / slowingDistance) * maxSpeed;
        float clamped = Mathf.Min(ramped, maxSpeed);
        Vector3 desired = (toTarget / dist) * clamped;

        return desired - velocity;
    }

    Vector3 Seek(Vector3 target)
    {
        Vector3 toTarget = target - transform.position + offset;
        Vector3 desired = toTarget.normalized * maxSpeed;

        return desired - velocity;
    }

    public Vector3 CalculateForce()
    {
        Vector3 force = Vector3.zero;
        if (seekEnabled)
        {
            force += Seek(target);
        }
        if (arriveEnabled)
        {
            force += Arrive(target);
        }

        if (pathFollowingEnabled)
        {
            force += FollowPath();
        }

        if (pursueEnabled)
        {
            force += Pursue(pursueTarget);
        }

        return force;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform != null)
        {
            target = targetTransform.position;
        }
        force = CalculateForce();
        acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;
        speed = velocity.magnitude;
        if (speed > 0)
        {
            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * banking), Time.deltaTime * 3.0f);
            transform.LookAt(transform.position + velocity, tempUp);
            //transform.forward = velocity;
            velocity -= (damping * velocity * Time.deltaTime);


        }

        
    }

    IEnumerator RandomizeOffset()
    {
        while (randomOffset == true)
        {
            offset = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            yield return new WaitForSeconds(1);
        }
    }

    public void ActivatePath()
    {
        pathFollowingEnabled = true;
    }

    public IEnumerator DestroyFighter()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(this.gameObject);
    }
}
