using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{

    private List<CheckpointSingle> checkpointSingleList;
    private List<int> nextCheckpointSingleIndexList;
    [SerializeField] private List<Transform> carTransformList;
    [SerializeField] private List<GameObject> carTransformNames;

    private List<Transform> checkpointTransformList;

    private KartClassicAgent kartClassicAgent;
    private void Awake()
    {
        Transform checkpointsTransform = transform.Find("AgentCheckpoints");

        checkpointSingleList = new List<CheckpointSingle>();
        foreach (Transform checkpointSingleTransform in checkpointsTransform)
        {
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();

            checkpointSingle.SetTrackCheckpoints(this);
            checkpointSingle.SetVectorTranform(checkpointSingleTransform.transform.forward);
            checkpointSingleList.Add(checkpointSingle);
        }

        nextCheckpointSingleIndexList = new List<int>();
        foreach (Transform carTransform in carTransformList)
        {
            nextCheckpointSingleIndexList.Add(0);
        }
    }

    public void CarThroughCheckpoint(CheckpointSingle checkpointSingle, Transform carTransform)
    {


        int nextCheckpointSingleIndex = nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)];

        if (checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex)
        {
            Debug.Log("Correct");
            nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)] = (nextCheckpointSingleIndex + 1) % checkpointSingleList.Count;


            GameObject go = GameObject.Find(carTransform.name);
            KartClassicAgent other = (KartClassicAgent)go.GetComponentInParent(typeof(KartClassicAgent));
            other.aggiungi(carTransform, 1f);
        }
        else
        {
            Debug.Log("Wrong");
            GameObject go = GameObject.Find(carTransform.name);
            KartClassicAgent other = (KartClassicAgent)go.GetComponent(typeof(KartClassicAgent));
            other.aggiungi(carTransform, -1f);
        }
    }

    public void ResetCheckpoint(Transform carTransform)
    {

        Debug.Log(carTransformList.IndexOf(carTransform));
        nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)] = 0;
    }


    public Vector3 GetNextCheckpoint(Transform carTransform)
    {
        int nextCheckpointSingleIndex = nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)];
        return checkpointSingleList[nextCheckpointSingleIndex].GetVectorTransform();
    }
}

