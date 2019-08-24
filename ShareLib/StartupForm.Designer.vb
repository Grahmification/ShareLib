<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Startup_Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Startup_Form))
        Me.TabControl_Setup = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBox_Name_Last = New System.Windows.Forms.TextBox()
        Me.TextBox_Setup_Name_First = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox_ServerPassword = New System.Windows.Forms.TextBox()
        Me.Btn_ServerPassword = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Panel_LibrarySelect = New System.Windows.Forms.Panel()
        Me.RichTextBox_ItunesLoc = New System.Windows.Forms.RichTextBox()
        Me.Btn_ConfirmMediaLoc = New System.Windows.Forms.Button()
        Me.Button_Browse1 = New System.Windows.Forms.Button()
        Me.TextBox_ItunesMediaLoc = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabControl_Setup.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Panel_LibrarySelect.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl_Setup
        '
        Me.TabControl_Setup.Controls.Add(Me.TabPage1)
        Me.TabControl_Setup.Controls.Add(Me.TabPage2)
        Me.TabControl_Setup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_Setup.Location = New System.Drawing.Point(0, 0)
        Me.TabControl_Setup.Name = "TabControl_Setup"
        Me.TabControl_Setup.SelectedIndex = 0
        Me.TabControl_Setup.Size = New System.Drawing.Size(370, 365)
        Me.TabControl_Setup.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.DarkGray
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.TextBox_Name_Last)
        Me.TabPage1.Controls.Add(Me.TextBox_Setup_Name_First)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.TextBox_ServerPassword)
        Me.TabPage1.Controls.Add(Me.Btn_ServerPassword)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(362, 339)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Step 1"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(130, 155)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 13)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Enter your last name:"
        '
        'TextBox_Name_Last
        '
        Me.TextBox_Name_Last.Location = New System.Drawing.Point(94, 171)
        Me.TextBox_Name_Last.Name = "TextBox_Name_Last"
        Me.TextBox_Name_Last.Size = New System.Drawing.Size(177, 20)
        Me.TextBox_Name_Last.TabIndex = 6
        '
        'TextBox_Setup_Name_First
        '
        Me.TextBox_Setup_Name_First.Location = New System.Drawing.Point(94, 131)
        Me.TextBox_Setup_Name_First.Name = "TextBox_Setup_Name_First"
        Me.TextBox_Setup_Name_First.Size = New System.Drawing.Size(177, 20)
        Me.TextBox_Setup_Name_First.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(130, 115)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Enter your first name:"
        '
        'TextBox_ServerPassword
        '
        Me.TextBox_ServerPassword.Location = New System.Drawing.Point(94, 66)
        Me.TextBox_ServerPassword.Name = "TextBox_ServerPassword"
        Me.TextBox_ServerPassword.Size = New System.Drawing.Size(177, 20)
        Me.TextBox_ServerPassword.TabIndex = 3
        '
        'Btn_ServerPassword
        '
        Me.Btn_ServerPassword.Enabled = False
        Me.Btn_ServerPassword.Location = New System.Drawing.Point(93, 233)
        Me.Btn_ServerPassword.Name = "Btn_ServerPassword"
        Me.Btn_ServerPassword.Size = New System.Drawing.Size(182, 34)
        Me.Btn_ServerPassword.TabIndex = 2
        Me.Btn_ServerPassword.Text = "Confirm Name and Password"
        Me.Btn_ServerPassword.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 18)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Step 1."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(114, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Enter the server password:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Panel_LibrarySelect)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(362, 339)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Step 2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Panel_LibrarySelect
        '
        Me.Panel_LibrarySelect.BackColor = System.Drawing.Color.DarkGray
        Me.Panel_LibrarySelect.Controls.Add(Me.RichTextBox_ItunesLoc)
        Me.Panel_LibrarySelect.Controls.Add(Me.Btn_ConfirmMediaLoc)
        Me.Panel_LibrarySelect.Controls.Add(Me.Button_Browse1)
        Me.Panel_LibrarySelect.Controls.Add(Me.TextBox_ItunesMediaLoc)
        Me.Panel_LibrarySelect.Controls.Add(Me.Label5)
        Me.Panel_LibrarySelect.Controls.Add(Me.Label4)
        Me.Panel_LibrarySelect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_LibrarySelect.Location = New System.Drawing.Point(3, 3)
        Me.Panel_LibrarySelect.Name = "Panel_LibrarySelect"
        Me.Panel_LibrarySelect.Size = New System.Drawing.Size(356, 333)
        Me.Panel_LibrarySelect.TabIndex = 0
        '
        'RichTextBox_ItunesLoc
        '
        Me.RichTextBox_ItunesLoc.BackColor = System.Drawing.Color.DarkGray
        Me.RichTextBox_ItunesLoc.Location = New System.Drawing.Point(7, 145)
        Me.RichTextBox_ItunesLoc.Name = "RichTextBox_ItunesLoc"
        Me.RichTextBox_ItunesLoc.ReadOnly = True
        Me.RichTextBox_ItunesLoc.Size = New System.Drawing.Size(339, 141)
        Me.RichTextBox_ItunesLoc.TabIndex = 9
        Me.RichTextBox_ItunesLoc.Text = ""
        '
        'Btn_ConfirmMediaLoc
        '
        Me.Btn_ConfirmMediaLoc.Enabled = False
        Me.Btn_ConfirmMediaLoc.Location = New System.Drawing.Point(105, 108)
        Me.Btn_ConfirmMediaLoc.Name = "Btn_ConfirmMediaLoc"
        Me.Btn_ConfirmMediaLoc.Size = New System.Drawing.Size(137, 31)
        Me.Btn_ConfirmMediaLoc.TabIndex = 6
        Me.Btn_ConfirmMediaLoc.Text = "Confirm Location"
        Me.Btn_ConfirmMediaLoc.UseVisualStyleBackColor = True
        '
        'Button_Browse1
        '
        Me.Button_Browse1.Location = New System.Drawing.Point(249, 63)
        Me.Button_Browse1.Name = "Button_Browse1"
        Me.Button_Browse1.Size = New System.Drawing.Size(91, 23)
        Me.Button_Browse1.TabIndex = 5
        Me.Button_Browse1.Text = "Browse"
        Me.Button_Browse1.UseVisualStyleBackColor = True
        '
        'TextBox_ItunesMediaLoc
        '
        Me.TextBox_ItunesMediaLoc.Location = New System.Drawing.Point(32, 64)
        Me.TextBox_ItunesMediaLoc.Name = "TextBox_ItunesMediaLoc"
        Me.TextBox_ItunesMediaLoc.Size = New System.Drawing.Size(200, 20)
        Me.TextBox_ItunesMediaLoc.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(76, 32)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(194, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Select your itunes media folder location:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 18)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Step 2."
        '
        'Startup_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(370, 365)
        Me.Controls.Add(Me.TabControl_Setup)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Startup_Form"
        Me.Text = "Setup"
        Me.TabControl_Setup.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.Panel_LibrarySelect.ResumeLayout(False)
        Me.Panel_LibrarySelect.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl_Setup As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBox_Name_Last As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_Setup_Name_First As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBox_ServerPassword As System.Windows.Forms.TextBox
    Friend WithEvents Btn_ServerPassword As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Panel_LibrarySelect As System.Windows.Forms.Panel
    Friend WithEvents RichTextBox_ItunesLoc As System.Windows.Forms.RichTextBox
    Friend WithEvents Btn_ConfirmMediaLoc As System.Windows.Forms.Button
    Friend WithEvents Button_Browse1 As System.Windows.Forms.Button
    Friend WithEvents TextBox_ItunesMediaLoc As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
