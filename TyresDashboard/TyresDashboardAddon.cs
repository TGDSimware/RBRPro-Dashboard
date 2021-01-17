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
            //TEMPERATURE 337 KELVINS
            //USURE 0.0036796

            _model.LeftFrontDamage = (int) AverageDamageTyres(data.car.suspensionLF.wheel.tire);
            _model.RightFrontDamage = (int)AverageDamageTyres(data.car.suspensionRF.wheel.tire);
            _model.LeftRearDamage = (int)AverageDamageTyres(data.car.suspensionLB.wheel.tire);
            _model.RightRearDamage = (int)AverageDamageTyres(data.car.suspensionRB.wheel.tire);

            _model.LeftFrontPressure = PascalToPsi(data.car.suspensionLF.wheel.tire.pressure);
            _model.RightFrontPressure = PascalToPsi(data.car.suspensionLF.wheel.tire.pressure);
            _model.LeftRearPressure = PascalToPsi(data.car.suspensionLB.wheel.tire.pressure);
            _model.RightRearPressure = PascalToPsi(data.car.suspensionRB.wheel.tire.pressure);

            _model.LeftFrontTemperature = KelvinCelsiusConverter(data.car.suspensionLF.wheel.tire.temperature, true);
            _model.RightFrontTemperature = KelvinCelsiusConverter(data.car.suspensionRF.wheel.tire.temperature, true);
            _model.LeftRearTemperature = KelvinCelsiusConverter(data.car.suspensionLB.wheel.tire.temperature, true);
            _model.RightRearTemperature = KelvinCelsiusConverter(data.car.suspensionRB.wheel.tire.temperature, true);

    }

        private static double AverageDamageTyres(Tire tire)
        {
            float[] damageSegment = {tire.segment1.wear, tire.segment2.wear, tire.segment3.wear, tire.segment4.wear,
                tire.segment5.wear, tire.segment6.wear, tire.segment7.wear, tire.segment8.wear};

            double sum = 0;

            for (int i = 0; i < damageSegment.Length; i++)
            {
                sum += damageSegment[i];
            }

            double average = sum / damageSegment.Length;
            return 100 - (average * 100);
        }

        private static double PascalToPsi(double value)
        {
            return Math.Round(value / 6894.76, 1);
        }


        private static double KelvinCelsiusConverter(double value, bool kelvin = false)
        {
            double kelvinToCelsius = Math.Round(value - 273.15, 1);
            double celsiusToKelvin = Math.Round(value + 273.15, 1);
            return kelvin ? kelvinToCelsius : celsiusToKelvin;
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
