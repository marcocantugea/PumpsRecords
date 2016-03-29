Imports System.Windows.Forms.DataVisualization.Charting

Public Class frmChartsMesu

    Dim l_list As New List(Of PumpsRecords.entities.PumpRecord)
    Public g_logged As Boolean
    Public g_type As String
    Public b_editdata As Boolean = False
    Public _pumpSelected As PumpsRecords.entities.pump
    Private _value_before_edit As String = ""

    Private Sub frmChartsMesu_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        'resize the pump data view records
        dgv_pumprecords.Size = New System.Drawing.Size(Me.Size.Width - 30, Me.Size.Height * 0.4)


        ' position of the chart
        '
        Dim y As Double
        y = 55 + ComboBox1.Size.Height + dgv_pumprecords.Size.Height
        Chart1.Location = New System.Drawing.Point(Chart1.Location.X, y)

        'resize the chart
        Chart1.Size = New System.Drawing.Size(Me.Size.Width - 30, Me.Size.Height * 0.35)

        'position of the check boxes
        RadioButton1.Location = New System.Drawing.Point(RadioButton1.Location.X, y - 25)

        RadioButton2.Location = New System.Drawing.Point(RadioButton2.Location.X, y - 25)

    End Sub

    Private Sub frmChartsMesu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadGridStructureRecords()
        LoadPumps()
        LoadGridRecords()
        'DrawGraph(1)
    End Sub

    Public Sub LoadPumps()
        Dim o_adopumps As New PumpsRecords.data.ADO.ADOPumpsRecords
        Dim l_pumps As New PumpsRecords.entities.pumpsCollection
        o_adopumps.GetAllPumps(l_pumps)

        ComboBox1.DataSource = l_pumps.Items
        ComboBox1.ValueMember = "IDPump"
        ComboBox1.DisplayMember = "Description"

    End Sub

    Public Sub LoadGridRecords()
        dgv_pumprecords.Rows.Clear()
        l_list = Nothing
        l_list = New List(Of PumpsRecords.entities.PumpRecord)

        Dim o_adorecords As New PumpsRecords.data.ADO.ADORecordPump

        Dim pump As String
        pump = CType(ComboBox1.SelectedItem, PumpsRecords.entities.pump).IDPump
        Dim parameter As String
        parameter = "IDPump = " & pump

        o_adorecords.GetAllRecordsPump(l_list, parameter)

        For Each item As PumpsRecords.entities.PumpRecord In l_list
            Dim row As String() = New String() {item.DatePumpRecord.ToString("MM/dd/yyyy"), item.PumpDesc, item.DischargePressure, item.Amps, item.Comment, item.IDRecord}
            dgv_pumprecords.Rows.Add(row)
        Next

    End Sub

    Public Sub LoadGridStructureRecords()
        dgv_pumprecords.ColumnCount = 6
        dgv_pumprecords.Columns(0).Name = "Date"
        dgv_pumprecords.Columns(1).Name = "Pump"
        dgv_pumprecords.Columns(2).Name = "Discharge Pressure"
        dgv_pumprecords.Columns(3).Name = "Amps"
        dgv_pumprecords.Columns(4).Name = "Comment"
        dgv_pumprecords.Columns(5).Name = "ID"

        Dim row As String() = New String() {"", "", "", "", "", ""}
        dgv_pumprecords.Rows.Add(row)
        dgv_pumprecords.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv_pumprecords.Columns(5).ReadOnly = True
    End Sub

    Public Sub DrawGraph(ByVal charoption As Integer)

        Chart1.Series.Clear()
        Chart1.Legends.Clear()


        Dim _chartlegend1 As New Legend
        Dim _chartSerie As New Series


        If charoption = 1 Then
            'Define the legends
            _chartlegend1.Name = "Leyenda1"
            Chart1.Legends.Add(_chartlegend1)
            Chart1.Legends("Leyenda1").Docking = Docking.Bottom

            'set up serie
            _chartSerie.ChartArea = "ChartArea1"
            _chartSerie.Legend = "Leyenda1"
            _chartSerie.Name = "Discharge Pressure"
            Chart1.Series.Add(_chartSerie)


            Chart1.DataSource = l_list
            Chart1.ChartAreas(0).AxisY.Minimum = GetTheLowerValueDispump(l_list) - 3
            Chart1.ChartAreas(0).AxisY.Maximum = GetTheMaxValueDispump(l_list) + 3
            Chart1.Series("Discharge Pressure").IsValueShownAsLabel = True
            Chart1.Series("Discharge Pressure").MarkerStyle = MarkerStyle.Diamond
            Chart1.Series("Discharge Pressure").MarkerSize = 10
            Chart1.Series("Discharge Pressure").MarkerColor = Color.DimGray

            Chart1.Series("Discharge Pressure").XValueMember = "DatePumpRecord"
            Chart1.Series("Discharge Pressure").YValueMembers = "DischargePressure"
            Chart1.Series("Discharge Pressure").Color = Color.Blue
            Chart1.Series("Discharge Pressure").ChartType = SeriesChartType.Line
            Chart1.Series("Discharge Pressure").BorderWidth = 4
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Presure bars"
        End If


        If charoption = 2 Then
            'Define the legends
            Dim _chartlegend2 As New Legend
            _chartlegend2.Name = "Leyenda2"
            Chart1.Legends.Add(_chartlegend2)
            Chart1.Legends("Leyenda2").Docking = Docking.Bottom
            Chart1.ChartAreas(0).AxisY.Minimum = GetTheLowerValueAmps(l_list) - 3
            Chart1.ChartAreas(0).AxisY.Maximum = GetTheMaxValueAmps(l_list) + 3
            'set up serie
            Dim _chartSerie2 As New Series
            _chartSerie2.ChartArea = "ChartArea1"
            _chartSerie2.Legend = "Leyenda2"
            _chartSerie2.Name = "Amps"
            Chart1.Series.Add(_chartSerie2)

            Chart1.Series("Amps").IsValueShownAsLabel = True
            Chart1.Series("Amps").MarkerStyle = MarkerStyle.Diamond
            Chart1.Series("Amps").MarkerSize = 10
            Chart1.Series("Amps").MarkerColor = Color.DimGray

            'set the data source
            Chart1.Series("Amps").IsValueShownAsLabel = True
            Chart1.Series("Amps").XValueMember = "DatePumpRecord"
            Chart1.Series("Amps").YValueMembers = "Amps"
            Chart1.Series("Amps").Color = Color.Red
            Chart1.Series("Amps").ChartType = SeriesChartType.Line
            Chart1.Series("Amps").BorderWidth = 4
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "AMPS"
            ' Create Chart Area
        End If






    End Sub


    Private Sub TextBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Enter
        MonthCalendar1.Visible = True
    End Sub

    Private Sub TextBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.Enter
        MonthCalendar2.Visible = True
    End Sub


    Private Sub MonthCalendar1_DateSelected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar1.DateSelected
        TextBox1.Text = MonthCalendar1.SelectionRange.Start
        MonthCalendar1.Visible = False
    End Sub

    Private Sub MonthCalendar2_DateSelected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar2.DateSelected
        TextBox2.Text = MonthCalendar2.SelectionRange.Start
        MonthCalendar2.Visible = False
    End Sub

    Private Sub RadioButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.Click
        DrawGraph(2)
    End Sub

    Private Sub RadioButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.Click
        DrawGraph(1)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

        dgv_pumprecords.Rows.Clear()
        l_list = Nothing

        l_list = New List(Of PumpsRecords.entities.PumpRecord)
        Dim o_adorecords As New PumpsRecords.data.ADO.ADORecordPump

        Dim pump As String
        pump = CType(ComboBox1.SelectedItem, PumpsRecords.entities.pump).IDPump
        Dim parameter As String
        parameter = "IDPump = " & pump & " order by DatePumpRecord"

        o_adorecords.GetAllRecordsPump(l_list, parameter)

        For Each item As PumpsRecords.entities.PumpRecord In l_list
            Dim row As String() = New String() {item.DatePumpRecord.ToString("MM/dd/yyyy"), item.PumpDesc, item.DischargePressure, item.Amps, item.Comment, item.IDRecord}
            dgv_pumprecords.Rows.Add(row)
        Next

        DrawGraph(1)
        RadioButton1.Checked = True

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        dgv_pumprecords.Rows.Clear()

        Dim parameter As String = ""
        Dim pump As String
        pump = CType(ComboBox1.SelectedItem, PumpsRecords.entities.pump).IDPump

        Dim startdate As Date
        Dim b_startdate As Boolean

        Dim enddate As Date
        Dim b_enddate As Boolean

        If TextBox1.Text = "" Then
            b_startdate = False
        Else
            b_startdate = True
            startdate = Date.Parse(TextBox1.Text)
        End If

        If TextBox2.Text = "" Then
            b_enddate = False
        Else
            b_enddate = True
            enddate = Date.Parse(TextBox2.Text)

        End If

        If b_startdate And b_enddate Then
            parameter = " IDPump=" & pump & " and DatePumpRecord between #" & startdate.ToString("MM/dd/yyyy") & "# and  #" & enddate.ToString("MM/dd/yyyy") & "# order by DatePumpRecord"
        End If

        If b_startdate And Not b_enddate Then
            parameter = " IDPump=" & pump & " and DatePumpRecord between #" & startdate.ToString("MM/dd/yyyy") & "# and  #" & Date.Now.ToString("MM/dd/yyyy") & "# order by DatePumpRecord"
        End If


        If Not b_startdate And b_enddate Then
            parameter = " IDPump=" & pump & " and DatePumpRecord between #" & DateAdd(DateInterval.Month, -1, enddate).ToString("MM/dd/yyyy") & "# and  #" & enddate.ToString("MM/dd/yyyy") & "# order by DatePumpRecord"
        End If

        If Not b_startdate And Not b_enddate Then
            parameter = " IDPump=" & pump & " and DatePumpRecord between #" & DateAdd(DateInterval.Month, -1, Date.Now).ToString("MM/dd/yyyy") & "# and  #" & Date.Now.ToString("MM/dd/yyyy") & "# order by DatePumpRecord"
        End If

        Dim o_adorecords As New PumpsRecords.data.ADO.ADORecordPump
        l_list = Nothing
        l_list = New List(Of PumpsRecords.entities.PumpRecord)

        o_adorecords.GetAllRecordsPump(l_list, parameter)

        For Each item As PumpsRecords.entities.PumpRecord In l_list
            Dim row As String() = New String() {item.DatePumpRecord.ToString("MM/dd/yyyy"), item.PumpDesc, item.DischargePressure, item.Amps, item.Comment, item.IDRecord}
            dgv_pumprecords.Rows.Add(row)
        Next

        DrawGraph(1)
        RadioButton1.Checked = True

    End Sub

    Private Sub dgv_pumprecords_CellBeginEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgv_pumprecords.CellBeginEdit
        If g_logged Then
            If g_type.Equals("Edit") Or g_type.Equals("Admin") Then
                b_editdata = True
                If e.ColumnIndex = 1 Then
                    e.Cancel = True
                    dgv_pumprecords.Columns(e.ColumnIndex).ReadOnly = True
                    SelectPump.ShowDialog()
                    If SelectPump.DialogResult = Windows.Forms.DialogResult.OK Then
                        _pumpSelected = CType(SelectPump.ComboBox1.SelectedItem, PumpsRecords.entities.pump)
                        dgv_pumprecords.Rows(e.RowIndex).Cells(1).Value = _pumpSelected.Description

                        Dim u_pumprecord As New PumpsRecords.entities.PumpRecord

                        u_pumprecord.IDRecord = dgv_pumprecords.Rows(e.RowIndex).Cells(5).Value
                        u_pumprecord.DatePumpRecord = Date.Parse(dgv_pumprecords.Rows(e.RowIndex).Cells(0).Value)
                        u_pumprecord.IDPump = _pumpSelected.IDPump
                        u_pumprecord.PumpDesc = _pumpSelected.Description

                        Dim o_ado As New PumpsRecords.data.ADO.ADORecordPump
                        o_ado.UpdateRecord(u_pumprecord)

                    End If
                    dgv_pumprecords.Columns(e.ColumnIndex).ReadOnly = False
                Else
                    _value_before_edit = dgv_pumprecords.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                End If
            Else
                MsgBox("No Access to edit data, please loggin to modify data", MsgBoxStyle.Critical, "Access deny")
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub dgv_pumprecords_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_pumprecords.CellEndEdit
        Dim value As String
        Dim o_ado As New PumpsRecords.data.ADO.ADORecordPump

        If e.ColumnIndex = 0 Then
            value = _value_before_edit
            Try
                Dim u_pumprecord As New PumpsRecords.entities.PumpRecord
                u_pumprecord.IDRecord = dgv_pumprecords.Rows(e.RowIndex).Cells(5).Value
                u_pumprecord.DatePumpRecord = Date.Parse(dgv_pumprecords.Rows(e.RowIndex).Cells(0).Value)
                u_pumprecord.IDPump = -1
                o_ado.UpdateRecord(u_pumprecord)

            Catch ex As Exception
                dgv_pumprecords.Rows(e.RowIndex).Cells(0).Value = value
                MsgBox("Error in the input please verify the input", MsgBoxStyle.Critical, "Input Error")

            End Try
        End If

        If e.ColumnIndex = 2 Or e.ColumnIndex = 3 Or e.ColumnIndex = 4 Then
            value = _value_before_edit
            Try
                Dim u_pumprecord As New PumpsRecords.entities.PumpRecord
                u_pumprecord.IDRecord = dgv_pumprecords.Rows(e.RowIndex).Cells(5).Value
                u_pumprecord.DischargePressure = Double.Parse(dgv_pumprecords.Rows(e.RowIndex).Cells(2).Value)
                u_pumprecord.Amps = Double.Parse(dgv_pumprecords.Rows(e.RowIndex).Cells(3).Value)
                u_pumprecord.Comment = dgv_pumprecords.Rows(e.RowIndex).Cells(4).Value
                u_pumprecord.IDPump = -1
                o_ado.UpdateRecord(u_pumprecord)

            Catch ex As Exception
                dgv_pumprecords.Rows(e.RowIndex).Cells(2).Value = value
                MsgBox("Error in the input please verify the input", MsgBoxStyle.Critical, "Input Error")

            End Try
        End If

    End Sub

    Private Sub dgv_pumprecords_UserDeletingRow(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles dgv_pumprecords.UserDeletingRow
        If g_logged Then
            If MsgBox("Are you sure to remove this record?", MsgBoxStyle.YesNo, "Deleting Record") = MsgBoxResult.Yes Then
                Try
                    Dim u_pumprecord As New PumpsRecords.entities.PumpRecord
                    u_pumprecord.IDRecord = e.Row.Cells(5).Value
                    Dim o_ado As New PumpsRecords.data.ADO.ADORecordPump
                    o_ado.DeleteRecord(u_pumprecord)

                Catch ex As Exception
                    e.Cancel = True
                    MsgBox("Error trying to delete the record", MsgBoxStyle.Critical, "Error")
                End Try
            Else
                e.Cancel = True
            End If
        Else
            MsgBox("No Access to edit data, please loggin to modify data", MsgBoxStyle.Critical, "Access deny")
            e.Cancel = True
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim _excel As New PumpsRecords.file.export.ExcelExport

        _excel.ExportData(l_list)
        ' _excel.CloseDocument()

    End Sub

    Private Function GetTheLowerValueDispump(ByVal list As List(Of PumpsRecords.entities.PumpRecord)) As Double
        Dim value As Double = 0
        For Each item As PumpsRecords.entities.PumpRecord In list
            If value = 0 Then
                value = item.DischargePressure
            Else
                If value > item.DischargePressure Then
                    value = item.DischargePressure
                End If
            End If
        Next
        Return value
    End Function

    Private Function GetTheMaxValueDispump(ByVal list As List(Of PumpsRecords.entities.PumpRecord)) As Double
        Dim value As Double = 0
        For Each item As PumpsRecords.entities.PumpRecord In list
            If value = 0 Then
                value = item.DischargePressure
            Else
                If value < item.DischargePressure Then
                    value = item.DischargePressure
                End If
            End If
        Next
        Return value
    End Function
    Private Function GetTheLowerValueAmps(ByVal list As List(Of PumpsRecords.entities.PumpRecord)) As Double
        Dim value As Double = 0
        For Each item As PumpsRecords.entities.PumpRecord In list
            If value = 0 Then
                value = item.Amps
            Else
                If value > item.Amps Then
                    value = item.Amps
                End If
            End If
        Next
        Return value
    End Function

    Private Function GetTheMaxValueAmps(ByVal list As List(Of PumpsRecords.entities.PumpRecord)) As Double
        Dim value As Double = 0
        For Each item As PumpsRecords.entities.PumpRecord In list
            If value = 0 Then
                value = item.Amps
            Else
                If value < item.Amps Then
                    value = item.Amps
                End If
            End If
        Next
        Return value
    End Function


End Class