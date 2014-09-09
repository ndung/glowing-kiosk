'=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                   execute advanced device-specific functions of ACR128
'  
'  Author :         Daryl M. Rojas
'
'  Module :         ModWinscard.vb
'   
'  Date   :         June 16, 2008
'
'  Revision Trail:  (Date/Author/Description) 
'
'==========================================================================================

Public Class AdvancedDevProg

    Public retCode, hContext, hCard, Protocol As Long
    Public connActive, autoDet As Boolean
    Public SendBuff(262), RecvBuff(262) As Byte
    Public SendLen, RecvLen, nBytesRet, reqType As Integer
    Public RdrState As SCARD_READERSTATE

    Private Sub AdvancedDevProg_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call InitMenu()

    End Sub

    Private Sub InitMenu()

        connActive = False
        autoDet = False
        Polltimer.Enabled = False
        cbReader.Items.Clear()
        mMsg.Items.Clear()
        displayOut(0, 0, "Program ready")
        bConnect.Enabled = False
        bInit.Enabled = True
        bReset.Enabled = False
        tFWI.Text = ""
        tPollTO.Text = ""
        tTFS.Text = ""
        gbFWI.Enabled = False
        rbAntOn.Checked = False
        rbAntOff.Checked = False
        gbAntenna.Enabled = False
        tFStop.Text = ""
        tSetup.Text = ""
        cbFilter.Checked = False
        tRecGain.Text = ""
        gbTransSet.Enabled = False
        tPICC1.Text = ""
        tPICC2.Text = ""
        tPICC3.Text = ""
        tPICC4.Text = ""
        tPICC5.Text = ""
        tPICC6.Text = ""
        tPICC7.Text = ""
        tPICC8.Text = ""
        tPICC9.Text = ""
        tPICC10.Text = ""
        tPICC11.Text = ""
        tPICC12.Text = ""
        gbPICC.Enabled = False
        tMsg.Text = ""
        rbType1.Checked = False
        rbType2.Checked = False
        rbType3.Checked = False
        gbPolling.Enabled = False
        gbErrHand.Enabled = False
        gbPPS.Enabled = False
        tRegCurr.Text = ""
        tRegNew.Text = ""
        gbReg.Enabled = False
        rbRIS1.Checked = False
        rbRIS2.Checked = False
        rbRIS3.Checked = False
        gbRefIS.Enabled = False
        cbReader.Text = ""

    End Sub

    Private Sub EnableButtons()

        bInit.Enabled = False
        bConnect.Enabled = True
        bReset.Enabled = True
        bClear.Enabled = True

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

            Case 0
                mMsg.ForeColor = Color.Teal
            Case 1
                mMsg.ForeColor = Color.Red
                PrintText = ModWinsCard.GetScardErrMsg(retVal)
            Case 2
                mMsg.ForeColor = Color.Black
                PrintText = "<" + PrintText
            Case 3
                mMsg.ForeColor = Color.Black
                PrintText = ">" + PrintText

        End Select
        mMsg.Items.Add(PrintText)
        mMsg.ForeColor = Color.Black
        mMsg.Focus()

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
        Call EnableButtons()

        ' Look for ACR128 SAM and make it the default reader in the combobox
        For indx = 0 To cbReader.Items.Count - 1

            cbReader.SelectedIndex = indx

            If InStr("ACR128 SAM", cbReader.Text) > 0 Then

                Exit Sub

            End If

        Next indx

        Exit Sub

    End Sub

    Private Sub bConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bConnect.Click

        ' Connect to selected reader using hContext handle and obtain hCard handle
        If connActive Then

            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)

        End If

        ' Shared Connection
        retCode = ModWinsCard.SCardConnect(hContext, cbReader.Text, 1, 0 Or 1, hCard, Protocol)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            ' check if ACR128 SAM is used and use Direct Mode if SAM is not detected
            If InStr(cbReader.Text, "ACR128U SAM") > 0 Then

                '1. Direct Connection
                retCode = ModWinsCard.SCardConnect(hContext, cbReader.Text, ModWinsCard.SCARD_SHARE_DIRECT, 0, hCard, Protocol)

                If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

                    Call displayOut(1, retCode, "")
                    connActive = False

                    Exit Sub

                Else

                    Call displayOut(0, 0, "Successful connection to " & cbReader.Text)

                End If

            Else

                Call displayOut(1, retCode, "")
                connActive = False
                Exit Sub

            End If

        Else

            Call displayOut(0, 0, "Successful connection to " & cbReader.Text)

        End If

        connActive = True
        gbFWI.Enabled = True
        gbAntenna.Enabled = True
        gbTransSet.Enabled = True
        gbPICC.Enabled = True
        gbPolling.Enabled = True
        rbType3.Checked = True
        gbErrHand.Enabled = True
        gbPPS.Enabled = True
        gbReg.Enabled = True
        gbRefIS.Enabled = True
        rbRIS3.Checked = True

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

    Private Function CallCardControl() As Integer

        Dim tmpStr As String
        Dim indx As Integer

        ' Display Apdu In
        tmpStr = "SCardControl: "

        For indx = 0 To SendLen

            tmpStr = tmpStr + " " + Format(Hex(SendBuff(indx)), "")

        Next indx

        Call displayOut(2, 0, tmpStr)

        retCode = ModWinsCard.SCardControl(hCard, ModWinsCard.IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND, SendBuff(0), SendLen, RecvBuff(0), RecvLen, nBytesRet)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Call displayOut(1, retCode, "")

            Exit Function

        Else

            tmpStr = ""

            For indx = 0 To RecvLen - 1

                tmpStr = tmpStr + " " + Format(Hex(RecvBuff(indx)), "")

            Next indx

            CallCardControl = retCode

        End If

    End Function

    Private Sub bGetFWI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGetFWI.Click

        Dim indx As Integer
        Dim tmpStr As String

        Call ClearBuffers()

        SendBuff(0) = &H1F
        SendBuff(1) = &H0
        SendLen = 2
        RecvLen = 8
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If


        tmpStr = ""

        For indx = 0 To 4

            tmpStr = tmpStr + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If tmpStr = "E10003" Then

            ' Interpret response data
            tFWI.Text = Format(Hex(RecvBuff(5)), "")
            tPollTO.Text = Format(Hex(RecvBuff(6)), "")
            tTFS.Text = Format(Hex(RecvBuff(7)), "")
            Call displayOut(3, 0, tmpStr)

        Else

            tFWI.Text = ""
            tPollTO.Text = ""
            tTFS.Text = ""
            displayOut(3, 0, "Invalid response")

        End If

    End Sub

    Private Sub bSetFWI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSetFWI.Click

        Dim indx As Integer
        Dim tmpStr As String
        Dim tmpLong As Byte

        If (tFWI.Text = "" Or Not Byte.TryParse(tFWI.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tFWI.SelectAll()
            tFWI.Focus()
            tFWI.Text = ""
            Exit Sub

        End If

        If (tPollTO.Text = "" Or Not Byte.TryParse(tPollTO.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tPollTO.SelectAll()
            tPollTO.Focus()
            tPollTO.Text = ""
            Exit Sub

        End If

        If (tTFS.Text = "" Or Not Byte.TryParse(tTFS.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tTFS.SelectAll()
            tTFS.Focus()
            tTFS.Text = ""
            Exit Sub

        End If

        Call ClearBuffers()
        SendBuff(0) = &H1F
        SendBuff(1) = &H3
        SendBuff(2) = CLng("&H" + tFWI.Text)
        SendBuff(3) = CLng("&H" + tPollTO.Text)
        SendBuff(4) = CLng("&H" + tTFS.Text)
        SendLen = 5
        RecvLen = 8
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        tmpStr = ""

        For indx = 0 To 4

            tmpStr = tmpStr + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If (tmpStr <> "E10003") Then

            displayOut(3, 0, "Invalid Response")

        End If

    End Sub

    Private Sub bGetAS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGetAS.Click

        Dim indx As Integer
        Dim tmpStr As String

        Call ClearBuffers()
        SendBuff(0) = &H25
        SendBuff(1) = &H0
        SendLen = 2
        RecvLen = 6

        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        tmpStr = ""

        For indx = 0 To 4

            tmpStr = tmpStr + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If (tmpStr = "E10001") Then

            ' Interpret response data
            If RecvBuff(5) = 0 Then

                rbAntOff.Checked = True

            Else

                rbAntOn.Checked = True

            End If

        Else

            rbAntOff.Checked = False
            rbAntOn.Checked = False
            Call displayOut(3, 0, "Invalid Response")

        End If

    End Sub

    Private Sub ReadPollingOption()

        Call ClearBuffers()
        SendBuff(0) = &H23
        SendBuff(1) = &H0
        SendLen = 2
        RecvLen = 6

        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

    End Sub

    Private Sub bSetAS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSetAS.Click

        Dim indx As Integer
        Dim tmpStr As String

        Call ReadPollingOption()

        If (RecvBuff(5) And &H1) <> 0 Then

            Call displayOut(0, 0, "Turn off automatic PICC Polling in the device before using this function.")
            Exit Sub

        End If

        Call ClearBuffers()
        SendBuff(0) = &H25
        SendBuff(1) = &H1

        If rbAntOn.Checked Then

            SendBuff(2) = &H1

        Else

            If rbAntOff.Checked Then

                SendBuff(2) = &H0

            Else

                rbAntOn.Focus()
                Exit Sub

            End If


        End If

        SendLen = 3
        RecvLen = 6

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        tmpStr = ""

        For indx = 0 To 4

            tmpStr = tmpStr + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If tmpStr <> "E10001" Then

            Call displayOut(3, 0, "Invalid response")

        End If

    End Sub

    Private Sub bGetTranSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGetTranSet.Click

        Dim indx, tmpVal As Integer
        Dim tmpStr As String

        Call ClearBuffers()
        SendBuff(0) = &H20
        SendBuff(1) = &H1
        SendLen = 2
        RecvLen = 9

        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        tmpStr = ""

        For indx = 0 To 5

            tmpStr = tmpStr + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If tmpStr <> "E10001" Then

            ' Interpret resonse data
            tmpVal = RecvBuff(6) >> 4
            tFStop.Text = tmpVal.ToString()
            tmpVal = RecvBuff(6) And &HF
            tSetup.Text = tmpVal.ToString()

            If (RecvBuff(7) And &H4) <> 0 Then

                cbFilter.Checked = True

            Else

                cbFilter.Checked = False

            End If
            tmpVal = RecvBuff(7) And &H3
            tRecGain.Text = tmpVal.ToString()
            tTxMode.Text = Format(Hex(RecvBuff(8)), "")

        Else

            tFStop.Text = ""
            tSetup.Text = ""
            cbFilter.Checked = False
            tRecGain.Text = ""
            tTxMode.Text = ""
            displayOut(3, 0, "Invalid response")

        End If

    End Sub

    Private Sub bSetTranSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSetTranSet.Click

        Dim tempInt As Integer
        Dim tmpLong As Byte

        If (tFStop.Text = "" Or Not Integer.TryParse(tFStop.Text, tempInt)) Then

            tFStop.SelectAll()
            tFStop.Focus()
            tFStop.Text = ""
            Exit Sub

        End If

        If (tSetup.Text = "" Or Not Integer.TryParse(tSetup.Text, tempInt)) Then

            tSetup.SelectAll()
            tSetup.Focus()
            tSetup.Text = ""
            Exit Sub

        End If

        If (tRecGain.Text = "" Or Not Integer.TryParse(tRecGain.Text, tempInt)) Then

            tRecGain.SelectAll()
            tRecGain.Focus()
            tRecGain.Text = ""

        End If

        If (tTxMode.Text = "" Or Not Byte.TryParse(tTxMode.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tTxMode.SelectAll()
            tTxMode.Focus()
            tTxMode.Text = ""

        End If

        If CInt(tFStop.Text) > 15 Then

            tFStop.Text = "15"
            tFStop.Focus()

        End If

        If CInt(tSetup.Text) > 15 Then

            tSetup.Text = "15"
            tSetup.Focus()

        End If

        If CInt(tRecGain.Text) > 3 Then

            tRecGain.Text = "3"
            tRecGain.Focus()

        End If

        ClearBuffers()
        SendBuff(0) = &H20
        SendBuff(1) = &H4
        SendBuff(2) = &H6
        SendBuff(3) = CInt(tFStop.Text) >> 4
        SendBuff(3) = SendBuff(3) + CInt(tSetup.Text)

        If cbFilter.Checked Then

            SendBuff(4) = &H4

        End If

        SendBuff(4) = SendBuff(4) + CInt(tRecGain.Text)
        SendBuff(5) = CLng("&H" + tTxMode.Text)
        SendLen = 6
        RecvLen = 5
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If


        If RecvBuff(0) <> &HE1 Then

            Call displayOut(3, 0, "Invalid response")

        End If

    End Sub

    Private Sub bGetEH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGetEH.Click

        Dim indx, tmpVal As Integer
        Dim tmpStr As String

        Call ClearBuffers()
        SendBuff(0) = &H2C
        SendBuff(1) = &H0
        SendLen = 2
        RecvLen = 6
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        tmpStr = ""

        For indx = 0 To 4

            tmpStr = tmpStr + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If (tmpStr = "E10001") Then

            ' Interpret response data
            tmpVal = RecvBuff(5) >> 4
            tPc2Pi.Text = (tmpVal).ToString()
            tmpVal = RecvBuff(5) And &H3
            tPi2PC.Text = (tmpVal).ToString()

        Else

            tPc2Pi.Text = ""
            tPi2PC.Text = ""
            Call displayOut(3, 0, "Invalid response")

        End If

    End Sub

    Private Sub bSetEH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSetEH.Click

        Dim indx, tempInt As Integer
        Dim tmpStr As String

        If Not Integer.TryParse(tPc2Pi.Text, tempInt) Then

            tPc2Pi.Focus()
            tPc2Pi.Text = ""
            Return

        End If

        If Not Integer.TryParse(tPi2PC.Text, tempInt) Then

            tPi2PC.Focus()
            tPi2PC.Text = ""
            Return

        End If



        If tPc2Pi.Text = "" Then

            tPc2Pi.Focus()

        End If

        If CInt(tPc2Pi.Text) > 3 Then

            tPc2Pi.Text = "3"
            tPc2Pi.Focus()

        End If

        If tPi2PC.Text = "" Then

            tPc2Pi.Focus()

        End If

        If CInt(tPi2PC.Text) > 3 Then

            tPi2PC.Text = "3"
            tPi2PC.Focus()

        End If

        Call ClearBuffers()
        SendBuff(0) = &H2C
        SendBuff(1) = &H1
        SendBuff(2) = CInt(tPc2Pi.Text)
        SendBuff(2) = SendBuff(2) + CInt(tPi2PC.Text)
        SendLen = 3
        RecvLen = 6
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        tmpStr = ""
        For indx = 0 To 4

            tmpStr = tmpStr + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If (tmpStr <> "E10001") Then

            displayOut(3, 0, "Invalid response")

        End If

    End Sub

    Private Sub bGetPICC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGetPICC.Click

        Dim indx As Integer
        Dim tmpStr As String

        ClearBuffers()
        SendBuff(0) = &H2A
        SendBuff(1) = &H0
        SendLen = 2
        RecvLen = 17
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        tmpStr = ""
        For indx = 0 To 4

            tmpStr = tmpStr + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If (tmpStr = "E1000C") Then

            ' Interpret response data
            tPICC1.Text = Format(Hex(RecvBuff(5)), "")
            tPICC2.Text = Format(Hex(RecvBuff(6)), "")
            tPICC3.Text = Format(Hex(RecvBuff(7)), "")
            tPICC4.Text = Format(Hex(RecvBuff(8)), "")
            tPICC5.Text = Format(Hex(RecvBuff(9)), "")
            tPICC6.Text = Format(Hex(RecvBuff(10)), "")
            tPICC7.Text = Format(Hex(RecvBuff(11)), "")
            tPICC8.Text = Format(Hex(RecvBuff(12)), "")
            tPICC9.Text = Format(Hex(RecvBuff(13)), "")
            tPICC10.Text = Format(Hex(RecvBuff(14)), "")
            tPICC11.Text = Format(Hex(RecvBuff(15)), "")
            tPICC12.Text = Format(Hex(RecvBuff(16)), "")

        Else

            tPICC1.Text = ""
            tPICC2.Text = ""
            tPICC3.Text = ""
            tPICC4.Text = ""
            tPICC5.Text = ""
            tPICC6.Text = ""
            tPICC7.Text = ""
            tPICC8.Text = ""
            tPICC9.Text = ""
            tPICC10.Text = ""
            tPICC11.Text = ""
            tPICC12.Text = ""
            Call displayOut(3, 0, "Invalid response")

        End If

    End Sub

    Private Sub bSetPICC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSetPICC.Click

        Dim indx As Integer
        Dim tmpStr As String
        Dim tmpLong As Byte

        If (tPICC1.Text = "" Or Not Byte.TryParse(tPICC1.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tPICC1.SelectAll()
            tPICC1.Focus()
            tPICC1.Text = ""
            Exit Sub

        End If

        If (tPICC2.Text = "" Or Not Byte.TryParse(tPICC2.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tPICC2.SelectAll()
            tPICC2.Focus()
            tPICC2.Text = ""
            Exit Sub

        End If

        If (tPICC3.Text = "" Or Not Byte.TryParse(tPICC3.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tPICC3.SelectAll()
            tPICC3.Focus()
            tPICC3.Text = ""
            Exit Sub

        End If

        If (tPICC4.Text = "" Or Not Byte.TryParse(tPICC4.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tPICC4.SelectAll()
            tPICC4.Focus()
            tPICC4.Text = ""
            Exit Sub

        End If

        If (tPICC5.Text = "" Or Not Byte.TryParse(tPICC5.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tPICC5.SelectAll()
            tPICC5.Focus()
            tPICC5.Text = ""
            Exit Sub

        End If

        If (tPICC6.Text = "" Or Not Byte.TryParse(tPICC6.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tPICC6.SelectAll()
            tPICC6.Focus()
            tPICC6.Text = ""
            Exit Sub

        End If

        If (tPICC7.Text = "" Or Not Byte.TryParse(tPICC7.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tPICC7.SelectAll()
            tPICC7.Focus()
            tPICC7.Text = ""
            Exit Sub

        End If

        If (tPICC8.Text = "" Or Not Byte.TryParse(tPICC8.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tPICC8.SelectAll()
            tPICC8.Focus()
            tPICC8.Text = ""
            Exit Sub

        End If

        If (tPICC9.Text = "" Or Not Byte.TryParse(tPICC9.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tPICC9.SelectAll()
            tPICC9.Focus()
            tPICC9.Text = ""
            Exit Sub

        End If

        If (tPICC10.Text = "" Or Not Byte.TryParse(tPICC10.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tPICC10.SelectAll()
            tPICC10.Focus()
            tPICC10.Text = ""
            Exit Sub

        End If

        If (tPICC11.Text = "" Or Not Byte.TryParse(tPICC11.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tPICC11.SelectAll()
            tPICC11.Focus()
            tPICC11.Text = ""
            Exit Sub

        End If

        If (tPICC12.Text = "" Or Not Byte.TryParse(tPICC3.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tPICC12.SelectAll()
            tPICC12.Focus()
            tPICC12.Text = ""
            Exit Sub

        End If

        Call ClearBuffers()
        SendBuff(0) = &H2A
        SendBuff(1) = &HC
        SendBuff(2) = Byte.TryParse(tPICC1.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)
        SendBuff(3) = Byte.TryParse(tPICC2.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)
        SendBuff(4) = Byte.TryParse(tPICC3.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)
        SendBuff(5) = Byte.TryParse(tPICC4.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)
        SendBuff(6) = Byte.TryParse(tPICC5.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)
        SendBuff(7) = Byte.TryParse(tPICC6.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)
        SendBuff(8) = Byte.TryParse(tPICC7.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)
        SendBuff(9) = Byte.TryParse(tPICC8.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)
        SendBuff(10) = Byte.TryParse(tPICC9.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)
        SendBuff(11) = Byte.TryParse(tPICC10.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)
        SendBuff(12) = Byte.TryParse(tPICC11.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)
        SendBuff(13) = Byte.TryParse(tPICC12.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)
        SendLen = 14
        RecvLen = 17

        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        tmpStr = ""
        For indx = 0 To 4

            tmpStr = tmpStr + "" + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If (tmpStr <> "E1000C") Then

            Call displayOut(3, 0, "Invalid response")

        End If

    End Sub

    Private Sub bGetPSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGetPSet.Click

        Dim indx As Integer
        Dim tmpStr As String

        Call ClearBuffers()
        SendBuff(0) = &H20
        SendBuff(1) = &H0
        SendBuff(3) = &HFF
        SendLen = 4
        RecvLen = 6

        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        tmpStr = ""
        For indx = 0 To 4

            tmpStr = tmpStr + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If (tmpStr <> "E10001") Then

            mMsg.Text = "Invalid Card Detected"
            Exit Sub

        End If

        ' Interpret Status
        Select Case RecvBuff(5)

            Case 1
                rbType1.Checked = True
            Case 2
                rbType2.Checked = True
            Case Else
                rbType3.Checked = True

        End Select

    End Sub

    Private Sub bSetPSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSetPSet.Click

        If rbType1.Checked Then

            reqType = 1

        Else

            If rbType2.Checked Then

                reqType = 2

            Else

                If rbType3.Checked Then

                    reqType = 3

                Else

                    rbType1.Focus()
                    Exit Sub

                End If

            End If

        End If

        Call ClearBuffers()
        SendBuff(0) = &H20
        SendBuff(1) = &H2

        Select Case reqType

            Case 1
                SendBuff(2) = &H1
            Case 2
                SendBuff(2) = &H2
            Case Else
                SendBuff(2) = &H3

        End Select

        SendBuff(3) = &HFF
        SendLen = 4
        RecvLen = 5
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

    End Sub

    Private Sub bPoll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bPoll.Click

        If autoDet Then

            autoDet = False
            bPoll.Text = "Start Auto &Detection"
            Polltimer.Enabled = False
            tMsg.Text = "Polling stopped..."
            Exit Sub

        End If

        tMsg.Text = "Polling started..."
        autoDet = True
        Polltimer.Enabled = True
        bPoll.Text = "End Auto &Detection"

    End Sub

    Private Sub Polltimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Polltimer.Tick

        Dim indx As Integer
        Dim tmpStr As String

        indx = 0
        cbReader.SelectedIndex = indx

        While cbReader.SelectedIndex = 0
            If indx = cbReader.Items.Count Then

                displayOut(0, 0, "Cannot find ACR128 ICC reader.")
                Polltimer.Enabled = False

            End If

            indx = indx + 1

            cbReader.SelectedIndex = indx

        End While

        RdrState.RdrName = cbReader.Text
        retCode = ModWinsCard.SCardGetStatusChange(hContext, 0, RdrState, 1)

        If retCode = ModWinsCard.SCARD_S_SUCCESS Then

            If (RdrState.RdrEventState) <> 0 Then

                Select Case reqType

                    Case 1
                        tmpStr = "ISO14443 Type A card"
                    Case 2
                        tmpStr = "ISO14443 Type B card"
                    Case Else
                        tmpStr = "ISO14443 card"

                End Select

                tMsg.Text = tmpStr + " is detected"

            Else

                tMsg.Text = "No Card within range."

            End If

        End If

    End Sub

    Private Sub bGetPPS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGetPPS.Click

        Dim indx As Integer
        Dim tmpStr As String

        Call ClearBuffers()
        SendBuff(0) = &H24
        SendBuff(1) = &H0
        SendLen = 2
        RecvLen = 7

        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        tmpStr = ""
        For indx = 0 To 4

            tmpStr = tmpStr + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If (tmpStr = "E10002") Then

            ' Interpret response data
            Select Case RecvBuff(5)

                Case 0
                    rbMaxSpeed1.Checked = True
                Case 1
                    rbMaxSpeed2.Checked = True
                Case 2
                    rbMaxSpeed3.Checked = True
                Case 3
                    rbMaxSpeed4.Checked = True
                Case Else
                    rbMaxSpeed5.Checked = True

            End Select

            Select Case RecvBuff(6)

                Case 0
                    rbCurrSpeed1.Checked = True
                Case 1
                    rbCurrSpeed2.Checked = True
                Case 2
                    rbCurrSpeed3.Checked = True
                Case 3
                    rbCurrSpeed4.Checked = True
                Case Else
                    rbCurrSpeed5.Checked = True


            End Select


        Else

            Call displayOut(3, 0, "Invalid response")

        End If

    End Sub

    Private Sub bSetPPS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSetPPS.Click

        Dim indx As Integer
        Dim tmpStr As String

        If ((rbMaxSpeed1.Checked = False) And (rbMaxSpeed2.Checked = False) And (rbMaxSpeed3.Checked = False) And (rbMaxSpeed4.Checked = False)) Then

            rbMaxSpeed5.Checked = True

        End If

        If ((rbCurrSpeed1.Checked = False) And (rbCurrSpeed2.Checked = False) And (rbCurrSpeed3.Checked = False) And (rbCurrSpeed4.Checked = False)) Then

            rbCurrSpeed5.Checked = True

        End If

        Call ClearBuffers()
        SendBuff(0) = &H24
        SendBuff(1) = &H1

        If rbMaxSpeed1.Checked = True Then

            SendBuff(2) = &H0

        End If

        If rbMaxSpeed2.Checked = True Then

            SendBuff(2) = &H1

        End If

        If rbMaxSpeed3.Checked = True Then

            SendBuff(2) = &H2

        End If

        If rbMaxSpeed4.Checked = True Then

            SendBuff(2) = &H3

        End If

        If rbMaxSpeed5.Checked = True Then

            SendBuff(2) = &HFF

        End If

        SendLen = 3
        RecvLen = 7
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        tmpStr = ""
        For indx = 0 To 4

            tmpStr = tmpStr + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If (tmpStr <> "E10002") Then

            Call displayOut(3, 0, "Invalid Response")

        End If

    End Sub

    Private Sub bGetReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGetReg.Click

        Dim indx As Integer
        Dim tmpStr As String

        Call ClearBuffers()
        SendBuff(0) = &H19
        SendBuff(1) = &H1
        SendLen = 2
        RecvLen = 6
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If


        tmpStr = ""
        For indx = 0 To 4

            tmpStr = tmpStr + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If (tmpStr = "E10001") Then

            ' Interpret response data
            tRegCurr.Text = "0" + Format(Hex(RecvBuff(5)), "")

        Else
            tRegCurr.Text = ""
            Call displayOut(3, 0, "Invalid Response")

        End If

    End Sub

    Private Sub bSetReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSetReg.Click

        Dim indx As Integer
        Dim tmpStr As String
        Dim tmpLong As Byte

        If Not Byte.TryParse(tRegCurr.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong) Then

            tRegCurr.Focus()
            tRegCurr.Text = ""
            Exit Sub

        End If

        If Not Byte.TryParse(tRegNew.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong) Then

            tRegNew.Focus()
            tRegNew.Text = ""
            Exit Sub

        End If

        If tRegCurr.Text = "" Then

            tRegCurr.SelectAll()
            tRegCurr.Focus()
            tRegNew.Text = "00"

        End If

        If tRegNew.Text = "" Then

            tRegNew.Text = "00"
            tRegNew.SelectAll()
            tRegNew.Focus()

        End If

        Call ClearBuffers()
        SendBuff(0) = &H1A
        SendBuff(1) = &H2
        SendBuff(2) = Hex("&H" + tRegCurr.Text)
        SendBuff(3) = Hex("&H" + tRegNew.Text)
        SendLen = 4
        RecvLen = 6
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        tmpStr = ""
        For indx = 0 To 4

            tmpStr = tmpStr + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If (tmpStr = "E10001") Then

            ' Interpret response data
            tRegCurr.Text = "0" + Format(Hex(RecvBuff(5)), "")

        Else

            tRegCurr.Text = ""
            tRegNew.Text = ""
            Call displayOut(3, 0, "Invalid response")

        End If

    End Sub

    Private Sub bRefIS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bRefIS.Click

        Dim indx As Integer
        Dim tmpStr As String

        If connActive Then

            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)

        End If

        cbReader.SelectedIndex = indx


        While cbReader.SelectedIndex = 0

            If indx = cbReader.Items.Count Then

                Call displayOut(0, 0, "Cannot find ACR128 SAM reader.")
                connActive = False

            End If

            indx = indx + 2
            cbReader.SelectedIndex = indx

        End While

        ' 1. For SAM Refresh, connect to SAM Interface in direct mode

        If rbRIS3.Checked Then

            retCode = ModWinsCard.SCardConnect(hContext, _
                           cbReader.Text, _
                           ModWinsCard.SCARD_SHARE_DIRECT, _
                           0, _
                           hCard, _
                           Protocol)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

                Call displayOut(1, retCode, "")
                connActive = False
                Exit Sub

            Else

                Call displayOut(0, 0, "Successful connection to " & cbReader.Text)

            End If

        Else

            ' 2. For other interfaces, connect to SAM Interface in direct or shared mode
            retCode = ModWinsCard.SCardConnect(hContext, _
                         cbReader.Text, _
                         ModWinsCard.SCARD_SHARE_DIRECT Or ModWinsCard.SCARD_SHARE_SHARED, _
                         0, _
                         hCard, _
                         Protocol)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

                Call displayOut(1, retCode, "")
                connActive = False
                Exit Sub

            Else

                Call displayOut(0, 0, "Successful connection to " & cbReader.Text)

            End If

        End If

        Call ClearBuffers()
        SendBuff(0) = &H2D
        SendBuff(1) = &H1

        If rbRIS1.Checked Then

            SendBuff(2) = &H1                           ' bit 0

        Else

            If rbRIS2.Checked Then

                SendBuff(2) = &H2                       ' bit 1

            Else

                SendBuff(2) = &H4                       ' bit 2

            End If

        End If

        SendLen = 3
        RecvLen = 6
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        tmpStr = ""
        For indx = 0 To 4

            tmpStr = tmpStr + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If (tmpStr <> "E10001") Then

            Call displayOut(3, 0, "Invalid response")

        End If

        ' 3. For SAM interface, disconnect and connect to SAM Interface in direct or shared mode
        If rbRIS3.Checked Then

            If connActive Then

                retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)

            End If

            indx = 0
            cbReader.SelectedIndex = indx

            While cbReader.SelectedIndex = 0

                If indx = cbReader.Items.Count Then

                    Call displayOut(0, 0, "Cannot find ACR128 SAM reader.")
                    connActive = False

                End If

                indx = indx + 2
                cbReader.SelectedIndex = indx

            End While

            retCode = ModWinsCard.SCardConnect(hContext, _
                                   cbReader.Text, _
                                   ModWinsCard.SCARD_SHARE_DIRECT Or ModWinsCard.SCARD_SHARE_SHARED, _
                                   0, _
                                   hCard, _
                                   Protocol)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

                Call displayOut(1, retCode, "")
                connActive = False
                Exit Sub

            Else
                Call displayOut(0, 0, "Successful connection to " + cbReader.Text)
            End If

        End If

    End Sub

End Class


