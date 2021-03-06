﻿using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RxVisualizer{
    /// <summary>
    /// The responsibility of the Drawer is to draw everything (Item marks, line labels, grid) for the RxVisualizerWindow
    /// </summary>
    public static class Drawer{

        private static readonly Vector2 LabelRectSize = new Vector2(400, 16); // Seems like 400x16 is good enough.

        private static readonly Color LineColor = new Color(.3f, .3f, .3f, 1f);

        
        public static void DrawLines(Rect rect,int countOfLines, float yAxisShift, float distanceBetweenLines){
            if (rect.width <= 1) return;
            if (countOfLines < 1) return;
        
            GLLine.PixelMatrixScope(() => {
            
                for (var i = 0; i < countOfLines; i++){
                    var y = yAxisShift + distanceBetweenLines * i;
                    if(y+rect.y > rect.yMax) continue;
                    if(rect.yMin > y+rect.y) continue;
                    GLLine.Draw(0, y, rect.width, y, LineColor);
                }
            });
        }

        #region Label

        /// <summary>
        /// Returns the position of label for line it belongs to.
        /// </summary>
        private static Vector2 GetLabelPosition(Vector2 linePosition){
            var labelOffset = Vector2.down * 16; // Shifts position of label 16 px up (V2.down = actually up because GUI Y axis look down)
            return linePosition + labelOffset;
        }

        private static Rect GetLabelRect(Vector2 position){
            return new Rect(position, LabelRectSize);
        }

        public static void DrawLabel(Vector2 linePosition, string text){
            var pos = GetLabelPosition(linePosition);
            var rect = GetLabelRect(pos);
            GUI.Label(rect, text);
        }
        
        #endregion
    }
}