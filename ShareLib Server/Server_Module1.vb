Imports System.Net.Sockets
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Xml.Serialization
Imports System.Security.Cryptography
Module Server_Module1

    Public NewUserKey As String = "skjhfege"
    Public LoadedForm As Mainform

    Public Const Defaultport As Integer = 8000
    Public Const ServerKey As String = "dsljfne234kjn3fsdf4"
    Public Const StringSep As Char = ","

    Public Const UserMsgKeyCorrect As String = "Password Correct"
    Public Const UserMsgKeyIncorrect As String = "Password Incorrect"
    Public Const UserMsgNameInUse As String = "The name is already in use."

    Public Class Multiple_Users_Class

        <Serializable>
        Private Class User
            Private _name As String
            Private _key As String
            Private _IpAddress As String

            Public Sub New(ByVal Name As String, ByVal Key As String, ByVal IpAddress As String)
                _name = Name
                _key = Key
                _IpAddress = IpAddress
            End Sub
            Public Property Name As String
                Get
                    Return _name
                End Get
                Set(value As String)
                    _name = value
                End Set
            End Property
            Public Property Key As String
                Get
                    Return _key
                End Get
                Set(value As String)
                    _key = value
                End Set
            End Property
            Public Property IpAddress As String
                Get
                    Return _IpAddress
                End Get
                Set(value As String)
                    _IpAddress = value
                End Set
            End Property


        End Class
        Private _users As New Dictionary(Of String, User)

        Public Event User_Added(ByVal key As String)
        Public Event User_Deleted(ByVal key As String)

        Public Function Add(ByVal Name As String, ByVal IpAddress As String, Optional ByVal Key As String = Nothing) As String
            Try
                If Key = Nothing Then
                    Key = CreateNewID()
                End If
                Dim addUser As New User(Name, Key, IpAddress)
                _users.Add(Key, addUser)
                RaiseEvent User_Added(Key)
                Return Key
            Catch
                Return Nothing
            End Try
        End Function
        Public Sub Delete(ByVal key As String)
            _users.Remove(key)
            RaiseEvent User_Deleted(key)
        End Sub

        Public Function CreateNewID() As String
            Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
            Dim sb As New StringBuilder

            While True
                '------------------------ Build String ----------------------
                Dim r As New Random
                sb.Clear()
                For i As Integer = 1 To 15
                    Dim idx As Integer = r.Next(0, 35)
                    sb.Append(s.Substring(idx, 1))
                Next
                '-------------------- Compare to each person -------------------------

                If Allkeys.Contains(sb.ToString) Then
                    Continue While
                End If

                Exit While

            End While
            LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, "Generated user key: " & sb.ToString)
            LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, " ")
            Return sb.ToString
        End Function

        Public Property Ip(ByVal Key As String) As String
            Get
                Return _users(Key).IpAddress
            End Get
            Set(value As String)
                _users(Key).IpAddress = value
            End Set
        End Property
        Public Property Name(ByVal Key As String) As String
            Get
                Return _users(Key).Name
            End Get
            Set(value As String)
                _users(Key).Name = value
            End Set
        End Property
        Public ReadOnly Property SendableList(ByVal key As String) As String
            Get
                Dim output As String = ""
                If _users.Count > 1 Then
                    Dim stringarr(3 * (_users.Count - 1) - 1) As String '-1 for index, -1 for not self

                    If _users.Count = 2 Then
                        ReDim stringarr(2) '3 parameters for 1 user
                    End If

                    Dim i As Integer = 0

                    For Each tmp As KeyValuePair(Of String, User) In _users
                        If tmp.Key <> key Then
                            stringarr(i) = tmp.Value.Name
                            i += 1
                            stringarr(i) = tmp.Value.IpAddress
                            i += 1
                            stringarr(i) = tmp.Value.Key
                            i += 1
                        End If
                    Next
                    output = String.Join(StringSep, stringarr)
                End If

                Return output
            End Get
        End Property

        Public ReadOnly Property AllNames As String()
            Get
                Dim output As New List(Of String)
                For Each u As KeyValuePair(Of String, User) In _users
                    output.Add(u.Value.Name)
                Next
                Return output.ToArray
            End Get
        End Property
        Public ReadOnly Property AllIps As String()
            Get
                Dim output As New List(Of String)
                For Each u As KeyValuePair(Of String, User) In _users
                    output.Add(u.Value.IpAddress)
                Next
                Return output.ToArray
            End Get
        End Property
        Public ReadOnly Property Allkeys As String()
            Get
                Return _users.Keys.ToArray
            End Get
        End Property

        Public Sub Load_FromFile(ByVal path As String)
            Conversions.XMLToObject(path, _users)
        End Sub
        Public Sub Save_ToFile(ByVal path As String)
            Conversions.ObjectToXML(_users, path)
        End Sub
    End Class
    Public Sub UpdateUserList(ByVal key As String) Handles users2.User_Added, users2.User_Deleted
        LoadedForm.Populate_UserTable(LoadedForm.DataGridView_Userlist)
    End Sub

    Public WithEvents users2 As New Multiple_Users_Class
    Public Connections As New List(Of ServerConnection)
    Public Connected As Boolean = False

    Public Sub NewConnection()
        Dim con As New ServerConnection(Defaultport)
        Connections.Add(con)

        AddHandler con.Connected_Server, AddressOf OnConnect
        AddHandler con.Data_Received, AddressOf OnReceive
        AddHandler con.Disconnecting_Server, AddressOf ConnectionDeleted
    End Sub 'step 1 - listen
    Public Sub OnConnect(ByRef Connection As ServerConnection, ByVal Asynch As Boolean)
        If Connected = True Then 'add a new listener if still connected
            NewConnection()
        End If

        LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, "Client connected from: " & Connection.IP)
        LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, " ")
        LoadedForm.AppendRTB(LoadedForm.RichTextBox_ConnectLog, "Client connected from: " & Connection.IP)
        LoadedForm.AppendRTB(LoadedForm.RichTextBox_ConnectLog, " ")


        Connection.Receive_Bytes_Async()
    End Sub 'step 2 - on connect
    Public Sub OnReceive(ByRef Con As ServerConnection, ByVal ReceiveData As Byte(), ByVal Asynch As Boolean)
        Dim receiveString As String

        Try
            If Asynch Then
                receiveString = Conversions.BytesToObject(ReceiveData)
                LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, "Authenticating....")

                '--------------------------------------- Authenticate Client -------------------------------------

                If users2.Allkeys.Contains(receiveString) Then 'user key correct
                    LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, "Current User found, key: " & receiveString)
                    Con.Send_Bytes(Conversions.ObjectToBytes(UserMsgKeyCorrect))
                    Call HandleOldUser(Con, receiveString)

                ElseIf receiveString = NewUserKey Then 'new user
                    LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, "New User found, key: " & receiveString)
                    Con.Send_Bytes(Conversions.ObjectToBytes(UserMsgKeyCorrect))
                    Call HandleNewUser(Con)

                Else
                    Con.Send_Bytes(Conversions.ObjectToBytes(UserMsgKeyIncorrect))
                    Con.Disconnect()
                    LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, "Authentication Failed.")

                End If
            End If
        Catch ex As Exception
            Con.Disconnect()
            LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, "Authentication error: " & ex.ToString)
        End Try
    End Sub 'step 3 - data received
    Public Sub HandleNewUser(ByRef con As ServerConnection)
        Try
            '---------------------------------- Get Name ------------------------------
            Dim receiveBytes As Byte() = Nothing
            Dim receiveString As String = Nothing

            receiveBytes = con.Receive_Bytes()
            receiveString = Conversions.BytesToObject(receiveBytes)

            LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, "New User Name: " & receiveString)

            '------------------------ Check to See if In use -------------------------

            If Users2.AllNames.Contains(receiveString) Then
                con.Send_Bytes(Conversions.ObjectToBytes(UserMsgNameInUse))
                Throw New Exception("Name already in use")
            End If

            LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, " ")
            '------------------------------- Get ip ---------------------------

            Dim IP As String = con.IP

            '------------------------------- Create User ---------------------------

            Dim key As String = Users2.Add(receiveString, IP)

            '------------------------------- Send Back key and list ---------------------------

            If Users2.Name(key) <> Nothing And Users2.Ip(key) <> Nothing And key <> Nothing Then
                con.Send_Bytes(Conversions.ObjectToBytes(key))
                HandleOldUser(con, key)  'call standard user function
            Else
                Users2.Delete(key)
                Throw New Exception("Unable to add user to list.")
            End If

        Catch ex As Exception
            LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, "New user handling error: " & ex.ToString)
            LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, " ")
            con.Disconnect()
        End Try
    End Sub 'step 4
    Public Sub HandleOldUser(ByRef con As ServerConnection, ByVal key As String)
        Try
            '-------- update IP address -----------------------------

            users2.Ip(key) = con.IP

            LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, "Updated existing user IP.")
            LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, " ")

            UpdateUserList(Nothing)

            '-------- get user list --------------------------------

            Dim sendlist As String = Users2.SendableList(key)
            con.Send_Bytes(Conversions.ObjectToBytes(sendlist))

            LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, "Sent user list to: " & Users2.Name(key) & " , " & Users2.Ip(key))
            LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, " ")

        Catch ex As Exception
            LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, "Current user handling error: " & ex.ToString)
            LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, " ")
        Finally
            con.Disconnect() 'new disconnect location
        End Try
        'tcp2.Disconnect(connectionIndex) old setup 
    End Sub ' step 5
    Public Sub ConnectionDeleted(ByRef Con As ServerConnection)
        LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, "Client disconnected from: " & Con.IP)
        LoadedForm.AppendRTB(LoadedForm.RichTextBox_Datalog, " ")
        LoadedForm.AppendRTB(LoadedForm.RichTextBox_ConnectLog, "Client disconnected from: " & Con.IP)
        LoadedForm.AppendRTB(LoadedForm.RichTextBox_ConnectLog, " ")

        Connections.Remove(Con)

        RemoveHandler Con.Connected_Server, AddressOf OnConnect
        RemoveHandler Con.Data_Received, AddressOf OnReceive
        RemoveHandler Con.Disconnecting_Server, AddressOf ConnectionDeleted
    End Sub 'step 6 - clean up


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

    Public Class ServerConnection_old


        Public Event Connected_Server(ByRef Connection As ServerConnection, ByVal Asynch As Boolean)
        Public Event Disconnecting_Server(ByRef Connection As ServerConnection)

        Private _Listener As TcpListener = Nothing
        Private _Listenering As Boolean = False
        Private _client As New TcpClient

        Private _Port As Integer
        Public ReadOnly Property Port As Integer
            Get
                Return _Port
            End Get
        End Property

        Public Sub New(ByVal port As Integer)
            _Port = port
            _Listener = New TcpListener(IPAddress.Any, _Port)
            _Listener.Start(50)
            _Listenering = True
            _Listener.BeginAcceptTcpClient(New AsyncCallback(AddressOf OnAccept), _Listener)
        End Sub
        Private Sub OnAccept(ByVal ar As IAsyncResult)
            If _Listenering Then
                _client = _Listener.EndAcceptTcpClient(ar)
                _Listener.Stop()
                _Listenering = False

                _stream = _client.GetStream()
                _stream.ReadTimeout = receiveTimeout
                'RaiseEvent Connected_Server(Me, True)
            End If
        End Sub
        Public Sub Server_Disconnect()
            Try
                'RaiseEvent Disconnecting_Server(Me)

                If _stream IsNot Nothing Then
                    _stream.Close()
                    _client.Close()
                End If

                If _Listenering Then
                    _Listenering = False
                    _Listener.Stop()
                End If
            Catch ex As Exception
            End Try
        End Sub




        Const bufferSize As Integer = 1024

        Protected Const sendtimeout As Integer = 1000 * 5
        Protected Const receiveTimeout As Integer = 1000 * 5
        Protected Const connectTimeout As Integer = 1000 * 1
        Protected Const SendKey As String = "jhjkkjhewrt"
        Protected Const CryptKey As String = "45gwwjnr54"

        Public Event DataReceived(ByRef Connection As ServerConnection, ByVal ReceiveData As Byte(), ByVal Asynch As Boolean)
        Public Event DataSent(ByRef Connection As ServerConnection, ByVal BytesSent As Integer, ByVal Asynch As Boolean)

        Protected _stream As NetworkStream = Nothing

        Public ReadOnly Property Stream As NetworkStream
            Get
                Return _stream
            End Get
        End Property

        Public ReadOnly Property IPAdress As String
            Get
                Dim IPadd As String = Nothing
                Dim ipend As Net.IPEndPoint = _client.Client.RemoteEndPoint
                If Not ipend Is Nothing Then
                    IPadd = ipend.Address.ToString
                End If
                Return IPadd
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


            'RaiseEvent DataReceived(Me, receiveData, False)
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
            'RaiseEvent DataReceived(Me, param.Buffer, True)
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

            'RaiseEvent DataSent(Me, Totalsent, True)
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
