using MASProject.Model.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASProject.RestServiceCaller
{
    public class CbsInput
    {
        public Tuple<int,int> dimension { get; set; }
        public List<Position> obstacles { get; set; }
        public List<Agent> agents { get; set; }

        public CbsInput CloneWithEmptyAgents()
        {
            var res = new CbsInput();
            res.dimension = dimension;
            res.obstacles = new List<Position>(obstacles);
            res.agents = new List<Agent>();
            return res;
        }
    }
    public class Agent
    {
        public Position start { get; set; }
        public Position goal { get; set; }
        public string name { get; set; }
    }
}
