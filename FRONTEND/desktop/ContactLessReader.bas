Attribute VB_Name = "ContactCardReader"
Global Const ALGO_DES = 0
Global Const ALGO_3DES = 1
Global Const ALGO_XOR = 3
Global Const DATA_ENCRYPT = 1
Global Const DATA_DECRYPT = 2

Public Declare Function Chain_DES Lib "chaindes.dll" (ByRef data As Any, ByRef key As Any, ByVal TripleDES As Integer, ByVal Blocks As Long, ByVal method As Long) As Long
Public Declare Function Chain_MAC Lib "chaindes.dll" (ByRef mac As Any, ByRef data As Any, ByRef key As Any, ByVal Blocks As Long) As Long
Public Declare Function Chain_MAC2 Lib "chaindes.dll" (ByRef mac As Any, ByRef data As Any, ByRef key As Any, ByVal Blocks As Long) As Long

Public Type SCARD_IO_REQUEST
    dwProtocol As Long
    cbPciLength As Long
End Type

Public Type APDURec
    bCLA As Byte
    bINS As Byte
    bP1 As Byte
    bP2 As Byte
    bP3 As Byte
    DataIn(1 To 255) As Byte
    DataOut(1 To 255) As Byte
    SW(1 To 2) As Byte
    IsSend As Boolean
End Type

Public Type SCARD_READERSTATE
    rdrname As String
    UserData As Long
    RdrCurrState As Long
    RdrEventState As Long
    ATRLength As Long
    ATRValue(1 To 36) As Byte
End Type

Global Const SCARD_S_SUCCESS = 0
Global Const SCARD_ATR_LENGTH = 33
Global Const CT_MCU = &H0                     ' MCU
Global Const CT_IIC_Auto = &H1                ' IIC (Auto Detect Memory Size)
Global Const CT_IIC_1K = &H2                  ' IIC (1K)
Global Const CT_IIC_2K = &H3                  ' IIC (2K)
Global Const CT_IIC_4K = &H4                  ' IIC (4K)
Global Const CT_IIC_8K = &H5                  ' IIC (8K)
Global Const CT_IIC_16K = &H6                 ' IIC (16K)
Global Const CT_IIC_32K = &H7                 ' IIC (32K)
Global Const CT_IIC_64K = &H8                 ' IIC (64K)
Global Const CT_IIC_128K = &H9                ' IIC (128K)
Global Const CT_IIC_256K = &HA                ' IIC (256K)
Global Const CT_IIC_512K = &HB                ' IIC (512K)
Global Const CT_IIC_1024K = &HC               ' IIC (1024K)
Global Const CT_AT88SC153 = &HD               ' AT88SC153
Global Const CT_AT88SC1608 = &HE              ' AT88SC1608
Global Const CT_SLE4418 = &HF                 ' SLE4418
Global Const CT_SLE4428 = &H10                ' SLE4428
Global Const CT_SLE4432 = &H11                ' SLE4432
Global Const CT_SLE4442 = &H12                ' SLE4442
Global Const CT_SLE4406 = &H13                ' SLE4406
Global Const CT_SLE4436 = &H14                ' SLE4436
Global Const CT_SLE5536 = &H15                ' SLE5536
Global Const CT_MCUT0 = &H16                  ' MCU T=0
Global Const CT_MCUT1 = &H17                  ' MCU T=1
Global Const CT_MCU_Auto = &H18               ' MCU Autodetect

Global Const SCARD_SCOPE_USER = 0 ' The context is a user context, and any
                                  ' database operations are performed within the
                                  ' domain of the user.
Global Const SCARD_SCOPE_TERMINAL = 1 ' The context is that of the current terminal,
                                      ' and any database operations are performed
                                      ' within the domain of that terminal.  (The
                                      ' calling application must have appropriate
                                      ' access permissions for any database actions.)
Global Const SCARD_SCOPE_SYSTEM = 2 ' The context is the system context, and any
                                    ' database operations are performed within the
                                    ' domain of the system.  (The calling
                                    ' application must have appropriate access
                                    ' permissions for any database actions.)

Global Const SCARD_STATE_UNAWARE = &H0 ' The application is unaware of the
                                            ' current state, and would like to
                                            ' know.  The use of this value
                                            ' results in an immediate return
                                            ' from state transition monitoring
                                            ' services.  This is represented by
                                            ' all bits set to zero.
Global Const SCARD_STATE_IGNORE = &H1 ' The application requested that
                                            ' this reader be ignored.  No other
                                            ' bits will be set.
Global Const SCARD_STATE_CHANGED = &H2 ' This implies that there is a
                                            ' difference between the state
                                            ' believed by the application, and
                                            ' the state known by the Service
                                            ' Manager.  When this bit is set,
                                            ' the application may assume a
                                            ' significant state change has
                                            ' occurred on this reader.

Global Const SCARD_STATE_UNKNOWN = &H4 ' This implies that the given
                                            ' reader name is not recognized by
                                            ' the Service Manager.  If this bit
                                            ' is set, then SCARD_STATE_CHANGED
                                            ' and SCARD_STATE_IGNORE will also
                                            ' be set.
Global Const SCARD_STATE_UNAVAILABLE = &H8 ' This implies that the actual
                                            ' state of this reader is not
                                            ' available.  If this bit is set,
                                            ' then all the following bits are
                                            ' clear.
Global Const SCARD_STATE_EMPTY = &H10 ' This implies that there is not
                                            ' card in the reader.  If this bit
                                            ' is set, all the following bits
                                            ' will be clear.
Global Const SCARD_STATE_PRESENT = &H20 ' This implies that there is a card
                                            ' in the reader.
Global Const SCARD_STATE_ATRMATCH = &H40 ' This implies that there is a card
                                            ' in the reader with an ATR
                                            ' matching one of the target cards.
                                            ' If this bit is set,
                                            ' SCARD_STATE_PRESENT will also be
                                            ' set.  This bit is only returned
                                            ' on the SCardLocateCard() service.
Global Const SCARD_STATE_EXCLUSIVE = &H80 ' This implies that the card in the
                                            ' reader is allocated for exclusive
                                            ' use by another application.  If
                                            ' this bit is set,
                                            ' SCARD_STATE_PRESENT will also be
                                            ' set.
Global Const SCARD_STATE_INUSE = &H100 ' This implies that the card in the
                                            ' reader is in use by one or more
                                            ' other applications, but may be
                                            ' connected to in shared mode.  If
                                            ' this bit is set,
                                            ' SCARD_STATE_PRESENT will also be
                                            ' set.
Global Const SCARD_STATE_MUTE = &H200 ' This implies that the card in the
                                            ' reader is unresponsive or not
                                            ' supported by the reader or
                                            ' software.
Global Const SCARD_STATE_UNPOWERED = &H400 ' This implies that the card in the
                                            ' reader has not been powered up.


Global Const SCARD_SHARE_EXCLUSIVE = 1 ' This application is not willing to share this
                                ' card with other applications.
Global Const SCARD_SHARE_SHARED = 2 ' This application is willing to share this
                                ' card with other applications.
Global Const SCARD_SHARE_DIRECT = 3 ' This application demands direct control of
                                ' the reader, so it is not available to other
                                ' applications.

Global Const SCARD_LEAVE_CARD = 0 ' Don't do anything special on close
Global Const SCARD_RESET_CARD = 1 ' Reset the card on close
Global Const SCARD_UNPOWER_CARD = 2 ' Power down the card on close
Global Const SCARD_EJECT_CARD = 3 ' Eject the card on close

Public Const FILE_DEVICE_SMARTCARD      As Long = &H310000

Public Const IOCTL_SMARTCARD_DIRECT           As Long = FILE_DEVICE_SMARTCARD + 2050 * 4
Public Const IOCTL_SMARTCARD_SELECT_SLOT   As Long = FILE_DEVICE_SMARTCARD + 2051 * 4
Public Const IOCTL_SMARTCARD_DRAW_LCDBMP   As Long = FILE_DEVICE_SMARTCARD + 2052 * 4
Public Const IOCTL_SMARTCARD_DISPLAY_LCD      As Long = FILE_DEVICE_SMARTCARD + 2053 * 4
Public Const IOCTL_SMARTCARD_CLR_LCD        As Long = FILE_DEVICE_SMARTCARD + 2054 * 4
Public Const IOCTL_SMARTCARD_READ_KEYPAD           As Long = FILE_DEVICE_SMARTCARD + 2055 * 4
Public Const IOCTL_SMARTCARD_READ_RTC         As Long = FILE_DEVICE_SMARTCARD + 2057 * 4
Public Const IOCTL_SMARTCARD_SET_RTC      As Long = FILE_DEVICE_SMARTCARD + 2058 * 4
Public Const IOCTL_SMARTCARD_SET_OPTION       As Long = FILE_DEVICE_SMARTCARD + 2059 * 4
Public Const IOCTL_SMARTCARD_SET_LED       As Long = FILE_DEVICE_SMARTCARD + 2060 * 4
Public Const IOCTL_SMARTCARD_LOAD_KEY    As Long = FILE_DEVICE_SMARTCARD + 2062 * 4
Public Const IOCTL_SMARTCARD_READ_EEPROM       As Long = FILE_DEVICE_SMARTCARD + 2065 * 4
Public Const IOCTL_SMARTCARD_WRITE_EEPROM       As Long = FILE_DEVICE_SMARTCARD + 2066 * 4
Public Const IOCTL_SMARTCARD_GET_VERSION  As Long = FILE_DEVICE_SMARTCARD + 2067 * 4
Public Const IOCTL_SMARTCARD_GET_READER_INFO  As Long = FILE_DEVICE_SMARTCARD + 2051 * 4
Public Const IOCTL_SMARTCARD_SET_CARD_TYPE  As Long = FILE_DEVICE_SMARTCARD + 2060 * 4
Public Const IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND As Long = FILE_DEVICE_SMARTCARD + 2079 * 4

