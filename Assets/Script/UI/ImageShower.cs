using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class ImageShower : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Slider slider;
    //public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
    public SteamVR_Action_Vector2 moveAction = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("Move");

    bool m_FisrtEnter = true;
    bool m_IsEnter = false;

    private void Update()
    {
        if (m_IsEnter)
        {
            foreach (var hand in Player.instance.hands)
            {
                slider.value += moveAction.GetAxis(hand.handType).y * 0.1f * -1;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!m_IsEnter)
        {
            m_IsEnter = true;
            if (m_FisrtEnter)
            {
                StartCoroutine(TeleportHintCoroutine());
            }
        }
    }

    IEnumerator Pulse(Hand hand)
    {
        for (int i = 0; i < 5; i++)
        {
            hand.TriggerHapticPulse(800);
            yield return new WaitForSeconds(1);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_IsEnter = false;
        HideTextHint();
    }

    private void HideTextHint()
    {
        ControllerButtonHints.HideTextHint(Player.instance.leftHand, moveAction);
        ControllerButtonHints.HideTextHint(Player.instance.rightHand, moveAction);

        CancelInvoke("ShowTeleportHint");
    }

    private IEnumerator TeleportHintCoroutine()
    {
        float prevBreakTime = Time.time;
        float prevHapticPulseTime = Time.time;

        while (m_IsEnter && m_FisrtEnter)
        {
            Hand hand = GameManager.Instance.GetHand();

            bool pulsed = false;

            //Show the hint on each eligible hand
            if (moveAction.GetAxis(hand.handType) == Vector2.zero)
            {
                bool isShowingHint = !string.IsNullOrEmpty(ControllerButtonHints.GetActiveHintText(hand, moveAction));
                if (!isShowingHint)
                {
                    HideTextHint();
                    ControllerButtonHints.ShowTextHint(hand, moveAction, "滚动滑轮切换图片");
                    prevBreakTime = Time.time;
                    prevHapticPulseTime = Time.time;
                }

                if (Time.time > prevHapticPulseTime + 0.05f)
                {
                    //Haptic pulse for a few seconds
                    pulsed = true;
                    hand.TriggerHapticPulse(500);
                }
            }
            else
            {
                m_FisrtEnter = false;
                HideTextHint();
            }

            if (Time.time > prevBreakTime + 3.0f)
            {
                //Take a break for a few seconds
                yield return new WaitForSeconds(3.0f);

                prevBreakTime = Time.time;
            }

            if (pulsed)
            {
                prevHapticPulseTime = Time.time;
            }

            yield return null;
            
        }
    }
}
