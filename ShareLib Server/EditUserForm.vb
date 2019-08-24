Public Class EditUserForm

    Private _edituserkey As String = Nothing

    Public Sub New(ByVal userKey As String)
        InitializeComponent()
        _edituserkey = userKey

        TextBox_key.Text = userKey
        TextBox_Name.Text = users2.Name(userKey)
        TextBox_IP.Text = users2.Ip(userKey)

        Me.Visible = True
        Me.Show()
    End Sub
    Private Sub Button_Update_Click(sender As Object, e As EventArgs) Handles Button_Update.Click, Button_Cancel.Click

        Dim sendbtn As Button = sender

        If sendbtn Is Button_Update Then
            users2.Delete(_edituserkey)
            users2.Add(TextBox_Name.Text, TextBox_IP.Text, TextBox_key.Text)
        End If

        Me.Close()

    End Sub
    Private Sub Button_KeyGen_Click(sender As Object, e As EventArgs) Handles Button_KeyGen.Click
        TextBox_key.Text = users2.CreateNewID
    End Sub

End Class