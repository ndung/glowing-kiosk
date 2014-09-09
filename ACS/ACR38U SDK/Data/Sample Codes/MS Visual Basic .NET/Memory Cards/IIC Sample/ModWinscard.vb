Imports System
Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices

<StructLayout(LayoutKind.Sequential)> _
Public Structure SCARD_IO_REQUEST

    Public dwProtocol As Integer
    Public cbPciLength As Integer

End Structure

<StructLayout(LayoutKind.Sequential)> _
Public Structure APDURec

    Public bCLA As Byte
    Public bINS As Byte
    Public bP1 As Byte
    Public bP2 As Byte
    Public bP3 As Byte
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=256)> _
    Public Data As Byte()
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=3)> _
    Public SW As Byte()
    Public IsSend As Boolean

End Structure

<StructLayout(LayoutKind.Sequential)> _
Public Structure SCARD_READERSTATE

    Public RdrName As String
    Public UserData As Integer
    Public RdrCurrState As Integer
    Public RdrEventState As Integer
    Public ATRLength As Integer
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=37)> _
    Public ATRValue As Byte()
End Structure


Public Class ModWinsCard

    Public Const SCARD_S_SUCCESS = 0
    Public Const SCARD_ATR_LENGTH = 33

    '===========================================================
    '  Memory Card type constants
    '===========================================================
    Public Const CT_MCU = &H0                     ' MCU
    Public Const CT_IIC_Auto = &H1                ' IIC (Auto Detect Memory Size)
    Public Const CT_IIC_1K = &H2                  ' IIC (1K)
    Public Const CT_IIC_2K = &H3                  ' IIC (2K)
    Public Const CT_IIC_4K = &H4                  ' IIC (4K)
    Public Const CT_IIC_8K = &H5                  ' IIC (8K)
    Public Const CT_IIC_16K = &H6                 ' IIC (16K)
    Public Const CT_IIC_32K = &H7                 ' IIC (32K)
    Public Const CT_IIC_64K = &H8                 ' IIC (64K)
    Public Const CT_IIC_128K = &H9                ' IIC (128K)
    Public Const CT_IIC_256K = &HA                ' IIC (256K)
    Public Const CT_IIC_512K = &HB                ' IIC (512K)
    Public Const CT_IIC_1024K = &HC               ' IIC (1024K)
    Public Const CT_AT88SC153 = &HD               ' AT88SC153
    Public Const CT_AT88SC1608 = &HE              ' AT88SC1608
    Public Const CT_SLE4418 = &HF                 ' SLE4418
    Public Const CT_SLE4428 = &H10                ' SLE4428
    Public Const CT_SLE4432 = &H11                ' SLE4432
    Public Const CT_SLE4442 = &H12                ' SLE4442
    Public Const CT_SLE4406 = &H13                ' SLE4406
    Public Const CT_SLE4436 = &H14                ' SLE4436
    Public Const CT_SLE5536 = &H15                ' SLE5536
    Public Const CT_MCUT0 = &H16                  ' MCU T=0
    Public Const CT_MCUT1 = &H17                  ' MCU T=1
    Public Const CT_MCU_Auto = &H18               ' MCU Autodetect

    '===============================================================
    ' CONTEXT SCOPE
    '===============================================================
    Public Const SCARD_SCOPE_USER = 0
    '===============================================================
    '  The context is a user context, and any database operations 
    '  are performed within the domain of the user.
    '===============================================================
    Public Const SCARD_SCOPE_TERMINAL = 1
    '===============================================================
    ' The context is that of the current terminal, and any database 
    ' operations are performed within the domain of that terminal.  
    ' (The calling application must have appropriate access 
    ' permissions for any database actions.)
    '===============================================================
    Public Const SCARD_SCOPE_SYSTEM = 2
    '===============================================================
    ' The context is the system context, and any database operations 
    ' are performed within the domain of the system.  (The calling
    ' application must have appropriate access permissions for any 
    ' database actions.)
    '===============================================================
    '=============================================================== 
    ' Context Scope
    '=============================================================== 
    Public Const SCARD_STATE_UNAWARE = &H0
    '===============================================================
    ' The application is unaware of the current state, and would like 
    ' to know. The use of this value results in an immediate return
    ' from state transition monitoring services. This is represented
    ' by all bits set to zero.
    '===============================================================
    Public Const SCARD_STATE_IGNORE = &H1
    '===============================================================
    ' The application requested that this reader be ignored. No other
    ' bits will be set.
    '===============================================================
    Public Const SCARD_STATE_CHANGED = &H2
    '===============================================================
    ' This implies that there is a difference between the state 
    ' believed by the application, and the state known by the Service
    ' Manager.When this bit is set, the application may assume a
    ' significant state change has occurred on this reader.
    '===============================================================
    Public Const SCARD_STATE_UNKNOWN = &H4
    '===============================================================
    ' This implies that the given reader name is not recognized by
    ' the Service Manager. If this bit is set, then SCARD_STATE_CHANGED
    ' and SCARD_STATE_IGNORE will also be set.
    '===============================================================
    Public Const SCARD_STATE_UNAVAILABLE = &H8
    '===============================================================
    ' This implies that the actual state of this reader is not
    ' available. If this bit is set, then all the following bits are
    ' clear.
    '===============================================================
    Public Const SCARD_STATE_EMPTY = &H10
    '===============================================================
    ' This implies that there is not card in the reader. If this bit
    ' is set, all the following bits will be clear.
    '===============================================================
    Public Const SCARD_STATE_PRESENT = &H20
    '===============================================================
    ' This implies that there is a card in the reader.
    '===============================================================
    Public Const SCARD_STATE_ATRMATCH = &H40
    '===============================================================
    ' This implies that there is a card in the reader with an ATR
    ' matching one of the target cards. If this bit is set,
    ' SCARD_STATE_PRESENT will also be set.  This bit is only returned
    ' on the SCardLocateCard() service.
    '===============================================================
    Public Const SCARD_STATE_EXCLUSIVE = &H80
    '===============================================================
    ' This implies that the card in the reader is allocated for 
    ' exclusive use by another application. If this bit is set,
    ' SCARD_STATE_PRESENT will also be set.
    '===============================================================
    Public Const SCARD_STATE_INUSE = &H100
    '===============================================================
    ' This implies that the card in the reader is in use by one or 
    ' more other applications, but may be connected to in shared mode.  
    ' If this bit is set, SCARD_STATE_PRESENT will also be set.
    '===============================================================
    Public Const SCARD_STATE_MUTE = &H200
    '===============================================================
    ' This implies that the card in the reader is unresponsive or not
    ' supported by the reader or software.
    '===============================================================
    Public Const SCARD_STATE_UNPOWERED = &H400
    '===============================================================
    ' This implies that the card in the reader has not been powered up.
    '===============================================================

    Public Const SCARD_SHARE_EXCLUSIVE = 1
    '===============================================================
    ' This application is not willing to share this card with other 
    'applications.
    '===============================================================
    Public Const SCARD_SHARE_SHARED = 2
    '===============================================================
    ' This application is willing to share this card with other 
    ' applications.
    '===============================================================
    Public Const SCARD_SHARE_DIRECT = 3
    '===============================================================
    ' This application demands direct control of the reader, so it 
    ' is not available to other applications.
    '===============================================================

    '===========================================================
    '   Disposition
    '===========================================================
    Public Const SCARD_LEAVE_CARD = 0   ' Don't do anything special on close
    Public Const SCARD_RESET_CARD = 1   ' Reset the card on close
    Public Const SCARD_UNPOWER_CARD = 2 ' Power down the card on close
    Public Const SCARD_EJECT_CARD = 3   ' Eject the card on close



    '===========================================================
    ' ACS IOCTL class
    '===========================================================
    Public Const FILE_DEVICE_SMARTCARD As Long = &H310000

    ' Reader action IOCTLs
    Public Const IOCTL_SMARTCARD_DIRECT As Long = FILE_DEVICE_SMARTCARD + 2050 * 4
    Public Const IOCTL_SMARTCARD_SELECT_SLOT As Long = FILE_DEVICE_SMARTCARD + 2051 * 4
    Public Const IOCTL_SMARTCARD_DRAW_LCDBMP As Long = FILE_DEVICE_SMARTCARD + 2052 * 4
    Public Const IOCTL_SMARTCARD_DISPLAY_LCD As Long = FILE_DEVICE_SMARTCARD + 2053 * 4
    Public Const IOCTL_SMARTCARD_CLR_LCD As Long = FILE_DEVICE_SMARTCARD + 2054 * 4
    Public Const IOCTL_SMARTCARD_READ_KEYPAD As Long = FILE_DEVICE_SMARTCARD + 2055 * 4
    Public Const IOCTL_SMARTCARD_READ_RTC As Long = FILE_DEVICE_SMARTCARD + 2057 * 4
    Public Const IOCTL_SMARTCARD_SET_RTC As Long = FILE_DEVICE_SMARTCARD + 2058 * 4
    Public Const IOCTL_SMARTCARD_SET_OPTION As Long = FILE_DEVICE_SMARTCARD + 2059 * 4
    Public Const IOCTL_SMARTCARD_SET_LED As Long = FILE_DEVICE_SMARTCARD + 2060 * 4
    Public Const IOCTL_SMARTCARD_LOAD_KEY As Long = FILE_DEVICE_SMARTCARD + 2062 * 4
    Public Const IOCTL_SMARTCARD_READ_EEPROM As Long = FILE_DEVICE_SMARTCARD + 2065 * 4
    Public Const IOCTL_SMARTCARD_WRITE_EEPROM As Long = FILE_DEVICE_SMARTCARD + 2066 * 4
    Public Const IOCTL_SMARTCARD_GET_VERSION As Long = FILE_DEVICE_SMARTCARD + 2067 * 4
    Public Const IOCTL_SMARTCARD_GET_READER_INFO As Long = FILE_DEVICE_SMARTCARD + 2051 * 4
    Public Const IOCTL_SMARTCARD_SET_CARD_TYPE As Long = FILE_DEVICE_SMARTCARD + 2060 * 4

    '===========================================================
    '   Error Codes
    '===========================================================
    Public Const SCARD_F_INTERNAL_ERROR = &H80100001
    Public Const SCARD_E_CANCELLED = &H80100002
    Public Const SCARD_E_INVALID_HANDLE = &H80100003
    Public Const SCARD_E_INVALID_PARAMETER = &H80100004
    Public Const SCARD_E_INVALID_TARGET = &H80100005
    Public Const SCARD_E_NO_MEMORY = &H80100006
    Public Const SCARD_F_WAITED_TOO_Integer = &H80100007
    Public Const SCARD_E_INSUFFICIENT_BUFFER = &H80100008
    Public Const SCARD_E_UNKNOWN_READER = &H80100009
    Public Const SCARD_E_TIMEOUT = &H8010000A
    Public Const SCARD_E_SHARING_VIOLATION = &H8010000B
    Public Const SCARD_E_NO_SMARTCARD = &H8010000C
    Public Const SCARD_E_UNKNOWN_CARD = &H8010000D
    Public Const SCARD_E_CANT_DISPOSE = &H8010000E
    Public Const SCARD_E_PROTO_MISMATCH = &H8010000F
    Public Const SCARD_E_NOT_READY = &H80100010
    Public Const SCARD_E_INVALID_VALUE = &H80100011
    Public Const SCARD_E_SYSTEM_CANCELLED = &H80100012
    Public Const SCARD_F_COMM_ERROR = &H80100013
    Public Const SCARD_F_UNKNOWN_ERROR = &H80100014
    Public Const SCARD_E_INVALID_ATR = &H80100015
    Public Const SCARD_E_NOT_TRANSACTED = &H80100016
    Public Const SCARD_E_READER_UNAVAILABLE = &H80100017
    Public Const SCARD_P_SHUTDOWN = &H80100018
    Public Const SCARD_E_PCI_TOO_SMALL = &H80100019
    Public Const SCARD_E_READER_UNSUPPORTED = &H8010001A
    Public Const SCARD_E_DUPLICATE_READER = &H8010001B
    Public Const SCARD_E_CARD_UNSUPPORTED = &H8010001C
    Public Const SCARD_E_NO_SERVICE = &H8010001D
    Public Const SCARD_E_SERVICE_STOPPED = &H8010001E
    Public Const SCARD_W_UNSUPPORTED_CARD = &H80100065
    Public Const SCARD_W_UNRESPONSIVE_CARD = &H80100066
    Public Const SCARD_W_UNPOWERED_CARD = &H80100067
    Public Const SCARD_W_RESET_CARD = &H80100068
    Public Const SCARD_W_REMOVED_CARD = &H80100069
    '===========================================================
    '   PROTOCOL
    '===========================================================
    Public Const SCARD_PROTOCOL_UNDEFINED = &H0           ' There is no active protocol.
    Public Const SCARD_PROTOCOL_T0 = &H1                  ' T=0 is the active protocol.
    Public Const SCARD_PROTOCOL_T1 = &H2                  ' T=1 is the active protocol.
    Public Const SCARD_PROTOCOL_RAW = &H10000             ' Raw is the active protocol.
    Public Const SCARD_PROTOCOL_DEFAULT = &H80000000      ' Use implicit PTS.
    '===========================================================
    '   READER STATE
    '===========================================================
    Public Const SCARD_UNKNOWN = 0
    '===============================================================
    ' This value implies the driver is unaware of the current 
    ' state of the reader.
    '===============================================================
    Public Const SCARD_ABSENT = 1
    '===============================================================
    ' This value implies there is no card in the reader.
    '===============================================================
    Public Const SCARD_PRESENT = 2
    '===============================================================
    ' This value implies there is a card is present in the reader, 
    'but that it has not been moved into position for use.
    '===============================================================
    Public Const SCARD_SWALLOWED = 3
    '===============================================================
    ' This value implies there is a card in the reader in position 
    ' for use.  The card is not powered.
    '===============================================================
    Public Const SCARD_POWERED = 4
    '===============================================================
    ' This value implies there is power is being provided to the card, 
    ' but the Reader Driver is unaware of the mode of the card.
    '===============================================================
    Public Const SCARD_NEGOTIABLE = 5
    '===============================================================
    ' This value implies the card has been reset and is awaiting 
    ' PTS negotiation.
    '===============================================================
    Public Const SCARD_SPECIFIC = 6
    '===============================================================
    ' This value implies the card has been reset and specific 
    ' communication protocols have been established.
    '===============================================================

    '==========================================================================
    ' Prototypes
    '==========================================================================

    Public Declare Function SCardEstablishContext Lib "Winscard.dll" (ByVal dwScope As Integer, _
                                                                      ByVal pvReserved1 As Integer, _
                                                                      ByVal pvReserved2 As Integer, _
                                                                      ByRef phContext As Integer) As Integer


    Public Declare Function SCardReleaseContext Lib "Winscard.dll" (ByVal hContext As Integer) As Integer


    Public Declare Function SCardConnect Lib "Winscard.dll" Alias "SCardConnectA" (ByVal hContext As Integer, _
                                                                                   ByVal szReaderName As String, _
                                                                                   ByVal dwShareMode As Integer, _
                                                                                   ByVal dwPrefProtocol As Integer, _
                                                                                   ByRef hCard As Integer, _
                                                                                   ByRef ActiveProtocol As Integer) As Integer


    Public Declare Function SCardDisconnect Lib "Winscard.dll" (ByVal hCard As Integer, _
                                                                ByVal Disposistion As Integer) As Integer

    Public Declare Function SCardBeginTransaction Lib "Winscard.dll" (ByVal hCard As Integer) As Integer


    Public Declare Function SCardEndTransaction Lib "Winscard.dll" (ByVal hCard As Integer, _
                                                                   ByVal Disposition As Integer) As Integer


    Public Declare Function SCardState Lib "Winscard.dll" (ByVal hCard As Integer, _
                                                           ByRef State As Integer, _
                                                           ByRef Protocol As Integer, _
                                                           ByRef ATR As Byte, _
                                                           ByRef ATRLen As Integer) As Integer

    Public Declare Function SCardStatus Lib "Winscard.dll" Alias "SCardStatusA" (ByVal hCard As Integer, _
                                                                                 ByVal szReaderName As String, _
                                                                                 ByRef pcchReaderLen As Integer, _
                                                                                 ByRef State As Integer, _
                                                                                 ByRef Protocol As Integer, _
                                                                                 ByRef ATR As Byte, _
                                                                                 ByRef ATRLen As Integer) As Integer

    Public Declare Function SCardTransmit Lib "Winscard.dll" (ByVal hCard As Integer, _
                                                              ByRef pioSendRequest As SCARD_IO_REQUEST, _
                                                              ByRef SendBuff As Byte, _
                                                              ByVal SendBuffLen As Integer, _
                                                              ByRef pioRecvRequest As SCARD_IO_REQUEST, _
                                                              ByRef RecvBuff As Byte, _
                                                              ByRef RecvBuffLen As Integer) As Integer



    Public Declare Function SCardListReaders Lib "Winscard.dll" Alias "SCardListReadersA" (ByVal hContext As Integer, _
                                                                                           ByVal mzGroup As String, _
                                                                                           ByVal ReaderList As String, _
                                                                                           ByRef pcchReaders As Integer) As Integer



    Public Declare Function SCardGetStatusChange Lib "Winscard.dll" Alias "SCardGetStatusChangeA" (ByVal hContext As Integer, _
                                                                                                   ByVal TimeOut As Integer, _
                                                                                                   ByRef ReaderState As SCARD_READERSTATE, _
                                                                                                   ByVal ReaderCount As Integer) As Integer



    Public Declare Function SCardControl Lib "Winscard.dll" (ByVal hCard As Integer, _
                                                              ByVal dwControlCode As Integer, _
                                                              ByRef pvInBuffer As Byte, _
                                                              ByVal cbInBufferSize As Integer, _
                                                              ByRef pvOutBuffer As Byte, _
                                                              ByVal cbOutBufferSize As Integer, _
                                                              ByRef pcbBytesReturned As Integer) As Integer

End Class
'=========================================================================================
'   Author :  Mary Anne C.A. Arana
'   Module :  ModWinscard.vb
'   Company:  Advanced Card Systems Ltd.
'   Date   :  July 11, 2005
'   Revision Date: August 23, 2005 
'=========================================================================================