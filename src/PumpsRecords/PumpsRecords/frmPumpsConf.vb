Public Class frmPumpsConf

    Public _windowstate As Integer = -1
    Dim l_pumps As New PumpsRecords.entities.pumpsCollection
    Public g_logged As Boolean
    Public g_type As String

    Private Sub frmPumpsConf_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadGrids()
    End Sub

    Public Sub LoadGrids()
        LoadPumpsGrid()
        LoadPumpsFromDB()
    End Sub

    Public Sub LoadPumpsGrid()
        dgv_pumps.ColumnCount = 3
        dgv_pumps.Columns(0).Name = "Pump Description"
        dgv_pumps.Columns(1).Name = "Status"
        dgv_pumps.Columns(2).Name = "ID"

        Dim row As String() = New String() {"", "", ""}
        dgv_pumps.Rows.Add(row)
        dgv_pumps.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        dgv_pumps.Columns(2).ReadOnly = True
        dgv_pumps.Columns(0).Width = 200
        dgv_pumps.Columns(1).Width = 200
        dgv_pumps.Columns(2).Width = 35
        dgv_pumps.Columns(2).ReadOnly = True
        dgv_pumps.DefaultCellStyle.WrapMode = DataGridViewTriState.True

    End Sub


    Public Sub LoadPumpsFromDB()
        Dim o_adopumps As New PumpsRecords.data.ADO.ADOPumpsRecords


        o_adopumps.GetAllPumps(l_pumps)

        If l_pumps.Items.Count > 0 Then
            dgv_pumps.Rows.Clear()
        End If

        For Each item As PumpsRecords.entities.pump In l_pumps.Items
            Dim row As String() = New String() {item.Description, item.Status, item.IDPump}
            dgv_pumps.Rows.Add(row)

        Next

    End Sub


    Private Sub dgv_pumps_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_pumps.CellEndEdit
        If dgv_pumps.Rows(e.RowIndex).Cells(0).Value <> "" Then
            If IsNothing(dgv_pumps.Rows(e.RowIndex).Cells(2).Value) Then
                Dim new_pump As New PumpsRecords.entities.pump
                new_pump.Description = dgv_pumps.Rows(e.RowIndex).Cells(0).Value
                new_pump.Status = dgv_pumps.Rows(e.RowIndex).Cells(1).Value
                'new_pump.Active = -1
                Try
                    l_pumps.Add(new_pump, True)
                Catch ex As Exception
                    Throw
                End Try
                dgv_pumps.Rows(e.RowIndex).Cells(2).Value = new_pump.IDPump

            Else
                Dim new_pump As New PumpsRecords.entities.pump
                new_pump.Description = dgv_pumps.Rows(e.RowIndex).Cells(0).Value
                new_pump.Status = dgv_pumps.Rows(e.RowIndex).Cells(1).Value
                new_pump.IDPump = dgv_pumps.Rows(e.RowIndex).Cells(2).Value
                Dim m_adopump As New PumpsRecords.data.ADO.ADOPumpsRecords

                Try
                    m_adopump.UpdatePump(new_pump)
                Catch ex As Exception
                    Throw
                End Try
            End If
        End If
    End Sub

    Private Sub dgv_pumps_UserDeletedRow(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles dgv_pumps.UserDeletedRow

    End Sub

    Private Sub dgv_pumps_UserDeletingRow(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles dgv_pumps.UserDeletingRow
        If g_logged Then
            If MsgBox("Are you sure that you want to delete this record?", MsgBoxStyle.YesNo, "Delete Record") = MsgBoxResult.Yes Then
                Try
                    If Not IsNothing(e.Row.Cells(2).Value) Then
                        'Dim result As Integer = MsgBox("Do you want to remove the record?", MsgBoxStyle.OkCancel, "Remove record")
                        'If result = vbOK Then
                        Dim new_pump As New PumpsRecords.entities.pump
                        new_pump.Description = e.Row.Cells(0).Value
                        new_pump.Status = e.Row.Cells(1).Value
                        new_pump.IDPump = e.Row.Cells(2).Value
                        Try
                            l_pumps.Remove(new_pump, True)
                        Catch ex As Exception
                            Throw
                        End Try
                        'Else
                        'Throw New Exception("Cancel Row Transaction")
                        'End If
                    End If
                Catch ex As Exception

                End Try
            Else
                e.Cancel = True
            End If
        Else
            MsgBox("No Access to edit data, please loggin to modify data", MsgBoxStyle.Critical, "Access deny")
            e.Cancel = True
        End If
    End Sub

    Private Sub dgv_pumps_CellBeginEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgv_pumps.CellBeginEdit
        If g_logged Then
            If g_type.Equals("Edit") Or g_type.Equals("Admin") Then
            Else
                MsgBox("No Access to edit data, please loggin to modify data", MsgBoxStyle.Critical, "Access deny")
                e.Cancel = True
            End If
        Else
            MsgBox("No Access to edit data, please loggin to modify data", MsgBoxStyle.Critical, "Access deny")
            e.Cancel = True
        End If

    End Sub
End Class