using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(ScrollRect))]
public class SrollRectUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    ScrollRect m_ScrollRect;
    public SteamVR_Action_Vector2 moveAction = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("Move");

    bool m_IsEnter;

    private void Start()
    {
        m_ScrollRect = GetComponent<ScrollRect>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (m_IsEnter)
        {
            foreach (var hand in Player.instance.hands)
            {
                m_ScrollRect.verticalNormalizedPosition += moveAction.GetAxis(hand.handType).y * 0.05f;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_IsEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_IsEnter = false;
    }

    
}