Global Const SCARD_F_INTERNAL_ERROR = &H80100001
Global Const SCARD_E_CANCELLED = &H80100002
Global Const SCARD_E_INVALID_HANDLE = &H80100003
Global Const SCARD_E_INVALID_PARAMETER = &H80100004
Global Const SCARD_E_INVALID_TARGET = &H80100005
Global Const SCARD_E_NO_MEMORY = &H80100006
Global Const SCARD_F_WAITED_TOO_LONG = &H80100007
Global Const SCARD_E_INSUFFICIENT_BUFFER = &H80100008
Global Const SCARD_E_UNKNOWN_READER = &H80100009
Global Const SCARD_E_TIMEOUT = &H8010000A
Global Const SCARD_E_SHARING_VIOLATION = &H8010000B
Global Const SCARD_E_NO_SMARTCARD = &H8010000C
Global Const SCARD_E_UNKNOWN_CARD = &H8010000D
Global Const SCARD_E_CANT_DISPOSE = &H8010000E
Global Const SCARD_E_PROTO_MISMATCH = &H8010000F
Global Const SCARD_E_NOT_READY = &H80100010
Global Const SCARD_E_INVALID_VALUE = &H80100011
Global Const SCARD_E_SYSTEM_CANCELLED = &H80100012
Global Const SCARD_F_COMM_ERROR = &H80100013
Global Const SCARD_F_UNKNOWN_ERROR = &H80100014
Global Const SCARD_E_INVALID_ATR = &H80100015
Global Const SCARD_E_NOT_TRANSACTED = &H80100016
Global Const SCARD_E_READER_UNAVAILABLE = &H80100017
Global Const SCARD_P_SHUTDOWN = &H80100018
Global Const SCARD_E_PCI_TOO_SMALL = &H80100019
Global Const SCARD_E_READER_UNSUPPORTED = &H8010001A
Global Const SCARD_E_DUPLICATE_READER = &H8010001B
Global Const SCARD_E_CARD_UNSUPPORTED = &H8010001C
Global Const SCARD_E_NO_SERVICE = &H8010001D
Global Const SCARD_E_SERVICE_STOPPED = &H8010001E
Global Const SCARD_W_UNSUPPORTED_CARD = &H80100065
Global Const SCARD_W_UNRESPONSIVE_CARD = &H80100066
Global Const SCARD_W_UNPOWERED_CARD = &H80100067
Global Const SCARD_W_RESET_CARD = &H80100068
Global Const SCARD_W_REMOVED_CARD = &H80100069

Global Const SCARD_A_CORRUPTED = &H80100070
Global Const SCARD_C_CHECKSUM = &H80100071
Global Const SCARD_C_NOT_AVAILABLE = &H80100072
Global Const SCARD_S_NOT_SATISFIED = &H80100073
Global Const SCARD_S_LOCKED = &H80100074
Global Const SCARD_D_AUTHENTICATION_NOT_COMPLETE = &H80100075
Global Const SCARD_A_INCONSISTENT = &H80100076
Global Const SCARD_A_NOT_EXIST = &H80100077
Global Const SCARD_A_RECORD_NOT_FOUND = &H80100078
Global Const SCARD_P_INCORRECT = &H80100079
Global Const SCARD_C_INVALID_AMOUNT = &H80100080
Global Const SCARD_P_INVALID_RESPONSE = &H80100081
Global Const SCARD_P_UNKNOWN_INS = &H80100082
Global Const SCARD_P_UNKNOWN_CLA = &H80100083
Global Const SCARD_A_COUNTER_MAXIMUM = &H80100084
Global Const SCARD_C_NOT_ACOS_CARD = &H80100085
Global Const SCARD_A_MAXIMUM_AMOUNT = &H80100086
Global Const SCARD_A_NOT_NUMERIC = &H80100087
Global Const SCARD_A_BLANK_AMOUNT = &H80100088
Global Const SCARD_A_BLANK_DATA = &H80100089

Global Const SCARD_PROTOCOL_UNDEFINED = &H0           ' There is no active protocol.
Global Const SCARD_PROTOCOL_T0 = &H1                  ' T=0 is the active protocol.
Global Const SCARD_PROTOCOL_T1 = &H2                  ' T=1 is the active protocol.
Global Const SCARD_PROTOCOL_RAW = &H10000             ' Raw is the active protocol.
Global Const SCARD_PROTOCOL_DEFAULT = &H80000000      ' Use implicit PTS.

Global Const SCARD_UNKNOWN = 0    ' This value implies the driver is unaware
                                  ' of the current state of the reader.
Global Const SCARD_ABSENT = 1     ' This value implies there is no card in
                                  ' the reader.
Global Const SCARD_PRESENT = 2    ' This value implies there is a card is
                                  ' present in the reader, but that it has
                                  ' not been moved into position for use.
Global Const SCARD_SWALLOWED = 3  ' This value implies there is a card in the
                                  ' reader in position for use.  The card is
                                  ' not powered.
Global Const SCARD_POWERED = 4    ' This value implies there is power is
                                  ' being provided to the card, but the
                                  ' Reader Driver is unaware of the mode of
                                  ' the card.
Global Const SCARD_NEGOTIABLE = 5 ' This value implies the card has been
                                  ' reset and is awaiting PTS negotiation.
Global Const SCARD_SPECIFIC = 6   ' This value implies the card has been
                                  ' reset and specific communication
                                  ' protocols have been established.

Public Declare Function SCardEstablishContext Lib "winscard.dll" (ByVal dwScope As Long, _
                                                                  ByVal pvReserved1 As Long, _
                                                                  ByVal pvReserved2 As Long, _
                                                                  ByRef phContext As Long) As Long
                                                                  
Public Declare Function SCardReleaseContext Lib "winscard.dll" (ByVal hContext As Long) As Long

Public Declare Function SCardConnect Lib "winscard.dll" Alias "SCardConnectA" (ByVal hContext As Long, _
                                                                               ByVal szReaderName As String, _
                                                                               ByVal dwShareMode As Long, _
                                                                               ByVal dwPrefProtocol As Long, _
                                                                               ByRef hCard As Long, _
                                                                               ByRef ActiveProtocol As Long) As Long
                                                         
Public Declare Function SCardDisconnect Lib "winscard.dll" (ByVal hCard As Long, _
                                                            ByVal Disposistion As Long) As Long

Public Declare Function SCardBeginTransaction Lib "winscard.dll" (ByVal hCard As Long) As Long

Public Declare Function SCardEndTransaction Lib "winscard.dll" (ByVal hCard As Long, _
                                                                ByVal Disposition As Long) As Long

Public Declare Function SCardState Lib "winscard.dll" (ByVal hCard As Long, _
                                                       ByRef state As Long, _
                                                       ByRef Protocol As Long, _
                                                       ByRef ATR As Byte, _
                                                       ByRef ATRLen As Long) As Long

Public Declare Function SCardStatus Lib "winscard.dll" Alias "SCardStatusA" (ByVal hCard As Long, _
                                                                             ByVal szReaderName As String, _
                                                                             ByRef pcchReaderLen As Long, _
                                                                             ByRef state As Long, _
                                                                             ByRef Protocol As Long, _
                                                                             ByRef ATR As Byte, _
                                                                             ByRef ATRLen As Long) As Long

Public Declare Function SCardTransmit Lib "winscard.dll" (ByVal hCard As Long, _
                                                          pioSendRequest As SCARD_IO_REQUEST, _
                                                          ByRef SendBuff As Byte, _
                                                          ByVal SendBuffLen As Long, _
                                                          ByRef pioRecvRequest As SCARD_IO_REQUEST, _
                                                          ByRef RecvBuff As Byte, _
                                                          ByRef RecvBuffLen As Long) As Long
                                                          
Public Declare Function SCardListReaders Lib "winscard.dll" Alias "SCardListReadersA" (ByVal hContext As Long, _
                                                            ByVal mzGroup As String, _
                                                            ByVal ReaderList As String, _
                                                            ByRef pcchReaders As Long) As Long

Public Declare Function SCardGetStatusChange Lib "winscard.dll" Alias "SCardGetStatusChangeA" (ByVal hContext As Long, _
                                                          ByVal TimeOut As Long, _
                                                          ByRef ReaderState As SCARD_READERSTATE, _
                                                          ByVal ReaderCount As Long) As Long

Public Declare Function SCardControl Lib "winscard.dll" (ByVal hCard As Long, _
                                                          ByVal dwControlCode As Long, _
                                                          ByRef pvInBuffer As Byte, _
                                                          ByVal cbInBufferSize As Long, _
                                                          ByRef pvOutBuffer As Byte, _
                                                          ByVal cbOutBufferSize As Long, _
                                                          ByRef pcbBytesReturned As Long) As Long

'Variable Declaration
Public retCode, Protocol, hContext, hCard, ReaderCount As Long
Public sReaderList As String * 256
Public sReaderGroup As String
Public ConnActive As Boolean
Public ioRequest As SCARD_IO_REQUEST
Public SendLen, RecvLen As Long
Public SendBuff(0 To 262) As Byte
Public RecvBuff(0 To 262) As Byte
Public cardResp As String
Public cardResponses As Long

Global Const INVALID_SW1SW2 = -450


Public Sub LoadListToControl(ByVal Ctrl As ComboBox, ByVal ReaderList As String)
Dim sTemp As String
Dim indx As Integer

indx = 1
sTemp = ""
Ctrl.Clear

While (Mid(ReaderList, indx, 1) <> vbNullChar)
    
    While (Mid(ReaderList, indx, 1) <> vbNullChar)
       sTemp = sTemp + Mid(ReaderList, indx, 1)
       indx = indx + 1
    Wend
    
    indx = indx + 1
    
    Ctrl.AddItem sTemp
    
    sTemp = ""
    
Wend

End Sub


