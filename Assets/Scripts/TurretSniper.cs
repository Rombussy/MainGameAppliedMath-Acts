using UnityEngine;

public class TurretSniper : MonoBehaviour
{
    [SerializeField] private Transform creature;

    [Header("Target Lock Detection")]
    public float range = 20f;
    public float alignmentThreshold = 0.98f;

    [Header("Visual Range")]
    public LineRenderer lineRenderer;
    public Color LineConeColor = Color.aliceBlue;

    [Header("Shoot Fire")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 0.6f;
    private float nextFireTime;

    private void Awake()
    {
        DrawLineArea();
    }

    private void Update()
    {
        if (creature == null || !creature.gameObject.activeInHierarchy || Vector2.Distance(transform.position, creature.position) > range)
        {
            GetNextTarget();
        }

        if (creature == null || !creature.gameObject.activeInHierarchy)
            return;

        Vector2 dir = creature.position - transform.position; //getting the direction of the enemy
        float pAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; //Using the radians with Atan2 to get the angle then it convert to degrees 
        transform.eulerAngles = new Vector3(0f, 0f, pAngle);

        float creatureDistance = dir.magnitude;

        Vector2 barrel = transform.right;
        Vector2 toCreature = ((Vector2)creature.position - (Vector2)transform.position).normalized;
        float dot = Vector2.Dot(barrel, toCreature);
        bool inSights = dot >= alignmentThreshold;

        if (creatureDistance <= range && inSights)
        {
            if (Time.time >= nextFireTime)
            {
                SniperShoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void GetNextTarget()
    {
        CreatureMovement[] targets = FindObjectsByType<CreatureMovement>(FindObjectsSortMode.None);
        foreach (var t in targets)
        {
            if (t.gameObject.activeInHierarchy && Vector2.Distance(transform.position, t.transform.position) <= range)
            {
                creature = t.transform;
                return;
            }
        }
    }

    private void DrawLineArea()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = false;

        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        lineRenderer.startColor = LineConeColor;
        lineRenderer.endColor = LineConeColor;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, Vector3.zero);

        lineRenderer.SetPosition(1, new Vector3(range, 0f, 0f));
    }

    public void SetTarget(Transform newTarget)
    {
        creature = newTarget;
    }

    private void SniperShoot()
    {
        if (projectilePrefab == null)
            return;

        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;
        GameObject proObj = Instantiate(projectilePrefab, spawnPosition, transform.rotation);

        BulletProjectile projectile = proObj.GetComponent<BulletProjectile>();
        if (projectile != null)
        {
            projectile.Initialize(creature);
        }
    }
}