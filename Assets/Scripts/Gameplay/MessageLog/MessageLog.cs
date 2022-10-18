using System;
using System.Collections.Generic;

public static class MessageLog // this is the main message log.
{
    public static event Action<Message> OnSendMessage = delegate { };
    public static event Action<Message> OnRemoveMessage = delegate { }; // is this really needed?

    private static int maxMessages = 200; // Max number of messages in the log. Can be higher than displayed by MessageLogView!
    private static List<Message> messageList = new List<Message>();

    public static void SendMessage(Message newMessage) // send message to the log
    {
        NewMessage(newMessage);
    }

    // constructor overload using string - just in case
    public static void SendMessage(string newMessage, MessagePrecedence setPrecedence = MessagePrecedence.ROUTINE,
        bool useTimestamp = true) 
    {
        Message sendMessage = new Message(newMessage, setPrecedence, useTimestamp);
        NewMessage(sendMessage);
    }

    private static void NewMessage(Message newMessage) // actually sending the message to the log
    {
        if (messageList.Count >= maxMessages) // if message limit is reached, remove the oldest
        {
            OnRemoveMessage(messageList[0]); // tell everyone that this message was removed
            messageList.Remove(messageList[0]);
        }
        messageList.Add(newMessage);
        OnSendMessage(newMessage); // tell everyone about the new message
    }
}
