object MainPICCProg: TMainPICCProg
  Left = 192
  Top = 107
  Width = 667
  Height = 445
  Caption = 'Programming Other PICC Cards'
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
    Top = 20
    Width = 68
    Height = 13
    Caption = 'Select Reader'
  end
  object cbReader: TComboBox
    Left = 88
    Top = 16
    Width = 209
    Height = 21
    Style = csDropDownList
    ItemHeight = 13
    TabOrder = 0
  end
  object bInit: TButton
    Left = 184
    Top = 48
    Width = 113
    Height = 25
    Caption = '&Initialize'
    TabOrder = 1
    OnClick = bInitClick
  end
  object bConnect: TButton
    Left = 184
    Top = 80
    Width = 113
    Height = 25
    Caption = '&Connect'
    TabOrder = 2
    OnClick = bConnectClick
  end
  object mMsg: TRichEdit
    Left = 312
    Top = 16
    Width = 337
    Height = 345
    ScrollBars = ssVertical
    TabOrder = 3
  end
  object bClear: TButton
    Left = 312
    Top = 376
    Width = 105
    Height = 25
    Caption = 'C&lear'
    TabOrder = 4
    OnClick = bClearClick
  end
  object bReset: TButton
    Left = 432
    Top = 376
    Width = 105
    Height = 25
    Caption = '&Reset'
    TabOrder = 5
    OnClick = bResetClick
  end
  object bQuit: TButton
    Left = 544
    Top = 376
    Width = 105
    Height = 25
    Caption = '&Quit'
    TabOrder = 6
    OnClick = bQuitClick
  end
  object gbGetData: TGroupBox
    Left = 8
    Top = 112
    Width = 289
    Height = 57
    Caption = 'Get Data Function'
    TabOrder = 7
    object cbIso14443A: TCheckBox
      Left = 8
      Top = 24
      Width = 121
      Height = 17
      Caption = 'ISO 14443 A Card'
      TabOrder = 0
    end
    object bGetData: TButton
      Left = 160
      Top = 16
      Width = 113
      Height = 25
      Caption = '&Get Data'
      TabOrder = 1
      OnClick = bGetDataClick
    end
  end
  object gbSendApdu: TGroupBox
    Left = 8
    Top = 176
    Width = 289
    Height = 233
    Caption = 'Send Card Command'
    TabOrder = 8
    object Label2: TLabel
      Left = 16
      Top = 24
      Width = 20
      Height = 13
      Caption = 'CLA'
    end
    object Label3: TLabel
      Left = 48
      Top = 24
      Width = 18
      Height = 13
      Caption = 'INS'
    end
    object Label4: TLabel
      Left = 80
      Top = 24
      Width = 13
      Height = 13
      Caption = 'P1'
    end
    object Label5: TLabel
      Left = 112
      Top = 24
      Width = 13
      Height = 13
      Caption = 'P2'
    end
    object Label6: TLabel
      Left = 144
      Top = 24
      Width = 12
      Height = 13
      Caption = 'Lc'
    end
    object Label7: TLabel
      Left = 16
      Top = 72
      Width = 35
      Height = 13
      Caption = 'Data In'
    end
    object Label8: TLabel
      Left = 176
      Top = 24
      Width = 12
      Height = 13
      Caption = 'Le'
    end
    object tCLA: TEdit
      Left = 16
      Top = 40
      Width = 33
      Height = 21
      MaxLength = 2
      TabOrder = 0
      OnKeyPress = tCLAKeyPress
      OnKeyUp = tCLAKeyUp
    end
    object tINS: TEdit
      Left = 48
      Top = 40
      Width = 33
      Height = 21
      MaxLength = 2
      TabOrder = 1
      OnKeyPress = tCLAKeyPress
    end
    object tP1: TEdit
      Left = 80
      Top = 40
      Width = 33
      Height = 21
      MaxLength = 2
      TabOrder = 2
      OnKeyPress = tCLAKeyPress
    end
    object tP2: TEdit
      Left = 112
      Top = 40
      Width = 33
      Height = 21
      MaxLength = 2
      TabOrder = 3
      OnKeyPress = tCLAKeyPress
    end
    object tLc: TEdit
      Left = 144
      Top = 40
      Width = 33
      Height = 21
      MaxLength = 2
      TabOrder = 4
      OnKeyPress = tCLAKeyPress
    end
    object tLe: TEdit
      Left = 176
      Top = 40
      Width = 33
      Height = 21
      MaxLength = 2
      TabOrder = 5
      OnKeyPress = tCLAKeyPress
    end
    object bSend: TButton
      Left = 160
      Top = 192
      Width = 113
      Height = 25
      Caption = '&Send Card Command'
      TabOrder = 6
      OnClick = bSendClick
    end
    object tData: TMemo
      Left = 16
      Top = 88
      Width = 257
      Height = 89
      TabOrder = 7
      OnKeyPress = tDataKeyPress
    end
  end
end
