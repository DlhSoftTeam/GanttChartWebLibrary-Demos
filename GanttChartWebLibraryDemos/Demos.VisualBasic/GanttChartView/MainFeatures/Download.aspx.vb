Namespace Demos.Samples.VisualBasic.GanttChartView.MainFeatures

    Public Class Download
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                Try
                    Response.ContentType = Request.QueryString("Filename")
                    Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", Request.QueryString("Filename")))
                    Response.Write(Session("DownloadContent"))
                    Response.End()
                Catch
                End Try

                Session.Remove("DownloadContent")
            End If
        End Sub

    End Class

End Namespace
