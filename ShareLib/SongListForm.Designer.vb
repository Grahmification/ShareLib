<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SongListForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SongListForm))
        Me.ToolStrip_Upper = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton_Refresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_Downloads = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripTextBox_Search = New System.Windows.Forms.ToolStripTextBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel_NumSongs = New System.Windows.Forms.ToolStripStatusLabel()
        Me.DataGridView_Library = New System.Windows.Forms.DataGridView()
        Me.ToolStrip_Upper.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.DataGridView_Library, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip_Upper
        '
        Me.ToolStrip_Upper.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton_Refresh, Me.ToolStripButton_Downloads, Me.ToolStripTextBox_Search})
        Me.ToolStrip_Upper.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip_Upper.Name = "ToolStrip_Upper"
        Me.ToolStrip_Upper.Size = New System.Drawing.Size(704, 25)
        Me.ToolStrip_Upper.TabIndex = 0
        Me.ToolStrip_Upper.Text = "ToolStrip1"
        '
        'ToolStripButton_Refresh
        '
        Me.ToolStripButton_Refresh.Image = CType(resources.GetObject("ToolStripButton_Refresh.Image"), System.Drawing.Image)
        Me.ToolStripButton_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Refresh.Name = "ToolStripButton_Refresh"
        Me.ToolStripButton_Refresh.Size = New System.Drawing.Size(66, 22)
        Me.ToolStripButton_Refresh.Text = "Refresh"
        '
        'ToolStripButton_Downloads
        '
        Me.ToolStripButton_Downloads.Image = CType(resources.GetObject("ToolStripButton_Downloads.Image"), System.Drawing.Image)
        Me.ToolStripButton_Downloads.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Downloads.Name = "ToolStripButton_Downloads"
        Me.ToolStripButton_Downloads.Size = New System.Drawing.Size(86, 22)
        Me.ToolStripButton_Downloads.Text = "Downloads"
        '
        'ToolStripTextBox_Search
        '
        Me.ToolStripTextBox_Search.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripTextBox_Search.Name = "ToolStripTextBox_Search"
        Me.ToolStripTextBox_Search.Size = New System.Drawing.Size(150, 25)
        Me.ToolStripTextBox_Search.Text = "Search...."
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel_NumSongs})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 320)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(704, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel_NumSongs
        '
        Me.ToolStripStatusLabel_NumSongs.Name = "ToolStripStatusLabel_NumSongs"
        Me.ToolStripStatusLabel_NumSongs.Size = New System.Drawing.Size(48, 17)
        Me.ToolStripStatusLabel_NumSongs.Text = "0 Songs"
        '
        'DataGridView_Library
        '
        Me.DataGridView_Library.AllowUserToAddRows = False
        Me.DataGridView_Library.AllowUserToDeleteRows = False
        Me.DataGridView_Library.AllowUserToResizeRows = False
        Me.DataGridView_Library.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView_Library.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView_Library.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DataGridView_Library.Location = New System.Drawing.Point(0, 25)
        Me.DataGridView_Library.MultiSelect = False
        Me.DataGridView_Library.Name = "DataGridView_Library"
        Me.DataGridView_Library.ReadOnly = True
        Me.DataGridView_Library.RowHeadersVisible = False
        Me.DataGridView_Library.Size = New System.Drawing.Size(704, 295)
        Me.DataGridView_Library.TabIndex = 4
        '
        'SongListForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(704, 342)
        Me.Controls.Add(Me.DataGridView_Library)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ToolStrip_Upper)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SongListForm"
        Me.Text = "SongListForm"
        Me.ToolStrip_Upper.ResumeLayout(False)
        Me.ToolStrip_Upper.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.DataGridView_Library, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip_Upper As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton_Refresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel_NumSongs As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripButton_Downloads As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripTextBox_Search As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents DataGridView_Library As System.Windows.Forms.DataGridView
End Class
