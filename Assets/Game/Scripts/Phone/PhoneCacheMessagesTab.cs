using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Phone;
using UnityEngine;

public class PhoneCacheMessagesTab : MonoBehaviour
{
    public void OpenCacheMessages(PhoneContact contact)
    {
        var messageLogs = GetComponentInChildren<PhoneCacheMessagesLogs>();
        
        if (messageLogs == null) return;
        
        messageLogs.AddMessages(contact.MessageLogs.ToArray());
    }

    public void CloseCacheMessages()
    {
        var messageLogs = GetComponentInChildren<PhoneCacheMessagesLogs>();
        
        if (messageLogs == null) return;
        
        messageLogs.RemoveMessages();
    }
}
