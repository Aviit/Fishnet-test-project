using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class Child : NetworkBehaviour
{
    [SyncVar(OnChange = nameof(OnNetworkParentChange))]
    public Parent networkParentedTo; 

    Rigidbody rb;

    public void InsertToParent(Parent parent)
    {
        if (IsServer) return;
        ServerInsertToParent(this, parent, false);
    }

    [ServerRpc(RequireOwnership = false)]
    void ServerInsertToParent(Child script, Parent parent, bool commitToDespawn)
    {
        script.networkParentedTo = parent;
    }

    public void OnNetworkParentChange(Parent oldParent, Parent newParent, bool asServer)
    {
        if (newParent != null)
        {
            DestroyLocalRigidbody();
            ParentTo(newParent.transform);
        }
        else 
        {
           //
        }
    }


    public void DestroyLocalRigidbody()
    {

        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Destroy(rb);
        }
    }

    public void ParentTo(Transform newParent)
    {
        transform.parent = newParent;
    }

    [ServerRpc(RequireOwnership = false)]
    public void NetworkChangeOwner(NetworkConnection player, NetworkObject obj)
    {
        Debug.Log("transferring ownership of " + gameObject.name + " to" + player.ClientId);
        if (player != null)
        {
            GiveOwnership(player);
        }
        else
        {
            GiveOwnership(null);
        }
    }


}
