//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              PollingSample.dpr
//
//  Description:       This sample program outlines the steps on how to
//                     execute card detection polling functions using ACR128
//
//  Author:	           Jose Isagani R. Mission
//
//  Date:	             May 20, 2008
//
//  Revision Trail:   (Date/Author/Description)
//
//======================================================================
unit PollingMain;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ACSModule, StdCtrls, ComCtrls, ExtCtrls;

const MAX_BUFFER_LEN    = 256;
const INVALID_SW1SW2    = -450;

type
  TMainPolling = class(TForm)
    Label1: TLabel;
    cbReader: TComboBox;
    mMsg: TRichEdit;
    bInit: TButton;
    bConnect: TButton;
    bReset: TButton;
    bQuit: TButton;
    bSetExMode: TButton;
    rgExMode: TRadioGroup;
    bGetExMode: TButton;
    bClear: TButton;
    rgCurrMode: TRadioGroup;
    gbPollOpt: TGroupBox;
    cbPollOpt1: TCheckBox;
    cbPollOpt2: TCheckBox;
    cbPollOpt3: TCheckBox;
    cbPollOpt4: TCheckBox;
    cbPollOpt6: TCheckBox;
    bReadPollOpt: TButton;
    bSetPollOpt: TButton;
    cbPollOpt5: TCheckBox;
    rgPICCInt: TRadioGroup;
    bManPoll: TButton;
    bAutoPoll: TButton;
    pollTimer: TTimer;
    sbMsg: TStatusBar;
    procedure bQuitClick(Sender: TObject);
    procedure bResetClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure bInitClick(Sender: TObject);
    procedure bConnectClick(Sender: TObject);
    procedure bClearClick(Sender: TObject);
    procedure bGetExModeClick(Sender: TObject);
    procedure bSetExModeClick(Sender: TObject);
    procedure bReadPollOptClick(Sender: TObject);
    procedure bSetPollOptClick(Sender: TObject);
    procedure bManPollClick(Sender: TObject);
    procedure bAutoPollClick(Sender: TObject);
    procedure pollTimerTimer(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  MainPolling: TMainPolling;
  hContext            : SCARDCONTEXT;
  hCard               : SCARDCONTEXT;
  ioRequest           : SCARD_IO_REQUEST;
  RdrState    : SCARD_READERSTATE;
  retCode, pollCase   : Integer;
  dwActProtocol, BufferLen  : DWORD;
  SendBuff, RecvBuff  : array [0..262] of Byte;
  ATRVal              : array [0..128] of byte;
  ATRLen              : ^DWORD;
  SendLen, RecvLen, nBytesRet: DWORD;
  Buffer              : array [0..MAX_BUFFER_LEN] of char;
  connActive, autoDet, dualPoll : Boolean;

procedure InitMenu();
procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
procedure EnableButtons(reqType: Integer);
procedure ClearBuffers();
procedure ErrorRoutine();
procedure GetATR();
procedure InterpretATR();
procedure ReadPollingOption(reqType: integer);
procedure GetExMode(reqType: integer);
function CallCardConnect(reqType: integer): integer;
function CallCardControl(): integer;
function SendAPDUandDisplay(SendType: integer): integer;

implementation

{$R *.dfm}

procedure InitMenu();
begin

  connActive := False;
  autoDet := False;
  dualPoll := False;
  MainPolling.cbReader.Clear;
  MainPolling.mMsg.Clear;
  DisplayOut(0, 0, 'Program ready');
  MainPolling.bConnect.Enabled := False;
  MainPolling.bReset.Enabled := False;
  MainPolling.bInit.Enabled := True;
  MainPolling.rgExMode.ItemIndex := -1;
  MainPolling.rgExMode.Enabled := False;
  MainPolling.rgCurrMode.ItemIndex := -1;
  MainPolling.rgCurrMode.Enabled := False;
  MainPolling.bSetExMode.Enabled := False;
  MainPolling.bGetExMode.Enabled := False;
  MainPolling.gbPollOpt.Enabled := False;
  MainPolling.cbPollOpt1.Checked := False;
  MainPolling.cbPollOpt2.Checked := False;
  MainPolling.cbPollOpt3.Checked := False;
  MainPolling.cbPollOpt4.Checked := False;
  MainPolling.cbPollOpt5.Checked := False;
  MainPolling.cbPollOpt6.Checked := False;
  MainPolling.rgPICCInt.ItemIndex := -1;
  MainPolling.rgPICCInt.Enabled := False;
  MainPolling.bReadPollOpt.Enabled := False;
  MainPolling.bSetPollOpt.Enabled := False;
  MainPolling.bManPoll.Enabled := False;
  MainPolling.bAutoPoll.Enabled := False;
  MainPolling.sbMsg.Panels[0].Text := 'ICC Reader Status';
  MainPolling.sbMsg.Panels[2].Text := 'PICC Reader Status';

end;

procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
begin
  case errType of
    0: MainPolling.mMsg.SelAttributes.Color := clTeal;      // Notifications
    1: begin                                                // Error Messages
         MainPolling.mMsg.SelAttributes.Color := clRed;
         PrintText := GetScardErrMsg(retVal);
       end;
    2: begin
         MainPolling.mMsg.SelAttributes.Color := clBlack;
         PrintText := '< ' + PrintText;                      // Input data
       end;
    3: begin
         MainPolling.mMsg.SelAttributes.Color := clBlack;
         PrintText := '> ' + PrintText;                      // Output data
       end;
    4: MainPolling.mMsg.SelAttributes.Color := clRed;        // For ACOS1 error
    5: MainPolling.sbMsg.Panels[1].Text := PrintText;        // ICC Polling Status
    6: MainPolling.sbMsg.Panels[3].Text := PrintText;        // PICC Polling Status
  end;
  if not (errType in [5,6]) then
    MainPolling.mMsg.Lines.Add(PrintText);
  MainPolling.mMsg.SelAttributes.Color := clBlack;
  MainPolling.mMsg.SetFocus;

end;

procedure EnableButtons(reqType: Integer);
begin

  case reqType of
    0: begin
        MainPolling.bInit.Enabled := False;
        MainPolling.bConnect.Enabled := True;
        MainPolling.bReset.Enabled := True;
      end;
    1: begin
        MainPolling.rgExMode.Enabled := True;
        MainPolling.rgCurrMode.Enabled := True;
        MainPolling.bSetExMode.Enabled := True;
        MainPolling.bGetExMode.Enabled := True;
        MainPolling.gbPollOpt.Enabled := True;
        MainPolling.rgPICCInt.Enabled := True;
        MainPolling.bReadPollOpt.Enabled := True;
        MainPolling.bSetPollOpt.Enabled := True;
        MainPolling.bManPoll.Enabled := True;
        MainPolling.bAutoPoll.Enabled := True;
      end;
  end;

end;

procedure ErrorRoutine();
begin

  MainPolling.rgExMode.ItemIndex := 0;
  MainPolling.rgExMode.Enabled := False;
  MainPolling.bSetExMode.Enabled := False;
  MainPolling.bGetExMode.Enabled := False;

end;
procedure ClearBuffers();
var indx: integer;
begin

  for indx := 0 to 262 do
    begin
      SendBuff[indx] := $00;
      RecvBuff[indx] := $00;
    end;

end;

procedure GetATR();
var ReaderLen, dwState: ^DWORD;
    tmpStr: String;
    indx: Integer;
    tmpWord: DWORD;
begin

  DisplayOut(0, 0, 'Invoke SCardStatus');
  // 1. Invoke SCardStatus using hCard handle
  //    and valid reader name
  tmpWord := 32;
  ATRLen := @tmpWord;
  retCode := SCardStatusA(hCard,
                         PChar(MainPolling.cbReader.Text),
                         @ReaderLen,
                         @dwState,
                         @dwActProtocol,
                         @ATRVal,
                         @ATRLen);
  if retCode <> SCARD_S_SUCCESS then
    DisplayOut(1, retCode, '')
  else begin
    // 2. Format ATRVal returned and display string as ATR value
    tmpStr := 'ATR Length: ' + InttoStr(integer(ATRLen));
    DisplayOut(3, 0, tmpStr);
    tmpStr := 'ATRValue: ';
    for indx := 0 to integer(ATRLen)-1 do
      tmpStr := tmpStr + Format('%.02X ',[ATRVal[indx]]);
    DisplayOut(3, 0, tmpStr);

    // 3. Interpret dwActProtocol returned and display as active protocol
    tmpStr := 'Active Protocol: ';
    case integer(dwActProtocol) of
      1: tmpStr := tmpStr + 'T=0';
      2: tmpStr := tmpStr + 'T=1';
    else
      tmpStr := 'No protocol is defined.';
    end;
    DisplayOut(3, 0, tmpStr);

    InterpretATR();
    
  end;

end;

procedure InterpretATR();
var RIDVal, cardName: String;
    indx: Integer;
begin

  // 4. Interpret ATR and guess card
  // 4.1. Mifare cards using ISO 14443 Part 3 Supplemental Document
  if integer(ATRLen) > 14 then begin
    RIDVal := '';
    for indx := 7 to 11 do
      RIDVal := RIDVal + Format('%.02X',[ATRVal[indx]]);
    if (RIDVal = 'A000000306') then begin
      cardName := '';
      case ATRVal[12] of
        0: cardName := 'No card information';
        1: cardName := 'ISO 14443 A, Part1 Card Type';
        2: cardName := 'ISO 14443 A, Part2 Card Type';
        3: cardName := 'ISO 14443 A, Part3 Card Type';
        5: cardName := 'ISO 14443 B, Part1 Card Type';
        6: cardName := 'ISO 14443 B, Part2 Card Type';
        7: cardName := 'ISO 14443 B, Part3 Card Type';
        9: cardName := 'ISO 15693, Part1 Card Type';
        10: cardName := 'ISO 15693, Part2 Card Type';
        11: cardName := 'ISO 15693, Part3 Card Type';
        12: cardName := 'ISO 15693, Part4 Card Type';
        13: cardName := 'Contact Card (7816-10) IIC Card Type';
        14: cardName := 'Contact Card (7816-10) Extended IIC Card Type';
        15: cardName := 'Contact Card (7816-10) 2WBP Card Type';
        16: cardName := 'Contact Card (7816-10) 3WBP Card Type';
      else
        cardName := 'Undefined card';
      end;
      if (ATRVal[12] = $03) then
        if ATRVal[13] = $00 then
          case ATRVal[14] of
            $01: cardName := cardName + ': Mifare Standard 1K';
            $02: cardName := cardName + ': Mifare Standard 4K';
            $03: cardName := cardName + ': Mifare Ultra light';
            $04: cardName := cardName + ': SLE55R_XXXX';
            $06: cardName := cardName + ': SR176';
            $07: cardName := cardName + ': SRI X4K';
            $08: cardName := cardName + ': AT88RF020';
            $09: cardName := cardName + ': AT88SC0204CRF';
            $0A: cardName := cardName + ': AT88SC0808CRF';
            $0B: cardName := cardName + ': AT88SC1616CRF';
            $0C: cardName := cardName + ': AT88SC3216CRF';
            $0D: cardName := cardName + ': AT88SC6416CRF';
            $0E: cardName := cardName + ': SRF55V10P';
            $0F: cardName := cardName + ': SRF55V02P';
            $10: cardName := cardName + ': SRF55V10S';
            $11: cardName := cardName + ': SRF55V02S';
            $12: cardName := cardName + ': TAG IT';
            $13: cardName := cardName + ': LRI512';
            $14: cardName := cardName + ': ICODESLI';
            $15: cardName := cardName + ': TEMPSENS';
            $16: cardName := cardName + ': I.CODE1';
            $17: cardName := cardName + ': PicoPass 2K';
            $18: cardName := cardName + ': PicoPass 2KS';
            $19: cardName := cardName + ': PicoPass 16K';
            $1A: cardName := cardName + ': PicoPass 16KS';
            $1B: cardName := cardName + ': PicoPass 16K(8x2)';
            $1C: cardName := cardName + ': PicoPass 16KS(8x2)';

            $1D: cardName := cardName + ': PicoPass 32KS(16+16)';
            $1E: cardName := cardName + ': PicoPass 32KS(16+8x2)';
            $1F: cardName := cardName + ': PicoPass 32KS(8x2+16)';
            $20: cardName := cardName + ': PicoPass 32KS(8x2+8x2)';
            $21: cardName := cardName + ': LRI64';
            $22: cardName := cardName + ': I.CODE UID';
            $23: cardName := cardName + ': I.CODE EPC';
            $24: cardName := cardName + ': LRI12';
            $25: cardName := cardName + ': LRI128';
            $26: cardName := cardName + ': Mifare Mini';
          end
        else
          if ATRVal[13] = $FF then
            case ATRVal[14] of
              $09: cardName := cardName + ': Mifare Mini';
            end;
      DisplayOut(3, 0, cardName + ' is detected.');
    end;
   end;

  // 4.2. Mifare DESFire card using ISO 14443 Part 4
  if integer(ATRLen) = 11 then begin
    RIDVal := '';
    for indx := 4 to 9 do
      RIDVal := RIDVal + Format('%.02X',[ATRVal[indx]]);
    if (RIDVal = '067577810280') then
      DisplayOut(3, 0, 'Mifare DESFire is detected.');
  end;

  // 4.3. Other cards using ISO 14443 Part 4
  if integer(ATRLen) = 17 then begin
    RIDVal := '';
    for indx := 4 to 15 do
      RIDVal := RIDVal + Format('%.02X',[ATRVal[indx]]);
    if (RIDVal = '50122345561253544E3381C3') then
      DisplayOut(3, 0, 'ST19XRC8E is detected.');
  end;

end;

procedure ReadPollingOption(reqType: integer);
begin

  ClearBuffers();
  SendBuff[0] := $23;
  SendBuff[1] := $00;
  SendLen := 2;
  RecvLen := 6;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  if reqType = 1 then begin
    // Interpret PICC Polling Setting
    if (RecvBuff[5] and $01) <> 0 then begin
      DisplayOut(3, 0, 'Automatic PICC polling is enabled.');
      MainPolling.cbPollOpt1.Checked := True;
    end
    else begin
      DisplayOut(3, 0, 'Automatic PICC polling is disabled.');
      MainPolling.cbPollOpt1.Checked := False;
    end;

    if (RecvBuff[5] and $02) <> 0 then begin
      DisplayOut(3, 0, 'Antenna off when no PICC found is enabled.');
      MainPolling.cbPollOpt2.Checked := True;
    end
    else begin
      DisplayOut(3, 0, 'Antenna off when no PICC found is disabled.');
      MainPolling.cbPollOpt2.Checked := False;
    end;

    if (RecvBuff[5] and $04) <> 0 then begin
      DisplayOut(3, 0, 'Antenna off when PICC is inactive is enabled.');
      MainPolling.cbPollOpt3.Checked := True;
    end
    else begin
      DisplayOut(3, 0, 'Antenna off when PICC is inactive is disabled.');
      MainPolling.cbPollOpt3.Checked := False;
    end;

    if (RecvBuff[5] and $08) <> 0 then begin
      DisplayOut(3, 0, 'Activate PICC when detected is enabled.');
      MainPolling.cbPollOpt4.Checked := True;
    end
    else begin
      DisplayOut(3, 0, 'Activate PICC when detected is disabled.');
      MainPolling.cbPollOpt4.Checked := False;
    end;

    if (((RecvBuff[5] and $10) = 0) and ((RecvBuff[5] and $20) = 0)) then begin
      DisplayOut(3, 0, 'Poll interval is 250 msec.');
      MainPolling.rgPICCInt.ItemIndex := 0;
    end;

    if (((RecvBuff[5] and $10) <> 0) and ((RecvBuff[5] and $20) = 0)) then begin
      DisplayOut(3, 0, 'Poll interval is 500 msec.');
      MainPolling.rgPICCInt.ItemIndex := 1;
    end;

    if (((RecvBuff[5] and $10) = 0) and ((RecvBuff[5] and $20) <> 0)) then begin
      DisplayOut(3, 0, 'Poll interval is 1 sec.');
      MainPolling.rgPICCInt.ItemIndex := 2;
    end;

    if (((RecvBuff[5] and $10) <> 0) and ((RecvBuff[5] and $20) <> 0)) then begin
      DisplayOut(3, 0, 'Poll interval is 2.5 sec.');
      MainPolling.rgPICCInt.ItemIndex := 4;
    end;

    if (RecvBuff[5] and $40) <> 0 then begin
      DisplayOut(3, 0, 'Test Mode is enabled.');
      MainPolling.cbPollOpt5.Checked := True;
    end
    else begin
      DisplayOut(3, 0, 'Test Mode is disabled.');
      MainPolling.cbPollOpt5.Checked := False;
    end;

    if (RecvBuff[5] and $80) <> 0 then begin
      DisplayOut(3, 0, 'ISO14443A Part4 is enforced.');
      MainPolling.cbPollOpt6.Checked := True;
    end
    else begin
      DisplayOut(3, 0, 'ISO14443A Part4 is not enforced.');
      MainPolling.cbPollOpt6.Checked := False;
    end;
  end;

end;

function CallCardConnect(reqType: integer): integer;
var indx: integer;
begin

  if connActive then
    retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);

  // 1. Shared Connection
  retCode := SCardConnectA(hContext,
                           PChar(MainPolling.cbReader.Text),
                           SCARD_SHARE_SHARED,
                           SCARD_PROTOCOL_T0 or SCARD_PROTOCOL_T1,
                           @hCard,
                           @dwActProtocol);
  if retCode <> SCARD_S_SUCCESS then begin
    if reqType = 1 then begin     // Connect to SAM reader
      // check if ACR128 SAM is used and use Direct Mode if SAM is not detected
      indx := 0;
      MainPolling.cbReader.ItemIndex := indx;
      while AnsiPos('ACR128U SAM', MainPolling.cbReader.Text) = 0 do begin
        if indx = MainPolling.cbReader.Items.Count then begin
          DisplayOut(0, 0, 'Cannot find ACR128 SAM reader.');
          Exit;
        end;
        inc(indx);
        MainPolling.cbReader.ItemIndex := indx;
      end;
      // 1. Direct Connection
      retCode := SCardConnectA(hContext,
                               PChar(MainPolling.cbReader.Text),
                               SCARD_SHARE_DIRECT,
                               0,
                               @hCard,
                               @dwActProtocol);
     if retCode <> SCARD_S_SUCCESS then begin
        DisplayOut(1, retCode, '');
        connActive := False;
        CallCardConnect := retCode;
        Exit;
      end
      else
        DisplayOut(0, 0, 'Successful connection to ' + MainPolling.cbReader.Text);
    end
    else begin
      DisplayOut(1, retCode, '');
      connActive := False;
      CallCardConnect := retCode;
      Exit;
    end;
  end
  else
    DisplayOut(0, 0, 'Successful connection to ' + MainPolling.cbReader.Text);
  CallCardConnect := retCode;

