using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IdentityModel.Selectors;
namespace WindowsApplication2
{
    public partial class AlertCenter : Form
    {
        public AlertCenter()
        {
            InitializeComponent();
        }

        private void MessageCenter_Load(object sender, EventArgs e)
        {
          
            
            
            AlertServiceClient alertService = new AlertServiceClient();


            DataContracts.Alert[] systemAlerts = alertService.GetAlerts();

            foreach (DataContracts.Alert alert in systemAlerts)
            {
                lbAlerts.Items.Add(alert);
                lbAlerts.DisplayMember = "Title";

            }


            try
            {

                MyAlertsServiceClient myAlertService = new MyAlertsServiceClient();

                DataContracts.Alert[] myAlerts = myAlertService.GetMyAlerts();

                foreach (DataContracts.Alert alert in myAlerts)
                {
                    lbMyAlerts.Items.Add(alert);
                    lbMyAlerts.DisplayMember = "Title";

                }
            }
            catch (UserCancellationException uce)
            {
                System.Windows.Forms.MessageBox.Show("User Cancelled Out of the CardSpace UI.");

            }
            catch (UntrustedRecipientException ure)
            { }
            catch (ServiceNotStartedException snse)
            { }
            catch (CardSpaceException cse)
            { }
            catch (Exception e1)
            { }
        }




        private void lbMyAlerts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbMyAlerts.SelectedIndex > -1)
            {
                DataContracts.Alert alert = (DataContracts.Alert)lbMyAlerts.Items[lbMyAlerts.SelectedIndex];
                webAlert.DocumentText = alert.AlertHTML;
                lblTitle.Text = alert.Title;


            }
        }

        private void lbAlerts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbAlerts.SelectedIndex > -1)
            {
                DataContracts.Alert alert = (DataContracts.Alert)lbAlerts.Items[lbAlerts.SelectedIndex];
                webAlert.DocumentText = alert.AlertHTML;
                lblTitle.Text = alert.Title;


            }
        }
    }
}