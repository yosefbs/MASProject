using MASProject.Model.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MASProject.Model
{
    public class Enviorment 
    {
        Cell[,] mapInfo;
        
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
            mapInfo = new Cell[rows,coulmns];
            for (uint r = 0; r < rows; r++)
            {
                for (uint c = 0; c < coulmns; c++)
                {
                    mapInfo[r, c] = new Cell(new Position(r,c));
                }
            }

        }
        public BaseItem GetItem(int row,int coulmn)
        {
            return mapInfo[row, coulmn].CellContent;
        }
        internal void ItemMoved(BaseItem item, Direction moveTo)
        {
            //if(InvalidMoveRequest(item.PositionOnMap,moveTo))
            //    throw new Exception($"Invalid Move! {moveTo} from {item.PositionOnMap}");
            //if(!item.IsMoveable)
            //    throw new Exception("Unmovable Item!");
            //if (mapInfo[item.PositionOnMap.Row , item.PositionOnMap.Column].CellContent != item)
            //    throw new Exception("Item must be added first!");
            //uint newRow, newCoulmn, oldRow, oldCoulmn;
            //oldRow = newRow = item.PositionOnMap.Row;
            //oldCoulmn = newCoulmn = item.PositionOnMap.Column;
            //switch (moveTo)
            //{
            //    case Direction.UP:
            //        newRow -= 1;
            //        //mapInfo[item.PositionOnMap.Row - 1, item.PositionOnMap.Column] = item;
            //        break;
            //    case Direction.DOWN:
            //        newRow += 1;
            //        //mapInfo[item.PositionOnMap.Row + 1, item.PositionOnMap.Column] = item;
            //        break;
            //    case Direction.LEFT:
            //        newCoulmn -= 1;
            //        //mapInfo[item.PositionOnMap.Row, item.PositionOnMap.Column - 1] = item;
            //        break;
            //    case Direction.RIGHT:
            //        newCoulmn += 1;
            //        //mapInfo[item.PositionOnMap.Row, item.PositionOnMap.Column + 1] = item;
            //        break;
            //    default:
            //        break;
            //}
            //mapInfo[newRow, newCoulmn].MoveInto(item);
            //mapInfo[oldRow, oldCoulmn].Leave(item);
            
            
            //mapInfo[dst.Row, dst.Column] = objToMove;

        }
        internal bool AddItem(BaseItem item,Position pos)
        {
            var obj = mapInfo[pos.Row, pos.Column];
            if (!obj.IsEmptyCell())
                throw new ArgumentException($"{pos} already used! (${obj})");
            mapInfo[pos.Row, pos.Column].MoveInto(item);
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
                    isValid = isValid && position.Row > 1;
                    break;
                case Direction.DOWN:
                    isValid = isValid && position.Row < NumOfRows - 1;
                    break;
                case Direction.LEFT:
                    isValid = isValid && position.Column > 1;
                    break;
                case Direction.RIGHT:
                    isValid = isValid && position.Column < NumOfCoulmns - 1;
                    break;
                default:
                    break;
            }
            return isValid;
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
    }

   
}
