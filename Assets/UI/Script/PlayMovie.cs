using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMovie : MonoBehaviour
{
    public Slider m_Movie;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_Movie.value = Time.time * 0.25f % 1;
    }
}
