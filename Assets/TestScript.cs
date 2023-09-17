using FishNet;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : NetworkBehaviour
{
    [SerializeField] Button moveButton;
    [SerializeField] Button parentButton;

    Parent parent;
    Child child;

    NetworkConnection localConnection;

    private void Awake()
    {
        moveButton.onClick.AddListener(OnMoveButtonPress);
        parentButton.onClick.AddListener(OnParentButtonPress);
    }

    public override void OnStartClient()
    {
        parent = FindObjectOfType<Parent>();
        child = FindObjectOfType<Child>();

        localConnection = InstanceFinder.ClientManager.Connection;
    }


    private void OnParentButtonPress()
    {
        NetworkObject networkObject = GetComponent<NetworkObject>();
        child.NetworkChangeOwner(localConnection, networkObject);
        child.transform.position = parent.transform.position + Vector3.up;

        parent.AttachItemToItemJig(child);
    }

    private void OnMoveButtonPress()
    {
        NetworkObject networkObject = GetComponent<NetworkObject>();
        child.NetworkChangeOwner(localConnection, networkObject);

        child.transform.position = child.transform.position + Vector3.right;
    }






}
