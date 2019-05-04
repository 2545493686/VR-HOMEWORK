using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

public enum HandType
{
    left, right
}

public class GameManager : MonoBehaviour
{
    public SteamVR_Action_Boolean action = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
    public VRInputModule inputModule;
    public Hand leftHand;
    public Hand rightHand;
    public Pointer pointers;

    public static GameManager Instance { get; private set; }
    public HandType HandType { get; private set; }

    bool m_LeftWheelFlag = false, m_IsJoinRoom = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HandType = HandType.left;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (var hand in Player.instance.hands)
        {
            if (action.GetState(hand.handType))
            {
                pointers.ResetWheel();

                inputModule.targetSources = hand.handType;

                if (hand.handType == SteamVR_Input_Sources.LeftHand)
                {
                    HandType = HandType.left;
                    pointers.transform.SetParent(hand.transform);
                }
                else if (hand.handType == SteamVR_Input_Sources.RightHand)
                {
                    HandType = HandType.right;
                    pointers.transform.SetParent(hand.transform);
                }

                pointers.transform.localPosition = Vector3.zero;
                pointers.transform.localRotation = Quaternion.identity;
            }
        }
    }

    public void JoinRoom()
    {
        SceneManager.LoadScene(1);
        m_IsJoinRoom = true;

        pointers.ResetWheel();
        pointers.gameObject.SetActive(false);
    }


    public Hand GetHand()
    {
        Hand hand = null;

        switch (HandType)
        {
            case HandType.left:
                hand = Player.instance.leftHand;
                break;
            case HandType.right:
                hand = Player.instance.rightHand;
                break;
        }

        return hand;
    }
}
