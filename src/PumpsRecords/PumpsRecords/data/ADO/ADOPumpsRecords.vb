Imports PumpsRecords.entities
Imports PumpsRecords.data.dao

Namespace data.ADO

    Public Class ADOPumpsRecords
        Inherits data.dao.OleDBConnectionObj

        Private _database As String = "DB-PUMPSRECORD"
        

        Public Sub InsertPump(ByVal o_pump As pump)
            Dim qbuilder As New QueryBuilder(Of pump)
            qbuilder.TypeQuery = TypeQuery.Insert
            qbuilder.Entity = o_pump
            qbuilder.BuildInsert("pumps")
            Try
                OpenDB(_database)
                connection.Command = New OleDb.OleDbCommand(qbuilder.Query, connection.Connection)
                connection.Command.ExecuteNonQuery()
            Catch ex As Exception
                Throw
            Finally
                CloseDB()
            End Try
            o_pump.IDPump = GetLastID("pumps", "IDPump")

        End Sub

        Public Sub UpdatePump(ByVal o_pump As pump)
            Dim qbuilder As New QueryBuilder(Of pump)
            qbuilder.TypeQuery = TypeQuery.Update
            qbuilder.Entity = o_pump
            qbuilder.BuildUpdate("pumps", "IDPump", o_pump.IDPump)
            Try
                OpenDB(_database)
                connection.Command = New OleDb.OleDbCommand(qbuilder.Query, connection.Connection)
                connection.Command.ExecuteNonQuery()
            Catch ex As Exception
                Throw
            Finally
                CloseDB()
            End Try
        End Sub

        Public Sub DeletePump(ByVal o_pump As pump)
            Try
                OpenDB(_database)
                connection.Command = New OleDb.OleDbCommand("Delete from pumps where IDPump=" & o_pump.IDPump.ToString & "", connection.Connection)
                connection.Command.ExecuteNonQuery()
            Catch ex As Exception
                Throw
            Finally
                CloseDB()
            End Try
        End Sub

        Public Sub DeactivatePump(ByVal o_pump As pump)
            Try
                OpenDB(_database)
                connection.Command = New OleDb.OleDbCommand("update pumps set Active=0 where IDPump=" & o_pump.IDPump.ToString & "", connection.Connection)
                connection.Command.ExecuteNonQuery()
            Catch ex As Exception
                Throw
            Finally
                CloseDB()
            End Try
        End Sub

        Public Sub ActivatePump(ByVal o_pump As pump)
            Try
                OpenDB(_database)
                connection.Command = New OleDb.OleDbCommand("update pumps set Active=-1 where IDPump=" & o_pump.IDPump.ToString & "", connection.Connection)
                connection.Command.ExecuteNonQuery()
            Catch ex As Exception
                Throw
            Finally
                CloseDB()
            End Try
        End Sub

        Public Sub GetAllPumps(ByVal ListOfPumps As pumpsCollection)
            Try
                'create the filler
                Dim w_entityfiller As New EntityFiller(Of pump)
                w_entityfiller.DataBase = _database
                'create a new object to select the fields with -7
                Dim _selectedfields As New pump
                _selectedfields.IDPump = -7
                _selectedfields.Description = "-7"
                _selectedfields.Status = "-7"

                'create the select
                Dim w_query As New QueryBuilder(Of pump)
                w_query.TypeQuery = TypeQuery.SelectInfo
                w_query.Entity = _selectedfields
                w_query.BuildSelect("pumps")


                'fill the list
                w_entityfiller.FillList(ListOfPumps.Items, "pumps", w_query)


            Catch ex As Exception
                Throw
            Finally
                CloseDB()
            End Try
        End Sub

        Public Function GetLastID(ByVal table As String, ByVal field As String) As Integer
            Dim result As Integer = -1
            Try
                OpenDB(_database)
                connection.Command = New OleDb.OleDbCommand("select max(" & field & ") from " & table, connection.Connection)
                result = connection.Command.ExecuteScalar()
            Catch ex As Exception
                Throw
            Finally
                CloseDB()
            End Try

            Return result
        End Function


    End Class
End Namespace
