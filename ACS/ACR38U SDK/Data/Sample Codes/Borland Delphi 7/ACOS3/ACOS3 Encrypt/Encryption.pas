//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              Encryption.pas
//
//  Description:       This sample program outlines the steps on how to
//                     use the encryption options in ACOS card using
//                     the PC/SC platform.
//
//  Author:	           Jose Isagani R. Mission
//
//  Date:	             May 13, 2004
//
//  Revision Trail:   (Date/Author/Description)
//
//======================================================================
unit Encryption;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ACSModule, StdCtrls, ComCtrls, ExtCtrls;

Const MAX_BUFFER_LEN    = 256;
Const INVALID_SW1SW2    = -450;

type
  TMainEncrypt = class(TForm)
    mMsg: TRichEdit;
    Label1: TLabel;
    cbReader: TComboBox;
    bInit: TButton;
    bConnect: TButton;
    rgOption: TRadioGroup;
    bReset: TButton;
    bQuit: TButton;
    rgEncrypt: TRadioGroup;
    gbSubmit: TGroupBox;
    cbCode: TComboBox;
    tValue: TEdit;
    bSubmit: TButton;
    Label2: TLabel;
    Label3: TLabel;
    gbInput: TGroupBox;
    bFormat: TButton;
    Label4: TLabel;
    Label5: TLabel;
    tCard: TEdit;
    tTerminal: TEdit;
    bSet: TButton;
    procedure bQuitClick(Sender: TObject);
    procedure bResetClick(Sender: TObject);
    procedure bInitClick(Sender: TObject);
    procedure bConnectClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure cbReaderChange(Sender: TObject);
    procedure rgEncryptClick(Sender: TObject);
    procedure bFormatClick(Sender: TObject);
    procedure rgOptionClick(Sender: TObject);
    procedure bSubmitClick(Sender: TObject);
    procedure bSetClick(Sender: TObject);
    procedure tCardKeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  MainEncrypt: TMainEncrypt;
  hContext    : SCARDCONTEXT;
  hCard       : SCARDCONTEXT;
  ioRequest   : SCARD_IO_REQUEST;
  retCode     : Integer;
  dwActProtocol, BufferLen  : DWORD;
  SendBuff, RecvBuff        : array [0..262] of Byte;
  SendLen, RecvLen          : DWORD;
  Buffer      : array [0..MAX_BUFFER_LEN] of char;
  SessionKey  : array [0..15] of Byte;
  cKey        : array [0..15] of Byte;
  tKey        : array [0..15] of Byte;
  ConnActive  : Boolean;

procedure Decrypt(data : PByte; Key : PByte); cdecl; external 'des.dll';
procedure Encrypt(data : PByte; Key : PByte); cdecl; external 'des.dll';
procedure DES (data : PByte; Key : PByte);
procedure TripleDES (data : PByte; Key : PBYTE);
procedure MAC (data : PByte; Key : PBYTE);
procedure TripleMAC (data : PByte; Key : PBYTE);

procedure ClearBuffers();
procedure ClearTextFields();
procedure InitMenu();
procedure AddButtons();
procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
function SendAPDUandDisplay(SendType: integer; ApduIn: string): integer;
function SubmitIC(): integer;
function selectFile(HiAddr: Byte; LoAddr: Byte): integer;
function writeRecord(caseType: Integer; RecNo: Byte; maxDataLen: Byte; DataLen: Byte; DataIn:array of Byte): integer;
function readRecord(RecNo: Byte; DataLen: Byte): integer;
function CheckACOS(): Boolean;
function ValidTemplate(validationType: integer): Boolean;
function ACOSError(SW1: Byte; SW2: Byte): Boolean;
function CheckSetting: Boolean;
function GetSessionKey(): integer;
function StartSession(): integer;
function SubmitCode(codeType: Byte; CodeIn:array of Byte): integer;
function Authenticate(DataIn:array of Byte): integer;

implementation

{$R *.dfm}

// this routine will encrypt 8-byte data with 8-byte key
// the result is stored in data
procedure DES (data : PByte; Key : PByte);
begin
  Encrypt (data, key);
end;

// this routine will use 3DES algo to encrypt 8-byte data with 16-byte key
// the result is stored in data
procedure TripleDES (data : PByte; Key : PBYTE);
var KeyRight : array [0..7] of byte;
begin

  move (pchar(key)[8], KeyRight, 8);  // copy right half of key to keyright
  Encrypt (data, key);                // data = DES (data, keyleft)
  Decrypt (data, @KeyRight);          // data = unDES (data, keyright)
  Encrypt (data, Key);                // data = DES (data, keyleft)

end;

// MAC as defined in ACOS manual
// receives 8-byte Key and 16-byte Data
// result is stored in Data
procedure MAC (data : PByte; Key : PBYTE);
var i : integer;
var buff : array [0..15] of byte;
begin

  DES (data, key);
  move (data^, buff, 16);
  for i := 0 to 7 do
  begin
    buff[i] := buff[i] xor buff[i+8];
  end;
  DES (@buff, key);
  move (buff, data^, 8);

end;

// Triple MAC as defined in ACOS manual
// receives 16-byte Key and 16-byte Data
// result is stored in Data
procedure TripleMAC (data : PByte; Key : PBYTE);
var i : integer;
var buff : array [0..15] of byte;
begin

  TripleDES (data, key);
  move (data^, buff, 16);
  for i := 0 to 7 do
  begin
    buff[i] := buff[i] xor buff[i+8];
  end;
  TripleDES (@buff, key);
  move (buff, data^, 8);

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

