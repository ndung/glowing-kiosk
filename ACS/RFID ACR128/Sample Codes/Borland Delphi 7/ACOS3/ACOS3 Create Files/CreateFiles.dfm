object MainCreateFiles: TMainCreateFiles
  Left = 281
  Top = 197
  Width = 644
  Height = 350
  Caption = 'an'
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
    Left = 208
    Top = 8
    Width = 417
    Height = 297
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
    Left = 24
    Top = 40
    Width = 169
    Height = 22
    ItemHeight = 14
    TabOrder = 1
    OnChange = cbReaderChange
  end
  object bInit: TButton
    Left = 52
    Top = 82
    Width = 107
    Height = 25
    Caption = '&Initialize'
    TabOrder = 2
    OnClick = bInitClick
  end
  object bConnect: TButton
    Left = 52
    Top = 130
    Width = 107
    Height = 25
    Caption = '&Connect'
    TabOrder = 3
    OnClick = bConnectClick
  end
  object bCreate: TButton
    Left = 52
    Top = 178
    Width = 107
    Height = 25
    Caption = 'Create &Files'
    TabOrder = 4
    OnClick = bCreateClick
  end
  object bReset: TButton
    Left = 52
    Top = 226
    Width = 105
    Height = 25
    Caption = '&Reset'
    TabOrder = 5
    OnClick = bResetClick
  end
  object bQuit: TButton
    Left = 52
    Top = 274
    Width = 105
    Height = 25
    Caption = '&Quit'
    TabOrder = 6
    OnClick = bQuitClick
  end
end
