using GalaSoft.MvvmLight;
using MASProject.Model;
using MatrixLib.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MASProject.ViewModel
{
    public class MapViewModel : ViewModelBase
    {
        public MatrixMap MapData {get;private set;}
        //public int Rows { get { return MapData.MapInfo.GetLength(0); } }
        //public int Coulmns { get { return MapData.MapInfo.GetLength(1); } }
        public MapViewModel( )
        {
            MapData = new MatrixMap(EnviormentController.EnvInstance);
        }
    }
  
}
