//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              PICCProg.pas
//
//  Description:       This sample program outlines the steps on how to
//                     transact with other PICC cards using ACR128
//
//  Author:	           Jose Isagani R. Mission
//
//  Date:	             June 17, 2008
//
//  Revision Trail:   (Date/Author/Description)
//
//======================================================================
unit PICCProg;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ACSModule, StdCtrls, ComCtrls;

const MAX_BUFFER_LEN    = 2048;

type
  TMainPICCProg = class(TForm)
    Label1: TLabel;
    cbReader: TComboBox;
    bInit: TButton;
    bConnect: TButton;
    mMsg: TRichEdit;
    bClear: TButton;
    bReset: TButton;
    bQuit: TButton;
    gbGetData: TGroupBox;
    cbIso14443A: TCheckBox;
    bGetData: TButton;
    gbSendApdu: TGroupBox;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    tCLA: TEdit;
    tINS: TEdit;
    tP1: TEdit;
    tP2: TEdit;
    Label5: TLabel;
    Label6: TLabel;
    tLc: TEdit;
    Label7: TLabel;
    tLe: TEdit;
    Label8: TLabel;
    bSend: TButton;
    tData: TMemo;
    procedure bInitClick(Sender: TObject);
    procedure bConnectClick(Sender: TObject);
    procedure bClearClick(Sender: TObject);
    procedure bResetClick(Sender: TObject);
    procedure bQuitClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure bGetDataClick(Sender: TObject);
    procedure tCLAKeyPress(Sender: TObject; var Key: Char);
    procedure tDataKeyPress(Sender: TObject; var Key: Char);
    procedure bSendClick(Sender: TObject);
    procedure tCLAKeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  MainPICCProg: TMainPICCProg;
  hContext            : SCARDCONTEXT;
  hCard               : SCARDCONTEXT;
  ioRequest           : SCARD_IO_REQUEST;
  retCode             : Integer;
  dwActProtocol, BufferLen  : DWORD;
  SendBuff, RecvBuff        : array [0..262] of Byte;
  SendLen, RecvLen, nBytesRet : DWORD;
  Buffer      : array [0..MAX_BUFFER_LEN] of char;
  connActive, validATS  : Boolean;

procedure InitMenu();
procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
procedure EnableButtons();
procedure ClearBuffers();
function CallCardControl(): integer;
function SendAPDUandDisplay(SendType: integer): integer;
function TrimInput(TrimType: integer; StrIn: string): string;

implementation

{$R *.dfm}
procedure InitMenu();
begin

  connActive := False;
  validATS := False;
  MainPICCProg.cbReader.Clear;
  MainPICCProg.mMsg.Clear;
  DisplayOut(0, 0, 'Program ready');
  MainPICCProg.bConnect.Enabled := False;
  MainPICCProg.bInit.Enabled := True;
  MainPICCProg.bReset.Enabled := False;
  MainPICCProg.cbIso14443A.Checked := False;
  MainPICCProg.gbGetData.Enabled := False;
  MainPICCProg.tCLA.Text := '';
  MainPICCProg.tINS.Text := '';
  MainPICCProg.tP1.Text := '';
  MainPICCProg.tP2.Text := '';
  MainPICCProg.tLc.Text := '';
  MainPICCProg.tLe.Text := '';
  MainPICCProg.tData.Text := '';
  MainPICCProg.gbSendApdu.Enabled := False;

end;

procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
begin

  case errType of
    0: MainPICCProg.mMsg.SelAttributes.Color := clTeal;      // Notifications
    1: begin                                                // Error Messages
         MainPICCProg.mMsg.SelAttributes.Color := clRed;
         PrintText := GetScardErrMsg(retVal);
       end;
    2: begin
         MainPICCProg.mMsg.SelAttributes.Color := clBlack;
         PrintText := '< ' + PrintText;                      // Input data
       end;
    3: begin
         MainPICCProg.mMsg.SelAttributes.Color := clBlack;
         PrintText := '> ' + PrintText;                      // Output data
       end;
    4: MainPICCProg.mMsg.SelAttributes.Color := clRed;        // For Card error
  end;
  MainPICCProg.mMsg.Lines.Add(PrintText);
  MainPICCProg.mMsg.SelAttributes.Color := clBlack;
  MainPICCProg.mMsg.SetFocus;

