Attribute VB_Name = "SmartPayout"
Option Explicit

' --------------------------------------------------------
' COM ENUM MODULE
' --------------------------------------------------------bl
Private Const HKEY_CLASSES_ROOT = &H80000000
Private Const HKEY_CURRENT_CONFIG = &H80000005
Private Const HKEY_CURRENT_USER = &H80000001
Private Const HKEY_DYN_DATA = &H80000006
Private Const HKEY_LOCAL_MACHINE = &H80000002
Private Const HKEY_PERFORMANCE_DATA = &H80000004
Private Const HKEY_USERS = &H80000003
Private Const REG_BINARY = 3
Private Const REG_DWORD = 4
Private Const REG_DWORD_BIG_ENDIAN = 5
Private Const REG_DWORD_LITTLE_ENDIAN = 4
Private Const REG_EXPAND_SZ = 2
Private Const REG_LINK = 6
Private Const REG_MULTI_SZ = 7
Private Const REG_NONE = 0
Private Const REG_RESOURCE_LIST = 8
Private Const REG_SZ = 1

' Return codes from Registration functions.
Private Const ERROR_SUCCESS = 0&
Private Const ERROR_BADDB = 1009&
Private Const ERROR_BADKEY = 1010&
Private Const ERROR_CANTOPEN = 1011&
Private Const ERROR_CANTREAD = 1012&
Private Const ERROR_CANTWRITE = 1013&
Private Const ERROR_OUTOFMEMORY = 14&
Private Const ERROR_INVALID_PARAMETER = 87&
Private Const ERROR_ACCESS_DENIED = 5&
Private Const ERROR_NO_MORE_ITEMS = 259&
Private Const ERROR_MORE_DATA = 234&

' Read/Write permissions:
Private Const REG_OPTION_NON_VOLATILE = 0
Private Const KEY_QUERY_VALUE = &H1
Private Const KEY_SET_VALUE = &H2
Private Const KEY_CREATE_SUB_KEY = &H4
Private Const KEY_ENUMERATE_SUB_KEYS = &H8
Private Const KEY_NOTIFY = &H10
Private Const KEY_CREATE_LINK = &H20
Private Const SYNCHRONIZE = &H100000
Private Const STANDARD_RIGHTS_ALL = &H1F0000
Private Const READ_CONTROL = &H20000
Private Const WRITE_DAC = &H40000
Private Const WRITE_OWNER = &H80000
Private Const STANDARD_RIGHTS_REQUIRED = &HF0000
Private Const STANDARD_RIGHTS_READ = READ_CONTROL
Private Const STANDARD_RIGHTS_WRITE = READ_CONTROL
Private Const STANDARD_RIGHTS_EXECUTE = READ_CONTROL
Private Const KEY_READ = STANDARD_RIGHTS_READ Or KEY_QUERY_VALUE Or KEY_ENUMERATE_SUB_KEYS Or KEY_NOTIFY
Private Const KEY_WRITE = STANDARD_RIGHTS_WRITE Or KEY_SET_VALUE Or KEY_CREATE_SUB_KEY
Private Const KEY_EXECUTE = KEY_READ

Private Const KEY_ALL_ACCESS = ((STANDARD_RIGHTS_ALL Or KEY_QUERY_VALUE Or _
   KEY_SET_VALUE Or KEY_CREATE_SUB_KEY Or KEY_ENUMERATE_SUB_KEYS Or _
   KEY_NOTIFY Or KEY_CREATE_LINK) And (Not SYNCHRONIZE))


Type FILETIME
  dwLowDateTime As Long
  dwHighDateTime As Long
End Type

Type FileInfo
   wLength            As Integer
   wValueLength       As Integer
   szKey              As String * 16
   dwSignature        As Long
   dwStrucVersion     As Long
   dwFileVersionMS    As Long
   dwFileVersionLS    As Long
End Type

' NOTE: The following Declare statements are case sensitive.

Private Declare Function GetFileVersionInfo& Lib "Version" _
     Alias "GetFileVersionInfoA" (ByVal FileName$, _
     ByVal dwHandle&, ByVal cbBuff&, ByVal lpvData$)
Private Declare Function GetFileVersionInfoSize& Lib "Version" Alias _
     "GetFileVersionInfoSizeA" (ByVal FileName$, dwHandle&)
Private Declare Sub hmemcpy Lib "kernel32" Alias "RtlMoveMemory" _
     (hpvDest As Any, hpvSource As Any, ByVal cbBytes&)

Private Declare Function RegOpenKeyEx Lib "advapi32.dll" Alias "RegOpenKeyExA" (ByVal hKey As Long, ByVal lpSubKey As String, ByVal ulOptions As Long, ByVal samDesired As Long, phkResult As Long) As Long
Private Declare Function RegQueryInfoKey Lib "advapi32.dll" Alias "RegQueryInfoKeyA" (ByVal hKey As Long, ByVal lpClass As String, lpcbClass As Long, ByVal lpReserved As Long, lpcSubKeys As Long, lpcbMaxSubKeyLen As Long, lpcbMaxClassLen As Long, lpcValues As Long, lpcbMaxValueNameLen As Long, lpcbMaxValueLen As Long, lpcbSecurityDescriptor As Long, lpftLastWriteTime As Long) As Long
Private Declare Function RegEnumValue Lib "advapi32.dll" Alias "RegEnumValueA" (ByVal hKey As Long, ByVal dwIndex As Long, ByVal lpValueName As String, lpcbValueName As Long, ByVal lpReserved As Long, lpType As Long, lpData As Byte, lpcbData As Long) As Long
Declare Function RegEnumKeyEx Lib "advapi32.dll" Alias "RegEnumKeyExA" (ByVal hKey As Long, ByVal dwIndex As Long, ByVal lpName As String, lpcbName As Long, lpReserved As Long, ByVal lpClass As String, lpcbClass As Long, lpftLastWriteTime As FILETIME) As Long
Private Declare Function RegQueryValueEx Lib "advapi32.dll" Alias "RegQueryValueExA" (ByVal hKey As Long, ByVal lpValueName As String, ByVal lpReserved As Long, lpType As Long, lpData As Any, lpcbData As Long) As Long
Declare Function FormatMessage Lib "kernel32" Alias "FormatMessageA" (ByVal dwFlags As Long, lpSource As Any, ByVal dwMessageId As Long, ByVal dwLanguageId As Long, ByVal lpBuffer As String, ByVal nSize As Long, Arguments As Long) As Long
Private Declare Function RegCloseKey Lib "advapi32.dll" (ByVal hKey As Long) As Long

' --------------------------------------------------------
' Declare MODULE
' --------------------------------------------------------
Public Type RAM_UPDATE_STATUS
    totalRamBlocks As Long
    currentRamBlocks As Long
    ramState As Long
End Type

Public Const DOWNLOAD_COMPLETE = &H100000

' error code definitions
Public Const OPEN_FILE_ERROR = &H100001
Public Const READ_FILE_ERROR = &H100002
Public Const NOT_ITL_FILE = &H100003
Public Const PORT_OPEN_FAIL = &H100004
Public Const SYNC_CONNECTION_FAIL = &H100005
Public Const SECURITY_PROTECTED_FILE = &H100006

Public Const DATA_TRANSFER_FAIL = &H100010
Public Const PROG_COMMAND_FAIL = &H100011
Public Const HEADER_FAIL = &H100012
Public Const PROG_STATUS_FAIL = &H100013
Public Const PROG_RESET_FAIL = &H100014
Public Const DOWNLOAD_NOT_ALLOWED = &H100015

Public Const V_CALIBRATION_DATA_READY = &HCF

Public Declare Function DownloadArrayData Lib "InnTechSSP.dll" (ByRef data As Byte, ByVal iPort As Integer) As Long

Public Declare Function ProcessBarCodeData Lib "InnTechSSP.dll" (ByRef data As Byte, ByVal dLen As Byte, ByRef scanData As Byte, ByRef scanLength As Byte, ByRef tdata As Byte) As Long
Public Declare Function SetDownSpeedFlag Lib "InnTechSSP.dll" (ByVal flag As Long) As Integer

' dll function to set port baud rate
Public Declare Function SetBaud Lib "InnTechSSP.dll" (ByVal iBaud As Integer) As Integer
Public Declare Function SetFastBaud Lib "InnTechSSP.dll" (ByVal iBaud As Long) As Integer
' sets/resets the DTR pin (0 and 1)
Public Declare Function SetDTR Lib "InnTechSSP.dll" (ByVal iDTR As Integer) As Integer
' set the sequence byte (0 or &h80)
Public Declare Function FlipSeq Lib "InnTechSSP.dll" () As Integer

'dll function to open a comms port (iPort)
Public Declare Function OpenPort Lib "InnTechSSP.dll" (ByVal iPort As Integer) As Integer
' dll function to close a comms port
Public Declare Function CloseComm Lib "InnTechSSP.dll" () As Integer
' dll function to send an SSP command and recieve a reply
' the command is sent and returned in the form of a structure (UDT)
' rxStatus is not used when sending, datalen is the number of command bytes
' array1 contains the array bytes
Public Declare Function Command Lib "InnTechSSP.dll" (src As UDT) As UDT

Public Declare Function SetRetryParameters Lib "InnTechSSP.dll" (rRtry As RTRY) As Integer

Public Declare Function GetLastRxPacket Lib "InnTechSSP.dll" () As UDT
Public Declare Function GetLastTxPacket Lib "InnTechSSP.dll" () As UDT
Public Declare Function SSPKillWaitTime Lib "InnTechSSP.dll" () As Integer
Public Declare Function GetBaud Lib "InnTechSSP.dll" () As Long
Public Declare Function DownloadRamFile Lib "InnTechSSP.dll" (ByVal szName As String, ByVal iPort As Integer) As Long
Public Declare Function DownLoadFile Lib "InnTechSSP.dll" Alias "DownloadFile" (ByVal szName As String, ByVal iPort As Integer) As Long
Public Declare Function GetDownloadData Lib "InnTechSSP.dll" () As Long
Public Declare Function GetRamDownloadData Lib "InnTechSSP.dll" () As Long

Public Declare Function GetRamDownloadStatus Lib "InnTechSSP.dll" (ByRef rdata As RAM_UPDATE_STATUS) As Long


Public Declare Function SSPLogFile Lib "InnTechSSP.dll" (ByVal szName As String) As Long

Public Declare Function GetSSPDLLVersion Lib "InnTechSSP.dll" (ByRef Major As Byte, ByRef Minor As Byte) As Long
Public Declare Function SetDownloadOptionFlag Lib "InnTechSSP.dll" (ByVal opt As Byte) As Long

Public Declare Function GetDownloadSSPAddress Lib "InnTechSSP.dll" () As Integer
Public Declare Function SetDownloadSSPAddress Lib "InnTechSSP.dll" (ByVal address As Integer) As Integer


' the user defined structure
Public Type UDT
    rxStatus As Integer
    datalen As Byte
    array1(254) As Byte
End Type

Public Type RTRY
    rDelay As Byte
    rRetries As Byte
End Type

Public reTry As RTRY

' --------------------------------------------------------
' Def Mode MODULE
' --------------------------------------------------------
Public Const REJECT_NOTE_CMD = &H8
Public Const BULB_ON_CMD = &H3
Public Const BULB_OFF_CMD = &H4
Public Const DISABLE_CMD = &H9
Public Const DISPENSE_CMD = &H12
Public Const ENABLE_CMD = &HA
Public Const POLL_CMD = &H7
Public Const RESET_CMD = &H1
Public Const PROGRAM_CURRENCY_CMD = &HB
Public Const SETUP_REQUEST_CMD = &H5
Public Const SET_INHIBITS_CMD = &H2
Public Const HOST_PROTOCOL = &H6
Public Const SERIAL_NUMBER = &HC
Public Const SET_INHIBITS = &H2
Public Const SYNC_CMD = &H11
Public Const FILE_HEADER = &H12
Public Const PROG_STATUS = &H16
Public Const UNIT_DATA = &HD
Public Const VALUE_DATA = &HE
Public Const SECURITY_DATA = &HF
Public Const CHANNEL_RETEACH = &H10
Public Const RAW_DATA_MODE = &HB
Public Const REF_NOTE_MODE = &H17
Public Const READ_REAR_FLAP = &H15
Public Const GET_ADC_DATA = &H48

Public Const OK = &HF0

Public Const GET_ROLLER_SEC = &H56
Public Const GET_SLOT_COUNT = &H57
Public Const GRAD_TEST = &H30
Public Const GRAD_DATA = &H31

