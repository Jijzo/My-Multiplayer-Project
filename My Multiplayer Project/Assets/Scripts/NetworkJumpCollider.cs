using Mirror;
using Mirror.Examples.Benchmark;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NetworkJumpCollider : NetworkBehaviour
{
    Rigidbody playerRigidbody;
    public bool hasBeenJumpedOn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            hasBeenJumpedOn = true;

            if (hasBeenJumpedOn)
            {
                Debug.Log("OnCollisionEnter"); ;
                playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
                Debug.Log(other.gameObject.GetComponent<StarterAssets.PlayerMovement>().trampolineEffectGameObject.name);
                other.gameObject.GetComponent<StarterAssets.PlayerMovement>().JumpHeight = 5;
                StarterAssetsInputs input = other.gameObject.GetComponent<StarterAssetsInputs>();
                input.jump = true;
                //hasBeenJumpedOn = true;
            }
            else
            {
                return;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("OnCollisionExit");
            hasBeenJumpedOn = false;
            other.gameObject.GetComponent<StarterAssets.PlayerMovement>().JumpHeight = 1.2f;          
        }
    }

}
