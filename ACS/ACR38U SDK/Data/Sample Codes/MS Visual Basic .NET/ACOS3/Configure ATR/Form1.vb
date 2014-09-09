Public Class Form1
    Inherits System.Windows.Forms.Form


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnDisconnect As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents lstOutput As System.Windows.Forms.ListBox
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbReaderPort As System.Windows.Forms.ComboBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnATRValue As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbo_rate As System.Windows.Forms.ComboBox
    Friend WithEvents cbo_nobytes As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents t1 As System.Windows.Forms.TextBox
    Friend WithEvents t2 As System.Windows.Forms.TextBox
    Friend WithEvents t3 As System.Windows.Forms.TextBox
    Friend WithEvents t4 As System.Windows.Forms.TextBox
    Friend WithEvents t5 As System.Windows.Forms.TextBox
    Friend WithEvents t6 As System.Windows.Forms.TextBox
    Friend WithEvents t7 As System.Windows.Forms.TextBox
    Friend WithEvents t8 As System.Windows.Forms.TextBox
    Friend WithEvents t9 As System.Windows.Forms.TextBox
    Friend WithEvents t10 As System.Windows.Forms.TextBox
    Friend WithEvents t11 As System.Windows.Forms.TextBox
    Friend WithEvents t12 As System.Windows.Forms.TextBox
    Friend WithEvents t13 As System.Windows.Forms.TextBox
    Friend WithEvents t14 As System.Windows.Forms.TextBox
    Friend WithEvents t15 As System.Windows.Forms.TextBox
    Friend WithEvents cmdUpdateATR As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmdUpdateATR = New System.Windows.Forms.Button
        Me.t15 = New System.Windows.Forms.TextBox
        Me.t14 = New System.Windows.Forms.TextBox
        Me.t13 = New System.Windows.Forms.TextBox
        Me.t12 = New System.Windows.Forms.TextBox
        Me.t11 = New System.Windows.Forms.TextBox
        Me.t10 = New System.Windows.Forms.TextBox
        Me.t9 = New System.Windows.Forms.TextBox
        Me.t8 = New System.Windows.Forms.TextBox
        Me.t7 = New System.Windows.Forms.TextBox
        Me.t6 = New System.Windows.Forms.TextBox
        Me.t5 = New System.Windows.Forms.TextBox
        Me.t4 = New System.Windows.Forms.TextBox
        Me.t3 = New System.Windows.Forms.TextBox
        Me.t2 = New System.Windows.Forms.TextBox
        Me.t1 = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.cbo_nobytes = New System.Windows.Forms.ComboBox
        Me.cbo_rate = New System.Windows.Forms.ComboBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnDisconnect = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.lstOutput = New System.Windows.Forms.ListBox
        Me.btnATRValue = New System.Windows.Forms.Button
        Me.btnConnect = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.cmbReaderPort = New System.Windows.Forms.ComboBox
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdUpdateATR)
        Me.GroupBox2.Controls.Add(Me.t15)
        Me.GroupBox2.Controls.Add(Me.t14)
        Me.GroupBox2.Controls.Add(Me.t13)
        Me.GroupBox2.Controls.Add(Me.t12)
        Me.GroupBox2.Controls.Add(Me.t11)
        Me.GroupBox2.Controls.Add(Me.t10)
        Me.GroupBox2.Controls.Add(Me.t9)
        Me.GroupBox2.Controls.Add(Me.t8)
        Me.GroupBox2.Controls.Add(Me.t7)
        Me.GroupBox2.Controls.Add(Me.t6)
        Me.GroupBox2.Controls.Add(Me.t5)
        Me.GroupBox2.Controls.Add(Me.t4)
        Me.GroupBox2.Controls.Add(Me.t3)
        Me.GroupBox2.Controls.Add(Me.t2)
        Me.GroupBox2.Controls.Add(Me.t1)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.TextBox2)
        Me.GroupBox2.Controls.Add(Me.TextBox1)
        Me.GroupBox2.Controls.Add(Me.cbo_nobytes)
        Me.GroupBox2.Controls.Add(Me.cbo_rate)
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Controls.Add(Me.btnDisconnect)
        Me.GroupBox2.Controls.Add(Me.btnClear)
        Me.GroupBox2.Controls.Add(Me.lstOutput)
        Me.GroupBox2.Controls.Add(Me.btnATRValue)
        Me.GroupBox2.Controls.Add(Me.btnConnect)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(552, 448)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'cmdUpdateATR
        '
        Me.cmdUpdateATR.Enabled = False
        Me.cmdUpdateATR.Location = New System.Drawing.Point(48, 408)
        Me.cmdUpdateATR.Name = "cmdUpdateATR"
        Me.cmdUpdateATR.Size = New System.Drawing.Size(104, 23)
        Me.cmdUpdateATR.TabIndex = 31
        Me.cmdUpdateATR.Text = "Update ATR"
        '
        't15
        '
        Me.t15.BackColor = System.Drawing.SystemColors.Window
        Me.t15.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.t15.Location = New System.Drawing.Point(168, 368)
        Me.t15.MaxLength = 2
        Me.t15.Name = "t15"
        Me.t15.Size = New System.Drawing.Size(32, 20)
        Me.t15.TabIndex = 30
        Me.t15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        't14
        '
        Me.t14.BackColor = System.Drawing.SystemColors.Window
        Me.t14.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.t14.Location = New System.Drawing.Point(128, 368)
        Me.t14.MaxLength = 2
        Me.t14.Name = "t14"
        Me.t14.Size = New System.Drawing.Size(32, 20)
        Me.t14.TabIndex = 29
        Me.t14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        't13
        '
        Me.t13.BackColor = System.Drawing.SystemColors.Window
        Me.t13.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.t13.Location = New System.Drawing.Point(88, 368)
        Me.t13.MaxLength = 2
        Me.t13.Name = "t13"
        Me.t13.Size = New System.Drawing.Size(32, 20)
        Me.t13.TabIndex = 28
        Me.t13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        't12
        '
        Me.t12.BackColor = System.Drawing.SystemColors.Window
        Me.t12.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.t12.Location = New System.Drawing.Point(48, 368)
        Me.t12.MaxLength = 2
        Me.t12.Name = "t12"
        Me.t12.Size = New System.Drawing.Size(32, 20)
        Me.t12.TabIndex = 27
        Me.t12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        't11
        '
        Me.t11.BackColor = System.Drawing.SystemColors.Window
        Me.t11.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.t11.Location = New System.Drawing.Point(8, 368)
        Me.t11.MaxLength = 2
        Me.t11.Name = "t11"
        Me.t11.Size = New System.Drawing.Size(32, 20)
        Me.t11.TabIndex = 26
        Me.t11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        't10
        '
        Me.t10.BackColor = System.Drawing.SystemColors.Window
        Me.t10.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.t10.Location = New System.Drawing.Point(168, 336)
        Me.t10.MaxLength = 2
        Me.t10.Name = "t10"
        Me.t10.Size = New System.Drawing.Size(32, 20)
        Me.t10.TabIndex = 25
        Me.t10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        't9
        '
        Me.t9.BackColor = System.Drawing.SystemColors.Window
        Me.t9.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.t9.Location = New System.Drawing.Point(128, 336)
        Me.t9.MaxLength = 2
        Me.t9.Name = "t9"
        Me.t9.Size = New System.Drawing.Size(32, 20)
        Me.t9.TabIndex = 24
        Me.t9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        't8
        '
        Me.t8.BackColor = System.Drawing.SystemColors.Window
        Me.t8.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.t8.Location = New System.Drawing.Point(88, 336)
        Me.t8.MaxLength = 2
        Me.t8.Name = "t8"
        Me.t8.Size = New System.Drawing.Size(32, 20)
        Me.t8.TabIndex = 23
        Me.t8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        't7
        '
        Me.t7.BackColor = System.Drawing.SystemColors.Window
        Me.t7.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.t7.Location = New System.Drawing.Point(48, 336)
        Me.t7.MaxLength = 2
        Me.t7.Name = "t7"
        Me.t7.Size = New System.Drawing.Size(32, 20)
        Me.t7.TabIndex = 22
        Me.t7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        't6
        '
        Me.t6.BackColor = System.Drawing.SystemColors.Window
        Me.t6.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.t6.Location = New System.Drawing.Point(8, 336)
        Me.t6.MaxLength = 2
        Me.t6.Name = "t6"
        Me.t6.Size = New System.Drawing.Size(32, 20)
        Me.t6.TabIndex = 21
        Me.t6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        't5
        '
        Me.t5.BackColor = System.Drawing.SystemColors.Window
        Me.t5.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.t5.Location = New System.Drawing.Point(168, 304)
        Me.t5.MaxLength = 2
        Me.t5.Name = "t5"
        Me.t5.Size = New System.Drawing.Size(32, 20)
        Me.t5.TabIndex = 20
        Me.t5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        't4
        '
        Me.t4.BackColor = System.Drawing.SystemColors.Window
        Me.t4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.t4.Location = New System.Drawing.Point(128, 304)
        Me.t4.MaxLength = 2
        Me.t4.Name = "t4"
        Me.t4.Size = New System.Drawing.Size(32, 20)
        Me.t4.TabIndex = 19
        Me.t4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        't3
        '
        Me.t3.BackColor = System.Drawing.SystemColors.Window
        Me.t3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.t3.Location = New System.Drawing.Point(88, 304)
        Me.t3.MaxLength = 2
        Me.t3.Name = "t3"
        Me.t3.Size = New System.Drawing.Size(32, 20)
        Me.t3.TabIndex = 18
        Me.t3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        't2
        '
        Me.t2.BackColor = System.Drawing.SystemColors.Window
        Me.t2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.t2.Location = New System.Drawing.Point(48, 304)
        Me.t2.MaxLength = 2
        Me.t2.Name = "t2"
        Me.t2.Size = New System.Drawing.Size(32, 20)
        Me.t2.TabIndex = 17
        Me.t2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        't1
        '
        Me.t1.BackColor = System.Drawing.SystemColors.Window
        Me.t1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.t1.Location = New System.Drawing.Point(8, 304)
        Me.t1.MaxLength = 2
        Me.t1.Name = "t1"
        Me.t1.Size = New System.Drawing.Size(32, 20)
        Me.t1.TabIndex = 16
        Me.t1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(8, 280)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 16)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Historical Bytes"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(152, 224)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 16)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "TO"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 224)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(120, 16)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "No. of Historical Bytes"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(152, 168)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(24, 16)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "TA"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 168)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 16)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Card Baud Rate"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(144, 248)
        Me.TextBox2.MaxLength = 0
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(32, 20)
        Me.TextBox2.TabIndex = 10
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(145, 192)
        Me.TextBox1.MaxLength = 0
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(32, 20)
        Me.TextBox1.TabIndex = 9
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cbo_nobytes
        '
        Me.cbo_nobytes.Enabled = False
        Me.cbo_nobytes.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "0A", "0B", "0C", "0D", "0E", "0F", "FF"})
        Me.cbo_nobytes.Location = New System.Drawing.Point(8, 248)
        Me.cbo_nobytes.Name = "cbo_nobytes"
        Me.cbo_nobytes.Size = New System.Drawing.Size(121, 21)
        Me.cbo_nobytes.TabIndex = 8
        '
        'cbo_rate
        '
        Me.cbo_rate.Enabled = False
        Me.cbo_rate.Items.AddRange(New Object() {"9600", "14400", "28800", "57600", "115200"})
        Me.cbo_rate.Location = New System.Drawing.Point(8, 192)
        Me.cbo_rate.Name = "cbo_rate"
        Me.cbo_rate.Size = New System.Drawing.Size(121, 21)
        Me.cbo_rate.TabIndex = 7
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(432, 416)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(104, 23)
        Me.btnExit.TabIndex = 6
        Me.btnExit.Text = "Exit"
        '
        'btnDisconnect
        '
        Me.btnDisconnect.Location = New System.Drawing.Point(208, 416)
        Me.btnDisconnect.Name = "btnDisconnect"
        Me.btnDisconnect.Size = New System.Drawing.Size(104, 23)
        Me.btnDisconnect.TabIndex = 5
        Me.btnDisconnect.Text = "Disconnect"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(320, 416)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(104, 23)
        Me.btnClear.TabIndex = 4
        Me.btnClear.Text = "Clear Output"
        '
        'lstOutput
        '
        Me.lstOutput.HorizontalScrollbar = True
        Me.lstOutput.Location = New System.Drawing.Point(208, 24)
        Me.lstOutput.Name = "lstOutput"
        Me.lstOutput.Size = New System.Drawing.Size(336, 381)
        Me.lstOutput.TabIndex = 3
        '
        'btnATRValue
        '
        Me.btnATRValue.Enabled = False
        Me.btnATRValue.Location = New System.Drawing.Point(47, 120)
        Me.btnATRValue.Name = "btnATRValue"
        Me.btnATRValue.Size = New System.Drawing.Size(112, 23)
        Me.btnATRValue.TabIndex = 2
        Me.btnATRValue.Text = "Get ATR"
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(47, 88)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(112, 23)
        Me.btnConnect.TabIndex = 1
        Me.btnConnect.Text = "Connect"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmbReaderPort)
        Me.GroupBox3.Location = New System.Drawing.Point(21, 24)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(160, 48)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Reader Port"
        '
        'cmbReaderPort
        '
        Me.cmbReaderPort.Location = New System.Drawing.Point(8, 16)
        Me.cmbReaderPort.Name = "cmbReaderPort"
        Me.cmbReaderPort.Size = New System.Drawing.Size(144, 21)
        Me.cmbReaderPort.TabIndex = 1
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(566, 460)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configure ATR"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim retcode, Aprotocol As Integer
    Dim hContext, hCard As Integer
    Dim array(256) As Byte
    Dim apdu As APDURec
    Dim SendLen, RecvLen As Long
    Dim SendBuff(262) As Byte
    Dim RecvBuff(262) As Byte
    Dim ctrbyte(14) As String
    Dim i As Integer
    Const INVALID_SW1SW2 = -450

    

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click

        Dim Aprotocol As Integer

        'Connect to selected reader using hContext handle and obtain hCard handle
        retcode = ModWinscard.SCardConnect(hContext, cmbReaderPort.Text, 1, 0 Or 1, hCard, Aprotocol)

        If retcode <> ModWinscard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add("Connection Failed!")
            lstOutput.Items.Add(GetScardErrMsg(retcode))
            Exit Sub
        Else
            lstOutput.Items.Add("Connection Successful!")

            ' set button
            btnATRValue.Enabled = True
        End If

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        lstOutput.Items.Clear()
    End Sub

    Private Sub btnDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisconnect.Click

        ' Disconnect card
        retcode = ModWinscard.SCardDisconnect(hCard, ModWinscard.SCARD_UNPOWER_CARD)

        If retcode <> ModWinscard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add("Disconnection Failed!")
            Exit Sub
        Else
            lstOutput.Items.Add("Disconnection Successful..")
        End If

        retcode = ModWinscard.SCardDisconnect(hCard, 2)
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        ' terminate the application
        retcode = ModWinscard.SCardDisconnect(hCard, 2)
        retcode = ModWinscard.SCardReleaseContext(hContext)
        End
    End Sub

    Private Sub btnATRValue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnATRValue.Click

        Dim CardState, ATRLen As Integer
        Dim Protocol, ReaderLen, dwState As Integer
        Dim ATR(33) As Byte
        Dim index As Short, Temp As String
        Dim PRT As Integer
        Dim tmpReader = String.Empty
        ReaderLen = 100

        ATRLen = ModWinscard.SCARD_ATR_LENGTH
        ' perform the Card Status
        retcode = ModWinscard.SCardStatus(hCard, tmpReader, ReaderLen, dwState, Protocol, ATR(0), ATRLen)

        If retcode <> ModWinscard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add("SCardState Failed.")
            Exit Sub
        Else
            lstOutput.Items.Add("SCardStatus OK.")

            ' select for the protocol
            Select Case (Protocol)
                Case 0 : PRT = ModWinscard.SCARD_PROTOCOL_UNDEFINED
                    lstOutput.Items.Add("Active Protocol Undefined")
                Case 1 : PRT = ModWinscard.SCARD_PROTOCOL_T0
                    lstOutput.Items.Add("Active Protocol T0")
                Case 2 : PRT = ModWinscard.SCARD_PROTOCOL_T1
                    lstOutput.Items.Add("Active Protocol T1")
            End Select

            Temp = ""

            ' do loop for ATRlen
            For index = 0 To ATRLen - 1
                Temp = Temp + " " + String.Format("{0:X2}", ATR(index))
            Next index

            TextBox2.Text = " " + String.Format("{0:X2}", ATR(1))
            TextBox1.Text = " " + String.Format("{0:X2}", ATR(2))

            'Display ATR value
            lstOutput.Items.Add("ATR:" + Temp)

            cmdUpdateATR.Enabled = True
            cbo_rate.Enabled = True
            cbo_nobytes.Enabled = True




        End If

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim sReaderList As String
        Dim ctr As Integer
        Dim sReaderGroup As String
        Dim ReaderCount As Long

        For ctr = 0 To 255
            sReaderList = sReaderList + vbNullChar
        Next

        ReaderCount = 255
        'Establish context and obtain hContext handle
        retcode = ModWinscard.SCardEstablishContext(ModWinscard.SCARD_SCOPE_USER, 0, 0, hContext)

        If retcode <> ModWinscard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add("Established Reader Failed.")
        End If
        ' List PCSC card readers installed
        retcode = ModWinscard.SCardListReaders(hContext, sReaderGroup, sReaderList, ReaderCount)

        Call LoadListToControl(cmbReaderPort, sReaderList)

        cmbReaderPort.SelectedIndex = -1

        cbo_rate.SelectedIndex = 0
        cbo_nobytes.SelectedIndex = -1




    End Sub


    Public Sub LoadListToControl(ByVal Ctrl As ComboBox, ByVal ReaderList As String)

        Dim sTemp As String
        Dim index As Integer

        index = 1
        sTemp = ""
        Ctrl.Items.Clear()
        While (Mid(ReaderList, index, 1) <> vbNullChar)
            While (Mid(ReaderList, index, 1) <> vbNullChar)
                sTemp = sTemp + Mid(ReaderList, index, 1)
                index = index + 1
            End While
            index = index + 1
            Ctrl.Items.Add(sTemp)
            sTemp = ""
        End While

    End Sub

    ' Routines for Error Codes    
    Public Function GetScardErrMsg(ByVal ReturnCode As Integer) As String

        Select Case ReturnCode
            Case ModWinscard.SCARD_E_CANCELLED
                GetScardErrMsg = "The action was canceled by an SCardCancel request."
            Case ModWinscard.SCARD_E_CANT_DISPOSE
                GetScardErrMsg = "The system could not dispose of the media in the requested manner."
            Case ModWinscard.SCARD_E_CARD_UNSUPPORTED
                GetScardErrMsg = "The smart card does not meet minimal requirements for support."
            Case ModWinscard.SCARD_E_DUPLICATE_READER
                GetScardErrMsg = "The reader driver didn't produce a unique reader name."
            Case ModWinscard.SCARD_E_INSUFFICIENT_BUFFER
                GetScardErrMsg = "The data buffer for returned data is too small for the returned data."
            Case ModWinscard.SCARD_E_INVALID_ATR
                GetScardErrMsg = "An ATR string obtained from the registry is not a valid ATR string."
            Case ModWinscard.SCARD_E_INVALID_HANDLE
                GetScardErrMsg = "The supplied handle was invalid."
            Case ModWinscard.SCARD_E_INVALID_PARAMETER
                GetScardErrMsg = "One or more of the supplied parameters could not be properly interpreted."
            Case ModWinscard.SCARD_E_INVALID_TARGET
                GetScardErrMsg = "Registry startup information is missing or invalid."
            Case ModWinscard.SCARD_E_INVALID_VALUE
                GetScardErrMsg = "One or more of the supplied parameter values could not be properly interpreted."
            Case ModWinscard.SCARD_E_NOT_READY
                GetScardErrMsg = "The reader or card is not ready to accept commands."
            Case ModWinscard.SCARD_E_NOT_TRANSACTED
                GetScardErrMsg = "An attempt was made to end a non-existent transaction."
            Case ModWinscard.SCARD_E_NO_MEMORY
                GetScardErrMsg = "Not enough memory available to complete this command."
            Case ModWinscard.SCARD_E_NO_SERVICE
                GetScardErrMsg = "The smart card resource manager is not running."
            Case ModWinscard.SCARD_E_NO_SMARTCARD
                GetScardErrMsg = "The operation requires a smart card, but no smart card is currently in the device."
            Case ModWinscard.SCARD_E_PCI_TOO_SMALL
                GetScardErrMsg = "The PCI receive buffer was too small."
            Case ModWinscard.SCARD_E_PROTO_MISMATCH
                GetScardErrMsg = "The requested protocols are incompatible with the protocol currently in use with the card."
            Case ModWinscard.SCARD_E_READER_UNAVAILABLE
                GetScardErrMsg = "The specified reader is not currently available for use."
            Case ModWinscard.SCARD_E_READER_UNSUPPORTED
                GetScardErrMsg = "The reader driver does not meet minimal requirements for support."
            Case ModWinscard.SCARD_E_SERVICE_STOPPED
                GetScardErrMsg = "The smart card resource manager has shut down."
            Case ModWinscard.SCARD_E_SHARING_VIOLATION
                GetScardErrMsg = "The smart card cannot be accessed because of other outstanding connections."
            Case ModWinscard.SCARD_E_SYSTEM_CANCELLED
                GetScardErrMsg = "The action was canceled by the system, presumably to log off or shut down."
            Case ModWinscard.SCARD_E_TIMEOUT
                GetScardErrMsg = "The user-specified timeout value has expired."
            Case ModWinscard.SCARD_E_UNKNOWN_CARD
                GetScardErrMsg = "The specified smart card name is not recognized."
            Case ModWinscard.SCARD_E_UNKNOWN_READER
                GetScardErrMsg = "The specified reader name is not recognized."
            Case ModWinscard.SCARD_F_COMM_ERROR
                GetScardErrMsg = "An internal communications error has been detected."
            Case ModWinscard.SCARD_F_INTERNAL_ERROR
                GetScardErrMsg = "An internal consistency check failed."
            Case ModWinscard.SCARD_F_UNKNOWN_ERROR
                GetScardErrMsg = "An internal error has been detected, but the source is unknown."
            Case ModWinscard.SCARD_F_WAITED_TOO_Integer
                GetScardErrMsg = "An internal consistency timer has expired."
            Case ModWinscard.SCARD_S_SUCCESS
                GetScardErrMsg = "No error was encountered."
            Case ModWinscard.SCARD_W_REMOVED_CARD
                GetScardErrMsg = "The smart card has been removed, so that further communication is not possible."
            Case ModWinscard.SCARD_W_RESET_CARD
                GetScardErrMsg = "The smart card has been reset, so any shared state information is invalid."
            Case ModWinscard.SCARD_W_UNPOWERED_CARD
                GetScardErrMsg = "Power has been removed from the smart card, so that further communication is not possible."
            Case ModWinscard.SCARD_W_UNRESPONSIVE_CARD
                GetScardErrMsg = "The smart card is not responding to a reset."
            Case ModWinscard.SCARD_W_UNSUPPORTED_CARD
                GetScardErrMsg = "The reader cannot communicate with the card, due to ATR string configuration conflicts."
            Case Else
                GetScardErrMsg = "?"
        End Select
    End Function

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub

  

    Private Sub cbo_nobytes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo_nobytes.SelectedIndexChanged
        Select Case cbo_nobytes.SelectedIndex
            Case 0 : TextBox2.Text = "B0"
            Case 1 : TextBox2.Text = "B1"
            Case 2 : TextBox2.Text = "B2"
            Case 3 : TextBox2.Text = "B3"
            Case 4 : TextBox2.Text = "B4"
            Case 5 : TextBox2.Text = "B5"
            Case 6 : TextBox2.Text = "B6"
            Case 7 : TextBox2.Text = "B7"
            Case 8 : TextBox2.Text = "B8"
            Case 9 : TextBox2.Text = "B9"
            Case 10 : TextBox2.Text = "BA"
            Case 11 : TextBox2.Text = "BB"
            Case 12 : TextBox2.Text = "BC"
            Case 13 : TextBox2.Text = "BD"
            Case 14 : TextBox2.Text = "BE"
            Case 15 : TextBox2.Text = "BF"
            Case 16 : TextBox2.Text = "BE"
        End Select
    End Sub

    Private Sub cmdUpdateATR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdateATR.Click

        Dim tmpStr As String
        Dim indx As Integer
        Dim tmpAPDU(35) As Byte
        Dim num_historical_byte As Integer
        Dim ctr As Integer

        'Select FF07 file
        Call SelectFile(&HFF, &H7)

        If retcode <> ModWinscard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        tmpStr = ""
        For indx = 0 To 1
            tmpStr = tmpStr + " " + String.Format("{0:X2}", RecvBuff(indx))
        Next indx


        'Submit IC code
        Call SubmitIC()
        If retcode <> ModWinscard.SCARD_S_SUCCESS Then
            Exit Sub
        End If


        'Updating the ATR Value.
        num_historical_byte = cbo_nobytes.SelectedIndex '- 1

       
        tmpAPDU(0) = CInt("&H" & Mid("00" & TextBox1.Text, CInt(Len(Trim("00" & TextBox1.Text)) - 2) + 1, 2))



        If num_historical_byte = 16 Then
            tmpAPDU(1) = &HFF 'restoring to it original historical bytes
        Else
            tmpAPDU(1) = num_historical_byte
        End If

        ctr = 2

        If num_historical_byte < 16 Then

            For indx = 0 To num_historical_byte - 1

             
                If Trim(ctrbyte(indx)) = "" Then
                    ctrbyte(indx) = "00"
                End If

            Next indx

            For indx = 0 To (num_historical_byte - 1)

                tmpAPDU(ctr) = CInt("&H" + ctrbyte(indx))
                ctr = ctr + 1
            Next indx


        End If

        For indx = ctr To 35
            tmpAPDU(indx) = &H0
        Next indx

        Call writeRecord(0, 36, tmpAPDU)

    End Sub
    Public Function writeRecord(ByVal RecNo As Byte, ByVal DataLen As Byte, ByRef ApduIn() As Byte) As Long

        Dim indx As Integer
        Dim tmpStr As String

        ' Write data to card
        Call ClearBuffers()



        apdu.bCLA = &H80       ' CLA
        apdu.bINS = &HD2       ' INS
        apdu.bP1 = RecNo       ' Record No
        apdu.bP2 = &H0         ' P2
        apdu.bP3 = DataLen     ' Length of Data

        apdu.IsSend = True


        For indx = 0 To DataLen - 1
            apdu.Data(indx) = ApduIn(indx)
        Next

        tmpStr = ""
        For indx = 0 To SendLen - 1
            tmpStr = tmpStr + " " + String.Format("{0:X2}", SendBuff(indx))
        Next indx

        Call PerformTransmitAPDU(apdu)

        If retcode <> ModWinscard.SCARD_S_SUCCESS Then
            writeRecord = retcode
            Exit Function
        End If


        tmpStr = ""
        For indx = 0 To SendLen - 1
            tmpStr = tmpStr + " " + String.Format("{0:X2}", RecvBuff(indx))
        Next indx

        writeRecord = retcode

        lstOutput.SelectedIndex = lstOutput.Items.Count - 1

    End Function




    Private Function SelectFile(ByVal HiAddr As Byte, ByVal LoAddr As Byte) As Long

        Dim indx As Integer
        Dim tmpStr As String

        apdu.Data = array
        Call ClearBuffers()

        apdu.bCLA = &H80       ' CLA
        apdu.bINS = &HA4       ' INS
        apdu.bP1 = &H0         ' P1
        apdu.bP2 = &H0         ' P2
        apdu.bP3 = &H2         ' P3

        apdu.Data(0) = HiAddr     ' Value of High Byte
        apdu.Data(1) = LoAddr     ' Value of Low Byte

        apdu.IsSend = True

        Call PerformTransmitAPDU(apdu)


        If retcode <> ModWinscard.SCARD_S_SUCCESS Then
            SelectFile = retcode
            Exit Function
        End If

        SelectFile = retcode

    End Function

    Private Function SubmitIC() As Long

        Dim indx As Integer
        Dim tmpStr As String

        apdu.Data = array
        Call ClearBuffers()

        apdu.bCLA = &H80       ' CLA
        apdu.bINS = &H20       ' INS
        apdu.bP1 = &H7         ' P1
        apdu.bP2 = &H0         ' P2
        apdu.bP3 = &H8         ' P3

        apdu.Data(0) = &H41       ' A
        apdu.Data(1) = &H43       ' C
        apdu.Data(2) = &H4F       ' O
        apdu.Data(3) = &H53       ' S
        apdu.Data(4) = &H54       ' T
        apdu.Data(5) = &H45       ' E
        apdu.Data(6) = &H53       ' S
        apdu.Data(7) = &H54       ' T

        apdu.IsSend = True

        lstOutput.Items.Add("Submit IC")

        Call PerformTransmitAPDU(apdu)
    End Function

    Private Sub ClearBuffers()

        Dim indx As Long

        For indx = 0 To 262
            RecvBuff(indx) = &H0
            SendBuff(indx) = &H0
        Next indx

    End Sub

    Private Sub PerformTransmitAPDU(ByRef apdu As APDURec)

        Dim SendRequest As SCARD_IO_REQUEST
        Dim RecvRequest As SCARD_IO_REQUEST
        'Dim SendBuff(255 + 5) As Byte
        Dim SendBuffLen As Integer
        ' Dim RecvBuff(255 + 2) As Byte
        Dim RecvBuffLen As Integer
        Dim indx As Integer
        Dim sTemp As String
        Dim ctr As Integer

        SendBuff(0) = apdu.bCLA
        SendBuff(1) = apdu.bINS
        SendBuff(2) = apdu.bP1
        SendBuff(3) = apdu.bP2
        SendBuff(4) = apdu.bP3


        If apdu.IsSend Then

            For indx = 0 To apdu.bP3
                SendBuff(5 + indx) = apdu.Data(indx)
            Next indx

            SendBuffLen = 5 + apdu.bP3
            RecvBuffLen = 2

        Else
            SendBuffLen = 5
            RecvBuffLen = 2 + apdu.bP3
        End If

        SendRequest.dwProtocol = Aprotocol
        SendRequest.cbPciLength = Len(SendRequest)

        RecvRequest.dwProtocol = Aprotocol
        RecvRequest.cbPciLength = Len(RecvRequest)

        retcode = ModWinscard.SCardTransmit(hCard, SendRequest, SendBuff(0), SendBuffLen, SendRequest, RecvBuff(0), RecvBuffLen)

        If retcode <> ModWinscard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add("SCardTransmit Error!")
            lstOutput.Items.Add(GetScardErrMsg(retcode))
            Exit Sub
        Else
            lstOutput.Items.Add("SCardTransmit OK...")
        End If

        sTemp = ""
        ' do loop for sendbuffLen
        For indx = 0 To SendBuffLen - 1
            sTemp = sTemp + " " + String.Format("{0:X2}", SendBuff(indx))
        Next

        ' Display Send Buffer Value
        lstOutput.Items.Add("Send Buffer : " + sTemp)
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1

        sTemp = ""

        ' do loop for RecvbuffLen
        For indx = 0 To RecvBuffLen - 1
            sTemp = sTemp + " " + String.Format("{0:X2}", RecvBuff(indx))
        Next

        ' Display Receive Buffer Value
        lstOutput.Items.Add("Receive Buffer:" + sTemp)
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1

        If Not apdu.IsSend Then

            For indx = 0 To apdu.bP3 + 2
                apdu.Data(indx) = RecvBuff(indx)
            Next indx

        End If

        lstOutput.SelectedIndex = lstOutput.Items.Count - 1

    End Sub


    Private Sub t1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t1.TextChanged

        ctrbyte(0) = t1.Text

    End Sub

    Private Sub t2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t2.TextChanged

        ctrbyte(1) = t2.Text

    End Sub

    Private Sub t3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t3.TextChanged

        ctrbyte(2) = t3.Text
    End Sub

    Private Sub t4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t4.TextChanged
        ctrbyte(3) = t4.Text
    End Sub

    Private Sub t5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t5.TextChanged
        ctrbyte(4) = t5.Text
    End Sub

    Private Sub t6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t6.TextChanged
        ctrbyte(5) = t6.Text
    End Sub

    Private Sub t7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t7.TextChanged
        ctrbyte(6) = t7.Text
    End Sub

    Private Sub t8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t8.TextChanged
        ctrbyte(7) = t8.Text
    End Sub

    Private Sub t9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t9.TextChanged
        ctrbyte(8) = t9.Text
    End Sub

    Private Sub t10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t10.TextChanged
        ctrbyte(9) = t10.Text
    End Sub

    Private Sub t11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t11.TextChanged
        ctrbyte(10) = t11.Text
    End Sub

    Private Sub t12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t12.TextChanged
        ctrbyte(11) = t12.Text
    End Sub

    Private Sub t13_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t13.TextChanged
        ctrbyte(12) = t13.Text
    End Sub

    Private Sub t14_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t14.TextChanged
        ctrbyte(13) = t14.Text
    End Sub

    Private Sub t15_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t15.TextChanged
        ctrbyte(14) = t15.Text
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

        Select Case Trim(TextBox1.Text)
            Case "11" : cbo_rate.SelectedIndex = 0
            Case "92" : cbo_rate.SelectedIndex = 1
            Case "93" : cbo_rate.SelectedIndex = 2
            Case "94" : cbo_rate.SelectedIndex = 3
            Case "95" : cbo_rate.SelectedIndex = 4
        End Select





    End Sub

    Private Sub cbo_rate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbo_rate.Click

        Select Case cbo_rate.SelectedIndex
            Case 0 : TextBox1.Text = "11"
            Case 1 : TextBox1.Text = "92"
            Case 2 : TextBox1.Text = "93"
            Case 3 : TextBox1.Text = "94"
            Case 4 : TextBox1.Text = "95"
        End Select


    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

        Select Case Trim(TextBox2.Text)
            Case "B0" : cbo_nobytes.SelectedIndex = 0
            Case "B1" : cbo_nobytes.SelectedIndex = 1
            Case "B2" : cbo_nobytes.SelectedIndex = 2
            Case "B3" : cbo_nobytes.SelectedIndex = 3
            Case "B4" : cbo_nobytes.SelectedIndex = 4
            Case "B5" : cbo_nobytes.SelectedIndex = 5
            Case "B6" : cbo_nobytes.SelectedIndex = 6
            Case "B7" : cbo_nobytes.SelectedIndex = 7
            Case "B8" : cbo_nobytes.SelectedIndex = 8
            Case "B9" : cbo_nobytes.SelectedIndex = 9
            Case "BA" : cbo_nobytes.SelectedIndex = 10
            Case "BB" : cbo_nobytes.SelectedIndex = 11
            Case "BC" : cbo_nobytes.SelectedIndex = 12
            Case "BD" : cbo_nobytes.SelectedIndex = 13
            Case "BF" : cbo_nobytes.SelectedIndex = 15
            Case Else

                If cbo_nobytes.SelectedIndex = 16 Then
                    cbo_nobytes.SelectedIndex = 16
                Else
                    cbo_nobytes.SelectedIndex = 14
                End If

                t1.Text = ""
                t2.Text = ""
                t3.Text = ""
                t4.Text = ""
                t5.Text = ""
                t6.Text = ""
                t7.Text = ""
                t8.Text = ""
                t9.Text = ""
                t10.Text = ""
                t11.Text = ""
                t12.Text = ""
                t13.Text = ""
                t14.Text = ""
                t15.Text = ""
        End Select

        If cbo_nobytes.SelectedIndex = 0 Then
            t1.Enabled = False
            t2.Enabled = False
            t3.Enabled = False
            t4.Enabled = False
            t5.Enabled = False
            t6.Enabled = False
            t7.Enabled = False
            t8.Enabled = False
            t9.Enabled = False
            t10.Enabled = False
            t11.Enabled = False
            t12.Enabled = False
            t13.Enabled = False
            t14.Enabled = False
            t15.Enabled = False

        ElseIf cbo_nobytes.SelectedIndex = 1 Then

            t1.Enabled = True
            t2.Enabled = False
            t3.Enabled = False
            t4.Enabled = False
            t5.Enabled = False
            t6.Enabled = False
            t7.Enabled = False
            t8.Enabled = False
            t9.Enabled = False
            t10.Enabled = False
            t11.Enabled = False
            t12.Enabled = False
            t13.Enabled = False
            t14.Enabled = False
            t15.Enabled = False

        ElseIf cbo_nobytes.SelectedIndex = 2 Then

            t1.Enabled = True
            t2.Enabled = True
            t3.Enabled = False
            t4.Enabled = False
            t5.Enabled = False
            t6.Enabled = False
            t7.Enabled = False
            t8.Enabled = False
            t9.Enabled = False
            t10.Enabled = False
            t11.Enabled = False
            t12.Enabled = False
            t13.Enabled = False
            t14.Enabled = False
            t15.Enabled = False

        ElseIf cbo_nobytes.SelectedIndex = 3 Then

            t1.Enabled = True
            t2.Enabled = True
            t3.Enabled = True
            t4.Enabled = False
            t5.Enabled = False
            t6.Enabled = False
            t7.Enabled = False
            t8.Enabled = False
            t9.Enabled = False
            t10.Enabled = False
            t11.Enabled = False
            t12.Enabled = False
            t13.Enabled = False
            t14.Enabled = False
            t15.Enabled = False


        ElseIf cbo_nobytes.SelectedIndex = 4 Then

            t1.Enabled = True
            t2.Enabled = True
            t3.Enabled = True
            t4.Enabled = True
            t5.Enabled = False
            t6.Enabled = False
            t7.Enabled = False
            t8.Enabled = False
            t9.Enabled = False
            t10.Enabled = False
            t11.Enabled = False
            t12.Enabled = False
            t13.Enabled = False
            t14.Enabled = False
            t15.Enabled = False

        ElseIf cbo_nobytes.SelectedIndex = 5 Then

            t1.Enabled = True
            t2.Enabled = True
            t3.Enabled = True
            t4.Enabled = True
            t5.Enabled = True
            t6.Enabled = False
            t7.Enabled = False
            t8.Enabled = False
            t9.Enabled = False
            t10.Enabled = False
            t11.Enabled = False
            t12.Enabled = False
            t13.Enabled = False
            t14.Enabled = False
            t15.Enabled = False


        ElseIf cbo_nobytes.SelectedIndex = 6 Then

            t1.Enabled = True
            t2.Enabled = True
            t3.Enabled = True
            t4.Enabled = True
            t5.Enabled = True
            t6.Enabled = True
            t7.Enabled = False
            t8.Enabled = False
            t9.Enabled = False
            t10.Enabled = False
            t11.Enabled = False
            t12.Enabled = False
            t13.Enabled = False
            t14.Enabled = False
            t15.Enabled = False


        ElseIf cbo_nobytes.SelectedIndex = 7 Then

            t1.Enabled = True
            t2.Enabled = True
            t3.Enabled = True
            t4.Enabled = True
            t5.Enabled = True
            t6.Enabled = True
            t7.Enabled = True
            t8.Enabled = False
            t9.Enabled = False
            t10.Enabled = False
            t11.Enabled = False
            t12.Enabled = False
            t13.Enabled = False
            t14.Enabled = False
            t15.Enabled = False

        ElseIf cbo_nobytes.SelectedIndex = 8 Then

            t1.Enabled = True
            t2.Enabled = True
            t3.Enabled = True
            t4.Enabled = True
            t5.Enabled = True
            t6.Enabled = True
            t7.Enabled = True
            t8.Enabled = True
            t9.Enabled = False
            t10.Enabled = False
            t11.Enabled = False
            t12.Enabled = False
            t13.Enabled = False
            t14.Enabled = False
            t15.Enabled = False

        ElseIf cbo_nobytes.SelectedIndex = 9 Then

            t1.Enabled = True
            t2.Enabled = True
            t3.Enabled = True
            t4.Enabled = True
            t5.Enabled = True
            t6.Enabled = True
            t7.Enabled = True
            t8.Enabled = True
            t9.Enabled = True
            t10.Enabled = False
            t11.Enabled = False
            t12.Enabled = False
            t13.Enabled = False
            t14.Enabled = False
            t15.Enabled = False


        ElseIf cbo_nobytes.SelectedIndex = 10 Then

            t1.Enabled = True
            t2.Enabled = True
            t3.Enabled = True
            t4.Enabled = True
            t5.Enabled = True
            t6.Enabled = True
            t7.Enabled = True
            t8.Enabled = True
            t9.Enabled = True
            t10.Enabled = True
            t11.Enabled = False
            t12.Enabled = False
            t13.Enabled = False
            t14.Enabled = False
            t15.Enabled = False

        ElseIf cbo_nobytes.SelectedIndex = 11 Then

            t1.Enabled = True
            t2.Enabled = True
            t3.Enabled = True
            t4.Enabled = True
            t5.Enabled = True
            t6.Enabled = True
            t7.Enabled = True
            t8.Enabled = True
            t9.Enabled = True
            t10.Enabled = True
            t11.Enabled = True
            t12.Enabled = False
            t13.Enabled = False
            t14.Enabled = False
            t15.Enabled = False

        ElseIf cbo_nobytes.SelectedIndex = 12 Then

            t1.Enabled = True
            t2.Enabled = True
            t3.Enabled = True
            t4.Enabled = True
            t5.Enabled = True
            t6.Enabled = True
            t7.Enabled = True
            t8.Enabled = True
            t9.Enabled = True
            t10.Enabled = True
            t11.Enabled = True
            t12.Enabled = True
            t13.Enabled = False
            t14.Enabled = False
            t15.Enabled = False


        ElseIf cbo_nobytes.SelectedIndex = 13 Then

            t1.Enabled = True
            t2.Enabled = True
            t3.Enabled = True
            t4.Enabled = True
            t5.Enabled = True
            t6.Enabled = True
            t7.Enabled = True
            t8.Enabled = True
            t9.Enabled = True
            t10.Enabled = True
            t11.Enabled = True
            t12.Enabled = True
            t13.Enabled = True
            t14.Enabled = False
            t15.Enabled = False

        ElseIf cbo_nobytes.SelectedIndex = 14 Then

            t1.Enabled = True
            t2.Enabled = True
            t3.Enabled = True
            t4.Enabled = True
            t5.Enabled = True
            t6.Enabled = True
            t7.Enabled = True
            t8.Enabled = True
            t9.Enabled = True
            t10.Enabled = True
            t11.Enabled = True
            t12.Enabled = True
            t13.Enabled = True
            t14.Enabled = True
            t15.Enabled = False

        ElseIf cbo_nobytes.SelectedIndex = 15 Then

            t1.Enabled = True
            t2.Enabled = True
            t3.Enabled = True
            t4.Enabled = True
            t5.Enabled = True
            t6.Enabled = True
            t7.Enabled = True
            t8.Enabled = True
            t9.Enabled = True
            t10.Enabled = True
            t11.Enabled = True
            t12.Enabled = True
            t13.Enabled = True
            t14.Enabled = True
            t15.Enabled = True

        End If

    End Sub


    Private Sub cbo_nobytes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbo_nobytes.Click


    End Sub

    Private Sub cbo_rate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo_rate.SelectedIndexChanged

        Select Case cbo_rate.SelectedIndex
            Case 0 : TextBox1.Text = "11"
            Case 1 : TextBox1.Text = "92"
            Case 2 : TextBox1.Text = "93"
            Case 3 : TextBox1.Text = "94"
            Case 4 : TextBox1.Text = "95"
        End Select

    End Sub
End Class
'*************************************************************************************
' 
' DESCRIPTION :  This sample program demonstrate displaying and Configuring ATR value 
'                using PC/SC.  
'  
'*************************************************************************************