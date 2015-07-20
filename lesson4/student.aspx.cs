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
            if (!IsPostBack) {
                if (!String.IsNullOrEmpty(Request.QueryString["StudentID"]))
                {
                    GetStudent();
                    //ShowAddButton();
                }
                else
                {
                    tblAdd.Enabled = false;
                }

                FillDropdowns();
            }
            else
            {
                if (String.IsNullOrEmpty(Request.QueryString["StudentID"])) {
                 //   ShowAddButton();
                    
                }
                
                //pnlCourses.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var conn = new ContosoEntities();

            Student objS = new Student();

            objS.FirstMidName = txtFirstName.Text;
            objS.LastName = txtLastName.Text;
            objS.EnrollmentDate = Convert.ToDateTime(txtEnrollmentDate.Text);

            conn.Students.Add(objS);
            conn.SaveChanges();

            Response.Redirect("students.aspx");
            
        }

        protected void GetStudent()
        {
            pnlCourses.Visible = true;

            Int32 sid = Convert.ToInt32(Request.QueryString["StudentID"]);

            using (ContosoEntities db = new ContosoEntities())
            {
                Student objS = (from s in db.Students                           
                                where s.StudentID == sid
                                select s).FirstOrDefault();

                txtFirstName.Text = objS.FirstMidName;
                txtLastName.Text = objS.LastName;
                txtEnrollmentDate.Text = objS.EnrollmentDate.ToShortDateString();

                //enrollments                
                var objE = (from en in db.Enrollments
                            join c in db.Courses on en.CourseID equals c.CourseID
                            join d in db.Departments on c.DepartmentID equals d.DepartmentID
                            where en.StudentID == sid
                            select new { en.EnrollmentID, en.Grade, c.Title, d.Name });
               
                grdCourses.DataSource = objE.ToList();

                //if (objE.Count() == 0)
                //{
                //    objE.
                //}
                grdCourses.DataBind();

                //if (objE.Count() > 0) {
                //    ShowAddButton();
                //}

                
            }

        }

        protected void grdCourses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 EnrollmentID = Convert.ToInt32(grdCourses.DataKeys[e.RowIndex].Values["EnrollmentID"].ToString());

            using (ContosoEntities db = new ContosoEntities())
            {
                Enrollment objE = (from en in db.Enrollments
                                   where en.EnrollmentID == EnrollmentID
                                   select en).FirstOrDefault();

                db.Enrollments.Remove(objE);
                db.SaveChanges();
                GetStudent();

            }
        }

        protected void FillDropdowns()
        {
            //fill dropdowns
            using (ContosoEntities db = new ContosoEntities())
            {
                //departments
                var deps = (from d in db.Departments
                            orderby d.Name
                            select d);

                //DropDownList ddl1 = (DropDownList)e.Row.FindControl("ddlDepartment");
                //ddl1.DataSource = deps.ToList();
                //ddl1.DataBind();
                ListItem itm = new ListItem("-Select-", "-1");
                //ddl1.Items.Insert(0, itm);

                //DropDownList ddl2 = (DropDownList)e.Row.FindControl("ddlCourse");
                //ddl2.Items.Insert(0, itm);

                ddlDepartment.DataSource = deps.ToList();
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, itm);

                ddlCourse.Items.Insert(0, itm);
            }
        }
        protected void grdCourses_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    //fill dropdowns
            //    using (ContosoEntities db = new ContosoEntities()) { 
            //        //departments
            //        var deps = (from d in db.Departments
            //                    orderby d.Name
            //                    select d);

            //        //DropDownList ddl1 = (DropDownList)e.Row.FindControl("ddlDepartment");
            //        //ddl1.DataSource = deps.ToList();
            //        //ddl1.DataBind();
            //        ListItem itm = new ListItem("-Select-", "-1");
            //        //ddl1.Items.Insert(0, itm);

            //        //DropDownList ddl2 = (DropDownList)e.Row.FindControl("ddlCourse");
            //        //ddl2.Items.Insert(0, itm);

            //        ddlDepartment.DataSource = deps.ToList();
            //        ddlDepartment.DataBind();
            //        ddlDepartment.Items.Insert(0, itm);

            //        ddlCourse.Items.Insert(0, itm);


            //    }

            //    ////generate Add button                
            //    //Button btn = new Button();
            //    //btn.CssClass = "btn btn-primary";
            //    //btn.Text = "Add";
            //    //btn.ID = "btnAdd";
            //    //btn.CommandName = "New";
            //    ////btn.Click = (Enroll);
            //    //e.Row.Cells[3].Controls.Add(btn);
            //}
        }

        //protected void Enroll()
        //{

        //}

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (ContosoEntities db = new ContosoEntities())
            {
                Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"].ToString());

                Enrollment objE = new Enrollment();
                objE.StudentID = StudentID;
                objE.CourseID = Convert.ToInt32(ddlCourse.SelectedValue);

                db.Enrollments.Add(objE);
                db.SaveChanges();
                GetStudent();

                ddlDepartment.ClearSelection();
                ddlCourse.ClearSelection();
            }
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (ContosoEntities db = new ContosoEntities())
                {
                    DropDownList ddl1 = (DropDownList)sender; // (DropDownList)grdCourses.FindControl("ddlDepartment");
                    Int32 DepartmentID = Convert.ToInt32(ddl1.SelectedValue);

                    //courses
                    var courses = (from c in db.Courses
                                       where c.DepartmentID == DepartmentID
                                       orderby c.Title
                                        select c);

                    //DropDownList ddl2 = (DropDownList)(grdCourses.FooterRow.FindControl("ddlCourse"));

                    //ddl2.DataSource = courses.ToList();
                    //ddl2.DataBind();

                    ddlCourse.DataSource = courses.ToList();
                    ddlCourse.DataBind();
                    ListItem itm = new ListItem("-Select-", "-1");
                    ddlCourse.Items.Insert(0, itm);
                }
        }

        protected void ShowAddButton()
        {
            //generate Add button                
            Button btn = new Button();
            btn.CssClass = "btn btn-primary";
            btn.Text = "Add";
            btn.ID = "btnAdd";
            btn.CommandName = "New";
            btn.Click += new EventHandler(btnAdd_Click);
            grdCourses.FooterRow.Cells[3].Controls.Add(btn);
        }

        protected void lnkDelete_Command(object sender, CommandEventArgs e)
        {

        }
        
        protected void grdCourses_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "New")
            {
                using (ContosoEntities db = new ContosoEntities())
                {
                    Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"].ToString());

                    Enrollment objE = new Enrollment();
                    objE.StudentID = StudentID;

                    DropDownList ddl2 = (DropDownList)(grdCourses.FooterRow.FindControl("ddlCourse"));
                    objE.CourseID = Convert.ToInt32(ddl2.SelectedValue);

                    db.Enrollments.Add(objE);
                    db.SaveChanges();
                    GetStudent();
                }
            }
            else if (e.CommandName == "Delete")
            {
                //Int32 EnrollmentID = Convert.ToInt32(grdCourses.DataKeys[e.RowIndex].Values["EnrollmentID"].ToString());

                //using (ContosoEntities db = new ContosoEntities())
                //{
                //    Enrollment objE = (from en in db.Enrollments
                //                       where en.EnrollmentID == EnrollmentID
                //                       select en).FirstOrDefault();

                //    db.Enrollments.Remove(objE);
                //    db.SaveChanges();
                //    GetStudent();
                //}
            }
        }
    }
}