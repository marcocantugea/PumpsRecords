
Imports PumpsRecords.entities
Imports PumpsRecords.data.dao

Namespace data.ADO

    Public Class ADORecordPump
        Inherits data.dao.OleDBConnectionObj

        Private _database As String = "DB-PUMPSRECORD"

        Public Sub InsertRecord(ByVal recordPump As PumpRecord)
            Dim qbuilder As New QueryBuilder(Of PumpRecord)
            qbuilder.TypeQuery = TypeQuery.Insert
            qbuilder.Entity = recordPump
            qbuilder.BuildInsert("records")
            Try
                OpenDB(_database)
                connection.Command = New OleDb.OleDbCommand(qbuilder.Query, connection.Connection)
                connection.Command.ExecuteNonQuery()
            Catch ex As Exception
                Throw
            Finally
                CloseDB()
            End Try
            recordPump.IDRecord = GetLastID("records", "IDRecord")

        End Sub

        Public Sub UpdateRecord(ByVal recordPump As PumpRecord)
            Dim qbuilder As New QueryBuilder(Of PumpRecord)
            qbuilder.TypeQuery = TypeQuery.Update
            qbuilder.Entity = recordPump
            qbuilder.BuildUpdate("records", "IDRecord", recordPump.IDRecord)
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
        Public Sub DeleteRecord(ByVal recordPump As PumpRecord)
            Try
                OpenDB(_database)
                connection.Command = New OleDb.OleDbCommand("Delete from records where IDRecord=" & recordPump.IDRecord.ToString & "", connection.Connection)
                connection.Command.ExecuteNonQuery()
            Catch ex As Exception
                Throw
            Finally
                CloseDB()
            End Try
        End Sub

        Public Sub GetAllRecordsPump(ByVal list As List(Of PumpRecord))
            GetAllRecordsPump(list, Nothing)
        End Sub

        Public Sub GetAllRecordsPump(ByVal list As List(Of PumpRecord), ByVal parameter As String)
            Try
                'create the filler
                Dim w_entityfiller As New EntityFiller(Of PumpRecord)
                w_entityfiller.DataBase = _database
                'create a new object to select the fields with -7
                Dim _selectedfields As New PumpRecord
                _selectedfields.IDRecord = -7
                _selectedfields.DatePumpRecord = New Date(1939, 9, 1)
                _selectedfields.Amps = -7
                _selectedfields.Comment = "-7"
                _selectedfields.DischargePressure = -7
                _selectedfields.IDPump = -7
                _selectedfields.PumpDesc = "-7"
                _selectedfields.Flag = True
                _selectedfields.RecordModifyBy = "-7"
                _selectedfields.DateCaptured = New Date(1939, 9, 1)
                _selectedfields.RecordCreatedBy = "-7"
                'create the select
                Dim w_query As New QueryBuilder(Of PumpRecord)
                w_query.TypeQuery = TypeQuery.SelectInfo
                w_query.Entity = _selectedfields
                w_query.BuildSelect("records")
                If Not IsNothing(parameter) Then
                    w_query.AddToQueryParameterForSelect(parameter)
                End If


                'fill the list
                w_entityfiller.FillList(list, "records", w_query)


            Catch ex As Exception
                Throw
            Finally
                CloseDB()
            End Try
        End Sub

        Public Sub GetRecordsPump(ByVal list As List(Of PumpRecord), ByVal addtionalparameter As String)
            Try
                'create the filler
                Dim w_entityfiller As New EntityFiller(Of PumpRecord)
                w_entityfiller.DataBase = _database
                'create a new object to select the fields with -7
                Dim _selectedfields As New PumpRecord
                _selectedfields.IDRecord = -7
                _selectedfields.DatePumpRecord = New Date(1939, 9, 1)
                _selectedfields.Amps = -7
                _selectedfields.Comment = "-7"
                _selectedfields.DischargePressure = -7
                _selectedfields.IDPump = -7
                _selectedfields.PumpDesc = "-7"
                _selectedfields.Flag = True
                _selectedfields.RecordModifyBy = "-7"
                _selectedfields.DateCaptured = New Date(1939, 9, 1)
                _selectedfields.RecordCreatedBy = "-7"


                'create the select
                Dim w_query As New QueryBuilder(Of PumpRecord)
                w_query.TypeQuery = TypeQuery.SelectInfo
                w_query.Entity = _selectedfields
                w_query.BuildSelect("records")
                'set aditional parameter


                'fill the list
                w_entityfiller.FillList(list, "records", w_query)


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
