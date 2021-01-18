using RBRPro.Api;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        public readonly int FirstPos = 35;

        public double FirstTempLF;
        public double FirstTempRF;
        public double FirstTempLR;
        public double FirstTempRR;

        Color[] _colors;

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

            if (_colors == null)
            {
                CreateColorsArray();
            }
            // I will try to add a critical section to avoid concurrent calls, remember that this event is called by another thread
            // then you are in another thread here, not in the same thread than your addon
           
                // it is better to try-catch this section
                try
                {
                    _model.LeftFrontDamage = Convert.ToInt32(Math.Round(AverageDamageTyres(data.car.suspensionLF.wheel.tire), 0)); // don't cast like this. use Math.Round or Convert.ToInt32()
                                                                                                                                   // this is better because it makes a rounding to nearest integer and then convert it to int

                    _model.RightFrontDamage = Convert.ToInt32(Math.Round(AverageDamageTyres(data.car.suspensionRF.wheel.tire),0));
                    _model.LeftRearDamage = Convert.ToInt32(Math.Round(AverageDamageTyres(data.car.suspensionLB.wheel.tire),0));
                    _model.RightRearDamage = Convert.ToInt32(Math.Round(AverageDamageTyres(data.car.suspensionRB.wheel.tire),0));

                    _model.LeftFrontPressure = PascalToPsi(data.car.suspensionLF.wheel.tire.pressure);
                    _model.RightFrontPressure = PascalToPsi(data.car.suspensionLF.wheel.tire.pressure);
                    _model.LeftRearPressure = PascalToPsi(data.car.suspensionLB.wheel.tire.pressure);
                    _model.RightRearPressure = PascalToPsi(data.car.suspensionRB.wheel.tire.pressure);

                    _model.LeftFrontTemperature = KelvinCelsiusConverter(data.car.suspensionLF.wheel.tire.temperature, true);
                    _model.RightFrontTemperature = KelvinCelsiusConverter(data.car.suspensionRF.wheel.tire.temperature, true);
                    _model.LeftRearTemperature = KelvinCelsiusConverter(data.car.suspensionLB.wheel.tire.temperature, true);
                    _model.RightRearTemperature = KelvinCelsiusConverter(data.car.suspensionRB.wheel.tire.temperature, true);


                //look at!

                _model.TemperatureLR = new SolidColorBrush(Color.FromArgb(1, 255,0,0));
                        //_model.TemperatureLF = new SolidColorBrush(colors[firstPos]);
                        //_model.TemperatureRR = new SolidColorBrush(colors[firstPos]);
                        //_model.TemperatureRF = new SolidColorBrush(colors[firstPos]);

                        /*this.firstTempLF = _model.LeftFrontTemperature;
                        this.firstTempRF = _model.RightFrontTemperature;
                        this.firstTempLR = _model.LeftRearTemperature;
                        this.firstTempRR = _model.RightRearTemperature;*/
                    


                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                /*else
                   * 
            {

                _model.TemperatureLR = new SolidColorBrush(colors[firstPos + ((int) _model.LeftRearTemperature - (int) firstTempLR)]);
                _model.TemperatureLF = new SolidColorBrush(colors[firstPos + ((int)_model.LeftFrontTemperature - (int)firstTempLF)]);
                _model.TemperatureRR = new SolidColorBrush(colors[firstPos + ((int)_model.RightRearTemperature - (int)firstTempRR)]);
                _model.TemperatureRF = new SolidColorBrush(colors[firstPos + ((int)_model.RightFrontTemperature - (int)firstTempRF)]);

                Console.WriteLine((int)_model.LeftRearTemperature - (int)firstTempLR);
            }*/


        }

        private Color[] CreateColorsArray()
        {
            return _colors = new Color[] {
                Color.FromRgb(0, 31, 229),
                Color.FromRgb(5, 35, 229),
                Color.FromRgb(10, 40, 230),
                Color.FromRgb(15, 44, 230),
                Color.FromRgb(20, 49, 231),
                Color.FromRgb(26, 53, 231),
                Color.FromRgb(31, 58, 232),
                Color.FromRgb(36, 63, 232),
                Color.FromRgb(41, 67, 233),
                Color.FromRgb(46, 72, 233),

                Color.FromRgb(52, 76, 234),
                Color.FromRgb(57, 81, 234),
                Color.FromRgb(62, 85, 235),
                Color.FromRgb(67, 90, 235),
                Color.FromRgb(72, 95, 236),
                Color.FromRgb(78, 99, 236),
                Color.FromRgb(83, 104, 237),
                Color.FromRgb(93, 113, 238),
                Color.FromRgb(93, 113, 238),
                Color.FromRgb(98, 117, 239),

                Color.FromRgb(104, 122, 239),
                Color.FromRgb(109, 127, 240),
                Color.FromRgb(114, 131, 240),
                Color.FromRgb(119, 136, 241),
                Color.FromRgb(124, 140, 241),
                Color.FromRgb(130, 145, 242),
                Color.FromRgb(135, 149, 242),
                Color.FromRgb(140, 154, 243),
                Color.FromRgb(145, 159, 243),
                Color.FromRgb(150, 163, 244),

                Color.FromRgb(156, 168, 244),
                Color.FromRgb(161, 172, 245),
                Color.FromRgb(166, 177, 245),
                Color.FromRgb(171, 181, 246),
                Color.FromRgb(176, 186, 247),
                Color.FromRgb(182, 191, 247),
                Color.FromRgb(187, 195, 248),
                Color.FromRgb(192, 200, 248),
                Color.FromRgb(197, 204, 249),
                Color.FromRgb(202, 209, 249),


                Color.FromRgb(208, 213, 250),
                Color.FromRgb(213, 218, 250),
                Color.FromRgb(218, 223, 251),
                Color.FromRgb(223, 227, 251),
                Color.FromRgb(228, 232, 252),
                Color.FromRgb(234, 236, 252),
                Color.FromRgb(239, 241, 253),
                Color.FromRgb(244, 245, 253),
                Color.FromRgb(249, 250, 254),
                Color.FromRgb(255, 255, 255),


                Color.FromRgb(255, 255, 255),
                Color.FromRgb(255, 249, 249),
                Color.FromRgb(255, 244, 244),
                Color.FromRgb(255, 239, 239),
                Color.FromRgb(255, 234, 234),
                Color.FromRgb(255, 228, 228),
                Color.FromRgb(255, 223, 223),
                Color.FromRgb(255, 218, 218),
                Color.FromRgb(255, 213, 213),
                Color.FromRgb(255, 208, 208),

                Color.FromRgb(255, 202, 202),
                Color.FromRgb(255, 197, 197),
                Color.FromRgb(255, 192, 192),
                Color.FromRgb(255, 187, 187),
                Color.FromRgb(255, 182, 182),
                Color.FromRgb(255, 176, 176),
                Color.FromRgb(255, 171, 171),
                Color.FromRgb(255, 166, 166),
                Color.FromRgb(255, 161, 161),
                Color.FromRgb(255, 156, 156),

                Color.FromRgb(255, 150, 150),
                Color.FromRgb(255, 145, 145),
                Color.FromRgb(255, 140, 140),
                Color.FromRgb(255, 135, 135),
                Color.FromRgb(255, 130, 130),
                Color.FromRgb(255, 124, 124),
                Color.FromRgb(255, 119, 119),
                Color.FromRgb(255, 114, 114),
                Color.FromRgb(255, 109, 109),
                Color.FromRgb(255, 104, 104),


                Color.FromRgb(255, 98, 98),
                Color.FromRgb(255, 93, 93),
                Color.FromRgb(255, 88, 88),
                Color.FromRgb(255, 83, 83),
                Color.FromRgb(255, 78, 78),
                Color.FromRgb(255, 72, 72),
                Color.FromRgb(255, 67, 67),
                Color.FromRgb(255, 62, 62),
                Color.FromRgb(255, 57, 57),
                Color.FromRgb(255, 52, 52),

                Color.FromRgb(255, 46, 46),
                Color.FromRgb(255, 41, 41),
                Color.FromRgb(255, 36, 36),
                Color.FromRgb(255, 31, 31),
                Color.FromRgb(255, 26, 26),
                Color.FromRgb(255, 20, 20),
                Color.FromRgb(255, 15, 15),
                Color.FromRgb(255, 10, 10),
                Color.FromRgb(255, 5, 5),
                Color.FromRgb(255, 0, 0),
            };
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
            _model.TemperatureLR = Brushes.Aquamarine; // this works ?
        //yes
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
