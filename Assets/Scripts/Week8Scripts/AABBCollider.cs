using UnityEngine;
using UnityEngine.PlayerLoop;
public class AABBCollider : MonoBehaviour
{
    [SerializeField] private float _width;
    [SerializeField] private float _length;

    public Vector2 max { get; private set; }
    public Vector2 min { get; private set; }

    private PlayerController _player;

    private void Start()
    {
        if (CollisionManager.Instance != null)
        {
            CollisionManager.Instance.Register(this);
        }
        _player = GetComponent<PlayerController>();
    }

   
    private void OnDestroy()
    {
        if (CollisionManager.Instance != null)
        {
            CollisionManager.Instance.Unregister(this);
        }
    }

    private void Update()
    {
        var posX = this.transform.position.x + _width * .5f;
        var posY = this.transform.position.y + _length * .5f;

        var negX = this.transform.position.x - _width * .5f;
        var negY = this.transform.position.y - _length * .5f;

        max = new Vector2(posX, posY);
        min = new Vector2(negX, negY);

        Debug.DrawLine(new Vector3(min.x, max.y), new Vector3(max.x, max.y), Color.blueViolet);
        Debug.DrawLine(new Vector3(max.x, max.y), new Vector3(max.x, min.y), Color.blueViolet);
        Debug.DrawLine(new Vector3(max.x, min.y), new Vector3(min.x, min.y), Color.blueViolet);
        Debug.DrawLine(new Vector3(min.x, min.y), new Vector3(min.x, max.y), Color.blueViolet);
    }

    public void OnCollision(AABBCollider col)
    {
      if (_player != null)
        {
            _player.OnCol(col);
        }
    }
}