Public Const SLAVE_RESET = &HF1
Public Const COMMAND_NOT_KNOWN = &HF2
Public Const WRONG_No_PARAMETERS = &HF3
Public Const PARAMETER_OUT_RANGE = &HF4
Public Const COMMAND_NOT_PROCESS = &HF5
Public Const SOFTWARE_ERROR = &HF6
Public Const INHIB_TEST = &HF
Public Const EEPROM = &H6
Public Const GET_LAST_VALID_FACE = &H18
Public Const SET_BAUD_RATE = &H20
Public Const GET_TUBE_RESULTS = &HE
Public Const GET_BD_RESULTS = &H1E
Public Const DISABLED = &HE8
Public Const GET_MAG_FACE = &H27
Public Const GET_SENSOR_TYPE = &H39
Public Const NOTE_READ = &HEF
Public Const SSP_ADDRESS = &H36
Public Const RESET_cctalk_KEY = &H37
Public Const SET_cctADDRESS = &H38
Public Const ERASE_MAG_FACE = &H35
Public Const CAL_TEST_START = &H29
Public Const GET_STACKER_DATA = &H53
Public Const GET_FULL_FIRMWARE = &H44
Public Const GET_VALIDATOR_STATUS = &H46
Public Const GET_VALIDATOR_STATS = &H47
Public Const EEPROM_PAGE = &H48
Public Const DATA_COLLECT_MODE = &H49
Public Const GET_SPF_VALIDATION = &H50
Public Const GET_RAW_JRN = &H51

Public Const READ_NV7_FLAG_POS = &H53


Public Const GET_TWIST_DATA = &H60
Public Const GET_TW_SLOT_DATA = &H61

Public Const DISPENSING = &HE1
Public Const GET_BOARD_TYPE = &H1F
Public Const LAST_REJECT = &H17

Public Const REJECTING_MS = &HED

Public Const COMMIT_TO_FLASH = &H14
Public Const NO_SENSOR_OFF = &H25
Public Const HOLD = &H18
Public Const RAW_MODE_PLUS_BD = &H32
Public Const BD_TX_MODE = &H33
Public Const DISPENSED = &HE2

Public Const REJECTED_MS = &HEC
Public Const FRAUD_ATTEMPT_MS = &HE6

Public Const NOTE_STACKED = &HEB
Public Const STACKING = &HCC
Public Const CREDIT = &HEE
Public Const SAFE_JAM = &HEA
Public Const UNSAFE_JAM = &HE9
Public Const STACKER_FULL = &HE7
Public Const STACKER_TEST = &H11
Public Const DA_TEST = &HB
Public Const EEPROM_FITTED = &H3C
Public Const ADP_TEST = &H3D
Public Const ADP_CLEAR_CREDIT = &H3E
Public Const ADP_SET_INHIBIT = &H3F
Public Const BD_TYPE_CODE = &H3F
Public Const DOWNLOAD_RAM_CODE = &H40
Public Const DOWNLOAD_RAM_STATUS = &H41
Public Const GET_DATASET_CODE = &H16
Public Const FLASH_TYPE = &H3E

Public Const GET_SPF_NORM = &H42
Public Const GET_FACE_NUMBER = &H43
Public Const GET_VALIDATION_DATA = &H45
Public Const PROG_RAM = &H3
Public Const VEND_TEST = &H10
Public Const PLS_TEST = &H50
Public Const PLS_DATA = &H51

Public Const READ_SWITCHES = &H34
Public Const SET_DIP_IGNORE = &H52


Public Const FEED_TO_START = &H26
Public Const EJECT_NOTE = &H27
Public Const RUN_MOTOR_TEST = &H28
Public Const GET_MAG_FACE_LIST = &H31

Public Const GET_JR_MAG = &H28
Public Const STACKER_CUR = &H1B
Public Const GET_MAG_COMP = &H29
Public Const GET_MAG_PULSES = &H30

Public Const GET_PC_DATA = &H3F
Public Const STACK_DOWN = &HC
Public Const STACK_UP = &HD
Public Const STACK_OFF = &HE
Public Const STX = &H7F
Public Const NV4 = &H2
Public Const NV7 = &H5
Public Const ENABLE_OP = &H7
Public Const DISABLE_OP = &H6
Public Const FaceData = &H1
Public Const EXPANSION_COMMAND = &H30
Public Const DIAGNOSTICS = &H13
Public Const MEM = &H15
Public Const GLOBAL_FACE_INFO = &HD
Public Const faceName = &H2
Public Const READ_MEMORY = &H1
Public Const WRITE_MEMORY = &H11
Public Const WRITE_EE_MEMORY = &H12
Public Const GET_JRN = &HA
Public Const GET_CAL_DATA = &HC
Public Const RE_INITIALISE = &HF
Public Const RE_CALIBRATE = &H25
Public Const FRONT_PWM = &H26
Public Const CAL_READ_TEST_MOVE = &H28
Public Const PATHMOTORON = &H1
Public Const PATHMOTOROFF = &H2
Public Const BEZEL_LED_ON = &H3
Public Const BEZEL_LED_OFF = &H4
Public Const READ_SENSOR = &H5
Public Const FRONT_SENSOR = &H1
Public Const STACKER_CYCLE = &H8
Public Const DAISY_WHEEL = &H9
Public Const SENSOR_RESPONSE = &HA
Public Const BTEST = &HB
Public Const POSITION_TEST = &H12
Public Const GET_STATS_ENDIAN_DATA = &H69


Public Const WRITE_CONFIG_RAM = &H22
Public Const SET_CONFIG_DATA = &H23

Public Const ESCROW_TEST = &H3A

Public Const GET_JR_BD_DATA = &H3A
Public Const SEND_CONFIG = &H1
Public Const GET_CONFIG = &H2
Public Const SET_ESCROW = &H4
Public Const SET_ENABLE = &H3
Public Const SET_RESET = &H5

Public Const MANUF_CODE = &H30

Public Const COMMS_LEVEL = &H3D

 Public Const SSP_OPTIONS = &H58

'BV defs
Public Const GET_STATUS = &H60
Public Const GET_SENSOR_INFO_DATA = &H61
Public Const GET_SENSOR_CONFIG_DATA = &H65

Public Const GET_MOTOR_INFO_DATA = &H66
Public Const GET_MOTOR_CONFIG_DATA = &H67

Public Const MOTOR_DIAG = &H41
Public Const BV_DATA_COLLECT_MODE = &H64
Public Const BV_GET_NOTE_DATA = &H62

Public Const jr_READ_DATA = &H1
Public Const jr_LENGTH_DATA = &H2

Public Const BV_OPTION_TABLE = &H72

Public Const ssp_MODE_NORMAL = &H0
Public Const ssp_MODE_DATA_COLLECT = &H1
Public Const ssp_STACKER_COLLECT = &H2
Public Const ssp_CAL_DATA_COLLECT = &H3
Public Const ssp_INIT_DATA_COLLECT = &H4
Public Const ssp_SET_SHORT_NOTE = &H5
Public Const V_DATA_READY = &HCD

Public Const LENS_SENSOR = 10
Public Const LENS_START_SENSOR = 11
Public Const POSITION_SENSOR = 12

Public Const tst_MODE_OFF = &H10
Public Const tst_MODE_READ_CYCLE_TEST = &H11
Public Const V_TEST_DATA_READY = &HD2
Public Const SET_TEST_MODE = &H70


Public Const V_STACKER_DATA_READY = &HCE
Public Const V_INITIALISE_DATA_READY = &HD0

Public Const ram_IDLE = &H31
Public Const ram_CURRENCY_UPDATE = &H30
Public Const ram_OK_ACK = &H32
Public Const ram_RESET = &H53
Public Const ram_FIRMWARE_UPDATE = &H50

Private Const VPS_HDR_CODE = &H42
Private Const NV9_HDR_CODE = &H9
Private Const NV10_HDR_CODE = &HA
Private Const BV20_HDR_CODE = &HB
Private Const BV50_HDR_CODE = &HC
Private Const BV100_HDR_CODE = &HE
Private Const NV200_HDR_CODE = &H10
Private Const BV20_ASIC_HDR_CODE = &H11
Private Const BV100_ASIC_HDR_CODE = &H12

 
Public Const BEZEL_SETUP_TEST = &H44
Public Const COMMIT_DATA = &H32

' the user defined structure
Public Type ssp_setup_data
  unit_type As Byte
  firmware_version As String
  country_code As String
  value_multiplier As Long
  number_of_channels As Byte
  channel_values(15) As Byte
  channel_security(15) As Byte
  reteach_count As Long
  protocol_version As Byte
End Type
Public Type BVSensor
    Name As String
    Code As Byte
    Type As Byte
    Gain1 As Byte
    Gain2 As Byte
    PathTgt As Byte
    PaperTgt As Byte
    ReadGain As Byte
    CalGain As Byte
    Clear As Byte
    Detect As Byte
    Threshold As Integer
End Type
Public Type SensorInitStatus
  Sensor As Byte
  status As eInitStatus
  Gain1 As Byte
  Gain2 As Byte
End Type
Public Enum eInitStatus
  cal_NOT_DONE = &H0
  cal_PAPER_INIT_BUSY = &H1
  cal_PAPER_INIT_COMPLETE = &H2
  cal_PATH_INIT_BUSY = &H3
  cal_PATH_INIT_COMPLETE = &H4
  cal_CALIBRATION_FAILED = &H5
End Enum
 Public Const GET_CALIBRATE_STATUS = &H76


Public Type face_struct
  face_name As String
  channel As Byte
  pulses As Long
End Type
Public Type UDTssp
    rxStatus As Integer
    datalen As Byte
    array1(254) As Byte
End Type
Public Enum NV9_SENSOR_TYPES
  NV7_OPTICAL_SENSORS = 6
  NV7_MAG_SENSORS = 10
  NV7_MULTI_MAG_SENSORS = 11
  SYNC_DEMOD_SENSORS = 21
  STANDARD_SENSORS = 20
  BV20_STANDARD_TYPE = BV20_HDR_CODE
  BV50_TYPE = BV50_HDR_CODE
  BV100_STANDARD_TYPE = BV100_HDR_CODE
  NV200_TYPE = NV200_HDR_CODE
  BV20_ASIC_TYPE = BV20_ASIC_HDR_CODE
  BV100_ASIC_TYPE = BV100_ASIC_HDR_CODE
End Enum

Public Enum download_file_type
  DOWNLOAD_CURRENCY = 1
  Download_Firmware = 0
End Enum
Public Enum download_speed_type
  DOWNLOAD_9600 = 0
  DOWNLOAD_28800 = &H6
  DOWNLOAD_115200 = &H88
End Enum

Public Const NO_VALIDATOR = "NOVAL"
Public Const NV7_VALIDATOR = "NV7"
Public Const NV8_VALIDATOR = "NV8"
Public Const NV9_VALIDATOR = "NV9"
Public Const NV9_SD_VALIDATOR = "NV9SD"
Public Const NV10_VALIDATOR = "NV10"
Public Const NV10_SD_VALIDATOR = "NV10SD"
Public Const BV100_VALIDATOR = "BV100"
Public Const PROG_OPTION_PARMETERS = &H68


'
'' target update register constants
'Public Const upd_REG_SUPPORT_VARI_SPEED_DOWNLOAD = 1
'Public Const upd_REG_SUPPORT_BASE_HW_ASIC = 2
'Public Const upd_REG_SUPPORT_MULTI_BEZEL = 4
'Public Const upd_REG_SUPPORT_INIT_DATA = 8
'Public Const upd_REG_SUPPORT_OP_4 = 16
'Public Const upd_REG_SUPPORT_OP_5 = 32
'Public Const upd_REG_SUPPORT_OP_6 = 64
'Public Const upd_REG_SUPPORT_OP_7 = 128

Public Enum eSupportedUpdateType
    upd_REG_SUPPORT_VARI_SPEED_DOWNLOAD = 1
    upd_REG_SUPPORT_BASE_HW_ASIC = 2
    upd_REG_SUPPORT_MULTI_BEZEL = 4
    upd_REG_SUPPORT_INIT_DATA = 8
    upd_REG_SUPPORT_OP_4 = 16
    upd_REG_SUPPORT_OP_5 = 32
    upd_REG_SUPPORT_OP_6 = 64
    upd_REG_SUPPORT_OP_7 = 128
End Enum

Public Const GET_CONFIG_TABLE_START = &H71

Public Const rmd_DOWNLOAD_RAM_FILE = &H100001
Public Const rmd_RAM_DOWNLOAD_IDLE = &H100000
Public Const rmd_ESTABLISH_COMS = &H100002
Public Const rmd_INITIATE_UPDATE_CMD = &H100003
Public Const rmd_INITIATE_UPDATE_HDR = &H100004
Public Const rmd_SEND_UPDATE_DATA = &H100005


'DA3
Public Const MPB_DATA = &H7
Public Const MPB_GET_FILE_NUMBER = &H0
Public Const MPB_FILE_TABLES = &H1
Public Const MPB_ERASE = &H2
Public Const MPB_COPY = &H3
Public Const MPB_MEM_COPY_STATUS = &H4
Public Const MEM_INFO = &H8
Public Const MEM_INFO_GET_MANUFACTURE_DATA = &H0

Public Const MEM_COPY_FIRMWARE = &H9
Public Const V_END_OF_PROCESS = &HD1
Public Const V_MEM_CARD_INSERT = &HD3
Public Const V_MEM_CARD_REMOVE = &HD4

