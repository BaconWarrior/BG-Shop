using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float movementSpeed = 3f;
    private Rigidbody2D rb;
    Vector2 movDirection;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Get the input values;
        movDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //Set animator movement values
        anim.SetFloat("Velocity", movDirection.magnitude);
        //Flip model in "X" to face movement direction only if the player is moving in x
        if(movDirection.x != 0)
            this.gameObject.transform.localScale = movDirection.x > 0 ? new Vector3(0.4f, 0.4f, 0.4f): new Vector3(-0.4f, 0.4f, 0.4f);
    }

    private void FixedUpdate()
    {
        //Apply phisics to move de player
        rb.velocity = movDirection * movementSpeed;
    }
}
