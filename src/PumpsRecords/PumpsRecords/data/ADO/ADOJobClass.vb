
Imports PumpsRecords.entities
Imports PumpsRecords.data.dao

Namespace data.ADO
    Public Class ADOJobClass
        Inherits data.dao.OleDBConnectionObj

        Private _database As String = "DB-PUMPSRECORD"

        Public Sub GetJobClass(ByVal listofClass As JobClassCollection)
            Try
                'create the filler
                Dim w_entityfiller As New EntityFiller(Of JobClass)
                w_entityfiller.DataBase = _database
                'create a new object to select the fields with -7
                Dim _selectedfields As New JobClass
                _selectedfields.IDClass = -7
                _selectedfields.ClassName = "-7"

                'create the select
                Dim w_query As New QueryBuilder(Of JobClass)
                w_query.TypeQuery = TypeQuery.SelectInfo
                w_query.Entity = _selectedfields
                w_query.BuildSelect("class")


                'fill the list
                w_entityfiller.FillList(listofClass.Items, "pumps", w_query)


            Catch ex As Exception
                Throw
            Finally
                CloseDB()
            End Try
        End Sub


    End Class
End Namespace