'BAR CODE
Public Const GET_BAR_CODE_CONFIG = &H23
Public Const SET_BAR_CODE_CONFIG = &H24
Public Const GET_BAR_CODE_INHIBIT = &H25
Public Const SET_BAR_CODE_INHIBIT = &H26
Public Const GET_BAR_CODE_DATA = &H27

Public Const V_BAR_CODE_VALIDATED = &HE5
Public Const V_BAR_CODE_TICKET_ACK = &HD1

'Enumeration of bit-shifting
Public Enum dcShiftDirection
    SLeft = -1
    SRight = 0
End Enum


' --------------------------------------------------------
' PayDevice MODULE
' --------------------------------------------------------
Public Type PAY_VALUES
    RequestedDispenseValue As Long
    RequestedFloatValue As Long
    StoredValue As Long
    CashBoxValue As Long
    ValueDispensed As Long
    ValuePaidIn As Long
    ValueFloated As Long
    ValueToCashBox As Long
End Type


Public Type PAY_SYSTEM_ACCOUNT
    Hopper As PAY_VALUES
    Payout As PAY_VALUES
    AmountToPay As Long
    TotalSystemValue As Long
    TotalStoredValue As Long
    TotalFloatTarget As Long
    TotalFloatedAmount As Long
End Type

' --------------------------------------------------------
' SSPProc MODULE
' --------------------------------------------------------
Public Const gen_R_OK = &HF0
Public Const gen_R_SLAVE_RESET = &HF1
Public Const gen_R_COMMAND_NOT_KNOWN = &HF2
Public Const gen_R_WRONG_NO_PARAMETERS = &HF3
Public Const gen_R_PARAMETER_OUT_RANGE = &HF4
Public Const gen_R_COMMAND_CANNOT_BE_PROC = &HF5
Public Const gen_R_SOFTWARE_ERROR = &HF6
Public Const gen_R_CHECKSUM_ERROR = &HF7
Public Const gen_R_FAIL = &HF8
Public Const gen_R_HEADER_FAIL = &HF9
Public Const gen_R_DISABLED = &HE8
Public Const gen_R_KEY_NOT_SET = &HFA

Public PacketCount As Long

Public Enum PAYOUT_REQ_STATUS
    LEVEL_NOT_SUFFICIENT = 1
    NOT_EXACT_AMOUNT
    DEVICE_BUSY
    DEVICE_DISABLED
    REQ_STATUS_OK = 255
End Enum

Public Type COINS
    CoinValue As Long
    CoinLevel As Long
End Type

Public Const MAX_COIN_CHANNEL = 30

Public Type DEVICE_PAY_DATA
    NumberOfCoinValues As Byte
    CoinsToPay(MAX_COIN_CHANNEL - 1) As COINS
    CoinsInHopper(MAX_COIN_CHANNEL - 1) As COINS
    FloatMode(MAX_COIN_CHANNEL - 1) As Byte
    SplitQty(MAX_COIN_CHANNEL - 1) As Integer
    AmountPayOutRequest As Long
    FloatAmountRequest As Long
    MinPayout As Integer
    mode As Byte
    DealMode As Byte
    LevelMode As Byte
    MinPayoutRequest As Long
End Type

Public Enum VALUE_CHECK_MODE
    CHECK_DISPENSE
    CHECK_FLOAT
End Enum

Public Enum PORT_STATUS
    PORT_CLOSED
    port_open
    PORT_ERROR
    ssp_reply_ok
    SSP_PACKET_ERROR
    SSP_CMD_TIMEOUT
End Enum

Public Type EightByteNumber
    LoValue As Long
    Hivalue As Long
End Type

Public Type SSP_KEYS
    Generator As EightByteNumber
    Modulus As EightByteNumber
    HostInter As EightByteNumber
    HostRandom As EightByteNumber
    SlaveInterKey As EightByteNumber
    SlaveRandom As EightByteNumber
    KeyHost As EightByteNumber
    KeySlave As EightByteNumber  ' test purposes only
End Type

Public Type SSP_FULL_KEY
    FixedKeyLowValue As Long
    FixedKeyHighValue As Long
    EncryptKeyLowValue As Long
    EncryptkeyHighValue As Long
End Type

Public Type SSP_PACKET
    PacketTime As Integer
    PacketLength As Byte
    PacketData(254) As Byte
End Type


Public Type SSP_COMMAND_INFO
    CommandName As String
    LogFileName As String
    Encrypted As Byte
    Transmit As SSP_PACKET
    Recieve As SSP_PACKET
    PreEncryptTransmit As SSP_PACKET
    PreEncryptRecieve As SSP_PACKET
End Type


Public Type SSP_COMMAND
    key As SSP_FULL_KEY
    BaudRate As Long
    TimeOut As Long
    PortNumber As Byte
    sspAddress As Byte
    RetryLevel As Byte
    EncryptionStatus As Byte
    CommandDataLength As Byte
    CommandData(254) As Byte
    ResponseStatus As Byte
    ResponseDataLength  As Byte
    ResponseData(254) As Byte
    ignoreerror As Byte
End Type


Public Const cmd_SSP_SET_GENERATOR = &H4A
Public Const cmd_SSP_SET_MODULUS = &H4B
Public Const cmd_SSP_REQ_KEY_EXCHANGE = &H4C


Public Declare Function TestSSPSlaveKeys Lib "ITLSSPProc.dll" (ByRef key As SSP_KEYS) As Integer
Public Declare Function CreateSSPHostEncryptionKey Lib "ITLSSPProc.dll" (ByRef key As SSP_KEYS) As Integer
Public Declare Function SSPSendCommand Lib "ITLSSPProc.dll" (ByRef sspc As SSP_COMMAND, ByRef sspInfo As SSP_COMMAND_INFO) As Integer
Public Declare Function OpenSSPComPort Lib "ITLSSPProc.dll" (ByRef sspc As SSP_COMMAND) As Integer
Public Declare Function CloseSSPComPort Lib "ITLSSPProc.dll" () As Integer
Public Declare Function OpenSSPComPort2 Lib "ITLSSPProc.dll" (ByRef sspc As SSP_COMMAND) As Integer
Public Declare Function CloseSSPComPort2 Lib "ITLSSPProc.dll" () As Integer
Public Declare Function OpenSSPComPortUSB Lib "ITLSSPProc.dll" (ByRef sspc As SSP_COMMAND) As Integer
Public Declare Function CloseSSPComPortUSB Lib "ITLSSPProc.dll" () As Integer
Public Declare Function InitiateSSPHostKeys Lib "ITLSSPProc.dll" (ByRef key As SSP_KEYS, ByRef sspc As SSP_COMMAND) As Integer
Public Declare Function GetProcDLLVersion Lib "ITLSSPProc.dll" (ByRef key As Byte) As Integer
Public Declare Function TestSplit Lib "ITLSSPProc.dll" (ByRef key As DEVICE_PAY_DATA, ByVal AmountToPay As Long) As Integer

' --------------------------------------------------------
' Payout MODULE
' --------------------------------------------------------
Public Type PAYOUT_SET
    pay As DEVICE_PAY_DATA
    ConnectionStatus As Boolean
    CountryCode As String
    FirmwareVersion As String
    ValueMultiplier As Long
    NoteSecurity() As Long
    MinPayout As Long
    NoteInhibitRegister As Integer
    PollResponse(100) As Byte
    PollLength As Byte
    TrueValueMuliplier As Long
End Type

Public Type MONEY_PAID
    CoinPaidIn As Long
    HopperPaid As Long
    PayoutPaid As Long
    CashBoxPaid As Long
    totalPaid As Long
    amountPaid As Long
    AmountToPay As Long
    amountrequest As Long
End Type

Public Const cmd_PAYOUT_CMD = &H33
Public Const cmd_GET_NOTE_AMOUNT = &H35
Public Const cmd_GET_NOTE_MIN_PAYOUT = &H3E
Public Const cmd_ENABLE_PAYOUT_DEVICE = &H5C
Public Const cmd_PAYOUT_FLOAT = &H3D

Public Const spp_ev_NOTE_STORED = &HDB

Public sspPayout As SSP_COMMAND
Public sspPayoutInfo As SSP_COMMAND_INFO
Public pySet As PAYOUT_SET

'HOPPER MODUlE
Public iPort As Integer
Public sspAddress As Byte
Public szDir As String

Public Const COL_PASS = &HC000&
Public Const COL_FAIL = &HFF&

Public Const DIAMETER_DATA_SIZE = 128

Public Const V_COIN_DATA_READY = &HD6
Public Const GET_COIN_DATA = &H7B
Public Const SET_COIN_DATA = &H7C
Public Const GET_COIN_READ = &H7D

Public Enum USR_OPTION_MODE
    usr_options_read
    usr_options_write
End Enum

Public Enum DEAL_MODE
    DEAL_MODE_SPLIT = 254
    DEAL_MODE_FREE
End Enum

Public Enum SPEED_MODE
    SPEED_MODE_LOW = 254
    SPEED_MODE_HIGH
End Enum

Public Enum EXIT_SECURITY
    LEVEL_COIL = 253
    LEVEL_FLAP
    LEVEL_SENSOR
End Enum

Public Enum LEVEL_CHECK
    LEVEL_CHECK_OFF = 254
    LEVEL_CHECK_ON
End Enum


Public Const SMART_HOPPER = &H81
Public Const SET_DATA_COLLECT_MODE = &H0
Public Const SET_COIN_READ_MODE = &H1
Public Const SET_USER_FIXED_KEY = &H2
Public Const SET_TEST_FUNCTION = &H3
Public Const GET_SH_TEST_DATA = &H4
Public Const ERASE_SH_TEST_DATA = &H5
Public Const RUN_CASHBOX_TEST = &H6
Public Const FLAP_MAP_TEST = &H7
Public Const SET_USER_OPTIONS = &H8
Public Const GET_CAL_SAMPLES = &H9
Public Const GET_CYCLE_DATA = &HA
Public Const RUN_INIT_CYCLE = &HB
Public Const GET_INIT_CORRECTION_DATA = &HC

Public Const cmd_GET_DEVICE_SETUP = &H5
Public Const cmd_SET_PAY_OUT_AMOUNT = &H33
Public Const cmd_SET_PAY_IN_VALUE = &H34
Public Const cmd_GET_PAY_IN_VALUE = &H35
Public Const cmd_SET_HALT = &H38
Public Const cmd_SET_COIN_ROUTE = &H3B
Public Const cmd_GET_COIN_ROUTE = &H3C
Public Const cmd_FLOAT = &H3D
Public Const cmd_GET_MIN_PAYOUT = &H3E
Public Const cmd_EMPTY_ALL = &H3F
Public Const cmd_SET_COIN_ACCEPT_INHIBIT = &H40
Public Const cmd_RESET = &H1
Public Const ssp_CMD_SET_SSP_USER_KEY = &H7C

Public Const hp_rsp_DISPENSING = &HDA
Public Const hp_rsp_DISPENSED = &HD2
Public Const hp_HOPPER_HALTED = &HD6
Public Const hp_FLOATING = &HD7
Public Const hp_FLOATED = &HD8
Public Const hp_TIME_OUT = &HD9
Public Const hp_EMPTYING = &HC2
Public Const hp_EMPTYED = &HC3
Public Const hp_INPCOMPLETE_PAYOUT = &HDC
Public Const hp_INPCOMPLETE_FLOAT = &HDD
Public Const hp_CASHBOX_PAID = &HDE
Public Const hp_COIN_CREDIT = &HDF
Public Const hp_rsp_FRAUD_ATTEMPT = &HE6

Public Enum FLOAT_MODE
    FLOAT_PAYOUT_TO_PAYOUT
    FLOAT_PAYOUT_TO_CASHBOX
End Enum

Public Enum VALIDATE_FAIL_STATUS
    val_PASS = 128
    val_FAIL_RADIUS
    val_FAIL_VECTOR
    val_FAIL_CROSS
End Enum


Public Enum FRAUD_REASON
    frd_NO_FRAUD_DETECTED
    frd_FLAP
    frd_EXIT
    frd_FLAP_ZERO
    frd_EXIT_ZERO
    frd_NO_COIN_ENTRIES
    frd_NO_COIN_EXITS
    frd_COIN_EXIT_FIRST_SLOT_TOO_LOW
    frd_COIN_EXIT_WIDTH_TOO_HIGH
    frd_FLAP_ACTIVE_WHILE_IDLE
    frd_EXIT_ACTIVE_WHILE_IDLE
    frd_FLAP_ACTIVE_WHILE_PAYOUT_IDLE
End Enum


