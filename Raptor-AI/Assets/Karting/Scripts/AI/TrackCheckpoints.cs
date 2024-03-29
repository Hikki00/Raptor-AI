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

        nextCheckpointSingleIndexList = new List<int>();
        foreach (Transform carTransform in carTransformList)
        {
            nextCheckpointSingleIndexList.Add(0);
        }
    }

    // controlla che il checkpoint attraversato dalla macchina sia quello giusto (devono essere attraversati in sequenza da 0 a n-1)
    // questo metodo viene utilizzato in particolare nel training, in modo da poter assegnare reward adeguati all'agente
    // checkpoint corretto = +3
    // checkpoint non corretto = -9
    public void CarThroughCheckpoint(CheckpointSingle checkpointSingle, Transform carTransform)
    {

        int nextCheckpointSingleIndex = nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)];

        if (checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex)
        {
            if (nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)] == checkpointSingleList.Count)
                ResetCheckpoint(carTransform);

            nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)] = (nextCheckpointSingleIndex + 1) % checkpointSingleList.Count;




            GameObject go = GameObject.Find(carTransform.name);
            KartClassicAgent other = (KartClassicAgent)go.GetComponentInParent(typeof(KartClassicAgent));
            if (other != null)
            {
                other.AddRewardOnCar(carTransform, 3f);
            }
        }
        else
        {

            GameObject go = GameObject.Find(carTransform.name);
            KartClassicAgent other = (KartClassicAgent)go.GetComponentInParent(typeof(KartClassicAgent));
            if (other != null)
            {
                other.AddRewardOnCar(carTransform, -9f);
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

}

