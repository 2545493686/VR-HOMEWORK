using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Teleporter : MonoBehaviour
{
    public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");

    void Update()
    {
        Hand hand = GameManager.Instance.GetHand();

        if (!hand)
        {
            return;
        }

        if (teleportAction.GetStateDown(hand.handType))
        {
            GameManager.Instance.pointers.SetColor(Color.red);
        }
        if (teleportAction.GetStateUp(hand.handType))
        {
            RaycastHit rayInfo = GameManager.Instance.pointers.CreateRaycast(30);

            if (rayInfo.collider.tag == "Floor")
            {
                Player.instance.transform.position = rayInfo.point;
            }

            GameManager.Instance.pointers.SetColor(Color.black);
        }
    }
}
