//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              ACOSBinary.dpr
//
//  Description:       This sample program outlines the steps on how to
//                     implement the binary file support in ACOS3-24K
//
//  Author:	           Jose Isagani R. Mission
//
//  Date:	             June 20, 2008
//
//  Revision Trail:   (Date/Author/Description)
//
//======================================================================
unit ACOSBinProg;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ACSModule, StdCtrls, ComCtrls;

Const MAX_BUFFER_LEN    = 2048;
Const INVALID_SW1SW2    = -450;

type
  TMainACOSBin = class(TForm)
    Label1: TLabel;
    cbReader: TComboBox;
    mMsg: TRichEdit;
    bInit: TButton;
    bConnect: TButton;
    bClear: TButton;
    bReset: TButton;
    bQuit: TButton;
    gbFormat: TGroupBox;
    bFormat: TButton;
    tFileID1: TEdit;
    tFileID2: TEdit;
    Label2: TLabel;
    tFileLen1: TEdit;
    tFileLen2: TEdit;
    Label3: TLabel;
    gbReadWrite: TGroupBox;
    Label4: TLabel;
    tFID1: TEdit;
    tFID2: TEdit;
    tData: TRichEdit;
    Label5: TLabel;
    bBinRead: TButton;
    bBinWrite: TButton;
    Label6: TLabel;
    tOffset1: TEdit;
    tOffset2: TEdit;
    Label7: TLabel;
    tLen: TEdit;
    procedure FormActivate(Sender: TObject);
    procedure bResetClick(Sender: TObject);
    procedure bQuitClick(Sender: TObject);
    procedure bInitClick(Sender: TObject);
    procedure bConnectClick(Sender: TObject);
    procedure bFormatClick(Sender: TObject);
    procedure tFileID1KeyPress(Sender: TObject; var Key: Char);
    procedure bBinReadClick(Sender: TObject);
    procedure bBinWriteClick(Sender: TObject);
    procedure bClearClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  MainACOSBin: TMainACOSBin;
  hContext    : SCARDCONTEXT;
  hCard       : SCARDCONTEXT;
  ioRequest   : SCARD_IO_REQUEST;
  retCode     : Integer;
  dwActProtocol, BufferLen  : DWORD;
  SendBuff, RecvBuff        : array [0..262] of Byte;
  SendLen, RecvLen          : DWORD;
  Buffer      : array [0..MAX_BUFFER_LEN] of char;
  ConnActive  : Boolean;

procedure EnableButtons();
procedure ClearBuffers();
procedure InitMenu();
procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
procedure getBinaryData();
function SendAPDUandDisplay(SendType: integer): integer;
function SubmitIC(): integer;
function selectFile(HiAddr: Byte; LoAddr: Byte): integer;
function readRecord(RecNo: Byte; DataLen: Byte): integer;
function writeRecord(caseType: Integer; RecNo: Byte; maxDataLen: Byte; DataLen: Byte; DataIn:array of Byte): integer;
function writeBinary(caseType: Integer; HiByte, LoByte, DataLen: Byte; DataIn:array of Byte): integer;
function readBinary(HiByte, LoByte: Byte; DataLen: Byte): integer;

implementation

{$R *.dfm}

procedure EnableButtons();
begin

  MainACOSBin.bInit.Enabled := False;
  MainACOSBin.bConnect.Enabled := True;
  MainACOSBin.bReset.Enabled := True;

end;

procedure ClearBuffers();
var indx: integer;
begin

  for indx := 0 to 262 do begin
    SendBuff[indx] := $00;
    RecvBuff[indx] := $00;
  end;

end;