end;

procedure GetExMode(reqType: integer);
var tmpStr: string;
    indx: integer;
begin

  ClearBuffers();
  SendBuff[0] := $2B;
  SendBuff[1] := $00;
  SendLen := 2;
  RecvLen := 7;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

  // Interpret Configuration Setting and Current Mode
  tmpStr := '';
  for indx := 0 to 4 do
    tmpStr := tmpStr + Format('%.02X',[RecvBuff[indx]]);
  if (tmpStr = 'E100000002') then begin
    if reqType = 1 then begin
      if RecvBuff[5] = 0 then
        MainPolling.rgExMode.ItemIndex := 0
      else
        MainPolling.rgExMode.ItemIndex := 1;
      if RecvBuff[6] = 0 then
        MainPolling.rgCurrMode.ItemIndex := 0
      else
        MainPolling.rgCurrMode.ItemIndex := 1;
     end;
  end
  else
    DisplayOut(4, 0, 'Wrong return values from device.');

end;

function CallCardControl(): integer;
var tmpStr: string;
    indx: integer;
begin

  // Display Apdu In
  tmpStr := 'SCardControl: ';
  for indx := 0 to SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  DisplayOut(2, 0, tmpStr);

  retCode := SCardControl(hCard,
                  IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND,
                  @SendBuff,
                  SendLen,
                  @RecvBuff,
                  RecvLen,
                  @nBytesRet);
  if retCode <> SCARD_S_SUCCESS then
    DisplayOut(1, retCode, '')
  else begin
    tmpStr := '';
    for indx := 0 to (RecvLen-1) do
      tmpStr := tmpStr + Format('%.02X ', [(RecvBuff[indx])]);
    DisplayOut(3, 0, Trim(tmpStr));
  end;
  CallCardControl := retCode;