Public Type HPCONFIGDATA
    BVType As String
    BVSerialNumber As Long
    BVSensors As Integer
    BVSensorsNames() As String
    BVPositionSensors As Integer
    BVLensNames() As String
    BVSensorcode() As Byte
    BVSensorType() As Byte
    BVMotors As Integer
    BVMotorNames() As String
    BVMotorCodes() As Byte
    BVMotorType() As Byte
    BVSensorGain1() As Byte
    BVSensorGain2() As Byte
    BVSensorPathTgt() As Byte
    BVSensorPaperTgt() As Byte
    BVSensorReadGain() As Byte
    BVSensorCalGain() As Byte
    BVSensorClear() As Byte
    BVSensorDetect() As Byte
    BVSensorThreshold() As Integer
    BVFirstGain1() As Byte
    BVFirstGain2() As Byte
    BVTargetAchieved() As Byte
    BVPaperAchieved() As Byte
    BVClearThreshold() As Integer
    HPCoilPathTarget() As Long
    HPCoilPathAch() As Long
End Type


Public Type HOPPER_INIT_DATA
    Hdr As Integer
    tablesize As Integer
    crc As Long
    InitStatus As Byte
    InitNumber As Byte
    Coil1Target As Long
    Coil1Level As Long
    Coil2Target As Long
    Coil2Level As Long
End Type


Public Enum FLAP_STATE
    flap_idle
    FLAP_CLOSE
    FLAP_OPEN
    FLAP_OPEN_WAIT
    FLAP_CLOSE_WAIT
    FLAP_EXIT_WAIT
End Enum

Public Enum MODE_TEST_STATE
    mode_TEST_IDLE
    mode_TEST_RUN
    mode_TEST_WAIT
    mode_TEST_UNJAM
    mode_TEST_WRITE_DATA
    mode_TEST_JAMMED
    mode_TEST_HALTED
    mode_TEST_RECAL
End Enum

Public Enum INIT_MODE
    mode_INIT_read
    mode_INIT_PAY
End Enum


Public Type HOPPER_FILE
    radMin As Integer
    radMax As Integer
    radMaterialMin As Integer
    radMaterialMax As Integer
    numVec As Byte
    vecData() As Integer
    Threshold As Long
    FileName As String
    coinName As String
    CoinValue As String
    vecOffset As Long
    vecSize As Long
End Type

Public Type HOPPER_SET
    pay As DEVICE_PAY_DATA
    ConnectionStatus As Boolean
    countyCode As String
    PollResponse(100) As Byte
    PollLength As Byte
    MinPayoutLevel As Long
    MandatoryEncryptedCommands() As Byte
    EncryptedCommandNumber As Byte
    OptionArrayLength As Byte
    OptionDataArray() As Byte
    ShowLog As Boolean
    LogFile As String
    LogFileEnable As Boolean
    CoinInhibitRegister As Integer
    CoinMechDetected As Boolean
End Type

Public hpset As HOPPER_SET
Public sspHopper As SSP_COMMAND
Public sspHopperInfo As SSP_COMMAND_INFO

' --------------------------------------------------------
' COM ENUM MODULE
' --------------------------------------------------------
Function LOWORD(X As Long) As Integer
   LOWORD = X And &HFFFF&
   ' Low 16 bits contain Minor revision number.
End Function

Function HIWORD(X As Long) As Integer
   HIWORD = X \ &HFFFF&
   ' High 16 bits contain Major revision number.
End Function
Public Sub get_com_ports(ByVal outputlist As ComboBox, ByVal CurrentComPort)
' Read list of COM ports from registry
    Dim hKey As Long, lName As Long, sName As String
    Dim nCtr As Integer
    Dim comname As String
    Dim counter As Long

    Dim Err As Integer
    Dim dLen As Long
    If RegOpenKeyEx(HKEY_LOCAL_MACHINE, "HARDWARE\DEVICEMAP\SERIALCOMM", 0, KEY_QUERY_VALUE, hKey) = ERROR_SUCCESS Then
      lName = 255
      sName = Space(255)
      nCtr = 0
      dLen = 255
      Dim mydata(255) As Byte
      outputlist.Clear
      Err = ERROR_SUCCESS
      While Err = ERROR_SUCCESS
        dLen = 255
        lName = 255
        sName = Space(255)
        Err = RegEnumValue(hKey, nCtr, sName, lName, 0, REG_SZ, mydata(0), dLen)
        If Len(Trim(sName)) > 0 Then
          Debug.Print Left(sName, lName)
          comname = ""
          For counter = 0 To dLen - 1
            comname = comname & Chr(mydata(counter))
          Next
          comname = Trim(comname)
          If (Asc(Mid(comname, Len(comname)))) = 0 Then
            comname = Left(comname, Len(comname) - 1)
          End If
          If Left(sName, 11) = "\Device\VCP" Then comname = comname & " (DA2)"
          If Left(sName, 14) = "\Device\USBSER" Then comname = comname & " (BV)"
          If Left(sName, 15) = "\Device\slabser" Then comname = comname & " (Hopper)"
          outputlist.AddItem (comname)
          Debug.Print comname
          If Val(Mid(comname, 4)) = CurrentComPort Then outputlist.ListIndex = outputlist.ListCount - 1
          nCtr = nCtr + 1
        End If
      Wend
      If outputlist.ListIndex = -1 Then
        If CurrentComPort > 0 Then
          outputlist.AddItem "COM" & CurrentComPort & " (INACTIVE)"
          outputlist.ListIndex = outputlist.ListCount - 1
        ElseIf outputlist.ListCount > 1 Then
          outputlist.ListIndex = 0
        End If
      End If
      Call RegCloseKey(hKey)
    End If
End Sub

Public Function get_com_port_number(ByVal Source As ComboBox) As Byte
   get_com_port_number = get_com_port_number_string(Source.text)
End Function

Public Function get_com_port_number_string(ByVal Source As String) As Byte
    If (Len(Source) >= 4) Then
        If (Left(Source, 3) = "COM") Then
            get_com_port_number_string = Val(Mid(Source, 4))
        Else
            get_com_port_number_string = 0
        End If
    Else
        get_com_port_number_string = 0
    End If
    If get_com_port_number_string > 255 Then get_com_port_number_string = 0
End Function

Public Function is_valid_com_port(ByVal p As Byte) As Boolean
' Read list of COM ports from registry
    Dim hKey As Long, lName As Long, sName As String
    Dim nCtr As Integer
    Dim comname As String
    Dim counter As Long

    Dim Err As Integer
    Dim dLen As Long
  If RegOpenKeyEx(HKEY_LOCAL_MACHINE, "HARDWARE\DEVICEMAP\SERIALCOMM", 0, KEY_QUERY_VALUE, hKey) = ERROR_SUCCESS Then
    lName = 255
    sName = Space(255)
    nCtr = 0
    dLen = 255
    Dim mydata(255) As Byte
    is_valid_com_port = False
    Err = ERROR_SUCCESS
    While Err = ERROR_SUCCESS
      dLen = 255
      lName = 255
      sName = Space(255)
      Err = RegEnumValue(hKey, nCtr, sName, lName, 0, REG_SZ, mydata(0), dLen)
      If Len(Trim(sName)) > 0 Then
        Debug.Print Left(sName, lName)
        comname = ""
        For counter = 0 To dLen - 2
          comname = comname & Chr(mydata(counter))
        Next
        If Left(sName, 11) = "\Device\VCP" Then comname = comname & " (DA2)"
        If Left(sName, 14) = "\Device\USBSER" Then comname = comname & " (BV)"
        If Left(sName, 15) = "\Device\slabser" Then comname = comname & " (Hopper)"
        If get_com_port_number_string(comname) = p Then is_valid_com_port = True
        nCtr = nCtr + 1
      End If
    Wend
    Call RegCloseKey(hKey)
  End If
End Function

' --------------------------------------------------------
' Def Mode MODULE
' --------------------------------------------------------

Public Function Shift(ByVal lValue As Long, ByVal lNumberOfBitsToShift As Long, ByVal lDirectionToShift As dcShiftDirection) As Long
Const ksCallname As String = "Shift"

On Error GoTo Procedure_Error
Dim LShift As Long
If lDirectionToShift Then
'shift left
LShift = lValue * (2 ^ lNumberOfBitsToShift)
Else
'shift right
LShift = lValue \ (2 ^ lNumberOfBitsToShift)
End If
Procedure_Exit:
Shift = LShift
Exit Function
Procedure_Error:
Err.Raise Err.Number, ksCallname, Err.Description, Err.HelpFile, Err.HelpContext
Resume Procedure_Exit
End Function

'===============================================================================
Public Function LShift(ByVal lValue As Long, ByVal lNumberOfBitsToShift As Long) As Long
Const ksCallname As String = "LShift"
On Error GoTo Procedure_Error
LShift = Shift(lValue, lNumberOfBitsToShift, SLeft)
Procedure_Exit:
Exit Function
Procedure_Error:
Err.Raise Err.Number, ksCallname, Err.Description, Err.HelpFile, Err.HelpContext
Resume Procedure_Exit
End Function

'================================================================================
Public Function RShift(ByVal lValue As Long, ByVal lNumberOfBitsToShift As Long) As Long
Const ksCallname As String = "RShift"
On Error GoTo Procedure_Error
RShift = Shift(lValue, lNumberOfBitsToShift, SRight)
Procedure_Exit:
Exit Function
Procedure_Error:
Err.Raise Err.Number, ksCallname, Err.Description, Err.HelpFile, Err.HelpContext
Resume Procedure_Exit
End Function

' --------------------------------------------------------
' Payout MODULE
' --------------------------------------------------------
Public Function InitialisePayout(sspc As SSP_COMMAND, pySetData As PAYOUT_SET, sspInfo As SSP_COMMAND_INFO) As Boolean
Dim hostkey As EightByteNumber
    
    ' set up key exchange
    ' sspc.key.FixedKeyLowValue = GetSetting("PayInPayOutSystem", "STARTUP", "PAYOUTFKLOW", &H1234567)
    ' sspc.key.FixedKeyHighValue = GetSetting("PayInPayOutSystem", "STARTUP", "PAYOUTFKHIGH", &H1234567)
    sspc.key.FixedKeyLowValue = &H1234567
    sspc.key.FixedKeyHighValue = &H1234567
    
    If Not NegotiateKeyExchange(sspc, sspInfo) Then
        Exit Function
    End If

    If Not GetPayoutSetUpdata(pySetData, sspc, sspInfo) Then Exit Function
    If Not GetPayoutNoteLevels(pySetData, sspc, sspInfo) Then Exit Function
    If Not GetPayoutMinPayout(pySetData, sspc, sspInfo) Then Exit Function

    InitialisePayout = True

End Function

Public Function SetPayoutFloatAmount(py As PAYOUT_SET, sysacc As PAY_SYSTEM_ACCOUNT, sspc As SSP_COMMAND, sspInfo As SSP_COMMAND_INFO) As Boolean
Dim i As Integer, j As Integer

    'If OpenSSPComPort(sspc) = 0 Then Exit Function

    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = SYNC_CMD
    sspInfo.CommandName = "SYNC_CMD"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function

    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = ENABLE_CMD
    sspInfo.CommandName = "ENABLE"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function

    sspc.CommandDataLength = 9
    sspc.EncryptionStatus = 1
    sspc.CommandData(0) = cmd_PAYOUT_FLOAT
    For i = 0 To 3
        sspc.CommandData(1 + i) = CByte(RShift(py.MinPayout, 8 * i) And 255)
    Next i
    For i = 0 To 3
        sspc.CommandData(5 + i) = CByte(RShift(sysacc.Payout.RequestedFloatValue, 8 * i) And 255)
    Next i
    sspInfo.CommandName = "FLOAT PAYOUT AMOUNT"
    
    'If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function

    Call SSPSendCommand(sspc, sspInfo)

    If sspc.ResponseStatus <> ssp_reply_ok Then
        Call WriteLog("SSP error to " & sspInfo.CommandName & " command " & GetSSPReplyStatus(sspc.ResponseStatus))
        'frmMain.smartPayoutInit = False
        'frmMain.RestartMachine
        frmMain.ResetSmartPayout
        Exit Function
    End If

    If sspc.ResponseData(0) <> OK Then
        Call WriteLog("Non OK response to command " & sspInfo.CommandName & Chr(13) & Chr(10) & GetPayRequestData(sspc.ResponseData(1)))
        'frmMain.smartPayoutInit = False
        'frmMain.RestartMachine
        frmMain.ResetSmartPayout
        Exit Function
    End If

    SetPayoutFloatAmount = True

