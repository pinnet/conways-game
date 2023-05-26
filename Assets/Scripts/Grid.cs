using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CellularAutomaton
{
    public class Grid
    {
        
        private DenseArray<Cell> _cells = null;

        public Grid(int x, int y)
        {
            if (x < 3 | y < 3) { throw new System.ArgumentException("Grid must be at least 3x3"); }
            if (x * y >= 10000) { throw new System.ArgumentException("Grid must be less than 10000 cells"); }
            _cells = new DenseArray<Cell>(x, y);
            if (_cells == null) { throw new System.ArgumentException("Null Array Error"); }
            else { InitArray(); }
        }

        public int Columns
        {
            get { return _cells.Columns; }
        }
        public int Rows
        {
            get { return _cells.Rows; }
        }  
        public Cell this[int x, int y]
        {
            get { return _cells[x, y]; }
            set {
                value.Position = new Vector2Int(x, y);
                _cells[x, y] = value;
            }
        }
        private void InitArray()
        {
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    Cell cell = new Cell();
                    cell.Position = new Vector2Int(i, j);
                    _cells[i, j] = cell;
                }
            }

        }
        public uint CountNeighbors(int x, int y)
        {
            uint count = 0;
            for (int i = x - 1; i <= x + 1; i++)
            {
                if (i < 0 | i >= Columns) { continue; }
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (j < 0 | j >= Rows) { continue; }
                    if (i == x & j == y) { continue; }
                    if (_cells[i, j].Alive) { count++; }
                }
            }
            return count;
        }
        public void Advance(OwnerSO owner)
        {
            Grid next = new Grid(Columns, Rows);
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    uint neighbors = CountNeighbors(i, j);
                    if (_cells[i, j].Alive)
                    {
                        if (neighbors <= 2 | neighbors > 3) 
                        { 
                            next[i, j].Alive = false; 
                        }
                        else 
                        { 
                            next[i, j].Owner = owner; 
                        }
                    }
                    else
                    {
                        if (neighbors == 3)
                        {
                            next[i, j].Owner = owner; 
                        }
                    }
                }
            }
            _cells = next._cells;
        }   
    }
}