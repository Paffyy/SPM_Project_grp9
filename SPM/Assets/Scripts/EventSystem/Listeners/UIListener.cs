using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIListener : MonoBehaviour
{
    [SerializeField]
    private GameObject pickUpTextPrefab;
    [SerializeField]
    private Canvas UICanvas;
    [SerializeField]
    private GameObject saveTextPrefab;

    void Start()
    {
        Register();
    }

    public void Register()
    {
        EventHandler.Instance.Register(EventHandler.EventType.PickUpEvent, UpdatePickUpText);
        EventHandler.Instance.Register(EventHandler.EventType.SaveEvent, UpdateSaveText);
    }

    private void UpdatePickUpText(BaseEventInfo e)
    {
        var pickUpEventInfo = e as PickupEventInfo;
        if (pickUpEventInfo != null)
        {
            GameObject pickUpText = Instantiate(pickUpTextPrefab, UICanvas.transform);
            pickUpText.GetComponent<Text>().text = pickUpEventInfo.PickUpObject.GetComponent<PickUp>().UIAlertText;
            pickUpText.SetActive(true);
            Destroy(pickUpText, 2f);
        }
    }

    private void UpdateSaveText(BaseEventInfo e)
    {
        SaveEventInfo saveEventInfo = e as SaveEventInfo;
        if(saveEventInfo != null)
        {
            saveTextPrefab.SetActive(true);
            saveTextPrefab.GetComponent<Text>().text = saveEventInfo.SaveGameText;
            saveTextPrefab.GetComponent<Animator>().SetBool("IsSaving", true);
        }
    }
}
