using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour{
    public enum CursorType {
        Default,
        Clickable,
        Generator,
        Code,
    }


    [SerializeField]
    private List<Texture2D> cursorTextures;

    private CursorType currentCursorType = CursorType.Default;


}
