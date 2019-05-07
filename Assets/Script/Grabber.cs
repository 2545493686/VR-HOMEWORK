using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Grabber : MonoBehaviour {
    public SteamVR_Action_Boolean triggerAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");

    bool hadGrabed = false;
    RaycastHit grabTarget;
    Transform grabOldParent;
    Hand holdHand;

    // Update is called once per frame
    void Update () {

        if (hadGrabed)
        {
            if (triggerAction.GetStateUp(holdHand.handType))
            {
                grabTarget.transform.parent = grabOldParent;
                grabTarget.collider.enabled = true;
                grabTarget.rigidbody.useGravity = true;
                hadGrabed = false;
            }

            return;
        }

        foreach (Hand hand in Player.instance.hands)
        {
            if (triggerAction.GetStateDown(hand.handType))
            {
                RaycastHit rayInfo = GameManager.Instance.pointers.CreateRaycast(30);

                if (rayInfo.collider.tag == "Throwable")
                {
                    if (!hadGrabed)
                    {
                        Grab(rayInfo);
                        holdHand = hand;
                    }
                }
            }
        }
	}

    private void Grab(RaycastHit ragInfo)
    {
        grabTarget = ragInfo;
        grabOldParent = ragInfo.transform.parent;
        ragInfo.transform.parent = GameManager.Instance.GetHand().transform;
        ragInfo.collider.enabled = false;
        ragInfo.transform.GetComponent<Rigidbody>().useGravity = false;
        hadGrabed = true;
    }
}
