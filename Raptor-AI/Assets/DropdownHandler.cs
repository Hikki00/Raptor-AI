using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{

    public TMPro.TMP_Dropdown dropdownRef;

    // assegna l'evento per modificare la difficoltà in base alla scelta
    // 1 = easy 
    // 2 = hard
    // 3 = raptor
    void Start()
    {
        LevelSelector.difficultyChosen = 1;

        dropdownRef.onValueChanged.AddListener(delegate
        {
            getNewDifficulty(dropdownRef);
        });

    }

    void getNewDifficulty(TMPro.TMP_Dropdown down)
    {
        LevelSelector.difficultyChosen = down.value + 1;
    }
}
