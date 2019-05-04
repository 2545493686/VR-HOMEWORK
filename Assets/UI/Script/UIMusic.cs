using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIMusic : MonoBehaviour
{
    public GameObject btnObj;
    public CanvasGroup Musicimage;
    private bool IsChlik=true;
    // Start is called before the first frame update
    void Start()
    {
        btnObj = GameObject.Find("MusicButton");
        Button btn = (Button)btnObj.GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void PointClicked()
    {
        if (IsChlik)
        {
            Musicimage.alpha = 1;
            IsChlik = false;
        }
        else
        {
            Musicimage.alpha = 0;
            IsChlik = true;
        }
    }
    void onClick()
        {
            if (IsChlik)
            {
                Musicimage.alpha = 1;
                IsChlik = false;
            }
            else
            {
                Musicimage.alpha = 0;
                IsChlik = true;
            }
        }
}
