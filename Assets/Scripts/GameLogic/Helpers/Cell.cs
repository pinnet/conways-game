using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CellularAutomaton
{
    public class Cell
    {
        private bool alive;
        private OwnerSO owner;
        private Vector2Int position;

        public Cell()
        {
            this.alive = false;
            this.owner = null;
        }
        public bool Alive 
        { 
            get { return alive; } 
            set { if (value == false) { this.owner = null; } } 
        }
                            
        public OwnerSO Owner
        {
            get { return owner; }
            set { if (value != null) { owner = value; alive = true; }
                  else { owner = null; alive = false; }
                }
        }
        public Vector2Int Position { get { return position; } set { position = value; } }
    }
}