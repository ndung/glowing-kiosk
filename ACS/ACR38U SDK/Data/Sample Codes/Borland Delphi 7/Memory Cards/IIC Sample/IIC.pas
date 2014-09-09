//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              IIC.pas
//
//  Description:       This sample program outlines the steps on how to
//                     program IIC memory cards using ACS readers
//                     in PC/SC platform.
//
//  Author:	           Jose Isagani R. Mission
//
//  Date:	             June 15, 2004
//
//  Revision Trail:   (Date/Author/Description)
//                09/02/2008 M.J.E.C.Castillo modified the connect button code
//
//======================================================================
unit IIC;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, ACSModule;

Const MAX_BUFFER_LEN    = 256;
Const INVALID_SW1SW2    = -450;

type
  TMainIIC = class(TForm)
    Label1: TLabel;
    cbReader: TComboBox;
    bInit: TButton;
    Label2: TLabel;
    cbCardType: TComboBox;
    bConnect: TButton;
    gbFunction: TGroupBox;
    Label3: TLabel;
    cbPageSize: TComboBox;
    Label4: TLabel;
    tHiAdd: TEdit;
    tLoAdd: TEdit;
    bSet: TButton;
    Label5: TLabel;
    tLen: TEdit;
    Label6: TLabel;
    bRead: TButton;
    bWrite: TButton;
    mMsg: TRichEdit;
    bReset: TButton;
    bQuit: TButton;
    tBitAdd: TEdit;
    mData: TEdit;
    procedure bQuitClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure bInitClick(Sender: TObject);
    procedure bResetClick(Sender: TObject);
    procedure cbReaderClick(Sender: TObject);
    procedure bConnectClick(Sender: TObject);
    procedure tHiAddKeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure tLoAddChange(Sender: TObject);
    procedure tLenChange(Sender: TObject);
    procedure tHiAddKeyPress(Sender: TObject; var Key: Char);
    procedure tLoAddKeyPress(Sender: TObject; var Key: Char);
    procedure tLenKeyPress(Sender: TObject; var Key: Char);
    procedure bSetClick(Sender: TObject);
    procedure tBitAddKeyPress(Sender: TObject; var Key: Char);
    procedure tBitAddKeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure tLenExit(Sender: TObject);
    procedure cbCardTypeClick(Sender: TObject);
    procedure bReadClick(Sender: TObject);
    procedure bWriteClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  MainIIC: TMainIIC;
  hContext    : SCARDCONTEXT;
  hCard       : SCARDCONTEXT;
  ioRequest   : SCARD_IO_REQUEST;
  retCode     : Integer;
  dwActProtocol, BufferLen    : DWORD;
  SendBuff, RecvBuff          : array [0..262] of Byte;
  SendLen, RecvLen, nBytesRet : DWORD;
  Buffer      : array [0..MAX_BUFFER_LEN] of char;
  ConnActive  : Boolean;

procedure ClearBuffers();
procedure InitMenu();
procedure AddButtons();
procedure ClearFields();
procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
function SendAPDUandDisplay(SendType: integer; ApduIn: string): integer;
function CallCardControl(): integer;
function InputOK(checkType: integer; opType: integer): Boolean;

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

  MainIIC.cbReader.Clear;
  MainIIC.mMsg.Clear;
  MainIIC.cbReader.Enabled := False;
  MainIIC.cbCardType.Enabled := False;
  MainIIC.bInit.Enabled := True;
  MainIIC.gbFunction.Enabled := False;
  MainIIC.bConnect.Enabled := False;

end;

procedure AddButtons();
begin

  MainIIC.cbReader.Enabled := True;
  MainIIC.bInit.Enabled := False;
  MainIIC.cbCardType.Enabled := True;
  MainIIC.cbCardType.ItemIndex := 0;
  MainIIC.bConnect.Enabled := True;
  MainIIC.bReset.Enabled := True;

end;

procedure ClearFields();
begin

  MainIIC.cbPageSize.ItemIndex := -1;
  MainIIC.tBitAdd.Text := '';
  MainIIC.tHiAdd.Text := '';
  MainIIC.tLoAdd.Text := '';
  MainIIC.tLen.Text := '';
  MainIIC.mData.Clear;

end;

procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
begin

  case errType of
    0: MainIIC.mMsg.SelAttributes.Color := clTeal;      // Notifications
    1: begin                                                  // Error Messages
         MainIIC.mMsg.SelAttributes.Color := clRed;
         PrintText := GetScardErrMsg(retVal);
       end;
    2: begin
         MainIIC.mMsg.SelAttributes.Color := clBlack;
         PrintText := '< ' + PrintText;                      // Input data
       end;
    3: begin
         MainIIC.mMsg.SelAttributes.Color := clBlack;
         PrintText := '> ' + PrintText;                      // Output data
       end;
  end;
  MainIIC.mMsg.Lines.Add(PrintText);
  MainIIC.mMsg.SelAttributes.Color := clBlack;
  MainIIC.mMsg.SetFocus;

