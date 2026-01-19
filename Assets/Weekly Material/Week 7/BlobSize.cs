using System.Threading.Tasks;
using UnityEngine;

public class BlobSize : MonoBehaviour
{
    // How big this blob should be
    public float radius = .5f;
    // Reference to this blob's collider
    private CircleCollider2D collider;
    private SpriteRenderer sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Resize();
    }

    void Secure()
    {
        if (!collider)
        {
            collider = GetComponent<CircleCollider2D>();
        }
        if (!sprite)
        {
            sprite = GetComponent<SpriteRenderer>();
        }
    }

    void OnValidate()
    {
        Resize();
    }

    public void Resize()
    {
        Secure();
        collider.radius = radius;
        float size = radius * 2f;
        sprite.size = new Vector2(size, size);
    }
}
