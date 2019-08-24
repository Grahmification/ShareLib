<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Mainwindow
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Mainwindow))
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Online", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Offline", System.Windows.Forms.HorizontalAlignment.Left)
        Me.ToolStrip_Upper = New System.Windows.Forms.ToolStrip()
        Me.ToolStripDropDownButton_File = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripMenuItem_ChangeDLPath = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_ChangeLibraryPath = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_ReloadLibrary = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_ClearUserData = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_ResetAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripButton_Connect = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_Downloads = New System.Windows.Forms.ToolStripButton()
        Me.ListView_Contacts = New System.Windows.Forms.ListView()
        Me.ImageList_Contacts = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolStrip_Bottom = New System.Windows.Forms.ToolStrip()
        Me.NotifyIcon_Main = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip_notifyIcon = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_CloseProgram = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_Maximize = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_Downloads2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_RefreshLibrary2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip_Upper.SuspendLayout()
        Me.ContextMenuStrip_notifyIcon.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip_Upper
        '
        Me.ToolStrip_Upper.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton_File, Me.ToolStripButton_Connect, Me.ToolStripButton_Downloads})
        Me.ToolStrip_Upper.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip_Upper.Name = "ToolStrip_Upper"
        Me.ToolStrip_Upper.Size = New System.Drawing.Size(345, 25)
        Me.ToolStrip_Upper.TabIndex = 0
        Me.ToolStrip_Upper.Text = "ToolStrip1"
        '
        'ToolStripDropDownButton_File
        '
        Me.ToolStripDropDownButton_File.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton_File.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_ChangeDLPath, Me.ToolStripMenuItem_ChangeLibraryPath, Me.ToolStripMenuItem_ReloadLibrary, Me.ToolStripMenuItem_ClearUserData, Me.ToolStripMenuItem_ResetAll})
        Me.ToolStripDropDownButton_File.Image = CType(resources.GetObject("ToolStripDropDownButton_File.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton_File.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton_File.Name = "ToolStripDropDownButton_File"
        Me.ToolStripDropDownButton_File.Size = New System.Drawing.Size(38, 22)
        Me.ToolStripDropDownButton_File.Text = "File"
        '
        'ToolStripMenuItem_ChangeDLPath
        '
        Me.ToolStripMenuItem_ChangeDLPath.Image = CType(resources.GetObject("ToolStripMenuItem_ChangeDLPath.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem_ChangeDLPath.Name = "ToolStripMenuItem_ChangeDLPath"
        Me.ToolStripMenuItem_ChangeDLPath.Size = New System.Drawing.Size(199, 22)
        Me.ToolStripMenuItem_ChangeDLPath.Text = "Change Download Path"
        '
        'ToolStripMenuItem_ChangeLibraryPath
        '
        Me.ToolStripMenuItem_ChangeLibraryPath.Image = CType(resources.GetObject("ToolStripMenuItem_ChangeLibraryPath.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem_ChangeLibraryPath.Name = "ToolStripMenuItem_ChangeLibraryPath"
        Me.ToolStripMenuItem_ChangeLibraryPath.Size = New System.Drawing.Size(199, 22)
        Me.ToolStripMenuItem_ChangeLibraryPath.Text = "Change Library Path"
        '
        'ToolStripMenuItem_ReloadLibrary
        '
        Me.ToolStripMenuItem_ReloadLibrary.Image = CType(resources.GetObject("ToolStripMenuItem_ReloadLibrary.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem_ReloadLibrary.Name = "ToolStripMenuItem_ReloadLibrary"
        Me.ToolStripMenuItem_ReloadLibrary.Size = New System.Drawing.Size(199, 22)
        Me.ToolStripMenuItem_ReloadLibrary.Text = "Refresh My library"
        '
        'ToolStripMenuItem_ClearUserData
        '
        Me.ToolStripMenuItem_ClearUserData.Image = CType(resources.GetObject("ToolStripMenuItem_ClearUserData.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem_ClearUserData.Name = "ToolStripMenuItem_ClearUserData"
        Me.ToolStripMenuItem_ClearUserData.Size = New System.Drawing.Size(199, 22)
        Me.ToolStripMenuItem_ClearUserData.Text = "Reset User Data"
        '
        'ToolStripMenuItem_ResetAll
        '
        Me.ToolStripMenuItem_ResetAll.Image = CType(resources.GetObject("ToolStripMenuItem_ResetAll.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem_ResetAll.Name = "ToolStripMenuItem_ResetAll"
        Me.ToolStripMenuItem_ResetAll.Size = New System.Drawing.Size(199, 22)
        Me.ToolStripMenuItem_ResetAll.Text = "Reset Program"
        '
        'ToolStripButton_Connect
        '
        Me.ToolStripButton_Connect.Image = CType(resources.GetObject("ToolStripButton_Connect.Image"), System.Drawing.Image)
        Me.ToolStripButton_Connect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Connect.Name = "ToolStripButton_Connect"
        Me.ToolStripButton_Connect.Size = New System.Drawing.Size(72, 22)
        Me.ToolStripButton_Connect.Text = "Connect"
        '
        'ToolStripButton_Downloads
        '
        Me.ToolStripButton_Downloads.Image = CType(resources.GetObject("ToolStripButton_Downloads.Image"), System.Drawing.Image)
        Me.ToolStripButton_Downloads.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Downloads.Name = "ToolStripButton_Downloads"
        Me.ToolStripButton_Downloads.Size = New System.Drawing.Size(86, 22)
        Me.ToolStripButton_Downloads.Text = "Downloads"
        '
        'ListView_Contacts
        '
        Me.ListView_Contacts.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ListView_Contacts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ListViewGroup1.Header = "Online"
        ListViewGroup1.Name = "Online"
        ListViewGroup2.Header = "Offline"
        ListViewGroup2.Name = "Offline"
        Me.ListView_Contacts.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        Me.ListView_Contacts.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ListView_Contacts.HideSelection = False
        Me.ListView_Contacts.LargeImageList = Me.ImageList_Contacts
        Me.ListView_Contacts.Location = New System.Drawing.Point(0, 28)
        Me.ListView_Contacts.MultiSelect = False
        Me.ListView_Contacts.Name = "ListView_Contacts"
        Me.ListView_Contacts.Size = New System.Drawing.Size(345, 251)
        Me.ListView_Contacts.SmallImageList = Me.ImageList_Contacts
        Me.ListView_Contacts.TabIndex = 1
        Me.ListView_Contacts.UseCompatibleStateImageBehavior = False
        '
        'ImageList_Contacts
        '
        Me.ImageList_Contacts.ImageStream = CType(resources.GetObject("ImageList_Contacts.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList_Contacts.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList_Contacts.Images.SetKeyName(0, "online-icon.png")
        Me.ImageList_Contacts.Images.SetKeyName(1, "offline-icon.png")
        '
        'ToolStrip_Bottom
        '
        Me.ToolStrip_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip_Bottom.Location = New System.Drawing.Point(0, 282)
        Me.ToolStrip_Bottom.Name = "ToolStrip_Bottom"
        Me.ToolStrip_Bottom.Size = New System.Drawing.Size(345, 25)
        Me.ToolStrip_Bottom.TabIndex = 2
        Me.ToolStrip_Bottom.Text = "ToolStrip_Bottom"
        '
        'NotifyIcon_Main
        '
        Me.NotifyIcon_Main.ContextMenuStrip = Me.ContextMenuStrip_notifyIcon
        Me.NotifyIcon_Main.Icon = CType(resources.GetObject("NotifyIcon_Main.Icon"), System.Drawing.Icon)
        Me.NotifyIcon_Main.Text = "ShareLib"
        Me.NotifyIcon_Main.Visible = True
        '
        'ContextMenuStrip_notifyIcon
        '
        Me.ContextMenuStrip_notifyIcon.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_CloseProgram, Me.ToolStripMenuItem_Maximize, Me.ToolStripMenuItem_Downloads2, Me.ToolStripMenuItem_RefreshLibrary2})
        Me.ContextMenuStrip_notifyIcon.Name = "ContextMenuStrip_notifyIcon"
        Me.ContextMenuStrip_notifyIcon.Size = New System.Drawing.Size(173, 92)
        '
        'ToolStripMenuItem_CloseProgram
        '
        Me.ToolStripMenuItem_CloseProgram.Image = CType(resources.GetObject("ToolStripMenuItem_CloseProgram.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem_CloseProgram.Name = "ToolStripMenuItem_CloseProgram"
        Me.ToolStripMenuItem_CloseProgram.Size = New System.Drawing.Size(172, 22)
        Me.ToolStripMenuItem_CloseProgram.Text = "Exit"
        '
        'ToolStripMenuItem_Maximize
        '
        Me.ToolStripMenuItem_Maximize.Image = CType(resources.GetObject("ToolStripMenuItem_Maximize.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem_Maximize.Name = "ToolStripMenuItem_Maximize"
        Me.ToolStripMenuItem_Maximize.Size = New System.Drawing.Size(172, 22)
        Me.ToolStripMenuItem_Maximize.Text = "Maximize"
        '
        'ToolStripMenuItem_Downloads2
        '
        Me.ToolStripMenuItem_Downloads2.Image = CType(resources.GetObject("ToolStripMenuItem_Downloads2.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem_Downloads2.Name = "ToolStripMenuItem_Downloads2"
        Me.ToolStripMenuItem_Downloads2.Size = New System.Drawing.Size(172, 22)
        Me.ToolStripMenuItem_Downloads2.Text = "Downloads"
        '
        'ToolStripMenuItem_RefreshLibrary2
        '
        Me.ToolStripMenuItem_RefreshLibrary2.Image = CType(resources.GetObject("ToolStripMenuItem_RefreshLibrary2.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem_RefreshLibrary2.Name = "ToolStripMenuItem_RefreshLibrary2"
        Me.ToolStripMenuItem_RefreshLibrary2.Size = New System.Drawing.Size(172, 22)
        Me.ToolStripMenuItem_RefreshLibrary2.Text = "Refresh My Library"
        '
        'Mainwindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(345, 307)
        Me.Controls.Add(Me.ToolStrip_Bottom)
        Me.Controls.Add(Me.ListView_Contacts)
        Me.Controls.Add(Me.ToolStrip_Upper)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Mainwindow"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ShareLib"
        Me.ToolStrip_Upper.ResumeLayout(False)
        Me.ToolStrip_Upper.PerformLayout()
        Me.ContextMenuStrip_notifyIcon.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip_Upper As System.Windows.Forms.ToolStrip
    Friend WithEvents ListView_Contacts As System.Windows.Forms.ListView
    Friend WithEvents ImageList_Contacts As System.Windows.Forms.ImageList
    Friend WithEvents ToolStripButton_Connect As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Downloads As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip_Bottom As System.Windows.Forms.ToolStrip
    Friend WithEvents NotifyIcon_Main As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenuStrip_notifyIcon As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_CloseProgram As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_Maximize As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_Downloads2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_RefreshLibrary2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripDropDownButton_File As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripMenuItem_ChangeDLPath As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ChangeLibraryPath As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ReloadLibrary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ClearUserData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ResetAll As System.Windows.Forms.ToolStripMenuItem

End Class
