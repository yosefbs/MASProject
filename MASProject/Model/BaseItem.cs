using MASProject.Model.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MASProject.Model
{
    
    public abstract class BaseItem
    {

        public int Id { get; set; }
        public bool IsMoveable { get; protected set; }
        public void CrashWith(BaseItem item)
        {
            IsMoveable = false;
        }
        public BaseItem(int id)
        {
            Id = id;
        }

    }

    public delegate void ItemMoved(MoveableItem sender, Direction moveDirection);
    public abstract class MoveableItem:BaseItem
    {
        public event ItemMoved OnItemMoved;
     
        public MoveableItem(int id):base(id)
        {
            IsMoveable = true;
        }
        internal void Move(Direction direction)
        {
            OnItemMoved(this, direction);
        }
    }
}
