using UnityEngine;
using Unity.Netcode;



public class VRNetworkPlayer : NetworkBehaviour
{

    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            HideLocalNetworkHands();
        }
    }

    void Update()
    {
        if (IsOwner)
        {
            //move the local player head, left and right hand
            head.transform.position = VRRigReferences.Instance.head.position;
            head.transform.rotation = VRRigReferences.Instance.head.rotation;
            leftHand.transform.position = VRRigReferences.Instance.leftHand.position;
            leftHand.transform.rotation = VRRigReferences.Instance.leftHand.rotation;
            rightHand.transform.position = VRRigReferences.Instance.rightHand.position;
            rightHand.transform.rotation = VRRigReferences.Instance.rightHand.rotation;
        }
        
    }

    private void HideLocalNetworkHands()
    {
        HideRenderers(leftHand);
        HideRenderers(rightHand);
    }

    private static void HideRenderers(Transform hand)
    {
        if (hand == null)
        {
            return;
        }

        foreach (Renderer renderer in hand.GetComponentsInChildren<Renderer>(true))
        {
            renderer.enabled = false;
        }
    }
}
