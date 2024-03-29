﻿using RBRPro.Api;
using RBRProTestAddOn;
using System.Windows;
using System.Windows.Controls;
using TGD.Utils;

namespace RBRProDashboard
{
    /// <summary>
    /// Logica di interazione per TestAddonGui.xaml
    /// </summary>
    public partial class WRCDashboardGui : UserControl
    {
        IRbrPro _rbrPro = null;
        Model _model = null;
        WRCDashboardAddon _addon = null;

        public WRCDashboardGui(IRbrProAddOn addon, IRbrPro interactor, Model model)
        {
            _addon = (WRCDashboardAddon) addon;
            this.DataContext = _model = model;
            _rbrPro = interactor;

            InitializeComponent();
            
            _rbrPro.SelectedLanguageChanged += _rbrPro_SelectedLanguageChanged;
            _rbrPro.ActiveCoDriverChanged += _rbrPro_ActiveCoDriverChanged1;
        }

        private void _rbrPro_ActiveCoDriverChanged1(object sender, RbrPro.API.ICoDriver e)
        {
            MsgBox.Info($"The active codriver changed: the new one is {e.Name}");
        }

      
        private void _rbrPro_SelectedLanguageChanged(object sender, string newLanguage)
        {
            //If you want to generate a translation iniFile uncomment the following line
            //Local.GenerateToFile($"{AddOns.BASEPATH}\\{_addon.Name}\\languages\\{language}.ini");

            // Translates the GUI according to the language selected in the manager
            Local.Load($"{AddOns.BASEPATH}\\{_addon.Name}\\languages\\{newLanguage}.ini");
            Local.Translate(this);
        }

        private void Button_Trigger_WRCDashboard(object sender, RoutedEventArgs e)
        {
            WRCDashboard wrcDashboard = new WRCDashboard(_model, null);
            wrcDashboard.Show();
        }
    }
}
