using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(SpriteRenderer), typeof(Animator))]
public class player : MonoBehaviour
{
    private Transform groundCheck;
    [SerializeField, Range(1, 20)] private float speed = 5;
    [SerializeField, Range(1, 20)] private  float jumpforce = 10;
    [SerializeField, Range(0.01f, 1)] private float groundCheckRadius = 0.02f;
    [SerializeField] private LayerMask isGroundLayer;
    private bool isGrounded = false;
    
    
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if(speed<=0)
        {
            speed = 5;
            Debug.Log("Speed was set incorrectly");

             }
        if(jumpforce<=0)
        {
            jumpforce = 10;
            Debug.Log("jumpforce was set incorrectly");
        }
        if (!groundCheck)  
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "groundCheck";
            groundCheck = obj.transform;






        }
    }

    // Update is called once per frame
    void Update()
    {
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        float hinput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(hinput * speed, rb.velocity.y);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpforce,
                ForceMode2D.Impulse) ;
        }

        if (hinput != 0) sr.flipX = (hinput < 0); 
  
        anim.SetFloat("hinput",Mathf.Abs( hinput));
        anim.SetBool("isGrounded", isGrounded);

        if (Input.GetButtonDown("Fire1")) 
        {
            anim.SetTrigger("Attack");
        }


        if (isGrounded)
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }











        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetTrigger("Attack2");
            
        }


    }

 



}
