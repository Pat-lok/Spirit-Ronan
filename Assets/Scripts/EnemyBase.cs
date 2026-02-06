using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Visuals (Optional)")]
    public GameObject whole;
    public GameObject sliced;

    [Header("Effects")]
    public ParticleSystem deathParticles;

    protected Rigidbody rb;
    protected Collider col;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        rb.useGravity = false;
        rb.isKinematic = false;

        // ✅ امن برای همه دشمن‌ها (حتی Final Boss)
        if (whole != null)
            whole.SetActive(true);

        if (sliced != null)
            sliced.SetActive(false);
    }

    public virtual void Init(Vector3 target, float speed)
    {
        Vector3 dir = (target - transform.position).normalized;
        rb.velocity = dir * speed;
    }

    public abstract void OnSlice(Vector3 direction, Vector3 hitPoint);
    public virtual void OnTap() { }

    protected void KillEnemy(int score)
    {
        if (ScoreSystem.Instance != null)
            ScoreSystem.Instance.AddScore(score);

        if (ComboSystem.Instance != null)
            ComboSystem.Instance.RegisterKill();

        PlayDeathEffect();
    }

    protected void PlayDeathEffect()
    {
        if (deathParticles != null)
            deathParticles.Play();
    }

    protected void ActivateSliced(Vector3 forceDir, float force)
    {
        // ⛔ فقط اگر sliced وجود داشت
        if (whole == null || sliced == null)
            return;

        whole.SetActive(false);
        sliced.SetActive(true);

        if (col != null)
            col.enabled = false;

        Rigidbody[] parts = sliced.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody part in parts)
        {
            part.velocity = rb.velocity;
            part.AddForce(forceDir * force, ForceMode.Impulse);
        }
    }
}
