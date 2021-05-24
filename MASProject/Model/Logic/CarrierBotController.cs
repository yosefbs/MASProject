using MASProject.Model.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MASProject.Model.Logic
{
    internal class CarrierBotController
    {
        Func<MoveableItem, Position> _locationProvider;
        public CarrierBotController(CarrierBot bot, Func<MoveableItem,Position> locationProvider)
        {
            _locationProvider = locationProvider;

        }
    }
}
