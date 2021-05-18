using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GUI_Script : MonoBehaviour
{
    public GameObject SkillTree;
    public GameObject inventoryUI;
    public GameObject escapeUI;
    public GameObject winPanel;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    void Awake()
    {
        SkillTree.SetActive(false);
        inventoryUI.SetActive(false);
    }
    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnoOffGameObject(SkillTree);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnoOffGameObject(inventoryUI);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escapeUI.SetActive(!escapeUI.activeSelf);
            if (escapeUI.activeSelf)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
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
    public bool ClickGUI()
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        /*foreach (RaycastResult result in results)
          {
              Debug.Log("Hit " + result.gameObject.name);
          }*/

        if (results.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    public void Win()
    {
        PauseGame();
        OnoOffGameObject(winPanel);
    }

    public void CloseWinPanel()
    {
        ResumeGame();
        OnoOffGameObject(winPanel);
    }
    public bool IsMauseOverUi()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
