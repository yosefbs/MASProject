using MASProject.Model.Logic;
using MASProject.Model.Structs;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace MASProject.Model
{
    public class Enviorment : IEnviormentSensor
    {
        Logger log = LogManager.GetCurrentClassLogger();
        Cell[,] mapInfo;
        PositionTracker positionTracker;
        public int NumOfRows
        {
            get { return mapInfo.GetLength(0); }
        }

        public int NumOfCoulmns
        {
            get { return mapInfo.GetLength(1); }
        }

        public Enviorment(int rows, int coulmns)
        {
            positionTracker = new PositionTracker();
            mapInfo = new Cell[rows,coulmns];
            for (uint r = 0; r < rows; r++)
            {
                for (uint c = 0; c < coulmns; c++)
                {
                    mapInfo[r, c] = new Cell(new Position(r,c));
                }
            }

        }

        public Cell GetHostCell(BaseItem item)
        {
            return GetCell(GetPosition(item));
        }
        public Cell GetCell(Position pos)
        {
            return mapInfo[pos.Row, pos.Column];
        }
        public Cell GetCell(int row,int coulmn)
        {
            return mapInfo[row, coulmn];
        }
         public Position GetPosition(BaseItem item)
        {
            return positionTracker.GetMyLocation(item);
        }
        private void ItemMoved(BaseItem item, Direction moveTo)
        {
            var pos = positionTracker.GetMyLocation(item);
            var cell = mapInfo[pos.Row, pos.Column];

            if (InvalidMoveRequest(pos, moveTo))
                throw new Exception($"Invalid Move! {moveTo} from {pos}");
            if (!item.IsMoveable)
                throw new Exception("Unmovable Item!");
            //if (mapInfo[item.PositionOnMap.Row, item.PositionOnMap.Column].CellContent != item)
            //    throw new Exception("Item must be added first!");
            uint newRow, newCoulmn, oldRow, oldCoulmn;
            oldRow = newRow = pos.Row;
            oldCoulmn = newCoulmn = pos.Column;
            switch (moveTo)
            {
                case Direction.UP:
                    newRow -= 1;
                    break;
                case Direction.DOWN:
                    newRow += 1;
                    break;
                case Direction.LEFT:
                    newCoulmn -= 1;
                    break;
                case Direction.RIGHT:
                    newCoulmn += 1;
                    break;
                default:
                    break;
            }
            var movedSuccess = mapInfo[newRow, newCoulmn].MoveInto(item);
            if (!movedSuccess)
            {
                log.Error($"cant move {item} to {newRow},{newCoulmn}");
            }
            else
            {
                log.Debug($"{item} moved to {newRow},{newCoulmn}");
                mapInfo[oldRow, oldCoulmn].Leave(item);
                positionTracker.SetItemLocation(item, new Position(newRow, newCoulmn));
            }

        }
        internal bool AddItem(BaseItem item,Position pos)
        {
            var obj = mapInfo[pos.Row, pos.Column];
            if (!obj.IsEmptyCell())
                throw new ArgumentException($"{pos} already used! (${obj})");
             mapInfo[pos.Row, pos.Column].MoveInto(item);
            
            positionTracker.SetItemLocation(item, pos);
            if (item is MoveableItem moveable)
            {
                moveable.OnItemMoved += ItemMoved;
            }            
            return true;
        }

        private bool InvalidMoveRequest(Position position, Direction moveTo)
        {
            bool isValid = true;
            switch (moveTo)
            {
                case Direction.UP:
                    isValid = isValid && position.Row >0;
                    break;
                case Direction.DOWN:
                    isValid = isValid && position.Row < NumOfRows ;
                    break;
                case Direction.LEFT:
                    isValid = isValid && position.Column > 0;
                    break;
                case Direction.RIGHT:
                    isValid = isValid && position.Column < NumOfCoulmns ;
                    break;
                default:
                    break;
            }
            return !isValid;
        }

        private bool IsFreePos(Position pos)
        {
            return mapInfo[pos.Row, pos.Column] == null;
        }

        private bool InRangePos(Position pos)
        {
            int rows = mapInfo.GetLength(0);
            int cols = mapInfo.GetLength(1);
            return pos.Row < rows && pos.Column < cols;
        }

        public bool CanMoveTo(BaseItem item, Direction direction)
        {
            return !InvalidMoveRequest(positionTracker.GetMyLocation(item), direction);
        }
    }

   
}