Public Function GetScardErrMsg(ByVal ReturnCode As Long) As String
  Select Case ReturnCode
    Case SCARD_E_CANCELLED
    GetScardErrMsg = "01" '"The action was canceled by an SCardCancel request."
    Case SCARD_E_CANT_DISPOSE
    GetScardErrMsg = "02" '"The system could not dispose of the media in the requested manner."
    Case SCARD_E_CARD_UNSUPPORTED
    GetScardErrMsg = "03" '"The smart card does not meet minimal requirements for support."
    Case SCARD_E_DUPLICATE_READER
    GetScardErrMsg = "04" '"The reader driver didn't produce a unique reader name."
    Case SCARD_E_INSUFFICIENT_BUFFER
    GetScardErrMsg = "05" '"The data buffer for returned data is too small for the returned data."
    Case SCARD_E_INVALID_ATR
    GetScardErrMsg = "06" '"An ATR string obtained from the registry is not a valid ATR string."
    Case SCARD_E_INVALID_HANDLE
    GetScardErrMsg = "07" '"The supplied handle was invalid."
    Case SCARD_E_INVALID_PARAMETER
    GetScardErrMsg = "08" '"One or more of the supplied parameters could not be properly interpreted."
    Case SCARD_E_INVALID_TARGET
    GetScardErrMsg = "09" '"Registry startup information is missing or invalid."
    Case SCARD_E_INVALID_VALUE
    GetScardErrMsg = "10" '"One or more of the supplied parameter values could not be properly interpreted."
    Case SCARD_E_NOT_READY
    GetScardErrMsg = "11" '"The reader or card is not ready to accept commands."
    Case SCARD_E_NOT_TRANSACTED
    GetScardErrMsg = "12" '"An attempt was made to end a non-existent transaction."
    Case SCARD_E_NO_MEMORY
    GetScardErrMsg = "13" '"Not enough memory available to complete this command."
    Case SCARD_E_NO_SERVICE
    GetScardErrMsg = "14" '"The smart card resource manager is not running."
    Case SCARD_E_NO_SMARTCARD
    GetScardErrMsg = "15" '"The operation requires a smart card, but no smart card is currently in the device."
    Case SCARD_E_PCI_TOO_SMALL
    GetScardErrMsg = "16" '"The PCI receive buffer was too small."
    Case SCARD_E_PROTO_MISMATCH
    GetScardErrMsg = "17" '"The requested protocols are incompatible with the protocol currently in use with the card."
    Case SCARD_E_READER_UNAVAILABLE
    GetScardErrMsg = "18" '"The specified reader is not currently available for use."
    Case SCARD_E_READER_UNSUPPORTED
    GetScardErrMsg = "19" '"The reader driver does not meet minimal requirements for support."
    Case SCARD_E_SERVICE_STOPPED
    GetScardErrMsg = "20" '"The smart card resource manager has shut down."
    Case SCARD_E_SHARING_VIOLATION
    GetScardErrMsg = "21" '"The smart card cannot be accessed because of other outstanding connections."
    Case SCARD_E_SYSTEM_CANCELLED
    GetScardErrMsg = "22" '"The action was canceled by the system, presumably to log off or shut down."
    Case SCARD_E_TIMEOUT
    GetScardErrMsg = "23" '"The user-specified timeout value has expired."
    Case SCARD_E_UNKNOWN_CARD
    GetScardErrMsg = "24" '"The specified smart card name is not recognized."
    Case SCARD_E_UNKNOWN_READER
    GetScardErrMsg = "25" '"The specified reader name is not recognized."
    Case SCARD_F_COMM_ERROR
    GetScardErrMsg = "26" '"An internal communications error has been detected."
    Case SCARD_F_INTERNAL_ERROR
    GetScardErrMsg = "27" '"An internal consistency check failed."
    Case SCARD_F_UNKNOWN_ERROR
    GetScardErrMsg = "28" '"An internal error has been detected, but the source is unknown."
    Case SCARD_F_WAITED_TOO_LONG
    GetScardErrMsg = "29" '"An internal consistency timer has expired."
    Case SCARD_S_SUCCESS
    GetScardErrMsg = "30" '"No error was encountered."
    Case SCARD_W_REMOVED_CARD
    GetScardErrMsg = "31" '"The smart card has been removed, so that further communication is not possible."
    Case SCARD_W_RESET_CARD
    GetScardErrMsg = "32" '"The smart card has been reset, so any shared state information is invalid."
    Case SCARD_W_UNPOWERED_CARD
    GetScardErrMsg = "33" '"Power has been removed from the smart card, so that further communication is not possible."
    Case SCARD_W_UNRESPONSIVE_CARD
    GetScardErrMsg = "34" '"The smart card is not responding to a reset."
    Case SCARD_W_UNSUPPORTED_CARD
    GetScardErrMsg = "35" '"The reader cannot communicate with the card, due to ATR string configuration conflicts."
    
    Case SCARD_A_CORRUPTED
    GetScardErrMsg = "37"
    Case SCARD_C_CHECKSUM
    GetScardErrMsg = "38"
    Case SCARD_C_NOT_AVAILABLE
    GetScardErrMsg = "39"
    Case SCARD_S_NOT_SATISFIED
    GetScardErrMsg = "40"
    Case SCARD_S_LOCKED
    GetScardErrMsg = "41"
    Case SCARD_D_AUTHENTICATION_NOT_COMPLETE
    GetScardErrMsg = "42"
    Case SCARD_A_INCONSISTENT
    GetScardErrMsg = "43"
    Case SCARD_A_NOT_EXIST
    GetScardErrMsg = "44"
    Case SCARD_A_RECORD_NOT_FOUND
    GetScardErrMsg = "45"
    Case SCARD_P_INCORRECT
    GetScardErrMsg = "46"
    Case SCARD_C_INVALID_AMOUNT
    GetScardErrMsg = "47"
    Case SCARD_P_INVALID_RESPONSE
    GetScardErrMsg = "48"
    Case SCARD_P_UNKNOWN_INS
    GetScardErrMsg = "49"
    Case SCARD_P_UNKNOWN_CLA
    GetScardErrMsg = "50"
    Case SCARD_A_COUNTER_MAXIMUM
    GetScardErrMsg = "51"
    Case SCARD_C_NOT_ACOS_CARD
    GetScardErrMsg = "85"
    Case SCARD_A_MAXIMUM_AMOUNT
    GetScardErrMsg = "86"
    Case SCARD_A_NOT_NUMERIC
    GetScardErrMsg = "87"
    Case SCARD_A_BLANK_AMOUNT
    GetScardErrMsg = "88"
    Case SCARD_A_BLANK_DATA
    GetScardErrMsg = "89"
    Case Else
    GetScardErrMsg = "36"
    End Select
End Function

Public Function ACOSError(ByVal Sw1 As Byte, ByVal Sw2 As Byte) As Long
  
  ' Check for error returned by ACOS card
  ACOSError = 0
  If ((Sw1 = &H62) And (Sw2 = &H81)) Then
        ACOSError = SCARD_A_CORRUPTED
        Call WriteLog("Account data may be corrupted.")
        Exit Function
  End If
  If (Sw1 = &H63) Then
        ACOSError = SCARD_C_CHECKSUM
        Call WriteLog("MAC cryptographic checksum is wrong.")
        Exit Function
  End If
  If ((Sw1 = &H69) And (Sw2 = &H66)) Then
        ACOSError = SCARD_C_NOT_AVAILABLE
        Call WriteLog("Command not available or option bit not set.")
        Exit Function
  End If
  If ((Sw1 = &H69) And (Sw2 = &H82)) Then
        ACOSError = SCARD_S_NOT_SATISFIED
        Call WriteLog("Security status not satisfied. Secret code, IC or PIN not submitted.")
        Exit Function
  End If
  If ((Sw1 = &H69) And (Sw2 = &H83)) Then
        ACOSError = SCARD_S_LOCKED
        Call WriteLog("The specified code is locked.")
        Exit Function
  End If
  If ((Sw1 = &H69) And (Sw2 = &H85)) Then
        ACOSError = SCARD_D_AUTHENTICATION_NOT_COMPLETE
        Call WriteLog("Preceding transaction was not DEBIT or mutual authentication has not been completed.")
        Exit Function
  End If
  If ((Sw1 = &H69) And (Sw2 = &HF0)) Then
        ACOSError = SCARD_A_INCONSISTENT
        Call WriteLog("Data in account is inconsistent. No access unless in Issuer mode.")
        Exit Function
  End If
  If ((Sw1 = &H6A) And (Sw2 = &H82)) Then
        ACOSError = SCARD_A_NOT_EXIST
        Call WriteLog("Account does not exist.")
      Exit Function
  End If
  If ((Sw1 = &H6A) And (Sw2 = &H83)) Then
        ACOSError = SCARD_A_RECORD_NOT_FOUND
        Call WriteLog("Record not found or file too short.")
        Exit Function
  End If
  If ((Sw1 = &H6A) And (Sw2 = &H86)) Then
        ACOSError = SCARD_P_INCORRECT
        Call WriteLog("P1 or P2 is incorrect.")
        Exit Function
  End If
  If ((Sw1 = &H6B) And (Sw2 = &H20)) Then
        ACOSError = SCARD_C_INVALID_AMOUNT
        Call WriteLog("Invalid amount in DEBIT/CREDIT command.")
        Exit Function
  End If
  If (Sw1 = &H6C) Then
        ACOSError = SCARD_P_INVALID_RESPONSE
        Call WriteLog("Issue GET RESPONSE with P3 = " & Hex(Sw2) & " to get response data.")
        Exit Function
  End If
  If (Sw1 = &H6D) Then
        ACOSError = SCARD_P_UNKNOWN_INS
        Call WriteLog("Unknown INS.")
        Exit Function
  End If
  If (Sw1 = &H6E) Then
        ACOSError = SCARD_P_UNKNOWN_CLA
        Call WriteLog("Unknown CLA.")
        Exit Function
  End If
  If ((Sw1 = &H6F) And (Sw2 = &H10)) Then
        ACOSError = SCARD_A_COUNTER_MAXIMUM
        Call WriteLog("Account Transaction Counter at maximum. No more transaction possible.")
        Exit Function
  End If

End Function

'Device Function & Procedure Declaration

' this routine will encrypt 8-byte data with 8-byte key
' the result is stored in data
Public Sub DES(data() As Byte, key() As Byte)
    Call Chain_DES(data(0), key(0), ALGO_DES, 1, DATA_ENCRYPT)
End Sub

' this routine will use 3DES algo to encrypt 8-byte data with 16-byte key
' the result is stored in data
Public Sub TripleDES(data() As Byte, key() As Byte)
    Call Chain_DES(data(0), key(0), ALGO_3DES, 1, DATA_ENCRYPT)
End Sub


' MAC as defined in ACOS manual
' receives 8-byte Key and 16-byte Data
' result is stored in Data
Public Sub mac(data() As Byte, key() As Byte)
Dim i As Integer

    DES data, key
    For i = 0 To 7
        data(i) = data(i) Xor data(i + 8)
    Next
    DES data, key
End Sub

' Triple MAC as defined in ACOS manual
' receives 16-byte Key and 16-byte Data
' result is stored in Data
Public Sub TripleMAC(data() As Byte, key() As Byte)
Dim i As Integer

    TripleDES data, key
    For i = 0 To 7
        data(i) = data(i) Xor data(i + 8)
    Next
    TripleDES data, key
End Sub

