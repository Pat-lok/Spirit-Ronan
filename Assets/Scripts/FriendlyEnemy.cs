using UnityEngine;

public class FriendlyEnemy : EnemyBase
{
    private bool saved = false;

    public override void OnTap()
{
    if (saved) return;

    saved = true;

    ScoreSystem.Instance.AddScore(4); // ✅ امتیاز Friendly

    PlayDeathEffect();
    ActivateSliced(Vector3.down, 1.5f);
    Destroy(gameObject, 1f);
    AudioManager.Instance.PlaySFX(AudioManager.Instance.friendlyTap);

}


    public override void OnSlice(Vector3 direction, Vector3 hitPoint)
    {
        Debug.Log("Friendly sliced → Lose life");
        LifeSystem.Instance.LoseLife();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.armoredSliceWrong);

        // باقی می‌مونه
    }
}
