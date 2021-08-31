using System;
using System.Collections.Generic;
using System.Text;

namespace MASProject.Model.Structs
{
    public class Position
    {
    
        public uint Row
        {
            get; set;
        }
        public uint Column
        {
            get;  set;
        }

        public Position(uint row, uint coulmn)
        {
            Row = row;
            Column = coulmn;

        }

        public  bool EqualsPos(Position obj)
        {
            return this.Row == obj.Row && this.Column == obj.Column;
        }
        public override string ToString()
        {
            return $"({Row},{Column})";
        }

        public int DistanceFrom(Position pos)
        {
            return (int)Math.Sqrt(Math.Pow(this.Column - pos.Column, 2) + Math.Pow(this.Row - pos.Row, 2));
        }

    }
}
