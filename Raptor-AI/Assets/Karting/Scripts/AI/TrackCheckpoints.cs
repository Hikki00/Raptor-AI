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

    //inizializza il conteggio di checkpoints per ogni macchina in gioco
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

    // controlla che il checkpoint attraversato dalla macchina sia quello giusto (devono essere attraversati in sequenza da 0 a n-1)
    // questo metodo viene utilizzato in particolare nel training, in modo da poter assegnare reward adeguati all'agente
    // checkpoint corretto = +1 
    // checkpoint non corretto = -1
    public void CarThroughCheckpoint(CheckpointSingle checkpointSingle, Transform carTransform)
    {

        int nextCheckpointSingleIndex = nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)];

        if (checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex)
        {
            nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)] = (nextCheckpointSingleIndex + 1) % checkpointSingleList.Count;

            GameObject go = GameObject.Find(carTransform.name);
            KartClassicAgent other = (KartClassicAgent)go.GetComponentInParent(typeof(KartClassicAgent));
            if (other != null)
            {
                other.AddRewardOnCar(carTransform, 1f);
            }
        }
        else
        {

            GameObject go = GameObject.Find(carTransform.name);
            KartClassicAgent other = (KartClassicAgent)go.GetComponentInParent(typeof(KartClassicAgent));
            if (other != null)
            {
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

    public int getLastCheckpoint()
    {

        int index = checkpointSingleList.Count - 1;

        return index;
    }

    public bool CarThroughLastCheckpoint(CheckpointSingle checkpointSingle)
    {

        if (checkpointSingleList.IndexOf(checkpointSingle) == getLastCheckpoint())
        {

            lastCheckpoint = true;

        }
        else
        {
            lastCheckpoint = false;
        }
        return lastCheckpoint;
    }

}

