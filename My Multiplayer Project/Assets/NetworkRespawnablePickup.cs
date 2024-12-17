using Mirror;
using Mirror.Examples.Benchmark;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkRespawnablePickup : NetworkBehaviour
{
    [SerializeField] float respawnTime = 5;


    private IEnumerator HideForSeconds(float seconds)
    {
        ShowPickup(false);
        yield return new WaitForSeconds(seconds);
        ShowPickup(true);
    }
    private void ShowPickup(bool shouldShow)
    {
        GetComponent<Collider>().enabled = shouldShow;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(shouldShow);
        }

    }
    public void Pickup()
    {
        Debug.Log("RespawnablePickup Pickup()");
        StartCoroutine(HideForSeconds(respawnTime));
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Pickup();

        }
    }
}
