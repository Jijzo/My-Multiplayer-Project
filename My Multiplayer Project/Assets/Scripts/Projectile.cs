using Mirror;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    [SerializeField] float speed = 0;
    [SerializeField] float maxLifeTime = 3f;
    [SerializeField] float timer = 0f;

    private Vector3 direction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isOwned)
        {
            if (!isServer)
            {

            }
            else
            {
                direction = Camera.main.transform.forward;
            }
        }


    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > maxLifeTime)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

    }


   


    



}
