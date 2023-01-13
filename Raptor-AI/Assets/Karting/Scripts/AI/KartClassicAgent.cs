using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using KartGame.KartSystems;

//script che gestisce il training dell'agente, gestendone le osservazioni ed i reward assegnati
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



    void Start()
    {
        other = (ArcadeKart)carObject.GetComponent(typeof(ArcadeKart));
        spawnCarPos = carObject.transform.position;
        spawnCarFor = carObject.transform.forward;

    }

    void update()
    {

    }

    void Awake()
    {
        arcade = GetComponent<ArcadeKart>();
    }

    //inizializzazione della macchina, effettuata all'inizio di ogni episodio
    //(gli episodi ricominciano quando il numero di step disponibili raggiunge il limite impostato nell'editor di Unity)
    public override void OnEpisodeBegin()
    {
        transform.position = spawnCarPos;
        transform.forward = spawnCarFor;

        trackCheckpoints.ResetCheckpoint(capsule);

        arcade.Rigidbody.velocity = default;

        m_Acceleration = false;
        m_Brake = false;
        m_Steering = 0f;
    }

    //consente l'interpretazione dell'input (di valore discreto) che l'agente vuole dare alla macchina, 
    //passandolo allo script di movimento
    // primo vettore => sinistra/dritto/destra
    // secondo vettore => accelera/frena
    public override void OnActionReceived(ActionBuffers actions)
    {
        base.OnActionReceived(actions);
        InterpretDiscreteActions(actions);

        other.FixedUpdateForML(this.GenerateInput());
    }

    void InterpretDiscreteActions(ActionBuffers actions)
    {
        //controllare con un debug
        m_Steering = actions.DiscreteActions[0] - 1f;
        m_Acceleration = actions.DiscreteActions[1] >= 1.0f;
        m_Brake = actions.DiscreteActions[1] < 1.0f;
    }

    public InputData GenerateInput()
    {
        return new InputData
        {
            Accelerate = m_Acceleration,
            Brake = m_Brake,
            TurnInput = m_Steering
        };
    }


    //assegna il valore adeguato all'azione scelta dall'agente
    public void AddRewardOnCar(Transform carTransform, float quantity)
    {

        if (carTransform == capsule)
        {
            AddReward(quantity);
        }


    }

    //effettua la raccolta delle osservazioni che l'agente ha sull'ambiente
    //in questo caso vediamo quanto correttamente la macchina è rivolta verso il prossimo checkpoint
    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 checkpointForward = trackCheckpoints.GetNextCheckpoint(capsule);
        float directionDot = Vector3.Dot(capsule.forward, checkpointForward);
        sensor.AddObservation(directionDot);
    }


    //controllo delle collisioni su una qualsiasi parte del muro che delinea il tracciato
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {

            AddReward(-0.3f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Wall")
        {
            AddReward(-0.005f);
        }

    }

    //controllo delle collisioni sulla sfera (ostacolo del tracciato 4)
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Sphere")
        {
            AddReward(-0.3f);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.name == "Sphere")
        {
            AddReward(-0.005f);
        }

    }
}