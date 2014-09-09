//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              DevProgMain.pas
//
//  Description:       This sample program outlines the steps on how to
//                     execute advanced device-specific functions of ACR128
//
//  Author:	           Jose Isagani R. Mission
//
//  Date:	             June 03, 2008
//
//  Revision Trail:   (Date/Author/Description)
//
//======================================================================
unit AdvDevProg;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, ACSModule, ExtCtrls;

const MAX_BUFFER_LEN    = 256;
const INVALID_SW1SW2    = -450;

type
  TMainAdvDevProg = class(TForm)
    Label1: TLabel;
    cbReader: TComboBox;
    bInit: TButton;
    bConnect: TButton;
    mMsg: TRichEdit;
    bClear: TButton;
    bReset: TButton;
    bQuit: TButton;
    gbFWI: TGroupBox;
    bGetFWI: TButton;
    bSetFWI: TButton;
    tFWI: TEdit;
    tPollTO: TEdit;
    tTFS: TEdit;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    gbAntenna: TGroupBox;
    bGetAS: TButton;
    bSetAS: TButton;
    rbAntOn: TRadioButton;
    rbAntOff: TRadioButton;
    gbTransSet: TGroupBox;
    tFStop: TEdit;
    tSetup: TEdit;
    Label5: TLabel;
    Label6: TLabel;
    cbFilter: TCheckBox;
    Label7: TLabel;
    tRecGain: TEdit;
    Label8: TLabel;
    tTxMode: TEdit;
    Label9: TLabel;
    bGetTranSet: TButton;
    bSetTranSet: TButton;
    gbPICC: TGroupBox;
    tPICC1: TEdit;
    tPICC2: TEdit;
    tPICC3: TEdit;
    tPICC4: TEdit;
    tPICC5: TEdit;
    tPICC6: TEdit;
    Label10: TLabel;
    Label11: TLabel;
    Label12: TLabel;
    Label13: TLabel;
    Label14: TLabel;
    Label15: TLabel;
    tPICC7: TEdit;
    tPICC8: TEdit;
    tPICC9: TEdit;
    tPICC10: TEdit;
    tPICC11: TEdit;
    tPICC12: TEdit;
    Label16: TLabel;
    Label17: TLabel;
    Label18: TLabel;
    Label19: TLabel;
    Label20: TLabel;
    Label21: TLabel;
    bGetPICC: TButton;
    bSetPICC: TButton;
    gbPolling: TGroupBox;
    rbType1: TRadioButton;
    rbType2: TRadioButton;
    rbType3: TRadioButton;
    bPoll: TButton;
    Polltimer: TTimer;
    Label22: TLabel;
    tMsg: TEdit;
    bGetPSet: TButton;
    bSetPSet: TButton;
    gbErrHand: TGroupBox;
    tPc2Pi: TEdit;
    tPi2Pc: TEdit;
    bGetEH: TButton;
    bSetEH: TButton;
    Label23: TLabel;
    Label24: TLabel;
    gbPPS: TGroupBox;
    bGetPPS: TButton;
    bSetPPS: TButton;
    rgMaxSpeed: TRadioGroup;
    rgCurrSpeed: TRadioGroup;
    gbReg: TGroupBox;
    tRegVal: TEdit;
    tRegNo: TEdit;
    Label25: TLabel;
    Label26: TLabel;
    bGetReg: TButton;
    bSetReg: TButton;
    gbRefIS: TGroupBox;
    bRefIS: TButton;
    rbRIS1: TRadioButton;
    rbRIS2: TRadioButton;
    rbRIS3: TRadioButton;
    procedure bResetClick(Sender: TObject);
    procedure bClearClick(Sender: TObject);
    procedure bQuitClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure bInitClick(Sender: TObject);
    procedure bConnectClick(Sender: TObject);
    procedure tFWIKeyPress(Sender: TObject; var Key: Char);
    procedure bGetFWIClick(Sender: TObject);
    procedure bSetFWIClick(Sender: TObject);
    procedure bGetASClick(Sender: TObject);
    procedure bSetASClick(Sender: TObject);
    procedure tFStopKeyPress(Sender: TObject; var Key: Char);
    procedure bGetTranSetClick(Sender: TObject);
    procedure bSetTranSetClick(Sender: TObject);
    procedure bGetPICCClick(Sender: TObject);
    procedure bSetPICCClick(Sender: TObject);
    procedure bPollClick(Sender: TObject);
    procedure PolltimerTimer(Sender: TObject);
    procedure bGetPSetClick(Sender: TObject);
    procedure bSetPSetClick(Sender: TObject);
    procedure bGetEHClick(Sender: TObject);
    procedure bSetEHClick(Sender: TObject);
    procedure bGetPPSClick(Sender: TObject);
    procedure bSetPPSClick(Sender: TObject);
    procedure bGetRegClick(Sender: TObject);
    procedure bSetRegClick(Sender: TObject);
    procedure bRefISClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  MainAdvDevProg: TMainAdvDevProg;
  hContext            : SCARDCONTEXT;
  hCard               : SCARDCONTEXT;
  ioRequest           : SCARD_IO_REQUEST;
  RdrState            : SCARD_READERSTATE;
  retCode             : Integer;
  Buffer              : array [0..MAX_BUFFER_LEN] of char;
  dwActProtocol, BufferLen  : DWORD;
  SendBuff, RecvBuff        : array [0..262] of Byte;
  SendLen, RecvLen, nBytesRet : DWORD;
  connActive, autoDet       : Boolean;
  reqType                   : integer;

