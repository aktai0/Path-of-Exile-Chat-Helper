Public Class LogLine
   Public ReadOnly Timestamp As DateTime
   Public ReadOnly PID As Integer
   Public ReadOnly ChatType As ChatEnum
   Public ReadOnly GuildTag As String
   Public ReadOnly Character As String
   Public ReadOnly Message As String

   Private Sub New(ByVal ts As DateTime, id As Integer, type As ChatEnum, tag As String, charn As String, msg As String)
      Timestamp = ts
      PID = id
      ChatType = type
      GuildTag = tag
      Character = charn
      Message = msg
   End Sub

   Public Enum ChatEnum
      Local
      Global_
      Trade
      Guild
      Party
      WhisperIn
      WhisperOut
      System
   End Enum

   Public Shared Function ChatEnumDisplay(ByVal chat As ChatEnum) As String
      Select Case chat
         Case ChatEnum.Local
            Return ""
         Case ChatEnum.Global_
            Return "#"
         Case ChatEnum.Trade
            Return "$"
         Case ChatEnum.Guild
            Return "&"
         Case ChatEnum.Party
            Return "%"
         Case ChatEnum.WhisperIn
            Return "@From "
         Case ChatEnum.WhisperOut
            Return "@To "
         Case ChatEnum.System
            Return "SYSTEM"
         Case Else
            Throw New Exception("Unrecognized ChatEnum: " & chat.ToString)
      End Select
   End Function


   ' Groups:
   '    1 - Date
   '    2 - Time
   '    3 - NotSure
   '    4 - NotSure2
   '    5 - PID
   '    6 - ChatType (Local None, Global #, Whisper @, Trade $, Party %, Guild &)
   '    6.5 - WhisperToFrom
   '    7 - GuildTag
   '    8 - Character
   '    9 - Message
   Const CHAT_PATTERN As String = "^(?<Date>[0-9]{4}/[0-9]{2}/[0-9]{2}) (?<Time>[0-9]{2}:[0-9]{2}:[0-9]{2}) (?<NotSure>[0-9]+) (?<NotSure2>[a-f0-9]+) \[INFO Client (?<PID>[0-9]+)] (?<ChatType>[%#$@&]?)(?:(?<WhisperToFrom>(?<=@)(?:To|From)) )?(?<GuildTag><.*?>)? ?(?<Character>[^ ]+?): (?<Message>.*)$"
   Private Shared CHAT_REGEX As New System.Text.RegularExpressions.Regex(CHAT_PATTERN, Text.RegularExpressions.RegexOptions.Compiled)

   Public Shared Function CanParseLine(ByVal input As String) As Boolean
      Return CHAT_REGEX.IsMatch(input)
   End Function

   Public Shared Function ParseLine(ByVal input As String) As LogLine
      Dim resultMatch As Text.RegularExpressions.Match
      resultMatch = CHAT_REGEX.Match(input)

      If resultMatch.Success Then
         Dim time = DateTime.Parse(resultMatch.Groups("Date").Value & " " & resultMatch.Groups("Time").Value)
         Dim id = Integer.Parse(resultMatch.Groups("PID").Value)

         Dim chatType As ChatEnum
         Select Case resultMatch.Groups("ChatType").Value
            Case ""
               If resultMatch.Groups("Character").Value.Contains(" "c) Then
                  ' System Message
                  chatType = ChatEnum.System
                  Throw New Exception("System messages not implemented yet!")
               End If
               chatType = ChatEnum.Local
            Case "#"
               chatType = ChatEnum.Global_
            Case "$"
               chatType = ChatEnum.Trade
            Case "%"
               chatType = ChatEnum.Party
            Case "&"
               chatType = ChatEnum.Guild
            Case "@"
               Select Case resultMatch.Groups("WhisperToFrom").Value
                  Case "To"
                     chatType = ChatEnum.WhisperOut
                  Case "From"
                     chatType = ChatEnum.WhisperIn
                  Case Else
                     Throw New Exception("Unhandled " & resultMatch.Groups("WhisperToFrom").Value)
               End Select
            Case Else
               Throw New Exception("Unhandled " & resultMatch.Groups("ChatType").Value)
         End Select

         Dim tag As String = ""
         If resultMatch.Groups("GuildTag").Value = "" Then

         Else
            tag = resultMatch.Groups("GuildTag").Value.Substring(1, resultMatch.Groups("GuildTag").Value.Length - 2) ' Guild tag without < > 
         End If

         Return New LogLine(time, id, chatType, tag, resultMatch.Groups("Character").Value, resultMatch.Groups("Message").Value)
      Else
         Return Nothing
      End If
   End Function

   Public Overrides Function ToString() As String
      Dim toRet As New System.Text.StringBuilder
      'toRet.AppendLine("Raw: " & rawString)
      'toRet.AppendLine("Date: " & resultMatch.Groups("Date").Value)

      'toRet.AppendLine("Time: " & resultMatch.Groups("Time").Value)
      'toRet.AppendLine("Not Sure: " & resultMatch.Groups("NotSure").Value)
      'toRet.AppendLine("Not Sure 2: " & resultMatch.Groups("NotSure2").Value)
      'toRet.AppendLine("PID: " & resultMatch.Groups("PID").Value)
      toRet.Append(Me.Timestamp.ToShortDateString & " " & Me.Timestamp.ToLongTimeString & " ")
      toRet.Append(LogLine.ChatEnumDisplay(Me.ChatType))
      If Me.GuildTag <> "" Then
         toRet.Append("<" & Me.GuildTag & "> ")
      End If
      toRet.Append(Me.Character & ": ")
      toRet.Append(Me.Message)

      Return toRet.ToString
   End Function

End Class
