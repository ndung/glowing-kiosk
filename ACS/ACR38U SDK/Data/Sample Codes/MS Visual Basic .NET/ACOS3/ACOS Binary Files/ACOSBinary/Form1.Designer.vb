<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainACOSBin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainACOSBin))
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbReader = New System.Windows.Forms.ComboBox
        Me.bInit = New System.Windows.Forms.Button
        Me.bConnect = New System.Windows.Forms.Button
        Me.gbFormat = New System.Windows.Forms.GroupBox
        Me.bFormat = New System.Windows.Forms.Button
        Me.tFileLen2 = New System.Windows.Forms.TextBox
        Me.tFileLen1 = New System.Windows.Forms.TextBox
        Me.tFileID2 = New System.Windows.Forms.TextBox
        Me.tFileID1 = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.gbReadWrite = New System.Windows.Forms.GroupBox
        Me.bBinWrite = New System.Windows.Forms.Button
        Me.bBinRead = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.tData = New System.Windows.Forms.TextBox
        Me.tLen = New System.Windows.Forms.TextBox
        Me.tOffset2 = New System.Windows.Forms.TextBox
        Me.tOffset1 = New System.Windows.Forms.TextBox
        Me.tFID2 = New System.Windows.Forms.TextBox
        Me.tFID1 = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.mMsg = New System.Windows.Forms.ListBox
        Me.bClear = New System.Windows.Forms.Button
        Me.bReset = New System.Windows.Forms.Button
        Me.bQuit = New System.Windows.Forms.Button
        Me.gbFormat.SuspendLayout()
        Me.gbReadWrite.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Select Reader"
        '
        'cbReader
        '
        Me.cbReader.FormattingEnabled = True
        Me.cbReader.Location = New System.Drawing.Point(93, 18)
        Me.cbReader.Name = "cbReader"
        Me.cbReader.Size = New System.Drawing.Size(188, 21)
        Me.cbReader.TabIndex = 1
        '
        'bInit
        '
        Me.bInit.Location = New System.Drawing.Point(161, 45)
        Me.bInit.Name = "bInit"
        Me.bInit.Size = New System.Drawing.Size(120, 23)
        Me.bInit.TabIndex = 2
        Me.bInit.Text = "Initialize"
        Me.bInit.UseVisualStyleBackColor = True
        '
        'bConnect
        '
        Me.bConnect.Location = New System.Drawing.Point(161, 74)
        Me.bConnect.Name = "bConnect"
        Me.bConnect.Size = New System.Drawing.Size(120, 23)
        Me.bConnect.TabIndex = 3
        Me.bConnect.Text = "Connect"
        Me.bConnect.UseVisualStyleBackColor = True
        '
        'gbFormat
        '
        Me.gbFormat.Controls.Add(Me.bFormat)
        Me.gbFormat.Controls.Add(Me.tFileLen2)
        Me.gbFormat.Controls.Add(Me.tFileLen1)
        Me.gbFormat.Controls.Add(Me.tFileID2)
        Me.gbFormat.Controls.Add(Me.tFileID1)
        Me.gbFormat.Controls.Add(Me.Label3)
        Me.gbFormat.Controls.Add(Me.Label2)
        Me.gbFormat.Location = New System.Drawing.Point(15, 103)
        Me.gbFormat.Name = "gbFormat"
        Me.gbFormat.Size = New System.Drawing.Size(266, 97)
        Me.gbFormat.TabIndex = 4
        Me.gbFormat.TabStop = False
        Me.gbFormat.Text = "Card Fornat Routine"
        '
        'bFormat
        '
        Me.bFormat.Location = New System.Drawing.Point(156, 62)
        Me.bFormat.Name = "bFormat"
        Me.bFormat.Size = New System.Drawing.Size(104, 23)
        Me.bFormat.TabIndex = 6
        Me.bFormat.Text = "Format Card"
        Me.bFormat.UseVisualStyleBackColor = True
        '
        'tFileLen2
        '
        Me.tFileLen2.Location = New System.Drawing.Point(118, 50)
        Me.tFileLen2.MaxLength = 2
        Me.tFileLen2.Name = "tFileLen2"
        Me.tFileLen2.Size = New System.Drawing.Size(33, 20)
        Me.tFileLen2.TabIndex = 5
        '
        'tFileLen1
        '
        Me.tFileLen1.Location = New System.Drawing.Point(78, 50)
        Me.tFileLen1.MaxLength = 2
        Me.tFileLen1.Name = "tFileLen1"
        Me.tFileLen1.Size = New System.Drawing.Size(33, 20)
        Me.tFileLen1.TabIndex = 4
        '
        'tFileID2
        '
        Me.tFileID2.Location = New System.Drawing.Point(118, 26)
        Me.tFileID2.MaxLength = 2
        Me.tFileID2.Name = "tFileID2"
        Me.tFileID2.Size = New System.Drawing.Size(33, 20)
        Me.tFileID2.TabIndex = 3
        '
        'tFileID1
        '
        Me.tFileID1.Location = New System.Drawing.Point(78, 26)
        Me.tFileID1.MaxLength = 2
        Me.tFileID1.Name = "tFileID1"
        Me.tFileID1.Size = New System.Drawing.Size(33, 20)
        Me.tFileID1.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Length"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "File ID"
        '
        'gbReadWrite
        '
        Me.gbReadWrite.Controls.Add(Me.bBinWrite)
        Me.gbReadWrite.Controls.Add(Me.bBinRead)
        Me.gbReadWrite.Controls.Add(Me.Label7)
        Me.gbReadWrite.Controls.Add(Me.tData)
        Me.gbReadWrite.Controls.Add(Me.tLen)
        Me.gbReadWrite.Controls.Add(Me.tOffset2)
        Me.gbReadWrite.Controls.Add(Me.tOffset1)
        Me.gbReadWrite.Controls.Add(Me.tFID2)
        Me.gbReadWrite.Controls.Add(Me.tFID1)
        Me.gbReadWrite.Controls.Add(Me.Label6)
        Me.gbReadWrite.Controls.Add(Me.Label5)
        Me.gbReadWrite.Controls.Add(Me.Label4)
        Me.gbReadWrite.Location = New System.Drawing.Point(15, 210)
        Me.gbReadWrite.Name = "gbReadWrite"
        Me.gbReadWrite.Size = New System.Drawing.Size(266, 267)
        Me.gbReadWrite.TabIndex = 5
        Me.gbReadWrite.TabStop = False
        Me.gbReadWrite.Text = "Read and Write to Binary File"
        '
        'bBinWrite
        '
        Me.bBinWrite.Location = New System.Drawing.Point(156, 235)
        Me.bBinWrite.Name = "bBinWrite"
        Me.bBinWrite.Size = New System.Drawing.Size(104, 23)
        Me.bBinWrite.TabIndex = 18
        Me.bBinWrite.Text = "Write Binary"
        Me.bBinWrite.UseVisualStyleBackColor = True
        '
        'bBinRead
        '
        Me.bBinRead.Location = New System.Drawing.Point(156, 206)
        Me.bBinRead.Name = "bBinRead"
        Me.bBinRead.Size = New System.Drawing.Size(104, 23)
        Me.bBinRead.TabIndex = 17
        Me.bBinRead.Text = "Read Binary"
        Me.bBinRead.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(18, 93)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(30, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Data"
        '
        'tData
        '
        Me.tData.Location = New System.Drawing.Point(21, 109)
        Me.tData.Multiline = True
        Me.tData.Name = "tData"
        Me.tData.Size = New System.Drawing.Size(233, 91)
        Me.tData.TabIndex = 15
        '
        'tLen
        '
        Me.tLen.Location = New System.Drawing.Point(227, 59)
        Me.tLen.MaxLength = 2
        Me.tLen.Name = "tLen"
        Me.tLen.Size = New System.Drawing.Size(33, 20)
        Me.tLen.TabIndex = 13
        '
        'tOffset2
        '
        Me.tOffset2.Location = New System.Drawing.Point(118, 59)
        Me.tOffset2.MaxLength = 2
        Me.tOffset2.Name = "tOffset2"
        Me.tOffset2.Size = New System.Drawing.Size(33, 20)
        Me.tOffset2.TabIndex = 12
        '
        'tOffset1
        '
        Me.tOffset1.Location = New System.Drawing.Point(78, 59)
        Me.tOffset1.MaxLength = 2
        Me.tOffset1.Name = "tOffset1"
        Me.tOffset1.Size = New System.Drawing.Size(33, 20)
        Me.tOffset1.TabIndex = 11
        '
        'tFID2
        '
        Me.tFID2.Location = New System.Drawing.Point(118, 33)
        Me.tFID2.MaxLength = 2
        Me.tFID2.Name = "tFID2"
        Me.tFID2.Size = New System.Drawing.Size(33, 20)
        Me.tFID2.TabIndex = 10
        '
        'tFID1
        '
        Me.tFID1.Location = New System.Drawing.Point(78, 33)
        Me.tFID1.MaxLength = 2
        Me.tFID1.Name = "tFID1"
        Me.tFID1.Size = New System.Drawing.Size(33, 20)
        Me.tFID1.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(18, 62)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "File Offset"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(181, 62)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Length"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "File ID"
        '
        'mMsg
        '
        Me.mMsg.FormattingEnabled = True
        Me.mMsg.Location = New System.Drawing.Point(296, 18)
        Me.mMsg.Name = "mMsg"
        Me.mMsg.Size = New System.Drawing.Size(325, 420)
        Me.mMsg.TabIndex = 6
        '
        'bClear
        '
        Me.bClear.Location = New System.Drawing.Point(296, 454)
        Me.bClear.Name = "bClear"
        Me.bClear.Size = New System.Drawing.Size(101, 23)
        Me.bClear.TabIndex = 7
        Me.bClear.Text = "Clear"
        Me.bClear.UseVisualStyleBackColor = True
        '
        'bReset
        '
        Me.bReset.Location = New System.Drawing.Point(403, 454)
        Me.bReset.Name = "bReset"
        Me.bReset.Size = New System.Drawing.Size(101, 23)
        Me.bReset.TabIndex = 8
        Me.bReset.Text = "Reset"
        Me.bReset.UseVisualStyleBackColor = True
        '
        'bQuit
        '
        Me.bQuit.Location = New System.Drawing.Point(510, 454)
        Me.bQuit.Name = "bQuit"
        Me.bQuit.Size = New System.Drawing.Size(111, 23)
        Me.bQuit.TabIndex = 9
        Me.bQuit.Text = "Quit"
        Me.bQuit.UseVisualStyleBackColor = True
        '
        'MainACOSBin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(635, 484)
        Me.Controls.Add(Me.bQuit)
        Me.Controls.Add(Me.bReset)
        Me.Controls.Add(Me.bClear)
        Me.Controls.Add(Me.mMsg)
        Me.Controls.Add(Me.gbReadWrite)
        Me.Controls.Add(Me.gbFormat)
        Me.Controls.Add(Me.bConnect)
        Me.Controls.Add(Me.bInit)
        Me.Controls.Add(Me.cbReader)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MainACOSBin"
        Me.Text = "Using Binary Files in ACOS3"
        Me.gbFormat.ResumeLayout(False)
        Me.gbFormat.PerformLayout()
        Me.gbReadWrite.ResumeLayout(False)
        Me.gbReadWrite.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbReader As System.Windows.Forms.ComboBox
    Friend WithEvents bInit As System.Windows.Forms.Button
    Friend WithEvents bConnect As System.Windows.Forms.Button
    Friend WithEvents gbFormat As System.Windows.Forms.GroupBox
    Friend WithEvents bFormat As System.Windows.Forms.Button
    Friend WithEvents tFileLen2 As System.Windows.Forms.TextBox
    Friend WithEvents tFileLen1 As System.Windows.Forms.TextBox
    Friend WithEvents tFileID2 As System.Windows.Forms.TextBox
    Friend WithEvents tFileID1 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents gbReadWrite As System.Windows.Forms.GroupBox
    Friend WithEvents tLen As System.Windows.Forms.TextBox
    Friend WithEvents tOffset2 As System.Windows.Forms.TextBox
    Friend WithEvents tOffset1 As System.Windows.Forms.TextBox
    Friend WithEvents tFID2 As System.Windows.Forms.TextBox
    Friend WithEvents tFID1 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents bBinWrite As System.Windows.Forms.Button
    Friend WithEvents bBinRead As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tData As System.Windows.Forms.TextBox
    Friend WithEvents mMsg As System.Windows.Forms.ListBox
    Friend WithEvents bClear As System.Windows.Forms.Button
    Friend WithEvents bReset As System.Windows.Forms.Button
    Friend WithEvents bQuit As System.Windows.Forms.Button

End Class
