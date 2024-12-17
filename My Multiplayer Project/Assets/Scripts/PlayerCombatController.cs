using Mirror;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatController : NetworkBehaviour
{

    [SerializeField] Transform rightHandTransform = null;
    [SerializeField] Projectile projectileToSpawn = null;
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

        //This line of code is working correctly and I am retrieving the values. 
        //Debug.Log(_starterAssetsInputs.GetLook());

        Shoot();
    }

    private void Shoot()
    {
        if (_starterAssetsInputs.shoot && timeBetweenShotsActual > timeBetweenShotsThreshold)
        {
            //Debug.Log(_starterAssetsInputs.GetLook() + "Shoot");
            CmdShoot();
            timeBetweenShotsActual = 0f;
        }
    }
    //    private void CmdShoot(Vector3 spawnPosition)

    [Command] 
    private void CmdShoot()
    {
        TargetShootProjectile(_networkIdentity.connectionToClient, rightHandTransform.position);
    }

    [TargetRpc]
    private void TargetShootProjectile(NetworkConnectionToClient target, Vector3 spawnPosition)
    {
        //_networkIdentity.AssignClientAuthority(connectionToClient);
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Projectile projectile = Instantiate(projectileToSpawn);
        projectile.transform.position = spawnPosition;
        NetworkServer.Spawn(projectile.gameObject, connectionToClient);
        Debug.Log(_networkIdentity.connectionToClient + " If Null, the server has authority of this object");
        projectile.GetComponent<Rigidbody>().AddForce(_ray.direction, ForceMode.Impulse);
        //Debug.Log(_ray.direction + "This is the Ray.direction of this camera. Hopefully it is different.");
        //Debug.Log("This is TargetShootProjectile() from a specific client");
    }

    [Command]
    void CmdPickupItem(NetworkIdentity item)
    {
        item.AssignClientAuthority(connectionToClient);
    }
}
