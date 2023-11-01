using System.Collections.Generic;
using UnityEngine;

public class CursorManager : StaticInstance<CursorManager>{
    public enum CursorType {
        Default,
        Clickable,
        Clicking,
        Tapp,
    }


    [SerializeField]
    private List<Texture2D> cursorTextures;

    private CursorType currentCursorType = CursorType.Default;
    private Interactable _currentInteractable;

    public void SetCursorClickable() {
        Cursor.SetCursor(cursorTextures[1], Vector2.zero, CursorMode.ForceSoftware);
    }

    public void SetCursorDefault() {
        Cursor.SetCursor(cursorTextures[0], Vector2.zero, CursorMode.ForceSoftware);
    }
}