End Function
Public Function SetPayoutEnable(sspc As SSP_COMMAND, pySetData As PAYOUT_SET, sspInfo As SSP_COMMAND_INFO) As Boolean
Dim i As Integer

    ' set up key exchange
    ' sspc.key.FixedKeyLowValue = GetSetting("PayInPayOutSystem", "STARTUP", "PAYOUTFKLOW", &H1234567)
    ' sspc.key.FixedKeyHighValue = GetSetting("PayInPayOutSystem", "STARTUP", "PAYOUTFKHIGH", &H1234567)
    sspc.key.FixedKeyLowValue = &H1234567
    sspc.key.FixedKeyHighValue = &H1234567
    
    If Not NegotiateKeyExchange(sspc, sspInfo) Then
        Exit Function
    End If
    
  
    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = SYNC_CMD
    sspInfo.CommandName = "SYNC_CMD"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function
        
        
    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 1
    sspc.CommandData(0) = cmd_ENABLE_PAYOUT_DEVICE
    sspInfo.CommandName = "ENABLE_PAYOUT_DEVICE"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function
    
    For i = 0 To pySetData.pay.NumberOfCoinValues - 1
        If Not SetNoteRoute(pySetData, i, sspc, sspInfo) Then Exit Function
    Next i

    sspc.CommandDataLength = 3
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = SET_INHIBITS
    sspc.CommandData(1) = pySet.NoteInhibitRegister Mod 256
    sspc.CommandData(2) = Int(pySetData.NoteInhibitRegister / 256)
    sspInfo.CommandName = "SET_INHIBITS"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function
    
    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = ENABLE_CMD
    sspInfo.CommandName = "ENABLE CMD"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function
    
    SetPayoutEnable = True
    
End Function

Public Function GetPayoutMinPayout(pySetData As PAYOUT_SET, sspc As SSP_COMMAND, sspInfo As SSP_COMMAND_INFO) As Boolean
Dim i As Integer, j As Integer

    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = cmd_GET_NOTE_MIN_PAYOUT
    sspInfo.CommandName = "GET_NOTE_MIN_PAYOUT"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function
    
    pySetData.MinPayout = CLng(sspc.ResponseData(1)) + (CLng(sspc.ResponseData(2)) * 256)
    
    GetPayoutMinPayout = True
    
End Function

Public Function GetPayoutNoteLevels(pySetData As PAYOUT_SET, sspc As SSP_COMMAND, sspInfo As SSP_COMMAND_INFO) As Boolean
Dim i As Integer
Dim j As Integer
    
  '  If OpenSSPComPort(sspc) = 0 Then Exit Function
    
 '   ReDim pySetData.NoteCounterLevels(pySetData.pay.NumberOfCoinValues - 1) As Long
    
    sspc.CommandDataLength = 5
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = cmd_GET_NOTE_AMOUNT
    For i = 0 To pySetData.pay.NumberOfCoinValues - 1
        For j = 0 To 3
            sspc.CommandData(j + 1) = CByte(RShift(pySetData.pay.CoinsInHopper(i).CoinValue, 8 * j) And 255)
        Next j
        If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function
        pySetData.pay.CoinsInHopper(i).CoinLevel = 0
        For j = 0 To 1
            pySetData.pay.CoinsInHopper(i).CoinLevel = pySetData.pay.CoinsInHopper(i).CoinLevel + LShift(CLng(sspc.ResponseData(j + 1)), 8 * j)
        Next j
        
    Next i
    
    For i = 0 To pySetData.pay.NumberOfCoinValues - 1
        sspc.CommandDataLength = 5
        sspc.CommandData(0) = cmd_GET_COIN_ROUTE
        sspc.EncryptionStatus = 1
        sspInfo.CommandName = "GET_COIN_ROUTE"
        For j = 0 To 3
           sspc.CommandData(1 + j) = CByte(RShift(pySetData.pay.CoinsInHopper(i).CoinValue, 8 * j) And 255)
        Next j
        If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function
        pySet.pay.FloatMode(i) = sspc.ResponseData(1)
    Next i
    
    GetPayoutNoteLevels = True

  '  Call CloseSSPComPort

End Function

Public Function GetPayoutSetUpdata(pySetData As PAYOUT_SET, sspc As SSP_COMMAND, sspInfo As SSP_COMMAND_INFO) As Boolean
Dim i As Integer

    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = SYNC_CMD
    sspInfo.CommandName = "SYNC_CMD"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function
    
    
    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = SETUP_REQUEST_CMD
    sspInfo.CommandName = "SETUP_REQUEST_CMD"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function
    
    pySetData.FirmwareVersion = ""
    For i = 0 To 3
        pySetData.FirmwareVersion = pySetData.FirmwareVersion & Chr(sspc.ResponseData(i + 1 + 1))
    Next i
    pySetData.CountryCode = ""
    For i = 0 To 2
        pySetData.CountryCode = pySetData.CountryCode & Chr(sspc.ResponseData(i + 1 + 5))
    Next i
    pySetData.ValueMultiplier = 0
    For i = 0 To 2
        pySetData.ValueMultiplier = pySet.ValueMultiplier + LShift(CLng(sspc.ResponseData(i + 1 + 8)), 8 * (2 - i))
    Next i
    pySetData.pay.NumberOfCoinValues = sspc.ResponseData(12)
 '   ReDim pySetData.NoteValues(pySetData.pay.NumberOfCoinValues - 1) As Long
    ReDim pySetData.NoteSecurity(pySetData.pay.NumberOfCoinValues - 1) As Long
 '   ReDim pySetData.NoteRoute(pySetData.pay.NumberOfCoinValues - 1) As Byte
    
    pySetData.TrueValueMuliplier = 0
    For i = 0 To 2
        pySetData.TrueValueMuliplier = pySetData.TrueValueMuliplier + LShift(CLng(sspc.ResponseData(13 + (pySetData.pay.NumberOfCoinValues * 2) + i)), (2 - i) * 8)
    Next i
    
    For i = 0 To pySetData.pay.NumberOfCoinValues - 1
        pySetData.pay.CoinsInHopper(i).CoinValue = CLng(sspc.ResponseData(i + 1 + 12)) * pySetData.ValueMultiplier * pySetData.TrueValueMuliplier
        pySetData.NoteSecurity(i) = sspc.ResponseData(i + 1 + 12 + pySetData.pay.NumberOfCoinValues)
    Next i
    
    For i = 0 To pySetData.pay.NumberOfCoinValues - 1
        If pySetData.pay.CoinsInHopper(i).CoinValue > 0 Then
            pySetData.NoteInhibitRegister = pySetData.NoteInhibitRegister + LShift(1, i)
        End If
    Next i
    
    If pySetData.TrueValueMuliplier = 0 Then
        Call WriteLog("This payout firmware does not support this system. Please update to the latest version of Payout and NV200 firmware.")
        GetPayoutSetUpdata = False
    Else
        GetPayoutSetUpdata = True
    End If
    
End Function

Public Function SetNoteRoute(py As PAYOUT_SET, Index As Integer, sspc As SSP_COMMAND, sspInfo As SSP_COMMAND_INFO) As Boolean
Dim i As Integer, j As Integer


    sspc.CommandDataLength = 6
    sspc.EncryptionStatus = 1
    sspc.CommandData(0) = cmd_SET_COIN_ROUTE
    sspInfo.CommandName = "cmd_SET_COIN_ROUTE"
    sspc.CommandData(1) = py.pay.FloatMode(Index)
    For i = 0 To 3
        sspc.CommandData(i + 2) = CByte(RShift(py.pay.CoinsInHopper(Index).CoinValue, 8 * i) And 255)
    Next i

    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function

    SetNoteRoute = True

End Function

' --------------------------------------------------------
' SSPProc MODULE
' --------------------------------------------------------

Public Function OpenCommunicationPorts() As Boolean

    'If sspHopper.PortNumber = sspPayout.PortNumber Then
    '    If OpenSSPComPortUSB(sspHopper) = 0 Then
    '        Exit Function
    '    End If
    'Else
            If OpenSSPComPort(sspPayout) = 0 Then
                Exit Function
            End If

            'If OpenSSPComPort2(sspHopper) = 0 Then
            '    Exit Function
            'End If
    'End If
    
    OpenCommunicationPorts = True

End Function
Public Function CloseCommunicationPorts() As Boolean

    'If sspHopper.PortNumber = sspPayout.PortNumber Then
    '    CloseSSPComPortUSB
    'Else
        'If sspHopper.PortNumber > 0 Then
        '    CloseSSPComPort2
        'End If
        If sspPayout.PortNumber > 0 Then
            CloseSSPComPort
        End If
    'End If
    
    CloseCommunicationPorts = True

End Function
Public Function NegotiateKeyExchange(sspc As SSP_COMMAND, sspInfo As SSP_COMMAND_INFO) As Boolean
Dim sspKey As SSP_KEYS
Dim i As Integer
                
    ' DLL call to create Modulus, Generator and Host inter numbers
    If InitiateSSPHostKeys(sspKey, sspc) = 0 Then
        Call WriteLog("Error initiating host key modulus or generator values set to zero")
        Exit Function
    End If

    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = SYNC_CMD
    sspInfo.CommandName = "SYNC"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function

    sspc.CommandDataLength = 9
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = cmd_SSP_SET_GENERATOR
    For i = 0 To 3
        sspc.CommandData(1 + i) = CByte(RShift(sspKey.Generator.LoValue, 8 * i) And &HFF)
        sspc.CommandData(5 + i) = CByte(RShift(sspKey.Generator.Hivalue, 8 * i) And &HFF)
    Next i
    sspInfo.CommandName = "SSP_SET_GENERATOR"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function

    sspc.CommandDataLength = 9
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = cmd_SSP_SET_MODULUS
    For i = 0 To 3
        sspc.CommandData(1 + i) = CByte(RShift(sspKey.Modulus.LoValue, 8 * i) And &HFF)
        sspc.CommandData(5 + i) = CByte(RShift(sspKey.Modulus.Hivalue, 8 * i) And &HFF)
    Next i
    sspInfo.CommandName = "SSP_SET_MODULUS"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function
    
    sspc.CommandDataLength = 9
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = cmd_SSP_REQ_KEY_EXCHANGE
    For i = 0 To 3
        sspc.CommandData(1 + i) = CByte(RShift(sspKey.HostInter.LoValue, 8 * i) And &HFF)
        sspc.CommandData(5 + i) = CByte(RShift(sspKey.HostInter.Hivalue, 8 * i) And &HFF)
    Next i
    sspInfo.CommandName = "cmd_SSP_REQ_KEY_EXCHANGE"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function
    sspKey.SlaveInterKey.LoValue = 0
    sspKey.SlaveInterKey.Hivalue = 0
    For i = 0 To 3
        sspKey.SlaveInterKey.LoValue = sspKey.SlaveInterKey.LoValue + (CLng(sspc.ResponseData(1 + i)) * (256 ^ i))
        sspKey.SlaveInterKey.Hivalue = sspKey.SlaveInterKey.Hivalue + (CLng(sspc.ResponseData(5 + i)) * (256 ^ i))
    Next i
    
    ' we can now calculate our host key
    If CreateSSPHostEncryptionKey(sspKey) = 0 Then
        Call WriteLog("Error creating host key")
        Exit Function
    End If
    
    sspc.key.EncryptKeyLowValue = sspKey.KeyHost.LoValue
    sspc.key.EncryptkeyHighValue = sspKey.KeyHost.Hivalue
    
    NegotiateKeyExchange = True
    
End Function

Public Function TransmitSSPCommand(sspc As SSP_COMMAND, sspInfo As SSP_COMMAND_INFO) As Boolean
Dim szaddTx As String, szAddRx As String, szAdd As String
Dim i As Integer

    If sspc.EncryptionStatus Then
        If sspc.key.EncryptkeyHighValue = 0 And sspc.key.EncryptKeyLowValue = 0 Then
            Call WriteLog("Smart payout error : The host has no key set")
            'Call CloseSSPComPort
            Exit Function
        End If
    End If
    
    Call SSPSendCommand(sspc, sspInfo)

    'If hpset.ShowLog Then Call UpdateSSPLogPacketData(frmLog1.List1, sspInfo)

'    szaddTx = ""
'    For i = 0 To sspInfo.Transmit.PacketLength - 1
'        If sspInfo.Transmit.PacketData(i) < &H10 Then
'            szadd = "0" & Hex(sspInfo.Transmit.PacketData(i))
'        Else
'            szadd = Hex(sspInfo.Transmit.PacketData(i))
'        End If
'        szaddTx = szaddTx & szadd & " "
'    Next i
'    szAddRx = ""
'    For i = 0 To sspInfo.Recieve.PacketLength - 1
'        If sspInfo.Recieve.PacketData(i) < &H10 Then
'            szadd = "0" & Hex(sspInfo.Recieve.PacketData(i))
'        Else
'            szadd = Hex(sspInfo.Recieve.PacketData(i))
'        End If
'        szAddRx = szAddRx & szadd & " "
'    Next i
'
'    If MDIForm1.List1.ListCount > 1000 Then MDIForm1.List1.Clear
'    If sspInfo.Encrypted Then
'        MDIForm1.List1.AddItem sspInfo.CommandName & " (Encrypted command)"
'    Else
'        MDIForm1.List1.AddItem sspInfo.CommandName
'    End If
'    MDIForm1.List1.AddItem "TX: " & szaddTx
'    If sspInfo.Recieve.PacketLength > 0 Then
'        MDIForm1.List1.AddItem "RX: " & szAddRx
'    Else
'        MDIForm1.List1.AddItem "Reply Timeout"
'    End If
'    MDIForm1.List1.AddItem "Reply Time (ms) : " & sspInfo.Recieve.PacketTime
'    MDIForm1.List1.AddItem "---------------------------------------------"
'    MDIForm1.List1.ListIndex = MDIForm1.List1.ListCount - 1
    DoEvents
    
    If sspc.ResponseStatus <> ssp_reply_ok Then
        Call WriteLog("SSP error to " & sspInfo.CommandName & " command " & GetSSPReplyStatus(sspc.ResponseStatus))
        frmMain.smartPayoutInit = False
        frmMain.smartPayoutHalt = True
        'frmMain.RestartMachine
        frmMain.ResetSmartPayout
        Exit Function
    End If

    If sspc.ResponseData(0) <> OK Then
        Call WriteLog("Non OK response to command " & sspInfo.CommandName & Chr(13) & Chr(10) & GetGenericData(sspc.ResponseData(0)))
        frmMain.smartPayoutInit = False
        frmMain.smartPayoutHalt = True
        'frmMain.RestartMachine
        frmMain.ResetSmartPayout
        Exit Function
    End If

    TransmitSSPCommand = True