end;

procedure EnableButtons();
begin

  MainPICCProg.bInit.Enabled := False;
  MainPICCProg.bConnect.Enabled := True;
  MainPICCProg.bReset.Enabled := True;

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
         DisplayOut(4, 0, 'Return bytes are not acceptable.');
       end;
    1: begin      // Display ATR after checking SW1/SW2
       for indx := (RecvLen-2) to (RecvLen-1) do
         tmpStr := tmpStr + Format('%.02X ', [(RecvBuff[indx])]);
       if (Trim(tmpStr) <> '90 00') then
         DisplayOut(4, 0, 'Return bytes are not acceptable.')
       else begin
         tmpStr := 'ATR :';
         for indx := 0 to (RecvLen-3) do
           tmpStr := tmpStr + Format('%.2X ', [(RecvBuff[Indx])]);
       end;
       end;
    2: begin      // Display all data
       for indx := 0 to (RecvLen-1) do
         tmpStr := tmpStr + Format('%.02X ', [(RecvBuff[indx])]);
       end;
    3: begin      // Interpret SW1/SW2
       for indx := (RecvLen-2) to (RecvLen-1) do
         tmpStr := tmpStr + Format('%.02X ', [(RecvBuff[indx])]);
       if (Trim(tmpStr) = '6A 81') then begin
         DisplayOut(4, 0, 'The function is not supported.');
         SendAPDUandDisplay := retCode;
         Exit;
       end;
       if (Trim(tmpStr) = '63 00') then begin
         DisplayOut(4, 0, 'The operation failed.');
         SendAPDUandDisplay := retCode;
         Exit;
       end;
       validATS := True;
       end;
     end;
    DisplayOut(3, 0, Trim(tmpStr));
  end;
  SendAPDUandDisplay := retCode;

end;

function TrimInput(TrimType: integer; StrIn: string): string;
var indx: integer;
    tmpStr: String;
begin
  tmpStr := '';
  StrIn := Trim(StrIn);
  case TrimType of
    0: begin
       for indx := 1 to length(StrIn) do
         if ((StrIn[indx] <> chr(13)) and (StrIn[indx] <> chr(10))) then
           tmpStr := tmpStr + StrIn[indx];
       end;
    1: begin
       for indx := 1 to length(StrIn) do
         if StrIn[indx] <> ' ' then
           tmpStr := tmpStr + StrIn[indx];
       end;
  end;
  TrimInput := tmpStr;
end;

procedure TMainPICCProg.bInitClick(Sender: TObject);
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
  // Look for ACR128 PICC and make it the default reader in the combobox
  for indx := 0 to cbReader.Items.Count-1 do begin
    cbReader.ItemIndex := indx;
    if AnsiPos('ACR128U PICC', cbReader.Text) > 0 then
      Exit;
  end;
  cbReader.ItemIndex := 0;

end;

procedure TMainPICCProg.bConnectClick(Sender: TObject);
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
  gbGetData.Enabled := True;
  gbSendApdu.Enabled := True;
  tCLA.SetFocus;

end;

procedure TMainPICCProg.bClearClick(Sender: TObject);
begin

  mMsg.Clear;

end;

procedure TMainPICCProg.bResetClick(Sender: TObject);
begin

  if connActive then
    retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
  retCode := SCardReleaseContext(hCard);
  InitMenu();

end;

procedure TMainPICCProg.bQuitClick(Sender: TObject);
begin
  Application.Terminate;
end;

procedure TMainPICCProg.FormActivate(Sender: TObject);
begin
  InitMenu();
end;

procedure TMainPICCProg.bGetDataClick(Sender: TObject);
var indx: integer;
    tmpStr: string;
