using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCacheMessagesLogs : MonoBehaviour
{
    public void AddMessages(GameObject[] messages)
    {
        if (messages.Length != 0)
        {
            foreach (var message in messages)
            {
                var messageInstance = Instantiate(message, transform.position, transform.rotation, transform);
                messageInstance.transform.localScale = Vector3.one;
                messageInstance.SetActive(true);
            }
        }
    }

    public void RemoveMessages()
    {
        if (transform.childCount != 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}
