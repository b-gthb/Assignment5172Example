using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int[] yearlist = new int[25];
            int yr = 1990;
            for (int i = 0; i < yearlist.Length; i++)
            {

                yearlist[i] = yr;

                yr++;

            }
            ddYears.DataSource = yearlist;
            ddYears.DataBind();
       }

            
        

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            AutomartEntities1 ae = new AutomartEntities1();
            Person p = new Person();
            p.FirstName = txtFirstName.Text;
            p.LastName = txtLastName.Text;
            ae.People.Add(p);

            vehicle v = new vehicle();
            v.LicenseNumber = txtLicense.Text;
            v.VehicleMake = txtMake.Text;
            v.VehicleYear = ddYears.SelectedItem.ToString();
            v.Person = p;
            ae.vehicles.Add(v);

            PassCodeGenerator pg = new PassCodeGenerator();
            int passcode = pg.GetPasscode();
            PasswordHash ph = new PasswordHash();

            RegisteredCustomer rc = new RegisteredCustomer();
            rc.Person = p;
            rc.Email = txtEmail.Text;
            rc.CustomerPassCode = passcode;
            rc.CustomerPassword = txtConfirm.Text;
            rc.CustomerHashedPassword = ph.Hashit(txtConfirm.Text, passcode.ToString());
            ae.RegisteredCustomers.Add(rc);

            ae.SaveChanges();

            Response.Redirect("Welcome.aspx");
        }
        catch (Exception ex)
        {
            lblResult.Text = ex.Message;
        }


    }
}