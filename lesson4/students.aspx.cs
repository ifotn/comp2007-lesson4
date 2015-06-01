using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//add references to use our Student & StudentContext models
using lesson4.Models;
using System.Web.ModelBinding;

namespace lesson4
{
    public partial class students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetStudents();
            }
        }

        protected void GetStudents()
        {
            var conn = new StudentContext();

            var Students = from s in conn.Students
                           select s;

            grdStudents.DataSource = Students.ToList();
            grdStudents.DataBind();

        }

        protected void grdStudents_Sorting(object sender, GridViewSortEventArgs e)
        {
            string SortColumn = e.SortExpression;

            var conn = new StudentContext();

            var Students = from s in conn.Students
                           orderby SortColumn
                           select s;

            grdStudents.DataSource = Students.ToList();
            grdStudents.DataBind();


        }
    }
}