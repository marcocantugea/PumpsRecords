Public Class frm_cap_disamp

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub MonthCalendar1_DateSelected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar1.DateSelected
        TextBox1.Text = MonthCalendar1.SelectionRange.Start

        MonthCalendar1.Visible = False

    End Sub

    Private Sub TextBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Enter
        MonthCalendar1.Visible = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim new_record As New PumpsRecords.entities.PumpRecord
            Dim sel_pump As PumpsRecords.entities.pump
            sel_pump = ComboBox1.SelectedItem
            new_record.PumpDesc = sel_pump.Description
            new_record.IDPump = sel_pump.IDPump
            new_record.DatePumpRecord = Date.Parse(TextBox1.Text)
            new_record.DischargePressure = Double.Parse(TextBox2.Text)
            new_record.Amps = Double.Parse(TextBox3.Text)
            new_record.Comment = TextBox4.Text
            new_record.DateCaptured = Date.Now()
            new_record.Flag = CheckBox1.Checked
            frmCapBy._new_record = new_record
            frmCapBy.Show()
        Catch ex As Exception
            MsgBox("Error on captured Data, please check the information.", MsgBoxStyle.Critical, "Error on input data")
        End Try


    End Sub

    Private Sub frm_cap_disamp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadPumps()
    End Sub

    Public Sub LoadPumps()
        Dim o_adopumps As New PumpsRecords.data.ADO.ADOPumpsRecords
        Dim l_pumps As New PumpsRecords.entities.pumpsCollection
        o_adopumps.GetAllPumps(l_pumps)

        ComboBox1.DataSource = l_pumps.Items
        ComboBox1.ValueMember = "IDPump"
        ComboBox1.DisplayMember = "Description"

    End Sub

End Class