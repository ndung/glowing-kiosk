//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              MifareProg.pas
//
//  Description:       This sample program outlines the steps on how to
//                     transact with Mifare 1K/4K cards using ACR128
//
//  Author:	           Jose Isagani R. Mission
//
//  Date:	             June 13, 2008
//
//  Revision Trail:   (Date/Author/Description)
//
//======================================================================
unit MifareProg;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ACSModule, ComCtrls, ExtCtrls;

const MAX_BUFFER_LEN    = 256;

type
  TMainMifareProg = class(TForm)
    Label1: TLabel;
    cbReader: TComboBox;
    bInit: TButton;
    bConnect: TButton;
    mMsg: TRichEdit;
    bClear: TButton;
    bReset: TButton;
    bQuit: TButton;
    gbLoadKeys: TGroupBox;
    rbNonVolMem: TRadioButton;
    rbVolMem: TRadioButton;
    Label2: TLabel;
    tMemAdd: TEdit;
    Label3: TLabel;
    tKey1: TEdit;
    tKey2: TEdit;
    tKey3: TEdit;
    tKey4: TEdit;
    tKey5: TEdit;
    tKey6: TEdit;
    bLoadKey: TButton;
    gbAuth: TGroupBox;
    rgSource: TRadioGroup;
    rgKType: TRadioGroup;
    Label4: TLabel;
    tBlkNo: TEdit;
    Label5: TLabel;
    tKeyIn1: TEdit;
    tKeyIn2: TEdit;
    tKeyIn3: TEdit;
    tKeyIn4: TEdit;
    tKeyIn5: TEdit;
    tKeyIn6: TEdit;
    bAuth: TButton;
    Label6: TLabel;
    tKeyAdd: TEdit;
    gbBinOps: TGroupBox;
    bBinRead: TButton;
    bBinUpd: TButton;
    tBinBlk: TEdit;
    Label7: TLabel;
    Label8: TLabel;
    tBinLen: TEdit;
    Label9: TLabel;
    tBinData: TEdit;
    gbValBlk: TGroupBox;
    Label10: TLabel;
    tValAmt: TEdit;
    bValStor: TButton;
    bValInc: TButton;
    bValDec: TButton;
    bValRead: TButton;
    BValRes: TButton;
    Label11: TLabel;
    tValBlk: TEdit;
    Label12: TLabel;
    Label13: TLabel;
    tValSrc: TEdit;
    tValTar: TEdit;
    procedure bClearClick(Sender: TObject);
    procedure bResetClick(Sender: TObject);
    procedure bQuitClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure bInitClick(Sender: TObject);
    procedure bConnectClick(Sender: TObject);
    procedure tMemAddKeyPress(Sender: TObject; var Key: Char);
    procedure tKey1KeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure tKey2KeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure tKey3KeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure tKey4KeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure tKey5KeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure rbNonVolMemClick(Sender: TObject);
    procedure rbVolMemClick(Sender: TObject);
    procedure bLoadKeyClick(Sender: TObject);
    procedure tBlkNoKeyPress(Sender: TObject; var Key: Char);
    procedure tKeyIn1KeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure tKeyIn2KeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure tKeyIn3KeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure tKeyIn4KeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure tKeyIn5KeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure rgSourceClick(Sender: TObject);
    procedure bAuthClick(Sender: TObject);
    procedure bBinReadClick(Sender: TObject);
    procedure bBinUpdClick(Sender: TObject);
    procedure bValStorClick(Sender: TObject);
    procedure bValReadClick(Sender: TObject);
    procedure bValIncClick(Sender: TObject);
    procedure bValDecClick(Sender: TObject);
    procedure BValResClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  MainMifareProg: TMainMifareProg;
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


implementation

{$R *.dfm}

