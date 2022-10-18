using UnityEngine;

public class MessageLogDebug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MessageLog.SendMessage(this + ": HULLO! I AM A MILDLY long line of characters. Wonder what is happening here.", MessagePrecedence.IMMEDIATE, false);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            //MessageLogSystem.Instance.SendMessageToLog("new message here");
            MessageLog.SendMessage("This message is ROUTINE. Any sufficiently advanced technology is indistinguishable from magic.");
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            //MessageLogSystem.Instance.SendMessageToLog("new message here");
            MessageLog.SendMessage("This message is PRIORITY. The only way to discover the limits of the possible is to go beyond them into the impossible.", MessagePrecedence.PRIORITY);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            //MessageLogSystem.Instance.SendMessageToLog("new message here");
            MessageLog.SendMessage("This message is IMMEDIATE. Sir Arthur Charles Clarke CBE FRAS was an English science-fiction writer, science writer, futurist, inventor, undersea explorer, and television series host. He co-wrote the screenplay for the 1968 film 2001: A Space Odyssey, one of the most influential films of all time.", MessagePrecedence.IMMEDIATE);
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            //MessageLogSystem.Instance.SendMessageToLog("new message here");
            MessageLog.SendMessage("This message is FLASH. Two possibilities exist: either we are alone in the Universe or we are not. Both are equally terrifying.", MessagePrecedence.FLASH);
        }


    }

}
