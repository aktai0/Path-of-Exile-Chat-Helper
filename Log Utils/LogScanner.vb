Public NotInheritable Class LogScanner
   ' Singleton Stuff
   Private Shared ReadOnly _instance As New Lazy(Of LogScanner)(Function()
                                                                   Return New LogScanner()
                                                                End Function, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication)

   Private Sub New()
      Init()
   End Sub

   Public Shared ReadOnly Property Instance() As LogScanner
      Get
         Return _instance.Value
      End Get
   End Property
   ' End Singleton Stuff

   ' Number of lines to keep in memory
   Private _LogLengthInMem As Integer = 10000
   Public Property LogLength() As Integer
      Get
         Return _LogLengthInMem
      End Get
      Set(ByVal value As Integer)
         _LogLengthInMem = value
      End Set
   End Property

   Private _LogFileFullPath As String = "C:\Program Files (x86)\Grinding Gear Games\Path of Exile\logs\Client.txt"
   Public Property LogFileFullPath() As String
      Get
         Return _LogFileFullPath
      End Get
      Set(ByVal value As String)
         _LogFileFullPath = value
      End Set
   End Property

   Private Lines As New List(Of LogLine)(_LogLengthInMem)

   Private AVG_LINE_BYTES = 400
   Dim fileStream As IO.StreamReader
   Private Sub Init()
      fileStream = New IO.StreamReader(New IO.FileStream(LogFileFullPath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite))
      ' Start from the end of the log file, move back some number of bytes, then read
      fileStream.BaseStream.Position = fileStream.BaseStream.Length - AVG_LINE_BYTES * LogLength

      ' Discard this line because it's probably a line cut in half
      fileStream.ReadLine()

      ' TODO: Add code in case the client file is shorter than the given length

      'ReadRestOfLog()
   End Sub

   Public ReadOnly Property CanReadLine() As Boolean
      Get
         Return If(fileStream IsNot Nothing, Not fileStream.EndOfStream, False)
      End Get
   End Property

   Public Sub ReadRestOfLog()
      While Not fileStream.EndOfStream
         Dim curLine = fileStream.ReadLine

         If LogLine.CanParseLine(curLine) Then
            ChatLines.Enqueue(LogLine.ParseLine(curLine))
         End If
      End While
   End Sub

   Private _ChatLines As New FixedQueue(Of LogLine)(LogLength)
   Public ReadOnly Property ChatLines() As FixedQueue(Of LogLine)
      Get
         Return _ChatLines
      End Get
   End Property

End Class
