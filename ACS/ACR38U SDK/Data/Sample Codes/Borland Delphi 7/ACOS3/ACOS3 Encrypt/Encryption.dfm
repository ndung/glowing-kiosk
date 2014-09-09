object MainEncrypt: TMainEncrypt
  Left = 479
  Top = 241
  Width = 602
  Height = 508
  Caption = 'Using Encryption Options in ACOS3'
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -12
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnActivate = FormActivate
  PixelsPerInch = 96
  TextHeight = 14
  object Label1: TLabel
    Left = 8
    Top = 16
    Width = 91
    Height = 16
    Caption = 'Select Reader'
    Font.Charset = ANSI_CHARSET
    Font.Color = clNavy
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object mMsg: TRichEdit
    Left = 280
    Top = 8
    Width = 305
    Height = 393
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    Lines.Strings = (
      '')
    ParentFont = False
    ScrollBars = ssVertical
    TabOrder = 0
  end
  object cbReader: TComboBox
    Left = 112
    Top = 16
    Width = 153
    Height = 22
    ItemHeight = 14
    TabOrder = 1
    OnChange = cbReaderChange
  end
  object bInit: TButton
    Left = 24
    Top = 56
    Width = 107
    Height = 25
    Caption = '&Initialize'
    TabOrder = 2
    OnClick = bInitClick
  end
  object bConnect: TButton
    Left = 160
    Top = 56
    Width = 107
    Height = 25
    Caption = '&Connect'
    TabOrder = 3
    OnClick = bConnectClick
  end
  object rgOption: TRadioGroup
    Left = 144
    Top = 96
    Width = 121
    Height = 81
    Caption = 'Security Option'
    Items.Strings = (
      'DES'
      '3-DES')
    TabOrder = 4
    OnClick = rgOptionClick
  end
  object bReset: TButton
    Left = 312
    Top = 424
    Width = 107
    Height = 25
    Caption = '&Reset'
    TabOrder = 5
    OnClick = bResetClick
  end
  object bQuit: TButton
    Left = 448
    Top = 424
    Width = 107
    Height = 25
    Caption = '&Quit'
    TabOrder = 6
    OnClick = bQuitClick
  end
  object rgEncrypt: TRadioGroup
    Left = 8
    Top = 96
    Width = 121
    Height = 81
    Caption = 'Encryption Option'
    Items.Strings = (
      'Not Encrypted'
      'Encrypted')
    TabOrder = 7
    OnClick = rgEncryptClick
  end
  object gbSubmit: TGroupBox
    Left = 8
    Top = 320
    Width = 257
    Height = 145
    Caption = 'Code Submission'
    TabOrder = 8
    object Label2: TLabel
      Left = 16
      Top = 32
      Width = 32
      Height = 16
      Caption = 'Code'
      Font.Charset = ANSI_CHARSET
      Font.Color = clNavy
      Font.Height = -13
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
    end
    object Label3: TLabel
      Left = 16
      Top = 72
      Width = 36
      Height = 16
      Caption = 'Value'
      Font.Charset = ANSI_CHARSET
      Font.Color = clNavy
      Font.Height = -13
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
    end
    object cbCode: TComboBox
      Left = 96
      Top = 32
      Width = 145
      Height = 22
      ItemHeight = 14
      TabOrder = 0
      Items.Strings = (
        'PIN'
        'App Code 1'
        'App Code 2'
        'App Code 3'
        'App Code 4'
        'App Code 5')
    end
    object tValue: TEdit
      Left = 96
      Top = 64
      Width = 145
      Height = 22
      MaxLength = 8
      TabOrder = 1
    end
    object bSubmit: TButton
      Left = 136
      Top = 104
      Width = 107
      Height = 25
      Caption = '&Submit'
      TabOrder = 2
      OnClick = bSubmitClick
    end
    object bSet: TButton
      Left = 16
      Top = 104
      Width = 89
      Height = 25
      Caption = 'Set &Value'
      TabOrder = 3
      OnClick = bSetClick
    end
  end
  object gbInput: TGroupBox
    Left = 8
    Top = 184
    Width = 257
    Height = 129
    Caption = 'Key Template'
    TabOrder = 9
    object Label4: TLabel
      Left = 8
      Top = 24
      Width = 58
      Height = 16
      Caption = 'Card Key'
      Font.Charset = ANSI_CHARSET
      Font.Color = clNavy
      Font.Height = -13
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
    end
    object Label5: TLabel
      Left = 8
      Top = 56
      Width = 82
      Height = 16
      Caption = 'Terminal Key'
      Font.Charset = ANSI_CHARSET
      Font.Color = clNavy
      Font.Height = -13
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
    end
    object bFormat: TButton
      Left = 136
      Top = 88
      Width = 107
      Height = 25
      Caption = '&Format Card'
      TabOrder = 0
      OnClick = bFormatClick
    end
    object tCard: TEdit
      Left = 96
      Top = 24
      Width = 145
      Height = 22
      TabOrder = 1
      OnKeyUp = tCardKeyUp
    end
    object tTerminal: TEdit
      Left = 96
      Top = 56
      Width = 145
      Height = 22
      TabOrder = 2
    end
  end
end
