using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _bloodScatter;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            Destroy(this.gameObject);
            Instantiate(_bloodScatter, this.transform.position, Quaternion.identity);
        }
    }
}
