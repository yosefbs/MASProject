using MASProject.Model.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MASProject.Model.Logic
{
    internal class CarrierBotController
    {
        Func<MoveableItem, Position> _locationProvider;
        public CarrierBotController(CarrierBot bot, Func<MoveableItem,Position> locationProvider)
        {
            _locationProvider = locationProvider;
            Task.Run(async () =>
            {
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        await Task.Delay(1000);
                        bot.Move(Direction.UP);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }                
            });

        }
    }
}
