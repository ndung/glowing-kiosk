'=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                   implement the binary file support in ACOS3-24K
'  
'  Author :         Daryl M. Rojas
'
'  Module :         ModWinscard.vb
'   
'  Date   :         June 21, 2008
'
' Revision Trail:   (Date/Author/Description) 
'
'==========================================================================================

Public Class MainACOSBin

    Public retCode, hContext, hCard, Protocol As Long
    Public connActive As Boolean = False
    Public SendBuff(262), RecvBuff(262) As Byte
    Public SendLen, RecvLen, nBytesRet, Aprotocol As Integer
    Public pioSendRequest, pioRecvRequest As SCARD_IO_REQUEST
    Const INVALID_SW1SW2 = -450

    Private Sub MainACOSBin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call InitMenu()

    End Sub

    Private Sub InitMenu()

        connActive = False
        cbReader.Items.Clear()
        mMsg.Items.Clear()
        displayOut(0, 0, "Program ready")
        bInit.Enabled = True
        bConnect.Enabled = False
        bReset.Enabled = False
        tFileID1.Text = ""
        tFileID2.Text = ""
        tFileLen1.Text = ""
        tFileLen2.Text = ""
        gbFormat.Enabled = False
        tFID1.Text = ""
        tFID2.Text = ""
        tOffset1.Text = ""
        tOffset2.Text = ""
        tLen.Text = ""
        tData.Clear()
        gbReadWrite.Enabled = False

    End Sub

    Private Sub EnableButtons()

        bInit.Enabled = False
        bConnect.Enabled = True
        bReset.Enabled = True

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
        Dim ctr As Integer


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

    End Sub

    Private Function readRecord(ByVal RecNo As Byte, ByVal DataLen As Byte) As Integer

        Dim indx As Integer
        Dim tmpStr As String

        Call ClearBuffers()
        SendBuff(0) = &H80        ' CLA
        SendBuff(1) = &HB2        ' INS
        SendBuff(2) = RecNo       ' P1
        SendBuff(3) = &H0         ' P2
        SendBuff(4) = DataLen     ' P3
        SendLen = &H5
        RecvLen = SendBuff(4) + 2

        retCode = SendAPDUandDisplay(0)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            readRecord = retCode
            Exit Function
        End If

        tmpStr = ""
        For indx = 0 To 1

            tmpStr = tmpStr + " " + (Format(Hex(RecvBuff(indx + SendBuff(4))), "")) ' + Format(Hex(SendBuff(4)), ""))

        Next indx

        If tmpStr.Trim <> "90 0" Then
            displayOut(2, 0, "The return string is invalid. Value: " + tmpStr)
            retCode = INVALID_SW1SW2
        End If

        readRecord = retCode

    End Function

    Private Sub getBinaryData()

        Dim indx, tmpLen As Integer
        Dim tmpStr As String


        ' 1. Send IC Code
        retCode = SubmitIC()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            displayOut(0, 0, "Insert ACOS3-24K card on contact card reader.")
        End If

        ' 2. Select FF 04
        retCode = selectFile(&HFF, &H4)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        tmpStr = ""
        For indx = 0 To 1

            tmpStr = tmpStr + " " + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If Trim(tmpStr) <> "90 0" Then

            displayOut(4, 0, "The return string is invalid. Value: " + tmpStr)
            retCode = INVALID_SW1SW2
            Exit Sub

        End If

        ' 3. Read first record
        retCode = readRecord(&H0, &H7)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            displayOut(0, 0, "Card may not have been formatted yet.")
            Exit Sub
        End If

        ' Provide parameter to Data Input Box
        tFID1.Text = Format(Hex(RecvBuff(4)), "")
        tFID2.Text = Format(Hex(RecvBuff(5)), "")
        tmpLen = RecvBuff(1)
        tmpLen = tmpLen + (RecvBuff(0) * 256)
        tData.MaxLength = tmpLen

    End Sub

    Private Sub bConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bConnect.Click

        ' Connect to selected reader using hContext handle and obtain hCard handle
        If connActive Then

            displayOut(0, 0, "Connection is already active.")

        End If

        displayOut(2, 0, "Invoke SCardConnect")

        ' 1. Connect to selected reader using hContext handle and obtain valid hCard handle
        retCode = ModWinsCard.SCardConnect(hContext, cbReader.Text, ModWinsCard.SCARD_SHARE_SHARED, ModWinsCard.SCARD_PROTOCOL_T0 Or ModWinsCard.SCARD_PROTOCOL_T1, hCard, Protocol)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Call displayOut(1, retCode, "")
            connActive = False
            Exit Sub

        Else

            Call displayOut(0, 0, "Successful connection to " & cbReader.Text)

        End If

        connActive = True
        gbFormat.Enabled = True
        gbReadWrite.Enabled = True
        getBinaryData()

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

                Case 0          ' Display SW1/SW2 value

                    For indx = (RecvLen - 2) To (RecvLen - 1)

                        tmpStr = tmpStr + " " + Format(Hex(RecvBuff(indx)), "")

                    Next indx

                    If tmpStr.Trim <> "90 0" Then

                        displayOut(0, 0, "Return bytes are not acceptable.")

                    End If

                Case 1      ' Display ATR after checking SW1/SW2

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

                Case 2      ' Display all data

                    For indx = 0 To (RecvLen - 1)

                        tmpStr = tmpStr + " " + Format(Hex(RecvBuff(indx)), "")

                    Next indx

            End Select

            displayOut(3, 0, tmpStr.Trim())

        End If

        SendAPDUandDisplay = retCode

    End Function

    Private Function SubmitIC() As Integer

        Dim indx As Integer
        Dim tmpStr As String

        Call ClearBuffers()
        SendBuff(0) = &H80     ' CLA
        SendBuff(1) = &H20     ' INS
        SendBuff(2) = &H7      ' P1
        SendBuff(3) = &H0      ' P2
        SendBuff(4) = &H8      ' P3
        SendBuff(5) = &H41     ' A
        SendBuff(6) = &H43     ' C
        SendBuff(7) = &H4F     ' O
        SendBuff(8) = &H53     ' S
        SendBuff(9) = &H54     ' T
        SendBuff(10) = &H45    ' E
        SendBuff(11) = &H53    ' S
        SendBuff(12) = &H54    ' T
        SendLen = &HD
        RecvLen = &H2

        retCode = SendAPDUandDisplay(0)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            SubmitIC = retCode
            Exit Function

        End If

        tmpStr = ""
        For indx = 0 To 1

            tmpStr = tmpStr + " " + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If tmpStr.Trim() <> "90 0" Then

            displayOut(2, 0, "The return string is invalid. Value: " + tmpStr)
            retCode = INVALID_SW1SW2
            SubmitIC = retCode
            Exit Function

        End If

        SubmitIC = retCode

    End Function

    Private Function selectFile(ByVal HiAddr As Byte, ByVal LoAddr As Byte) As Integer

        Call ClearBuffers()
        SendBuff(0) = &H80       ' CLA
        SendBuff(1) = &HA4       ' INS
        SendBuff(2) = &H0        ' P1
        SendBuff(3) = &H0        ' P2
        SendBuff(4) = &H2        ' P3
        SendBuff(5) = HiAddr     ' Value of High Byte
        SendBuff(6) = LoAddr     ' Value of Low Byte
        SendLen = &H7
        RecvLen = &H2

        retCode = SendAPDUandDisplay(2)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            selectFile = retCode
            Exit Function

        End If
        selectFile = retCode

    End Function

    Private Sub bFormat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bFormat.Click

        Dim indx As Integer
        Dim tmpStr As String
        Dim tmpArray(0 To 30) As Byte
        Dim tmpLong As Byte

        ' Validate Inputs
        If (tFileID1.Text = "" Or Not Byte.TryParse(tFileID1.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tFileID1.Focus()
            tFileID1.Text = ""
            Exit Sub

        End If

        If (tFileID2.Text = "" Or Not Byte.TryParse(tFileID2.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tFileID2.Focus()
            tFileID2.Text = ""
            Exit Sub

        End If

        If (tFileLen2.Text = "" Or Not Byte.TryParse(tFileLen2.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tFileLen2.Focus()
            tFileLen2.Text = ""
            Exit Sub

        End If

        ' Send ICC Code
        retCode = SubmitIC()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            displayOut(0, 0, "Insert ACOS3-24K card on contact card reader.")
            Exit Sub

        End If

        ' 2. Select FF 02
        retCode = selectFile(&HFF, &H2)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        tmpStr = ""
        For indx = 0 To 1

            tmpStr = tmpStr + " " + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If tmpStr.Trim() <> "90 0" Then

            Call displayOut(2, 0, "The return string is invalid. Value: " + tmpStr)
            retCode = INVALID_SW1SW2
            Exit Sub

        End If

        '3. Write to FF 02
        ' This will create 1 binary file, no Option registers and
        ' Security Option registers defined, Personalization bit is not set

        tmpArray(0) = &H0    ' 00    Option registers
        tmpArray(1) = &H0    ' 00    Security option register
        tmpArray(2) = &H1    ' 01    No of user files
        tmpArray(3) = &H0    ' 00    Personalization bit
        retCode = writeRecord(0, &H0, &H4, &H4, tmpArray)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        Call displayOut(0, 0, "File FF 02 is updated.")

        ' 4. Perform a reset for changes in the ACOS3 to take effect
        connActive = False
        retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            displayOut(0, retCode, "")
            Exit Sub

        End If

        retCode = ModWinsCard.SCardConnect(hContext, cbReader.Text, ModWinsCard.SCARD_SHARE_SHARED, ModWinsCard.SCARD_PROTOCOL_T0 Or ModWinsCard.SCARD_PROTOCOL_T1, hCard, Protocol)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            displayOut(0, retCode, "")
            Exit Sub

        End If

        Call displayOut(3, 0, "Card reset is successful.")
        connActive = True

        ' 5. Select FF 04
        retCode = selectFile(&HFF, &H4)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        tmpStr = ""
        For indx = 0 To 1

            tmpStr = tmpStr + " " + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If tmpStr.Trim() <> "90 0" Then

            Call displayOut(2, 0, "The return string is invalid. Value: " + tmpStr)
            retCode = INVALID_SW1SW2
            Exit Sub

        End If

        ' 6. Send IC Code
        retCode = SubmitIC()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        ' 7. Write to FF 04
        ' Write to first record of FF 04
        If tFileLen1.Text = "" Then

            tmpArray(0) = &H0                              ' File Length: High Byte

        Else

            tmpArray(0) = CLng("&H" + tFileLen1.Text)      ' File Length: High Byte

        End If

        tmpArray(1) = CLng("&H" + tFileLen2.Text)           ' File Length: Low Byte
        tmpArray(2) = &H0                                   ' 00    Read security attribute
        tmpArray(3) = &H0                                   ' 00    Write security attribute
        tmpArray(4) = CLng("&H" + tFileID1.Text)            ' File identifier
        tmpArray(5) = CLng("&H" + tFileID2.Text)            ' File identifier
        tmpArray(6) = &H80                                  ' File Access Flag: Binary File Type
        retCode = writeRecord(0, &H0, &H7, &H7, tmpArray)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        tmpStr = ""
        tmpStr = tFileID1.Text + " " + tFileID2.Text
        Call displayOut(0, 0, "User File " + tmpStr + " is defined.")

    End Sub

    Private Function readBinary(ByVal HiByte As Byte, ByVal LoByte As Byte, ByVal DataLen As Byte) As Integer

        Call ClearBuffers()
        SendBuff(0) = &H80          ' CLA
        SendBuff(1) = &HB0          ' INS
        SendBuff(2) = HiByte        ' P1    High Byte Address
        SendBuff(3) = LoByte        ' P2    Low Byte Address
        SendBuff(4) = DataLen       ' P3    Length of data to be read
        SendLen = &H5
        RecvLen = SendBuff(4) + 2

        retCode = SendAPDUandDisplay(0)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            readBinary = retCode

        End If

        readBinary = retCode

    End Function

    Private Sub bBinRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBinRead.Click

        Dim indx, tmpLen As Integer
        Dim tmpStr As String
        Dim HiByte, LoByte, FileID1, FileID2 As Byte

        ' Validate Input
        If tFID1.Text = "" Then

            tFID1.Focus()
            Exit Sub

        End If

        If tFID2.Text = "" Then

            tFID2.Focus()
            Exit Sub

        End If

        If tOffset2.Text = "" Then

            tOffset2.Focus()
            Exit Sub

        End If

        If tLen.Text = "" Then

            tLen.Focus()
            Exit Sub

        End If

        Call ClearBuffers()
        FileID1 = CLng("&H" + tFID1.Text)
        FileID2 = CLng("&H" + tFID2.Text)

        If tOffset1.Text = "" Then
            HiByte = &H0
        Else
            HiByte = CLng("&H" + tOffset1.Text)
        End If

        LoByte = CLng("&H" + tOffset2.Text)
        tmpLen = CLng("&H" + tLen.Text)


        ' 1. Select User File
        retCode = selectFile(FileID1, FileID2)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        tmpStr = ""
        For indx = 0 To 1

            tmpStr = tmpStr + " " + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If tmpStr.Trim() <> "91 0" Then

            Call displayOut(2, 0, "The return string is invalid. Value: " + tmpStr)
            retCode = INVALID_SW1SW2
            Exit Sub

        End If

        ' 2. Read binary

        retCode = readBinary(HiByte, LoByte, tmpLen)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            displayOut(0, 0, "Card may not have been formatted yet.")
            Exit Sub
        End If

        tmpStr = ""
        indx = 0
        While (RecvBuff(indx) <> &H0)

            If indx < tData.MaxLength Then

                tmpStr = tmpStr + Chr(RecvBuff(indx))

            End If
            indx = indx + 1
        End While
        tData.Text = tmpStr

    End Sub

    Private Sub bBinWrite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBinWrite.Click

        Dim indx, tmpLen As Integer
        Dim tmpStr As String
        Dim HiByte, LoByte, FileID1, FileID2 As Byte
        Dim tmpArray(0 To 255) As Byte
        Dim tmpLong As Byte

        ' Validate Input
        If (tFID1.Text = "" Or Not Byte.TryParse(tFID1.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tFID1.Focus()
            tFID1.Text = ""

            Exit Sub

        End If

        If (tFID2.Text = "" Or Not Byte.TryParse(tFID2.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tFID2.Focus()
            tFID2.Text = ""
            Exit Sub

        End If

        If (tOffset2.Text = "" Or Not Byte.TryParse(tOffset2.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tOffset2.Focus()
            tOffset2.Text = ""
            Exit Sub

        End If

        If (tLen.Text = "" Or Not Byte.TryParse(tLen.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tLen.Focus()
            tLen.Text = ""
            Exit Sub

        End If

        If tData.Text = "" Then

            tData.Focus()
            Exit Sub

        End If

        Call ClearBuffers()
        FileID1 = CLng("&H" + tFID1.Text)
        FileID2 = CLng("&H" + tFID2.Text)

        If tOffset1.Text = "" Then

            HiByte = &H0
        Else

            HiByte = CLng("&H" + tOffset1.Text)

        End If

        LoByte = CLng("&H" + tOffset2.Text)
        tmpLen = CLng("&H" + tLen.Text)


        ' 1. Select User File
        retCode = selectFile(FileID1, FileID2)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        tmpStr = ""
        For indx = 0 To 1

            tmpStr = tmpStr + " " + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If tmpStr.Trim() <> "91 0" Then

            Call displayOut(2, 0, "The return string is invalid. Value: " + tmpStr)
            retCode = INVALID_SW1SW2
            Exit Sub

        End If

        ' 2. Write input data to card
        tmpStr = tData.Text
        For indx = 0 To tmpStr.Length - 1

            tmpArray(indx) = Asc(tmpStr(indx))

        Next indx

        retCode = writeBinary(1, HiByte, LoByte, tmpLen, tmpArray)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

    End Sub

    Private Function writeRecord(ByVal caseType As Integer, ByVal RecNo As Byte, ByVal maxDataLen As Byte, ByVal DataLen As Byte, ByVal DataIn() As Byte) As Integer

        Dim indx As Integer
        Dim tmpStr As String

        If caseType = 1 Then            ' If card data is to be erased before writing new data
            ' 1. Re-initialize card values to &H0
            Call ClearBuffers()
            SendBuff(0) = &H80          ' CLA
            SendBuff(1) = &HD2          ' INS
            SendBuff(2) = RecNo         ' P1   
            SendBuff(3) = &H0           ' P2   
            SendBuff(4) = maxDataLen    ' P3    Length 

            For indx = 0 To maxDataLen - 1
                SendBuff(indx + 5) = &H0
            Next indx
            SendLen = maxDataLen + 5
            RecvLen = &H2

            retCode = SendAPDUandDisplay(0)
            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

                writeRecord = retCode
                Exit Function

            End If

            tmpStr = ""
            For indx = 0 To 1

                tmpStr = tmpStr + " " + Format(Hex(RecvBuff(indx)), "")

            Next indx

            If tmpStr.Trim() <> "90 0" Then

                Call displayOut(3, 0, "The return string is invalid. Value: " + tmpStr)
                retCode = INVALID_SW1SW2
                writeRecord = retCode
                Exit Function

            End If

        End If

        ' 2. Write data to card
        Call ClearBuffers()
        SendBuff(0) = &H80          ' CLA
        SendBuff(1) = &HD2          ' INS
        SendBuff(2) = RecNo         ' P1  
        SendBuff(3) = &H0           ' P2  
        SendBuff(4) = DataLen       ' P3    Length 

        For indx = 0 To DataLen - 1

            SendBuff(indx + 5) = DataIn(indx)

        Next indx

        SendLen = DataLen + 5
        RecvLen = &H2

        retCode = SendAPDUandDisplay(0)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            writeRecord = retCode
            Exit Function

        End If

        tmpStr = ""
        For indx = 0 To 1

            tmpStr = tmpStr + " " + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If tmpStr.Trim() <> "90 0" Then

            Call displayOut(3, 0, "The return string is invalid. Value: " + tmpStr)
            retCode = INVALID_SW1SW2
            writeRecord = retCode
            Exit Function

        End If

        writeRecord = retCode

    End Function

    Function writeBinary(ByVal caseType As Integer, ByVal HiByte As Integer, ByVal LoByte As Integer, ByVal DataLen As Byte, ByVal DataIn() As Byte) As Integer

        Dim indx As Integer
        Dim tmpStr As String

        If caseType = 1 Then            ' If card data is to be erased before writing new data
            ' 1. Re-initialize card values to &H0
            Call ClearBuffers()
            SendBuff(0) = &H80          ' CLA
            SendBuff(1) = &HD0          ' INS
            SendBuff(2) = HiByte        ' P1    High Byte Address
            SendBuff(3) = LoByte        ' P2    Low Byte Address
            SendBuff(4) = DataLen       ' P3    Length of data to be read

            For indx = 0 To DataLen - 1
                SendBuff(indx + 5) = &H0
            Next indx
            SendLen = DataLen + 5
            RecvLen = &H2

            retCode = SendAPDUandDisplay(2)
            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

                writeBinary = retCode
                Exit Function

            End If

            tmpStr = ""
            For indx = 0 To 1

                tmpStr = tmpStr + " " + Format(Hex(RecvBuff(indx)), "")

            Next indx

            If tmpStr.Trim() <> "90 0" Then

                Call displayOut(3, 0, "The return string is invalid. Value: " + tmpStr)
                retCode = INVALID_SW1SW2
                Exit Function

            End If

        End If

        ' 2. Write data to card
        Call ClearBuffers()
        SendBuff(0) = &H80          ' CLA
        SendBuff(1) = &HD0          ' INS
        SendBuff(2) = HiByte        ' P1    High Byte Address
        SendBuff(3) = LoByte        ' P2    Low Byte Address
        SendBuff(4) = DataLen       ' P3    Length of data to be read

        For indx = 0 To DataLen - 1

            SendBuff(indx + 5) = DataIn(indx)

        Next indx

        SendLen = DataLen + 5
        RecvLen = &H2

        retCode = SendAPDUandDisplay(0)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            writeBinary = retCode
            Exit Function

        End If

        tmpStr = ""
        For indx = 0 To 1

            tmpStr = tmpStr + " " + Format(Hex(RecvBuff(indx)), "")

        Next indx

        If tmpStr.Trim() <> "90 0" Then

            Call displayOut(3, 0, "The return string is invalid. Value: " + tmpStr)
            retCode = INVALID_SW1SW2
            writeBinary = retCode
            Exit Function

        End If

        writeBinary = retCode

    End Function

End Class
