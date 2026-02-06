using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider bladeCollider;
    private TrailRenderer bladeTrail;

    private Vector3 startPos;
    private Vector3 lastPos;
    private float startTime;

    public Vector3 SwipeDirection { get; private set; }

    [Header("Swipe Settings")]
    public float minSwipeDistance = 0.3f;
    public float minSliceVelocity = 0.05f;
    public float maxTapTime = 0.25f;

    private bool isSwiping;
    private bool sliceRegistered;

    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopBlade();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartInput();
        else if (Input.GetMouseButton(0))
            UpdateInput();
        else if (Input.GetMouseButtonUp(0))
            EndInput();
    }

    void StartInput()
    {
        startTime = Time.time;
        startPos = GetWorldPosition();
        lastPos = startPos;

        transform.position = startPos;

        isSwiping = true;
        sliceRegistered = false;

        bladeTrail.Clear();
        bladeTrail.enabled = true;

        ComboSystem.Instance.StartSlice();
    }

    void UpdateInput()
    {
        if (!isSwiping) return;

        Vector3 currentPos = GetWorldPosition();
        SwipeDirection = currentPos - lastPos;

        float velocity = SwipeDirection.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = currentPos;
        lastPos = currentPos;
    }

    void EndInput()
    {
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
        isSwiping = false;

        float distance = Vector3.Distance(startPos, lastPos);
        float duration = Time.time - startTime;

        if (distance < minSwipeDistance && duration <= maxTapTime)
            HandleTap();

        ComboSystem.Instance.EndSlice();
    }

    void HandleTap()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            EnemyBase enemy = hit.collider.GetComponent<EnemyBase>();
            if (enemy != null)
                enemy.OnTap();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isSwiping || sliceRegistered) return;

        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy == null) return;

        sliceRegistered = true;
        enemy.OnSlice(SwipeDirection.normalized, transform.position);
    }

    Vector3 GetWorldPosition()
    {
        Vector3 pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        return pos;
    }

    void StopBlade()
    {
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
        isSwiping = false;
    }
}
