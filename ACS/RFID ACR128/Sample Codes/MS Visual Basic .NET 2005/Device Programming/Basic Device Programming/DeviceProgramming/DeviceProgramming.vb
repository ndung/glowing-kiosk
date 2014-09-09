'=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                   execute device-specific functions of ACR128
'
'  Author :         Daryl M. Rojas
'
'  Module :         ModWinscard.vb
'   
'  Date   :         June 2, 2008
'
' Revision Trail:   (Date/Author/Description) 
'
'=========================================================================================

Public Class DeviceProgramming

    Public retCode, hContext, hCard, Protocol As Long
    Public connActive As Boolean = False
    Public SendBuff(262), RecvBuff(262) As Byte
    Public SendLen, RecvLen, nBytesRet As Integer

    Private Sub DeviceProgramming_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Call InitMenu()

    End Sub

    Private Sub InitMenu()

        connActive = False
        cbReader.Text = ""
        mMsg.Text = ""
        mMsg.Items.Clear()
        bConnect.Enabled = False
        bInit.Enabled = True
        bReset.Enabled = False
        bGetFW.Enabled = False
        gbLED.Enabled = False
        cbBuzzLed1.Checked = False
        cbBuzzLed2.Checked = False
        cbBuzzLed3.Checked = False
        cbBuzzLed4.Checked = False
        cbBuzzLed5.Checked = False
        cbBuzzLed6.Checked = False
        cbBuzzLed7.Checked = False
        tBuzzDur.Text = ""
        gbBuzz.Enabled = False
        bSetBuzzDur.Enabled = False
        bGetLed.Enabled = False
        bSetLed.Enabled = False
        bGetBuzzState.Enabled = False
        bSetBuzzState.Enabled = False
        gbBuzzState.Enabled = False
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

    Private Sub EnableButtons()

        bInit.Enabled = False
        bConnect.Enabled = True
        bReset.Enabled = True
        bClear.Enabled = True
        gbLED.Enabled = True

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

            If cbReader.SelectedIndex = 2 Then

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
        bGetFW.Enabled = True
        bGetLed.Enabled = True
        bSetLed.Enabled = True
        gbBuzz.Enabled = True
        gbBuzzState.Enabled = True
        gbLED.Enabled = True
        bGetBuzzState.Enabled = True
        bSetBuzzState.Enabled = True
        bSetBuzzDur.Enabled = True
        Call GetLEDState()
        Call GetBuzzLEDState()

    End Sub

    Private Function GetLEDState() As Integer

        Call ClearBuffers()
        SendBuff(0) = &H29
        SendBuff(1) = &H0
        SendLen = 2
        RecvLen = 6
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            GetLEDState = retCode
            Exit Function

        End If

        ' interpret LED data
        Select Case RecvBuff(5)

            Case 0
                Call displayOut(3, 0, "Currently connected to SAM reader interface.")
                cbRed.Enabled = True

            Case 1
                Call displayOut(3, 0, "No PICC found.")
                cbRed.Enabled = True

            Case 2
                Call displayOut(3, 0, "PICC is present but not activated.")
                cbRed.Enabled = True

            Case 3
                Call displayOut(3, 0, "PICC is present and activated.")
                cbRed.Enabled = True

            Case 4
                Call displayOut(3, 0, "PICC is present and activated.")
                cbRed.Enabled = True

            Case 5
                Call displayOut(3, 0, "PICC is present and activated.")
                cbGreen.Enabled = True

            Case 6
                Call displayOut(3, 0, "ICC is absent or not activated.")
                cbGreen.Enabled = True

            Case 7
                Call displayOut(3, 0, "ICC is operating.")
                cbGreen.Enabled = True

        End Select

        If (RecvBuff(5) And &H2) <> 0 Then
            cbGreen.Checked = True
        Else
            cbGreen.Checked = False
        End If

        If (RecvBuff(5) And &H1) <> 0 Then
            cbRed.Checked = True
        Else
            cbRed.Checked = False
        End If

        GetLEDState = retCode

    End Function

    Private Sub bGetFW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGetFW.Click

        Dim tmpStr As String
        Dim indx As Integer

        ClearBuffers()
        SendBuff(0) = &H18
        SendBuff(1) = &H0
        SendLen = 2
        RecvLen = 35
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        ' Interpret Firmware Data
        tmpStr = "Firware Version"

        For indx = 5 To 24

            If RecvBuff(indx) <> &H0 Then

                tmpStr = tmpStr + Chr(RecvBuff(indx))

            End If

        Next indx

        Call displayOut(3, 0, tmpStr)

    End Sub

    Private Sub bStartBuzz_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bStartBuzz.Click

        Call ClearBuffers()
        SendBuff(0) = &H28
        SendBuff(1) = &H1
        SendBuff(2) = &HFF
        SendLen = 3
        RecvLen = 6
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If
    End Sub

    Private Sub bStopBuzz_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bStopBuzz.Click

        Call ClearBuffers()
        SendBuff(0) = &H28
        SendBuff(1) = &H1
        SendBuff(2) = &H0
        SendLen = 3
        RecvLen = 6
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

    End Sub

    Private Sub bSetBuzzDur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSetBuzzDur.Click


        Dim tempInt As Integer

        If tBuzzDur.Text = "" Then

            tBuzzDur.Text = "1"
            tBuzzDur.SelectAll()
            tBuzzDur.Focus()

        End If

        If Not Integer.TryParse(tBuzzDur.Text, tempInt) Then

            MessageBox.Show("Invalid Input")
            tBuzzDur.Text = ""
            Return

        End If

        If CInt(tBuzzDur.Text) > 255 Then

            tBuzzDur.Text = "255"
            tBuzzDur.SelectAll()
            tBuzzDur.Focus()
            Exit Sub

        End If

        If CInt(tBuzzDur.Text) < 1 Then

            tBuzzDur.Text = "1"
            tBuzzDur.SelectAll()
            tBuzzDur.Focus()

        End If

        Call ClearBuffers()
        SendBuff(0) = &H28
        SendBuff(1) = &H1
        SendBuff(2) = CInt(tBuzzDur.Text)
        SendLen = 3
        RecvLen = 6
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

    End Sub

    Private Function GetBuzzLEDState() As Integer

        Call ClearBuffers()
        SendBuff(0) = &H21
        SendBuff(1) = &H0
        SendLen = 2
        RecvLen = 6
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            GetBuzzLEDState = retCode
            Exit Function

        End If

        ' Interpret buzzer State Data
        If (RecvBuff(5) And &H1) <> 0 Then

            Call displayOut(3, 0, "ICC Activation Status LED is enabled.")
            cbBuzzLed1.Checked = True

        Else

            Call displayOut(3, 0, "ICC Activation Status LED is disabled.")
            cbBuzzLed1.Checked = False

        End If

        If (RecvBuff(5) And &H2) <> 0 Then

            Call displayOut(3, 0, "PICC Polling Status LED is enabled.")
            cbBuzzLed2.Checked = True

        Else

            Call displayOut(3, 0, "PICC Polling Status LED is disabled.")
            cbBuzzLed2.Checked = False

        End If

        If (RecvBuff(5) And &H4) <> 0 Then

            Call displayOut(3, 0, "PICC Activation Status Buzzer is enabled.")
            cbBuzzLed3.Checked = True

        Else

            Call displayOut(3, 0, "PICC Activation Status Buzzer is disabled.")
            cbBuzzLed3.Checked = False

        End If

        If (RecvBuff(5) And &H8) <> 0 Then

            Call displayOut(3, 0, "PICC PPS Status Buzzer is enabled.")
            cbBuzzLed4.Checked = True

        Else

            Call displayOut(3, 0, "PICC PPS Status Buzzer is disabled.")
            cbBuzzLed4.Checked = False

        End If

        If (RecvBuff(5) And &H10) <> 0 Then

            Call displayOut(3, 0, "Card Insertion and Removal Events Buzzer is enabled.")
            cbBuzzLed5.Checked = True

        Else

            Call displayOut(3, 0, "Card Insertion and Removal Events Buzzer is disabled.")
            cbBuzzLed5.Checked = False

        End If

        If (RecvBuff(5) And &H20) <> 0 Then

            Call displayOut(3, 0, "RC531 Reset Indication Buzzer is enabled.")
            cbBuzzLed6.Checked = True

        Else

            Call displayOut(3, 0, "RC531 Reset Indication Buzzer is disabled.")
            cbBuzzLed6.Checked = False

        End If

        If (RecvBuff(5) And &H40) <> 0 Then

            Call displayOut(3, 0, "Exclusive Mode Status Buzzer is enabled.")
            cbBuzzLed7.Checked = True

        Else

            Call displayOut(3, 0, "Exclusive Mode Status Buzzer is disabled.")
            cbBuzzLed7.Checked = False

        End If

        If (RecvBuff(5) And &H80) <> 0 Then

            Call displayOut(3, 0, "Card Operation Blinking LED is enabled.")
            cbBuzzLed8.Checked = True

        Else

            Call displayOut(3, 0, "Card Operation Blinking LED is disabled.")
            cbBuzzLed8.Checked = False

        End If

        GetBuzzLEDState = retCode

    End Function

    Private Sub bGetBuzzState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGetBuzzState.Click

        Call GetBuzzLEDState()

    End Sub

    Private Sub bSetBuzzState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSetBuzzState.Click

        Call ClearBuffers()

        SendBuff(0) = &H21
        SendBuff(1) = &H1
        SendBuff(2) = &H0

        If cbBuzzLed1.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H1

        End If

        If cbBuzzLed2.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H2

        End If

        If cbBuzzLed3.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H4

        End If

        If cbBuzzLed4.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H8

        End If

        If cbBuzzLed5.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H10

        End If

        If cbBuzzLed6.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H20

        End If

        If cbBuzzLed7.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H40

        End If

        If cbBuzzLed8.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H80

        End If

        SendLen = 3
        RecvLen = 6
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

    End Sub

    Private Function CallCardControl() As Integer

        Dim tmpStr As String
        Dim indx As Integer

        ' Display Apdu In
        tmpStr = "SCardControl: "

        For indx = 0 To SendLen

            tmpStr = tmpStr + Format(Hex(SendBuff(indx)), "")

        Next indx

        Call displayOut(2, 0, tmpStr)

        retCode = ModWinsCard.SCardControl(hCard, ModWinsCard.IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND, SendBuff(0), SendLen, RecvBuff(0), RecvLen, nBytesRet)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Call displayOut(1, retCode, "")

            Exit Function

        Else

            tmpStr = ""

            For indx = 0 To RecvLen - 1

                tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

            Next indx

            CallCardControl = retCode

        End If

    End Function

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

    Private Sub bGetLed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGetLed.Click

        Call GetLEDState()

    End Sub

    Private Sub bSetLed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSetLed.Click


        Call ClearBuffers()
        SendBuff(0) = &H29
        SendBuff(1) = &H1

        If cbRed.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H1

        End If

        If cbGreen.Checked = True Then

            SendBuff(2) = SendBuff(2) Or &H2

        End If

        SendLen = 3
        RecvLen = 6
        retCode = CallCardControl()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

    End Sub
End Class
