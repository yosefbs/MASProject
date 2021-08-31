using System;
using System.Collections.Generic;
using System.Text;

namespace MASProject.Model.Structs
{
    public class CarrierBot : MoveableItem
    {
        public string NameId { get; private set; }
        public CarrierBot(string name,int id) : base(id)
        {
            NameId = name;
            //PositionOnMap = position;
        }
        public override string ToString()
        {
            return $"CarrierBot - {NameId}";
        }

    }
}
