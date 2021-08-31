using MASProject.Model.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MASProject.Model.Logic
{
    public interface IEnviormentSensor
    {
        Position GetPosition(BaseItem item);
        Cell GetHostCell(BaseItem item);
        bool CanMoveTo(BaseItem item, Direction direction);
    }
}
