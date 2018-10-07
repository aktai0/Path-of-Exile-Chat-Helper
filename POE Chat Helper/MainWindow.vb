Imports System.Text.RegularExpressions

Public Class MainWindow
   Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      Dim a = Log_Utils.LogScanner.Instance
      BackgroundWorker1.RunWorkerAsync()
      RTBsToClearLabels = New Dictionary(Of RichTextBox, Label)
      RTBsToClearLabels.Add(MessageRichTextBox, ClearMessageLabel)
      RTBsToClearLabels.Add(NameRichTextBox, ClearCharLabel)
      RTBsToClearLabels.Add(GuildRichTextBox, ClearGuildLabel)
      ClearLabelsToRTBs = New Dictionary(Of Label, RichTextBox)
      ClearLabelsToRTBs.Add(ClearMessageLabel, MessageRichTextBox)
      ClearLabelsToRTBs.Add(ClearCharLabel, NameRichTextBox)
      ClearLabelsToRTBs.Add(ClearGuildLabel, GuildRichTextBox)
      UpdateLogLength(0)
   End Sub

   Private Sub UpdateLogLength(Optional ByVal amount As Integer = 0)
      Dim a = Log_Utils.LogScanner.Instance

      Dim curLength = a.LogLength
      curLength += amount

      If amount <> 0 AndAlso curLength > 0 Then
         a.LogLength = curLength
      End If
      LogLengthRichTextBox.Text = a.LogLength
   End Sub

   Private FilterChanged As Boolean = False
   Private ChatLogChanged As Boolean = False
   Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
      Dim a = Log_Utils.LogScanner.Instance
      While True
         If a.CanReadLine Then
            BackgroundWorker1.ReportProgress(1)
            Dim curTime = DateTime.Now
            a.ReadRestOfLog()
            Dim elapsedTime = DateTime.Now - curTime
            Console.WriteLine("ReadRestOfLog Time: " & elapsedTime.TotalMilliseconds & " ms")
            ChatLogChanged = True
            BackgroundWorker1.ReportProgress(0)
            BackgroundWorker1.ReportProgress(2)
         ElseIf FilterChanged Then
            BackgroundWorker1.ReportProgress(1)
            BackgroundWorker1.ReportProgress(0)
            BackgroundWorker1.ReportProgress(2)
         End If
         Threading.Thread.Sleep(1000)
      End While
   End Sub

   Private Sub UpdateListBox()
      Dim curTime = DateTime.Now

      Dim curSelection = ChatLogListBox.SelectedItem

      FilterChanged = False
      ChatLogChanged = False

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

      If UniqueCheckBox.Checked Then
         Dim curCount = newLines.Count
         Dim nameList As New SortedSet(Of String)

         For i = newLines.Count - 1 To 0 Step -1
            Dim curLine = newLines(i)

            If Not nameList.Contains(curLine.Character) Then
               nameList.Add(curLine.Character)
            Else
               newLines.RemoveAt(i)
            End If
         Next
         Console.WriteLine("Removed " & curCount - newLines.Count & " lines from non-unique chars")
      End If

      ' First load
      If ChatLogListBox.DataSource Is Nothing Then
         ChatLogListBox.DataSource = newLines
         newLines.RaiseListChangedEvents = True
         newLines.ResetBindings()
         ChatLogListBox.SelectedItem = Nothing
         ChatLogListBox.TopIndex = ChatLogListBox.Items.Count - 1
      Else
         newLines.RaiseListChangedEvents = True
         newLines.ResetBindings()
      End If

      'Threading.Thread.Sleep(3000)
      ' Keep selected item as same as before, if anything was selected.
      If curSelection IsNot Nothing AndAlso ChatLogListBox.Items.Contains(curSelection) Then
         ChatLogListBox.TopIndex = ChatLogListBox.Items.Count - 1
         ChatLogListBox.SelectedItem = curSelection
      Else
         ChatLogListBox.SelectedItem = Nothing
         ChatLogListBox.TopIndex = ChatLogListBox.Items.Count - 1
      End If

      Dim elapsedTime = DateTime.Now - curTime
      Console.WriteLine("UpdateListBox Time: " & elapsedTime.TotalMilliseconds & " ms")
   End Sub

   Dim newLines As New System.ComponentModel.BindingList(Of Log_Utils.LogLine)()
   Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
      Select Case e.ProgressPercentage
         Case 0
            UpdateListBox()
         Case 1
            IncreaseLogButton.Enabled = False
            DecreaseLogButton.Enabled = False
         Case 2
            IncreaseLogButton.Enabled = True
            DecreaseLogButton.Enabled = True
      End Select
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

   Private RTBsToClearLabels As Dictionary(Of RichTextBox, Label)
   Private Sub Filters_TextChanged(sender As Object, e As EventArgs) Handles MessageRichTextBox.TextChanged, NameRichTextBox.TextChanged, GuildRichTextBox.TextChanged
      FilterChanged = True

      Dim curRTB = CType(sender, RichTextBox)
      If curRTB.TextLength > 0 Then
         RTBsToClearLabels(curRTB).Visible = True
      Else
         RTBsToClearLabels(curRTB).Visible = False
      End If
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

   Private Sub ListBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ChatLogListBox.KeyDown
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
      Dim result = ParseURL(ChatLogListBox.SelectedItem.ToString)
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

   Private Sub MouseWheelToListBox(sender As Object, e As MouseEventArgs)
      'Console.WriteLine(CType(sender, Control).Name & ": " & e.Delta)
      If CType(sender, Control).Focused AndAlso Not sender.Equals(ChatLogListBox) Then
         If e.Delta > 0 Then
            ChatLogListBox.TopIndex -= 3
         ElseIf e.Delta < 0 Then
            ChatLogListBox.TopIndex += 3
         End If
      End If
   End Sub

   Private Sub SetAllMouseWheelEventsForFilterRichTextBoxes(ByVal curControl As Control)
      AddHandler curControl.MouseWheel, AddressOf MouseWheelToListBox

      Dim cc = curControl.Controls
      For i = 0 To cc.Count - 1
         Dim c = cc(i)
         SetAllMouseWheelEventsForFilterRichTextBoxes(c)
      Next
   End Sub

   Private Sub MainWindow_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
      SetAllMouseWheelEventsForFilterRichTextBoxes(Me)
   End Sub

   Private ClearLabelsToRTBs As Dictionary(Of Label, RichTextBox)
   Private Sub ClearTextButton_Click(sender As Object, e As EventArgs) Handles ClearMessageLabel.Click, ClearCharLabel.Click, ClearGuildLabel.Click
      FilterChanged = True

      Dim curRTB = ClearLabelsToRTBs(CType(sender, Label))
      curRTB.Clear()
      curRTB.Focus()
   End Sub

   Private Sub ScrollToBottomButton_Click(sender As Object, e As EventArgs) Handles ScrollToBottomButton.Click
      ChatLogListBox.TopIndex = ChatLogListBox.Items.Count - 1
      ChatLogListBox.SelectedItem = Nothing
   End Sub

   Private Sub IncreaseLogButton_Click(sender As Object, e As EventArgs) Handles IncreaseLogButton.Click
      UpdateLogLength(10000)
      FilterChanged = True
   End Sub

   Private Sub DecreaseLogButton_Click(sender As Object, e As EventArgs) Handles DecreaseLogButton.Click
      UpdateLogLength(-10000)
      FilterChanged = True
   End Sub

   Private Sub ResetButton_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
      If AnyRadioButton.Checked = False OrElse (NameRichTextBox.Text.Length + MessageRichTextBox.Text.Length + GuildRichTextBox.Text.Length) > 0 OrElse UniqueCheckBox.Checked Then
         FilterChanged = True
      End If
      AnyRadioButton.Checked = True
      NameRichTextBox.Clear()
      MessageRichTextBox.Clear()
      GuildRichTextBox.Clear()
      UniqueCheckBox.Checked = False
   End Sub

   Private Sub UniqueCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles UniqueCheckBox.CheckedChanged
      FilterChanged = True
   End Sub
End Class
