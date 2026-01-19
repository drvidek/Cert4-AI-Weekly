using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BlobSize : MonoBehaviour
{
    // How big this blob should be
    public float radius = .5f;

    public bool randomRadius;

    // Reference to this blob's collider
    private CircleCollider2D collider;
    private SpriteRenderer sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (randomRadius)
        {
            radius = Random.Range(0.2f, 1.2f);
        }
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
        sprite.size = radius * 2f * Vector2.one;
    }
}
