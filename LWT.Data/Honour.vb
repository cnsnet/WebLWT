Imports System.Text
Imports System.Data.OleDb

Public Class Honour

#Region "Model"
    Private _id As Integer = 0
    Private _prizename As String = String.Empty
    Private _image As String = String.Empty
    Private _getdate As DateTime = DateTime.Now()
    Private _detail As String = String.Empty

    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property

    Public Property prizename() As String
        Get
            Return _prizename
        End Get
        Set(value As String)
            _prizename = value
        End Set
    End Property

    Public Property image() As String
        Get
            Return _image
        End Get
        Set(value As String)
            _image = value
        End Set
    End Property

    Public Property getdate() As DateTime
        Get
            Return _getdate
        End Get
        Set(value As DateTime)
            _getdate = value
        End Set
    End Property

    Public Property detail() As String
        Get
            Return _detail
        End Get
        Set(value As String)
            _detail = value
        End Set
    End Property
#End Region

    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer)
        Dim strSql As New StringBuilder()
        strSql.Append("select [ID],[prizename],[image],[getdate],[detail] ")
        strSql.Append("FROM [honours] ")
        strSql.Append("where [ID]=@ID ")
        Dim parameters As OleDbParameter() = {New OleDbParameter("@ID", OleDbType.[Integer], 4)}
        parameters(0).Value = id

        Dim dt As DataTable = OleDbHelper.ExecuteTable(strSql.ToString(), parameters)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            If dr("ID") <> Nothing AndAlso dr("ID").ToString() <> "" Then
                Me.ID = Integer.Parse(dr("ID").ToString())
            End If
            If dr("prizename") <> Nothing AndAlso dr("prizename").ToString() <> "" Then
                Me.prizename = dr("prizename").ToString()
            End If
            If dr("image") <> Nothing AndAlso dr("image").ToString() <> "" Then
                Me.image = dr("image").ToString()
            End If
            If dr("getdate") <> Nothing AndAlso dr("getdate").ToString() <> "" Then
                Me.getdate = DateTime.Parse(dr("getdate").ToString())
            End If
            If dr("detail") <> Nothing AndAlso dr("detail").ToString() <> "" Then
                Me.detail = dr("detail").ToString()
            End If
        End If
    End Sub

    Public Shared Function AddNew(ByVal prizename As String, ByVal imagepath As String, ByVal getdate As DateTime, ByVal detail As String) As Boolean
        Dim strSql As New StringBuilder()
        strSql.Append("insert into honours (")
        strSql.Append("[prizename],[image],[getdate],[detail])")
        strSql.Append(" values (")
        strSql.Append("@prizename,@image,@getdate,@detail)")
        Dim parameters As OleDbParameter() = {
            New OleDbParameter("@prizename", OleDbType.VarChar, 255),
            New OleDbParameter("@image", OleDbType.VarChar, 255),
            New OleDbParameter("@getdate", OleDbType.Date),
            New OleDbParameter("@detail", OleDbType.VarChar, 255)
        }
        parameters(0).Value = prizename
        parameters(1).Value = imagepath
        parameters(2).Value = getdate
        parameters(3).Value = detail

        Dim result As Integer = OleDbHelper.ExecuteNonQuery(strSql.ToString(), parameters)

        If result > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function Update(ByVal id As Integer, ByVal prizename As String, ByVal imagepath As String, ByVal getdate As Date, ByVal detail As String) As Boolean
        Dim strSql As New StringBuilder()
        strSql.Append("update [honours] set ")
        strSql.Append("[prizename]=@prizename,")
        strSql.Append("[image]=@image,")
        strSql.Append("[getdate]=@getdate,")
        strSql.Append("[detail]=@detail")
        strSql.Append(" where [ID]=@ID ")
        Dim parameters As OleDbParameter() = {New OleDbParameter("@prizename", OleDbType.VarChar, 255), New OleDbParameter("@image", OleDbType.VarChar, 255), New OleDbParameter("@getdate", OleDbType.[Date]), New OleDbParameter("@detail", OleDbType.VarChar, 255), New OleDbParameter("@ID", OleDbType.[Integer], 4)}
        parameters(0).Value = prizename
        parameters(1).Value = imagepath
        parameters(2).Value = getdate
        parameters(3).Value = detail
        parameters(4).Value = id

        Dim rows As Integer = OleDbHelper.ExecuteNonQuery(strSql.ToString(), parameters)
        If rows > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function delete(ByVal id As String) As Boolean
        Dim strSql As New StringBuilder()
        strSql.Append("delete from [honours] ")
        strSql.Append(" where ID=@ID ")
        Dim parameters As OleDbParameter() = {New OleDbParameter("@ID", OleDbType.[Integer], 4)}
        parameters(0).Value = id

        Dim rows As Integer = OleDbHelper.ExecuteNonQuery(strSql.ToString(), parameters)
        If rows > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetListAll(ByVal top As Integer, ByVal where As String, ByVal orderby As String) As DataTable
        Dim strSql As New StringBuilder()
        strSql.Append("select ")

        If top > 0 Then
            strSql.Append("top " & top)
        End If

        strSql.Append(" [ID],[prizename],[image],[getdate],[detail] FROM [honours]")

        If Not String.IsNullOrEmpty(where) Then
            strSql.Append(" where " & where)
        End If

        If Not String.IsNullOrEmpty(orderby) Then
            strSql.Append(" order by " & orderby)
        End If

        Return OleDbHelper.ExecuteTable(strSql.ToString())
    End Function

    Public Sub GetModel(ByVal ID As Integer)
        Dim strSql As New StringBuilder()
        strSql.Append("select [ID],[prizename],[image],[getdate],[detail]")
        strSql.Append("FROM [honours] ")
        strSql.Append("where [ID]=@ID ")
        Dim parameters As OleDbParameter() = {New OleDbParameter("@ID", OleDbType.[Integer], 4)}
        parameters(0).Value = ID

        Dim dt As DataTable = OleDbHelper.ExecuteTable(strSql.ToString(), parameters)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            If dr("ID") <> Nothing AndAlso dr("ID").ToString() <> "" Then
                Me.ID = Integer.Parse(dr("ID").ToString())
            End If
            If dr("prizename") <> Nothing AndAlso dr("prizename").ToString() <> "" Then
                Me.prizename = dr("prizename").ToString()
            End If
            If dr("image") <> Nothing AndAlso dr("image").ToString() <> "" Then
                Me.image = dr("image").ToString()
            End If
            If dr("getdate") <> Nothing AndAlso dr("getdate").ToString() <> "" Then
                Me.getdate = DateTime.Parse(dr("getdate").ToString())
            End If
            If dr("detail") <> Nothing AndAlso dr("detail").ToString() <> "" Then
                Me.detail = dr("detail").ToString()
            End If
        End If
    End Sub
End Class