Imports System.Threading
Imports System.Text
Imports System.IO
Public Class Startup_Form

    Public Delegate Sub AppendRichTextBox_Del(ByVal RTB As RichTextBox, ByVal text As String)
    Public Delegate Sub ShowForm_Del(ByVal NameConfirmed As Boolean, ByVal MusicPathConfirmed As Boolean)

    Public Event SetupCompleted(ByVal Name As String, ByVal mykey As String, ByVal MusicPath As String, ByVal ReceivePath As String)
    Public Event SetupClosed()

    Private _Name As String = Nothing
    Private _Mykey As String = Nothing
    Private _MusicPath As String = Nothing
    Private _ReceivePath As String = Nothing

    Private _NameConfirmed As Boolean = False
    Private _MusicPathConfirmed As Boolean = False

    Public Sub AppendRichTextBox(ByVal RTB As RichTextBox, ByVal text As String)
        If RTB.InvokeRequired Then
            Invoke(New AppendRichTextBox_Del(AddressOf AppendRichTextBox), RTB, text)
        Else
            RTB.AppendText(text)
            RTB.AppendText(vbNewLine)
            RTB.ScrollToCaret()
        End If
    End Sub
    Public Sub ShowForm(ByVal NameConfirmed As Boolean, ByVal MusicPathConfirmed As Boolean)

        _MusicPathConfirmed = MusicPathConfirmed
        _NameConfirmed = NameConfirmed

        If Me.InvokeRequired Then
            Invoke(New ShowForm_Del(AddressOf ShowForm), NameConfirmed, MusicPathConfirmed)
        Else
            If _NameConfirmed = False Or _MusicPathConfirmed = False Then
                If Me.Visible = False Then
                    Me.Visible = True
                    Me.Show()
                    Me.BringToFront()
                End If

                If _NameConfirmed = True Then
                    TabControl_Setup.TabPages.Remove(TabPage1)
                End If

                If _MusicPathConfirmed = True Then
                    TabControl_Setup.TabPages.Remove(TabPage2)
                End If
            Else
                On_Success() 'run exiting thread
            End If
        End If
    End Sub
    Public Sub On_Success()
        RaiseEvent SetupCompleted(_Name, _Mykey, _MusicPath, _ReceivePath)
        Me.Close()
        Me.Dispose()
    End Sub 'setup successful 
    Private Sub Startup_Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If _Name = Nothing Or _MusicPath = Nothing Then
            Dim result As Integer = MessageBox.Show("Setup must be completed before the program can be used. Are you sure you want to exit?", "Exit setup?", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                RaiseEvent SetupClosed()
            Else
                e.Cancel = True
            End If
        Else
        End If
    End Sub

#Region "Name and password tab"

    Private Sub Btn_ServerPassword_Click(sender As Object, e As EventArgs) Handles Btn_ServerPassword.Click
        Dim T1 As New Thread(AddressOf Confirm_PassWord)
        T1.Start()
    End Sub
    Private Sub Confirm_PassWord()
        Try
            Dim name As String = TextBox_Setup_Name_First.Text & " " & TextBox_Name_Last.Text
            Dim pw As String = TextBox_ServerPassword.Text

            Dim response As String = ConfirmServer(pw, name)

            If response = "Password Incorrect" Or response = "The name is already in use." Then
                MessageBox.Show(response)
            Else
                MessageBox.Show("Success!")
                _NameConfirmed = True
                _Mykey = response
                _Name = name
                Me.ShowForm(_NameConfirmed, _MusicPathConfirmed)
            End If

        Catch ex As TimeoutException
            MessageBox.Show("Could not connect to server.", "Unable to connect.")
        Catch ex As System.IO.IOException
            MessageBox.Show("Could not connect to server.", "Unable to connect.")
        Catch ex As Exception

        End Try

    End Sub
    Private Function ConfirmServer(ByVal PassWord As String, ByVal Name As String) As String

        Dim receivestring As String = Nothing
        Dim connection As New ClientConnection(ServerIP, Serverport, False)

        connection.Send_Bytes(Conversions.ObjectToBytes(PassWord))
        receivestring = Conversions.BytesToObject(connection.Receive_Bytes())

        If receivestring = "Password Incorrect" Then
            Return receivestring
        End If

        connection.Send_Bytes(Conversions.ObjectToBytes(Name))
        receivestring = Conversions.BytesToObject(connection.Receive_Bytes())

        Return receivestring
    End Function

    Private Sub EnableConfirmButton() Handles TextBox_Name_Last.TextChanged, TextBox_Setup_Name_First.TextChanged, TextBox_ServerPassword.TextChanged

        If TextBox_Name_Last.Text <> "" And TextBox_ServerPassword.Text <> "" And TextBox_Setup_Name_First.Text <> "" Then
            Btn_ServerPassword.Enabled = True
        Else
            Btn_ServerPassword.Enabled = False
        End If

    End Sub

#End Region

#Region "Media Loc Tab"
    Private Sub TextBox_ItunesMediaLoc_TextChanged(sender As Object, e As EventArgs) Handles TextBox_ItunesMediaLoc.TextChanged
        If TextBox_ItunesMediaLoc.Text = "" Then
            Btn_ConfirmMediaLoc.Enabled = False
        Else
            Btn_ConfirmMediaLoc.Enabled = True
        End If
    End Sub

    Private Sub Button_Browse1_Click(sender As Object, e As EventArgs) Handles Button_Browse1.Click
        Dim fd As New FolderBrowserDialog
        fd.ShowDialog()
        If fd.SelectedPath <> "" Then
            TextBox_ItunesMediaLoc.Text = fd.SelectedPath
        End If
    End Sub

    Private Sub Btn_ConfirmMediaLoc_Click(sender As Object, e As EventArgs) Handles Btn_ConfirmMediaLoc.Click
        Dim T1 As New Thread(AddressOf Test_Media_Folder)
        T1.Start(TextBox_ItunesMediaLoc.Text)
    End Sub

    Private Sub Test_Media_Folder(ByVal path As String)

        Dim mp3num As Integer = 0
        Dim AddToItunesFolder As Integer = 0

        If Directory.Exists(path) Then
            Try
                AppendRichTextBox(RichTextBox_ItunesLoc, "Analyzing folder. This may take a few minutes if a 5000+ songs are present.")
                Dim dirs As String() = Directory.GetDirectories(path, "Automatically Add to iTunes", SearchOption.AllDirectories)
                AddToItunesFolder = dirs.Count

                If AddToItunesFolder > 0 Then
                    AppendRichTextBox(RichTextBox_ItunesLoc, "Found auto add to itunes directory.")
                Else
                    AppendRichTextBox(RichTextBox_ItunesLoc, "Could not find auto add to itunes directory.")
                End If

                mp3num = GetNumFiles(path)
                AppendRichTextBox(RichTextBox_ItunesLoc, "Found " & mp3num & " mp3 files in directory.")

                Dim result As Integer = MessageBox.Show("Found " & mp3num & " music files in this folder. This number should be similar to the number of songs on your itunes. Is this correct?", "Folder Correct?", MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then
                    _MusicPathConfirmed = True
                    _MusicPath = path
                    If AddToItunesFolder > 0 Then
                        _ReceivePath = dirs(0)
                    Else
                        _ReceivePath = path
                    End If
                    MessageBox.Show("Selection complete. Music will download to <" & _ReceivePath & ">. This path can be changed in the file dropdown menu.", "Download location")
                    Me.ShowForm(_NameConfirmed, _MusicPathConfirmed)
                End If

            Catch
                AppendRichTextBox(RichTextBox_ItunesLoc, "Error analyzing folder.")
            End Try
        Else
            AppendRichTextBox(RichTextBox_ItunesLoc, "The directory does not exist")
        End If

    End Sub
    Public Function GetNumFiles(ByVal Path As String) As Integer
        Dim output As Integer = 0
        If Directory.Exists(Path) Then
            output = Directory.GetFiles(Path, "*.mp3*", SearchOption.AllDirectories).Count
        End If
        Return output
    End Function

#End Region




End Class