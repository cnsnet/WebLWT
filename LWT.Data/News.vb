Imports System.Text
Imports System.Data.OleDb

Public Class News

#Region "Model"
    Private _id As Integer = 0
    Private _title As String = String.Empty
    Private _content As String = String.Empty
    Private _viewcount As Integer = 0
    Private _createdate As DateTime = DateTime.Now()
    Private _modifydate As DateTime = DateTime.Now()
    Private _istop As Boolean = False
    Private Shared _pageSize As Integer = 5

    Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property
    Public Property Title() As String
        Get
            Return _title
        End Get
        Set(value As String)
            _title = value
        End Set
    End Property
    Public Property Content() As String
        Get
            Return _content
        End Get
        Set(value As String)
            _content = value
        End Set
    End Property
    Public Property viewCount() As Integer
        Get
            Return _viewcount
        End Get
        Set(value As Integer)
            _viewcount = value
        End Set
    End Property
    Public Property CreateDate() As DateTime
        Get
            Return _createdate
        End Get
        Set(value As DateTime)
            _createdate = value
        End Set
    End Property
    Public Property ModifyDate() As DateTime
        Get
            Return _modifydate
        End Get
        Set(value As DateTime)
            _modifydate = value
        End Set
    End Property
    Public Property istop() As Boolean
        Get
            Return _istop
        End Get
        Set(value As Boolean)
            _istop = value
        End Set
    End Property

    'Public Shared Property pageSize As Integer
    '    Get
    '        Return _pageSize
    '    End Get
    '    Set(value As Integer)
    '        _pageSize = value
    '    End Set
    'End Property
