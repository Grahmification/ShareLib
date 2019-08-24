Imports System.Threading
Public Class DLManager_Form

    Private _DownloadingSongs As List(Of Song)
    Private _FinishedSongs As List(Of Song)

    Public Event DownloadStarted(ByVal S As Song, ByVal UserID As String)
    Public Event DownloadFinished(ByVal S As Song, ByVal UserID As String)

    Public Sub Add_Song(ByVal _Song As Song, ByVal UserName As String)

        _DownloadingSongs.Add(_Song)
        Populate_Download_Listview(ListView_Downloads)

        Dim T1 As New Thread(AddressOf Download_Song)
        ThreadList.Add(T1)
        T1.Start(Dlsong)
    End Sub

End Class