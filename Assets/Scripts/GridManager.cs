using UnityEngine;
using System;


namespace Tetris
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] cells = new GameObject[220];
        int gridWidth = 10;
        int gridHeight = 21;

        private void Start()
        {
            Color[] grid = new Color[220];
            UpdateGrid(grid);
        }

        private void AssignColor(int x, int y, Color c)
        {
            c.a = 1f; // set alpha
            int index = GetArrayLocation(x, y);
            cells[index].GetComponent<SpriteRenderer>().color = c;
        }
        private int GetArrayLocation(int x, int y) 
        {
            return x * 10 + y;
        }
        private int GetX(int index)
        {
            return index / 10;
        }
        private int GetY(int index)
        {
            return (index - (index / 10) * 10);
        }

        public void UpdateCell(int x, int y, Color c)
        {
            int i = GetArrayLocation(x, y);
            cells[i].GetComponent<SpriteRenderer>().color = c;
        }
        public void UpdatePosition(int i, Color c)
        {
            if (c == Palette.White)
            {
                c.a = 0;
            } else
            {
                c.a = 1f;
            }
            if (i < 0 || i > 220) return;
            cells[i].GetComponent<SpriteRenderer>().color = c;
        }
        public void UpdateGrid(Color[] newGrid)
        {
            if (newGrid == null) return;
            int length = Math.Min(cells.Length, newGrid.Length);
            for (int i = 0; i < length; i++)
            {
                cells[i].GetComponent<SpriteRenderer>().color = newGrid[i];
            }

        }
        public bool HasCollision(int pos)
        {
            if (pos < 0 || pos > cells.Length) return true;

            if (cells[pos].GetComponent<SpriteRenderer>().color.a > 0)
            {
                return true;
            }
            return false;
        }
        public void ResolveLines()
        {
            bool flag = false;

            Debug.Log("GridManager::ResolveLines called.");
            for (int i=gridHeight; i > 0; i--)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    if (cells[i*gridWidth -1 -j].GetComponent<SpriteRenderer>().color.a == 0)
                    {
                        flag = false;
                    } 
                }
                if (flag)
                {
                    Debug.Log($"Solid line detected at {i-1}");
                    RemoveLineAt(i - 1);
                    GameManager.Instance.UpdateScore(); // increase score
                }
                flag = true;
            }
        }
        private void RemoveLineAt(int index)
        {
            //Debug.Log($"GridManager::RemoveLineAt called with index = {index}");
            for (int i = index+2; i< gridHeight;  i++)
                CopyRow(i, i - 1);
        }


        private void CopyRow(int source, int target)
        {
            int sourceStartPos = source * gridWidth - 1;
            int targetStartPos = target * gridWidth - 1;

            //Debug.Log($"sourceStartPos={sourceStartPos} --> targetStartPos = {targetStartPos}");

            GameObject sourceObject;
            GameObject targetObject;
            SpriteRenderer sourceSR;
            SpriteRenderer targetSR;

            for (int i =0; i<gridWidth; i++)
            {
                sourceObject = cells[sourceStartPos-i];
                targetObject = cells[targetStartPos-i];

                sourceSR = sourceObject.GetComponent<SpriteRenderer>();
                targetSR = targetObject.GetComponent<SpriteRenderer>();

                if (sourceSR != null && targetSR != null)
                {
                    targetSR.color = sourceSR.color;
                }

            }

        }
    }
}
