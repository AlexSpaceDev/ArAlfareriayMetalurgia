using UnityEngine;

public class FloatUI : MonoBehaviour
{
    public float speed = 1f;
    public float amplitude = 20f;

    RectTransform rect;
    float initialY;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        initialY = rect.anchoredPosition.y;
    }

    void Update()
    {
        float y = initialY + Mathf.Sin(Time.time * speed) * amplitude;
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, y);
    }
}
