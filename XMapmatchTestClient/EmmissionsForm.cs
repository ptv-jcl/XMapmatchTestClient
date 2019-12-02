using System.Windows.Forms;

using XServer;

namespace XMapmatchTestClient
{
    public partial class EmmissionsForm : Form
    {
        public EmmissionsForm(ExtendedRoute extendedRoute)
        {
            InitializeComponent();
            ammoniaLbl.Text = extendedRoute.route.emissions.ammonia.ToString("F3") + " g";
            benzeneLbl.Text = extendedRoute.route.emissions.benzene.ToString("F3") + " g";
            carbonDioxideLbl.Text = extendedRoute.route.emissions.carbonDioxide.ToString("F3") + " kg";
            carbonMonoxideLbl.Text = extendedRoute.route.emissions.carbonMonoxide.ToString("F3") + " g";
            fuelLbl.Text = extendedRoute.route.emissions.fuel.ToString("F3") + " kg";
            hydrocarbonsLbl.Text = extendedRoute.route.emissions.hydrocarbons.ToString("F3") + " g";
            hydrocarbonsExMethaneLbl.Text = extendedRoute.route.emissions.hydrocarbonsExMethane.ToString("F3") + " g";
            leadLbl.Text = extendedRoute.route.emissions.lead.ToString("F3") + " g";
            methaneLbl.Text = extendedRoute.route.emissions.methane.ToString("F3") + " g";
            nitrogenOxidesLbl.Text = extendedRoute.route.emissions.nitrogenOxides.ToString("F3") + " g";
            nitrousOxideLbl.Text = extendedRoute.route.emissions.nitrousOxide.ToString("F3") + " g";
            particlesLbl.Text = extendedRoute.route.emissions.particles.ToString("F3") + " g";
            sulphurDioxideLbl.Text = extendedRoute.route.emissions.sulphurDioxide.ToString("F3") + " g";
        }
    }
}