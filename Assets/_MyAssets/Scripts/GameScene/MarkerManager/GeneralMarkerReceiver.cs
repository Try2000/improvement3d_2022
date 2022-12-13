using System;
using UnityEngine;
using UnityEngine.Playables;

public class GeneralMarkerReceiver : MonoBehaviour, INotificationReceiver
{
    public Action<string> onNotify;

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        var element = notification as GeneralMarker_string;
        if (element == null)
            return;
        if (onNotify != null) onNotify(element.generalName);
    }
}