Imports System.Configuration
Imports System.Data.OleDb
Imports System.Web

Public Class OleDbHelper
    Public Sub New()

    End Sub

    Private Shared ReadOnly strconn As String = ConfigurationManager.AppSettings("myds") + HttpContext.Current.Request.MapPath(ConfigurationManager.AppSettings("myconn"))
    Private Shared ReadOnly CommandTimeout As Integer = 3

    Private Shared Function GetOleDbConnection() As OleDbConnection
        Return New OleDbConnection(strconn)
    End Function

    Public Shared Function ExecuteNonQuery(ByVal cmdText As String, ByVal ParamArray commandParameters() As OleDbParameter) As Integer
        Dim command As OleDbCommand = New OleDbCommand()

        Using connection As OleDbConnection = GetOleDbConnection()
            PrepareCommand(command, connection, CommandType.Text, cmdText, commandParameters)
            Dim val As Integer = command.ExecuteNonQuery()
            command.Parameters.Clear()
            connection.Close()
            Return val
        End Using
    End Function

    Public Shared Function ExecuteNonQuery(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray commandParameters() As OleDbParameter) As Integer
        Dim command As OleDbCommand = New OleDbCommand()

        Using connection As OleDbConnection = GetOleDbConnection()
            PrepareCommand(command, connection, cmdType, cmdText, commandParameters)
            Dim val As Integer = command.ExecuteNonQuery()
            command.Parameters.Clear()
            connection.Close()
            Return val
        End Using
    End Function

    Public Shared Function ExecuteScalar(ByVal cmdText As String, ByVal ParamArray commandParameters() As OleDbParameter) As Object
        Dim command As OleDbCommand = New OleDbCommand()

        Using connection As OleDbConnection = GetOleDbConnection()
            PrepareCommand(command, connection, CommandType.Text, cmdText, commandParameters)
            Dim val As Object = command.ExecuteScalar()
            command.Parameters.Clear()
            connection.Close()
            Return val
        End Using
    End Function

    Public Shared Function ExecuteScalar(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray commandParameters() As OleDbParameter) As Object
        Dim command As OleDbCommand = New OleDbCommand()

        Using connection As OleDbConnection = GetOleDbConnection()
            PrepareCommand(command, connection, cmdType, cmdText, commandParameters)
            Dim val As Object = command.ExecuteScalar()
            command.Parameters.Clear()
            connection.Close()
            Return val
        End Using
    End Function

    Public Shared Function ExecuteReader(ByVal cmdText As String, ByVal ParamArray commandParameters() As OleDbParameter) As OleDbDataReader
        Dim connection As OleDbConnection = GetOleDbConnection()
        Dim command As OleDbCommand = New OleDbCommand()

        Try
            PrepareCommand(command, connection, CommandType.Text, cmdText, commandParameters)
            Dim reader As OleDbDataReader = command.ExecuteReader(CommandBehavior.CloseConnection)
            command.Parameters.Clear()
            Return reader
        Catch ex As Exception
            connection.Close()
            Throw
        End Try
    End Function

    Public Shared Function ExecuteReader(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray commandParameters() As OleDbParameter) As OleDbDataReader
        Dim connection As OleDbConnection = GetOleDbConnection()
        Dim command As OleDbCommand = New OleDbCommand()

        Try
            PrepareCommand(command, connection, cmdType, cmdText, commandParameters)
            Dim reader As OleDbDataReader = command.ExecuteReader(CommandBehavior.CloseConnection)
            command.Parameters.Clear()
            Return reader
        Catch ex As Exception
            connection.Close()
            Throw
        End Try
    End Function

    Public Shared Function ExecuteTable(ByVal cmdText As String, ByVal ParamArray cmmandParameters() As OleDbParameter) As DataTable
        Dim connection As OleDbConnection = GetOleDbConnection()
        Dim command As OleDbCommand = New OleDbCommand()
        Dim ds As New DataSet()

        Try
            PrepareCommand(command, connection, CommandType.Text, cmdText, cmmandParameters)

            Using oda As OleDbDataAdapter = New OleDbDataAdapter(command)
                oda.Fill(ds)
                command.Parameters.Clear()
                connection.Dispose()
                command.Dispose()
                Return ds.Tables(0)
            End Using
        Catch ex As Exception
            connection.Close()
            Throw
        End Try

        'If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
        '    Return ds.Tables(0)
        'Else
        '    Return Nothing
        'End If
    End Function

    Public Shared Function ExecuteTable(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray cmmandParameters() As OleDbParameter) As DataTable
        Dim connection As OleDbConnection = GetOleDbConnection()
        Dim command As OleDbCommand = New OleDbCommand()
        Dim ds As New DataSet()

        Try
            PrepareCommand(command, connection, cmdType, cmdText, cmmandParameters)

            Using oda As OleDbDataAdapter = New OleDbDataAdapter(command)
                oda.Fill(ds)
                command.Parameters.Clear()
                connection.Dispose()
                command.Dispose()
                Return ds.Tables(0)
            End Using
        Catch ex As Exception
            connection.Close()
            Throw
        End Try

        'If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
        '    Return ds.Tables(0)
        'Else
        '    Return Nothing
        'End If
    End Function

    Private Shared Sub PrepareCommand(ByVal cmd As OleDbCommand, ByVal conn As OleDbConnection, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms() As OleDbParameter)
        If conn.State <> ConnectionState.Open Then
            conn.Open()
        End If

        cmd.Connection = conn
        cmd.CommandText = cmdText
        cmd.CommandType = cmdType
        cmd.CommandTimeout = CommandTimeout

        If cmdParms IsNot Nothing Then
            For Each parm As OleDbParameter In cmdParms
                cmd.Parameters.Add(parm)
            Next
        End If
    End Sub
End Class
