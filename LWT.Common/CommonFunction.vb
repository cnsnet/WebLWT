Imports System.Text.RegularExpressions

Public Class CommonFunction
    Public Shared Function MD5(ByVal strTohash As String) As String
        Return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strTohash, "MD5")
    End Function

    Public Shared Function isyyyyMMddDate(ByVal str As String) As Boolean
        Dim regEx As Regex = New Regex("(\d{4}-\d{1,2}-\d{1,2})|(\d{4}/\d{1,2}/\d{1,2})")

        If String.IsNullOrEmpty(str) Then
            Return False
        Else
            Return regEx.IsMatch(str)
        End If
    End Function
End Class