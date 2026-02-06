using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class PanelAnimator : MonoBehaviour
{
    public float animDuration = 0.25f;
    public Vector3 startScale = new Vector3(0.85f, 0.85f, 1f);

    private CanvasGroup canvasGroup;
    private Vector3 originalScale;
    private Coroutine currentRoutine;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        originalScale = transform.localScale;
    }

    public void Show()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(Animate(true));
    }

    public void Hide()
    {
        if (!gameObject.activeSelf) return;

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(Animate(false));
    }

    IEnumerator Animate(bool show)
    {
        float t = 0f;

        if (show)
        {
            canvasGroup.alpha = 0f;
            transform.localScale = startScale;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }

        while (t < animDuration)
        {
            t += Time.unscaledDeltaTime;
            float p = t / animDuration;

            canvasGroup.alpha = show ? p : 1f - p;
            transform.localScale = Vector3.Lerp(
                show ? startScale : originalScale,
                show ? originalScale : startScale,
                p
            );

            yield return null;
        }

        canvasGroup.alpha = show ? 1f : 0f;

        if (!show)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
            gameObject.SetActive(false);
        }
    }
}
