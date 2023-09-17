using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : NetworkBehaviour
{
    [SyncVar(OnChange = nameof(OnAttachedItemChange))]
    public Child networkChildItem;

    public void AttachItemToItemJig(Child itemToAttach)
    {
        if (IsServer) return;

        ServerAttachItemToThis(itemToAttach, false);
    }

    [ServerRpc(RequireOwnership = false)]
    void ServerAttachItemToThis(Child itemToAttach, bool commitItemToDespawn)
    {
        if (itemToAttach != null)
        {
            networkChildItem = itemToAttach;
        }
        else
        {
            networkChildItem = null;
        }
    }


    public void OnAttachedItemChange(Child oldItem, Child newItem, bool asServer)
    {

        if (newItem != null)
        {

            newItem.InsertToParent(this);

        }
        else 
        {
            //
        }


    }
}
