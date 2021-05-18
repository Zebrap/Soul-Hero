using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    GUI_Script uI_Script;
    void Start()
    {
        uI_Script = GameObject.FindObjectOfType<GUI_Script>();
       GetComponent<HealthEnemy>().DieEvent += SpecialDieEvent; 
    }

    private void SpecialDieEvent(object sender, EventArgs e)
    {
        uI_Script.Win();
    }
}
