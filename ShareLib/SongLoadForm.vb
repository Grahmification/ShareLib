Imports System.Threading
Imports System.IO
Imports ID3TagLibrary
Imports HundredMilesSoftware.UltraID3Lib
Public Class SongLoadForm

    Public Delegate Sub cancelbar_del()
    Public Delegate Sub UpdateProgress_del(ByVal NumCompleted As Integer, ByVal TotalFiles As Integer, ByVal ImportMethod As Integer)

    Public Sub New()
        InitializeComponent()
        AddHandler Self.SongImported, AddressOf UpdateProgress
        Me.Show()
        Dim T1 As New Thread(AddressOf doWork)
        T1.Start()
    End Sub
    Private Sub doWork(ByVal path As String)
        Try
            Self.RefreshSongList()
            LibraryUpdated = True

        Catch ex As TaskCanceledException

        Catch ex As ID3FileException
            MessageBox.Show("Error: Unable to load library - error reading music files.")

        Catch ex As DirectoryNotFoundException
            MessageBox.Show("Error: Unable to load library - the path is incorrect.", "Library path incorrect")

        Catch ex As Exception
            MessageBox.Show("Error: Unable to load library.")
        Finally
            cancelbar()
        End Try
    End Sub
    Public Sub UpdateProgress(ByVal NumCompleted As Integer, ByVal TotalFiles As Integer, ByVal ImportMethod As Integer)
        If NumCompleted Mod 10 = 0 Then
            If ProgressBar_SongSearch.InvokeRequired Then
                Invoke(New UpdateProgress_del(AddressOf UpdateProgress), NumCompleted, TotalFiles, ImportMethod)
            Else
                ProgressBar_SongSearch.Value = 100 * CDbl(NumCompleted) / CDbl(TotalFiles)
                Label_progress.Text = NumCompleted & " / " & TotalFiles & " songs"
            End If
        End If
    End Sub

    Public Sub cancelbar()
        If Me.InvokeRequired Then
            Invoke(New cancelbar_del(AddressOf cancelbar))
        Else
            RemoveHandler Self.SongImported, AddressOf UpdateProgress
            Me.Hide()
            Me.Dispose()
        End If
    End Sub
    Private Sub Button_CancelSearch_Click(sender As Object, e As EventArgs) Handles Button_CancelSearch.Click
        Self.CancelRefresh()
        cancelbar()
    End Sub


End Class