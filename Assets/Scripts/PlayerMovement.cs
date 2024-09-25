using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Animator animator;
    public Transform interaction_center;
    
    Rigidbody2D rb;
    float vertical, horizontal;
    float ineraction_centre_offset;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ineraction_centre_offset = -interaction_center.localPosition.y;
    }

    void Update()
    {
        if (vertical==0) {horizontal = Input.GetAxisRaw("Horizontal");}
        if (horizontal==0) {vertical = Input.GetAxisRaw("Vertical");}

        rb.velocity = Vector3.right * speed * horizontal + Vector3.up * speed * vertical;

        animator.SetFloat("horizontal", rb.velocity.x);
        animator.SetFloat("vertical", rb.velocity.y);

        if (horizontal!=0 | vertical!=0) {
            interaction_center.localPosition = (Vector3.right * horizontal + Vector3.up * vertical) * ineraction_centre_offset;
        }
    }
}
