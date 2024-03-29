﻿using RBRPro.Api;
using System.ComponentModel;
using TGD.Framework;
using System.Windows.Media.Imaging;

namespace RBRProDashboard
{
    public class Model : DynamicContextManager
    {
        //static IniFile _otherIni = new IniFile("OtherIniFile.ini");

        /// <summary>
        /// The Persistent Properties are loaded and saved automatically. They can have a default value. 
        /// Only C# base types only (the framework cannot persist complex types)
        /// </summary>
        #region PERSISTENT PROPERTIES
        [DefaultValue(true)]
        [ConfigProperty]
        public bool ShowCarSpeed { get { return GetIniProperty<bool>(); } set { SetIniProperty(value); } }

        /// <summary>
        /// This property is mapped in a specific section of the .ini File
        /// </summary>
        [ConfigProperty("Section2")] 
        public string AnotherPersistentProperty { get { return GetIniProperty<string>(); } set { SetIniProperty(value); } }

        /// <summary>
        /// This property is mapped in a specific section of the .ini File with a specific name
        /// </summary>
        [ConfigProperty("Section2", "PropertyName")]
        public string YetAnotherPersistentProperty { get { return GetIniProperty<string>(); } set { SetIniProperty(value); } }

        [DefaultValue(120)]
        [ConfigProperty("Overlay", "Top")]
        public double OverlayTop { get { return GetIniPropertyNoCache<double>(); } set { SetIniProperty(value); } }

        [DefaultValue(120)]
        [ConfigProperty("Overlay", "Left")]
        public double OverlayLeft { get { return GetIniPropertyNoCache<double>(); } set { SetIniProperty(value); } }

        /// <summary>
        /// This property is mapped in a specific section of the .ini File with a specific name
        /// </summary>
        /*[ConfigProperty]
        public string AdditionalPersistentProperty { get { return GetIniProperty<string>(_otherIni); } set { SetIniProperty(_otherIni, value); } }*/
        #endregion

        /// <summary>
        /// The Runtime Properties are the volatile ones. No limitations about the types: complex types and collections are allowed
        /// </summary>
        #region RUNTIME PROPERTIES
        [RuntimeProperty("Car.Speed")]
        public int CarSpeed { get { return GetRuntimeProperty<int>(); } set { SetRuntimeProperty(value); } }

        [RuntimeProperty("Control.Gear")]
        public char ControlGear { get { return GetRuntimeProperty<char>(); } set { SetRuntimeProperty(value); } }

        [RuntimeProperty("Control.Throttle")]
        public float ControlThrottle { get { return GetRuntimeProperty<float>(); } set { SetRuntimeProperty(value); } }

        [RuntimeProperty("Control.BrakePressure")]
        public float ControlBrakePressure { get { return GetRuntimeProperty<float>(); } set { SetRuntimeProperty(value); } }

        [RuntimeProperty("Control.Handbrake")]
        public float ControlHandbrake { get { return GetRuntimeProperty<float>(); } set { SetRuntimeProperty(value); } }

        [RuntimeProperty("Stage.Distance")]
        public float StageDistance { get { return GetRuntimeProperty<float>(); } set { SetRuntimeProperty(value); } }

        [RuntimeProperty("Stage.Time")]
        public string StageTime { get { return GetRuntimeProperty<string>(); } set { SetRuntimeProperty(value); } }

        [RuntimeProperty("Driver.Name")]
        public string DriverName { get { return GetRuntimeProperty<string>(); } set { SetRuntimeProperty(value); } }

        [RuntimeProperty("Driver.Country")]
        public BitmapImage DriverFlag { get { return GetRuntimeProperty<BitmapImage>(); } set { SetRuntimeProperty(value); } }

        [RuntimeProperty("Rpm.Max")]
        public decimal RpmMax { get { return GetRuntimeProperty<decimal>(); } set { SetRuntimeProperty(value); } }

        [RuntimeProperty("Engine.Rpm")]
        public float EngineRpm { get { return GetRuntimeProperty<float>(); } set { SetRuntimeProperty(value); } }

        /*[RuntimeProperty]
        public int AnotherRuntimeProperty { get { return GetRuntimeProperty<int>(); } set { SetRuntimeProperty(value); } }*/
        #endregion

        /// <summary>
        /// The file TestAddOn.ini is used as the main repository of the persistent properties
        /// </summary>
        public Model(WRCDashboardAddon addon) : base($"{AddOns.BASEPATH}\\{addon.Name}\\{addon.Name}.ini")
        {
            // Other model initialization code here
        }


    }
}
