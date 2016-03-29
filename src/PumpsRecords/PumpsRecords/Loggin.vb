Imports System.Windows.Forms

Public Class Loggin

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        PumpsRecords.main.typed_pass = MaskedTextBox1.Text
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub


    Private Sub Loggin_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        MaskedTextBox1.Text = ""
        MaskedTextBox1.Focus()
    End Sub
End Class
