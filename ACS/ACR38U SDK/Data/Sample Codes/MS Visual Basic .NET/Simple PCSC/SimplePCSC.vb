'=======================================================================================
'
'   Class Form:  SimplePCSC.vb
'
'   Company   : Advanced Card System LTD.
'
'   Author    : Aileen Grace Sarte
'
'   Date      : September 21, 2006
'
'   Revision    :
'                Name                   Date                    Brief Description
'=======================================================================================

Public Class SimplePCSC
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents bInit As System.Windows.Forms.Button
    Friend WithEvents bList As System.Windows.Forms.Button
    Friend WithEvents bConnect As System.Windows.Forms.Button
    Friend WithEvents bBeginTran As System.Windows.Forms.Button
    Friend WithEvents bStatus As System.Windows.Forms.Button
    Friend WithEvents bTransmit As System.Windows.Forms.Button
    Friend WithEvents bEnd As System.Windows.Forms.Button
    Friend WithEvents bDisconnect As System.Windows.Forms.Button
    Friend WithEvents bRelease As System.Windows.Forms.Button
    Friend WithEvents bClear As System.Windows.Forms.Button
    Friend WithEvents bQuit As System.Windows.Forms.Button
    Friend WithEvents cbReader As System.Windows.Forms.ComboBox
    Friend WithEvents fApdu As System.Windows.Forms.GroupBox
    Friend WithEvents tDataIn As System.Windows.Forms.TextBox
    Friend WithEvents mMsg As System.Windows.Forms.RichTextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SimplePCSC))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.mMsg = New System.Windows.Forms.RichTextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cbReader = New System.Windows.Forms.ComboBox
        Me.bClear = New System.Windows.Forms.Button
        Me.bQuit = New System.Windows.Forms.Button
        Me.bRelease = New System.Windows.Forms.Button
        Me.bDisconnect = New System.Windows.Forms.Button
        Me.bEnd = New System.Windows.Forms.Button
        Me.bTransmit = New System.Windows.Forms.Button
        Me.bStatus = New System.Windows.Forms.Button
        Me.bBeginTran = New System.Windows.Forms.Button
        Me.bConnect = New System.Windows.Forms.Button
        Me.bList = New System.Windows.Forms.Button
        Me.bInit = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.fApdu = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.tDataIn = New System.Windows.Forms.TextBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.fApdu.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.mMsg)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.bClear)
        Me.GroupBox1.Controls.Add(Me.bQuit)
        Me.GroupBox1.Controls.Add(Me.bRelease)
        Me.GroupBox1.Controls.Add(Me.bDisconnect)
        Me.GroupBox1.Controls.Add(Me.bEnd)
        Me.GroupBox1.Controls.Add(Me.bTransmit)
        Me.GroupBox1.Controls.Add(Me.bStatus)
        Me.GroupBox1.Controls.Add(Me.bBeginTran)
        Me.GroupBox1.Controls.Add(Me.bConnect)
        Me.GroupBox1.Controls.Add(Me.bList)
        Me.GroupBox1.Controls.Add(Me.bInit)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.fApdu)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(424, 631)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'mMsg
        '
        Me.mMsg.Location = New System.Drawing.Point(17, 416)
        Me.mMsg.Name = "mMsg"
        Me.mMsg.Size = New System.Drawing.Size(383, 200)
        Me.mMsg.TabIndex = 14
        Me.mMsg.Text = ""
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cbReader)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(202, 64)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(200, 56)
        Me.GroupBox2.TabIndex = 12
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Port"
        '
        'cbReader
        '
        Me.cbReader.Location = New System.Drawing.Point(16, 19)
        Me.cbReader.Name = "cbReader"
        Me.cbReader.Size = New System.Drawing.Size(168, 23)
        Me.cbReader.TabIndex = 3
        '
        'bClear
        '
        Me.bClear.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bClear.Location = New System.Drawing.Point(208, 374)
        Me.bClear.Name = "bClear"
        Me.bClear.Size = New System.Drawing.Size(168, 32)
        Me.bClear.TabIndex = 13
        Me.bClear.Text = "Clear Output Window"
        '
        'bQuit
        '
        Me.bQuit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bQuit.Location = New System.Drawing.Point(208, 334)
        Me.bQuit.Name = "bQuit"
        Me.bQuit.Size = New System.Drawing.Size(168, 32)
        Me.bQuit.TabIndex = 12
        Me.bQuit.Text = "Quit"
        '
        'bRelease
        '
        Me.bRelease.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bRelease.Location = New System.Drawing.Point(17, 373)
        Me.bRelease.Name = "bRelease"
        Me.bRelease.Size = New System.Drawing.Size(168, 32)
        Me.bRelease.TabIndex = 11
        Me.bRelease.Text = "SCardReleaseContext"
        '
        'bDisconnect
        '
        Me.bDisconnect.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bDisconnect.Location = New System.Drawing.Point(17, 334)
        Me.bDisconnect.Name = "bDisconnect"
        Me.bDisconnect.Size = New System.Drawing.Size(168, 32)
        Me.bDisconnect.TabIndex = 10
        Me.bDisconnect.Text = "SCardDisconnect"
        '
        'bEnd
        '
        Me.bEnd.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bEnd.Location = New System.Drawing.Point(17, 295)
        Me.bEnd.Name = "bEnd"
        Me.bEnd.Size = New System.Drawing.Size(168, 32)
        Me.bEnd.TabIndex = 9
        Me.bEnd.Text = "SCardEndTransaction"
        '
        'bTransmit
        '
        Me.bTransmit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bTransmit.Location = New System.Drawing.Point(16, 256)
        Me.bTransmit.Name = "bTransmit"
        Me.bTransmit.Size = New System.Drawing.Size(168, 32)
        Me.bTransmit.TabIndex = 7
        Me.bTransmit.Text = "SCardTransmit"
        '
        'bStatus
        '
        Me.bStatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bStatus.Location = New System.Drawing.Point(16, 217)
        Me.bStatus.Name = "bStatus"
        Me.bStatus.Size = New System.Drawing.Size(168, 32)
        Me.bStatus.TabIndex = 6
        Me.bStatus.Text = "SCardStatus"
        '
        'bBeginTran
        '
        Me.bBeginTran.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bBeginTran.Location = New System.Drawing.Point(16, 178)
        Me.bBeginTran.Name = "bBeginTran"
        Me.bBeginTran.Size = New System.Drawing.Size(168, 32)
        Me.bBeginTran.TabIndex = 5
        Me.bBeginTran.Text = "SCardBeginTransaction"
        '
        'bConnect
        '
        Me.bConnect.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bConnect.Location = New System.Drawing.Point(16, 139)
        Me.bConnect.Name = "bConnect"
        Me.bConnect.Size = New System.Drawing.Size(168, 32)
        Me.bConnect.TabIndex = 4
        Me.bConnect.Text = "SCardConnect"
        '
        'bList
        '
        Me.bList.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bList.Location = New System.Drawing.Point(16, 99)
        Me.bList.Name = "bList"
        Me.bList.Size = New System.Drawing.Size(168, 32)
        Me.bList.TabIndex = 2
        Me.bList.Text = "SCardListReaders"
        '
        'bInit
        '
        Me.bInit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bInit.Location = New System.Drawing.Point(16, 60)
        Me.bInit.Name = "bInit"
        Me.bInit.Size = New System.Drawing.Size(168, 32)
        Me.bInit.TabIndex = 1
        Me.bInit.Text = "SCardEstablishContext"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(264, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "This is simple PCSC Application"
        '
        'fApdu
        '
        Me.fApdu.Controls.Add(Me.Label2)
        Me.fApdu.Controls.Add(Me.tDataIn)
        Me.fApdu.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fApdu.Location = New System.Drawing.Point(202, 237)
        Me.fApdu.Name = "fApdu"
        Me.fApdu.Size = New System.Drawing.Size(200, 67)
        Me.fApdu.TabIndex = 13
        Me.fApdu.TabStop = False
        Me.fApdu.Text = "APDU Input"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(17, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "(Use HEX values only)"
        '
        'tDataIn
        '
        Me.tDataIn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tDataIn.Location = New System.Drawing.Point(17, 24)
        Me.tDataIn.MaxLength = 0
        Me.tDataIn.Name = "tDataIn"
        Me.tDataIn.Size = New System.Drawing.Size(160, 21)
        Me.tDataIn.TabIndex = 8
        '
        'SimplePCSC
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(437, 652)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "SimplePCSC"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Simple PCSC"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.fApdu.ResumeLayout(False)
        Me.fApdu.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim retCode, Protocol, hContext, hCard, ReaderCount As Integer
    Dim sReaderList As String
    Dim sReaderGroup As String
    Dim diFlag As Boolean
    Dim ioRequest As SCARD_IO_REQUEST
    Dim SendLen, RecvLen As Integer
    Dim SendBuff(262) As Byte
    Dim RecvBuff(262) As Byte

    Public Const INVALID_SW1SW2 = -450


    Private Sub ClearBuffers()

        Dim indx As Integer

        For indx = 0 To 262
            RecvBuff(indx) = &H0
            SendBuff(indx) = &H0
        Next indx

    End Sub


    Private Sub DisableAPDUInput()

        tDataIn.Text = ""
        fApdu.Enabled = False

    End Sub


    Private Sub InitMenu()

        cbReader.Items.Clear()
        bInit.Enabled = True
        bList.Enabled = False
        bConnect.Enabled = False
        bBeginTran.Enabled = False
        bStatus.Enabled = False
        bTransmit.Enabled = False
        bEnd.Enabled = False
        bDisconnect.Enabled = False
        bRelease.Enabled = False
        mMsg.Text = ""
        Call DisplayOut(0, 0, "Program ready")
        Call DisableAPDUInput()

    End Sub


    Private Sub DisplayOut(ByVal mType As Integer, ByVal msgCode As Long, ByVal PrintText As String)

        Select Case mType
            Case 0                                  ' Notifications only
                mMsg.SelectionColor = Color.Green
            Case 1                                  ' Error Messages
                mMsg.SelectionColor = Color.Red
                mMsg.SelectedText = GetScardErrMsg(retCode)
            Case 2                                  ' Input data
                mMsg.SelectionColor = Color.Black
                PrintText = "< " & PrintText
            Case 3                                  ' Output data
                mMsg.SelectionColor = Color.Black
                PrintText = "> " & PrintText
            Case 4                                  ' Critical Errors
                mMsg.SelectionColor = Color.Red
        End Select

        mMsg.SelectedText = PrintText & vbCrLf
        mMsg.SelectionStart = Len(mMsg.Text)
        mMsg.SelectionColor = Color.Black

    End Sub


    Private Function TrimInput(ByVal textInp As String) As String

        Dim tmpStr As String
        Dim indx As Short

        tmpStr = ""
        textInp = Trim(textInp)

        For indx = 1 To Len(textInp)
            If Microsoft.VisualBasic.Mid(textInp, indx, 1) <> Chr(32) Then
                tmpStr = tmpStr & Microsoft.VisualBasic.Mid(textInp, indx, 1)
            End If
        Next indx
        diFlag = False
        If (Len(tmpStr) Mod 2 <> 0) Then
            diFlag = True
        End If

        TrimInput = tmpStr

    End Function


    Private Function SendAPDU() As Integer

        Dim indx As Short
        Dim tmpStr As String

        ioRequest.dwProtocol = Protocol
        ioRequest.cbPciLength = Len(ioRequest)
        tmpStr = ""
        RecvLen = 262

        retCode = ModWinscard.SCardTransmit(hCard, ioRequest, SendBuff(0), SendLen, ioRequest, RecvBuff(0), RecvLen)

        If retCode <> ModWinscard.SCARD_S_SUCCESS Then
            Call DisplayOut(1, retCode, "")
            SendAPDU = retCode
            Exit Function
        End If

        SendAPDU = retCode

    End Function


    Private Sub bInit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bInit.Click

        ReaderCount = 255

        'Establish context and obtain hContext handle
        retCode = ModWinscard.SCardEstablishContext(ModWinscard.SCARD_SCOPE_USER, 0, 0, hContext)
        If retCode <> ModWinscard.SCARD_S_SUCCESS Then
            Call DisplayOut(1, retCode, "")
            Exit Sub
        Else
            Call DisplayOut(0, 0, "SCardEstablishContext... OK")

        End If

        bInit.Enabled = False
        bList.Enabled = True
        bRelease.Enabled = True

    End Sub


    Private Sub bList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bList.Click

        Dim ctr As Integer

        For ctr = 0 To 255
            sReaderList = sReaderList + vbNullChar
        Next

        ReaderCount = 255

        'List PC/SC card readers installed in the system
        retCode = ModWinscard.SCardListReaders(hContext, sReaderGroup, sReaderList, ReaderCount)

        If retCode <> ModWinscard.SCARD_S_SUCCESS Then
            Call DisplayOut(1, retCode, "")
            Exit Sub
        Else
            Call DisplayOut(0, 0, "SCardListReaders... OK")

        End If
        Call LoadListToControl(cbReader, sReaderList)
        cbReader.SelectedIndex = 0

        bConnect.Enabled = True

    End Sub


    Private Sub bConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bConnect.Click

        'Connect to selected reader using hContext handle
        'and obtain valid hCard handle
        retCode = ModWinscard.SCardConnect(hContext, _
                                           cbReader.Text, _
                                           ModWinscard.SCARD_SHARE_EXCLUSIVE, _
                                           ModWinscard.SCARD_PROTOCOL_T0 Or ModWinscard.SCARD_PROTOCOL_T1, _
                                           hCard, _
                                           Protocol)
        If retCode <> ModWinscard.SCARD_S_SUCCESS Then
            Call DisplayOut(1, retCode, "")
            Exit Sub
        Else
            Call DisplayOut(0, 0, "SCardConnect...OK")

        End If

        bList.Enabled = False
        bConnect.Enabled = False
        bBeginTran.Enabled = True
        bDisconnect.Enabled = True
        bRelease.Enabled = False

    End Sub


    Private Sub bBeginTran_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBeginTran.Click

        retCode = ModWinscard.SCardBeginTransaction(hCard)

        If retCode <> ModWinscard.SCARD_S_SUCCESS Then
            Call DisplayOut(1, retCode, "")
            Exit Sub
        Else
            Call DisplayOut(0, 0, "SCardBeginTransaction...OK")

        End If
        fApdu.Enabled = True
        tDataIn.Focus()
        bBeginTran.Enabled = False
        bStatus.Enabled = True
        bTransmit.Enabled = True
        bEnd.Enabled = True
        bDisconnect.Enabled = False

    End Sub


    Private Sub bStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bStatus.Click

        Dim ReaderLen, dwState, ATRLen, indx As Long
        Dim ATRVal(31) As Byte
        Dim tmpStr As String
        ATRLen = 32

        Dim tmpReader = String.Empty
        ReaderLen = 100
        retCode = ModWinsCard.SCardStatus(hCard, _
                                          tmpReader, _
                                          ReaderLen, _
                                          dwState, _
                                          Protocol, _
                                          ATRVal(0), _
                                          ATRLen)
        If retCode <> ModWinscard.SCARD_S_SUCCESS Then
            Call DisplayOut(1, retCode, "")
            Exit Sub
        Else
            Call DisplayOut(0, 0, "SCardStatus...OK")

        End If

        'Format ATRVal returned and display string as ATR value
        tmpStr = ""
        For indx = 0 To ATRLen - 1
            tmpStr = tmpStr & Microsoft.VisualBasic.Right("00" & Hex(ATRVal(indx)), 2) & " "
        Next indx
        Call DisplayOut(3, 0, tmpStr)

        'Interpret dwstate returned and display as state
        Select Case dwState
            Case 0
                tmpStr = "SCARD_UNKNOWN"
            Case 1
                tmpStr = "SCARD_ABSENT"
            Case 2
                tmpStr = "SCARD_PRESENT"
            Case 3
                tmpStr = "SCARD_SWALLOWED"
            Case 4
                tmpStr = "SCARD_POWERED"
            Case 5
                tmpStr = "SCARD_NEGOTIABLE"
            Case 6
                tmpStr = "SCARD_SPECIFIC"
        End Select
        Call DisplayOut(3, 0, "Reader State: " & tmpStr)

        'Interpret dwActProtocol returned and display as active protocol
        Select Case Protocol
            Case 1
                tmpStr = "T=0"
            Case 2
                tmpStr = "T=1"
        End Select
        Call DisplayOut(3, 0, "Active protocol: " & tmpStr)

    End Sub


    Private Sub bTransmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bTransmit.Click

        Dim indx As Integer
        Dim tmpStr As String

        If tDataIn.Text = "" Then
            Call DisplayOut(4, 0, "No data input")
            Exit Sub
        End If

        tmpStr = TrimInput(tDataIn.Text)
        If Len(tmpStr) < 10 Then
            Call DisplayOut(4, 0, "Insufficient data input")
            Exit Sub
        End If
        If diFlag Then
            Call DisplayOut(4, 0, "Invalid data input, uneven number of characters")
            tDataIn.Focus()
            Exit Sub
        End If

        Call ClearBuffers()
        For indx = 0 To 4
            SendBuff(indx) = CInt("&H" & Microsoft.VisualBasic.Mid(tmpStr, indx * 2 + 1, 2))
        Next indx

        ' if APDU length < 6 then P3 is Le
        If Len(tmpStr) < 12 Then
            For indx = 0 To 4
                SendBuff(indx) = CInt("&H" & Microsoft.VisualBasic.Mid(tmpStr, indx * 2 + 1, 2))
            Next indx
            SendLen = &H5
            RecvLen = SendBuff(4) + 2
            tmpStr = ""
            For indx = 0 To 4
                tmpStr = tmpStr & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(indx)), 2) & " "
            Next indx
            Call DisplayOut(2, 0, tmpStr)
            retCode = SendAPDU()
            If retCode = ModWinscard.SCARD_S_SUCCESS Then
                tmpStr = ""
                For indx = 0 To RecvLen - 1
                    tmpStr = tmpStr & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) & " "
                Next indx
                Call DisplayOut(3, 0, tmpStr)
            End If
        Else
            For indx = 0 To 4
                SendBuff(indx) = CInt("&H" & Microsoft.VisualBasic.Mid(tmpStr, indx * 2 + 1, 2))
            Next indx
            SendLen = &H5 + SendBuff(4)
            If Len(tmpStr) < SendLen * 2 Then
                Call DisplayOut(4, 0, "Invalid data input, insufficient data length")
                tDataIn.Focus()
                Exit Sub
            End If
            For indx = 0 To SendBuff(4) - 1
                SendBuff(indx + 5) = CInt("&H" & Microsoft.VisualBasic.Mid(tmpStr, (indx + 5) * 2 + 1, 2))
            Next indx
            RecvLen = &H2
            tmpStr = ""
            For indx = 0 To SendLen - 1
                tmpStr = tmpStr & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(indx)), 2) & " "
            Next indx
            Call DisplayOut(2, 0, tmpStr)
            retCode = SendAPDU()
            If retCode = ModWinscard.SCARD_S_SUCCESS Then
                tmpStr = ""
                For indx = 0 To RecvLen - 1
                    tmpStr = tmpStr & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) & " "
                Next indx
                Call DisplayOut(3, 0, tmpStr)
            End If
        End If

    End Sub


    Private Sub bEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bEnd.Click

        retCode = ModWinscard.SCardEndTransaction(hCard, ModWinscard.SCARD_LEAVE_CARD)
        If retCode <> ModWinscard.SCARD_S_SUCCESS Then
            Call DisplayOut(1, retCode, "")
            Exit Sub
        Else
            Call DisplayOut(0, 0, "SCardEndTransaction...OK")

        End If

        Call DisableAPDUInput()

        bBeginTran.Enabled = True
        bStatus.Enabled = False
        bTransmit.Enabled = False
        bEnd.Enabled = False
        bDisconnect.Enabled = True

    End Sub


    Private Sub bDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bDisconnect.Click

        retCode = ModWinscard.SCardDisconnect(hCard, ModWinscard.SCARD_UNPOWER_CARD)

        If retCode <> ModWinscard.SCARD_S_SUCCESS Then
            Call DisplayOut(1, retCode, "")
            Exit Sub
        Else
            Call DisplayOut(0, 0, "SCardDisconnect...OK")

        End If

        bList.Enabled = True
        bConnect.Enabled = True
        bBeginTran.Enabled = False
        bDisconnect.Enabled = False
        bRelease.Enabled = True

    End Sub


    Private Sub bRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bRelease.Click

        retCode = ModWinscard.SCardReleaseContext(hContext)

        If retCode <> ModWinscard.SCARD_S_SUCCESS Then
            Call DisplayOut(1, retCode, "")
            Exit Sub
        Else
            Call DisplayOut(0, 0, "SCardReleaseContext...OK")

        End If

        bInit.Enabled = True
        bList.Enabled = False
        bConnect.Enabled = False
        bRelease.Enabled = False
        cbReader.Items.Clear()
    End Sub


    Private Sub bQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bQuit.Click

        retCode = ModWinscard.SCardDisconnect(hCard, ModWinscard.SCARD_UNPOWER_CARD)
        retCode = ModWinscard.SCardReleaseContext(hContext)
        Me.Close()

    End Sub


    Private Sub bClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bClear.Click

        mMsg.Clear()

    End Sub


    Private Sub cbReader_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbReader.Click

        retCode = ModWinscard.SCardDisconnect(hCard, ModWinscard.SCARD_UNPOWER_CARD)

    End Sub


    Private Sub SimplePCSC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call InitMenu()

    End Sub


    Public Function KeyVerify(ByVal key As Integer) As Boolean

        If key >= 48 And key <= 57 Then
            KeyVerify = False
        ElseIf key >= 65 And key <= 70 Then
            KeyVerify = False
        ElseIf key >= 97 And key <= 102 Then
            KeyVerify = False
        ElseIf key = 8 Then
            KeyVerify = False
        Else
            KeyVerify = True
        End If

    End Function


    Private Sub tDataIn_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tDataIn.KeyPress
        e.Handled = KeyVerify(Asc(e.KeyChar))
    End Sub


    Public Sub LoadListToControl(ByVal Ctrl As ComboBox, ByVal ReaderList As String)

        Dim sTemp As String
        Dim indx As Integer

        indx = 1
        sTemp = ""
        Ctrl.Items.Clear()

        While (Mid(ReaderList, indx, 1) <> vbNullChar)

            While (Mid(ReaderList, indx, 1) <> vbNullChar)
                sTemp = sTemp + Mid(ReaderList, indx, 1)
                indx = indx + 1
            End While
            indx = indx + 1
            Ctrl.Items.Add(sTemp)
            sTemp = ""

        End While

    End Sub


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
            Case ModWinsCard.SCARD_F_WAITED_TOO_Integer
                GetScardErrMsg = "An internal consistency timer has expired."
            Case ModWinsCard.SCARD_S_SUCCESS
                GetScardErrMsg = "No error was encountered."
            Case ModWinsCard.SCARD_W_REMOVED_CARD
                GetScardErrMsg = "The smart card has been removed, so that further communication is not possible."
            Case ModWinsCard.SCARD_W_RESET_CARD
                GetScardErrMsg = "The smart card has been reset, so any shared state information is invalid."
            Case ModWinsCard.SCARD_W_UNPOWERED_CARD
                GetScardErrMsg = "Power has been removed from the smart card, so that further communication is not possible."
            Case ModWinsCard.SCARD_W_UNRESPONSIVE_CARD
                GetScardErrMsg = "The smart card is not responding to a reset."
            Case ModWinsCard.SCARD_W_UNSUPPORTED_CARD
                GetScardErrMsg = "The reader cannot communicate with the card, due to ATR string configuration conflicts."
            Case Else
                GetScardErrMsg = "?"
        End Select

    End Function

End Class
