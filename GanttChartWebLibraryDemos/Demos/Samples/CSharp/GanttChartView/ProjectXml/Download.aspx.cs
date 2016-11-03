using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DlhSoft.Web.UI.WebControls;
using System.Drawing;
using DlhSoft.Windows.Data;

namespace Demos.Samples.CSharp.GanttChartView.ProjectXml
{
    public partial class Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Response.ContentType = "text/xml";
                    Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", Request.QueryString["Filename"]));
                    Response.Write(Session["DownloadContent"]);
                    Response.End();
                }
                catch { }

                Session.Remove("DownloadContent");
            }
        }
   }
}