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
   Private _LogLengthInMem As Integer = 1000
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

   Private Sub Init()
      InitReadLog()
   End Sub

   Private AVG_LINE_BYTES = 120
   Dim fileStream As IO.StreamReader
   Private Sub InitReadLog()
      fileStream = My.Computer.FileSystem.OpenTextFileReader(LogFileFullPath)
      ' Start from the end of the log file, move back some number of bytes, then read
      fileStream.BaseStream.Position = fileStream.BaseStream.Length - AVG_LINE_BYTES * LogLength

      ' Discard this line because it's probably a line cut in half
      fileStream.ReadLine()

      While Not fileStream.EndOfStream
         Dim curLine = fileStream.ReadLine

         If LogLine.CanParseLine(curLine) Then
            Dim b As New LogLine(curLine)
            Console.WriteLine(b.ToString)
         End If
      End While
   End Sub


End Class
