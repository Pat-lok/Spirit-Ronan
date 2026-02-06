using UnityEngine;

public class OutOfBoundsDestroyer : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        // قطعات جدا شده
        if (other.CompareTag("Debris"))
        {
            Destroy(other.gameObject);
            return;
        }

        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            LifeSystem.Instance.EnemyMissed();
            Destroy(other.gameObject);
        }
    }
}
