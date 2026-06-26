using UnityEngine;

public class PlayerKillEnemieScript : MonoBehaviour
{
    private const string PlayerAttackTag = "PlayerShootToKill";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(PlayerAttackTag))
        {
            return;
        }

        Destroy(collision.gameObject);
    }
}
