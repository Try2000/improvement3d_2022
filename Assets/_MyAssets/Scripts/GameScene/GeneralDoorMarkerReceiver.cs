using UnityEngine;
using UnityEngine.Playables;

public class GeneralDoorMarkerReceiver : MonoBehaviour, INotificationReceiver
{
    [SerializeField] WakeUpDoorManager wakeUpDoorManager;
    [SerializeField] string markerName;

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        var element = notification as GeneralMarker_string;
        if (element == null || wakeUpDoorManager == null)
            return;
        if(markerName == element.generalName) wakeUpDoorManager.PlayWakeUp();
    }
}