<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DeviceProgramming
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
        Me.bStopBuzz = New System.Windows.Forms.Button
        Me.bGetBuzzState = New System.Windows.Forms.Button
        Me.cbBuzzLed7 = New System.Windows.Forms.CheckBox
        Me.bStartBuzz = New System.Windows.Forms.Button
        Me.bSetBuzzDur = New System.Windows.Forms.Button
        Me.tBuzzDur = New System.Windows.Forms.TextBox
        Me.cbBuzzLed8 = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbBuzzLed6 = New System.Windows.Forms.CheckBox
        Me.bClear = New System.Windows.Forms.Button
        Me.mMsg = New System.Windows.Forms.ListBox
        Me.cbBuzzLed5 = New System.Windows.Forms.CheckBox
        Me.bReset = New System.Windows.Forms.Button
        Me.cbBuzzLed4 = New System.Windows.Forms.CheckBox
        Me.cbBuzzLed3 = New System.Windows.Forms.CheckBox
        Me.cbBuzzLed2 = New System.Windows.Forms.CheckBox
        Me.cbBuzzLed1 = New System.Windows.Forms.CheckBox
        Me.bSetBuzzState = New System.Windows.Forms.Button
        Me.gbBuzzState = New System.Windows.Forms.GroupBox
        Me.gbLED = New System.Windows.Forms.GroupBox
        Me.cbGreen = New System.Windows.Forms.CheckBox
        Me.cbRed = New System.Windows.Forms.CheckBox
        Me.bGetFW = New System.Windows.Forms.Button
        Me.bConnect = New System.Windows.Forms.Button
        Me.bInit = New System.Windows.Forms.Button
        Me.cbReader = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.bQuit = New System.Windows.Forms.Button
        Me.gbBuzz = New System.Windows.Forms.GroupBox
        Me.bSetLed = New System.Windows.Forms.Button
        Me.bGetLed = New System.Windows.Forms.Button
        Me.gbBuzzState.SuspendLayout()
        Me.gbLED.SuspendLayout()
        Me.gbBuzz.SuspendLayout()
        Me.SuspendLayout()
        '
        'bStopBuzz
        '
        Me.bStopBuzz.Location = New System.Drawing.Point(134, 29)
        Me.bStopBuzz.Name = "bStopBuzz"
        Me.bStopBuzz.Size = New System.Drawing.Size(111, 23)
        Me.bStopBuzz.TabIndex = 4
        Me.bStopBuzz.Text = "Stop Buzzer Tone"
        Me.bStopBuzz.UseVisualStyleBackColor = True
        '
        'bGetBuzzState
        '
        Me.bGetBuzzState.Location = New System.Drawing.Point(6, 226)
        Me.bGetBuzzState.Name = "bGetBuzzState"
        Me.bGetBuzzState.Size = New System.Drawing.Size(125, 23)
        Me.bGetBuzzState.TabIndex = 8
        Me.bGetBuzzState.Text = "Get Buzzer/LED State"
        Me.bGetBuzzState.UseVisualStyleBackColor = True
        '
        'cbBuzzLed7
        '
        Me.cbBuzzLed7.AutoSize = True
        Me.cbBuzzLed7.Location = New System.Drawing.Point(9, 171)
        Me.cbBuzzLed7.Name = "cbBuzzLed7"
        Me.cbBuzzLed7.Size = New System.Drawing.Size(205, 17)
        Me.cbBuzzLed7.TabIndex = 6
        Me.cbBuzzLed7.Text = "Enable Exclusive Mode Status Buzzer"
        Me.cbBuzzLed7.UseVisualStyleBackColor = True
        '
        'bStartBuzz
        '
        Me.bStartBuzz.Location = New System.Drawing.Point(18, 29)
        Me.bStartBuzz.Name = "bStartBuzz"
        Me.bStartBuzz.Size = New System.Drawing.Size(108, 23)
        Me.bStartBuzz.TabIndex = 3
        Me.bStartBuzz.Text = "Set Buzzer Tone"
        Me.bStartBuzz.UseVisualStyleBackColor = True
        '
        'bSetBuzzDur
        '
        Me.bSetBuzzDur.Location = New System.Drawing.Point(132, 66)
        Me.bSetBuzzDur.Name = "bSetBuzzDur"
        Me.bSetBuzzDur.Size = New System.Drawing.Size(110, 23)
        Me.bSetBuzzDur.TabIndex = 2
        Me.bSetBuzzDur.Text = "Set Buzzer Duration"
        Me.bSetBuzzDur.UseVisualStyleBackColor = True
        '
        'tBuzzDur
        '
        Me.tBuzzDur.Location = New System.Drawing.Point(61, 68)
        Me.tBuzzDur.Name = "tBuzzDur"
        Me.tBuzzDur.Size = New System.Drawing.Size(59, 20)
        Me.tBuzzDur.TabIndex = 1
        '
        'cbBuzzLed8
        '
        Me.cbBuzzLed8.AutoSize = True
        Me.cbBuzzLed8.Location = New System.Drawing.Point(9, 194)
        Me.cbBuzzLed8.Name = "cbBuzzLed8"
        Me.cbBuzzLed8.Size = New System.Drawing.Size(197, 17)
        Me.cbBuzzLed8.TabIndex = 7
        Me.cbBuzzLed8.Text = "Enable Card Operation Blinking LED"
        Me.cbBuzzLed8.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Select Reader"
        '
        'cbBuzzLed6
        '
        Me.cbBuzzLed6.AutoSize = True
        Me.cbBuzzLed6.Location = New System.Drawing.Point(9, 148)
        Me.cbBuzzLed6.Name = "cbBuzzLed6"
        Me.cbBuzzLed6.Size = New System.Drawing.Size(210, 17)
        Me.cbBuzzLed6.TabIndex = 5
        Me.cbBuzzLed6.Text = "Enable RC531 Reset Indication Buzzer"
        Me.cbBuzzLed6.UseVisualStyleBackColor = True
        '
        'bClear
        '
        Me.bClear.Location = New System.Drawing.Point(287, 562)
        Me.bClear.Name = "bClear"
        Me.bClear.Size = New System.Drawing.Size(114, 23)
        Me.bClear.TabIndex = 25
        Me.bClear.Text = "Clear Screen"
        Me.bClear.UseVisualStyleBackColor = True
        '
        'mMsg
        '
        Me.mMsg.FormattingEnabled = True
        Me.mMsg.Location = New System.Drawing.Point(287, 15)
        Me.mMsg.Name = "mMsg"
        Me.mMsg.Size = New System.Drawing.Size(321, 537)
        Me.mMsg.TabIndex = 24
        '
        'cbBuzzLed5
        '
        Me.cbBuzzLed5.AutoSize = True
        Me.cbBuzzLed5.Location = New System.Drawing.Point(9, 123)
        Me.cbBuzzLed5.Name = "cbBuzzLed5"
        Me.cbBuzzLed5.Size = New System.Drawing.Size(245, 17)
        Me.cbBuzzLed5.TabIndex = 4
        Me.cbBuzzLed5.Text = "Enable Card Insertion/Removal Events Buzzer"
        Me.cbBuzzLed5.UseVisualStyleBackColor = True
        '
        'bReset
        '
        Me.bReset.Location = New System.Drawing.Point(407, 562)
        Me.bReset.Name = "bReset"
        Me.bReset.Size = New System.Drawing.Size(102, 23)
        Me.bReset.TabIndex = 26
        Me.bReset.Text = "Reset"
        Me.bReset.UseVisualStyleBackColor = True
        '
        'cbBuzzLed4
        '
        Me.cbBuzzLed4.AutoSize = True
        Me.cbBuzzLed4.Location = New System.Drawing.Point(9, 100)
        Me.cbBuzzLed4.Name = "cbBuzzLed4"
        Me.cbBuzzLed4.Size = New System.Drawing.Size(178, 17)
        Me.cbBuzzLed4.TabIndex = 3
        Me.cbBuzzLed4.Text = "Enable PICC PPS Status Buzzer"
        Me.cbBuzzLed4.UseVisualStyleBackColor = True
        '
        'cbBuzzLed3
        '
        Me.cbBuzzLed3.AutoSize = True
        Me.cbBuzzLed3.Location = New System.Drawing.Point(10, 77)
        Me.cbBuzzLed3.Name = "cbBuzzLed3"
        Me.cbBuzzLed3.Size = New System.Drawing.Size(204, 17)
        Me.cbBuzzLed3.TabIndex = 2
        Me.cbBuzzLed3.Text = "Enable PICC Activation Status Buzzer"
        Me.cbBuzzLed3.UseVisualStyleBackColor = True
        '
        'cbBuzzLed2
        '
        Me.cbBuzzLed2.AutoSize = True
        Me.cbBuzzLed2.Location = New System.Drawing.Point(10, 54)
        Me.cbBuzzLed2.Name = "cbBuzzLed2"
        Me.cbBuzzLed2.Size = New System.Drawing.Size(177, 17)
        Me.cbBuzzLed2.TabIndex = 1
        Me.cbBuzzLed2.Text = "Enable PICC Polling Status LED"
        Me.cbBuzzLed2.UseVisualStyleBackColor = True
        '
        'cbBuzzLed1
        '
        Me.cbBuzzLed1.AutoSize = True
        Me.cbBuzzLed1.Location = New System.Drawing.Point(10, 31)
        Me.cbBuzzLed1.Name = "cbBuzzLed1"
        Me.cbBuzzLed1.Size = New System.Drawing.Size(186, 17)
        Me.cbBuzzLed1.TabIndex = 0
        Me.cbBuzzLed1.Text = "Enable ICC Activation Status LED"
        Me.cbBuzzLed1.UseVisualStyleBackColor = True
        '
        'bSetBuzzState
        '
        Me.bSetBuzzState.Location = New System.Drawing.Point(137, 226)
        Me.bSetBuzzState.Name = "bSetBuzzState"
        Me.bSetBuzzState.Size = New System.Drawing.Size(113, 23)
        Me.bSetBuzzState.TabIndex = 9
        Me.bSetBuzzState.Text = "Set Buzz/LED State"
        Me.bSetBuzzState.UseVisualStyleBackColor = True
        '
        'gbBuzzState
        '
        Me.gbBuzzState.Controls.Add(Me.bSetBuzzState)
        Me.gbBuzzState.Controls.Add(Me.bGetBuzzState)
        Me.gbBuzzState.Controls.Add(Me.cbBuzzLed8)
        Me.gbBuzzState.Controls.Add(Me.cbBuzzLed7)
        Me.gbBuzzState.Controls.Add(Me.cbBuzzLed6)
        Me.gbBuzzState.Controls.Add(Me.cbBuzzLed5)
        Me.gbBuzzState.Controls.Add(Me.cbBuzzLed4)
        Me.gbBuzzState.Controls.Add(Me.cbBuzzLed3)
        Me.gbBuzzState.Controls.Add(Me.cbBuzzLed2)
        Me.gbBuzzState.Controls.Add(Me.cbBuzzLed1)
        Me.gbBuzzState.Location = New System.Drawing.Point(15, 317)
        Me.gbBuzzState.Name = "gbBuzzState"
        Me.gbBuzzState.Size = New System.Drawing.Size(256, 268)
        Me.gbBuzzState.TabIndex = 23
        Me.gbBuzzState.TabStop = False
        Me.gbBuzzState.Text = "LED and Buzzer Setting"
        '
        'gbLED
        '
        Me.gbLED.Controls.Add(Me.cbGreen)
        Me.gbLED.Controls.Add(Me.cbRed)
        Me.gbLED.Location = New System.Drawing.Point(12, 143)
        Me.gbLED.Name = "gbLED"
        Me.gbLED.Size = New System.Drawing.Size(120, 56)
        Me.gbLED.TabIndex = 19
        Me.gbLED.TabStop = False
        Me.gbLED.Text = "LED Settings"
        '
        'cbGreen
        '
        Me.cbGreen.AutoSize = True
        Me.cbGreen.Location = New System.Drawing.Point(58, 25)
        Me.cbGreen.Name = "cbGreen"
        Me.cbGreen.Size = New System.Drawing.Size(55, 17)
        Me.cbGreen.TabIndex = 1
        Me.cbGreen.Text = "Green"
        Me.cbGreen.UseVisualStyleBackColor = True
        '
        'cbRed
        '
        Me.cbRed.AutoSize = True
        Me.cbRed.Location = New System.Drawing.Point(12, 25)
        Me.cbRed.Name = "cbRed"
        Me.cbRed.Size = New System.Drawing.Size(46, 17)
        Me.cbRed.TabIndex = 0
        Me.cbRed.Text = "Red"
        Me.cbRed.UseVisualStyleBackColor = True
        '
        'bGetFW
        '
        Me.bGetFW.Location = New System.Drawing.Point(137, 109)
        Me.bGetFW.Name = "bGetFW"
        Me.bGetFW.Size = New System.Drawing.Size(129, 28)
        Me.bGetFW.TabIndex = 18
        Me.bGetFW.Text = "Get Firmware Version"
        Me.bGetFW.UseVisualStyleBackColor = True
        '
        'bConnect
        '
        Me.bConnect.Location = New System.Drawing.Point(137, 80)
        Me.bConnect.Name = "bConnect"
        Me.bConnect.Size = New System.Drawing.Size(129, 23)
        Me.bConnect.TabIndex = 17
        Me.bConnect.Text = "Connect"
        Me.bConnect.UseVisualStyleBackColor = True
        '
        'bInit
        '
        Me.bInit.Location = New System.Drawing.Point(137, 51)
        Me.bInit.Name = "bInit"
        Me.bInit.Size = New System.Drawing.Size(129, 23)
        Me.bInit.TabIndex = 16
        Me.bInit.Text = "Initialize"
        Me.bInit.UseVisualStyleBackColor = True
        '
        'cbReader
        '
        Me.cbReader.FormattingEnabled = True
        Me.cbReader.Location = New System.Drawing.Point(93, 15)
        Me.cbReader.Name = "cbReader"
        Me.cbReader.Size = New System.Drawing.Size(173, 21)
        Me.cbReader.TabIndex = 15
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Value"
        '
        'bQuit
        '
        Me.bQuit.Location = New System.Drawing.Point(515, 562)
        Me.bQuit.Name = "bQuit"
        Me.bQuit.Size = New System.Drawing.Size(93, 23)
        Me.bQuit.TabIndex = 27
        Me.bQuit.Text = "Quit"
        Me.bQuit.UseVisualStyleBackColor = True
        '
        'gbBuzz
        '
        Me.gbBuzz.Controls.Add(Me.bStopBuzz)
        Me.gbBuzz.Controls.Add(Me.bStartBuzz)
        Me.gbBuzz.Controls.Add(Me.bSetBuzzDur)
        Me.gbBuzz.Controls.Add(Me.tBuzzDur)
        Me.gbBuzz.Controls.Add(Me.Label2)
        Me.gbBuzz.Location = New System.Drawing.Point(15, 205)
        Me.gbBuzz.Name = "gbBuzz"
        Me.gbBuzz.Size = New System.Drawing.Size(253, 106)
        Me.gbBuzz.TabIndex = 22
        Me.gbBuzz.TabStop = False
        Me.gbBuzz.Text = "Set Buzzer Duration (x10 mSec)"
        '
        'bSetLed
        '
        Me.bSetLed.Location = New System.Drawing.Point(138, 174)
        Me.bSetLed.Name = "bSetLed"
        Me.bSetLed.Size = New System.Drawing.Size(128, 25)
        Me.bSetLed.TabIndex = 21
        Me.bSetLed.Text = "Set Led State"
        Me.bSetLed.UseVisualStyleBackColor = True
        '
        'bGetLed
        '
        Me.bGetLed.Location = New System.Drawing.Point(138, 143)
        Me.bGetLed.Name = "bGetLed"
        Me.bGetLed.Size = New System.Drawing.Size(130, 25)
        Me.bGetLed.TabIndex = 20
        Me.bGetLed.Text = "Get Led State"
        Me.bGetLed.UseVisualStyleBackColor = True
        '
        'DeviceProgramming
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(620, 600)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.bClear)
        Me.Controls.Add(Me.mMsg)
        Me.Controls.Add(Me.bReset)
        Me.Controls.Add(Me.gbBuzzState)
        Me.Controls.Add(Me.gbLED)
        Me.Controls.Add(Me.bGetFW)
        Me.Controls.Add(Me.bConnect)
        Me.Controls.Add(Me.bInit)
        Me.Controls.Add(Me.cbReader)
        Me.Controls.Add(Me.bQuit)
        Me.Controls.Add(Me.gbBuzz)
        Me.Controls.Add(Me.bSetLed)
        Me.Controls.Add(Me.bGetLed)
        Me.Name = "DeviceProgramming"
        Me.Text = "ACR 128 Device Programming"
        Me.gbBuzzState.ResumeLayout(False)
        Me.gbBuzzState.PerformLayout()
        Me.gbLED.ResumeLayout(False)
        Me.gbLED.PerformLayout()
        Me.gbBuzz.ResumeLayout(False)
        Me.gbBuzz.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents bStopBuzz As System.Windows.Forms.Button
    Friend WithEvents bGetBuzzState As System.Windows.Forms.Button
    Friend WithEvents cbBuzzLed7 As System.Windows.Forms.CheckBox
    Friend WithEvents bStartBuzz As System.Windows.Forms.Button
    Friend WithEvents bSetBuzzDur As System.Windows.Forms.Button
    Friend WithEvents tBuzzDur As System.Windows.Forms.TextBox
    Friend WithEvents cbBuzzLed8 As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbBuzzLed6 As System.Windows.Forms.CheckBox
    Friend WithEvents bClear As System.Windows.Forms.Button
    Friend WithEvents mMsg As System.Windows.Forms.ListBox
    Friend WithEvents cbBuzzLed5 As System.Windows.Forms.CheckBox
    Friend WithEvents bReset As System.Windows.Forms.Button
    Friend WithEvents cbBuzzLed4 As System.Windows.Forms.CheckBox
    Friend WithEvents cbBuzzLed3 As System.Windows.Forms.CheckBox
    Friend WithEvents cbBuzzLed2 As System.Windows.Forms.CheckBox
    Friend WithEvents cbBuzzLed1 As System.Windows.Forms.CheckBox
    Friend WithEvents bSetBuzzState As System.Windows.Forms.Button
    Friend WithEvents gbBuzzState As System.Windows.Forms.GroupBox
    Friend WithEvents gbLED As System.Windows.Forms.GroupBox
    Friend WithEvents cbGreen As System.Windows.Forms.CheckBox
    Friend WithEvents cbRed As System.Windows.Forms.CheckBox
    Friend WithEvents bGetFW As System.Windows.Forms.Button
    Friend WithEvents bConnect As System.Windows.Forms.Button
    Friend WithEvents bInit As System.Windows.Forms.Button
    Friend WithEvents cbReader As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents bQuit As System.Windows.Forms.Button
    Friend WithEvents gbBuzz As System.Windows.Forms.GroupBox
    Friend WithEvents bSetLed As System.Windows.Forms.Button
    Friend WithEvents bGetLed As System.Windows.Forms.Button

End Class
