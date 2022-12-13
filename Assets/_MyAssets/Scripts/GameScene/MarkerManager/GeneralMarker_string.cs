using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable, DisplayName("GeneralMarker")]
public class GeneralMarker_string : Marker, INotification
{
    public string generalName;

    public PropertyName id
    {
        get
        {
            return new PropertyName("method");
        }
    }
}