procedure ClearTextFields();
begin

  MainEncrypt.tCard.Text := '';
  MainEncrypt.tTerminal.Text := '';
  MainEncrypt.tValue.Text := '';

end;

procedure InitMenu();
begin

  MainEncrypt.cbReader.Clear;
  MainEncrypt.mMsg.Clear;
  DisplayOut(0, 0, 'Program ready');
  MainEncrypt.cbReader.Enabled := False;
  MainEncrypt.bInit.Enabled := True;
  MainEncrypt.bConnect.Enabled := False;
  MainEncrypt.bFormat.Enabled := False;
  MainEncrypt.rgOption.Enabled := False;
  MainEncrypt.rgEncrypt.Enabled := False;
  MainEncrypt.gbInput.Enabled := False;
  MainEncrypt.gbSubmit.Enabled := False;
  MainEncrypt.bReset.Enabled := False;
  ClearTextFields();
  
end;

procedure AddButtons();
begin

  MainEncrypt.cbReader.Enabled := True;
  MainEncrypt.bInit.Enabled := False;
  MainEncrypt.bConnect.Enabled := True;
  MainEncrypt.bReset.Enabled := True;

end;

procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
begin

  case errType of
    0: MainEncrypt.mMsg.SelAttributes.Color := clTeal;      // Notifications
    1: begin                                                // Error Messages
         MainEncrypt.mMsg.SelAttributes.Color := clRed;
         PrintText := GetScardErrMsg(retVal);
       end;
    2: begin
         MainEncrypt.mMsg.SelAttributes.Color := clBlack;
         PrintText := '< ' + PrintText;                      // Input data
       end;
    3: begin
         MainEncrypt.mMsg.SelAttributes.Color := clBlack;
         PrintText := '> ' + PrintText;                      // Output data
       end;
    4: MainEncrypt.mMsg.SelAttributes.Color := clRed;        // For ACOS error
  end;
  MainEncrypt.mMsg.Lines.Add(PrintText);
  MainEncrypt.mMsg.SelAttributes.Color := clBlack;
  MainEncrypt.mMsg.SetFocus;

end;


function SendAPDUandDisplay(SendType: integer; ApduIn: string): integer;
var tmpStr: string;
    indx: integer;
begin

  ioRequest.dwProtocol := dwActProtocol;
  ioRequest.cbPciLength := sizeof(SCARD_IO_REQUEST);
  DisplayOut(2, 0, ApduIn);
  RecvLen := 262;
  retCode := SCardTransmit(hCard,
                           @ioRequest,
                           @SendBuff,
                           SendLen,
                           Nil,
                           @RecvBuff,
                           @RecvLen);
  if retCode <> SCARD_S_SUCCESS then
    begin
      DisplayOut(1, retCode, '');
      SendAPDUandDisplay := retCode;
      Exit;
    end
  else
    begin
      case SendType of
      0: begin      // Read all data received
         for indx := 0 to RecvLen-1 do
           tmpStr := tmpStr + Format('%.02X ', [(RecvBuff[Indx])]);
         end;
      1: begin      // Read ATR after checking SW1/SW2
         for indx := (RecvLen-2) to (RecvLen-1) do
           tmpStr := tmpStr + Format('%.02X ', [(RecvBuff[indx])]);
         if (Trim(tmpStr) <> '90 00') then
           DisplayOut(1, 0, 'Return bytes are not acceptable.')
         else
           begin
             tmpStr := 'ATR :';
             for indx := 0 to (RecvLen-3) do
               tmpStr := tmpStr + Format('%.2X ', [(RecvBuff[Indx])]);
           end;
         end;
      2: begin      // Read SW1/SW2
         for indx := (RecvLen-2) to (RecvLen-1) do
           tmpStr := tmpStr + Format('%.02X ', [(RecvBuff[indx])]);
         if (Trim(tmpStr) <> '90 00') then
           DisplayOut(1, 0, 'Return bytes are not acceptable.')
         else
           begin
             tmpStr := '';
             for indx := 0 to (RecvLen-3) do
               tmpStr := tmpStr + Format('%.2X ', [(RecvBuff[Indx])]);
           end;
         end;

      end;
      DisplayOut(3, 0, Trim(tmpStr));
    end;
  SendAPDUandDisplay := retCode;

end;

function SubmitIC(): integer;
var indx: integer;
    tmpStr: String;
begin

  ClearBuffers();
  SendBuff[0] := $80;     // CLA
  SendBuff[1] := $20;     // INS
  SendBuff[2] := $07;     // P1
  SendBuff[3] := $00;     // P2
  SendBuff[4] := $08;     // P3
  SendBuff[5] := $41;     // A
  SendBuff[6] := $43;     // C
  SendBuff[7] := $4F;     // O
  SendBuff[8] := $53;     // S
  SendBuff[9] := $54;     // T
  SendBuff[10] := $45;    // E
  SendBuff[11] := $53;    // S
  SendBuff[12] := $54;    // T
  SendLen := $0D;
  RecvLen := $02;

  tmpStr := '';
  for indx := 0 to SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  retCode := SendAPDUandDisplay(0, tmpStr);
  if retCode <> SCARD_S_SUCCESS then
    begin
      SubmitIC := retCode;
      Exit;
    end ;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> '90 00 ' then
    begin
      displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      SubmitIC := retCode;
      Exit;
    end;
  SubmitIC := retCode;

