Imports System.Text.RegularExpressions

Public Class MainWindow
   Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      Dim a = Log_Utils.LogScanner.Instance
      BackgroundWorker1.RunWorkerAsync()
   End Sub

   Private FilterChanged As Boolean = False
   Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
      Dim a = Log_Utils.LogScanner.Instance
      While True
         If a.CanReadLine Then
            a.ReadRestOfLog()
            BackgroundWorker1.ReportProgress(0)
         ElseIf FilterChanged Then
            BackgroundWorker1.ReportProgress(0)
         End If
         Threading.Thread.Sleep(1000)
      End While
   End Sub


   Dim newLines As New System.ComponentModel.BindingList(Of Log_Utils.LogLine)()
   Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
      newLines.RaiseListChangedEvents = False
      newLines.Clear()
      For Each cl In Log_Utils.LogScanner.Instance.ChatLines
         newLines.Add(cl)
      Next

      If MessageRichTextBox.Text <> "" Then
         FilterBy(newLines, FilterType.Message)
      End If

      If NameRichTextBox.Text <> "" Then
         FilterBy(newLines, FilterType.Name)
      End If

      If GuildRichTextBox.Text <> "" Then
         FilterBy(newLines, FilterType.Guild)
      End If

      If Not AnyRadioButton.Checked Then
         FilterBy(newLines, FilterType.ChatType)
      End If

      If ChatLogListBox.DataSource Is Nothing Then
         ChatLogListBox.DataSource = newLines
      End If

      newLines.RaiseListChangedEvents = True
      newLines.ResetBindings()

      FilterChanged = False
   End Sub

   Private Enum FilterType
      Message
      Name
      Guild
      ChatType
      'Guild, etc
   End Enum

   Private Sub FilterBy(ByRef lineBindingList As System.ComponentModel.BindingList(Of Log_Utils.LogLine), ByVal filterType As FilterType)
      Dim copy As New List(Of Log_Utils.LogLine)(lineBindingList)
      lineBindingList.Clear()

      For Each chatLine In copy
         Select Case filterType
            Case FilterType.Message
               If chatLine.Message.ToLower.Contains(MessageRichTextBox.Text.ToLower) Then
                  lineBindingList.Add(chatLine)
               End If
            Case FilterType.Name
               If chatLine.Character.ToLower.Contains(NameRichTextBox.Text.ToLower) Then
                  lineBindingList.Add(chatLine)
               End If
            Case FilterType.Guild
               If chatLine.GuildTag.ToLower.Contains(GuildRichTextBox.Text.ToLower) Then
                  lineBindingList.Add(chatLine)
               End If
            Case FilterType.ChatType
               Dim chatType = GetChatTypeFilter()

               Select Case chatType
                  Case Log_Utils.LogLine.ChatEnum.Global_, Log_Utils.LogLine.ChatEnum.Guild, Log_Utils.LogLine.ChatEnum.Local, Log_Utils.LogLine.ChatEnum.Party, Log_Utils.LogLine.ChatEnum.Trade
                     If chatLine.ChatType = chatType Then
                        lineBindingList.Add(chatLine)
                     End If
                  Case Log_Utils.LogLine.ChatEnum.WhisperIn
                     If chatLine.ChatType = chatType OrElse chatLine.ChatType = Log_Utils.LogLine.ChatEnum.WhisperOut Then
                        lineBindingList.Add(chatLine)
                     End If
                  Case Else
                     lineBindingList.Add(chatLine)
               End Select
         End Select
      Next
   End Sub

   Private Sub Filters_TextChanged(sender As Object, e As EventArgs) Handles MessageRichTextBox.TextChanged, NameRichTextBox.TextChanged, GuildRichTextBox.TextChanged
      FilterChanged = True
   End Sub


   ' Focus stuff to select all text in a RTB on focus change, the mouse stuff is for focus change on click which is weird
   Dim FocusChanged = False
   Private Sub Filters_FocusChanged(sender As Object, e As EventArgs) Handles MessageRichTextBox.GotFocus, NameRichTextBox.GotFocus, GuildRichTextBox.GotFocus
      Dim myRTB = CType(sender, RichTextBox)
      myRTB.SelectAll()
      FocusChanged = True
      'Console.WriteLine("FocusChanged + " & myRTB.Text)
   End Sub

   Private Sub MessageRichTextBox_MouseClick(sender As Object, e As MouseEventArgs) Handles MessageRichTextBox.MouseClick, NameRichTextBox.MouseClick, GuildRichTextBox.MouseClick
      Dim myRTB = CType(sender, RichTextBox)
      'Console.WriteLine("MouseClick + " & myRTB.Text)
      If FocusChanged Then
         FocusChanged = Not FocusChanged
         myRTB.SelectAll()
      End If
   End Sub

   Private Sub ListBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles ChatLogListBox.KeyUp
      If e.Control And e.KeyCode = Keys.C Then
         If e.Alt Then
            My.Computer.Clipboard.SetText(CType(ChatLogListBox.SelectedItem, Log_Utils.LogLine).Character)
         Else
            My.Computer.Clipboard.SetText(ChatLogListBox.SelectedItem.ToString)
         End If
      ElseIf e.Control And e.KeyCode = Keys.T Then
         Dim result = ParseURL(ChatLogListBox.SelectedItem.ToString)
         If result <> "" Then
            Process.Start(result)
         End If
      End If
   End Sub

   Private Function ParseURL(ByVal msg As String) As String
      Dim pattern = "^.*(https?://[^ ]+).*$"
      Dim regex = New Regex(pattern)
      Dim result = regex.Match(msg)
      Return result.Groups(1).Value
   End Function

   Private Sub ListBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ChatLogListBox.MouseDoubleClick
      Dim result = ParseURL(ChatLogListBox.SelectedItem)
      If result <> "" Then
         Process.Start(result)
      End If
   End Sub

   Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles AnyRadioButton.CheckedChanged, GlobalRadioButton.CheckedChanged, GuildRadioButton.CheckedChanged, PartyRadioButton.CheckedChanged, TradeRadioButton.CheckedChanged, WhisperRadioButton.CheckedChanged
      FilterChanged = True
   End Sub

   Private Function GetChatTypeFilter() As Log_Utils.LogLine.ChatEnum
      If AnyRadioButton.Checked Then
         Return Nothing
      ElseIf GlobalRadioButton.Checked Then
         Return Log_Utils.LogLine.ChatEnum.Global_
      ElseIf GuildRadioButton.Checked Then
         Return Log_Utils.LogLine.ChatEnum.Guild
      ElseIf PartyRadioButton.Checked Then
         Return Log_Utils.LogLine.ChatEnum.Party
      ElseIf TradeRadioButton.Checked Then
         Return Log_Utils.LogLine.ChatEnum.Trade
      ElseIf WhisperRadioButton.Checked Then
         Return Log_Utils.LogLine.ChatEnum.WhisperIn
      Else
         Return Nothing
      End If
   End Function

   Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
      ChatLogListBox.DataSource = Log_Utils.LogScanner.Instance.ChatLines.ToList
   End Sub
End Class
