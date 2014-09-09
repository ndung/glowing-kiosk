'=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                   transact with Mifare 1K/4K cards using ACR128
'  
'  Author :         Daryl M. Rojas
'
'  Module :         ModWinscard.vb
'   
'  Date   :         June 18, 2008
'
'  Revision Trail:  (Date/Author/Description) 
'
'=========================================================================================

Public Class MainMifareProg

    Public retCode, hContext, hCard, Protocol As Long
    Public connActive, autoDet As Boolean
    Public SendBuff(262), RecvBuff(262) As Byte
    Public SendLen, RecvLen, nBytesRet, reqType, Aprotocol As Integer
    Public dwProtocol As Integer
    Public cbPciLength As Integer
    Public RdrState As SCARD_READERSTATE
    Public dwState, dwActProtocol As Long
    Public pioSendRequest, pioRecvRequest As SCARD_IO_REQUEST

    Private Sub MainMifareProg_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call InitMenu()

    End Sub

    Private Sub InitMenu()

        connActive = False
        cbReader.Items.Clear()
        cbReader.Text = ""
        mMsg.Items.Clear()
        displayOut(0, 0, "Program ready")
        bConnect.Enabled = False
        bInit.Enabled = True
        bReset.Enabled = False
        rbNonVolMem.Checked = False
        rbVolMem.Checked = False
        tMemAdd.Text = ""
        tKey1.Text = ""
        tKey2.Text = ""
        tKey3.Text = ""
        tKey4.Text = ""
        tKey5.Text = ""
        tKey6.Text = ""
        gbLoadKeys.Enabled = False
        tBlkNo.Text = ""
        tKeyAdd.Text = ""
        tKeyIn1.Text = ""
        tKeyIn2.Text = ""
        tKeyIn3.Text = ""
        tKeyIn4.Text = ""
        tKeyIn5.Text = ""
        tKeyIn6.Text = ""
        gbAuth.Enabled = False
        tBinBlk.Text = ""
        tBinLen.Text = ""
        tBinData.Text = ""
        gbBinOps.Enabled = False
        tValAmt.Text = ""
        tValBlk.Text = ""
        tValSrc.Text = ""
        tValTar.Text = ""
        gbValBlk.Enabled = False

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

    Private Sub EnableButtons()

        bInit.Enabled = False
        bConnect.Enabled = True
        bReset.Enabled = True
        bClear.Enabled = True

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

        ' Look for ACR128 PICC and make it the default reader in the combobox
        For indx = 1 To cbReader.Items.Count - 1

            cbReader.SelectedIndex = indx

            If InStr("ACR128 PICC", cbReader.Text) = 0 Then

                Exit Sub

            End If

        Next indx

        Exit Sub
        cbReader.SelectedIndex = 0

    End Sub

    Private Sub bConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bConnect.Click

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
        gbLoadKeys.Enabled = True
        gbAuth.Enabled = True
        gbBinOps.Enabled = True
        gbValBlk.Enabled = True


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

    Private Sub bLoadKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bLoadKey.Click

        Dim tmpLong As Byte

        ' Check for valid inputs
        If (Not (rbNonVolMem.Checked) And Not (rbVolMem.Checked)) Then

            rbNonVolMem.Focus()
            Exit Sub

        End If

        If rbNonVolMem.Checked Then

            If (tMemAdd.Text = "" Or Not Byte.TryParse(tMemAdd.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

                tMemAdd.Focus()
                tMemAdd.Text = ""
                Exit Sub

            End If

            If CLng("&H" + tMemAdd.Text) > &H1F Then

                tMemAdd.Text = "1F"
                Exit Sub

            End If

        End If

        If (tKey1.Text = "" Or Not Byte.TryParse(tKey1.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tKey1.Focus()
            tKey1.Text = ""
            Exit Sub

        End If

        If (tKey2.Text = "" Or Not Byte.TryParse(tKey2.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tKey2.Focus()
            tKey2.Text = ""
            Exit Sub

        End If

        If (tKey3.Text = "" Or Not Byte.TryParse(tKey3.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tKey3.Focus()
            tKey3.Text = ""
            Exit Sub

        End If

        If (tKey4.Text = "" Or Not Byte.TryParse(tKey4.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tKey4.Focus()
            tKey4.Text = ""
            Exit Sub

        End If

        If (tKey5.Text = "" Or Not Byte.TryParse(tKey5.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tKey5.Focus()
            tKey5.Text = ""
            Exit Sub

        End If

        If (tKey6.Text = "" Or Not Byte.TryParse(tKey6.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tKey6.Focus()
            tKey6.Text = ""
            Exit Sub

        End If

        Call ClearBuffers()
        SendBuff(0) = &HFF                              ' CLA
        SendBuff(1) = &H82                              ' INS

        If rbNonVolMem.Checked Then
            SendBuff(2) = &H20                          ' P1 : Non volatile memory
            SendBuff(3) = CLng("&H" + tMemAdd.Text)     ' P2 : Memory location

        Else

            SendBuff(2) = &H0                           ' P1 : Volatile memory
            SendBuff(3) = &H20                          ' P2 : Session Key        

        End If
        SendBuff(4) = &H6                               ' P3
        SendBuff(5) = CLng("&H" + tKey1.Text)           ' Key 1 value
        SendBuff(6) = CLng("&H" + tKey2.Text)           ' Key 2 value
        SendBuff(7) = CLng("&H" + tKey3.Text)           ' Key 3 value
        SendBuff(8) = CLng("&H" + tKey4.Text)           ' Key 4 value
        SendBuff(9) = CLng("&H" + tKey5.Text)           ' Key 5 value
        SendBuff(10) = CLng("&H" + tKey6.Text)          ' Key 6 value
        SendLen = &HB
        RecvLen = &H2

        retCode = SendAPDUandDisplay(0)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

    End Sub

    Private Function SendAPDUandDisplay(ByVal reqType As Integer) As Integer

        Dim indx As Integer
        Dim tmpStr As String

        pioSendRequest.dwProtocol = Aprotocol
        pioSendRequest.cbPciLength = Len(pioSendRequest)

        ' Display Apdu In
        tmpStr = ""
        For indx = 0 To SendLen - 1

            tmpStr = tmpStr + " " + Format(Hex(SendBuff(indx)), "")

        Next indx

        displayOut(2, 0, tmpStr)
        retCode = ModWinsCard.SCardTransmit(hCard, pioSendRequest, SendBuff(0), SendLen, pioSendRequest, RecvBuff(0), RecvLen)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            displayOut(1, retCode, "")
            SendAPDUandDisplay = retCode
            Exit Function

        Else

            tmpStr = ""
            Select Case reqType

                Case 0
                    For indx = (RecvLen - 2) To (RecvLen - 1)

                        tmpStr = tmpStr + " " + Format(Hex(RecvBuff(indx)), "")

                    Next indx

                    If Trim(tmpStr) <> "90 0" Then

                        displayOut(4, 0, "Return bytes are not acceptable.")

                    End If

                Case 1

                    For indx = (RecvLen - 2) To (RecvLen - 1)

                        tmpStr = tmpStr + " " + Format(Hex(RecvBuff(indx)), "")

                    Next indx

                    If tmpStr.Trim() <> "90 0" Then

                        tmpStr = tmpStr + " " + Format(Hex(RecvBuff(indx)), "")

                    Else

                        tmpStr = "ATR : "
                        For indx = 0 To (RecvLen - 3)

                            tmpStr = tmpStr + " " + Format(Hex(RecvBuff(indx)), "")

                        Next indx

                    End If

                Case 2

                    For indx = 0 To (RecvLen - 1)

                        tmpStr = tmpStr + " " + Format(Hex(RecvBuff(indx)), "")

                    Next indx

            End Select

            displayOut(3, 0, tmpStr.Trim())

        End If

        SendAPDUandDisplay = retCode

    End Function

    Private Sub bAuth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bAuth.Click

        Dim tmpLong As Byte

        ' Validate input
        If tBlkNo.Text = "" Then

            tBlkNo.Focus()
            Exit Sub

        End If

        If CInt(tBlkNo.Text) > 319 Then

            tBlkNo.Text = "319"

        End If

        If rbSource1.Checked = True Then

            If (tKeyIn1.Text = "" Or Not Byte.TryParse(tKeyIn1.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

                tKeyIn1.Focus()
                tKeyIn1.Text = ""
                Exit Sub

            End If

            If (tKeyIn2.Text = "" Or Not Byte.TryParse(tKeyIn2.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

                tKeyIn2.Focus()
                tKeyIn2.Text = ""
                Exit Sub

            End If

            If (tKeyIn3.Text = "" Or Not Byte.TryParse(tKeyIn3.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

                tKeyIn3.Focus()
                tKeyIn3.Text = ""
                Exit Sub

            End If

            If (tKeyIn4.Text = "" Or Not Byte.TryParse(tKeyIn4.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

                tKeyIn4.Focus()
                tKeyIn4.Text = ""
                Exit Sub

            End If

            If (tKeyIn5.Text = "" Or Not Byte.TryParse(tKeyIn5.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

                tKeyIn5.Focus()
                tKeyIn5.Text = ""
                Exit Sub

            End If

        Else

            If rbSource3.Checked = True Then

                If (tKeyAdd.Text = "" Or Not Byte.TryParse(tKeyAdd.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

                    tKeyAdd.Focus()
                    tKeyAdd.Text = ""
                    Exit Sub

                End If

                If CLng("&H" + tKeyAdd.Text) > &H1F Then

                    tKeyAdd.Text = "&H1F"

                End If

            End If

        End If

        Call ClearBuffers()
        SendBuff(0) = &HFF                                          ' CLA
        SendBuff(1) = &H0                                           ' P1 : Same for all source type

        If rbSource1.Checked = True Then

            SendBuff(1) = &H88                                      ' INS : for manual key input
            SendBuff(3) = CInt(tBlkNo.Text)                         ' P2  : Sector No. for manual key input

            If rbKType1.Checked = True Then

                SendBuff(4) = &H60                                  ' P3  : Key A for manual key input
            Else
                SendBuff(4) = &H61                                  ' P3  : Key B for manual key input

            End If

            SendBuff(5) = CLng("&H" + tKeyIn1.Text)                 ' Key 1
            SendBuff(6) = CLng("&H" + tKeyIn2.Text)                 ' Key 2
            SendBuff(7) = CLng("&H" + tKeyIn3.Text)                 ' Key 3
            SendBuff(8) = CLng("&H" + tKeyIn4.Text)                 ' Key 4
            SendBuff(9) = CLng("&H" + tKeyIn5.Text)                 ' Key 5
            SendBuff(10) = CLng("&H" + tKeyIn6.Text)                ' Key 6

        Else

            SendBuff(1) = &H86                                      ' INS : for stored key input
            SendBuff(3) = &H0                                       ' P2  : for stored key input
            SendBuff(4) = &H5                                       ' P3  : for stored key input
            SendBuff(5) = &H1                                       ' Byte 1 : Version no.
            SendBuff(6) = &H0                                       ' Byte 2
            SendBuff(7) = CInt(tBlkNo.Text)                         ' Byte 3 : Sector No. for stored key input

            If rbKType1.Checked = True Then

                SendBuff(8) = &H60                                  ' Byte 4 : Key A for stored key input

            Else

                SendBuff(8) = &H61                                  ' Byte 4 : Key B for stored key input
            End If

            If rbSource2.Checked = True Then

                SendBuff(9) = &H20                                  ' Byte 5 : Session key for volatile memory

            Else

                SendBuff(9) = CLng("&H" + tKeyAdd.Text)             ' Byte 5 : Key No. for non-volatile memory

            End If

        End If

        If rbSource1.Checked = True Then

            SendLen = &HB

        Else

            SendLen = &HA
        End If

        RecvLen = &H2

        retCode = SendAPDUandDisplay(0)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

    End Sub

    Private Sub bBinRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBinRead.Click

        Dim tmpStr As String
        Dim indx As Integer

        ' Validate Inputs
        tBinData.Text = ""

        If tBinBlk.Text = "" Then

            tBinBlk.Focus()
            Exit Sub

        End If

        If CInt(tBinBlk.Text) > 319 Then

            tBinBlk.Text = "319"
            Exit Sub

        End If

        If tBinLen.Text = "" Then

            tBinLen.Focus()
            Exit Sub

        End If

        Call ClearBuffers()
        SendBuff(0) = &HFF                                          ' CLA
        SendBuff(1) = &HB0                                          ' INS
        SendBuff(2) = &H0                                           ' P1
        SendBuff(3) = CInt(tBinBlk.Text)                            ' P2 : Starting Block No.
        SendBuff(4) = CInt(tBinLen.Text)                            ' P3 : Data Length

        SendLen = &H5
        RecvLen = SendBuff(4) + 2

        retCode = SendAPDUandDisplay(2)


        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        ' Display data in text format

        tmpStr = ""


        For indx = 0 To RecvLen - 1

            tmpStr = tmpStr + Chr(RecvBuff(indx))

        Next indx

        tBinData.Text = tmpStr

    End Sub

    Private Sub bBinUpd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBinUpd.Click

        Dim tmpStr As String
        Dim indx, tempInt As Integer

        ' Validate inputs
        If (tBinBlk.Text = "" Or Not Integer.TryParse(tBinBlk.Text, tempInt)) Then

            tBinBlk.Focus()
            tBinBlk.Text = ""
            Exit Sub

        End If

        If CInt(tBinBlk.Text) > 319 Then

            tBinBlk.Text = "319"
            Exit Sub

        End If

        If (tBinLen.Text = "" Or Not Integer.TryParse(tBinLen.Text, tempInt)) Then

            tBinLen.Focus()
            tBinLen.Text = ""
            Exit Sub

        End If

        If tBinData.Text = "" Then

            tBinData.Focus()
            Exit Sub

        End If


        tmpStr = tBinData.Text
        Call ClearBuffers()
        SendBuff(0) = &HFF                                          ' CLA
        SendBuff(1) = &HD6                                          ' INS
        SendBuff(2) = &H0                                           ' P1
        SendBuff(3) = CInt(tBinBlk.Text)                            ' P2 : Starting Block No.
        SendBuff(4) = CInt(tBinLen.Text)                            ' P3 : Data Length

        For indx = 0 To Len(tmpStr) - 1

            SendBuff(indx + 5) = Asc(tmpStr(indx))

        Next indx
        SendLen = SendBuff(4) + 5
        RecvLen = &H2

        retCode = SendAPDUandDisplay(2)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

    End Sub

    Private Sub bValStor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bValStor.Click

        Dim Amount As Long
        Dim tempInt As Integer

        ' Validate inputs
        If (tValAmt.Text = "" Or Not Integer.TryParse(tValAmt.Text, tempInt)) Then

            tValAmt.Focus()
            tValAmt.Text = ""
            Exit Sub

        End If

        If Convert.ToInt64(tValAmt.Text) > 4294967295 Then

            tValAmt.Text = "4294967295"
            tValAmt.Focus()
            Exit Sub

        End If

        If (tValBlk.Text = "" Or Not Integer.TryParse(tValBlk.Text, tempInt)) Then

            tValBlk.Focus()
            tValBlk.Text = ""
            Exit Sub

        End If

        If CInt(tValBlk.Text) > 319 Then

            tValBlk.Text = "319"
            Exit Sub

        End If

        tValSrc.Text = ""
        tValTar.Text = ""

        Amount = Convert.ToInt64(tValAmt.Text)
        Call ClearBuffers()
        SendBuff(0) = &HFF                                      ' CLA
        SendBuff(1) = &HD7                                      ' INS
        SendBuff(2) = &H0                                       ' P1
        SendBuff(3) = CInt(tValBlk.Text)                        ' P2 : Block No.    
        SendBuff(4) = &H5                                       ' Lc : Data length
        SendBuff(5) = &H0                                       ' VB_OP Value
        SendBuff(6) = (Amount >> 24) And &HFF                   ' Amount MSByte
        SendBuff(7) = (Amount >> 16) And &HFF                   ' Amount middle byte
        SendBuff(8) = (Amount >> 8) And &HFF                    ' Amount middle byte
        SendBuff(9) = Amount And &HFF                           ' Amount LSByte

        SendLen = SendBuff(4) + 5
        RecvLen = &H2

        retCode = SendAPDUandDisplay(2)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

    End Sub

    Private Sub bValRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bValRead.Click

        Dim Amount As Long

        If CInt(tValBlk.Text) > 319 Then

            tValBlk.Text = "319"
            Exit Sub

        End If

        tValAmt.Text = ""
        tValSrc.Text = ""
        tValTar.Text = ""

        Call ClearBuffers()
        SendBuff(0) = &HFF                                  ' CLA
        SendBuff(1) = &HB1                                  ' INS
        SendBuff(2) = &H0                                   ' P1
        SendBuff(3) = CInt(tValBlk.Text)                    ' P2 : Block No.
        SendBuff(4) = &H0                                   ' Le

        SendLen = &H5
        RecvLen = &H6

        retCode = SendAPDUandDisplay(2)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        Amount = RecvBuff(3)
        Amount = Amount + (RecvBuff(2) * 256)
        Amount = Amount + (RecvBuff(1) * 256 * 256)
        Amount = Amount + (RecvBuff(0) * 256 * 256 * 256)
        tValAmt.Text = CInt(Amount)

    End Sub

    Private Sub bValInc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bValInc.Click

        Dim Amount As Long
        Dim tempInt As Integer

        If (tValAmt.Text = "" Or Not Integer.TryParse(tValAmt.Text, tempInt)) Then

            tValAmt.Focus()
            tValAmt.Text = ""
            Exit Sub

        End If

        If Convert.ToInt64(tValAmt.Text) > 4294967295 Then

            tValAmt.Text = "4294967295"
            tValAmt.Focus()
            Exit Sub

        End If

        If (tValBlk.Text = "" Or Not Integer.TryParse(tValBlk.Text, tempInt)) Then

            tValBlk.Focus()
            tValBlk.Text = ""
            Exit Sub

        End If

        If CInt(tValBlk.Text) > 319 Then

            tValBlk.Text = "319"
            Exit Sub

        End If

        tValSrc.Text = ""
        tValTar.Text = ""

        Amount = Convert.ToInt64(tValAmt.Text)
        Call ClearBuffers()
        SendBuff(0) = &HFF                                  ' CLA
        SendBuff(1) = &HD7                                  ' INS
        SendBuff(2) = &H0                                   ' P1
        SendBuff(3) = CInt(tValBlk.Text)                    ' P2 : Block No.
        SendBuff(4) = &H5                                   ' Lc : Data length
        SendBuff(5) = &H1                                   ' VB_OP Value
        SendBuff(6) = (Amount >> 24) And &HFF               ' Amount MSByte
        SendBuff(7) = (Amount >> 16) And &HFF               ' Amount middle byte
        SendBuff(8) = (Amount >> 8) And &HFF                ' Amount middle byte
        SendBuff(9) = Amount And &HFF                       ' Amount LSByte

        SendLen = SendBuff(4) + 5
        RecvLen = &H2

        retCode = SendAPDUandDisplay(2)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

    End Sub

    Private Sub bValDec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bValDec.Click

        Dim Amount As Long
        Dim tempInt As Integer

        If (tValAmt.Text = "" Or Not Integer.TryParse(tValAmt.Text, tempInt)) Then

            tValAmt.Focus()
            tValAmt.Text = ""
            Exit Sub

        End If

        If Convert.ToInt64(tValAmt.Text) > 4294967295 Then

            tValAmt.Text = "4294967295"
            tValAmt.Focus()
            Exit Sub

        End If

        If (tValBlk.Text = "" Or Not Integer.TryParse(tValBlk.Text, tempInt)) Then

            tValBlk.Focus()
            tValBlk.Text = ""
            Exit Sub

        End If

        If CInt(tValBlk.Text) > 319 Then

            tValBlk.Text = "319"
            Exit Sub

        End If

        tValSrc.Text = ""
        tValTar.Text = ""

        Amount = Convert.ToInt64(tValAmt.Text)
        Call ClearBuffers()
        SendBuff(0) = &HFF                                  ' CLA
        SendBuff(1) = &HD7                                  ' INS
        SendBuff(2) = &H0                                   ' P1
        SendBuff(3) = CInt(tValBlk.Text)                    ' P2 : Block No.
        SendBuff(4) = &H5                                   ' Lc : Data length
        SendBuff(5) = &H2                                   ' VB_OP Value
        SendBuff(6) = (Amount >> 24) And &HFF               ' Amount MSByte
        SendBuff(7) = (Amount >> 16) And &HFF               ' Amount middle byte
        SendBuff(8) = (Amount >> 8) And &HFF                ' Amount middle byte
        SendBuff(9) = Amount And &HFF                       ' Amount LSByte

        SendLen = SendBuff(4) + 5
        RecvLen = &H2

        retCode = SendAPDUandDisplay(2)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

    End Sub

    Private Sub bValRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bValRes.Click

        Dim tempInt As Integer

        ' Validate inputs
        If (tValSrc.Text = "" Or Not Integer.TryParse(tValSrc.Text, tempInt)) Then

            tValSrc.Focus()
            tValSrc.Text = ""
            Exit Sub

        End If

        If (tValTar.Text = "" Or Not Integer.TryParse(tValTar.Text, tempInt)) Then

            tValTar.Focus()
            tValTar.Text = ""
            Exit Sub

        End If

        If CInt(tValSrc.Text) > 319 Then

            tValSrc.Text = "319"
            Exit Sub

        End If

        If CInt(tValTar.Text) > 319 Then

            tValTar.Text = "319"
            Exit Sub

        End If

        tValAmt.Text = ""
        tValBlk.Text = ""

        Call ClearBuffers()
        SendBuff(0) = &HFF                                  ' CLA
        SendBuff(1) = &HD7                                  ' INS
        SendBuff(2) = &H0                                   ' P1
        SendBuff(3) = CInt(tValSrc.Text)                    ' P2 : Source Block No.
        SendBuff(4) = &H2                                   ' Lc
        SendBuff(5) = &H3                                   ' Data In Byte 1
        SendBuff(6) = CInt(tValTar.Text)                    ' P2 : Target Block No.

        SendLen = &H7
        RecvLen = &H2

        retCode = SendAPDUandDisplay(2)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

    End Sub

    Private Sub rbVolMem_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbVolMem.CheckedChanged

        tMemAdd.Text = ""
        tMemAdd.Enabled = False

    End Sub

    Private Sub rbNonVolMem_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbNonVolMem.CheckedChanged

        tMemAdd.Enabled = True

    End Sub

    Private Sub rbSource1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSource1.CheckedChanged

        If rbSource1.Checked = True Then

            tBlkNo.Enabled = True
            tKeyAdd.Enabled = False
            tKeyIn1.Enabled = True
            tKeyIn2.Enabled = True
            tKeyIn3.Enabled = True
            tKeyIn4.Enabled = True
            tKeyIn5.Enabled = True
            tKeyIn6.Enabled = True
            Exit Sub

        End If

    End Sub

    Private Sub rbSource2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSource2.CheckedChanged

        If rbSource2.Checked = True Then

            tBlkNo.Enabled = True
            tKeyAdd.Enabled = False
            tKeyIn1.Enabled = False
            tKeyIn2.Enabled = False
            tKeyIn3.Enabled = False
            tKeyIn4.Enabled = False
            tKeyIn5.Enabled = False
            tKeyIn6.Enabled = False
            Exit Sub

        End If

    End Sub

    Private Sub rbSource3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSource3.CheckedChanged

        If rbSource3.Checked = True Then

            tBlkNo.Enabled = True
            tKeyAdd.Enabled = True
            tKeyIn1.Enabled = False
            tKeyIn2.Enabled = False
            tKeyIn3.Enabled = False
            tKeyIn4.Enabled = False
            tKeyIn5.Enabled = False
            tKeyIn6.Enabled = False
            Exit Sub

        End If

    End Sub

    Private Sub tBinLen_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tBinLen.TextChanged

        Dim numbyte As Byte

        If Byte.TryParse(tBinLen.Text, numbyte) Then

            tBinData.MaxLength = numbyte

        End If

    End Sub

End Class


