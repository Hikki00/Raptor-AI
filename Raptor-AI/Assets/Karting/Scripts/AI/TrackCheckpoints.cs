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

    private bool lastCheckpoint = false;

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

        getLastCheckpoint();

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
            nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)] = (nextCheckpointSingleIndex + 1) % checkpointSingleList.Count;
            
            GameObject go = GameObject.Find(carTransform.name);
            KartClassicAgent other = (KartClassicAgent)go.GetComponentInParent(typeof(KartClassicAgent));
            if(other!=null){
                other.AddRewardOnCar(carTransform, 1f);
            }
        }
        else
        {

            GameObject go = GameObject.Find(carTransform.name);
            KartClassicAgent other = (KartClassicAgent)go.GetComponentInParent(typeof(KartClassicAgent));
            if(other!=null){
                other.AddRewardOnCar(carTransform, -1f);
            }
        }

    }

    public void ResetCheckpoint(Transform carTransform)
    {
        nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)] = 0;
    }

    public Vector3 GetNextCheckpoint(Transform carTransform)
    {
        int nextCheckpointSingleIndex = nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)];
        return checkpointSingleList[nextCheckpointSingleIndex].GetVectorTransform();
    }

    public int getLastCheckpoint(){

        int index = checkpointSingleList.Count - 1;
        Debug.Log("indice dell'ultimo checkpoint" + index);

        return index;
    }

    public bool CarThroughLastCheckpoint(CheckpointSingle checkpointSingle)
    {

        if (checkpointSingleList.IndexOf(checkpointSingle) == getLastCheckpoint())
        {
           Debug.Log("sei arrivato alla fine");
           lastCheckpoint = true;

        }
        else {
            lastCheckpoint = false;
        }
        return lastCheckpoint;
    }

}

