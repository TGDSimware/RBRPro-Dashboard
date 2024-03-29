﻿using RBRPro.Api;
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
    public class WRCDashboardAddon : IRbrProAddOn
    {
        // Maybe in future these properties will be replaced by class attributes
        #region ABOUT
        public string Name { get => "WRCDashboard"; }
        public string Description { get => "This is a Test Add-On"; }
        public string Author { get => "TGD"; }
        public readonly char[] Gears = { 'R', 'N', '1', '2', '3', '4', '5', '6' };

        // An optional icon is provided to the manager, just to decorate the tab item a bit...
        public Image Icon => new Image { Source = new BitmapImage(new Uri($"pack://application:,,,/WRCDashboard;component/icon.png", UriKind.Absolute)) };

        // This property tells the manager if the addon can be detached in a separate window or not
        public bool IsDetachable { get => true; }

        #endregion

        // The interface used to interact with the manager
        public IRbrPro _interactor;

        // The viewmodel class
        Model _model;

        public WRCDashboardAddon()
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
            _model.ControlHandbrake = data.control.handbrake;
            _model.StageDistance = data.stage.distanceToEnd;
            _model.StageTime = TimeSpan.FromSeconds(data.stage.raceTime).ToString("%m'.'ss'.'f");
            _model.ControlGear = Gears[data.control.gear];
            _model.ControlThrottle = data.control.throttle;
            _model.ControlBrakePressure = data.control.footbrakePressure;
            _model.CarSpeed = (int) Math.Round(data.car.speed);
            _model.EngineRpm = data.car.engine.rpm;
            _model.RpmMax = Convert.ToDecimal(10000 - data.car.engine.rpm);
        }

        /// <summary>
        /// Called by the manager when it finishes the initialization and the contents are available
        /// </summary>
        /// <param name="rbrProInteractor"></param>
        public void Ready(IRbrPro rbrProInteractor)
        {
            _model.DriverName = _interactor.User.Name.ToUpper() ;
            _model.DriverFlag = new BitmapImage(new Uri($"http://rbrpro.org/img/flags/{_interactor.User.Country}.jpg"));
                

        }

        /// <summary>
        /// Called by the manager, this method just returns an instance of the GUI Control
        /// </summary>
        /// <returns></returns>
        public System.Windows.Controls.Control GetGui()
        {
            return new WRCDashboardGui(this, _interactor, _model);
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
