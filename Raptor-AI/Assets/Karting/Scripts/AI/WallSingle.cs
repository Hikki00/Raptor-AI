using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSingle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
           Debug.Log("muro");
        }
    }


}
