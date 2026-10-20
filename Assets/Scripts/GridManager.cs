using UnityEngine;
using System;


namespace Tetris
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] cells = new GameObject[220];
        private bool updateAvailable;

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

            //Array.Copy(newGrid, cells, length);
            updateAvailable = true;
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
    }
}
