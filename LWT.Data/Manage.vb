Imports System.Text
Imports System.Data.OleDb
Imports System.Web

Public Class Manage
    Public Shared Function userLogin(ByVal UserName As String, ByVal UserPwd As String) As Boolean
        Dim strSql As StringBuilder = New StringBuilder()
        strSql.Append("select [adminUser] from admin ")
        strSql.Append("where [adminUser]='" + UserName + "'and [adminPwd]='" + UserPwd + "' ;")

        Dim reader As OleDbDataReader = OleDbHelper.ExecuteReader(strSql.ToString(), Nothing)
        While (reader.Read())
            HttpContext.Current.Session("user") = reader("adminUser")

            reader.Close()
            reader = Nothing

            Return True
        End While

        reader.Close()
        reader = Nothing

        Return False
    End Function
End Class