procedure InitMenu();
begin

  connActive := False;
  MainMifareProg.cbReader.Clear;
  MainMifareProg.mMsg.Clear;
  DisplayOut(0, 0, 'Program ready');
  MainMifareProg.bConnect.Enabled := False;
  MainMifareProg.bInit.Enabled := True;
  MainMifareProg.bReset.Enabled := False;
  MainMifareProg.rbNonVolMem.Checked := False;
  MainMifareProg.rbVolMem.Checked := False;
  MainMifareProg.tMemAdd.Text := '';
  MainMifareProg.tKey1.Text := '';
  MainMifareProg.tKey2.Text := '';
  MainMifareProg.tKey3.Text := '';
  MainMifareProg.tKey4.Text := '';
  MainMifareProg.tKey5.Text := '';
  MainMifareProg.tKey6.Text := '';
  MainMifareProg.gbLoadKeys.Enabled := False;
  MainMifareProg.tBlkNo.Text := '';
  MainMifareProg.tKeyAdd.Text := '';
  MainMifareProg.tKeyIn1.Text := '';
  MainMifareProg.tKeyIn2.Text := '';
  MainMifareProg.tKeyIn3.Text := '';
  MainMifareProg.tKeyIn4.Text := '';
  MainMifareProg.tKeyIn5.Text := '';
  MainMifareProg.tKeyIn6.Text := '';
  MainMifareProg.rgSource.ItemIndex := 0;
  MainMifareProg.rgKType.ItemIndex := 0;
  MainMifareProg.gbAuth.Enabled := False;
  MainMifareProg.tBinBlk.Text := '';
  MainMifareProg.tBinLen.Text := '';
  MainMifareProg.tBinData.Text := '';
  MainMifareProg.gbBinOps.Enabled := False;
  MainMifareProg.tValAmt.Text := '';
  MainMifareProg.tValBlk.Text := '';
  MainMifareProg.tValSrc.Text := '';
  MainMifareProg.tValTar.Text := '';
  MainMifareProg.gbValBlk.Enabled := False;

end;

procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
begin

  case errType of
    0: MainMifareProg.mMsg.SelAttributes.Color := clTeal;      // Notifications
    1: begin                                                // Error Messages
         MainMifareProg.mMsg.SelAttributes.Color := clRed;
         PrintText := GetScardErrMsg(retVal);
       end;
    2: begin
         MainMifareProg.mMsg.SelAttributes.Color := clBlack;
         PrintText := '< ' + PrintText;                      // Input data
       end;
    3: begin
         MainMifareProg.mMsg.SelAttributes.Color := clBlack;
         PrintText := '> ' + PrintText;                      // Output data
       end;
    4: MainMifareProg.mMsg.SelAttributes.Color := clRed;        // For Card error
  end;
  MainMifareProg.mMsg.Lines.Add(PrintText);
  MainMifareProg.mMsg.SelAttributes.Color := clBlack;
  MainMifareProg.mMsg.SetFocus;

end;

procedure EnableButtons();
begin

  MainMifareProg.bInit.Enabled := False;
  MainMifareProg.bConnect.Enabled := True;
  MainMifareProg.bReset.Enabled := True;

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
     end;
    DisplayOut(3, 0, Trim(tmpStr));
  end;
  SendAPDUandDisplay := retCode;

end;


procedure TMainMifareProg.bClearClick(Sender: TObject);
begin

  mMsg.Clear;

end;

procedure TMainMifareProg.bResetClick(Sender: TObject);
begin

  if connActive then
    retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
  retCode := SCardReleaseContext(hCard);
  InitMenu();

end;

procedure TMainMifareProg.bQuitClick(Sender: TObject);
begin
  Application.Terminate;
end;

procedure TMainMifareProg.FormActivate(Sender: TObject);
begin
  InitMenu();
end;

procedure TMainMifareProg.bInitClick(Sender: TObject);
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

procedure TMainMifareProg.bConnectClick(Sender: TObject);
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
  gbLoadKeys.Enabled := True;
  gbAuth.Enabled := True;
  gbBinOps.Enabled := True;
  gbValBlk.Enabled :=True;

end;

