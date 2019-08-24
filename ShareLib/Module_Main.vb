Imports System.Net.Sockets
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Xml.Serialization
Imports ID3TagLibrary
Imports HundredMilesSoftware.UltraID3Lib
Imports System.Security.Cryptography
Public Module Module_Main


#Region "Constants"
    Public Const ServerIP As String = "172.16.1.78"
    Public Const Serverkey As String = "dsljfne234kjn3fsdf4"
    Public Const Serverport As Integer = 8000
    Public Const Userport As Integer = 8001



    Public Const Sep_song As Char = "|"
    Public Const Sep_field As Char = "`"
    Public Const StringSep As Char = ","

    Public ContactSavePath As String = Application.StartupPath & "\Users"
    Public SelfSavePath As String = Application.StartupPath & "\self"
    Public DlListSavePath As String = Application.StartupPath & "\Download_history"
#End Region
#Region "Message Constants"
    Public Const ServerCorrectPass = "Password Correct"

    Public Const UserReplyConfirm As String = "ok"
    Public Const UserOnline As String = "online"
    Public Const UserOffline As String = "offline"
    Public Const UserMusicListRequest As String = "sendmusiclist"
    Public Const UserSongDLRequest As String = "downloadSong"
#End Region

    Public Class Song
        Private _title As String = Nothing
        Private _artist As String = Nothing
        Private _album As String = Nothing
        Private _duration As String = Nothing
        Private _bitrate As String = Nothing
        Private _path As String = Nothing
        Private _filesize As String = Nothing

        Public ReadOnly Property Title
            Get
                Return _title
            End Get
        End Property
        Public ReadOnly Property Artist
            Get
                Return _artist
            End Get
        End Property
        Public ReadOnly Property Album
            Get
                Return _album
            End Get
        End Property
        Public ReadOnly Property Duration
            Get
                Return _duration
            End Get
        End Property
        Public ReadOnly Property Bitrate
            Get
                Return _bitrate
            End Get
        End Property
        Public ReadOnly Property path
            Get
                Return _path
            End Get
        End Property
        Public ReadOnly Property filesize
            Get
                Return _filesize
            End Get
        End Property

        Public Sub New()

        End Sub
        Public Sub New(ByVal Title As String, ByVal artist As String, ByVal album As String, ByVal duration As String, ByVal bitrate As String, ByVal filesize As String)
            _title = Title
            _artist = artist
            _album = album
            _duration = duration
            _bitrate = bitrate
            _filesize = filesize
        End Sub 'for remote files
        Public Function import(ByVal filepath As String) As Integer
            _path = filepath

            Dim importmethod As Integer = -1

            '---------------- Get file size --------------

            Dim myFile As New FileInfo(_path)
            Dim sizeInMB As Long = myFile.Length / (1000 * 1000)
            _filesize = CStr(sizeInMB)

            '--------------- Read MP3 Data ----------------
            Dim mp3 As New UltraID3
            mp3.Read(_path)

            If mp3.Title = "" And mp3.Album = "" And mp3.Artist = "" Then 'could not retrieve data, try other method

                Dim mp3_2 As New ID3TagLibrary.MP3File(_path)
                _artist = mp3_2.Artist
                _title = mp3_2.Title
                _album = mp3_2.Album

                importmethod = 1
            Else 'first method worked, get data

                _title = mp3.Title
                _artist = mp3.Artist
                _album = mp3.Album
                _duration = mp3.Duration.Minutes & ":" & mp3.Duration.Seconds
                _bitrate = mp3.FirstMPEGFrameInfo.Bitrate

                importmethod = 0
            End If

            If _title = "" Then 'both methods failed, get song name from file name
                Dim parts() As String = _path.Split("\")
                _title = parts(parts.Length - 1)

                importmethod = 2
            End If

            Return importmethod
        End Function 'for local files

    End Class
    <Serializable>
    Public Class Remote_User
        Private _Name As String
        Private _Key As String
        Private _online As Boolean
        Private _IpAddress As String
        Private _songList As List(Of Song)

        Public Event SongListRefreshed(ByVal songlist As List(Of Song))
        Public Event StatusChanged(ByVal key As String)

        Public Sub New()
        End Sub
        Public Sub New(ByVal Name As String, ByVal key As String, ByVal online As Boolean, ByVal IP As String, ByVal songlist As List(Of Song))
            _Name = Name
            _Key = key
            _online = online
            _IpAddress = IP
            _songList = songlist
        End Sub
        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property
        Public ReadOnly Property key() As String
            Get
                Return _Key
            End Get
        End Property
        Public Property online() As Boolean
            Get
                Return _online
            End Get
            Set(value As Boolean)
                _online = value
                RaiseEvent StatusChanged(_Key)
            End Set
        End Property
        Public ReadOnly Property IpAddress() As String
            Get
                Return _IpAddress
            End Get
        End Property
        Public ReadOnly Property songlist() As List(Of Song)
            Get
                Return _songList
            End Get
        End Property


        Private Function UserConnect(ByVal MyKey As String, ByVal MyName As String) As ClientConnection
            Dim output As ClientConnection = New ClientConnection(_IpAddress, Userport, False)

            output.Send_Bytes(Conversions.ObjectToBytes(Serverkey))
            output.Send_Bytes(Conversions.ObjectToBytes(MyKey))
            output.Send_Bytes(Conversions.ObjectToBytes(MyName))
            Dim receiveString As String = Conversions.BytesToObject(output.Receive_Bytes())

            If receiveString = UserReplyConfirm Then
                Return output
            Else
                Return Nothing
            End If
        End Function
        Public Sub RefreshMusic(ByVal MyKey As String, ByVal MyName As String)
            Dim connection As ClientConnection = UserConnect(MyKey, MyName)
            connection.Send_Bytes(Conversions.ObjectToBytes(UserMusicListRequest))
            _songList = Conversions.BytesToSongList(connection.Receive_Bytes)
            RaiseEvent SongListRefreshed(_songList)
        End Sub

        <MTAThread()>
        Public Sub AnnounceStatus(ByVal online As Boolean, ByRef waitfor As ManualResetEvent, ByVal mykey As String, ByVal myname As String)
            Dim param As New Param_data(online, waitfor, mykey, myname)
            ThreadPool.QueueUserWorkItem(AddressOf AnnounceStatus_DoWork, param)
        End Sub
        Private Sub AnnounceStatus_DoWork(ByVal param As Param_data)
            Try
                Dim con As ClientConnection = UserConnect(param.Mykey, param.Myname)

                If param.online Then
                    con.Send_Bytes(Conversions.ObjectToBytes(UserOnline))
                Else
                    con.Send_Bytes(Conversions.ObjectToBytes(UserOffline))
                End If

                If param.online Then ' can connect... the user is online
                    online = True
                Else
                    Throw New TimeoutException("User cannot be online")
                End If


            Catch ex As Exception
                online = False
            Finally
                param.WaitHandle.Set()
            End Try
        End Sub
        Private Class Param_data
            Private _Online As Boolean
            Private _waithandle As ManualResetEvent
            Private _mykey As String
            Private _myName As String

            Public Sub New(ByVal online As Boolean, ByRef waithandle As ManualResetEvent, ByVal mykey As String, ByVal myName As String)
                _Online = online
                _waithandle = waithandle
                _mykey = mykey
                _myName = myName
            End Sub
            Public ReadOnly Property online() As Boolean
                Get
                    Return _Online
                End Get
            End Property
            Public ReadOnly Property WaitHandle() As ManualResetEvent
                Get
                    Return _waithandle
                End Get
            End Property
            Public ReadOnly Property Mykey As String
                Get
                    Return _mykey
                End Get
            End Property
            Public ReadOnly Property Myname As String
                Get
                    Return _myName
                End Get
            End Property
        End Class

    End Class
    <Serializable>
    Public Class Self_Class
        Private _Name As String = Nothing
        Private _MusicPath As String = Nothing
        Private _key As String = Nothing
        Private _ReceivePath As String = Nothing
        Private _songList As List(Of Song) = New List(Of Song)

        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property
        Public ReadOnly Property MusicPath() As String
            Get
                Return _MusicPath
            End Get
        End Property
        Public ReadOnly Property Key() As String
            Get
                Return _key
            End Get
        End Property
        Public ReadOnly Property ReceivePath As String
            Get
                Return _ReceivePath
            End Get
        End Property
        Public ReadOnly Property songlist() As List(Of Song)
            Get
                Return _songList
            End Get
        End Property

        Public Sub New()

        End Sub
        Public Sub New(ByVal name As String, ByVal musicpath As String, ByVal key As String, ByVal receivepath As String)
            _Name = name
            _MusicPath = musicpath
            _key = key
            _ReceivePath = receivepath
        End Sub

        Private _continueRefresh As Boolean = True 'refresh will continue as long as true

        Public Event SongImported(ByVal NumCompleted As Integer, ByVal TotalFiles As Integer, ByVal ImportMethod As Integer)
        Public Event ImportCancelled(ByVal completed As Integer, ByVal total As Integer)
        Public Event ImportFinished(ByVal Numfiles As Integer)
        Public Sub CancelRefresh()
            _continueRefresh = False
        End Sub
        Public Sub RefreshSongList()
            _continueRefresh = True
            Dim ext As String() = {"*.mp3", "*.mp4"} 'file exstensions to filter
            Dim Allfiles As String() = ext.SelectMany(Function(f) Directory.GetFiles(_MusicPath, f, SearchOption.AllDirectories)).ToArray 'get paths of all files in folder w/ filter

            Dim _tmp As List(Of Song) = New List(Of Song) 'temporary list to fill with songs

            For index As Integer = 0 To Allfiles.Length - 1 'import all songs
                If _continueRefresh = True Then

                    Dim tmpsong As New Song
                    Dim method As Integer = tmpsong.import(Allfiles(index))
                    _tmp.Add(tmpsong)

                    RaiseEvent SongImported(index + 1, Allfiles.Length, method)

                Else 'import has been cancelled

                    RaiseEvent ImportCancelled(index + 1, Allfiles.Length)
                    Return

                End If
            Next

            _songList = _tmp 'overwrite user songlist
            RaiseEvent ImportFinished(Allfiles.Length)
        End Sub


        Public Sub SavetoFile(ByVal path As String)
            Conversions.ObjectToXML(Me, path)
        End Sub
        Public Sub LoadFromFile(ByVal path As String)
            Conversions.XMLToObject(path, Me)
        End Sub

    End Class

    Public WithEvents Self As New Self_Class
    'Public WithEvents UserList As New Dictionary(Of String, Remote_User)
    Public WithEvents UL As New Multiple_User_Fns
    Public WithEvents ServConList As New List(Of ServerConnection)
    Public DefaultForm As Mainwindow
    Public Songforms As New List(Of SongListForm)

    Public Class Multiple_User_Fns

        Public Event UserAdded(ByVal key As String)
        Public Event Userdeleted(ByVal key As String)
        Public Event UserUpdated(ByVal Key As String)

        Public ReadOnly Property User(ByVal key As String) As Remote_User
            Get
                Return _UserList(key)
            End Get
        End Property
        Public ReadOnly Property Keys As Dictionary(Of String, Remote_User).KeyCollection
            Get
                Return _UserList.Keys
            End Get
        End Property

        Private WithEvents _UserList As New Dictionary(Of String, Remote_User)
        Public Sub AddUser(ByVal Name As String, ByVal key As String, ByVal online As Boolean, ByVal IP As String, ByVal songlist As List(Of Song))
            Dim contact As New Remote_User(Name, key, online, IP, songlist)

            AddHandler contact.StatusChanged, AddressOf UserDataChange
            AddHandler contact.SongListRefreshed, AddressOf HandlesEvents

            _UserList.Add(key, contact)
            RaiseEvent UserAdded(key)
        End Sub 'add to list and add handlers for each user
        Public Sub AddUser(ByVal Contact As Remote_User, ByVal key As String)


            AddHandler Contact.StatusChanged, AddressOf UserDataChange
            AddHandler Contact.SongListRefreshed, AddressOf HandlesEvents

            _UserList.Add(key, Contact)
            RaiseEvent UserAdded(key)
        End Sub
        Private Sub HandlesEvents()

        End Sub
        Public Sub DeleteUser(ByVal key As String)
            Dim delcont As Remote_User = _UserList(key)

            AddHandler delcont.StatusChanged, AddressOf UserDataChange
            AddHandler delcont.SongListRefreshed, AddressOf HandlesEvents

            _UserList.Remove(key)
            RaiseEvent Userdeleted(key)
        End Sub



        Private Sub UpdateUserList(ByVal newlist As Dictionary(Of String, Remote_User))

            For Each oldkey As String In _UserList.Keys.ToArray
                If newlist.Keys.Contains(oldkey) = False Then 'user does not exist any more, must delete
                    DeleteUser(oldkey)
                End If
            Next

            For Each Updatedkey As String In newlist.Keys.ToArray
                If _UserList.Keys.Contains(Updatedkey) Then 'user already exists, only needs to be modified
                    _UserList(Updatedkey) = newlist(Updatedkey)
                    RaiseEvent UserUpdated(Updatedkey)
                Else 'key is not in old list, need to add new user
                    AddUser(newlist(Updatedkey), Updatedkey)
                End If
            Next

        End Sub 'makes use of overwrite event

        <MTAThread()>
        Public Sub Announce_All(ByVal online As Boolean, ByVal mykey As String, ByVal myname As String)
            Dim userkeys As String() = _UserList.Keys.ToArray
            If userkeys IsNot Nothing Then
                Dim waitfor(userkeys.Length - 1) As ManualResetEvent
                ThreadPool.SetMinThreads(20, 20)

                For i As Integer = 0 To userkeys.Count - 1

                    waitfor(i) = New ManualResetEvent(False)
                    _UserList(userkeys(i)).AnnounceStatus(online, waitfor(i), mykey, myname)

                Next i

                For i As Integer = 0 To userkeys.Count - 1
                    waitfor(i).WaitOne()
                Next
            End If
        End Sub
        Public Sub Load_Users_Server(ByVal mykey As String)
            Try
                Dim newlist As New Dictionary(Of String, Remote_User)
                LoadfromFile(ContactSavePath) 'try to load from saved file

                '----------------------------- Get Updated List from server ----------------------------------
                Dim con As New ClientConnection(ServerIP, Serverport, False)

                con.Send_Bytes(Conversions.ObjectToBytes(mykey))
                Dim receivestring As String = Conversions.BytesToObject(con.Receive_Bytes) 'get whether password is correct

                If receivestring <> ServerCorrectPass Then
                    Throw New System.Exception("Incorrect Password")
                End If

                receivestring = Conversions.BytesToObject(con.Receive_Bytes) 'get user list
                Dim userdata As String() = receivestring.Split(StringSep)

                '------------------------------ Convert updated data to new user list -------------------------
                If receivestring <> "" Then

                    Dim i As Integer = 0

                    While i < userdata.Length

                        Dim name As String = userdata(i)
                        Dim IP As String = userdata(i + 1)
                        Dim key As String = userdata(i + 2)
                        i += 3
                        Dim online As Boolean = False
                        Dim songlist As List(Of Song) = New List(Of Song)


                        If _UserList.Count > 0 And _UserList.Keys.Contains(key) Then 'for already existing info
                            Dim tmp As Remote_User = _UserList(key)
                            If tmp IsNot Nothing Then
                                songlist = tmp.songlist
                                online = tmp.online
                            End If
                        End If

                        Dim Contact As New Remote_User(name, key, online, IP, songlist)
                        newlist.Add(key, Contact)
                    End While
                End If

                UpdateUserList(newlist)
            Catch ex As IOException
                MessageBox.Show("Unable to connect to server, displaying last saved user list (User list saves upon program close). ")
            Catch ex As TimeoutException
                MessageBox.Show("Unable to connect to server, displaying last saved user list (User list saves upon program close). ")
            Catch ex As Exception
                MessageBox.Show("Unable to read data from server, displaying last saved user list (User list saves upon program close).")
            End Try
        End Sub


        Public Sub SaveToFile(ByVal Path As String)
            Conversions.ObjectToXML(_UserList, Path)
        End Sub
        Public Sub LoadfromFile(ByVal Path As String)
            Dim newlist As New Dictionary(Of String, Remote_User)
            Conversions.XMLToObject(Path, newlist)
            UpdateUserList(newlist)
        End Sub

    End Class
    Private Sub UserDataChange(ByVal key As String) Handles UL.UserAdded, UL.Userdeleted, UL.UserUpdated
        DefaultForm.Populate_Listview(UL.Keys.ToArray, DefaultForm.ListView_Contacts)
    End Sub
  
    Public LibraryUpdated As Boolean = False
    Public Connected As Boolean = False


    Public Sub Online(ByVal State As Boolean)
        Try
            If State = True Then
                NewConnection() 'start listening
                Connected = True

                UL.Load_Users_Server(Self.Key)

            Else 'close all connections
                Connected = False
                Dim l As Integer = ServConList.Count
                For i As Integer = 0 To l - 1
                    ServConList(i).Disconnect()
                Next
            End If

            UL.Announce_All(State, Self.Key, Self.Name)
            DefaultForm.Populate_Listview(UL.Keys.ToArray, DefaultForm.ListView_Contacts)

        Catch ex As IOException
            MessageBox.Show("Unable to connect to server, displaying last saved user list (User list saves upon program close). " & vbNewLine & ex.ToString)
        Catch ex As TimeoutException
            MessageBox.Show("Unable to connect to server, displaying last saved user list (User list saves upon program close). " & vbNewLine & ex.ToString)
        Catch ex As Exception
            Call DefaultForm.ConnectBtn_Enable(Not State)
        Finally
            Call DefaultForm.ConnectBtn_Enable(State)
        End Try
    End Sub
    Public Sub NewConnection()
        Dim sCon As New ServerConnection(Userport)
        ServConList.Add(sCon)
        AddHandler sCon.Connected_Server, AddressOf OnConnect
        AddHandler sCon.Disconnecting_Server, AddressOf OnDisconnect
    End Sub 'adds connection handlers and list
    Public Sub OnConnect(ByRef Con As ServerConnection, ByVal Asynch As Boolean)
        Try
            If Connected = True Then

                NewConnection() '---- reconnect

                '---------------- Authenticate Client ------------------

                Dim servkey As String = Conversions.BytesToObject(Con.Receive_Bytes) 'server key is sent first
                Dim clientKey As String = Conversions.BytesToObject(Con.Receive_Bytes) 'personal key is sent second
                Dim clientName As String = Conversions.BytesToObject(Con.Receive_Bytes) 'name is sent third

                If Not UL.Keys.Contains(clientKey) And servkey = Serverkey Then 'case of new user
                    UL.AddUser(clientName, clientKey, True, Con.IP, New List(Of Song))
                End If

                If servkey = Serverkey Then 'if authenticated 
                    Con.Send_Bytes(Conversions.ObjectToBytes(UserReplyConfirm))
                    HandleFunctions(Con, clientKey)
                Else
                    Throw New KeyNotFoundException("wrong key")
                End If


            Else
                Throw New IOException("online disabled")
            End If
        Catch ex As Exception
        Finally
            Con.Disconnect()
        End Try
    End Sub
    Public Sub HandleFunctions(ByRef Con As ServerConnection, ByVal clientkey As String)
        Dim receivestring As String = Conversions.BytesToObject(Con.Receive_Bytes)

        If receivestring = UserOnline Then
            UL.User(clientkey).online = True 'will trigger event
            'DefaultForm.Populate_Listview(UL.Keys.ToArray, DefaultForm.ListView_Contacts)

        ElseIf receivestring = UserOffline Then
            UL.User(clientkey).online = False 'will trigger event
            'DefaultForm.Populate_Listview(UL.Keys.ToArray, DefaultForm.ListView_Contacts)

        ElseIf receivestring = UserMusicListRequest Then
            Con.Send_Bytes(Conversions.SongListToBytes(Self.songlist))

        ElseIf receivestring = UserSongDLRequest Then
            Dim songPath As String = Conversions.BytesToObject(Con.Receive_Bytes)
            Con.Send_Bytes(Conversions.FileToBytes(songPath))

        End If
    End Sub
    Public Sub OnDisconnect(ByRef Con As ServerConnection)
        ServConList.Remove(Con)

        RemoveHandler Con.Connected_Server, AddressOf OnConnect
        RemoveHandler Con.Disconnecting_Server, AddressOf OnDisconnect
    End Sub 'removes connection handlers and list


    Public Class Conversions

        Private Const _EncryptionKey As String = "MAKV2SPBNI99212"
        Private Const _EncryptBytes As Boolean = False
        Private Const _EncryptXML As Boolean = True

        Public ReadOnly Property EncryptBytes As Boolean
            Get
                Return _EncryptBytes
            End Get
        End Property
        Public ReadOnly Property EncryptXML As Boolean
            Get
                Return _EncryptXML
            End Get
        End Property
        Public ReadOnly Property EncryptionKey As String
            Get
                Return _EncryptionKey
            End Get
        End Property

        Public Shared Sub ObjectToXML(ByVal SaveData As Object, ByVal SavePath As String, Optional ByVal FileName As String = Nothing)
            If _EncryptXML Then
                Dim encrypted As Byte() = encrypt(SaveData, _EncryptionKey)
                SaveData = New Object
                SaveData = encrypted
            End If

            If FileName <> Nothing Then
                SavePath = SavePath & "\" & FileName
            End If

            Dim ser As New XmlSerializer(SaveData.GetType)
            Dim fs As New FileStream(SavePath, FileMode.Create)
            ser.Serialize(fs, SaveData)
            fs.Close()
        End Sub
        Public Shared Sub XMLToObject(ByVal LoadPath As String, ByRef LoadData As Object)
            If File.Exists(LoadPath) Then
                If _EncryptXML Then
                    Dim encrypted_data As Byte() = Nothing
                    Dim ser As New XmlSerializer(GetType(Byte()))
                    Dim fs As New FileStream(LoadPath, FileMode.OpenOrCreate)
                    encrypted_data = DirectCast(ser.Deserialize(fs), Byte())
                    fs.Close()

                    LoadData = decrypt(encrypted_data, _EncryptionKey)
                Else '-------------------- run normal code ----------------------------
                    Dim ser As New XmlSerializer(LoadData.GetType)
                    Dim fs As New FileStream(LoadPath, FileMode.OpenOrCreate)
                    LoadData = DirectCast(ser.Deserialize(fs), Object)
                    fs.Close()
                End If

            End If
        End Sub

        Public Shared Function FileToBytes(Path As String) As Byte()

            Dim fullPath As String = Path '& "\" & FileName

            Dim Fs As New FileStream(fullPath, FileMode.Open, FileAccess.Read)

            Dim output(Fs.Length - 1) As Byte

            Fs.Read(output, 0, Fs.Length)
            Fs.Close()

            Return output
        End Function
        Public Shared Function BytesToFile(ByteArr As Byte(), Path As String, Optional ByVal FileName As String = Nothing) As Boolean

            Dim fullPath As String

            If FileName = Nothing Then
                fullPath = Path
            Else
                fullPath = Path & "\" & FileName
            End If

            Dim Fs As New FileStream(fullPath, FileMode.Create, FileAccess.Write)
            Fs.Write(ByteArr, 0, ByteArr.Length)
            Fs.Close()

            Return True
        End Function

        Public Shared Function ObjectToBytes(ByVal Input As Object) As Byte()
            If _EncryptBytes Then
                Return encrypt(Input, _EncryptionKey)
            Else
                Dim MS As New MemoryStream()
                Dim BF As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
                BF.Serialize(MS, Input)
                Return MS.ToArray()
            End If
        End Function
        Public Shared Function BytesToObject(ByVal Input As Byte()) As Object
            If _EncryptBytes Then
                Return decrypt(Input, _EncryptionKey)
            Else
                Dim MS As New MemoryStream()
                MS.Write(Input, 0, Input.Length)
                Dim BF As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
                MS.Seek(0, SeekOrigin.Begin)
                Return BF.Deserialize(MS)
            End If
        End Function

        Public Shared Function SongListToBytes(Input As List(Of Song)) As Byte()
            Dim stringarr(Input.Count - 1) As String

            For Index As Integer = 0 To Input.Count - 1
                stringarr(Index) = Input(Index).Title & Sep_field & Input(Index).Artist & Sep_field & Input(Index).Album & Sep_field & Input(Index).Duration & Sep_field & Input(Index).Bitrate & Sep_field & Input(Index).path & Sep_field & Input(Index).filesize
            Next Index

            Dim combined As String = String.Join(Sep_song, stringarr)
            Dim output As Byte() = ObjectToBytes(combined)
            Return output
        End Function
        Public Shared Function BytesToSongList(Input As Byte()) As List(Of Song)
            Dim InputString As String = BytesToObject(Input)
            Dim stringarr As String() = InputString.Split(Sep_song)
            Dim output As List(Of Song) = New List(Of Song)

            For Index As Integer = 0 To stringarr.Length - 1
                Dim fields As String() = stringarr(Index).Split(Sep_field)
                output.Add(New Song(fields(0), fields(1), fields(2), fields(3), fields(4), fields(5)))
            Next Index

            Return output
        End Function

        Private Shared Function encrypt(ByVal input As Object, ByVal EncryptionKey As String) As Byte()

            Dim _MemoryStream As New MemoryStream()
            Dim _BinaryFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            _BinaryFormatter.Serialize(_MemoryStream, input)
            Dim output As Byte() = _MemoryStream.ToArray()



            Using encryptor As Aes = Aes.Create()
                Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
                 &H65, &H64, &H76, &H65, &H64, &H65, _
                 &H76})
                encryptor.Key = pdb.GetBytes(32)
                encryptor.IV = pdb.GetBytes(16)
                Using ms As New MemoryStream()
                    Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                        cs.Write(output, 0, output.Length)
                        cs.Close()
                        output = ms.ToArray
                    End Using
                End Using
            End Using

            Return output

        End Function
        Private Shared Function decrypt(ByVal input As Byte(), ByVal EncryptionKey As String) As Object

            Using encryptor As Aes = Aes.Create()
                Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
                 &H65, &H64, &H76, &H65, &H64, &H65, _
                 &H76})
                encryptor.Key = pdb.GetBytes(32)
                encryptor.IV = pdb.GetBytes(16)
                Using ms As New MemoryStream()
                    Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                        cs.Write(input, 0, input.Length)
                        cs.Close()
                    End Using
                    input = ms.ToArray()
                End Using
            End Using

            Dim _MemoryStream As New MemoryStream()
            _MemoryStream.Write(input, 0, input.Length)
            Dim _BinaryFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            _MemoryStream.Seek(0, SeekOrigin.Begin)
            Dim output As Object = _BinaryFormatter.Deserialize(_MemoryStream)

            Return output
        End Function

    End Class
    Public Class ServerConnection
        Inherits TCP_Functions

        Public Event Connected_Server(ByRef Connection As ServerConnection, ByVal Asynch As Boolean)
        Public Event Disconnecting_Server(ByRef Connection As ServerConnection)
        Public Event Data_Received(ByRef Connection As ServerConnection, ByVal ReceiveData As Byte(), ByVal Asynch As Boolean)
        Public Event Data_Sent(ByRef Connection As ServerConnection, ByVal BytesSent As Integer, ByVal Asynch As Boolean)

        Private _Listener As TcpListener = Nothing
        Private _Listening As Boolean = False
        Private _client As New TcpClient

        Private _Port As Integer
        Public ReadOnly Property Port As Integer
            Get
                Return _Port
            End Get
        End Property
        Public ReadOnly Property Listening As Boolean
            Get
                Return _Listening
            End Get
        End Property
        Public ReadOnly Property IP As String
            Get
                Dim IPadd As String = Nothing
                Dim ipend As Net.IPEndPoint = _client.Client.RemoteEndPoint
                If Not ipend Is Nothing Then
                    IPadd = ipend.Address.ToString
                End If
                Return IPadd
            End Get
        End Property


        Public Sub New(ByVal port As Integer)
            _Port = port
            _Listener = New TcpListener(IPAddress.Any, _Port)
            _Listener.Start(50)
            _Listening = True
            _Listener.BeginAcceptTcpClient(New AsyncCallback(AddressOf OnAccept), _Listener)
        End Sub
        Private Sub OnAccept(ByVal ar As IAsyncResult)
            Try
                If _Listening Then
                    _client = _Listener.EndAcceptTcpClient(ar)
                    _Listener.Stop()
                    _Listening = False

                    _stream = _client.GetStream()
                    _stream.ReadTimeout = receiveTimeout
                    RaiseEvent Connected_Server(Me, True)
                End If
            Catch ex As Exception
            End Try
        End Sub
        Public Sub Disconnect()
            Try
                RaiseEvent Disconnecting_Server(Me)

                If _Listening Then
                    _Listening = False
                    _Listener.Stop()
                End If

                If Stream IsNot Nothing Then
                    _stream.Close()
                    _client.Close()
                End If
            Catch ex As Exception
            End Try
        End Sub

        Private Sub OnReceive(ByRef stream As NetworkStream, ByVal ReceiveData As Byte(), ByVal Asynch As Boolean) Handles Me.DataReceived
            RaiseEvent Data_Received(Me, ReceiveData, Asynch)
        End Sub 'upgrade receive event
        Private Sub OnSend(ByRef stream As NetworkStream, ByVal BytesSent As Integer, ByVal Asynch As Boolean) Handles Me.DataSent
            RaiseEvent Data_Sent(Me, BytesSent, Asynch)
        End Sub 'upgrade send event
    End Class
    Public Class ClientConnection
        Inherits TCP_Functions

        Public Event Connected_Client(ByRef Connection As ClientConnection, ByVal Asynch As Boolean)
        Public Event Data_Received(ByRef Connection As ClientConnection, ByVal ReceiveData As Byte(), ByVal Asynch As Boolean)
        Public Event Data_Sent(ByRef Connection As ClientConnection, ByVal BytesSent As Integer, ByVal Asynch As Boolean)

        Private _client As New TcpClient

        Public ReadOnly Property IP As String
            Get
                Dim IPadd As String = Nothing
                Dim ipend As Net.IPEndPoint = _client.Client.RemoteEndPoint
                If Not ipend Is Nothing Then
                    IPadd = ipend.Address.ToString
                End If
                Return IPadd
            End Get
        End Property

        Public Sub New(ByVal IP As String, ByVal Port As Integer, ByVal AsynchConnect As Boolean)
            If AsynchConnect = False Then
                Client_Connect(IP, Port)
            Else
                Dim result As IAsyncResult = _client.BeginConnect(IP, Port, New AsyncCallback(AddressOf OnConnect), _client)
            End If
        End Sub
        Private Sub Client_Connect(ByVal IP As String, ByVal Port As Integer)

            Dim result As IAsyncResult = _client.BeginConnect(IP, Port, Nothing, Nothing)
            Dim connected As Boolean = result.AsyncWaitHandle.WaitOne(connectTimeout)

            If connected = False Then
                Throw New TimeoutException("Connect Timed out")
            End If

            _client.EndConnect(result)

            _stream = _client.GetStream()
            RaiseEvent Connected_Client(Me, False)
        End Sub
        Private Sub OnConnect(ByVal ar As IAsyncResult)
            Try
                _client.EndConnect(ar)
                _stream = _client.GetStream
                RaiseEvent Connected_Client(Me, True)
            Catch ex As Exception
            End Try
        End Sub

        Private Sub OnReceive(ByRef stream As NetworkStream, ByVal ReceiveData As Byte(), ByVal Asynch As Boolean) Handles Me.DataReceived
            RaiseEvent Data_Received(Me, ReceiveData, Asynch)
        End Sub 'upgrade receive event
        Private Sub OnSend(ByRef stream As NetworkStream, ByVal BytesSent As Integer, ByVal Asynch As Boolean) Handles Me.DataSent
            RaiseEvent Data_Sent(Me, BytesSent, Asynch)
        End Sub 'upgrade send event
    End Class
    Public Class TCP_Functions

        Const bufferSize As Integer = 1024

        Protected Const sendtimeout As Integer = 1000 * 5
        Protected Const receiveTimeout As Integer = 1000 * 5
        Protected Const connectTimeout As Integer = 1000 * 1
        Protected Const SendKey As String = "jhjkkjhewrt"
        Protected Const CryptKey As String = "45gwwjnr54"

        Protected Event DataReceived(ByRef stream As NetworkStream, ByVal ReceiveData As Byte(), ByVal Asynch As Boolean)
        Protected Event DataSent(ByRef stream As NetworkStream, ByVal BytesSent As Integer, ByVal Asynch As Boolean)

        Protected _stream As NetworkStream = Nothing

        Public ReadOnly Property Stream As NetworkStream
            Get
                Return _stream
            End Get
        End Property

        Private Class AsyncParam
            Private _Buffer As Byte()
            Private _Stream As NetworkStream

            Public Sub New(ByRef Stream As NetworkStream, ByVal Buffer As Byte())
                _Buffer = Buffer
                _Stream = Stream

            End Sub
            Public Property Buffer As Byte()
                Get
                    Return _Buffer
                End Get
                Set(value As Byte())
                    _Buffer = value
                End Set
            End Property

            Public Property Stream As NetworkStream
                Get
                    Return _Stream
                End Get
                Set(value As NetworkStream)
                    _Stream = value
                End Set
            End Property

        End Class

        Public Function Receive_Bytes() As Byte()
            _stream.ReadTimeout = receiveTimeout
            '--------------------------- Get Key ---------------------------------------
            Dim receivekey(encrypt(SendKey).Length - 1) As Byte

            Dim totalRead As Integer = 0
            Dim currentRead As Integer = 0

            While totalRead < receivekey.Length
                currentRead = _stream.Read(receivekey, totalRead, receivekey.Length - totalRead)
                totalRead += currentRead
            End While

            Dim key As String = decrypt(receivekey)
            If key <> SendKey Then
                Throw New KeyNotFoundException("Incorrect receive key")
            End If

            '----------------------- Get message length --------------------------

            Dim receiveLength(3) As Byte

            totalRead = 0
            currentRead = 0

            While totalRead < receiveLength.Length
                currentRead = _stream.Read(receiveLength, totalRead, receiveLength.Length - totalRead)
                totalRead += currentRead
            End While

            Dim messageSize As Integer = BitConverter.ToInt32(receiveLength, 0)

            '----------------------- Get data --------------------------

            totalRead = 0
            currentRead = 0

            Dim receiveData(messageSize - 1) As Byte 'critical for array size 

            While totalRead < receiveData.Length
                currentRead = _stream.Read(receiveData, totalRead, receiveData.Length - totalRead)
                totalRead += currentRead
            End While


            RaiseEvent DataReceived(_stream, receiveData, False)
            Return receiveData
        End Function
        Public Sub Receive_Bytes_Async()
            Dim length As Integer = encrypt(SendKey).Length - 1
            Dim receivekey(length) As Byte
            Dim param As New AsyncParam(_stream, receivekey)

            Dim result As IAsyncResult = _stream.BeginRead(receivekey, 0, receivekey.Length, New AsyncCallback(AddressOf OnReceive), param)
        End Sub
        Private Sub OnReceive(ByVal ar As IAsyncResult)
            Dim param As AsyncParam = CType(ar.AsyncState, AsyncParam)
            param.Stream.EndRead(ar)
            param.Stream.ReadTimeout = receiveTimeout

            Dim key As String = decrypt(param.Buffer)
            If key <> SendKey Then
                Throw New KeyNotFoundException("Incorrect receive key")
            End If

            Dim receiveLength(3) As Byte

            Dim totalRead As Integer = 0
            Dim currentRead As Integer = 0

            While totalRead < receiveLength.Length
                currentRead = param.Stream.Read(receiveLength, totalRead, receiveLength.Length - totalRead)
                totalRead += currentRead
            End While

            totalRead = 0
            currentRead = 0

            Dim messageSize As Integer = BitConverter.ToInt32(receiveLength, 0)
            Dim receiveData(messageSize - 1) As Byte 'critical for array size 

            While totalRead < receiveData.Length
                currentRead = param.Stream.Read(receiveData, totalRead, receiveData.Length - totalRead)
                totalRead += currentRead
            End While

            param.Buffer = receiveData
            RaiseEvent DataReceived(param.Stream, param.Buffer, True)
        End Sub

        Public Function Send_Bytes(ByVal SendData As Byte()) As Integer
            _stream.WriteTimeout = sendtimeout

            Dim tcpkey As Byte() = encrypt(SendKey)
            _stream.Write(tcpkey, 0, tcpkey.Length)


            Dim sendLength As Byte() = BitConverter.GetBytes(SendData.Length)
            _stream.Write(sendLength, 0, sendLength.Length)

            Dim Totalsent As Integer = 0
            Dim Currentsent As Integer = bufferSize

            While (Totalsent < SendData.Length)
                If (SendData.Length - Totalsent < bufferSize) Then
                    Currentsent = SendData.Length - Totalsent
                End If

                _stream.Write(SendData, Totalsent, Currentsent)
                Totalsent += Currentsent
            End While

            Return Totalsent 'return # of bytes sent
        End Function
        Public Sub Send_Bytes_Async(ByVal SendData As Byte())
            Dim tcpkey As Byte() = encrypt(SendKey)
            Dim param As New AsyncParam(_stream, SendData)
            Dim result As IAsyncResult = _stream.BeginWrite(tcpkey, 0, tcpkey.Length, New AsyncCallback(AddressOf OnSend), param)
        End Sub
        Private Sub OnSend(ByVal ar As IAsyncResult)
            Dim param As AsyncParam = CType(ar.AsyncState, AsyncParam)
            param.Stream.WriteTimeout = sendtimeout

            Dim sendLength As Byte() = BitConverter.GetBytes(param.Buffer.Length)

            param.Stream.Write(sendLength, 0, sendLength.Length)

            Dim Totalsent As Integer = 0
            Dim Currentsent As Integer = bufferSize

            While (Totalsent < param.Buffer.Length)

                If (param.Buffer.Length - Totalsent < bufferSize) Then
                    Currentsent = param.Buffer.Length - Totalsent
                End If

                param.Stream.Write(param.Buffer, Totalsent, Currentsent)
                Totalsent += Currentsent
            End While

            RaiseEvent DataSent(param.Stream, Totalsent, True)
        End Sub

        Private Function encrypt(ByVal input As Object) As Byte()

            Dim _MemoryStream As New MemoryStream()
            Dim _BinaryFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            _BinaryFormatter.Serialize(_MemoryStream, input)
            Dim output As Byte() = _MemoryStream.ToArray()

            Using encryptor As Aes = Aes.Create()
                Dim pdb As New Rfc2898DeriveBytes(CryptKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
                 &H65, &H64, &H76, &H65, &H64, &H65, _
                 &H76})
                encryptor.Key = pdb.GetBytes(32)
                encryptor.IV = pdb.GetBytes(16)
                Using ms As New MemoryStream()
                    Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                        cs.Write(output, 0, output.Length)
                        cs.Close()
                        output = ms.ToArray
                    End Using
                End Using
            End Using

            Return output

        End Function
        Private Function decrypt(ByVal input As Byte()) As Object

            Using encryptor As Aes = Aes.Create()
                Dim pdb As New Rfc2898DeriveBytes(CryptKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
                 &H65, &H64, &H76, &H65, &H64, &H65, _
                 &H76})
                encryptor.Key = pdb.GetBytes(32)
                encryptor.IV = pdb.GetBytes(16)
                Using ms As New MemoryStream()
                    Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                        cs.Write(input, 0, input.Length)
                        cs.Close()
                    End Using
                    input = ms.ToArray()
                End Using
            End Using

            Dim _MemoryStream As New MemoryStream()
            _MemoryStream.Write(input, 0, input.Length)
            Dim _BinaryFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            _MemoryStream.Seek(0, SeekOrigin.Begin)
            Dim output As Object = _BinaryFormatter.Deserialize(_MemoryStream)

            Return output
        End Function
    End Class 'base class for send/receive functions

End Module
