Imports System.Text
Imports System.Data.OleDb

Public Class Design

#Region "Model"
    Private _id As Integer = 0
    Private _name As String = String.Empty
    Private _viewcount As Integer = 0
    Private _imgurl As String = String.Empty
    Private _description As String = String.Empty
    Private _createdate As DateTime = DateTime.Now()
    Private _modifydate As DateTime = DateTime.Now()
    Private _type As Integer = 1
    Private _author As String = "潍坊乐维特建筑技术有限公司"
    Private _designdate As DateTime = DateTime.Now()

    Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property
    ''' <summary>
    ''' 设计名称
    ''' </summary>
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property
    ''' <summary>
    ''' 浏览次数
    ''' </summary>
    Public Property viewcount() As Integer
        Get
            Return _viewcount
        End Get
        Set(value As Integer)
            _viewcount = value
        End Set
    End Property
    ''' <summary>
    ''' 图片地址
    ''' </summary>
    Public Property ImgUrl() As String
        Get
            Return _imgurl
        End Get
        Set(value As String)
            _imgurl = value
        End Set
    End Property
    ''' <summary>
    ''' 设计描述
    ''' </summary>
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(value As String)
            _description = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    Public Property CreateDate() As DateTime
        Get
            Return _createdate
        End Get
        Set(value As DateTime)
            _createdate = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    Public Property ModifyDate() As DateTime
        Get
            Return _modifydate
        End Get
        Set(value As DateTime)
            _modifydate = value
        End Set
    End Property
    ''' <summary>
    ''' 所属类别
    ''' </summary>
    Public Property type() As Integer
        Get
            Return _type
        End Get
        Set(value As Integer)
            _type = value
        End Set
    End Property
    ''' <summary>
    ''' 设计作者
    ''' </summary>
    Public Property author() As String
        Get
            Return _author
        End Get
        Set(value As String)
            _author = value
        End Set
    End Property
    ''' <summary>
    ''' 设计日期
    ''' </summary>
    Public Property designdate() As DateTime
        Get
            Return _designdate
        End Get
        Set(value As DateTime)
            _designdate = value
        End Set
    End Property
#End Region

    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer)
        Dim strSql As New StringBuilder()
        strSql.Append("select [Id],[Name],[viewcount],[ImgUrl],[Description],[CreateDate],[ModifyDate],[type],[author],[designdate] ")
        strSql.Append(" FROM [design] ")
        strSql.Append(" where Id=@Id ")
        Dim parameters As OleDbParameter() = {New OleDbParameter("@Id", OleDbType.[Integer], 4)}
        parameters(0).Value = id

        Dim dt As DataTable = OleDbHelper.ExecuteTable(strSql.ToString(), parameters)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            If dr("Id") IsNot Nothing AndAlso dr("Id").ToString() <> "" Then
                Me.Id = Integer.Parse(dr("Id").ToString())
            End If
            If dr("Name") IsNot Nothing AndAlso dr("Name").ToString() <> "" Then
                Me.Name = dr("Name").ToString()
            End If
            If dr("viewcount") IsNot Nothing AndAlso dr("viewcount").ToString() <> "" Then
                Me.viewcount = Integer.Parse(dr("viewcount").ToString())
            End If
            If dr("ImgUrl") IsNot Nothing AndAlso dr("ImgUrl").ToString() <> "" Then
                Me.ImgUrl = dr("ImgUrl").ToString()
            End If
            If dr("Description") IsNot Nothing AndAlso dr("Description").ToString() <> "" Then
                Me.Description = dr("Description").ToString()
            End If
            If dr("CreateDate") IsNot Nothing AndAlso dr("CreateDate").ToString() <> "" Then
                Me.CreateDate = DateTime.Parse(dr("CreateDate").ToString())
            End If
            If dr("ModifyDate") IsNot Nothing AndAlso dr("ModifyDate").ToString() <> "" Then
                Me.ModifyDate = DateTime.Parse(dr("ModifyDate").ToString())
            End If
            If dr("type") IsNot Nothing AndAlso dr("type").ToString() <> "" Then
                Me.type = Integer.Parse(dr("type").ToString())
            End If
            If dr("author") IsNot Nothing AndAlso dr("author").ToString() <> "" Then
                Me.author = dr("author").ToString()
            End If
            If dr("designdate") IsNot Nothing AndAlso dr("designdate").ToString() <> "" Then
                Me.designdate = DateTime.Parse(dr("designdate").ToString())
            End If
        End If
    End Sub

    Public Shared Function AddNew(ByVal name As String, ByVal ImgUrl As String, ByVal Description As String, ByVal createDate As DateTime, ByVal type As Integer, ByVal author As String, ByVal designdate As String) As Boolean
        Dim strSql As New StringBuilder()
        strSql.Append("insert into [design] (")
        strSql.Append("[Name],[ImgUrl],[Description],[CreateDate],[type],[author],[designdate])")
        strSql.Append(" values (")
        strSql.Append("@Name,@ImgUrl,@Description,@CreateDate,@type,@author,@designdate)")

        Dim parameters As OleDbParameter() = {
            New OleDbParameter("@Name", OleDbType.VarChar, 50),
            New OleDbParameter("@ImgUrl", OleDbType.VarChar, 50),
            New OleDbParameter("@Description", OleDbType.VarChar, 0),
            New OleDbParameter("@CreateDate", OleDbType.[Date]),
            New OleDbParameter("@type", OleDbType.[Integer], 4),
            New OleDbParameter("@author", OleDbType.VarChar, 255),
            New OleDbParameter("@designdate", OleDbType.[Date])}
        parameters(0).Value = name
        parameters(1).Value = ImgUrl
        parameters(2).Value = Description
        parameters(3).Value = createDate
        parameters(4).Value = type
        parameters(5).Value = If(String.IsNullOrEmpty(author), "潍坊乐维特建筑技术有限公司", author)
        parameters(6).Value = designdate

        Dim Rows As Integer = OleDbHelper.ExecuteNonQuery(strSql.ToString(), parameters)

        If Rows > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function Delete(ByVal id As Integer) As Boolean
        Dim strSql As New StringBuilder()
        strSql.Append("delete from [design] ")
        strSql.Append(" where [Id]=@Id ")
        Dim parameters As OleDbParameter() = {
            New OleDbParameter("@Id", OleDbType.Integer, 4)
        }
        parameters(0).Value = id

        Dim rows As Integer = OleDbHelper.ExecuteNonQuery(strSql.ToString(), parameters)

        If rows > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function Update(ByVal id As Integer, ByVal name As String, ByVal ImgUrl As String, ByVal Description As String, ByVal modifyDate As DateTime, ByVal type As Integer, ByVal author As String, ByVal designdate As String) As Boolean
        Dim strSql As New StringBuilder()
        strSql.Append("update [design] set ")
        strSql.Append("[Name]=@Name,")
        strSql.Append("[ImgUrl]=@ImgUrl,")
        strSql.Append("[Description]=@Description,")
        strSql.Append("[ModifyDate]=@ModifyDate,")
        strSql.Append("[type]=@type,")
        strSql.Append("[author]=@author,")
        strSql.Append("[designdate]=@designdate")
        strSql.Append(" where [Id]=@Id ")
        Dim parameters As OleDbParameter() = {
            New OleDbParameter("@Name", OleDbType.VarChar, 50),
            New OleDbParameter("@ImgUrl", OleDbType.VarChar, 50),
            New OleDbParameter("@Description", OleDbType.VarChar, 0),
            New OleDbParameter("@ModifyDate", OleDbType.[Date]),
            New OleDbParameter("@type", OleDbType.[Integer], 4),
            New OleDbParameter("@author", OleDbType.VarChar, 255),
            New OleDbParameter("@designdate", OleDbType.[Date]),
            New OleDbParameter("@Id", OleDbType.[Integer], 4)}
        parameters(0).Value = name
        parameters(1).Value = ImgUrl
        parameters(2).Value = Description
        parameters(3).Value = modifyDate
        parameters(4).Value = type
        parameters(5).Value = If(String.IsNullOrEmpty(author), "潍坊乐维特建筑技术有限公司", author)
        parameters(6).Value = designdate
        parameters(7).Value = id

        Dim rows As Integer = OleDbHelper.ExecuteNonQuery(strSql.ToString(), parameters)
        If rows > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub UpdateViewCount(ByVal id As Integer)
        Dim strSql As New StringBuilder()
        strSql.Append("update [design] set ")
        strSql.Append("[viewCount]=[viewCount] + 1")
        strSql.Append(" where Id=@Id ")

        Dim parameters As OleDbParameter() = {
            New OleDbParameter("@Id", OleDbType.Integer, 4)
        }
        parameters(0).Value = id

        OleDbHelper.ExecuteNonQuery(strSql.ToString(), parameters)
    End Sub

    Public Shared Function GetListAll(ByVal top As Integer, ByVal where As String, ByVal orderby As String) As DataTable
        Dim strSql As New StringBuilder()
        strSql.Append("select ")

        If top > 0 Then
            strSql.Append("top " & top)
        End If

        strSql.Append(" [id],[Name],[ImgUrl],[viewcount],[Description],[CreateDate],[ModifyDate],[type],[author],[designdate] FROM [design]")

        If Not String.IsNullOrEmpty(where) Then
            strSql.Append(" where " & where)
        End If

        If Not String.IsNullOrEmpty(orderby) Then
            strSql.Append(" order by " & orderby)
        End If

        Return OleDbHelper.ExecuteTable(strSql.ToString())
    End Function

    Public Shared Function getListByPage(ByVal currentPage As Integer, ByVal pageSize As Integer) As DataTable
        Dim strSql As New StringBuilder()

        strSql.Append("select top " & pageSize & " [id],[Name],[ImgUrl],[viewcount],[Description],[CreateDate],[ModifyDate],[type],[author],[designdate] from design ")
        If currentPage > 1 Then
            Dim tmp As Integer = (currentPage - 1) * pageSize
            strSql.Append("where id>")
            strSql.Append("(select max (id) from ")
            strSql.Append("(select top " & tmp & " id from design order by id desc) as T")
            strSql.Append(") ")
        End If
        strSql.Append("order by id desc")

        Return OleDbHelper.ExecuteTable(strSql.ToString())
    End Function

    Public Sub getModel(ByVal id As Integer)
        Dim strSql As New StringBuilder()
        strSql.Append("select [Id],[Name],[viewcount],[ImgUrl],[Description],[CreateDate],[ModifyDate],[type],[author],[designdate] ")
        strSql.Append(" FROM [design] ")
        strSql.Append(" where Id=@Id ")
        Dim parameters As OleDbParameter() = {New OleDbParameter("@Id", OleDbType.[Integer], 4)}
        parameters(0).Value = id

        Dim dt As DataTable = OleDbHelper.ExecuteTable(strSql.ToString(), parameters)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            If dr("Id") IsNot Nothing AndAlso dr("Id").ToString() <> "" Then
                Me.Id = Integer.Parse(dr("Id").ToString())
            End If
            If dr("Name") IsNot Nothing AndAlso dr("Name").ToString() <> "" Then
                Me.Name = dr("Name").ToString()
            End If
            If dr("viewcount") IsNot Nothing AndAlso dr("viewcount").ToString() <> "" Then
                Me.viewcount = Integer.Parse(dr("viewcount").ToString())
            End If
            If dr("ImgUrl") IsNot Nothing AndAlso dr("ImgUrl").ToString() <> "" Then
                Me.ImgUrl = dr("ImgUrl").ToString()
            End If
            If dr("Description") IsNot Nothing AndAlso dr("Description").ToString() <> "" Then
                Me.Description = dr("Description").ToString()
            End If
            If dr("CreateDate") IsNot Nothing AndAlso dr("CreateDate").ToString() <> "" Then
                Me.CreateDate = DateTime.Parse(dr("CreateDate").ToString())
            End If
            If dr("ModifyDate") IsNot Nothing AndAlso dr("ModifyDate").ToString() <> "" Then
                Me.ModifyDate = DateTime.Parse(dr("ModifyDate").ToString())
            End If
            If dr("type") IsNot Nothing AndAlso dr("type").ToString() <> "" Then
                Me.type = Integer.Parse(dr("type").ToString())
            End If
            If dr("author") IsNot Nothing AndAlso dr("author").ToString() <> "" Then
                Me.author = dr("author").ToString()
            End If
            If dr("designdate") IsNot Nothing AndAlso dr("designdate").ToString() <> "" Then
                Me.designdate = DateTime.Parse(dr("designdate").ToString())
            End If
        End If
    End Sub
End Class