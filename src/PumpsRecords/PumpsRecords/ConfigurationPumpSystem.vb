Public Class ConfigurationPumpSystem

    Dim appconfig As New PumpsRecords.configuration.extras.AppConfigFileSettings

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Browsdatabase.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fi As New System.IO.FileInfo(Browsdatabase.FileName)
            TextBox1.Text = fi.FullName
        End If

    End Sub

    Public Sub loadparameters()

        Dim connectionstring As String = System.Configuration.ConfigurationSettings.AppSettings("DB-PUMPSRECORD")
        Dim val As String()
        val = connectionstring.Split(";")
        Dim path As String
        path = val(1).Substring(12, val(1).Length - 12)
        TextBox1.Text = path
        TextBox2.Text = System.Configuration.ConfigurationSettings.AppSettings("AdminPass")
        TextBox3.Text = System.Configuration.ConfigurationSettings.AppSettings("Edit")
    End Sub

    Private Sub ConfigurationPumpSystem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadparameters()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim conectionstring As String
            conectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & TextBox1.Text
            appconfig.UpdateAppSettings("DB-DDR", conectionstring)
            MsgBox("Configuration Saved", MsgBoxStyle.Information, "Configuration Saved")
        Catch ex As Exception
            MsgBox("Error trying to save configuration", MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try

            appconfig.UpdateAppSettings("AdminPass", TextBox2.Text)
            MsgBox("Configuration Saved", MsgBoxStyle.Information, "Configuration Saved")
        Catch ex As Exception
            MsgBox("Error trying to save configuration", MsgBoxStyle.Critical, "Error")

        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try

            appconfig.UpdateAppSettings("Edit", TextBox3.Text)
            MsgBox("Configuration Saved", MsgBoxStyle.Information, "Configuration Saved")
        Catch ex As Exception
            MsgBox("Error trying to save configuration", MsgBoxStyle.Critical, "Error")

        End Try
    End Sub
End Class