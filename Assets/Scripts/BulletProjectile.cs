using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BulletProjectile : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float lifeTime = 2f;
    [SerializeField] float bulletRadius = 0.5f;
    [SerializeField] private GameObject coinPrefab;

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
        if (creature != null && creature.gameObject.activeInHierarchy)
        {
            float distance = Vector2.Distance(transform.position, creature.position);

            if (distance <= bulletRadius)
            {

                if (coinPrefab != null)
                {
                    GameObject spawnedCoins = Instantiate(coinPrefab, creature.position, Quaternion.identity);
                    CollectCoin coinScript = spawnedCoins.GetComponent<CollectCoin>();
                    if (coinScript != null && CoinUIManager.Instance != null)
                    {
                        coinScript.CollectToUI(CoinUIManager.Instance.coinUITarget);
                    }
                }

                creature.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}