procedure InitMenu();
procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
procedure EnableButtons();
procedure ClearBuffers();
procedure ReadPollingOption();
function CallCardControl(): integer;
function SendAPDUandDisplay(SendType: integer): integer;

implementation

{$R *.dfm}

procedure InitMenu();
begin

  connActive := False;
  autoDet := False;
  MainAdvDevProg.Polltimer.Enabled := False;
  MainAdvDevProg.cbReader.Clear;
  MainAdvDevProg.mMsg.Clear;
  DisplayOut(0, 0, 'Program ready');
  MainAdvDevProg.bConnect.Enabled := False;
  MainAdvDevProg.bInit.Enabled := True;
  MainAdvDevProg.bReset.Enabled := False;
  MainAdvDevProg.tFWI.Text := '';
  MainAdvDevProg.tPollTO.Text := '';
  MainAdvDevProg.tTFS.Text := '';
  MainAdvDevProg.gbFWI.Enabled := False;
  MainAdvDevProg.rbAntOn.Checked := False;
  MainAdvDevProg.rbAntOff.Checked := False;
  MainAdvDevProg.gbAntenna.Enabled := False;
  MainAdvDevProg.tFStop.Text := '';
  MainAdvDevProg.tSetup.Text := '';
  MainAdvDevProg.cbFilter.Checked := False;
  MainAdvDevProg.tRecGain.Text := '';
  MainAdvDevProg.gbTransSet.Enabled := False;
  MainAdvDevProg.tPICC1.Text := '';
  MainAdvDevProg.tPICC2.Text := '';
  MainAdvDevProg.tPICC3.Text := '';
  MainAdvDevProg.tPICC4.Text := '';
  MainAdvDevProg.tPICC5.Text := '';
  MainAdvDevProg.tPICC6.Text := '';
  MainAdvDevProg.tPICC7.Text := '';
  MainAdvDevProg.tPICC8.Text := '';
  MainAdvDevProg.tPICC9.Text := '';
  MainAdvDevProg.tPICC10.Text := '';
  MainAdvDevProg.tPICC11.Text := '';
  MainAdvDevProg.tPICC12.Text := '';
  MainAdvDevProg.gbPICC.Enabled := False;
  MainAdvDevProg.tMsg.Text := '';
  MainAdvDevProg.rbType1.Checked := False;
  MainAdvDevProg.rbType2.Checked := False;
  MainAdvDevProg.rbType3.Checked := False;
  MainAdvDevProg.gbPolling.Enabled := False;
  MainAdvDevProg.gbErrHand.Enabled := False;
  MainAdvDevProg.rgMaxSpeed.ItemIndex := -1;
  MainAdvDevProg.rgCurrSpeed.ItemIndex := -1;
  MainAdvDevProg.gbPPS.Enabled := False;
  MainAdvDevProg.tRegNo.Text := '';
  MainAdvDevProg.tRegVal.Text := '';
  MainAdvDevProg.gbReg.Enabled := False;
  MainAdvDevProg.rbRIS1.Checked := False;
  MainAdvDevProg.rbRIS2.Checked := False;
  MainAdvDevProg.rbRIS3.Checked := False;
  MainAdvDevProg.gbRefIS.Enabled := False;


