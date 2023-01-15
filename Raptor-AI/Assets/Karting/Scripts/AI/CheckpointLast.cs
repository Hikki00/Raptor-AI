using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointLast : MonoBehaviour
{
    public static bool isPass;

    // Start is called before the first frame update
    void Awake()
    {
        CheckpointLast.isPass = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider){
        if(collider.tag == "Player" || collider.tag == "Agent"){
            CheckpointLast.isPass = true;
        }
    }
}
