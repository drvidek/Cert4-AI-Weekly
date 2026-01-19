using UnityEngine;

public class BlobCollision : MonoBehaviour
{

    public BlobSize mySize;

    void Start()
    {
         mySize = GetComponent<BlobSize>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<BlobSize>(out BlobSize otherSize))
        {
            if (otherSize.radius < mySize.radius)
            {
                mySize.radius += otherSize.radius;
                mySize.Resize();
                Destroy(otherSize.transform.root.gameObject);
            }
        }
    }
}
