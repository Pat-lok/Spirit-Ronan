using UnityEngine;

public class SplitterEnemy : EnemyBase
{
    private bool isSplit = false;
    private int aliveParts;

    [Header("Mini Movement")]
    public float miniBaseSpeed = 1.3f;
    public float separationForce = 3.5f;
    public float sliceForce = 2.5f;

    protected override void Awake()
    {
        base.Awake();

        MiniSplitterPart[] parts = sliced.GetComponentsInChildren<MiniSplitterPart>();
        aliveParts = parts.Length;

        foreach (MiniSplitterPart part in parts)
        {
            part.Init(this);

            Rigidbody rbPart = part.GetComponent<Rigidbody>();
            rbPart.useGravity = false;
            rbPart.drag = 0f;
        }
    }

    public override void OnSlice(Vector3 direction, Vector3 hitPoint)
    {
        if (isSplit) return;
        isSplit = true;

        whole.SetActive(false);
        sliced.SetActive(true);
        col.enabled = false;

        Rigidbody[] parts = sliced.GetComponentsInChildren<Rigidbody>();
        Vector3 center = transform.position;

        foreach (Rigidbody part in parts)
        {
            // سرعت بیشتر از دشمن اصلی
            part.velocity = rb.velocity * miniBaseSpeed;

            // جدا شدن از مرکز
            Vector3 sepDir = (part.position - center).normalized;
            if (sepDir == Vector3.zero)
                sepDir = Random.insideUnitCircle.normalized;

            part.AddForce(sepDir * separationForce, ForceMode.Impulse);
            part.AddForce(direction.normalized * sliceForce, ForceMode.Impulse);
        }
        AudioManager.Instance.PlaySFX(AudioManager.Instance.splitterSlice);
    }

    public void NotifyPartDestroyed()
    {
        aliveParts--;

        if (aliveParts <= 0)
        {
            ScoreSystem.Instance.AddScore(3);
            Destroy(gameObject);
        }
        ComboSystem.Instance.RegisterKill();

    }
    
}
