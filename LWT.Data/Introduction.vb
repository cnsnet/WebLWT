Imports System.Text
Imports System.Data.OleDb
Imports System.Web

Public Class Introduction

#Region "Model"
    Private _id As Integer = 0
    Private _title As String = String.Empty
    Private _content As String = String.Empty
    Private _createdate As DateTime = DateTime.Now()
    Private _modifydate As DateTime = DateTime.Now()

    Public Property id As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property

    Public Property title As String
        Get
            Return _title
        End Get
        Set(value As String)
            _title = value
        End Set
    End Property

    Public Property content As String
        Get
            Return _content
        End Get
        Set(value As String)
            _content = value
        End Set
    End Property

    Public Property createdate As DateTime
        Get
            Return _createdate
        End Get
        Set(value As DateTime)
            _createdate = value
        End Set
    End Property

    Public Property modifydate As DateTime
        Get
            Return _modifydate
        End Get
        Set(value As DateTime)
            _modifydate = value
        End Set
    End Property
#End Region

    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer)
        Dim strSql As New StringBuilder()
        strSql.Append("select [id],[title],[content],[createdate],[modifydate] ")
        strSql.Append("FROM [Introduction] ")
        strSql.Append("where [id]=@id ")
        Dim parameters As OleDbParameter() = {New OleDbParameter("@id", OleDbType.Integer, 4)}
        parameters(0).Value = id

        Dim dt As DataTable = OleDbHelper.ExecuteTable(strSql.ToString(), parameters)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            If dr("id") IsNot Nothing AndAlso dr("id").ToString() <> "" Then
                Me.id = Integer.Parse(dr("id").ToString())
            End If
            If dr("title") IsNot Nothing AndAlso dr("title").ToString() <> "" Then
                Me.title = dr("title").ToString()
            End If
            If dr("content") IsNot Nothing AndAlso dr("content").ToString() <> "" Then
                Me.content = dr("content").ToString()
            End If
            If dr("createdate") IsNot Nothing AndAlso dr("createdate").ToString() <> "" Then
                Me.createdate = DateTime.Parse(dr("createdate").ToString())
            End If
            If dr("modifydate") IsNot Nothing AndAlso dr("modifydate").ToString() <> "" Then
                Me.modifydate = DateTime.Parse(dr("modifydate").ToString())
            End If
        End If
    End Sub

    Public Sub getModel(ByVal id As String)
        Dim strSql As New StringBuilder()
        strSql.Append("select [id],[title],[content],[createdate],[modifydate] ")
        strSql.Append("FROM [Introduction] ")
        strSql.Append("where [id]=@id ")
        Dim parameters As OleDbParameter() = {New OleDbParameter("@id", OleDbType.Integer, 4)}
        parameters(0).Value = id

        Dim dt As DataTable = OleDbHelper.ExecuteTable(strSql.ToString(), parameters)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            If dr("id") IsNot Nothing AndAlso dr("id").ToString() <> "" Then
                Me.id = Integer.Parse(dr("id").ToString())
            End If
            If dr("title") IsNot Nothing AndAlso dr("title").ToString() <> "" Then
                Me.title = dr("title").ToString()
            End If
            If dr("content") IsNot Nothing AndAlso dr("content").ToString() <> "" Then
                Me.content = dr("content").ToString()
            End If
            If dr("createdate") IsNot Nothing AndAlso dr("createdate").ToString() <> "" Then
                Me.createdate = DateTime.Parse(dr("createdate").ToString())
            End If
            If dr("modifydate") IsNot Nothing AndAlso dr("modifydate").ToString() <> "" Then
                Me.modifydate = DateTime.Parse(dr("modifydate").ToString())
            End If
        End If
    End Sub

    Public Shared Function Update(ByVal content As String) As Boolean
        Dim strSql As StringBuilder = New StringBuilder()
        strSql.Append("update [Introduction] set ")
        strSql.Append("[content]=@content,")
        strSql.Append("[modifydate]=@modifydate ")
        strSql.Append("where id=@id ")

        Dim parameters As OleDbParameter() = {
            New OleDbParameter("@content", OleDbType.VarChar),
            New OleDbParameter("@modifydate", OleDbType.Date),
            New OleDbParameter("@id", OleDbType.Integer, 4)
        }

        parameters(0).Value = content
        parameters(1).Value = DateTime.Now()
        parameters(2).Value = 1
        Dim rows As Integer = OleDbHelper.ExecuteNonQuery(strSql.ToString(), parameters)

        If rows > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
End Class