﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace RealEstateWebRole.Account
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterUser.ContinueDestinationPageUrl = "~/Public/Login.aspx";
            if (!IsPostBack)
            {
                Page.Title = "Registration Form";
                HtmlMeta htmlmeta = new HtmlMeta();
                htmlmeta.Name = "Keywords";
                htmlmeta.Content = "Property in zambia,property zambia,real estate in zambia,zambian real estate,real estate agent in zambia,proprty listing site in zambia,plots for sale,most popular property site in zambia";
                Page.Header.Controls.Add(htmlmeta);

                HtmlMeta htmlmeta2 = new HtmlMeta();
                htmlmeta2.Name = "keywords2";
                htmlmeta2.Content = "Nation daily,nation news paper zambia";
                Page.Header.Controls.Add(htmlmeta2);
            }
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);

            MembershipUser user = Membership.GetUser(RegisterUser.UserName);
            user.Email = RegisterUser.UserName;
            Membership.UpdateUser(user);
            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            if (String.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }

    }
}
