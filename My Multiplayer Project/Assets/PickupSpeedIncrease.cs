using Mirror;
using StarterAssets;
using UnityEngine;
using UnityEngine.UIElements;

public class PickupSpeedIncrease : NetworkBehaviour
{

    [SerializeField] public float _speed = 1;
    [SerializeField] public float _timeOfBoost = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isServer) return;
        if (other.transform.tag == "Player")
        {
            Debug.Log("OnTriggerEnter");
            other.gameObject.GetComponent<PlayerMovement>().CmdIncreaseSpeed(_speed, _timeOfBoost);
        }
    }
}
