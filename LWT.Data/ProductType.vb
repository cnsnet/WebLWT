Imports System.Text
Imports System.Data.OleDb

Public Class ProductType

#Region "Model"
    Private _id As Integer
    Private _typename As String
    Private _fatherid As Integer = 0
    ''' <summary>
    '''
    ''' </summary>
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property
    ''' <summary>
    '''
    ''' </summary>
    Public Property typename() As String
        Get
            Return _typename
        End Get
        Set(value As String)
            _typename = value
        End Set
    End Property
    ''' <summary>
    '''
    ''' </summary>
    Public Property fatherid() As Integer
        Get
            Return _fatherid
        End Get
        Set(value As Integer)
            _fatherid = value
        End Set
    End Property
#End Region

    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer)
        Dim strSql As New StringBuilder()
        strSql.Append("select [id],[typename],[fatherid] ")
        strSql.Append("FROM [ProductType] ")
        strSql.Append("where id=@id ")
        Dim parameters As OleDbParameter() = {New OleDbParameter("@id", OleDbType.[Integer], 4)}
        parameters(0).Value = id

        Dim dt As DataTable = OleDbHelper.ExecuteTable(strSql.ToString(), parameters)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            If dr("id") <> Nothing AndAlso dr("id").ToString() <> "" Then
                Me.id = Integer.Parse(dr("id").ToString())
            End If
            If dr("typename") <> Nothing AndAlso dr("typename").ToString() <> "" Then
                Me.typename = dr("typename").ToString()
            End If
            If dr("fatherid") <> Nothing AndAlso dr("fatherid").ToString() <> "" Then
                Me.fatherid = Integer.Parse(dr("fatherid").ToString())
            End If
        End If
    End Sub

    Public Shared Function Exists(ByVal name As String) As Boolean
        Dim strSql As New StringBuilder()
        strSql.Append("select count(1) from [ProductType]")
        strSql.Append(" where [name]=@name ")

        Dim parameters As OleDbParameter() = {New OleDbParameter("@name", OleDbType.VarChar)}
        parameters(0).Value = name

        Dim reader As OleDbDataReader = OleDbHelper.ExecuteReader(strSql.ToString(), parameters)
        While (reader.Read())
            reader.Close()
            reader = Nothing

            Return True
        End While

        reader.Close()
        reader = Nothing

        Return False
    End Function

    Public Shared Function AddNew(ByVal typename As String, ByVal fatherid As Integer) As Boolean
        Dim strSql As New StringBuilder()
        strSql.Append("insert into [ProductType] (")
        strSql.Append("[typename],[fatherid])")
        strSql.Append(" values (")
        strSql.Append("@typename,@fatherid)")
        Dim parameters As OleDbParameter() = {
            New OleDbParameter("@typename", OleDbType.VarChar, 50),
            New OleDbParameter("@fatherid", OleDbType.[Integer], 4)
        }
        parameters(0).Value = typename
        parameters(1).Value = fatherid

        Dim Rows As Integer = OleDbHelper.ExecuteNonQuery(strSql.ToString(), parameters)

        If Rows > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function Delete(ByVal id As Integer) As Boolean
        Dim strSql As New StringBuilder()
        strSql.Append("delete from [ProductType] ")
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

    Public Shared Function Update(ByVal id As Integer, ByVal typename As String, ByVal fatherid As Integer) As Boolean
        Dim strSql As New StringBuilder()
        strSql.Append("update [ProductType] set ")
        strSql.Append("[typename]=@typename,")
        strSql.Append("[fatherid]=@fatherid")
        strSql.Append(" where id=@id ")
        Dim parameters As OleDbParameter() = {New OleDbParameter("@typename", OleDbType.VarChar, 50), New OleDbParameter("@fatherid", OleDbType.[Integer], 4), New OleDbParameter("@id", OleDbType.[Integer], 4)}
        parameters(0).Value = typename
        parameters(1).Value = fatherid
        parameters(2).Value = id

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

        strSql.Append(" [id],[typename],[fatherid] FROM [ProductType]")

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
        strSql.Append("select id,typename,fatherid ")
        strSql.Append("FROM [ProductType] ")
        strSql.Append("where id=@id ")
        Dim parameters As OleDbParameter() = {New OleDbParameter("@id", OleDbType.[Integer], 4)}
        parameters(0).Value = id

        Dim dt As DataTable = OleDbHelper.ExecuteTable(strSql.ToString(), parameters)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            If dr("id") <> Nothing AndAlso dr("id").ToString() <> "" Then
                Me.id = Integer.Parse(dr("id").ToString())
            End If
            If dr("typename") <> Nothing AndAlso dr("typename").ToString() <> "" Then
                Me.typename = dr("typename").ToString()
            End If
            If dr("fatherid") <> Nothing AndAlso dr("fatherid").ToString() <> "" Then
                Me.fatherid = Integer.Parse(dr("fatherid").ToString())
            End If
        End If
    End Sub
End Class
