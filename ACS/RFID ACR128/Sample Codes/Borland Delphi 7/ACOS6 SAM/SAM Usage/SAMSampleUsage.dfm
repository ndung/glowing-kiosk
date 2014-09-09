object frmMain: TfrmMain
  Left = 214
  Top = 141
  Width = 689
  Height = 568
  Caption = 'SAM Sample Usage'
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
  object mMsg: TRichEdit
    Left = 272
    Top = 5
    Width = 401
    Height = 521
    HideScrollBars = False
    Lines.Strings = (
      'mMsg')
    ScrollBars = ssBoth
    TabOrder = 0
  end
  object grpSAM: TTabbedNotebook
    Left = 8
    Top = 5
    Width = 257
    Height = 521
    TabFont.Charset = DEFAULT_CHARSET
    TabFont.Color = clBtnText
    TabFont.Height = -11
    TabFont.Name = 'MS Sans Serif'
    TabFont.Style = []
    TabOrder = 1
    object TTabPage
      Left = 4
      Top = 24
      Caption = 'Security'
      object GroupBox1: TGroupBox
        Left = 8
        Top = 16
        Width = 233
        Height = 465
        TabOrder = 0
        object lblCardReader: TLabel
          Left = 8
          Top = 64
          Width = 60
          Height = 13
          Caption = 'Card Reader'
        end
        object Label1: TLabel
          Left = 8
          Top = 112
          Width = 61
          Height = 13
          Caption = 'SAM Reader'
        end
        object Label2: TLabel
          Left = 8
          Top = 243
          Width = 89
          Height = 13
          Caption = 'SAM GLOBAL PIN'
        end
        object Label3: TLabel
          Left = 8
          Top = 315
          Width = 83
          Height = 13
          Caption = 'ACOS CARD PIN'
        end
        object Label4: TLabel
          Left = 8
          Top = 387
          Width = 75
          Height = 13
          Caption = 'ACOS New PIN'
        end
        object btnListReaders: TButton
          Left = 8
          Top = 16
          Width = 89
          Height = 33
          Caption = 'List Readers'
          TabOrder = 0
          OnClick = btnListReadersClick
        end
        object cmbCardReader: TComboBox
          Left = 8
          Top = 80
          Width = 201
          Height = 21
          ItemHeight = 13
          TabOrder = 1
        end
        object cmbSAMReader: TComboBox
          Left = 8
          Top = 128
          Width = 201
          Height = 21
          ItemHeight = 13
          TabOrder = 2
        end
        object btnConnect: TButton
          Left = 8
          Top = 168
          Width = 89
          Height = 33
          Caption = 'Connect'
          TabOrder = 3
          OnClick = btnConnectClick
        end
        object rbDES: TRadioButton
          Left = 16
          Top = 216
          Width = 73
          Height = 17
          Caption = 'DES'
          Enabled = False
          TabOrder = 4
          OnClick = rbDESClick
        end
        object rb3DES: TRadioButton
          Left = 104
          Top = 216
          Width = 65
          Height = 17
          Caption = '3DES'
          Enabled = False
          TabOrder = 5
          OnClick = rb3DESClick
        end
        object tSAMGPIN: TEdit
          Left = 104
          Top = 240
          Width = 121
          Height = 21
          Enabled = False
          MaxLength = 16
          TabOrder = 6
          OnKeyPress = tSAMGPINKeyPress
        end
        object btnMA: TButton
          Left = 8
          Top = 272
          Width = 121
          Height = 33
          Caption = 'Mutual Authentication'
          Enabled = False
          TabOrder = 7
          OnClick = btnMAClick
        end
        object tACOSPIN: TEdit
          Left = 104
          Top = 312
          Width = 121
          Height = 21
          Enabled = False
          MaxLength = 16
          TabOrder = 8
          OnKeyPress = tACOSPINKeyPress
        end
        object btnSubmit: TButton
          Left = 8
          Top = 344
          Width = 121
          Height = 33
          Caption = 'Submit PIN (Ciphered)'
          Enabled = False
          TabOrder = 9
          OnClick = btnSubmitClick
        end
        object tACOSNewPIN: TEdit
          Left = 104
          Top = 384
          Width = 121
          Height = 21
          Enabled = False
          MaxLength = 16
          TabOrder = 10
          OnKeyPress = tACOSNewPINKeyPress
        end
        object btnChangePIN: TButton
          Left = 8
          Top = 416
          Width = 121
          Height = 33
          Caption = 'Change PIN (Ciphered)'
          Enabled = False
          TabOrder = 11
          OnClick = btnChangePINClick
        end
      end
    end
    object TTabPage
      Left = 4
      Top = 24
      Caption = 'Account'
      object grpAccount: TGroupBox
        Left = 8
        Top = 8
        Width = 233
        Height = 473
        TabOrder = 0
        object lblMaxBalance: TLabel
          Left = 8
          Top = 24
          Width = 62
          Height = 13
          Caption = 'Max Balance'
        end
        object lblBalance: TLabel
          Left = 8
          Top = 72
          Width = 39
          Height = 13
          Caption = 'Balance'
        end
        object lblCreditAmt: TLabel
          Left = 8
          Top = 192
          Width = 66
          Height = 13
          Caption = 'Credit Amount'
        end
        object lblDebitAmt: TLabel
          Left = 8
          Top = 312
          Width = 64
          Height = 13
          Caption = 'Debit Amount'
        end
        object tMaxBal: TEdit
          Left = 8
          Top = 40
          Width = 209
          Height = 21
          MaxLength = 16
          ReadOnly = True
          TabOrder = 0
        end
        object tBal: TEdit
          Left = 8
          Top = 88
          Width = 209
          Height = 21
          ReadOnly = True
          TabOrder = 1
        end
        object btnInquireAccount: TButton
          Left = 8
          Top = 120
          Width = 105
          Height = 33
          Caption = 'Inquire Account'
          TabOrder = 2
          OnClick = btnInquireAccountClick
        end
        object tCreditAmt: TEdit
          Left = 8
          Top = 208
          Width = 209
          Height = 21
          MaxLength = 7
          TabOrder = 3
          OnKeyPress = tCreditAmtKeyPress
        end
        object bCredit: TButton
          Left = 8
          Top = 240
          Width = 81
          Height = 33
          Caption = 'Credit'
          TabOrder = 4
          OnClick = bCreditClick
        end
        object tDebitAmt: TEdit
          Left = 8
          Top = 328
          Width = 209
          Height = 21
          MaxLength = 7
          TabOrder = 5
          OnKeyPress = tDebitAmtKeyPress
        end
        object btnDebitAmt: TButton
          Left = 8
          Top = 360
          Width = 81
          Height = 33
          Caption = 'Debit'
          TabOrder = 6
          OnClick = btnDebitAmtClick
        end
      end
    end
  end
end
