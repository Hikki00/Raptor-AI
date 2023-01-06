using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    private TrackCheckpoints trackCheckpoints;
    private Vector3 vectorTransform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trackCheckpoints.CarThroughCheckpoint(this, other.transform);
        }
    }

    public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints)
    {
        this.trackCheckpoints = trackCheckpoints;
    }

    public void SetVectorTranform(Vector3 vectorTransform)
    {
        this.vectorTransform = vectorTransform;
    }

    public Vector3 GetVectorTransform()
    {
        return vectorTransform;
    }
}
