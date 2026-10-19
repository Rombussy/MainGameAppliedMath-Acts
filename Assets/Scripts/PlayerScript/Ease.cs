using UnityEngine;

public static class Ease 
{
    public static float Linear(float t) => t;
    public static float EaseOutQuad(float t) => 1f - (1f - t) * (1f - t);
    public static float EaseOutCubic(float t) => 1f - (1f - t) * (1f - t) * (1f - t);
}
