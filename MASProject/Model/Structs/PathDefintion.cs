using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASProject.Model.Structs
{
    public class PathDefintion
    {
        private PathDefintion()
        {
            Steps = new Dictionary<int, Position>();
        }
        public Dictionary<int,Position> Steps { get; private set; }
        public static PathDefintion FromJson(JToken steps)
        {
            PathDefintion res = new PathDefintion();
            foreach (var step in steps)
            {
                res.Steps[step["t"].Value<int>()] = new Position(step["x"].Value<uint>(), step["y"].Value<uint>());
            }
            return res;
        }
        public override string ToString()
        {
            return String.Join(',', Steps.Select((kv) => $"${kv.Key} -> ${kv.Value}"));

        }
    }


    public class Step
    {
        public int T { get; set; }
        public Position Pos { get; set; }

    }
}
