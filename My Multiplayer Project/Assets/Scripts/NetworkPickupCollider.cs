using Mirror;
using Mirror.Examples.Benchmark;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NetworkPickupCollider : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            NetworkRespawnablePickup pickup = GetComponentInParent<NetworkRespawnablePickup>();
            pickup.Pickup();

        }
    }
}
