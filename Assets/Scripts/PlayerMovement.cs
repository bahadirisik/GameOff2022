using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    //private float moveSpeed = 5f;
    private Vector2 movement;
    [SerializeField] private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement.magnitude >= 1f)
		{
            anim.SetBool("isRunning",true);
		}
		else
		{
            anim.SetBool("isRunning", false);
        }
    }

	private void FixedUpdate()
	{
        rb.MovePosition(rb.position + movement * PlayerStats.playerMoveSpeed * Time.fixedDeltaTime);
	}
}
