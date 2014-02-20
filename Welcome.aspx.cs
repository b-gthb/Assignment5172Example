using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Welcome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["personKey"] != null)
        {
            GetCustomerInfo();
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void GetCustomerInfo()
    {
        AutomartEntities1 ae = new AutomartEntities1();
        int pk = (int)Session["personKey"];
        var customer = from c in ae.RegisteredCustomers
                       where c.PersonKey == pk
                       select new
                       {
                           c.Person.LastName,
                           c.Person.FirstName,
                           c.Email
                       };

        GridView1.DataSource = customer.ToList();
        GridView1.DataBind();
    }
}