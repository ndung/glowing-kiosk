//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              ReadWrite.pas
//
//  Description:       This sample program outlines the steps on how to
//                     format the ACOS card and how to  read or write
//                     data into it using the PC/SC platform.
//
//  Author:	           Jose Isagani R. Mission
//
//  Date:	             May 5, 2004
//
//  Revision Trail:   (Date/Author/Description)
//   (June 24, 2008 / M.J.E.C. Castillo / Added File Access Flag bit to FF 04)
//
//======================================================================
unit ReadWrite;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls, ComCtrls, ACSModule;

Const MAX_BUFFER_LEN    = 256;
Const INVALID_SW1SW2    = -450;

type
  TMainReadWrite = class(TForm)
    Label1: TLabel;
    cbReader: TComboBox;
    mMsg: TRichEdit;
    gbFunction: TGroupBox;
    bInit: TButton;
    bConnect: TButton;
    bReset: TButton;
    bQuit: TButton;
    rgUserFile: TRadioGroup;
    bRead: TButton;
    bWrite: TButton;
    tData: TEdit;
    Label2: TLabel;
    bFormat: TButton;
    procedure bQuitClick(Sender: TObject);
    procedure bConnectClick(Sender: TObject);
    procedure bInitClick(Sender: TObject);
    procedure bResetClick(Sender: TObject);
    procedure cbReaderChange(Sender: TObject);
    procedure bFormatClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure bReadClick(Sender: TObject);
    procedure bWriteClick(Sender: TObject);
    procedure rgUserFileClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  MainReadWrite: TMainReadWrite;
  hContext    : SCARDCONTEXT;
  hCard       : SCARDCONTEXT;
  ioRequest   : SCARD_IO_REQUEST;
  retCode     : Integer;
  dwActProtocol, BufferLen  : DWORD;
  SendBuff, RecvBuff        : array [0..262] of Byte;
  SendLen, RecvLen          : DWORD;
  Buffer      : array [0..MAX_BUFFER_LEN] of char;
  ConnActive  : Boolean;

procedure ClearBuffers();
procedure InitMenu();
procedure AddButtons();
procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
function SendAPDUandDisplay(SendType: integer; ApduIn: string): integer;
function SubmitIC(): integer;
function selectFile(HiAddr: Byte; LoAddr: Byte): integer;
function writeRecord(caseType: Integer; RecNo: Byte; maxDataLen: Byte; DataLen: Byte; DataIn:array of Byte): integer;
function readRecord(RecNo: Byte; DataLen: Byte): integer;

implementation

{$R *.dfm}

procedure ClearBuffers();
var indx: integer;
begin

  for indx := 0 to 262 do
    begin
      SendBuff[indx] := $00;
      RecvBuff[indx] := $00;
    end;

end;

procedure InitMenu();
begin

  MainReadWrite.cbReader.Clear;
  MainReadWrite.mMsg.Clear;
  DisplayOut(0, 0, 'Program ready');
  MainReadWrite.cbReader.Enabled := False;
  MainReadWrite.bInit.Enabled := True;
  MainReadWrite.bConnect.Enabled := False;
  MainReadWrite.bFormat.Enabled := False;
  MainReadWrite.gbFunction.Enabled := False;
  MainReadWrite.rgUserFile.ItemIndex := -1;
  MainReadWrite.tData.Text := '';
  MainReadWrite.tData.Enabled := False;
  MainReadWrite.bReset.Enabled := False;

end;

procedure AddButtons();
begin

  MainReadWrite.cbReader.Enabled := True;
  MainReadWrite.bInit.Enabled := False;
  MainReadWrite.bConnect.Enabled := True;
  MainReadWrite.bReset.Enabled := True;

end;

procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
begin

  case errType of
    0: MainReadWrite.mMsg.SelAttributes.Color := clTeal;      // Notifications
    1: begin                                                    // Error Messages
         MainReadWrite.mMsg.SelAttributes.Color := clRed;
         PrintText := GetScardErrMsg(retVal);
       end;
    2: begin
         MainReadWrite.mMsg.SelAttributes.Color := clBlack;
         PrintText := '< ' + PrintText;                      // Input data
       end;
    3: begin
         MainReadWrite.mMsg.SelAttributes.Color := clBlack;
         PrintText := '> ' + PrintText;                      // Output data
       end;
  end;
  MainReadWrite.mMsg.Lines.Add(PrintText);
  MainReadWrite.mMsg.SelAttributes.Color := clBlack;

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
  if tmpStr <> '90 00 ' then
    begin
      displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
    end;
  readRecord := retCode;

end;

procedure TMainReadWrite.bQuitClick(Sender: TObject);
begin

  if ConnActive then
    begin
      retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
      ConnActive := False;
    end;

  retCode := SCardReleaseContext(hCard);
  Application.Terminate;
  
end;

procedure TMainReadWrite.bConnectClick(Sender: TObject);
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
  gbFunction.Enabled := True;
  rgUserFile.ItemIndex := 0;
  tData.Enabled := True;
  tData.Text := '';

end;

procedure TMainReadWrite.bInitClick(Sender: TObject);
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

  MainReadWrite.cbReader.Clear;;
  LoadListToControl(MainReadWrite.cbReader,@buffer,bufferLen);
  MainReadWrite.cbReader.ItemIndex := 0;
  AddButtons();

end;

procedure TMainReadWrite.bResetClick(Sender: TObject);
begin

  if ConnActive then
    begin
      retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
      ConnActive := False;
    end;

  retCode := SCardReleaseContext(hCard);
  InitMenu();

end;

procedure TMainReadWrite.cbReaderChange(Sender: TObject);
begin

  bFormat.Enabled := False;
  gbFunction.Enabled := False;
  tData.Text := '';
  tData.Enabled := False;

  if ConnActive then
  begin
    retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
    ConnActive := False;
  end;

end;

procedure TMainReadWrite.bFormatClick(Sender: TObject);
var tmpStr: String;
    indx: integer;
    tmpArray: array[0..31] of Byte;
