object MainACOSBin: TMainACOSBin
  Left = 192
  Top = 107
  Width = 697
  Height = 519
  Caption = 'Using Binary Files in ACOS3'
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
    Left = 16
    Top = 19
    Width = 68
    Height = 13
    Caption = 'Select Reader'
  end
  object cbReader: TComboBox
    Left = 96
    Top = 16
    Width = 209
    Height = 21
    Style = csDropDownList
    ItemHeight = 13
    TabOrder = 0
  end
  object mMsg: TRichEdit
    Left = 320
    Top = 16
    Width = 361
    Height = 417
    TabOrder = 1
  end
  object bInit: TButton
    Left = 200
    Top = 48
    Width = 105
    Height = 25
    Caption = '&Initialize'
    TabOrder = 2
    OnClick = bInitClick
  end
  object bConnect: TButton
    Left = 200
    Top = 80
    Width = 105
    Height = 25
    Caption = '&Connect'
    TabOrder = 3
    OnClick = bConnectClick
  end
  object bClear: TButton
    Left = 328
    Top = 448
    Width = 105
    Height = 25
    Caption = 'C&lear'
    TabOrder = 4
    OnClick = bClearClick
  end
  object bReset: TButton
    Left = 448
    Top = 448
    Width = 105
    Height = 25
    Caption = 'R&eset'
    TabOrder = 5
    OnClick = bResetClick
  end
  object bQuit: TButton
    Left = 568
    Top = 448
    Width = 105
    Height = 25
    Caption = '&Quit'
    TabOrder = 6
    OnClick = bQuitClick
  end
  object gbFormat: TGroupBox
    Left = 8
    Top = 112
    Width = 297
    Height = 89
    Caption = 'Card Format Routine'
    TabOrder = 7
    object Label2: TLabel
      Left = 16
      Top = 27
      Width = 30
      Height = 13
      Caption = 'File ID'
    end
    object Label3: TLabel
      Left = 16
      Top = 51
      Width = 33
      Height = 13
      Caption = 'Length'
    end
    object bFormat: TButton
      Left = 192
      Top = 48
      Width = 97
      Height = 25
      Caption = '&Format Card'
      TabOrder = 0
      OnClick = bFormatClick
    end
    object tFileID1: TEdit
      Left = 56
      Top = 24
      Width = 33
      Height = 21
      MaxLength = 2
      TabOrder = 1
      OnKeyPress = tFileID1KeyPress
    end
    object tFileID2: TEdit
      Left = 88
      Top = 24
      Width = 33
      Height = 21
      MaxLength = 2
      TabOrder = 2
      OnKeyPress = tFileID1KeyPress
    end
    object tFileLen1: TEdit
      Left = 56
      Top = 48
      Width = 33
      Height = 21
      MaxLength = 2
      TabOrder = 3
      OnKeyPress = tFileID1KeyPress
    end
    object tFileLen2: TEdit
      Left = 88
      Top = 48
      Width = 33
      Height = 21
      MaxLength = 2
      TabOrder = 4
      OnKeyPress = tFileID1KeyPress
    end
  end
  object gbReadWrite: TGroupBox
    Left = 8
    Top = 208
    Width = 305
    Height = 273
    Caption = 'Read and Write to Binary File'
    TabOrder = 8
    object Label4: TLabel
      Left = 16
      Top = 27
      Width = 30
      Height = 13
      Caption = 'File ID'
    end
    object Label5: TLabel
      Left = 16
      Top = 72
      Width = 23
      Height = 13
      Caption = 'Data'
    end
    object Label6: TLabel
      Left = 16
      Top = 51
      Width = 47
      Height = 13
      Caption = 'File Offset'
    end
    object Label7: TLabel
      Left = 200
      Top = 51
      Width = 33
      Height = 13
      Caption = 'Length'
    end
    object tFID1: TEdit
      Left = 72
      Top = 24
      Width = 33
      Height = 21
      MaxLength = 2
      TabOrder = 0
    end
    object tFID2: TEdit
      Left = 104
      Top = 24
      Width = 33
      Height = 21
      MaxLength = 2
      TabOrder = 1
      OnKeyPress = tFileID1KeyPress
    end
    object tData: TRichEdit
      Left = 16
      Top = 88
      Width = 273
      Height = 97
      TabOrder = 2
    end
    object bBinRead: TButton
      Left = 192
      Top = 200
      Width = 97
      Height = 25
      Caption = '&Read Binary'
      TabOrder = 3
      OnClick = bBinReadClick
    end
    object bBinWrite: TButton
      Left = 192
      Top = 232
      Width = 97
      Height = 25
      Caption = '&Write Binary'
      TabOrder = 4
      OnClick = bBinWriteClick
    end
    object tOffset1: TEdit
      Left = 72
      Top = 48
      Width = 33
      Height = 21
      MaxLength = 2
      TabOrder = 5
      OnKeyPress = tFileID1KeyPress
    end
    object tOffset2: TEdit
      Left = 104
      Top = 48
      Width = 33
      Height = 21
      MaxLength = 2
      TabOrder = 6
      OnKeyPress = tFileID1KeyPress
    end
    object tLen: TEdit
      Left = 248
      Top = 48
      Width = 33
      Height = 21
      MaxLength = 2
      TabOrder = 7
      OnKeyPress = tFileID1KeyPress
    end
  end
end
