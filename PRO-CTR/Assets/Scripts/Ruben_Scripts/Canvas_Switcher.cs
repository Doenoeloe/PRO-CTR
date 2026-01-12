using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Canvas_Switcher : MonoBehaviour
{
    public GameObject canvas01;
    public GameObject canvas02;
    public float fadeDuration = 0.5f;

    private CanvasGroup canvasGroup01;
    private CanvasGroup canvasGroup02;
    private bool isSwitched;
    private bool isTransitioning;

    private void Start()
    {
        isSwitched = false;
        isTransitioning = false;
        canvasGroup01 = canvas01.GetComponent<CanvasGroup>();
        if (canvasGroup01 == null)
            canvasGroup01 = canvas01.AddComponent<CanvasGroup>();

        canvasGroup02 = canvas02.GetComponent<CanvasGroup>();
        if (canvasGroup02 == null)
            canvasGroup02 = canvas02.AddComponent<CanvasGroup>();

        canvasGroup01.alpha = 1f;
        canvas01.SetActive(true);

        canvasGroup02.alpha = 0f;
        canvas02.SetActive(false);
    }

    private void Update()
    {
        if (isTransitioning) return;

        if (!isSwitched && Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            StartCoroutine(FadeCanvases(canvasGroup01, canvasGroup02));
            isSwitched = true;
        }
        else if (isSwitched && Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            StartCoroutine(FadeCanvases(canvasGroup02, canvasGroup01));
            isSwitched = false;
        }
    }

    private IEnumerator FadeCanvases(CanvasGroup fadeOut, CanvasGroup fadeIn)
    {
        isTransitioning = true;
        fadeIn.gameObject.SetActive(true);

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            fadeOut.alpha = 1f - t;
            fadeIn.alpha = t;

            yield return null;
        }

        fadeOut.alpha = 0f;
        fadeIn.alpha = 1f;

        fadeOut.gameObject.SetActive(false);

        isTransitioning = false;
    }
}