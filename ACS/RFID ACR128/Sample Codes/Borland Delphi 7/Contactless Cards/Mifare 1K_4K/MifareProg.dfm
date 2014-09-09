object MainMifareProg: TMainMifareProg
  Left = 372
  Top = 66
  Width = 685
  Height = 683
  Caption = 'Mifare Card Programming'
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
    Left = 80
    Top = 16
    Width = 185
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
    Top = 80
    Width = 113
    Height = 25
    Caption = '&Connect'
    TabOrder = 2
    OnClick = bConnectClick
  end
  object mMsg: TRichEdit
    Left = 280
    Top = 200
    Width = 385
    Height = 401
    TabOrder = 3
  end
  object bClear: TButton
    Left = 304
    Top = 616
    Width = 105
    Height = 25
    Caption = 'C&lear'
    TabOrder = 4
    OnClick = bClearClick
  end
  object bReset: TButton
    Left = 424
    Top = 616
    Width = 105
    Height = 25
    Caption = '&Reset'
    TabOrder = 5
    OnClick = bResetClick
  end
  object bQuit: TButton
    Left = 544
    Top = 616
    Width = 105
    Height = 25
    Caption = '&Quit'
    TabOrder = 6
    OnClick = bQuitClick
  end
  object gbLoadKeys: TGroupBox
    Left = 8
    Top = 112
    Width = 257
    Height = 145
    Caption = 'Store Authentications Keys to Device'
    TabOrder = 7
    object Label2: TLabel
      Left = 8
      Top = 52
      Width = 66
      Height = 13
      Caption = 'Key Store No.'
    end
    object Label3: TLabel
      Left = 8
      Top = 75
      Width = 75
      Height = 13
      Caption = 'Key Value Input'
    end
    object rbNonVolMem: TRadioButton
      Left = 8
      Top = 24
      Width = 121
      Height = 17
      Caption = 'Non-Volatile Memory'
      TabOrder = 0
      OnClick = rbNonVolMemClick
    end
    object rbVolMem: TRadioButton
      Left = 136
      Top = 24
      Width = 113
      Height = 17
      Caption = 'Volatile Memory'
      TabOrder = 1
      OnClick = rbVolMemClick
    end
    object tMemAdd: TEdit
      Left = 96
      Top = 48
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 2
      OnKeyPress = tMemAddKeyPress
    end
    object tKey1: TEdit
      Left = 96
      Top = 72
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 3
      OnKeyPress = tMemAddKeyPress
      OnKeyUp = tKey1KeyUp
    end
    object tKey2: TEdit
      Left = 120
      Top = 72
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 4
      OnKeyPress = tMemAddKeyPress
      OnKeyUp = tKey2KeyUp
    end
    object tKey3: TEdit
      Left = 144
      Top = 72
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 5
      OnKeyPress = tMemAddKeyPress
      OnKeyUp = tKey3KeyUp
    end
    object tKey4: TEdit
      Left = 168
      Top = 72
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 6
      OnKeyPress = tMemAddKeyPress
      OnKeyUp = tKey4KeyUp
    end
    object tKey5: TEdit
      Left = 192
      Top = 72
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 7
      OnKeyPress = tMemAddKeyPress
      OnKeyUp = tKey5KeyUp
    end
    object tKey6: TEdit
      Left = 216
      Top = 72
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 8
      OnKeyPress = tMemAddKeyPress
    end
    object bLoadKey: TButton
      Left = 136
      Top = 104
      Width = 105
      Height = 25
      Caption = 'Load &Key'
      TabOrder = 9
      OnClick = bLoadKeyClick
    end
  end
  object gbAuth: TGroupBox
    Left = 8
    Top = 264
    Width = 257
    Height = 225
    Caption = 'Authentication Function'
    TabOrder = 8
    object Label4: TLabel
      Left = 8
      Top = 107
      Width = 73
      Height = 13
      Caption = 'Block No (Dec)'
    end
    object Label5: TLabel
      Left = 8
      Top = 156
      Width = 75
      Height = 13
      Caption = 'Key Value Input'
    end
    object Label6: TLabel
      Left = 8
      Top = 131
      Width = 66
      Height = 13
      Caption = 'Key Store No.'
    end
    object rgSource: TRadioGroup
      Left = 8
      Top = 16
      Width = 129
      Height = 81
      Caption = 'Key Source'
      Items.Strings = (
        'Manual Input'
        'Volatile Memory'
        'Non-volatile Memory')
      TabOrder = 0
      OnClick = rgSourceClick
    end
    object rgKType: TRadioGroup
      Left = 144
      Top = 16
      Width = 105
      Height = 81
      Caption = 'Key Type'
      Items.Strings = (
        'Key A'
        'Key B')
      TabOrder = 1
    end
    object tBlkNo: TEdit
      Left = 96
      Top = 104
      Width = 25
      Height = 21
      MaxLength = 3
      TabOrder = 2
      OnKeyPress = tBlkNoKeyPress
    end
    object tKeyIn1: TEdit
      Left = 96
      Top = 152
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 3
      OnKeyPress = tMemAddKeyPress
      OnKeyUp = tKeyIn1KeyUp
    end
    object tKeyIn2: TEdit
      Left = 120
      Top = 152
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 4
      OnKeyPress = tMemAddKeyPress
      OnKeyUp = tKeyIn2KeyUp
    end
    object tKeyIn3: TEdit
      Left = 144
      Top = 152
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 5
      OnKeyPress = tMemAddKeyPress
      OnKeyUp = tKeyIn3KeyUp
    end
    object tKeyIn4: TEdit
      Left = 168
      Top = 152
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 6
      OnKeyPress = tMemAddKeyPress
      OnKeyUp = tKeyIn4KeyUp
    end
    object tKeyIn5: TEdit
      Left = 192
      Top = 152
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 7
      OnKeyPress = tMemAddKeyPress
      OnKeyUp = tKeyIn5KeyUp
    end
    object tKeyIn6: TEdit
      Left = 216
      Top = 152
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 8
      OnKeyPress = tMemAddKeyPress
    end
    object bAuth: TButton
      Left = 136
      Top = 184
      Width = 105
      Height = 25
      Caption = '&Authenticate'
      TabOrder = 9
      OnClick = bAuthClick
    end
    object tKeyAdd: TEdit
      Left = 96
      Top = 128
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 10
      OnKeyPress = tMemAddKeyPress
    end
  end
  object gbBinOps: TGroupBox
    Left = 8
    Top = 496
    Width = 257
    Height = 145
    Caption = 'Binary Block Functions'
    TabOrder = 9
    object Label7: TLabel
      Left = 8
      Top = 27
      Width = 81
      Height = 13
      Caption = 'Start Block (Dec)'
    end
    object Label8: TLabel
      Left = 144
      Top = 27
      Width = 62
      Height = 13
      Caption = 'Length (Dec)'
    end
    object Label9: TLabel
      Left = 8
      Top = 56
      Width = 53
      Height = 13
      Caption = 'Data (Text)'
    end
    object bBinRead: TButton
      Left = 16
      Top = 104
      Width = 105
      Height = 25
      Caption = 'Read &Block'
      TabOrder = 0
      OnClick = bBinReadClick
    end
    object bBinUpd: TButton
      Left = 136
      Top = 104
      Width = 105
      Height = 25
      Caption = '&Update Block'
      TabOrder = 1
      OnClick = bBinUpdClick
    end
    object tBinBlk: TEdit
      Left = 96
      Top = 24
      Width = 33
      Height = 21
      MaxLength = 3
      TabOrder = 2
      OnKeyPress = tBlkNoKeyPress
    end
    object tBinLen: TEdit
      Left = 208
      Top = 24
      Width = 33
      Height = 21
      MaxLength = 2
      TabOrder = 3
      OnKeyPress = tBlkNoKeyPress
    end
    object tBinData: TEdit
      Left = 8
      Top = 72
      Width = 233
      Height = 21
      TabOrder = 4
    end
  end
  object gbValBlk: TGroupBox
    Left = 280
    Top = 8
    Width = 385
    Height = 177
    Caption = 'Value Block Functions'
    TabOrder = 10
    object Label10: TLabel
      Left = 16
      Top = 27
      Width = 66
      Height = 13
      Caption = 'Value Amount'
    end
    object Label11: TLabel
      Left = 16
      Top = 59
      Width = 47
      Height = 13
      Caption = 'Block No.'
    end
    object Label12: TLabel
      Left = 16
      Top = 91
      Width = 64
      Height = 13
      Caption = 'Source Block'
    end
    object Label13: TLabel
      Left = 16
      Top = 123
      Width = 61
      Height = 13
      Caption = 'Target Block'
    end
    object tValAmt: TEdit
      Left = 96
      Top = 24
      Width = 121
      Height = 21
      MaxLength = 10
      TabOrder = 0
      OnKeyPress = tBlkNoKeyPress
    end
    object bValStor: TButton
      Left = 248
      Top = 16
      Width = 123
      Height = 25
      Caption = '&Store Value'
      TabOrder = 1
      OnClick = bValStorClick
    end
    object bValInc: TButton
      Left = 248
      Top = 48
      Width = 123
      Height = 25
      Caption = 'I&ncrement'
      TabOrder = 2
      OnClick = bValIncClick
    end
    object bValDec: TButton
      Left = 248
      Top = 80
      Width = 123
      Height = 25
      Caption = '&Decrement'
      TabOrder = 3
      OnClick = bValDecClick
    end
    object bValRead: TButton
      Left = 248
      Top = 112
      Width = 123
      Height = 25
      Caption = 'R&ead Value'
      TabOrder = 4
      OnClick = bValReadClick
    end
    object BValRes: TButton
      Left = 248
      Top = 144
      Width = 123
      Height = 25
      Caption = 'Res&tore Value'
      TabOrder = 5
      OnClick = BValResClick
    end
    object tValBlk: TEdit
      Left = 96
      Top = 56
      Width = 33
      Height = 21
      MaxLength = 3
      TabOrder = 6
      OnKeyPress = tBlkNoKeyPress
    end
    object tValSrc: TEdit
      Left = 96
      Top = 88
      Width = 33
      Height = 21
      MaxLength = 3
      TabOrder = 7
      OnKeyPress = tBlkNoKeyPress
    end
    object tValTar: TEdit
      Left = 96
      Top = 120
      Width = 33
      Height = 21
      MaxLength = 3
      TabOrder = 8
      OnKeyPress = tBlkNoKeyPress
    end
  end
end
