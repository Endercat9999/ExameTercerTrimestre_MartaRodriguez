using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variable para la velocidad de movimiento
    public float playerSpeed = 5.5f;
    //Variable para la fuerza del salto
    public float jumpForce = 3f;

    //Variable para acceder al SpriteRenderer
    private SpriteRenderer spriteRenderer;
    //Variable para acceder al RigidBody2D
    public Rigidbody2D rBody;
    //Variable para acceder al GroundSensor
    private GroundSensor sensor;

    public Animator anim;

    public bool canShoot = true;

    public Transform bulledPrefab;
    
    public float reatOffire =1;

    public float timer;

    public Transform bulledSpawn;


    //Variable para almacenar el input de movimiento
    float horizontal;

    GameManager gameManager;

    void Awake()
    {
        //Asignamos la variables del SpriteRender con el componente que tiene este objeto
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Asignamos la variable del Rigidbody2D con el componente que tiene este objeto
        rBody = GetComponent<Rigidbody2D>();
        //Buscamos un Objeto por su nombre, cojemos el Componente GroundSensor de este objeto y lo asignamos a la variable
        sensor = GameObject.Find("GroundSensor").GetComponent<GroundSensor>();
        anim = GetComponent <Animator>();
        //Buscamos el objeto del GameManager y SFXManager lo asignamos a las variables
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameOver == false)
        {
            horizontal = Input.GetAxis("Horizontal");

            if(horizontal < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                anim.SetBool("ItsRunnig", true);
            }
            else if(horizontal > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                anim.SetBool("ItsRunnig", true);
            }
            else
            {
            anim.SetBool("ItsRunnig", false);   
            }


            if(Input.GetButtonDown("Jump") && sensor.isGrounded)
            {
                rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                anim.SetBool("ItsJumping", true); 
            }



            Shoot();
        }    


        
    }

    void FixedUpdate()
    {
        rBody.velocity = new Vector2(horizontal * playerSpeed, rBody.velocity.y);
    }

    public void Die()
    {
        gameManager.GameOver();
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Coin")
        {
            gameManager.AddCoin();
            Destroy(collider.gameObject);
        }
    }

     void Shoot()
    {
        if (!canShoot)
        {
            timer += Time.deltaTime;
            if(timer  >= reatOffire)
            {
                canShoot = true;

                timer = 0;
            }
        }
        if(Input.GetKeyDown(KeyCode.F) && canShoot)
        {
            Instantiate(bulledPrefab, bulledSpawn.position, bulledSpawn.rotation);

            canShoot =false;
        }
    }
}
