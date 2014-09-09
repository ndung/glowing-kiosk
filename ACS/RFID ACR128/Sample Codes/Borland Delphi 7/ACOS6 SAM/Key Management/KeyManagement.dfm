object frmMain: TfrmMain
  Left = 208
  Top = 163
  Width = 740
  Height = 579
  Caption = 'Key Management'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  Icon.Data = {
    0000010001002020100000000000E80200001600000028000000200000004000
    0000010004000000000080020000000000000000000000000000000000000000
    0000000080000080000000808000800000008000800080800000C0C0C0008080
    80000000FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF000000
    0000000000000000000000000000000000000000000000000000000000000000
    0000000000000000000000000000000000000000000000000000000000000000
    0000000000000000000000000000000000000000000000000000000000000000
    0000000000000000000000000000000000000999999999999990000000000000
    0099999999999999999999000000000099999900000000000099999900000009
    9990000000000000000009999000009990000999909999099990000999000099
    0000999999999999999900009900099000009900999009990099000009900990
    0000990099900000009900000990099000009999999000009990000009900990
    0000009999900099900000000990099000009900999009990099000009900099
    0000999999999999999900009900009990000999909990099990000999000009
    9990000000000000000009999000000099999900000000000099999900000000
    0099999999999999999999000000000000000999999999999990000000000000
    0000000000000000000000000000000000000000000000000000000000000000
    0000000000000000000000000000000000000000000000000000000000000000
    0000000000000000000000000000000000000000000000000000000000000000
    000000000000000000000000000000000000000000000000000000000000FFFF
    FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF8001FFFC00
    003FF03FFC0FE1FFFF87C78421E3CF0000F39F318CF99F31FCF99F01F1F99FC1
    C7F99F318CF9CF0000F3C78461E3E1FFFF87F03FFC0FFC00003FFF8001FFFFFF
    FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF}
  OldCreateOrder = False
  OnActivate = FormActivate
  PixelsPerInch = 96
  TextHeight = 13
  object tabMain: TTabbedNotebook
    Left = 8
    Top = 5
    Width = 377
    Height = 529
    TabFont.Charset = DEFAULT_CHARSET
    TabFont.Color = clBtnText
    TabFont.Height = -11
    TabFont.Name = 'MS Sans Serif'
    TabFont.Style = [fsBold]
    TabOrder = 0
    object TTabPage
      Left = 4
      Top = 24
      Caption = 'SAM Initialization'
      object grpSAM: TGroupBox
        Left = 8
        Top = 8
        Width = 353
        Height = 489
        Caption = 'SAM Master Keys'
        TabOrder = 0
        object lblGlobalPin: TLabel
          Left = 16
          Top = 96
          Width = 89
          Height = 13
          Caption = 'SAM GLOBAL PIN'
        end
        object lblSAMIC: TLabel
          Left = 16
          Top = 167
          Width = 75
          Height = 13
          Caption = 'Issuer Code (IC)'
        end
        object lblSAMKc: TLabel
          Left = 16
          Top = 200
          Width = 65
          Height = 13
          Caption = 'Card Key (Kc)'
        end
        object lblSAMKt: TLabel
          Left = 16
          Top = 232
          Width = 80
          Height = 13
          Caption = 'Terminal Key (Kt)'
        end
        object lblSAMKd: TLabel
          Left = 16
          Top = 264
          Width = 68
          Height = 13
          Caption = 'Debit Key (Kd)'
        end
        object lblSAMKcr: TLabel
          Left = 16
          Top = 296
          Width = 73
          Height = 13
          Caption = 'Credit Key (Kcr)'
        end
        object lblSAMKcf: TLabel
          Left = 16
          Top = 328
          Width = 75
          Height = 13
          Caption = 'Certify Key (Kcf)'
        end
        object lblSAMKrd: TLabel
          Left = 16
          Top = 360
          Width = 87
          Height = 26
          Caption = 'Revoke Debit Key (Krd)'
          WordWrap = True
        end
        object Label1: TLabel
          Left = 16
          Top = 136
          Width = 303
          Height = 13
          Caption = 
            '------------------------------ SAM Master Keys -----------------' +
            '------------------------'
        end
        object cmbSamReader: TComboBox
          Left = 8
          Top = 24
          Width = 145
          Height = 21
          ItemHeight = 13
          TabOrder = 1
        end
        object btnSAMList: TButton
          Left = 168
          Top = 24
          Width = 97
          Height = 25
          Caption = 'List Readers'
          TabOrder = 0
          OnClick = btnSAMListClick
        end
        object btnSAMConnect: TButton
          Left = 168
          Top = 56
          Width = 97
          Height = 25
          Caption = 'Connect'
          TabOrder = 2
          OnClick = btnSAMConnectClick
        end
        object tSAMGPIN: TEdit
          Left = 112
          Top = 96
          Width = 129
          Height = 21
          BevelInner = bvNone
          BevelOuter = bvNone
          CharCase = ecUpperCase
          Enabled = False
          MaxLength = 16
          TabOrder = 3
          OnKeyPress = tSAMGPINKeyPress
        end
        object tSAMIC: TEdit
          Left = 112
          Top = 164
          Width = 233
          Height = 21
          CharCase = ecUpperCase
          Enabled = False
          MaxLength = 32
          TabOrder = 4
          OnKeyPress = tSAMICKeyPress
        end
        object tSAMKc: TEdit
          Left = 112
          Top = 195
          Width = 233
          Height = 21
          CharCase = ecUpperCase
          Enabled = False
          MaxLength = 32
          TabOrder = 5
          OnKeyPress = tSAMKcKeyPress
        end
        object tSAMKt: TEdit
          Left = 112
          Top = 227
          Width = 233
          Height = 21
          CharCase = ecUpperCase
          Enabled = False
          MaxLength = 32
          TabOrder = 6
          OnKeyPress = tSAMKtKeyPress
        end
        object tSAMKd: TEdit
          Left = 112
          Top = 259
          Width = 233
          Height = 21
          CharCase = ecUpperCase
          Enabled = False
          MaxLength = 32
          TabOrder = 7
          OnKeyPress = tSAMKdKeyPress
        end
        object tSAMKcr: TEdit
          Left = 112
          Top = 290
          Width = 233
          Height = 21
          CharCase = ecUpperCase
          Enabled = False
          MaxLength = 32
          TabOrder = 8
          OnKeyPress = tSAMKcrKeyPress
        end
        object tSAMKcf: TEdit
          Left = 112
          Top = 323
          Width = 233
          Height = 21
          CharCase = ecUpperCase
          Enabled = False
          MaxLength = 32
          TabOrder = 9
          OnKeyPress = tSAMKcfKeyPress
        end
        object tSAMKrd: TEdit
          Left = 112
          Top = 357
          Width = 233
          Height = 21
          CharCase = ecUpperCase
          Enabled = False
          MaxLength = 32
          TabOrder = 10
          OnKeyPress = tSAMKrdKeyPress
        end
        object btnInitSAM: TButton
          Left = 208
          Top = 400
          Width = 97
          Height = 33
          Caption = 'Initialize SAM'
          Enabled = False
          TabOrder = 11
          OnClick = btnInitSAMClick
        end
      end
    end
    object TTabPage
      Left = 4
      Top = 24
      Caption = 'ACOS Card Initialization'
      object grpACOS: TGroupBox
        Left = 8
        Top = 8
        Width = 353
        Height = 481
        Caption = 'Generated Keys'
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        TabOrder = 0
        object lblCardSN: TLabel
          Left = 7
          Top = 86
          Width = 96
          Height = 14
          Caption = 'Card Serial Number '
        end
        object lblACOSIC: TLabel
          Left = 7
          Top = 168
          Width = 78
          Height = 14
          Caption = 'Issuer Code (IC)'
        end
        object lblACOSPIN: TLabel
          Left = 7
          Top = 117
          Width = 74
          Height = 14
          Caption = 'ACOS Card PIN'
        end
        object lblACOSKc: TLabel
          Left = 7
          Top = 205
          Width = 69
          Height = 14
          Caption = 'Card Key (Kc)'
        end
        object lblACOSKt: TLabel
          Left = 7
          Top = 240
          Width = 83
          Height = 14
          Caption = 'Terminal Key (Kt)'
        end
        object lblACOSKd: TLabel
          Left = 6
          Top = 278
          Width = 70
          Height = 14
          Caption = 'Debit Key (Kd)'
        end
        object lblACOSKcr: TLabel
          Left = 6
          Top = 315
          Width = 78
          Height = 14
          Caption = 'Credit Key (Kcr)'
        end
        object lblACOSKcf: TLabel
          Left = 6
          Top = 348
          Width = 82
          Height = 14
          Caption = 'Certify Key (Kcf)'
        end
        object lblACOSKrd: TLabel
          Left = 6
          Top = 384
          Width = 85
          Height = 28
          Caption = 'Revoke Debit Key (Krd)'
          WordWrap = True
        end
        object cmbACOSReader: TComboBox
          Left = 8
          Top = 24
          Width = 145
          Height = 22
          ItemHeight = 14
          TabOrder = 9
        end
        object btnACOSList: TButton
          Left = 182
          Top = 24
          Width = 97
          Height = 25
          Caption = 'List Readers'
          TabOrder = 8
          OnClick = btnACOSListClick
        end
        object btnACOSConnect: TButton
          Left = 182
          Top = 56
          Width = 97
          Height = 25
          Caption = 'Connect'
          TabOrder = 11
          OnClick = btnACOSConnectClick
        end
        object tACOSCardSN: TEdit
          Left = 103
          Top = 86
          Width = 234
          Height = 22
          TabStop = False
          Color = clScrollBar
          ReadOnly = True
          TabOrder = 0
        end
        object tACOSIC: TEdit
          Left = 103
          Top = 171
          Width = 234
          Height = 22
          TabStop = False
          CharCase = ecUpperCase
          Color = clScrollBar
          Font.Charset = ANSI_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'Arial'
          Font.Style = []
          ParentFont = False
          ReadOnly = True
          TabOrder = 1
        end
        object tACOSPIN: TEdit
          Left = 103
          Top = 112
          Width = 114
          Height = 22
          Color = clWhite
          Font.Charset = ANSI_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'Arial'
          Font.Style = []
          MaxLength = 16
          ParentFont = False
          TabOrder = 12
          OnKeyPress = tACOSPINKeyPress
        end
        object tACOSKc: TEdit
          Left = 103
          Top = 202
          Width = 114
          Height = 22
          TabStop = False
          Color = clScrollBar
          Font.Charset = ANSI_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'Arial'
          Font.Style = []
          ParentFont = False
          ReadOnly = True
          TabOrder = 2
        end
        object tACOSKt: TEdit
          Left = 103
          Top = 235
          Width = 114
          Height = 22
          TabStop = False
          Color = clScrollBar
          Font.Charset = ANSI_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'Arial'
          Font.Style = []
          ParentFont = False
          ReadOnly = True
          TabOrder = 3
        end
        object tACOSKd: TEdit
          Left = 103
          Top = 273
          Width = 114
          Height = 22
          TabStop = False
          Color = clScrollBar
          ReadOnly = True
          TabOrder = 4
        end
        object tACOSKcr: TEdit
          Left = 103
          Top = 310
          Width = 114
          Height = 22
          TabStop = False
          Color = clScrollBar
          ReadOnly = True
          TabOrder = 5
        end
        object tACOSKcf: TEdit
          Left = 103
          Top = 347
          Width = 114
          Height = 22
          TabStop = False
          Color = clScrollBar
          ReadOnly = True
          TabOrder = 6
        end
        object tACOSKrd: TEdit
          Left = 103
          Top = 384
          Width = 114
          Height = 22
          TabStop = False
          Color = clScrollBar
          ReadOnly = True
          TabOrder = 7
        end
        object Button1: TButton
          Left = 141
          Top = 432
          Width = 89
          Height = 33
          Caption = 'Generate Keys'
          TabOrder = 17
          OnClick = Button1Click
        end
        object Button2: TButton
          Left = 250
          Top = 432
          Width = 89
          Height = 33
          Caption = 'Save Keys'
          TabOrder = 19
          OnClick = Button2Click
        end
        object rb3DES: TRadioButton
          Left = 156
          Top = 144
          Width = 97
          Height = 17
          Caption = '3DES'
          Enabled = False
          TabOrder = 15
          OnClick = rb3DESClick
        end
        object rbDES: TRadioButton
          Left = 36
          Top = 144
          Width = 97
          Height = 17
          Caption = 'DES'
          Enabled = False
          TabOrder = 14
          OnClick = rbDESClick
        end
        object tACOSKcRyt: TEdit
          Left = 222
          Top = 202
          Width = 114
          Height = 22
          Color = clScrollBar
          ReadOnly = True
          TabOrder = 10
        end
        object tACOSKtRyt: TEdit
          Left = 223
          Top = 234
          Width = 114
          Height = 22
          Color = clScrollBar
          ReadOnly = True
          TabOrder = 13
        end
        object tACOSKdRyt: TEdit
          Left = 223
          Top = 273
          Width = 114
          Height = 22
          TabStop = False
          Color = clScrollBar
          ReadOnly = True
          TabOrder = 16
        end
        object tACOSKcrRyt: TEdit
          Left = 222
          Top = 309
          Width = 114
          Height = 22
          TabStop = False
          Color = clScrollBar
          ReadOnly = True
          TabOrder = 18
        end
        object tACOSKcfRyt: TEdit
          Left = 222
          Top = 346
          Width = 114
          Height = 22
          TabStop = False
          Color = clScrollBar
          ReadOnly = True
          TabOrder = 20
        end
        object tACOSKrdRyt: TEdit
          Left = 223
          Top = 385
          Width = 114
          Height = 22
          TabStop = False
          Color = clScrollBar
          ReadOnly = True
          TabOrder = 21
        end
      end
    end
  end
  object mMsg: TRichEdit
    Left = 391
    Top = 13
    Width = 337
    Height = 521
    TabStop = False
    HideScrollBars = False
    Lines.Strings = (
      'mMsg')
    ScrollBars = ssVertical
    TabOrder = 1
  end
end
