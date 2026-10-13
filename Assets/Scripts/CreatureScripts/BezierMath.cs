using UnityEngine;

public static class BezierMath
{
  public static Vector3 Quadratic(Vector3 p0,Vector3 p1, Vector3 p2, float t)
    {
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);

        return Vector3.Lerp(a, b, t);
    }

    public static Vector3 Cubic(Vector3 p0, Vector3 p1, Vector3 p2,Vector3 p3, float t)
    {
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(d, e, t);


    }
}
