using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Rockets : MonoBehaviour
{
    [SerializeField] int numProjectiles;
    [SerializeField] GameObject projectile;
    [SerializeField] float fireRate = 2f;

    Vector2 startPoint;
    float radius = 5f;
    float moveSpeed = 5f; 
    float timer; 

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            timer = 0f;
            SpawnProjectiles(numProjectiles);
        }
    }

    void SpawnProjectiles(int numProjectiles)
    {
        float angleStep = 360f / numProjectiles;
        float angle = 0f;

        for (int i = 0; i <= numProjectiles - 1; i++)
        {
            float projectileDirXposition = startPoint.x + Mathf.Sin ((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = startPoint.y + Mathf.Cos ((angle * Mathf.PI) / 180) * radius; 

            Vector2 projectileVector = new Vector2 (projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

            var proj = Instantiate (projectile, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D> ().linearVelocity = 
                new Vector2 (projectileMoveDirection.x, projectileMoveDirection.y);

                angle += angleStep;
        }
    }
}