end;

procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
begin

  case errType of
    0: MainAdvDevProg.mMsg.SelAttributes.Color := clTeal;      // Notifications
    1: begin                                                // Error Messages
         MainAdvDevProg.mMsg.SelAttributes.Color := clRed;
         PrintText := GetScardErrMsg(retVal);
       end;
    2: begin
         MainAdvDevProg.mMsg.SelAttributes.Color := clBlack;
         PrintText := '< ' + PrintText;                      // Input data
       end;
    3: begin
         MainAdvDevProg.mMsg.SelAttributes.Color := clBlack;
         PrintText := '> ' + PrintText;                      // Output data
       end;
    4: MainAdvDevProg.mMsg.SelAttributes.Color := clRed;        // For ACOS1 error
  end;
  MainAdvDevProg.mMsg.Lines.Add(PrintText);
  MainAdvDevProg.mMsg.SelAttributes.Color := clBlack;
  MainAdvDevProg.mMsg.SetFocus;

end;

procedure EnableButtons();
begin

  MainAdvDevProg.bInit.Enabled := False;
  MainAdvDevProg.bConnect.Enabled := True;
  MainAdvDevProg.bReset.Enabled := True;

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

procedure ReadPollingOption();
begin

  ClearBuffers();
  SendBuff[0] := $23;
  SendBuff[1] := $00;
  SendLen := 2;
  RecvLen := 6;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

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


procedure TMainAdvDevProg.bResetClick(Sender: TObject);
begin

  if connActive then
    retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
  retCode := SCardReleaseContext(hCard);
  InitMenu();

end;

procedure TMainAdvDevProg.bClearClick(Sender: TObject);
begin

  mMsg.Clear;

end;

procedure TMainAdvDevProg.bQuitClick(Sender: TObject);
begin
  Application.Terminate;
end;

procedure TMainAdvDevProg.FormActivate(Sender: TObject);
begin
  InitMenu();
end;

procedure TMainAdvDevProg.bInitClick(Sender: TObject);
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

procedure TMainAdvDevProg.bConnectClick(Sender: TObject);
begin

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
  gbFWI.Enabled := True;
  gbAntenna.Enabled := True;
  gbTransSet.Enabled := True;
  gbPICC.Enabled := True;
  gbPolling.Enabled := True;
  rbType3.Checked := True;
  gbErrHand.Enabled := True;
  gbPPS.Enabled := True;
  gbReg.Enabled := True;
  gbRefIS.Enabled := True;
  rbRIS3.Checked := True;

end;

procedure TMainAdvDevProg.tFWIKeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TMainAdvDevProg.bGetFWIClick(Sender: TObject);
var tmpStr: string;
    indx: integer;
begin

  ClearBuffers();
  SendBuff[0] := $1F;
  SendBuff[1] := $00;
  SendLen := 2;
  RecvLen := 8;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  tmpStr := '';
  for indx := 0 to 4 do
    tmpStr := tmpStr + Format('%.02X', [RecvBuff[indx]]);
  if tmpStr = 'E100000003' then begin
    // Interpret response data
    tFWI.Text := Format('%.02X', [RecvBuff[5]]);
    tPollTO.Text := Format('%.02X', [RecvBuff[6]]);
    tTFS.Text := Format('%.02X', [RecvBuff[7]]);
  end
  else begin
    tFWI.Text := '';
    tPollTO.Text := '';
    tTFS.Text := '';
    DisplayOut(3, 0, 'Invalid response');
  end;

end;

procedure TMainAdvDevProg.bSetFWIClick(Sender: TObject);
var tmpStr: string;
    indx: integer;
begin

  if tFWI.Text = '' then begin
    tFWI.SetFocus;
    Exit;
  end;
  if tPollTO.Text = '' then begin
    tPollTO.SetFocus;
    Exit;
  end;
  if tTFS.Text = '' then begin
    tTFS.SetFocus;
    Exit;
  end;

  ClearBuffers();
  SendBuff[0] := $1F;
  SendBuff[1] := $03;
  SendBuff[2] := StrToInt('$' + tFWI.Text);
  SendBuff[3] := StrToInt('$' + tPollTO.Text);
  SendBuff[4] := StrToInt('$' + tTFS.Text);
  SendLen := 5;
  RecvLen := 8;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  tmpStr := '';
  for indx := 0 to 4 do
    tmpStr := tmpStr + Format('%.02X', [RecvBuff[indx]]);
  if tmpStr <> 'E100000003' then
    DisplayOut(3, 0, 'Invalid response');