end;

function SendAPDUandDisplay(SendType: integer; ApduIn: string): integer;
var tmpStr: string;
    indx: integer;
begin

  ioRequest.dwProtocol := dwActProtocol;
  ioRequest.cbPciLength := sizeof(SCARD_IO_REQUEST);
  DisplayOut(2, 0, ApduIn);
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
         else
           begin
             tmpStr := 'ATR :';
             for indx := 0 to (RecvLen-3) do
               tmpStr := tmpStr + Format('%.2X ', [(RecvBuff[Indx])]);
           end;
         end;
      2: begin      // Display all data after checking SW1/SW2
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

function CallCardControl(): integer;
begin

  RecvLen := 262;
  retCode := SCardControl(hCard,
                  IOCTL_SMARTCARD_SET_CARD_TYPE,
                  @SendBuff,
                  SendLen,
                  @RecvBuff,
                  RecvLen,
                  @nBytesRet);
  if retCode <> SCARD_S_SUCCESS then
      DisplayOut(1, retCode, '');
  CallCardControl := retCode;

end;

function InputOK(checkType: integer; opType: integer): Boolean;
begin

  InputOK := False;
  if checkType = 1 then    // For 17-bit address input
    if  MainIIC.tBitAdd.Text = '' then begin
      MainIIC.tBitAdd.SetFocus;
      Exit;
    end;
  if  MainIIC.tHiAdd.Text = '' then begin
    MainIIC.tHiAdd.SetFocus;
    Exit;
  end;
  if  MainIIC.tLoAdd.Text = '' then begin
    MainIIC.tLoAdd.SetFocus;
    Exit;
  end;
  if opType = 1 then       // For Write Operation
    if  MainIIC.mData.Text = '' then begin
      MainIIC.mData.SetFocus;
      Exit;
    end;

  InputOK := True;

end;

procedure TMainIIC.bQuitClick(Sender: TObject);
begin

  if ConnActive then
    begin
      retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
      ConnActive := False;
    end;
  retCode := SCardReleaseContext(hCard);
  Application.Terminate;

end;

procedure TMainIIC.FormCreate(Sender: TObject);
begin

  InitMenu();
  
end;

procedure TMainIIC.bInitClick(Sender: TObject);
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
    DisplayOut(0, 0, 'Select reader and card type, and connect.');

  MainIIC.cbReader.Clear;
  LoadListToControl(MainIIC.cbReader,@buffer,bufferLen);
  MainIIC.cbReader.ItemIndex := 0;
  AddButtons();

end;

procedure TMainIIC.bResetClick(Sender: TObject);
begin

  cbCardType.ItemIndex := -1;
  ClearFields();
  if ConnActive then
    begin
      retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
      ConnActive := False;
    end;

  retCode := SCardReleaseContext(hCard);
  InitMenu();

end;

procedure TMainIIC.cbReaderClick(Sender: TObject);
begin

  cbCardType.ItemIndex := 0;
  ClearFields();
  gbFunction.Enabled := False;
  if ConnActive then
    begin
      retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
      ConnActive := False;
    end;

end;

procedure TMainIIC.bConnectClick(Sender: TObject);
var cardType: Integer;
begin

  if ConnActive then
  begin
    DisplayOut(0, 0, 'Connection is already active.');
    Exit;
  end;
 
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
  ConnActive := True;
  gbFunction.Enabled := True;
  cbPageSize.ItemIndex := 0;
  if cbCardType.ItemIndex = 11 then
    tBitAdd.Enabled := True
  else
    tBitAdd.Enabled := False;

end;

procedure TMainIIC.tHiAddKeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin

  if Length(tHiAdd.Text) > 1 then
         tLoAdd.SetFocus;

end;

procedure TMainIIC.tLoAddChange(Sender: TObject);
begin

  if Length(tLoAdd.Text) > 1 then
         tLen.SetFocus;

end;

procedure TMainIIC.tLenChange(Sender: TObject);
begin

  if Length(tLen.Text) > 1 then
         mData.SetFocus;

end;

procedure TMainIIC.tHiAddKeyPress(Sender: TObject; var Key: Char);
begin

  if Key in ['a'..'z'] then Dec(Key,32);
  if Not(Key in ['0'..'9']) then
    if not(Key in ['A'..'F'])  then
      if Key <> chr(8) then
        Key := #0;

end;

procedure TMainIIC.tLoAddKeyPress(Sender: TObject; var Key: Char);
begin

  if Key in ['a'..'z'] then Dec(Key,32);
  if Not(Key in ['0'..'9']) then
    if not(Key in ['A'..'F'])  then
      if Key <> chr(8) then
           Key := #0;

