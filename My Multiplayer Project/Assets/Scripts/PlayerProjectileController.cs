using Mirror;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatController : NetworkBehaviour
{

    //PROJECTILE VALUES
    [SerializeField] Transform projectileMount;
    [SerializeField] GameObject projectileToSpawn = null;
    [SerializeField] float timeBetweenShotsThreshold = 2f;
    [SerializeField] float timeBetweenShotsActual = 0f;
    [SerializeField] public KeyCode swingKey = KeyCode.Mouse0;

    private PlayerInput _playerInput;
    private StarterAssetsInputs _starterAssetsInputs;
    private NetworkIdentity _networkIdentity;
    private Animator _animator;

    void Start()
    {
        _networkIdentity = GetComponent<NetworkIdentity>();
        _playerInput = GetComponent<PlayerInput>();
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (!isLocalPlayer) return;        
        if (Input.GetKeyDown(swingKey))
        {
            CmdOnSwing();
        }

    }

    //SWING CODE
    [Command]
    private void CmdOnSwing()
    {
        RpcOnSwing();
    }
    [ClientRpc]
    void RpcOnSwing()
    {
        Debug.Log("RpcOnSwing");
        _animator.SetTrigger("SwingAttack");
    }

    //PROJECTILE CODE
    [Command]
    private void CmdSpawnProjectile()
    {
        GameObject projectile = Instantiate(projectileToSpawn, projectileMount.position, projectileMount.rotation);
        NetworkServer.Spawn(projectile);
    }

    ////INPUT CODE
    void DisableInputSystem()
    {
        if (!isLocalPlayer) return;
        _playerInput.DeactivateInput();
    }

    void EnableInputSystem()
    { 
        if (!isLocalPlayer) return;
        _playerInput.ActivateInput();
    }

}
