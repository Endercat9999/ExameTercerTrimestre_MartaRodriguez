using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public bool isGrounded;

    public Animator anim;
    
    PlayerController playerScript;

    private Rigidbody2D rBody;


    GameManager gameManager;
    

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponentInParent<Animator>();
        playerScript = GetComponentInParent<PlayerController>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 3)
        {
            isGrounded = true;
            anim.SetBool("ItsJumping", false);    
        }
        else if(other.gameObject.layer == 6)
        {
            Debug.Log("Goomba muerto");
            
            playerScript.rBody.AddForce(Vector2.up * playerScript.jumpForce, ForceMode2D.Impulse);
            anim.SetBool ("ItsJumping", true); 

            Enemy goomba = other.gameObject.GetComponent<Enemy>();
            goomba.Die();

        }


        if(other.gameObject.tag == "DeadZone")
        {
            Debug.Log("Estoy muerto");

            gameManager.GameOver();
        }  
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.layer == 3)
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == 3)
        {
            isGrounded = false;
        }
    }
}