procedure InitMenu();
begin

  connActive := False;
  MainACOSBin.cbReader.Clear;
  MainACOSBin.mMsg.Clear;
  DisplayOut(0, 0, 'Program ready');
  MainACOSBin.bInit.Enabled := True;
  MainACOSBin.bConnect.Enabled := False;
  MainACOSBin.bReset.Enabled := False;
  MainACOSBin.tFileID1.Text := '';
  MainACOSBin.tFileID2.Text := '';
  MainACOSBin.tFileLen1.Text := '';
  MainACOSBin.tFileLen2.Text := '';
  MainACOSBin.gbFormat.Enabled := False;
  MainACOSBin.tFID1.Text := '';
  MainACOSBin.tFID2.Text := '';
  MainACOSBin.tOffset1.Text := '';
  MainACOSBin.tOffset2.Text := '';
  MainACOSBin.tLen.Text := '';
  MainACOSBin.tData.Clear;
  MainACOSBin.gbReadWrite.Enabled := False;

end;

procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
begin

  case errType of
    0: MainACOSBin.mMsg.SelAttributes.Color := clTeal;      // Notifications
    1: begin                                                // Error Messages
         MainACOSBin.mMsg.SelAttributes.Color := clRed;
         PrintText := GetScardErrMsg(retVal);
       end;
    2: begin
         MainACOSBin.mMsg.SelAttributes.Color := clBlack;
         PrintText := '< ' + PrintText;                      // Input data
       end;
    3: begin
         MainACOSBin.mMsg.SelAttributes.Color := clBlack;
         PrintText := '> ' + PrintText;                      // Output data
       end;
    4: MainACOSBin.mMsg.SelAttributes.Color := clRed;        // For ACOS1 error
  end;
  MainACOSBin.mMsg.Lines.Add(PrintText);
  MainACOSBin.mMsg.SelAttributes.Color := clBlack;
  MainACOSBin.mMsg.SetFocus;

end;

procedure getBinaryData();
var tmpStr: string;
    indx: integer;
    tmpLen: integer;
begin

  // 1. Send IC Code
  retCode := SubmitIC();
  if retCode <> SCARD_S_SUCCESS then begin
    displayOut(4, 0, 'Insert ACOS3-24K card on contact card reader.');
    Exit;
  end;

  // 2. Select FF 04
  retCode := selectFile($FF, $04);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> '90 00 ' then begin
    displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
    retCode := INVALID_SW1SW2;
    Exit;
  end;

  // 3. Read first record
  retCode := readRecord($00, $07);
  if retCode <> SCARD_S_SUCCESS then begin
    displayOut(4, 0, 'Card may not have been formatted yet.');
    Exit;
  end;

  // Provide parameter to Data Input Box
  MainACOSBin.tFID1.Text := Format('%.2x',[RecvBuff[4]]);
  MainACOSBin.tFID2.Text := Format('%.2x',[RecvBuff[5]]);
	tmpLen := RecvBuff[1];
	tmpLen := tmpLen + (RecvBuff[0] * 256);
  MainACOSBin.tData.MaxLength := tmpLen;


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
       for indx := 0 to (RecvLen-1) do
         tmpStr := tmpStr + Format('%.02X ', [(RecvBuff[indx])]);
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

  retCode := SendAPDUandDisplay(0);
  if retCode <> SCARD_S_SUCCESS then begin
    SubmitIC := retCode;
    Exit;
  end ;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> '90 00 ' then begin
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

  retCode := SendAPDUandDisplay(2);
  if retCode <> SCARD_S_SUCCESS then begin
    selectFile := retCode;
    Exit;
  end ;
  selectFile := retCode;

end;

function writeRecord(caseType: Integer; RecNo: Byte; maxDataLen: Byte; DataLen: Byte; DataIn:array of Byte): integer;
var indx: integer;
    tmpStr: String;
begin

  if caseType = 1 then begin  // If card data is to be erased before writing new data
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

    retCode := SendAPDUandDisplay(0);
    if retCode <> SCARD_S_SUCCESS then begin
      writeRecord := retCode;
      Exit;
    end;
    tmpStr := '';
    for indx := 0 to 1 do
      tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
    if tmpStr <> '90 00 ' then begin
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

  retCode := SendAPDUandDisplay(0);
  if retCode <> SCARD_S_SUCCESS then begin
    writeRecord := retCode;
    Exit;
  end;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> '90 00 ' then begin
    displayOut(3, 0, 'The return string is invalid. Value: ' + tmpStr);
    retCode := INVALID_SW1SW2;
    writeRecord := retCode;
    Exit;
  end;
  writeRecord := retCode;

