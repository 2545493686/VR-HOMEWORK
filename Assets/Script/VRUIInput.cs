using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Valve.Newtonsoft;
using UnityEngine;

[RequireComponent(typeof(SteamVR_LaserPointer))]
public class VRUIInput : MonoBehaviour
{
    SteamVR_LaserPointer m_Pointer;

    private void Start()
    {
        m_Pointer = GetComponent<SteamVR_LaserPointer>();
        m_Pointer.PointerIn += In;
        m_Pointer.PointerClick += Click;
    }

    private void Click(object sender, PointerEventArgs e)
    {
        if (e.target.tag == "UI")
        {
        }
    }

    private void In(object sender, PointerEventArgs e)
    {
        if (e.target.tag == "UI")
        {
            e.target.GetComponent<Selectable>().Select();
        }
    }
}
