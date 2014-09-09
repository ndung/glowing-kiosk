object MainMutAuth: TMainMutAuth
  Left = 354
  Top = 208
  Width = 662
  Height = 467
  Caption = 'Using ACOS3 for Mutual Authentication'
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
    Left = 288
    Top = 8
    Width = 361
    Height = 425
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    ScrollBars = ssVertical
    TabOrder = 0
  end
  object cbReader: TComboBox
    Left = 104
    Top = 16
    Width = 169
    Height = 22
    ItemHeight = 14
    TabOrder = 1
    OnChange = cbReaderChange
  end
  object bInit: TButton
    Left = 168
    Top = 56
    Width = 107
    Height = 25
    Caption = '&Initialize'
    TabOrder = 2
    OnClick = bInitClick
  end
  object bConnect: TButton
    Left = 168
    Top = 96
    Width = 105
    Height = 25
    Caption = '&Connect'
    TabOrder = 3
    OnClick = bConnectClick
  end
  object bReset: TButton
    Left = 168
    Top = 344
    Width = 105
    Height = 25
    Caption = '&Reset'
    TabOrder = 4
    OnClick = bResetClick
  end
  object bQuit: TButton
    Left = 168
    Top = 392
    Width = 105
    Height = 25
    Caption = '&Quit'
    TabOrder = 5
    OnClick = bQuitClick
  end
  object gbInput: TGroupBox
    Left = 8
    Top = 136
    Width = 265
    Height = 137
    Caption = 'Key Template'
    TabOrder = 6
    object Label2: TLabel
      Left = 8
      Top = 24
      Width = 48
      Height = 14
      Caption = 'Card Key'
    end
    object Label3: TLabel
      Left = 8
      Top = 56
      Width = 70
      Height = 14
      Caption = 'Terminal Key'
    end
    object bFormat: TButton
      Left = 144
      Top = 96
      Width = 105
      Height = 25
      Caption = '&Format Card'
      TabOrder = 0
      OnClick = bFormatClick
    end
    object tCard: TEdit
      Left = 96
      Top = 24
      Width = 153
      Height = 22
      TabOrder = 1
      OnKeyUp = tCardKeyUp
    end
    object tTerminal: TEdit
      Left = 96
      Top = 56
      Width = 153
      Height = 22
      TabOrder = 2
    end
  end
  object rgOption: TRadioGroup
    Left = 8
    Top = 48
    Width = 105
    Height = 73
    Caption = 'Security Option'
    Items.Strings = (
      'DES'
      '3-DES')
    TabOrder = 7
    OnClick = rgOptionClick
  end
  object bAuth: TButton
    Left = 168
    Top = 296
    Width = 105
    Height = 25
    Caption = '&Execute MA'
    TabOrder = 8
    OnClick = bAuthClick
  end
end
