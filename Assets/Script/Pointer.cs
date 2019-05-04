using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class Pointer : MonoBehaviour
{
    public float defaultLength = 5.0f;
    public GameObject dot;
    public VRInputModule inputModule;

    LineRenderer m_LineRenderer = null;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        SetWheel(true);
        UpdataLine();
    }

    public void ResetWheel()
    {
        SetWheel(false);
    }

    private void UpdataLine()
    {
        PointerEventData data = inputModule.Data;

        float targetLength = data.pointerCurrentRaycast.distance == 0 ? defaultLength : data.pointerCurrentRaycast.distance;
        RaycastHit hit = CreateRaycast(targetLength);
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        if (hit.collider != null)
        {
            endPosition = hit.point;
        }

        dot.transform.position = endPosition;

        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);
    }

    private void SetWheel(bool value)
    {
        Hand hand = GameManager.Instance.GetHand();

        if (hand)
        {
            SteamVR_RenderModel model = hand.GetComponentInChildren<SteamVR_RenderModel>();

            if (model)
            {
                model.controllerModeState = new RenderModel_ControllerMode_State_t
                {
                    bScrollWheelVisible = value
                };
            }
        }
    }


    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, defaultLength);

        return hit;
    }
}
