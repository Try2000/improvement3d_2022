using UnityEngine;
using UnityEngine.Playables;

public class ThickCircleMarkerReceiver : MonoBehaviour, INotificationReceiver
{
    [SerializeField] ThickCircleAnimateManager thickCircleAnimateManager;

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        var element = notification as ThickCircleMarker;
        if (element == null || thickCircleAnimateManager == null)
            return;
        thickCircleAnimateManager.PlayThickCircle(element.circlename);
    }
}