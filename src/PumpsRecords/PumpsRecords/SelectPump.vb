Imports System.Windows.Forms

Public Class SelectPump

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        frmChartsMesu._pumpSelected = CType(ComboBox1.SelectedItem, PumpsRecords.entities.pump)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Sub LoadPumps()
        Dim o_adopumps As New PumpsRecords.data.ADO.ADOPumpsRecords
        Dim l_pumps As New PumpsRecords.entities.pumpsCollection
        o_adopumps.GetAllPumps(l_pumps)

        ComboBox1.DataSource = l_pumps.Items
        ComboBox1.ValueMember = "IDPump"
        ComboBox1.DisplayMember = "Description"

    End Sub

    Private Sub SelectPump_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadPumps()
    End Sub
End Class
