object MainIIC: TMainIIC
  Left = 442
  Top = 250
  Width = 603
  Height = 430
  Caption = 'IIC Sample in PC/SC'
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -12
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnCreate = FormCreate
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
  object Label2: TLabel
    Left = 8
    Top = 88
    Width = 109
    Height = 16
    Caption = 'Select Card Type'
    Font.Charset = ANSI_CHARSET
    Font.Color = clNavy
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object cbReader: TComboBox
    Left = 112
    Top = 16
    Width = 145
    Height = 22
    ItemHeight = 14
    TabOrder = 0
    OnClick = cbReaderClick
  end
  object bInit: TButton
    Left = 152
    Top = 48
    Width = 107
    Height = 25
    Caption = '&Initialize'
    TabOrder = 1
    OnClick = bInitClick
  end
  object cbCardType: TComboBox
    Left = 136
    Top = 88
    Width = 121
    Height = 22
    ItemHeight = 14
    TabOrder = 2
    OnClick = cbCardTypeClick
    Items.Strings = (
      'Auto Detect'
      '1 Kbit'
      '2 Kbit'
      '4 Kbit'
      '8 Kbit'
      '16 Kbit'
      '32 Kbit'
      '64 Kbit'
      '128 Kbit'
      '256 Kbit'
      '512 Kbit'
      '1024 Kbit')
  end
  object bConnect: TButton
    Left = 144
    Top = 120
    Width = 113
    Height = 25
    Caption = '&Connect'
    TabOrder = 3
    OnClick = bConnectClick
  end
  object gbFunction: TGroupBox
    Left = 8
    Top = 152
    Width = 249
    Height = 241
    Caption = 'Functions'
    Font.Charset = ANSI_CHARSET
    Font.Color = clNavy
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 4
    object Label3: TLabel
      Left = 16
      Top = 32
      Width = 21
      Height = 14
      Caption = 'Size'
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlack
      Font.Height = -12
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
    end
    object Label4: TLabel
      Left = 16
      Top = 80
      Width = 43
      Height = 14
      Caption = 'Address'
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlack
      Font.Height = -12
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
    end
    object Label5: TLabel
      Left = 16
      Top = 112
      Width = 39
      Height = 14
      Caption = 'Length'
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlack
      Font.Height = -12
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
    end
    object Label6: TLabel
      Left = 16
      Top = 152
      Width = 25
      Height = 14
      Caption = 'Data'
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlack
      Font.Height = -12
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
    end
    object cbPageSize: TComboBox
      Left = 48
      Top = 32
      Width = 73
      Height = 22
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlack
      Font.Height = -12
      Font.Name = 'Tahoma'
      Font.Style = []
      ItemHeight = 14
      ParentFont = False
      TabOrder = 0
      Items.Strings = (
        '8-byte'
        '16-byte'
        '32-byte'
        '64-byte'
        '128-byte')
    end
    object tHiAdd: TEdit
      Left = 104
      Top = 80
      Width = 33
      Height = 22
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlack
      Font.Height = -12
      Font.Name = 'Tahoma'
      Font.Style = []
      MaxLength = 2
      ParentFont = False
      TabOrder = 1
      OnKeyPress = tHiAddKeyPress
      OnKeyUp = tHiAddKeyUp
    end
    object tLoAdd: TEdit
      Left = 144
      Top = 80
      Width = 33
      Height = 22
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlack
      Font.Height = -12
      Font.Name = 'Tahoma'
      Font.Style = []
      MaxLength = 2
      ParentFont = False
      TabOrder = 2
      OnChange = tLoAddChange
      OnKeyPress = tLoAddKeyPress
    end
    object bSet: TButton
      Left = 136
      Top = 32
      Width = 97
      Height = 25
      Caption = '&Set Page Size'
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlack
      Font.Height = -12
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
      TabOrder = 3
      OnClick = bSetClick
    end
    object tLen: TEdit
      Left = 64
      Top = 112
      Width = 33
      Height = 22
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlack
      Font.Height = -12
      Font.Name = 'Tahoma'
      Font.Style = []
      MaxLength = 2
      ParentFont = False
      TabOrder = 4
      OnChange = tLenChange
      OnExit = tLenExit
      OnKeyPress = tLenKeyPress
    end
    object bRead: TButton
      Left = 16
      Top = 192
      Width = 97
      Height = 25
      Caption = '&Read'
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlack
      Font.Height = -12
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
      TabOrder = 5
      OnClick = bReadClick
    end
    object bWrite: TButton
      Left = 136
      Top = 192
      Width = 97
      Height = 25
      Caption = '&Write'
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlack
      Font.Height = -12
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
      TabOrder = 6
      OnClick = bWriteClick
    end
    object tBitAdd: TEdit
      Left = 64
      Top = 80
      Width = 33
      Height = 22
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlack
      Font.Height = -12
      Font.Name = 'Tahoma'
      Font.Style = []
      MaxLength = 1
      ParentFont = False
      TabOrder = 7
      OnKeyPress = tBitAddKeyPress
      OnKeyUp = tBitAddKeyUp
    end
    object mData: TEdit
      Left = 56
      Top = 152
      Width = 177
      Height = 22
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlack
      Font.Height = -12
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
      TabOrder = 8
    end
  end
  object mMsg: TRichEdit
    Left = 272
    Top = 8
    Width = 313
    Height = 329
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    ScrollBars = ssVertical
    TabOrder = 5
  end
  object bReset: TButton
    Left = 328
    Top = 360
    Width = 107
    Height = 25
    Caption = 'R&eset'
    TabOrder = 6
    OnClick = bResetClick
  end
  object bQuit: TButton
    Left = 472
    Top = 360
    Width = 105
    Height = 25
    Caption = '&Quit'
    TabOrder = 7
    OnClick = bQuitClick
  end
end
