using UnityEngine;

public class MiniSplitterPart : MonoBehaviour
{
    private bool isDead = false;
    private SplitterEnemy parent;

    public ParticleSystem sparkEffect;

    public void Init(SplitterEnemy parentEnemy)
    {
        parent = parentEnemy;
    }

    private void OnTriggerEnter(Collider other)
    {
        Blade blade = other.GetComponent<Blade>();
        if (blade == null || isDead) return;

        Die();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.splitterSlice);
    }

    void Die()
    {
        isDead = true;

        if (sparkEffect != null)
            sparkEffect.Play();

        parent.NotifyPartDestroyed();

        Destroy(gameObject, 0.05f);
    }
}
