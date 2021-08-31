using MASProject.Model.Structs;
using MASProject.RestServiceCaller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MASProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //test();

        }

        public async void test()
        {
            try
            {
                CbsInput ci = new CbsInput();
                ci.dimension = new Tuple<int, int>(17, 17);
                ci.obstacles = new List<Position>(){
                    new Position(10, 7),
                    new Position(10, 8),
                    new Position(10, 9),
                    new Position(10, 10),
                    new Position(10, 11),
                    new Position(10, 12),

                    new Position(6, 6),
                    new Position(6, 7),
                    new Position(6, 8),
                    new Position(6, 9),
                    new Position(6, 10),
                    new Position(6, 11),
                    new Position(6, 12),

                    new Position(14, 6),
                    new Position(14, 7),
                    new Position(14, 8),
                    new Position(14, 9),
                    new Position(14, 10),
                    new Position(14, 11),
                    new Position(14, 12),

                    new Position(3, 14),
                    new Position(4, 14),
                    new Position(5, 14),
                    new Position(6, 14),
                    new Position(7, 14),
                    new Position(8, 14),

                    new Position(6, 4),
                    new Position(7, 4),
                    new Position(8, 4),
                    new Position(9, 4),
                    new Position(10, 4),
                    new Position(11, 4),

                    new Position(0, 0),
                    new Position(0, 1),
                    new Position(14, 14),
                    new Position(14, 13),
                    new Position(9, 2)
                };
                ci.agents = new List<Agent>() {
                    new Agent()   { goal = new Position(15,15), start = new Position(3,3), name="agent0"},
                      new Agent()   { goal = new Position(2,15), start = new Position(9,1), name="agent1"}
                };
                var res = await Mapc.CalcCbs(ci);
                Console.WriteLine(res);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