end;

procedure TMainAdvDevProg.bGetASClick(Sender: TObject);
var tmpStr: string;
    indx: integer;
begin

  ClearBuffers();
  SendBuff[0] := $25;
  SendBuff[1] := $00;
  SendLen := 2;
  RecvLen := 6;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  tmpStr := '';
  for indx := 0 to 4 do
    tmpStr := tmpStr + Format('%.02X', [RecvBuff[indx]]);
  if tmpStr = 'E100000001' then begin
    // Interpret response data
    if RecvBuff[05] = 00 then
      rbAntOff.Checked := True
    else
      rbAntOn.Checked := True;
  end
  else begin
    rbAntOff.Checked := False;
    rbAntOn.Checked := False;
    DisplayOut(3, 0, 'Invalid response');
  end;

end;

procedure TMainAdvDevProg.bSetASClick(Sender: TObject);
var tmpStr: string;
    indx: integer;
begin

  ReadPollingOption();
  if (RecvBuff[5] and $01) <> 0 then begin
    DisplayOut(0, 0, 'Turn off automatic PICC polling in the device before using this function.');
    Exit;
  end;
  ClearBuffers();
  SendBuff[0] := $25;
  SendBuff[1] := $01;
  if rbAntOn.Checked then
    SendBuff[2] := $01
  else
    if rbAntOff.Checked then
      SendBuff[2] := $00
    else begin
      rbAntOn.SetFocus;
      Exit;
    end;

  SendLen := 3;
  RecvLen := 6;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  tmpStr := '';
  for indx := 0 to 4 do
    tmpStr := tmpStr + Format('%.02X', [RecvBuff[indx]]);
  if tmpStr <> 'E100000001' then
    DisplayOut(3, 0, 'Invalid response');

end;

procedure TMainAdvDevProg.tFStopKeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then
    if Not (Key in ['0'..'9'])then
      Key := Chr($00);
end;

procedure TMainAdvDevProg.bGetTranSetClick(Sender: TObject);
var tmpStr: string;
    indx: integer;
    tmpVal: integer;
begin

  ClearBuffers();
  SendBuff[0] := $20;
  SendBuff[1] := $01;
  SendLen := 2;
  RecvLen := 9;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  tmpStr := '';
  for indx := 0 to 5 do
    tmpStr := tmpStr + Format('%.02X', [RecvBuff[indx]]);
  if tmpStr = 'E10000000406' then begin
    // Interpret response data
    tmpVal := RecvBuff[6] shr 4;
    tFStop.Text := InttoStr(tmpVal);
    tmpVal := RecvBuff[6] and $0F;
    tSetup.Text := InttoStr(tmpVal);
    if (RecvBuff[7] and $04) <> 0 then
      cbFilter.Checked := True
    else
      cbFilter.Checked := False;
    tmpVal := RecvBuff[7] and $03;
    tRecGain.Text := InttoStr(tmpVal);
    tTxMode.Text := Format('%.02X', [RecvBuff[8]]);
  end
  else begin
    tFStop.Text := '';
    tSetup.Text := '';
    cbFilter.Checked := False;
    tRecGain.Text := '';
    tTxMode.Text := '';
    DisplayOut(3, 0, 'Invalid response');
  end;

end;

