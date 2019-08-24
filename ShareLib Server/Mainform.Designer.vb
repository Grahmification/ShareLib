<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Mainform
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Mainform))
        Me.ToolStrip_Top = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton_Start = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripDropDownButton_UserFns = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripMenuItem_Adduser = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_UserKey = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBox_CurrentUserKey = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripMenuItem_GenNewUserKey = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_SaveUserList = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_LoadUserList = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl_main = New System.Windows.Forms.TabControl()
        Me.Tab_Userlist = New System.Windows.Forms.TabPage()
        Me.DataGridView_Userlist = New System.Windows.Forms.DataGridView()
        Me.Tab_Datalog = New System.Windows.Forms.TabPage()
        Me.RichTextBox_Datalog = New System.Windows.Forms.RichTextBox()
        Me.Tab_ConnectLog = New System.Windows.Forms.TabPage()
        Me.RichTextBox_ConnectLog = New System.Windows.Forms.RichTextBox()
        Me.ContextMenuStrip_UserList = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_DeleteUser = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_EditUser = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip_RTB = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_ClearTextBox = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_SaveTextBox = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.NotifyIcon_TaskBar = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ToolStrip_Top.SuspendLayout()
        Me.TabControl_main.SuspendLayout()
        Me.Tab_Userlist.SuspendLayout()
        CType(Me.DataGridView_Userlist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Tab_Datalog.SuspendLayout()
        Me.Tab_ConnectLog.SuspendLayout()
        Me.ContextMenuStrip_UserList.SuspendLayout()
        Me.ContextMenuStrip_RTB.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip_Top
        '
        Me.ToolStrip_Top.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton_Start, Me.ToolStripDropDownButton_UserFns})
        Me.ToolStrip_Top.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip_Top.Name = "ToolStrip_Top"
        Me.ToolStrip_Top.Size = New System.Drawing.Size(553, 25)
        Me.ToolStrip_Top.TabIndex = 1
        Me.ToolStrip_Top.Text = "ToolStrip1"
        '
        'ToolStripButton_Start
        '
        Me.ToolStripButton_Start.CheckOnClick = True
        Me.ToolStripButton_Start.Image = CType(resources.GetObject("ToolStripButton_Start.Image"), System.Drawing.Image)
        Me.ToolStripButton_Start.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Start.Name = "ToolStripButton_Start"
        Me.ToolStripButton_Start.Size = New System.Drawing.Size(51, 22)
        Me.ToolStripButton_Start.Text = "Start"
        '
        'ToolStripDropDownButton_UserFns
        '
        Me.ToolStripDropDownButton_UserFns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton_UserFns.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_Adduser, Me.ToolStripMenuItem_UserKey, Me.ToolStripMenuItem_SaveUserList, Me.ToolStripMenuItem_LoadUserList})
        Me.ToolStripDropDownButton_UserFns.Image = CType(resources.GetObject("ToolStripDropDownButton_UserFns.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton_UserFns.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton_UserFns.Name = "ToolStripDropDownButton_UserFns"
        Me.ToolStripDropDownButton_UserFns.Size = New System.Drawing.Size(48, 22)
        Me.ToolStripDropDownButton_UserFns.Text = "Users"
        Me.ToolStripDropDownButton_UserFns.ToolTipText = "Users"
        '
        'ToolStripMenuItem_Adduser
        '
        Me.ToolStripMenuItem_Adduser.Name = "ToolStripMenuItem_Adduser"
        Me.ToolStripMenuItem_Adduser.Size = New System.Drawing.Size(161, 22)
        Me.ToolStripMenuItem_Adduser.Text = "Create New User"
        '
        'ToolStripMenuItem_UserKey
        '
        Me.ToolStripMenuItem_UserKey.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripTextBox_CurrentUserKey, Me.ToolStripMenuItem_GenNewUserKey})
        Me.ToolStripMenuItem_UserKey.Name = "ToolStripMenuItem_UserKey"
        Me.ToolStripMenuItem_UserKey.Size = New System.Drawing.Size(161, 22)
        Me.ToolStripMenuItem_UserKey.Text = "New User Key"
        '
        'ToolStripTextBox_CurrentUserKey
        '
        Me.ToolStripTextBox_CurrentUserKey.Name = "ToolStripTextBox_CurrentUserKey"
        Me.ToolStripTextBox_CurrentUserKey.Size = New System.Drawing.Size(140, 23)
        '
        'ToolStripMenuItem_GenNewUserKey
        '
        Me.ToolStripMenuItem_GenNewUserKey.Name = "ToolStripMenuItem_GenNewUserKey"
        Me.ToolStripMenuItem_GenNewUserKey.Size = New System.Drawing.Size(200, 22)
        Me.ToolStripMenuItem_GenNewUserKey.Text = "Generate New User Key"
        '
        'ToolStripMenuItem_SaveUserList
        '
        Me.ToolStripMenuItem_SaveUserList.Name = "ToolStripMenuItem_SaveUserList"
        Me.ToolStripMenuItem_SaveUserList.Size = New System.Drawing.Size(161, 22)
        Me.ToolStripMenuItem_SaveUserList.Text = "Save User List"
        '
        'ToolStripMenuItem_LoadUserList
        '
        Me.ToolStripMenuItem_LoadUserList.Name = "ToolStripMenuItem_LoadUserList"
        Me.ToolStripMenuItem_LoadUserList.Size = New System.Drawing.Size(161, 22)
        Me.ToolStripMenuItem_LoadUserList.Text = "Load User List"
        '
        'TabControl_main
        '
        Me.TabControl_main.Controls.Add(Me.Tab_Userlist)
        Me.TabControl_main.Controls.Add(Me.Tab_Datalog)
        Me.TabControl_main.Controls.Add(Me.Tab_ConnectLog)
        Me.TabControl_main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_main.Location = New System.Drawing.Point(0, 25)
        Me.TabControl_main.Name = "TabControl_main"
        Me.TabControl_main.SelectedIndex = 0
        Me.TabControl_main.Size = New System.Drawing.Size(553, 359)
        Me.TabControl_main.TabIndex = 2
        '
        'Tab_Userlist
        '
        Me.Tab_Userlist.Controls.Add(Me.DataGridView_Userlist)
        Me.Tab_Userlist.Location = New System.Drawing.Point(4, 22)
        Me.Tab_Userlist.Name = "Tab_Userlist"
        Me.Tab_Userlist.Padding = New System.Windows.Forms.Padding(3)
        Me.Tab_Userlist.Size = New System.Drawing.Size(545, 333)
        Me.Tab_Userlist.TabIndex = 0
        Me.Tab_Userlist.Text = "User List"
        Me.Tab_Userlist.UseVisualStyleBackColor = True
        '
        'DataGridView_Userlist
        '
        Me.DataGridView_Userlist.AllowUserToAddRows = False
        Me.DataGridView_Userlist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView_Userlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView_Userlist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView_Userlist.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DataGridView_Userlist.Location = New System.Drawing.Point(3, 3)
        Me.DataGridView_Userlist.Name = "DataGridView_Userlist"
        Me.DataGridView_Userlist.ReadOnly = True
        Me.DataGridView_Userlist.RowHeadersVisible = False
        Me.DataGridView_Userlist.ShowEditingIcon = False
        Me.DataGridView_Userlist.Size = New System.Drawing.Size(539, 327)
        Me.DataGridView_Userlist.TabIndex = 0
        '
        'Tab_Datalog
        '
        Me.Tab_Datalog.Controls.Add(Me.RichTextBox_Datalog)
        Me.Tab_Datalog.Location = New System.Drawing.Point(4, 22)
        Me.Tab_Datalog.Name = "Tab_Datalog"
        Me.Tab_Datalog.Padding = New System.Windows.Forms.Padding(3)
        Me.Tab_Datalog.Size = New System.Drawing.Size(545, 333)
        Me.Tab_Datalog.TabIndex = 1
        Me.Tab_Datalog.Text = "Data log"
        Me.Tab_Datalog.UseVisualStyleBackColor = True
        '
        'RichTextBox_Datalog
        '
        Me.RichTextBox_Datalog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox_Datalog.Location = New System.Drawing.Point(3, 3)
        Me.RichTextBox_Datalog.Name = "RichTextBox_Datalog"
        Me.RichTextBox_Datalog.ReadOnly = True
        Me.RichTextBox_Datalog.Size = New System.Drawing.Size(539, 327)
        Me.RichTextBox_Datalog.TabIndex = 0
        Me.RichTextBox_Datalog.Text = ""
        '
        'Tab_ConnectLog
        '
        Me.Tab_ConnectLog.Controls.Add(Me.RichTextBox_ConnectLog)
        Me.Tab_ConnectLog.Location = New System.Drawing.Point(4, 22)
        Me.Tab_ConnectLog.Name = "Tab_ConnectLog"
        Me.Tab_ConnectLog.Size = New System.Drawing.Size(545, 333)
        Me.Tab_ConnectLog.TabIndex = 2
        Me.Tab_ConnectLog.Text = "Connection log"
        Me.Tab_ConnectLog.UseVisualStyleBackColor = True
        '
        'RichTextBox_ConnectLog
        '
        Me.RichTextBox_ConnectLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox_ConnectLog.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBox_ConnectLog.Name = "RichTextBox_ConnectLog"
        Me.RichTextBox_ConnectLog.ReadOnly = True
        Me.RichTextBox_ConnectLog.Size = New System.Drawing.Size(545, 333)
        Me.RichTextBox_ConnectLog.TabIndex = 0
        Me.RichTextBox_ConnectLog.Text = ""
        '
        'ContextMenuStrip_UserList
        '
        Me.ContextMenuStrip_UserList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_DeleteUser, Me.ToolStripMenuItem_EditUser})
        Me.ContextMenuStrip_UserList.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip_UserList.Size = New System.Drawing.Size(134, 48)
        '
        'ToolStripMenuItem_DeleteUser
        '
        Me.ToolStripMenuItem_DeleteUser.Name = "ToolStripMenuItem_DeleteUser"
        Me.ToolStripMenuItem_DeleteUser.Size = New System.Drawing.Size(133, 22)
        Me.ToolStripMenuItem_DeleteUser.Text = "Delete User"
        '
        'ToolStripMenuItem_EditUser
        '
        Me.ToolStripMenuItem_EditUser.Name = "ToolStripMenuItem_EditUser"
        Me.ToolStripMenuItem_EditUser.Size = New System.Drawing.Size(133, 22)
        Me.ToolStripMenuItem_EditUser.Text = "Edit User"
        '
        'ContextMenuStrip_RTB
        '
        Me.ContextMenuStrip_RTB.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_ClearTextBox, Me.ToolStripMenuItem_SaveTextBox})
        Me.ContextMenuStrip_RTB.Name = "ContextMenuStrip_RTB"
        Me.ContextMenuStrip_RTB.Size = New System.Drawing.Size(122, 48)
        '
        'ToolStripMenuItem_ClearTextBox
        '
        Me.ToolStripMenuItem_ClearTextBox.Name = "ToolStripMenuItem_ClearTextBox"
        Me.ToolStripMenuItem_ClearTextBox.Size = New System.Drawing.Size(121, 22)
        Me.ToolStripMenuItem_ClearTextBox.Text = "Clear"
        '
        'ToolStripMenuItem_SaveTextBox
        '
        Me.ToolStripMenuItem_SaveTextBox.Name = "ToolStripMenuItem_SaveTextBox"
        Me.ToolStripMenuItem_SaveTextBox.Size = New System.Drawing.Size(121, 22)
        Me.ToolStripMenuItem_SaveTextBox.Text = "Save Log"
        '
        'Timer1
        '
        Me.Timer1.Interval = 86400000
        '
        'NotifyIcon_TaskBar
        '
        Me.NotifyIcon_TaskBar.Icon = CType(resources.GetObject("NotifyIcon_TaskBar.Icon"), System.Drawing.Icon)
        Me.NotifyIcon_TaskBar.Text = "Snatcher Server"
        Me.NotifyIcon_TaskBar.Visible = True
        '
        'Mainform
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(553, 384)
        Me.Controls.Add(Me.TabControl_main)
        Me.Controls.Add(Me.ToolStrip_Top)
        Me.Name = "Mainform"
        Me.Text = "ShareLib Server"
        Me.ToolStrip_Top.ResumeLayout(False)
        Me.ToolStrip_Top.PerformLayout()
        Me.TabControl_main.ResumeLayout(False)
        Me.Tab_Userlist.ResumeLayout(False)
        CType(Me.DataGridView_Userlist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Tab_Datalog.ResumeLayout(False)
        Me.Tab_ConnectLog.ResumeLayout(False)
        Me.ContextMenuStrip_UserList.ResumeLayout(False)
        Me.ContextMenuStrip_RTB.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip_Top As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton_Start As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripDropDownButton_UserFns As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripMenuItem_Adduser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_UserKey As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripTextBox_CurrentUserKey As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripMenuItem_GenNewUserKey As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_SaveUserList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_LoadUserList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabControl_main As System.Windows.Forms.TabControl
    Friend WithEvents Tab_Userlist As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView_Userlist As System.Windows.Forms.DataGridView
    Friend WithEvents Tab_Datalog As System.Windows.Forms.TabPage
    Friend WithEvents RichTextBox_Datalog As System.Windows.Forms.RichTextBox
    Friend WithEvents Tab_ConnectLog As System.Windows.Forms.TabPage
    Friend WithEvents RichTextBox_ConnectLog As System.Windows.Forms.RichTextBox
    Friend WithEvents ContextMenuStrip_UserList As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_DeleteUser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_EditUser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip_RTB As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_ClearTextBox As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_SaveTextBox As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents NotifyIcon_TaskBar As System.Windows.Forms.NotifyIcon

End Class
