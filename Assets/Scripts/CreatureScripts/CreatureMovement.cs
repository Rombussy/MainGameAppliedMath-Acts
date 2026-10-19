using System.Collections;
using UnityEngine;

public class CreatureMovement : MonoBehaviour
{
    public enum PathType
    {
        Quadratic,
        Cubic
    }

    [Header("Movement Settings")]
    public PathType pathType = PathType.Quadratic;
    public float durationCreature = 2f;
    public float arcHeight = 3f;
    public float sCurve = 2f;

    public float creatureDamage = 1f;

    private Transform targetLocation; 
    private Coroutine moveCoroutine;

    public void Initialize(Transform target, PathType path)
    {
        targetLocation = target;
        pathType = path;

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = StartCoroutine(MoveCreature());
    }

    private IEnumerator MoveCreature()
    {
        if (targetLocation == null) yield break;

        Vector3 startPoint = transform.position;
        Vector3 endPoint = targetLocation.position;
        float elapsed = 0f;

        //QuadraticMovement 3 ctrl points
        if (pathType == PathType.Quadratic)
        {
            Vector3 middlePoint = (startPoint + endPoint) / 2f;
            Vector3 p1 = middlePoint + Vector3.up * arcHeight;
            Vector3 p2 = endPoint;

            while (elapsed < durationCreature)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / durationCreature;
                transform.position = BezierMath.Quadratic(startPoint, p1, p2, t);

                yield return null;
            }
        }

        //Cubic Movement 4 ctrl Points

        else if (pathType == PathType.Cubic)
        {
            Vector3 p3 = endPoint;

            //Dividing the path into 1/3 2/3 points 
            Vector3 p1Base = Vector3.Lerp(startPoint, p3, 0.33f);
            Vector3 p2Base = Vector3.Lerp(startPoint, p3, 0.66f);

            Vector3 p1 = p1Base + Vector3.up * sCurve;
            Vector3 p2 = p2Base - Vector3.up * sCurve;

            while (elapsed < durationCreature)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / durationCreature;
                transform.position = BezierMath.Cubic(startPoint, p1, p2, p3, t);
                yield return null;
            }
        }

        transform.position = endPoint;

        if (targetLocation != null)
        {
            PlayerHealth playerHealth = targetLocation.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(creatureDamage);
            }
        }
        gameObject.SetActive(false);
    }
}