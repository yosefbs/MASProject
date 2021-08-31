using MASProject.Model.Logic;
using MASProject.Model.Structs;
using MASProject.RestServiceCaller;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MASProject.Model
{
    class EnviormentController
    {
        static Logger log = LogManager.GetCurrentClassLogger();
        private  EnviormentController()
        {

        }
        static Clock envClock;
        static Enviorment _env;
        static CbsInput baseCbsInput;
        public static Enviorment EnvInstance
        {
            get
            {
                if (_env == null)
                {
                    _env = initEnv();

                }
                return _env;
            }
        }

        private static Enviorment initEnv()
        {
            int envRows = 17;
            int envCoulmns = 17;
            Enviorment env = new Enviorment(envRows, envCoulmns);
             baseCbsInput = new CbsInput() { dimension = new Tuple<int, int>(envRows, envCoulmns),obstacles=new List<Position>() };
            envClock = new Clock();
            //load shelfs
            for (uint i = 7; i < 13; i++)            
                SetShelf(env, 10, i);

            for (uint i = 6; i < 13; i++)
                SetShelf(env, 6, i);

            for (uint i = 6; i < 13; i++)
                SetShelf(env, 14, i);

            for (uint i = 3; i < 9; i++)
                SetShelf(env, i, 14);

            for (uint i = 6; i < 12; i++)
                SetShelf(env, i, 4);

            SetDoor(env, 0, 0);
            SetDoor(env, 0, 1);
            SetDoor(env, 14, 14);
            SetDoor(env, 14, 13);
            
            for (uint i = 0; i < 3; i++)
            {

                var bot = new CarrierBot($"Bot{i}", (int)i);
                var controler = new CarrierBotController(bot, env);                
                envClock.OnTick += controler.Tick;
                env.AddItem(bot, new Position(9, 0 + i));
                MissionsManager.Instance.RegisterAsMissionHandler(controler);
            }

            return env;
        }

     

        private static void SetShelf(Enviorment env,uint row, uint coulmn)
        {
            Position pos = new Position(row, coulmn);
            env.AddItem(new Shelf(), pos);
            baseCbsInput.obstacles.Add(pos);
        }
        private static void SetDoor(Enviorment env, uint row, uint coulmn)
        {
            Position pos = new Position(row, coulmn);
            env.AddItem(new Door(), pos);
            baseCbsInput.obstacles.Add(pos);
        }

        public static void AssignMission(CarriersMission mission)
        {            
            MissionsManager.Instance.AssignMission(mission);
            ReCalcPath();
        }
        public static void StopAllMission()
        {
            envClock.Stop();
            foreach (var agent in MissionsManager.Instance.missionHandlers)
            {
                agent.AssignedMission = null;
                agent.PathToTarget = null;
            }
                
        }

        public static async void ReCalcPath()
        {
            log.Info("stop clock");
            envClock.Stop();
            var res = await DefinePaths();
            log.Info("start clock");
            envClock.Start();
        }
        public static async Task<bool> DefinePaths()
        {
            var cbsInput = baseCbsInput.CloneWithEmptyAgents();
            var agentDict = new Dictionary<string, CarrierBotController>();
            foreach (var agent in MissionsManager.Instance.missionHandlers)
            {
                var agentPos = _env.GetPosition(agent.HandlerItem);
                if (agent.AssignedMission == null)
                {
                    log.Info($"add bot as obstacle ${agent}");
                    cbsInput.obstacles.Add(agentPos);
                    continue;
                }
                agentDict[agent.HandlerItem.Id.ToString()] = agent;
                var cbsAgent = new Agent() { start = agentPos, name = agent.HandlerItem.Id.ToString() };
                if (agent.AssignedMission.PickedUp)
                    cbsAgent.goal = agent.AssignedMission.To;
                else
                    cbsAgent.goal = agent.AssignedMission.From;
                log.Info($"add bot to cbs calc ${agent}");
                cbsInput.agents.Add(cbsAgent);
            }
            var paths = await RestServiceCaller.Mapc.CalcCbs(cbsInput);
            foreach (var path in paths)
            {
                agentDict[path.Key].PathToTarget = path.Value;
                log.Info($"${agentDict[path.Key]} path is: ${path.Value}");
            }
            if (paths.Count < cbsInput.agents.Count)
                log.Error("Partial result received!");
            Console.WriteLine(paths);
            return paths != null;
            
        }
    }

    class PositionTracker
    {
        Dictionary<BaseItem, Position> _positions;
        public PositionTracker()
        {
            _positions = new Dictionary<BaseItem, Position>();
        }
        public void SetItemLocation(BaseItem item, Position position)
        {
            if (!_positions.ContainsKey(item))
                _positions.Add(item, null);
            _positions[item] = position;
        }

        public Position GetMyLocation(BaseItem item)
        {
            return _positions[item];
        }
    }
}
