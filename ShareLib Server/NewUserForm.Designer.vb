<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewUserForm
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox_NewUserIP = New System.Windows.Forms.TextBox()
        Me.TextBox_NewUserName = New System.Windows.Forms.TextBox()
        Me.Btn_Cancel = New System.Windows.Forms.Button()
        Me.Button_AddNewUser = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "IP Address"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "User Name"
        '
        'TextBox_NewUserIP
        '
        Me.TextBox_NewUserIP.Location = New System.Drawing.Point(79, 35)
        Me.TextBox_NewUserIP.Name = "TextBox_NewUserIP"
        Me.TextBox_NewUserIP.Size = New System.Drawing.Size(143, 20)
        Me.TextBox_NewUserIP.TabIndex = 9
        '
        'TextBox_NewUserName
        '
        Me.TextBox_NewUserName.Location = New System.Drawing.Point(79, 9)
        Me.TextBox_NewUserName.Name = "TextBox_NewUserName"
        Me.TextBox_NewUserName.Size = New System.Drawing.Size(143, 20)
        Me.TextBox_NewUserName.TabIndex = 8
        '
        'Btn_Cancel
        '
        Me.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Btn_Cancel.Location = New System.Drawing.Point(132, 67)
        Me.Btn_Cancel.Name = "Btn_Cancel"
        Me.Btn_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Btn_Cancel.TabIndex = 7
        Me.Btn_Cancel.Text = "Cancel"
        Me.Btn_Cancel.UseVisualStyleBackColor = True
        '
        'Button_AddNewUser
        '
        Me.Button_AddNewUser.Location = New System.Drawing.Point(25, 68)
        Me.Button_AddNewUser.Name = "Button_AddNewUser"
        Me.Button_AddNewUser.Size = New System.Drawing.Size(75, 23)
        Me.Button_AddNewUser.TabIndex = 6
        Me.Button_AddNewUser.Text = "Add"
        Me.Button_AddNewUser.UseVisualStyleBackColor = True
        '
        'NewUserForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(242, 101)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox_NewUserIP)
        Me.Controls.Add(Me.TextBox_NewUserName)
        Me.Controls.Add(Me.Btn_Cancel)
        Me.Controls.Add(Me.Button_AddNewUser)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "NewUserForm"
        Me.Text = "Add New User"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox_NewUserIP As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_NewUserName As System.Windows.Forms.TextBox
    Friend WithEvents Btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents Button_AddNewUser As System.Windows.Forms.Button
End Class
