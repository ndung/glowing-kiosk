object MainAccount: TMainAccount
  Left = 265
  Top = 243
  Width = 642
  Height = 531
  Caption = 'Using ACOS3 Account Files'
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
    Width = 345
    Height = 441
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
    Left = 112
    Top = 16
    Width = 153
    Height = 22
    ItemHeight = 14
    TabOrder = 1
    OnChange = cbReaderChange
  end
  object bInit: TButton
    Left = 144
    Top = 56
    Width = 121
    Height = 25
    Caption = '&Initialize'
    TabOrder = 2
    OnClick = bInitClick
  end
  object bConnect: TButton
    Left = 144
    Top = 96
    Width = 121
    Height = 25
    Caption = '&Connect'
    TabOrder = 3
    OnClick = bConnectClick
  end
  object bFormat: TButton
    Left = 144
    Top = 136
    Width = 121
    Height = 25
    Caption = '&Format Card'
    TabOrder = 4
    OnClick = bFormatClick
  end
  object gbFunctions: TGroupBox
    Left = 8
    Top = 312
    Width = 257
    Height = 177
    Caption = 'Account Functions'
    Font.Charset = ANSI_CHARSET
    Font.Color = clNavy
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 5
    object Label6: TLabel
      Left = 72
      Top = 24
      Width = 44
      Height = 14
      Caption = 'Amount'
    end
    object tAmount: TEdit
      Left = 144
      Top = 24
      Width = 100
      Height = 22
      MaxLength = 8
      TabOrder = 0
      OnKeyPress = tAmountKeyPress
    end
    object bCredit: TButton
      Left = 16
      Top = 104
      Width = 100
      Height = 25
      Caption = 'Cr&edit'
      TabOrder = 1
      OnClick = bCreditClick
    end
    object bDebit: TButton
      Left = 16
      Top = 136
      Width = 100
      Height = 25
      Caption = '&Debit'
      TabOrder = 2
      OnClick = bDebitClick
    end
    object bInquire: TButton
      Left = 144
      Top = 104
      Width = 100
      Height = 25
      Caption = 'I&nquire Balance'
      TabOrder = 3
      OnClick = bInquireClick
    end
    object bRevDebit: TButton
      Left = 144
      Top = 136
      Width = 100
      Height = 25
      Caption = 'Re&voke Debit'
      TabOrder = 4
      OnClick = bRevDebitClick
    end
    object chk_dbc: TCheckBox
      Left = 16
      Top = 72
      Width = 169
      Height = 17
      Caption = 'Require Debit Certificate'
      TabOrder = 5
    end
  end
  object rgOption: TRadioGroup
    Left = 8
    Top = 88
    Width = 121
    Height = 73
    Caption = 'Security Option'
    Items.Strings = (
      'DES'
      '3-DES')
    TabOrder = 6
    OnClick = rgOptionClick
  end
  object gbKeys: TGroupBox
    Left = 8
    Top = 176
    Width = 257
    Height = 129
    Caption = 'Security Keys'
    Font.Charset = ANSI_CHARSET
    Font.Color = clNavy
    Font.Height = -12
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 7
    object Label2: TLabel
      Left = 16
      Top = 24
      Width = 32
      Height = 14
      Caption = 'Credit'
    end
    object Label3: TLabel
      Left = 16
      Top = 48
      Width = 29
      Height = 14
      Caption = 'Debit'
    end
    object Label4: TLabel
      Left = 16
      Top = 72
      Width = 35
      Height = 14
      Caption = 'Certify'
    end
    object Label5: TLabel
      Left = 16
      Top = 96
      Width = 73
      Height = 14
      Caption = 'Revoke Debit'
    end
    object tCredit: TEdit
      Left = 104
      Top = 24
      Width = 137
      Height = 22
      MaxLength = 16
      TabOrder = 0
      OnKeyUp = tCreditKeyUp
    end
    object tDebit: TEdit
      Left = 104
      Top = 48
      Width = 137
      Height = 22
      MaxLength = 16
      TabOrder = 1
      OnKeyUp = tDebitKeyUp
    end
    object tCertify: TEdit
      Left = 104
      Top = 72
      Width = 137
      Height = 22
      MaxLength = 16
      TabOrder = 2
      OnKeyUp = tCertifyKeyUp
    end
    object tRevDebit: TEdit
      Left = 104
      Top = 96
      Width = 137
      Height = 22
      MaxLength = 16
      TabOrder = 3
    end
  end
  object bQuit: TButton
    Left = 480
    Top = 464
    Width = 121
    Height = 25
    Caption = '&Quit'
    TabOrder = 8
    OnClick = bQuitClick
  end
  object bReset: TButton
    Left = 336
    Top = 464
    Width = 121
    Height = 25
    Caption = '&Reset'
    TabOrder = 9
    OnClick = bResetClick
  end
end
