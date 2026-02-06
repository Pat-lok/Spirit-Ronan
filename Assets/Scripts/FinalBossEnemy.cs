using UnityEngine;

public class FinalBossEnemy : EnemyBase
{
    [Header("Boss Parts")]
    public Rigidbody rightHorn;
    public Rigidbody leftHorn;
    public Rigidbody head;

    [Header("Effects")]
    public ParticleSystem sliceParticles;

    [Header("Audio")]
    public string sliceSFX = "BossSlice";
    public string finalSliceSFX = "BossFinalSlice";

    [Header("Boss Settings")]
    public float partFallForce = 2f;
    public float torqueForce = 3f;
    public float finalBodyFallForce = 3f;

    private int hitCount = 0;
    private bool isDead = false;

    protected override void Awake()
    {
        base.Awake();

        if (sliced != null)
            sliced.SetActive(true);
    }

    public override void OnSlice(Vector3 direction, Vector3 hitPoint)
    {
        if (isDead) return;

        // 🔥 پارتیکل هر اسلایس
        PlaySliceParticles(hitPoint);

        hitCount++;

        if (hitCount < 3)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.sliceSFX);
        }
        else
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.finalSliceSFX);
        }

        if (hitCount == 1)
            CutPart(rightHorn);
        else if (hitCount == 2)
            CutPart(leftHorn);
        else if (hitCount == 3)
            Die(direction);
    }

    void PlaySliceParticles(Vector3 pos)
    {
        if (sliceParticles == null) return;

        sliceParticles.transform.position = pos;
        sliceParticles.Play();
    }

    void CutPart(Rigidbody part)
    {
        if (part == null) return;

        part.transform.SetParent(null);
        part.isKinematic = false;
        part.useGravity = true;
        part.velocity = Vector3.zero;

        part.gameObject.tag = "Debris";

        part.AddForce(Vector3.down * partFallForce, ForceMode.Impulse);
        part.AddTorque(Random.insideUnitSphere * torqueForce, ForceMode.Impulse);
    }

    void Die(Vector3 direction)
    {
        isDead = true;

        // قطع سر
        CutPart(head);

        // سقوط بدن
        rb.useGravity = true;
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.down * finalBodyFallForce, ForceMode.Impulse);

        KillEnemy(20);
        CoinSystem.Instance.AddCoins(5);

        Destroy(gameObject, 4f);
    }
}