procedure TMainAdvDevProg.bSetTranSetClick(Sender: TObject);
begin

  if tFStop.Text = '' then begin
    tFStop.SetFocus;
    Exit;
  end;
  if tSetup.Text = '' then begin
    tSetup.SetFocus;
    Exit;
  end;
  if tRecGain.Text = '' then begin
    tRecGain.SetFocus;
    Exit;
  end;
  if tTxMode.Text = '' then begin
    tTxMode.SetFocus;
    Exit;
  end;
  if StrToInt(tFStop.Text) > 15 then begin
    tFStop.Text := '15';
    tFStop.SetFocus;
    Exit;
  end;
  if StrToInt(tSetup.Text) > 15 then begin
    tSetup.Text := '15';
    tSetup.SetFocus;
    Exit;
  end;
  if StrToInt(tRecGain.Text) > 3 then begin
    tRecGain.Text := '3';
    tRecGain.SetFocus;
    Exit;
  end;

  ClearBuffers();
  SendBuff[0] := $20;
  SendBuff[1] := $04;
  SendBuff[2] := $06;
  SendBuff[3] := StrToInt(tFStop.Text) shl 4;
  SendBuff[3] := SendBuff[3] + StrToInt(tSetup.Text);
  if cbFilter.Checked then
    SendBuff[4] := $04;
  SendBuff[4] := SendBuff[4] + StrToInt(tRecGain.Text);
  SendBuff[5] := StrToInt('$' + tTxMode.Text);
  SendLen := 6;
  RecvLen := 5;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  if RecvBuff[0] <> $E1 then
    DisplayOut(3, 0, 'Invalid response');

end;

procedure TMainAdvDevProg.bGetPICCClick(Sender: TObject);
var tmpStr: string;
    indx: integer;
begin

  ClearBuffers();
  SendBuff[0] := $2A;
  SendBuff[1] := $00;
  SendLen := 2;
  RecvLen := 17;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  tmpStr := '';
  for indx := 0 to 4 do
    tmpStr := tmpStr + Format('%.02X', [RecvBuff[indx]]);
  if tmpStr = 'E10000000C' then begin
    // Interpret response data
    tPICC1.Text := Format('%.02X', [RecvBuff[5]]);
    tPICC2.Text := Format('%.02X', [RecvBuff[6]]);
    tPICC3.Text := Format('%.02X', [RecvBuff[7]]);
    tPICC4.Text := Format('%.02X', [RecvBuff[8]]);
    tPICC5.Text := Format('%.02X', [RecvBuff[9]]);
    tPICC6.Text := Format('%.02X', [RecvBuff[10]]);
    tPICC7.Text := Format('%.02X', [RecvBuff[11]]);
    tPICC8.Text := Format('%.02X', [RecvBuff[12]]);
    tPICC9.Text := Format('%.02X', [RecvBuff[13]]);
    tPICC10.Text := Format('%.02X', [RecvBuff[14]]);
    tPICC11.Text := Format('%.02X', [RecvBuff[15]]);
    tPICC12.Text := Format('%.02X', [RecvBuff[16]]);
  end
  else begin
    tPICC1.Text :=  '';
    tPICC2.Text :=  '';
    tPICC3.Text :=  '';
    tPICC4.Text :=  '';
    tPICC5.Text :=  '';
    tPICC6.Text :=  '';
    tPICC7.Text :=  '';
    tPICC8.Text :=  '';
    tPICC9.Text :=  '';
    tPICC10.Text :=  '';
    tPICC11.Text :=  '';
    tPICC12.Text :=  '';
    DisplayOut(3, 0, 'Invalid response');
  end;
end;

procedure TMainAdvDevProg.bSetPICCClick(Sender: TObject);
var tmpStr: string;
    indx: integer;
