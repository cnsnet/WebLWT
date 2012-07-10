Imports System.Text
Imports System.Data.OleDb

Public Class Jobs

#Region "Model"
    Private _id As Integer = 0
    Private _jobsname As String = String.Empty
    Private _description As String = String.Empty
    Private _viewcount As Integer = 0
    Private _createdate As DateTime = DateTime.Now()
    Private _modifydate As DateTime = DateTime.Now()
    Private _istop As Boolean = False
    Private _islongeffective As Boolean = False
    Private _age As String = "不限"
    Private _salary As String = "不限"
    Private _enddate As DateTime = DateTime.Now()
    Private _experience As String = "不限"
    Private _degree As String = "不限"
    Private _people As String = "若干"
    Private _contact As String = "请查看联系我们"
    Private _phone As String = "请查看联系我们"

    Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property

    ''' <summary>
    ''' 职位名称
    ''' </summary>
    Public Property jobsname() As String
        Get
            Return _jobsname
        End Get
        Set(value As String)
            _jobsname = value
        End Set
    End Property

    ''' <summary>
    ''' 职位描述
    ''' </summary>
    Public Property description() As String
        Get
            Return _description
        End Get
        Set(value As String)
            _description = value
        End Set
    End Property

    ''' <summary>
    ''' 浏览次数
    ''' </summary>
    Public Property viewCount() As Integer
        Get
            Return _viewcount
        End Get
        Set(value As Integer)
            _viewcount = value
        End Set
    End Property

    ''' <summary>
    ''' 创建日期
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
    ''' 修改日期
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
    ''' 是否置顶
    ''' </summary>
    Public Property istop() As Boolean
        Get
            Return _istop
        End Get
        Set(value As Boolean)
            _istop = value
        End Set
    End Property

    ''' <summary>
    ''' 是否长期有效
    ''' </summary>
    Public Property isLongEffective() As Boolean
        Get
            Return _islongeffective
        End Get
        Set(value As Boolean)
            _islongeffective = value
        End Set
    End Property

    ''' <summary>
    ''' 年龄
    ''' </summary>
    Public Property Age() As String
        Get
            Return _age
        End Get
        Set(value As String)
            _age = value
        End Set
    End Property

    ''' <summary>
    ''' 薪酬待遇
    ''' </summary>
    Public Property salary() As String
        Get
            Return _salary
        End Get
        Set(value As String)
            _salary = value
        End Set
    End Property

    ''' <summary>
    ''' 结束日期
    ''' </summary>
    Public Property endDate() As DateTime
        Get
            Return _enddate
        End Get
        Set(value As DateTime)
            _enddate = value
        End Set
    End Property

    ''' <summary>
    ''' 工作经验
    ''' </summary>
    Public Property experience() As String
        Get
            Return _experience
        End Get
        Set(value As String)
            _experience = value
        End Set
    End Property

    ''' <summary>
    ''' 学历
    ''' </summary>
    Public Property degree() As String
        Get
            Return _degree
        End Get
        Set(value As String)
            _degree = value
        End Set
    End Property

    ''' <summary>
    ''' 招聘人数
    ''' </summary>
    Public Property people() As String
        Get
            Return _people
        End Get
        Set(value As String)
            _people = value
        End Set
    End Property

    ''' <summary>
    ''' 联系人
    ''' </summary>
    Public Property contact() As String
        Get
            Return _contact
        End Get
        Set(value As String)
            _contact = value
        End Set
    End Property

    ''' <summary>
    ''' 联系电话
    ''' </summary>
    Public Property phone() As String
        Get
            Return _phone
        End Get
        Set(value As String)
            _phone = value
        End Set
    End Property
#End Region

    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer)
        Dim strSql As New StringBuilder()
        strSql.Append("select [Id],[jobsname],[description],[viewCount],[CreateDate],[ModifyDate],[istop],[isLongEffective],[age],[salary],[endDate],[experience],[degree],[people],[contact],[phone] ")
        strSql.Append(" FROM [Jobs] ")
        strSql.Append(" where Id=@Id ")
        Dim parameters As OleDbParameter() = {New OleDbParameter("@Id", OleDbType.[Integer], 4)}
        parameters(0).Value = id

        Dim dt As DataTable = OleDbHelper.ExecuteTable(strSql.ToString(), parameters)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            If dr("Id") <> Nothing AndAlso dr("Id").ToString() <> "" Then
                Me.Id = Integer.Parse(dr("Id").ToString())
            End If
            If dr("jobsname") <> Nothing AndAlso dr("jobsname").ToString() <> "" Then
                Me.jobsname = dr("jobsname").ToString()
            End If
            If dr("description") <> Nothing AndAlso dr("description").ToString() <> "" Then
                Me.description = dr("description").ToString()
            End If
            If dr("viewCount") <> Nothing AndAlso dr("viewCount").ToString() <> "" Then
                Me.viewCount = Integer.Parse(dr("viewCount").ToString())
            End If
            If dr("CreateDate") <> Nothing AndAlso dr("CreateDate").ToString() <> "" Then
                Me.CreateDate = DateTime.Parse(dr("CreateDate").ToString())
            End If
            If dr("ModifyDate") <> Nothing AndAlso dr("ModifyDate").ToString() <> "" Then
                Me.ModifyDate = DateTime.Parse(dr("ModifyDate").ToString())
            End If
            If dr("endDate") <> Nothing AndAlso dr("endDate").ToString() <> "" Then
                Me.endDate = DateTime.Parse(dr("endDate").ToString())
            End If
            If dr("istop") <> Nothing AndAlso dr("istop").ToString() <> "" Then
                If (dr("istop").ToString() = "1") OrElse (dr("istop").ToString().ToLower() = "true") Then
                    Me.istop = True
                Else
                    Me.istop = False
                End If
            End If
            If dr("isLongEffective") <> Nothing AndAlso dr("isLongEffective").ToString() <> "" Then
                If (dr("isLongEffective").ToString() = "1") OrElse (dr("isLongEffective").ToString().ToLower() = "true") Then
                    Me.isLongEffective = True
                Else
                    Me.isLongEffective = False
                End If
            End If
            If dr("age") <> Nothing AndAlso dr("age").ToString() <> "" Then
                Me.Age = dr("age").ToString()
            End If
            If dr("salary") <> Nothing AndAlso dr("salary").ToString() <> "" Then
                Me.salary = dr("salary").ToString()
            End If
            If dr("experience") <> Nothing AndAlso dr("experience").ToString() <> "" Then
                Me.experience = dr("experience").ToString()
            End If
            If dr("degree") <> Nothing AndAlso dr("degree").ToString() <> "" Then
                Me.degree = dr("degree").ToString()
            End If
            If dr("people") <> Nothing AndAlso dr("people").ToString() <> "" Then
                Me.people = dr("people").ToString()
            End If
            If dr("contact") <> Nothing AndAlso dr("contact").ToString() <> "" Then
                Me.contact = dr("contact").ToString()
            End If
            If dr("phone") <> Nothing AndAlso dr("phone").ToString() <> "" Then
                Me.phone = dr("phone").ToString()
            End If
        End If
    End Sub

    Public Shared Function AddNew(ByVal jobsname As String,
                                  ByVal description As String,
                                  ByVal isTop As Boolean,
                                  ByVal isLongEffective As Boolean,
                                  ByVal startDate As DateTime,
                                  ByVal endDate As DateTime,
                                  ByVal age As String,
                                  ByVal salary As String,
                                  ByVal experience As String,
                                  ByVal degree As String,
                                  ByVal people As String,
                                  ByVal contact As String,
                                  ByVal phone As String) As Boolean

        Dim strSql As New StringBuilder()
        strSql.Append("insert into [Jobs] (")
        strSql.Append("[jobsname],[description],[istop],[isLongEffective],[CreateDate],[endDate],[age],[salary],[experience],[degree],[people],[contact],[phone])")
        strSql.Append(" values (")
        strSql.Append("@jobsname,@description,@istop,@isLongEffective,@CreateDate,@endDate,@age,@salary,@experience,@degree,@people,@contact,@phone)")
        Dim parameters As OleDbParameter() = {
            New OleDbParameter("@jobsname", OleDbType.VarChar, 50),
            New OleDbParameter("@description", OleDbType.VarChar, 0),
            New OleDbParameter("@istop", OleDbType.[Boolean], 1),
            New OleDbParameter("@isLongEffective", OleDbType.[Boolean], 1),
            New OleDbParameter("@CreateDate", OleDbType.Date, 1),
            New OleDbParameter("@endDate", OleDbType.[Date]),
            New OleDbParameter("@age", OleDbType.VarChar),
            New OleDbParameter("@salary", OleDbType.VarChar, 255),
            New OleDbParameter("@experience", OleDbType.VarChar, 255),
            New OleDbParameter("@degree", OleDbType.VarChar, 255),
            New OleDbParameter("@people", OleDbType.VarChar, 255),
            New OleDbParameter("@contact", OleDbType.VarChar, 255),
            New OleDbParameter("@phone", OleDbType.VarChar, 255)
        }

        parameters(0).Value = jobsname
        parameters(1).Value = description
        parameters(2).Value = isTop
        parameters(3).Value = isLongEffective
        parameters(4).Value = startDate
        parameters(5).Value = endDate
        parameters(6).Value = If(String.IsNullOrEmpty(age), "不限", age)
        parameters(7).Value = If(String.IsNullOrEmpty(salary), "面议", salary)
        parameters(8).Value = If(String.IsNullOrEmpty(experience), "不限", experience)
        parameters(9).Value = If(String.IsNullOrEmpty(degree), "不限", degree)
        parameters(10).Value = If(String.IsNullOrEmpty(people), "若干", people)
        parameters(11).Value = If(String.IsNullOrEmpty(contact), "请查看联系我们", contact)
        parameters(12).Value = If(String.IsNullOrEmpty(phone), "请查看联系我们", phone)

        Dim Rows As Integer = OleDbHelper.ExecuteNonQuery(strSql.ToString(), parameters)

        If Rows > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function Delete(ByVal id As Integer) As Boolean
        Dim strSql As New StringBuilder()
        strSql.Append("delete from [Jobs] ")
        strSql.Append(" where Id=@Id ")
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

    Public Shared Function Update(ByVal id As Integer,
                                  ByVal jobsname As String,
                                  ByVal description As String,
                                  ByVal isTop As Boolean,
                                  ByVal isLongEffective As Boolean,
                                  ByVal ModifyDate As DateTime,
                                  ByVal endDate As DateTime,
                                  ByVal age As String,
                                  ByVal salary As String,
                                  ByVal experience As String,
                                  ByVal degree As String,
                                  ByVal people As String,
                                  ByVal contact As String,
                                  ByVal phone As String) As Boolean
        Dim strSql As New StringBuilder()
        strSql.Append("update [Jobs] set ")
        strSql.Append("[jobsname]=@jobsname,")
        strSql.Append("[description]=@description,")
        strSql.Append("[istop]=@istop,")
        strSql.Append("[isLongEffective]=@isLongEffective,")
        strSql.Append("[ModifyDate]=@ModifyDate,")
        strSql.Append("[endDate]=@endDate,")
        strSql.Append("[age]=@age,")
        strSql.Append("[salary]=@salary,")
        strSql.Append("[experience]=@experience,")
        strSql.Append("[degree]=@degree,")
        strSql.Append("[people]=@people,")
        strSql.Append("[contact]=@contact,")
        strSql.Append("[phone]=@phone")
        strSql.Append(" where Id=@Id ")

        Dim parameters As OleDbParameter() = {
            New OleDbParameter("@jobsname", OleDbType.VarChar, 50),
            New OleDbParameter("@description", OleDbType.VarChar, 0),
            New OleDbParameter("@istop", OleDbType.[Boolean], 1),
            New OleDbParameter("@isLongEffective", OleDbType.[Boolean], 1),
            New OleDbParameter("@ModifyDate", OleDbType.[Date]),
            New OleDbParameter("@endDate", OleDbType.[Date]),
            New OleDbParameter("@age", OleDbType.VarChar),
            New OleDbParameter("@salary", OleDbType.VarChar, 255),
            New OleDbParameter("@experience", OleDbType.VarChar, 255),
            New OleDbParameter("@degree", OleDbType.VarChar, 255),
            New OleDbParameter("@people", OleDbType.VarChar, 255),
            New OleDbParameter("@contact", OleDbType.VarChar, 255),
            New OleDbParameter("@phone", OleDbType.VarChar, 255),
            New OleDbParameter("@Id", OleDbType.[Integer], 4)}

        parameters(0).Value = jobsname
        parameters(1).Value = description
        parameters(2).Value = isTop
        parameters(3).Value = isLongEffective
        parameters(4).Value = ModifyDate
        parameters(5).Value = endDate
        parameters(6).Value = If(String.IsNullOrEmpty(age), "不限", age)
        parameters(7).Value = If(String.IsNullOrEmpty(salary), "面议", salary)
        parameters(8).Value = If(String.IsNullOrEmpty(experience), "不限", experience)
        parameters(9).Value = If(String.IsNullOrEmpty(degree), "不限", degree)
        parameters(10).Value = If(String.IsNullOrEmpty(people), "若干", people)
        parameters(11).Value = If(String.IsNullOrEmpty(contact), "请查看联系我们", contact)
        parameters(12).Value = If(String.IsNullOrEmpty(phone), "请查看联系我们", phone)
        parameters(13).Value = id

        Dim rows As Integer = OleDbHelper.ExecuteNonQuery(strSql.ToString(), parameters)
        If rows > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub UpdateViewCount(ByVal id As Integer)
        Dim strSql As New StringBuilder()
        strSql.Append("update [Jobs] set ")
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

        strSql.Append(" [Id],[jobsname],[description],[viewCount],[CreateDate],[ModifyDate],[istop],[isLongEffective],[age],[salary],[endDate],[experience],[degree],[people],[contact],[phone] FROM [Jobs]")

        If Not String.IsNullOrEmpty(where) Then
            strSql.Append(" where " & where)
        End If

        If Not String.IsNullOrEmpty(orderby) Then
            strSql.Append(" order by " & orderby)
        End If

        Return OleDbHelper.ExecuteTable(strSql.ToString())
    End Function

    Public Sub getModel(ByVal id As Integer)
        Dim strSql As New StringBuilder()
        strSql.Append("select [Id],[jobsname],[description],[viewCount],[CreateDate],[ModifyDate],[istop],[isLongEffective],[age],[salary],[endDate],[experience],[degree],[people],[contact],[phone] ")
        strSql.Append(" FROM [Jobs] ")
        strSql.Append(" where Id=@Id ")
        Dim parameters As OleDbParameter() = {New OleDbParameter("@Id", OleDbType.[Integer], 4)}
        parameters(0).Value = id

        Dim dt As DataTable = OleDbHelper.ExecuteTable(strSql.ToString(), parameters)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            If dr("Id") IsNot Nothing AndAlso dr("Id").ToString() <> "" Then
                Me.Id = Integer.Parse(dr("Id").ToString())
            End If
            If dr("jobsname") IsNot Nothing AndAlso dr("jobsname").ToString() <> "" Then
                Me.jobsname = dr("jobsname").ToString()
            End If
            If dr("description") IsNot Nothing AndAlso dr("description").ToString() <> "" Then
                Me.description = dr("description").ToString()
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
            If dr("endDate") IsNot Nothing AndAlso dr("endDate").ToString() <> "" Then
                Me.endDate = DateTime.Parse(dr("endDate").ToString())
            End If
            If dr("istop") IsNot Nothing AndAlso dr("istop").ToString() <> "" Then
                If (dr("istop").ToString() = "1") OrElse (dr("istop").ToString().ToLower() = "true") Then
                    Me.istop = True
                Else
                    Me.istop = False
                End If
            End If
            If dr("isLongEffective") IsNot Nothing AndAlso dr("isLongEffective").ToString() <> "" Then
                If (dr("isLongEffective").ToString() = "1") OrElse (dr("isLongEffective").ToString().ToLower() = "true") Then
                    Me.isLongEffective = True
                Else
                    Me.isLongEffective = False
                End If
            End If
            If dr("age") IsNot Nothing AndAlso dr("age").ToString() <> "" Then
                Me.Age = dr("age").ToString()
            End If
            If dr("salary") IsNot Nothing AndAlso dr("salary").ToString() <> "" Then
                Me.salary = dr("salary").ToString()
            End If
            If dr("experience") IsNot Nothing AndAlso dr("experience").ToString() <> "" Then
                Me.experience = dr("experience").ToString()
            End If
            If dr("degree") IsNot Nothing AndAlso dr("degree").ToString() <> "" Then
                Me.degree = dr("degree").ToString()
            End If
            If dr("people") IsNot Nothing AndAlso dr("people").ToString() <> "" Then
                Me.people = dr("people").ToString()
            End If
            If dr("contact") IsNot Nothing AndAlso dr("contact").ToString() <> "" Then
                Me.contact = dr("contact").ToString()
            End If
            If dr("phone") IsNot Nothing AndAlso dr("phone").ToString() <> "" Then
                Me.phone = dr("phone").ToString()
            End If
        End If
    End Sub
End Class