﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAjaxExtension
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = DateTime.Now.ToString();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Label2.Text = DateTime.Now.ToString();
        }
    }
}