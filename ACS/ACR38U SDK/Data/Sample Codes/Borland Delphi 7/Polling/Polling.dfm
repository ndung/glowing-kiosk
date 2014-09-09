object MainPolling: TMainPolling
  Left = 267
  Top = 250
  Width = 306
  Height = 243
  Caption = 'Card Detection Polling'
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
  object cbReader: TComboBox
    Left = 112
    Top = 8
    Width = 169
    Height = 22
    ItemHeight = 14
    TabOrder = 0
    OnChange = cbReaderChange
  end
  object bInit: TButton
    Left = 176
    Top = 64
    Width = 107
    Height = 25
    Caption = '&Initialize'
    TabOrder = 1
    OnClick = bInitClick
  end
  object bStart: TButton
    Left = 8
    Top = 64
    Width = 105
    Height = 25
    Caption = 'Start Polling'
    TabOrder = 2
    OnClick = bStartClick
  end
  object bEnd: TButton
    Left = 8
    Top = 104
    Width = 105
    Height = 25
    Caption = '&End Polling'
    TabOrder = 3
    OnClick = bEndClick
  end
  object bReset: TButton
    Left = 176
    Top = 104
    Width = 105
    Height = 25
    Caption = '&Reset'
    TabOrder = 4
    OnClick = bResetClick
  end
  object bQuit: TButton
    Left = 176
    Top = 144
    Width = 105
    Height = 25
    Caption = '&Quit'
    TabOrder = 5
    OnClick = bQuitClick
  end
  object sbMsg: TStatusBar
    Left = 0
    Top = 185
    Width = 298
    Height = 24
    Panels = <>
  end
  object PollTimer: TTimer
    Enabled = False
    Interval = 100
    OnTimer = PollTimerTimer
    Left = 8
    Top = 152
  end
end
