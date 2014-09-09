//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              DevProgMain.pas
//
//  Description:       This sample program outlines the steps on how to
//                     execute device-specific functions of ACR128
//
//  Author:	           Jose Isagani R. Mission
//
//  Date:	             May 19, 2008
//
//  Revision Trail:   (Date/Author/Description)
//
//======================================================================
unit DevProgMain;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ACSModule, StdCtrls, ComCtrls, ExtCtrls;

const MAX_BUFFER_LEN    = 256;
const INVALID_SW1SW2    = -450;

type
  TMainDevProg = class(TForm)
    bQuit: TButton;
    bReset: TButton;
    bInit: TButton;
    cbReader: TComboBox;
    mMsg: TRichEdit;
    Label1: TLabel;
    bGetFW: TButton;
    rgLED: TRadioGroup;
    bConnect: TButton;
    bGetLedSet: TButton;
    bSetLedSet: TButton;
    gbBuzz: TGroupBox;
    Label2: TLabel;
    tBuzzDur: TEdit;
    bSetBuzzDur: TButton;
    gbBuzzState: TGroupBox;
    bGetBuzzState: TButton;
    bSetBuzzState: TButton;
    cbBuzzLed1: TCheckBox;
    cbBuzzLed2: TCheckBox;
    cbBuzzLed3: TCheckBox;
    cbBuzzLed4: TCheckBox;
    cbBuzzLed5: TCheckBox;
    cbBuzzLed6: TCheckBox;
    cbBuzzLed7: TCheckBox;
    cbBuzzLed8: TCheckBox;
    bClear: TButton;
    cbRed: TCheckBox;
    cbGreen: TCheckBox;
    bStartBuzz: TButton;
    bStopBuzz: TButton;
    procedure bQuitClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure bInitClick(Sender: TObject);
    procedure bGetFWClick(Sender: TObject);
    procedure bConnectClick(Sender: TObject);
    procedure bResetClick(Sender: TObject);
    procedure bGetLedSetClick(Sender: TObject);
    procedure bSetLedSetClick(Sender: TObject);
    procedure tBuzzDurKeyPress(Sender: TObject; var Key: Char);
    procedure bSetBuzzDurClick(Sender: TObject);
    procedure bGetBuzzStateClick(Sender: TObject);
    procedure bSetBuzzStateClick(Sender: TObject);
    procedure bClearClick(Sender: TObject);
    procedure bStartBuzzClick(Sender: TObject);
    procedure bStopBuzzClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  MainDevProg: TMainDevProg;
  hContext            : SCARDCONTEXT;
  hCard               : SCARDCONTEXT;
  ioRequest           : SCARD_IO_REQUEST;
  retCode             : Integer;
  dwActProtocol, BufferLen  : DWORD;
  SendBuff, RecvBuff        : array [0..262] of Byte;
  SendLen, RecvLen, nBytesRet : DWORD;
  Buffer      : array [0..MAX_BUFFER_LEN] of char;
  connActive  : Boolean;

procedure InitMenu();
procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
procedure EnableButtons();
procedure ClearBuffers();
function CallCardControl(): integer;
function SendAPDUandDisplay(SendType: integer): integer;
function GetLEDState(): integer;
function GetBuzzLEDState(): integer;


implementation

{$R *.dfm}

procedure InitMenu();
begin

  connActive := False;
  MainDevProg.cbReader.Clear;
  MainDevProg.mMsg.Clear;
  DisplayOut(0, 0, 'Program ready');
  MainDevProg.bConnect.Enabled := False;
  MainDevProg.bInit.Enabled := True;
  MainDevProg.bReset.Enabled := False;
  MainDevProg.bGetFW.Enabled := False;
  MainDevProg.cbRed.Checked := False;
  MainDevProg.cbGreen.Checked := False;
  MainDevProg.cbBuzzLed1.Checked := False;
  MainDevProg.cbBuzzLed2.Checked := False;
  MainDevProg.cbBuzzLed3.Checked := False;
  MainDevProg.cbBuzzLed4.Checked := False;
  MainDevProg.cbBuzzLed5.Checked := False;
  MainDevProg.cbBuzzLed6.Checked := False;
  MainDevProg.cbBuzzLed7.Checked := False;
  MainDevProg.cbBuzzLed8.Checked := False;
  MainDevProg.gbBuzzState.Enabled := False;
  MainDevProg.tBuzzDur.Text := '';
  MainDevProg.gbBuzz.Enabled := False;
  MainDevProg.bGetLedSet.Enabled := False;
  MainDevProg.bSetLedSet.Enabled := False;
  
