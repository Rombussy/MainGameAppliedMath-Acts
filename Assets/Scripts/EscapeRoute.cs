using UnityEngine;

public class EscapeRoute : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float triggerZone;
    [SerializeField] private GameObject winPanel;

    public static bool hasEscaped = false;

    private void Awake()
    {
        hasEscaped = false;
    }

    private void Update()
    {
        if (hasEscaped || playerTransform == null)
            return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance <= triggerZone)
        {
            hasEscaped = true;
            winPanel.SetActive(true);
        }
    }
}