using UnityEngine;
using Unity.MLAgents;
using KartGame.KartSystems;

public class KartClassicAgent : Agent
{
    private ArcadeKart arcade;
    private int checkpointIndex;
    [SerializeField] private TrackCheckpoints trackCheckpoints;
    [SerializeField] private Transform capsule;
    [SerializeField] private GameObject carObject;


    private bool m_Acceleration;
    private bool m_Brake;
    private float m_Steering;

    private ArcadeKart other;
    private Vector3 spawnCarPos;
    private Vector3 spawnCarFor;


    // Start is called before the first frame update
    void Start()
    {
        other = (ArcadeKart)carObject.GetComponent(typeof(ArcadeKart));
        spawnCarPos = carObject.transform.position;
        spawnCarFor = carObject.transform.forward;

    }

    void Awake()
    {
        arcade = GetComponent<ArcadeKart>();
    }

    public override void OnEpisodeBegin()
    {
        //init
        Debug.Log(spawnCarPos);
        transform.position = spawnCarPos;
        transform.forward = spawnCarFor;

        trackCheckpoints.ResetCheckpoint(capsule);

        arcade.Rigidbody.velocity = default;

        m_Acceleration = false;
        m_Brake = false;
        m_Steering = 0f;
    }

    public void AddRewardOnCar(Transform carTransform, float quantity)
    {

        if (carTransform == capsule)
        {
            Debug.Log("entrato");
            AddReward(quantity);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {

            AddReward(-0.5f);
            EndEpisode();
        }
    }



    

}
