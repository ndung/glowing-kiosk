object MainDevProg: TMainDevProg
  Left = 443
  Top = 205
  Width = 687
  Height = 632
  Caption = 'ACR128 Device Programming'
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
    Left = 11
    Top = 19
    Width = 68
    Height = 13
    Caption = 'Select Reader'
  end
  object bQuit: TButton
    Left = 544
    Top = 560
    Width = 113
    Height = 25
    Caption = '&Quit'
    TabOrder = 11
    OnClick = bQuitClick
  end
  object bReset: TButton
    Left = 416
    Top = 560
    Width = 113
    Height = 25
    Caption = '&Reset'
    TabOrder = 10
    OnClick = bResetClick
  end
  object bInit: TButton
    Left = 152
    Top = 48
    Width = 113
    Height = 25
    Caption = '&Initialize'
    TabOrder = 1
    OnClick = bInitClick
  end
  object cbReader: TComboBox
    Left = 96
    Top = 16
    Width = 169
    Height = 21
    Style = csDropDownList
    ItemHeight = 13
    TabOrder = 0
  end
  object mMsg: TRichEdit
    Left = 280
    Top = 16
    Width = 385
    Height = 529
    TabOrder = 9
  end
  object bGetFW: TButton
    Left = 152
    Top = 112
    Width = 113
    Height = 25
    Caption = 'Get &Firmware Version'
    TabOrder = 3
    OnClick = bGetFWClick
  end
  object rgLED: TRadioGroup
    Left = 8
    Top = 144
    Width = 137
    Height = 65
    Caption = 'LED Setting'
    Columns = 2
    TabOrder = 4
  end
  object bConnect: TButton
    Left = 152
    Top = 80
    Width = 113
    Height = 25
    Caption = '&Connect'
    TabOrder = 2
    OnClick = bConnectClick
  end
  object bGetLedSet: TButton
    Left = 152
    Top = 152
    Width = 113
    Height = 25
    Caption = '&Get LED State'
    TabOrder = 5
    OnClick = bGetLedSetClick
  end
  object bSetLedSet: TButton
    Left = 152
    Top = 184
    Width = 113
    Height = 25
    Caption = '&Set LED State'
    TabOrder = 6
    OnClick = bSetLedSetClick
  end
  object gbBuzz: TGroupBox
    Left = 8
    Top = 216
    Width = 257
    Height = 105
    Caption = 'Set Buzzer Duration (x10 mSec)'
    TabOrder = 7
    object Label2: TLabel
      Left = 16
      Top = 67
      Width = 27
      Height = 13
      Caption = 'Value'
    end
    object tBuzzDur: TEdit
      Left = 56
      Top = 64
      Width = 49
      Height = 21
      MaxLength = 3
      TabOrder = 0
      OnKeyPress = tBuzzDurKeyPress
    end
    object bSetBuzzDur: TButton
      Left = 136
      Top = 64
      Width = 113
      Height = 25
      Caption = 'Set Buzzer &Duration'
      TabOrder = 1
      OnClick = bSetBuzzDurClick
    end
    object bStartBuzz: TButton
      Left = 16
      Top = 24
      Width = 113
      Height = 25
      Caption = 'Start Buzzer Tone'
      TabOrder = 2
      OnClick = bStartBuzzClick
    end
    object bStopBuzz: TButton
      Left = 136
      Top = 24
      Width = 115
      Height = 25
      Caption = 'Stop Buzzer Tone'
      TabOrder = 3
      OnClick = bStopBuzzClick
    end
  end
  object gbBuzzState: TGroupBox
    Left = 8
    Top = 328
    Width = 257
    Height = 265
    Caption = 'LED and Buzzer Setting'
    TabOrder = 8
    object bGetBuzzState: TButton
      Left = 8
      Top = 224
      Width = 113
      Height = 25
      Caption = 'Get Buzzer/LED State'
      TabOrder = 8
      OnClick = bGetBuzzStateClick
    end
    object bSetBuzzState: TButton
      Left = 128
      Top = 224
      Width = 113
      Height = 25
      Caption = 'Set &Buzzer/LED State'
      TabOrder = 9
      OnClick = bSetBuzzStateClick
    end
    object cbBuzzLed1: TCheckBox
      Left = 8
      Top = 24
      Width = 233
      Height = 17
      Caption = 'Enable ICC Activation Status LED'
      TabOrder = 0
    end
    object cbBuzzLed2: TCheckBox
      Left = 8
      Top = 48
      Width = 233
      Height = 17
      Caption = 'Enable PICC Polling Status LED'
      TabOrder = 1
    end
    object cbBuzzLed3: TCheckBox
      Left = 8
      Top = 72
      Width = 233
      Height = 17
      Caption = 'Enable PICC Activation Status Buzzer'
      TabOrder = 2
    end
    object cbBuzzLed4: TCheckBox
      Left = 8
      Top = 96
      Width = 233
      Height = 17
      Caption = 'Enable PICC PPS Status Buzzer'
      TabOrder = 3
    end
    object cbBuzzLed5: TCheckBox
      Left = 8
      Top = 120
      Width = 241
      Height = 17
      Caption = 'Enable Card Insertion/Removal Events Buzzer'
      TabOrder = 4
    end
    object cbBuzzLed6: TCheckBox
      Left = 8
      Top = 144
      Width = 233
      Height = 17
      Caption = 'Enable RC531 Reset Indication Buzzer'
      TabOrder = 5
    end
    object cbBuzzLed7: TCheckBox
      Left = 8
      Top = 168
      Width = 233
      Height = 17
      Caption = 'Enable Exclusive Mode Status Buzzer'
      TabOrder = 6
    end
    object cbBuzzLed8: TCheckBox
      Left = 8
      Top = 192
      Width = 233
      Height = 17
      Caption = 'Enable Card Operation Blinking LED'
      TabOrder = 7
    end
  end
  object bClear: TButton
    Left = 288
    Top = 560
    Width = 113
    Height = 25
    Caption = 'C&lear Screen'
    TabOrder = 12
    OnClick = bClearClick
  end
  object cbRed: TCheckBox
    Left = 24
    Top = 168
    Width = 49
    Height = 17
    Caption = 'Red'
    TabOrder = 13
  end
  object cbGreen: TCheckBox
    Left = 80
    Top = 168
    Width = 49
    Height = 17
    Caption = 'Green'
    TabOrder = 14
  end
end
