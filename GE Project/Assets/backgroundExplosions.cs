using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundExplosions : MonoBehaviour
{
    public GameObject exp1;
    public GameObject exp2;
    public GameObject exp3;

    bool exploding;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explosion());
        exploding = true;
    }

    IEnumerator Explosion()
    {
        exploding = true;
        yield return new WaitForSeconds(1.0f);
        Instantiate(exp1, new Vector3(Random.Range(-100.0f, 100.0f), Random.Range(-100.0f, 100.0f), Random.Range(-100.0f, 100.0f)), Quaternion.identity);
        Instantiate(exp2, new Vector3(Random.Range(-100.0f, 100.0f), Random.Range(-100.0f, 100.0f), Random.Range(-100.0f, 100.0f)), Quaternion.identity);
        Instantiate(exp3, new Vector3(Random.Range(-100.0f, 100.0f), Random.Range(-100.0f, 100.0f), Random.Range(-100.0f, 100.0f)), Quaternion.identity);
        exploding = false;
    }

    void Update()
    {
        if (exploding == false)
        {
            StartCoroutine(Explosion());
        }
    }
}
