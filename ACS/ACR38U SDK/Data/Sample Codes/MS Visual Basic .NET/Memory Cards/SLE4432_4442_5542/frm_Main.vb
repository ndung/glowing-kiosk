'==================================================================================================================
'
'  Copyright(C):      Advanced Card Systems Ltd
'
'  Project Name:      SLE4432_4442_5542
'
'  Description:       This sample program outlines the steps on how to
'                     program SLE4432/4442 memory cards using ACS readers
'                     in PC/SC platform.
'
'  Author:            Aileen Grace L. Sarte
'
'  Date:              February 20, 2007
'
'  Revision Trail:   (Date/Author/Description)
' 
'
'==================================================================================================================

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
    Friend WithEvents rb_SLE4442 As System.Windows.Forms.RadioButton
    Friend WithEvents rb_SLE4432 As System.Windows.Forms.RadioButton
    Friend WithEvents mMsg As System.Windows.Forms.RichTextBox
    Friend WithEvents cbReader As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tData As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tLen As System.Windows.Forms.TextBox
    Friend WithEvents bQuit As System.Windows.Forms.Button
    Friend WithEvents bReset As System.Windows.Forms.Button
    Friend WithEvents bConnect As System.Windows.Forms.Button
    Friend WithEvents bInit As System.Windows.Forms.Button
    Friend WithEvents tAdd As System.Windows.Forms.TextBox
    Friend WithEvents bChange As System.Windows.Forms.Button
    Friend WithEvents bWrite As System.Windows.Forms.Button
    Friend WithEvents bSubmit As System.Windows.Forms.Button
    Friend WithEvents bWriteProt As System.Windows.Forms.Button
    Friend WithEvents bRead As System.Windows.Forms.Button
    Friend WithEvents fCardType As System.Windows.Forms.GroupBox
    Friend WithEvents fFunction As System.Windows.Forms.GroupBox
    Friend WithEvents bErrCtr As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Main))
        Me.fCardType = New System.Windows.Forms.GroupBox
        Me.rb_SLE4442 = New System.Windows.Forms.RadioButton
        Me.rb_SLE4432 = New System.Windows.Forms.RadioButton
        Me.mMsg = New System.Windows.Forms.RichTextBox
        Me.cbReader = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.fFunction = New System.Windows.Forms.GroupBox
        Me.bErrCtr = New System.Windows.Forms.Button
        Me.bChange = New System.Windows.Forms.Button
        Me.bWrite = New System.Windows.Forms.Button
        Me.bSubmit = New System.Windows.Forms.Button
        Me.bWriteProt = New System.Windows.Forms.Button
        Me.tData = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.tLen = New System.Windows.Forms.TextBox
        Me.tAdd = New System.Windows.Forms.TextBox
        Me.bRead = New System.Windows.Forms.Button
        Me.bQuit = New System.Windows.Forms.Button
        Me.bReset = New System.Windows.Forms.Button
        Me.bConnect = New System.Windows.Forms.Button
        Me.bInit = New System.Windows.Forms.Button
        Me.fCardType.SuspendLayout()
        Me.fFunction.SuspendLayout()
        Me.SuspendLayout()
        '
        'fCardType
        '
        Me.fCardType.Controls.Add(Me.rb_SLE4442)
        Me.fCardType.Controls.Add(Me.rb_SLE4432)
        Me.fCardType.Location = New System.Drawing.Point(11, 43)
        Me.fCardType.Name = "fCardType"
        Me.fCardType.Size = New System.Drawing.Size(116, 75)
        Me.fCardType.TabIndex = 30
        Me.fCardType.TabStop = False
        Me.fCardType.Text = "Card Type"
        '
        'rb_SLE4442
        '
        Me.rb_SLE4442.Location = New System.Drawing.Point(8, 48)
        Me.rb_SLE4442.Name = "rb_SLE4442"
        Me.rb_SLE4442.Size = New System.Drawing.Size(104, 16)
        Me.rb_SLE4442.TabIndex = 1
        Me.rb_SLE4442.Text = "SLE 4442/5542"
        '
        'rb_SLE4432
        '
        Me.rb_SLE4432.Location = New System.Drawing.Point(8, 16)
        Me.rb_SLE4432.Name = "rb_SLE4432"
        Me.rb_SLE4432.Size = New System.Drawing.Size(72, 24)
        Me.rb_SLE4432.TabIndex = 0
        Me.rb_SLE4432.Text = "SLE 4432"
        '
        'mMsg
        '
        Me.mMsg.Location = New System.Drawing.Point(246, 12)
        Me.mMsg.Name = "mMsg"
        Me.mMsg.Size = New System.Drawing.Size(272, 316)
        Me.mMsg.TabIndex = 29
        Me.mMsg.Text = ""
        '
        'cbReader
        '
        Me.cbReader.Location = New System.Drawing.Point(114, 13)
        Me.cbReader.Name = "cbReader"
        Me.cbReader.Size = New System.Drawing.Size(121, 21)
        Me.cbReader.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 15)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Select Reader"
        '
        'fFunction
        '
        Me.fFunction.Controls.Add(Me.bErrCtr)
        Me.fFunction.Controls.Add(Me.bChange)
        Me.fFunction.Controls.Add(Me.bWrite)
        Me.fFunction.Controls.Add(Me.bSubmit)
        Me.fFunction.Controls.Add(Me.bWriteProt)
        Me.fFunction.Controls.Add(Me.tData)
        Me.fFunction.Controls.Add(Me.Label6)
        Me.fFunction.Controls.Add(Me.Label5)
        Me.fFunction.Controls.Add(Me.Label4)
        Me.fFunction.Controls.Add(Me.tLen)
        Me.fFunction.Controls.Add(Me.tAdd)
        Me.fFunction.Controls.Add(Me.bRead)
        Me.fFunction.Location = New System.Drawing.Point(10, 128)
        Me.fFunction.Name = "fFunction"
        Me.fFunction.Size = New System.Drawing.Size(224, 232)
        Me.fFunction.TabIndex = 27
        Me.fFunction.TabStop = False
        Me.fFunction.Text = "Functions"
        '
        'bErrCtr
        '
        Me.bErrCtr.Enabled = False
        Me.bErrCtr.Location = New System.Drawing.Point(8, 104)
        Me.bErrCtr.Name = "bErrCtr"
        Me.bErrCtr.Size = New System.Drawing.Size(100, 32)
        Me.bErrCtr.TabIndex = 19
        Me.bErrCtr.Text = "Read Err Ctr"
        '
        'bChange
        '
        Me.bChange.Enabled = False
        Me.bChange.Location = New System.Drawing.Point(115, 184)
        Me.bChange.Name = "bChange"
        Me.bChange.Size = New System.Drawing.Size(100, 32)
        Me.bChange.TabIndex = 18
        Me.bChange.Text = "Change Code"
        '
        'bWrite
        '
        Me.bWrite.Location = New System.Drawing.Point(115, 144)
        Me.bWrite.Name = "bWrite"
        Me.bWrite.Size = New System.Drawing.Size(100, 32)
        Me.bWrite.TabIndex = 17
        Me.bWrite.Text = "Write"
        '
        'bSubmit
        '
        Me.bSubmit.Enabled = False
        Me.bSubmit.Location = New System.Drawing.Point(8, 184)
        Me.bSubmit.Name = "bSubmit"
        Me.bSubmit.Size = New System.Drawing.Size(100, 32)
        Me.bSubmit.TabIndex = 16
        Me.bSubmit.Text = "Submit Code"
        '
        'bWriteProt
        '
        Me.bWriteProt.Location = New System.Drawing.Point(8, 144)
        Me.bWriteProt.Name = "bWriteProt"
        Me.bWriteProt.Size = New System.Drawing.Size(100, 32)
        Me.bWriteProt.TabIndex = 15
        Me.bWriteProt.Text = "Write Protect"
        '
        'tData
        '
        Me.tData.Location = New System.Drawing.Point(10, 73)
        Me.tData.MaxLength = 0
        Me.tData.Name = "tData"
        Me.tData.Size = New System.Drawing.Size(200, 20)
        Me.tData.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(13, 53)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 16)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Data (string)"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(128, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 16)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Length"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(14, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 16)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Address"
        '
        'tLen
        '
        Me.tLen.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tLen.Location = New System.Drawing.Point(176, 22)
        Me.tLen.MaxLength = 2
        Me.tLen.Name = "tLen"
        Me.tLen.Size = New System.Drawing.Size(32, 20)
        Me.tLen.TabIndex = 7
        '
        'tAdd
        '
        Me.tAdd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tAdd.Location = New System.Drawing.Point(67, 22)
        Me.tAdd.MaxLength = 2
        Me.tAdd.Name = "tAdd"
        Me.tAdd.Size = New System.Drawing.Size(32, 20)
        Me.tAdd.TabIndex = 4
        '
        'bRead
        '
        Me.bRead.Location = New System.Drawing.Point(116, 104)
        Me.bRead.Name = "bRead"
        Me.bRead.Size = New System.Drawing.Size(100, 32)
        Me.bRead.TabIndex = 3
        Me.bRead.Text = "Read"
        '
        'bQuit
        '
        Me.bQuit.Location = New System.Drawing.Point(404, 336)
        Me.bQuit.Name = "bQuit"
        Me.bQuit.Size = New System.Drawing.Size(96, 24)
        Me.bQuit.TabIndex = 26
        Me.bQuit.Text = "Quit"
        '
        'bReset
        '
        Me.bReset.Location = New System.Drawing.Point(268, 336)
        Me.bReset.Name = "bReset"
        Me.bReset.Size = New System.Drawing.Size(96, 24)
        Me.bReset.TabIndex = 25
        Me.bReset.Text = "Reset"
        '
        'bConnect
        '
        Me.bConnect.Location = New System.Drawing.Point(136, 80)
        Me.bConnect.Name = "bConnect"
        Me.bConnect.Size = New System.Drawing.Size(96, 24)
        Me.bConnect.TabIndex = 24
        Me.bConnect.Text = "Connect"
        '
        'bInit
        '
        Me.bInit.Location = New System.Drawing.Point(136, 48)
        Me.bInit.Name = "bInit"
        Me.bInit.Size = New System.Drawing.Size(96, 24)
        Me.bInit.TabIndex = 23
        Me.bInit.Text = "Initialize"
        '
        'frm_Main
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(528, 373)
        Me.Controls.Add(Me.fCardType)
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
        Me.Text = "Using SLE4432/4442/5542 in PC/SC"
        Me.fCardType.ResumeLayout(False)
        Me.fFunction.ResumeLayout(False)
        Me.fFunction.PerformLayout()
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
        fCardType.Enabled = False
        bConnect.Enabled = False
        bReset.Enabled = False
        fFunction.Enabled = False
        mMsg.Text = ""
        Call DisplayOut(0, 0, "Program ready")

    End Sub


    Private Sub ClearFields()

        tAdd.Text = ""
        tLen.Text = ""
        tData.Text = ""

    End Sub


    Private Sub DisplayOut(ByVal mType As Integer, ByVal msgCode As Integer, ByVal PrintText As String)

        Select Case mType
            Case 0                                  ' Notifications only
                mMsg.SelectionColor = Color.Green
            Case 1                                  ' Error Messages
                mMsg.SelectionColor = Color.Red
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


    Private Sub AddButtons()

        cbReader.Enabled = True
        bInit.Enabled = False
        fCardType.Enabled = True
        rb_SLE4432.Checked = True
        bConnect.Enabled = True
        bReset.Enabled = True

    End Sub


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
                        tmpStr = tmpStr & String.Format("{0:X2}", RecvBuff(indx)) & " "
                    Next indx
                Case 1                  ' Display ATR after checking SW1/SW2
                    For indx = RecvLen - 2 To RecvLen - 1
                        tmpStr = tmpStr & String.Format("{0:X2}", RecvBuff(indx)) & " "
                    Next indx
                    If tmpStr <> "90 00 " Then
                        Call DisplayOut(1, 0, "Return bytes are not acceptable.")
                    Else
                        tmpStr = "ATR: "
                        For indx = 0 To RecvLen - 3
                            tmpStr = tmpStr & String.Format("{0:X2}", RecvBuff(indx)) & " "
                        Next indx
                    End If
                Case 2                  ' Display all data after checking SW1/SW2
                    For indx = RecvLen - 2 To RecvLen - 1
                        tmpStr = tmpStr & String.Format("{0:X2}", RecvBuff(indx)) & " "
                    Next indx
                    If tmpStr <> "90 00 " Then
                        Call DisplayOut(1, 0, "Return bytes are not acceptable.")
                    Else
                        tmpStr = ""
                        For indx = 0 To RecvLen - 1
                            tmpStr = tmpStr & String.Format("{0:X2}", RecvBuff(indx)) & " "
                        Next indx
                    End If
            End Select
            Call DisplayOut(3, 0, tmpStr)
        End If

        SendAPDUandDisplay = retCode

    End Function


    Private Function InputOK(ByVal checkType As Integer) As Boolean

        Dim tmpStr As String
        Dim indx As Integer

        InputOK = False
        Select Case checkType
            Case 0              ' for Read function
                If tAdd.Text = "" Then
                    tAdd.Focus()
                    Exit Function
                End If
                If tLen.Text = "" Then
                    tLen.Focus()
                    Exit Function
                End If
            Case 1              ' for Write function
                If tAdd.Text = "" Then
                    tAdd.Focus()
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
            Case 2              ' for Verify function
                tAdd.Text = ""
                tLen.Text = ""
                If tData.Text = "" Then
                    tData.Focus()
                    Exit Function
                End If
                tData.Text = UCase(tData.Text)
                tmpStr = ""
                For indx = 0 To Len(tData.Text)
                    If Mid(tData.Text, indx + 1, 1) <> " " Then
                        tmpStr = tmpStr & Mid(tData.Text, indx + 1, 1)
                    End If
                Next indx
                If Len(tmpStr) <> 6 Then
                    tData.SelectionStart = 0
                    tData.SelectionLength = Len(tData.Text)
                    tData.Focus()
                    Exit Function
                End If
                For indx = 0 To Len(tmpStr) - 1
                    If Asc(Mid(tmpStr, indx + 1, 1)) < 48 Or Asc(Mid(tmpStr, indx + 1, 1)) > 57 Then
                        If Asc(Mid(tmpStr, indx + 1, 1)) < 65 Or Asc(Mid(tmpStr, indx + 1, 1)) > 70 Then
                            tData.SelectionStart = 0
                            tData.SelectionLength = Len(tData.Text)
                            tData.Focus()
                            Exit Function
                        End If
                    End If
                Next indx
        End Select
        InputOK = True

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

        Dim i As Integer
        Dim tempstr As String

        If ConnActive Then
            Call DisplayOut(0, 0, "Connection is already active.")
            Exit Sub
        End If

        ' 1. Connect to reader using SCARD_SHARE_DIRECT
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

        Call ClearBuffers()
        'Select CardType
        SendBuff(0) = &HFF
        SendBuff(1) = &HA4
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H1
        SendBuff(5) = &H6

        SendLen = 6
        RecvLen = 255

        tempstr = ""
        For i = 0 To SendLen - 1
            tempstr = tempstr & String.Format("{0:X2}", SendBuff(i)) & " "
        Next i
        retCode = SendAPDUandDisplay(2, tempstr)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        ConnActive = True
        fFunction.Enabled = True
        ClearFields()

    End Sub


    Private Sub frm_Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call InitMenu()

    End Sub


    Private Sub bWriteProt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bWriteProt.Click

        Dim indx As Integer
        Dim tmpStr As String

        ' 1. Check for all input fields
        If Not InputOK(3) Then
            Exit Sub
        End If

        ' 2. Read input fields and pass data to card
        tmpStr = tData.Text

        Call ClearBuffers()

        SendBuff(0) = &HFF
        SendBuff(1) = &HD1
        SendBuff(2) = &H0
        SendBuff(3) = CInt("&H" & Mid(tAdd.Text, 1, 2))
        SendBuff(4) = CInt("&H" & Mid(tLen.Text, 1, 2))

        For indx = 0 To Len(tmpStr) - 1
            If Asc(Mid(tmpStr, indx + 1, 1)) <> &H0 Then
                SendBuff(indx + 5) = Asc(Mid(tmpStr, indx + 1, 1))
            End If
        Next indx

        SendLen = SendBuff(4) + 5
        RecvLen = 2
        tmpStr = ""

        For indx = 0 To SendLen - 1
            tmpStr = tmpStr & String.Format("{0:X2}", SendBuff(indx)) & " "
        Next indx

        retCode = SendAPDUandDisplay(2, tmpStr)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        tData.Text = ""

    End Sub


    Private Sub bSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSubmit.Click

        Dim indx As Integer
        Dim tmpStr As String

        ' 1. Check for all input fields
        If Not InputOK(2) Then
            Exit Sub
        End If

        ' 2. Read input fields and pass data to card
        tmpStr = ""
        For indx = 0 To Len(tData.Text)
            If Mid(tData.Text, indx + 1, 1) <> " " Then
                tmpStr = tmpStr & Mid(tData.Text, indx + 1, 1)
            End If
        Next indx

        Call ClearBuffers()

        SendBuff(0) = &HFF
        SendBuff(1) = &H20
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H3

        For indx = 0 To 2
            SendBuff(indx + 5) = CInt("&H" & Mid(tmpStr, (indx * 2) + 1, 2))
        Next indx

        SendLen = SendBuff(4) + 5
        RecvLen = 6
        tmpStr = ""

        For indx = 0 To SendLen - 1
            tmpStr = tmpStr & String.Format("{0:X2}", SendBuff(indx)) & " "
        Next indx

        retCode = SendAPDUandDisplay(2, tmpStr)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        tData.Text = ""

    End Sub

    Private Sub bRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bRead.Click

        Dim indx As Integer
        Dim tmpStr As String

        ' 1. Check for all input fields
        If Not InputOK(0) Then
            Exit Sub
        End If

        ' 2. Read input fields and pass data to card
        tData.Text = ""

        Call ClearBuffers()
        SendBuff(0) = &HFF
        SendBuff(1) = &HB0
        SendBuff(2) = &H0
        SendBuff(3) = CInt("&H" & Mid(tAdd.Text, 1, 2))
        SendBuff(4) = CInt("&H" & Mid(tLen.Text, 1, 2))

        SendLen = 5
        RecvLen = SendBuff(4) + 6
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
        If Not InputOK(1) Then
            Exit Sub
        End If

        ' 2. Read input fields and pass data to card
        tmpStr = tData.Text

        Call ClearBuffers()

        SendBuff(0) = &HFF
        SendBuff(1) = &HD0
        SendBuff(2) = &H0
        SendBuff(3) = CInt("&H" & Mid(tAdd.Text, 1, 2))
        SendBuff(4) = CInt("&H" & Mid(tLen.Text, 1, 2))

        For indx = 0 To Len(tmpStr) - 1
            If Asc(Mid(tmpStr, indx + 1, 1)) <> &H0 Then
                SendBuff(indx + 5) = Asc(Mid(tmpStr, indx + 1, 1))
            End If
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


    Private Sub bChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bChange.Click

        Dim indx As Integer
        Dim tmpStr As String

        ' 1. Check for all input fields
        If Not InputOK(2) Then
            Exit Sub
        End If

        ' 2. Read input fields and pass data to card
        tmpStr = ""

        For indx = 0 To Len(tData.Text)
            If Mid(tData.Text, indx + 1, 1) <> " " Then
                tmpStr = tmpStr & Mid(tData.Text, indx + 1, 1)
            End If
        Next indx

        Call ClearBuffers()

        SendBuff(0) = &HFF
        SendBuff(1) = &HD2
        SendBuff(2) = &H0
        SendBuff(3) = &H1
        SendBuff(4) = &H3

        For indx = 0 To 2
            SendBuff(indx + 5) = CInt("&H" & Mid(tmpStr, (indx * 2) + 1, 2))
        Next indx

        SendLen = SendBuff(4) + 5
        RecvLen = 2
        tmpStr = ""

        For indx = 0 To SendLen - 1
            tmpStr = tmpStr & String.Format("{0:X2}", SendBuff(indx)) & " "
        Next indx

        retCode = SendAPDUandDisplay(2, tmpStr)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        tData.Text = ""

    End Sub


    Private Sub bReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bReset.Click

        rb_SLE4432.Checked = False
        rb_SLE4442.Checked = False
        bSubmit.Enabled = True

        Call ClearFields()

        If ConnActive Then
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
            ConnActive = False
        End If

        retCode = ModWinsCard.SCardReleaseContext(hContext)

        Call InitMenu()

    End Sub


    Private Sub rb_SLE4432_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rb_SLE4432.Click

        fFunction.Enabled = False

        Call ClearFields()

        If ConnActive Then
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
            ConnActive = False
        End If

        bSubmit.Enabled = False
        bChange.Enabled = False
        bErrCtr.Enabled = False

    End Sub


    Private Sub rb_SLE4442_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rb_SLE4442.Click

        fFunction.Enabled = False

        Call ClearFields()

        If ConnActive Then
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
            ConnActive = False
        End If

        bSubmit.Enabled = True
        bChange.Enabled = True
        bErrCtr.Enabled = True

    End Sub


    Private Sub tAdd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tAdd.TextChanged

        If Len(tAdd.Text) > 1 Then
            tLen.Focus()
        End If

    End Sub


    Private Sub tAdd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tAdd.KeyPress

        'Verify Input
        e.Handled = KeyVerify(Asc(e.KeyChar))

    End Sub


    Private Sub tLen_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tLen.KeyPress

        'Verify Input
        e.Handled = KeyVerify(Asc(e.KeyChar))

    End Sub


    Private Sub tLen_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tLen.LostFocus

        If tLen.Text <> "" Then
            tData.Text = ""
            tData.MaxLength = CInt("&H" & Mid(tLen.Text, 1, 2))
        Else
            tData.MaxLength = 0
        End If

    End Sub


    Private Sub tLen_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tLen.KeyUp

        If Len(tLen.Text) > 1 Then
            tData.Focus()
        End If

    End Sub


    Private Sub bQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bQuit.Click

        If ConnActive Then
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
            ConnActive = False
        End If
        retCode = ModWinsCard.SCardReleaseContext(hContext)
        Me.Close()

    End Sub


    Private Sub cbReader_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbReader.Click

        fFunction.Enabled = False
        Call ClearFields()
        If ConnActive Then
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
            ConnActive = False
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


    Private Sub bErrCtr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bErrCtr.Click

        Dim indx As Integer
        Dim tmpStr As String

        ' 1. Clear all input fields
        Call ClearFields()

        ' 2. Read input fields and pass data to card
        Call ClearBuffers()

        SendBuff(0) = &HFF
        SendBuff(1) = &HB1
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H4

        SendLen = 5
        RecvLen = 6
        tmpStr = ""

        For indx = 0 To SendLen - 1
            tmpStr = tmpStr & String.Format("{0:X2}", SendBuff(indx)) & " "
        Next indx

        retCode = SendAPDUandDisplay(2, tmpStr)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

    End Sub

End Class
