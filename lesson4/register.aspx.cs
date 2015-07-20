using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace lesson4
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlResult.Visible = false;
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            pnlResult.Visible = true;

            // Default UserStore constructor uses the default connection string named: DefaultConnection
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var user = new IdentityUser() { UserName = txtUsername.Text };
            IdentityResult result = manager.Create(user, txtPassword.Text);

            if (result.Succeeded)
            {
                lblStatus.Text = string.Format("User {0} was created successfully!", user.UserName);
            }
            else
            {
                lblStatus.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}