using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    
    void Update()
    {
        Vector3 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        transform.position += speed * Time.deltaTime * input;
    }
}
