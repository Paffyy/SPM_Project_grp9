﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIListener : MonoBehaviour
{
    public GameObject PickUpText;
    public Canvas UICanvas;
    public Image SwordCoolDown;
    public Image BowCoolDown;

    void Start()
    {
        Register();
    }

    public void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.PickUpEvent, UpdateUIText);
    }

    private void UpdateUIText(BaseEventInfo e)
    {
        var pickUpEventInfo = e as PickupEventInfo;
        if (pickUpEventInfo != null)
        {
            GameObject UIAlertText = Instantiate(PickUpText, UICanvas.transform);
            UIAlertText.GetComponent<Text>().text = pickUpEventInfo.PickUpObject.GetComponent<PickUp>().UIAlertText;
            UIAlertText.SetActive(true);
            Destroy(UIAlertText, 2f);
        }
    }
}