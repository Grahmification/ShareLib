Public Class NewUserForm

    Private Sub Button_AddNewUser_Click(sender As Object, e As EventArgs) Handles Button_AddNewUser.Click, Btn_Cancel.Click
        Dim sendbtn As Button = sender

        If sendbtn Is Button_AddNewUser Then
            Users2.Add(TextBox_NewUserName.Text, TextBox_NewUserIP.Text)
        End If

        Me.Close()
    End Sub

End Class