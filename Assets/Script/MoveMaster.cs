using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MoveMaster : MonoBehaviour
{
    public SteamVR_Action_Vector2 action = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("Walk");

    // Update is called once per frame
    void Update()
    {
        foreach (var hand in Player.instance.hands)
        {
            var axis = action.GetAxis(hand.handType);
            Player.instance.transform.Translate(new Vector3(axis.x, 0, axis.y) * Time.deltaTime * 1.5f, Space.Self);
        }
    }
}
