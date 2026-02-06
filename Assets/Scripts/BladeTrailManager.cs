using UnityEngine;

public class BladeTrailManager : MonoBehaviour
{
    public TrailRenderer trail;
    public Gradient[] trailGradients;

    private const string TRAIL_KEY = "SelectedBladeTrail";

    void Start()
    {
        LoadTrail();
    }

    public void LoadTrail()
    {
        int index = PlayerPrefs.GetInt(TRAIL_KEY, 0);

        if (index < 0 || index >= trailGradients.Length)
            index = 0;

        trail.colorGradient = trailGradients[index];
    }
}
