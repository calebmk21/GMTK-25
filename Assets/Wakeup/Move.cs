using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
       // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(transform.up * 3);
        StartCoroutine("StopMovement");
    }

    void OnMouseDown()
    {
        Destroy(gameObject);
        WakeupMechanics.count--;
    }

    IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(2);
        rigidbody2D.linearVelocity = Vector2.zero;
        rigidbody2D.angularVelocity= 0;
    }
}
