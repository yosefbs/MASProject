using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASProject.Model
{
    public  class Clock
    {
        static Logger log = LogManager.GetCurrentClassLogger();
        public event EventHandler<int> OnTick;
        private  void RaiseEvent(int t)
        {
            OnTick?.Invoke(this, t);
        }

        bool active = false;
        public  void Stop()
        {
            active = false;

        }

        public  void Start()
        {
            active = true;
            Task.Run(async () =>
           {
           int i = 1;
           while (active)
           {
                   try
                   {
                       RaiseEvent(i++);
                       //log.Info("tick " + i.ToString());
                   }
                   catch (Exception ex)
                   {
                       log.Error(ex);
                   }
                    await Task.Delay(1000);
                }
            });
        }
    }
}
