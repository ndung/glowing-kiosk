<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainMifareProg
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainMifareProg))
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbReader = New System.Windows.Forms.ComboBox
        Me.bInit = New System.Windows.Forms.Button
        Me.bConnect = New System.Windows.Forms.Button
        Me.gbLoadKeys = New System.Windows.Forms.GroupBox
        Me.bLoadKey = New System.Windows.Forms.Button
        Me.tKey6 = New System.Windows.Forms.TextBox
        Me.tKey5 = New System.Windows.Forms.TextBox
        Me.tKey4 = New System.Windows.Forms.TextBox
        Me.tKey3 = New System.Windows.Forms.TextBox
        Me.tKey1 = New System.Windows.Forms.TextBox
        Me.tKey2 = New System.Windows.Forms.TextBox
        Me.tMemAdd = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.rbVolMem = New System.Windows.Forms.RadioButton
        Me.rbNonVolMem = New System.Windows.Forms.RadioButton
        Me.gbAuth = New System.Windows.Forms.GroupBox
        Me.bAuth = New System.Windows.Forms.Button
        Me.tKeyIn6 = New System.Windows.Forms.TextBox
        Me.tKeyIn5 = New System.Windows.Forms.TextBox
        Me.tKeyIn4 = New System.Windows.Forms.TextBox
        Me.tKeyIn3 = New System.Windows.Forms.TextBox
        Me.tKeyIn2 = New System.Windows.Forms.TextBox
        Me.tKeyIn1 = New System.Windows.Forms.TextBox
        Me.tKeyAdd = New System.Windows.Forms.TextBox
        Me.tBlkNo = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.gbKType = New System.Windows.Forms.GroupBox
        Me.rbKType2 = New System.Windows.Forms.RadioButton
        Me.rbKType1 = New System.Windows.Forms.RadioButton
        Me.gbSource = New System.Windows.Forms.GroupBox
        Me.rbSource3 = New System.Windows.Forms.RadioButton
        Me.rbSource2 = New System.Windows.Forms.RadioButton
        Me.rbSource1 = New System.Windows.Forms.RadioButton
        Me.gbBinOps = New System.Windows.Forms.GroupBox
        Me.bBinUpd = New System.Windows.Forms.Button
        Me.bBinRead = New System.Windows.Forms.Button
        Me.tBinData = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.tBinLen = New System.Windows.Forms.TextBox
        Me.tBinBlk = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.gbValBlk = New System.Windows.Forms.GroupBox
        Me.bValRes = New System.Windows.Forms.Button
        Me.bValRead = New System.Windows.Forms.Button
        Me.bValDec = New System.Windows.Forms.Button
        Me.bValInc = New System.Windows.Forms.Button
        Me.bValStor = New System.Windows.Forms.Button
        Me.tValTar = New System.Windows.Forms.TextBox
        Me.tValSrc = New System.Windows.Forms.TextBox
        Me.tValBlk = New System.Windows.Forms.TextBox
        Me.tValAmt = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.mMsg = New System.Windows.Forms.ListBox
        Me.bClear = New System.Windows.Forms.Button
        Me.bReset = New System.Windows.Forms.Button
        Me.bQuit = New System.Windows.Forms.Button
        Me.gbLoadKeys.SuspendLayout()
        Me.gbAuth.SuspendLayout()
        Me.gbKType.SuspendLayout()
        Me.gbSource.SuspendLayout()
        Me.gbBinOps.SuspendLayout()
        Me.gbValBlk.SuspendLayout()
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
        Me.cbReader.Size = New System.Drawing.Size(216, 21)
        Me.cbReader.TabIndex = 1
        '
        'bInit
        '
        Me.bInit.Location = New System.Drawing.Point(191, 45)
        Me.bInit.Name = "bInit"
        Me.bInit.Size = New System.Drawing.Size(117, 23)
        Me.bInit.TabIndex = 2
        Me.bInit.Text = "Initialize"
        Me.bInit.UseVisualStyleBackColor = True
        '
        'bConnect
        '
        Me.bConnect.Location = New System.Drawing.Point(191, 74)
        Me.bConnect.Name = "bConnect"
        Me.bConnect.Size = New System.Drawing.Size(117, 23)
        Me.bConnect.TabIndex = 3
        Me.bConnect.Text = "Connect"
        Me.bConnect.UseVisualStyleBackColor = True
        '
        'gbLoadKeys
        '
        Me.gbLoadKeys.Controls.Add(Me.bLoadKey)
        Me.gbLoadKeys.Controls.Add(Me.tKey6)
        Me.gbLoadKeys.Controls.Add(Me.tKey5)
        Me.gbLoadKeys.Controls.Add(Me.tKey4)
        Me.gbLoadKeys.Controls.Add(Me.tKey3)
        Me.gbLoadKeys.Controls.Add(Me.tKey1)
        Me.gbLoadKeys.Controls.Add(Me.tKey2)
        Me.gbLoadKeys.Controls.Add(Me.tMemAdd)
        Me.gbLoadKeys.Controls.Add(Me.Label3)
        Me.gbLoadKeys.Controls.Add(Me.Label2)
        Me.gbLoadKeys.Controls.Add(Me.rbVolMem)
        Me.gbLoadKeys.Controls.Add(Me.rbNonVolMem)
        Me.gbLoadKeys.Location = New System.Drawing.Point(12, 103)
        Me.gbLoadKeys.Name = "gbLoadKeys"
        Me.gbLoadKeys.Size = New System.Drawing.Size(296, 135)
        Me.gbLoadKeys.TabIndex = 4
        Me.gbLoadKeys.TabStop = False
        Me.gbLoadKeys.Text = "Store Authentication Keys to Device"
        '
        'bLoadKey
        '
        Me.bLoadKey.Location = New System.Drawing.Point(169, 100)
        Me.bLoadKey.Name = "bLoadKey"
        Me.bLoadKey.Size = New System.Drawing.Size(118, 23)
        Me.bLoadKey.TabIndex = 11
        Me.bLoadKey.Text = "Load Key"
        Me.bLoadKey.UseVisualStyleBackColor = True
        '
        'tKey6
        '
        Me.tKey6.Location = New System.Drawing.Point(262, 74)
        Me.tKey6.MaxLength = 2
        Me.tKey6.Name = "tKey6"
        Me.tKey6.Size = New System.Drawing.Size(25, 20)
        Me.tKey6.TabIndex = 10
        '
        'tKey5
        '
        Me.tKey5.Location = New System.Drawing.Point(231, 74)
        Me.tKey5.MaxLength = 2
        Me.tKey5.Name = "tKey5"
        Me.tKey5.Size = New System.Drawing.Size(25, 20)
        Me.tKey5.TabIndex = 9
        '
        'tKey4
        '
        Me.tKey4.Location = New System.Drawing.Point(200, 74)
        Me.tKey4.MaxLength = 2
        Me.tKey4.Name = "tKey4"
        Me.tKey4.Size = New System.Drawing.Size(25, 20)
        Me.tKey4.TabIndex = 8
        '
        'tKey3
        '
        Me.tKey3.Location = New System.Drawing.Point(169, 74)
        Me.tKey3.MaxLength = 2
        Me.tKey3.Name = "tKey3"
        Me.tKey3.Size = New System.Drawing.Size(25, 20)
        Me.tKey3.TabIndex = 7
        '
        'tKey1
        '
        Me.tKey1.Location = New System.Drawing.Point(107, 74)
        Me.tKey1.MaxLength = 2
        Me.tKey1.Name = "tKey1"
        Me.tKey1.Size = New System.Drawing.Size(25, 20)
        Me.tKey1.TabIndex = 6
        '
        'tKey2
        '
        Me.tKey2.Location = New System.Drawing.Point(138, 74)
        Me.tKey2.MaxLength = 2
        Me.tKey2.Name = "tKey2"
        Me.tKey2.Size = New System.Drawing.Size(25, 20)
        Me.tKey2.TabIndex = 5
        '
        'tMemAdd
        '
        Me.tMemAdd.Location = New System.Drawing.Point(107, 46)
        Me.tMemAdd.MaxLength = 2
        Me.tMemAdd.Name = "tMemAdd"
        Me.tMemAdd.Size = New System.Drawing.Size(25, 20)
        Me.tMemAdd.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Key Value Input"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Key Store No."
        '
        'rbVolMem
        '
        Me.rbVolMem.AutoSize = True
        Me.rbVolMem.Location = New System.Drawing.Point(139, 21)
        Me.rbVolMem.Name = "rbVolMem"
        Me.rbVolMem.Size = New System.Drawing.Size(99, 17)
        Me.rbVolMem.TabIndex = 1
        Me.rbVolMem.TabStop = True
        Me.rbVolMem.Text = "Volatile Memory"
        Me.rbVolMem.UseVisualStyleBackColor = True
        '
        'rbNonVolMem
        '
        Me.rbNonVolMem.AutoSize = True
        Me.rbNonVolMem.Location = New System.Drawing.Point(10, 20)
        Me.rbNonVolMem.Name = "rbNonVolMem"
        Me.rbNonVolMem.Size = New System.Drawing.Size(122, 17)
        Me.rbNonVolMem.TabIndex = 0
        Me.rbNonVolMem.TabStop = True
        Me.rbNonVolMem.Text = "Non-Volatile Memory"
        Me.rbNonVolMem.UseVisualStyleBackColor = True
        '
        'gbAuth
        '
        Me.gbAuth.Controls.Add(Me.bAuth)
        Me.gbAuth.Controls.Add(Me.tKeyIn6)
        Me.gbAuth.Controls.Add(Me.tKeyIn5)
        Me.gbAuth.Controls.Add(Me.tKeyIn4)
        Me.gbAuth.Controls.Add(Me.tKeyIn3)
        Me.gbAuth.Controls.Add(Me.tKeyIn2)
        Me.gbAuth.Controls.Add(Me.tKeyIn1)
        Me.gbAuth.Controls.Add(Me.tKeyAdd)
        Me.gbAuth.Controls.Add(Me.tBlkNo)
        Me.gbAuth.Controls.Add(Me.Label6)
        Me.gbAuth.Controls.Add(Me.Label5)
        Me.gbAuth.Controls.Add(Me.Label4)
        Me.gbAuth.Controls.Add(Me.gbKType)
        Me.gbAuth.Controls.Add(Me.gbSource)
        Me.gbAuth.Location = New System.Drawing.Point(12, 244)
        Me.gbAuth.Name = "gbAuth"
        Me.gbAuth.Size = New System.Drawing.Size(297, 240)
        Me.gbAuth.TabIndex = 5
        Me.gbAuth.TabStop = False
        Me.gbAuth.Text = "Authentication Function"
        '
        'bAuth
        '
        Me.bAuth.Location = New System.Drawing.Point(169, 207)
        Me.bAuth.Name = "bAuth"
        Me.bAuth.Size = New System.Drawing.Size(118, 23)
        Me.bAuth.TabIndex = 13
        Me.bAuth.Text = "Authenticate"
        Me.bAuth.UseVisualStyleBackColor = True
        '
        'tKeyIn6
        '
        Me.tKeyIn6.Location = New System.Drawing.Point(262, 181)
        Me.tKeyIn6.MaxLength = 2
        Me.tKeyIn6.Name = "tKeyIn6"
        Me.tKeyIn6.Size = New System.Drawing.Size(25, 20)
        Me.tKeyIn6.TabIndex = 12
        '
        'tKeyIn5
        '
        Me.tKeyIn5.Location = New System.Drawing.Point(231, 181)
        Me.tKeyIn5.MaxLength = 2
        Me.tKeyIn5.Name = "tKeyIn5"
        Me.tKeyIn5.Size = New System.Drawing.Size(25, 20)
        Me.tKeyIn5.TabIndex = 11
        '
        'tKeyIn4
        '
        Me.tKeyIn4.Location = New System.Drawing.Point(200, 181)
        Me.tKeyIn4.MaxLength = 2
        Me.tKeyIn4.Name = "tKeyIn4"
        Me.tKeyIn4.Size = New System.Drawing.Size(25, 20)
        Me.tKeyIn4.TabIndex = 10
        '
        'tKeyIn3
        '
        Me.tKeyIn3.Location = New System.Drawing.Point(169, 181)
        Me.tKeyIn3.MaxLength = 2
        Me.tKeyIn3.Name = "tKeyIn3"
        Me.tKeyIn3.Size = New System.Drawing.Size(25, 20)
        Me.tKeyIn3.TabIndex = 9
        '
        'tKeyIn2
        '
        Me.tKeyIn2.Location = New System.Drawing.Point(138, 181)
        Me.tKeyIn2.MaxLength = 2
        Me.tKeyIn2.Name = "tKeyIn2"
        Me.tKeyIn2.Size = New System.Drawing.Size(25, 20)
        Me.tKeyIn2.TabIndex = 8
        '
        'tKeyIn1
        '
        Me.tKeyIn1.Location = New System.Drawing.Point(107, 181)
        Me.tKeyIn1.MaxLength = 2
        Me.tKeyIn1.Name = "tKeyIn1"
        Me.tKeyIn1.Size = New System.Drawing.Size(25, 20)
        Me.tKeyIn1.TabIndex = 7
        '
        'tKeyAdd
        '
        Me.tKeyAdd.Location = New System.Drawing.Point(107, 155)
        Me.tKeyAdd.MaxLength = 2
        Me.tKeyAdd.Name = "tKeyAdd"
        Me.tKeyAdd.Size = New System.Drawing.Size(25, 20)
        Me.tKeyAdd.TabIndex = 6
        '
        'tBlkNo
        '
        Me.tBlkNo.Location = New System.Drawing.Point(107, 128)
        Me.tBlkNo.MaxLength = 3
        Me.tBlkNo.Name = "tBlkNo"
        Me.tBlkNo.Size = New System.Drawing.Size(25, 20)
        Me.tBlkNo.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 181)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Key Value Input"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 155)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Key Store Number"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 128)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Block No (Dec)"
        '
        'gbKType
        '
        Me.gbKType.Controls.Add(Me.rbKType2)
        Me.gbKType.Controls.Add(Me.rbKType1)
        Me.gbKType.Location = New System.Drawing.Point(169, 19)
        Me.gbKType.Name = "gbKType"
        Me.gbKType.Size = New System.Drawing.Size(109, 86)
        Me.gbKType.TabIndex = 1
        Me.gbKType.TabStop = False
        Me.gbKType.Text = "Key Type"
        '
        'rbKType2
        '
        Me.rbKType2.AutoSize = True
        Me.rbKType2.Location = New System.Drawing.Point(16, 53)
        Me.rbKType2.Name = "rbKType2"
        Me.rbKType2.Size = New System.Drawing.Size(53, 17)
        Me.rbKType2.TabIndex = 2
        Me.rbKType2.TabStop = True
        Me.rbKType2.Text = "Key B"
        Me.rbKType2.UseVisualStyleBackColor = True
        '
        'rbKType1
        '
        Me.rbKType1.AutoSize = True
        Me.rbKType1.Location = New System.Drawing.Point(16, 19)
        Me.rbKType1.Name = "rbKType1"
        Me.rbKType1.Size = New System.Drawing.Size(53, 17)
        Me.rbKType1.TabIndex = 1
        Me.rbKType1.TabStop = True
        Me.rbKType1.Text = "Key A"
        Me.rbKType1.UseVisualStyleBackColor = True
        '
        'gbSource
        '
        Me.gbSource.Controls.Add(Me.rbSource3)
        Me.gbSource.Controls.Add(Me.rbSource2)
        Me.gbSource.Controls.Add(Me.rbSource1)
        Me.gbSource.Location = New System.Drawing.Point(10, 19)
        Me.gbSource.Name = "gbSource"
        Me.gbSource.Size = New System.Drawing.Size(140, 86)
        Me.gbSource.TabIndex = 0
        Me.gbSource.TabStop = False
        Me.gbSource.Text = "Key Source"
        '
        'rbSource3
        '
        Me.rbSource3.AutoSize = True
        Me.rbSource3.Location = New System.Drawing.Point(11, 63)
        Me.rbSource3.Name = "rbSource3"
        Me.rbSource3.Size = New System.Drawing.Size(122, 17)
        Me.rbSource3.TabIndex = 2
        Me.rbSource3.TabStop = True
        Me.rbSource3.Text = "Non-Volatile Memory"
        Me.rbSource3.UseVisualStyleBackColor = True
        '
        'rbSource2
        '
        Me.rbSource2.AutoSize = True
        Me.rbSource2.Location = New System.Drawing.Point(11, 42)
        Me.rbSource2.Name = "rbSource2"
        Me.rbSource2.Size = New System.Drawing.Size(99, 17)
        Me.rbSource2.TabIndex = 1
        Me.rbSource2.TabStop = True
        Me.rbSource2.Text = "Volatile Memory"
        Me.rbSource2.UseVisualStyleBackColor = True
        '
        'rbSource1
        '
        Me.rbSource1.AutoSize = True
        Me.rbSource1.Location = New System.Drawing.Point(11, 19)
        Me.rbSource1.Name = "rbSource1"
        Me.rbSource1.Size = New System.Drawing.Size(87, 17)
        Me.rbSource1.TabIndex = 0
        Me.rbSource1.TabStop = True
        Me.rbSource1.Text = "Manual Input"
        Me.rbSource1.UseVisualStyleBackColor = True
        '
        'gbBinOps
        '
        Me.gbBinOps.Controls.Add(Me.bBinUpd)
        Me.gbBinOps.Controls.Add(Me.bBinRead)
        Me.gbBinOps.Controls.Add(Me.tBinData)
        Me.gbBinOps.Controls.Add(Me.Label9)
        Me.gbBinOps.Controls.Add(Me.tBinLen)
        Me.gbBinOps.Controls.Add(Me.tBinBlk)
        Me.gbBinOps.Controls.Add(Me.Label8)
        Me.gbBinOps.Controls.Add(Me.Label7)
        Me.gbBinOps.Location = New System.Drawing.Point(12, 490)
        Me.gbBinOps.Name = "gbBinOps"
        Me.gbBinOps.Size = New System.Drawing.Size(297, 135)
        Me.gbBinOps.TabIndex = 6
        Me.gbBinOps.TabStop = False
        Me.gbBinOps.Text = "Binary Block Functions"
        '
        'bBinUpd
        '
        Me.bBinUpd.Location = New System.Drawing.Point(140, 106)
        Me.bBinUpd.Name = "bBinUpd"
        Me.bBinUpd.Size = New System.Drawing.Size(116, 23)
        Me.bBinUpd.TabIndex = 19
        Me.bBinUpd.Text = "Update Block"
        Me.bBinUpd.UseVisualStyleBackColor = True
        '
        'bBinRead
        '
        Me.bBinRead.Location = New System.Drawing.Point(27, 106)
        Me.bBinRead.Name = "bBinRead"
        Me.bBinRead.Size = New System.Drawing.Size(107, 23)
        Me.bBinRead.TabIndex = 18
        Me.bBinRead.Text = "Read Block"
        Me.bBinRead.UseVisualStyleBackColor = True
        '
        'tBinData
        '
        Me.tBinData.Location = New System.Drawing.Point(12, 78)
        Me.tBinData.Name = "tBinData"
        Me.tBinData.Size = New System.Drawing.Size(252, 20)
        Me.tBinData.TabIndex = 17
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(9, 62)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 13)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Data (Text)"
        '
        'tBinLen
        '
        Me.tBinLen.Location = New System.Drawing.Point(231, 25)
        Me.tBinLen.MaxLength = 2
        Me.tBinLen.Name = "tBinLen"
        Me.tBinLen.Size = New System.Drawing.Size(33, 20)
        Me.tBinLen.TabIndex = 15
        '
        'tBinBlk
        '
        Me.tBinBlk.Location = New System.Drawing.Point(101, 25)
        Me.tBinBlk.MaxLength = 2
        Me.tBinBlk.Name = "tBinBlk"
        Me.tBinBlk.Size = New System.Drawing.Size(33, 20)
        Me.tBinBlk.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(156, 28)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(69, 13)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "Length (Dec)"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 28)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Start Block (Dec)"
        '
        'gbValBlk
        '
        Me.gbValBlk.Controls.Add(Me.bValRes)
        Me.gbValBlk.Controls.Add(Me.bValRead)
        Me.gbValBlk.Controls.Add(Me.bValDec)
        Me.gbValBlk.Controls.Add(Me.bValInc)
        Me.gbValBlk.Controls.Add(Me.bValStor)
        Me.gbValBlk.Controls.Add(Me.tValTar)
        Me.gbValBlk.Controls.Add(Me.tValSrc)
        Me.gbValBlk.Controls.Add(Me.tValBlk)
        Me.gbValBlk.Controls.Add(Me.tValAmt)
        Me.gbValBlk.Controls.Add(Me.Label13)
        Me.gbValBlk.Controls.Add(Me.Label12)
        Me.gbValBlk.Controls.Add(Me.Label11)
        Me.gbValBlk.Controls.Add(Me.Label10)
        Me.gbValBlk.Location = New System.Drawing.Point(315, 12)
        Me.gbValBlk.Name = "gbValBlk"
        Me.gbValBlk.Size = New System.Drawing.Size(344, 168)
        Me.gbValBlk.TabIndex = 7
        Me.gbValBlk.TabStop = False
        Me.gbValBlk.Text = "Value Block Functions"
        '
        'bValRes
        '
        Me.bValRes.Location = New System.Drawing.Point(237, 133)
        Me.bValRes.Name = "bValRes"
        Me.bValRes.Size = New System.Drawing.Size(101, 23)
        Me.bValRes.TabIndex = 27
        Me.bValRes.Text = "Restore Value"
        Me.bValRes.UseVisualStyleBackColor = True
        '
        'bValRead
        '
        Me.bValRead.Location = New System.Drawing.Point(237, 104)
        Me.bValRead.Name = "bValRead"
        Me.bValRead.Size = New System.Drawing.Size(101, 23)
        Me.bValRead.TabIndex = 26
        Me.bValRead.Text = "Read Value"
        Me.bValRead.UseVisualStyleBackColor = True
        '
        'bValDec
        '
        Me.bValDec.Location = New System.Drawing.Point(237, 76)
        Me.bValDec.Name = "bValDec"
        Me.bValDec.Size = New System.Drawing.Size(101, 23)
        Me.bValDec.TabIndex = 25
        Me.bValDec.Text = "Decrement"
        Me.bValDec.UseVisualStyleBackColor = True
        '
        'bValInc
        '
        Me.bValInc.Location = New System.Drawing.Point(237, 50)
        Me.bValInc.Name = "bValInc"
        Me.bValInc.Size = New System.Drawing.Size(101, 23)
        Me.bValInc.TabIndex = 24
        Me.bValInc.Text = "Increment"
        Me.bValInc.UseVisualStyleBackColor = True
        '
        'bValStor
        '
        Me.bValStor.Location = New System.Drawing.Point(237, 22)
        Me.bValStor.Name = "bValStor"
        Me.bValStor.Size = New System.Drawing.Size(101, 23)
        Me.bValStor.TabIndex = 23
        Me.bValStor.Text = "Store Value"
        Me.bValStor.UseVisualStyleBackColor = True
        '
        'tValTar
        '
        Me.tValTar.Location = New System.Drawing.Point(96, 104)
        Me.tValTar.MaxLength = 2
        Me.tValTar.Name = "tValTar"
        Me.tValTar.Size = New System.Drawing.Size(33, 20)
        Me.tValTar.TabIndex = 22
        '
        'tValSrc
        '
        Me.tValSrc.Location = New System.Drawing.Point(96, 79)
        Me.tValSrc.MaxLength = 2
        Me.tValSrc.Name = "tValSrc"
        Me.tValSrc.Size = New System.Drawing.Size(33, 20)
        Me.tValSrc.TabIndex = 21
        '
        'tValBlk
        '
        Me.tValBlk.Location = New System.Drawing.Point(96, 53)
        Me.tValBlk.MaxLength = 2
        Me.tValBlk.Name = "tValBlk"
        Me.tValBlk.Size = New System.Drawing.Size(33, 20)
        Me.tValBlk.TabIndex = 20
        '
        'tValAmt
        '
        Me.tValAmt.Location = New System.Drawing.Point(96, 24)
        Me.tValAmt.Name = "tValAmt"
        Me.tValAmt.Size = New System.Drawing.Size(111, 20)
        Me.tValAmt.TabIndex = 4
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(17, 111)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(68, 13)
        Me.Label13.TabIndex = 3
        Me.Label13.Text = "Target Block"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(17, 82)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(71, 13)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Source Block"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(17, 53)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(54, 13)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Block No."
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(17, 27)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 13)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Value Amount"
        '
        'mMsg
        '
        Me.mMsg.FormattingEnabled = True
        Me.mMsg.Location = New System.Drawing.Point(315, 190)
        Me.mMsg.Name = "mMsg"
        Me.mMsg.Size = New System.Drawing.Size(344, 394)
        Me.mMsg.TabIndex = 8
        '
        'bClear
        '
        Me.bClear.Location = New System.Drawing.Point(325, 602)
        Me.bClear.Name = "bClear"
        Me.bClear.Size = New System.Drawing.Size(102, 23)
        Me.bClear.TabIndex = 9
        Me.bClear.Text = "Clear"
        Me.bClear.UseVisualStyleBackColor = True
        '
        'bReset
        '
        Me.bReset.Location = New System.Drawing.Point(433, 602)
        Me.bReset.Name = "bReset"
        Me.bReset.Size = New System.Drawing.Size(113, 23)
        Me.bReset.TabIndex = 24
        Me.bReset.Text = "Reset"
        Me.bReset.UseVisualStyleBackColor = True
        '
        'bQuit
        '
        Me.bQuit.Location = New System.Drawing.Point(552, 602)
        Me.bQuit.Name = "bQuit"
        Me.bQuit.Size = New System.Drawing.Size(101, 23)
        Me.bQuit.TabIndex = 25
        Me.bQuit.Text = "Quit"
        Me.bQuit.UseVisualStyleBackColor = True
        '
        'MainMifareProg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(670, 640)
        Me.Controls.Add(Me.bQuit)
        Me.Controls.Add(Me.bReset)
        Me.Controls.Add(Me.bClear)
        Me.Controls.Add(Me.mMsg)
        Me.Controls.Add(Me.gbValBlk)
        Me.Controls.Add(Me.gbBinOps)
        Me.Controls.Add(Me.gbAuth)
        Me.Controls.Add(Me.gbLoadKeys)
        Me.Controls.Add(Me.bConnect)
        Me.Controls.Add(Me.bInit)
        Me.Controls.Add(Me.cbReader)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MainMifareProg"
        Me.Text = "Mifare Card Programming"
        Me.gbLoadKeys.ResumeLayout(False)
        Me.gbLoadKeys.PerformLayout()
        Me.gbAuth.ResumeLayout(False)
        Me.gbAuth.PerformLayout()
        Me.gbKType.ResumeLayout(False)
        Me.gbKType.PerformLayout()
        Me.gbSource.ResumeLayout(False)
        Me.gbSource.PerformLayout()
        Me.gbBinOps.ResumeLayout(False)
        Me.gbBinOps.PerformLayout()
        Me.gbValBlk.ResumeLayout(False)
        Me.gbValBlk.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbReader As System.Windows.Forms.ComboBox
    Friend WithEvents bInit As System.Windows.Forms.Button
    Friend WithEvents bConnect As System.Windows.Forms.Button
    Friend WithEvents gbLoadKeys As System.Windows.Forms.GroupBox
    Friend WithEvents rbNonVolMem As System.Windows.Forms.RadioButton
    Friend WithEvents tKey1 As System.Windows.Forms.TextBox
    Friend WithEvents tMemAdd As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rbVolMem As System.Windows.Forms.RadioButton
    Friend WithEvents tKey6 As System.Windows.Forms.TextBox
    Friend WithEvents tKey5 As System.Windows.Forms.TextBox
    Friend WithEvents tKey4 As System.Windows.Forms.TextBox
    Friend WithEvents tKey3 As System.Windows.Forms.TextBox
    Friend WithEvents tKey2 As System.Windows.Forms.TextBox
    Friend WithEvents bLoadKey As System.Windows.Forms.Button
    Friend WithEvents gbAuth As System.Windows.Forms.GroupBox
    Friend WithEvents gbKType As System.Windows.Forms.GroupBox
    Friend WithEvents gbSource As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents rbKType2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbKType1 As System.Windows.Forms.RadioButton
    Friend WithEvents rbSource3 As System.Windows.Forms.RadioButton
    Friend WithEvents rbSource2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbSource1 As System.Windows.Forms.RadioButton
    Friend WithEvents bAuth As System.Windows.Forms.Button
    Friend WithEvents tKeyIn6 As System.Windows.Forms.TextBox
    Friend WithEvents tKeyIn5 As System.Windows.Forms.TextBox
    Friend WithEvents tKeyIn4 As System.Windows.Forms.TextBox
    Friend WithEvents tKeyIn3 As System.Windows.Forms.TextBox
    Friend WithEvents tKeyIn2 As System.Windows.Forms.TextBox
    Friend WithEvents tKeyIn1 As System.Windows.Forms.TextBox
    Friend WithEvents tKeyAdd As System.Windows.Forms.TextBox
    Friend WithEvents tBlkNo As System.Windows.Forms.TextBox
    Friend WithEvents gbBinOps As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents bBinUpd As System.Windows.Forms.Button
    Friend WithEvents bBinRead As System.Windows.Forms.Button
    Friend WithEvents tBinData As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tBinLen As System.Windows.Forms.TextBox
    Friend WithEvents tBinBlk As System.Windows.Forms.TextBox
    Friend WithEvents gbValBlk As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents tValTar As System.Windows.Forms.TextBox
    Friend WithEvents tValSrc As System.Windows.Forms.TextBox
    Friend WithEvents tValBlk As System.Windows.Forms.TextBox
    Friend WithEvents tValAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents bValRes As System.Windows.Forms.Button
    Friend WithEvents bValRead As System.Windows.Forms.Button
    Friend WithEvents bValDec As System.Windows.Forms.Button
    Friend WithEvents bValInc As System.Windows.Forms.Button
    Friend WithEvents bValStor As System.Windows.Forms.Button
    Friend WithEvents mMsg As System.Windows.Forms.ListBox
    Friend WithEvents bClear As System.Windows.Forms.Button
    Friend WithEvents bReset As System.Windows.Forms.Button
    Friend WithEvents bQuit As System.Windows.Forms.Button

End Class
