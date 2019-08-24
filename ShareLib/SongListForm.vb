Imports System.Threading
Public Class SongListForm
    Dim User_ID As String
    

    Public ReadOnly Property ID As String
        Get
            Return User_ID
        End Get
    End Property
    Public Sub New(ByVal ID As String)
        Try
            InitializeComponent()
            Songforms.Add(Me)
            User_ID = ID

            Me.Visible = True
            Me.Show()
            Me.Text = UL.User(ID).Name & "'s Library"


            If UL.User(ID).songlist IsNot Nothing Then 'if user has songlist saved from before then load it
                Populate_datagridView(UL.User(ID).songlist, DataGridView_Library, Bindsource)
            End If
        Catch ex As Exception
            MessageBox.Show("Unable to load previously saved list. Requires refresh.", "Requires Refresh")
        End Try
    End Sub
    Private Sub SongListForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Songforms.Remove(Me)
    End Sub


    Private Sub RefreshMusic()
        Try
            UL.User(User_ID).RefreshMusic(Self.Key, Self.Name)
            Populate_datagridView(UL.User(User_ID).songlist, DataGridView_Library, Bindsource)
        Catch ex As Exception
            MessageBox.Show("Error refreshing library. User may have disconnected.")
        End Try
    End Sub


    Dim dtable As New DataTable
    Dim Bindsource As New BindingSource
    Delegate Sub Populate_datagridView_del(ByVal Songs As List(Of Song), ByVal GridView As DataGridView, ByVal Bindsor As BindingSource)
    Private Sub Populate_datagridView(ByVal Songs As List(Of Song), ByVal GridView As DataGridView, ByVal Bindsor As BindingSource)
        If GridView.InvokeRequired Then
            Invoke(New Populate_datagridView_del(AddressOf Populate_datagridView), Songs, GridView, Bindsor)
        Else

            If GridView.ColumnCount = 0 Then
                dtable.Columns.Add("Title")
                dtable.Columns.Add("Artist")
                dtable.Columns.Add("Album")
                dtable.Columns.Add("Bitrate")


                Bindsource.DataSource = dtable
                DataGridView_Library.DataSource = Bindsource

                Dim col As New DataGridViewButtonColumn
                col.Name = "Download"
                col.HeaderText = "Download"
                col.Text = "Download"
                col.FillWeight = 50
                col.UseColumnTextForButtonValue = True
                DataGridView_Library.Columns.Add(col)
            End If

            dtable.Clear()
            DataGridView_Library.DataSource = Nothing

            For index As Integer = 0 To Songs.Count - 1
                Dim row As DataRow = dtable.NewRow

                row("Title") = Songs(index).Title
                row("Artist") = Songs(index).Artist
                row("Album") = Songs(index).Album
                row("Bitrate") = Songs(index).Bitrate
                dtable.Rows.Add(row)
            Next

            DataGridView_Library.DataSource = Bindsource

            ToolStripStatusLabel_NumSongs.Text = CStr(Songs.Count) & " Songs"

            GridView.Columns.GetLastColumn(DataGridViewElementStates.Displayed, DataGridViewElementStates.None).FillWeight = 25 'make bitrate column small
            GridView.AutoSize = True
            GridView.AutoResizeColumns(System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells)

            For Each c As DataGridViewColumn In GridView.Columns
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            Next

        End If
    End Sub


#Region "Toolstrip"
    Private Sub TSButton_Refresh_Click(sender As Object, e As EventArgs) Handles ToolStripButton_Refresh.Click
        Dim btn As ToolStripButton = sender
        If UL.User(User_ID).online Then
            Dim T1 As New Thread(AddressOf RefreshMusic)
            T1.Start()
        Else
            MessageBox.Show("The user is not online.")
        End If
    End Sub

    Private Sub TSTextBox_Search_KeyUp(sender As Object, e As KeyEventArgs) Handles ToolStripTextBox_Search.KeyUp
        Bindsource.Filter = "Title like '%" & ToolStripTextBox_Search.Text & "%'"
        ToolStripStatusLabel_NumSongs.Text = CStr(Bindsource.List.Count) & " Songs"
    End Sub
    Private Sub TSTextBox_Search_GotFocus(sender As Object, e As EventArgs) Handles ToolStripTextBox_Search.GotFocus
        ToolStripTextBox_Search.Text = ""
    End Sub
    Private Sub TSTextBox_Search_LostFocus(sender As Object, e As EventArgs) Handles ToolStripTextBox_Search.LostFocus
        If ToolStripTextBox_Search.Text = "" Then
            ToolStripTextBox_Search.Text = "Search..."
        End If
    End Sub

#End Region

End Class