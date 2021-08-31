using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MASProject.Model;
using MASProject.Model.Logic;
using MASProject.Model.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASProject.ViewModel
{
    public class MissionsMangerViewModel : ViewModelBase
    {
        public RelayCommand AssignMissionCommand { get; private set; }
        public RelayCommand StopAllMission { get; private set; }

        public List<CarrierBotController> Agents { get {return MissionsManager.Instance.missionHandlers; } }

        CarriersMission _newMission;
        public CarriersMission NewMission { get { return _newMission; } private set { _newMission = value; RaisePropertyChanged(); } }
        public MissionsMangerViewModel()
        {
            _newMission = new CarriersMission();
            AssignMissionCommand = new RelayCommand(
                () => 
                {
                    if (NewMission.From.EqualsPos(NewMission.To))
                        return;
                    EnviormentController.AssignMission(NewMission);
                    NewMission = new CarriersMission();
                }
                
                );
            StopAllMission = new RelayCommand(() => { EnviormentController.StopAllMission(); });
       
        }
    }
}
