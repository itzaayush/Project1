using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _bloodScatter;
    TrailRenderer trailRenderer;

    private void OnEnable()
    {
        Destroy(this.gameObject, 5.0f);
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
            Destroy(this.gameObject);
    }
}
