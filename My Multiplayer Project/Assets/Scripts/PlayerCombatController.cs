using Mirror;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatController : NetworkBehaviour
{

    [SyncVar] public int health = 100;
    [SerializeField] public GameObject targetPlayer;
    [SerializeField] public KeyCode swingKey = KeyCode.Mouse0;
    [SerializeField] public BoxCollider punchBoxCollider;
    [SerializeField] public Rigidbody punchBoxRigidbody;

    private PlayerInput _playerInput;
    private Animator _animator;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
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
        //player.GetComponent<PlayerCombatController>().health -= 5;
        RpcOnSwing();
    }
    [ClientRpc]
    void RpcOnSwing()
    {
        Debug.Log("RpcOnSwing");
        _animator.SetTrigger("SwingAttack");
    }

    ////INPUT CODE
    void DisableInputSystem()
    {
        if (!isLocalPlayer) return;
        punchBoxCollider.enabled = true;
        _playerInput.DeactivateInput();
    }

    void EnableInputSystem()
    { 
        if (!isLocalPlayer) return;
        punchBoxCollider.enabled = false;
        _playerInput.ActivateInput();
    }

}
