using UnityEngine;
using TMPro;

public class FloatingDamageText : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public float floatSpeed = 1f;
    public float lifetime = 20f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
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
}