begin

  validATS := False;
  ClearBuffers();
  SendBuff[0] := $FF;                             // CLA
  SendBuff[1] := $CA;                             // INS
  if cbIso14443A.Checked then
    SendBuff[2] := $01                            // P1 : ISO 14443 A Card
  else
    SendBuff[2] := $00;                           // P1 : Other cards
  SendBuff[3] := $00;                             // P2
  SendBuff[4] := $00;                             // Le : Full Length

  SendLen := SendBuff[4] + 5;
  RecvLen := $FF;

  retCode := SendAPDUandDisplay(3);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // Interpret and display return values
  if validATS then begin
    if cbIso14443A.Checked then
      tmpStr := 'UID :'
    else
      tmpStr := 'ATS :';
    for indx := 0 to (RecvLen-3) do
      tmpStr := tmpStr + Format('%.2X ', [(RecvBuff[Indx])]);
    DisplayOut(3, 0, Trim(tmpStr));
  end;

end;

procedure TMainPICCProg.tCLAKeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TMainPICCProg.tDataKeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then
    if Key <> chr($0D) then
      if Key <> chr($20) then begin
        if Key in ['a'..'z'] then
          Dec(Key, 32);
        if Not (Key in ['0'..'9', 'A'..'F'])then
          Key := Chr($00);
      end;
end;

procedure TMainPICCProg.bSendClick(Sender: TObject);
var tmpData: string;
    directCmd: Boolean;
    indx: integer;
begin

  directCmd := True;

  // Validate inputs

  if tCLA.Text = '' then begin
    tCLA.Text := '00';
    tCLA.SetFocus;
    Exit;
  end;

  tmpData := '';

  ClearBuffers();
  SendBuff[0] := StrToInt('$'+ tCLA.Text);        // CLA
  if tINS.Text <> '' then
    SendBuff[1] := StrToInt('$'+ tINS.Text);      // INS
  if tP1.Text <> '' then
    directCmd := False;
  if not directCmd then begin
    SendBuff[2] := StrToInt('$'+ tP1.Text);       // P1
    if tP2.Text = '' then begin
      tP2.Text := '00';                           // P2 : Ask user to confirm
      tP2.SetFocus;
      Exit;
    end
    else
      SendBuff[3] := StrToInt('$'+ tP2.Text);     // P2
    if tLc.Text <> '' then begin
      SendBuff[4] := StrToInt('$'+ tLc.Text);     // Lc
      if SendBuff[4] > 0 then begin               // Process Data In if Lc > 0
        tmpData := TrimInput(0, tData.Text);
        tmpData := TrimInput(1, tmpData);
        if SendBuff[4] > (Length(tmpData) div 2) then begin  // Check if Data In is
          tData.SetFocus;                                    // consistent with Lc value
          Exit;
        end;
        for indx :=0 to SendBuff[4]-1 do
          SendBuff[indx+5] := StrToInt('$' + copy(tmpData,(indx*2+1),2)); // Format Data In
        if tLe.Text <> '' then
          SendBuff[SendBuff[4]+5] := StrToInt('$'+ tLe.Text);             // Le
      end
      else
        if tLe.Text <> '' then
          SendBuff[5] := StrToInt('$'+ tLe.Text);                // Le

    end
    else
      if tLe.Text <> '' then
        SendBuff[4] := StrToInt('$'+ tLe.Text);                // Le
  end;

  if directCmd then begin
    if tINS.Text = '' then
      SendLen := $01
    else
      SendLen := $02;
  end
  else
    if tLc.Text = '' then begin
      if tLe.Text <> '' then
        SendLen := 5
      else
        SendLen := 4;
    end
    else
      if tLe.Text = '' then
        SendLen := SendBuff[4] + 5
      else
        SendLen := SendBuff[4] + 6;
  RecvLen := $FF;

  retCode := SendAPDUandDisplay(2);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

procedure TMainPICCProg.tCLAKeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
  if Length(tCLA.Text) > 1 then
    tINS.SetFocus;
end;

end.
