using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShakeOnClick : MonoBehaviour
{
    public RectTransform targetImage;   // La imagen del candado
    public float shakeAmount = 10f;     // Intensidad
    public float shakeDuration = 0.2f;  // Duración en segundos

    private Vector2 originalPos;
    private bool isShaking = false;

    void Start()
    {
        if (targetImage != null)
            originalPos = targetImage.anchoredPosition;

        // Agregar automáticamente el listener del botón
        GetComponent<Button>().onClick.AddListener(Shake);
    }

    public void Shake()
    {
        if (!isShaking)
            StartCoroutine(ShakeRoutine());
    }

    IEnumerator ShakeRoutine()
    {
        isShaking = true;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-shakeAmount, shakeAmount);
            float offsetY = Random.Range(-shakeAmount * 0.5f, shakeAmount * 0.5f);

            targetImage.anchoredPosition = originalPos + new Vector2(offsetX, offsetY);

            elapsed += Time.deltaTime;
            yield return null;
        }

        targetImage.anchoredPosition = originalPos;
        isShaking = false;
    }
}