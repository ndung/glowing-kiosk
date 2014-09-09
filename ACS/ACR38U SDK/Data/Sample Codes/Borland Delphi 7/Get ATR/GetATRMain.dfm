object MainGetATR: TMainGetATR
  Left = 192
  Top = 107
  Width = 673
  Height = 381
  Caption = 'Get ATR Sample'
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
    Top = 16
    Width = 68
    Height = 13
    Caption = 'Select Reader'
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
  object bInit: TButton
    Left = 152
    Top = 48
    Width = 113
    Height = 25
    Caption = '&Initialize'
    TabOrder = 1
    OnClick = bInitClick
  end
  object bConnect: TButton
    Left = 152
    Top = 88
    Width = 113
    Height = 25
    Caption = '&Connect'
    TabOrder = 2
    OnClick = bConnectClick
  end
  object bGetAtr: TButton
    Left = 152
    Top = 128
    Width = 113
    Height = 25
    Caption = '&Get ATR'
    TabOrder = 3
    OnClick = bGetAtrClick
  end
  object mMsg: TRichEdit
    Left = 288
    Top = 16
    Width = 361
    Height = 321
    ScrollBars = ssVertical
    TabOrder = 4
  end
  object bReset: TButton
    Left = 152
    Top = 264
    Width = 113
    Height = 25
    Caption = '&Reset'
    TabOrder = 5
    OnClick = bResetClick
  end
  object bQuit: TButton
    Left = 152
    Top = 304
    Width = 113
    Height = 25
    Caption = '&Quit'
    TabOrder = 6
    OnClick = bQuitClick
  end
  object bClear: TButton
    Left = 152
    Top = 224
    Width = 113
    Height = 25
    Caption = 'C&lear Screen'
    TabOrder = 7
    OnClick = bClearClick
  end
end
