using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelector : MonoBehaviour
{
    public static int spawnChosen;
    public static int difficultyChosen;
    public static int enemyNumber;


    [SerializeField] string scene;
    [SerializeField] int level;

    // Start is called before the first frame update
    void Start()
    {

    }

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
