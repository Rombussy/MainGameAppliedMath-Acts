using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    [Header("UI Sliders")]
    public Slider healthSlider;
    public Slider easeSlider;

    [Header("Health Settings")]
    public float maxHealth = 20f;
    public float health;
    public float ghostBarDelayDuration = 0.04f; //ghost health bar stays after taking dmg
    public float ghostBarEaseDuration = 0.05f; //after that ghost bar dissapears/ease slowly

    private float timer = 0f;
    private float startingGhostValue; //to avoid visual glitches
    private bool isEasing = false;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;

        healthSlider.maxValue = maxHealth;
        easeSlider.maxValue = maxHealth;

        healthSlider.value = health;
        easeSlider.value = health;

    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        //Ease Animation
        if (isEasing)
        {
            timer += Time.deltaTime;
            if (timer < ghostBarDelayDuration) return;
            float progress = (timer - ghostBarDelayDuration) / ghostBarEaseDuration;
            float easedT = Ease.EaseOutQuad(progress);

            //Mmm lerp lerp lerp lerp lerp ghost bar to current health
            easeSlider.value = Mathf.Lerp(startingGhostValue, health, easedT);

            if (progress >= 1)
            {
                easeSlider.value = health;
                isEasing = false;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);

        startingGhostValue = easeSlider.value;
        timer = 0f;
        isEasing = true;
    }
}