begin

  if tPICC1.Text = '' then begin
    tPICC1.SetFocus;
    Exit;
  end;
  if tPICC2.Text = '' then begin
    tPICC2.SetFocus;
    Exit;
  end;
  if tPICC3.Text = '' then begin
    tPICC3.SetFocus;
    Exit;
  end;
  if tPICC4.Text = '' then begin
    tPICC4.SetFocus;
    Exit;
  end;
  if tPICC5.Text = '' then begin
    tPICC5.SetFocus;
    Exit;
  end;
  if tPICC6.Text = '' then begin
    tPICC6.SetFocus;
    Exit;
  end;
  if tPICC7.Text = '' then begin
    tPICC7.SetFocus;
    Exit;
  end;
  if tPICC8.Text = '' then begin
    tPICC8.SetFocus;
    Exit;
  end;
  if tPICC9.Text = '' then begin
    tPICC9.SetFocus;
    Exit;
  end;
  if tPICC10.Text = '' then begin
    tPICC10.SetFocus;
    Exit;
  end;
  if tPICC11.Text = '' then begin
    tPICC11.SetFocus;
    Exit;
  end;
  if tPICC12.Text = '' then begin
    tPICC12.SetFocus;
    Exit;
  end;

  ClearBuffers();
  SendBuff[0] := $2A;
  SendBuff[1] := $0C;
  SendBuff[2] := StrToInt('$' + tPICC1.Text);
  SendBuff[3] := StrToInt('$' + tPICC2.Text);
  SendBuff[4] := StrToInt('$' + tPICC3.Text);
  SendBuff[5] := StrToInt('$' + tPICC4.Text);
  SendBuff[6] := StrToInt('$' + tPICC5.Text);
  SendBuff[7] := StrToInt('$' + tPICC6.Text);
  SendBuff[8] := StrToInt('$' + tPICC7.Text);
  SendBuff[9] := StrToInt('$' + tPICC8.Text);
  SendBuff[10] := StrToInt('$' + tPICC9.Text);
  SendBuff[11] := StrToInt('$' + tPICC10.Text);
  SendBuff[12] := StrToInt('$' + tPICC11.Text);
  SendBuff[13] := StrToInt('$' + tPICC12.Text);
  SendLen := 14;
  RecvLen := 17;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  for indx := 0 to 4 do
    tmpStr := tmpStr + Format('%.02X', [RecvBuff[indx]]);
  if tmpStr <> 'E10000000C' then
    DisplayOut(3, 0, 'Invalid response');

end;

procedure TMainAdvDevProg.bPollClick(Sender: TObject);
begin

  if autoDet then begin
    autoDet := False;
    bPoll.Caption := 'Start Auto &Detection';
    pollTimer.Enabled := False;
    tMsg.Text := 'Polling stopped...';
    Exit;
  end;

  tMsg.Text := 'Polling started...';
  autoDet := True;
  pollTimer.Enabled := True;
  bPoll.Caption := 'End Auto &Detection';

end;

procedure TMainAdvDevProg.PolltimerTimer(Sender: TObject);
var indx: integer;
    tmpStr: string;
begin

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
    if (RdrState.dwEventStates and SCARD_STATE_PRESENT) <> 0 then begin
      case reqType of
        1: tmpStr := 'ISO14443 Type A card';
        2: tmpStr := 'ISO14443 Type B card';
      else
        tmpStr := 'ISO14443 card';
      end;
      tMsg.Text := tmpStr + ' is detected';
    end
    else
      tMsg.Text := 'No card within range.';
  end;
  Application.ProcessMessages;

end;

procedure TMainAdvDevProg.bGetPSetClick(Sender: TObject);
var indx: integer;
    tmpStr: string;
begin

  ClearBuffers();
  SendBuff[0] := $20;
  SendBuff[1] := $00;
  SendBuff[3] := $FF;
  SendLen := 4;
  RecvLen := 6;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  tmpStr := '';
  for indx := 0 to 4 do
    tmpStr := tmpStr + Format('%.02X', [RecvBuff[indx]]);
  if tmpStr <> 'E100000001' then begin
    tMsg.Text := 'Invalid card detected';
    Exit;
  end;
  // interpret status
  case RecvBuff[5] of
    1: rbType1.Checked := True;
    2: rbType2.Checked := True;
  else
    rbType3.Checked := True;
  end;

end;

procedure TMainAdvDevProg.bSetPSetClick(Sender: TObject);
begin

  if rbType1.Checked then
    reqType := 1
  else
    if rbType2.Checked then
      reqType := 2
    else
      if rbType3.Checked then
        reqType := 3
      else begin
        rbType1.SetFocus;
        Exit;
      end;

  ClearBuffers();
  SendBuff[0] := $20;
  SendBuff[1] := $02;
  case reqType of
    1: SendBuff[2] := $01;
    2: SendBuff[2] := $02;
  else
    SendBuff[2] := $03;
  end;
  SendBuff[3] := $FF;
  SendLen := 4;
  RecvLen := 5;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;
end;

procedure TMainAdvDevProg.bGetEHClick(Sender: TObject);
var tmpStr: string;
    indx: integer;
    tmpVal: integer;
