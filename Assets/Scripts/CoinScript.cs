using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    [SerializeField]
    Text _text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            Destroy(collision.gameObject);

            _text.text = Convert.ToString(Convert.ToInt32(_text.text) + 1);
        }
    }
}
