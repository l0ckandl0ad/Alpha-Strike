using System;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Message
{
    [SerializeField]
    private string text; // the message itself

    // textComponent is used to bind message to viewable gameObject with <Text> component
    // this would probably have to be changed when I decide to display the message log at several places at once
    // binding multiple gameObject text components would be the easiest route to choose
    // but some other, more flexible solution should be sought
    // getting rid of such binding all together
    // maybe create ViewableMessage class to diffirentiate from the base message in the main log...
    private Text textComponent; 
    

    private MessagePrecedence precedence = MessagePrecedence.ROUTINE;
    private DateTime timestamp;

    public string Text { get => text; }
    public Text TextComponent { get => textComponent; }
    public MessagePrecedence Precedence { get => precedence; }
    public DateTime Timestamp { get => timestamp; }
    public bool UseTimestamp { get; private set; }

    public Message(string messageText, MessagePrecedence setPrecedence = MessagePrecedence.ROUTINE,
        bool useTimestamp = true)
    {
        text = messageText;
        precedence = setPrecedence;
        timestamp = DateTimeModel.CurrentDateTime;
        UseTimestamp = useTimestamp;
    }

    public void SetPrecedence(MessagePrecedence newPrecedence)
    {
        precedence = newPrecedence;
    }


    // maybe this should be refactored later to be done somewhere of Message class so that there was no need
    // to bind anything. Use MessageProcessor instead?
    public void BindTextObject(GameObject bindTextObject)
    {
        // bind the message to viewable textObject on screen
        textComponent = bindTextObject.GetComponent<Text>();
        if (UseTimestamp)
        {
            textComponent.text = timestamp.ToString("G") + ": " + text;
        }
        else
        {
            textComponent.text = text;
        }

    }

    public void DestroyTextObject()
    {
        // destroy viewable textObject when it's no longer needed to be displayed
        UnityEngine.Object.Destroy(textComponent.gameObject);
        textComponent = null;
    }

}
