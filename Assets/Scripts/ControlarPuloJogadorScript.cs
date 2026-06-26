using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ControlarPuloJogadorScript : MonoBehaviour
{
    private const string GroundTag = "Ground";

    private Rigidbody2D rig;
    private bool _jumpRequested;

    [SerializeField]
    private float force = 300f;

    private bool IsGrounded = false;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        if (!_jumpRequested)
        {
            return;
        }

        _jumpRequested = false;

        if (!IsGrounded)
        {
            return;
        }

        rig.AddForce(Vector2.up * force);
        IsGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        UpdateGroundedState(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        UpdateGroundedState(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GroundTag))
        {
            IsGrounded = false;
        }
    }

    private void UpdateGroundedState(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(GroundTag))
        {
            return;
        }

        IsGrounded = HasGroundContact(collision);
    }

    private bool HasGroundContact(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                return true;
            }
        }

        return false;
    }
}
