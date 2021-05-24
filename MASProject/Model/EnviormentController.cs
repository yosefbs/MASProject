using MASProject.Model.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MASProject.Model
{
    class EnviormentController
    {
        static Enviorment _env;
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
            Enviorment newMap = new Enviorment(20, 20);
            //load shelfs

            newMap.AddItem(new Shelf(), new Position(10, 10));
            newMap.AddItem(new Shelf(), new Position(10, 11));
            newMap.AddItem(new Shelf(), new Position(10, 12));
            newMap.AddItem(new CarrierBot(), new Position(9, 11));
            newMap.AddItem(new Door(), new Position(7, 11));
            newMap.AddItem(new Door(), new Position(19, 19));
            return newMap;
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
