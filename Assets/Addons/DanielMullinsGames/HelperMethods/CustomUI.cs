using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CustomUI {
    public static Dictionary<Vector3, char> GetTextLetterPositions(Text text, float characterSpacing) {
        var letterPositions = new Dictionary<Vector3, char>();
        var textGen = text.cachedTextGenerator;
        int quadIndex = 0;
        float baseY = 0;
        // A variable to store the threshold for detecting a new line 
        // will depend on the line spacing on the DialogueText
        float threshold = 50f;

        for (int i = 0; i < text.text.Length; i++) {
            if (text.text[i] != ' ') {
                int vertIndex = quadIndex * 4;
                var quadCenterPos = (textGen.verts[vertIndex].position +
                    textGen.verts[vertIndex + 1].position +
                    textGen.verts[vertIndex + 2].position +
                    textGen.verts[vertIndex + 3].position) / 4f;

                // Make characters same hight if they are on the same line
                if(i ==0) {
                    baseY = quadCenterPos.y;
                } else {
                    // If the difference is greater than the threshold,
                    // assume a new line has started and update the base value
                    if (System.Math.Abs(quadCenterPos.y - baseY) > threshold) {
                        baseY = quadCenterPos.y;
                    }
                }
                quadCenterPos.y = baseY;
                // Add an x-coordinate offset based on the character spacing
                quadCenterPos.x += characterSpacing * quadIndex;
                letterPositions.Add(text.transform.TransformPoint(quadCenterPos), text.text[i]);
                quadIndex++;
            }
        }

        return letterPositions;
    }
}