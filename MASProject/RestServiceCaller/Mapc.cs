using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MASProject.Model.Structs;
using Newtonsoft.Json.Linq;
using Refit;

namespace MASProject.RestServiceCaller
{
    public class Mapc
    {


        public static async Task<Dictionary<string, PathDefintion>> CalcCbs(CbsInput ci)
        {
            IMapc _instance = RestService.For<IMapc>("http://127.0.0.1:8000");
            try
            {
                var res = await _instance.CalcCbs(ci);

                JObject agentsResults = JObject.Parse(res);
                Dictionary<string, PathDefintion> result = new Dictionary<string, PathDefintion>();
                foreach (var agentRes in agentsResults)
                {
                    result.Add(agentRes.Key, PathDefintion.FromJson(agentRes.Value));
                    Console.WriteLine(agentRes);
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("err", ex);
            }
        }
    }
}

