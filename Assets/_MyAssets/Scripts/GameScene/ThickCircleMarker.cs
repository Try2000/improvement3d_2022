using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


[System.Serializable, DisplayName("ThickCircleMarker")]
public class ThickCircleMarker : Marker, INotification
{
    public string circlename;

    public PropertyName id
    {
        get
        {
            return new PropertyName("method");
        }
    }
}