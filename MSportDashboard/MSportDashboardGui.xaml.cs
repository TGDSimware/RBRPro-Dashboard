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
    public partial class MSportDashboardGui : UserControl
    {
        IRbrPro _rbrPro = null;
        Model _model = null;
        MSportDashboardAddon _addon = null;

        public MSportDashboardGui(IRbrProAddOn addon, IRbrPro interactor, Model model)
        {
            _addon = (MSportDashboardAddon) addon;
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

        private void Button_Trigger_MSportDashboard(object sender, RoutedEventArgs e)
        {
            MSportDashboard myOverlay = new MSportDashboard(_model, null);
            myOverlay.Show();
            //_rbrPro?.StartGame(false, false, false); // this is the action
        }
    }
}
