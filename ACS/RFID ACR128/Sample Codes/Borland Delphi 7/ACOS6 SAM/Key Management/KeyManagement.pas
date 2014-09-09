///////////////////////////////////////////////////////////////////////////////
//
// FORM NAME : Key Management Sample
//
// COMPANY : ADVANDCED CARD SYSTEMS, LTD
//
// AUTHOR : MALCOLM BERNARD U. SOLAÑA
//
// DATE :  01 / 20 / 2007
//
//
// Description : This program implements the SAM & ACOS commands
//
//'   Initial Step :  1.  Press List Readers (SAM Initialization).
//'                   2.  Choose the SAM reader where you insert your SAM card.
//'                   3.  Press Connect.
//'                   4.  Enter 8 bytes global PIN (hex format).
//'                   5.  If you haven't initialize the SAM card yet, select algorithm to use (DES/3DES).
//'                          otherwise proceed to step 8 to initialize keys of new ACOS card.
//'                   6.  Enter 16 bytes Master Key to be use for key generation of
//'                          each type of ACOS Keys (e.g IC, PIN, Card Key....).
//'                   7.  Press Initialize SAM button.
//'                   8.  Press ACOS Card Initialization Tab.
//'                   9.  Press List Readers (ACOS Card Initialization).
//'                   10. Choose the slot reader where you insert your ACOS card.
//'                   11. Press Connect.
//'                   12. Select Algorithm Reference to use (DES/3DES)
//'                   13. Press Generate Keys.
//'                   14. Press Save Keys.
//'
//'   NOTE:
//'                   Please note that once the ACOS card was initialized or the IC was changed
//'                   from it's default key (0x41 0x43 0x4F 0x53 0x54 0x45 0x53 0x54) it is not possible to
//'                   save keys unless you change it's IC key back to default value.
// Revision Trail: (Date/Author/Description)
///////////////////////////////////////////////////////////////////////////////
unit KeyManagement;
interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, TabNotBk, ACSModule, StrUtils;

Const MAX_BUFFER_LEN    = 256;
Const INVALID_SW1SW2    = -450;

type
  TfrmMain = class(TForm)
    tabMain: TTabbedNotebook;
    grpSAM: TGroupBox;
    mMsg: TRichEdit;
    cmbSamReader: TComboBox;
    btnSAMList: TButton;
    btnSAMConnect: TButton;
    lblGlobalPin: TLabel;
    tSAMGPIN: TEdit;
    lblSAMIC: TLabel;
    lblSAMKc: TLabel;
    lblSAMKt: TLabel;
    lblSAMKd: TLabel;
    lblSAMKcr: TLabel;
    lblSAMKcf: TLabel;
    lblSAMKrd: TLabel;
    tSAMIC: TEdit;
    tSAMKc: TEdit;
    tSAMKt: TEdit;
    tSAMKd: TEdit;
    tSAMKcr: TEdit;
    tSAMKcf: TEdit;
    tSAMKrd: TEdit;
    btnInitSAM: TButton;
    grpACOS: TGroupBox;
    cmbACOSReader: TComboBox;
    btnACOSList: TButton;
    btnACOSConnect: TButton;
    lblCardSN: TLabel;
    lblACOSIC: TLabel;
    lblACOSPIN: TLabel;
    lblACOSKc: TLabel;
    lblACOSKt: TLabel;
    lblACOSKd: TLabel;
    lblACOSKcr: TLabel;
    lblACOSKcf: TLabel;
    lblACOSKrd: TLabel;
    tACOSCardSN: TEdit;
    tACOSIC: TEdit;
    tACOSPIN: TEdit;
    tACOSKc: TEdit;
    tACOSKt: TEdit;
    tACOSKd: TEdit;
    tACOSKcr: TEdit;
    tACOSKcf: TEdit;
    tACOSKrd: TEdit;
    Button1: TButton;
    Button2: TButton;
    rb3DES: TRadioButton;
    rbDES: TRadioButton;
    tACOSKcRyt: TEdit;
    tACOSKtRyt: TEdit;
    Label1: TLabel;
    tACOSKdRyt: TEdit;
    tACOSKcrRyt: TEdit;
    tACOSKcfRyt: TEdit;
    tACOSKrdRyt: TEdit;
    procedure btnSAMListClick(Sender: TObject);
    procedure btnSAMConnectClick(Sender: TObject);
    procedure btnACOSListClick(Sender: TObject);
    procedure btnACOSConnectClick(Sender: TObject);
    procedure rb3DESClick(Sender: TObject);
    procedure rbDESClick(Sender: TObject);
    procedure btnInitSAMClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure tSAMGPINKeyPress(Sender: TObject; var Key: Char);
    procedure tSAMICKeyPress(Sender: TObject; var Key: Char);
    procedure tSAMKcKeyPress(Sender: TObject; var Key: Char);
    procedure tSAMKtKeyPress(Sender: TObject; var Key: Char);
    procedure tSAMKdKeyPress(Sender: TObject; var Key: Char);
    procedure tSAMKcrKeyPress(Sender: TObject; var Key: Char);
    procedure tSAMKcfKeyPress(Sender: TObject; var Key: Char);
    procedure tSAMKrdKeyPress(Sender: TObject; var Key: Char);
    procedure tACOSPINKeyPress(Sender: TObject; var Key: Char);
    procedure btnResetClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);

  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmMain: TfrmMain;
  G_hContext    : SCARDCONTEXT;
  G_hCard       : SCARDCONTEXT;
  G_hSAMCard    : SCARDCONTEXT;
  G_ioRequest   : SCARD_IO_REQUEST;
  G_RdrState    : SCARD_READERSTATE;
  G_retCode     : Integer;
  G_dwActProtocol, BufferLen  : DWORD;
  G_SendBuff   : array [0..262] of Byte;
  G_RecvBuff      : array [0..262] of Byte;
  G_SendLen      : DWORD;
  G_RecvLen          : DWORD;
  G_Buffer      : array [0..MAX_BUFFER_LEN] of char;
  G_SessionKey  : array [0..15] of Byte;
  G_cKey        : array [0..15] of Byte;
  G_tKey        : array [0..15] of Byte;
  G_ConnActive  : Boolean;
  G_ConnActiveMCU  : Boolean;
  G_AlgoRef : Byte;
  G_TLV_LEN : Byte;


  procedure displayOut(errType: Integer; retVal: Integer; PrintText: String; AppText : STRING);
  procedure ClearBuffers();
  Procedure ResetSAM();
  Procedure ResetMCU();
  procedure InitMenu();
  
  function writeRecord(caseType: Integer; RecNo: Byte; maxDataLen: Byte; DataLen: Byte; DataIn:array of Byte): integer;
  function SendAPDUSAM(SendBuff : array of Byte; SendLen : integer;  RecLev : integer ; RecvBuff : array of Byte): Boolean;
  Function CreateSamFile(FileLen : Byte; DataArr : Array of Byte; maxDataLen : Integer) : integer;
  Function AppendSamFile(KeyId : Byte; DataArr: Array of byte; maxDatalen : integer) : integer;
  function SendAPDUandDisplay(SendType: integer; ApduIn: string): integer;
  function selectFile(HiAddr: Byte; LoAddr: Byte): integer;
  function readRecord(RecNo: Byte; DataLen: Byte): integer;
  function SubmitIC(): integer;
  Function GenerateSAMKey(KeyId : Byte; DataArr: Array of byte; maxDatalen : integer) : integer;

implementation

{$R *.dfm}


procedure InitMenu();
begin
  frmMain.mMsg.Clear;
end;

procedure displayOut(errType: Integer; retVal: Integer; PrintText: String; AppText : STRING);
	//Displays the APDU sent and received by the SAM and MCU card..
	//returns 1 if erronous and 0 if successful
begin

  case errType of
    0: frmMain.mMsg.SelAttributes.Color := clTeal;      // Notifications
    1: begin                                                    // Error Messages
         frmMain.mMsg.SelAttributes.Color := clRed;
         PrintText := APPTEXT + '>' + GetScardErrMsg(retVal);
       end;
    2: begin
         frmMain.mMsg.SelAttributes.Color := clBlack;
         PrintText := APPTEXT + '< ' + PrintText;                      // Input data
       end;
    3: begin
         frmMain.mMsg.SelAttributes.Color := clBlack;
         PrintText := APPTEXT + '> ' + PrintText;                      // Output data
       end;
  end;
  frmMain.mMsg.Lines.Add(PrintText);
  frmMain.mMsg.SelAttributes.Color := clBlack;

end ;

procedure TfrmMain.btnSAMListClick(Sender: TObject);
begin
  // 1. Establish context and obtain hContext handle
  G_retCode := SCardEstablishContext(SCARD_SCOPE_USER,
                                   nil,
                                   nil,
                                   @G_hContext);
  if G_retCode <> SCARD_S_SUCCESS then
    begin
      displayout(1, G_retCode, '', 'SAM');
      Exit;
    end ;

  // 2. List PC/SC card readers installed in the system
  BufferLen := MAX_BUFFER_LEN;
  G_retCode := SCardListReadersA(G_hContext,
                               nil,
                               @G_Buffer,
                               @BufferLen);
  if G_retCode <> SCARD_S_SUCCESS then
    begin
      DisplayOut(1, G_retCode, '', 'SAM');
      Exit;
    end
  else
    DisplayOut(0, 0, 'Select reader, insert mcu card and connect.', 'SAM');

  frmmain.cmbSamReader.Clear;
  LoadListToControl(frmmain.cmbSamReader,@G_buffer,bufferLen);
  frmmain.cmbSamReader.ItemIndex := 0;
end;

procedure TfrmMain.btnSAMConnectClick(Sender: TObject);
var ReaderLen, ATRLen: DWORD;
    dwState: integer;
    ATRVal: array[0..19] of Byte;
    tmpStr: String;
    indx: integer;
begin

  if G_ConnActive then
  begin
    DisplayOut(0, 0, 'Connection is already active.', 'SAM');
    Exit;
  end;

  DisplayOut(2, 0, 'Invoke SCardConnect', 'SAM');
  // 1. Connect to selected reader using hContext handle
  //    and obtain valid hCard handle
  G_retCode := SCardConnectA(G_hContext,
                           PChar(cmbSamReader.Text),
                           SCARD_SHARE_SHARED,
                           SCARD_PROTOCOL_T0 or SCARD_PROTOCOL_T1,
                           @G_hSAMCard,
                           @G_dwActProtocol);
  if G_retCode <> SCARD_S_SUCCESS then
    begin
      DisplayOut(1, G_retCode, '', 'SAM');
      G_ConnActive := False;
      Exit;
    end
  else
    DisplayOut(0, 0, 'Successful connection to ' + cmbSamReader.Text, 'SAM');

  DisplayOut(2, 0, 'Get Card Status', 'SAM');
  ATRLen := 32;
  ReaderLen := 0;
  dwState := 0;
  G_retCode := SCardStatusA(G_hSAMCard,
                         PChar(cmbSamReader.Text),
                         @ReaderLen,
                         @dwState,
                         @G_dwActProtocol,
                         @ATRVal,
                         @ATRLen);

  tmpStr := '';
  for indx := 0 to integer(ATRLen)-1 do
    begin
      tmpStr := tmpStr + Format('%.02X ',[ATRVal[indx]]);
    end;

  DisplayOut(3, 0, Format('ATR Value: %s',[tmpStr]), 'SAM');
  tmpStr := '';

  case integer(G_dwActProtocol) of
    1: tmpStr := 'T=0';
    2: tmpStr := 'T=1';
  else
    tmpStr := 'No protocol is defined.';
  end;

  DisplayOut(3, 0, Format('Active Protocol: %s',[tmpStr]), 'SAM');

  G_ConnActive := True;

  //Enable PIN and Master Keys Inputbox
  tSAMGPIN.Enabled := True;
  rbDES.Enabled := True;
  rb3DES.Enabled := True;

  tSAMIC.Enabled := True;
  TSAMKc.Enabled := True;
  TSAMKt.Enabled := True;
  TSAMKd.Enabled := True;
  TSAMKcr.Enabled := True;
  TSAMKcf.Enabled := True;
  TSAMKrd.Enabled := True;

  btnInitSAM.Enabled := True;

