using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPicture : MonoBehaviour
{
    public Button btnObjP;
    public Button btnObj;
    public CanvasGroup Setup;
    public CanvasGroup Music;
    public CanvasGroup Out;
    public CanvasGroup Picture;

    // Start is called before the first frame update
    void Start()
    {
        btnObjP.onClick.AddListener(OnClick);
        btnObj.onClick.AddListener(OnClickP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    void OnClick()
    {
        Setup.alpha = 0;
        Music.alpha = 0;
        Out.alpha = 0;
        Picture.alpha = 1;
        Setup.interactable = false;
        Music.interactable = false;
        Out.interactable = false;
        Setup.blocksRaycasts = false;
        Music.blocksRaycasts = false;
        Out.blocksRaycasts = false;
    }
    void OnClickP()
    {
        Setup.alpha = 1;
        Music.alpha = 1;
        Out.alpha = 1;
        Setup.interactable = true;
        Music.interactable = true;
        Out.interactable = true;
        Setup.blocksRaycasts = true;
        Music.blocksRaycasts = true;
        Out.blocksRaycasts = true;
        Picture.alpha = 0;
    }
}
