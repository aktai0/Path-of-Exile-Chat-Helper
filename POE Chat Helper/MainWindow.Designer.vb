<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainWindow
   Inherits System.Windows.Forms.Form

   'Form overrides dispose to clean up the component list.
   <System.Diagnostics.DebuggerNonUserCode()>
   Protected Overrides Sub Dispose(ByVal disposing As Boolean)
      Try
         If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
         End If
      Finally
         MyBase.Dispose(disposing)
      End Try
   End Sub

   'Required by the Windows Form Designer
   Private components As System.ComponentModel.IContainer

   'NOTE: The following procedure is required by the Windows Form Designer
   'It can be modified using the Windows Form Designer.  
   'Do not modify it using the code editor.
   <System.Diagnostics.DebuggerStepThrough()>
   Private Sub InitializeComponent()
      Me.ListBox1 = New System.Windows.Forms.ListBox()
      Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
      Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
      Me.Label1 = New System.Windows.Forms.Label()
      Me.SuspendLayout()
      '
      'ListBox1
      '
      Me.ListBox1.FormattingEnabled = True
      Me.ListBox1.ItemHeight = 16
      Me.ListBox1.Location = New System.Drawing.Point(12, 12)
      Me.ListBox1.Name = "ListBox1"
      Me.ListBox1.Size = New System.Drawing.Size(776, 452)
      Me.ListBox1.TabIndex = 0
      '
      'BackgroundWorker1
      '
      Me.BackgroundWorker1.WorkerReportsProgress = True
      '
      'RichTextBox1
      '
      Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
      Me.RichTextBox1.Location = New System.Drawing.Point(52, 470)
      Me.RichTextBox1.Multiline = False
      Me.RichTextBox1.Name = "RichTextBox1"
      Me.RichTextBox1.Size = New System.Drawing.Size(146, 22)
      Me.RichTextBox1.TabIndex = 1
      Me.RichTextBox1.Text = ""
      '
      'Label1
      '
      Me.Label1.AutoSize = True
      Me.Label1.Location = New System.Drawing.Point(12, 470)
      Me.Label1.Name = "Label1"
      Me.Label1.Size = New System.Drawing.Size(43, 17)
      Me.Label1.TabIndex = 2
      Me.Label1.Text = "Filter:"
      '
      'MainWindow
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(800, 573)
      Me.Controls.Add(Me.RichTextBox1)
      Me.Controls.Add(Me.Label1)
      Me.Controls.Add(Me.ListBox1)
      Me.Name = "MainWindow"
      Me.Text = "POE Chat Helper"
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub

   Friend WithEvents ListBox1 As ListBox
   Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
   Friend WithEvents RichTextBox1 As RichTextBox
   Friend WithEvents Label1 As Label
End Class