end;

Procedure ResetSAM();
//Resets the connection to SAM card..
begin
  if G_ConnActive then
    begin
      G_retCode := SCardDisconnect(G_hSAMCard, SCARD_UNPOWER_CARD);
      G_ConnActive := False;
    end;

  G_retCode := SCardReleaseContext(G_hSAMCard);
end;

Procedure ResetMCU();
//Resets the connection to ACOS card..
begin
  if G_ConnActiveMCU then
    begin
      G_retCode := SCardDisconnect(G_hCard, SCARD_UNPOWER_CARD);
      G_ConnActiveMCU := False;
    end;

  G_retCode := SCardReleaseContext(G_hCard);
end;

procedure TfrmMain.btnACOSListClick(Sender: TObject);
begin
    // 1. Establish context and obtain hContext handle
  G_retCode := SCardEstablishContext(SCARD_SCOPE_USER,
                                   nil,
                                   nil,
                                   @G_hContext);
  if G_retCode <> SCARD_S_SUCCESS then
    begin
      displayout(1, G_retCode, '', 'ACOS');
      Exit;
    end ;

  // 2. List PC/SC card readers installed in the system
  BufferLen := MAX_BUFFER_LEN;
  G_retCode := SCardListReadersA(G_hContext,
                               nil,
                               @G_Buffer,
                               @BufferLen);
  if G_retCode <> SCARD_S_SUCCESS then
    begin
      DisplayOut(1, G_retCode, '', 'ACOS');
      Exit;
    end
  else
    DisplayOut(0, 0, 'Select reader, insert mcu card and connect.', 'ACOS');

  frmmain.cmbACOSReader.Clear;
  LoadListToControl(frmmain.cmbACOSReader,@G_buffer,bufferLen);
  frmmain.cmbACOSReader.ItemIndex := 0;
end;

procedure TfrmMain.btnACOSConnectClick(Sender: TObject);
var ReaderLen, ATRLen: DWORD;
    dwState: integer;
    ATRVal: array[0..19] of Byte;
    tmpStr: String;
    indx: integer;
begin

  if G_ConnActiveMCU then
  begin
    DisplayOut(0, 0, 'Connection is already active.', 'ACOS');
    Exit;
  end;

  DisplayOut(2, 0, 'Invoke SCardConnect', 'ACOS');
  // 1. Connect to selected reader using hContext handle
  //    and obtain valid hCard handle
  G_retCode := SCardConnectA(G_hContext,
                           PChar(cmbACOSReader.Text),
                           SCARD_SHARE_SHARED,
                           SCARD_PROTOCOL_T0 or SCARD_PROTOCOL_T1,
                           @G_hCard,
                           @G_dwActProtocol);

  if G_retCode <> SCARD_S_SUCCESS then
    begin
      DisplayOut(1, G_retCode, '', 'ACOS');
      G_ConnActive := False;
      Exit;
    end
  else

  DisplayOut(0, 0, 'Successful connection to ' + cmbACOSReader.Text, 'ACOS');
  DisplayOut(2, 0, 'Get Card Status', 'ACOS');
  ATRLen := 32;
  ReaderLen := 0;
  dwState := 0;
  G_retCode := SCardStatusA(G_hCard,
                         PChar(cmbACOSReader.Text),
                         @ReaderLen,
                         @dwState,
                         @G_dwActProtocol,
                         @ATRVal,
                         @ATRLen);
  tmpStr := '';
  for indx := 0 to integer(ATRLen)-1 do
    begin
      tmpStr := tmpStr + Format('%.02X ',[ATRVal[indx]]);
    end;

  DisplayOut(3, 0, Format('ATR Value: %s',[tmpStr]), 'ACOS');
  tmpStr := '';
  case integer(G_dwActProtocol) of
    1: tmpStr := 'T=0';
    2: tmpStr := 'T=1';
    else
      tmpStr := 'No protocol is defined.';
    end;
  DisplayOut(3, 0, Format('Active Protocol: %s',[tmpStr]), 'ACOS');

  rbDes.Enabled := True;
  rb3Des.Enabled := True;
  rbDes.Checked := True;
  G_AlgoRef := 1;
  G_ConnActiveMCU := True;
end ;

procedure ClearBuffers();
    	//Clears the send and receive buffer for the PCSC Commands
      
var indx: integer;
begin

  for indx := 0 to 262 do
    begin
      G_SendBuff[indx] := $00;
      G_RecvBuff[indx] := $00;
    end;

end;


function SubmitIC(): integer;
  //Submits the default IC to the ACOS Card...
var indx: integer;
    tmpStr: String;
begin

  ClearBuffers();
  G_SendBuff[0] := $80;     // CLA
  G_SendBuff[1] := $20;     // INS
  G_SendBuff[2] := $07;     // P1
  G_SendBuff[3] := $00;     // P2
  G_SendBuff[4] := $08;     // P3
  G_SendBuff[5] := $41;     // A
  G_SendBuff[6] := $43;     // C
  G_SendBuff[7] := $4F;     // O
  G_SendBuff[8] := $53;     // S
  G_SendBuff[9] := $54;     // T
  G_SendBuff[10] := $45;    // E
  G_SendBuff[11] := $53;    // S
  G_SendBuff[12] := $54;    // T
  G_SendLen := $0D;
  G_RecvLen := $02;

  tmpStr := '';
  for indx := 0 to G_SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);
  DisplayOut(2, 0, tmpstr, 'MCU');
  
  G_retCode := SendAPDUandDisplay(0, tmpStr);
  if G_retCode <> SCARD_S_SUCCESS then
    begin
      SubmitIC := G_retCode;
      Exit;
    end ;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[G_RecvBuff[indx]]) + ' ';
  if tmpStr <> '90 00 ' then
    begin
      displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr, 'MCU');
      G_retCode := INVALID_SW1SW2;
      SubmitIC := G_retCode;
      Exit;
    end;
  SubmitIC := G_retCode;

end;

function SendAPDUSAM(SendBuff : array of Byte; SendLen : integer;  RecLev : integer ; RecvBuff : array of Byte): Boolean;
  //function that calls the formal command to the SAM card..
var tmpStr: string;
    indx: integer;
begin
  //Send APDU to SAM Card Reader
  G_ioRequest.dwProtocol := G_dwActProtocol;
  G_ioRequest.cbPciLength := sizeof(SCARD_IO_REQUEST);

  G_retCode := SCardTransmit(G_hSAMCard,
                           @G_ioRequest,
                           @G_SendBuff,
                           G_SendLen,
                           @G_ioRequest,
                           @G_RecvBuff,
                           @G_RecvLen);

  if G_retCode <> SCARD_S_SUCCESS then
    begin
        for indx := 0 to G_RecvLen -1 do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[Indx])]);

        DisplayOut(1, G_retCode, '', 'SAM');
        SendAPDUSAM := False;
        Exit;
    end;

  if G_retCode = SCARD_S_SUCCESS then
    begin
      for indx := 0 to G_RecvLen - 1 do
        tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[Indx])]);
    end;

  DisplayOut(3, 0, Trim(tmpStr), 'SAM');
  SendAPDUSAM := True;

end;

function SendAPDUandDisplay(SendType: integer; ApduIn: string): integer;
  //function that calls the formal command to the ACOS card..
var tmpStr: string;
    indx: integer;
begin

  //Send APDU to ACOS Card Reader
  G_ioRequest.dwProtocol := G_dwActProtocol;
  G_ioRequest.cbPciLength := sizeof(SCARD_IO_REQUEST);
  G_RecvLen := 262;
  G_retCode := SCardTransmit(G_hCard,
                           @G_ioRequest,
                           @G_SendBuff,
                           G_SendLen,
                           @G_ioRequest,
                           @G_RecvBuff,
                           @G_RecvLen);
  if G_retCode <> SCARD_S_SUCCESS then
    begin
      DisplayOut(1, 0, 'Error in Card Transmit', 'MCU');
      SendAPDUandDisplay := G_retCode;
      Exit;
    end
  else
    begin
      case SendType of
      0: begin      // Read all data received
          for indx := 0 to G_RecvLen-1 do
           tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[Indx])]);
         end;
      1: begin      // Read ATR after checking SW1/SW2
          for indx := (G_RecvLen-2) to (G_RecvLen-1) do
           tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

          if (Trim(tmpStr) <> '90 00') then
           DisplayOut(1, 0, 'Return bytes are not acceptable.', 'MCU')
          else
           begin
             tmpStr := 'ATR :';
             for indx := 0 to (G_RecvLen-3) do
               tmpStr := tmpStr + Format('%.2X ', [(G_RecvBuff[Indx])]);
           end;
         end;
      2: begin      // Read SW1/SW2
         for indx := (G_RecvLen-2) to (G_RecvLen-1) do
           tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

         if (Trim(tmpStr) <> '90 00') then
           DisplayOut(1, 0, 'Return bytes are not acceptable.', 'MCU')
         else
           begin
             tmpStr := '';
             for indx := 0 to (G_RecvLen-3) do
               tmpStr := tmpStr + Format('%.2X ', [(G_RecvBuff[Indx])]);
           end;
         end;

      end;
      DisplayOut(3, 0, Trim(tmpStr), 'MCU');
  end;
  SendAPDUandDisplay := G_retCode;

end;

function selectFile(HiAddr: Byte; LoAddr: Byte): integer;
  //selects the current file structure needed on the ACOS card..
var tmpStr: String;
    indx: integer;
begin

  ClearBuffers();
  G_SendBuff[0] := $80;     // CLA
  G_SendBuff[1] := $A4;     // INS
  G_SendBuff[2] := $00;     // P1
  G_SendBuff[3] := $00;     // P2
  G_SendBuff[4] := $02;     // P3
  G_SendBuff[5] := HiAddr;     // Value of High Byte
  G_SendBuff[6] := LoAddr;     // Value of Low Byte
  G_SendLen := $07;
  G_RecvLen := $02;
  tmpStr := '';

  for indx := 0 to G_SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);

  DisplayOut(2, 0, tmpstr, 'MCU');
  G_retCode := SendAPDUandDisplay(0, tmpStr);
  if G_retCode <> SCARD_S_SUCCESS then
    begin
      selectFile := G_retCode;
      Exit;
    end ;
  selectFile := G_retCode;

