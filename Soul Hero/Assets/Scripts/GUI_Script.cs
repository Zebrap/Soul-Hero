using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Script : MonoBehaviour
{
    public GameObject SkillTree;

    void Awake()
    {
        SkillTree.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnoOffGameObject(SkillTree);
        }
    }

    private void OnoOffGameObject(GameObject window)
    {
        if (window.activeSelf)
        {
            window.SetActive(false);
        }
        else
        {
            window.SetActive(true);
        }
    }

}
