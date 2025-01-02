using Mirror;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    [SerializeField] float speed = 0;
    [SerializeField] float maxLifeTime = 3f;
    [SerializeField] float timer = 0f;

    public Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > maxLifeTime)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        rb.AddForce(transform.forward * speed);
    }













}
