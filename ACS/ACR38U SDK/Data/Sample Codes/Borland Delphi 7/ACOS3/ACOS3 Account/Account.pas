//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              Account.pas
//
//  Description:       This sample program outlines the steps on how to
//                     use the Account File functionalities of ACOS
//                     using the PC/SC platform.
//
//  Author:	           Jose Isagani R. Mission
//
//  Date:	             May 6, 2004
//
//  Revision Trail:   (Date/Author/Description)
//   11/09/2005      Fernando G. Robles      Added Debit Certification
//
//======================================================================
unit Account;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ACSModule, ExtCtrls, ComCtrls;

Const MAX_BUFFER_LEN    = 256;
Const INVALID_SW1SW2    = -450;

type
  TMainAccount = class(TForm)
    Label1: TLabel;
    mMsg: TRichEdit;
    cbReader: TComboBox;
    bInit: TButton;
    bConnect: TButton;
    bFormat: TButton;
    gbFunctions: TGroupBox;
    rgOption: TRadioGroup;
    gbKeys: TGroupBox;
    Label3: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    tCredit: TEdit;
    bQuit: TButton;
    bReset: TButton;
    tDebit: TEdit;
    tCertify: TEdit;
    tRevDebit: TEdit;
    Label6: TLabel;
    tAmount: TEdit;
    bCredit: TButton;
    bDebit: TButton;
    bInquire: TButton;
    bRevDebit: TButton;
    Label2: TLabel;
    chk_dbc: TCheckBox;
    procedure bQuitClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure bInitClick(Sender: TObject);
    procedure bResetClick(Sender: TObject);
    procedure bConnectClick(Sender: TObject);
    procedure cbReaderChange(Sender: TObject);
    procedure bFormatClick(Sender: TObject);
    procedure rgOptionClick(Sender: TObject);
    procedure tCreditKeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure tDebitKeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure tCertifyKeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure bInquireClick(Sender: TObject);
    procedure bCreditClick(Sender: TObject);
    procedure tAmountKeyPress(Sender: TObject; var Key: Char);
    procedure bDebitClick(Sender: TObject);
    procedure bRevDebitClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  MainAccount: TMainAccount;
  hContext    : SCARDCONTEXT;
  hCard       : SCARDCONTEXT;
  ioRequest   : SCARD_IO_REQUEST;
  retCode     : Integer;
  dwActProtocol, BufferLen  : DWORD;
  SendBuff, RecvBuff        : array [0..262] of Byte;
  SendLen, RecvLen          : DWORD;
  Buffer      : array [0..MAX_BUFFER_LEN] of char;
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
function InquireAccount(keyNo: Byte; DataIn: array of Byte): integer;
function GetResponse(): integer;
function GetResponseDebitCertificate(): integer;
function CreditAmount(CreditData: array of Byte): integer;
function DebitAmount(DebitData: array of Byte): integer;
function DebitAmountwithDBC(DebitData: array of Byte): integer;
function RevokeDebit(RevDebData: array of Byte): integer;
function CheckACOS(): Boolean;
function ValidTemplate(): Boolean;
function ACOSError(SW1: Byte; SW2: Byte): Boolean;

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

  MainAccount.tDebit.Text := '';
  MainAccount.tCredit.Text := '';
  MainAccount.tCertify.Text := '';
  MainAccount.tRevDebit .Text := '';
  MainAccount.tAmount.Text := '';
  

end;

procedure InitMenu();
begin

  MainAccount.cbReader.Clear;
  MainAccount.mMsg.Clear;
  DisplayOut(0, 0, 'Program ready');
  MainAccount.cbReader.Enabled := False;
  MainAccount.bInit.Enabled := True;
  MainAccount.bConnect.Enabled := False;
  MainAccount.bFormat.Enabled := False;
  MainAccount.gbKeys.Enabled := False;
  MainAccount.gbFunctions.Enabled := False;
  MainAccount.rgOption.Enabled := False;
  ClearTextFields();
  MainAccount.bReset.Enabled := False;

end;

procedure AddButtons();
begin

  MainAccount.cbReader.Enabled := True;
  MainAccount.bInit.Enabled := False;
  MainAccount.bConnect.Enabled := True;
  MainAccount.bReset.Enabled := True;

end;

procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
begin

  case errType of
    0: MainAccount.mMsg.SelAttributes.Color := clTeal;      // Notifications
    1: begin                                                // Error Messages
         MainAccount.mMsg.SelAttributes.Color := clRed;
         PrintText := GetScardErrMsg(retVal);
       end;
    2: begin
         MainAccount.mMsg.SelAttributes.Color := clBlack;
         PrintText := '< ' + PrintText;                      // Input data
       end;
    3: begin
         MainAccount.mMsg.SelAttributes.Color := clBlack;
         PrintText := '> ' + PrintText;                      // Output data
       end;
    4: MainAccount.mMsg.SelAttributes.Color := clRed;        // For ACOS error
  end;
  MainAccount.mMsg.Lines.Add(PrintText);
  MainAccount.mMsg.SelAttributes.Color := clBlack;
  MainAccount.mMsg.SetFocus;
  
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

function InquireAccount(keyNo: Byte; DataIn: array of Byte): integer;
var indx: integer;
    tmpStr: String;