end;

function writeRecord(caseType: Integer; RecNo: Byte; maxDataLen: Byte; DataLen: Byte; DataIn:array of Byte): integer;
  //Writes the data needed to the ACOS Card..
  //Note : Please select the file currently needed first
  //      before writing to card.
var indx: integer;
    tmpStr: String;
begin

  if caseType = 1 then   // If card data is to be erased before writing new data
    begin
      // 1. Re-initialize card values to $00
      ClearBuffers();
      G_SendBuff[0] :=  $80;          // CLA
      G_SendBuff[1] :=  $D2;          // INS
      G_SendBuff[2] :=  RecNo;        // P1    Record to be written
      G_SendBuff[3] :=  $00;          // P2
      G_SendBuff[4] :=  maxDataLen;   // P3    Length
      for indx := 0 to maxDataLen-1 do
        G_SendBuff[indx + 5] := $00;

      G_SendLen := maxDataLen + 5;
      G_RecvLen := $02;
      tmpStr := '';
      for indx := 0 to G_SendLen-1 do
        tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);

      DisplayOut(2, 0, tmpstr, 'MCU');
      G_retCode := SendAPDUandDisplay(0, tmpStr);
      if G_retCode <> SCARD_S_SUCCESS then
        begin
          writeRecord := G_retCode;
          Exit;
        end;
      tmpStr := '';

      for indx := 0 to 1 do
        tmpStr  := tmpStr + Format('%.2x',[G_RecvBuff[indx]]) + ' ';

      if tmpStr <> '90 00 ' then
        begin
          displayOut(1, 0, 'The return string is invalid. Value: ' + tmpStr, 'ACOS');
          G_retCode := INVALID_SW1SW2;
          writeRecord := G_retCode;
          Exit;
        end;
    end;

  // 2. Write data to card
  ClearBuffers();
  G_SendBuff[0] :=  $80;          // CLA
  G_SendBuff[1] :=  $D2;          // INS
  G_SendBuff[2] :=  RecNo;        // P1    Record to be written
  G_SendBuff[3] :=  $00;          // P2
  G_SendBuff[4] :=  DataLen;   // P3    Length
  for indx := 0 to DataLen-1 do
    G_SendBuff[indx + 5] := DataIn[indx];

  G_SendLen := DataLen + 5;
  G_RecvLen := $02;
  tmpStr := '';
  for indx := 0 to G_SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);

  DisplayOut(2, 0, tmpstr, 'MCU');
  G_retCode := SendAPDUandDisplay(0, tmpStr);
  if G_retCode <> SCARD_S_SUCCESS then
    begin
      writeRecord := G_retCode;
      Exit;
    end;

  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[G_RecvBuff[indx]]) + ' ';
  if tmpStr <> '90 00 ' then
    begin
      displayOut(1, 0, 'The return string is invalid. Value: ' + tmpStr, 'ACOS');
      G_retCode := INVALID_SW1SW2;
      writeRecord := G_retCode;
      Exit;
    end;
  writeRecord := G_retCode;

end;

Function CreateSamFile(FileLen : Byte; DataArr : Array of Byte; maxDataLen : Integer) : integer;
  //Creates/Defines a SAM file
	//returns 1 if erronous and 0 if successful
var indx : integer;
    tmpstr : string;
begin
    CreateSamFile := 0;
    ClearBuffers();
    G_SendBuff[0] := $00;
    G_SendBuff[1] := $E0;
    G_SendBuff[2] := $00;
    G_SendBuff[3] := $00;
    G_SendBuff[4] := FileLen;

    for indx := 0 to maxDatalen - 1 do
      G_SendBuff[indx + 5] := DataArr[indx];

    tmpstr := '';
    for indx := 0 to (maxDatalen + 5) -1 do
        tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    G_RecvLen := 2;
    G_SendLen := maxDatalen + 5;

    tmpstr := '';
    if SendAPDUSAM(G_SendBuff, G_SendLen, G_RecvLen, G_RecvBuff) = True then
      begin

          for indx := (G_RecvLen-2) to (G_RecvLen-1) do
           tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);
          if (Trim(tmpStr) <> '90 00') then begin
              DisplayOut(1, 0, Trim(tmpStr), 'SAM');
              CreateSamFile := 1;
            end
          else
            CreateSamFile := 0;
      end;

end;

Function GenerateSAMKey(KeyId : Byte; DataArr: Array of byte; maxDatalen : integer) : integer;
  //Generates the SAM key base from the User Input..
var indx : integer;
    tmpstr : string;
begin
    GenerateSAMKey := 0;
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $88;
    G_SendBuff[2] := $00;
    G_SendBuff[3] := KeyId; //KeyID
    G_SendBuff[4] := $8;

    for indx := 0 to maxDatalen - 1 do
      G_SendBuff[indx + 5] := DataArr[indx];

    tmpstr := '';
    for indx := 0 to (maxDatalen + 5) -1 do
      tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    G_RecvLen := 2;
    G_SendLen := (maxDatalen + 5);
    if SendAPDUSAM(G_SendBuff, G_SendLen, G_RecvLen, G_RecvBuff) = True then
        begin
          tmpstr := '';
          for indx := (G_RecvLen-2) to (G_RecvLen-1) do
            tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

          if (Trim(tmpStr) <> '61 08') then
            begin
              DisplayOut(1, 0, Trim(tmpStr), 'SAM');
              exit;
              GenerateSAMKey := 1;
            end
          else
            GenerateSAMKey := 0;
        end;
end;

Function GetSAMResponse() : String;
  //Acquires the SAM Key generated..
  //Function returns the hex value of generated key..
var indx : integer;
    tmpstr : string;
Begin
    GetSAMResponse := '';
    ClearBuffers();
    G_SendBuff[0] := $00;
    G_SendBuff[1] := $C0;
    G_SendBuff[2] := $00;
    G_SendBuff[3] := $00;
    G_SendBuff[4] := $8;

    tmpstr := '';
    for indx := 0 to 4 do
      tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    G_RecvLen := 10;
    G_SendLen := 5;

    if SendAPDUSAM(G_SendBuff, G_SendLen, G_RecvLen, G_RecvBuff) = True then
        begin
          tmpstr := '';
          for indx := (G_RecvLen-2) to (G_RecvLen-1) do
            tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

          if (Trim(tmpStr) <> '90 00') then
            begin
              DisplayOut(1, G_RetCode, Trim(tmpStr), 'SAM');
              GetSAMResponse := '';
              exit;
            end
          else
            begin
                //Generated Key  IC
                tmpstr := '';
                for indx := 0 to 7 do
                  tmpStr := tmpStr + Format('%.02X', [(G_RecvBuff[indx])]);

                //DisplayOut(3, 0, 'Key : ' + Trim(tmpStr), 'SAM');
                GetSAMResponse := Trim(tmpStr);
            end;
        end;

end;

Function AppendSamFile(KeyId : Byte; DataArr: Array of byte; maxDatalen : integer) : integer;
    //Appends the SAM file when creating a DF
	  //returns 1 if erronous and 0 if successful
var indx : integer;
    tmpstr : string;
begin
    AppendSamFile := 0;
    ClearBuffers();
    G_SendBuff[0] := $00;
    G_SendBuff[1] := $E2;
    G_SendBuff[2] := $00;
    G_SendBuff[3] := $00;
    G_SendBuff[4] := $16;
    G_SendBuff[5] := KeyId;
    G_SendBuff[6] := $3;
    G_SendBuff[7] := $FF;
    G_SendBuff[8] := $FF;
    G_SendBuff[9] := $88;
    G_SendBuff[10] := $00;

    for indx := 0 to maxDatalen - 1 do
      G_SendBuff[indx + 11] := DataArr[indx];

    tmpstr := '';

    for indx := 0 to (maxDatalen + 11) -1 do
       tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    G_RecvLen := 2;
    G_SendLen := (maxDatalen + 11);

	  if SendAPDUSAM(G_SendBuff, G_SendLen, G_RecvLen, G_RecvBuff) = True then
      begin
        tmpstr := '';
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

        if (Trim(tmpStr) <> '90 00') then begin
          DisplayOut(1, 0, Trim(tmpStr), 'SAM');
          AppendSamFile := 1;
          end
		    else
		      AppendSamFile := 0;
      end;
end;

function readRecord(RecNo: Byte; DataLen: Byte): integer;
    //Reads the record on a Specified file on the MCU card.
	//Return 1 if erroneous and 0 if successful
var indx: integer;
    tmpStr: String;
begin

  ClearBuffers();
  G_SendBuff[0] := $80;        // CLA
  G_SendBuff[1] := $B2;        // INS
  G_SendBuff[2] := RecNo;      // P1    Record No
  G_SendBuff[3] := $00;        // P2
  G_SendBuff[4] := DataLen;    // P3    Length of data to be read
  G_SendLen := $05;
  G_RecvLen := G_SendBuff[4] + 2;
  tmpStr := '';
  for indx := 0 to G_SendLen-1 do
    tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);
  displayOut(3, 0, tmpStr, 'MCU');

  G_retCode := SendAPDUandDisplay(0, tmpStr);
  if G_retCode <> SCARD_S_SUCCESS then
    begin
      readRecord := G_retCode;
      Exit;
    end;

  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[G_RecvBuff[indx + G_SendBuff[4]]]) + ' ';
  if tmpStr <> '90 00 ' then
    begin
      displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr, 'ACOS');
      G_retCode := INVALID_SW1SW2;
    end;
  readRecord := G_retCode;

end;

procedure TfrmMain.rb3DESClick(Sender: TObject);
begin

      G_AlgoRef := 0;

end;

procedure TfrmMain.rbDESClick(Sender: TObject);
begin

     G_AlgoRef := 1;
end;


procedure TfrmMain.btnInitSAMClick(Sender: TObject);
var tmpStr : String;
    indx : integer;
    tmpArr : array [0..42] of byte;