Private Sub ClearBuffers()

  Dim indx As Long
  
  For indx = 0 To 262
    RecvBuff(indx) = &H0
    SendBuff(indx) = &H0
  Next indx
  
End Sub

Public Function SendAPDUandDisplay(ByVal SendType As Integer, ByVal ApduIn As String) As Long
Dim indx As Integer
Dim TmpStr As String

    ioRequest.dwProtocol = Protocol
    ioRequest.cbPciLength = Len(ioRequest)
    Call WriteLog("ACR: <" & ApduIn)
    TmpStr = ""
    RecvLen = 262
  
    retCode = SCardTransmit(hCard, _
                          ioRequest, _
                          SendBuff(0), _
                          SendLen, _
                          ioRequest, _
                          RecvBuff(0), _
                          RecvLen)
    If retCode <> SCARD_S_SUCCESS Then
        Call WriteLog("ACR Error : " & retCode)
        SendAPDUandDisplay = retCode
    Exit Function
    Else
        Select Case SendType
        Case 0                  ' Read all data received
            For indx = 0 To RecvLen - 1
                TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
            Next indx
        Case 1                  ' Read ATR after checking SW1/SW2
            For indx = RecvLen - 2 To RecvLen - 1
                TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
            Next indx
            If TmpStr <> "90 00 " Then
                Call WriteLog("ACR Error : Return bytes are not acceptable.")
            Else
                TmpStr = "ATR: "
            For indx = 0 To RecvLen - 3
                TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
            Next indx
            End If
        Case 2                  ' Read data after checking SW1/SW2
            For indx = RecvLen - 2 To RecvLen - 1
                TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
            Next indx
            If TmpStr <> "90 00 " Then
                Call WriteLog("ACR Error : Return bytes are not acceptable.")
            Else
                TmpStr = ""
            For indx = 0 To RecvLen - 3
                TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
            Next indx
            End If
        End Select
        Call WriteLog("ACR > : " & TmpStr)
    End If
    SendAPDUandDisplay = retCode
  
End Function

Private Function SubmitIC() As Long

  Dim indx As Integer
  Dim TmpStr As String

  Call ClearBuffers
  SendBuff(0) = &H80        ' CLA
  SendBuff(1) = &H20        ' INS
  SendBuff(2) = &H7         ' P1
  SendBuff(3) = &H0         ' P2
  SendBuff(4) = &H8         ' P3
  SendBuff(5) = &H41        ' A
  SendBuff(6) = &H43        ' C
  SendBuff(7) = &H4F        ' O
  SendBuff(8) = &H53        ' S
  SendBuff(9) = &H54        ' T
  SendBuff(10) = &H45       ' E
  SendBuff(11) = &H53       ' S
  SendBuff(12) = &H54       ' T
  
  SendLen = &HD
  RecvLen = &H2
  TmpStr = ""
  For indx = 0 To SendLen - 1
    TmpStr = TmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  
  retCode = SendAPDUandDisplay(0, TmpStr)
  
  If retCode <> SCARD_S_SUCCESS Then
    SubmitIC = retCode
    Exit Function
  End If
  
  TmpStr = ""
  For indx = 0 To 1
    TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
  Next indx
  If TmpStr <> "90 00 " Then
    Call WriteLog("Return string is invalid. Value: " & TmpStr)
    SubmitIC = INVALID_SW1SW2
    Exit Function
  End If
  
  SubmitIC = retCode

End Function

Public Function SelectFile(ByVal HiAddr As Byte, ByVal LoAddr As Byte) As Long
Dim indx As Integer
Dim TmpStr As String

    Call ClearBuffers
    SendBuff(0) = &H80        ' CLA
    SendBuff(1) = &HA4        ' INS
    SendBuff(2) = &H0         ' P1
    SendBuff(3) = &H0         ' P2
    SendBuff(4) = &H2         ' P3
    SendBuff(5) = HiAddr      ' Value of High Byte
    SendBuff(6) = LoAddr      ' Value of Low Byte
  
    SendLen = &O7
    RecvLen = &H2
    TmpStr = ""
    For indx = 0 To SendLen - 1
        TmpStr = TmpStr & Format(Hex(SendBuff(indx)), "00") & " "
    Next indx
    retCode = SendAPDUandDisplay(0, TmpStr)
    If retCode <> SCARD_S_SUCCESS Then
        SelectFile = retCode
        Exit Function
    End If
  
    SelectFile = retCode

End Function

Public Function ReadRecord(ByVal RecNo As Byte, ByVal datalen As Byte) As Long
Dim indx As Integer
Dim TmpStr As String
  
    ' 1. Read data from card
    Call ClearBuffers
    SendBuff(0) = &H80        ' CLA
    SendBuff(1) = &HB2        ' INS
    SendBuff(2) = RecNo       ' Record No
    SendBuff(3) = &H0         ' P2
    SendBuff(4) = datalen     ' Length of Data
    SendLen = 5
    RecvLen = SendBuff(4) + 2
    TmpStr = ""
    For indx = 0 To SendLen - 1
        TmpStr = TmpStr & Format(Hex(SendBuff(indx)), "00") & " "
    Next indx
    retCode = SendAPDUandDisplay(0, TmpStr)
    If retCode <> SCARD_S_SUCCESS Then
        ReadRecord = retCode
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx + SendBuff(4))), "00") & " "
    Next indx
    If TmpStr <> "90 00 " Then
        Call WriteLog("Return string is invalid. Value: " & TmpStr)
        ReadRecord = INVALID_SW1SW2
        Exit Function
    End If
  
    ReadRecord = retCode

End Function

Public Function WriteRecord(ByVal caseType As Integer, ByVal RecNo As Byte, ByVal maxLen As Byte, _
                             ByVal datalen As Byte, ByRef ApduIn() As Byte) As Long

Dim indx As Integer
Dim TmpStr As String

    If caseType = 1 Then   ' If card data is to be erased before writing new data
        ' 1. Re-initialize card values to $00
        Call ClearBuffers
        SendBuff(0) = &H80        ' CLA
        SendBuff(1) = &HD2        ' INS
        SendBuff(2) = RecNo       ' Record No
        SendBuff(3) = &H0         ' P2
        SendBuff(4) = maxLen     ' Length of Data
        For indx = 0 To maxLen - 1
            SendBuff(indx + 5) = &H0
        Next indx
        SendLen = SendBuff(4) + 5
        RecvLen = &H2
        TmpStr = ""
        For indx = 0 To SendLen - 1
            TmpStr = TmpStr & Format(Hex(SendBuff(indx)), "00") & " "
        Next indx
        retCode = SendAPDUandDisplay(0, TmpStr)
        If retCode <> SCARD_S_SUCCESS Then
            WriteRecord = retCode
        Exit Function
        End If
        TmpStr = ""
        For indx = 0 To 1
            TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
        Next indx
        If TmpStr <> "90 00 " Then
            Call WriteLog("Return string is invalid. Value: " & TmpStr)
            WriteRecord = INVALID_SW1SW2
            Exit Function
        End If
    End If
  
    ' 2. Write data to card
    Call ClearBuffers
    SendBuff(0) = &H80        ' CLA
    SendBuff(1) = &HD2        ' INS
    SendBuff(2) = RecNo       ' Record No
    SendBuff(3) = &H0         ' P2
    SendBuff(4) = datalen     ' Length of Data
    For indx = 0 To datalen - 1
        SendBuff(indx + 5) = ApduIn(indx)
    Next indx
    SendLen = SendBuff(4) + 5
    RecvLen = &H2
    TmpStr = ""
    For indx = 0 To SendLen - 1
        TmpStr = TmpStr & Format(Hex(SendBuff(indx)), "00") & " "
    Next indx
    retCode = SendAPDUandDisplay(0, TmpStr)
    If retCode <> SCARD_S_SUCCESS Then
        WriteRecord = retCode
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
    If TmpStr <> "90 00 " Then
        Call WriteLog("Return string is invalid. Value: " & TmpStr)
        WriteRecord = INVALID_SW1SW2
        Exit Function
    End If
  
    WriteRecord = retCode

End Function

Public Function CheckACOSCard() As Boolean
Dim indx As Integer
Dim TmpStr As String
Dim respCode As String

    ' 1. Reconnect reader to accommodate change of cards
    If ConnActive Then
        retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
        ConnActive = False
    End If
    retCode = SCardConnect(hContext, _
                        "ACS ACR128U ICC Interface 0", _
                        SCARD_SHARE_EXCLUSIVE, _
                        SCARD_PROTOCOL_T0 Or SCARD_PROTOCOL_T1, _
                        hCard, _
                        Protocol)
    If retCode <> SCARD_S_SUCCESS Then
        'Tulis Ke Log
        respCode = GetScardErrMsg(retCode)
        Call WriteLog(respCode)
        ConnActive = False
        CheckACOSCard = False
        Exit Function
    End If
    ConnActive = True

    ' 2. Check for File FF 00
    retCode = SelectFile(&HFF, &H0)
    If retCode <> SCARD_S_SUCCESS Then
        CheckACOSCard = False
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
    If TmpStr <> "90 00 " Then
        cardResp = "Return string is invalid. Value: " & TmpStr
        CheckACOSCard = False
        Exit Function
    End If
  
    ' 3. Check for File FF 01
    retCode = SelectFile(&HFF, &H1)
    If retCode <> SCARD_S_SUCCESS Then
        CheckACOSCard = False
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
    If TmpStr <> "90 00 " Then
        cardResp = "Return string is invalid. Value: " & TmpStr
        CheckACOSCard = False
        Exit Function
    End If
  
    ' 4. Check for File FF 02
    retCode = SelectFile(&HFF, &H2)
    If retCode <> SCARD_S_SUCCESS Then
        CheckACOSCard = False
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
    If TmpStr <> "90 00 " Then
        cardResp = "Return string is invalid. Value: " & TmpStr
        CheckACOSCard = False
        Exit Function
    End If
  
    '5. Check for File FF 03
    retCode = SelectFile(&HFF, &H3)
    If retCode <> SCARD_S_SUCCESS Then
        CheckACOSCard = False
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
    If TmpStr <> "90 00 " Then
        cardResp = "Return string is invalid. Value: " & TmpStr
        CheckACOSCard = False
        Exit Function
    End If
  
    '6. Check for File FF 04
    retCode = SelectFile(&HFF, &H4)
    If retCode <> SCARD_S_SUCCESS Then
        CheckACOSCard = False
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
    If TmpStr <> "90 00 " Then
        cardResp = "Return string is invalid. Value: " & TmpStr
        CheckACOSCard = False
        Exit Function
    End If
  
    '6. Check for File FF 05
    retCode = SelectFile(&HFF, &H5)
    If retCode <> SCARD_S_SUCCESS Then
        CheckACOSCard = False
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
    If TmpStr <> "90 00 " Then
        cardResp = "Return string is invalid. Value: " & TmpStr
        CheckACOSCard = False
        Exit Function
    End If
  
    '7. Check for File FF 06
    retCode = SelectFile(&HFF, &H6)
    If retCode <> SCARD_S_SUCCESS Then
        CheckACOSCard = False
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
    If TmpStr <> "90 00 " Then
        cardResp = "Return string is invalid. Value: " & TmpStr
        CheckACOSCard = False
        Exit Function
    End If
  
    '8. Check for File FF 07
    retCode = SelectFile(&HFF, &H7)
    If retCode <> SCARD_S_SUCCESS Then
        CheckACOSCard = False
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
    If TmpStr <> "90 00 " Then
        cardResp = "Return string is invalid. Value: " & TmpStr
        CheckACOSCard = False
        Exit Function
    End If
    CheckACOSCard = True