end;

procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
begin

  case errType of
    0: MainDevProg.mMsg.SelAttributes.Color := clTeal;      // Notifications
    1: begin                                                // Error Messages
         MainDevProg.mMsg.SelAttributes.Color := clRed;
         PrintText := GetScardErrMsg(retVal);
       end;
    2: begin
         MainDevProg.mMsg.SelAttributes.Color := clBlack;
         PrintText := '< ' + PrintText;                      // Input data
       end;
    3: begin
         MainDevProg.mMsg.SelAttributes.Color := clBlack;
         PrintText := '> ' + PrintText;                      // Output data
       end;
    4: MainDevProg.mMsg.SelAttributes.Color := clRed;        // For ACOS1 error
  end;
  MainDevProg.mMsg.Lines.Add(PrintText);
  MainDevProg.mMsg.SelAttributes.Color := clBlack;
  MainDevProg.mMsg.SetFocus;

end;

procedure EnableButtons();
begin

  MainDevProg.bInit.Enabled := False;
  MainDevProg.bConnect.Enabled := True;
  MainDevProg.bReset.Enabled := True;
  MainDevProg.rgLED.Enabled := True;
  MainDevProg.rgLED.ItemIndex := 0;

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

function GetLEDState(): integer;
begin

  ClearBuffers();
  SendBuff[0] := $29;
  SendBuff[1] := $00;
  SendLen := 2;
  RecvLen := 6;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then begin
    GetLEDState := retCode;
    Exit;
  end;
  // Interpret LED data
  case RecvBuff[5] of
    0: begin
         DisplayOut(3, 0, 'Currently connected to SAM reader interface.');
         MainDevProg.rgLED.ItemIndex := 0;
       end;
    1: begin
         DisplayOut(3, 0, 'No PICC found.');
         MainDevProg.rgLED.ItemIndex := 0;
       end;
    2: begin
         DisplayOut(3, 0, 'PICC is present but not activated.');
         MainDevProg.rgLED.ItemIndex := 0;
       end;
    3: begin
         DisplayOut(3, 0, 'PICC is present and activated.');
         MainDevProg.rgLED.ItemIndex := 0;
       end;
    4: begin
         DisplayOut(3, 0, 'PICC is present and activated.');
         MainDevProg.rgLED.ItemIndex := 0;
       end;
    5: begin
         DisplayOut(3, 0, 'ICC is present and activated.');
         MainDevProg.rgLED.ItemIndex := 1;
       end;
    6: begin
         DisplayOut(3, 0, 'ICC is absent or not activated.');
         MainDevProg.rgLED.ItemIndex := 1;
       end;
    7: begin
         DisplayOut(3, 0, 'ICC is operating.');
         MainDevProg.rgLED.ItemIndex := 1;
       end;
  end;
  if (RecvBuff[5] and $02) <> 0 then
    MainDevProg.cbGreen.Checked := True
  else
    MainDevProg.cbGreen.Checked := False;
  if (RecvBuff[5] and $01) <> 0 then
    MainDevProg.cbRed.Checked := True
  else
    MainDevProg.cbRed.Checked := False;

  GetLEDState := retCode;

end;

