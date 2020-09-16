using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform aimTransform;
    public Transform muzzle;
    Vector3 mousePosition;
    public GameObject bulletprefab;
    [SerializeField]
    float bulletSpeed = 20.0f;
    [SerializeField]
    Transform aimRotation;
    float fireRate = 0.5f;
    float nextTimeToFire = 0.0f;

    void Update()
    {
        Aim();

        if(nextTimeToFire < Time.time && Input.GetMouseButtonDown(0))
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Aim()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDir = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        if (Vector3.Distance(transform.position, mousePosition) < 3)
            mousePosition = new Vector3(transform.position.x + 3, transform.position.y + 3, 0);
    }

    void Shoot()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rayCastDirection = (mousePosition - muzzle.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(muzzle.position, rayCastDirection, Mathf.Infinity);

        if (hit)
        {
            Debug.Log(hit.transform.name);
        }
                
        GameObject bullet = Instantiate(bulletprefab, muzzle.transform.position, Quaternion.Euler(aimRotation.eulerAngles));
        bullet.GetComponent<Rigidbody2D>().AddForce(rayCastDirection * bulletSpeed);
    }
}