end;

procedure TMainIIC.tLenKeyPress(Sender: TObject; var Key: Char);
begin

  if Key in ['a'..'z'] then Dec(Key,32);
  if Not(Key in ['0'..'9']) then
    if not(Key in ['A'..'F'])  then
      if Key <> chr(8) then
           Key := #0;

end;

procedure TMainIIC.bSetClick(Sender: TObject);
var indx: integer;
    tmpStr: String;
begin

  ClearBuffers();
  SendBuff[0] := $FF;
  SendBuff[1] := $01;
  SendBuff[2] := $00;
  SendBuff[3] := $00;
  SendBuff[4] := $01;
  case cbPageSize.ItemIndex of
    0: SendBuff[5] := $03;
    1: SendBuff[5] := $04;
    2: SendBuff[5] := $05;
    3: SendBuff[5] := $06;
    4: SendBuff[5] := $07;
  end;
  SendLen := 6;
  RecvLen := 2;
  for indx := 0 to 5 do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  retCode := SendAPDUandDisplay(0, tmpStr);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

procedure TMainIIC.tBitAddKeyPress(Sender: TObject; var Key: Char);
begin

  if Not(Key in ['0'..'1']) then
    if Key <> chr(8) then
      Key := #0;

end;

procedure TMainIIC.tBitAddKeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin

  if Length(tBitAdd.Text) > 0 then
         tHiAdd.SetFocus;

end;

procedure TMainIIC.tLenExit(Sender: TObject);
begin

  if tLen.Text <> '' then begin
    mData.Clear;
    mData.MaxLength := StrToInt('$' + copy(tLen.Text,1,2));
  end;

end;

procedure TMainIIC.cbCardTypeClick(Sender: TObject);
begin

  ClearFields();
  gbFunction.Enabled := False;
  if ConnActive then
    begin
      retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
      ConnActive := False;
    end;

end;

procedure TMainIIC.bReadClick(Sender: TObject);
var indx: integer;
    tmpStr: String;
begin

  // 1. Check for all input fields
  if cbCardType.ItemIndex = 11 then
    indx := 1
  else
    indx := 0;
  if not InputOK(indx, 0) then
    Exit;

  // 2. Read input fields and pass data to card
  mData.Clear;
  ClearBuffers();
  SendBuff[0] := $FF;
  if ((cbCardType.ItemIndex = 11) and (StrToInt(tBitAdd.Text) = 1))then
    SendBuff[1] := $B1
  else
    SendBuff[1] := $B0;
  SendBuff[2] := StrToInt('$' + copy(tHiAdd.Text,1,2));
  SendBuff[3] := StrToInt('$' + copy(tLoAdd.Text,1,2));
  SendBuff[4] := StrToInt('$' + copy(tLen.Text,1,2));
  SendLen := 5;
  RecvLen := SendBuff[4]+2;
  for indx := 0 to 5 do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  retCode := SendAPDUandDisplay(2, tmpStr);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';

  // 3. Display data read from card into Data textbox
  for indx := 0 to SendBuff[4]-1 do
    tmpStr := tmpStr + chr(RecvBuff[indx]);
  mData.Text := tmpStr;

end;

procedure TMainIIC.bWriteClick(Sender: TObject);
var indx: integer;
    tmpStr: String;
begin

  // 1. Check for all input fields
  if cbCardType.ItemIndex = 11 then
    indx := 1
  else
    indx := 0;
  if not InputOK(indx, 1) then
    Exit;

  // 2. Read input fields and pass data to card
  tmpStr := mData.Text;
  ClearBuffers();
  SendBuff[0] := $FF;
  if ((cbCardType.ItemIndex = 11) and (StrToInt(tBitAdd.Text) = 1))then
    SendBuff[1] := $D1
  else
    SendBuff[1] := $D0;
  SendBuff[2] := StrToInt('$' + copy(tHiAdd.Text,1,2));
  SendBuff[3] := StrToInt('$' + copy(tLoAdd.Text,1,2));
  SendBuff[4] := StrToInt('$' + copy(tLen.Text,1,2));
  for indx := 0 to SendBuff[4]-1 do
    if (ord(tmpStr[indx+1]) <> $00) then
      SendBuff[indx+5] := ord(tmpStr[indx+1])
    else
      SendBuff[indx+5] := $00;
  SendLen := 5 + SendBuff[4];
  RecvLen := 2;
  tmpStr := '';
  for indx := 0 to SendLen do
    tmpStr := tmpStr + Format('%.02X ',[SendBuff[indx]]);
  retCode := SendAPDUandDisplay(0, tmpStr);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  mData.Clear;

end;

end.