#End Region

    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer)
        Dim strSql As New StringBuilder()
        strSql.Append("select [Id],[Title],[Content],[viewCount],[CreateDate],[ModifyDate],[istop] ")
        strSql.Append("FROM [News] ")
        strSql.Append("where [Id]=@Id ")
        Dim parameters As OleDbParameter() = {
            New OleDbParameter("@Id", OleDbType.Integer, 4)
        }
        parameters(0).Value = id

        Dim dt As DataTable = OleDbHelper.ExecuteTable(strSql.ToString(), parameters)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            If dr("Id") IsNot Nothing AndAlso dr("Id").ToString() <> "" Then
                Me.Id = Integer.Parse(dr("Id").ToString())
            End If
            If dr("Title") IsNot Nothing AndAlso dr("Title").ToString() <> "" Then
                Me.Title = dr("Title").ToString()
            End If
            If dr("Content") IsNot Nothing AndAlso dr("Content").ToString() <> "" Then
                Me.Content = dr("Content").ToString()
            End If
            If dr("viewCount") IsNot Nothing AndAlso dr("viewCount").ToString() <> "" Then
                Me.viewCount = Integer.Parse(dr("viewCount").ToString())
            End If
            If dr("CreateDate") IsNot Nothing AndAlso dr("CreateDate").ToString() <> "" Then
                Me.CreateDate = DateTime.Parse(dr("CreateDate").ToString())
            End If
            If dr("ModifyDate") IsNot Nothing AndAlso dr("ModifyDate").ToString() <> "" Then
                Me.ModifyDate = DateTime.Parse(dr("ModifyDate").ToString())
            End If
            If dr("istop") IsNot Nothing AndAlso dr("istop").ToString() <> "" Then
                If (dr("istop").ToString() = "1") OrElse (dr("istop").ToString().ToLower() = "true") Then
                    Me.istop = True
                Else
                    Me.istop = False
                End If
            End If
        End If
    End Sub

    Public Shared Function AddNew(ByVal title As String, ByVal content As String, ByVal createDate As DateTime, ByVal isTop As Boolean, ByVal htmlPath As String) As Boolean
        Dim strSql As New StringBuilder()
        strSql.Append("insert into [News] (")
        strSql.Append("[Title],[Content],[CreateDate],[istop],[htmlpath])")
        strSql.Append(" values (")
        strSql.Append("@Title,@Content,@CreateDate,@istop,@htmlpath)")
        Dim parameters As OleDbParameter() = {
            New OleDbParameter("@Title", OleDbType.VarChar, 50),
            New OleDbParameter("@Content", OleDbType.VarChar, 0),
            New OleDbParameter("@CreateDate", OleDbType.Date),
            New OleDbParameter("@istop", OleDbType.Boolean),
            New OleDbParameter("@htmlpath", OleDbType.VarChar)
        }

        parameters(0).Value = title
        parameters(1).Value = content
        parameters(2).Value = createDate
        parameters(3).Value = isTop
        parameters(4).Value = htmlPath

        Dim Rows As Integer = OleDbHelper.ExecuteNonQuery(strSql.ToString(), parameters)

        If Rows > 0 Then
            News.ReIndex()
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function Delete(ByVal id As Integer) As Boolean
        Dim strSql As New StringBuilder()
        strSql.Append("delete from [News] ")
        strSql.Append(" where Id=@Id ")
        Dim parameters As OleDbParameter() = {
            New OleDbParameter("@Id", OleDbType.Integer, 4)
        }
        parameters(0).Value = id

        Dim rows As Integer = OleDbHelper.ExecuteNonQuery(strSql.ToString(), parameters)

        If rows > 0 Then
            News.ReIndex()
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function Update(ByVal id As Integer, ByVal title As String, ByVal content As String, ByVal modifyDate As DateTime, ByVal isTop As Boolean) As Boolean
        Dim strSql As New StringBuilder()
        strSql.Append("update [News] set ")
        strSql.Append("[Title]=@Title,")
        strSql.Append("[Content]=@Content,")
        strSql.Append("[ModifyDate]=@ModifyDate,")
        strSql.Append("[istop]=@istop")
        strSql.Append(" where [Id]=@Id ")
        Dim parameters As OleDbParameter() = {
            New OleDbParameter("@Title", OleDbType.VarChar, 50),
            New OleDbParameter("@Content", OleDbType.LongVarChar),
            New OleDbParameter("@ModifyDate", OleDbType.Date),
            New OleDbParameter("@istop", OleDbType.Boolean),
            New OleDbParameter("@Id", OleDbType.Integer, 4)
        }

        parameters(0).Value = title
        parameters(1).Value = content
        parameters(2).Value = modifyDate
        parameters(3).Value = isTop
        parameters(4).Value = id

        Dim rows As Integer = OleDbHelper.ExecuteNonQuery(strSql.ToString(), parameters)
        If rows > 0 Then
            News.ReIndex()
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub UpdateViewCount()
        Dim strSql As New StringBuilder()
        strSql.Append("update [News] set ")
        strSql.Append("[viewCount]=[viewCount] + 1")
        strSql.Append(" where Id=@Id ")

        Dim parameters As OleDbParameter() = {
            New OleDbParameter("@Id", OleDbType.Integer, 4)
        }
        parameters(0).Value = Me.Id

        OleDbHelper.ExecuteNonQuery(strSql.ToString(), parameters)
    End Sub

    Public Shared Function GetListAll(ByVal top As Integer, ByVal where As String, ByVal orderby As String) As DataTable
        Dim strSql As New StringBuilder()
        strSql.Append("select ")

        If top > 0 Then
            strSql.Append("top " & top)
        End If

        strSql.Append(" [Id],[Title],[viewCount],[CreateDate],[ModifyDate],[istop] FROM [News]")

        If Not String.IsNullOrEmpty(where) Then
            strSql.Append(" where " & where)
        End If

        If Not String.IsNullOrEmpty(orderby) Then
            strSql.Append(" order by " & orderby)
        End If

        Return OleDbHelper.ExecuteTable(strSql.ToString())
    End Function

    Public Shared Function GetListPageDesc(ByVal currentPage As Integer, ByVal pageSize As Integer) As DataTable
        Dim strSql As New StringBuilder()

        strSql.Append("select top " & pageSize & " [Id],[Title],[CreateDate],[istop] from NewsIndex ")
        If currentPage > 1 Then
            Dim tmp As Integer = (currentPage - 1) * pageSize
            strSql.Append("where [index]<")
            strSql.Append("(select min([index]) from ")
            strSql.Append("(select top " & tmp & " [index] from News order by [index] desc) as T")
            strSql.Append(") ")
        End If
        strSql.Append("order by [index] desc")

        Return OleDbHelper.ExecuteTable(strSql.ToString())
    End Function

    Public Shared Function GetListPageAsc(ByVal currentPage As Integer, ByVal pageSize As Integer) As DataTable
        Dim strSql As New StringBuilder()

        strSql.Append("select top " & pageSize & " [Id],[Title],[CreateDate],[istop] from NewsIndex ")
        If currentPage > 1 Then
            Dim tmp As Integer = (currentPage - 1) * pageSize
            strSql.Append("where [index]>")
            strSql.Append("(select max([index]) from ")
            strSql.Append("(select top " & tmp & " id from News order by [index] asc) as T")
            strSql.Append(") ")
        End If
        strSql.Append("order by [index] asc")

        Return OleDbHelper.ExecuteTable(strSql.ToString())
    End Function

    Public Shared Function GetTotalPage(ByVal pageSize As Integer) As Integer
        Dim strSql As New StringBuilder()
        Dim totalCount As Integer = 0
        strSql.Append("select count(id) from NewsIndex")

        totalCount = Integer.Parse(OleDbHelper.ExecuteScalar(strSql.ToString()))

        If (totalCount Mod pageSize) = 0 Then
            Return (totalCount \ pageSize)
        Else
            Return (totalCount \ pageSize) + 1
        End If

    End Function

    Public Shared Sub ReIndex()
        Dim commandText As String = String.Empty
        commandText = "delete * from NewsIndex"
        OleDbHelper.ExecuteNonQuery(commandText)
        commandText = "ALTER TABLE NewsIndex ALTER COLUMN [index] COUNTER (1, 1)"
        OleDbHelper.ExecuteNonQuery(commandText)

        Dim dt As DataTable = News.GetListAll(0, String.Empty, "istop,createdate desc")

        For Each dr In dt.Rows
            commandText = "insert into newsindex ([id],[title],[CreateDate],[istop]) values ('" & dr("id").ToString() & "','" & dr("title").ToString() & "','" & DateTime.Parse(dr("createdate")) & "'," & dr("istop") & ")"
            OleDbHelper.ExecuteNonQuery(commandText)
            commandText = Nothing
        Next
    End Sub

    Public Sub GetModel(ByVal id As Integer)
        Dim strSql As New StringBuilder()
        strSql.Append("select [Id],[Title],[Content],[viewCount],[CreateDate],[ModifyDate],[istop] ")
        strSql.Append("FROM [News] ")
        strSql.Append("where [Id]=@Id ")
        Dim parameters As OleDbParameter() = {New OleDbParameter("@Id", OleDbType.Integer, 4)}
        parameters(0).Value = id

        Dim dt As DataTable = OleDbHelper.ExecuteTable(strSql.ToString(), parameters)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            If dr("Id") IsNot Nothing AndAlso dr("Id").ToString() <> "" Then
                Me.Id = Integer.Parse(dr("Id").ToString())
            End If
            If dr("Title") IsNot Nothing AndAlso dr("Title").ToString() <> "" Then
                Me.Title = dr("Title").ToString()
            End If
            If dr("Content") IsNot Nothing AndAlso dr("Content").ToString() <> "" Then
                Me.Content = dr("Content").ToString()
            End If
            If dr("viewCount") IsNot Nothing AndAlso dr("viewCount").ToString() <> "" Then
                Me.viewCount = Integer.Parse(dr("viewCount").ToString())
            End If
            If dr("CreateDate") IsNot Nothing AndAlso dr("CreateDate").ToString() <> "" Then
                Me.CreateDate = DateTime.Parse(dr("CreateDate").ToString())
            End If
            If dr("ModifyDate") IsNot Nothing AndAlso dr("ModifyDate").ToString() <> "" Then
                Me.ModifyDate = DateTime.Parse(dr("ModifyDate").ToString())
            End If
            If dr("istop") IsNot Nothing AndAlso dr("istop").ToString() <> "" Then
                If (dr("istop").ToString() = "1") OrElse (dr("istop").ToString().ToLower() = "true") Then
                    Me.istop = True
                Else
                    Me.istop = False
                End If
            End If
        End If
    End Sub
End Class