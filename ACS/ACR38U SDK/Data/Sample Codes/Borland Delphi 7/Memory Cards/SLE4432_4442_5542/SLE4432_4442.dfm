object SLE4432_4442Main: TSLE4432_4442Main
  Left = 252
  Top = 256
  Caption = 'Using SLE4432/4442/5542 in PC/SC'
  ClientHeight = 360
  ClientWidth = 584
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -12
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  PixelsPerInch = 96
  TextHeight = 14
  object Label1: TLabel
    Left = 8
    Top = 8
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
  object rgCardType: TRadioGroup
    Left = 8
    Top = 32
    Width = 129
    Height = 73
    Caption = 'Card Type'
    Items.Strings = (
      'SLE 4432'
      'SLE 4442/5542')
    TabOrder = 6
    OnClick = rgCardTypeClick
  end
  object cbReader: TComboBox
    Left = 112
    Top = 8
    Width = 145
    Height = 22
    ItemHeight = 14
    TabOrder = 0
    OnClick = cbReaderClick
  end
  object bInit: TButton
    Left = 168
    Top = 40
    Width = 89
    Height = 25
    Caption = '&Initialize'
    TabOrder = 1
    OnClick = bInitClick
  end
  object bConnect: TButton
    Left = 168
    Top = 80
    Width = 89
    Height = 25
    Caption = '&Connect'
    TabOrder = 2
    OnClick = bConnectClick
  end
  object mMsg: TRichEdit
    Left = 264
    Top = 8
    Width = 313
    Height = 289
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    ScrollBars = ssVertical
    TabOrder = 3
  end
  object bReset: TButton
    Left = 296
    Top = 320
    Width = 115
    Height = 25
    Caption = 'R&eset'
    TabOrder = 4
    OnClick = bResetClick
  end
  object bQuit: TButton
    Left = 456
    Top = 320
    Width = 113
    Height = 25
    Caption = '&Quit'
    TabOrder = 5
    OnClick = bQuitClick
  end
  object gbFunction: TGroupBox
    Left = 8
    Top = 112
    Width = 249
    Height = 241
    Caption = 'Functions'
    TabOrder = 7
    object Label2: TLabel
      Left = 16
      Top = 24
      Width = 43
      Height = 14
      Caption = 'Address'
    end
    object Label3: TLabel
      Left = 144
      Top = 24
      Width = 39
      Height = 14
      Caption = 'Length'
    end
    object Label4: TLabel
      Left = 16
      Top = 56
      Width = 71
      Height = 14
      Caption = 'Data (String)'
    end
    object tLen: TEdit
      Left = 192
      Top = 24
      Width = 41
      Height = 22
      MaxLength = 2
      TabOrder = 0
      OnExit = tLenExit
      OnKeyPress = tLenKeyPress
      OnKeyUp = tLenKeyUp
    end
    object tData: TEdit
      Left = 16
      Top = 72
      Width = 217
      Height = 22
      MaxLength = 256
      TabOrder = 1
    end
    object bRead: TButton
      Left = 136
      Top = 112
      Width = 97
      Height = 25
      Caption = '&Read'
      TabOrder = 2
      OnClick = bReadClick
    end
    object bWrite: TButton
      Left = 136
      Top = 152
      Width = 97
      Height = 25
      Caption = '&Write'
      TabOrder = 3
      OnClick = bWriteClick
    end
    object tAdd: TEdit
      Left = 64
      Top = 24
      Width = 33
      Height = 22
      MaxLength = 2
      TabOrder = 4
      OnKeyPress = tAddKeyPress
      OnKeyUp = tAddKeyUp
    end
    object bWriteProt: TButton
      Left = 16
      Top = 152
      Width = 97
      Height = 25
      Caption = 'Write &Protect'
      TabOrder = 5
      OnClick = bWriteProtClick
    end
    object bSubmit: TButton
      Left = 16
      Top = 192
      Width = 97
      Height = 25
      Caption = '&Submit Code'
      TabOrder = 6
      OnClick = bSubmitClick
    end
    object bReadCtr: TButton
      Left = 16
      Top = 112
      Width = 97
      Height = 25
      Caption = 'Read Err C&tr'
      TabOrder = 7
      OnClick = bReadCtrClick
    end
    object bChange: TButton
      Left = 136
      Top = 192
      Width = 97
      Height = 25
      Caption = 'C&hange Code'
      TabOrder = 8
      OnClick = bChangeClick
    end
  end
end
