<%@ Page Title="Contoso University - Courses" Language="C#" MasterPageFile="~/contoso.Master" AutoEventWireup="true" CodeBehind="courses.aspx.cs" Inherits="lesson4.courses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Courses</h1>
    <a href="course.aspx">Add Course</a>
    <asp:GridView ID="grdCourses" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover sort display" DataKeyNames="CourseID" OnSorting="grdCourses_Sorting"
         AllowSorting="true" AllowPaging="true" PageSize="3" OnPageIndexChanging="grdCourses_PageIndexChanging" OnRowCreated="grdCourses_RowCreated" OnRowDeleting="grdCourses_RowDeleting">
        <Columns>
            <asp:BoundField DataField="CourseID" HeaderText="Course ID" SortExpression="CourseID" />
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
            <asp:BoundField DataField="Credits" HeaderText="Credits" SortExpression="Credits" />
            <asp:BoundField DataField="Name" HeaderText="Department" SortExpression="Department" />
            <asp:HyperLinkField HeaderText="Edit" Text="Edit" NavigateUrl="~/course.aspx" DataNavigateUrlFields="CourseID" DataNavigateUrlFormatString="course.aspx?CourseID={0}" />
            <asp:CommandField DeleteText="Delete" HeaderText="Delete" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>

</asp:Content>
