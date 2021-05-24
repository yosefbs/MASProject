using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MASProject.Model;
using MatrixLib.Matrix;


namespace MASProject.ViewModel
{
    /// <summary>
    /// A matrix that displays countries in the columns
    /// and attributes of a country in the rows.
    /// </summary>
    public class MatrixMap : MatrixBase<int, int>
    {
        Enviorment _map;
        public MatrixMap(Enviorment map)
        {
            _map = map;
        }
        protected override object GetCellValue(int rowHeaderValue, int columnHeaderValue)
        {
            return _map.GetCell(rowHeaderValue, columnHeaderValue);
        }

        protected override IEnumerable<int> GetColumnHeaderValues()
        {
            return Enumerable.Range(0, _map.NumOfCoulmns).ToList();
        }

        protected override IEnumerable<int> GetRowHeaderValues()
        {
            return Enumerable.Range(0, _map.NumOfRows).ToList();
        }
    }
}