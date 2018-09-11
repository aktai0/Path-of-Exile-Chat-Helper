﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
      Me.ChatLogListBox = New System.Windows.Forms.ListBox()
      Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
      Me.MessageRichTextBox = New System.Windows.Forms.RichTextBox()
      Me.NameRichTextBox = New System.Windows.Forms.RichTextBox()
      Me.GuildRichTextBox = New System.Windows.Forms.RichTextBox()
      Me.FiltersGroupBox = New System.Windows.Forms.GroupBox()
      Me.WhisperRadioButton = New System.Windows.Forms.RadioButton()
      Me.PartyRadioButton = New System.Windows.Forms.RadioButton()
      Me.GuildRadioButton = New System.Windows.Forms.RadioButton()
      Me.TradeRadioButton = New System.Windows.Forms.RadioButton()
      Me.GlobalRadioButton = New System.Windows.Forms.RadioButton()
      Me.AnyRadioButton = New System.Windows.Forms.RadioButton()
      Me.Label3 = New System.Windows.Forms.Label()
      Me.Label2 = New System.Windows.Forms.Label()
      Me.Label1 = New System.Windows.Forms.Label()
      Me.Panel1 = New System.Windows.Forms.Panel()
      Me.FiltersGroupBox.SuspendLayout()
      Me.Panel1.SuspendLayout()
      Me.SuspendLayout()
      '
      'ChatLogListBox
      '
      Me.ChatLogListBox.Dock = System.Windows.Forms.DockStyle.Fill
      Me.ChatLogListBox.FormattingEnabled = True
      Me.ChatLogListBox.ItemHeight = 16
      Me.ChatLogListBox.Location = New System.Drawing.Point(0, 0)
      Me.ChatLogListBox.Name = "ChatLogListBox"
      Me.ChatLogListBox.ScrollAlwaysVisible = True
      Me.ChatLogListBox.Size = New System.Drawing.Size(1095, 466)
      Me.ChatLogListBox.TabIndex = 3
      Me.ChatLogListBox.TabStop = False
      '
      'BackgroundWorker1
      '
      Me.BackgroundWorker1.WorkerReportsProgress = True
      '
      'MessageRichTextBox
      '
      Me.MessageRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
      Me.MessageRichTextBox.Location = New System.Drawing.Point(108, 21)
      Me.MessageRichTextBox.Multiline = False
      Me.MessageRichTextBox.Name = "MessageRichTextBox"
      Me.MessageRichTextBox.Size = New System.Drawing.Size(221, 22)
      Me.MessageRichTextBox.TabIndex = 0
      Me.MessageRichTextBox.Text = ""
      '
      'NameRichTextBox
      '
      Me.NameRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
      Me.NameRichTextBox.Location = New System.Drawing.Point(108, 49)
      Me.NameRichTextBox.Multiline = False
      Me.NameRichTextBox.Name = "NameRichTextBox"
      Me.NameRichTextBox.Size = New System.Drawing.Size(221, 22)
      Me.NameRichTextBox.TabIndex = 1
      Me.NameRichTextBox.Text = ""
      '
      'GuildRichTextBox
      '
      Me.GuildRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
      Me.GuildRichTextBox.Location = New System.Drawing.Point(108, 77)
      Me.GuildRichTextBox.Multiline = False
      Me.GuildRichTextBox.Name = "GuildRichTextBox"
      Me.GuildRichTextBox.Size = New System.Drawing.Size(221, 22)
      Me.GuildRichTextBox.TabIndex = 2
      Me.GuildRichTextBox.Text = ""
      '
      'FiltersGroupBox
      '
      Me.FiltersGroupBox.Controls.Add(Me.WhisperRadioButton)
      Me.FiltersGroupBox.Controls.Add(Me.PartyRadioButton)
      Me.FiltersGroupBox.Controls.Add(Me.GuildRadioButton)
      Me.FiltersGroupBox.Controls.Add(Me.TradeRadioButton)
      Me.FiltersGroupBox.Controls.Add(Me.GlobalRadioButton)
      Me.FiltersGroupBox.Controls.Add(Me.AnyRadioButton)
      Me.FiltersGroupBox.Controls.Add(Me.Label3)
      Me.FiltersGroupBox.Controls.Add(Me.Label2)
      Me.FiltersGroupBox.Controls.Add(Me.Label1)
      Me.FiltersGroupBox.Controls.Add(Me.MessageRichTextBox)
      Me.FiltersGroupBox.Controls.Add(Me.GuildRichTextBox)
      Me.FiltersGroupBox.Controls.Add(Me.NameRichTextBox)
      Me.FiltersGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom
      Me.FiltersGroupBox.Location = New System.Drawing.Point(0, 466)
      Me.FiltersGroupBox.Name = "FiltersGroupBox"
      Me.FiltersGroupBox.Size = New System.Drawing.Size(1095, 113)
      Me.FiltersGroupBox.TabIndex = 4
      Me.FiltersGroupBox.TabStop = False
      Me.FiltersGroupBox.Text = "Filters"
      '
      'WhisperRadioButton
      '
      Me.WhisperRadioButton.AutoSize = True
      Me.WhisperRadioButton.Location = New System.Drawing.Point(424, 75)
      Me.WhisperRadioButton.Name = "WhisperRadioButton"
      Me.WhisperRadioButton.Size = New System.Drawing.Size(81, 21)
      Me.WhisperRadioButton.TabIndex = 11
      Me.WhisperRadioButton.Text = "Whisper"
      '
      'PartyRadioButton
      '
      Me.PartyRadioButton.AutoSize = True
      Me.PartyRadioButton.Location = New System.Drawing.Point(424, 47)
      Me.PartyRadioButton.Name = "PartyRadioButton"
      Me.PartyRadioButton.Size = New System.Drawing.Size(62, 21)
      Me.PartyRadioButton.TabIndex = 10
      Me.PartyRadioButton.Text = "Party"
      '
      'GuildRadioButton
      '
      Me.GuildRadioButton.AutoSize = True
      Me.GuildRadioButton.Location = New System.Drawing.Point(424, 19)
      Me.GuildRadioButton.Name = "GuildRadioButton"
      Me.GuildRadioButton.Size = New System.Drawing.Size(62, 21)
      Me.GuildRadioButton.TabIndex = 9
      Me.GuildRadioButton.Text = "Guild"
      '
      'TradeRadioButton
      '
      Me.TradeRadioButton.AutoSize = True
      Me.TradeRadioButton.Location = New System.Drawing.Point(344, 75)
      Me.TradeRadioButton.Name = "TradeRadioButton"
      Me.TradeRadioButton.Size = New System.Drawing.Size(67, 21)
      Me.TradeRadioButton.TabIndex = 8
      Me.TradeRadioButton.Text = "Trade"
      '
      'GlobalRadioButton
      '
      Me.GlobalRadioButton.AutoSize = True
      Me.GlobalRadioButton.Location = New System.Drawing.Point(344, 47)
      Me.GlobalRadioButton.Name = "GlobalRadioButton"
      Me.GlobalRadioButton.Size = New System.Drawing.Size(70, 21)
      Me.GlobalRadioButton.TabIndex = 7
      Me.GlobalRadioButton.Text = "Global"
      '
      'AnyRadioButton
      '
      Me.AnyRadioButton.AutoSize = True
      Me.AnyRadioButton.Checked = True
      Me.AnyRadioButton.Location = New System.Drawing.Point(344, 19)
      Me.AnyRadioButton.Name = "AnyRadioButton"
      Me.AnyRadioButton.Size = New System.Drawing.Size(53, 21)
      Me.AnyRadioButton.TabIndex = 6
      Me.AnyRadioButton.TabStop = True
      Me.AnyRadioButton.Text = "Any"
      '
      'Label3
      '
      Me.Label3.AutoSize = True
      Me.Label3.Location = New System.Drawing.Point(13, 77)
      Me.Label3.Name = "Label3"
      Me.Label3.Size = New System.Drawing.Size(70, 17)
      Me.Label3.TabIndex = 5
      Me.Label3.Text = "Guild Tag"
      '
      'Label2
      '
      Me.Label2.AutoSize = True
      Me.Label2.Location = New System.Drawing.Point(13, 49)
      Me.Label2.Name = "Label2"
      Me.Label2.Size = New System.Drawing.Size(79, 17)
      Me.Label2.TabIndex = 4
      Me.Label2.Text = "Char Name"
      '
      'Label1
      '
      Me.Label1.AutoSize = True
      Me.Label1.Location = New System.Drawing.Point(13, 21)
      Me.Label1.Name = "Label1"
      Me.Label1.Size = New System.Drawing.Size(65, 17)
      Me.Label1.TabIndex = 3
      Me.Label1.Text = "Message"
      '
      'Panel1
      '
      Me.Panel1.AutoScroll = True
      Me.Panel1.Controls.Add(Me.ChatLogListBox)
      Me.Panel1.Controls.Add(Me.FiltersGroupBox)
      Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
      Me.Panel1.Location = New System.Drawing.Point(10, 10)
      Me.Panel1.Name = "Panel1"
      Me.Panel1.Size = New System.Drawing.Size(1095, 579)
      Me.Panel1.TabIndex = 6
      '
      'MainWindow
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(1115, 599)
      Me.Controls.Add(Me.Panel1)
      Me.Name = "MainWindow"
      Me.Padding = New System.Windows.Forms.Padding(10)
      Me.Text = "POE Chat Helper"
      Me.FiltersGroupBox.ResumeLayout(False)
      Me.FiltersGroupBox.PerformLayout()
      Me.Panel1.ResumeLayout(False)
      Me.ResumeLayout(False)

   End Sub

   Friend WithEvents ChatLogListBox As ListBox
   Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
   Friend WithEvents MessageRichTextBox As RichTextBox
   Friend WithEvents NameRichTextBox As RichTextBox
   Friend WithEvents GuildRichTextBox As RichTextBox
   Friend WithEvents FiltersGroupBox As GroupBox
   Friend WithEvents Label3 As Label
   Friend WithEvents Label2 As Label
   Friend WithEvents Label1 As Label
   Friend WithEvents WhisperRadioButton As RadioButton
   Friend WithEvents PartyRadioButton As RadioButton
   Friend WithEvents GuildRadioButton As RadioButton
   Friend WithEvents TradeRadioButton As RadioButton
   Friend WithEvents GlobalRadioButton As RadioButton
   Friend WithEvents AnyRadioButton As RadioButton
   Friend WithEvents Panel1 As Panel
End Class
