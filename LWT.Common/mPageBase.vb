Imports System.Web

Public Class mPageBase
    Inherits System.Web.UI.Page

    Public Sub New()
        AddHandler (Me.Load), AddressOf mPageBase_load
    End Sub

    Private Sub mPageBase_load(ByVal sender As Object, ByVal e As System.EventArgs)
        checkLoginState()
    End Sub

    Private Sub checkLoginState()
        If HttpContext.Current.Session("user") Is Nothing Then
            Response.Redirect("login.aspx")
        End If
    End Sub
End Class