<%@ Page Title="Contoso University - Registration" Language="C#" MasterPageFile="~/contoso.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="lesson4.register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Register</h1>
    <h5>All fields are required</h5>

    <asp:panel ID="pnlResult" runat="server">
        <asp:Label ID="lblStatus" CssClass="alert alert-info" runat="server" />
    </asp:panel>

    <div class="form-group">
        <label for="txtUsername" class="col-sm-2">Username:</label>
        <asp:TextBox ID="txtUsername" runat="server" />
    </div>
    <div class="form-group">
        <label for="txtPassword" class="col-sm-2">Password:</label>
        <asp:TextBox ID="txtPassword" TextMode="password" runat="server" />
    </div>
    <div class="form-group">
        <label for="txtConfirm" class="col-sm-2">Confirm Password:</label>
        <asp:TextBox ID="txtConfirm" TextMode="password" runat="server" />
    </div>
    <div class="col-sm-offset-2">
        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" CssClass="btn btn-primary" />
    </div>
</asp:Content>