begin

  ClearBuffers();
  SendBuff[0] := $2C;
  SendBuff[1] := $00;
  SendLen := 2;
  RecvLen := 7;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  tmpStr := '';
  for indx := 0 to 4 do
    tmpStr := tmpStr + Format('%.02X', [RecvBuff[indx]]);
  if (tmpStr = 'E100000002') and (RecvBuff[6] = $7F) then begin
    // Interpret response data
    tmpVal := RecvBuff[5] shr 4;
    tPc2Pi.Text := InttoStr(tmpVal);
    tmpVal := RecvBuff[5] and $03;
    tPi2Pc.Text := InttoStr(tmpVal);
  end
  else begin
    tPc2Pi.Text := '';
    tPi2Pc.Text := '';
    DisplayOut(3, 0, 'Invalid response');
  end;

end;

procedure TMainAdvDevProg.bSetEHClick(Sender: TObject);
var tmpStr: string;
    indx: integer;
begin

  if tPc2Pi.Text = '' then begin
    tPc2Pi.SetFocus;
    Exit;
  end;
  if StrToInt(tPc2Pi.Text) > 3 then begin
    tPc2Pi.Text := '3';
    tPc2Pi.SetFocus;
    Exit;
  end;
  if tPi2Pc.Text = '' then begin
    tPi2Pc.SetFocus;
    Exit;
  end;
  if StrToInt(tPi2Pc.Text) > 3 then begin
    tPi2Pc.Text := '3';
    tPi2Pc.SetFocus;
    Exit;
  end;

  ClearBuffers();
  SendBuff[0] := $2C;
  SendBuff[1] := $02;
  SendBuff[2] := StrToInt(tPc2Pi.Text) shl 4;
  SendBuff[2] := SendBuff[2] + StrToInt(tPi2Pc.Text);
  SendBuff[3] := $7F;
  SendLen := 4;
  RecvLen := 7;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';
  for indx := 0 to 4 do
    tmpStr := tmpStr + Format('%.02X', [RecvBuff[indx]]);
  if tmpStr <> 'E100000002' then
    DisplayOut(3, 0, 'Invalid response');

end;

procedure TMainAdvDevProg.bGetPPSClick(Sender: TObject);
var tmpStr: string;
    indx: integer;
begin

  ClearBuffers();
  SendBuff[0] := $24;
  SendBuff[1] := $00;
  SendLen := 2;
  RecvLen := 7;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  tmpStr := '';
  for indx := 0 to 4 do
    tmpStr := tmpStr + Format('%.02X', [RecvBuff[indx]]);
  if tmpStr = 'E100000002' then begin
    // Interpret response data
    case RecvBuff[5] of
      0: rgMaxSpeed.ItemIndex := 0;
      1: rgMaxSpeed.ItemIndex := 1;
      2: rgMaxSpeed.ItemIndex := 2;
      3: rgMaxSpeed.ItemIndex := 3;
    else
      rgMaxSpeed.ItemIndex := 4;
    end;
    case RecvBuff[6] of
      0: rgCurrSpeed.ItemIndex := 0;
      1: rgCurrSpeed.ItemIndex := 1;
      2: rgCurrSpeed.ItemIndex := 2;
      3: rgCurrSpeed.ItemIndex := 3;
    else
      rgCurrSpeed.ItemIndex := 4;
    end;
  end
  else begin
    rgMaxSpeed.ItemIndex := -1;
    rgCurrSpeed.ItemIndex := -1;
    DisplayOut(3, 0, 'Invalid response');
  end;

end;

procedure TMainAdvDevProg.bSetPPSClick(Sender: TObject);
var tmpStr: string;
    indx: integer;
begin

  if rgMaxSpeed.ItemIndex = -1 then
    rgMaxSpeed.ItemIndex := 4;
  if rgCurrSpeed.ItemIndex = -1 then
    rgCurrSpeed.ItemIndex := 4;

  ClearBuffers();
  SendBuff[0] := $24;
  SendBuff[1] := $01;
  case rgMaxSpeed.ItemIndex of
    0: SendBuff[2] := $00;
    1: SendBuff[2] := $01;
    2: SendBuff[2] := $02;
    3: SendBuff[2] := $03;
  else
    SendBuff[2] := $FF;
  end;
  SendLen := 3;
  RecvLen := 7;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  tmpStr := '';
  for indx := 0 to 4 do
    tmpStr := tmpStr + Format('%.02X', [RecvBuff[indx]]);
  if tmpStr <> 'E100000002' then
    DisplayOut(3, 0, 'Invalid response');

end;

