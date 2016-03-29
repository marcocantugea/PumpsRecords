Public Class frmCapBy

    Public _new_record As New PumpsRecords.entities.PumpRecord

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()


    End Sub

    Private Sub frmCapBy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadClasses()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Not IsNothing(_new_record) Then

            If Not IsNothing(ComboBox1.SelectedItem) Then
                _new_record.RecordCreatedBy = CType(ComboBox1.SelectedItem, PumpsRecords.entities.JobClass).ClassName
                Dim _ADORecordPump As New PumpsRecords.data.ADO.ADORecordPump

                Try
                    _ADORecordPump.InsertRecord(_new_record)
                    MsgBox("Record Saved", MsgBoxStyle.Information, "Record Saved")
                    Me.Hide()
                    frm_cap_disamp.Dispose()
                Catch ex As Exception
                    Throw
                End Try

            Else
                MsgBox("Please Select an option", MsgBoxStyle.Critical, "Select an option")
            End If
        End If
    End Sub

    Public Sub LoadClasses()
        Dim o_adoJobClass As New PumpsRecords.data.ADO.ADOJobClass
        Dim l_classes As New PumpsRecords.entities.JobClassCollection

        o_adoJobClass.GetJobClass(l_classes)

        ComboBox1.DataSource = l_classes.Items
        ComboBox1.ValueMember = "IDClass"
        ComboBox1.DisplayMember = "ClassName"


    End Sub



End Class