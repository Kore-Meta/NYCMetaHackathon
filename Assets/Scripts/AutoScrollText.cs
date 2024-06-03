using UnityEngine;
using TMPro;

public class AutoScrollText : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 20.0f;  // Serialized field for scroll speed

    private RectTransform rectTransform;
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUI component is missing from this GameObject.");
            return;
        }

        rectTransform = textMeshPro.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform component is missing from this GameObject.");
            return;
        }
    }

    void Update()
    {
        // Move the text upwards over time
        rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
    }
}
