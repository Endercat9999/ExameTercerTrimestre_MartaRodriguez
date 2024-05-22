using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulled : MonoBehaviour
{
    public Rigidbody2D rigidbody2D; 

    public float bulledSpeed = 6;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        rigidbody2D.AddForce(transform.right * bulledSpeed, ForceMode2D.Impulse);
    }

   void OnTriggerEnter2D(Collider2D collider)
   {
    if(collider.gameObject.tag == "Player")
    {
        return;
    }

    if (collider.gameObject.layer == 6)
    {
        Destroy(collider.gameObject);
    }
    Destroy(gameObject);
   }

   
}




   