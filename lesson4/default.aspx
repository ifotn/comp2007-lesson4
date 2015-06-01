<%@ Page Title="" Language="C#" MasterPageFile="~/contoso.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="lesson4._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="well">
            <h3>Department Administration</h3>
            <div class="list-group">
                <a class="list-group-item" href="departments.aspx">List Departments</a>
                <a class="list-group-item" href="department.aspx">Add Department</a>
            </div>
        </div>

        <div class="well">
            <h3>Course Administration</h3>
            <div class="list-group">
                <a class="list-group-item" href="courses.aspx">List Courses</a>
                <a class="list-group-item" href="course.aspx">Add Course</a>
            </div>
        </div>
        <div class="well">
            <h3>Student Administration</h3>
            <div class="list-group">
                <a class="list-group-item" href="students.aspx">List Students</a>
                <a class="list-group-item" href="student.aspx">Add Student</a>
            </div>
        </div>
    </div>
</asp:Content>
