using UnityEngine;
using UnityEngine.Playables;

public class GeneralMoveMarkerReceiver : MonoBehaviour, INotificationReceiver
{
    [SerializeField] GeneralMoveManager generalMoveManager;

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        var element = notification as GeneralMarker_string;
        if (element == null || generalMoveManager == null)
            return;
        generalMoveManager.PlayMove(element.generalName);
    }
}