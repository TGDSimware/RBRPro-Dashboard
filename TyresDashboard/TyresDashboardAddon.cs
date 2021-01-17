using RBRPro.Api;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TGD.Rbr.Telemetry.Data;

namespace RBRProDashboard
{
    /// <summary>
    /// The TestAddon
    /// A RBRPro Addon is a class implementing the IRbrProAddOn interface.
    /// Optionally, the Addon can receive telemetry by implementing the ITelemetryClient interface
    /// </summary>
    public class TyresDashboardAddon : IRbrProAddOn
    {
        // Maybe in future these properties will be replaced by class attributes
        #region ABOUT
        public string Name { get => "TyresDashboard"; }
        public string Description { get => "This is a Test Add-On"; }
        public string Author { get => "TGD"; }
        public readonly char[] Gears = { 'R', 'N', '1', '2', '3', '4', '5', '6' };

        // An optional icon is provided to the manager, just to decorate the tab item a bit...
        public Image Icon => new Image { Source = new BitmapImage(new Uri($"pack://application:,,,/TyresDashboard;component/icon.png", UriKind.Absolute)) };

        // This property tells the manager if the addon can be detached in a separate window or not
        public bool IsDetachable { get => true; }

        #endregion

        // The interface used to interact with the manager
        public IRbrPro _interactor;

        // The viewmodel class
        Model _model;

        public TyresDashboardAddon()
        {
            _model = new Model(this);       
        }

        /// <summary>
        /// Called by the manager just after the addon loading, the contents are not ready yet...
        /// but in the meantime you can do something to prepare...
        /// for example we can load our language strings
        /// </summary>
        public void Init(IRbrPro rbrProInteractor)
        {
            _interactor = rbrProInteractor;
            _interactor.DataReceived += _interactor_DataReceived;
        }

        private void _interactor_DataReceived(object sender, TelemetryData data)
        {
            // this is where the telemetry is received, the data object contais all the infos

            //PRESSURE 216340.858
            //TEMPERATURE 337 DEGRés
            //USURE 0.0036796


            _model.LeftFrontDamage = data.car.suspensionLF.wheel.tire.segment1.wear;
            _model.RightFrontDamage = data.car.suspensionLF.wheel.tire.segment1.wear;
            _model.LeftRearDamage = data.car.suspensionLF.wheel.tire.segment1.wear;
            _model.RightRearDamage = data.car.suspensionLF.wheel.tire.segment1.wear;

            _model.LeftFrontPressure = data.car.suspensionLF.wheel.tire.pressure;
            _model.RightFrontPressure = data.car.suspensionRF.wheel.tire.pressure;
            _model.LeftRearPressure = data.car.suspensionLB.wheel.tire.pressure;
            _model.RightRearPressure = data.car.suspensionRB.wheel.tire.pressure;

            _model.LeftFrontTemperature = data.car.suspensionLF.wheel.tire.temperature;
            _model.RightFrontTemperature = data.car.suspensionRF.wheel.tire.temperature;
            _model.LeftRearTemperature = data.car.suspensionLB.wheel.tire.temperature;
            _model.RightRearTemperature = data.car.suspensionRB.wheel.tire.temperature;
        }

        /// <summary>
        /// Called by the manager when it finishes the initialization and the contents are available
        /// </summary>
        /// <param name="rbrProInteractor"></param>
        public void Ready(IRbrPro rbrProInteractor)
        {                

        }

        /// <summary>
        /// Called by the manager, this method just returns an instance of the GUI Control
        /// </summary>
        /// <returns></returns>
        public System.Windows.Controls.Control GetGui()
        {
            return new TyresDashboardGui(this, _interactor, _model);
        }

        public void Exit()
        {
            // No action performed
        }

        public void OnParentWindowChange(Window parent)
        {
            // No action performed
        }
    }
}
