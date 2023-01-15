using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> carTransformLista;
    private List<int> nextLap;
    [SerializeField] public int laps;

    void Start()
    {

        nextLap = new List<int>();
        foreach (GameObject carTransform in carTransformLista)
        {
            nextLap.Add(1);
        }
    }

    //tiene traccia di quanti giri ha fatto ogni macchina, controllando eventuali casi di vittoria/sconfitta
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag != "Wheel" && CheckpointLast.isPass)
        {
            int kartIndex = carTransformLista.IndexOf(other.gameObject);

            nextLap[kartIndex]++;

            if (nextLap[kartIndex] > laps)
            {
                if (other.tag == "Player")
                {
                    SceneManager.LoadScene("winScene");

                }
                else
                    SceneManager.LoadScene("LoseScene");
            }
        }
    }

}