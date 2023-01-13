using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelector : MonoBehaviour
{
    // variabili globali
    public static int spawnChosen;
    public static int difficultyChosen;
    public static int enemyNumber;


    [SerializeField] string scene;
    [SerializeField] int level;


    void Start()
    {

    }

    //permette di passare alla scena principale (gara)
    public void OpenScene()
    {
        spawnChosen = level;
        SceneManager.LoadScene(scene);
    }


    public void setDifficulty(int value)
    {
        difficultyChosen = value;
    }

}
