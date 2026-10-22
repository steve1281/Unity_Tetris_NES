using System;

[Serializable]
public struct Matrix4x4Data
{
    // A 1D array of 16 elements represents our 4x4 grid
    public bool[] rows;

    // Helper method to access data like a 2D array in code
    public bool GetValue(int row, int col) => rows[row * 4 + col];
    public void SetValue(int row, int col, bool value) => rows[row * 4 + col] = value;
}
