Public Class LogLine
   ReadOnly rawString As String
   Public Sub New(ByVal rawLine As String)
      rawString = rawLine
      ParseLine()
   End Sub

   ' Groups:
   '    1 - Date
   '    2 - Time
   '    3 - NotSure
   '    4 - NotSure2
   '    5 - PID
   '    6 - ChatType (Local None, Global #, Whisper @, Trade $, Party %, Guild &)
   '    7 - GuildTag
   '    8 - Character
   '    9 - Message
   Const CHAT_PATTERN As String = "^(?<Date>[0-9]{4}/[0-9]{2}/[0-9]{2}) (?<Time>[0-9]{2}:[0-9]{2}:[0-9]{2}) (?<NotSure>[0-9]+) (?<NotSure2>[a-f0-9]+) \[INFO Client (?<PID>[0-9]+)] (?<ChatType>[%#$@&]?)(?<GuildTag><.*?>)? ?(?<Character>.*?): (?<Message>.*)$"
   Private Shared CHAT_REGEX As New System.Text.RegularExpressions.Regex(CHAT_PATTERN, Text.RegularExpressions.RegexOptions.Compiled)

   Public Shared Function CanParseLine(ByVal input As String) As Boolean
      Return CHAT_REGEX.IsMatch(input)
   End Function

   Public Function ParseLine()
      resultMatch = CHAT_REGEX.Match(rawString)

      If resultMatch.Success Then
         Console.WriteLine(Me.ToString)
      Else
         Console.WriteLine("Not a match")
      End If

      'If myMatch.Index Then
      '   Dim b = groups.Item("asdf")
      'End If

      Return Nothing
   End Function

   Dim resultMatch As Text.RegularExpressions.Match
   Public Overrides Function ToString() As String
      Dim toRet As New System.Text.StringBuilder
      toRet.AppendLine("Raw: " & rawString)
      toRet.AppendLine("Date: " & resultMatch.Groups("Date").Value)

      toRet.AppendLine("Time: " & resultMatch.Groups("Time").Value)
      toRet.AppendLine("Not Sure: " & resultMatch.Groups("NotSure").Value)
      toRet.AppendLine("Not Sure 2: " & resultMatch.Groups("NotSure2").Value)
      toRet.AppendLine("PID: " & resultMatch.Groups("PID").Value)
      toRet.AppendLine("Chat Type: " & resultMatch.Groups("ChatType").Value)
      toRet.AppendLine("GuildTag: " & resultMatch.Groups("GuildTag").Value)
      toRet.AppendLine("Character: " & resultMatch.Groups("Character").Value)
      toRet.AppendLine("Message: " & resultMatch.Groups("Message").Value)

      Return toRet.ToString
   End Function

End Class
