using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Laser : MonoBehaviour
{
    private LineRenderer m_Laser;
    private Hand m_Hand;

    // Use this for initialization
    void Start()
    {
        m_Laser = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 start = Player.instance.GetHand(0).transform.position;
        Vector3 dir = Player.instance.GetHand(0).transform.forward * 100;
        m_Laser.SetPosition(0, start);
        m_Laser.SetPosition(1, dir + start);
    }
}