function GetBuzzLEDState(): integer;
begin

  ClearBuffers();
  SendBuff[0] := $21;
  SendBuff[1] := $00;
  SendLen := 2;
  RecvLen := 6;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then begin
    GetBuzzLEDState := retCode;
    Exit;
  end;

  // Interpret Buzzer State data
  if (RecvBuff[5] and $01) <> 0 then begin
    DisplayOut(3, 0, 'ICC Activation Status LED is enabled.');
    MainDevProg.cbBuzzLed1.Checked := True;
  end
  else begin
    DisplayOut(3, 0, 'ICC Activation Status LED is disabled.');
    MainDevProg.cbBuzzLed1.Checked := False;
  end;
  if (RecvBuff[5] and $02) <> 0 then begin
    DisplayOut(3, 0, 'PICC Polling Status LED is enabled.');
    MainDevProg.cbBuzzLed2.Checked := True;
  end
  else begin
    DisplayOut(3, 0, 'PICC Polling Status LED is disabled.');
    MainDevProg.cbBuzzLed2.Checked := False;
  end;
  if (RecvBuff[5] and $04) <> 0 then begin
    DisplayOut(3, 0, 'PICC Activation Status Buzzer is enabled.');
    MainDevProg.cbBuzzLed3.Checked := True;
  end
  else begin
    DisplayOut(3, 0, 'PICC Activation Status Buzzer is disabled.');
    MainDevProg.cbBuzzLed3.Checked := False;
  end;
  if (RecvBuff[5] and $08) <> 0 then begin
    DisplayOut(3, 0, 'PICC PPS Status Buzzer is enabled.');
    MainDevProg.cbBuzzLed4.Checked := True;
  end
  else begin
    DisplayOut(3, 0, 'PICC PPS Status Buzzer is disabled.');
    MainDevProg.cbBuzzLed4.Checked := False;
  end;
  if (RecvBuff[5] and $10) <> 0 then begin
    DisplayOut(3, 0, 'Card Insertion and Removal Events Buzzer is enabled.');
    MainDevProg.cbBuzzLed5.Checked := True;
  end
  else begin
    DisplayOut(3, 0, 'Card Insertion and Removal Events Buzzer is disabled.');
    MainDevProg.cbBuzzLed5.Checked := False;
  end;
  if (RecvBuff[5] and $20) <> 0 then begin
    DisplayOut(3, 0, 'RC531 Reset Indication Buzzer is enabled.');
    MainDevProg.cbBuzzLed6.Checked := True;
  end
  else begin
    DisplayOut(3, 0, 'RC531 Reset Indication Buzzer is disabled.');
    MainDevProg.cbBuzzLed6.Checked := False;
  end;
  if (RecvBuff[5] and $40) <> 0 then begin
    DisplayOut(3, 0, 'Exclusive Mode Status Buzzer is enabled.');
    MainDevProg.cbBuzzLed7.Checked := True;
  end
  else begin
    DisplayOut(3, 0, 'Exclusive Mode Status Buzzer is disabled.');
    MainDevProg.cbBuzzLed7.Checked := False;
  end;
  if (RecvBuff[5] and $80) <> 0 then begin
    DisplayOut(3, 0, 'Card Operation Blinking LED is enabled.');
    MainDevProg.cbBuzzLed8.Checked := True;
  end
  else begin
    DisplayOut(3, 0, 'Card Operation Blinking LED is disabled.');
    MainDevProg.cbBuzzLed8.Checked := False;
  end;
  GetBuzzLEDState := retCode;
  
end;

procedure TMainDevProg.bQuitClick(Sender: TObject);
begin
  Application.Terminate;
end;

procedure TMainDevProg.FormActivate(Sender: TObject);
begin
  InitMenu();
end;

procedure TMainDevProg.bInitClick(Sender: TObject);
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


  EnableButtons();
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

procedure TMainDevProg.bGetFWClick(Sender: TObject);
var tmpStr: string;
    indx: integer;
begin

  ClearBuffers();
  SendBuff[0] := $18;
  SendBuff[1] := $00;
  SendLen := 2;
  RecvLen := 35;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  // Interpret Firmware data
  tmpStr := 'Firmware version: ';
  for indx := 5 to 24 do
    if (RecvBuff[indx] <> $00) then
      tmpStr := tmpStr + chr(RecvBuff[indx]);
  DisplayOut(3, 0, tmpStr);

end;

procedure TMainDevProg.bConnectClick(Sender: TObject);
begin

  MainDevProg.bGetFW.Enabled := False;

  if connActive then
    retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);

  // 1. Shared Connection
  retCode := SCardConnectA(hContext,
                           PChar(cbReader.Text),
                           SCARD_SHARE_SHARED,
                           SCARD_PROTOCOL_T0 or SCARD_PROTOCOL_T1,
                           @hCard,
                           @dwActProtocol);
  if retCode <> SCARD_S_SUCCESS then begin
    // check if ACR128 SAM is used and use Direct Mode if SAM is not detected
    if AnsiPos('ACR128U SAM', cbReader.Text) > 0 then begin
      // 1. Direct Connection
      retCode := SCardConnectA(hContext,
                               PChar(cbReader.Text),
                               SCARD_SHARE_DIRECT,
                               0,
                               @hCard,
                               @dwActProtocol);
      if retCode <> SCARD_S_SUCCESS then begin
        DisplayOut(1, retCode, '');
        connActive := False;
        Exit;
      end
      else
        DisplayOut(0, 0, 'Successful connection to ' + cbReader.Text);
    end
    else begin
      DisplayOut(1, retCode, '');
      connActive := False;
      Exit;
    end;
  end
  else
    DisplayOut(0, 0, 'Successful connection to ' + cbReader.Text);

  connActive := True;
  MainDevProg.bGetFW.Enabled := True;
  MainDevProg.rgLED.Enabled := True;
  MainDevProg.gbBuzzState.Enabled := True;
  MainDevProg.gbBuzz.Enabled := True;
  MainDevProg.bGetLedSet.Enabled := True;
  MainDevProg.bSetLedSet.Enabled := True;
  GetLEDState();
  GetBuzzLEDState();

