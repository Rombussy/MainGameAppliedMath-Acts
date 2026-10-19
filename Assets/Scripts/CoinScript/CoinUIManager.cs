using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CoinUIManager : MonoBehaviour
{
    public static CoinUIManager Instance;
    [Header("Coin Settings")]
    public int currentCoins = 0;
    public float countSpeed = 1f;
    private float displayedCoins;
    public TMP_Text currentCoinsText;
    public Transform coinUITarget;
    [Header("Coin Pulse/Punch Animation (When Collected)")]
    public float pulseScale = 1f;
    public float pulseDuration = 0.2f;
    private Coroutine pulseCoroutine;
    private Vector3 originalScale;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        displayedCoins = currentCoins;

        if (coinUITarget != null)
            originalScale = coinUITarget.localScale;

        if (currentCoinsText != null)
            currentCoinsText.text = ": " + currentCoins;

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(displayedCoins - currentCoins) > 0.01f)
        {
            displayedCoins = Mathf.MoveTowards(displayedCoins, currentCoins, Time.deltaTime * countSpeed);
            if (currentCoinsText != null) currentCoinsText.text = ": " + Mathf.RoundToInt(displayedCoins);
        }
        else
        {
            displayedCoins = currentCoins;
            if (currentCoinsText != null) currentCoinsText.text = ": " + currentCoins;
        }
    }
    public void AddCoin(int amount)
    {
        currentCoins += amount;
            if (pulseCoroutine != null) StopCoroutine(pulseCoroutine);
        pulseCoroutine = StartCoroutine(PulseEffect());
    }

    private IEnumerator PulseEffect()
    {
        if (coinUITarget == null) yield break;

        float elapsed = 0f;
        Vector3 targetScale = originalScale * pulseDuration;

        while (elapsed < pulseDuration)
        {
            elapsed += Time.deltaTime;
            coinUITarget.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / pulseDuration);
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < pulseDuration)
        {
            elapsed += Time.deltaTime;
            coinUITarget.localScale = Vector3.Lerp(targetScale, originalScale, elapsed / pulseDuration);
            yield return null;
        }

        coinUITarget.localScale = originalScale;
    }
}

    
