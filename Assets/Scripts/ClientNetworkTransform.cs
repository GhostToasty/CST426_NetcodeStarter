using UnityEngine;
using Unity.Netcode.Components;

public class ClientNetworkTransform : NetworkTransform
{
    //client is able to override server authority when transforming it's own player
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