End Function

Public Function UpdateSSPLogPacketData(List1 As ListBox, sspInfo As SSP_COMMAND_INFO)
Dim szaddTx As String, szAddRx As String, szAdd As String, szaddEncTx As String, szAddEncRx As String
Dim i As Integer
Dim ifree As Integer
Dim PrData(9) As String

    szaddTx = ""
     For i = 0 To sspInfo.PreEncryptTransmit.PacketLength - 1
        If sspInfo.PreEncryptTransmit.PacketData(i) < &H10 Then
            szAdd = "0" & Hex(sspInfo.PreEncryptTransmit.PacketData(i)) 'Hex(sspinfo.Transmit.PacketData(i))
        Else
            szAdd = Hex(sspInfo.PreEncryptTransmit.PacketData(i)) 'Hex(sspinfo.Transmit.PacketData(i))
        End If
        szaddTx = szaddTx & szAdd & " "
    Next i
    szAddRx = ""
    For i = 0 To sspInfo.PreEncryptRecieve.PacketLength - 1
        If sspInfo.PreEncryptRecieve.PacketData(i) < &H10 Then
            szAdd = "0" & Hex(sspInfo.PreEncryptRecieve.PacketData(i))
        Else
            szAdd = Hex(sspInfo.PreEncryptRecieve.PacketData(i))
        End If
        szAddRx = szAddRx & szAdd & " "
    Next i
    szaddEncTx = ""
     For i = 0 To sspInfo.Transmit.PacketLength - 1
        If sspInfo.Transmit.PacketData(i) < &H10 Then
            szAdd = "0" & Hex(sspInfo.Transmit.PacketData(i))
        Else
            szAdd = Hex(sspInfo.Transmit.PacketData(i))
        End If
        szaddEncTx = szaddEncTx & szAdd & " "
    Next i
    szAddEncRx = ""
    For i = 0 To sspInfo.Recieve.PacketLength - 1
        If sspInfo.Recieve.PacketData(i) < &H10 Then
            szAdd = "0" & Hex(sspInfo.Recieve.PacketData(i))
        Else
            szAdd = Hex(sspInfo.Recieve.PacketData(i))
        End If
        szAddEncRx = szAddEncRx & szAdd & " "
    Next i
    
    
    PacketCount = PacketCount + 1
    
    

    PrData(0) = "Packet# " & PacketCount & "  ------------------------------------------------------------------------------ " & Now
    PrData(1) = "Command name: " & sspInfo.CommandName
    PrData(2) = "Plain Transmit data: " & szaddTx
    PrData(3) = "Plain Receive data : " & szAddRx
    PrData(4) = " "
    PrData(5) = "Encrypted packet transmit: " & szaddEncTx
    PrData(6) = "Encrypted packet receive : " & szAddEncRx
    PrData(7) = "Reply Time (ms): " & sspInfo.Recieve.PacketTime
    PrData(8) = " "
    
    If List1.ListCount >= 2000 Then List1.Clear
    For i = 0 To 8
        List1.AddItem PrData(i)
    Next i
    List1.ListIndex = List1.ListCount - 1

    If Not hpset.LogFileEnable Then Exit Function

    On Local Error GoTo Err
    If hpset.LogFileEnable Then
        ifree = FreeFile
        Open hpset.LogFile For Append As #ifree
            For i = 0 To 8
                Print #ifree, PrData(i)
            Next i
        Close #ifree
    End If

Err:
End Function

Public Function GetGenericData(gCode As Byte) As String
    
    Select Case gCode
    Case gen_R_OK
        GetGenericData = "00" '"OK"
    Case gen_R_COMMAND_NOT_KNOWN
        GetGenericData = "05" '"Command unknown by this slave"
    Case gen_R_WRONG_NO_PARAMETERS
        GetGenericData = "06" '"Incorrect parmeters for this command to this slave"
    Case gen_R_PARAMETER_OUT_RANGE
        GetGenericData = "07" '"Parameters for this command out of range for this slave"
    Case gen_R_COMMAND_CANNOT_BE_PROC
        GetGenericData = "08" '"Slave cannot process this command"
    Case gen_R_SOFTWARE_ERROR
        GetGenericData = "09" '"Slave has a software error"
    Case gen_R_CHECKSUM_ERROR
        GetGenericData = "10" '"Slave has a checksum error"
    Case gen_R_FAIL
        GetGenericData = "11" '"Slave has a fail response"
    Case gen_R_HEADER_FAIL
        GetGenericData = "12" '"Programming header fail"
    Case gen_R_KEY_NOT_SET
        GetGenericData = "13" '"Slave has encrypted requirements and a key has not been negotiated"
    Case Else
        GetGenericData = "14" '"Unknown generic response " & Hex(gCode)
    End Select


End Function

Public Function GetPayRequestData(gCode As Byte) As String

    Select Case gCode
    Case LEVEL_NOT_SUFFICIENT
        GetPayRequestData = "LEVEL_NOT_SUFFICIENT"
    Case NOT_EXACT_AMOUNT
        GetPayRequestData = "NOT_EXACT_AMOUNT"
    Case DEVICE_BUSY
        GetPayRequestData = "DEVICE_BUSY"
    Case DEVICE_DISABLED
        GetPayRequestData = "DEVICE_DISABLED"
    Case REQ_STATUS_OK
        GetPayRequestData = "REQ_STATUS_OK"
    Case Else
        GetPayRequestData = "UNKNOWN CODE"
    End Select
    
End Function

Public Function TestDevicePresence(sspc As SSP_COMMAND, sspInfo As SSP_COMMAND_INFO) As Boolean
Dim re As Integer
Dim TimeOut As Integer
Dim ignoreerror As Byte

    re = sspc.RetryLevel
    TimeOut = sspc.TimeOut
    ignoreerror = sspc.ignoreerror
    sspc.ignoreerror = 1
    sspc.RetryLevel = 1
    sspc.TimeOut = 100

    sspc.CommandDataLength = 1
    sspc.CommandData(0) = SYNC_CMD

    Call SSPSendCommand(sspc, sspInfo)

    If sspc.ResponseStatus <> ssp_reply_ok Or (sspc.ResponseData(0) <> OK And sspc.ResponseData(0) <> &HFA) Then
        sspc.RetryLevel = re
        sspc.TimeOut = TimeOut
        sspc.ignoreerror = ignoreerror
   '     CloseSSPComPort
        Exit Function
    End If

    sspc.RetryLevel = re
    sspc.TimeOut = TimeOut
    sspc.ignoreerror = ignoreerror
 '   CloseSSPComPort

    TestDevicePresence = True


End Function


Public Function SetNewSSPUserKey(newKey() As Byte) As Boolean
'Dim hostKey As EightByteNumber
'Dim i As Integer
'
'
'    If Not NegotiateKeyExchange(hostKey) Then Exit Function
'
'    FullKey.EncryptkeyHighValue = hostKey.Hivalue
'    FullKey.EncryptKeyLowValue = hostKey.LoValue
'
'
'    If OpenSSPComPort(sspC) = 0 Then Exit Function
'
'    sspC.CommandDataLength = 12
'    sspC.EncryptionStatus = 1
'    sspC.CommandData(0) = EXPANSION_COMMAND
'    sspC.CommandData(1) = NV7
'    sspC.CommandData(2) = SMART_HOPPER
'    sspC.CommandData(3) = SET_USER_FIXED_KEY
'    For i = 0 To 7
'        sspC.CommandData(4 + i) = newKey(i)
'    Next i
'    sspInfo.CommandName = "SET_SSP_USER_KEY"
'    If Not TransmitSSPCommand(sspC, sspInfo) Then Exit Function
'
'    sspC.CommandDataLength = 1
'    sspC.EncryptionStatus = 0
'    sspC.CommandData(0) = RESET_CMD
'    sspInfo.CommandName = "RESET"
'    If Not TransmitSSPCommand(sspC, sspInfo) Then Exit Function
'
'    Call CloseSSPComPort
'
'    SetNewSSPUserKey = True

End Function

Public Function GetSSPReplyStatus(replyCode As Byte) As String

    Select Case replyCode
    Case SSP_PACKET_ERROR
        GetSSPReplyStatus = "02" '"SSP PACKET ERROR"
    Case port_open
        GetSSPReplyStatus = "03" '"PORT ALREADY OPEN"
    Case PORT_ERROR
        GetSSPReplyStatus = "04" '"PORT_ERROR"
    Case SSP_CMD_TIMEOUT
        GetSSPReplyStatus = "05" '"COMMAND TIME-OUT"
    End Select
    
End Function

'HOPPER

Public Function InitialiseHopper(sspc As SSP_COMMAND, hpSetData As HOPPER_SET, sspInfo As SSP_COMMAND_INFO) As Boolean

    ' set up key exchange
    ' sspc.key.FixedKeyLowValue = GetSetting(App.ProductName, "STARTUP", "HOPPERFKLOW", &H1234567)
    ' sspc.key.FixedKeyHighValue = GetSetting(App.ProductName, "STARTUP", "HOPPERFKHIGH", &H1234567)
    sspc.key.FixedKeyLowValue = &H1234567
    sspc.key.FixedKeyHighValue = &H1234567
    
    If Not NegotiateKeyExchange(sspc, sspInfo) Then
        Exit Function
    End If

    If Not GetHopperSetUpdata(hpSetData, sspc, sspInfo) Then Exit Function

    If Not GetCoinLevels(hpSetData, sspc, sspInfo) Then Exit Function
    If Not GetMinHopperPayout(hpSetData, sspc, sspInfo) Then Exit Function
    
    InitialiseHopper = True

End Function

Public Function SetCoinMechInhibit(sspc As SSP_COMMAND, sspInfo As SSP_COMMAND_INFO, CoinValue As Long, state As Integer) As Boolean
Dim i As Integer, j As Integer


    sspc.CommandDataLength = 4
    sspc.EncryptionStatus = 1
    sspc.CommandData(0) = cmd_SET_COIN_ACCEPT_INHIBIT
    sspInfo.CommandName = "SET_COIN_ACCEPT_INHIBIT"
    sspc.CommandData(1) = state
    For i = 0 To 1
        sspc.CommandData(i + 2) = CByte(RShift(CoinValue, 8 * i) And 255)
    Next i
    
    Call SSPSendCommand(sspc, sspInfo)
    'If hpset.ShowLog Then Call UpdateSSPLogPacketData(frmLog1.List1, sspInfo)
    DoEvents
    If sspc.ResponseStatus <> ssp_reply_ok Then
        MsgBox "SSP error to " & sspInfo.CommandName & " command " & GetSSPReplyStatus(sspc.ResponseStatus), vbExclamation, App.ProductName
        Exit Function
    End If
    
    If sspc.ResponseData(0) = WRONG_No_PARAMETERS Then
        hpset.CoinMechDetected = False
    Else
        hpset.CoinMechDetected = True
    End If
    

    SetCoinMechInhibit = True

End Function

Public Function SetMandatoryEncryptedCommandList(hp As HOPPER_SET)
    ' get the list of mandatory commands   */
    hp.EncryptedCommandNumber = 7
    ReDim hpset.MandatoryEncryptedCommands(hp.EncryptedCommandNumber - 1) As Byte
    hp.MandatoryEncryptedCommands(0) = cmd_SET_PAY_OUT_AMOUNT
    hp.MandatoryEncryptedCommands(1) = cmd_SET_PAY_IN_VALUE
    hp.MandatoryEncryptedCommands(2) = cmd_SET_HALT
    hp.MandatoryEncryptedCommands(3) = cmd_SET_COIN_ROUTE
    hp.MandatoryEncryptedCommands(4) = cmd_GET_COIN_ROUTE
    hp.MandatoryEncryptedCommands(5) = cmd_FLOAT
    hp.MandatoryEncryptedCommands(6) = cmd_EMPTY_ALL
End Function


