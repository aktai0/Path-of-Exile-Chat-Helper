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

   Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
      ListBox1.BeginUpdate()
      ListBox1.Items.Clear()
      Dim a = Log_Utils.LogScanner.Instance
      For Each ll In a.ChatLines
         If RichTextBox1.Text <> "" Then
            ' Filter
            If ll.Message.Contains(RichTextBox1.Text) Then
               ListBox1.Items.Add(ll.ToString())
            End If
         Else
            ' No filter
            ListBox1.Items.Add(ll.ToString())
         End If
      Next
      ListBox1.TopIndex = ListBox1.Items.Count - 1
      ListBox1.EndUpdate()
      FilterChanged = False
   End Sub

   Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged
      FilterChanged = True
   End Sub
End Class
