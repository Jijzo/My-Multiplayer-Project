using Mirror;
using UnityEngine;

public class PunchBoxTargeter : NetworkBehaviour
{
    [SerializeField] public BoxCollider boxCollider;
    [SerializeField] public GameObject playerGameObject;
    void Start()
    {
        boxCollider.enabled = false;
    }

    [Command]
    void CmdTakeDamage(GameObject player)
    {
        player.GetComponent<PlayerCombatController>().health -= 5;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerGameObject) return;
        if (other.gameObject.tag == "Player")
        {
            CmdTakeDamage(other.gameObject);
            Debug.Log("OnTriggerEnter");
            boxCollider.enabled = false;
        }

    }
}
