using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FVAssist : MonoBehaviour
{
   private Camera camOverlay;

   private void OnEnable()
   {
       Events.OnTrackingFound += OnTrackingFound;
   }

   private void OnDisable()
   {
       Events.OnTrackingLost -= OnTrackingFound;
   }

   private void Awake()
   {
       camOverlay = GetComponent<Camera>();
   }

   private void OnTrackingFound() {
       camOverlay.fieldOfView = Camera.main.fieldOfView;
   }
}
