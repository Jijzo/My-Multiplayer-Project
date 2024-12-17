using Mirror;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatController : NetworkBehaviour
{

    [SerializeField] Transform rightHandTransform = null;
    [SerializeField] GameObject projectileToSpawn = null;
    [SerializeField] float timeBetweenShotsThreshold = 2f;
    [SerializeField] float timeBetweenShotsActual = 0f;

    private PlayerInput _playerInput;
    private StarterAssetsInputs _starterAssetsInputs;
    private NetworkIdentity _networkIdentity;
    private Ray _ray;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _networkIdentity = GetComponent<NetworkIdentity>();
        _playerInput = GetComponent<PlayerInput>();
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenShotsActual += Time.deltaTime;
        Shoot();
    }

    private void Shoot()
    {
        if (_starterAssetsInputs.shoot && timeBetweenShotsActual > timeBetweenShotsThreshold)
        {
            CmdShoot();
            timeBetweenShotsActual = 0f;
        }
    }

    
    [Command]
    private void CmdShoot()
    {
        TargetGetShootingDirection();
        GameObject projectile = Instantiate(projectileToSpawn, rightHandTransform.position, rightHandTransform.rotation);
        NetworkServer.Spawn(projectile);
    }


    //TargetRPC Attribute runs a function on a specific client. In this case, we want the specific client's
    //camera direction for the projectile. 
    [TargetRpc]
    private void TargetGetShootingDirection()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log("[TargetRpc] method on client: " + _networkIdentity.netId + _ray.direction);
    }
}
