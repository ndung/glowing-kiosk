object MainReadWrite: TMainReadWrite
  Left = 340
  Top = 196
  Width = 649
  Height = 452
  Caption = 'Reading and Writing on ACOS3'
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
  object Label2: TLabel
    Left = 8
    Top = 320
    Width = 125
    Height = 14
    Caption = 'String Value of Data'
    Font.Charset = ANSI_CHARSET
    Font.Color = clNavy
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object cbReader: TComboBox
    Left = 112
    Top = 16
    Width = 161
    Height = 22
    ItemHeight = 14
    TabOrder = 0
    OnChange = cbReaderChange
  end
  object mMsg: TRichEdit
    Left = 288
    Top = 8
    Width = 345
    Height = 409
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    ScrollBars = ssVertical
    TabOrder = 1
  end
  object gbFunction: TGroupBox
    Left = 8
    Top = 176
    Width = 265
    Height = 129
    TabOrder = 2
    object rgUserFile: TRadioGroup
      Left = 0
      Top = 0
      Width = 97
      Height = 129
      Caption = 'User File'
      Items.Strings = (
        'AA 11'
        'BB 22'
        'CC 33')
      TabOrder = 0
      OnClick = rgUserFileClick
    end
    object bRead: TButton
      Left = 136
      Top = 32
      Width = 105
      Height = 25
      Caption = '&Read'
      TabOrder = 1
      OnClick = bReadClick
    end
    object bWrite: TButton
      Left = 136
      Top = 80
      Width = 105
      Height = 25
      Caption = '&Write'
      TabOrder = 2
      OnClick = bWriteClick
    end
  end
  object bInit: TButton
    Left = 168
    Top = 56
    Width = 107
    Height = 25
    Caption = '&Initialize'
    TabOrder = 3
    OnClick = bInitClick
  end
  object bConnect: TButton
    Left = 168
    Top = 96
    Width = 105
    Height = 25
    Caption = '&Connect'
    TabOrder = 4
    OnClick = bConnectClick
  end
  object bReset: TButton
    Left = 32
    Top = 384
    Width = 107
    Height = 25
    Caption = 'R&eset'
    TabOrder = 5
    OnClick = bResetClick
  end
  object bQuit: TButton
    Left = 168
    Top = 384
    Width = 105
    Height = 25
    Caption = '&Quit'
    TabOrder = 6
    OnClick = bQuitClick
  end
  object tData: TEdit
    Left = 8
    Top = 344
    Width = 265
    Height = 22
    TabOrder = 7
  end
  object bFormat: TButton
    Left = 168
    Top = 136
    Width = 105
    Height = 25
    Caption = '&Format Card'
    TabOrder = 8
    OnClick = bFormatClick
  end
end
