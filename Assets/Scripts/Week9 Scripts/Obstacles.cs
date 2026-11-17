using JetBrains.Annotations;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private float _spd = 300f;
    public Vector3 obstaclePosition = new Vector3(0f, -2f, 1000f);
    public int laneIndex;
    private Vector3 initialScale;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        initialScale = transform.localScale;
    }
    private void Update()
    {
        obstaclePosition.z -= _spd * Time.deltaTime;
        if (obstaclePosition.z < -100f)
        {
            Destroy(gameObject);
            return;
        }

        Perspective();
            }


        public void Perspective()
    { 
        float perspective = CameraComponent.focalL / (CameraComponent.focalL + obstaclePosition.z);
      
        transform.localScale = Vector3.one * perspective;
        Vector3 flatPos = new Vector3(obstaclePosition.x, obstaclePosition.y, 0f);
        transform.position = CameraComponent.vanishingP + (flatPos - CameraComponent.vanishingP) * perspective;

        if(_spriteRenderer != null)
        {
            _spriteRenderer.sortingOrder = Mathf.RoundToInt(-obstaclePosition.z);
        }
    }
}
