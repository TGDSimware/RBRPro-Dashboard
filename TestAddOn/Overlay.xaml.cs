using MahApps.Metro.Controls;
using System.Windows;
using TGD.Rbr.Telemetry.Data;

namespace RBRProTestAddOn
{
    /// <summary>
    /// Logica di interazione per Navigator.xaml
    /// </summary>    
    public partial class Overlay : MetroWindow
    {
        Model _model;

        public Overlay(Model model, Window owner = null)
        {
            if (owner != null) {
                this.Owner = owner;
            }           
            if (model != null)
            {
                this.DataContext = _model = model;                
            }

            this.Top = _model.OverlayTop;
            this.Left = _model.OverlayLeft;

            InitializeComponent();
        }

        private void MetroWindow_LocationChanged(object sender, System.EventArgs e)
        {
            _model.OverlayTop = this.Top;
            _model.OverlayLeft = this.Left;
        }

        private void MetroWindow_Deactivated(object sender, System.EventArgs e)
        {
            ShowTitleBar = false;
            ShowCloseButton = false;
        }

        private void MetroWindow_Activated(object sender, System.EventArgs e)
        {
            ShowTitleBar = true;
            ShowCloseButton = true;
        }
    }
}
