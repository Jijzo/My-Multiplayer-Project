using Mirror;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    [SerializeField] float speed = 0;
    [SerializeField] float maxLifeTime = 3f;
    [SerializeField] float timer = 0f;

    public Rigidbody rb;
    public NetworkIdentity _networkIdentity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
            Debug.Log(_networkIdentity.isOwned);
            Debug.Log(_networkIdentity.connectionToClient + " Server has Authority if this is Null");
            rb.AddForce(transform.forward * speed);

    }













}
