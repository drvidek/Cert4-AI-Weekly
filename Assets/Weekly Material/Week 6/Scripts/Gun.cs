using UnityEngine;

public class Gun : MonoBehaviour
{
    // How long to wait between shooting bullets
    public float cooldownMax = .5f;
    
    // The prefab to spawn a copy of
    public GameObject bulletPrefab;

    // How fast the bullet should move
    public float bulletSpeed;

    //How long before the gun will shoot again
    public float cooldownCurrent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Start with a full cooldown
        cooldownCurrent = cooldownMax;
    }

    // Update is called once per frame
    void Update()
    {
        // Reduce our current cooldown based on how much time has passed 
        cooldownCurrent -= Time.deltaTime;

        // If we run out of our cooldown...
        if (cooldownCurrent <= 0)
        {
            // Reset the cooldown for next time (+= instead of = to avoid rounding errors adding up over time)
            cooldownCurrent += cooldownMax;

            // Instantiate the bullet prefab, at our current position, with no rotation, and reference it in a variable
            GameObject spawnedBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // Find the rigidbody on the bullet spawned and set its velocity based on the direction we're currently looking 
            spawnedBullet.GetComponentInChildren<Rigidbody2D>().linearVelocity = transform.up * bulletSpeed;

            // Destroy the bullet in 8 seconds (for cleanup purposes)
            Destroy(spawnedBullet, 8);
        }
    }
}
