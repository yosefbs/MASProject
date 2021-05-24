using System;
using System.Collections.Generic;
using System.Text;

namespace MASProject.Model.Structs
{
    public class Position
    {
    
        public uint Row
        {
            get;private set;
        }
        public uint Column
        {
            get; private set;
        }

        public Position(uint row, uint coulmn)
        {
            Row = row;
            Column = coulmn;

        }

        public override string ToString()
        {
            return $"({Row},{Column})";
        }

    }
}
