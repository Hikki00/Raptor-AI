using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DropdownHandlerEnemies : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdownRef;

    // Start is called before the first frame update
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
