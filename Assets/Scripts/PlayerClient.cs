using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using StarterAssets;

public class PlayerClient : NetworkBehaviour
{
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] StarterAssetsInputs _starterAssetsInputs;
    [SerializeField] ThirdPersonController _thirdPersonController;

    
    //turns everything off as default
    void Awake()
    {
        _playerInput.enabled = false;
        _starterAssetsInputs.enabled = false;
        _thirdPersonController.enabled = false;
    }


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        //only turns everything back on only for the owner that these properties belong to
        if (IsOwner)
        {
            _playerInput.enabled = true;
            _starterAssetsInputs.enabled = true;
            
        }

        //third person controller only belongs to the server
        if (IsServer)
        {
            _thirdPersonController.enabled = true;
        }
    }


    //sets up RPC
    //allows client to call function on server, or vice versa
    //gets client input and runs the logic on the server
    [Rpc(target:SendTo.Server)]
    private void UpdateInputServerRpc(UnityEngine.Vector2 move, UnityEngine.Vector2 look, bool jump, bool sprint)
    {
        _starterAssetsInputs.MoveInput(move);
        _starterAssetsInputs.LookInput(look);
        _starterAssetsInputs.JumpInput(jump);
        _starterAssetsInputs.SprintInput(sprint);
    }

    
    //uses late update to make sure that everything syncs up
    //player client will call rpc using these values and only processes values on server 
    private void LateUpdate()
    {
        if (!IsOwner)
            return;
        
        UpdateInputServerRpc(_starterAssetsInputs.move, _starterAssetsInputs.look, _starterAssetsInputs.jump, _starterAssetsInputs.sprint);
    }

}