End Function

 
Public Function InquireAccount(ByVal keyNo As Byte, ByRef DataIn() As Byte) As Long
  
    Dim indx As Integer
    Dim TmpStr As String
    Dim acos As Long

    Call ClearBuffers
    SendBuff(0) = &H80        ' CLA
    SendBuff(1) = &HE4        ' INS
    SendBuff(2) = keyNo       ' Key No
    SendBuff(3) = &H0         ' P2
    SendBuff(4) = &H4      ' Length of Data
    For indx = 0 To 3
        SendBuff(indx + 5) = DataIn(indx)
    Next indx
    SendLen = SendBuff(4) + 5
    RecvLen = &H2
    TmpStr = ""
    For indx = 0 To SendLen - 1
        TmpStr = TmpStr & Format(Hex(SendBuff(indx)), "00") & " "
    Next indx
    retCode = SendAPDUandDisplay(0, TmpStr)
    If retCode <> SCARD_S_SUCCESS Then
        InquireAccount = retCode
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
    acos = ACOSError(RecvBuff(0), RecvBuff(1))
    If acos <> 0 Then
        InquireAccount = acos
        Exit Function
    End If
    If TmpStr <> "61 19 " Then     ' SW1/SW2 must be equal to 6119h
        Call WriteLog("INQUIRE ACCOUNT command failed.")
        Call WriteLog("Return string is invalid. Value: " & TmpStr)
        InquireAccount = INVALID_SW1SW2
        Exit Function
    End If

    InquireAccount = retCode
  
End Function
 
Public Function GetResponse(ByVal LE As String) As Long
  
    Dim indx As Integer
    Dim TmpStr As String
    Dim acos As Long

    Call ClearBuffers
    SendBuff(0) = &H80        ' CLA
    SendBuff(1) = &HC0        ' INS
    SendBuff(2) = &H0         ' P1
    SendBuff(3) = &H0         ' P2
    SendBuff(4) = LE         ' Length of Data
    SendLen = 5
    RecvLen = SendBuff(4) + 2
    TmpStr = ""
    For indx = 0 To SendLen - 1
        TmpStr = TmpStr & Format(Hex(SendBuff(indx)), "00") & " "
    Next indx
    retCode = SendAPDUandDisplay(0, TmpStr)
    If retCode <> SCARD_S_SUCCESS Then
        GetResponse = retCode
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx + SendBuff(4))), "00") & " "
    Next indx
    acos = ACOSError(RecvBuff(SendBuff(4)), RecvBuff(SendBuff(4) + 1)) <> 0
    If acos <> 0 Then
        GetResponse = acos
        Exit Function
    End If
    If TmpStr <> "90 00 " Then
        Call WriteLog("GET RESPONSE command failed.")
        Call WriteLog("Return string is invalid. Value: " & TmpStr)
        GetResponse = INVALID_SW1SW2
        Exit Function
    End If

    GetResponse = retCode

End Function

Public Function CreditAmount(ByRef CreditData() As Byte) As Long
  
    Dim indx As Integer
    Dim TmpStr As String
    Dim acos As Long

    Call ClearBuffers
    SendBuff(0) = &H80        ' CLA
    SendBuff(1) = &HE2        ' INS
    SendBuff(2) = &H0         ' P1
    SendBuff(3) = &H0         ' P2
    SendBuff(4) = &HB         ' P3
    For indx = 0 To 11
        SendBuff(indx + 5) = CreditData(indx)
    Next indx
    SendLen = SendBuff(4) + 5
    RecvLen = &H2
    For indx = 0 To SendLen - 1
        TmpStr = TmpStr & Format(Hex(SendBuff(indx)), "00") & " "
    Next indx
    retCode = SendAPDUandDisplay(0, TmpStr)
    If retCode <> SCARD_S_SUCCESS Then
        CreditAmount = retCode
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
    acos = ACOSError(RecvBuff(0), RecvBuff(1))
    If acos <> 0 Then
        CreditAmount = acos
        Exit Function
    End If
    If TmpStr <> "90 00 " Then
        Call WriteLog("CREDIT AMOUNT command failed.")
        Call WriteLog("Return string is invalid. Value: " & TmpStr)
        CreditAmount = INVALID_SW1SW2
        Exit Function
    End If

    CreditAmount = retCode

End Function

Public Function DebitAmount(ByRef DebitData() As Byte) As Long
  
    Dim indx As Integer
    Dim TmpStr As String
    Dim acos As Long

    Call ClearBuffers
    SendBuff(0) = &H80        ' CLA
    SendBuff(1) = &HE6        ' INS
    SendBuff(2) = &H0         ' P1
    SendBuff(3) = &H0         ' P2
    SendBuff(4) = &HB         ' P3
    For indx = 0 To 11
        SendBuff(indx + 5) = DebitData(indx)
    Next indx
    SendLen = SendBuff(4) + 5
    RecvLen = &H2
    For indx = 0 To SendLen - 1
        TmpStr = TmpStr & Format(Hex(SendBuff(indx)), "00") & " "
    Next indx
    retCode = SendAPDUandDisplay(0, TmpStr)
    If retCode <> SCARD_S_SUCCESS Then
        DebitAmount = retCode
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
    acos = ACOSError(RecvBuff(0), RecvBuff(1))
    If acos <> 0 Then
        DebitAmount = acos
        Exit Function
    End If
    If TmpStr <> "90 00 " Then
        Call WriteLog("DEBIT AMOUNT command failed.")
        Call WriteLog("Return string is invalid. Value: " & TmpStr)
        DebitAmount = INVALID_SW1SW2
        Exit Function
    End If

  DebitAmount = retCode

End Function

Public Function DebitAmountwithDBC(ByRef DebitData() As Byte) As Long
  
    Dim indx As Integer
    Dim TmpStr As String
    Dim acos As Long

    Call ClearBuffers
    SendBuff(0) = &H80        ' CLA
    SendBuff(1) = &HE6        ' INS
    SendBuff(2) = &H1          ' P1
    SendBuff(3) = &H0         ' P2
    SendBuff(4) = &HB         ' P3
    For indx = 0 To 11
        SendBuff(indx + 5) = DebitData(indx)
    Next indx
    SendLen = SendBuff(4) + 5
    RecvLen = &H2
    For indx = 0 To SendLen - 1
        TmpStr = TmpStr & Format(Hex(SendBuff(indx)), "00") & " "
    Next indx
    retCode = SendAPDUandDisplay(0, TmpStr)
    If retCode <> SCARD_S_SUCCESS Then
        DebitAmountwithDBC = retCode
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
    acos = ACOSError(RecvBuff(0), RecvBuff(1))
    If acos <> 0 Then
        DebitAmountwithDBC = acos
        Exit Function
    End If
    If TmpStr <> "61 04 " Then
        Call WriteLog("DEBIT AMOUNT command failed.")
        Call WriteLog("Return string is invalid. Value: " & TmpStr)
        DebitAmountwithDBC = INVALID_SW1SW2
        Exit Function
    End If
    DebitAmountwithDBC = retCode
End Function

Public Function RevokeDebit(ByRef RevDebData() As Byte) As Long
    Dim indx As Integer
    Dim TmpStr As String
    Dim acos As Long

    Call ClearBuffers
    SendBuff(0) = &H80        ' CLA
    SendBuff(1) = &HE8        ' INS
    SendBuff(2) = &H0         ' P1
    SendBuff(3) = &H0         ' P2
    SendBuff(4) = &H4         ' P3
    For indx = 0 To 4
        SendBuff(indx + 5) = RevDebData(indx)
    Next indx
    SendLen = SendBuff(4) + 5
    RecvLen = &H2
    For indx = 0 To SendLen - 1
        TmpStr = TmpStr & Format(Hex(SendBuff(indx)), "00") & " "
    Next indx
    retCode = SendAPDUandDisplay(0, TmpStr)
    If retCode <> SCARD_S_SUCCESS Then
        RevokeDebit = retCode
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
    acos = ACOSError(RecvBuff(0), RecvBuff(1))
    If acos <> 0 Then
        RevokeDebit = acos
        Exit Function
    End If
    If TmpStr <> "90 00 " Then
        Call WriteLog("REVOKE DEBIT command failed.")
        Call WriteLog("Return string is invalid. Value: " & TmpStr)
        RevokeDebit = INVALID_SW1SW2
        Exit Function
    End If

    RevokeDebit = retCode

End Function

Public Function ConnectReader() As Long
    If ConnActive Then
        Call WriteLog("Connection is already active.")
        Exit Function
    End If
  
    cardResp = "Invoke SCardConnect"
    ' 1. Connect to selected reader using hContext handle
    '    and obtain valid hCard handle
    retCode = SCardConnect(hContext, _
                        "ACS ACR128U ICC Interface 0", _
                        SCARD_SHARE_EXCLUSIVE, _
                        SCARD_PROTOCOL_T0 Or SCARD_PROTOCOL_T1, _
                        hCard, _
                        Protocol)
    If retCode <> SCARD_S_SUCCESS Then
        ConnectReader = False
        cardResponses = retCode
        ConnActive = False
        Exit Function
    Else
        cardResp = "Successful connection to ACS ACR128U ICC Interface 0"
        ConnectReader = True
    End If

    ConnActive = True
