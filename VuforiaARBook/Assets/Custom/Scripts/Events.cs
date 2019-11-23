using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events 
{
    // Start is called before the first frame update
    public delegate void Method();
    public static event Method OnTrackingFound;
    public static void TrackingFound()
    {
        OnTrackingFound?.Invoke();
    }
    public static event Method OnTrackingLost;

    public static void TrackingLost()
    {
        OnTrackingLost?.Invoke();
    }
}
