using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerStart : MonoBehaviour
{
    [SerializeField] List<Transform> spawnLists;
    [SerializeField] List<GameObject> carsList;


    void Start()
    {

    }

    void Update()
    {

    }

    //chiamato al momento del caricamento della scena
    //consente di cominciare la gara nel tracciato e con nemici/difficoltà scelti
    void Awake()
    {
        //prende vettore dello spawn scelto
        Transform spawnTrans = null;
        switch (LevelSelector.spawnChosen)
        {
            case 1:

                spawnTrans = spawnLists[0];

                break;
            case 2:
                spawnTrans = spawnLists[1];
                break;
            case 3:
                spawnTrans = spawnLists[2];
                break;
            case 4:
                spawnTrans = spawnLists[3];
                break;
        }

        //spawna macchina giocatore
        carsList[0].SetActive(true);
        carsList[0].transform.position = spawnTrans.position;
        carsList[0].transform.forward = spawnTrans.forward;


        int valueCalculatedFor = 1 + (5 * (LevelSelector.difficultyChosen - 1));
        Vector3 tempVec = spawnTrans.position;
        bool pari = true;
        //spawna macchine con difficoltà scelta (1-5 easy, 6-10 hard, 11-16 raptor)
        for (int i = valueCalculatedFor; i < LevelSelector.enemyNumber + valueCalculatedFor; i++)
        {

            carsList[i].SetActive(true);
            if (pari)
            {
                tempVec.x += 5;
                pari = false;
            }
            else
            {
                tempVec.x -= 5;
                tempVec.z -= 4;
                pari = true;
            }
            carsList[i].transform.position = tempVec;
            carsList[i].transform.forward = spawnTrans.forward;


        }



    }

}