procedure TMainMifareProg.tMemAddKeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TMainMifareProg.tKey1KeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
  if Length(tKey1.Text) > 1 then
         tKey2.SetFocus;
end;

procedure TMainMifareProg.tKey2KeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
  if Length(tKey2.Text) > 1 then
         tKey3.SetFocus;
end;

procedure TMainMifareProg.tKey3KeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
  if Length(tKey3.Text) > 1 then
         tKey4.SetFocus;
end;

procedure TMainMifareProg.tKey4KeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
  if Length(tKey4.Text) > 1 then
         tKey5.SetFocus;
end;

procedure TMainMifareProg.tKey5KeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
  if Length(tKey5.Text) > 1 then
         tKey6.SetFocus;
end;

procedure TMainMifareProg.rbNonVolMemClick(Sender: TObject);
begin
  tMemAdd.Enabled := True;
end;

procedure TMainMifareProg.rbVolMemClick(Sender: TObject);
begin
  tMemAdd.Text := '';
  tMemAdd.Enabled := False;
end;

procedure TMainMifareProg.bLoadKeyClick(Sender: TObject);
begin

  // Check for valid inputs
  if (not(rbNonVolMem.Checked) and not(rbVolMem.Checked)) then begin
    rbNonVolMem.SetFocus;
    Exit;
  end;
  if rbNonVolMem.Checked then begin
    if tMemAdd.Text = '' then begin
      tMemAdd.SetFocus;
      Exit;
    end;
    if StrToInt('$' + tMemAdd.Text) > $1F then begin
      tMemAdd.Text := '1F';
      Exit
    end;
  end;
  if tKey1.Text = '' then begin
    tKey1.SetFocus;
    Exit;
  end;
  if tKey2.Text = '' then begin
    tKey2.SetFocus;
    Exit;
  end;
  if tKey3.Text = '' then begin
    tKey3.SetFocus;
    Exit;
  end;
  if tKey4.Text = '' then begin
    tKey4.SetFocus;
    Exit;
  end;
  if tKey5.Text = '' then begin
    tKey5.SetFocus;
    Exit;
  end;
  if tKey6.Text = '' then begin
    tKey6.SetFocus;
    Exit;
  end;

  ClearBuffers();
  SendBuff[0] := $FF;                             // CLA
  SendBuff[1] := $82;                             // INS
  if rbNonVolMem.Checked then begin
    SendBuff[2] := $20;                           // P1 : Non volatile memory
    SendBuff[3] := StrToInt('$' + tMemAdd.Text);  // P2 : Memory location
  end
  else begin
    SendBuff[2] := $00;                           // P1 : Volatile memory
    SendBuff[3] := $20;                           // P2 : Session Key
  end;
  SendBuff[4] := $06;                             // P3
  SendBuff[5] := StrToInt('$' + tKey1.Text);      // Key 1 value
  SendBuff[6] := StrToInt('$' + tKey2.Text);      // Key 2 value
  SendBuff[7] := StrToInt('$' + tKey3.Text);      // Key 3 value
  SendBuff[8] := StrToInt('$' + tKey4.Text);      // Key 4 value
  SendBuff[9] := StrToInt('$' + tKey5.Text);      // Key 5 value
  SendBuff[10] := StrToInt('$' + tKey6.Text);     // Key 6 value
  SendLen := $0B;
  RecvLen := $02;

  retCode := SendAPDUandDisplay(0);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

procedure TMainMifareProg.tBlkNoKeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Not (Key in ['0'..'9'])then
      Key := Chr($00);
  end;
end;

procedure TMainMifareProg.tKeyIn1KeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
  if Length(tKeyIn1.Text) > 1 then
         tKeyIn2.SetFocus;
end;

procedure TMainMifareProg.tKeyIn2KeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
  if Length(tKeyIn2.Text) > 1 then
         tKeyIn3.SetFocus;
end;

procedure TMainMifareProg.tKeyIn3KeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
  if Length(tKeyIn3.Text) > 1 then
         tKeyIn4.SetFocus;
end;