begin

  ClearBuffers();
  SendBuff[0] := $80;        // CLA
  SendBuff[1] := $E4;        // INS
  SendBuff[2] := keyNo;      // P1    Key No
  SendBuff[3] := $00;        // P2
  SendBuff[4] := $04;        // P3    Length of data input
  for indx := 0 to 3 do
    SendBuff[indx + 5] := DataIn[indx];
  SendLen := SendBuff[4] + 5;
  RecvLen := $02;
  tmpStr := '';
  for indx := 0 to SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  retCode := SendAPDUandDisplay(0, tmpStr);
  if retCode <> SCARD_S_SUCCESS then
    begin
      InquireAccount := retCode;
      Exit;
    end;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if ACOSError(RecvBuff[0], RecvBuff[1]) then
    begin
      retCode := INVALID_SW1SW2;
      InquireAccount := retCode;
      Exit;
    end;
  if tmpStr <> '61 19 ' then     // SW1/SW2 must be equal to 6119h
    begin
      displayOut(3, 0, 'INQUIRE ACCOUNT command failed.');
      retCode := INVALID_SW1SW2;
      InquireAccount := retCode;
      Exit;
    end;

  InquireAccount := retCode;

end;

function GetResponse(): integer;
var indx: integer;
    tmpStr: String;
