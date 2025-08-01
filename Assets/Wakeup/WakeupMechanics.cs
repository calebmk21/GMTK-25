using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class WakeupMechanics : MonoBehaviour
{
    [SerializeField] private GameObject Z;
    [SerializeField] private int maxtoSpawn = 10;

    void Start()
    {
        StartCoroutine("Testing");
        StartCoroutine("Voices");
    }

    IEnumerator Testing()
    {
        for(int i = 0; i < maxtoSpawn; ++i)
        {
            InstantiateZ();
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(5);

        if(Move.countImp > 0)
        {
            //code for Boss scolding or whateva, replace with event
            Debug.Log("WAKE UP BIH");
        }
        Debug.Log("End");
    }

    void InstantiateZ()
    {
        Instantiate(Z, new Vector3(Random.Range(-7f, 7f), Random.Range(-4f, 4f), 0), Quaternion.Euler(new Vector3(0, 0, Random.Range(-45f, 45f))));
    }
}
