Namespace data.dao
    Public Class EntityFiller(Of T)
        Inherits data.dao.OleDBConnectionObj


        Private _Entity As T
        Private _DataBase As String

        Public Property DataBase() As String
            Get
                Return _DataBase
            End Get
            Set(ByVal value As String)
                _DataBase = value
            End Set
        End Property

        Public ReadOnly Property Entity()
            Get
                Return _Entity
            End Get
        End Property

        Public Sub FillList(ByVal list As List(Of T), ByVal table As String, ByVal query As QueryBuilder(Of T))
            Try
                OpenDB(_DataBase)
                connection.Command = New OleDb.OleDbCommand(query.Query, connection.Connection)
                connection.Adap = New OleDb.OleDbDataAdapter(connection.Command)

                Dim dts As New DataSet
                connection.Adap.Fill(dts)


                If dts.Tables.Count > 0 Then
                    If dts.Tables(0).Rows.Count > 0 Then
                        For Each row As DataRow In dts.Tables(0).Rows
                            Dim _item = CreateNewItemAsT(Of T)()
                            For Each member In _item.GetType.GetProperties
                                If member.CanWrite Then
                                    'If member.PropertyType.Name = "String" Or member.PropertyType.Name = "Int32" Or member.PropertyType.Name = "DateTime" Or member.PropertyType.Name = "Boolean" Then
                                    If Not IsDBNull(row(member.Name)) Then
                                        If member.PropertyType.Name = "String" Then
                                            member.SetValue(_item, row(member.Name).ToString, Nothing)
                                        End If
                                        If member.PropertyType.Name = "Int32" Then
                                            member.SetValue(_item, Integer.Parse(row(member.Name)), Nothing)
                                        End If
                                        If member.PropertyType.Name = "Double" Then
                                            member.SetValue(_item, Double.Parse(row(member.Name)), Nothing)
                                        End If
                                        If member.PropertyType.Name = "DateTime" Then
                                            member.SetValue(_item, Date.Parse(row(member.Name)), Nothing)
                                        End If
                                        If member.PropertyType.Name = "Boolean" Then
                                            member.SetValue(_item, Boolean.Parse(row(member.Name)), Nothing)
                                        End If
                                    End If
                                    'End If
                                End If
                            Next
                            list.Add(_item)
                        Next
                    End If
                End If

            Catch ex As Exception
                Throw
            Finally
                CloseDB()
            End Try
        End Sub

        Public Sub FillList(ByVal list As List(Of T), ByVal query As String)
            Try
                OpenDB(_DataBase)
                connection.Command = New OleDb.OleDbCommand(query, connection.Connection)
                connection.Adap = New OleDb.OleDbDataAdapter(connection.Command)

                Dim dts As New DataSet
                connection.Adap.Fill(dts)

                If dts.Tables.Count > 0 Then
                    If dts.Tables(0).Rows.Count > 0 Then
                        For Each row As DataRow In dts.Tables(0).Rows
                            Dim _item = CreateNewItemAsT(Of T)()
                            For Each member In _item.GetType.GetProperties
                                If member.CanWrite Then
                                    If member.PropertyType.Name = "String" Or member.PropertyType.Name = "Int32" Or member.PropertyType.Name = "DateTime" Or member.PropertyType.Name = "Boolean" Then
                                        If Not IsDBNull(row(member.Name)) Then
                                            If member.PropertyType.Name = "String" Then
                                                member.SetValue(_item, row(member.Name).ToString, Nothing)
                                            End If
                                            If member.PropertyType.Name = "Int32" Then
                                                member.SetValue(_item, Integer.Parse(row(member.Name)), Nothing)
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                            list.Add(_item)
                        Next
                    End If
                End If

            Catch ex As Exception
                Throw
            Finally
                CloseDB()
            End Try
        End Sub

        Public Sub FillObj(ByVal _item As T, ByVal table As String, ByVal query As QueryBuilder(Of T))
            Try
                OpenDB(_DataBase)
                connection.Command = New OleDb.OleDbCommand(query.Query, connection.Connection)
                connection.Adap = New OleDb.OleDbDataAdapter(connection.Command)

                Dim dts As New DataSet
                connection.Adap.Fill(dts)


                If dts.Tables.Count > 0 Then
                    If dts.Tables(0).Rows.Count > 0 Then
                        For Each row As DataRow In dts.Tables(0).Rows
                            For Each member In _item.GetType.GetProperties
                                If member.CanWrite Then
                                    If member.PropertyType.Name = "String" Or member.PropertyType.Name = "Int32" Or member.PropertyType.Name = "DateTime" Or member.PropertyType.Name = "Boolean" Then
                                        If Not IsDBNull(row(member.Name)) Then
                                            If member.PropertyType.Name = "String" Then
                                                member.SetValue(_item, row(member.Name).ToString, Nothing)
                                            End If
                                            If member.PropertyType.Name = "Int32" Then
                                                member.SetValue(_item, Integer.Parse(row(member.Name)), Nothing)
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                        Next
                    End If
                End If

            Catch ex As Exception
                Throw
            Finally
                CloseDB()
            End Try
        End Sub

        Public Sub FillObj(ByVal _item As T, ByVal query As String)
            Try
                OpenDB(_DataBase)
                connection.Command = New OleDb.OleDbCommand(query, connection.Connection)
                connection.Adap = New OleDb.OleDbDataAdapter(connection.Command)

                Dim dts As New DataSet
                connection.Adap.Fill(dts)


                If dts.Tables.Count > 0 Then
                    If dts.Tables(0).Rows.Count > 0 Then
                        For Each row As DataRow In dts.Tables(0).Rows
                            For Each member In _item.GetType.GetProperties
                                If member.CanWrite Then
                                    If member.PropertyType.Name = "String" Or member.PropertyType.Name = "Int32" Or member.PropertyType.Name = "DateTime" Or member.PropertyType.Name = "Boolean" Then
                                        If Not IsDBNull(row(member.Name)) Then
                                            If member.PropertyType.Name = "String" Then
                                                member.SetValue(_item, row(member.Name).ToString, Nothing)
                                            End If
                                            If member.PropertyType.Name = "Int32" Then
                                                member.SetValue(_item, Integer.Parse(row(member.Name)), Nothing)
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                        Next
                    End If
                End If

            Catch ex As Exception
                Throw
            Finally
                CloseDB()
            End Try

        End Sub

        Public Function CreateNewItemAsT(Of T)() As T
            Dim tmp As T = GetType(T).GetConstructor(New System.Type() {}).Invoke(New Object() {})
            Return tmp
        End Function


    End Class
End Namespace