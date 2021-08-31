using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASProject.RestServiceCaller
{
    public interface IMapc
    {

        [Post("/centralized/cbs")]
        Task<string> CalcCbs(CbsInput data);

    }
}