Public Function GetValidateStatus(status As Byte, ByRef col As Long) As String

    
    Select Case status
    Case val_PASS
        GetValidateStatus = "PASS"
        col = COL_PASS
    Case val_FAIL_RADIUS
        GetValidateStatus = "FAIL_RADIUS"
        col = COL_FAIL
    Case val_FAIL_VECTOR
        GetValidateStatus = "FAIL_VECTOR"
        col = COL_FAIL
    Case val_FAIL_CROSS
        GetValidateStatus = "FAIL_CROSS"
        col = COL_FAIL
    Case Else
        col = COL_FAIL
        GetValidateStatus = "unknown fail"
    End Select

End Function

Public Function GetFraudReason(fd As Byte) As String

    Select Case fd
    Case frd_NO_FRAUD_DETECTED
        GetFraudReason = "NO_FRAUD_DETECTED"
    Case frd_FLAP
        GetFraudReason = "ILLEGAL FLAP ACTIVATIION"
    Case frd_EXIT
        GetFraudReason = "ILLEGAL EXIT DETECTION"
    Case frd_FLAP_ZERO
        GetFraudReason = "FLAP ZERO DETECTION"
    Case frd_EXIT_ZERO
        GetFraudReason = "EXIT ZERO DETECTION"
    Case frd_NO_COIN_ENTRIES
        GetFraudReason = "EXPECTED COINS NOT SEEN AT EXIT"
    Case frd_NO_COIN_EXITS
        GetFraudReason = "EXPECTED COINS NOT CLEARING EXIT"
    Case frd_COIN_EXIT_FIRST_SLOT_TOO_LOW
        GetFraudReason = "EXPECTED COIN TOO EARLY AT EXIT"
    Case frd_COIN_EXIT_WIDTH_TOO_HIGH
        GetFraudReason = "DETECTED COINS TOO WIDE AT EXIT"
    Case frd_FLAP_ACTIVE_WHILE_IDLE
        GetFraudReason = "FLAP SENSOR ACTIVE WHILE IDLE"
    Case frd_EXIT_ACTIVE_WHILE_IDLE
        GetFraudReason = "EXIT SENSOR ACTIVE WHILE IDLE"
    Case frd_FLAP_ACTIVE_WHILE_PAYOUT_IDLE
        GetFraudReason = "FLAP SENSOR ON PAYOUT IDLE"
    End Select

End Function

Public Function SetHopperFloatAmount(hp As HOPPER_SET, sysacc As PAY_SYSTEM_ACCOUNT, sspc As SSP_COMMAND, sspInfo As SSP_COMMAND_INFO) As Boolean
Dim i As Integer, j As Integer


    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = SYNC_CMD
    sspInfo.CommandName = "SYNC_CMD"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function

    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = ENABLE_CMD
    sspInfo.CommandName = "ENABLE"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function


    sspc.CommandDataLength = 9
    sspc.EncryptionStatus = 1
    sspc.CommandData(0) = cmd_FLOAT
    For i = 0 To 1
        sspc.CommandData(1 + i) = CByte(RShift(hp.MinPayoutLevel, 8 * i) And 255)
    Next i
    For i = 0 To 3
        sspc.CommandData(3 + i) = CByte(RShift(sysacc.Hopper.RequestedFloatValue, 8 * i) And 255)
    Next i
    sspInfo.CommandName = "FLOAT HOPPER AMOUNT"
'    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function

    Call SSPSendCommand(sspc, sspInfo)

    If sspc.ResponseStatus <> ssp_reply_ok Then
        MsgBox "SSP error to " & sspInfo.CommandName & " command " & GetSSPReplyStatus(sspc.ResponseStatus), vbExclamation, App.ProductName
        Exit Function
    End If

    If sspc.ResponseData(0) <> OK Then
        MsgBox "Non OK response to command " & sspInfo.CommandName & Chr(13) & Chr(10) & GetPayRequestData(sspc.ResponseData(1)), vbExclamation, App.ProductName
        Exit Function
    End If

    SetHopperFloatAmount = True
'
End Function

Public Function GetPayOutError(errCode As Byte) As String

    Select Case errCode
    Case 0
        GetPayOutError = "Insufficient value in hopper to float to level requested"
    Case 1
        GetPayOutError = "Cannot float to exact amount requested"
    End Select

End Function

Public Function GetHopperOptions(sspc As SSP_COMMAND, sspInfo As SSP_COMMAND_INFO, hp As HOPPER_SET, mode As Byte) As Boolean
Dim i As Integer, j As Integer

    If mode = usr_options_read Then
        sspc.CommandDataLength = 5
    Else
        sspc.CommandDataLength = hp.OptionArrayLength + 5
    End If
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = EXPANSION_COMMAND
    sspc.CommandData(1) = NV7
    sspc.CommandData(2) = SMART_HOPPER
    sspc.CommandData(3) = SET_USER_OPTIONS
    sspc.CommandData(4) = mode
    If mode = usr_options_write Then
        For i = 0 To hp.OptionArrayLength - 1
            sspc.CommandData(5 + i) = hp.OptionDataArray(i)
        Next i
    End If
    

    sspInfo.CommandName = "SET_USER_OPTIONS"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function

    If mode = usr_options_read Then
        hp.OptionArrayLength = sspc.ResponseDataLength - 1
        ReDim hp.OptionDataArray(hp.OptionArrayLength - 1) As Byte
        For i = 0 To hp.OptionArrayLength - 1
            hp.OptionDataArray(i) = sspc.ResponseData(i + 1)
        Next i
    End If

    GetHopperOptions = True
    
End Function

Public Function CalculateSyncGain(g1 As Byte, g2 As Byte) As Long
Dim g1C As Double, g2C As Double
    
Const SYNC_G1_R1 = 100000
Const SYNC_G1_R2 = 2000
Const SYNC_G2_R1 = 100000
Const SYNC_G2_R2 = 27000
    
    g1C = (((g1 * (SYNC_G1_R1 / 256)) + SYNC_G1_R2) / SYNC_G1_R2)
    g2C = (((g2 * (SYNC_G2_R1 / 256)) + SYNC_G2_R2) / SYNC_G2_R2)

    CalculateSyncGain = CLng(g1C * g2C)

End Function

Public Function SendHalt() As Boolean

'
'
'    If OpenSSPComPort(sspC) = 0 Then Exit Function
'
'
'    sspC.CommandDataLength = 1
'    sspC.EncryptionStatus = 0
'    sspC.CommandData(0) = SYNC_CMD
'    sspInfo.CommandName = "SYNC_CMD"
'    If Not TransmitSSPCommand(sspC, sspInfo) Then Exit Function
'
'
'    sspC.CommandDataLength = 1
'    sspC.EncryptionStatus = 1
'    sspC.CommandData(0) = cmd_SET_HALT
'    sspInfo.CommandName = "SET_HALT"
'    If Not TransmitSSPCommand(sspC, sspInfo) Then Exit Function
'
'
'
'    Call CloseSSPComPort
'
'    SendHalt = False
'
    
End Function

Public Function SendReset(sspc As SSP_COMMAND, sspInfo As SSP_COMMAND_INFO) As Boolean

    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = SYNC_CMD
    sspInfo.CommandName = "SYNC_CMD"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function


    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = cmd_RESET
    sspInfo.CommandName = "RESET"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function

    SendReset = True
    
End Function

Public Function SetHopperEnable(sspc As SSP_COMMAND, hpSetData As HOPPER_SET, sspInfo As SSP_COMMAND_INFO) As Boolean
Dim i As Integer

    ' set up key exchange
    ' sspc.key.FixedKeyLowValue = GetSetting(App.ProductName, "STARTUP", "PAYOUTFKLOW", &H1234567)
    ' sspc.key.FixedKeyHighValue = GetSetting(App.ProductName, "STARTUP", "PAYOUTFKHIGH", &H1234567)
    sspc.key.FixedKeyLowValue = &H1234567
    sspc.key.FixedKeyHighValue = &H1234567
    
    If Not NegotiateKeyExchange(sspc, sspInfo) Then
        Exit Function
    End If
    
    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = SYNC_CMD
    sspInfo.CommandName = "SYNC_CMD"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function
    
        sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = ENABLE_CMD
    sspInfo.CommandName = "ENABLE"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function
    
    For i = 0 To hpSetData.pay.NumberOfCoinValues - 1
        If Not SetCoinMechInhibit(sspc, sspInfo, hpSetData.pay.CoinsInHopper(i).CoinValue, RShift(hpSetData.CoinInhibitRegister, i) And &H1) Then Exit Function
    Next i
    
    SetHopperEnable = True
    
End Function

Public Function EmptyAllToCashBox(sspc As SSP_COMMAND, sspInfo As SSP_COMMAND_INFO) As Boolean

    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = SYNC_CMD
    sspInfo.CommandName = "SYNC_CMD"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function

    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = ENABLE_CMD
    sspInfo.CommandName = "ENABLE_CMD"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function

    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 1
    sspc.CommandData(0) = cmd_EMPTY_ALL
    sspInfo.CommandName = "EMPTY_ALL"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function

    EmptyAllToCashBox = True
        
End Function


Public Function SetCoinRoute(hp As HOPPER_SET, Index As Integer, coinNumber As Integer, sspc As SSP_COMMAND, sspInfo As SSP_COMMAND_INFO) As Boolean
Dim i As Integer, j As Integer


    sspc.CommandDataLength = 4
    sspc.EncryptionStatus = 1
    sspc.CommandData(0) = cmd_SET_COIN_ROUTE
    sspInfo.CommandName = "cmd_SET_COIN_ROUTE"
    sspc.CommandData(1) = hp.pay.FloatMode(Index)
    For i = 0 To 1
        sspc.CommandData(i + 2) = CByte(RShift(hp.pay.CoinsInHopper(Index).CoinValue, 8 * i) And 255)
    Next i

    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function

    SetCoinRoute = True

End Function



Public Function UpdateCoinsInCounter(CoinValue As Integer, coinNumber As Integer, sspc As SSP_COMMAND) As Boolean
Dim i As Integer, j As Integer

    UpdateCoinsInCounter = False

    sspc.CommandDataLength = 5
    sspc.EncryptionStatus = 1
    sspc.CommandData(0) = cmd_SET_PAY_IN_VALUE
    sspHopperInfo.CommandName = "SET_PAY_IN_VALUE"
    For i = 0 To 1
        sspc.CommandData(i + 1) = CByte(RShift(coinNumber, 8 * i) And 255)
    Next i
    For i = 0 To 1
        sspc.CommandData(i + 3) = CByte(RShift(CoinValue, 8 * i) And 255)
    Next i
    If Not TransmitSSPCommand(sspc, sspHopperInfo) Then Exit Function


    UpdateCoinsInCounter = True

End Function



Public Function AddCoinsToCounter(hp As HOPPER_SET, Index As Integer, coinNumber As Integer, sspc As SSP_COMMAND, sspInfo As SSP_COMMAND_INFO) As Boolean
Dim i As Integer, j As Integer


    sspc.CommandDataLength = 5
    sspc.EncryptionStatus = 1
    sspc.CommandData(0) = cmd_SET_PAY_IN_VALUE
    sspInfo.CommandName = "SET_PAY_IN_VALUE"
    For i = 0 To 1
        sspc.CommandData(i + 1) = CByte(RShift(coinNumber, 8 * i) And 255)
    Next i
    For i = 0 To 1
        sspc.CommandData(i + 3) = CByte(RShift(hp.pay.CoinsInHopper(Index).CoinValue, 8 * i) And 255)
    Next i
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function



    AddCoinsToCounter = True

End Function

Public Function GetMinHopperPayout(hp As HOPPER_SET, sspc As SSP_COMMAND, sspInfo As SSP_COMMAND_INFO) As Boolean
Dim src As UDTssp
Dim cpy As UDTssp
Dim i As Integer, j As Integer


        ' send the start up data collect sequence
 '   If OpenSSPComPort(sspc) = 0 Then Exit Function
    
    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = SYNC_CMD
    sspInfo.CommandName = "SYNC_CMD"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function
    
        
    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = cmd_GET_MIN_PAYOUT
    sspInfo.CommandName = "GET_MIN_PAYOUT"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function
    
    
    hp.MinPayoutLevel = CLng(sspc.ResponseData(1)) + (CLng(sspc.ResponseData(2)) * 256)

 '    Call CloseSSPComPort
    
    GetMinHopperPayout = True
    
End Function

