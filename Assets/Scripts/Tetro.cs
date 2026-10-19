using UnityEngine;
using System.Collections.Generic;

namespace Tetris
{
    [CreateAssetMenu(fileName = "Tetro", menuName = "Tetro")]
    public class Tetro : ScriptableObject
    {
        [SerializeField] public List<int> shape = new List<int>();
        [SerializeField] public PaletteColor chosenColor = PaletteColor.Yellow;

        public Color ActiveColor => Palette.GetColor(chosenColor);

    }
}
