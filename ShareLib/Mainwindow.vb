Imports System.IO
Imports System.Text
Imports System.Threading
Public Class Mainwindow

    Private _MusicPathConfirmed As Boolean = False
    Private _NameConfirmed As Boolean = False
    Private Sub Mainwindow_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            DefaultForm = Me
            '----------------------- Try to load user and self data ---------------------
            Dim tmp As New DLManager_Form
            tmp.Show()
            Self.LoadFromFile(SelfSavePath)
            UL.LoadfromFile(ContactSavePath) 'load user list
            Populate_Listview(UL.Keys.ToArray(), ListView_Contacts) 'populate with all user keys
            'DlManager.Load_DLList_File(DlListSavePath)


            If File.Exists(ContactSavePath) And File.Exists(SelfSavePath) Then
                _MusicPathConfirmed = True
                _NameConfirmed = True
            End If

            If NotifyIcon_Main.Visible = False Then
                NotifyIcon_Main.Visible = True
            End If



            If _MusicPathConfirmed = False And _NameConfirmed = False Then
                Enable_Toolstrip(False, ToolStrip_Upper)
                Dim FS As New Startup_Form
                FS.ShowForm(_NameConfirmed, _MusicPathConfirmed)
                AddHandler FS.SetupCompleted, AddressOf SetupComplete
                AddHandler FS.SetupClosed, AddressOf SetupCancelled
            Else
                Enable_Toolstrip(True, ToolStrip_Upper)
                Dim bar As New SongLoadForm() 'update music list
            End If
        Catch ex As Exception
            Me.Close()
            MessageBox.Show("An error has occurred upon startup: " & ex.Data.ToString)
        End Try
    End Sub
    Private Sub Mainform_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        NotifyIcon_Main.Visible = False
        NotifyIcon_Main.Dispose()

        If _MusicPathConfirmed And _NameConfirmed Then
            Dim T1 As New Thread(AddressOf CloseForm)
            T1.IsBackground = False
            T1.Start()
        End If
    End Sub
    Private Sub CloseForm()
        Try
            If Connected = True Then
                Online(False)
                Thread.Sleep(1000)
            End If

            Self.SavetoFile(SelfSavePath)
            UL.SaveToFile(ContactSavePath)  'save the contacts list
            'DlManager.Save_DLList_File(DlListSavePath)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetupComplete(ByVal Name As String, ByVal mykey As String, ByVal MusicPath As String, ByVal ReceivePath As String)

        MessageBox.Show("Snatcher will now scan your itunes media folder in the background. This may take a few minutes if a large amount of music is present.")

        _MusicPathConfirmed = True
        _NameConfirmed = True

        Self = New Self_Class(Name, MusicPath, mykey, ReceivePath)
        Enable_Toolstrip(True, ToolStrip_Upper)
        Dim bar As New SongLoadForm() 'update music list
    End Sub
    Private Sub SetupCancelled()
        _MusicPathConfirmed = False
        _NameConfirmed = False
        Me.Close()
    End Sub


   


    Delegate Sub Populate_Listview_del(ByVal keylist As String(), ByRef List_view As ListView)
    Public Sub Populate_Listview(ByVal keylist As String(), ByRef List_view As ListView)
        If List_view.InvokeRequired Then
            Invoke(New Populate_Listview_del(AddressOf Populate_Listview), keylist, List_view)
        Else
            List_view.Clear()

            If keylist IsNot Nothing Then
                For Each ID As String In keylist
                    Dim li As New ListViewItem
                    li.Text = UL.User(ID).Name
                    li.Tag = ID

                    If UL.User(ID).online = True Then
                        li.ImageIndex = 0
                        li.Group = List_view.Groups("Online")
                    Else
                        li.ImageIndex = 1
                        li.Group = List_view.Groups("Offline")
                    End If

                    List_view.Items.Add(li)
                Next
            End If
        End If
    End Sub
    Private Sub ListView_Contacts_MouseClick(sender As Object, e As MouseEventArgs) Handles ListView_Contacts.MouseDoubleClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim name As String = ListView_Contacts.FocusedItem.Text
            Dim clientkey As String = CStr(ListView_Contacts.FocusedItem.Tag)

            Dim openFormKeys As New List(Of String)
            For Each Sform As SongListForm In Songforms 'get all open ID's
                openFormKeys.Add(Sform.ID)
            Next


            If UL.Keys.Contains(clientkey) And Not openFormKeys.Contains(clientkey) Then
                Dim SongForm As New SongListForm(clientkey)
            End If
        End If
    End Sub