end;

function selectFile(HiAddr: Byte; LoAddr: Byte): integer;
var tmpStr: String;
    indx: integer;
begin

  ClearBuffers();
  SendBuff[0] := $80;     // CLA
  SendBuff[1] := $A4;     // INS
  SendBuff[2] := $00;     // P1
  SendBuff[3] := $00;     // P2
  SendBuff[4] := $02;     // P3
  SendBuff[5] := HiAddr;     // Value of High Byte
  SendBuff[6] := LoAddr;     // Value of Low Byte
  SendLen := $07;
  RecvLen := $02;
  tmpStr := '';
  for indx := 0 to SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  retCode := SendAPDUandDisplay(0, tmpStr);
  if retCode <> SCARD_S_SUCCESS then
    begin
      selectFile := retCode;
      Exit;
    end ;
  selectFile := retCode;

end;

function writeRecord(caseType: Integer; RecNo: Byte; maxDataLen: Byte; DataLen: Byte; DataIn:array of Byte): integer;
var indx: integer;
    tmpStr: String;
begin

  if caseType = 1 then   // If card data is to be erased before writing new data
    begin
      // 1. Re-initialize card values to $00
      ClearBuffers();
      SendBuff[0] :=  $80;          // CLA
      SendBuff[1] :=  $D2;          // INS
      SendBuff[2] :=  RecNo;        // P1    Record to be written
      SendBuff[3] :=  $00;          // P2
      SendBuff[4] :=  maxDataLen;   // P3    Length
      for indx := 0 to maxDataLen-1 do
        SendBuff[indx + 5] := $00;
      SendLen := maxDataLen + 5;
      RecvLen := $02;
    tmpStr := '';
    for indx := 0 to SendLen-1 do
      tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
    retCode := SendAPDUandDisplay(0, tmpStr);
    if retCode <> SCARD_S_SUCCESS then
      begin
        writeRecord := retCode;
        Exit;
      end;
    tmpStr := '';
    for indx := 0 to 1 do
      tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
    if ACOSError(RecvBuff[0], RecvBuff[1]) then
      begin
        retCode := INVALID_SW1SW2;
        writeRecord := retCode;
        Exit;
      end;
    if tmpStr <> '90 00 ' then
      begin
        displayOut(3, 0, 'The return string is invalid. Value: ' + tmpStr);
        retCode := INVALID_SW1SW2;
        writeRecord := retCode;
        Exit;
      end;
  end;

  // 2. Write data to card
  ClearBuffers();
  SendBuff[0] :=  $80;          // CLA
  SendBuff[1] :=  $D2;          // INS
  SendBuff[2] :=  RecNo;        // P1    Record to be written
  SendBuff[3] :=  $00;          // P2
  SendBuff[4] :=  DataLen;   // P3    Length
  for indx := 0 to DataLen-1 do
    SendBuff[indx + 5] := DataIn[indx];
  SendLen := DataLen + 5;
  RecvLen := $02;
  tmpStr := '';
  for indx := 0 to SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  retCode := SendAPDUandDisplay(0, tmpStr);
  if retCode <> SCARD_S_SUCCESS then
    begin
      writeRecord := retCode;
      Exit;
    end;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if ACOSError(RecvBuff[0], RecvBuff[1]) then
    begin
      retCode := INVALID_SW1SW2;
      writeRecord := retCode;
      Exit;
    end;
  if tmpStr <> '90 00 ' then
    begin
      displayOut(3, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      writeRecord := retCode;
      Exit;
    end;
  writeRecord := retCode;

end;

function readRecord(RecNo: Byte; DataLen: Byte): integer;
var indx: integer;
    tmpStr: String;
begin

  ClearBuffers();
  SendBuff[0] := $80;        // CLA
  SendBuff[1] := $B2;        // INS
  SendBuff[2] := RecNo;      // P1    Record No
  SendBuff[3] := $00;        // P2
  SendBuff[4] := DataLen;    // P3    Length of data to be read
  SendLen := $05;
  RecvLen := SendBuff[4] + 2;
  tmpStr := '';
  for indx := 0 to SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  retCode := SendAPDUandDisplay(0, tmpStr);
  if retCode <> SCARD_S_SUCCESS then
    begin
      readRecord := retCode;
      Exit;
    end;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx + SendBuff[4]]]) + ' ';
  if ACOSError(RecvBuff[SendBuff[4]], RecvBuff[SendBuff[4]+1]) then
    begin
      retCode := INVALID_SW1SW2;
      readRecord := retCode;
      Exit;
    end;
  if tmpStr <> '90 00 ' then
    begin
      displayOut(3, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
    end;
  readRecord := retCode;

end;

function ValidTemplate(validationType: integer): Boolean;
begin

  ValidTemplate := False;
  case MainEncrypt.rgOption.ItemIndex of
    0: begin
         if Length(MainEncrypt.tCard.Text) < 8 then
           begin
             MainEncrypt.tCard.SelectAll;
             MainEncrypt.tCard.SetFocus;
             Exit;
           end;
         if Length(MainEncrypt.tTerminal.Text) < 8 then
           begin
             MainEncrypt.tTerminal.SelectAll;
             MainEncrypt.tTerminal.SetFocus;
             Exit;
           end;
       end;
    1: begin
         if Length(MainEncrypt.tCard.Text) < 16 then
           begin
             MainEncrypt.tCard.SelectAll;
             MainEncrypt.tCard.SetFocus;
             Exit;
           end;
         if Length(MainEncrypt.tTerminal.Text) < 16 then
           begin
             MainEncrypt.tTerminal.SelectAll;
             MainEncrypt.tTerminal.SetFocus;
             Exit;
           end;
       end;
  end;
  if validationType = 1 then      // For validation that includes Code input
    if Length(MainEncrypt.tValue.Text) < 8 then
      begin
        MainEncrypt.tValue.SelectAll;
        MainEncrypt.tValue.SetFocus;
        ValidTemplate := False;
        Exit;
     end;
   ValidTemplate := True;

end;

function CheckACOS(): Boolean;
var tmpStr: String;
    indx: integer;
begin

  CheckACOS := False;
  // 1. Reestablish connection to accommodate change of card
  retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
  ConnActive := False;
  retCode := SCardConnectA(hContext,
                           PChar(MainEncrypt.cbReader.Text),
                           SCARD_SHARE_SHARED,
                           SCARD_PROTOCOL_T0 or SCARD_PROTOCOL_T1,
                           @hCard,
                           @dwActProtocol);
  if retCode <> SCARD_S_SUCCESS then
    begin
      DisplayOut(1, retCode, '');
      ConnActive := False;
      Exit;
    end;
  ConnActive := True;
  // 2. Check for File FF 00
  retCode := selectFile($FF, $00);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> '90 00 ' then
    begin
      displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
      Exit;
    end;

  // 3. Check for File FF 01
  retCode := selectFile($FF, $01);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> '90 00 ' then
    begin
      displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
      Exit;
    end;

  // 4. Check for File FF 02
  retCode := selectFile($FF, $02);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> '90 00 ' then
    begin
      displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
      Exit;
    end;

  CheckACOS := True;

end;

function ACOSError(SW1: Byte; SW2: Byte): Boolean;
begin

  // Check for error returned by ACOS card OS
  ACOSError := True;

  if ((SW1 = $62) and (SW2 = $81)) then
    begin
      displayOut(4, 0, 'Account data may be corrupted.');
      Exit;
    end;
  if (SW1 = $63) then
    begin
      displayOut(4, 0, 'MAC cryptographic checksum is wrong.');
      Exit;
    end;
  if ((SW1 = $69) and (SW2 = $66)) then
    begin
      displayOut(4, 0, 'Command not available or option bit not set.');
      Exit;
    end;
  if ((SW1 = $69) and (SW2 = $82)) then
    begin
      displayOut(4, 0, 'Security status not satisfied. Secret code, IC or PIN not submitted.');
      Exit;
    end;
  if ((SW1 = $69) and (SW2 = $83)) then
    begin
      displayOut(4, 0, 'The specified code is locked.');
      Exit;
    end;
  if ((SW1 = $69) and (SW2 = $85)) then
    begin
      displayOut(4, 0, 'Preceding transaction was not DEBIT or mutual authentication has not been completed.');
      Exit;
    end;
  if ((SW1 = $69) and (SW2 = $F0)) then
    begin
      displayOut(4, 0, 'Data in account is inconsistent. No access unless in Issuer mode.');
      Exit;
    end;
  if ((SW1 = $6A) and (SW2 = $82)) then
    begin
      displayOut(4, 0, 'Account does not exist.');
      Exit;
    end;
  if ((SW1 = $6A) and (SW2 = $83)) then
    begin
      displayOut(4, 0, 'Record not found or file too short.');
      Exit;
    end;
  if ((SW1 = $6A) and (SW2 = $86)) then
    begin
      displayOut(4, 0, 'P1 or P2 is incorrect.');
      Exit;
    end;
  if ((SW1 = $6B) and (SW2 = $20)) then
    begin
      displayOut(4, 0, 'Invalid amount in DEBIT/CREDIT command.');
      Exit;
    end;
  if (SW1 = $6C) then
    begin
      displayOut(4, 0, 'Issue GET RESPONSE with P3 = ' + Format('%.02X ',[SW2]) + ' to get response data.');
      Exit;
    end;
  if (SW1 = $6D) then
    begin
      displayOut(4, 0, 'Unknown INS.');
      Exit;
    end;
  if (SW1 = $6E) then
    begin
      displayOut(4, 0, 'Unknown CLA.');
      Exit;
    end;
  if ((SW1 = $6F) and (SW2 = $10)) then
    begin
      displayOut(4, 0, 'Account Transaction Counter at maximum. No more transaction possible.');
      Exit;
    end;

  ACOSError := False;

end;

function CheckSetting: Boolean;
var indx: integer;
    tmpStr: String;
begin

  CheckSetting := False;
  // 1. Check if card inserted is ACOS
  if not checkACOS then
    begin
      displayOut(0, 0, 'Please insert an ACOS card.');
      retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
      ConnActive := False;
      Exit;
    end;
  displayOut(0, 0, 'ACOS card detected.');

  // 2. Select FF 02
  retCode := selectFile($FF, $02);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> '90 00 ' then
    begin
      displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      Exit;
    end;

  // 3. Read first record of the FF 02
  retCode := readRecord($00, 04);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 4. Check for Option register setting
  if (RecvBuff[0] and $02) <> 0 then    // 3-DES bit  is checked
    MainEncrypt.rgOption.ItemIndex := 1
  else
    MainEncrypt.rgOption.ItemIndex := 0;

  // 5. Check for Security Option register setting
  if ((RecvBuff[1] and $02) <> 0) then    // AC1 bit is checked
      MainEncrypt.rgEncrypt.ItemIndex := 1
  else
    MainEncrypt.rgEncrypt.ItemIndex := 0;

 CheckSetting := True;

end;

function GetSessionKey(): integer;
var indx: integer;
    tmpStr: String;
    CRnd: array[0..7] of byte;         // Card random number
    TRnd: array[0..7] of byte;         // Terminal random number
    tmpArray: array [0..15] of byte;
    ReverseKey: array[0..15] of byte;  // Reverse of Terminal key
begin

  // 1. Get Card Key and Terminal Key from Key Template
  tmpStr := MainEncrypt.tCard.Text;
  case MainEncrypt.rgOption.ItemIndex of
    0: for indx := 0 to 7 do
         cKey[indx] := ord(tmpStr[indx+1]);
    1: for indx := 0 to 15 do
         cKey[indx] := ord(tmpStr[indx+1]);
  end;
  tmpStr := MainEncrypt.tTerminal.Text;
  case MainEncrypt.rgOption.ItemIndex of
    0: for indx := 0 to 7 do
         tKey[indx] := ord(tmpStr[indx+1]);
    1: for indx := 0 to 15 do
         tKey[indx] := ord(tmpStr[indx+1]);
  end;

  // 2. Generate random number from card
  retCode := StartSession();
  if retCode <> SCARD_S_SUCCESS then
    begin
      GetSessionKey := retCode;
      Exit;
    end;

  // 3. Store the random number generated by the card to Crnd
  for indx := 0 to 7 do
    CRnd[indx] := RecvBuff[indx];

  // 4. Encrypt Random No (CRnd) with Terminal Key (tKey)
  //      tmpArray will hold the 8-byte Enrypted number
  for indx := 0 to 7 do
    tmpArray[indx] := CRnd[indx];
  case MainEncrypt.rgOption.ItemIndex of
    0: DES(@tmpArray, @tKey);
    1: TripleDES(@tmpArray, @tKey);
  end;

  // 5. Issue Authenticate command using 8-byte Encrypted No (tmpArray)
  //      and Random Terminal number (TRnd)
  for indx := 0 to 7 do
    tmpArray[indx+8] := TRnd[indx];
  retCode := Authenticate(tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    begin
      GetSessionKey := retCode;
      Exit;
    end;

  // 6. Compute for Session Key
  case MainEncrypt.rgOption.ItemIndex of
    0: begin
		     // for single DES
     		 // prepare SessionKey
	       // SessionKey = DES (DES(RNDc, KC) XOR RNDt, KT)

		     // calculate DES(cRnd,cKey)
         for indx := 0 to 7 do
           tmpArray[indx] := cRnd[indx];
         DES (@tmpArray, @cKey);

		     // XOR the result with tRnd
         for indx := 0 to 7 do
           tmpArray[indx] := tmpArray[indx] xor TRnd[indx];

 	       // DES the result with tKey
         DES (@tmpArray, @tKey);

 		     // temp now holds the SessionKey
         for indx := 0 to 7 do
           SessionKey[indx] := tmpArray[indx];
       end;
    1: begin
     		 // for triple DES
	    	 // prepare SessionKey
		     // Left half SessionKey =  3DES (3DES (CRnd, cKey), tKey)
		     // Right half SessionKey = 3DES (TRnd, REV (tKey))

   		   // tmpArray = 3DES (CRnd, cKey)
         for indx := 0 to 7 do
           tmpArray[indx] := cRnd[indx];
         TripleDES (@tmpArray, @cKey);

         // tmpArray = 3DES (tmpArray, tKey)
         TripleDES (@tmpArray, @tKey);

		     // tmpArray holds the left half of SessionKey
         for indx := 0 to 7 do
           SessionKey[indx] := tmpArray[indx];

  		   // compute ReverseKey of tKey
	  	   // just swap its left side with right side
		     // ReverseKey = right half of tKey + left half of tKey
         for indx := 0 to 7 do
           ReverseKey[indx] := tKey[8+indx];
         for indx := 0 to 7 do
           ReverseKey[8+indx] := tKey[indx];

         // compute tmpArray = 3DES (TRnd, ReverseKey)
         for indx := 0 to 7 do
           tmpArray[indx] := TRnd[indx];
         TripleDES (@tmpArray, @ReverseKey);

   		   // tmpArray holds the right half of SessionKey
         for indx := 0 to 7 do
           SessionKey[indx+8] := tmpArray[indx];
       end;
  end;
  GetSessionKey := retCode;

end;

function StartSession(): integer;
var indx: integer;
    tmpStr: String;
begin

  ClearBuffers();
  SendBuff[0] := $80;     // CLA
  SendBuff[1] := $84;     // INS
  SendBuff[2] := $00;     // P1
  SendBuff[3] := $00;     // P2
  SendBuff[4] := $08;     // P3
  SendLen := $05;
  RecvLen := $0A;

  tmpStr := '';
  for indx := 0 to SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  retCode := SendAPDUandDisplay(0, tmpStr);
  if retCode <> SCARD_S_SUCCESS then
    begin
      StartSession := retCode;
      Exit;
    end ;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx+ SendBuff[4]]]) + ' ';
  if tmpStr <> '90 00 ' then
    begin
      displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      StartSession := retCode;
      Exit;
    end;
  StartSession := retCode;