end;

function writeBinary(caseType: Integer; HiByte, LoByte, DataLen: Byte; DataIn:array of Byte): integer;
var indx: integer;
    tmpStr: String;
begin

  if caseType = 1 then begin  // If card data is to be erased before writing new data
    // 1. Re-initialize card values to $00
    ClearBuffers();
    SendBuff[0] := $80;        // CLA
    SendBuff[1] := $D0;        // INS
    SendBuff[2] := HiByte;     // P1    High Byte Address
    SendBuff[3] := LoByte;     // P2    Low Byte Address
    SendBuff[4] := DataLen;    // P3    Length of data to be read
    for indx := 0 to DataLen-1 do
      SendBuff[indx + 5] := $00;
    SendLen := DataLen + 5;
    RecvLen := $02;

    retCode := SendAPDUandDisplay(2);
    if retCode <> SCARD_S_SUCCESS then begin
      writeBinary := retCode;
      Exit;
    end;
    tmpStr := '';
    for indx := 0 to 1 do
      tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
    if tmpStr <> '90 00 ' then begin
      displayOut(3, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      writeBinary := retCode;
      Exit;
    end;
  end;

  // 2. Write data to card
  ClearBuffers();
  SendBuff[0] := $80;        // CLA
  SendBuff[1] := $D0;        // INS
  SendBuff[2] := HiByte;     // P1    High Byte Address
  SendBuff[3] := LoByte;     // P2    Low Byte Address
  SendBuff[4] := DataLen;    // P3    Length of data to be read
  for indx := 0 to DataLen-1 do
    SendBuff[indx + 5] := DataIn[indx];
  SendLen := DataLen + 5;
  RecvLen := $02;

  retCode := SendAPDUandDisplay(0);
  if retCode <> SCARD_S_SUCCESS then begin
    writeBinary := retCode;
    Exit;
  end;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> '90 00 ' then begin
    displayOut(3, 0, 'The return string is invalid. Value: ' + tmpStr);
    retCode := INVALID_SW1SW2;
    writeBinary := retCode;
    Exit;
  end;
  writeBinary := retCode;

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

  retCode := SendAPDUandDisplay(0);
  if retCode <> SCARD_S_SUCCESS then begin
    readRecord := retCode;
    Exit;
  end;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx + SendBuff[4]]]) + ' ';
  if tmpStr <> '90 00 ' then begin
    displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
    retCode := INVALID_SW1SW2;
  end;
  readRecord := retCode;

end;

function readBinary(HiByte, LoByte: Byte; DataLen: Byte): integer;
var indx: integer;
    tmpStr: String;
begin

  ClearBuffers();
  SendBuff[0] := $80;        // CLA
  SendBuff[1] := $B0;        // INS
  SendBuff[2] := HiByte;     // P1    High Byte Address
  SendBuff[3] := LoByte;     // P2    Low Byte Address
  SendBuff[4] := DataLen;    // P3    Length of data to be read
  SendLen := $05;
  RecvLen := SendBuff[4] + 2;

  retCode := SendAPDUandDisplay(2);
  if retCode <> SCARD_S_SUCCESS then begin
    readBinary := retCode;
    Exit;
  end;
  readBinary := retCode;

end;

procedure TMainACOSBin.FormActivate(Sender: TObject);
begin

  InitMenu();

end;

procedure TMainACOSBin.bResetClick(Sender: TObject);
begin

  if ConnActive then begin
    retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
    ConnActive := False;
  end;

  retCode := SCardReleaseContext(hCard);
  InitMenu();

end;

procedure TMainACOSBin.bQuitClick(Sender: TObject);
begin

  if ConnActive then begin
    retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
    ConnActive := False;
  end;

  retCode := SCardReleaseContext(hCard);
  Application.Terminate;

