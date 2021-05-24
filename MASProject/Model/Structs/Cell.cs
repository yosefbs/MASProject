﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MASProject.Model.Structs
{
    public delegate void CellContentChanged(Cell sender);
    public class Cell
    {
        public event CellContentChanged OnContentChanged;
        public Position CellPos { get; private set; }
        public BaseItem CellContent { get; private set; }
        //public IItemOnMap C
        public Cell(Position pos)
        {
            CellPos = pos;
        }

        public bool MoveInto(BaseItem item)
        {
            if(CellContent != null && item !=null)
            {
                CellContent.CrashWith(item);
                item.CrashWith(CellContent);
                return false;
            }

            CellContent = item;
            if (OnContentChanged != null)
                OnContentChanged(this);
            return true;
        }

        internal void Leave(BaseItem item)
        {
            if (CellContent != item)
                throw new Exception("Cant leave place that you not there at all!");
            CellContent = null;
            if (OnContentChanged != null)
                OnContentChanged(this);
        }

        internal bool IsEmptyCell()
        {
            return CellContent == null;
        }
    }
}
