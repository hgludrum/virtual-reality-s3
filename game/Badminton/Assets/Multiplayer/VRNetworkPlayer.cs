using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;


public class VRNetworkPlayer : NetworkBehaviour
{

    public Transform head;
    public Transform leftHand;
    public Transform rightHand;


    public List<Renderer> localRendererToHide;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsOwner)
        {
            foreach (var item in localRendererToHide)
            {
                item.enabled = false;
            }
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

}
