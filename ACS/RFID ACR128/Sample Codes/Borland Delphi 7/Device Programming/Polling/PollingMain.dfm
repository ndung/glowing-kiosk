object MainPolling: TMainPolling
  Left = 480
  Top = 157
  Width = 688
  Height = 599
  Caption = 'Polling Sample'
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
    Left = 96
    Top = 16
    Width = 185
    Height = 21
    Style = csDropDownList
    ItemHeight = 13
    TabOrder = 0
  end
  object mMsg: TRichEdit
    Left = 296
    Top = 16
    Width = 369
    Height = 473
    ScrollBars = ssVertical
    TabOrder = 1
  end
  object bInit: TButton
    Left = 16
    Top = 48
    Width = 129
    Height = 25
    Caption = '&Initialize'
    TabOrder = 2
    OnClick = bInitClick
  end
  object bConnect: TButton
    Left = 152
    Top = 48
    Width = 129
    Height = 25
    Caption = '&Connect'
    TabOrder = 3
    OnClick = bConnectClick
  end
  object bReset: TButton
    Left = 424
    Top = 512
    Width = 113
    Height = 25
    Caption = '&Reset'
    TabOrder = 4
    OnClick = bResetClick
  end
  object bQuit: TButton
    Left = 552
    Top = 512
    Width = 113
    Height = 25
    Caption = '&Quit'
    TabOrder = 5
    OnClick = bQuitClick
  end
  object bSetExMode: TButton
    Left = 152
    Top = 232
    Width = 129
    Height = 25
    Caption = 'S&et Mode Configuration'
    TabOrder = 6
    OnClick = bSetExModeClick
  end
  object rgExMode: TRadioGroup
    Left = 8
    Top = 88
    Width = 273
    Height = 65
    Caption = 'Configuration Setting'
    Items.Strings = (
      'Both ICC & PICC interfaces can be activated'
      'Either ICC or PICC interface can be activated')
    TabOrder = 7
  end
  object bGetExMode: TButton
    Left = 16
    Top = 232
    Width = 129
    Height = 25
    Caption = 'Read Current &Mode'
    TabOrder = 8
    OnClick = bGetExModeClick
  end
  object bClear: TButton
    Left = 296
    Top = 512
    Width = 113
    Height = 25
    Caption = 'C&lear Screen'
    TabOrder = 9
    OnClick = bClearClick
  end
  object rgCurrMode: TRadioGroup
    Left = 8
    Top = 160
    Width = 273
    Height = 65
    Caption = 'Current Mode'
    Items.Strings = (
      'Exclusive Mode is not active.'
      'Exclusive Mode is active.')
    TabOrder = 10
  end
  object gbPollOpt: TGroupBox
    Left = 8
    Top = 272
    Width = 273
    Height = 121
    Caption = 'Contactless Polling Options'
    TabOrder = 11
    object cbPollOpt1: TCheckBox
      Left = 8
      Top = 16
      Width = 233
      Height = 17
      Caption = 'Automatic PICC Polling'
      TabOrder = 0
    end
    object cbPollOpt2: TCheckBox
      Left = 8
      Top = 32
      Width = 233
      Height = 17
      Caption = 'Turn off antenna field if no PICC within range'
      TabOrder = 1
    end
    object cbPollOpt3: TCheckBox
      Left = 8
      Top = 48
      Width = 233
      Height = 17
      Caption = 'Turn off antenna field if PICC is inactive'
      TabOrder = 2
    end
    object cbPollOpt4: TCheckBox
      Left = 8
      Top = 64
      Width = 233
      Height = 17
      Caption = 'Activate the PICC when detected'
      TabOrder = 3
    end
    object cbPollOpt6: TCheckBox
      Left = 8
      Top = 96
      Width = 233
      Height = 17
      Caption = 'Enforce ISO 14443A Part4'
      TabOrder = 4
    end
    object cbPollOpt5: TCheckBox
      Left = 8
      Top = 80
      Width = 233
      Height = 17
      Caption = 'Test Mode'
      TabOrder = 5
    end
  end
  object bReadPollOpt: TButton
    Left = 152
    Top = 400
    Width = 129
    Height = 25
    Caption = 'Read &Polling Option'
    TabOrder = 12
    OnClick = bReadPollOptClick
  end
  object bSetPollOpt: TButton
    Left = 152
    Top = 440
    Width = 129
    Height = 25
    Caption = '&Set Polling Option'
    TabOrder = 13
    OnClick = bSetPollOptClick
  end
  object rgPICCInt: TRadioGroup
    Left = 16
    Top = 400
    Width = 97
    Height = 97
    Caption = 'Poll Interval'
    Items.Strings = (
      '250 msec'
      '500 msec'
      '1 sec'
      '2.5 sec')
    TabOrder = 14
  end
  object bManPoll: TButton
    Left = 16
    Top = 512
    Width = 129
    Height = 25
    Caption = '&Manual Card Detection'
    TabOrder = 15
    OnClick = bManPollClick
  end
  object bAutoPoll: TButton
    Left = 152
    Top = 512
    Width = 129
    Height = 25
    Caption = 'Start &Auto Detection'
    TabOrder = 16
    OnClick = bAutoPollClick
  end
  object sbMsg: TStatusBar
    Left = 0
    Top = 553
    Width = 680
    Height = 19
    Panels = <
      item
        Alignment = taCenter
        Width = 120
      end
      item
        Width = 210
      end
      item
        Alignment = taCenter
        Width = 120
      end
      item
        Width = 210
      end>
  end
  object pollTimer: TTimer
    Enabled = False
    Interval = 500
    OnTimer = pollTimerTimer
    Left = 640
  end
end
