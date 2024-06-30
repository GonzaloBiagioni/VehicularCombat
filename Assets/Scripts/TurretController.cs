using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Transform turretBase;
    public Transform turretBarrel;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float rotationSpeed = 5f;
    public float projectileSpeed = 20f;
    public float fireRate = 1f;

    private float nextFireTime = 0f;

    void Update()
    {
        RotateTurret();

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            FireProjectile();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void RotateTurret()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPoint = hit.point;
            Vector3 directionToTarget = targetPoint - turretBase.position;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            turretBase.rotation = Quaternion.Slerp(turretBase.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * projectileSpeed;
    }
}