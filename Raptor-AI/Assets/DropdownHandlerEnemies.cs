using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DropdownHandlerEnemies : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdownRef;

    //assegna l'evento per modificare il numero di nemici in base alla scelta
    void Start()
    {
        LevelSelector.enemyNumber = 1;

        dropdownRef.onValueChanged.AddListener(delegate
        {
            getNewNumEnemies(dropdownRef);
        });

    }

    void getNewNumEnemies(TMPro.TMP_Dropdown down)
    {
        LevelSelector.enemyNumber = down.value + 1;
    }
}