begin

  // 1. Send IC Code
  retCode := SubmitIC();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

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

  // 3. Write to FF 02
  //    This will create 3 User files, no Option registers and
  //    Security Option registers defined, Personalization bit
  //    is not set
  tmpArray[0] :=  $00;    // 00    Option registers
  tmpArray[1] :=  $00;    // 00    Security option register
  tmpArray[2] :=  $03;    // 03    No of user files
  tmpArray[3] :=  $00;    // 00    Personalization bit
  retCode := writeRecord(0, $00, $04, $04, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  DisplayOut(0, 0, 'File FF 02 is updated.');

  // 4. Perform a reset for changes in the ACOS to take effect
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
  displayOut(3, 0, 'Card reset is successful.');
  ConnActive := True;

  // 5. Select FF 04
  retCode := selectFile($FF, $04);
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

  // 6. Send IC Code
  retCode := SubmitIC();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // 7. Write to FF 04
  // 7.1. Write to first record of FF 04
  tmpArray[0] := $0A;    // 10    Record length
  tmpArray[1] := $01;    // 1     No of records
  tmpArray[2] := $00;    // 00    Read security attribute
  tmpArray[3] := $00;    // 00    Write security attribute
  tmpArray[4] := $AA;    // AA    File identifier
  tmpArray[5] := $11;    // 11    File identifier
  tmpArray[6] := $00;    // File Access Flag
  retCode := writeRecord(0, $00, $07, $07, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  DisplayOut(0, 0, 'User File AA 11 is defined.');

  // 7.2. Write to second record of FF 04
  tmpArray[0] := $10;    // 16    Record length
  tmpArray[1] := $01;    // 1     No of records
  tmpArray[2] := $00;    // 00    Read security attribute
  tmpArray[3] := $00;    // 00    Write security attribute
  tmpArray[4] := $BB;    // BB    File identifier
  tmpArray[5] := $22;    // 22    File identifier
        tmpArray[6] := $00;    // File Access Flag
  retCode := writeRecord(0, $01, $07, $07, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  DisplayOut(0, 0, 'User File BB 22 is defined.');

  // 7.3. Write to third record of FF 04
  tmpArray[0] := $20;    // 32    Record length
  tmpArray[1] := $01;    // 1     No of records
  tmpArray[2] := $00;    // 00    Read security attribute
  tmpArray[3] := $00;    // 00    Write security attribute
  tmpArray[4] := $CC;    // CC    File identifier
  tmpArray[5] := $33;    // 33    File identifier
  tmpArray[6] := $00;    // File Access Flag
  retCode := writeRecord(0, $02, $07, $07, tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  DisplayOut(0, 0, 'User File CC 33 is defined.');

  rgUserFile.ItemIndex := 0;
  tData.Text := '';

end;

procedure TMainReadWrite.FormActivate(Sender: TObject);
begin

  InitMenu();

end;

procedure TMainReadWrite.bReadClick(Sender: TObject);
var HiByte, LoByte, LenByte: Byte;
    tmpStr, chkStr: String;
    indx: integer;
begin

  // 1. Select User File
  case rgUserFile.ItemIndex of
    0: begin       // User File AA 11
         HiByte := $AA;
         LoByte := $11;
         chkStr := '91 00 ';
         LenByte := $0A;
       end;
    1: begin       // User File BB 22
         HiByte := $BB;
         LoByte := $22;
         chkStr := '91 01 ';
         LenByte := $10;
       end;
    2: begin       // User File CC 33
         HiByte := $CC;
         LoByte := $33;
         chkStr := '91 02 ';
         LenByte := $20;
       end;
  end;
  retCode := selectFile(HiByte, LoByte);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> chkStr then
    begin
      displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      Exit;
    end;

  // 2. Read first record of the user file selected
  retCode := readRecord($00, LenByte);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';
  indx := 0;
  while (RecvBuff[indx] <> $00) do
    begin
      if indx < tData.MaxLength then
        tmpStr  := tmpStr + chr(RecvBuff[indx]);
      Inc(indx);
    end;
  tData.Text := tmpStr;
  DisplayOut(0, 0, 'Data read is displayed in Message Box.');

end;

procedure TMainReadWrite.bWriteClick(Sender: TObject);
var HiByte, LoByte: Byte;
    tmpStr, chkStr: String;
    indx: integer;
    tmpArray: array[0..56] of Byte;
begin

  // 1. Validate input
  if tData.Text = '' then
    begin
      tData.SetFocus;
      Exit;
    end;

  // 2. Select User File
  case rgUserFile.ItemIndex of
    0: begin       // User File AA 11
         HiByte := $AA;
         LoByte := $11;
         chkStr := '91 00 ';
         tData.MaxLength  := 10;
       end;
    1: begin       // User File BB 22
         HiByte := $BB;
         LoByte := $22;
         chkStr := '91 01 ';
         tData.MaxLength := 16;
       end;
    2: begin       // User File CC 33
         HiByte := $CC;
         LoByte := $33;
         chkStr := '91 02 ';
         tData.MaxLength := 32;
       end;
  end;
  retCode := selectFile(HiByte, LoByte);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> chkStr then
    begin
      displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      Exit;
    end;

  // 3. Write input data to card

  tmpStr := tData.Text;
  for indx := 0 to Length(tmpStr)-1 do
    begin
      tmpArray[indx] := ord(tmpStr[indx+1]);
    end;
  retCode := writeRecord(1, $00, tData.MaxLength, Length(tmpStr), tmpArray);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  DisplayOut(0, 0, 'Data read from Message Box is written to card.');

end;

procedure TMainReadWrite.rgUserFileClick(Sender: TObject);
begin

  tData.Text := '';
  case rgUserFile.ItemIndex of
    0: begin       // User File AA 11
         tData.MaxLength  := 10;
       end;
    1: begin       // User File BB 22
         tData.MaxLength := 16;
       end;
    2: begin       // User File CC 33
        tData.MaxLength := 32;
       end;
  end;

end;

end.