end;

procedure TMainDevProg.bResetClick(Sender: TObject);
begin

  if connActive then
    retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
  retCode := SCardReleaseContext(hCard);
  InitMenu();

end;

procedure TMainDevProg.bGetLedSetClick(Sender: TObject);
begin

  GetLEDState();

end;

procedure TMainDevProg.bSetLedSetClick(Sender: TObject);
begin

  ClearBuffers();
  SendBuff[0] := $29;
  SendBuff[1] := $01;
  if cbRed.Checked then
    SendBuff[2] := SendBuff[2] or $01;
  if cbGreen.Checked then
    SendBuff[2] := SendBuff[2] or $02;
  SendLen := 3;
  RecvLen := 6;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

procedure TMainDevProg.tBuzzDurKeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then
    if Not (Key in ['0'..'9'])then
      Key := Chr($00);
end;

procedure TMainDevProg.bSetBuzzDurClick(Sender: TObject);
begin

  if tBuzzDur.Text = '' then begin
    tBuzzDur.Text := '1';
    tBuzzDur.SelectAll;
    tBuzzDur.SetFocus;
  end;
  if StrToInt(tBuzzDur.Text) >255 then begin
    tBuzzDur.Text := '255';
    tBuzzDur.SelectAll;
    tBuzzDur.SetFocus;
    Exit;
  end;

  ClearBuffers();
  SendBuff[0] := $28;
  SendBuff[1] := $01;
  SendBuff[2] := StrtoInt(tBuzzDur.Text);
  SendLen := 3;
  RecvLen := 6;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

procedure TMainDevProg.bGetBuzzStateClick(Sender: TObject);
begin

  GetBuzzLEDState();

end;

procedure TMainDevProg.bSetBuzzStateClick(Sender: TObject);
begin

  ClearBuffers();
  SendBuff[0] := $21;
  SendBuff[1] := $01;
  SendBuff[2] := $00;
  if cbBuzzLed1.Checked = True then
    SendBuff[2] := SendBuff[2] or $01;
  if cbBuzzLed2.Checked = True then
    SendBuff[2] := SendBuff[2] or $02;
  if cbBuzzLed3.Checked = True then
    SendBuff[2] := SendBuff[2] or $04;
  if cbBuzzLed4.Checked = True then
    SendBuff[2] := SendBuff[2] or $08;
  if cbBuzzLed5.Checked = True then
    SendBuff[2] := SendBuff[2] or $10;
  if cbBuzzLed6.Checked = True then
    SendBuff[2] := SendBuff[2] or $20;
  if cbBuzzLed7.Checked = True then
    SendBuff[2] := SendBuff[2] or $40;
  if cbBuzzLed8.Checked = True then
    SendBuff[2] := SendBuff[2] or $80;
  SendLen := 3;
  RecvLen := 6;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

procedure TMainDevProg.bClearClick(Sender: TObject);
begin

  mMsg.Clear;
  
end;

procedure TMainDevProg.bStartBuzzClick(Sender: TObject);
begin

  ClearBuffers();
  SendBuff[0] := $28;
  SendBuff[1] := $01;
  SendBuff[2] := $FF;
  SendLen := 3;
  RecvLen := 6;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

procedure TMainDevProg.bStopBuzzClick(Sender: TObject);
begin

  ClearBuffers();
  SendBuff[0] := $28;
  SendBuff[1] := $01;
  SendBuff[2] := $00;
  SendLen := 3;
  RecvLen := 6;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

end.