end;

function SendAPDUandDisplay(SendType: integer): integer;
var tmpStr: string;
    indx: integer;
begin

  ioRequest.dwProtocol := dwActProtocol;
  ioRequest.cbPciLength := sizeof(SCARD_IO_REQUEST);

  // Display Apdu In
  tmpStr := '';
  for indx := 0 to SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  DisplayOut(2, 0, tmpStr);

  retCode := SCardTransmit(hCard,
                           @ioRequest,
                           @SendBuff,
                           SendLen,
                           Nil,
                           @RecvBuff,
                           @RecvLen);
  if retCode <> SCARD_S_SUCCESS then begin
      DisplayOut(1, retCode, '');
      SendAPDUandDisplay := retCode;
      Exit;
  end
  else begin
    tmpStr := '';
    case SendType of
    0: begin      // Display SW1/SW2 value
       for indx := (RecvLen-2) to (RecvLen-1) do
         tmpStr := tmpStr + Format('%.02X ', [(RecvBuff[indx])]);
       if (Trim(tmpStr) <> '90 00') then
         DisplayOut(1, 0, 'Return bytes are not acceptable.');
       end;
    1: begin      // Display ATR after checking SW1/SW2
       for indx := (RecvLen-2) to (RecvLen-1) do
         tmpStr := tmpStr + Format('%.02X ', [(RecvBuff[indx])]);
       if (Trim(tmpStr) <> '90 00') then
         DisplayOut(1, 0, 'Return bytes are not acceptable.')
       else begin
         tmpStr := 'ATR :';
         for indx := 0 to (RecvLen-3) do
           tmpStr := tmpStr + Format('%.2X ', [(RecvBuff[Indx])]);
       end;
       end;
    2: begin      // Display all data
       for indx := (RecvLen-2) to (RecvLen-1) do
         tmpStr := tmpStr + Format('%.02X ', [(RecvBuff[indx])]);
       end;
     end;
    DisplayOut(3, 0, Trim(tmpStr));
  end;
  SendAPDUandDisplay := retCode;

