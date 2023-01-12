using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerStart : MonoBehaviour
{
    [SerializeField] List<Transform> spawnLists;
    [SerializeField] List<GameObject> carsList;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        //prendi punto di spawn nel tracciato scelto
        Transform spawnTrans = null;
        switch (LevelSelector.spawnChosen)
        {
            case 1:

                spawnTrans = spawnLists[0];
                break;
            case 2:
                spawnTrans = spawnLists[1];
                break;
        }

        //spawna macchina
        carsList[0].SetActive(true);
        carsList[0].transform.position = spawnTrans.position;
        carsList[0].transform.forward = spawnTrans.forward;


        int valueCalculatedFor = 1 + (5 * (LevelSelector.difficultyChosen - 1));
        //spawna macchine con difficoltà scelta (1-5 easy, 6-10 hard, 11-16 raptor)
        for (int i = valueCalculatedFor; i < LevelSelector.enemyNumber + valueCalculatedFor; i++)
        {
            carsList[i].SetActive(true);
            carsList[i].transform.position = spawnTrans.position;
            carsList[i].transform.forward = spawnTrans.forward;


        }

    }
}
