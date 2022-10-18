using UnityEngine;
using UnityEngine.UI;


// used to handle Messages and their corresponding viewable textObjects without binding one to another
public static class MessageProcessor
{
    // paint the viewable textObject according to message precedence
    public static void ColorByPrecedence(Message message, GameObject textObject)
    {
        MessagePrecedence precedence = message.Precedence;

        if (textObject.TryGetComponent<Text>(out Text textComponent))
        {
            switch (precedence)
            {
                case MessagePrecedence.ROUTINE:
                    textComponent.color = Color.white;
                    break;
                case MessagePrecedence.PRIORITY:
                    //textComponent.fontStyle = FontStyle.Italic;
                    textComponent.color = Color.blue;
                    break;
                case MessagePrecedence.IMMEDIATE:
                    textComponent.color = Color.yellow;
                    break;
                case MessagePrecedence.FLASH:
                    textComponent.text = textComponent.text.ToUpper();
                    textComponent.color = Color.red;
                    break;
                default:
                    textComponent.color = Color.white;
                    break;
            }
        }
    }
}
