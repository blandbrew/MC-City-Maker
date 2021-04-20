using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_City_Maker.Grid_Zones.Infrustructure
{
    public class GenericInfrustructure : INotifyPropertyChanged
    {

        public ObservableCollection<string> UI_InfrustructureSelection { get; private set; } = new ObservableCollection<string>()
        {
             "Road","Rails"
        };

        private string _InfrustructureType;
        public string InfrustructureType
        {
            get { return _InfrustructureType; }
            set { _InfrustructureType = value; RaisePropertyChanged(nameof(InfrustructureType)); }
        }


        /// <summary>
        /// Road - Defines the street width for a road
        /// </summary>
        private int _StreetWidth = 8;
        public int StreetWidth
        {
            get { return _StreetWidth; }
            set
            {
                _StreetWidth = value;
                RaisePropertyChanged(nameof(StreetWidth));
            }
        }

        /// <summary>
        /// Road - defines the sidewalk width for a road
        /// </summary>
        private int _SidewalkWidth = 2;
        public int SidewalkWidth
        {
            get { return _SidewalkWidth; }
            set
            {
                _SidewalkWidth = value;
                RaisePropertyChanged(nameof(SidewalkWidth));
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            //Console.WriteLine("Changed: " + propertyName);
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {

                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
