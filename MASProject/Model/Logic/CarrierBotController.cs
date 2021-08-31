using MASProject.Model.Structs;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MASProject.Model.Logic
{
    public  class CarrierBotController
    {
        IEnviormentSensor _env;
        CarrierBot _bot;
        Logger log = LogManager.GetCurrentClassLogger();
        public PathDefintion PathToTarget { get; set; }
        public CarrierBotController(CarrierBot bot, IEnviormentSensor env)
        {
            _env = env;
            _bot = bot;
            //Demo();
        }

        //public CarriersMission IMissionHandler.AssignedMission { get;  set; }

        public bool FreeForMission => _bot.IsMoveable && AssignedMission == null;
        public BaseItem HandlerItem => _bot;
        public CarriersMission AssignedMission { get ; set; }

        public int StepsDistanceFromTarget(Position pos)
        {
            var myPos = _env.GetPosition(_bot);
            return myPos.DistanceFrom(pos);
        }

        public override string ToString()
        {
            return $"${_bot} Controler, Mission: ${AssignedMission}";
        }
        public void Tick(object timeProvider, int t)
        {
            if (PathToTarget == null || !PathToTarget.Steps.ContainsKey(t))
                return;

            var myPos = _env.GetPosition(_bot);
            if (PathToTarget.Steps[t - 1].EqualsPos(myPos))
            {
                Move(myPos, PathToTarget.Steps[t]);
                myPos = _env.GetPosition(_bot);
            }
            if (AssignedMission != null && myPos.EqualsPos(AssignedMission.From))
            {
                AssignedMission.PickedUp = true;
                EnviormentController.ReCalcPath();
            }
            if (AssignedMission != null && myPos.EqualsPos(AssignedMission.To))
                AssignedMission = null;

        }

        private void Move(Position from,Position to)
        {
            if (from.Row == to.Row)
            {
                if (from.Column == to.Column - 1)
                    _bot.Move(Direction.RIGHT);
                if (from.Column == to.Column + 1)
                    _bot.Move(Direction.LEFT);
            }
            if (from.Column == to.Column)
            {
                if (from.Row == to.Row - 1)
                    _bot.Move(Direction.DOWN);
                if (from.Row == to.Row + 1)
                    _bot.Move(Direction.UP);
            }
        }
        //private void Demo()
        //{
        //    Task.Run(async () =>
        //    {
        //        Random r = new Random();
        //        for (int i = 0; i < 10005; i++)
        //        {
        //            try
        //            {
        //                if (!_bot.IsMoveable)
        //                    break;
        //                await Task.Delay(r.Next(1000));
        //                var myPos = _env.GetPosition(_bot);
        //                Direction directionToMove = (Direction)r.Next(4);
        //                if (_env.CanMoveTo(_bot, directionToMove))
        //                {
        //                    log.Info($"{_bot} try to move {directionToMove} from {_env.GetPosition(_bot)}");
        //                    _bot.Move(directionToMove);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.Message);
        //            }
        //        }
        //    });

        //}


    }
}
