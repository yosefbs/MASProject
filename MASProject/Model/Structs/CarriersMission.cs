using System;
using System.Collections.Generic;
using System.Text;

namespace MASProject.Model.Structs
{
    public class CarriersMission
    {
        public Position From { get; set; }
        public Position To { get; set; }
        public bool PickedUp { get; set; }
        public CarriersMission()
        {
            From = new Position(0,0);
            To = new Position(0, 0);
        }
        public override string ToString()
        {
            return $"${From} TO ${To}, PickedUp: ${PickedUp}";
        }

    }
  
}
