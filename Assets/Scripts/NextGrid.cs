using UnityEngine;
using System;

namespace Tetris
{
    public class NextGrid : MonoBehaviour
    {
        [SerializeField] private GameObject[] cells = new GameObject[220];

        private void Awake()
        {
            Color[] grid = new Color[220];
            UpdateGrid(grid);
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
        public void RenderTet(Tetro tet)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cells[i * 4 + j].GetComponent<SpriteRenderer>().color = Palette.Black;
                }
            }
            TetroShape tshape = tet.shapes[0];
            Debug.Log($"NextGrid::RenderTet ==> {tshape.shape[0]}, {tshape.shape[1]}, {tshape.shape[2]}, {tshape.shape[3]}");
            Matrix4x4Data m = tshape.xshape;
            Color t = Palette.Yellow;
            t.a = 1.0f;

            for (int i = 0; i < 4; i++) 
            {
                for (int j = 0; j < 4; j++)
                {
                    if (m.GetValue(i, j)) {
                        cells[i * 4 + j].GetComponent<SpriteRenderer>().color = tshape.ActiveColor;
                    }
                }
            }
        }
    }
}
