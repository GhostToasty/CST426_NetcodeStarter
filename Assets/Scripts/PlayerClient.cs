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

    //only turns everything back on only for the owner that these properties belong to
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsOwner)
        {
            _playerInput.enabled = true;
            _starterAssetsInputs.enabled = true;
            _thirdPersonController.enabled = true;
        }
    }

}