end;


procedure TMainPolling.bQuitClick(Sender: TObject);
begin

  Application.Terminate;

end;

procedure TMainPolling.bResetClick(Sender: TObject);
begin

  if connActive then
    retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
  retCode := SCardReleaseContext(hCard);
  InitMenu();

end;

procedure TMainPolling.FormActivate(Sender: TObject);
begin
  InitMenu();
end;

procedure TMainPolling.bInitClick(Sender: TObject);
var indx: integer;
begin

  // 1. Establish context and obtain hContext handle
  retCode := SCardEstablishContext(SCARD_SCOPE_USER,
                                   nil,
                                   nil,
                                   @hContext);
  if retCode <> SCARD_S_SUCCESS then begin
    DisplayOut(1, retCode, '');
    Exit;
  end ;

  // 2. List PC/SC card readers installed in the system
  BufferLen := MAX_BUFFER_LEN;
  retCode := SCardListReadersA(hContext,
                               nil,
                               @Buffer,
                               @BufferLen);
  if retCode <> SCARD_S_SUCCESS then begin
    DisplayOut(1, retCode, '');
    Exit;
  end;

  EnableButtons(0);
  cbReader.Clear;;
  LoadListToControl(cbReader,@buffer,bufferLen);
  // Look for ACR128 SAM and make it the default reader in the combobox
  for indx := 0 to cbReader.Items.Count-1 do begin
    cbReader.ItemIndex := indx;
    if AnsiPos('ACR128U SAM', cbReader.Text) > 0 then
      Exit;
  end;
  cbReader.ItemIndex := 0;

