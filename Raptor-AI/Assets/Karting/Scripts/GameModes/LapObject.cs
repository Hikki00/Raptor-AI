using UnityEngine;

/// <summary>
/// This class inherits from TargetObject and represents a LapObject.
/// </summary>
public class LapObject : TargetObject
{
   // private TrackCheckpoints trackCheckpoints;

    [Header("LapObject")]
    [Tooltip("Is this the first/last lap object?")]
    public bool finishLap;

    [HideInInspector]
    public bool lapOverNextPass;

    void Start() {
        Register();
    }
    
    void OnEnable()
    {
        lapOverNextPass = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"){
            Debug.Log("entrato");
            return;
        }
       
        Objective.OnUnregisterPickup?.Invoke(this);
    }
   
}