procedure TMainMifareProg.tKeyIn4KeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
  if Length(tKeyIn4.Text) > 1 then
         tKeyIn5.SetFocus;
end;

procedure TMainMifareProg.tKeyIn5KeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
  if Length(tKeyIn5.Text) > 1 then
         tKeyIn6.SetFocus;
end;

procedure TMainMifareProg.rgSourceClick(Sender: TObject);
begin
  if rgSource.ItemIndex =0 then begin
    tKeyAdd.Text := '';
    tKeyAdd.Enabled := False;
    tKeyIn1.Enabled := True;
    tKeyIn2.Enabled := True;
    tKeyIn3.Enabled := True;
    tKeyIn4.Enabled := True;
    tKeyIn5.Enabled := True;
    tKeyIn6.Enabled := True;
  end
  else begin
    tKeyIn1.Text := '';
    tKeyIn2.Text := '';
    tKeyIn3.Text := '';
    tKeyIn4.Text := '';
    tKeyIn5.Text := '';
    tKeyIn6.Text := '';
    tKeyAdd.Enabled := True;
    tKeyIn1.Enabled := False;
    tKeyIn2.Enabled := False;
    tKeyIn3.Enabled := False;
    tKeyIn4.Enabled := False;
    tKeyIn5.Enabled := False;
    tKeyIn6.Enabled := False;
    tKeyAdd.Enabled := True;
    if rgSource.ItemIndex = 1 then
      tKeyAdd.Enabled := False;
  end;

end;

procedure TMainMifareProg.bAuthClick(Sender: TObject);
begin

  // Validate input
  if tBlkNo.Text = '' then begin
    tBlkNo.SetFocus;
    Exit;
  end;
  if StrToInt(tBlkNo.Text) > 255 then begin
    tBlkNo.Text := '255';
    Exit;
  end;
  if rgSource.ItemIndex = 0 then begin
    if tKeyIn1.Text = '' then begin
      tKeyIn1.SetFocus;
      Exit;
    end;
    if tKeyIn2.Text = '' then begin
      tKeyIn2.SetFocus;
      Exit;
    end;
    if tKeyIn1.Text = '' then begin
      tKeyIn1.SetFocus;
      Exit;
    end;
    if tKeyIn3.Text = '' then begin
      tKeyIn3.SetFocus;
      Exit;
    end;
    if tKeyIn4.Text = '' then begin
      tKeyIn4.SetFocus;
      Exit;
    end;
    if tKeyIn5.Text = '' then begin
      tKeyIn5.SetFocus;
      Exit;
    end;
  end
  else begin
    if rgSource.ItemIndex = 2 then begin
      if tKeyAdd.Text = '' then begin
        tKeyAdd.SetFocus;
        Exit;
      end;
      if StrToInt('$' + tKeyAdd.Text) > $1F then begin
        tKeyAdd.Text := '1F';
        Exit
      end;
    end;
  end;

 if rgSource.ItemIndex = 0 then begin
      //store values in volatile memory
      ClearBuffers();
      SendBuff[0] := $FF;                             // CLA
      SendBuff[1] := $82;                             // INS
      SendBuff[2] := $00;                           // P1 : Volatile memory
      SendBuff[3] := $20;                           // P2 : Session Key
      SendBuff[4] := $06;                             // P3
      SendBuff[5] := StrToInt('$' + tKeyIn1.Text);      // Key 1 value
      SendBuff[6] := StrToInt('$' + tKeyIn2.Text);      // Key 2 value
      SendBuff[7] := StrToInt('$' + tKeyIn3.Text);      // Key 3 value
      SendBuff[8] := StrToInt('$' + tKeyIn4.Text);      // Key 4 value
      SendBuff[9] := StrToInt('$' + tKeyIn5.Text);      // Key 5 value
      SendBuff[10] := StrToInt('$' + tKeyIn6.Text);     // Key 6 value
      SendLen := $0B;
      RecvLen := $02;

      retCode := SendAPDUandDisplay(0);
      
      if retCode <> SCARD_S_SUCCESS then
          Exit;

      //use volatile memory to authenticate
      ClearBuffers();
      SendBuff[0] := $FF;                             // CLA
      SendBuff[2] := $00;                             // P1 : Same for all source type
      SendBuff[1] := $86;                           // INS : for stored key input
      SendBuff[3] := $00;                           // P2  : for stored key input
      SendBuff[4] := $05;                           // P3  : for stored key input
      SendBuff[5] := $01;                           // Byte 1 : Version no.
      SendBuff[6] := $00;                           // Byte 2
      SendBuff[7] := StrToInt(tBlkNo.Text);         // Byte 3 : Sector No. for stored key input
      if rgKType.ItemIndex = 0 then
        SendBuff[8] := $60                          // Byte 4 : Key A for stored key input
      else
        SendBuff[8] := $61;                         // Byte 4 : Key B for stored key input

      SendBuff[9] := $20                          // Byte 5 : Session key for volatile memory

  end
  else begin
    ClearBuffers();
    SendBuff[0] := $FF;                             // CLA
    SendBuff[2] := $00;                             // P1 : Same for all source type
    SendBuff[1] := $86;                           // INS : for stored key input
    SendBuff[3] := $00;                           // P2  : for stored key input
    SendBuff[4] := $05;                           // P3  : for stored key input
    SendBuff[5] := $01;                           // Byte 1 : Version no.
    SendBuff[6] := $00;                           // Byte 2
    SendBuff[7] := StrToInt(tBlkNo.Text);         // Byte 3 : Sector No. for stored key input
    if rgKType.ItemIndex = 0 then
      SendBuff[8] := $60                          // Byte 4 : Key A for stored key input
    else
      SendBuff[8] := $61;                         // Byte 4 : Key B for stored key input
    if rgSource.ItemIndex = 1 then
      SendBuff[9] := $20                          // Byte 5 : Session key for volatile memory
    else
      SendBuff[9] := StrToInt('$' + tKeyAdd.Text);// Byte 5 : Key No. for non-volatile memory
  end;

  if rgSource.ItemIndex = 0 then
    SendLen := $0B
  else
    SendLen := $0A;
  RecvLen := $02;

  retCode := SendAPDUandDisplay(0);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

