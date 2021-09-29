using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillEnemieScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerShootToKill")
        {
            Destroy(collision.gameObject);
        }
    }
}