Public Function GetHopperSetUpdata(hp As HOPPER_SET, sspc As SSP_COMMAND, sspInfo As SSP_COMMAND_INFO) As Boolean
Dim i As Integer, j As Integer


        ' send the start up data collect sequence

 '   If OpenSSPComPort(sspc) = 0 Then Exit Function
    
    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = SYNC_CMD
    sspInfo.CommandName = "SYNC_CMD"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function
    

    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = cmd_GET_DEVICE_SETUP
    sspInfo.CommandName = "GET_DEVICE_SETUP"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function


        
    hp.countyCode = ""
    For i = 0 To 2
        hp.countyCode = hp.countyCode & Chr(sspc.ResponseData(i + 6))
    Next i
    hp.pay.NumberOfCoinValues = sspc.ResponseData(10)
  '  ReDim hp.pay.CoinsInHopper(hp.pay.NumberOfCoinValues - 1) As COINS
 '   ReDim hp.CoinValues(hp.pay.NumberOfCoinValues - 1) As Integer
 '   ReDim hp.CoinLevel(hp.pay.NumberOfCoinValues - 1) As Integer
  '  ReDim hp.pay.FloatMode(hp.pay.NumberOfCoinValues - 1) As Byte
        
    For i = 0 To hp.pay.NumberOfCoinValues - 1
 '       hp.CoinValues(i) = CLng(sspc.ResponseData(11 + (i * 2))) + (CLng(sspc.ResponseData(11 + (i * 2) + 1)) * 256)
        hp.pay.CoinsInHopper(i).CoinValue = CLng(sspc.ResponseData(11 + (i * 2))) + (CLng(sspc.ResponseData(11 + (i * 2) + 1)) * 256)
    Next i
            

'    Call CloseSSPComPort
    GetHopperSetUpdata = True

End Function




Public Function PollHopper(hp As HOPPER_SET, OpenPt As Boolean) As Boolean
'Dim i As Integer, j As Integer
'
'
'
'        ' send the start up data collect sequence
'    If OpenPt Then If OpenSSPComPort(sspC) = 0 Then Exit Function
'
'
'
'    sspC.CommandDataLength = 1
'    sspC.EncryptionStatus = 1
'    sspC.CommandData(0) = POLL_CMD
'    sspInfo.CommandName = "POLL_CMD"
'    If Not TransmitSSPCommand(sspC, sspInfo) Then Exit Function
'    For i = 0 To sspC.ResponseDataLength - 1
'        hp.PollResponse(i) = sspC.ResponseData(i)
'    Next i
'    hp.PollLength = sspC.ResponseDataLength
'
'
'    If OpenPt Then Call CloseSSPComPort
'
'
'    PollHopper = True
    
End Function


Public Function GetCoinLevels(hp As HOPPER_SET, sspc As SSP_COMMAND, sspInfo As SSP_COMMAND_INFO) As Boolean
Dim i As Integer, j As Integer
Dim src As UDTssp, cpy As UDTssp


'        ' send the start up data collect sequence
 '   If OpenSSPComPort(sspc) = 0 Then Exit Function

    sspc.CommandDataLength = 1
    sspc.EncryptionStatus = 0
    sspc.CommandData(0) = SYNC_CMD
    sspInfo.CommandName = "SYNC_CMD"
    If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function


    On Local Error Resume Next
    For i = 0 To hp.pay.NumberOfCoinValues - 1
        ' get the coin counter level for each value
        sspc.CommandDataLength = 5
        sspc.EncryptionStatus = 0
        sspc.CommandData(0) = cmd_GET_PAY_IN_VALUE
        sspInfo.CommandName = "GET_PAY_IN_VALUE"
        For j = 0 To 3
           sspc.CommandData(1 + j) = CByte(RShift(hp.pay.CoinsInHopper(i).CoinValue, 8 * j) And 255)
        Next j
        If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function
        hp.pay.CoinsInHopper(i).CoinLevel = 0  'hp.CoinLevel(i) = 0
        For j = 0 To 1
            hp.pay.CoinsInHopper(i).CoinLevel = hp.pay.CoinsInHopper(i).CoinLevel + (CLng(sspc.ResponseData(j + 1)) * (256 ^ j))
        Next j
    Next i
    For i = 0 To hp.pay.NumberOfCoinValues - 1
        sspc.CommandDataLength = 3
        sspc.CommandData(0) = cmd_GET_COIN_ROUTE
        sspc.EncryptionStatus = 1
        sspInfo.CommandName = "GET_COIN_ROUTE"
        For j = 0 To 1
           sspc.CommandData(1 + j) = CByte(RShift(hp.pay.CoinsInHopper(i).CoinValue, 8 * j) And 255)
        Next j
        If Not TransmitSSPCommand(sspc, sspInfo) Then Exit Function
        hp.pay.FloatMode(i) = sspc.ResponseData(1)
    Next i


 '    Call CloseSSPComPort

    GetCoinLevels = True

End Function


Public Function GetHopperInitSensorData(bvData As HPCONFIGDATA, hpInit As HOPPER_INIT_DATA) As Boolean
'Dim bData() As Byte
'Dim i As Integer, j As Integer
'Dim sz As String, szlen As Integer
'
'    GetHopperInitSensorData = False
'
'    sspC.CommandDataLength = 1
'    sspC.EncryptionStatus = 0
'    sspC.CommandData(0) = SERIAL_NUMBER
'    sspInfo.CommandName = "SERIAL_NUMBER"
'    If Not TransmitSSPCommand(sspC, sspInfo) Then Exit Function
'    bvData.BVSerialNumber = 0
'    On Local Error Resume Next
'    For i = 0 To 3
'        bvData.BVSerialNumber = bvData.BVSerialNumber + CLng(sspC.ResponseData(i + 1)) * (256 ^ (3 - i))
'    Next i
'
'    sspC.CommandDataLength = 3
'    sspC.EncryptionStatus = 0
'    sspC.CommandData(0) = EXPANSION_COMMAND
'    sspC.CommandData(1) = NV7
'    sspC.CommandData(2) = GET_SENSOR_CONFIG_DATA
'    sspInfo.CommandName = "EXP GET_SENSOR_CONFIG_DATA"
'    If Not TransmitSSPCommand(sspC, sspInfo) Then Exit Function
'
'        ' how many sensors
'    bvData.BVSensors = sspC.ResponseData(1) + sspC.ResponseData(2)
'
'    ' the validator type
'    bvData.BVType = ""
'    For i = 0 To 9
'        If sspC.ResponseData(18 + i) < 48 Or sspC.ResponseData(18 + i) > 126 Then Exit For
'        bvData.BVType = bvData.BVType & Chr(sspC.ResponseData(18 + i))
'    Next i
'    bvData.BVType = Trim(bvData.BVType)
'
'
'    sspC.CommandDataLength = 3
'    sspC.EncryptionStatus = 0
'    sspC.CommandData(0) = EXPANSION_COMMAND
'    sspC.CommandData(1) = NV7
'    sspC.CommandData(2) = GET_FULL_FIRMWARE
'    sspInfo.CommandName = "EXP GET_FULL_FIRMWARE"
'    If Not TransmitSSPCommand(sspC, sspInfo) Then Exit Function
'
'    ' get the validator type
'    bvData.BVType = ""
'    For i = 0 To 5
'        bvData.BVType = bvData.BVType & Chr(sspC.ResponseData(i + 1))
'    Next i
'
'
'    ReDim bvData.BVSensorcode(bvData.BVSensors - 1) As Byte
'    ReDim bvData.BVSensorType(bvData.BVSensors - 1) As Byte
'    ReDim bvData.BVSensorsNames(bvData.BVSensors - 1) As String
'    ReDim bvData.BVSensorGain1(bvData.BVSensors - 1) As Byte
'    ReDim bvData.BVSensorGain2(bvData.BVSensors - 1) As Byte
'    ReDim bvData.BVSensorPathTgt(bvData.BVSensors - 1) As Byte
'    ReDim bvData.BVSensorPaperTgt(bvData.BVSensors - 1) As Byte
'    ReDim bvData.BVSensorReadGain(bvData.BVSensors - 1) As Byte
'    ReDim bvData.BVSensorCalGain(bvData.BVSensors - 1) As Byte
'    ReDim bvData.BVSensorClear(bvData.BVSensors - 1) As Byte
'    ReDim bvData.BVSensorDetect(bvData.BVSensors - 1) As Byte
'    ReDim bvData.BVSensorThreshold(bvData.BVSensors - 1) As Integer
'    ReDim bvData.BVFirstGain1(bvData.BVSensors - 1) As Byte
'    ReDim bvData.BVFirstGain2(bvData.BVSensors - 1) As Byte
'    ReDim bvData.BVTargetAchieved(bvData.BVSensors - 1) As Byte
'    ReDim bvData.BVPaperAchieved(bvData.BVSensors - 1) As Byte
'    ReDim bvData.BVClearThreshold(bvData.BVSensors - 1) As Integer
'    ReDim bvData.HPCoilPathTarget(bvData.BVSensors - 1) As Long
'    ReDim bvData.HPCoilPathAch(bvData.BVSensors - 1) As Long
'
'    sspC.CommandDataLength = 4
'    sspC.EncryptionStatus = 0
'    sspC.CommandData(0) = EXPANSION_COMMAND
'    sspC.CommandData(1) = NV7
'    sspC.CommandData(2) = GET_SENSOR_INFO_DATA
'    sspInfo.CommandName = "EXP GET_SENSOR_INFO_DATA"
'
'
'
'
'
'    For i = 0 To bvData.BVSensors - 1
'        sspC.CommandData(3) = i
'        If Not TransmitSSPCommand(sspC, sspInfo) Then Exit Function
'
'        bvData.BVSensorsNames(i) = ""
'        For j = 0 To 19
'            bvData.BVSensorsNames(i) = bvData.BVSensorsNames(i) & Chr(sspC.ResponseData(1 + j))
'        Next j
'        bvData.BVSensorsNames(i) = Trim(bvData.BVSensorsNames(i))
'
'        bvData.BVSensorcode(i) = sspC.ResponseData(1 + 20)
'        bvData.BVSensorType(i) = sspC.ResponseData(1 + 21)
'        bvData.BVSensorPaperTgt(i) = sspC.ResponseData(1 + 22)
'        bvData.BVSensorPathTgt(i) = sspC.ResponseData(1 + 23)
'        bvData.BVSensorGain1(i) = sspC.ResponseData(1 + 24)
'        bvData.BVSensorGain2(i) = sspC.ResponseData(1 + 25)
'        bvData.BVSensorClear(i) = sspC.ResponseData(1 + 26)
'        bvData.BVSensorDetect(i) = sspC.ResponseData(1 + 27)
'        bvData.BVSensorCalGain(i) = sspC.ResponseData(1 + 28)
'        bvData.BVSensorThreshold(i) = sspC.ResponseData(1 + 29)
'        bvData.BVFirstGain1(i) = sspC.ResponseData(1 + 30)
'        bvData.BVFirstGain2(i) = sspC.ResponseData(1 + 31)
'        bvData.BVTargetAchieved(i) = sspC.ResponseData(1 + 32)
'        bvData.BVPaperAchieved(i) = sspC.ResponseData(1 + 33)
'        bvData.HPCoilPathAch(i) = CLng(sspC.ResponseData(1 + 56)) + (CLng(sspC.ResponseData(1 + 57)) * 256)
'        bvData.HPCoilPathTarget(i) = CLng(sspC.ResponseData(1 + 58)) + (CLng(sspC.ResponseData(1 + 59)) * 256)
'        bvData.BVClearThreshold(i) = sspC.ResponseData(1 + 34)
'
'
'
'        If bvData.BVSensorThreshold(i) > 127 Then
'            bvData.BVSensorThreshold(i) = bvData.BVSensorThreshold(i) - 256
'        End If
'
'        If bvData.BVClearThreshold(i) > 127 Then
'            bvData.BVClearThreshold(i) = bvData.BVClearThreshold(i) - 256
'        End If
'
'    Next i
'
'    sspC.CommandDataLength = 4
'    sspC.EncryptionStatus = 0
'    sspC.CommandData(0) = EXPANSION_COMMAND
'    sspC.CommandData(1) = NV7
'    sspC.CommandData(2) = SMART_HOPPER
'    sspC.CommandData(3) = GET_INIT_CORRECTION_DATA
'    sspInfo.CommandName = "GET_INIT_CORRECTION_DATA"
'    If Not TransmitSSPCommand(sspC, sspInfo) Then Exit Function
'
'    hpInit.Hdr = CLng(sspC.ResponseData(1)) + (CLng(sspC.ResponseData(2) * 256))
'    hpInit.tablesize = CLng(sspC.ResponseData(3)) + (CLng(sspC.ResponseData(4) * 256))
'    hpInit.crc = CLng(sspC.ResponseData(5)) + (CLng(sspC.ResponseData(6) * 256))
'    hpInit.InitStatus = sspC.ResponseData(7)
'    hpInit.InitNumber = sspC.ResponseData(8)
'    hpInit.Coil1Target = CLng(sspC.ResponseData(9)) + (CLng(sspC.ResponseData(10) * 256))
'    hpInit.Coil1Level = CLng(sspC.ResponseData(11)) + (CLng(sspC.ResponseData(12) * 256))
'    hpInit.Coil2Target = CLng(sspC.ResponseData(13)) + (CLng(sspC.ResponseData(14) * 256))
'    hpInit.Coil2Level = CLng(sspC.ResponseData(15)) + (CLng(sspC.ResponseData(16) * 256))
'
'    GetHopperInitSensorData = True
'
'    Call CloseSSPComPort

End Function

Public Sub RestartSmartPayout()
    CloseCommunicationPorts
    OpenCommunicationPorts
End Sub
