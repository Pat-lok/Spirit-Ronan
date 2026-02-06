using UnityEngine;

public class Explosion : MonoBehaviour
{
    public ParticleSystem[] effects;

    private void Awake()
    {
        if (effects == null || effects.Length == 0)
            effects = GetComponentsInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        foreach (var ps in effects)
            ps.Play();

        float maxDuration = 0f;
        foreach (var ps in effects)
        {
            float duration = ps.main.duration + ps.main.startLifetime.constantMax;
            if (duration > maxDuration)
                maxDuration = duration;
        }

        Destroy(gameObject, maxDuration);
    }
}