procedure TMainAdvDevProg.bGetRegClick(Sender: TObject);
var tmpStr: string;
    indx: integer;
begin

  if tRegNo.Text = '' then begin
    tRegNo.SetFocus;
    Exit;
  end;

  ClearBuffers();
  SendBuff[0] := $19;
  SendBuff[1] := $01;
  SendBuff[2] := StrToInt('$' + tRegNo.Text);
  SendLen := 3;
  RecvLen := 6;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  tmpStr := '';
  for indx := 0 to 4 do
    tmpStr := tmpStr + Format('%.02X', [RecvBuff[indx]]);
  if tmpStr = 'E100000001' then begin
    // Interpret response data
    tRegVal.Text := Format('%.02X', [RecvBuff[5]]);
    
  end
  else begin
    tRegVal.Text := '';
    DisplayOut(3, 0, 'Invalid response');
  end;

end;

procedure TMainAdvDevProg.bSetRegClick(Sender: TObject);
var tmpStr: string;
    indx: integer;
begin

  if tRegNo.Text = '' then begin
    tRegNo.SetFocus;
    Exit;
  end;
  if tRegVal.Text = '' then begin
    tRegVal.SetFocus;
    Exit;
  end;

  ClearBuffers();
  SendBuff[0] := $1A;
  SendBuff[1] := $02;
  SendBuff[2] := StrToInt('$' + tRegNo.Text);
  SendBuff[3] := StrToInt('$' + tRegVal.Text);
  SendLen := 4;
  RecvLen := 6;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  tmpStr := '';
  for indx := 0 to 4 do
    tmpStr := tmpStr + Format('%.02X', [RecvBuff[indx]]);
  if tmpStr = 'E100000001' then
    // Interpret response data

    tRegVal.Text := Format('%.02X', [RecvBuff[5]])
  else begin
    tRegNo.Text := '';
    tRegVal.Text := '';
    DisplayOut(3, 0, 'Invalid response');
  end;

end;

procedure TMainAdvDevProg.bRefISClick(Sender: TObject);
var tmpStr: string;
    indx: integer;
begin


  if connActive then
    retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
  indx := 0;
  cbReader.ItemIndex := indx;
  while AnsiPos('ACR128U SAM', cbReader.Text) = 0 do begin
    if indx = cbReader.Items.Count then begin
      DisplayOut(0, 0, 'Cannot find ACR128 SAM reader.');
      connActive := False;
      Exit;
    end;
    inc(indx);
    cbReader.ItemIndex := indx;
  end;
  // 1. For SAM Refresh, connect to SAM Interface in direct mode
  if rbRIS3.Checked then begin
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
  // 2. For other interfaces, connect to SAM Interface in direct or shared mode
    retCode := SCardConnectA(hContext,
                             PChar(cbReader.Text),
                             SCARD_SHARE_DIRECT or SCARD_SHARE_SHARED,
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
  end;

  ClearBuffers();
  SendBuff[0] := $2D;
  SendBuff[1] := $01;
  if rbRIS1.Checked then
    SendBuff[2] := $01         // bit0
  else
    if rbRIS2.Checked then
      SendBuff[2] := $02       // bit1
    else
      SendBuff[2] := $04;      // bit 2

  SendLen := 3;
  RecvLen := 6;
  retCode := CallCardControl();
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';
  for indx := 0 to 4 do
    tmpStr := tmpStr + Format('%.02X', [RecvBuff[indx]]);
  if tmpStr <> 'E100000001' then begin
    // Interpret response data
    DisplayOut(3, 0, 'Invalid response');
    Exit;
  end;

  // 3. For SAM interface, disconnect and connect to SAM Interface in direct or shared mode
  if rbRIS3.Checked then begin
    if connActive then
      retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
    indx := 0;
    cbReader.ItemIndex := indx;
    while AnsiPos('ACR128U SAM', cbReader.Text) = 0 do begin
      if indx = cbReader.Items.Count then begin
        DisplayOut(0, 0, 'Cannot find ACR128 SAM reader.');
        connActive := False;
        Exit;
      end;
      inc(indx);
      cbReader.ItemIndex := indx;
    end;
    retCode := SCardConnectA(hContext,
                             PChar(cbReader.Text),
                             SCARD_SHARE_DIRECT or SCARD_SHARE_SHARED,
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
  end;

end;

end.