procedure TMainMifareProg.bBinReadClick(Sender: TObject);
var indx: integer;
    tmpStr: string;
begin

  // Validate inputs
  tBinData.Text := '';
  if tBinBlk.Text = '' then begin
    tBinBlk.SetFocus;
    Exit;
  end;
  if tBinBlk.Text = '' then begin
    tBinBlk.SetFocus;
    Exit;
  end;
  if StrToInt(tBinBlk.Text) > 255 then begin
    tBinBlk.Text := '255';
    Exit;
  end;
  if tBinLen.Text = '' then begin
    tBinLen.SetFocus;
    Exit;
  end;

  ClearBuffers();
  SendBuff[0] := $FF;                             // CLA
  SendBuff[1] := $B0;                             // INS
  SendBuff[2] := $00;                             // P1
  SendBuff[3] := StrToInt(tBinBlk.Text);          // P2 : Starting Block No.
  SendBuff[4] := StrToInt(tBinLen.Text);          // P3 : Data length

  SendLen := $05;
  RecvLen := SendBuff[4] + 2;

  retCode := SendAPDUandDisplay(2);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  // Display data in text format
  tmpStr := '';
  for indx := 0 to RecvLen-1 do
    tmpStr := tmpStr + chr(RecvBuff[indx]);
  tBinData.Text := tmpStr;

end;

procedure TMainMifareProg.bBinUpdClick(Sender: TObject);
var indx: integer;
    tmpStr: string;
