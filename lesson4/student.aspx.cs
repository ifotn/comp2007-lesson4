using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using lesson4.Models;

namespace lesson4
{
    public partial class student : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var conn = new StudentContext();

            Student objS = new Student();

            objS.FirstMidName = txtFirstName.Text;
            objS.LastName = txtLastName.Text;
            objS.EnrollmentDate = Convert.ToDateTime(txtEnrollmentDate.Text);

            conn.Students.Add(objS);
            conn.SaveChanges();

            Response.Redirect("students.aspx");
            
        }
    }
}