end;

procedure TMainACOSBin.bInitClick(Sender: TObject);
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
  end
  else
    DisplayOut(0, 0, 'Select reader, insert mcu card and connect.');

  cbReader.Clear;;
  LoadListToControl(cbReader,@buffer,bufferLen);
  cbReader.ItemIndex := 0;
  EnableButtons();

end;

procedure TMainACOSBin.bConnectClick(Sender: TObject);
var ReaderLen: ^DWORD;
begin

  if ConnActive then begin
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
  if retCode <> SCARD_S_SUCCESS then begin
    DisplayOut(1, retCode, '');
    ConnActive := False;
    Exit;
  end
  else
    DisplayOut(0, 0, 'Successful connection to ' + cbReader.Text);

  connActive := True;
  gbFormat.Enabled := True;
  gbReadWrite.Enabled := True;
  getBinaryData();

end;

procedure TMainACOSBin.bFormatClick(Sender: TObject);
var tmpStr: String;
    indx: integer;
    tmpArray: array[0..31] of Byte;
begin

  // Validate inputs
  if tFileID1.Text = '' then begin
    tFileID1.SetFocus;
    Exit;
  end;
  if tFileID2.Text = '' then begin
    tFileID2.SetFocus;
    Exit;
  end;
  if tFileLen2.Text = '' then begin
    tFileLen2.SetFocus;
    Exit;
  end;

  // 1. Send IC Code
  retCode := SubmitIC();
  if retCode <> SCARD_S_SUCCESS then begin
    displayOut(4, 0, 'Insert ACOS3-24K card on contact card reader.');
    Exit;
  end;

  // 2. Select FF 02
  retCode := selectFile($FF, $02);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> '90 00 ' then begin
    displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
    retCode := INVALID_SW1SW2;
    Exit;
  end;

  // 3. Write to FF 02
  //    This will create 1 binary file, no Option registers and
  //    Security Option registers defined, Personalization bit
  //    is not set
  tmpArray[0] :=  $00;    // 00    Option registers
  tmpArray[1] :=  $00;    // 00    Security option register
  tmpArray[2] :=  $01;    // 01    No of user files
  tmpArray[3] :=  $00;    // 00    Personalization bit
  retCode := writeRecord(0, $00, $04, $04, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  DisplayOut(0, 0, 'File FF 02 is updated.');

  // 4. Perform a reset for changes in the ACOS3 to take effect
  connActive := False;
	retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
  if retCode <> SCARD_S_SUCCESS then begin
    displayOut(0, retCode, '');
    Exit;
  end ;
  retCode := SCardConnectA(hContext,
                           PChar(cbReader.Text),
                           SCARD_SHARE_SHARED,
                           SCARD_PROTOCOL_T0 or SCARD_PROTOCOL_T1,
                           @hCard,
                           @dwActProtocol);
  if retCode <> SCARD_S_SUCCESS then begin
    displayOut(0, retCode, '');
    Exit;
  end ;
  displayOut(3, 0, 'Card reset is successful.');
  connActive := True;

  // 5. Select FF 04
  retCode := selectFile($FF, $04);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> '90 00 ' then begin
    displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
    retCode := INVALID_SW1SW2;
    Exit;
  end;

  // 6. Send IC Code
  retCode := SubmitIC();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 7. Write to FF 04
  // 7.1. Write to first record of FF 04
  if tFileLen1.Text = '' then
    tmpArray[0] := $00                              // File Length: High Byte
  else
    tmpArray[0] := StrToInt('$'+ tFileLen1.Text);   // File Length: High Byte
  tmpArray[1] := StrToInt('$'+ tFileLen2.Text);     // File Length: Low Byte
  tmpArray[2] := $00;                               // 00    Read security attribute
  tmpArray[3] := $00;                               // 00    Write security attribute
  tmpArray[4] := StrToInt('$'+ tFileID1.Text);      // File identifier
  tmpArray[5] := StrToInt('$'+ tFileID2.Text);      // File identifier
  tmpArray[6] := $80;                               // File Access Flag: Binary File Type
  retCode := writeRecord(0, $00, $07, $07, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  tmpStr := '';
  tmpStr := tFileID1.Text + ' ' + tFileID2.Text;
  DisplayOut(0, 0, 'User File ' + tmpStr + ' is defined.');

end;

procedure TMainACOSBin.tFileID1KeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TMainACOSBin.bBinReadClick(Sender: TObject);
var indx, tmpLen: integer;
    tmpStr: string;
    HiByte, LoByte, FileID1, FileID2: byte;
begin

  // Validate input
  if tFID1.Text = '' then begin
    tFID1.SetFocus;
    Exit;
  end;
  if tFID2.Text = '' then begin
    tFID2.SetFocus;
    Exit;
  end;
  if tOffset2.Text = '' then begin
    tOffset2.SetFocus;
    Exit;
  end;
  if tLen.Text = '' then begin
    tLen.SetFocus;
    Exit;
  end;

  ClearBuffers();
  FileID1 := StrToInt('$' + tFID1.Text);
  FileID2 := StrToInt('$' + tFID2.Text);
  if tOffset1.Text = '' then
    HiByte := $00
  else
    HiByte := StrToInt('$' + tOffset1.Text);
  LoByte := StrToInt('$' + tOffset2.Text);
	tmpLen := StrToInt('$' + tLen.Text);

  // 1. Select User File
  retCode := selectFile(FileID1, FileID2);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> '91 00 ' then begin
    displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
    retCode := INVALID_SW1SW2;
    Exit;
  end;

  // 2. Read binary
  retCode := readBinary(HiByte, LoByte, tmpLen);
  if retCode <> SCARD_S_SUCCESS then begin
    displayOut(4, 0, 'Card may not have been formatted yet.');
    Exit;
  end;

  tmpStr := '';
  indx := 0;
  while (RecvBuff[indx] <> $00) do begin
    if indx < tData.MaxLength then
      tmpStr  := tmpStr + chr(RecvBuff[indx]);
    Inc(indx);
  end;
  tData.Text := tmpStr;

end;

procedure TMainACOSBin.bBinWriteClick(Sender: TObject);
var indx, tmpLen: integer;
    tmpStr: string;
    HiByte, LoByte, FileID1, FileID2: byte;
    tmpArray: array[0..255] of Byte;
begin

  // Validate input
  if tFID1.Text = '' then begin
    tFID1.SetFocus;
    Exit;
  end;
  if tFID2.Text = '' then begin
    tFID2.SetFocus;
    Exit;
  end;
  if tOffset2.Text = '' then begin
    tOffset2.SetFocus;
    Exit;
  end;
  if tLen.Text = '' then begin
    tLen.SetFocus;
    Exit;
  end;
  if tData.Text = '' then begin
    tData.SetFocus;
    Exit;
  end;

  ClearBuffers();
  FileID1 := StrToInt('$' + tFID1.Text);
  FileID2 := StrToInt('$' + tFID2.Text);
  if tOffset1.Text = '' then
    HiByte := $00
  else
    HiByte := StrToInt('$' + tOffset1.Text);
  LoByte := StrToInt('$' + tOffset2.Text);
	tmpLen := StrToInt('$' + tLen.Text);

  // 1. Select User File
  retCode := selectFile(FileID1, FileID2);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> '91 00 ' then begin
    displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
    retCode := INVALID_SW1SW2;
    Exit;
  end;

  // 3. Write input data to card
  tmpStr := tData.Text;
  for indx := 0 to Length(tmpStr)-1 do
    tmpArray[indx] := ord(tmpStr[indx+1]);
  retCode := writeBinary(1, HiByte, LoByte, tmpLen, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

procedure TMainACOSBin.bClearClick(Sender: TObject);
begin

  mMsg.Clear;
  
end;

end.