begin

  // Validate inputs
  if tBinData.Text = '' then begin
    tBinData.SetFocus;
    Exit;
  end;
  if tBinBlk.Text = '' then begin
    tBinBlk.SetFocus;
    Exit;
  end;
  if StrToInt(tBinBlk.Text) > 255 then begin
    tBinBlk.Text := '255';
    Exit;
  end;
  if tBinLen.Text = '' then begin
    tBinLen.SetFocus;
    Exit;
  end;

  tmpStr := tBinData.Text;
  ClearBuffers();
  SendBuff[0] := $FF;                             // CLA
  SendBuff[1] := $D6;                             // INS
  SendBuff[2] := $00;                             // P1
  SendBuff[3] := StrToInt(tBinBlk.Text);          // P2 : Starting Block No.
  SendBuff[4] := StrToInt(tBinLen.Text);          // P3 : Data length
  for indx := 0 to Length(tmpStr)-1 do
    SendBuff[indx+5] := ord(tmpStr[indx+1]);
  SendLen := SendBuff[4] + 5;
  RecvLen := $02;

  retCode := SendAPDUandDisplay(2);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

procedure TMainMifareProg.bValStorClick(Sender: TObject);
var Amount: DWORD;
begin

  // Validate inputs
  if tValAmt.Text = '' then begin
    tValAmt.SetFocus;
    Exit;
  end;
  if StrToInt64(tValAmt.Text) > 4294967295 then begin
    tValAmt.Text := '4294967295';
    tValAmt.SetFocus;
    Exit;
  end;
  if tValBlk.Text = '' then begin
    tValBlk.SetFocus;
    Exit;
  end;
  if StrToInt(tValBlk.Text) > 255 then begin
    tValBlk.Text := '255';
    Exit;
  end;
  tValSrc.Text := '';
  tValTar.Text := '';

  Amount := StrToInt64(tValAmt.Text);
  ClearBuffers();
  SendBuff[0] := $FF;                             // CLA
  SendBuff[1] := $D7;                             // INS
  SendBuff[2] := $00;                             // P1
  SendBuff[3] := StrToInt(tValBlk.Text);          // P2 : Block No.
  SendBuff[4] := $05;                             // Lc : Data length
  SendBuff[5] := $00;                             // VB_OP Value
	SendBuff[6] := (Amount shr 24) and $FF;	        // Amount MSByte
	SendBuff[7] := (Amount shr 16) and $FF;	        // Amount middle byte
	SendBuff[8] := (Amount shr 8) and $FF;	        // Amount middle byte
	SendBuff[9] := Amount and $FF;			            // Amount LSByte

  SendLen := SendBuff[4] + 5;
  RecvLen := $02;

  retCode := SendAPDUandDisplay(2);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

procedure TMainMifareProg.bValReadClick(Sender: TObject);
var Amount: DWORD;
begin

  // Validate inputs
  if tValBlk.Text = '' then begin
    tValBlk.SetFocus;
    Exit;
  end;
  if StrToInt(tValBlk.Text) > 255 then begin
    tValBlk.Text := '255';
    Exit;
  end;
  tValAmt.Text := '';
  tValSrc.Text := '';
  tValTar.Text := '';

  ClearBuffers();
  SendBuff[0] := $FF;                             // CLA
  SendBuff[1] := $B1;                             // INS
  SendBuff[2] := $00;                             // P1
  SendBuff[3] := StrToInt(tValBlk.Text);          // P2 : Block No.
  SendBuff[4] := $00;                             // Le

  SendLen := $05;
  RecvLen := $06;

  retCode := SendAPDUandDisplay(2);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
	Amount := RecvBuff[3];
	Amount := Amount + (RecvBuff[2] * 256);
	Amount := Amount + (RecvBuff[1] * 256 * 256);
	Amount := Amount + (RecvBuff[0] * 256 * 256 * 256);
  tValAmt.Text := IntToStr(Amount);

end;

