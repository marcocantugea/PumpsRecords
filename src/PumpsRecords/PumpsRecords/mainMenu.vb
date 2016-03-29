Public Class main

    Dim g_logged As Boolean = False
    ' Types
    ' Read - Just Read no modification on data
    ' Edit - can edit but not enter to configuration
    ' Admin - Can edit and enter to configuration
    Dim g_type As String = "Read"
    Public typed_pass As String = ""

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        frm_cap_disamp.Show()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        frmChartsMesu.g_logged = g_logged
        frmChartsMesu.g_type = g_type
        frmChartsMesu.Show()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        frmPumpsConf.g_logged = g_logged
        frmPumpsConf.g_type = g_type
        frmPumpsConf.Show()

    End Sub


    Private Sub Button1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Button1.KeyDown
        If e.KeyCode = Keys.F7 Then
            If Not g_logged Then
                Loggin.ShowDialog()
                If Loggin.DialogResult = Windows.Forms.DialogResult.OK Then
                    If typed_pass = System.Configuration.ConfigurationSettings.AppSettings("AdminPass") Then
                        g_logged = True
                        g_type = "Admin"
                    End If
                    If typed_pass = System.Configuration.ConfigurationSettings.AppSettings("Edit") Then
                        g_logged = True
                        g_type = "Edit"
                    End If

                    If g_logged Then
                        MsgBox("Loggin Succesfully, you can modify the records and configuration", MsgBoxStyle.Information, "Loggin Successfully!")
                    Else
                        MsgBox("Error on password, please try again", MsgBoxStyle.Critical, "Error on Loggin!")
                    End If

                    'System.Configuration.ConfigurationSettings.AppSettings(s)
                End If
            End If
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If g_logged And g_type = "Admin" Then
            ConfigurationPumpSystem.Show()
        Else
            MsgBox("No Access to this option, please loggin as administrator", MsgBoxStyle.Critical, "Access deny")
        End If
    End Sub
End Class
