<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainPICCProg
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainPICCProg))
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbReader = New System.Windows.Forms.ComboBox
        Me.bInit = New System.Windows.Forms.Button
        Me.bConnect = New System.Windows.Forms.Button
        Me.gbGetData = New System.Windows.Forms.GroupBox
        Me.bGetData = New System.Windows.Forms.Button
        Me.cbIso14443A = New System.Windows.Forms.CheckBox
        Me.gbSendApdu = New System.Windows.Forms.GroupBox
        Me.tData = New System.Windows.Forms.TextBox
        Me.bSend = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.tINS = New System.Windows.Forms.TextBox
        Me.tP1 = New System.Windows.Forms.TextBox
        Me.tP2 = New System.Windows.Forms.TextBox
        Me.tLc = New System.Windows.Forms.TextBox
        Me.tLe = New System.Windows.Forms.TextBox
        Me.tCLA = New System.Windows.Forms.TextBox
        Me.mMsg = New System.Windows.Forms.ListBox
        Me.bClear = New System.Windows.Forms.Button
        Me.bReset = New System.Windows.Forms.Button
        Me.bQuit = New System.Windows.Forms.Button
        Me.gbGetData.SuspendLayout()
        Me.gbSendApdu.SuspendLayout()
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
        Me.cbReader.Size = New System.Drawing.Size(221, 21)
        Me.cbReader.TabIndex = 1
        '
        'bInit
        '
        Me.bInit.Location = New System.Drawing.Point(183, 45)
        Me.bInit.Name = "bInit"
        Me.bInit.Size = New System.Drawing.Size(131, 23)
        Me.bInit.TabIndex = 2
        Me.bInit.Text = "Initialize"
        Me.bInit.UseVisualStyleBackColor = True
        '
        'bConnect
        '
        Me.bConnect.Location = New System.Drawing.Point(183, 74)
        Me.bConnect.Name = "bConnect"
        Me.bConnect.Size = New System.Drawing.Size(131, 23)
        Me.bConnect.TabIndex = 3
        Me.bConnect.Text = "Connect"
        Me.bConnect.UseVisualStyleBackColor = True
        '
        'gbGetData
        '
        Me.gbGetData.Controls.Add(Me.bGetData)
        Me.gbGetData.Controls.Add(Me.cbIso14443A)
        Me.gbGetData.Location = New System.Drawing.Point(15, 103)
        Me.gbGetData.Name = "gbGetData"
        Me.gbGetData.Size = New System.Drawing.Size(299, 69)
        Me.gbGetData.TabIndex = 4
        Me.gbGetData.TabStop = False
        Me.gbGetData.Text = "Get Data Function"
        '
        'bGetData
        '
        Me.bGetData.Location = New System.Drawing.Point(152, 25)
        Me.bGetData.Name = "bGetData"
        Me.bGetData.Size = New System.Drawing.Size(125, 23)
        Me.bGetData.TabIndex = 4
        Me.bGetData.Text = "Get Data"
        Me.bGetData.UseVisualStyleBackColor = True
        '
        'cbIso14443A
        '
        Me.cbIso14443A.AutoSize = True
        Me.cbIso14443A.Location = New System.Drawing.Point(15, 29)
        Me.cbIso14443A.Name = "cbIso14443A"
        Me.cbIso14443A.Size = New System.Drawing.Size(105, 17)
        Me.cbIso14443A.TabIndex = 0
        Me.cbIso14443A.Text = "Iso 14443A Card"
        Me.cbIso14443A.UseVisualStyleBackColor = True
        '
        'gbSendApdu
        '
        Me.gbSendApdu.Controls.Add(Me.tData)
        Me.gbSendApdu.Controls.Add(Me.bSend)
        Me.gbSendApdu.Controls.Add(Me.Label8)
        Me.gbSendApdu.Controls.Add(Me.Label7)
        Me.gbSendApdu.Controls.Add(Me.Label6)
        Me.gbSendApdu.Controls.Add(Me.Label5)
        Me.gbSendApdu.Controls.Add(Me.Label4)
        Me.gbSendApdu.Controls.Add(Me.Label3)
        Me.gbSendApdu.Controls.Add(Me.Label2)
        Me.gbSendApdu.Controls.Add(Me.tINS)
        Me.gbSendApdu.Controls.Add(Me.tP1)
        Me.gbSendApdu.Controls.Add(Me.tP2)
        Me.gbSendApdu.Controls.Add(Me.tLc)
        Me.gbSendApdu.Controls.Add(Me.tLe)
        Me.gbSendApdu.Controls.Add(Me.tCLA)
        Me.gbSendApdu.Location = New System.Drawing.Point(15, 178)
        Me.gbSendApdu.Name = "gbSendApdu"
        Me.gbSendApdu.Size = New System.Drawing.Size(299, 238)
        Me.gbSendApdu.TabIndex = 5
        Me.gbSendApdu.TabStop = False
        Me.gbSendApdu.Text = "Send Card Command"
        '
        'tData
        '
        Me.tData.Location = New System.Drawing.Point(12, 101)
        Me.tData.Multiline = True
        Me.tData.Name = "tData"
        Me.tData.Size = New System.Drawing.Size(265, 94)
        Me.tData.TabIndex = 13
        '
        'bSend
        '
        Me.bSend.Location = New System.Drawing.Point(152, 202)
        Me.bSend.Name = "bSend"
        Me.bSend.Size = New System.Drawing.Size(125, 23)
        Me.bSend.TabIndex = 11
        Me.bSend.Text = "Send Card Command"
        Me.bSend.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 81)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 13)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "Data In"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(199, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(19, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Le"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(165, 25)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(19, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Lc"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(123, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(20, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "P2"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(88, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(20, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "P1"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(57, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "INS"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "CLA"
        '
        'tINS
        '
        Me.tINS.Location = New System.Drawing.Point(50, 43)
        Me.tINS.MaxLength = 2
        Me.tINS.Name = "tINS"
        Me.tINS.Size = New System.Drawing.Size(32, 20)
        Me.tINS.TabIndex = 6
        '
        'tP1
        '
        Me.tP1.Location = New System.Drawing.Point(88, 43)
        Me.tP1.MaxLength = 2
        Me.tP1.Name = "tP1"
        Me.tP1.Size = New System.Drawing.Size(32, 20)
        Me.tP1.TabIndex = 7
        '
        'tP2
        '
        Me.tP2.Location = New System.Drawing.Point(126, 43)
        Me.tP2.MaxLength = 2
        Me.tP2.Name = "tP2"
        Me.tP2.Size = New System.Drawing.Size(32, 20)
        Me.tP2.TabIndex = 8
        '
        'tLc
        '
        Me.tLc.Location = New System.Drawing.Point(164, 43)
        Me.tLc.MaxLength = 2
        Me.tLc.Name = "tLc"
        Me.tLc.Size = New System.Drawing.Size(32, 20)
        Me.tLc.TabIndex = 9
        '
        'tLe
        '
        Me.tLe.Location = New System.Drawing.Point(202, 43)
        Me.tLe.MaxLength = 2
        Me.tLe.Name = "tLe"
        Me.tLe.Size = New System.Drawing.Size(32, 20)
        Me.tLe.TabIndex = 10
        '
        'tCLA
        '
        Me.tCLA.Location = New System.Drawing.Point(12, 43)
        Me.tCLA.MaxLength = 2
        Me.tCLA.Name = "tCLA"
        Me.tCLA.Size = New System.Drawing.Size(32, 20)
        Me.tCLA.TabIndex = 5
        '
        'mMsg
        '
        Me.mMsg.FormattingEnabled = True
        Me.mMsg.Location = New System.Drawing.Point(320, 18)
        Me.mMsg.Name = "mMsg"
        Me.mMsg.Size = New System.Drawing.Size(306, 355)
        Me.mMsg.TabIndex = 6
        '
        'bClear
        '
        Me.bClear.Location = New System.Drawing.Point(320, 390)
        Me.bClear.Name = "bClear"
        Me.bClear.Size = New System.Drawing.Size(99, 23)
        Me.bClear.TabIndex = 7
        Me.bClear.Text = "Clear"
        Me.bClear.UseVisualStyleBackColor = True
        '
        'bReset
        '
        Me.bReset.Location = New System.Drawing.Point(425, 390)
        Me.bReset.Name = "bReset"
        Me.bReset.Size = New System.Drawing.Size(99, 23)
        Me.bReset.TabIndex = 8
        Me.bReset.Text = "Reset"
        Me.bReset.UseVisualStyleBackColor = True
        '
        'bQuit
        '
        Me.bQuit.Location = New System.Drawing.Point(527, 390)
        Me.bQuit.Name = "bQuit"
        Me.bQuit.Size = New System.Drawing.Size(99, 23)
        Me.bQuit.TabIndex = 9
        Me.bQuit.Text = "Quit"
        Me.bQuit.UseVisualStyleBackColor = True
        '
        'MainPICCProg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(638, 425)
        Me.Controls.Add(Me.bQuit)
        Me.Controls.Add(Me.bReset)
        Me.Controls.Add(Me.bClear)
        Me.Controls.Add(Me.mMsg)
        Me.Controls.Add(Me.gbSendApdu)
        Me.Controls.Add(Me.gbGetData)
        Me.Controls.Add(Me.bConnect)
        Me.Controls.Add(Me.bInit)
        Me.Controls.Add(Me.cbReader)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MainPICCProg"
        Me.Text = "Programming Other PICC Cards"
        Me.gbGetData.ResumeLayout(False)
        Me.gbGetData.PerformLayout()
        Me.gbSendApdu.ResumeLayout(False)
        Me.gbSendApdu.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbReader As System.Windows.Forms.ComboBox
    Friend WithEvents bInit As System.Windows.Forms.Button
    Friend WithEvents bConnect As System.Windows.Forms.Button
    Friend WithEvents gbGetData As System.Windows.Forms.GroupBox
    Friend WithEvents cbIso14443A As System.Windows.Forms.CheckBox
    Friend WithEvents bGetData As System.Windows.Forms.Button
    Friend WithEvents gbSendApdu As System.Windows.Forms.GroupBox
    Friend WithEvents tINS As System.Windows.Forms.TextBox
    Friend WithEvents tP1 As System.Windows.Forms.TextBox
    Friend WithEvents tP2 As System.Windows.Forms.TextBox
    Friend WithEvents tLc As System.Windows.Forms.TextBox
    Friend WithEvents tLe As System.Windows.Forms.TextBox
    Friend WithEvents tCLA As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents bSend As System.Windows.Forms.Button
    Friend WithEvents mMsg As System.Windows.Forms.ListBox
    Friend WithEvents bClear As System.Windows.Forms.Button
    Friend WithEvents bReset As System.Windows.Forms.Button
    Friend WithEvents bQuit As System.Windows.Forms.Button
    Friend WithEvents tData As System.Windows.Forms.TextBox

End Class
