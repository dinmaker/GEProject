using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public GameObject explosion;
    public GameObject bigExplosion;

    public int HP;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, collision.transform.position, Quaternion.identity);
        Destroy(collision.gameObject);
        HP -= 1;
    }

    void Update()
    {
        if (HP <= 0)
        {
            Instantiate(bigExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }
    }
}
