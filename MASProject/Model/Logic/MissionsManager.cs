using MASProject.Model.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASProject.Model.Logic
{
    public class MissionsManager
    {
        static MissionsManager _instance;

        static object key = new object();
        public static MissionsManager Instance
        {
            get
            {
                if (_instance == null)
                    lock (key)
                    {
                        if (_instance == null)
                            _instance = new MissionsManager();
                    }
                return _instance;
            }
        }
        private MissionsManager()
        {
            missionHandlers = new List<CarrierBotController>();
        }
        public List<CarrierBotController> missionHandlers;

        public void RegisterAsMissionHandler(CarrierBotController missionHandler)
        {
            missionHandlers.Add(missionHandler);
        }

        public bool AssignMission(CarriersMission mission)
        {
            int minDist = int.MaxValue;
            CarrierBotController handler = null;
            foreach (var item in missionHandlers)
            {
                if (item.FreeForMission)
                {
                    var dist = item.StepsDistanceFromTarget(mission.From);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        handler = item;
                    }
                }

            }
            if (handler != null)
                handler.AssignedMission = mission;
            return handler == null;
            
        }
    }
}
