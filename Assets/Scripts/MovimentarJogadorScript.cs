using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class MovimentarJogadorScript : MonoBehaviour
{
    private Rigidbody2D _rig;
    private SpriteRenderer _spriteRenderer;
    private float _moveHorizontal;

    [SerializeField]
    private float speed = 5f;

    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _moveHorizontal = Input.GetAxisRaw("Horizontal");
        UpdateFacingDirection();
    }

    private void FixedUpdate()
    {
        _rig.velocity = new Vector2(_moveHorizontal * speed, _rig.velocity.y);
    }

    private void UpdateFacingDirection()
    {
        if (_moveHorizontal < 0f)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_moveHorizontal > 0f)
        {
            _spriteRenderer.flipX = false;
        }
    }
}
