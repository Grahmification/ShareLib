<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DLManager_Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DLManager_Form))
        Me.ToolStrip_DLManager = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton_ClearDL = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_ChangeDLPath = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_DLFolder = New System.Windows.Forms.ToolStripButton()
        Me.StatusStrip_DLManager = New System.Windows.Forms.StatusStrip()
        Me.SplitContainer_Downloads = New System.Windows.Forms.SplitContainer()
        Me.ListView_Downloading = New System.Windows.Forms.ListView()
        Me.ListView_FinishedDLs = New System.Windows.Forms.ListView()
        Me.ToolStrip_DLManager.SuspendLayout()
        CType(Me.SplitContainer_Downloads, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer_Downloads.Panel1.SuspendLayout()
        Me.SplitContainer_Downloads.Panel2.SuspendLayout()
        Me.SplitContainer_Downloads.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip_DLManager
        '
        Me.ToolStrip_DLManager.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton_ClearDL, Me.ToolStripButton_ChangeDLPath, Me.ToolStripButton_DLFolder})
        Me.ToolStrip_DLManager.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip_DLManager.Name = "ToolStrip_DLManager"
        Me.ToolStrip_DLManager.Size = New System.Drawing.Size(604, 25)
        Me.ToolStrip_DLManager.TabIndex = 1
        Me.ToolStrip_DLManager.Text = "ToolStrip1"
        '
        'ToolStripButton_ClearDL
        '
        Me.ToolStripButton_ClearDL.Image = CType(resources.GetObject("ToolStripButton_ClearDL.Image"), System.Drawing.Image)
        Me.ToolStripButton_ClearDL.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_ClearDL.Name = "ToolStripButton_ClearDL"
        Me.ToolStripButton_ClearDL.Size = New System.Drawing.Size(75, 22)
        Me.ToolStripButton_ClearDL.Text = "Clear List"
        '
        'ToolStripButton_ChangeDLPath
        '
        Me.ToolStripButton_ChangeDLPath.Image = CType(resources.GetObject("ToolStripButton_ChangeDLPath.Image"), System.Drawing.Image)
        Me.ToolStripButton_ChangeDLPath.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_ChangeDLPath.Name = "ToolStripButton_ChangeDLPath"
        Me.ToolStripButton_ChangeDLPath.Size = New System.Drawing.Size(152, 22)
        Me.ToolStripButton_ChangeDLPath.Text = "Change Download Path"
        '
        'ToolStripButton_DLFolder
        '
        Me.ToolStripButton_DLFolder.Image = CType(resources.GetObject("ToolStripButton_DLFolder.Image"), System.Drawing.Image)
        Me.ToolStripButton_DLFolder.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_DLFolder.Name = "ToolStripButton_DLFolder"
        Me.ToolStripButton_DLFolder.Size = New System.Drawing.Size(122, 22)
        Me.ToolStripButton_DLFolder.Text = "Downloads Folder"
        '
        'StatusStrip_DLManager
        '
        Me.StatusStrip_DLManager.Location = New System.Drawing.Point(0, 339)
        Me.StatusStrip_DLManager.Name = "StatusStrip_DLManager"
        Me.StatusStrip_DLManager.Size = New System.Drawing.Size(604, 22)
        Me.StatusStrip_DLManager.TabIndex = 2
        Me.StatusStrip_DLManager.Text = "StatusStrip1"
        '
        'SplitContainer_Downloads
        '
        Me.SplitContainer_Downloads.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer_Downloads.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer_Downloads.Name = "SplitContainer_Downloads"
        Me.SplitContainer_Downloads.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer_Downloads.Panel1
        '
        Me.SplitContainer_Downloads.Panel1.Controls.Add(Me.ListView_Downloading)
        '
        'SplitContainer_Downloads.Panel2
        '
        Me.SplitContainer_Downloads.Panel2.Controls.Add(Me.ListView_FinishedDLs)
        Me.SplitContainer_Downloads.Size = New System.Drawing.Size(604, 314)
        Me.SplitContainer_Downloads.SplitterDistance = 192
        Me.SplitContainer_Downloads.TabIndex = 3
        '
        'ListView_Downloading
        '
        Me.ListView_Downloading.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView_Downloading.FullRowSelect = True
        Me.ListView_Downloading.GridLines = True
        Me.ListView_Downloading.Location = New System.Drawing.Point(0, 0)
        Me.ListView_Downloading.Name = "ListView_Downloading"
        Me.ListView_Downloading.Size = New System.Drawing.Size(604, 192)
        Me.ListView_Downloading.TabIndex = 3
        Me.ListView_Downloading.UseCompatibleStateImageBehavior = False
        Me.ListView_Downloading.View = System.Windows.Forms.View.Details
        '
        'ListView_FinishedDLs
        '
        Me.ListView_FinishedDLs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView_FinishedDLs.FullRowSelect = True
        Me.ListView_FinishedDLs.GridLines = True
        Me.ListView_FinishedDLs.Location = New System.Drawing.Point(0, 0)
        Me.ListView_FinishedDLs.Name = "ListView_FinishedDLs"
        Me.ListView_FinishedDLs.Size = New System.Drawing.Size(604, 118)
        Me.ListView_FinishedDLs.TabIndex = 4
        Me.ListView_FinishedDLs.UseCompatibleStateImageBehavior = False
        Me.ListView_FinishedDLs.View = System.Windows.Forms.View.Details
        '
        'DLManager_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(604, 361)
        Me.Controls.Add(Me.SplitContainer_Downloads)
        Me.Controls.Add(Me.StatusStrip_DLManager)
        Me.Controls.Add(Me.ToolStrip_DLManager)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DLManager_Form"
        Me.Text = "Download Manager"
        Me.ToolStrip_DLManager.ResumeLayout(False)
        Me.ToolStrip_DLManager.PerformLayout()
        Me.SplitContainer_Downloads.Panel1.ResumeLayout(False)
        Me.SplitContainer_Downloads.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer_Downloads, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer_Downloads.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip_DLManager As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton_ClearDL As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_ChangeDLPath As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_DLFolder As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip_DLManager As System.Windows.Forms.StatusStrip
    Friend WithEvents SplitContainer_Downloads As System.Windows.Forms.SplitContainer
    Friend WithEvents ListView_Downloading As System.Windows.Forms.ListView
    Friend WithEvents ListView_FinishedDLs As System.Windows.Forms.ListView
End Class
