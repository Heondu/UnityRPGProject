using UnityEngine;
using UnityEngine.UI;

public class FloatingDamage : MonoBehaviour
{
    private float moveSpeed = 100;
    private float alphaSpeed = 1f;
    private float destroyTime = 2;
    private Text text;
    private Color alpha = Color.white;
    private Vector3 position;
    private Vector3 offset = Vector3.up;

    private void Awake()
    {
        text = GetComponent<Text>();
        Invoke("DestroyObject", destroyTime);
    }

    public void Init(int damage, Vector3 position)
    {
        this.position = position;
        text.text = damage.ToString();
    }

    private void Update()
    {
        offset += Vector3.up * moveSpeed * Time.deltaTime;
        Vector3 newPos = Camera.main.WorldToScreenPoint(position) + offset;
        newPos.z = 0;
        transform.position = newPos;
        alpha.a = Mathf.Lerp(alpha.a, 0, alphaSpeed * Time.deltaTime);
        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
