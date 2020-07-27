using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{

    private GUI_Script GUI_Script;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private void Start()
    {
        GameObject UI = GameObject.Find(Tags.CANVAS_TAG);
        GUI_Script = UI.GetComponent<GUI_Script>();
    }

    private void OnMouseOver()
    {
        if (!GUI_Script.IsMauseOverUi())
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
    }
    void OnMouseExit()
    {
        Cursor.SetCursor(null, hotSpot, cursorMode);
    }

}
