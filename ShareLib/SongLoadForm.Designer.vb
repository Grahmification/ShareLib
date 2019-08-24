<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SongLoadForm
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
        Me.Label_progress = New System.Windows.Forms.Label()
        Me.ProgressBar_SongSearch = New System.Windows.Forms.ProgressBar()
        Me.Button_CancelSearch = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label_progress
        '
        Me.Label_progress.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label_progress.AutoSize = True
        Me.Label_progress.Location = New System.Drawing.Point(109, 38)
        Me.Label_progress.Name = "Label_progress"
        Me.Label_progress.Size = New System.Drawing.Size(53, 13)
        Me.Label_progress.TabIndex = 5
        Me.Label_progress.Text = "xxx songs"
        '
        'ProgressBar_SongSearch
        '
        Me.ProgressBar_SongSearch.Location = New System.Drawing.Point(31, 12)
        Me.ProgressBar_SongSearch.Name = "ProgressBar_SongSearch"
        Me.ProgressBar_SongSearch.Size = New System.Drawing.Size(248, 23)
        Me.ProgressBar_SongSearch.TabIndex = 4
        '
        'Button_CancelSearch
        '
        Me.Button_CancelSearch.Location = New System.Drawing.Point(113, 64)
        Me.Button_CancelSearch.Name = "Button_CancelSearch"
        Me.Button_CancelSearch.Size = New System.Drawing.Size(75, 23)
        Me.Button_CancelSearch.TabIndex = 3
        Me.Button_CancelSearch.Text = "Cancel"
        Me.Button_CancelSearch.UseVisualStyleBackColor = True
        '
        'SongLoadForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(314, 95)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label_progress)
        Me.Controls.Add(Me.ProgressBar_SongSearch)
        Me.Controls.Add(Me.Button_CancelSearch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "SongLoadForm"
        Me.Text = "Importing Library..."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label_progress As System.Windows.Forms.Label
    Friend WithEvents ProgressBar_SongSearch As System.Windows.Forms.ProgressBar
    Friend WithEvents Button_CancelSearch As System.Windows.Forms.Button
End Class
