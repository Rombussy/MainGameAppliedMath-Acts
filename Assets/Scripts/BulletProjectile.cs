using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BulletProjectile : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float lifeTime = 2f;
    [SerializeField] float bulletRadius = 0.5f;

    public Transform creature;


    void Start()
    {
        Destroy(gameObject, lifeTime);
    }


    public void Initialize(Transform creatureTransform)
    {
        creature = creatureTransform;
    }

    void Update()
    {
        
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (creature != null)
        {
            float distance = Vector2.Distance(transform.position, creature.position);
            if (distance <= bulletRadius)
            {
                RestartScene();
            }
        }
        }
    private void RestartScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
    }
}
