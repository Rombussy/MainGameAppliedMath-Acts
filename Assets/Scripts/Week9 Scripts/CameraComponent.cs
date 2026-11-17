using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    public static float focalL = 300f;
    public static Vector3 vanishingP = Vector3.zero;
    [SerializeField] private Vector2 horizonP = Vector2.zero;

    private void Update()
    {
        vanishingP = new Vector3(horizonP.x, horizonP.y, 0f);
    }
}