#Region "Notify Icon"

    Delegate Sub TrayShowBubble_del(ByVal title As String, ByVal text As String, ByVal Icon As ToolTipIcon)
    Public Sub TrayShowBubble(ByVal title As String, ByVal text As String, ByVal Icon As ToolTipIcon)
        If Me.InvokeRequired Then
            Invoke(New TrayShowBubble_del(AddressOf TrayShowBubble), title, text, Icon)
        Else
            NotifyIcon_Main.BalloonTipIcon = Icon
            NotifyIcon_Main.BalloonTipText = text
            NotifyIcon_Main.BalloonTipTitle = title
            NotifyIcon_Main.ShowBalloonTip(2000)
        End If
    End Sub
    Private Sub NotifyIcon_Main_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon_Main.MouseDoubleClick
        If Me.Visible = True Then
            Me.Visible = False
            Me.Hide()
        Else
            Me.Visible = True
            Me.Show()
        End If
    End Sub
    Private Sub NotifyIcon_Main_MouseDown(sender As Object, e As MouseEventArgs) Handles NotifyIcon_Main.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            'ContextMenuStrip_notifyIcon.Show(e.Location)
        End If
    End Sub

    Private Sub ToolStripMenuItem_CloseProgram_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_CloseProgram.Click
        Me.Close()
    End Sub
    Private Sub ToolStripMenuItem_RefreshLibrary2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_RefreshLibrary2.Click
        Dim bar As New SongLoadForm()
    End Sub
    Private Sub ToolStripMenuItem_Maximize_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_Maximize.Click
        Me.Visible = True
        Me.Show()
    End Sub
    Private Sub ToolStripMenuItem_Downloads2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_Downloads2.Click
        'DlManager.ToggleForm(True)
    End Sub

#End Region

    Private Sub ToolStripButton_Connect_Click(sender As Object, e As EventArgs) Handles ToolStripButton_Connect.Click
        Try

            Dim btn As ToolStripButton = sender
            If LibraryUpdated Then

                If Connected = False Then
                    btn.Text = "Connecting..."
                    btn.Enabled = False
                Else
                    btn.Text = "Disconnecting..."
                    btn.Enabled = False
                End If

                Dim T1 As New Thread(AddressOf Online)
                T1.Start(Not Connected)

            Else
                MessageBox.Show("You must update your library before connecting. This can be done in the file dropdown.")
            End If

        Catch ex As Exception
        End Try
    End Sub

    Delegate Sub Enable_Toolstrip_del(ByVal enabled As Boolean, ByVal TS As ToolStrip)
    Public Sub Enable_Toolstrip(ByVal enabled As Boolean, ByVal TS As ToolStrip)
        If TS.InvokeRequired Then
            Invoke(New Enable_Toolstrip_del(AddressOf Enable_Toolstrip), enabled, TS)
        Else
            TS.Enabled = enabled
        End If
    End Sub

    Public Delegate Sub ConnectBtn_Enable_del(ByVal online As Boolean)
    Public Sub ConnectBtn_Enable(ByVal online As Boolean)
        If Me.InvokeRequired Then
            Invoke(New ConnectBtn_Enable_del(AddressOf ConnectBtn_Enable), online)
        Else
            If online Then
                ToolStripButton_Connect.Text = "Disconnect"
            Else
                ToolStripButton_Connect.Text = "Connect"
            End If
            ToolStripButton_Connect.Enabled = True
        End If
    End Sub


End Class