procedure TMainMifareProg.bValIncClick(Sender: TObject);
var Amount: DWORD;
begin

  // Validate inputs
  if tValAmt.Text = '' then begin
    tValAmt.SetFocus;
    Exit;
  end;
  if StrToInt64(tValAmt.Text) > 4294967295 then begin
    tValAmt.Text := '4294967295';
    tValAmt.SetFocus;
    Exit;
  end;
  if tValBlk.Text = '' then begin
    tValBlk.SetFocus;
    Exit;
  end;
  if StrToInt(tValBlk.Text) > 255 then begin
    tValBlk.Text := '255';
    Exit;
  end;
  tValSrc.Text := '';
  tValTar.Text := '';

  Amount := StrToInt64(tValAmt.Text);
  ClearBuffers();
  SendBuff[0] := $FF;                             // CLA
  SendBuff[1] := $D7;                             // INS
  SendBuff[2] := $00;                             // P1
  SendBuff[3] := StrToInt(tValBlk.Text);          // P2 : Block No.
  SendBuff[4] := $05;                             // Lc : Data length
  SendBuff[5] := $01;                             // VB_OP Value
	SendBuff[6] := (Amount shr 24) and $FF;	        // Amount MSByte
	SendBuff[7] := (Amount shr 16) and $FF;	        // Amount middle byte
	SendBuff[8] := (Amount shr 8) and $FF;	        // Amount middle byte
	SendBuff[9] := Amount and $FF;			            // Amount LSByte

  SendLen := SendBuff[4] + 5;
  RecvLen := $02;

  retCode := SendAPDUandDisplay(2);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

procedure TMainMifareProg.bValDecClick(Sender: TObject);
var Amount: DWORD;
begin

  // Validate inputs
  if tValAmt.Text = '' then begin
    tValAmt.SetFocus;
    Exit;
  end;
  if StrToInt64(tValAmt.Text) > 4294967295 then begin
    tValAmt.Text := '4294967295';
    tValAmt.SetFocus;
    Exit;
  end;
  if tValBlk.Text = '' then begin
    tValBlk.SetFocus;
    Exit;
  end;
  if StrToInt(tValBlk.Text) > 255 then begin
    tValBlk.Text := '255';
    Exit;
  end;
  tValSrc.Text := '';
  tValTar.Text := '';

  Amount := StrToInt64(tValAmt.Text);
  ClearBuffers();
  SendBuff[0] := $FF;                             // CLA
  SendBuff[1] := $D7;                             // INS
  SendBuff[2] := $00;                             // P1
  SendBuff[3] := StrToInt(tValBlk.Text);          // P2 : Block No.
  SendBuff[4] := $05;                             // Lc : Data length
  SendBuff[5] := $02;                             // VB_OP Value
	SendBuff[6] := (Amount shr 24) and $FF;	        // Amount MSByte
	SendBuff[7] := (Amount shr 16) and $FF;	        // Amount middle byte
	SendBuff[8] := (Amount shr 8) and $FF;	        // Amount middle byte
	SendBuff[9] := Amount and $FF;			            // Amount LSByte

  SendLen := SendBuff[4] + 5;
  RecvLen := $02;

  retCode := SendAPDUandDisplay(2);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

procedure TMainMifareProg.BValResClick(Sender: TObject);
begin

  // Validate inputs
  if tValSrc.Text = '' then begin
    tValSrc.SetFocus;
    Exit;
  end;
  if tValTar.Text = '' then begin
    tValTar.SetFocus;
    Exit;
  end;
  if StrToInt(tValSrc.Text) > 255 then begin
    tValSrc.Text := '255';
    Exit;
  end;
  if StrToInt(tValTar.Text) > 255 then begin
    tValTar.Text := '255';
    Exit;
  end;
  tValAmt.Text := '';
  tValBlk.Text := '';

  ClearBuffers();
  SendBuff[0] := $FF;                             // CLA
  SendBuff[1] := $D7;                             // INS
  SendBuff[2] := $00;                             // P1
  SendBuff[3] := StrToInt(tValSrc.Text);          // P2 : Source Block No.
  SendBuff[4] := $02;                             // Lc
  SendBuff[5] := $03;                             // Data In Byte 1
  SendBuff[6] := StrToInt(tValTar.Text);          // P2 : Target Block No.

  SendLen := $07;
  RecvLen := $02;

  retCode := SendAPDUandDisplay(2);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

end.
