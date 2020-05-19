using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFighters : MonoBehaviour
{

    public Transform[] cruisers;

    public GameObject fighter;
    BigBoid fighterControl;

    GameObject fighterClone;

    private void Start()
    {
        SpawnFighterWing();
    }

    void SpawnFighterWing()
    {
        for (int i=0; i< cruisers.Length*6; i++)
        {
            Vector3 position = new Vector3(Random.Range(-50.0f, 50.0f), Random.Range(-50,50), Random.Range(-50.0f, 50.0f));
            fighterClone = Instantiate(fighter, position, Quaternion.identity);
            fighterControl = fighterClone.GetComponent<BigBoid>();
            fighterControl.targetTransform = cruisers[Random.Range(0, 3)];
            StartCoroutine(fighterControl.DestroyFighter());
        }
        return;
    }
    
}
