'  Copyright(C):      Advanced Card Systems Ltd
'
'  Project Name:      IIC
'
'  Description :      This sample program outlines the steps on how to
'                     program IIC memory cards using ACS readers
'                     in PC/SC platform.
'
'  Author:            Aileen Grace L. Sarte
'
'  Date:              February 12, 2007
'
'  Revision Trail:   (Date/Author/Description)
'
'
'================================================================================================


Public Class frm_Main
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbReader As System.Windows.Forms.ComboBox
    Friend WithEvents bInit As System.Windows.Forms.Button
    Friend WithEvents bConnect As System.Windows.Forms.Button
    Friend WithEvents tLoAdd As System.Windows.Forms.TextBox
    Friend WithEvents tHiAdd As System.Windows.Forms.TextBox
    Friend WithEvents bReset As System.Windows.Forms.Button
    Friend WithEvents bQuit As System.Windows.Forms.Button
    Friend WithEvents tData As System.Windows.Forms.TextBox
    Friend WithEvents tLen As System.Windows.Forms.TextBox
    Friend WithEvents bWrite As System.Windows.Forms.Button
    Friend WithEvents bRead As System.Windows.Forms.Button
    Friend WithEvents mMsg As System.Windows.Forms.RichTextBox
    Friend WithEvents fFunction As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbCardType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbPageSize As System.Windows.Forms.ComboBox
    Friend WithEvents bSet As System.Windows.Forms.Button
    Friend WithEvents tBitAdd As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frm_Main))
        Me.cbReader = New System.Windows.Forms.ComboBox
        Me.bInit = New System.Windows.Forms.Button
        Me.bConnect = New System.Windows.Forms.Button
        Me.bReset = New System.Windows.Forms.Button
        Me.bQuit = New System.Windows.Forms.Button
        Me.fFunction = New System.Windows.Forms.GroupBox
        Me.tBitAdd = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.tData = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.tLen = New System.Windows.Forms.TextBox
        Me.tLoAdd = New System.Windows.Forms.TextBox
        Me.tHiAdd = New System.Windows.Forms.TextBox
        Me.bWrite = New System.Windows.Forms.Button
        Me.bRead = New System.Windows.Forms.Button
        Me.cbPageSize = New System.Windows.Forms.ComboBox
        Me.bSet = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.mMsg = New System.Windows.Forms.RichTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cbCardType = New System.Windows.Forms.ComboBox
        Me.fFunction.SuspendLayout()
        Me.SuspendLayout()
        '
        'cbReader
        '
        Me.cbReader.Location = New System.Drawing.Point(116, 13)
        Me.cbReader.Name = "cbReader"
        Me.cbReader.Size = New System.Drawing.Size(121, 21)
        Me.cbReader.TabIndex = 0
        '
        'bInit
        '
        Me.bInit.Location = New System.Drawing.Point(152, 43)
        Me.bInit.Name = "bInit"
        Me.bInit.Size = New System.Drawing.Size(80, 24)
        Me.bInit.TabIndex = 2
        Me.bInit.Text = "Initialize"
        '
        'bConnect
        '
        Me.bConnect.Location = New System.Drawing.Point(152, 112)
        Me.bConnect.Name = "bConnect"
        Me.bConnect.Size = New System.Drawing.Size(80, 24)
        Me.bConnect.TabIndex = 3
        Me.bConnect.Text = "Connect"
        '
        'bReset
        '
        Me.bReset.Location = New System.Drawing.Point(352, 365)
        Me.bReset.Name = "bReset"
        Me.bReset.Size = New System.Drawing.Size(80, 24)
        Me.bReset.TabIndex = 5
        Me.bReset.Text = "Reset"
        '
        'bQuit
        '
        Me.bQuit.Location = New System.Drawing.Point(440, 365)
        Me.bQuit.Name = "bQuit"
        Me.bQuit.Size = New System.Drawing.Size(80, 24)
        Me.bQuit.TabIndex = 6
        Me.bQuit.Text = "Quit"
        '
        'fFunction
        '
        Me.fFunction.Controls.Add(Me.tBitAdd)
        Me.fFunction.Controls.Add(Me.Label3)
        Me.fFunction.Controls.Add(Me.tData)
        Me.fFunction.Controls.Add(Me.Label6)
        Me.fFunction.Controls.Add(Me.Label5)
        Me.fFunction.Controls.Add(Me.Label4)
        Me.fFunction.Controls.Add(Me.tLen)
        Me.fFunction.Controls.Add(Me.tLoAdd)
        Me.fFunction.Controls.Add(Me.tHiAdd)
        Me.fFunction.Controls.Add(Me.bWrite)
        Me.fFunction.Controls.Add(Me.bRead)
        Me.fFunction.Controls.Add(Me.cbPageSize)
        Me.fFunction.Controls.Add(Me.bSet)
        Me.fFunction.Location = New System.Drawing.Point(12, 144)
        Me.fFunction.Name = "fFunction"
        Me.fFunction.Size = New System.Drawing.Size(224, 208)
        Me.fFunction.TabIndex = 7
        Me.fFunction.TabStop = False
        Me.fFunction.Text = "Functions"
        '
        'tBitAdd
        '
        Me.tBitAdd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tBitAdd.Location = New System.Drawing.Point(65, 64)
        Me.tBitAdd.MaxLength = 2
        Me.tBitAdd.Name = "tBitAdd"
        Me.tBitAdd.Size = New System.Drawing.Size(32, 20)
        Me.tBitAdd.TabIndex = 16
        Me.tBitAdd.Text = ""
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(10, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 16)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Size"
        '
        'tData
        '
        Me.tData.Location = New System.Drawing.Point(65, 128)
        Me.tData.MaxLength = 0
        Me.tData.Name = "tData"
        Me.tData.Size = New System.Drawing.Size(136, 20)
        Me.tData.TabIndex = 14
        Me.tData.Text = ""
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(9, 136)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 16)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Data"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(9, 96)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 16)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Length"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(9, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 16)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Address"
        '
        'tLen
        '
        Me.tLen.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tLen.Location = New System.Drawing.Point(65, 96)
        Me.tLen.MaxLength = 2
        Me.tLen.Name = "tLen"
        Me.tLen.Size = New System.Drawing.Size(32, 20)
        Me.tLen.TabIndex = 7
        Me.tLen.Text = ""
        '
        'tLoAdd
        '
        Me.tLoAdd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tLoAdd.Location = New System.Drawing.Point(144, 64)
        Me.tLoAdd.MaxLength = 2
        Me.tLoAdd.Name = "tLoAdd"
        Me.tLoAdd.Size = New System.Drawing.Size(32, 20)
        Me.tLoAdd.TabIndex = 6
        Me.tLoAdd.Text = ""
        '
        'tHiAdd
        '
        Me.tHiAdd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tHiAdd.Location = New System.Drawing.Point(104, 64)
        Me.tHiAdd.MaxLength = 2
        Me.tHiAdd.Name = "tHiAdd"
        Me.tHiAdd.Size = New System.Drawing.Size(32, 20)
        Me.tHiAdd.TabIndex = 5
        Me.tHiAdd.Text = ""
        '
        'bWrite
        '
        Me.bWrite.Location = New System.Drawing.Point(128, 168)
        Me.bWrite.Name = "bWrite"
        Me.bWrite.Size = New System.Drawing.Size(80, 24)
        Me.bWrite.TabIndex = 3
        Me.bWrite.Text = "Write"
        '
        'bRead
        '
        Me.bRead.Location = New System.Drawing.Point(16, 168)
        Me.bRead.Name = "bRead"
        Me.bRead.Size = New System.Drawing.Size(80, 24)
        Me.bRead.TabIndex = 2
        Me.bRead.Text = "Read"
        '
        'cbPageSize
        '
        Me.cbPageSize.Items.AddRange(New Object() {"8-byte", "16-byte", "32-byte", "64-byte", "128-byte"})
        Me.cbPageSize.Location = New System.Drawing.Point(65, 26)
        Me.cbPageSize.Name = "cbPageSize"
        Me.cbPageSize.Size = New System.Drawing.Size(55, 21)
        Me.cbPageSize.TabIndex = 13
        '
        'bSet
        '
        Me.bSet.Location = New System.Drawing.Point(128, 25)
        Me.bSet.Name = "bSet"
        Me.bSet.Size = New System.Drawing.Size(88, 24)
        Me.bSet.TabIndex = 13
        Me.bSet.Text = "Set Page Size"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 16)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Select Reader"
        '
        'mMsg
        '
        Me.mMsg.Location = New System.Drawing.Point(248, 12)
        Me.mMsg.Name = "mMsg"
        Me.mMsg.Size = New System.Drawing.Size(272, 340)
        Me.mMsg.TabIndex = 10
        Me.mMsg.Text = "mMsg"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 16)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Select Card Type"
        '
        'cbCardType
        '
        Me.cbCardType.Items.AddRange(New Object() {"Auto Detect", "1 Kbit", "2 Kbit", "4 Kbit", "8 Kbit", "16 Kbit", "32 Kbit", "64 Kbit", "128 Kbit", "256 Kbit", "512 Kbit", "1024 Kbit"})
        Me.cbCardType.Location = New System.Drawing.Point(124, 80)
        Me.cbCardType.Name = "cbCardType"
        Me.cbCardType.Size = New System.Drawing.Size(112, 21)
        Me.cbCardType.TabIndex = 12
        '
        'frm_Main
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(528, 397)
        Me.Controls.Add(Me.cbCardType)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.mMsg)
        Me.Controls.Add(Me.cbReader)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.fFunction)
        Me.Controls.Add(Me.bQuit)
        Me.Controls.Add(Me.bReset)
        Me.Controls.Add(Me.bConnect)
        Me.Controls.Add(Me.bInit)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frm_Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IIC"
        Me.fFunction.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim retCode, Protocol, hContext, hCard, ReaderCount As Integer
    Dim sReaderList As String
    Dim sReaderGroup As String
    Dim ConnActive As Boolean
    Dim ioRequest As SCARD_IO_REQUEST
    Dim SendLen, RecvLen, nBytesRet As Integer
    Dim SendBuff(262) As Byte
    Dim RecvBuff(262) As Byte

    Const INVALID_SW1SW2 = -450


    Private Sub ClearBuffers()

        Dim indx As Integer

        For indx = 0 To 262
            RecvBuff(indx) = &H0
            SendBuff(indx) = &H0
        Next indx

    End Sub


    Private Sub InitMenu()

        cbReader.Items.Clear()
        bInit.Enabled = True
        bConnect.Enabled = False
        bReset.Enabled = False
        fFunction.Enabled = False
        mMsg.Text = ""
        Call DisplayOut(0, 0, "Program ready")

    End Sub


    Private Sub ClearFields()

        tHiAdd.Text = ""
        tLoAdd.Text = ""
        tLen.Text = ""
        tData.Text = ""

    End Sub


    Private Sub DisplayOut(ByVal mType As Integer, ByVal msgCode As Integer, ByVal PrintText As String)

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


    Public Function GetScardErrMsg(ByVal ReturnCode As Integer) As String

        Select Case ReturnCode
            Case ModWinsCard.SCARD_E_CANCELLED
                GetScardErrMsg = "The action was canceled by an SCardCancel request."
            Case ModWinsCard.SCARD_E_CANT_DISPOSE
                GetScardErrMsg = "The system could not dispose of the media in the requested manner."
            Case ModWinsCard.SCARD_E_CARD_UNSUPPORTED
                GetScardErrMsg = "The smart card does not meet minimal requirements for support."
            Case ModWinsCard.SCARD_E_DUPLICATE_READER
                GetScardErrMsg = "The reader driver didn't produce a unique reader name."
            Case ModWinsCard.SCARD_E_INSUFFICIENT_BUFFER
                GetScardErrMsg = "The data buffer for returned data is too small for the returned data."
            Case ModWinsCard.SCARD_E_INVALID_ATR
                GetScardErrMsg = "An ATR string obtained from the registry is not a valid ATR string."
            Case ModWinsCard.SCARD_E_INVALID_HANDLE
                GetScardErrMsg = "The supplied handle was invalid."
            Case ModWinsCard.SCARD_E_INVALID_PARAMETER
                GetScardErrMsg = "One or more of the supplied parameters could not be properly interpreted."
            Case ModWinsCard.SCARD_E_INVALID_TARGET
                GetScardErrMsg = "Registry startup information is missing or invalid."
            Case ModWinsCard.SCARD_E_INVALID_VALUE
                GetScardErrMsg = "One or more of the supplied parameter values could not be properly interpreted."
            Case ModWinsCard.SCARD_E_NOT_READY
                GetScardErrMsg = "The reader or card is not ready to accept commands."
            Case ModWinsCard.SCARD_E_NOT_TRANSACTED
                GetScardErrMsg = "An attempt was made to end a non-existent transaction."
            Case ModWinsCard.SCARD_E_NO_MEMORY
                GetScardErrMsg = "Not enough memory available to complete this command."
            Case ModWinsCard.SCARD_E_NO_SERVICE
                GetScardErrMsg = "The smart card resource manager is not running."
            Case ModWinsCard.SCARD_E_NO_SMARTCARD
                GetScardErrMsg = "The operation requires a smart card, but no smart card is currently in the device."
            Case ModWinsCard.SCARD_E_PCI_TOO_SMALL
                GetScardErrMsg = "The PCI receive buffer was too small."
            Case ModWinsCard.SCARD_E_PROTO_MISMATCH
                GetScardErrMsg = "The requested protocols are incompatible with the protocol currently in use with the card."
            Case ModWinsCard.SCARD_E_READER_UNAVAILABLE
                GetScardErrMsg = "The specified reader is not currently available for use."
            Case ModWinsCard.SCARD_E_READER_UNSUPPORTED
                GetScardErrMsg = "The reader driver does not meet minimal requirements for support."
            Case ModWinsCard.SCARD_E_SERVICE_STOPPED
                GetScardErrMsg = "The smart card resource manager has shut down."
            Case ModWinsCard.SCARD_E_SHARING_VIOLATION
                GetScardErrMsg = "The smart card cannot be accessed because of other outstanding connections."
            Case ModWinsCard.SCARD_E_SYSTEM_CANCELLED
                GetScardErrMsg = "The action was canceled by the system, presumably to log off or shut down."
            Case ModWinsCard.SCARD_E_TIMEOUT
                GetScardErrMsg = "The user-specified timeout value has expired."
            Case ModWinsCard.SCARD_E_UNKNOWN_CARD
                GetScardErrMsg = "The specified smart card name is not recognized."
            Case ModWinsCard.SCARD_E_UNKNOWN_READER
                GetScardErrMsg = "The specified reader name is not recognized."
            Case ModWinsCard.SCARD_F_COMM_ERROR
                GetScardErrMsg = "An internal communications error has been detected."
            Case ModWinsCard.SCARD_F_INTERNAL_ERROR
                GetScardErrMsg = "An internal consistency check failed."
            Case ModWinsCard.SCARD_F_UNKNOWN_ERROR
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


    Private Function SendAPDUandDisplay(ByVal SendType As Integer, ByVal ApduIn As String) As Integer

        Dim indx As Integer
        Dim tmpStr As String

        ioRequest.dwProtocol = Protocol
        ioRequest.cbPciLength = Len(ioRequest)
        Call DisplayOut(2, 0, ApduIn)
        tmpStr = ""

        retCode = ModWinsCard.SCardTransmit(hCard, _
                                            ioRequest, _
                                            SendBuff(0), _
                                            SendLen, _
                                            ioRequest, _
                                            RecvBuff(0), _
                                            RecvLen)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Call DisplayOut(1, retCode, "")
            SendAPDUandDisplay = retCode
            Exit Function
        Else
            Select Case SendType
                Case 0                  ' Display SW1/SW2 value
                    For indx = RecvLen - 2 To RecvLen - 1
                        tmpStr = tmpStr & String.Format("{0:X2}", (RecvBuff(indx))) & " "
                    Next indx
                Case 1                  ' Display ATR after checking SW1/SW2
                    For indx = RecvLen - 2 To RecvLen - 1
                        tmpStr = tmpStr & String.Format("{0:X2}", (RecvBuff(indx))) & " "
                    Next indx
                    If tmpStr <> "90 00 " Then
                        Call DisplayOut(1, 0, "Return bytes are not acceptable.")
                    Else
                        tmpStr = "ATR: "
                        For indx = 0 To RecvLen - 3
                            tmpStr = tmpStr & String.Format("{0:X2}", (RecvBuff(indx))) & " "
                        Next indx
                    End If
                Case 2                  ' Display all data after checking SW1/SW2
                    For indx = RecvLen - 2 To RecvLen - 1
                        tmpStr = tmpStr & String.Format("{0:X2}", (RecvBuff(indx))) & " "
                    Next indx
                    If tmpStr <> "90 00 " Then
                        Call DisplayOut(1, 0, "Return bytes are not acceptable.")
                    Else
                        tmpStr = ""
                        For indx = 0 To RecvLen - 1
                            tmpStr = tmpStr & String.Format("{0:X2}", (RecvBuff(indx))) & " "
                        Next indx
                    End If
            End Select
            Call DisplayOut(3, 0, tmpStr)
        End If
        SendAPDUandDisplay = retCode

    End Function



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


    Private Sub AddButtons()

        cbReader.Enabled = True
        bInit.Enabled = False
        bConnect.Enabled = True
        bReset.Enabled = True

    End Sub


    Private Function InputOK(ByVal checkType As Integer, ByVal opType As Integer) As Boolean

        InputOK = False

        Select Case checkType
            Case 0              ' for Read function
                If tHiAdd.Text = "" Then
                    tHiAdd.Focus()
                    Exit Function
                End If
                If tLoAdd.Text = "" Then
                    tLoAdd.Focus()
                    Exit Function
                End If
                If tLen.Text = "" Then
                    tLen.Focus()
                    Exit Function
                End If

            Case 1              ' for Write function
                If tHiAdd.Text = "" Then
                    tHiAdd.Focus()
                    Exit Function
                End If
                If tLoAdd.Text = "" Then
                    tLoAdd.Focus()
                    Exit Function
                End If
                If tLen.Text = "" Then
                    tLen.Focus()
                    Exit Function
                End If
                If tData.Text = "" Then
                    tData.Focus()
                    Exit Function
                End If

        End Select

        InputOK = True

    End Function


    Private Sub bInit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bInit.Click

        Dim ctr As Integer

        For ctr = 0 To 255
            sReaderList = sReaderList + vbNullChar
        Next

        ReaderCount = 255

        ' 1. Establish context and obtain hContext handle
        retCode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, hContext)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Call DisplayOut(1, retCode, "")
            Exit Sub
        End If

        ' 2. List PC/SC card readers installed in the system
        retCode = ModWinsCard.SCardListReaders(hContext, sReaderGroup, sReaderList, ReaderCount)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Call DisplayOut(1, retCode, "")
            Exit Sub
        End If
        Call LoadListToControl(cbReader, sReaderList)
        cbReader.SelectedIndex = 0

        Call AddButtons()

    End Sub


    Private Sub bConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bConnect.Click

        Dim cardType As Integer

        If ConnActive Then
            Call DisplayOut(0, 0, "Connection is already active.")
            Exit Sub
        End If

        ' 1. Connect to reader using SCARD_SHARE_SHARED
        retCode = ModWinsCard.SCardConnect(hContext, _
                                           cbReader.Text, _
                                           ModWinsCard.SCARD_SHARE_DIRECT, _
                                           0, _
                                           hCard, _
                                           Protocol)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Call DisplayOut(1, retCode, "")
            ConnActive = False
            Exit Sub
        End If

        ' 2. Select Card Type
        Select Case (cbCardType.SelectedIndex)
            Case 0
                cardType = &H1
            Case 1
                cardType = &H2
            Case 2
                cardType = &H3
            Case 3
                cardType = &H4
            Case 4
                cardType = &H5
            Case 5
                cardType = &H6
            Case 6
                cardType = &H7
            Case 7
                cardType = &H8
            Case 8
                cardType = &H9
            Case 9
                cardType = &HA
            Case 10
                cardType = &HB
            Case 11
                cardType = &HC
        End Select

        Call ClearBuffers()
        SendLen = 4
        SendBuff(0) = cardType
        RecvLen = 262

        retCode = ModWinsCard.SCardControl(hCard, _
                                           ModWinsCard.IOCTL_SMARTCARD_SET_CARD_TYPE, _
                                           SendBuff(0), _
                                           SendLen, _
                                           RecvBuff(0), _
                                           RecvLen, _
                                           nBytesRet)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Call DisplayOut(1, retCode, "")
            ConnActive = False
            Exit Sub
        End If

        ' 3. Reconnect reader
        retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Call DisplayOut(1, retCode, "")
            ConnActive = False
            Exit Sub
        End If

        retCode = ModWinsCard.SCardConnect(hContext, _
                                           cbReader.Text, _
                                           ModWinsCard.SCARD_SHARE_SHARED, _
                                           ModWinsCard.SCARD_PROTOCOL_T0 Or ModWinsCard.SCARD_PROTOCOL_T1, _
                                           hCard, _
                                           Protocol)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Call DisplayOut(1, retCode, "")
            ConnActive = False
            Exit Sub
        Else
            Call DisplayOut(0, 0, "Successful connection to " & cbReader.Text)
        End If

        ConnActive = True
        fFunction.Enabled = True

        If cbCardType.SelectedIndex = 11 Then
            tBitAdd.Enabled = True
        Else
            tBitAdd.Enabled = False
        End If

    End Sub


    Private Sub bRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bRead.Click

        Dim indx As Integer
        Dim tmpStr As String

        ' 1. Check for all input fields
        If cbCardType.SelectedIndex = 11 Then
            indx = 1
        Else
            indx = 0
        End If
        If Not InputOK(indx, 0) Then
            Exit Sub
        End If

        ' 2. Read input fields and pass data to card
        tData.Text = ""

        Call ClearBuffers()

        SendBuff(0) = &HFF
        If ((cbCardType.SelectedIndex = 11) And (tBitAdd.Text = "1")) Then
            SendBuff(1) = &HB1
        Else
            SendBuff(1) = &HB0
        End If
        SendBuff(2) = CInt("&H" & Mid(tHiAdd.Text, 1, 2))
        SendBuff(3) = CInt("&H" & Mid(tLoAdd.Text, 1, 2))
        SendBuff(4) = CInt("&H" & Mid(tLen.Text, 1, 2))

        SendLen = 5

        RecvLen = SendBuff(4) + 2
        tmpStr = ""

        For indx = 0 To SendLen - 1
            tmpStr = tmpStr & String.Format("{0:X2}", SendBuff(indx)) & " "
        Next indx

        retCode = SendAPDUandDisplay(2, tmpStr)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        ' 3. Display data read from card into Data textbox
        tmpStr = ""

        For indx = 0 To SendBuff(4) - 1
            tmpStr = tmpStr & Chr(RecvBuff(indx))
        Next indx

        tData.Text = tmpStr

    End Sub


    Private Sub bWrite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bWrite.Click

        Dim indx As Integer
        Dim tmpStr As String

        ' 1. Check for all input fields
        If cbCardType.SelectedIndex = 11 Then
            indx = 1
        Else
            indx = 0
        End If
        If Not InputOK(indx, 1) Then
            Exit Sub
        End If

        ' 2. Read input fields and pass data to card
        tmpStr = tData.Text

        Call ClearBuffers()

        SendBuff(0) = &HFF
        If ((cbCardType.SelectedIndex = 11) And (tBitAdd.Text = "1")) Then
            SendBuff(1) = &HD1
        Else
            SendBuff(1) = &HD0
        End If
        SendBuff(2) = CInt("&H" & Mid(tHiAdd.Text, 1, 2))
        SendBuff(3) = CInt("&H" & Mid(tLoAdd.Text, 1, 2))
        SendBuff(4) = CInt("&H" & Mid(tLen.Text, 1, 2))

        For indx = 0 To Len(tmpStr) - 1
            SendBuff(indx + 5) = Asc(Mid(tmpStr, indx + 1, 1))
        Next indx

        SendLen = SendBuff(4) + 5
        RecvLen = 2
        tmpStr = ""

        For indx = 0 To SendLen - 1
            tmpStr = tmpStr & String.Format("{0:X2}", SendBuff(indx)) & " "
        Next indx

        retCode = SendAPDUandDisplay(0, tmpStr)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        tData.Text = ""

    End Sub

    Private Sub bReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bReset.Click

        Call ClearFields()

        If ConnActive Then
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
            ConnActive = False
        End If

        retCode = ModWinsCard.SCardReleaseContext(hContext)

        Call InitMenu()

    End Sub


    Private Sub bQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bQuit.Click

        If ConnActive Then
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
            ConnActive = False
        End If

        retCode = ModWinsCard.SCardReleaseContext(hContext)
        Me.Close()

    End Sub


    Private Sub frm_Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call InitMenu()

    End Sub


    Private Sub cbReader_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbReader.Click

        fFunction.Enabled = False
        Call ClearFields()
        If ConnActive Then
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
            ConnActive = False
        End If

    End Sub


    Private Sub tBitAdd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tBitAdd.KeyPress

        'Verify Input
        e.Handled = KeyVerify(Asc(e.KeyChar))

    End Sub


    Private Sub tHiAdd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tHiAdd.KeyPress

        'Verify Input
        e.Handled = KeyVerify(Asc(e.KeyChar))

    End Sub


    Private Sub tHiAdd_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tHiAdd.KeyUp

        If Len(tHiAdd.Text) > 1 Then
            tLoAdd.Focus()
        End If

    End Sub


    Private Sub tLen_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tLen.KeyPress

        'Verify Input
        e.Handled = KeyVerify(Asc(e.KeyChar))

    End Sub


    Private Sub tLen_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tLen.KeyUp

        If Len(tLen.Text) > 1 Then
            tData.Focus()
        End If

    End Sub


    Private Sub tLen_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tLen.LostFocus

        If tLen.Text <> "" Then
            tData.Text = ""
            tData.MaxLength = CInt("&H" & Mid(tLen.Text, 1, 2))
        End If

    End Sub


    Private Sub tLoAdd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tLoAdd.KeyPress

        'Verify Input
        e.Handled = KeyVerify(Asc(e.KeyChar))

    End Sub


    Private Sub tLoAdd_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tLoAdd.KeyUp

        If Len(tLoAdd.Text) > 1 Then
            tLen.Focus()
        End If

    End Sub


    Public Function KeyVerify(ByVal key As Integer) As Boolean

        '=========================================
        '  Routine for Verifying the Inputed  
        '  Character 0-9 and A-F
        '=========================================

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


    Private Sub bSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSet.Click

        Dim indx As Integer
        Dim tmpStr As String

        Call ClearBuffers()
        SendBuff(0) = &HFF
        SendBuff(1) = &H1
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H1

        Select Case cbPageSize.SelectedIndex
            Case 0
                SendBuff(5) = &H3
            Case 1
                SendBuff(5) = &H4
            Case 2
                SendBuff(5) = &H5
            Case 3
                SendBuff(5) = &H6
            Case 4
                SendBuff(5) = &H7
        End Select

        tmpStr = ""
        SendLen = 6
        RecvLen = 2

        For indx = 0 To SendLen - 1
            tmpStr = tmpStr & String.Format("{0:X2}", SendBuff(indx)) & " "
        Next indx

        retCode = SendAPDUandDisplay(0, tmpStr)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

    End Sub


    Private Sub cbCardType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbCardType.Click

        fFunction.Enabled = False
        Call ClearFields()
        If ConnActive Then
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
            ConnActive = False
        End If

    End Sub

End Class
