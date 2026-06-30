using UnityEngine;
using TMPro;

public class FloatingDamageText : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public float floatSpeed = 1f;
    public float lifetime = 20f;
    public Vector2 randomTextOffsetRange = new Vector2(30f, 18f);
    public Vector2 randomFontScaleRange = new Vector2(0.9f, 1.15f);

    private RectTransform damageTextRect;
    private Vector2 startingAnchoredPosition;
    private float startingFontSize;

    void Start()
    {
        damageTextRect = damageText.rectTransform;
        startingAnchoredPosition = damageTextRect.anchoredPosition;
        startingFontSize = damageText.fontSize;

        RandomizePopupStyle();
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        if (Camera.main != null)
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }

    public void SetDamage(float damage)
    {
        damageText.text = "-" + damage.ToString("0");
    }

    void RandomizePopupStyle()
    {
        if (damageTextRect == null)
        {
            return;
        }

        float offsetX = Random.Range(-randomTextOffsetRange.x, randomTextOffsetRange.x);
        float offsetY = Random.Range(-randomTextOffsetRange.y, randomTextOffsetRange.y);
        damageTextRect.anchoredPosition = startingAnchoredPosition + new Vector2(offsetX, offsetY);

        float fontScale = Random.Range(randomFontScaleRange.x, randomFontScaleRange.y);
        damageText.fontSize = startingFontSize * fontScale;
    }
}