end;

function SubmitCode(codeType: Byte; CodeIn:array of Byte): integer;
var indx: integer;
    tmpStr: String;
begin

  ClearBuffers();
  SendBuff[0] := $80;         // CLA
  SendBuff[1] := $20;         // INS
  SendBuff[2] := codeType;    // P1
  SendBuff[3] := $00;         // P2
  SendBuff[4] := $08;         // P3
  for indx := 0 to 7 do
    SendBuff[indx + 5] := CodeIn[indx];
  SendLen := SendBuff[4] + 5;
  RecvLen := $02;
  tmpStr := '';
  for indx := 0 to SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  retCode := SendAPDUandDisplay(0, tmpStr);
  if retCode <> SCARD_S_SUCCESS then
    begin
      SubmitCode := retCode;
      Exit;
    end ;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> '90 00 ' then
    begin
      displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      SubmitCode := retCode;
      Exit;
    end;
  SubmitCode := retCode;

end;

function Authenticate(DataIn:array of Byte): integer;
var indx: integer;
    tmpStr: String;
begin

  ClearBuffers();
  SendBuff[0] :=  $80;          // CLA
  SendBuff[1] :=  $82;          // INS
  SendBuff[2] :=  $00;          // P1
  SendBuff[3] :=  $00;          // P2
  SendBuff[4] :=  $10;          // P3
  for indx := 0 to 15 do
    SendBuff[indx + 5] := DataIn[indx];
  SendLen := SendBuff[4] + 5;
  RecvLen := $02;
  tmpStr := '';
  for indx := 0 to SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  retCode := SendAPDUandDisplay(0, tmpStr);
  if retCode <> SCARD_S_SUCCESS then
    begin
      Authenticate := retCode;
      Exit;
    end;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if ACOSError(RecvBuff[0], RecvBuff[1]) then
    begin
      retCode := INVALID_SW1SW2;
      Authenticate := retCode;
      Exit;
    end;
  if tmpStr <> '61 08 ' then
    begin
      displayOut(3, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      Authenticate := retCode;
      Exit;
    end;
  Authenticate := retCode;

end;

procedure TMainEncrypt.bQuitClick(Sender: TObject);
begin

  if ConnActive then
    begin
      retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
      ConnActive := False;
    end;

  retCode := SCardReleaseContext(hCard);
  Application.Terminate;

end;

procedure TMainEncrypt.bResetClick(Sender: TObject);
begin

  if ConnActive then
    begin
      retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
      ConnActive := False;
    end;

  retCode := SCardReleaseContext(hCard);
  rgOption.ItemIndex := -1;
  rgEncrypt.ItemIndex := -1;
  cbCode.ItemIndex := -1;
  InitMenu();

end;

procedure TMainEncrypt.bInitClick(Sender: TObject);
begin

  // 1. Establish context and obtain hContext handle
  retCode := SCardEstablishContext(SCARD_SCOPE_USER,
                                   nil,
                                   nil,
                                   @hContext);
  if retCode <> SCARD_S_SUCCESS then
    begin
      DisplayOut(1, retCode, '');
      Exit;
    end ;

  // 2. List PC/SC card readers installed in the system
  BufferLen := MAX_BUFFER_LEN;
  retCode := SCardListReadersA(hContext,
                               nil,
                               @Buffer,
                               @BufferLen);
  if retCode <> SCARD_S_SUCCESS then
    begin
      DisplayOut(1, retCode, '');
      Exit;
    end
  else
    DisplayOut(0, 0, 'Select reader, insert mcu card and connect.');

  MainEncrypt.cbReader.Clear;;
  LoadListToControl(MainEncrypt.cbReader,@buffer,bufferLen);
  MainEncrypt.cbReader.ItemIndex := 0;
  AddButtons();

end;

procedure TMainEncrypt.bConnectClick(Sender: TObject);
var ReaderLen, ATRLen: DWORD;
    dwState: integer;
    ATRVal: array[0..19] of Byte;
    tmpStr: String;
    indx: integer;
begin

  if ConnActive then
  begin
    DisplayOut(0, 0, 'Connection is already active.');
    Exit;
  end;

  DisplayOut(2, 0, 'Invoke SCardConnect');
  // 1. Connect to selected reader using hContext handle
  //    and obtain valid hCard handle
  retCode := SCardConnectA(hContext,
                           PChar(cbReader.Text),
                           SCARD_SHARE_SHARED,
                           SCARD_PROTOCOL_T0 or SCARD_PROTOCOL_T1,
                           @hCard,
                           @dwActProtocol);
  if retCode <> SCARD_S_SUCCESS then
    begin
      DisplayOut(1, retCode, '');
      ConnActive := False;
      Exit;
    end
  else
    DisplayOut(0, 0, 'Successful connection to ' + cbReader.Text);
  DisplayOut(2, 0, 'Get Card Status');
  ATRLen := 32;
  ReaderLen := 0;
  dwState := 0;
  retCode := SCardStatusA(hCard,
                         PChar(cbReader.Text),
                         @ReaderLen,
                         @dwState,
                         @dwActProtocol,
                         @ATRVal,
                         @ATRLen);
  tmpStr := '';
  for indx := 0 to integer(ATRLen)-1 do
    begin
      tmpStr := tmpStr + Format('%.02X ',[ATRVal[indx]]);
    end;
  DisplayOut(3, 0, Format('ATR Value: %s',[tmpStr]));
  tmpStr := '';
  case integer(dwActProtocol) of
    1: tmpStr := 'T=0';
    2: tmpStr := 'T=1';
    else
      tmpStr := 'No protocol is defined.';
    end;
  DisplayOut(3, 0, Format('Active Protocol: %s',[tmpStr]));

  if not CheckSetting then
    Exit;

  ConnActive := True;
  bFormat.Enabled := True;
  rgEncrypt.Enabled := True;
  gbInput.Enabled := True;
  gbSubmit.Enabled := True;
  cbCode.ItemIndex := 0;
  ClearTextFields();

end;

procedure TMainEncrypt.FormActivate(Sender: TObject);
begin

  InitMenu();

end;

procedure TMainEncrypt.cbReaderChange(Sender: TObject);
begin

  bFormat.Enabled := False;
  rgOption.ItemIndex := -1;
  rgEncrypt.ItemIndex := -1;
  cbCode.ItemIndex := -1;
  gbSubmit.Enabled := False;
  rgOption.Enabled := False;
  rgEncrypt.Enabled := False;
  ClearTextFields();

  if ConnActive then
  begin
    retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
    ConnActive := False;
  end;

end;

procedure TMainEncrypt.rgEncryptClick(Sender: TObject);
begin

  case rgEncrypt.ItemIndex of
    0: begin
         rgOption.ItemIndex := 0;
         rgOption.Enabled := False;    // 3-DES Option cannot be set
       end;
    1: rgOption.Enabled := True;
  end;

end;

procedure TMainEncrypt.bFormatClick(Sender: TObject);
var tmpStr: String;
    indx: integer;
    tmpArray: array[0..31] of Byte;
begin

  // 1. Validate template data for keys
  if not ValidTemplate(0) then
    begin
      Exit;
    end;

  // 2. Check if card inserted is an ACOS card
  if not checkACOS then
    begin
      displayOut(0, 0, 'Please insert an ACOS card.');
      Exit;
    end;
  displayOut(0, 0, 'ACOS card detected.');

  // 3. Submit IC Code
  retCode := SubmitIC();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 4. Select FF 02
  retCode := selectFile($FF, $02);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> '90 00 ' then
    begin
      displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      Exit;
    end;

  // 5. Write to FF 02
  //    This step will define the Option registers and Security Option registers.
  //    Personalization bit is not set. Although Secret Codes may be set
  //    individually, this program adopts uniform encryption option for
  //    all codes to simplify coding. Issuer Code (IC) is not encrypted
  //    to remove risk of locking the ACOS card for wrong IC submission.
  case rgOption.ItemIndex of   // Option  Register
    0: tmpArray[0] :=  $00;    // 00h  3-DES is not set
    1: tmpArray[0] :=  $02;    // 02h  3-DES is enabled
  end;
  case rgEncrypt.ItemIndex of   // Security Option Register
    0: tmpArray[1] :=  $00;    // 00h  Encryption is not set
    1: tmpArray[1] :=  $7E;    // Encryption on all codes, except IC, enabled
  end;
  tmpArray[2] :=  $00;        // 00    No of user files
  tmpArray[3] :=  $00;        // 00    Personalization bit
  retCode := writeRecord(0, $00, $04, $04, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 6. Perform a reset for changes in the ACOS to take effect
  ConnActive := False;
	retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
  if retCode <> SCARD_S_SUCCESS then
    begin
      displayOut(0, retCode, '');
      Exit;
    end ;
  retCode := SCardConnectA(hContext,
                           PChar(cbReader.Text),
                           SCARD_SHARE_SHARED,
                           SCARD_PROTOCOL_T0 or SCARD_PROTOCOL_T1,
                           @hCard,
                           @dwActProtocol);
  if retCode <> SCARD_S_SUCCESS then
    begin
      displayOut(0, retCode, '');
      Exit;
    end ;
  DisplayOut(0, 0, 'File FF 02 is updated.');
  ConnActive := True;

  // 7. Submit IC Code
  retCode := SubmitIC();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 8. Select FF 03
  retCode := selectFile($FF, $03);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> '90 00 ' then
    begin
      displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      Exit;
    end;

  // 9. Write to FF 03
  case rgOption.ItemIndex of
    0: begin
       // 5a.1. Write Record 02 for Card key
         tmpStr := tCard.Text;
         for indx := 0 to Length(tmpStr)-1 do
           begin
             tmpArray[indx] := ord(tmpStr[indx+1]);
           end;
         retCode := writeRecord(0, $02, 8, 8, tmpArray);
         if retCode <> SCARD_S_SUCCESS then
           Exit;

       // 5a.2. Write Record 03 for Terminal key
         tmpStr := tTerminal.Text;
         for indx := 0 to Length(tmpStr)-1 do
           begin
             tmpArray[indx] := ord(tmpStr[indx+1]);
           end;
         retCode := writeRecord(0, $03, 8, 8, tmpArray);
         if retCode <> SCARD_S_SUCCESS then
           Exit;
       end;
    1: begin
         // 5b.1. Write Record 02 and Record 12 for Card key
         tmpStr := tCard.Text;
         for indx := 0 to 7 do     // Left half of Card key
           begin
             tmpArray[indx] := ord(tmpStr[indx+1]);
           end;
         retCode := writeRecord(0, $02, 8, 8, tmpArray);
         if retCode <> SCARD_S_SUCCESS then
           Exit;
         for indx := 8 to 15 do    // Right half of Card key
           begin
             tmpArray[indx-8] := ord(tmpStr[indx+1]);
           end;
         retCode := writeRecord(0, $0C, 8, 8, tmpArray);
         if retCode <> SCARD_S_SUCCESS then
           Exit;

         // 5b.2. Write Record 03 and Record 13 for Terminal key
         tmpStr := tTerminal.Text;
         for indx := 0 to 7 do     // Left half of Terminal key
           begin
             tmpArray[indx] := ord(tmpStr[indx+1]);
           end;
         retCode := writeRecord(0, $03, 8, 8, tmpArray);
         if retCode <> SCARD_S_SUCCESS then
           Exit;
         for indx := 8 to 15 do    // Right half of Terminal key
           begin
             tmpArray[indx-8] := ord(tmpStr[indx+1]);
           end;
         retCode := writeRecord(0, $0D, 8, 8, tmpArray);
         if retCode <> SCARD_S_SUCCESS then
           Exit;
       end;
  end;

  DisplayOut(0, 0, 'File FF 03 is updated.');

end;

procedure TMainEncrypt.rgOptionClick(Sender: TObject);
begin

  ClearTextFields();
  case MainEncrypt.rgOption.ItemIndex of
    0: begin
         MainEncrypt.tCard.MaxLength := 8;
         MainEncrypt.tTerminal.MaxLength := 8;
       end;
    1: begin
         MainEncrypt.tCard.MaxLength := 16;
         MainEncrypt.tTerminal.MaxLength := 16;
       end;
  end;

end;

procedure TMainEncrypt.bSubmitClick(Sender: TObject);
var indx: integer;
    tmpStr: String;
    tmpArray: array [0..15] of byte;
    tmpByte: byte;
begin

  // 1. Validate template data for keys
  if not ValidTemplate(1) then
    begin
      Exit;
    end;

  // 2. Check if card inserted is an ACOS card
  if not checkACOS then
    begin
      displayOut(0, 0, 'Please insert an ACOS card.');
      Exit;
    end;
  displayOut(0, 0, 'ACOS card detected.');

  // 3. Get Code input
  tmpStr := tValue.Text;
  for indx := 0 to 7 do
    begin
      tmpArray[indx] := ord(tmpStr[indx+1]);
      end;
  if rgEncrypt.ItemIndex = 1 then    // Check Encryption option
    begin
      retCode := GetSessionKey;
      if retCode <> SCARD_S_SUCCESS then
        Exit;
      case rgOption.ItemIndex of
        0: DES(@tmpArray, @SessionKey);
        1: TripleDES(@tmpArray, @SessionKey);
      end;
    end;
  tmpByte := $00;
  case cbCode.ItemIndex of
    0: begin
         tmpByte := $06;
         tmpStr := 'PIN Code';
       end;
    1: begin
         tmpByte := $01;
         tmpStr := 'Application Code 1';
       end;
    2: begin
         tmpByte := $02;
         tmpStr := 'Application Code 2';
       end;
    3: begin
         tmpByte := $03;
         tmpStr := 'Application Code 3';
       end;
    4: begin
         tmpByte := $04;
         tmpStr := 'Application Code 4';
       end;
    5: begin
         tmpByte := $05;
         tmpStr := 'Application Code 5';
       end;
  end;


  // 4. Submit Code
  retCode := SubmitCode(tmpByte, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  DisplayOut(0, 0, tmpStr + ' submitted successfully.');

end;

procedure TMainEncrypt.bSetClick(Sender: TObject);
var indx: integer;
    tmpStr: String;
    tmpArray: array [0..15] of byte;
    tmpByte: byte;
begin

  // 1. Validate template data for keys
  if not ValidTemplate(1) then
    begin
      Exit;
    end;

  // 2. Check if card inserted is an ACOS card
  if not checkACOS then
    begin
      displayOut(0, 0, 'Please insert an ACOS card.');
      Exit;
    end;
  displayOut(0, 0, 'ACOS card detected.');

  // 3. Submit IC Code
  retCode := SubmitIC();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 4. Select FF 03
  retCode := selectFile($FF, $03);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> '90 00 ' then
    begin
      displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      Exit;
    end;

  // 5. Get Code Input
  tmpStr := tValue.Text;
  for indx := 0 to 7 do
    begin
      tmpArray[indx] := ord(tmpStr[indx+1]);
      end;
  tmpByte := $00;
  case cbCode.ItemIndex of
    0: begin
         tmpByte := $01;
         tmpStr := 'PIN Code';
       end;
    1: begin
         tmpByte := $05;
         tmpStr := 'Application Code 1';
       end;
    2: begin
         tmpByte := $06;
         tmpStr := 'Application Code 2';
       end;
    3: begin
         tmpByte := $07;
         tmpStr := 'Application Code 3';
       end;
    4: begin
         tmpByte := $08;
         tmpStr := 'Application Code 4';
       end;
    5: begin
         tmpByte := $09;
         tmpStr := 'Application Code 5';
       end;
  end;

  // 6. Write to corresponding record in FF 03
  retCode := writeRecord(0, tmpByte, $08, $08, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  DisplayOut(0, 0, tmpStr + ' changed successfully.');

end;

procedure TMainEncrypt.tCardKeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin

  case rgOption.ItemIndex of
    0: if Length(tCard.Text) > 7 then
         tTerminal.SetFocus;
    1: if Length(tCard.Text) > 15 then
         tTerminal.SetFocus;
  end;

end;

end.


