using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    [SerializeField] private int percentageImportant;
    public static int countImp = 0;
    [SerializeField] private int secsTillInteruppt;
    
    void Start()
    {
        //random code for interactable, sets whether it is a special one or not
        int random = Random.Range(0, 100);
        if(random < percentageImportant)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            ++countImp;
        }
        //drifting code, adds force/movement
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(transform.up * 3);
        //stops the movement
        StartCoroutine("StopMovement");
    }

    //interactable portion of code
    void OnMouseDown()
    {
        Destroy(gameObject);
        if(GetComponent<SpriteRenderer>().color == Color.red)
        {
            countImp--;
            Debug.Log("Boss");
        }
        else
        {
            Debug.Log("Normal");
        }
    }

    IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(2);
        rigidbody2D.linearVelocity = Vector2.zero;
        rigidbody2D.angularVelocity= 0;
    }
}
