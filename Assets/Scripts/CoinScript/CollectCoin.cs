using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public int coinValue;
    public float collectSpeed = 15f;
    public Vector3 targetWorldPosition;
    private bool isCollected = false;

    void Update()
    {
        if (!isCollected) return;

        transform.position = Vector3.MoveTowards(transform.position, targetWorldPosition, collectSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWorldPosition) < 0.2f)
        {
            if (CoinUIManager.Instance != null)
            {
                CoinUIManager.Instance.AddCoin(coinValue);
            }
            Destroy(gameObject);
        }
    }

    public void CollectToUI(Transform uiTarget)
    {
        if (uiTarget == null || Camera.main == null) return;

        Vector3 screenPos = uiTarget.position;
        screenPos.z = Mathf.Abs(Camera.main.transform.position.z);

        targetWorldPosition = Camera.main.ScreenToWorldPoint(screenPos);
        targetWorldPosition.z = 0f;

        isCollected = true;
    }
}