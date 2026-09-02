using UnityEngine;
using Unity.Netcode.Components;

public class ClientNetworkAnimator : NetworkAnimator
{
    //client is able to override server authority when animating it's own player
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
