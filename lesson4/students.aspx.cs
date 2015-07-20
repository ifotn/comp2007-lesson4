using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//add references to use our Student & StudentContext models
using lesson4.Models;
using System.Web.ModelBinding;

using System.Linq.Dynamic;


namespace lesson4
{
    public partial class students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SortColumn"] = "StudentID";
                Session["Dir"] = "ASC";
                GetStudents();
            }
        }

        protected void GetStudents()
        {
            var conn = new StudentContext();
            String SortColumn = Session["SortColumn"].ToString() + " " + Session["Dir"].ToString();

            var Students = from s in conn.Students
                           orderby SortColumn
                           select s;

            grdStudents.DataSource = Students.AsQueryable().OrderBy(SortColumn).ToList(); //Students.ToList();
            grdStudents.DataBind();

        }

        protected void grdStudents_Sorting(object sender, GridViewSortEventArgs e)
        {
            string SortColumn = e.SortExpression;
            Session["SortColumn"] = SortColumn;

            if (String.IsNullOrEmpty(Session["Dir"].ToString())) 
            {
                Session["Dir"] = "ASC";
            }

            SortColumn += " " + Session["Dir"].ToString();

            GetStudents();

            //toggle direction
            if (Session["Dir"].ToString() == "ASC")
            {
                Session["Dir"] = "DESC";
            }
            else
            {
                Session["Dir"] = "ASC";
            }
        }

        protected void grdStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            using (StudentContext db = new StudentContext()) 
            {
                Int32 StudentID = Convert.ToInt32(grdStudents.DataKeys[e.RowIndex].Values["StudentID"]);

                var objS = (from s in db.Students
                            where s.StudentID == StudentID
                            select s).First();

                db.Students.Remove(objS);
                db.SaveChanges();

                GetStudents();
            }
        }

        protected void grdStudents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack) { 
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Image SortImage = new Image();
                
                    for (int i = 0; i <= grdStudents.Columns.Count -1; i++) {
                        if (grdStudents.Columns[i].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["Dir"].ToString() == "DESC")
                            {
                                SortImage.ImageUrl = "images/desc.jpg";
                                SortImage.AlternateText = "Sort Ascending";
                            }
                            else
                            {
                                SortImage.ImageUrl = "images/asc.jpg";
                                SortImage.AlternateText = "Sort Descending";
                            }
                        
                            e.Row.Cells[i].Controls.Add(SortImage);
                            
                        }
                    }
                }
               
            }
        }

        protected void grdStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdStudents.PageIndex = e.NewPageIndex;
            GetStudents();
        }

    }
}