end;

procedure TMainPolling.bConnectClick(Sender: TObject);
begin

  retCode := CallCardConnect(1);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  connActive := True;
  EnableButtons(1);

end;

procedure TMainPolling.bClearClick(Sender: TObject);
begin

  mMsg.Clear;

end;

procedure TMainPolling.bGetExModeClick(Sender: TObject);
begin

  GetExMode(1);

end;

procedure TMainPolling.bSetExModeClick(Sender: TObject);
begin

  ClearBuffers();
  SendBuff[0] := $2B;
  SendBuff[1] := $01;
  case rgExMode.ItemIndex of
    0: SendBuff[2] := $00;
    1: SendBuff[2] := $01;
  end;
  SendLen := 3;
  RecvLen := 7;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

procedure TMainPolling.bReadPollOptClick(Sender: TObject);
begin

  ReadPollingOption(1);

end;

procedure TMainPolling.bSetPollOptClick(Sender: TObject);
begin

  ClearBuffers();
  SendBuff[0] := $23;
  SendBuff[1] := $01;
  if cbPollOpt1.Checked = True then
    SendBuff[2] := SendBuff[2] or $01;
  if cbPollOpt2.Checked = True then
    SendBuff[2] := SendBuff[2] or $02;
  if cbPollOpt3.Checked = True then
    SendBuff[2] := SendBuff[2] or $04;
  if cbPollOpt4.Checked = True then
    SendBuff[2] := SendBuff[2] or $08;

  case rgPICCInt.ItemIndex of
    1: begin
         SendBuff[2] := SendBuff[2] or $10;
       end;
    2: begin
         SendBuff[2] := SendBuff[2] or $20;
       end;
    3: begin
         SendBuff[2] := SendBuff[2] or $10;
         SendBuff[2] := SendBuff[2] or $20;
       end;
  end;

  if cbPollOpt5.Checked = True then
    SendBuff[2] := SendBuff[2] or $40;
  if cbPollOpt6.Checked = True then
    SendBuff[2] := SendBuff[2] or $80;
  SendLen := 3;
  RecvLen := 7;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

