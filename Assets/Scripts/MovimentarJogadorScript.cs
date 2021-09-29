using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentarJogadorScript : MonoBehaviour
{
    Rigidbody2D _rig;
    SpriteRenderer _spriteRenderer;
    //Animator _animator;

    [SerializeField]
    float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _rig = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //_animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        _rig.velocity = new Vector2(moveHorizontal * speed, _rig.velocity.y);
        _spriteRenderer.flipX = moveHorizontal < 0;

        //if (moveHorizontal != 0)
        //    _animator.Play("HeroWalk");
        //else
        //    _animator.Play("HeroIdle");
    }
}