End Function

Public Function InitializeReader() As Long
    sReaderList = String(255, vbNullChar)
    ReaderCount = 255
     
    ' 1. Establish context and obtain hContext handle
    retCode = SCardEstablishContext(SCARD_SCOPE_USER, 0, 0, hContext)
    If retCode <> SCARD_S_SUCCESS Then
        InitializeReader = retCode
        Exit Function
    End If
  
    ' 2. List PC/SC card readers installed in the system
    retCode = SCardListReaders(hContext, sReaderGroup, sReaderList, ReaderCount)
    If retCode <> SCARD_S_SUCCESS Then
        InitializeReader = retCode
        Exit Function
    End If
    Call WriteLog("sReaderList :" & sReaderList)
End Function

Public Function ResetCard() As Long
    If ConnActive Then
        retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
        ConnActive = False
    End If
    retCode = SCardReleaseContext(hContext)
    ResetCard = retCode
End Function

Public Function FormatCard() As Long
Dim indx As Integer
Dim TmpStr As String
Dim tmpArray(0 To 31) As Byte
  
    ' 2. Check if card inserted is an ACOS card
    If Not CheckACOSCard Then
        Call WriteLog("Please insert an ACOS card.")
        FormatCard = SCARD_C_NOT_ACOS_CARD
        Exit Function
    End If
    Call WriteLog("ACOS card is detected.")
  
    ' 3. Submit Issuer Code
    retCode = SubmitIC()
    If retCode <> SCARD_S_SUCCESS Then
        FormatCard = retCode
        Exit Function
    End If

    ' 4. Select FF 02
    retCode = SelectFile(&HFF, &H2)
    If retCode <> SCARD_S_SUCCESS Then
        FormatCard = retCode
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
    If TmpStr <> "90 00 " Then
        Call WriteLog("Return string is invalid. Value: " & TmpStr)
        FormatCard = INVALID_SW1SW2
        Exit Function
    End If

    ' 5. Write to FF 02
    '    This step will define the Option registers,
    '    Security Option registers and Personalization bit
    '    are not set
    tmpArray(0) = &H29          ' 29h  Only REV_DEB, DEB_MAC and Account bits are set
    'tmpArray(0) = &H2B          ' 2Bh  REV_DEB, DEB_MAC, 3-DES and Account bits are set
    tmpArray(1) = &H0             ' 00    Security option register
    tmpArray(2) = &H3             ' 00    No of user files
    tmpArray(3) = &H0             ' 00    Personalization bit
    retCode = WriteRecord(0, &H0, &H4, &H4, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
        FormatCard = retCode
        Exit Function
    End If
    Call WriteLog("FF 02 is updated")

    ' 6. Perform a reset for changes in the ACOS to take effect
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
    retCode = SCardConnect(hContext, _
                        "ACS ACR128U ICC Interface 0", _
                        SCARD_SHARE_EXCLUSIVE, _
                        SCARD_PROTOCOL_T0 Or SCARD_PROTOCOL_T1, _
                        hCard, _
                        Protocol)
    If retCode <> SCARD_S_SUCCESS Then
        FormatCard = retCode
        ConnActive = False
        Exit Function
    End If
    Call WriteLog("Account files are enabled")
    ConnActive = True

    ' 7. Submit Issuer Code to write into FF 05 and FF 06
    retCode = SubmitIC()
    If retCode <> SCARD_S_SUCCESS Then
        FormatCard = retCode
        Exit Function
    End If

    ' 8. Select FF 05
    retCode = SelectFile(&HFF, &H5)
    If retCode <> SCARD_S_SUCCESS Then
        FormatCard = retCode
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
    If TmpStr <> "90 00 " Then
        Call WriteLog("Return string is invalid. Value: " & TmpStr)
        FormatCard = INVALID_SW1SW2
        Exit Function
    End If

    ' 9. Write to FF 05
    ' 9.1. Record 00
    tmpArray(0) = &H0          ' TRANSTYP 0
    tmpArray(1) = &H0          ' (3 bytes
    tmpArray(2) = &H0          '  reserved for
    tmpArray(3) = &H0          '  BALANCE 0)
    retCode = WriteRecord(0, &H0, &H4, &H4, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
        FormatCard = retCode
        Exit Function
    End If

    ' 9.2.Record 01
    tmpArray(0) = &H0          ' (2 bytes reserved
    tmpArray(1) = &H0          '  for ATC 0)
    tmpArray(2) = &H1          ' Set CHECKSUM 0
    tmpArray(3) = &H0          ' 00h
    retCode = WriteRecord(0, &H1, &H4, &H4, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
        FormatCard = retCode
        Exit Function
    End If

    ' 9.3. Record 02
    tmpArray(0) = &H0          ' TRANSTYP 1
    tmpArray(1) = &H0          ' (3 bytes
    tmpArray(2) = &H0          '  reserved for
    tmpArray(3) = &H0          '  BALANCE 1)
    retCode = WriteRecord(0, &H2, &H4, &H4, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
       FormatCard = retCode
        Exit Function
    End If

    ' 9.4.Record 03
    tmpArray(0) = &H0          ' (2 bytes reserved
    tmpArray(1) = &H0          '  for ATC 1)
    tmpArray(2) = &H1          ' Set CHECKSUM 1
    tmpArray(3) = &H0          ' 00h
    retCode = WriteRecord(0, &H3, &H4, &H4, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
        FormatCard = retCode
        Exit Function
    End If

    ' 9.5.Record 04
    tmpArray(0) = &HFF          ' (3 bytes
    tmpArray(1) = &HFF          '  initialized for
    tmpArray(2) = &HFF          '  MAX BALANCE)
    tmpArray(3) = &H0           ' 00h
    retCode = WriteRecord(0, &H4, &H4, &H4, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
        FormatCard = retCode
        Exit Function
    End If
 
    ' 9.6.Record 05
    tmpArray(0) = &H0           ' (4 bytes
    tmpArray(1) = &H0           '  reserved
    tmpArray(2) = &H0           '  for
    tmpArray(3) = &H0           '  AID)
    retCode = WriteRecord(0, &H5, &H4, &H4, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
        FormatCard = retCode
        Exit Function
    End If
 
    ' 9.7.Record 06
    tmpArray(0) = &H0           ' (4 bytes
    tmpArray(1) = &H0           '  reserved
    tmpArray(2) = &H0           '  for
    tmpArray(3) = &H0           '  TTREF_C)
    retCode = WriteRecord(0, &H6, &H4, &H4, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
        FormatCard = retCode
        Exit Function
    End If

    ' 9.8.Record 07
    tmpArray(0) = &H0           ' (4 bytes
    tmpArray(1) = &H0           '  reserved
    tmpArray(2) = &H0           '  for
    tmpArray(3) = &H0           '  TTREF_D)
    retCode = WriteRecord(0, &H7, &H4, &H4, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
        FormatCard = retCode
        Exit Function
    End If
    Call WriteLog("FF 05 is updated")

  ' 10. Select FF 06
    retCode = SelectFile(&HFF, &H6)
    If retCode <> SCARD_S_SUCCESS Then
        FormatCard = retCode
        Exit Function
    End If
    TmpStr = ""
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
    If TmpStr <> "90 00 " Then
        Call WriteLog("Return string is invalid. Value: " & TmpStr)
        FormatCard = INVALID_SW1SW2
        Exit Function
    End If

    ' 11. Write to FF 05
    ' DES option uses 8-byte key
  
    '  11a.1. Record 00 for Debit key
    TmpStr = "22222222"
    For indx = 0 To 7
        tmpArray(indx) = Asc(Mid(TmpStr, indx + 1, 1))
    Next indx
    retCode = WriteRecord(0, &H0, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
        FormatCard = retCode
        Exit Function
    End If
   
    ' 11a.2. Record 01 for Credit key
    TmpStr = "11111111"
    For indx = 0 To 7
        tmpArray(indx) = Asc(Mid(TmpStr, indx + 1, 1))
    Next indx
    retCode = WriteRecord(0, &H1, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
        FormatCard = retCode
        Exit Function
    End If
   
    ' 11a.3. Record 02 for Certify key
    TmpStr = "33333333"
    For indx = 0 To 7
        tmpArray(indx) = Asc(Mid(TmpStr, indx + 1, 1))
    Next indx
    retCode = WriteRecord(0, &H2, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
        FormatCard = retCode
        Exit Function
    End If

   ' 11a.4. Record 03 for Revoke Debit key
    TmpStr = "44444444"
    For indx = 0 To 7
        tmpArray(indx) = Asc(Mid(TmpStr, indx + 1, 1))
    Next indx
    retCode = WriteRecord(0, &H3, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
        FormatCard = retCode
        Exit Function
    End If
    '  Else                          ' 3-DES option uses 16-byte key
    '
    '  '  11b.1. Record 04 for Left half of Debit key
    '    tmpStr = "2222222222222222"
    '    For indx = 0 To 7           ' Left half of Debit key
    '      tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
    '    Next indx
    '    retCode = writeRecord(0, &H4, &H8, &H8, tmpArray)
    '    If retCode <> SCARD_S_SUCCESS Then
    '      Exit Sub
    '    End If
    '
    '  '  11b.2. Record 00 for Right half of Debit key
    '    For indx = 8 To 15          ' Right half of Debit key
    '      tmpArray(indx - 8) = Asc(Mid(tmpStr, indx + 1, 1))
    '    Next indx
    '    retCode = writeRecord(0, &H0, &H8, &H8, tmpArray)
    '    If retCode <> SCARD_S_SUCCESS Then
    '      Exit Sub
    '    End If
    '
    '  '  11b.3. Record 05 for Left half of Credit key
    '    tmpStr = "1111111111111111"
    '    For indx = 0 To 7           ' Left half of Credit key
    '      tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
    '    Next indx
    '    retCode = writeRecord(0, &H5, &H8, &H8, tmpArray)
    '    If retCode <> SCARD_S_SUCCESS Then
    '      Exit Sub
    '    End If
    '
    '  '  11b.4. Record 01 for Right half of Credit key
    '    For indx = 8 To 15          ' Right half of Credit key
    '      tmpArray(indx - 8) = Asc(Mid(tmpStr, indx + 1, 1))
    '    Next indx
    '    retCode = writeRecord(0, &H1, &H8, &H8, tmpArray)
    '    If retCode <> SCARD_S_SUCCESS Then
    '      Exit Sub
    '    End If
    '
    '  '  11b.5. Record 06 for Left half of Certify key
    '    tmpStr = "3333333333333333"
    '    For indx = 0 To 7           ' Left half of Certify key
    '      tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
    '    Next indx
    '    retCode = writeRecord(0, &H6, &H8, &H8, tmpArray)
    '    If retCode <> SCARD_S_SUCCESS Then
    '      Exit Sub
    '    End If
    '
    '  '  11b.6. Record 02 for Right half of Certify key
    '    For indx = 8 To 15          ' Right half of Certify key
    '      tmpArray(indx - 8) = Asc(Mid(tmpStr, indx + 1, 1))
    '    Next indx
    '    retCode = writeRecord(0, &H2, &H8, &H8, tmpArray)
    '    If retCode <> SCARD_S_SUCCESS Then
    '      Exit Sub
    '    End If
    '
    '  '  11b.7. Record 07 for Left half of Revoke Debit key
    '    tmpStr = "4444444444444444"
    '    For indx = 0 To 7           ' Left half of Revoke Debit key
    '      tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
    '    Next indx
    '    retCode = writeRecord(0, &H7, &H8, &H8, tmpArray)
    '    If retCode <> SCARD_S_SUCCESS Then
    '      Exit Sub
    '    End If
    '
    '  '  11b.8. Record 03 for Right half of Revoke Debit key
    '    For indx = 8 To 15          ' Right half of Revoke Debit key
    '      tmpArray(indx - 8) = Asc(Mid(tmpStr, indx + 1, 1))
    '    Next indx
    '    retCode = writeRecord(0, &H3, &H8, &H8, tmpArray)
    '    If retCode <> SCARD_S_SUCCESS Then
    '      Exit Sub
    '    End If
    '  End If
  
    Call WriteLog("Card Key Security is valid")
    
End Function

Public Function TopUpBalance(ByVal amt As String) As Long
Dim indx As Integer
Dim TmpStr As String
Dim tmpArray(0 To 31) As Byte
Dim amount, tmpVal As Long
Dim tmpKey(0 To 15) As Byte       ' Credit key to verify MAC
Dim TTREFc(0 To 3) As Byte
Dim ATREF(0 To 5) As Byte

    ' 1. Check if Credit key and valid Transaction value are provided
    If amt = "" Then
        TopUpBalance = SCARD_A_BLANK_AMOUNT
        Exit Function
    End If
    If Not IsNumeric(amt) Then
        TopUpBalance = SCARD_A_NOT_NUMERIC
        Exit Function
    End If
    If CLng(amt) > 16777215 Then
        TopUpBalance = SCARD_A_MAXIMUM_AMOUNT
        Exit Function
    End If
  
    ' 2. Check if card inserted is an ACOS card
    If Not CheckACOSCard Then
        TopUpBalance = SCARD_C_NOT_ACOS_CARD
        Call WriteLog("Please insert an ACOS card.")
        Exit Function
    End If
  
    Call WriteLog("ACOS card is detected.")
  
    ' 3. Issue INQUIRE ACCOUNT command using any arbitrary data and Credit key
    '    Arbitrary data is 1111h
    For indx = 0 To 3
        tmpArray(indx) = &H1
    Next indx
    retCode = InquireAccount(&H2, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
        TopUpBalance = retCode
        Exit Function
    End If

    ' 4. Issue GET RESPONSE command with Le = 19h or 25 bytes
    retCode = GetResponse("&H19")
    If retCode <> SCARD_S_SUCCESS Then
        TopUpBalance = retCode
        Exit Function
    End If

    ' 5. Store ACOS card values for TTREFc and ATREF
    For indx = 0 To 3
        TTREFc(indx) = RecvBuff(indx + 17)
    Next indx
    For indx = 0 To 5
        ATREF(indx) = RecvBuff(indx + 8)
    Next indx

  '  6. Prepare MAC data block: E2 + AMT + TTREFc + ATREF + 00 + 00
    '    use tmpArray as the data block
    amount = CLng(amt) * 100
    tmpArray(0) = &HE2
    tmpVal = Int(amount / 256)
    tmpVal = Int(tmpVal / 256)
    tmpArray(1) = tmpVal Mod 256                  ' Amount MSByte
    tmpVal = Int(amount / 256)
    tmpArray(2) = tmpVal Mod 256                  ' Amount Middle Byte
    tmpArray(3) = amount Mod 256                  ' Amount LSByte
    For indx = 0 To 3
        tmpArray(indx + 4) = TTREFc(indx)
    Next indx
    For indx = 0 To 5
        tmpArray(indx + 8) = ATREF(indx)
    Next indx
    tmpArray(13) = tmpArray(13) + 1               ' increment last byte of ATREF
    tmpArray(14) = &H0
    tmpArray(15) = &H0

    ' 7. Generate applicable MAC values, MAC result will be stored in tmpArray
    TmpStr = "11111111"
    For indx = 0 To Len(TmpStr) - 1
        tmpKey(indx) = Asc(Mid(TmpStr, indx + 1, 1))
    Next indx
    Call mac(tmpArray, tmpKey)
    '3Des
    'Call TripleMAC(tmpArray, tmpKey)
  

    ' 8. Format Credit command data and execute credit command
    '    Using tmpArray, the first four bytes are carried over
    tmpVal = Int(amount / 256)
    tmpVal = Int(tmpVal / 256)
    tmpArray(4) = tmpVal Mod 256                  ' Amount MSByte
    tmpVal = Int(amount / 256)
    tmpArray(5) = tmpVal Mod 256                  ' Amount Middle Byte
    tmpArray(6) = amount Mod 256                  ' Amount LSByte
    For indx = 0 To 3
        tmpArray(indx + 7) = ATREF(indx)
    Next indx
    retCode = CreditAmount(tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
        TopUpBalance = retCode
        Exit Function
    End If
    
    Call WriteLog("Credit transaction completed")
    
End Function

Public Function DebitBalance(ByVal amt As String) As Long
Dim indx, i As Integer
Dim TmpStr As String
Dim tmpArray(0 To 31) As Byte
Dim amount As Double
Dim tmpVal As Long
Dim tmpKey(0 To 15) As Byte       ' Debit key to verify MAC
Dim TTREFd(0 To 3) As Byte
Dim ATREF(0 To 5) As Byte
Dim tmpBalance(0 To 3) As Double
Dim new_balance As Double
  
    If amt = "" Then
        DebitBalance = SCARD_A_BLANK_AMOUNT
        Exit Function
    End If
    If Not IsNumeric(amt) Then
        DebitBalance = SCARD_A_NOT_NUMERIC
        Exit Function
    End If
    If CLng(amt) > 16777215 Then
        DebitBalance = SCARD_A_MAXIMUM_AMOUNT
        Exit Function
    End If
  
    ' 2. Check if card inserted is an ACOS card
    If Not CheckACOSCard Then
        DebitBalance = SCARD_C_NOT_ACOS_CARD
        Call WriteLog("Please insert an ACOS card.")
        Exit Function
    End If
    Call WriteLog("ACOS card is detected.")
  
    ' 3. Issue INQUIRE ACCOUNT command using any arbitrary data and Credit key
    '    Arbitrary data is 1111h
    For indx = 0 To 3
        tmpArray(indx) = &H1
    Next indx
    retCode = InquireAccount(&H2, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
        DebitBalance = retCode
        Exit Function
    End If

    ' 4. Issue GET RESPONSE command with Le = 19h or 25 bytes
    retCode = GetResponse("&H19")
    If retCode <> SCARD_S_SUCCESS Then
        DebitBalance = retCode
        Exit Function
    End If

    tmpBalance(1) = RecvBuff(7)
    tmpBalance(2) = RecvBuff(6)
    tmpBalance(2) = tmpBalance(2) * 256
    tmpBalance(3) = RecvBuff(5)
    tmpBalance(3) = tmpBalance(3) * 256
    tmpBalance(3) = tmpBalance(3) * 256
    tmpBalance(0) = tmpBalance(1) + tmpBalance(2) + tmpBalance(3)


    ' 5. Store ACOS card values for TTREFd and ATREF
    For indx = 0 To 3
        TTREFd(indx) = RecvBuff(indx + 21)
    Next indx
    For indx = 0 To 5
        ATREF(indx) = RecvBuff(indx + 8)
    Next indx

    ' 6. Prepare MAC data block: E6 + AMT + TTREFd + ATREF + 00 + 00
    '    use tmpArray as the data block
    amount = CDbl(amt) * 100
    tmpArray(0) = &HE6
    tmpVal = Int(amount / 256)
    tmpVal = Int(tmpVal / 256)
    tmpArray(1) = tmpVal Mod 256                  ' Amount MSByte
    tmpVal = Int(amount / 256)
    tmpArray(2) = tmpVal Mod 256                  ' Amount Middle Byte
    tmpArray(3) = amount Mod 256                  ' Amount LSByte
    For indx = 0 To 3
        tmpArray(indx + 4) = TTREFd(indx)
    Next indx
    For indx = 0 To 5
        tmpArray(indx + 8) = ATREF(indx)
    Next indx
    tmpArray(13) = tmpArray(13) + 1               ' increment last byte of ATREF
    tmpArray(14) = &H0
    tmpArray(15) = &H0

    ' 7. Generate applicable MAC values, MAC result will be stored in tmpArray
    TmpStr = "22222222"
    For indx = 0 To Len(TmpStr) - 1
        tmpKey(indx) = Asc(Mid(TmpStr, indx + 1, 1))
    Next indx
    Call mac(tmpArray, tmpKey)
    '3Des
    'Call TripleMAC(tmpArray, tmpKey)
  
    ' 8. Format Debit command data and execute debit command
    '    Using tmpArray, the first four bytes are carried over
    tmpVal = Int(amount / 256)
    tmpVal = Int(tmpVal / 256)
    tmpArray(4) = tmpVal Mod 256                  ' Amount MSByte
    tmpVal = Int(amount / 256)
    tmpArray(5) = tmpVal Mod 256                  ' Amount Middle Byte
    tmpArray(6) = amount Mod 256                  ' Amount LSByte
    For indx = 0 To 5
        tmpArray(indx + 7) = ATREF(indx)
    Next indx
  
    'If chk_dbc.Value = 0 Then 'Without Debit Certificate
    
    '    retCode = DebitAmount(tmpArray)
    '    If retCode <> SCARD_S_SUCCESS Then
    '      Exit Sub
    '    End If
    '
    'Else 'With Debit Certificate
    
    retCode = DebitAmountwithDBC(tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
        DebitBalance = retCode
        Exit Function
    End If
    
    'Issue GET RESPONSE command with Le = 4h
    retCode = GetResponse("&H4")
    If retCode <> SCARD_S_SUCCESS Then
        DebitBalance = retCode
        Exit Function
    End If
    
    'Prepare MAC data block: 01 + New Balance + ATC + TTREFD + 00 + 00 + 00
    '    use tmpArray as the data block
    
    amount = CDbl(amt)
    new_balance = tmpBalance(0) - amount
    tmpArray(0) = &H1
    
    tmpVal = Int(new_balance / 256)
    tmpVal = Int(tmpVal / 256)
    tmpArray(1) = tmpVal Mod 256                  ' Amount MSByte
    tmpVal = Int(new_balance / 256)
    tmpArray(2) = tmpVal Mod 256                  ' Amount Middle Byte
    tmpArray(3) = new_balance Mod 256                  ' Amount LSByte
    
    tmpVal = Int(amount / 256)
    tmpVal = Int(tmpVal / 256)
    tmpArray(4) = tmpVal Mod 256                  ' Amount MSByte
    tmpVal = Int(amount / 256)
    tmpArray(5) = tmpVal Mod 256                  ' Amount Middle Byte
    tmpArray(6) = amount Mod 256                  ' Amount LSByte
    tmpArray(7) = ATREF(4)
    tmpArray(8) = ATREF(5) + 1                    ' Increment ATC after every transaction
    
    For indx = 0 To 3
        tmpArray(indx + 9) = TTREFd(indx)
    Next indx
    tmpArray(13) = &H0
    tmpArray(14) = &H0
    tmpArray(15) = &H0

    'Generate applicable MAC values, MAC result will be stored in tmpArray
    TmpStr = "22222222"
    For indx = 0 To Len(TmpStr) - 1
        tmpKey(indx) = Asc(Mid(TmpStr, indx + 1, 1))
    Next indx
    '    If rbDES.Value = True Then
        Call mac(tmpArray, tmpKey)
    '   Else
    '      Call TripleMAC(tmpArray, tmpKey)
    ' End If
    
    For i = 0 To 3
        If RecvBuff(i) <> tmpArray(i) Then
            Call WriteLog("Debit Certificate Failed.")
            'chk_dbc.Value = 0
            Exit Function
        
        End If
    Next i
        
    Call WriteLog("Debit Certificate Verified.")
            
    'End If
  
    Call WriteLog("Debit transaction completed")
    
End Function

Public Function InquiryBalance() As String
Dim indx As Integer
Dim TmpStr As String
Dim tmpArray(0 To 31) As Byte
Dim tmpBalance(0 To 3) As Long
Dim tmpKey(0 To 15) As Byte       ' certify key to verify MAC
Dim LastTran As Byte
Dim TTREFc(0 To 3) As Byte
Dim TTREFd(0 To 3) As Byte
Dim ATREF(0 To 5) As Byte
  
    ' 2. Check if card inserted is an ACOS card
    If Not CheckACOSCard Then
        InquiryBalance = SCARD_C_NOT_ACOS_CARD
        Call WriteLog("Please insert an ACOS card.")
        Exit Function
    End If
    Call WriteLog("ACOS card is detected.")
  
    ' 3. Issue INQUIRE ACCOUNT command using any arbitrary data and Certify key
    '    Arbitrary data is 1111h
    For indx = 0 To 3
        tmpArray(indx) = &H1
    Next indx
    retCode = InquireAccount(&H2, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
        InquiryBalance = retCode
        Exit Function
    End If

    ' 4. Issue GET RESPONSE command with Le = 19h or 25 bytes
    retCode = GetResponse("&H19")
    If retCode <> SCARD_S_SUCCESS Then
        InquiryBalance = retCode
        Exit Function
    End If

    ' 5. Check integrity of data returned by card
    ' 5.1. Build MAC input data
    ' 5.1.1. Extract the info from ACOS card in Dataout
    LastTran = RecvBuff(4)
    tmpBalance(1) = RecvBuff(7)
    tmpBalance(2) = RecvBuff(6)
    tmpBalance(2) = tmpBalance(2) * 256
    tmpBalance(3) = RecvBuff(5)
    tmpBalance(3) = tmpBalance(3) * 256
    tmpBalance(3) = tmpBalance(3) * 256
    tmpBalance(0) = tmpBalance(1) + tmpBalance(2) + tmpBalance(3)
    For indx = 0 To 3
        TTREFc(indx) = RecvBuff(indx + 17)
    Next indx
    For indx = 0 To 3
        TTREFd(indx) = RecvBuff(indx + 21)
    Next indx
    For indx = 0 To 5
        ATREF(indx) = RecvBuff(indx + 8)
    Next indx

    ' 5.1.2. Move data from ACOS card as input to MAC calculations
    tmpArray(4) = RecvBuff(4)          ' 4 BYTE MAC + LAST TRANS TYPE
    For indx = 0 To 2                  ' Copy BALANCE
        tmpArray(indx + 5) = RecvBuff(indx + 5)
    Next indx
    For indx = 0 To 5                  ' Copy ATREF
        tmpArray(indx + 8) = RecvBuff(indx + 8)
    Next indx
    tmpArray(14) = &H0
    tmpArray(15) = &H0
    For indx = 0 To 3                  ' Copy TTREFc
        tmpArray(indx + 16) = TTREFc(indx)
    Next indx
    For indx = 0 To 3                  ' Copy TTREFd
        tmpArray(indx + 20) = TTREFd(indx)
    Next indx

    ' 5.2. Generate applicable MAC values
    TmpStr = "33333333"
    For indx = 0 To Len(TmpStr) - 1
        tmpKey(indx) = Asc(Mid(TmpStr, indx + 1, 1))
    Next indx
    Call mac(tmpArray, tmpKey)
    '3Des
    'Call TripleMAC(tmpArray, tmpKey)
  
    ' 5.3. Compare MAC values
    For indx = 0 To 3
        If tmpArray(indx) <> RecvBuff(indx) Then
            Call WriteLog("MAC is incorrect, data integrity is jeopardized.")
            Exit For
        End If
    Next indx
  
    ' 6. Display relevant data from ACOS card
    Select Case LastTran
    Case 1
        TmpStr = "D"
    Case 2
        TmpStr = "R"
    Case 3
        TmpStr = "C"
    Case Else
        TmpStr = "N"
    End Select
    Call WriteLog("Last transaction is " & TmpStr & ".")
    Call WriteLog(tmpBalance(0) / 100)
    InquiryBalance = RightJustify(Format(tmpBalance(0) / 100, ""), 12) & TmpStr
End Function

Public Function WriteData(ByVal address As String, ByVal data As String) As Long
Dim indx As Integer
Dim TmpStr, ChkStr As String
Dim HiAddr, LoAddr, datalen As Byte
Dim tmpArray(0 To 56) As Byte
  
  ' 1. Validate input template
    If data = "" Then
        WtiteData = SCARD_A_BLANK_DATA
        Exit Function
    End If
  
    ' 2. Check User File selected by user
    If address = "AA11" Then
        HiAddr = &HAA
        LoAddr = &H11
        datalen = &HA
        ChkStr = "91 00 "
    End If

    If address = "BB22" Then
        HiAddr = &HBB
        LoAddr = &H22
        datalen = &H10
        ChkStr = "91 01 "
    End If

    If address = "CC33" Then
        HiAddr = &HCC
        LoAddr = &H33
        datalen = &H20
        ChkStr = "91 02 "
    End If

    ' 3. Select User File
    retCode = SelectFile(HiAddr, LoAddr)
  
    If retCode <> SCARD_S_SUCCESS Then
        WriteData = retCode
        Exit Function
    End If
  
    TmpStr = ""
  
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
  
    If TmpStr <> ChkStr Then
        Call WriteLog("ACR write data select user file : Return string is invalid. Value: " & TmpStr)
        Exit Function
    End If
  
    ' 4. Write data from text box to card
    TmpStr = data
  
    For indx = 0 To Len(TmpStr) - 1
        tmpArray(indx) = Asc(Mid(TmpStr, indx + 1, 1))
    Next indx
  
    retCode = WriteRecord(1, &H0, datalen, Len(TmpStr), tmpArray)
  
    If retCode <> SCARD_S_SUCCESS Then
        WriteData = retCode
        Exit Function
    End If
  
    Call WriteLog("ACR write data read from is written to card.")
    WriteData = retCode
End Function
Public Function ReadData(ByVal address As String) As String
Dim indx As Integer
Dim TmpStr, ChkStr As String
Dim HiAddr, LoAddr, datalen As Byte
  
    ' 1. Check User File selected by user
    If address = "AA11" Then
        HiAddr = &HAA
        LoAddr = &H11
        datalen = &HA
        ChkStr = "91 00 "
    End If
  
    If address = "BB22" Then
        HiAddr = &HBB
        LoAddr = &H22
        datalen = &H10
        ChkStr = "91 01 "
    End If

    If address = "CC33" Then
        HiAddr = &HCC
        LoAddr = &H33
        datalen = &H10
        ChkStr = "91 02 "
    End If
  
    ' 2. Select User File
    retCode = SelectFile(HiAddr, LoAddr)
  
    If retCode <> SCARD_S_SUCCESS Then
        ReadData = retCode
        Exit Function
    End If
  
    TmpStr = ""
  
    For indx = 0 To 1
        TmpStr = TmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
  
    If TmpStr <> ChkStr Then
        Call WriteLog("ACR read data, select file : Return string is invalid. Value: " & TmpStr)
        Exit Function
    End If

    ' 3. Read First Record of User File selected
    retCode = ReadRecord(&H0, datalen)
  
    If retCode <> SCARD_S_SUCCESS Then
        ReadData = retCode
        Exit Function
    End If

    ' 4. Display data read from card to textbox
    TmpStr = ""
    indx = 0
  
    While (RecvBuff(indx) <> &H0)
        'If indx < tData.MaxLength Then
        TmpStr = TmpStr & Chr(RecvBuff(indx))
        'End If
        indx = indx + 1
    Wend
    
    Call WriteLog("ACR read data is displayed : " & TmpStr)
    ReadData = TmpStr

End Function

