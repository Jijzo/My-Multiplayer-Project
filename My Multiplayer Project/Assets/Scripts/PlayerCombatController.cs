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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(transform.position, transform.TransformDirection(ray.direction) * 1000, Color.white);

        if (_starterAssetsInputs.shoot && timeBetweenShotsActual > timeBetweenShotsThreshold)
        {
            CmdShoot(GetRightHandTransform().position);
            timeBetweenShotsActual = 0f;
        }

    }

    [Command] 
    private void CmdShoot(Vector3 spawnPosition)
    {
        Projectile projectile = Instantiate(projectileToSpawn);
        projectile.transform.position = spawnPosition;
        NetworkServer.Spawn(projectile.gameObject, connectionToClient);
    }
    Transform GetRightHandTransform()
    {
        return rightHandTransform;
    }
}
