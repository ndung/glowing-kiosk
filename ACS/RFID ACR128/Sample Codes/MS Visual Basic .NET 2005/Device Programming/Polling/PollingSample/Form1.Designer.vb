<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PollingSample
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PollingSample))
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbReader = New System.Windows.Forms.ComboBox
        Me.bInit = New System.Windows.Forms.Button
        Me.bConnect = New System.Windows.Forms.Button
        Me.gbExMode = New System.Windows.Forms.GroupBox
        Me.optEither = New System.Windows.Forms.RadioButton
        Me.optBoth = New System.Windows.Forms.RadioButton
        Me.gbCurrMode = New System.Windows.Forms.GroupBox
        Me.optExActive = New System.Windows.Forms.RadioButton
        Me.optExNotActive = New System.Windows.Forms.RadioButton
        Me.bGetExMode = New System.Windows.Forms.Button
        Me.bSetExMode = New System.Windows.Forms.Button
        Me.gbPollOpt = New System.Windows.Forms.GroupBox
        Me.cbPollOpt6 = New System.Windows.Forms.CheckBox
        Me.cbPollOpt5 = New System.Windows.Forms.CheckBox
        Me.cbPollOpt4 = New System.Windows.Forms.CheckBox
        Me.cbPollOpt3 = New System.Windows.Forms.CheckBox
        Me.cbPollOpt2 = New System.Windows.Forms.CheckBox
        Me.cbPollOpt1 = New System.Windows.Forms.CheckBox
        Me.gbPICCInt = New System.Windows.Forms.GroupBox
        Me.opt25 = New System.Windows.Forms.RadioButton
        Me.opt1 = New System.Windows.Forms.RadioButton
        Me.opt500 = New System.Windows.Forms.RadioButton
        Me.opt250 = New System.Windows.Forms.RadioButton
        Me.bReadPollOpt = New System.Windows.Forms.Button
        Me.bSetPollOpt = New System.Windows.Forms.Button
        Me.bManPoll = New System.Windows.Forms.Button
        Me.bAutoPoll = New System.Windows.Forms.Button
        Me.mMsg = New System.Windows.Forms.ListBox
        Me.bClear = New System.Windows.Forms.Button
        Me.bReset = New System.Windows.Forms.Button
        Me.bQuit = New System.Windows.Forms.Button
        Me.PollTimer = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.tsMsg1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.tsMsg2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.tsMsg3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.tsMsg4 = New System.Windows.Forms.ToolStripStatusLabel
        Me.gbExMode.SuspendLayout()
        Me.gbCurrMode.SuspendLayout()
        Me.gbPollOpt.SuspendLayout()
        Me.gbPICCInt.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Select Reader"
        '
        'cbReader
        '
        Me.cbReader.FormattingEnabled = True
        Me.cbReader.Location = New System.Drawing.Point(93, 17)
        Me.cbReader.Name = "cbReader"
        Me.cbReader.Size = New System.Drawing.Size(180, 21)
        Me.cbReader.TabIndex = 1
        '
        'bInit
        '
        Me.bInit.Location = New System.Drawing.Point(15, 44)
        Me.bInit.Name = "bInit"
        Me.bInit.Size = New System.Drawing.Size(123, 23)
        Me.bInit.TabIndex = 2
        Me.bInit.Text = "Initialize"
        Me.bInit.UseVisualStyleBackColor = True
        '
        'bConnect
        '
        Me.bConnect.Location = New System.Drawing.Point(144, 44)
        Me.bConnect.Name = "bConnect"
        Me.bConnect.Size = New System.Drawing.Size(129, 23)
        Me.bConnect.TabIndex = 3
        Me.bConnect.Text = "Connect"
        Me.bConnect.UseVisualStyleBackColor = True
        '
        'gbExMode
        '
        Me.gbExMode.Controls.Add(Me.optEither)
        Me.gbExMode.Controls.Add(Me.optBoth)
        Me.gbExMode.Location = New System.Drawing.Point(15, 73)
        Me.gbExMode.Name = "gbExMode"
        Me.gbExMode.Size = New System.Drawing.Size(258, 75)
        Me.gbExMode.TabIndex = 4
        Me.gbExMode.TabStop = False
        Me.gbExMode.Text = "Configuration Setting"
        '
        'optEither
        '
        Me.optEither.AutoSize = True
        Me.optEither.Location = New System.Drawing.Point(14, 45)
        Me.optEither.Name = "optEither"
        Me.optEither.Size = New System.Drawing.Size(194, 17)
        Me.optEither.TabIndex = 1
        Me.optEither.TabStop = True
        Me.optEither.Text = "Either ICC or PICC can be activated"
        Me.optEither.UseVisualStyleBackColor = True
        '
        'optBoth
        '
        Me.optBoth.AutoSize = True
        Me.optBoth.Location = New System.Drawing.Point(14, 22)
        Me.optBoth.Name = "optBoth"
        Me.optBoth.Size = New System.Drawing.Size(229, 17)
        Me.optBoth.TabIndex = 0
        Me.optBoth.TabStop = True
        Me.optBoth.Text = "Both ICC_PICC interfaces can be activated"
        Me.optBoth.UseVisualStyleBackColor = True
        '
        'gbCurrMode
        '
        Me.gbCurrMode.Controls.Add(Me.optExActive)
        Me.gbCurrMode.Controls.Add(Me.optExNotActive)
        Me.gbCurrMode.Location = New System.Drawing.Point(15, 163)
        Me.gbCurrMode.Name = "gbCurrMode"
        Me.gbCurrMode.Size = New System.Drawing.Size(258, 77)
        Me.gbCurrMode.TabIndex = 5
        Me.gbCurrMode.TabStop = False
        Me.gbCurrMode.Text = "Current Mode"
        '
        'optExActive
        '
        Me.optExActive.AutoSize = True
        Me.optExActive.Location = New System.Drawing.Point(13, 44)
        Me.optExActive.Name = "optExActive"
        Me.optExActive.Size = New System.Drawing.Size(142, 17)
        Me.optExActive.TabIndex = 1
        Me.optExActive.TabStop = True
        Me.optExActive.Text = "Exclusive Mode is active"
        Me.optExActive.UseVisualStyleBackColor = True
        '
        'optExNotActive
        '
        Me.optExNotActive.AutoSize = True
        Me.optExNotActive.Location = New System.Drawing.Point(13, 21)
        Me.optExNotActive.Name = "optExNotActive"
        Me.optExNotActive.Size = New System.Drawing.Size(160, 17)
        Me.optExNotActive.TabIndex = 0
        Me.optExNotActive.TabStop = True
        Me.optExNotActive.Text = "Exclusive Mode is not active"
        Me.optExNotActive.UseVisualStyleBackColor = True
        '
        'bGetExMode
        '
        Me.bGetExMode.Location = New System.Drawing.Point(15, 246)
        Me.bGetExMode.Name = "bGetExMode"
        Me.bGetExMode.Size = New System.Drawing.Size(123, 23)
        Me.bGetExMode.TabIndex = 6
        Me.bGetExMode.Text = "Read Current Mode"
        Me.bGetExMode.UseVisualStyleBackColor = True
        '
        'bSetExMode
        '
        Me.bSetExMode.Location = New System.Drawing.Point(144, 246)
        Me.bSetExMode.Name = "bSetExMode"
        Me.bSetExMode.Size = New System.Drawing.Size(129, 23)
        Me.bSetExMode.TabIndex = 7
        Me.bSetExMode.Text = "Set Mode Configuration"
        Me.bSetExMode.UseVisualStyleBackColor = True
        '
        'gbPollOpt
        '
        Me.gbPollOpt.Controls.Add(Me.cbPollOpt6)
        Me.gbPollOpt.Controls.Add(Me.cbPollOpt5)
        Me.gbPollOpt.Controls.Add(Me.cbPollOpt4)
        Me.gbPollOpt.Controls.Add(Me.cbPollOpt3)
        Me.gbPollOpt.Controls.Add(Me.cbPollOpt2)
        Me.gbPollOpt.Controls.Add(Me.cbPollOpt1)
        Me.gbPollOpt.Location = New System.Drawing.Point(15, 275)
        Me.gbPollOpt.Name = "gbPollOpt"
        Me.gbPollOpt.Size = New System.Drawing.Size(258, 163)
        Me.gbPollOpt.TabIndex = 8
        Me.gbPollOpt.TabStop = False
        Me.gbPollOpt.Text = "Contactless Polling Options"
        '
        'cbPollOpt6
        '
        Me.cbPollOpt6.AutoSize = True
        Me.cbPollOpt6.Location = New System.Drawing.Point(8, 133)
        Me.cbPollOpt6.Name = "cbPollOpt6"
        Me.cbPollOpt6.Size = New System.Drawing.Size(152, 17)
        Me.cbPollOpt6.TabIndex = 5
        Me.cbPollOpt6.Text = "Enforce ISO 14443A Part4"
        Me.cbPollOpt6.UseVisualStyleBackColor = True
        '
        'cbPollOpt5
        '
        Me.cbPollOpt5.AutoSize = True
        Me.cbPollOpt5.Location = New System.Drawing.Point(8, 111)
        Me.cbPollOpt5.Name = "cbPollOpt5"
        Me.cbPollOpt5.Size = New System.Drawing.Size(77, 17)
        Me.cbPollOpt5.TabIndex = 4
        Me.cbPollOpt5.Text = "Test Mode"
        Me.cbPollOpt5.UseVisualStyleBackColor = True
        '
        'cbPollOpt4
        '
        Me.cbPollOpt4.AutoSize = True
        Me.cbPollOpt4.Location = New System.Drawing.Point(9, 89)
        Me.cbPollOpt4.Name = "cbPollOpt4"
        Me.cbPollOpt4.Size = New System.Drawing.Size(184, 17)
        Me.cbPollOpt4.TabIndex = 3
        Me.cbPollOpt4.Text = "Activate the PICC when detected"
        Me.cbPollOpt4.UseVisualStyleBackColor = True
        '
        'cbPollOpt3
        '
        Me.cbPollOpt3.AutoSize = True
        Me.cbPollOpt3.Location = New System.Drawing.Point(9, 65)
        Me.cbPollOpt3.Name = "cbPollOpt3"
        Me.cbPollOpt3.Size = New System.Drawing.Size(212, 17)
        Me.cbPollOpt3.TabIndex = 2
        Me.cbPollOpt3.Text = "Turn off antenna field if PICC is inactive"
        Me.cbPollOpt3.UseVisualStyleBackColor = True
        '
        'cbPollOpt2
        '
        Me.cbPollOpt2.AutoSize = True
        Me.cbPollOpt2.Location = New System.Drawing.Point(9, 41)
        Me.cbPollOpt2.Name = "cbPollOpt2"
        Me.cbPollOpt2.Size = New System.Drawing.Size(237, 17)
        Me.cbPollOpt2.TabIndex = 1
        Me.cbPollOpt2.Text = "Turn off antenna field if no PICC within range"
        Me.cbPollOpt2.UseVisualStyleBackColor = True
        '
        'cbPollOpt1
        '
        Me.cbPollOpt1.AutoSize = True
        Me.cbPollOpt1.Location = New System.Drawing.Point(10, 19)
        Me.cbPollOpt1.Name = "cbPollOpt1"
        Me.cbPollOpt1.Size = New System.Drawing.Size(134, 17)
        Me.cbPollOpt1.TabIndex = 0
        Me.cbPollOpt1.Text = "Automatic PICC Polling" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.cbPollOpt1.UseVisualStyleBackColor = True
        '
        'gbPICCInt
        '
        Me.gbPICCInt.Controls.Add(Me.opt25)
        Me.gbPICCInt.Controls.Add(Me.opt1)
        Me.gbPICCInt.Controls.Add(Me.opt500)
        Me.gbPICCInt.Controls.Add(Me.opt250)
        Me.gbPICCInt.Location = New System.Drawing.Point(15, 444)
        Me.gbPICCInt.Name = "gbPICCInt"
        Me.gbPICCInt.Size = New System.Drawing.Size(113, 112)
        Me.gbPICCInt.TabIndex = 9
        Me.gbPICCInt.TabStop = False
        Me.gbPICCInt.Text = "Poll Interval"
        '
        'opt25
        '
        Me.opt25.AutoSize = True
        Me.opt25.Location = New System.Drawing.Point(10, 89)
        Me.opt25.Name = "opt25"
        Me.opt25.Size = New System.Drawing.Size(60, 17)
        Me.opt25.TabIndex = 3
        Me.opt25.TabStop = True
        Me.opt25.Text = "2.5 sec"
        Me.opt25.UseVisualStyleBackColor = True
        '
        'opt1
        '
        Me.opt1.AutoSize = True
        Me.opt1.Location = New System.Drawing.Point(10, 66)
        Me.opt1.Name = "opt1"
        Me.opt1.Size = New System.Drawing.Size(51, 17)
        Me.opt1.TabIndex = 2
        Me.opt1.TabStop = True
        Me.opt1.Text = "1 sec"
        Me.opt1.UseVisualStyleBackColor = True
        '
        'opt500
        '
        Me.opt500.AutoSize = True
        Me.opt500.Location = New System.Drawing.Point(9, 41)
        Me.opt500.Name = "opt500"
        Me.opt500.Size = New System.Drawing.Size(71, 17)
        Me.opt500.TabIndex = 1
        Me.opt500.TabStop = True
        Me.opt500.Text = "500 msec"
        Me.opt500.UseVisualStyleBackColor = True
        '
        'opt250
        '
        Me.opt250.AutoSize = True
        Me.opt250.Location = New System.Drawing.Point(9, 19)
        Me.opt250.Name = "opt250"
        Me.opt250.Size = New System.Drawing.Size(71, 17)
        Me.opt250.TabIndex = 0
        Me.opt250.TabStop = True
        Me.opt250.Text = "250 msec"
        Me.opt250.UseVisualStyleBackColor = True
        '
        'bReadPollOpt
        '
        Me.bReadPollOpt.Location = New System.Drawing.Point(157, 457)
        Me.bReadPollOpt.Name = "bReadPollOpt"
        Me.bReadPollOpt.Size = New System.Drawing.Size(116, 23)
        Me.bReadPollOpt.TabIndex = 10
        Me.bReadPollOpt.Text = "Read Polling Option"
        Me.bReadPollOpt.UseVisualStyleBackColor = True
        '
        'bSetPollOpt
        '
        Me.bSetPollOpt.Location = New System.Drawing.Point(157, 486)
        Me.bSetPollOpt.Name = "bSetPollOpt"
        Me.bSetPollOpt.Size = New System.Drawing.Size(116, 23)
        Me.bSetPollOpt.TabIndex = 11
        Me.bSetPollOpt.Text = "Set Polling Option"
        Me.bSetPollOpt.UseVisualStyleBackColor = True
        '
        'bManPoll
        '
        Me.bManPoll.Location = New System.Drawing.Point(15, 574)
        Me.bManPoll.Name = "bManPoll"
        Me.bManPoll.Size = New System.Drawing.Size(129, 23)
        Me.bManPoll.TabIndex = 12
        Me.bManPoll.Text = "Manual Card Detection"
        Me.bManPoll.UseVisualStyleBackColor = True
        '
        'bAutoPoll
        '
        Me.bAutoPoll.Location = New System.Drawing.Point(150, 574)
        Me.bAutoPoll.Name = "bAutoPoll"
        Me.bAutoPoll.Size = New System.Drawing.Size(123, 23)
        Me.bAutoPoll.TabIndex = 13
        Me.bAutoPoll.Text = "Start Auto Detection"
        Me.bAutoPoll.UseVisualStyleBackColor = True
        '
        'mMsg
        '
        Me.mMsg.ForeColor = System.Drawing.Color.Black
        Me.mMsg.FormattingEnabled = True
        Me.mMsg.Location = New System.Drawing.Point(295, 21)
        Me.mMsg.Name = "mMsg"
        Me.mMsg.Size = New System.Drawing.Size(330, 524)
        Me.mMsg.TabIndex = 14
        '
        'bClear
        '
        Me.bClear.Location = New System.Drawing.Point(295, 574)
        Me.bClear.Name = "bClear"
        Me.bClear.Size = New System.Drawing.Size(111, 23)
        Me.bClear.TabIndex = 15
        Me.bClear.Text = "Clear Screen"
        Me.bClear.UseVisualStyleBackColor = True
        '
        'bReset
        '
        Me.bReset.Location = New System.Drawing.Point(412, 574)
        Me.bReset.Name = "bReset"
        Me.bReset.Size = New System.Drawing.Size(102, 23)
        Me.bReset.TabIndex = 16
        Me.bReset.Text = "Reset"
        Me.bReset.UseVisualStyleBackColor = True
        '
        'bQuit
        '
        Me.bQuit.Location = New System.Drawing.Point(520, 574)
        Me.bQuit.Name = "bQuit"
        Me.bQuit.Size = New System.Drawing.Size(105, 23)
        Me.bQuit.TabIndex = 17
        Me.bQuit.Text = "Quit"
        Me.bQuit.UseVisualStyleBackColor = True
        '
        'PollTimer
        '
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsMsg1, Me.tsMsg2, Me.tsMsg3, Me.tsMsg4})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 618)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(649, 22)
        Me.StatusStrip1.TabIndex = 18
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'tsMsg1
        '
        Me.tsMsg1.AutoSize = False
        Me.tsMsg1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsMsg1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.tsMsg1.Name = "tsMsg1"
        Me.tsMsg1.Size = New System.Drawing.Size(150, 17)
        Me.tsMsg1.Text = "ICC Reader Status"
        '
        'tsMsg2
        '
        Me.tsMsg2.AutoSize = False
        Me.tsMsg2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsMsg2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.tsMsg2.Name = "tsMsg2"
        Me.tsMsg2.Size = New System.Drawing.Size(150, 17)
        '
        'tsMsg3
        '
        Me.tsMsg3.AutoSize = False
        Me.tsMsg3.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsMsg3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.tsMsg3.Name = "tsMsg3"
        Me.tsMsg3.Size = New System.Drawing.Size(150, 17)
        Me.tsMsg3.Text = "PICC Reader Status"
        '
        'tsMsg4
        '
        Me.tsMsg4.AutoSize = False
        Me.tsMsg4.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsMsg4.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.tsMsg4.Name = "tsMsg4"
        Me.tsMsg4.Size = New System.Drawing.Size(150, 17)
        Me.tsMsg4.Text = " "
        '
        'PollingSample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(649, 640)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.bQuit)
        Me.Controls.Add(Me.bReset)
        Me.Controls.Add(Me.bClear)
        Me.Controls.Add(Me.mMsg)
        Me.Controls.Add(Me.bAutoPoll)
        Me.Controls.Add(Me.bManPoll)
        Me.Controls.Add(Me.bSetPollOpt)
        Me.Controls.Add(Me.bReadPollOpt)
        Me.Controls.Add(Me.gbPICCInt)
        Me.Controls.Add(Me.gbPollOpt)
        Me.Controls.Add(Me.bSetExMode)
        Me.Controls.Add(Me.bGetExMode)
        Me.Controls.Add(Me.gbCurrMode)
        Me.Controls.Add(Me.gbExMode)
        Me.Controls.Add(Me.bConnect)
        Me.Controls.Add(Me.bInit)
        Me.Controls.Add(Me.cbReader)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PollingSample"
        Me.Text = "Polling Sample"
        Me.gbExMode.ResumeLayout(False)
        Me.gbExMode.PerformLayout()
        Me.gbCurrMode.ResumeLayout(False)
        Me.gbCurrMode.PerformLayout()
        Me.gbPollOpt.ResumeLayout(False)
        Me.gbPollOpt.PerformLayout()
        Me.gbPICCInt.ResumeLayout(False)
        Me.gbPICCInt.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbReader As System.Windows.Forms.ComboBox
    Friend WithEvents bInit As System.Windows.Forms.Button
    Friend WithEvents bConnect As System.Windows.Forms.Button
    Friend WithEvents gbExMode As System.Windows.Forms.GroupBox
    Friend WithEvents optEither As System.Windows.Forms.RadioButton
    Friend WithEvents optBoth As System.Windows.Forms.RadioButton
    Friend WithEvents gbCurrMode As System.Windows.Forms.GroupBox
    Friend WithEvents optExActive As System.Windows.Forms.RadioButton
    Friend WithEvents optExNotActive As System.Windows.Forms.RadioButton
    Friend WithEvents bGetExMode As System.Windows.Forms.Button
    Friend WithEvents bSetExMode As System.Windows.Forms.Button
    Friend WithEvents gbPollOpt As System.Windows.Forms.GroupBox
    Friend WithEvents cbPollOpt6 As System.Windows.Forms.CheckBox
    Friend WithEvents cbPollOpt5 As System.Windows.Forms.CheckBox
    Friend WithEvents cbPollOpt4 As System.Windows.Forms.CheckBox
    Friend WithEvents cbPollOpt3 As System.Windows.Forms.CheckBox
    Friend WithEvents cbPollOpt2 As System.Windows.Forms.CheckBox
    Friend WithEvents cbPollOpt1 As System.Windows.Forms.CheckBox
    Friend WithEvents gbPICCInt As System.Windows.Forms.GroupBox
    Friend WithEvents opt25 As System.Windows.Forms.RadioButton
    Friend WithEvents opt1 As System.Windows.Forms.RadioButton
    Friend WithEvents opt500 As System.Windows.Forms.RadioButton
    Friend WithEvents opt250 As System.Windows.Forms.RadioButton
    Friend WithEvents bReadPollOpt As System.Windows.Forms.Button
    Friend WithEvents bSetPollOpt As System.Windows.Forms.Button
    Friend WithEvents bManPoll As System.Windows.Forms.Button
    Friend WithEvents bAutoPoll As System.Windows.Forms.Button
    Friend WithEvents mMsg As System.Windows.Forms.ListBox
    Friend WithEvents bClear As System.Windows.Forms.Button
    Friend WithEvents bReset As System.Windows.Forms.Button
    Friend WithEvents bQuit As System.Windows.Forms.Button
    Friend WithEvents PollTimer As System.Windows.Forms.Timer
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents tsMsg1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsMsg2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsMsg3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsMsg4 As System.Windows.Forms.ToolStripStatusLabel

End Class