procedure TMainPolling.bManPollClick(Sender: TObject);
begin

  // Always use a valid connection for Card Control commands
  retCode := CallCardConnect(1);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  ReadPollingOption(0);
  if (RecvBuff[5] and $01) <> 0 then
    DisplayOut(0, 0, 'Turn off automatic PICC polling in the device before using this function.')
  else begin
    ClearBuffers();
    SendBuff[0] := $22;
    SendBuff[1] := $01;
    SendBuff[2] := $0A;
    SendLen := 3;
    RecvLen := 6;
    retCode := CallCardControl();
    if retCode <> SCARD_S_SUCCESS then
      Exit;
    if (RecvBuff[5] and $01) <> 0 then
      DisplayOut(6, 0, 'No card within range.')
    else
      DisplayOut(6, 0, 'Card is detected.');
  end;

end;

procedure TMainPolling.bAutoPollClick(Sender: TObject);
begin

  // pollCase legend
  // 1 = Both ICC and PICC can poll, but only one at a time
  // 2 = Only ICC can poll
  // 3 = Both reader can be polled


  if autoDet then begin
    autoDet := False;
    bAutoPoll.Caption := 'Start &Auto Detection';
    pollTimer.Enabled := False;
    DisplayOut(5, 0, '');
    DisplayOut(6, 0, '');
    Exit;
  end;

  // Always use a valid connection for Card Control commands
  retCode := CallCardConnect(1);
  if retCode <> SCARD_S_SUCCESS then begin
    bAutoPoll.Caption := 'Start &Auto Detection';
    autoDet := False;
    Exit;
  end;
  GetExMode(0);
  if RecvBuff[5] <> 0 then begin   // Either ICC or PICC can be polled at any one time
    ReadPollingOption(0);
    if (RecvBuff[5] and $01) <> 0 then      // Auto PICC polling is enabled
      pollCase := 1                         // Either ICC and PICC can be polled
    else
      pollCase := 2;                        // Only ICC can be polled
  end
  else begin                       // Both ICC and PICC can be enabled at the same time
    ReadPollingOption(0);
    if (RecvBuff[5] and $01) <> 0 then      // Auto PICC polling is enabled
      pollCase := 3                         // Both ICC and PICC can be polled
    else
      pollCase := 2;                        // Only ICC can be polled
  end;


  case pollCase of
    1: DisplayOut(0, 0, 'Either reader can detect cards, but not both.');
    2: DisplayOut(0, 0, 'Automatic PICC polling is disabled, only ICC can detect card.');
    3: DisplayOut(0, 0, 'Both ICC and PICC readers can automatically detect card.');
  end;

  autoDet := True;
  pollTimer.Enabled := True;
  bAutoPoll.Caption := 'End &Auto Detection';

