using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Grabber : MonoBehaviour
{
    public SteamVR_Action_Boolean triggerAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");

    bool hadGrabed = false;
    RaycastHit grabTarget;
    Transform grabOldParent;
    Hand holdHand;

    // Update is called once per frame
    void Update()
    {

        if (hadGrabed) //如果手上有东西
        {
            if (triggerAction.GetStateUp(holdHand.handType)) //是否松开扳机
            {
                grabTarget.transform.parent = grabOldParent; //复位
                grabTarget.collider.enabled = true;
                grabTarget.rigidbody.useGravity = true;
                hadGrabed = false;
            }

            return;
        }

        foreach (Hand hand in Player.instance.hands)
        {
            if (triggerAction.GetStateDown(hand.handType)) //如果该手按下了扳机
            {
                RaycastHit rayInfo = GameManager.Instance.pointers.CreateRaycast(30); //发送射线

                if (rayInfo.collider.tag == "Throwable") //如果射线目标是"Throwable"标签的物体
                {
                    if (!hadGrabed) //手上没有东西
                    {
                        Grab(rayInfo); //抓起东西
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
        ragInfo.transform.parent = GameManager.Instance.GetHand().transform; //设置目标物体的父坐标为手
        ragInfo.collider.enabled = false;  //接触碰撞体
        ragInfo.transform.GetComponent<Rigidbody>().useGravity = false; //解除重力
        hadGrabed = true;
    }
}