begin

    if length(tSAMGPIN.Text) < tSAMGPIN.MaxLength then
      begin
       DisplayOut(0, 0, 'Invalid GLobal Pin Entry', 'SAM');
       tSAMGPIN.SetFocus;
       exit;
      end;

    if length(tSAMIC.Text) < tSAMIC.MaxLength then
      begin
       DisplayOut(0, 0, 'Invalid IC Entry', 'SAM');
       tSAMIC.SetFocus;
       exit;
      end;

    if length(tSAMKC.Text) < tSAMKC.MaxLength then
      begin
       DisplayOut(0, 0, 'Invalid Card Key Entry', 'SAM');
       tSAMKc.SetFocus;
       exit;
      end;

    if length(tSAMKt.Text) < tSAMKt.MaxLength then
      begin
       DisplayOut(0, 0, 'Invalid Terminal Key Entry', 'SAM');
       tSAMKt.SetFocus;
       exit;
      end;

    if length(tSAMKd.Text) < tSAMKd.MaxLength then
      begin
       DisplayOut(0, 0, 'Invalid Debit Key Entry', 'SAM');
       tSAMKd.SetFocus;
       exit;
      end;

    if length(tSAMKcr.Text) < tSAMKcr.MaxLength then
      begin
       DisplayOut(0, 0, 'Invalid Credit Key Entry', 'SAM');
       tSAMKcr.SetFocus;
       exit;
      end;

    if length(tSAMKcf.Text) < tSAMKcf.MaxLength then
      begin
       DisplayOut(0, 0, 'Invalid Certify Key Entry', 'SAM');
       tSAMKcf.SetFocus;
       exit;
      end;

    if length(tSAMKrd.Text) < tSAMKrd.MaxLength then
      begin
       DisplayOut(0, 0, 'Invalid Revoke Key Entry', 'SAM');
       tSAMKrd.SetFocus;
       exit;
      end;

    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $30;
    G_SendBuff[2] := $00;
    G_SendBuff[3] := $00;
    G_SendBuff[4] := $00;

    for indx := 0 to 4 do
      tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');

    G_RecvLen := 2;
    G_SendLen := 5;
    tmpstr := '' ;
    if SendAPDUSAM(G_SendBuff, G_SendLen, G_RecvLen, G_RecvBuff) = False then begin
      for indx := 0 to 1 do
        tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[Indx])]);
        exit;
      end
    else
      for indx := 0 to 1 do
     tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');

    //Close Connection
    ResetSAM();
    DisplayOut(0, 0, 'Closing connection to ' + cmbSamReader.Text, 'SAM');

    DisplayOut(2, 0, 'Invoke SCardConnect', 'SAM');
    //Re-apply connection
    G_retCode := SCardConnectA(G_hContext,
                           PChar(cmbSamReader.Text),
                           SCARD_SHARE_SHARED,
                           SCARD_PROTOCOL_T0 or SCARD_PROTOCOL_T1,
                           @G_hSAMCard,
                           @G_dwActProtocol);
    if G_retCode <> SCARD_S_SUCCESS then
      begin
        DisplayOut(1, G_retCode, '', 'SAM');
        G_ConnActive := False;
        Exit;
      end
    else
      DisplayOut(0, 0, 'Successful re-connection to ' + cmbSamReader.Text, 'SAM');

    //1. Create Master File *****************************
    tmpArr[0] := $62;
    tmpArr[1] := $C;
    tmpArr[2] := $80;
    tmpArr[3] := $2;
    tmpArr[4] := $2C;
    tmpArr[5] := $00;
    tmpArr[6] := $82;
    tmpArr[7] := $2;
    tmpArr[8] := $3F;
    tmpArr[9] := $FF;
    tmpArr[10] := $83;
    tmpArr[11] := $2;
    tmpArr[12] := $3F;
    tmpArr[13] := $00;

    if CreateSamFile($E,tmpArr,14) <> 0 then
      Exit;

    //Create EF1 to store PIN's******************************
    //FDB=0C MRL=0A NOR=01 READ=NONE WRITE=IC*********************
    tmpArr[0] := $62;
    tmpArr[1] := $19;
    tmpArr[2] := $83;
    tmpArr[3] := $2;
    tmpArr[4] := $FF;
    tmpArr[5] := $A;
    tmpArr[6] := $88;
    tmpArr[7] := $1;
    tmpArr[8] := $1;
    tmpArr[9] := $82;
    tmpArr[10] := $6;
    tmpArr[11] := $C;
    tmpArr[12] := $00;
    tmpArr[13] := $00;
    tmpArr[14] := $A;
    tmpArr[15] := $00;
    tmpArr[16] := $1;
    tmpArr[17] := $8C;
    tmpArr[18] := $8;
    tmpArr[19] := $7F;
    tmpArr[20] := $FF;
    tmpArr[21] := $FF;
    tmpArr[22] := $FF;
    tmpArr[23] := $FF;
    tmpArr[24] := $27;
    tmpArr[25] := $27;
    tmpArr[26] := $FF;

    if CreateSamFile($1B,tmpArr,27) <> 0 then
      exit;

    //Set Global PIN's************************************
    ClearBuffers();

    G_SendBuff[0] := $00;
    G_SendBuff[1] := $DC;
    G_SendBuff[2] := $1;
    G_SendBuff[3] := $4;
    G_SendBuff[4] := $A;
    G_SendBuff[5] := $1;
    G_SendBuff[6] := $88;
    G_SendBuff[7] := strToInt('$' + MidStr(tSAMGPIN.Text,1,2));
    G_SendBuff[8] := strToInt('$' + MidStr(tSAMGPIN.Text,3,2));
    G_SendBuff[9] := strToInt('$' + MidStr(tSAMGPIN.Text,5,2));
    G_SendBuff[10] := strToInt('$' + MidStr(tSAMGPIN.Text,7,2));
    G_SendBuff[11] := strToInt('$' + MidStr(tSAMGPIN.Text,9,2));
    G_SendBuff[12] := strToInt('$' + MidStr(tSAMGPIN.Text,11,2));
    G_SendBuff[13] := strToInt('$' + MidStr(tSAMGPIN.Text,13,2));
    G_SendBuff[14] := strToInt('$' + MidStr(tSAMGPIN.Text,15,2));

    tmpstr := '';
    for indx := 0 to 14 do
      tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    G_RecvLen := 2;
    G_SendLen := 15;
    if SendAPDUSAM(G_SendBuff, G_SendLen, G_RecvLen, G_RecvBuff) = True then
      begin
        tmpstr := '';
          for indx := (G_RecvLen-2) to (G_RecvLen-1) do
           tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

          if (Trim(tmpStr) <> '90 00') then
            begin
              DisplayOut(1, 0, Trim(tmpStr), 'SAM');
              exit;
            end;
      end;

     //Create Next DF DRT01: 1100 ***************************
    tmpArr[0] := $62;
    tmpArr[1] := $29;
    tmpArr[2] := $82;
    tmpArr[3] := $1;
    tmpArr[4] := $38;
    tmpArr[5] := $83;
    tmpArr[6] := $2;
    tmpArr[7] := $11;
    tmpArr[8] := $00;
    tmpArr[9] := $8A;
    tmpArr[10] := $1;
    tmpArr[11] := $1;
    tmpArr[12] := $8C;
    tmpArr[13] := $8;
    tmpArr[14] := $7F;
    tmpArr[15] := $3;
    tmpArr[16] := $3;
    tmpArr[17] := $3;
    tmpArr[18] := $3;
    tmpArr[19] := $3;
    tmpArr[20] := $3;
    tmpArr[21] := $3;
    tmpArr[22] := $8D;
    tmpArr[23] := $2;
    tmpArr[24] := $41;
    tmpArr[25] := $3;
    tmpArr[26] := $80;
    tmpArr[27] := $2;
    tmpArr[28] := $3;
    tmpArr[29] := $20;
    tmpArr[30] := $AB;
    tmpArr[31] := $B;
    tmpArr[32] := $84;
    tmpArr[33] := $1;
    tmpArr[34] := $88;
    tmpArr[35] := $A4;
    tmpArr[36] := $6;
    tmpArr[37] := $83;
    tmpArr[38] := $1;
    tmpArr[39] := $81;
    tmpArr[40] := $95;
    tmpArr[41] := $1;
    tmpArr[42] := $FF;

    if CreateSamFile($2B,tmpArr,43) <> 0 then
      exit;

     //Create Key File EF2 1101 **************************************
    //MRL=16 NOR=08 **************************************************
    tmpArr[0] := $62;
    tmpArr[1] := $1B;
    tmpArr[2] := $82;
    tmpArr[3] := $5;
    tmpArr[4] := $C;
    tmpArr[5] := $41;
    tmpArr[6] := $00;
    tmpArr[7] := $16;
    tmpArr[8] := $8;
    tmpArr[9] := $83;
    tmpArr[10] := $2;
    tmpArr[11] := $11;
    tmpArr[12] := $1;
    tmpArr[13] := $88;
    tmpArr[14] := $1;
    tmpArr[15] := $2;
    tmpArr[16] := $8A;
    tmpArr[17] := $1;
    tmpArr[18] := $1;
    tmpArr[19] := $8C;
    tmpArr[20] := $8;
    tmpArr[21] := $7F;
    tmpArr[22] := $3;
    tmpArr[23] := $3;
    tmpArr[24] := $3;
    tmpArr[25] := $3;
    tmpArr[26] := $3;
    tmpArr[27] := $3;
    tmpArr[28] := $3;


      if CreateSamFile($1D,tmpArr,29) <> 0 then
      exit;

    //Append Record To EF2, Define 8 Key Records in EF2 - Master Keys
    //1st Master key, key ID=81, key type=03, int/ext authenticate, usage counter = FF FF
    tmpArr[0] := strToInt('$' + MidStr(tSAMIC.Text,1,2));
    tmpArr[1] := strToInt('$' + MidStr(tSAMIC.Text,3,2));
    tmpArr[2] := strToInt('$' + MidStr(tSAMIC.Text,5,2));
    tmpArr[3] := strToInt('$' + MidStr(tSAMIC.Text,7,2));
    tmpArr[4] := strToInt('$' + MidStr(tSAMIC.Text,9,2));
    tmpArr[5] := strToInt('$' + MidStr(tSAMIC.Text,11,2));
    tmpArr[6] := strToInt('$' + MidStr(tSAMIC.Text,13,2));
    tmpArr[7] := strToInt('$' + MidStr(tSAMIC.Text,15,2));
    tmpArr[8] := strToInt('$' + MidStr(tSAMIC.Text,17,2));
    tmpArr[9] := strToInt('$' + MidStr(tSAMIC.Text,19,2));
    tmpArr[10] := strToInt('$' + MidStr(tSAMIC.Text,21,2));
    tmpArr[11] := strToInt('$' + MidStr(tSAMIC.Text,23,2));
    tmpArr[12] := strToInt('$' + MidStr(tSAMIC.Text,25,2));
    tmpArr[13] := strToInt('$' + MidStr(tSAMIC.Text,27,2));
    tmpArr[14] := strToInt('$' + MidStr(tSAMIC.Text,29,2));
    tmpArr[15] := strToInt('$' + MidStr(tSAMIC.Text,31,2));


    if AppendSamFile($81, tmpArr, 16) <> 0 then
        Exit;

    //2nd Master key, key ID=82, key type=03, int/ext authenticate, usage counter = FF FF
    tmpArr[0] := strToInt('$' + MidStr(tSAMKc.Text,1,2));
    tmpArr[1] := strToInt('$' + MidStr(tSAMKc.Text,3,2));
    tmpArr[2] := strToInt('$' + MidStr(tSAMKc.Text,5,2));
    tmpArr[3] := strToInt('$' + MidStr(tSAMKc.Text,7,2));
    tmpArr[4] := strToInt('$' + MidStr(tSAMKc.Text,9,2));
    tmpArr[5] := strToInt('$' + MidStr(tSAMKc.Text,11,2));
    tmpArr[6] := strToInt('$' + MidStr(tSAMKc.Text,13,2));
    tmpArr[7] := strToInt('$' + MidStr(tSAMKc.Text,15,2));
    tmpArr[8] := strToInt('$' + MidStr(tSAMKc.Text,17,2));
    tmpArr[9] := strToInt('$' + MidStr(tSAMKc.Text,19,2));
    tmpArr[10] := strToInt('$' + MidStr(tSAMKc.Text,21,2));
    tmpArr[11] := strToInt('$' + MidStr(tSAMKc.Text,23,2));
    tmpArr[12] := strToInt('$' + MidStr(tSAMKc.Text,25,2));
    tmpArr[13] := strToInt('$' + MidStr(tSAMKc.Text,27,2));
    tmpArr[14] := strToInt('$' + MidStr(tSAMKc.Text,29,2));
    tmpArr[15] := strToInt('$' + MidStr(tSAMKc.Text,31,2));

    if AppendSamFile($82, tmpArr, 16) <> 0 then
        Exit;

    //3rd Master key, key ID=83, key type=03, int/ext authenticate, usage counter = FF FF
    tmpArr[0] := strToInt('$' + MidStr(tSAMKt.Text,1,2));
    tmpArr[1] := strToInt('$' + MidStr(tSAMKt.Text,3,2));
    tmpArr[2] := strToInt('$' + MidStr(tSAMKt.Text,5,2));
    tmpArr[3] := strToInt('$' + MidStr(tSAMKt.Text,7,2));
    tmpArr[4] := strToInt('$' + MidStr(tSAMKt.Text,9,2));
    tmpArr[5] := strToInt('$' + MidStr(tSAMKt.Text,11,2));
    tmpArr[6] := strToInt('$' + MidStr(tSAMKt.Text,13,2));
    tmpArr[7] := strToInt('$' + MidStr(tSAMKt.Text,15,2));
    tmpArr[8] := strToInt('$' + MidStr(tSAMKt.Text,17,2));
    tmpArr[9] := strToInt('$' + MidStr(tSAMKt.Text,19,2));
    tmpArr[10] := strToInt('$' + MidStr(tSAMKt.Text,21,2));
    tmpArr[11] := strToInt('$' + MidStr(tSAMKt.Text,23,2));
    tmpArr[12] := strToInt('$' + MidStr(tSAMKt.Text,25,2));
    tmpArr[13] := strToInt('$' + MidStr(tSAMKt.Text,27,2));
    tmpArr[14] := strToInt('$' + MidStr(tSAMKt.Text,29,2));
    tmpArr[15] := strToInt('$' + MidStr(tSAMKt.Text,31,2));

    if AppendSamFile($83, tmpArr, 16) <> 0 then
        Exit;

    //4th Master key, key ID=84, key type=03, int/ext authenticate, usage counter = FF FF
    tmpArr[0] := strToInt('$' + MidStr(tSAMKd.Text,1,2));
    tmpArr[1] := strToInt('$' + MidStr(tSAMKd.Text,3,2));
    tmpArr[2] := strToInt('$' + MidStr(tSAMKd.Text,5,2));
    tmpArr[3] := strToInt('$' + MidStr(tSAMKd.Text,7,2));
    tmpArr[4] := strToInt('$' + MidStr(tSAMKd.Text,9,2));
    tmpArr[5] := strToInt('$' + MidStr(tSAMKd.Text,11,2));
    tmpArr[6] := strToInt('$' + MidStr(tSAMKd.Text,13,2));
    tmpArr[7] := strToInt('$' + MidStr(tSAMKd.Text,15,2));
    tmpArr[8] := strToInt('$' + MidStr(tSAMKd.Text,17,2));
    tmpArr[9] := strToInt('$' + MidStr(tSAMKd.Text,19,2));
    tmpArr[10] := strToInt('$' + MidStr(tSAMKd.Text,21,2));
    tmpArr[11] := strToInt('$' + MidStr(tSAMKd.Text,23,2));
    tmpArr[12] := strToInt('$' + MidStr(tSAMKd.Text,25,2));
    tmpArr[13] := strToInt('$' + MidStr(tSAMKd.Text,27,2));
    tmpArr[14] := strToInt('$' + MidStr(tSAMKd.Text,29,2));
    tmpArr[15] := strToInt('$' + MidStr(tSAMKd.Text,31,2));

    if AppendSamFile($84, tmpArr, 16) <> 0 then
        Exit;

     //5th Master key, key ID=85, key type=03, int/ext authenticate, usage counter = FF FF
    tmpArr[0] := strToInt('$' + MidStr(tSAMKcr.Text,1,2));
    tmpArr[1] := strToInt('$' + MidStr(tSAMKcr.Text,3,2));
    tmpArr[2] := strToInt('$' + MidStr(tSAMKcr.Text,5,2));
    tmpArr[3] := strToInt('$' + MidStr(tSAMKcr.Text,7,2));
    tmpArr[4] := strToInt('$' + MidStr(tSAMKcr.Text,9,2));
    tmpArr[5] := strToInt('$' + MidStr(tSAMKcr.Text,11,2));
    tmpArr[6] := strToInt('$' + MidStr(tSAMKcr.Text,13,2));
    tmpArr[7] := strToInt('$' + MidStr(tSAMKcr.Text,15,2));
    tmpArr[8] := strToInt('$' + MidStr(tSAMKcr.Text,17,2));
    tmpArr[9] := strToInt('$' + MidStr(tSAMKcr.Text,19,2));
    tmpArr[10] := strToInt('$' + MidStr(tSAMKcr.Text,21,2));
    tmpArr[11] := strToInt('$' + MidStr(tSAMKcr.Text,23,2));
    tmpArr[12] := strToInt('$' + MidStr(tSAMKcr.Text,25,2));
    tmpArr[13] := strToInt('$' + MidStr(tSAMKcr.Text,27,2));
    tmpArr[14] := strToInt('$' + MidStr(tSAMKcr.Text,29,2));
    tmpArr[15] := strToInt('$' + MidStr(tSAMKcr.Text,31,2));

    if AppendSamFile($85, tmpArr, 16) <> 0 then
        Exit;

    //'6th Master key, key ID=86, key type=03, int/ext authenticate, usage counter = FF FF
    tmpArr[0] := strToInt('$' + MidStr(tSAMKcf.Text,1,2));
    tmpArr[1] := strToInt('$' + MidStr(tSAMKcf.Text,3,2));
    tmpArr[2] := strToInt('$' + MidStr(tSAMKcf.Text,5,2));
    tmpArr[3] := strToInt('$' + MidStr(tSAMKcf.Text,7,2));
    tmpArr[4] := strToInt('$' + MidStr(tSAMKcf.Text,9,2));
    tmpArr[5] := strToInt('$' + MidStr(tSAMKcf.Text,11,2));
    tmpArr[6] := strToInt('$' + MidStr(tSAMKcf.Text,13,2));
    tmpArr[7] := strToInt('$' + MidStr(tSAMKcf.Text,15,2));
    tmpArr[8] := strToInt('$' + MidStr(tSAMKcf.Text,17,2));
    tmpArr[9] := strToInt('$' + MidStr(tSAMKcf.Text,19,2));
    tmpArr[10] := strToInt('$' + MidStr(tSAMKcf.Text,21,2));
    tmpArr[11] := strToInt('$' + MidStr(tSAMKcf.Text,23,2));
    tmpArr[12] := strToInt('$' + MidStr(tSAMKcf.Text,25,2));
    tmpArr[13] := strToInt('$' + MidStr(tSAMKcf.Text,27,2));
    tmpArr[14] := strToInt('$' + MidStr(tSAMKcf.Text,29,2));
    tmpArr[15] := strToInt('$' + MidStr(tSAMKcf.Text,31,2));

    if AppendSamFile($86, tmpArr, 16) <> 0 then
        Exit;

     //'7th Master key, key ID=87, key type=03, int/ext authenticate, usage counter = FF FF
    tmpArr[0] := strToInt('$' + MidStr(tSAMKrd.Text,1,2));
    tmpArr[1] := strToInt('$' + MidStr(tSAMKrd.Text,3,2));
    tmpArr[2] := strToInt('$' + MidStr(tSAMKrd.Text,5,2));
    tmpArr[3] := strToInt('$' + MidStr(tSAMKrd.Text,7,2));
    tmpArr[4] := strToInt('$' + MidStr(tSAMKrd.Text,9,2));
    tmpArr[5] := strToInt('$' + MidStr(tSAMKrd.Text,11,2));
    tmpArr[6] := strToInt('$' + MidStr(tSAMKrd.Text,13,2));
    tmpArr[7] := strToInt('$' + MidStr(tSAMKrd.Text,15,2));
    tmpArr[8] := strToInt('$' + MidStr(tSAMKrd.Text,17,2));
    tmpArr[9] := strToInt('$' + MidStr(tSAMKrd.Text,19,2));
    tmpArr[10] := strToInt('$' + MidStr(tSAMKrd.Text,21,2));
    tmpArr[11] := strToInt('$' + MidStr(tSAMKrd.Text,23,2));
    tmpArr[12] := strToInt('$' + MidStr(tSAMKrd.Text,25,2));
    tmpArr[13] := strToInt('$' + MidStr(tSAMKrd.Text,27,2));
    tmpArr[14] := strToInt('$' + MidStr(tSAMKrd.Text,29,2));
    tmpArr[15] := strToInt('$' + MidStr(tSAMKrd.Text,31,2));

    if AppendSamFile($87, tmpArr, 16) <> 0 then
        Exit;
