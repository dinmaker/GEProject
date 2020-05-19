using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundExplosions : MonoBehaviour
{
    public GameObject exp1;
    public GameObject exp2;
    public GameObject exp3;

    GameObject one;
    GameObject two;
    GameObject three;

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
        yield return new WaitForSeconds(0.5f);
        one = Instantiate(exp1, new Vector3(Random.Range(-500.0f, 500.0f), Random.Range(-500.0f, 500.0f), Random.Range(-500.0f, 500.0f)), Quaternion.identity);
        two = Instantiate(exp2, new Vector3(Random.Range(-500.0f, 500.0f), Random.Range(-500.0f, 500.0f), Random.Range(-500.0f, 500.0f)), Quaternion.identity);
        three = Instantiate(exp3, new Vector3(Random.Range(-500.0f, 500.0f), Random.Range(-500.0f, 500.0f), Random.Range(-500.0f, 500.0f)), Quaternion.identity);
        exploding = false;
        yield return new WaitForSeconds(1.0f);
        Destroy(one);
        Destroy(two);
        Destroy(three);

    }

    void Update()
    {
        if (exploding == false)
        {
            StartCoroutine(Explosion());
        }
    }
}
