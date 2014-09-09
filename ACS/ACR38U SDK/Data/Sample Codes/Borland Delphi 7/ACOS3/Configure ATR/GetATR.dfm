object MainConfigureATR: TMainConfigureATR
  Left = 271
  Top = 135
  Width = 482
  Height = 555
  Caption = 'Configure ATR'
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
  object Label2: TLabel
    Left = 8
    Top = 208
    Width = 96
    Height = 14
    Caption = 'Card Baud Rate'
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object Label3: TLabel
    Left = 8
    Top = 256
    Width = 135
    Height = 14
    Caption = 'No. of Historical Bytes'
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object Label4: TLabel
    Left = 8
    Top = 304
    Width = 94
    Height = 14
    Caption = 'Historical Bytes'
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object Label5: TLabel
    Left = 152
    Top = 256
    Width = 15
    Height = 14
    Alignment = taCenter
    Caption = 'T0'
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object Label6: TLabel
    Left = 152
    Top = 208
    Width = 16
    Height = 14
    Alignment = taCenter
    Caption = 'TA'
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object cbReader: TComboBox
    Left = 8
    Top = 32
    Width = 137
    Height = 22
    ItemHeight = 14
    TabOrder = 0
    OnChange = cbReaderChange
  end
  object bConnect: TButton
    Left = 24
    Top = 120
    Width = 113
    Height = 25
    Caption = '&Connect'
    TabOrder = 1
    OnClick = bConnectClick
  end
  object bATR: TButton
    Left = 24
    Top = 168
    Width = 115
    Height = 25
    Caption = '&Get ATR'
    TabOrder = 2
    OnClick = bATRClick
  end
  object bReset: TButton
    Left = 24
    Top = 456
    Width = 115
    Height = 25
    Caption = '&Reset'
    TabOrder = 3
    OnClick = bResetClick
  end
  object bQuit: TButton
    Left = 24
    Top = 488
    Width = 115
    Height = 25
    Caption = '&Quit'
    TabOrder = 4
    OnClick = bQuitClick
  end
  object bInit: TButton
    Left = 24
    Top = 72
    Width = 113
    Height = 25
    Caption = '&Initialize'
    TabOrder = 5
    OnClick = bInitClick
  end
  object mMsg: TRichEdit
    Left = 184
    Top = 8
    Width = 281
    Height = 505
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    ScrollBars = ssVertical
    TabOrder = 6
  end
  object cbo_baud: TComboBox
    Left = 8
    Top = 224
    Width = 137
    Height = 22
    Enabled = False
    ItemHeight = 14
    TabOrder = 7
    OnChange = cbo_baudChange
    OnClick = cbo_baudClick
    Items.Strings = (
      '9600'
      '14400'
      '28800'
      '57600'
      '115200')
  end
  object cbo_byte: TComboBox
    Left = 8
    Top = 272
    Width = 137
    Height = 22
    Enabled = False
    ItemHeight = 14
    TabOrder = 8
    OnChange = cbo_byteChange
    Items.Strings = (
      '00'
      '01'
      '02'
      '03'
      '04'
      '05'
      '06'
      '07'
      '08'
      '09'
      '0A'
      '0B'
      '0C'
      '0D'
      '0E'
      '0F'
      'FF')
  end
  object Edit1: TEdit
    Left = 152
    Top = 272
    Width = 22
    Height = 22
    Enabled = False
    TabOrder = 9
    OnChange = Edit1Change
  end
  object Edit2: TEdit
    Left = 153
    Top = 224
    Width = 22
    Height = 22
    Enabled = False
    TabOrder = 10
    OnChange = Edit2Change
  end
  object Edit3: TEdit
    Left = 8
    Top = 320
    Width = 22
    Height = 22
    Enabled = False
    MaxLength = 2
    TabOrder = 11
    OnChange = Edit3Change
  end
  object Edit4: TEdit
    Left = 32
    Top = 320
    Width = 22
    Height = 22
    Enabled = False
    MaxLength = 2
    TabOrder = 12
    OnChange = Edit4Change
  end
  object Edit5: TEdit
    Left = 56
    Top = 320
    Width = 22
    Height = 22
    Enabled = False
    MaxLength = 2
    TabOrder = 13
    OnChange = Edit5Change
  end
  object Edit6: TEdit
    Left = 80
    Top = 320
    Width = 22
    Height = 22
    Enabled = False
    MaxLength = 2
    TabOrder = 14
    OnChange = Edit6Change
  end
  object Edit7: TEdit
    Left = 104
    Top = 320
    Width = 22
    Height = 22
    Enabled = False
    MaxLength = 2
    TabOrder = 15
    OnChange = Edit7Change
  end
  object Edit8: TEdit
    Left = 8
    Top = 344
    Width = 22
    Height = 22
    Enabled = False
    MaxLength = 2
    TabOrder = 16
    OnChange = Edit8Change
  end
  object Edit9: TEdit
    Left = 32
    Top = 344
    Width = 22
    Height = 22
    Enabled = False
    MaxLength = 2
    TabOrder = 17
    OnChange = Edit9Change
  end
  object Edit10: TEdit
    Left = 56
    Top = 344
    Width = 22
    Height = 22
    Enabled = False
    MaxLength = 2
    TabOrder = 18
    OnChange = Edit10Change
  end
  object Edit11: TEdit
    Left = 80
    Top = 344
    Width = 22
    Height = 22
    Enabled = False
    MaxLength = 2
    TabOrder = 19
    OnChange = Edit11Change
  end
  object Edit12: TEdit
    Left = 104
    Top = 344
    Width = 22
    Height = 22
    Enabled = False
    MaxLength = 2
    TabOrder = 20
    OnChange = Edit12Change
  end
  object Edit13: TEdit
    Left = 8
    Top = 368
    Width = 22
    Height = 22
    Enabled = False
    MaxLength = 2
    TabOrder = 21
    OnChange = Edit13Change
  end
  object Edit14: TEdit
    Left = 32
    Top = 368
    Width = 22
    Height = 22
    Enabled = False
    MaxLength = 2
    TabOrder = 22
    OnChange = Edit14Change
  end
  object Edit15: TEdit
    Left = 56
    Top = 368
    Width = 22
    Height = 22
    Enabled = False
    MaxLength = 2
    TabOrder = 23
    OnChange = Edit15Change
  end
  object Edit16: TEdit
    Left = 80
    Top = 368
    Width = 22
    Height = 22
    Enabled = False
    MaxLength = 2
    TabOrder = 24
    OnChange = Edit16Change
  end
  object Edit17: TEdit
    Left = 104
    Top = 368
    Width = 22
    Height = 22
    Enabled = False
    MaxLength = 2
    TabOrder = 25
    OnChange = Edit17Change
  end
  object bUpdate: TButton
    Left = 24
    Top = 400
    Width = 113
    Height = 25
    Caption = '&Update ATR'
    Enabled = False
    TabOrder = 26
    OnClick = bUpdateClick
  end
end