end;

procedure TfrmMain.Button1Click(Sender: TObject);
var tmpstr : String;
    tmpstr2 : String;
    indx : integer;
    ArrIndx2 : integer;
    tmpArr : array [0..42] of byte;
    tmpArr2 : array [0..42] of byte;
begin

    //Select Issuer DF ************************************************
    ClearBuffers();
    G_SendBuff[0] := $00;
    G_SendBuff[1] := $A4;
    G_SendBuff[2] := $00;
    G_SendBuff[3] := $00;
    G_SendBuff[4] := $2;
    G_SendBuff[5] := $11;
    G_SendBuff[6] := $00;

    tmpstr := '';
    for indx := 0 to 6 do
      tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    G_RecvLen := 2;
    G_SendLen := 7;
    if SendAPDUSAM(G_SendBuff, G_SendLen, G_RecvLen, G_RecvBuff) = True then
      begin
        tmpstr := '';
          for indx := (G_RecvLen-2) to (G_RecvLen-1) do
            tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);
            if (Trim(tmpStr) <> '61 2D') then
              begin
                DisplayOut(1, 0, Trim(tmpStr), 'SAM');
                exit;
              end;
      end;

    //Submit Issuer PIN *************************************************
    ClearBuffers();
    G_SendBuff[0] := $00;
    G_SendBuff[1] := $20;
    G_SendBuff[2] := $00;
    G_SendBuff[3] := $1;
    G_SendBuff[4] := $8;

    indx := 1;        //Array of textbox increments by 2 characters
    ArrIndx2 := 5;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(tSAMGPIN.Text))/2) + 4 do begin
      tmpstr2 := MidStr(tSAMGPIN.Text,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;


    tmpstr := '';
    for indx := 0 to 12 do
      tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    G_RecvLen := 2;
    G_SendLen := 13;
    if SendAPDUSAM(G_SendBuff, G_SendLen, G_RecvLen, G_RecvBuff) = True then
      begin
        tmpstr := '';
          for indx := (G_RecvLen-2) to (G_RecvLen-1) do
            tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

            if (Trim(tmpStr) <> '90 00') then
              begin
                  DisplayOut(1, 0, Trim(tmpStr), 'SAM');
                  exit;
              end;
      end;

    //Get Card Serial Number *******************************************
    //Select FF00 ******************************************************
    tmpstr := '';
    if SelectFile($FF, $00) <> 0 then
      begin
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

        DisplayOut(1, 0, Trim(tmpStr), 'MCU');
        exit;
      end;


    //Read FF 00 to retrieve card serial number
    tmpstr := '';
    if readRecord(0, 8) <> 0 then
      begin
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

        DisplayOut(1, 0, Trim(tmpStr), 'MCU');
        exit;
      end
    else begin
      for indx := 0 to 7 do
        tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

      tACOSCardSn.Text := tmpstr;
    end;

    //Normal tmpArr
    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 0;    //Array index increment by 1 only
    while ArrIndx2 <= (Length(tACOSCardSN.Text)/2) - 5 do begin
      tmpstr2 := MidStr(tACOSCardSN.Text,indx,2);
      tmpArr[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 3;
    end;

    //Xor tmpArr (tmpArr2)
    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 0;    //Array index increment by 1 only
    while ArrIndx2 <= (Length(tACOSCardSN.Text)/2) - 5 do begin
      tmpstr2 := MidStr(tACOSCardSN.Text,indx,2);
      tmpArr2[ArrIndx2] := (strToInt('$' + tmpstr2) Xor $FF);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 3;
    end;

    //Generate Key ***********************************************************
    //Generate IC Using 1st SAM Master Key (KeyID=81) ************************
    if GenerateSAMKey($81, tmpArr, 8) <> 0 then
      exit;

    //Get Response to Retrieve Generated IC Key***********************
     tACOSIC.Text := GetSAMResponse;

    if tACOSIC.text = '' then
      exit
    else begin
      tmpStr := '';
      for indx := (G_RecvLen-2) to (G_RecvLen-1) do
        tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

      DisplayOut(3, 0, Trim(tmpStr), 'SAM');
    end;


    //'Generate Card Key Using 2nd SAM Master Key (KeyID=82)
    if GenerateSAMKey($82, tmpArr, 8) <> 0 then
      exit;

    //Get Response to Retrieve Generated Key (Card Key)**********************
    tACOSKc.Text := GetSAMResponse;

    if tACOSKc.text = '' then
      exit
    else begin
      tmpStr := '';
      for indx := (G_RecvLen-2) to (G_RecvLen-1) do
        tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

      DisplayOut(3, 0, Trim(tmpStr), 'SAM');
    end;

    //'If Algorithm Reference = 3DES
    //then Generate Right Half of Card Key (Kc) Using 2nd SAM Master Key (KeyID=82)
    if G_AlgoRef = 0 then
      begin

        if GenerateSAMKey($82, tmpArr2, 8) <> 0 then
          exit;

        //Get Response to Retrieve 3DES
        //Generated Key (Card Key)**********************
        tACOSKcRyt.Text := GetSAMResponse;

        if tACOSKcRyt.text = '' then
          exit
        else begin
          tmpStr := '';
          for indx := (G_RecvLen-2) to (G_RecvLen-1) do
            tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

          DisplayOut(3, 0, Trim(tmpStr), 'SAM');
        end;
      end
    else
        tACOSKcRyt.Text := '';

     // 'Generate Terminal Key (Kt) Using 3rd SAM Master Key (KeyID=83)**********
    if GenerateSAMKey($83, tmpArr, 8) <> 0 then
      exit;


    //Get Response to Retrieve Generated Key (Terminal Key)**********************
    tACOSKt.Text := GetSAMResponse;

    if tACOSKt.text = '' then
      exit
    else begin
      tmpStr := '';
      for indx := (G_RecvLen-2) to (G_RecvLen-1) do
        tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

      DisplayOut(3, 0, Trim(tmpStr), 'SAM');
    end;

    //'If Algorithm Reference = 3DES
    //then Generate Right Half of Terminal Key (Kt) Using 3rd SAM Master Key (KeyID=83)
    if G_AlgoRef = 0 then
      begin

        if GenerateSAMKey($83, tmpArr2, 8) <> 0 then
          exit;

        //Get Response to Retrieve 3DES
        //Generated Key (Terminal Key)**********************
        tACOSKtRyt.Text := GetSAMResponse;

        if tACOSKtRyt.text = '' then
          exit
        else begin
          tmpStr := '';
          for indx := (G_RecvLen-2) to (G_RecvLen-1) do
            tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

          DisplayOut(3, 0, Trim(tmpStr), 'SAM');
        end;
      end
    else
        tACOSKtRyt.Text := '';


    //Generate Debit Key (Kd) Using 4th SAM Master Key (KeyID=84)
    if GenerateSAMKey($84, tmpArr, 8) <> 0 then
      exit;

    //Get Response to Retrieve Generated Key (Debit Key)**********************
    tACOSKd.Text := GetSAMResponse;

    if tACOSKd.text = '' then
      exit
    else begin
      tmpStr := '';
      for indx := (G_RecvLen-2) to (G_RecvLen-1) do
        tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);
        DisplayOut(3, 0, Trim(tmpStr), 'SAM');
    end;

    //'If Algorithm Reference = 3DES
    //then Generate Right Half of Debit Key (Kd) Using 4th SAM Master Key (KeyID=84)
    if G_AlgoRef = 0 then
      begin
        if GenerateSAMKey($84, tmpArr2, 8) <> 0 then
          exit;

        //Get Response to Retrieve 3DES
        //Generated Key (Debit Key)**********************
        tACOSKdRyt.Text := GetSAMResponse;

        if tACOSKdRyt.text = '' then
          exit
        else begin
          tmpStr := '';
          for indx := (G_RecvLen-2) to (G_RecvLen-1) do
            tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

          DisplayOut(3, 0, Trim(tmpStr), 'SAM');
        end;
      end
    else
        tACOSKdRyt.Text := '';

    //'Generate Credit Key (Kcr) Using 5th SAM Master Key (KeyID=85)
    if GenerateSAMKey($85, tmpArr, 8) <> 0 then
      exit;

    //Get Response to Retrieve Generated Key (Credit Key)**********************
    tACOSKcr.Text := GetSAMResponse;

    if tACOSKcr.text = '' then
      exit
    else begin
      tmpStr := '';
      for indx := (G_RecvLen-2) to (G_RecvLen-1) do
        tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

      DisplayOut(3, 0, Trim(tmpStr), 'SAM');
    end;

    //'If Algorithm Reference = 3DES
    //then Generate Right Half of Credit Key (Kcr) Using 5th SAM Master Key (KeyID=85)
    if G_AlgoRef = 0 then
      begin
        if GenerateSAMKey($85, tmpArr2, 8) <> 0 then
          exit;

        //Get Response to Retrieve 3DES
        //Generated Key (Credit Key)**********************
          tACOSKcrRyt.Text := GetSAMResponse;

        if tACOSKcrRyt.text = '' then
          exit
        else begin
          tmpStr := '';
          for indx := (G_RecvLen-2) to (G_RecvLen-1) do
            tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

            DisplayOut(3, 0, Trim(tmpStr), 'SAM');
        end;
      end
    else
        tACOSKcrRyt.Text := '';


    //'Generate Certify Key (Kcf) Using 6th SAM Master Key (KeyID=86)
    if GenerateSAMKey($86, tmpArr, 8) <> 0 then
      exit;

    //Get Response to Retrieve Generated Key (Certify Key)**********************
    tACOSKcf.Text := GetSAMResponse;

    if tACOSKcf.text = '' then
      exit
    else begin
      tmpStr := '';
      for indx := (G_RecvLen-2) to (G_RecvLen-1) do
        tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

      DisplayOut(3, 0, Trim(tmpStr), 'SAM');
    end;

   //'If Algorithm Reference = 3DES
    //then Generate Right Half of Certify Key (Kcf) Using 6th SAM Master Key (KeyID=86)
    if G_AlgoRef = 0 then
      begin
        if GenerateSAMKey($86, tmpArr2, 8) <> 0 then
          exit;

        //Get Response to Retrieve 3DES
        //Generated Key (Certify Key)**********************
        tACOSKcfRyt.Text := GetSAMResponse;

        if tACOSKcfRyt.text = '' then
          exit
        else begin
          tmpStr := '';

          for indx := (G_RecvLen-2) to (G_RecvLen-1) do
            tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

          DisplayOut(3, 0, Trim(tmpStr), 'SAM');
        end;
      end
    else
        tACOSKcfRyt.Text := '';

   //'Generate Revoke Debit Key (Krd) Using 7th SAM Master Key (KeyID=87)
    if GenerateSAMKey($87, tmpArr, 8) <> 0 then
      exit;

    //Get Response to Retrieve Generated Key (Revoke Debit Key)**********************
    tACOSKrd.Text := GetSAMResponse;

    if tACOSKrd.text = '' then
      exit
    else begin
      tmpStr := '';
      for indx := (G_RecvLen-2) to (G_RecvLen-1) do
        tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

      DisplayOut(3, 0, Trim(tmpStr), 'SAM');
    end;

    //'If Algorithm Reference = 3DES
    //then Generate Right Half of Revoke Debit Key (Krd) Using 7th SAM Master Key (KeyID=87)
    if G_AlgoRef = 0 then
      begin
        if GenerateSAMKey($87, tmpArr2, 8) <> 0 then
          exit;

        //Get Response to Retrieve 3DES
        //Generated Key (Revoke Debit Key)**********************
        tACOSKrdRyt.Text := GetSAMResponse;

        if tACOSKrdRyt.text = '' then
          exit
        else begin
          tmpStr := '';
          for indx := (G_RecvLen-2) to (G_RecvLen-1) do
            tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

          DisplayOut(3, 0, Trim(tmpStr), 'SAM');
        end;
      end
    else
        tACOSKrdRyt.Text := '';
end;

procedure TfrmMain.Button2Click(Sender: TObject);
var tmpstr : string;
    tmpstr2 : string;
    indx : integer;
    ArrIndx2 : integer;
    tmpArr : array [0..15] of byte;
    retcode : integer;
    AccLoc : byte;
begin
   if length(tACOSPIN.Text) < tACOSPIN.MaxLength then
      begin
       DisplayOut(0, 0, 'Invalid ACOS Pin Entry', 'SAM');
       tACOSPIN.SetFocus;
       exit;
      end;

   if length(tACOSCardSN.Text) <= 0 then
      begin
        DisplayOut(0, 0, 'Invalid ACOS Card SN Entry', 'MCU');
       tACOSCardSN.SetFocus;
       exit;
      end;

   if length(tACOSIC.Text) <= 0 then
      begin
        DisplayOut(0, 0, 'Invalid ACOS IC Entry', 'MCU');
       tACOSIC.SetFocus;
       exit;
      end;

   if length(tACOSKc.Text) <= 0 then
      begin
        DisplayOut(0, 0, 'Invalid ACOS Kc Entry', 'MCU');
       tACOSKc.SetFocus;
       exit;
      end;

   if length(tACOSKt.Text) <= 0 then
      begin
        DisplayOut(0, 0, 'Invalid ACOS Kt Entry', 'MCU');
       tACOSKt.SetFocus;
       exit;
      end;

   if length(tACOSKd.Text) <= 0 then
      begin
        DisplayOut(0, 0, 'Invalid ACOS Kd Entry', 'MCU');
       tACOSKd.SetFocus;
       exit;
      end;

   if length(tACOSKcr.Text) <= 0 then
      begin
        DisplayOut(0, 0, 'Invalid ACOS Kcr Entry', 'MCU');
       tACOSKcr.SetFocus;
       exit;
      end;

   if length(tACOSKcf.Text) <= 0 then
      begin
        DisplayOut(0, 0, 'Invalid ACOS Kcf Entry', 'MCU');
       tACOSKcf.SetFocus;
       exit;
      end;

   if length(tACOSKrd.Text) <= 0 then
      begin
        DisplayOut(0, 0, 'Invalid ACOS Krd Entry', 'MCU');
       tACOSKrd.SetFocus;
       exit;
      end;

   if G_AlgoRef = 0 then begin
      if length(tACOSKcRyt.Text) <= 0 then
        begin
          DisplayOut(0, 0, 'Invalid ACOS Kc Right', 'MCU');
          tACOSKcRyt.SetFocus;
          exit;
        end;

      if length(tACOSKtRyt.Text) <= 0 then
        begin
          DisplayOut(0, 0, 'Invalid ACOS Kt Right', 'MCU');
          tACOSKtRyt.SetFocus;
          exit;
        end;

      if length(tACOSKdRyt.Text) <= 0 then
        begin
          DisplayOut(0, 0, 'Invalid ACOS Kd Right', 'MCU');
          tACOSKdRyt.SetFocus;
          exit;
        end;

      if length(tACOSKcfRyt.Text) <= 0 then
        begin
          DisplayOut(0, 0, 'Invalid ACOS Kcf Right', 'MCU');
          tACOSKcfRyt.SetFocus;
          exit;
        end;

      if length(tACOSKcrRyt.Text) <= 0 then
        begin
          DisplayOut(0, 0, 'Invalid ACOS Kcr Right', 'MCU');
          tACOSKtRyt.SetFocus;
          exit;
        end;
      if length(tACOSKrdRyt.Text) <= 0 then
        begin
          DisplayOut(0, 0, 'Invalid ACOS Krd Right', 'MCU');
          tACOSKtRyt.SetFocus;
          exit;
        end;
     end;

  //'Update Personalization File (FF02)*****************************

  //'Select FF02****************************************************
  tmpstr := '';
  retcode := SelectFile($FF, $02);
  if retcode <> 0 then begin
    for indx := (G_RecvLen-2) to (G_RecvLen-1) do
      tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

      DisplayOut(1, 0, Trim(tmpStr), 'MCU');
      exit;
    end;

  //Submit Default IC************************************************
  retcode := SubmitIC();
  if retcode <> SCARD_S_SUCCESS then
    begin
      for indx := (G_RecvLen-2) to (G_RecvLen-1) do
        tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

      DisplayOut(1, 0, Trim(tmpStr), 'MCU');

      exit;
    end;

  //'Update FF02 record 0 *******************************************
   if G_AlgoRef = 0 then
      tmpArr[0] := $FF
   else
      tmpArr[0] := $FD;

   tmpArr[1] := $40;  //Security Option Register..
   tmpArr[2] := $0;
   tmpArr[3] := $0;

   retcode := writeRecord(0, $00, $4, $4, tmpArr);
   if retcode <> 0 then
    exit;


   //Reset
   ResetMCU();
   DisplayOut(0, 0, 'Closing connection to ' + cmbACOSReader.Text, 'SAM');

   //Reconnection to ACOS card
   DisplayOut(2, 0, 'Invoke SCardConnect', 'ACOS');
    // 1. Connect to selected reader using hContext handle
    //    and obtain valid hCard handle
    G_retCode := SCardConnectA(G_hContext,
                           PChar(cmbACOSReader.Text),
                           SCARD_SHARE_SHARED,
                           SCARD_PROTOCOL_T0 or SCARD_PROTOCOL_T1,
                           @G_hCard,
                           @G_dwActProtocol);
    if G_retCode <> SCARD_S_SUCCESS then
      begin
        DisplayOut(1, G_retCode, '', 'ACOS');
        G_ConnActive := False;
        Exit;
      end
  else
    DisplayOut(0, 0, 'Successful re-connection to ' + cmbACOSReader.Text, 'ACOS');


    //'Update Card Keys (FF03) Security File **************************
    //'Select FF03  ***************************************************
    tmpstr := '';
    retcode := SelectFile($FF, $03);
    if retcode <> 0 then
      begin
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

        DisplayOut(1, 0, Trim(tmpStr), 'MCU');
        exit;
      end;

    //Submit Default IC************************************************
    retcode := SubmitIC();
    if retcode <> SCARD_S_SUCCESS then
      begin
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

        DisplayOut(1, 0, Trim(tmpStr), 'MCU');
        exit;
      end;

    //'Update FF03 record 0 (IC)
    indx := 1;        //Array of textbox increments by 2 characters
    ArrIndx2 := 0;    //Array index increment by 1 only
    //while ArrIndx2 <= ((Lentmpstr)/2) - 1 do begin
    while ArrIndx2 <= (Length(tACOSIC.Text)/2) -1 do begin
      tmpstr2 := MidStr(tACOSIC.Text,indx,2);
      tmpArr[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    retcode := writeRecord(0, $0, $8, $8, tmpArr);
    if retcode <> 0 then
      exit;

    //'Update FF03 record 1 (PIN) ******************************************
    indx := 1;        //Array of textbox increments by 2 characters
    ArrIndx2 := 0;    //Array index increment by 1 only
    while ArrIndx2 <= (Length(tACOSPIN.Text)/2) -1 do begin
      tmpstr2 := MidStr(tACOSPIN.Text,indx,2);
      tmpArr[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    retcode := writeRecord(0, $1, $8, $8, tmpArr);
    if retcode <> 0 then
      exit;


    //'Update FF03 record 2 (Kc) ******************************************
    indx := 1;        //Array of textbox increments by 2 characters
    ArrIndx2 := 0;    //Array index increment by 1 only
    //while ArrIndx2 <= ((Lentmpstr)/2) - 1 do begin
    while ArrIndx2 <= (Length(tACOSKc.Text)/2) -1 do begin
      tmpstr2 := MidStr(tACOSKc.Text,indx,2);
      tmpArr[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    retcode := writeRecord(0, $2, $8, $8, tmpArr);
    if retcode <> 0 then
      exit;

    //'If Algorithm Reference = 3DES
    //Update FF03 record 0x0C Right Half (Kc)
    if G_AlgoRef = 0 then
      begin
        indx := 1;        //Array of textbox increments by 2 characters
        ArrIndx2 := 0;    //Array index increment by 1 only
        while ArrIndx2 <= (Length(tACOSKcRyt.Text)/2) -1 do begin
          tmpstr2 := MidStr(tACOSKcRyt.Text,indx,2);
          tmpArr[ArrIndx2] := strToInt('$' + tmpstr2);
          ArrIndx2 := ArrIndx2 + 1;
          indx := indx + 2;
        end;

        retcode := writeRecord(0, $C, $8, $8, tmpArr);
        if retcode <> 0 then
          exit;
    end;

    // 'Update FF03 record 3 (Kt)
    indx := 1;        //Array of textbox increments by 2 characters
    ArrIndx2 := 0;    //Array index increment by 1 only
    while ArrIndx2 <= (Length(tACOSKt.Text)/2) -1 do begin
      tmpstr2 := MidStr(tACOSKt.Text,indx,2);
      tmpArr[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    retcode := writeRecord(0, $3, $8, $8, tmpArr);
    if retcode <> 0 then
      exit;

    //'If Algorithm Reference = 3DES
    //Update FF03 record 0x0D Right Half (Kt)
    if G_AlgoRef = 0 then
      begin
        indx := 1;        //Array of textbox increments by 2 characters
        ArrIndx2 := 0;    //Array index increment by 1 only
        while ArrIndx2 <= (Length(tACOSKtRyt.Text)/2) -1 do begin
          tmpstr2 := MidStr(tACOSKtRyt.Text,indx,2);
          tmpArr[ArrIndx2] := strToInt('$' + tmpstr2);
          ArrIndx2 := ArrIndx2 + 1;
          indx := indx + 2;
        end;

        retcode := writeRecord(0, $D, $8, $8, tmpArr);
        if retcode <> 0 then
          exit;
    end;

    //'Select FF06 (Account Security File) ******************************
    tmpstr := '';
    retcode := SelectFile($FF, $06);
    if retcode <> 0 then
      begin
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

        DisplayOut(1, 0, Trim(tmpStr), 'MCU');
        exit;
    end;

    //'Update FF06 record 0 (Kd) *****************************************
    indx := 1;        //Array of textbox increments by 2 characters
    ArrIndx2 := 0;    //Array index increment by 1 only
    while ArrIndx2 <= (Length(tACOSKd.Text)/2) -1 do begin
      tmpstr2 := MidStr(tACOSKd.Text,indx,2);
      tmpArr[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    if G_AlgoRef = 0 then
      AccLoc := $4
    else
      AccLoc := $0;

    retcode := writeRecord(0, AccLoc, $8, $8, tmpArr);
    if retcode <> 0 then
      exit;

    //'Update FF06 record 1 (Kcr) ***************************************
    indx := 1;        //Array of textbox increments by 2 characters
    ArrIndx2 := 0;    //Array index increment by 1 only
    while ArrIndx2 <= (Length(tACOSKcr.Text)/2) -1 do begin
      tmpstr2 := MidStr(tACOSKcr.Text,indx,2);
      tmpArr[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    if G_AlgoRef = 0 then
      AccLoc := $5
    else
      AccLoc := $1;

    retcode := writeRecord(0, AccLoc, $8, $8, tmpArr);
    if retcode <> 0 then
      exit;

    //'Update FF06 record 2 (Kcf)*****************************************
    indx := 1;        //Array of textbox increments by 2 characters
    ArrIndx2 := 0;    //Array index increment by 1 only
    while ArrIndx2 <= (Length(tACOSKcf.Text)/2) - 1 do begin
      tmpstr2 := MidStr(tACOSKcf.Text,indx,2);
      tmpArr[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    if G_AlgoRef = 0 then
      AccLoc := $6
    else
      AccLoc := $2;

    retcode := writeRecord(0, AccLoc, $8, $8, tmpArr);
    if retcode <> 0 then
      exit;

    //'Update FF06 record 3 (Krd)*****************************************
    indx := 1;        //Array of textbox increments by 2 characters
    ArrIndx2 := 0;    //Array index increment by 1 only
    while ArrIndx2 <= (Length(tACOSKrd.Text)/2) - 1 do begin
      tmpstr2 := MidStr(tACOSKrd.Text,indx,2);
      tmpArr[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    if G_AlgoRef = 0 then
      AccLoc := $7
    else
      AccLoc := $3;

    retcode := writeRecord(0, AccLoc, $8, $8, tmpArr);
    if retcode <> 0 then
      exit;

    //'If Algorithm Reference = 3DES
    //then update Right Half of the Keys
    if G_AlgoRef = 0 then
      begin
        //'Update FF06 record 0 (Kd) *****************************************
        indx := 1;        //Array of textbox increments by 2 characters
        ArrIndx2 := 0;    //Array index increment by 1 only
        while ArrIndx2 <= (Length(tACOSKdRyt.Text)/2) - 1 do begin
          tmpstr2 := MidStr(tACOSKdRyt.Text,indx,2);
          tmpArr[ArrIndx2] := strToInt('$' + tmpstr2);
          ArrIndx2 := ArrIndx2 + 1;
          indx := indx + 2;
        end;

        retcode := writeRecord(0, $0, $8, $8, tmpArr);
        if retcode <> 0 then
          exit;

        //'Update FF06 record 1 (Kcr) *****************************************
        indx := 1;        //Array of textbox increments by 2 characters
        ArrIndx2 := 0;    //Array index increment by 1 only
        while ArrIndx2 <= (Length(tACOSKcrRyt.Text)/2) - 1 do begin
          tmpstr2 := MidStr(tACOSKcrRyt.Text,indx,2);
          tmpArr[ArrIndx2] := strToInt('$' + tmpstr2);
          ArrIndx2 := ArrIndx2 + 1;
          indx := indx + 2;
        end;

        retcode := writeRecord(0, $1, $8, $8, tmpArr);
        if retcode <> 0 then
          exit;

        //'Update FF06 record 2 (Kcf) *****************************************
        indx := 1;        //Array of textbox increments by 2 characters
        ArrIndx2 := 0;    //Array index increment by 1 only
        while ArrIndx2 <= (Length(tACOSKcfRyt.Text)/2) - 1 do begin
          tmpstr2 := MidStr(tACOSKcfRyt.Text,indx,2);
          tmpArr[ArrIndx2] := strToInt('$' + tmpstr2);
          ArrIndx2 := ArrIndx2 + 1;
          indx := indx + 2;
        end;

        retcode := writeRecord(0, $2, $8, $8, tmpArr);
        if retcode <> 0 then
          exit;

        //'Update FF06 record 3 (Krd) *****************************************
        indx := 1;        //Array of textbox increments by 2 characters
        ArrIndx2 := 0;    //Array index increment by 1 only
        while ArrIndx2 <= (Length(tACOSKrdRyt.Text)/2) - 1 do begin
          tmpstr2 := MidStr(tACOSKrdRyt.Text,indx,2);
          tmpArr[ArrIndx2] := strToInt('$' + tmpstr2);
          ArrIndx2 := ArrIndx2 + 1;
          indx := indx + 2;
        end;

        retcode := writeRecord(0, $3, $8, $8, tmpArr);
        if retcode <> 0 then
          exit;

  end;

   //'Select FF05 (Account File) ******************************
  tmpstr := '';
  retcode := SelectFile($FF, $05);
  if retcode <> 0 then
    begin
      for indx := (G_RecvLen-2) to (G_RecvLen-1) do
        tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

      DisplayOut(1, 0, Trim(tmpStr), 'MCU');
      exit;
    end;

    //'Initialize FF05 Account File*******************************************
   //Initialize Record 0 of Account File **************************************
   tmpArr[0] := $00;
   tmpArr[1] := $00;
   tmpArr[2] := $00;
   tmpArr[3] := $00;

   retcode := writeRecord(0, $00, $4, $4, tmpArr);
   if retcode <> 0 then
    exit;

   //Initialize Record 1 of Account File **************************************
   tmpArr[0] := $00;
   tmpArr[1] := $00;
   tmpArr[2] := $01;
   tmpArr[3] := $00;

   retcode := writeRecord(0, $01, $4, $4, tmpArr);
   if retcode <> 0 then
    exit;

   //Initialize Record 2 of Account File **************************************
   tmpArr[0] := $00;
   tmpArr[1] := $00;
   tmpArr[2] := $00;
   tmpArr[3] := $00;

   retcode := writeRecord(0, $02, $4, $4, tmpArr);
   if retcode <> 0 then
    exit;

   //Initialize Record 3 of Account File **************************************
   tmpArr[0] := $00;
   tmpArr[1] := $00;
   tmpArr[2] := $01;
   tmpArr[3] := $00;

   retcode := writeRecord(0, $03, $4, $4, tmpArr);
   if retcode <> 0 then
    exit;

   //Initialize Record 4 of Account File **************************************
   //'Set Max Balance to 98 96 7F = 9,999,999
   tmpArr[0] := $98;
   tmpArr[1] := $96;
   tmpArr[2] := $7F;
   tmpArr[3] := $00;

   retcode := writeRecord(0, $04, $4, $4, tmpArr);
   if retcode <> 0 then
    exit;

   //Initialize Record 5 of Account File **************************************
   tmpArr[0] := $00;
   tmpArr[1] := $00;
   tmpArr[2] := $00;
   tmpArr[3] := $00;

   retcode := writeRecord(0, $05, $4, $4, tmpArr);
   if retcode <> 0 then
    exit;

   //Initialize Record 6 of Account File **************************************
   tmpArr[0] := $00;
   tmpArr[1] := $00;
   tmpArr[2] := $00;
   tmpArr[3] := $00;

   retcode := writeRecord(0, $06, $4, $4, tmpArr);
   if retcode <> 0 then
    exit;

   //Initialize Record 7 of Account File **************************************
   tmpArr[0] := $00;
   tmpArr[1] := $00;
   tmpArr[2] := $00;
   tmpArr[3] := $00;

   retcode := writeRecord(0, $07, $4, $4, tmpArr);
   if retcode <> 0 then
    exit;

end;

procedure TfrmMain.tSAMGPINKeyPress(Sender: TObject; var Key: Char);
begin
  if Key in ['a'..'z'] then Dec(Key,32);
  if Not(Key in ['0'..'9', 'A'..'F', chr(8), chr(32)]) then
    Key := #0;
end;

procedure TfrmMain.tSAMICKeyPress(Sender: TObject; var Key: Char);
begin
    if Key in ['a'..'z'] then Dec(Key,32);
    if Not(Key in ['0'..'9', 'A'..'F', chr(8), chr(32)]) then
      Key := #0;
end;

procedure TfrmMain.tSAMKcKeyPress(Sender: TObject; var Key: Char);
begin
  if Key in ['a'..'z'] then Dec(Key,32);
    if Not(Key in ['0'..'9', 'A'..'F', chr(8), chr(32)]) then
      Key := #0;

end;

procedure TfrmMain.tSAMKtKeyPress(Sender: TObject; var Key: Char);
begin
    if Key in ['a'..'z'] then Dec(Key,32);
    if Not(Key in ['0'..'9', 'A'..'F', chr(8), chr(32)]) then
      Key := #0;

end;

procedure TfrmMain.tSAMKdKeyPress(Sender: TObject; var Key: Char);
begin
    if Key in ['a'..'z'] then Dec(Key,32);
    if Not(Key in ['0'..'9', 'A'..'F', chr(8), chr(32)]) then
      Key := #0;

end;

procedure TfrmMain.tSAMKcrKeyPress(Sender: TObject; var Key: Char);
begin
  if Key in ['a'..'z'] then Dec(Key,32);
    if Not(Key in ['0'..'9', 'A'..'F', chr(8), chr(32)]) then
      Key := #0;

end;

procedure TfrmMain.tSAMKcfKeyPress(Sender: TObject; var Key: Char);
begin
    if Key in ['a'..'z'] then Dec(Key,32);
    if Not(Key in ['0'..'9', 'A'..'F', chr(8), chr(32)]) then
      Key := #0;

end;

procedure TfrmMain.tSAMKrdKeyPress(Sender: TObject; var Key: Char);
begin
  if Key in ['a'..'z'] then Dec(Key,32);
    if Not(Key in ['0'..'9', 'A'..'F', chr(8), chr(32)]) then
      Key := #0;

end;


procedure TfrmMain.tACOSPINKeyPress(Sender: TObject; var Key: Char);
begin
  if Key in ['a'..'z'] then Dec(Key,32);
    if Not(Key in ['0'..'9', 'A'..'F', chr(8), chr(32)]) then
      Key := #0;
end;

procedure TfrmMain.btnResetClick(Sender: TObject);
begin

ResetMCU();
ResetSAM();

//Clear SAM Tab
cmbSAMReader.Clear;

tSAMGPIN.Text := '';
tSAMIC.Text := '';
tSAMKc.Text := '';
tSAMKt.Text := '';
tSAMKd.Text := '';
tSAMKcr.Text := '';
tSAMKcf.Text := '';
tSAMKrd.Text := '';

tSAMGPin.Enabled := False;
tSAMIC.Enabled := False;
tSAMKc.Enabled := False;
tSAMKt.Enabled := False;
tSAMKd.Enabled := False;
tSAMKcr.Enabled := False;
tSAMKcf.Enabled := False;
tSAMKrd.Enabled := False;
btnInitSAM.Enabled := False;

//Clear ACOS Card Tab
cmbACOSReader.Clear;
rbDes.Checked := True;

tACOSCardSN.Text := '';
tACOSPIN.Text := '';
tACOSIC.Text := '';
tACOSKc.Text := '';
tACOSKcRyt.Text := '';
tACOSKt.Text := '';
tACOSKtRyt.Text := '';
tACOSKd.Text := '';
tACOSKdRyt.Text := '';
tACOSKcr.Text := '';
tACOSKcrRyt.Text := '';
tACOSKcf.Text := '';
tACOSKcfRyt.Text := '';
tACOSKrd.Text := '';
tACOSKrdRyt.Text := '';

tACOSCardSN.Enabled := False;
tACOSPIN.Enabled := False;
tACOSIC.Enabled := False;
tACOSKc.Enabled := False;
tACOSKcRyt.Enabled := False;
tACOSKt.Enabled := False;
tACOSKtRyt.Enabled := False;
tACOSKd.Enabled := False;
tACOSKdRyt.Enabled := False;
tACOSKcr.Enabled := False;
tACOSKcrRyt.Enabled := False;
tACOSKcf.Enabled := False;
tACOSKcfRyt.Enabled := False;
tACOSKrd.Enabled := False;
tACOSKrdRyt.Enabled := False;

//Final Initialization
mMsg.Clear;
cmbSAMReader.SetFocus;

end;

procedure TfrmMain.FormActivate(Sender: TObject);
begin
  InitMenu();
end;

end.