end;

procedure TMainPolling.pollTimerTimer(Sender: TObject);
var indx: integer;
begin

  case pollCase of
//    1: // Either reader can detect cards, but not both.
//    begin
//    end;
    2: // Automatic PICC polling is disabled, only ICC can detect card
    begin
      // Connect to ICC reader
      DisplayOut(6, 0, 'Auto Polling is disabled.');
      indx := 0;
      MainPolling.cbReader.ItemIndex := indx;
      while AnsiPos('ACR128U ICC', MainPolling.cbReader.Text) = 0 do begin
        if indx = MainPolling.cbReader.Items.Count then begin
          DisplayOut(0, 0, 'Cannot find ACR128 ICC reader.');
          pollTimer.Enabled := False;
          Exit;
        end;
        inc(indx);
        MainPolling.cbReader.ItemIndex := indx;
      end;

      RdrState.szReader := PChar(cbReader.Text);
      retCode := SCardGetStatusChangeA(hContext,
                                       0,
                                       @RdrState,
                                       1);
      if retCode <> SCARD_S_SUCCESS then begin
        DisplayOut(1, retCode, '');
        pollTimer.Enabled := False;
        Exit;
      end;
      if (RdrState.dwEventStates and SCARD_STATE_PRESENT) <> 0 then
        DisplayOut(5, 0, 'Card is inserted.')
      else
        DisplayOut(5, 0, 'Card is removed.');

    end;
    1,3: //Both ICC and PICC readers can automatically detect card
    begin
      // Attempt to poll ICC reader
      indx := 0;
      cbReader.ItemIndex := indx;
      while AnsiPos('ACR128U ICC', cbReader.Text) = 0 do begin
        if indx = cbReader.Items.Count then begin
          DisplayOut(0, 0, 'Cannot find ACR128 ICC reader.');
          pollTimer.Enabled := False;
          Exit;
        end;
        inc(indx);
        cbReader.ItemIndex := indx;
      end;

      RdrState.szReader := PChar(cbReader.Text);
      retCode := SCardGetStatusChangeA(hContext,
                                       0,
                                       @RdrState,
                                       1);
      if retCode = SCARD_S_SUCCESS then begin
        if (RdrState.dwEventStates and SCARD_STATE_PRESENT) <> 0 then
          DisplayOut(5, 0, 'Card is inserted.')
        else
          DisplayOut(5, 0, 'Card is removed.');
      end;
      // Attempt to poll PICC reader
      indx := 0;
      cbReader.ItemIndex := indx;
      while AnsiPos('ACR128U PICC', cbReader.Text) = 0 do begin
        if indx = cbReader.Items.Count then begin
          DisplayOut(0, 0, 'Cannot find ACR128 PICC reader.');
          pollTimer.Enabled := False;
          Exit;
        end;
        inc(indx);
        cbReader.ItemIndex := indx;
      end;

      RdrState.szReader := PChar(cbReader.Text);
      retCode := SCardGetStatusChangeA(hContext,
                                       0,
                                       @RdrState,
                                       1);
      if retCode = SCARD_S_SUCCESS then begin
        if (RdrState.dwEventStates and SCARD_STATE_PRESENT) <> 0 then
          DisplayOut(6, 0, 'Card is detected.')
        else
          DisplayOut(6, 0, 'No card within range.');
      end;
    end;
  end;
  Application.ProcessMessages;
  
end;

end.
