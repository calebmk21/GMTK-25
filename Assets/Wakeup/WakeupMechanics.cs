using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class WakeupMechanics : MonoBehaviour
{
    [SerializeField] private GameObject Z;
    [SerializeField] private int maxMinuteTens = 10;
    [SerializeField] private int startingZs = 5;
    public static int count = 0;

    void Start()
    {
        StartCoroutine("Testing");
    }

    void Update()
    {
        if (count == 0)
        {
            SceneManager.LoadScene(0);
        }
    }
    IEnumerator Testing()
    {
        //invoke function to get end stats/result of the zs
        for(int i = 0; i < startingZs; ++i)
        {
            InstantiateZ();
        }
        yield return new WaitForSeconds(5);
        for(int i = 0; i < maxMinuteTens - startingZs; ++i)
        {
            InstantiateZ();
            yield return new WaitForSeconds(2);
        }
        yield return new WaitForSeconds(5);
        Debug.Log(count);
        SceneManager.LoadScene(0);

    }

    void InstantiateZ()
    {
        Instantiate(Z, new Vector3(Random.Range(-7f, 7f), Random.Range(-4f, 4f), 0), Quaternion.Euler(new Vector3(0, 0, Random.Range(-45f, 45f))));
        ++count;
    }
}
