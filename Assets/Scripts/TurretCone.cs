using Unity.Android.Gradle;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.Rendering.HableCurve;

public class TurretCone : MonoBehaviour
{
    [SerializeField] Transform creature;

    [Header("Target Lock Detection")]
    public float range = 5f;
    public float coneAngle = 60f;

    [Header("Visual Range")]
    public LineRenderer lineRenderer;
    public Color LineConeColor = Color.aliceBlue;
    int lineSegments = 10;

    [Header("Shoot Fire")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 0.6f;
    private float nextFireTime;

    private void Awake()
    {
        DrawConeArea();
    }

    private void Update()
    {
        if (EscapeRoute.hasEscaped || creature == null)
            return;

        Vector2 dir = creature.position - transform.position; //getting the direction of the enemy
        float pAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; //Using the radians with Atan2 to get the angle then it convert to degrees 

        if (EnemyInRange(dir, pAngle))
        {
          //rotating the turrets
            transform.eulerAngles = new Vector3(0f, 0f, pAngle);

            //fire projectile from ConeShoot
            if (Time.time >= nextFireTime)
            {
                ConeShoot();
                nextFireTime = Time.time * fireRate;
            }
        }
    }

    public bool EnemyInRange(Vector2 dir , float pAngle)
    {
    
        if (dir.magnitude > range)
            return false;
         
        float tAngle = transform.eulerAngles.z;

        float delta = Mathf.Abs(Mathf.DeltaAngle(tAngle, pAngle));

        return delta <= coneAngle / 2f;


    }

    private void DrawConeArea()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = true;

        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        lineRenderer.startColor = LineConeColor;
        lineRenderer.endColor = LineConeColor;

        lineRenderer.positionCount = lineSegments + 1;
        lineRenderer.SetPosition(0, Vector3.zero);

        float startAngle = -coneAngle / 2f;
        float endAngle = coneAngle / 2f;

        for (int i = 0; i < lineSegments; i++)
        {

            float currentAngle = Mathf.Lerp(startAngle, endAngle, (float)i / (lineSegments - 1));
            float rad = currentAngle * Mathf.Deg2Rad;

            float x = Mathf.Cos(rad) * range;
            float y = Mathf.Sin(rad) * range;

            lineRenderer.SetPosition(i + 1, new Vector3(x, y, 0f));
        }

    }

    private void ConeShoot()
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
    
