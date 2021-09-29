using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlarPuloJogadorScript : MonoBehaviour
{
    Rigidbody2D rig;

    [SerializeField]
    float force = 300f;

    bool IsGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsGrounded && Input.GetButton("Jump"))
        {
            rig.AddForce(Vector2.up * force);
            IsGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsGrounded = false;

        if (collision.gameObject.tag == "Ground")
            IsGrounded = true;
    }
}
