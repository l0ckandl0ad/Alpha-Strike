using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageLogView : MonoBehaviour
{

    // REFACTOR NOTE! This system WILL FAIL if you disable the script and then enable it again.
    // In such a case, the script will not catch/contain messages that were sent from the MessageLog
    // while MessageLogView was disabled
    // So this may turn out to be problematic.

    private int maxMessages = 100; // max messages to be DISPLAYED!


    [SerializeField]
    private List<Message> messageList = new List<Message>();
    [SerializeField]
    private GameObject viewportContent, textObjectPrefab, scrollbarVertical;
    private Scrollbar scrollbarVerticalCache;
    private Transform viewportContentTransformCache;

    private void Awake()
    {
        scrollbarVerticalCache = scrollbarVertical.GetComponent<Scrollbar>(); // cache UI element references
        viewportContentTransformCache = viewportContent.transform;
    }

    private void OnEnable()
    {
        MessageLog.OnSendMessage += AddMessage; // listen for messages from the MessageLog
    }

    private void OnDisable()
    {
        MessageLog.OnSendMessage -= AddMessage;
    }

    private void AddMessage(Message message)
    {
        if (messageList.Count >= maxMessages)
        {
            messageList[0].DestroyTextObject(); // Destroy(unbind) viewable textObject from the message
            messageList.Remove(messageList[0]);
        }

        GameObject newTextObject = Instantiate(textObjectPrefab, viewportContentTransformCache); // instantiate textObj prefab copy

        message.BindTextObject(newTextObject);
        MessageProcessor.ColorByPrecedence(message, newTextObject); // color the message component
        messageList.Add(message);



        if (scrollbarVerticalCache != null)
        {
            scrollbarVerticalCache.value = 0f; // scroll the scrollbar to the bottom to display the new message
        }
    }

}
