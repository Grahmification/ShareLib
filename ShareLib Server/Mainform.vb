
Public Class Mainform

    Dim selectedrow As Integer = 0

    Private Sub Mainform_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadedForm = Me
        ToolStripMenuItem_GenNewUserKey.PerformClick()
        NotifyIcon_TaskBar.Visible = True
        Call Timer1_Tick()
    End Sub
    Private Sub Mainform_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim rtn As Integer = MessageBox.Show("Save User list?", "Save User list?", MessageBoxButtons.YesNoCancel)

        If ToolStripButton_Start.Checked = True Then
            ToolStripButton_Start.PerformClick()
        End If

        If rtn = 6 Then ' yes
            ToolStripMenuItem_SaveUserList.PerformClick()
        ElseIf rtn = 7 Then 'no 
        Else
            e.Cancel = True
        End If
        NotifyIcon_TaskBar.Visible = False
    End Sub

    Private Sub NotifyIcon_TaskBar_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon_TaskBar.DoubleClick
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Show()
            Me.WindowState = FormWindowState.Normal
        Else
            Me.WindowState = FormWindowState.Minimized
            Me.Hide()
        End If
    End Sub
    Private Sub Timer1_Tick() Handles Timer1.Tick
        If TimeOfDay.Hour = 24 Or TimeOfDay.Hour = 1 Then
            ToolStripMenuItem_GenNewUserKey.PerformClick()
            Timer1.Interval = CInt(24 * 3600 * 1000)
            Timer1.Enabled = True
        Else
            Dim Currenthour As Integer = TimeOfDay.Hour
            Dim Currentmin As Integer = TimeOfDay.Minute
            Dim timedifference As Double = 24 - Currenthour - (Currentmin / 60)
            Timer1.Interval = CInt(timedifference * 3600 * 1000)
            Timer1.Enabled = True
        End If
    End Sub



    Public Delegate Sub Populate_UserTable_del(ByVal GridView As DataGridView)
    Public Sub Populate_UserTable(ByVal GridView As DataGridView)

        If InvokeRequired Then
            Invoke(New Populate_UserTable_del(AddressOf Populate_UserTable), GridView)
        Else
            Dim Table As New DataTable
            Dim source As New BindingSource

            If Table.Columns.Count = 0 Then
                Table.Columns.Add("Name")
                Table.Columns.Add("IP Address")
                Table.Columns.Add("User Key")
            End If

            source.DataSource = Table
            GridView.DataSource = source

            Table.Clear()

            Dim names As String() = Users2.AllNames
            Dim Ips As String() = Users2.AllIps
            Dim keys As String() = Users2.Allkeys



            For i = 0 To names.Length - 1
                Dim row As DataRow = Table.NewRow
                row("Name") = names(i)
                row("IP Address") = Ips(i)
                row("User Key") = keys(i)
                Table.Rows.Add(row)
            Next

            GridView.AutoResizeColumns(System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells)

        End If
    End Sub
    Private Sub DataGridView_Userlist_CellContentClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView_Userlist.CellMouseDown
        If e.Button = MouseButtons.Right And e.RowIndex >= 0 Then
            selectedrow = e.RowIndex
            ContextMenuStrip_UserList.Show(DataGridView_Userlist, e.Location)
        End If
    End Sub

