using UnityEngine;

public class TurretShotgun : MonoBehaviour
{
    [SerializeField] private Transform creature;

    [Header("Target Lock Detection")]
    public float range = 5f;

    [Header("Shotgun Settings")]
    public int pelletCount = 5;
    public float spreadAngle = 30f;

    [Header("Visual Range Ring")]
    public LineRenderer lineRenderer;
    public Color lineRingColor = Color.aliceBlue;
    public int lineSegments = 32;

    [Header("Shoot Fire")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 0.6f;
    private float nextFireTime;

    private void Awake()
    {
        DrawRangeRing();
    }

    private void Update()
    {
        if (creature == null || !creature.gameObject.activeInHierarchy || !EnemyInRange(creature.position - transform.position))
        {
            GetNextTarget();
        }

        if (creature == null || !creature.gameObject.activeInHierarchy)
            return;

        Vector2 dir = creature.position - transform.position;

        if (EnemyInRange(dir))
        {
            float baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.eulerAngles = new Vector3(0f, 0f, baseAngle);

            if (Time.time >= nextFireTime)
            {
                ShotgunShoot(baseAngle);
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void GetNextTarget()
    {
        CreatureMovement[] targets = FindObjectsByType<CreatureMovement>(FindObjectsSortMode.None);
        foreach (var t in targets)
        {
            if (t.gameObject.activeInHierarchy && EnemyInRange(t.transform.position - transform.position))
            {
                creature = t.transform;
                return;
            }
        }
    }

    public bool EnemyInRange(Vector2 dir)
    {
        return dir.magnitude <= range;
    }

    private void DrawRangeRing()
    {
        if (lineRenderer == null)
            return;

        lineRenderer.useWorldSpace = true;
        lineRenderer.loop = true;

        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        lineRenderer.startColor = lineRingColor;
        lineRenderer.endColor = lineRingColor;

        lineRenderer.positionCount = lineSegments;

        Vector3 center = transform.position;

        for (int i = 0; i < lineSegments; i++)
        {
            float currentAngle = ((float)i / lineSegments) * Mathf.PI * 2f;
            float x = center.x + Mathf.Cos(currentAngle) * range;
            float y = center.y + Mathf.Sin(currentAngle) * range;

            lineRenderer.SetPosition(i, new Vector3(x, y, center.z));
        }
    }

    public void SetTarget(Transform newTarget)
    {
        creature = newTarget;
    }

    private void ShotgunShoot(float baseAngle)
    {
        if (projectilePrefab == null)
            return;

        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;

        for (int i = 0; i < pelletCount; i++)
        {
            float offset = pelletCount > 1
                ? Mathf.Lerp(-spreadAngle / 2f, spreadAngle / 2f, (float)i / (pelletCount - 1))
                : 0f;

            float pelletAngle = baseAngle + offset;
            Quaternion rotation = Quaternion.Euler(0f, 0f, pelletAngle);

            GameObject proObj = Instantiate(projectilePrefab, spawnPosition, rotation);

            BulletProjectile projectile = proObj.GetComponent<BulletProjectile>();
            if (projectile != null)
            {
                projectile.Initialize(creature);
            }
        }
    }
}