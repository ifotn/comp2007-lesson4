using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using lesson4.Models;
using System.Web.ModelBinding;

namespace lesson4
{
    public partial class course : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDepartments();

                if (!String.IsNullOrEmpty(Request.QueryString["CourseID"]))
                {
                    GetCourse();
                }
            }
        }

        protected void GetDepartments()
        {
            using (ContosoEntities db = new ContosoEntities())
            {
                var deps = (from d in db.Departments
                                    orderby d.Name
                                    select d);

                ddlDepartment.DataSource = deps.ToList();
                ddlDepartment.DataBind();
            }
        }

        protected void GetCourse()
        {
            using (ContosoEntities db = new ContosoEntities())
            {
                Int32 CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);

                Course objC = (from c in db.Courses
                               where c.CourseID == CourseID
                               select c).FirstOrDefault();

                txtTitle.Text = objC.Title;
                txtCredits.Text = objC.Credits.ToString();
                ddlDepartment.Items.FindByValue(objC.DepartmentID.ToString()).Selected = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (ContosoEntities db = new ContosoEntities())
            {
                Course objC = new Course();

                if (!String.IsNullOrEmpty(Request.QueryString["CourseID"]))
                {
                    Int32 CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);
                    objC = (from c in db.Courses
                            where c.CourseID == CourseID
                            select c).FirstOrDefault();
                }

                objC.Title = txtTitle.Text;
                objC.Credits = Convert.ToInt32(txtCredits.Text);
                objC.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);

                if (String.IsNullOrEmpty(Request.QueryString["CourseID"]))
                {
                    db.Courses.Add(objC);
                }

                db.SaveChanges();
                Response.Redirect("courses.aspx");

            }
        }
    }
}