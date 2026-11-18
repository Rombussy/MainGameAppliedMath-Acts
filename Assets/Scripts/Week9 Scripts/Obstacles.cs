using JetBrains.Annotations;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacles : MonoBehaviour
{
    [SerializeField] float _spd = 300f;
    [SerializeField] float _hitDepth = 25f;
    [SerializeField] private float _damage = 5f;
    public Vector3 obstaclePosition = new Vector3(0f, -2f, 1000f);
    public int laneIndex;
    public PlayerControl player;
    private bool _hasHit = false;
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

        if (player != null && !_hasHit)
        {
            bool inZRange = Mathf.Abs(obstaclePosition.z) <= _hitDepth;
            bool inSameLane = laneIndex == player.playerLane;
            bool onGround = !player._isJumping;

            if (inZRange && inSameLane && onGround)
            {
               _hasHit = true;
                PlayerHealth health = player.GetComponent <PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(_damage);
                }
            }
        }


        Perspective();
            }


        public void Perspective()
    { 
        float perspective = CameraComponent.focalL / (CameraComponent.focalL + obstaclePosition.z);
      
        transform.localScale = initialScale * perspective;
        Vector3 flatPos = new Vector3(obstaclePosition.x, obstaclePosition.y, 0f);
        transform.position = CameraComponent.vanishingP + (flatPos - CameraComponent.vanishingP) * perspective;

        if(_spriteRenderer != null)
        {
            _spriteRenderer.sortingOrder = Mathf.RoundToInt(-obstaclePosition.z);
        }
    }
}