begin

  ClearBuffers();
  SendBuff[0] := $80;        // CLA
  SendBuff[1] := $C0;        // INS
  SendBuff[2] := $00;        // P1
  SendBuff[3] := $00;        // P2
  SendBuff[4] := $19;        // P3    Data length expected
  SendLen := $05;
  RecvLen := SendBuff[4] + 2;
  tmpStr := '';
  for indx := 0 to SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  retCode := SendAPDUandDisplay(0, tmpStr);
  if retCode <> SCARD_S_SUCCESS then
    begin
      GetResponse := retCode;
      Exit;
    end;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx + SendBuff[4]]]) + ' ';
  if ACOSError(RecvBuff[SendBuff[4]], RecvBuff[SendBuff[4]+1]) then
    begin
      retCode := INVALID_SW1SW2;
      GetResponse := retCode;
      Exit;
    end;
  if tmpStr <> '90 00 ' then
    begin
      displayOut(3, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
    end;
  GetResponse := retCode;

end;


function GetResponseDebitCertificate(): integer;
var indx: integer;
    tmpStr: String;
begin

  ClearBuffers();
  SendBuff[0] := $80;        // CLA
  SendBuff[1] := $C0;        // INS
  SendBuff[2] := $00;        // P1
  SendBuff[3] := $00;        // P2
  SendBuff[4] := $04;        // P3    Data length expected
  SendLen := $05;
  RecvLen := SendBuff[4] + 2;
  tmpStr := '';
  for indx := 0 to SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  retCode := SendAPDUandDisplay(0, tmpStr);
  if retCode <> SCARD_S_SUCCESS then
    begin
      GetResponseDebitCertificate := retCode;
      Exit;
    end;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx + SendBuff[4]]]) + ' ';
  if ACOSError(RecvBuff[SendBuff[4]], RecvBuff[SendBuff[4]+1]) then
    begin
      retCode := INVALID_SW1SW2;
      GetResponseDebitCertificate := retCode;
      Exit;
    end;
  if tmpStr <> '90 00 ' then
    begin
      displayOut(3, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
    end;
  GetResponseDebitCertificate := retCode;

end;

function CreditAmount(CreditData: array of Byte): integer;
var indx: integer;
    tmpStr: String;
begin

  ClearBuffers();
  SendBuff[0] := $80;        // CLA
  SendBuff[1] := $E2;        // INS
  SendBuff[2] := $00;        // P1
  SendBuff[3] := $00;        // P2
  SendBuff[4] := $0B;        // P3
  for indx := 0 to 11 do
    SendBuff[indx + 5] := CreditData[indx];
  SendLen := SendBuff[4] + 5;
  RecvLen := $02;
  tmpStr := '';
  for indx := 0 to SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  retCode := SendAPDUandDisplay(0, tmpStr);
  if retCode <> SCARD_S_SUCCESS then
    begin
      CreditAmount := retCode;
      Exit;
    end;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if ACOSError(RecvBuff[0], RecvBuff[1]) then
    begin
      retCode := INVALID_SW1SW2;
      CreditAmount := retCode;
      Exit;
    end;
  if tmpStr <> '90 00 ' then
    begin
      displayOut(3, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      CreditAmount := retCode;
      Exit;
    end;
  CreditAmount := retCode;

end;

function DebitAmount(DebitData: array of Byte): integer;
var indx: integer;
    tmpStr: String;
begin

  ClearBuffers();
  SendBuff[0] := $80;        // CLA
  SendBuff[1] := $E6;        // INS
  SendBuff[2] := $00;        // P1
  SendBuff[3] := $00;        // P2
  SendBuff[4] := $0B;        // P3
  for indx := 0 to 11 do
    SendBuff[indx + 5] := DebitData[indx];
  SendLen := SendBuff[4] + 5;
  RecvLen := $02;
  tmpStr := '';
  for indx := 0 to SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  retCode := SendAPDUandDisplay(0, tmpStr);
  if retCode <> SCARD_S_SUCCESS then
    begin
      DebitAmount := retCode;
      Exit;
    end;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if ACOSError(RecvBuff[0], RecvBuff[1]) then
    begin
      retCode := INVALID_SW1SW2;
      DebitAmount := retCode;
      Exit;
    end;
  if tmpStr <> '90 00 ' then
    begin
      displayOut(3, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      DebitAmount := retCode;
      Exit;
    end;
  DebitAmount := retCode;

end;

function DebitAmountwithDBC(DebitData: array of Byte): integer;
var indx: integer;
    tmpStr: String;
begin

  ClearBuffers();
  SendBuff[0] := $80;        // CLA
  SendBuff[1] := $E6;        // INS
  SendBuff[2] := $01;        // P1
  SendBuff[3] := $00;        // P2
  SendBuff[4] := $0B;        // P3
  for indx := 0 to 11 do
    SendBuff[indx + 5] := DebitData[indx];
  SendLen := SendBuff[4] + 5;
  RecvLen := $02;
  tmpStr := '';
  for indx := 0 to SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  retCode := SendAPDUandDisplay(0, tmpStr);
  if retCode <> SCARD_S_SUCCESS then
    begin
      DebitAmountwithDBC := retCode;
      Exit;
    end;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if ACOSError(RecvBuff[0], RecvBuff[1]) then
    begin
      retCode := INVALID_SW1SW2;
      DebitAmountwithDBC := retCode;
      Exit;
    end;
  if tmpStr <> '61 04 ' then
    begin
      displayOut(3, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      DebitAmountwithDBC := retCode;
      Exit;
    end;
  DebitAmountwithDBC := retCode;

end;

function RevokeDebit(RevDebData: array of Byte): integer;
var indx: integer;
    tmpStr: String;
begin

  ClearBuffers();
  SendBuff[0] := $80;        // CLA
  SendBuff[1] := $E8;        // INS
  SendBuff[2] := $00;        // P1
  SendBuff[3] := $00;        // P2
  SendBuff[4] := $04;        // P3
  for indx := 0 to 3 do
    SendBuff[indx + 5] := RevDebData[indx];
  SendLen := SendBuff[4] + 5;
  RecvLen := $02;
  tmpStr := '';
  for indx := 0 to SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  retCode := SendAPDUandDisplay(0, tmpStr);
  if retCode <> SCARD_S_SUCCESS then
    begin
      RevokeDebit := retCode;
      Exit;
    end;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if ACOSError(RecvBuff[0], RecvBuff[1]) then
    begin
      retCode := INVALID_SW1SW2;
      RevokeDebit := retCode;
      Exit;
    end;
  if tmpStr <> '90 00 ' then
    begin
      displayOut(3, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      RevokeDebit := retCode;
      Exit;
    end;
  RevokeDebit := retCode;

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
                           PChar(MainAccount.cbReader.Text),
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

function ValidTemplate(): Boolean;
begin

  ValidTemplate := False;
  case MainAccount.rgOption.ItemIndex of
    0: begin
         if Length(MainAccount.tCredit.Text) < 8 then
           begin
             MainAccount.tCredit.SelectAll;
             MainAccount.tCredit.SetFocus;
             Exit;
           end;
         if Length(MainAccount.tDebit.Text) < 8 then
           begin
             MainAccount.tDebit.SelectAll;
             MainAccount.tDebit.SetFocus;
             Exit;
           end;
         if Length(MainAccount.tCertify.Text) < 8 then
           begin
             MainAccount.tCertify.SelectAll;
             MainAccount.tCertify.SetFocus;
             Exit;
           end;
         if Length(MainAccount.tRevDebit.Text) < 8 then
           begin
             MainAccount.tRevDebit.SelectAll;
             MainAccount.tRevDebit.SetFocus;
             Exit;
           end;
       end;
    1: begin
         if Length(MainAccount.tCredit.Text) < 16 then
           begin
             MainAccount.tCredit.SelectAll;
             MainAccount.tCredit.SetFocus;
             Exit;
           end;
         if Length(MainAccount.tDebit.Text) < 16 then
           begin
             MainAccount.tDebit.SelectAll;
             MainAccount.tDebit.SetFocus;
             Exit;
           end;
         if Length(MainAccount.tCertify.Text) < 16 then
           begin
             MainAccount.tCertify.SelectAll;
             MainAccount.tCertify.SetFocus;
             Exit;
           end;
         if Length(MainAccount.tRevDebit.Text) < 16 then
           begin
             MainAccount.tRevDebit.SelectAll;
             MainAccount.tRevDebit.SetFocus;
             Exit;
           end;
       end;
  end;
  ValidTemplate := True;

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

procedure TMainAccount.bQuitClick(Sender: TObject);
begin

  if ConnActive then
    begin
      retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
      ConnActive := False;
    end;

  retCode := SCardReleaseContext(hCard);
  Application.Terminate;

end;

procedure TMainAccount.FormActivate(Sender: TObject);
begin
  InitMenu();
end;

procedure TMainAccount.bInitClick(Sender: TObject);
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

  MainAccount.cbReader.Clear;;
  LoadListToControl(MainAccount.cbReader,@buffer,bufferLen);
  MainAccount.cbReader.ItemIndex := 0;
  AddButtons();

end;

procedure TMainAccount.bResetClick(Sender: TObject);
begin

  if ConnActive then
    begin
      retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
      ConnActive := False;
    end;

  retCode := SCardReleaseContext(hCard);
  rgOption.ItemIndex := -1;
  chk_dbc.Checked := false;
  InitMenu();

end;

procedure TMainAccount.bConnectClick(Sender: TObject);
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

  ConnActive := True;
  bFormat.Enabled := True;
  gbKeys.Enabled := True;
  gbFunctions.Enabled := True;
  rgOption.Enabled := True;
  rgOption.ItemIndex := 0;
  ClearTextFields();
  tCredit.MaxLength := 8;
  tDebit.MaxLength := 8;
  tCertify.MaxLength := 8;
  tRevDebit.MaxLength := 8;

end;

procedure TMainAccount.cbReaderChange(Sender: TObject);
begin

  bFormat.Enabled := False;
  gbKeys.Enabled := False;
  rgOption.ItemIndex := -1;
  gbFunctions.Enabled := False;
  rgOption.Enabled := False;
  ClearTextFields();

  if ConnActive then
  begin
    retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
    ConnActive := False;
  end;

end;

procedure TMainAccount.bFormatClick(Sender: TObject);
var tmpStr: String;
    indx: integer;
    tmpArray: array[0..31] of Byte;
begin

  // 1. Validate template data for keys
  if not ValidTemplate then
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
  //    This step will define the Option registers,
  //    Security Option registers and Personalization bit
  //    are not set
  case rgOption.ItemIndex of
    0: tmpArray[0] :=  $29;    // 29h  Only REV_DEB, DEB_MAC and Account bits are set
    1: tmpArray[0] :=  $2B;    // 2Bh  REV_DEB, DEB_MAC, 3-DES and Account bits are set
  end;
  tmpArray[1] :=  $00;        // 00    Security option register
  tmpArray[2] :=  $00;        // 00    No of user files
  tmpArray[3] :=  $00;        // 00    Personalization bit
  retCode := writeRecord(0, $00, $04, $04, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  DisplayOut(0, 0, 'File FF 02 is updated.');

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
  displayOut(3, 0, 'Account files are enabled.');
  ConnActive := True;

  // 7. Submit IC Code to write into FF 05 and FF 06
  retCode := SubmitIC();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 8. Select FF 05
  retCode := selectFile($FF, $05);
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

  // 9. Write to FF 05
  // 9.1. Record 00
  tmpArray[0] :=  $00;        // TRANSTYP 0
  tmpArray[1] :=  $00;        // (3 bytes
  tmpArray[2] :=  $00;        //  reserved for
  tmpArray[3] :=  $00;        //  BALANCE 0)
  retCode := writeRecord(0, $00, $04, $04, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 9.2.Record 01
  tmpArray[0] :=  $00;        // (2 bytes reserved
  tmpArray[1] :=  $00;        //  for ATC 0)
  tmpArray[2] :=  $01;        // Set CHECKSUM 0
  tmpArray[3] :=  $00;        // 00h
  retCode := writeRecord(0, $01, $04, $04, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 9.3. Record 02
  tmpArray[0] :=  $00;        // TRANSTYP 1
  tmpArray[1] :=  $00;        // (3 bytes
  tmpArray[2] :=  $00;        //  reserved for
  tmpArray[3] :=  $00;        //  BALANCE 1)
  retCode := writeRecord(0, $02, $04, $04, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 9.4.Record 03
  tmpArray[0] :=  $00;        // (2 bytes reserved
  tmpArray[1] :=  $00;        //  for ATC 1)
  tmpArray[2] :=  $01;        // Set CHECKSUM 1
  tmpArray[3] :=  $00;        // 00h
  retCode := writeRecord(0, $03, $04, $04, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 9.5.Record 04
  // Maximum balance arbitrarily set to FFFFFF or 16.7M units
  tmpArray[0] :=  $FF;        // (3 bytes
  tmpArray[1] :=  $FF;        //  initialized for
  tmpArray[2] :=  $FF;        //  MAX BALANCE)
  tmpArray[3] :=  $00;        // 00h
  retCode := writeRecord(0, $04, $04, $04, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 9.6.Record 05
  tmpArray[0] :=  $00;        // (4 bytes
  tmpArray[1] :=  $00;        //  reserved
  tmpArray[2] :=  $00;        //  for
  tmpArray[3] :=  $00;        //  AID)
  retCode := writeRecord(0, $05, $04, $04, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 9.7.Record 06
  tmpArray[0] :=  $00;        // (4 bytes
  tmpArray[1] :=  $00;        //  reserved
  tmpArray[2] :=  $00;        //  for
  tmpArray[3] :=  $00;        //  TTREF_C)
  retCode := writeRecord(0, $06, $04, $04, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 9.8.Record 07
  tmpArray[0] :=  $00;        // (4 bytes
  tmpArray[1] :=  $00;        //  reserved
  tmpArray[2] :=  $00;        //  for
  tmpArray[3] :=  $00;        //  TTREF_D)
  retCode := writeRecord(0, $07, $04, $04, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  DisplayOut(0, 0, 'File FF 05 is updated.');

  // 10. Select FF 06
  retCode := selectFile($FF, $06);
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

  case rgOption.ItemIndex of
    0: begin
         // 11a. Write to FF 06
         // 11a.1. Record 00 for Debit key
         tmpStr := tDebit.Text;
         for indx := 0 to Length(tmpStr)-1 do
           begin
             tmpArray[indx] := ord(tmpStr[indx+1]);
           end;
         retCode := writeRecord(0, $00, tDebit.MaxLength, Length(tmpStr), tmpArray);
         if retCode <> SCARD_S_SUCCESS then
           Exit;

         // 11a.2. Record 01 for Credit key
         tmpStr := tCredit.Text;
         for indx := 0 to Length(tmpStr)-1 do
           begin
             tmpArray[indx] := ord(tmpStr[indx+1]);
           end;
         retCode := writeRecord(0, $01, tCredit.MaxLength, Length(tmpStr), tmpArray);
         if retCode <> SCARD_S_SUCCESS then
           Exit;

         // 11a.3. Record 02 for Certify key
         tmpStr := tCertify.Text;
         for indx := 0 to Length(tmpStr)-1 do
           begin
             tmpArray[indx] := ord(tmpStr[indx+1]);
           end;
         retCode := writeRecord(0, $02, tCertify.MaxLength, Length(tmpStr), tmpArray);
         if retCode <> SCARD_S_SUCCESS then
           Exit;

         // 11a.3. Record 03 for Revoke Debit key
         tmpStr := tRevDebit.Text;
         for indx := 0 to Length(tmpStr)-1 do
           begin
             tmpArray[indx] := ord(tmpStr[indx+1]);
           end;
         retCode := writeRecord(0, $03, tRevDebit.MaxLength, Length(tmpStr), tmpArray);
         if retCode <> SCARD_S_SUCCESS then
           Exit;
       end;
    1: begin
         // 11b. Write to FF 06
         // 11b.1. Record 00 for Debit key
         tmpStr := tDebit.Text;
         for indx := 0 to 7 do     // Left half of Debit key
           begin
             tmpArray[indx] := ord(tmpStr[indx+1]);
           end;
         retCode := writeRecord(0, $04, 8, 8, tmpArray);
         if retCode <> SCARD_S_SUCCESS then
           Exit;
         for indx := 8 to 15 do    // Right half of Debit key
           begin
             tmpArray[indx-8] := ord(tmpStr[indx+1]);
           end;
         retCode := writeRecord(0, $00, 8, 8, tmpArray);
         if retCode <> SCARD_S_SUCCESS then
           Exit;

         // 11b.2. Record 01 for Credit key
         tmpStr := tCredit.Text;
         for indx := 0 to 7 do     // Left half of Credit key
           begin
             tmpArray[indx] := ord(tmpStr[indx+1]);
           end;
         retCode := writeRecord(0, $05, 8, 8, tmpArray);
         if retCode <> SCARD_S_SUCCESS then
           Exit;
         for indx := 8 to 15 do    // Right half of Credit key
           begin
             tmpArray[indx-8] := ord(tmpStr[indx+1]);
           end;
         retCode := writeRecord(0, $01, 8, 8, tmpArray);
         if retCode <> SCARD_S_SUCCESS then
           Exit;

         // 11b.3. Record 02 for Certify key
         tmpStr := tCertify.Text;
         for indx := 0 to 7 do     // Left half of Certify key
           begin
             tmpArray[indx] := ord(tmpStr[indx+1]);
           end;
         retCode := writeRecord(0, $06, 8, 8, tmpArray);
         if retCode <> SCARD_S_SUCCESS then
           Exit;
         tmpStr := tCertify.Text;
         for indx := 8 to 15 do    // Right half of Certify key
           begin
             tmpArray[indx-8] := ord(tmpStr[indx+1]);
           end;
         retCode := writeRecord(0, $02, 8, 8, tmpArray);
         if retCode <> SCARD_S_SUCCESS then
           Exit;

         // 11b.3. Record 03 for Revoke Debit key
         tmpStr := tRevDebit.Text;
         for indx := 0 to 7 do     // Left half of Revoke Debit key
           begin
             tmpArray[indx] := ord(tmpStr[indx+1]);
           end;
         retCode := writeRecord(0, $07, 8, 8, tmpArray);
         if retCode <> SCARD_S_SUCCESS then
           Exit;
         for indx := 8 to 15 do    // Right half of Revoke Debit key
           begin
             tmpArray[indx-8] := ord(tmpStr[indx+1]);
           end;
         retCode := writeRecord(0, $03, 8, 8, tmpArray);
         if retCode <> SCARD_S_SUCCESS then
           Exit;
       end;
  end;

  DisplayOut(0, 0, 'File FF 06 is updated.');
  ClearTextFields();

end;

procedure TMainAccount.rgOptionClick(Sender: TObject);
begin

  ClearTextFields();
  case MainAccount.rgOption.ItemIndex of
    0: begin
         MainAccount.tCredit.MaxLength := 8;
         MainAccount.tDebit.MaxLength := 8;
         MainAccount.tCertify.MaxLength := 8;
         MainAccount.tRevDebit.MaxLength := 8;
       end;
    1: begin
         MainAccount.tCredit.MaxLength := 16;
         MainAccount.tDebit.MaxLength := 16;
         MainAccount.tCertify.MaxLength := 16;
         MainAccount.tRevDebit.MaxLength := 16;
       end;
  end;
  MainAccount.tCredit.SetFocus;
  
end;

procedure TMainAccount.tCreditKeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin

  case rgOption.ItemIndex of
    0: if Length(tCredit.Text) > 7 then
         tDebit.SetFocus;
    1: if Length(tCredit.Text) > 15 then
         tDebit.SetFocus;
  end;

end;

procedure TMainAccount.tDebitKeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin

  case rgOption.ItemIndex of
    0: if Length(tDebit.Text) > 7 then
         tCertify.SetFocus;
    1: if Length(tDebit.Text) > 15 then
         tCertify.SetFocus;
  end;

end;

procedure TMainAccount.tCertifyKeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin

  case rgOption.ItemIndex of
    0: if Length(tCertify.Text) > 7 then
         tRevDebit.SetFocus;
    1: if Length(tCertify.Text) > 15 then
         tRevDebit.SetFocus;
  end;

end;

procedure TMainAccount.bInquireClick(Sender: TObject);
var indx: integer;
    tmpStr : String;
    tmpArray: array[0..30] of Byte;
    tmpBalance: DWORD;
    tmpKey : array [0..15] of byte;	 // certify key to verify MAC
    LastTran : BYTE;
    TTREFc : array [0..3] of byte;
    TTREFd : array [0..3] of byte;
    ATREF : array [0..5] of byte;
begin

  // 1. Check if Certify key is provided
  case rgOption.ItemIndex of
    0: begin
         if Length(tCertify.Text) < 8 then
           begin
             tCertify.SelectAll;
             tCertify.SetFocus;
             Exit;
           end;
       end;
    1: begin
         if Length(tCertify.Text) < 16 then
           begin
             tCertify.SelectAll;
             tCertify.SetFocus;
             Exit;
           end;
       end;
  end;
  
  // 2. Check if card inserted is an ACOS card
  if not checkACOS then
    begin
      displayOut(0, 0, 'Please insert an ACOS card.');
      Exit;
    end;
  displayOut(0, 0, 'ACOS card detected.');

  // 3. Issue INQUIRE ACCOUNT command using any arbitrary data and Certify key
  //    Arbitrary data is 1111h
  for indx := 0 to 3 do
    tmpArray[indx] := $01;
  retCode := InquireAccount($02, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 4. Issue GET RESPONSE command with Le = 19h or 25 bytes
  retCode := GetResponse();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 5. Check integrity of data returned by card
  // 5.1. Build MAC input data
 	// 5.1.1. Extract the info from ACOS card in Dataout
	LastTran := RecvBuff[4];
	tmpBalance := RecvBuff[7];
	tmpBalance := tmpBalance + (RecvBuff[6] * 256);
	tmpBalance := tmpBalance + (RecvBuff[5] * 256 * 256);
 	move(RecvBuff[17], TTREFc, 4);
	move(RecvBuff[21], TTREFd, 4);
	move(RecvBuff[8], ATREF, 6);

  // 5.1.2. Move data from ACOS card as input to MAC calculations
	tmpArray[4] := RecvBuff[4]; // 4 BYTE MAC + LAST TRANS TYPE
	move(RecvBuff[5], tmpArray[5], 3); // COPY BALANCE
	move(RecvBuff[8], tmpArray[8], 6); // COPY ATTREF
	tmpArray[14] := $00;
  tmpArray[15] := $00;
	move(TTREFc, tmpArray[16], 4);	// copy TTREFc
	move(TTREFd, tmpArray[20], 4);	// copy TTREFd

  // 5.2. Generate applicable MAC values
  tmpStr := tCertify.Text;
  for indx := 0 to Length(tmpStr)-1 do
    tmpKey[indx] := ord(tmpStr[indx+1]);
  case rgOption.ItemIndex of
    0: MAC(@tmpArray, @tmpKey);
    1: TripleMAC(@tmpArray, @tmpKey);
  end;

  // 5.3. Compare MAC values
  if CompareMem (@tmpArray, @RecvBuff, 4) then
    displayOut(0, 0, 'MAC is correct, data integrity is confirmed.')
  else
    displayOut(0, 0, 'MAC is incorrect, data integrity is jeopardized.');

  // 6. Display relevant data from ACOS card
  case LastTran of
    1: tmpStr := 'DEBIT';
    2: tmpStr := 'REVOKE DEBIT';
    3: tmpStr := 'CREDIT';
  else
    tmpStr := 'NOT DEFINED';
  end;
  displayOut(3, 0, 'Last transaction is ' + tmpStr + '.');
  ClearTextFields();
  chk_dbc.Checked := false;
  tAmount.Text := IntToStr(tmpBalance);

end;

procedure TMainAccount.bCreditClick(Sender: TObject);
var indx: integer;
    tmpStr : String;
    Amount : DWORD;	                 // amount to be added to balance
    tmpArray: array[0..16] of Byte;
    tmpKey : array [0..15] of byte;	 // Credit key to verify MAC
    TTREFc : array [0..3] of byte;
    ATREF : array [0..5] of byte;
begin

  // 1. Check if Credit key and data amount are provided
  case rgOption.ItemIndex of
    0: begin
         if Length(tCredit.Text) < 8 then
           begin
             tCredit.SelectAll;
             tCredit.SetFocus;
             Exit;
           end;
       end;
    1: begin
         if Length(tCredit.Text) < 16 then
           begin
             tCredit.SelectAll;
             tCredit.SetFocus;
             Exit;
           end;
       end;
  end;
  if tAmount.text <> '' then
    begin
      if ((StrToInt64(tAmount.Text) > 16777215) or (StrToInt64(tAmount.Text) < 1)) then
        begin
          tAmount.SelectAll;
          tAmount.SetFocus;
          Exit;
        end;
    end
  else
    begin
      tAmount.SetFocus;
      Exit;
    end;

  // 2. Check if card inserted is an ACOS card
  if not checkACOS then
    begin
      displayOut(0, 0, 'Please insert an ACOS card.');
      Exit;
    end;
  displayOut(0, 0, 'ACOS card detected.');

  // 3. Issue INQUIRE ACCOUNT command using any arbitrary data and Credit key
  //    Arbitrary data is 1111h
  for indx := 0 to 3 do
    tmpArray[indx] := $01;
  retCode := InquireAccount($02, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 4. Issue GET RESPONSE command with Le = 19h or 25 bytes
  retCode := GetResponse();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 5. Store ACOS card values for TTREFc and ATREF
 	move(RecvBuff[17], TTREFc, 4);
	move(RecvBuff[8], ATREF, 6);

 	// 6. Prepare MAC data block: E2 + AMT + TTREFc + ATREF + 00 + 00
	//    use tmpArray as the data block
  Amount := StrToInt64(tAmount.Text);
	tmpArray[0] := $E2;
	tmpArray[1] := (Amount shr 16) and $FF;	      // Amount MSByte
	tmpArray[2] := (Amount shr 8) and $FF;	      // Amount middle byte
	tmpArray[3] := Amount and $FF;			          // Amount LSByte
	move (TTREFc, tmpArray[4], 4);
	move (ATREF, tmpArray[8], 6);
	tmpArray[13] := tmpArray[13] + 1;							// increment last byte of ATREF
	tmpArray[14] := $00;
  tmpArray[15] := $00;

  // 7. Generate applicable MAC values, MAC result will be stored in tmpArray
  tmpStr := tCredit.Text;
  for indx := 0 to Length(tmpStr)-1 do
    tmpKey[indx] := ord(tmpStr[indx+1]);
  case rgOption.ItemIndex of
    0: MAC(@tmpArray, @tmpKey);
    1: TripleMAC(@tmpArray, @tmpKey);
  end;

  // 8. Execute Credit command data and execute credit command
  //    Using tmpArray, the first four bytes are carried over
 	tmpArray[4] := (Amount shr 16) and $FF;	 // Amount MSB
	tmpArray[5] := (Amount shr 8) and $FF;	 // middle byte of Amount
	tmpArray[6] := Amount and $FF;	         // LSB of Amount
	move (TTREFc, tmpArray[7], 4);	         // TTREFc
  retCode := CreditAmount(tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  displayOut(3, 0, 'Credit transaction completed');
  ClearTextFields();
  chk_dbc.Checked := false;
end;

procedure TMainAccount.tAmountKeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then
    if Not (Key in ['0'..'9'])then
      Key := Chr($00);
end;

procedure TMainAccount.bDebitClick(Sender: TObject);
var indx: integer;
    tmpStr : String;
    Amount : DWORD;	                 // amount to be added to balance
    tmpArray: array[0..16] of Byte;
    tmpKey : array [0..15] of byte;	 // Debit key to verify MAC
    TTREFd : array [0..3] of byte;
    ATREF : array [0..5] of byte;
    tmpbalance : array [0..3] of byte;
    new_balance : DWORD;
begin

  // 1. Check if Debit key and data amount are provided
  case rgOption.ItemIndex of
    0: begin
         if Length(tDebit.Text) < 8 then
           begin
             tDebit.SelectAll;
             tDebit.SetFocus;
             Exit;
           end;
       end;
    1: begin
         if Length(tDebit.Text) < 16 then
           begin
             tDebit.SelectAll;
             tDebit.SetFocus;
             Exit;
           end;
       end;
  end;
  if tAmount.text <> '' then
    begin
      if ((StrToInt64(tAmount.Text) > 16777215) or (StrToInt64(tAmount.Text) < 1)) then
        begin
          tAmount.SelectAll;
          tAmount.SetFocus;
          Exit;
        end;
    end
  else
    begin
      tAmount.SetFocus;
      Exit;
    end;

  // 2. Check if card inserted is an ACOS card
  if not checkACOS then
    begin
      displayOut(0, 0, 'Please insert an ACOS card.');
      Exit;
    end;
  displayOut(0, 0, 'ACOS card detected.');

  // 3. Issue INQUIRE ACCOUNT command using any arbitrary data and Debit key
  //    Arbitrary data is 1111h
  for indx := 0 to 3 do
    tmpArray[indx] := $01;
  retCode := InquireAccount($02, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 4. Issue GET RESPONSE command with Le = 19h or 25 bytes
  retCode := GetResponse();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  tmpBalance[1] := RecvBuff[7];
  tmpBalance[2] := RecvBuff[6];
  tmpBalance[2] := tmpBalance[2] * 256;
  tmpBalance[3] := RecvBuff[5];
  tmpBalance[3] := tmpBalance[3] * 256;
  tmpBalance[3] := tmpBalance[3] * 256;
  tmpBalance[0] := tmpBalance[1] + tmpBalance[2] + tmpBalance[3];

  // 5. Store ACOS card values for TTREFd and ATREF
 	move(RecvBuff[21], TTREFd, 4);
	move(RecvBuff[8], ATREF, 6);

 	// 6. Prepare MAC data block: E6 + AMT + TTREFd + ATREF + 00 + 00
	//    use tmpArray as the data block
  Amount := StrToInt64(tAmount.Text);
	tmpArray[0] := $E6;
	tmpArray[1] := (Amount shr 16) and $FF;	      // Amount MSByte
	tmpArray[2] := (Amount shr 8) and $FF;	      // Amount middle byte
	tmpArray[3] := Amount and $FF;			          // Amount LSByte
	move (TTREFd, tmpArray[4], 4);
	move (ATREF, tmpArray[8], 6);
	tmpArray[13] := tmpArray[13] + 1;							// increment last byte of ATREF
	tmpArray[14] := $00;
  tmpArray[15] := $00;

  // 7. Generate applicable MAC values, MAC result will be stored in tmpArray
  tmpStr := tDebit.Text;
  for indx := 0 to Length(tmpStr)-1 do
    tmpKey[indx] := ord(tmpStr[indx+1]);
  case rgOption.ItemIndex of
    0: MAC(@tmpArray, @tmpKey);
    1: TripleMAC(@tmpArray, @tmpKey);
  end;

  // 8. Execute Debit command data and execute credit command
  //    Using tmpArray, the first four bytes are carried over
 	tmpArray[4] := (Amount shr 16) and $FF;	 // Amount MSB
	tmpArray[5] := (Amount shr 8) and $FF;	 // middle byte of Amount
	tmpArray[6] := Amount and $FF;	         // LSB of Amount
	move (TTREFd, tmpArray[7], 4);	         // TTREFd

  if chk_dbc.Checked = false then
  begin
    retCode := DebitAmount(tmpArray);
    if retCode <> SCARD_S_SUCCESS then
    Exit;
  end
  else
  begin
    retCode := DebitAmountwithDBC(tmpArray);
    if retCode <> SCARD_S_SUCCESS then
    Exit;

    //Issue GET RESPONSE command with Le = 04h
    retCode := GetResponseDebitCertificate();
    if retCode <> SCARD_S_SUCCESS then
    Exit;

    //Prepare MAC data block: 01 + New Balance + ATC + TTREFD + 00 + 00 + 00
    //    use tmpArray as the data block
    //NOTE: For further explanation in this part please refer to the documentation.
    Amount := StrToInt64(tAmount.Text);
    new_balance := (tmpBalance[0] - Amount);


    tmpArray[0] := $01;

	  tmpArray[1] := (new_balance shr 16) and $FF;	      // New Balance MSByte
  	tmpArray[2] := (new_balance shr 8) and $FF;	      // New Balance middle byte
	  tmpArray[3] := new_balance and $FF;			          // New Balance LSByte

    tmpArray[4] := (Amount shr 16) and $FF;	      // Amount MSByte
	  tmpArray[5] := (Amount shr 8) and $FF;	      // Amount middle byte
  	tmpArray[6] := Amount and $FF;			          // Amount LSByte

    tmpArray[7]  := ATREF[4];
    tmpArray[8]  := (ATREF[5] + 1);               //Increment ATC after every transaction
    move (TTREFd, tmpArray[9], 4);
    tmpArray[13] := $00;
    tmpArray[14] := $00;
    tmpArray[15] := $00;

    //Generate applicable MAC values, MAC result will be stored in tmpArray
    tmpStr := tDebit.Text;
    for indx := 0 to Length(tmpStr)-1 do
      tmpKey[indx] := ord(tmpStr[indx+1]);
    case rgOption.ItemIndex of
      0: MAC(@tmpArray, @tmpKey);
      1: TripleMAC(@tmpArray, @tmpKey);
    end;

    for indx := 0 to 3 do
    begin
      if RecvBuff[indx] <> tmpArray[indx] then
      begin
        displayOut(0, 0, 'Debit Certificate Failed.');
        Exit;
      end;
    end;

    displayOut(0, 0, 'Debit Certificate Verified.');

  end;


  displayOut(3, 0, 'Debit transaction completed');
  ClearTextFields();
  chk_dbc.Checked := false;

end;

procedure TMainAccount.bRevDebitClick(Sender: TObject);
var indx: integer;
    tmpStr : String;
    Amount : DWORD;	                 // amount to be added to balance
    tmpArray: array[0..16] of Byte;
    tmpKey : array [0..15] of byte;	 // Debit key to verify MAC
    TTREFd : array [0..3] of byte;
    ATREF : array [0..5] of byte;
begin

  // 1. Check if Revoke Debit key and data amount are provided
  case rgOption.ItemIndex of
    0: begin
         if Length(tRevDebit.Text) < 8 then
           begin
             tRevDebit.SelectAll;
             tRevDebit.SetFocus;
             Exit;
           end;
       end;
    1: begin
         if Length(tRevDebit.Text) < 16 then
           begin
             tRevDebit.SelectAll;
             tRevDebit.SetFocus;
             Exit;
           end;
       end;
  end;
  if tAmount.text <> '' then
    begin
      if ((StrToInt64(tAmount.Text) > 16777215) or (StrToInt64(tAmount.Text) < 1)) then
        begin
          tAmount.SelectAll;
          tAmount.SetFocus;
          Exit;
        end;
    end
  else
    begin
      tAmount.SetFocus;
      Exit;
    end;

  // 2. Check if card inserted is an ACOS card
  if not checkACOS then
    begin
      displayOut(0, 0, 'Please insert an ACOS card.');
      Exit;
    end;
  displayOut(0, 0, 'ACOS card detected.');

  // 3. Issue INQUIRE ACCOUNT command using any arbitrary data and Revoke Debit key
  //    Arbitrary data is 1111h
  for indx := 0 to 3 do
    tmpArray[indx] := $01;
  retCode := InquireAccount($02, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 4. Issue GET RESPONSE command with Le = 19h or 25 bytes
  retCode := GetResponse();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 5. Store ACOS card values for TTREFd and ATREF
 	move(RecvBuff[21], TTREFd, 4);
	move(RecvBuff[8], ATREF, 6);

 	// 6. Prepare MAC data block: E8 + AMT + TTREFd + ATREF + 00 + 00
	//    use tmpArray as the data block
  Amount := StrToInt64(tAmount.Text);
	tmpArray[0] := $E8;
	tmpArray[1] := (Amount shr 16) and $FF;	      // Amount MSByte
	tmpArray[2] := (Amount shr 8) and $FF;	      // Amount middle byte
	tmpArray[3] := Amount and $FF;			          // Amount LSByte
	move (TTREFd, tmpArray[4], 4);
	move (ATREF, tmpArray[8], 6);
	tmpArray[13] := tmpArray[13] + 1;							// increment last byte of ATREF
	tmpArray[14] := $00;
  tmpArray[15] := $00;

  // 7. Generate applicable MAC values, MAC result will be stored in tmpArray
  tmpStr := tRevDebit.Text;
  for indx := 0 to Length(tmpStr)-1 do
    tmpKey[indx] := ord(tmpStr[indx+1]);
  case rgOption.ItemIndex of
    0: MAC(@tmpArray, @tmpKey);
    1: TripleMAC(@tmpArray, @tmpKey);
  end;

  // 8. Execute Revoke Debit command data and execute credit command
  //    Using tmpArray, the first four bytes storing the MAC value are carried over
  retCode := RevokeDebit(tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  displayOut(3, 0, 'Revoke Debit transaction completed');
  ClearTextFields();
  chk_dbc.Checked := false;
end;

end.
