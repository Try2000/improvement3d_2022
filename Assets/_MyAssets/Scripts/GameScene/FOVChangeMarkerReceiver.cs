using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FOVChangeMarkerReceiver : MonoBehaviour, INotificationReceiver
{
    [SerializeField] FOVChangeManager fOVChangeManager;
    [SerializeField] string markerName;

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        var element = notification as GeneralMarker_string;
        if (element == null || fOVChangeManager == null)
            return;
        if (markerName == element.generalName) fOVChangeManager.StartAnimation();
    }
}