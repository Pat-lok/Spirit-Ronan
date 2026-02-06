using UnityEngine;

public class ArmoredEnemy : EnemyBase
{
    public Vector2 weakDirection;
    public float dotThreshold = 0.7f;
    public float sliceForce = 4f;

    [Header("Block Feedback")]
    public ParticleSystem blockParticles;

    public override void OnSlice(Vector3 direction, Vector3 hitPoint)
    {
        Vector2 swipeDir = new Vector2(direction.x, direction.y).normalized;
        float dot = Vector2.Dot(swipeDir, weakDirection);

        if (dot >= dotThreshold)
        {
            KillEnemy(2); // ✅ امتیاز + Combo فقط اینجا

            ActivateSliced(direction.normalized, sliceForce);
            Destroy(gameObject, 3f);
        }
        else
        {
            Block(hitPoint);
        }
        AudioManager.Instance.PlaySFX(AudioManager.Instance.armoredSliceCorrect);
    }

    void Block(Vector3 hitPoint)
    {
        Debug.Log("Armored BLOCK");

        if (blockParticles != null)
        {
            blockParticles.transform.position = hitPoint;
            blockParticles.Play();
        }
        AudioManager.Instance.PlaySFX(AudioManager.Instance.armoredSliceWrong);
    }
}
