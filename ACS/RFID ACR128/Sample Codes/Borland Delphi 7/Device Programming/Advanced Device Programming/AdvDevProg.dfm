object MainAdvDevProg: TMainAdvDevProg
  Left = 42
  Top = 52
  Width = 946
  Height = 593
  Caption = 'Advanced Device Programming'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnActivate = FormActivate
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 8
    Top = 19
    Width = 81
    Height = 13
    AutoSize = False
    Caption = 'Select Reader'
  end
  object cbReader: TComboBox
    Left = 88
    Top = 16
    Width = 193
    Height = 21
    Style = csDropDownList
    ItemHeight = 13
    TabOrder = 0
  end
  object bInit: TButton
    Left = 168
    Top = 48
    Width = 113
    Height = 25
    Caption = '&Initialize'
    TabOrder = 1
    OnClick = bInitClick
  end
  object bConnect: TButton
    Left = 168
    Top = 80
    Width = 113
    Height = 25
    Caption = '&Connect'
    TabOrder = 2
    OnClick = bConnectClick
  end
  object mMsg: TRichEdit
    Left = 608
    Top = 200
    Width = 321
    Height = 313
    TabOrder = 3
  end
  object bClear: TButton
    Left = 616
    Top = 528
    Width = 95
    Height = 25
    Caption = 'C&lear Screen'
    TabOrder = 4
    OnClick = bClearClick
  end
  object bReset: TButton
    Left = 720
    Top = 528
    Width = 95
    Height = 25
    Caption = '&Reset'
    TabOrder = 5
    OnClick = bResetClick
  end
  object bQuit: TButton
    Left = 824
    Top = 528
    Width = 95
    Height = 25
    Caption = '&Quit'
    TabOrder = 6
    OnClick = bQuitClick
  end
  object gbFWI: TGroupBox
    Left = 8
    Top = 112
    Width = 273
    Height = 97
    Caption = 'FWI, Polling Timeout and Transmit Frame Size'
    TabOrder = 7
    object Label2: TLabel
      Left = 38
      Top = 20
      Width = 50
      Height = 13
      Caption = 'FWI Value'
    end
    object Label3: TLabel
      Left = 38
      Top = 44
      Width = 68
      Height = 13
      Caption = 'Polling timeout'
    end
    object Label4: TLabel
      Left = 38
      Top = 68
      Width = 95
      Height = 13
      Caption = 'Transmit Frame Size'
    end
    object bGetFWI: TButton
      Left = 160
      Top = 24
      Width = 105
      Height = 25
      Caption = '&Get Current Values'
      TabOrder = 0
      OnClick = bGetFWIClick
    end
    object bSetFWI: TButton
      Left = 160
      Top = 56
      Width = 105
      Height = 25
      Caption = '&Set Options'
      TabOrder = 1
      OnClick = bSetFWIClick
    end
    object tFWI: TEdit
      Left = 8
      Top = 16
      Width = 25
      Height = 21
      CharCase = ecUpperCase
      MaxLength = 2
      TabOrder = 2
      OnKeyPress = tFWIKeyPress
    end
    object tPollTO: TEdit
      Left = 8
      Top = 40
      Width = 25
      Height = 21
      CharCase = ecUpperCase
      MaxLength = 2
      TabOrder = 3
      OnKeyPress = tFWIKeyPress
    end
    object tTFS: TEdit
      Left = 8
      Top = 64
      Width = 25
      Height = 21
      CharCase = ecUpperCase
      MaxLength = 2
      TabOrder = 4
      OnKeyPress = tFWIKeyPress
    end
  end
  object gbAntenna: TGroupBox
    Left = 8
    Top = 216
    Width = 273
    Height = 89
    Caption = 'Antenna Field Setting'
    TabOrder = 8
    object bGetAS: TButton
      Left = 160
      Top = 16
      Width = 105
      Height = 25
      Caption = 'Get C&urrent Setting'
      TabOrder = 0
      OnClick = bGetASClick
    end
    object bSetAS: TButton
      Left = 160
      Top = 48
      Width = 105
      Height = 25
      Caption = 'Set &Antenna Option'
      TabOrder = 1
      OnClick = bSetASClick
    end
    object rbAntOn: TRadioButton
      Left = 8
      Top = 24
      Width = 113
      Height = 17
      Caption = 'Antenna ON'
      TabOrder = 2
    end
    object rbAntOff: TRadioButton
      Left = 8
      Top = 48
      Width = 113
      Height = 17
      Caption = 'Antenna OFF'
      TabOrder = 3
    end
  end
  object gbTransSet: TGroupBox
    Left = 8
    Top = 312
    Width = 273
    Height = 145
    Caption = 'Tranceiver Settings'
    TabOrder = 9
    object Label5: TLabel
      Left = 38
      Top = 20
      Width = 109
      Height = 13
      Caption = 'Field Stop Time (x5 ms)'
    end
    object Label6: TLabel
      Left = 38
      Top = 44
      Width = 96
      Height = 13
      Caption = 'Setup Time (x10 ms)'
    end
    object Label7: TLabel
      Left = 38
      Top = 67
      Width = 80
      Height = 13
      Caption = 'LP Filter (On/Off)'
    end
    object Label8: TLabel
      Left = 40
      Top = 91
      Width = 68
      Height = 13
      Caption = 'Receiver Gain'
    end
    object Label9: TLabel
      Left = 40
      Top = 115
      Width = 44
      Height = 13
      Caption = 'TX Mode'
    end
    object tFStop: TEdit
      Left = 8
      Top = 16
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 0
      OnKeyPress = tFStopKeyPress
    end
    object tSetup: TEdit
      Left = 8
      Top = 40
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 1
      OnKeyPress = tFStopKeyPress
    end
    object cbFilter: TCheckBox
      Left = 8
      Top = 64
      Width = 25
      Height = 17
      TabOrder = 2
    end
    object tRecGain: TEdit
      Left = 8
      Top = 88
      Width = 25
      Height = 21
      MaxLength = 1
      TabOrder = 3
      OnKeyPress = tFStopKeyPress
    end
    object tTxMode: TEdit
      Left = 8
      Top = 112
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 4
      OnKeyPress = tFWIKeyPress
    end
    object bGetTranSet: TButton
      Left = 144
      Top = 72
      Width = 121
      Height = 25
      Caption = 'G&et Current Setting'
      TabOrder = 5
      OnClick = bGetTranSetClick
    end
    object bSetTranSet: TButton
      Left = 144
      Top = 104
      Width = 121
      Height = 25
      Caption = 'Set &Tranceiver Options'
      TabOrder = 6
      OnClick = bSetTranSetClick
    end
  end
  object gbPICC: TGroupBox
    Left = 296
    Top = 16
    Width = 297
    Height = 177
    Caption = 'PICC Settings'
    TabOrder = 10
    object Label10: TLabel
      Left = 37
      Top = 19
      Width = 44
      Height = 13
      Caption = 'MOD_B1'
    end
    object Label11: TLabel
      Left = 37
      Top = 43
      Width = 50
      Height = 13
      Caption = 'COND_B1'
    end
    object Label12: TLabel
      Left = 37
      Top = 67
      Width = 34
      Height = 13
      Caption = 'RX_B1'
    end
    object Label13: TLabel
      Left = 37
      Top = 91
      Width = 44
      Height = 13
      Caption = 'MOD_B2'
    end
    object Label14: TLabel
      Left = 37
      Top = 115
      Width = 50
      Height = 13
      Caption = 'COND_B2'
    end
    object Label15: TLabel
      Left = 37
      Top = 139
      Width = 34
      Height = 13
      Caption = 'RX_B2'
    end
    object Label16: TLabel
      Left = 125
      Top = 19
      Width = 44
      Height = 13
      Caption = 'MOD_A1'
    end
    object Label17: TLabel
      Left = 125
      Top = 43
      Width = 50
      Height = 13
      Caption = 'COND_A1'
    end
    object Label18: TLabel
      Left = 125
      Top = 67
      Width = 34
      Height = 13
      Caption = 'RX_A1'
    end
    object Label19: TLabel
      Left = 125
      Top = 91
      Width = 44
      Height = 13
      Caption = 'MOD_A2'
    end
    object Label20: TLabel
      Left = 125
      Top = 115
      Width = 50
      Height = 13
      Caption = 'COND_A2'
    end
    object Label21: TLabel
      Left = 125
      Top = 139
      Width = 34
      Height = 13
      Caption = 'RX_A2'
    end
    object tPICC1: TEdit
      Left = 8
      Top = 16
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 0
      OnKeyPress = tFWIKeyPress
    end
    object tPICC2: TEdit
      Left = 8
      Top = 40
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 1
      OnKeyPress = tFWIKeyPress
    end
    object tPICC3: TEdit
      Left = 8
      Top = 64
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 2
      OnKeyPress = tFWIKeyPress
    end
    object tPICC4: TEdit
      Left = 8
      Top = 88
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 3
      OnKeyPress = tFWIKeyPress
    end
    object tPICC5: TEdit
      Left = 8
      Top = 112
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 4
      OnKeyPress = tFWIKeyPress
    end
    object tPICC6: TEdit
      Left = 8
      Top = 136
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 5
      OnKeyPress = tFWIKeyPress
    end
    object tPICC7: TEdit
      Left = 96
      Top = 16
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 6
      OnKeyPress = tFWIKeyPress
    end
    object tPICC8: TEdit
      Left = 96
      Top = 40
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 7
      OnKeyPress = tFWIKeyPress
    end
    object tPICC9: TEdit
      Left = 96
      Top = 64
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 8
      OnKeyPress = tFWIKeyPress
    end
    object tPICC10: TEdit
      Left = 96
      Top = 88
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 9
      OnKeyPress = tFWIKeyPress
    end
    object tPICC11: TEdit
      Left = 96
      Top = 112
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 10
      OnKeyPress = tFWIKeyPress
    end
    object tPICC12: TEdit
      Left = 96
      Top = 136
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 11
      OnKeyPress = tFWIKeyPress
    end
    object bGetPICC: TButton
      Left = 184
      Top = 104
      Width = 105
      Height = 25
      Caption = 'Get &PICC Settings'
      TabOrder = 12
      OnClick = bGetPICCClick
    end
    object bSetPICC: TButton
      Left = 184
      Top = 136
      Width = 105
      Height = 25
      Caption = 'Set PICC &Options'
      TabOrder = 13
      OnClick = bSetPICCClick
    end
  end
  object gbPolling: TGroupBox
    Left = 296
    Top = 200
    Width = 297
    Height = 145
    Caption = 'Polling for Specific PICC Types'
    TabOrder = 11
    object Label22: TLabel
      Left = 16
      Top = 115
      Width = 46
      Height = 13
      Caption = 'Message:'
    end
    object rbType1: TRadioButton
      Left = 16
      Top = 24
      Width = 140
      Height = 17
      Caption = 'ISO14443 Type A only'
      TabOrder = 0
    end
    object rbType2: TRadioButton
      Left = 16
      Top = 48
      Width = 140
      Height = 17
      Caption = 'ISO14443 Type B only'
      TabOrder = 1
    end
    object rbType3: TRadioButton
      Left = 16
      Top = 72
      Width = 140
      Height = 17
      Caption = 'ISO14443 Types A/B'
      TabOrder = 2
    end
    object bPoll: TButton
      Left = 184
      Top = 80
      Width = 105
      Height = 25
      Caption = 'Start Auto &Detection'
      TabOrder = 3
      OnClick = bPollClick
    end
    object tMsg: TEdit
      Left = 72
      Top = 112
      Width = 217
      Height = 21
      TabOrder = 4
    end
    object bGetPSet: TButton
      Left = 184
      Top = 16
      Width = 105
      Height = 25
      Caption = 'Get Curre&nt Setting'
      TabOrder = 5
      OnClick = bGetPSetClick
    end
    object bSetPSet: TButton
      Left = 184
      Top = 48
      Width = 105
      Height = 25
      Caption = 'Set PICC T&ype'
      TabOrder = 6
      OnClick = bSetPSetClick
    end
  end
  object gbErrHand: TGroupBox
    Left = 8
    Top = 464
    Width = 273
    Height = 89
    Caption = 'PICC T=CL Data Exchange Error Handling'
    TabOrder = 12
    object Label23: TLabel
      Left = 40
      Top = 28
      Width = 61
      Height = 13
      Caption = 'PCD to PICC'
    end
    object Label24: TLabel
      Left = 40
      Top = 60
      Width = 61
      Height = 13
      Caption = 'PICC to PCD'
    end
    object tPc2Pi: TEdit
      Left = 8
      Top = 24
      Width = 25
      Height = 21
      MaxLength = 1
      TabOrder = 0
      OnKeyPress = tFStopKeyPress
    end
    object tPi2Pc: TEdit
      Left = 8
      Top = 56
      Width = 25
      Height = 21
      MaxLength = 1
      TabOrder = 1
      OnKeyPress = tFStopKeyPress
    end
    object bGetEH: TButton
      Left = 160
      Top = 16
      Width = 105
      Height = 25
      Caption = 'Get Current &Value'
      TabOrder = 2
      OnClick = bGetEHClick
    end
    object bSetEH: TButton
      Left = 160
      Top = 48
      Width = 105
      Height = 25
      Caption = 'Set Error &Handling'
      TabOrder = 3
      OnClick = bSetEHClick
    end
  end
  object gbPPS: TGroupBox
    Left = 296
    Top = 352
    Width = 297
    Height = 201
    Caption = 'PPS Setting (Communication Speed)'
    TabOrder = 13
    object bGetPPS: TButton
      Left = 64
      Top = 160
      Width = 105
      Height = 25
      Caption = 'Get Current Setting'
      TabOrder = 0
      OnClick = bGetPPSClick
    end
    object bSetPPS: TButton
      Left = 176
      Top = 160
      Width = 105
      Height = 25
      Caption = 'Set PPS Value'
      TabOrder = 1
      OnClick = bSetPPSClick
    end
    object rgMaxSpeed: TRadioGroup
      Left = 16
      Top = 16
      Width = 121
      Height = 129
      Caption = 'Maximum Speed'
      Items.Strings = (
        '106 kbps'
        '212 kbps'
        '424 kbps'
        '848 kbps'
        'No Auto PPS')
      TabOrder = 2
    end
    object rgCurrSpeed: TRadioGroup
      Left = 160
      Top = 16
      Width = 121
      Height = 129
      Caption = 'Current Speed'
      Items.Strings = (
        '106 kbps'
        '212 kbps'
        '424 kbps'
        '848 kbps'
        'No Auto PPS')
      TabOrder = 3
    end
  end
  object gbReg: TGroupBox
    Left = 608
    Top = 16
    Width = 321
    Height = 81
    Caption = 'RC531 Register Setting'
    TabOrder = 14
    object Label25: TLabel
      Left = 56
      Top = 52
      Width = 69
      Height = 13
      Caption = 'Register Value'
    end
    object Label26: TLabel
      Left = 56
      Top = 28
      Width = 79
      Height = 13
      Caption = 'Register Number'
    end
    object tRegVal: TEdit
      Left = 16
      Top = 48
      Width = 33
      Height = 21
      MaxLength = 2
      TabOrder = 0
      OnKeyPress = tFWIKeyPress
    end
    object tRegNo: TEdit
      Left = 16
      Top = 24
      Width = 33
      Height = 21
      MaxLength = 2
      TabOrder = 1
      OnKeyPress = tFWIKeyPress
    end
    object bGetReg: TButton
      Left = 200
      Top = 16
      Width = 107
      Height = 25
      Caption = 'Get Current Value'
      TabOrder = 2
      OnClick = bGetRegClick
    end
    object bSetReg: TButton
      Left = 200
      Top = 48
      Width = 107
      Height = 25
      Caption = 'Set New Value'
      TabOrder = 3
      OnClick = bSetRegClick
    end
  end
  object gbRefIS: TGroupBox
    Left = 608
    Top = 104
    Width = 321
    Height = 89
    Caption = 'Refresh Interface Status'
    TabOrder = 15
    object bRefIS: TButton
      Left = 200
      Top = 48
      Width = 105
      Height = 25
      Caption = 'Re&fresh Interface'
      TabOrder = 0
      OnClick = bRefISClick
    end
    object rbRIS1: TRadioButton
      Left = 16
      Top = 16
      Width = 113
      Height = 17
      Caption = 'ICC Interface'
      TabOrder = 1
    end
    object rbRIS2: TRadioButton
      Left = 16
      Top = 40
      Width = 113
      Height = 17
      Caption = 'PICC Interface'
      TabOrder = 2
    end
    object rbRIS3: TRadioButton
      Left = 16
      Top = 64
      Width = 113
      Height = 17
      Caption = 'SAM Interface'
      TabOrder = 3
    end
  end
  object Polltimer: TTimer
    Enabled = False
    Interval = 500
    OnTimer = PolltimerTimer
    Left = 904
  end
end
