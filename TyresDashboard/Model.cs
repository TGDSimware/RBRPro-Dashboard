using RBRPro.Api;
using System.ComponentModel;
using TGD.Framework;
using System.Windows.Media.Imaging;
using System.Windows.Media;

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
        public double YetAnotherPersistentProperty { get { return GetIniProperty<double>(); } set { SetIniProperty(value); } }

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

        //TEMPERATURE PNEU GAUCHE AV float
        [RuntimeProperty("Tyre.LeftFrontTemperature")]
        public double LeftFrontTemperature { get { return GetRuntimeProperty<double>(); } set { SetRuntimeProperty(value); } }

        //PRESSION PNEU GAUCHE AV float
        [RuntimeProperty("Tyre.LeftFrontPressure")]
        public double LeftFrontPressure { get { return GetRuntimeProperty<double> (); } set { SetRuntimeProperty(value); } }

        //USURE PNEU GAUCHE AV float
        [RuntimeProperty("Tyre.LeftFrontDamage")]
        public int LeftFrontDamage { get { return GetRuntimeProperty<int> (); } set { SetRuntimeProperty(value); } }


        //TEMPERATURE PNEU DROIT AV float
        [RuntimeProperty("Tyre.RightFrontTemperature")]
        public double RightFrontTemperature { get { return GetRuntimeProperty<double>(); } set { SetRuntimeProperty(value); } }

        //PRESSION PNEU DROIT AV float
        [RuntimeProperty("Tyre.RightFrontPressure")]
        public double RightFrontPressure { get { return GetRuntimeProperty<double> (); } set { SetRuntimeProperty(value); } }

        //USURE PNEU DROIT AV float
        [RuntimeProperty("Tyre.RightFrontDamage")]
        public int RightFrontDamage { get { return GetRuntimeProperty<int> (); } set { SetRuntimeProperty(value); } }


        //PRESSION PNEU GAUCHE AR
        [RuntimeProperty("Tyre.LeftRearPressure")]
        public double LeftRearPressure { get { return GetRuntimeProperty<double> (); } set { SetRuntimeProperty(value); } }

        //TEMPERATURE PNEU GAUCHE AR
        [RuntimeProperty("Tyre.LeftRearTemperature")]
        public double LeftRearTemperature { get { return GetRuntimeProperty<double>(); } set { SetRuntimeProperty(value); } }

        //USURE PNEU GAUCHE AR
        [RuntimeProperty("Tyre.LeftRearDamage")]
        public int LeftRearDamage { get { return GetRuntimeProperty<int> (); } set { SetRuntimeProperty(value); } }


        //TEMPERATURE PNEU DROIT AR
        [RuntimeProperty("Tyre.RightRearTemperature")]
        public double RightRearTemperature { get { return GetRuntimeProperty<double> (); } set { SetRuntimeProperty(value); } }

        //PRESSION PNEU DROIT AR
        [RuntimeProperty("Tyre.RightRearPressure")]
        public double RightRearPressure { get { return GetRuntimeProperty<double> (); } set { SetRuntimeProperty(value); } }

        //USURE PNEU DROIT AR
        [RuntimeProperty("Tyre.RightRearDamage")]
        public int RightRearDamage { get { return GetRuntimeProperty<int> (); } set { SetRuntimeProperty(value); } }


        [RuntimeProperty("Tyre.TemperatureLF")]
        public Brush TemperatureLF { get { return GetRuntimeProperty<Brush>(); } set { SetRuntimeProperty(value); } }

        [RuntimeProperty("Tyre.TemperatureRF")]
        public Brush TemperatureRF { get { return GetRuntimeProperty<Brush>(); } set { SetRuntimeProperty(value); } }

        [RuntimeProperty("Tyre.TemperatureRR")]
        public Brush TemperatureRR { get { return GetRuntimeProperty<Brush>(); } set { SetRuntimeProperty(value); } }

        [RuntimeProperty("Tyre.TemperatureLR")]
        public Brush TemperatureLR { get { return GetRuntimeProperty<Brush>(); } set { SetRuntimeProperty(value); } }

        /*[RuntimeProperty]
        public int AnotherRuntimeProperty { get { return GetRuntimeProperty<int>(); } set { SetRuntimeProperty(value); } }*/
        #endregion

        /// <summary>
        /// The file TestAddOn.ini is used as the main repository of the persistent properties
        /// </summary>
        public Model(TyresDashboardAddon addon) : base($"{AddOns.BASEPATH}\\{addon.Name}\\{addon.Name}.ini")
        {
            // Other model initialization code here
        }


    }
}
