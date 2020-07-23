using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlace : MonoBehaviour
{

    public GameObject Gameobj;
    private GameObject spawnedObj;
    private ARRaycastManager arraymanger;
    private Vector2 touchPose;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        arraymanger = GetComponent<ARRaycastManager>();
    }

   bool TryTouchPosition(out Vector2 touchon)
    {
        if(Input.touchCount > 0)
        {
            touchPose = Input.GetTouch(0).position;
            touchon = default;
            return true;
        }

        touchPose = default;
        touchon = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (TryTouchPosition(out touchPose))
            return;

        if (arraymanger.Raycast(touchPose, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitpose = hits[0].pose;

            if (spawnedObj == null)
            {
                spawnedObj = Instantiate(Gameobj, hitpose.position, hitpose.rotation);
            }
        }
    }

}