#Region "Toolstrip functions"
    Private Sub ToolStripMenuItem_GenNewUserKey_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_GenNewUserKey.Click
        NewUserKey = Users2.CreateNewID
        ToolStripTextBox_CurrentUserKey.Text = NewUserKey
    End Sub
    Private Sub ToolStripTextBox_CurrentUserKey_KeyUp(sender As Object, e As KeyEventArgs) Handles ToolStripTextBox_CurrentUserKey.KeyUp
        If e.KeyCode = Keys.Enter Then
            NewUserKey = ToolStripTextBox_CurrentUserKey.Text
        End If
    End Sub

    Private Sub ToolStripButton_Start_Click(sender As Object, e As EventArgs) Handles ToolStripButton_Start.Click
        Dim TsBtn As ToolStripButton = sender

        If Connected = False Then
            TsBtn.Text = "Stop"
            NewConnection()
            Connected = True

            LoadedForm.AppendRTB(RichTextBox_ConnectLog, "Listening on port: " & CStr(Defaultport))
            LoadedForm.AppendRTB(RichTextBox_ConnectLog, " ")
            LoadedForm.AppendRTB(RichTextBox_Datalog, "Listening on port: " & CStr(Defaultport))
            LoadedForm.AppendRTB(RichTextBox_Datalog, " ")
        Else
            TsBtn.Text = "Start"
            Dim l As Integer = Connections.Count

            For i As Integer = 0 To l - 1
                Connections(i).Disconnect()
            Next

            Connected = False

            LoadedForm.AppendRTB(RichTextBox_ConnectLog, "Stopped Listening")
            LoadedForm.AppendRTB(RichTextBox_ConnectLog, " ")
            LoadedForm.AppendRTB(RichTextBox_Datalog, "Stopped Listening")
            LoadedForm.AppendRTB(RichTextBox_Datalog, " ")
        End If
    End Sub

    Private Sub ToolStripMenuItem_Adduser_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_Adduser.Click
        Dim NewUForm As New NewUserForm
        NewUForm.Show()
    End Sub
    Private Sub ToolStripMenuItem_DeleteUser_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_DeleteUser.Click, ToolStripMenuItem_EditUser.Click
        Dim colindex As Integer = 0
        Dim Btn As ToolStripMenuItem = sender

        For Each c As DataGridViewColumn In DataGridView_Userlist.Columns
            If c.HeaderText = "User Key" Then
                colindex = c.Index
            End If
        Next

        Dim ID As String = DataGridView_Userlist.Item(colindex, selectedrow).Value

        If Btn Is ToolStripMenuItem_DeleteUser Then
            Users2.Delete(ID)

        ElseIf Btn Is ToolStripMenuItem_EditUser Then
            Dim Editform As New EditUserForm(ID)
        End If

    End Sub

    Private Sub ToolStripMenuItem_SaveUserList_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_SaveUserList.Click
        Dim fd As New SaveFileDialog
        fd.Filter = "XML File|*.xml"
        fd.FileName = "SnatcherServerUserList"
        fd.ShowDialog()
        If fd.FileName <> "SnatcherServerUserList" Then 'stops if box cancelled
            Users2.Save_ToFile(fd.FileName)
        End If
    End Sub
    Private Sub ToolStripMenuItem_LoadUserList_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_LoadUserList.Click
        Dim fb As New OpenFileDialog
        fb.Filter = "XML File|*.xml"
        fb.FileName = "SnatcherServerUserList"
        fb.ShowDialog()

        If fb.FileName <> "SnatcherServerUserList" Then 'stops if box cancelled
            Users2.Load_FromFile(fb.FileName)
        End If

        Populate_UserTable(DataGridView_Userlist)

    End Sub

#End Region

#Region "Rich Textbox fns"

    Public Delegate Sub AppendRTB_del(ByVal RTB As RichTextBox, ByVal Message As String)
    Public Sub AppendRTB(ByVal RTB As RichTextBox, ByVal Message As String)
        If RTB.InvokeRequired Then
            Invoke(New AppendRTB_del(AddressOf AppendRTB), RTB, Message)
        Else
            RTB.AppendText(Message)
            RTB.AppendText(Environment.NewLine)
        End If
    End Sub

    Private Sub RichTextBox_MouseDown(sender As Object, e As MouseEventArgs) Handles RichTextBox_Datalog.MouseDown, RichTextBox_ConnectLog.MouseDown

        Dim RTB As RichTextBox = sender

        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip_RTB.Show(RTB, e.Location)
        End If

    End Sub
    Private Sub RTB_Contextmenu_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_ClearTextBox.Click, ToolStripMenuItem_SaveTextBox.Click
        Dim MItem As ToolStripMenuItem = sender
        Dim RTB As RichTextBox = ContextMenuStrip_RTB.SourceControl

        If MItem Is ToolStripMenuItem_SaveTextBox Then
            Dim fd As New SaveFileDialog
            fd.Filter = "Text File|*.txt"
            fd.ShowDialog()
            If fd.FileName <> "" Then 'stops if box cancelled
                RTB.SaveFile(fd.FileName, RichTextBoxStreamType.PlainText)
            End If

        ElseIf MItem Is ToolStripMenuItem_ClearTextBox Then
            RTB.Clear()
        End If


    End Sub
#End Region


 
End Class
