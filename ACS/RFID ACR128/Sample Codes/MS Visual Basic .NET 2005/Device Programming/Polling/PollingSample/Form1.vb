'=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                   execute card detection polling functions using ACR128
'  
'  Author :         Daryl M. Rojas
'
'  Module :         ModWinscard.vb
'   
'  Date   :         June 6, 2008
'
' Revision Trail:   (Date/Author/Description) 
'
'==========================================================================================

Public Class PollingSample

    Public retCode, hContext, hCard, Protocol, pollCase As Long
    Public connActive As Boolean = False
    Public autoDet, dualPoll As Boolean
    Public SendBuff(262), RecvBuff(262) As Byte
    Public SendLen, RecvLen, nBytesRet As Integer
    Public RdrState As SCARD_READERSTATE
    Public ioRequet As SCARD_IO_REQUEST

    Private Sub PollingSample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call InitMenu()

    End Sub

    Private Sub InitMenu()

        connActive = False
        autoDet = False
        dualPoll = False
        bConnect.Enabled = False
        bReset.Enabled = False
        bInit.Enabled = True
        gbExMode.Enabled = False
        gbCurrMode.Enabled = False
        bSetExMode.Enabled = False
        bGetExMode.Enabled = False
        gbPollOpt.Enabled = False
        cbPollOpt1.Enabled = True
        cbPollOpt2.Enabled = True
        cbPollOpt3.Enabled = True
        cbPollOpt4.Enabled = True
        cbPollOpt5.Enabled = True
        cbPollOpt6.Enabled = True
        gbPICCInt.Enabled = False
        bReadPollOpt.Enabled = False
        bSetPollOpt.Enabled = False
        bManPoll.Enabled = False
        bAutoPoll.Enabled = False
        Call displayOut(0, 0, "Program ready")

    End Sub

    Private Sub ClearBuffers()

        Dim indx As Long

        For indx = 0 To 262
            RecvBuff(indx) = &H0
            SendBuff(indx) = &H0
        Next indx

    End Sub

    Private Sub displayOut(ByVal errType As Integer, ByVal retVal As Integer, ByVal PrintText As String)

        Select Case errType

            Case 0                                          ' Notifications only
                mMsg.Text = PrintText
                mMsg.Items.Add(PrintText)
            Case 1                                          ' Error Messages
                PrintText = ModWinsCard.GetScardErrMsg(retVal)
                mMsg.Items.Add(PrintText)
            Case 2                                          ' Input data
                PrintText = "< " + PrintText
                mMsg.Items.Add(PrintText)
            Case 3                                          ' Output data
                PrintText = "> " + PrintText
                mMsg.Items.Add(PrintText)
            Case 4                                          ' For ACOS1 error
                mMsg.Text = PrintText
            Case 5                                          ' ICC Polling Status
                tsMsg2.Text = PrintText
            Case 6
                tsMsg4.Text = PrintText                     ' PICC Polling Status

        End Select

        mMsg.ForeColor = Color.Black
        mMsg.Focus()

    End Sub

    Private Sub EnableButtons(ByVal reqType As Integer)

        Select Case reqType

            Case 0
                bInit.Enabled = False
                bConnect.Enabled = True
                bReset.Enabled = True

            Case 1
                gbExMode.Enabled = True
                gbCurrMode.Enabled = True
                bSetExMode.Enabled = True
                bGetExMode.Enabled = True
                gbPollOpt.Enabled = True
                gbPICCInt.Enabled = True
                bReadPollOpt.Enabled = True
                bSetPollOpt.Enabled = True
                bAutoPoll.Enabled = True
                bManPoll.Enabled = True

        End Select

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

    Private Sub bInit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bInit.Click

        Dim sReaderList As String
        Dim ReaderCount As Integer
        Dim ctr, indx As Integer

        For ctr = 0 To 255
            sReaderList = sReaderList + vbNullChar
        Next

        ReaderCount = 255

        ' 1. Establish context and obtain hContext handle
        retCode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, hContext)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Call displayOut(1, retCode, "")

            Exit Sub

        End If

        ' 2. List PC/SC card readers installed in the system
        retCode = ModWinsCard.SCardListReaders(hContext, "", sReaderList, ReaderCount)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Call displayOut(1, retCode, "")

            Exit Sub

        End If

        ' Load Available Readers
        Call LoadListToControl(cbReader, sReaderList)
        cbReader.SelectedIndex = 0
        Call EnableButtons(0)

        ' Look for ACR128 SAM and make it the default reader in the combobox
        For indx = 0 To cbReader.Items.Count - 1

            cbReader.SelectedIndex = indx

            If cbReader.SelectedIndex.Equals(3) Then


                Exit Sub

            End If

        Next indx

        Exit Sub

    End Sub

    Private Sub bConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bConnect.Click

        Call CallCardConnect(1)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        connActive = True
        Call EnableButtons(1)

    End Sub

    Private Function CallCardConnect(ByVal reqType As Integer) As Integer

        Dim intIndx As Integer

        If connActive Then

            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)

        End If

        ' Shared Connection
        retCode = ModWinsCard.SCardConnect(hContext, _
                            cbReader.Text, _
                            ModWinsCard.SCARD_SHARE_SHARED, _
                            ModWinsCard.SCARD_PROTOCOL_T0 Or ModWinsCard.SCARD_PROTOCOL_T1, _
                            hCard, _
                            Protocol)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            ' Connect to SAM Reader
            If reqType = 1 Then

                ' check if ACR128 SAM is used and use Direct Mode if SAM is not detected
                intIndx = 0

                cbReader.SelectedIndex = intIndx

                While cbReader.SelectedIndex.Equals(0)

                    If intIndx = cbReader.Items.Count Then

                        Call displayOut(0, 0, "Cannot find ACR128 SAM reader.")

                    End If

                    intIndx = intIndx + 2
                    cbReader.SelectedIndex = intIndx

                End While

                ' Direct Connection
                retCode = ModWinsCard.SCardConnect(hContext, _
                            cbReader.Text, _
                            ModWinsCard.SCARD_SHARE_DIRECT, _
                            0, _
                            hCard, _
                            Protocol)

                If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

                    Call displayOut(1, retCode, "")
                    connActive = False
                    CallCardConnect = retCode
                    Exit Function

                Else

                    Call displayOut(0, 0, "Successful connection to " & cbReader.Text)

                End If

            Else

                Call displayOut(1, retCode, "")
                connActive = False
                CallCardConnect = retCode
                Exit Function

            End If

        Else

            Call displayOut(0, 0, "Successful connection to " & cbReader.Text)
            CallCardConnect = retCode

        End If

    End Function

    Private Function CallCardControl() As Integer

        Dim tmpStr As String
        Dim indx As Integer

        ' Display Apdu In
        tmpStr = "SCardControl: "
        For indx = 0 To SendLen

            tmpStr = tmpStr + Format(Hex(SendBuff(indx)))

        Next indx

        Call displayOut(2, 0, tmpStr)

        retCode = ModWinsCard.SCardControl(hCard, ModWinsCard.IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND, SendBuff(0), SendLen, RecvBuff(0), RecvLen, nBytesRet)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Call displayOut(1, retCode, "")

        Else

            tmpStr = ""

            For indx = 0 To RecvLen - 1

                tmpStr = tmpStr + Format(Hex(RecvBuff(indx)))

            Next indx

            Call displayOut(3, 0, tmpStr)

        End If

        CallCardControl = retCode

    End Function

    Private Sub GetExMode(ByVal reqType As Integer)

        Dim indx As Integer
        Dim tmpStr As String

        Call ClearBuffers()

        SendBuff(0) = &H2B
        SendBuff(1) = &H0
        SendLen = 2
        RecvLen = 7
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        ' Interpret Configuration Setting and Current Mode
        tmpStr = ""

        For indx = 0 To RecvLen - 1

            tmpStr = tmpStr + Format(Hex(RecvBuff(indx)))

        Next indx

        If tmpStr.Equals("E1000201") Or tmpStr.Equals("E1000211") Or tmpStr.Equals("E1000200") Or tmpStr.Equals("E1000210") Then

            If reqType = 1 Then

                If RecvBuff(5) = 0 Then

                    optBoth.Checked = True

                Else

                    optEither.Checked = True

                End If

                If RecvBuff(6) = 0 Then

                    optExNotActive.Checked = True

                Else

                    optExActive.Checked = True

                End If

            End If

        Else

            Call displayOut(4, 0, "Wrong return values from device.")

        End If

    End Sub

    Private Sub bGetExMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGetExMode.Click

        Call GetExMode(1)

    End Sub

    Private Sub ReadPollingOption(ByVal reqType As Integer)

        Call ClearBuffers()
        SendBuff(0) = &H23
        SendBuff(1) = &H0
        SendLen = 2
        RecvLen = 6
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        If reqType = 1 Then

            ' Interpret PICC Polling Setting
            If (RecvBuff(5) And &H1) <> 0 Then

                Call displayOut(3, 0, "Automatic PICC polling is enabled.")
                cbPollOpt1.Checked = True

            Else

                Call displayOut(3, 0, "Automatic PICC polling is disabled.")
                cbPollOpt1.Checked = False

            End If

            If (RecvBuff(5) And &H2) <> 0 Then

                Call displayOut(3, 0, "Antenna off when no PICC found is enabled.")
                cbPollOpt2.Checked = True

            Else

                Call displayOut(3, 0, "Antenna off when no PICC found is disabled.")
                cbPollOpt2.Checked = False

            End If

            If (RecvBuff(5) And &H4) <> 0 Then

                Call displayOut(3, 0, "Antenna off when PICC is inactive is enabled.")
                cbPollOpt3.Checked = True

            Else

                Call displayOut(3, 0, "Antenna off when PICC is inactive is disabled.")
                cbPollOpt3.Checked = False

            End If

            If (RecvBuff(5) And &H8) <> 0 Then

                Call displayOut(3, 0, "Activate PICC when detected is enabled.")
                cbPollOpt4.Checked = True

            Else

                Call displayOut(3, 0, "Activate PICC when detected is disabled.")
                cbPollOpt4.Checked = False

            End If

            If (((RecvBuff(5) And &H10) = 0) And ((RecvBuff(5) And &H20) = 0)) Then

                Call displayOut(3, 0, "Poll interval is 250 msec.")
                opt250.Checked = True

            End If

            If (((RecvBuff(5) And &H10) <> 0) And ((RecvBuff(5) And &H20) = 0)) Then

                Call displayOut(3, 0, "Poll interval is 500 msec.")
                opt500.Checked = True

            End If

            If (((RecvBuff(5) And &H10) = 0) And ((RecvBuff(5) And &H20) <> 0)) Then

                Call displayOut(3, 0, "Poll interval is 1 sec.")
                opt1.Checked = True

            End If

            If (((RecvBuff(5) And &H10) <> 0) And ((RecvBuff(5) And &H20) <> 0)) Then

                Call displayOut(3, 0, "Poll interval is 2.5 sec.")
                opt25.Checked = True

            End If

            If (RecvBuff(5) And &H40) <> 0 Then

                Call displayOut(3, 0, "Test Mode is enabled.")
                cbPollOpt5.Checked = True

            Else

                Call displayOut(3, 0, "Test Mode is disabled.")
                cbPollOpt5.Checked = False

            End If

            If (RecvBuff(5) And &H80) <> 0 Then

                Call displayOut(3, 0, "ISO14443A Part4 is enforced.")
                cbPollOpt6.Checked = True

            Else

                Call displayOut(3, 0, "ISO14443A Part4 is not enforced.")
                cbPollOpt6.Checked = False

            End If

        End If

    End Sub

    Private Sub bReadPollOpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bReadPollOpt.Click

        Call ReadPollingOption(1)

    End Sub

    Private Sub bSetPollOpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSetPollOpt.Click

        Call ClearBuffers()
        SendBuff(0) = &H23
        SendBuff(1) = &H1

        If cbPollOpt1.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H1

        End If

        If cbPollOpt2.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H2

        End If

        If cbPollOpt3.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H4

        End If

        If cbPollOpt4.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H8

        End If

        If opt500.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H10

        End If

        If opt1.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H20

        End If

        If opt25.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H10
            SendBuff(2) = SendBuff(2) Or &H20

        End If

        If cbPollOpt5.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H40

        End If

        If cbPollOpt6.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H80

        End If

        SendLen = 3
        RecvLen = 7
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

    End Sub

    Private Sub bSetExMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSetExMode.Click

        Call ClearBuffers()
        SendBuff(0) = &H2B
        SendBuff(1) = &H1

        If optBoth.Checked = True Then

            SendBuff(2) = &H0

        End If

        If optEither.Checked = True Then

            SendBuff(2) = &H1

        End If

        SendLen = 3
        RecvLen = 7
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

    End Sub

    Private Sub bClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bClear.Click

        mMsg.Items.Clear()

    End Sub

    Private Sub bReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bReset.Click

        If connActive Then

            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)

        End If

        retCode = ModWinsCard.SCardReleaseContext(hCard)
        Call InitMenu()

    End Sub

    Private Sub bQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bQuit.Click

        ' terminate the application
        retCode = ModWinsCard.SCardReleaseContext(hContext)
        retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)

        End

    End Sub


    Private Sub bManPoll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bManPoll.Click

        ' Always use a valid connection for Card Control commands
        retCode = CallCardConnect(1)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        Call ReadPollingOption(0)

        If (RecvBuff(5) And &H1) <> 0 Then

            Call displayOut(0, 0, "Turn off automatic PICC polling in the device before using this function.")

        Else

            Call ClearBuffers()
            SendBuff(0) = &H22
            SendBuff(1) = &H1
            SendBuff(2) = &HA
            SendLen = 3
            RecvLen = 6
            retCode = CallCardControl()

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

                Exit Sub

            End If

            If (RecvBuff(5) And &H1) <> 0 Then

                Call displayOut(6, 0, "No card within range.")

            Else

                Call displayOut(6, 0, "Card is detected.")

            End If

        End If

    End Sub

    Private Sub bAutoPoll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bAutoPoll.Click

        ' pollCase legend
        ' 1 = Both ICC and PICC can poll, but only one at a time
        ' 2 = Only ICC can poll
        ' 3 = Both reader can be polled

        If autoDet Then

            autoDet = False
            bAutoPoll.Text = "Start &Auto Detection"
            PollTimer.Enabled = False
            displayOut(5, 0, "")
            displayOut(6, 0, "")

            Return

        End If

        ' Always use a valid connection for Card Control commands
        retCode = CallCardConnect(1)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            bAutoPoll.Text = "Start &Auto Detection"
            autoDet = False
            Return
        End If

        GetExMode(0)

        ' Either ICC or PICC can be polled at any one time
        If RecvBuff(5) <> 0 Then

            ReadPollingOption(0)

            ' Auto PICC polling is enabled
            If (RecvBuff(5) And 1) <> 0 Then

                ' Either ICC and PICC can be polled
                pollCase = 1

            Else


                ' Only ICC can be polled
                pollCase = 2

            End If

        Else

            ' Both ICC and PICC can be enabled at the same time

            ReadPollingOption(0)

            ' Auto PICC polling is enabled
            If (RecvBuff(5) And 1) <> 0 Then

                ' Both ICC and PICC can be polled
                pollCase = 3

            Else


                ' Only ICC can be polled
                pollCase = 2

            End If

        End If

        Select Case pollCase

            Case 1

                displayOut(0, 0, "Either reader can detect cards, but not both.")
                Exit Select

            Case 2

                displayOut(0, 0, "Automatic PICC polling is disabled, only ICC can detect card.")
                Exit Select

            Case 3

                displayOut(0, 0, "Both ICC and PICC readers can automatically detect card.")
                Exit Select

        End Select

        autoDet = True
        PollTimer.Enabled = True
        bAutoPoll.Text = "End &Auto Detection"

    End Sub

    Private Sub PollTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PollTimer.Tick

        Dim intIndx As Integer

        Select Case pollCase

            ' Automatic PICC polling is disabled, only ICC can detect card
            Case 2

                ' Connect to ICC reader
                displayOut(6, 0, "Auto Polling is disabled.")
                intIndx = 0
                cbReader.SelectedIndex = intIndx

                While cbReader.SelectedIndex = 1

                    If intIndx = cbReader.Items.Count Then

                        displayOut(0, 0, "Cannot find ACR128 ICC reader.")

                        PollTimer.Enabled = False

                    End If

                    intIndx = intIndx + 1

                    cbReader.SelectedIndex = intIndx

                End While


                RdrState.RdrName = cbReader.Text
                retCode = ModWinsCard.SCardGetStatusChange(Me.hContext, 0, RdrState, 1)

                If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

                    displayOut(1, retCode, "")
                    PollTimer.Enabled = False

                    Return

                End If


                If (RdrState.RdrEventState) <> 0 Then

                    displayOut(5, 0, "Card is inserted.")

                Else

                    displayOut(5, 0, "Card is removed.")
                End If

                Exit Select


            ' Both ICC and PICC readers can automatically detect card
            Case 1, 3

                ' Attempt to poll ICC reader
                intIndx = 0
                cbReader.SelectedIndex = intIndx

                While cbReader.SelectedIndex = 1

                    If intIndx = cbReader.Items.Count Then

                        displayOut(0, 0, "Cannot find ACR128 ICC reader.")

                        PollTimer.Enabled = False
                    End If

                    intIndx = intIndx + 1

                    cbReader.SelectedIndex = intIndx

                End While


                RdrState.RdrName = cbReader.Text

                retCode = ModWinsCard.SCardGetStatusChange(Me.hContext, 0, RdrState, 1)

                If retCode = ModWinsCard.SCARD_S_SUCCESS Then

                    If (RdrState.RdrEventState) <> 0 Then


                        displayOut(5, 0, "Card is inserted.")

                    Else

                        displayOut(5, 0, "Card is removed.")

                    End If

                End If

                ' Attempt to poll PICC reader
                intIndx = 0
                cbReader.SelectedIndex = intIndx

                While cbReader.SelectedIndex = 0

                    If intIndx = cbReader.SelectedIndex Then

                        displayOut(0, 0, "Cannot find ACR128 PICC reader.")

                        PollTimer.Enabled = False

                    End If

                    intIndx = intIndx + 1

                    cbReader.SelectedIndex = intIndx

                End While

                RdrState.RdrName = cbReader.Text

                retCode = ModWinsCard.SCardGetStatusChange(hContext, 0, RdrState, 1)

                If retCode = ModWinsCard.SCARD_S_SUCCESS Then

                    If (RdrState.RdrEventState) <> 0 Then

                        displayOut(6, 0, "Card is detected.")

                    Else

                        displayOut(6, 0, "No card within range.")

                    End If

                End If

                Exit Select

        End Select

    End Sub

End Class

