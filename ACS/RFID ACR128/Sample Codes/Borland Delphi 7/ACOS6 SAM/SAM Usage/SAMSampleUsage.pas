///////////////////////////////////////////////////////////////////////////////
//
// FORM NAME : Key Management Sample
//
// COMPANY : ADVANDCED CARD SYSTEMS, LTD
//
// AUTHOR : MALCOLM BERNARD U. SOLAÑA
//
// DATE :  01 / 30 / 2007
//
//
// Description : This tests the Keys and Account initial settings set by
//              the Key Management program. 
//
//'   Initial Step :  1.  Press List Readers.
//'                   2.  Choose the SAM reader and ACOS card reader.
//'                   3.  Press Connect.
//'                   4.  Select Algorithm Reference to use (DES/3DES)
//'                   5.  Enter SAM Global PIN. (PIN used in KeyManagement sample, SAM Initialization)
//'                   6.  Press Mutual Authentication.
//'                   7.  Enter ACOS Card PIN. (PIN used in KeyManagement sample, ACOS Card Initialization)
//'                   8.  Press Submit PIN.
//'                   9.  If you don't want to change your current PIN go to step 10.
//'                       To changed current PIN, enter the desired new PIN and press Change PIN
//'                   10. To check current card balance (e-purse) press Inquire Account.
//'                   11. To credit amount to the card e-purse enter the amount to credit and press Credit.
//'                   12. To dedit amount to the card e-purse enter the amount to dedit and press Dedit.
//'
//'   NOTE:
//'                   Please note that this sample program assumes that the SAM and ACOS card were already
//'                   initialized using KeyManagement Sample program.
//
// Revision Trail: (Date/Author/Description)
///////////////////////////////////////////////////////////////////////////////

unit SAMSampleUsage;
interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, TabNotBk, ACSModule, StrUtils;

  
Const MAX_BUFFER_LEN    = 256;
Const INVALID_SW1SW2    = -450;

type
  TfrmMain = class(TForm)
    grpSAM: TTabbedNotebook;
    GroupBox1: TGroupBox;
    btnListReaders: TButton;
    cmbCardReader: TComboBox;
    cmbSAMReader: TComboBox;
    lblCardReader: TLabel;
    Label1: TLabel;
    btnConnect: TButton;
    rbDES: TRadioButton;
    rb3DES: TRadioButton;
    Label2: TLabel;
    tSAMGPIN: TEdit;
    btnMA: TButton;
    tACOSPIN: TEdit;
    Label3: TLabel;
    btnSubmit: TButton;
    tACOSNewPIN: TEdit;
    Label4: TLabel;
    btnChangePIN: TButton;
    mMsg: TRichEdit;
    grpAccount: TGroupBox;
    lblMaxBalance: TLabel;
    tMaxBal: TEdit;
    lblBalance: TLabel;
    tBal: TEdit;
    btnInquireAccount: TButton;
    lblCreditAmt: TLabel;
    tCreditAmt: TEdit;
    bCredit: TButton;
    lblDebitAmt: TLabel;
    tDebitAmt: TEdit;
    btnDebitAmt: TButton;

    procedure btnListReadersClick(Sender: TObject);
    procedure btnConnectClick(Sender: TObject);
    procedure tSAMGPINKeyPress(Sender: TObject; var Key: Char);
    procedure tACOSPINKeyPress(Sender: TObject; var Key: Char);
    procedure tACOSNewPINKeyPress(Sender: TObject; var Key: Char);
    procedure rb3DESClick(Sender: TObject);
    procedure rbDESClick(Sender: TObject);
    procedure btnMAClick(Sender: TObject);
    procedure btnSubmitClick(Sender: TObject);
    procedure btnChangePINClick(Sender: TObject);
    procedure btnInquireAccountClick(Sender: TObject);
    procedure bCreditClick(Sender: TObject);
    procedure btnDebitAmtClick(Sender: TObject);
    procedure tCreditAmtKeyPress(Sender: TObject; var Key: Char);
    procedure tDebitAmtKeyPress(Sender: TObject; var Key: Char);
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

  procedure DisplayOut(errType: Integer; retVal: Integer; PrintText: String; AppText : STRING);
  procedure InitMenu();

  function SendAPDUandDisplay(SendType: integer; ApduIn: string): integer;
  Function GetBalance(Data1 : Byte; Data2 : Byte; Data3 : Byte) : String;
  Function GetMCUResponse(SLen : Byte) : String;
  function CreditDebit(DataIn:array of Byte; maxDataLen : Byte; Buff1 : Byte; Buff2 : Byte; Response : String) : integer;
  function Debit_Acos2(Response : String) : integer;
  
implementation

{$R *.dfm}

procedure InitMenu();
begin
  frmMain.mMsg.Clear;
end;

procedure TfrmMain.btnListReadersClick(Sender: TObject);
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

  frmmain.cmbCardReader.Clear;
  LoadListToControl(frmmain.cmbCardReader,@G_buffer,bufferLen);
  frmmain.cmbCardReader.ItemIndex := 0;
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

  if G_retCode <> SCARD_S_SUCCESS then begin
        for indx := 0 to G_RecvLen -1 do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[Indx])]);

        DisplayOut(1, G_retCode, tmpStr, 'SAM');
        SendAPDUSAM := False;
        Exit;
     end;

  if G_retCode = SCARD_S_SUCCESS then begin
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

         DisplayOut(3, 0, Trim(tmpStr), 'MCU');
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

         DisplayOut(3, 0, Trim(tmpStr), 'MCU');
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

function Debit_Acos2(Response : String) : integer;
Var indx : integer;
      tmpstr : string;
     tmpstr2 : string;
     ArrIndx2 : integer;
begin
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $E6;
    G_SendBuff[2] := $0;
    G_SendBuff[3] := $0;
    G_SendBuff[4] := $B;
    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 5;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(Response)/2) - 1) + 5 do begin
      tmpstr2 := MidStr(Response,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    G_SendLen := 16;
    G_RecvLen := $02;
    tmpStr  := '';
    for indx := 0 to G_SendLen-1 do
      tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);

    DisplayOut(2, 0, tmpstr, 'MCU');
    G_retCode := SendAPDUandDisplay(0, tmpStr);

    if G_retCode <> SCARD_S_SUCCESS then
      begin
        Debit_Acos2 := G_retCode;
        Exit;
      end;

    tmpStr := '';
    for indx := 0 to 1 do
      tmpStr  := tmpStr + Format('%.2x',[G_RecvBuff[indx]]) + ' ';

    if tmpStr <> '90 00' then
      begin
        displayOut(1, 0, 'The return string is invalid. Value: ' + tmpStr, 'ACOS');
        G_retCode := INVALID_SW1SW2;
        Debit_Acos2 := G_retCode;
        Exit;
    end;

    Debit_Acos2 := G_retCode;
end;

function CreditDebit(DataIn:array of Byte; maxDataLen : Byte; Buff1 : Byte; Buff2 : Byte; Response : String) : integer;
Var indx : Integer;
    tmpstr : string;
begin
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := Buff1;
    G_SendBuff[2] := Buff2;
    G_SendBuff[3] := $0;
    G_SendBuff[4] := $B;
    for indx := 0 to maxDataLen-1 do
      G_SendBuff[indx + 5] := DataIn[indx];

    G_SendLen := maxDataLen + 5;
    G_RecvLen := $02;
    tmpStr  := '';
    for indx := 0 to G_SendLen-1 do
      tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);

    DisplayOut(2, 0, tmpstr, 'MCU');
    G_retCode := SendAPDUandDisplay(0, tmpStr);
    if G_retCode <> SCARD_S_SUCCESS then
      begin
        CreditDebit := G_retCode;
        Exit;
      end;

    tmpStr := '';
    for indx := 0 to 1 do
      tmpStr  := tmpStr + Format('%.2x',[G_RecvBuff[indx]]) + ' ';

    if Buff1 = $E2 then
      begin //Credit
        if tmpStr <> '90 00 ' then
          begin
            displayOut(1, 0, 'The return string is invalid. Value: ' + tmpStr, 'ACOS');
            G_retCode := INVALID_SW1SW2;
            CreditDebit := G_retCode;
            Exit;
        end;
      end;

    if Buff1 = $E6 then
      begin
        if tmpStr <> '61 04 ' then
          begin

            if tmpstr = '6A 86 ' then
              begin
                DisplayOut(0, 0, 'Debit Certificate Not Supported By ACOS2 card or lower', 'MCU');
                DisplayOut(0, 0, 'Change P1 = 0 to perform Debit without returning debit certificate', 'MCU');
                CreditDebit := Debit_Acos2(Response);
                exit;
              end
            else
              begin
                displayOut(1, 0, 'The return string is invalid. Value: ' + tmpStr, 'MCU');
                G_retCode := INVALID_SW1SW2;
                CreditDebit := G_retCode;
                Exit;
              end;
          end;
      end;

    CreditDebit := G_retCode;
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
        if (Trim(tmpStr) <> '90 00') then
          begin
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

Function GetSAMResponse(RecvLen : Byte; Buff4 : Byte) : String;
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
    G_SendBuff[4] := Buff4;

    tmpstr := '';
    for indx := 0 to 4 do
      tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    G_RecvLen := RecvLen;
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
        else begin
          tmpstr := '';
          for indx := 0 to 7 do
            tmpStr := tmpStr + Format('%.02X', [(G_RecvBuff[indx])]);

            GetSAMResponse := Trim(tmpStr);
        end;
      end;

end;

Function GetBalance(Data1 : Byte; Data2 : Byte; Data3 : Byte) : String;
var TotalBalance : Long;
begin
     //Get Total Balance ***************************************************
     TotalBalance := Data1 * 65536;

     TotalBalance := TotalBalance + (Data2 * 256);

     TotalBalance := TotalBalance + Data3;

     GetBalance := IntToStr(TotalBalance);

end;

Function GetMCUResponse(SLen : Byte) : String;
  //Acquires the MCU generated..
  //Function returns the hex value of generated key..
var indx : integer;
    tmpstr : string;
Begin
    GetMCUResponse := '';
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $C0;
    G_SendBuff[2] := $00;
    G_SendBuff[3] := $00;
    G_SendBuff[4] := SLen;

    tmpstr := '';
      for indx := 0 to 4 do
        tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'MCU');
    G_RecvLen := 27;
    G_SendLen := 5;

    if SendAPDUandDisplay(2, tmpstr) = 0 then
      begin
        tmpstr := '';
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

        if (Trim(tmpStr) <> '90 00') then
          begin
            DisplayOut(1, G_RetCode, Trim(tmpStr), 'MCU');
            GetMCUResponse := '';
            exit;
          end
        else begin
          tmpstr := '';
          for indx := 0 to G_RecvLen -3 do
            tmpStr := tmpStr + Format('%.02X', [(G_RecvBuff[indx])]);

          GetMCUResponse := Trim(tmpStr);
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

        if (Trim(tmpStr) <> '90 00') then
          begin
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
      displayOut(2, 0, 'The return string is invalid. Value: ' + tmpStr, 'MCU');
      G_retCode := INVALID_SW1SW2;
    end;
  readRecord := G_retCode;

end;


procedure displayOut(errType: Integer; retVal: Integer; PrintText: String; AppText : STRING);
	//Displays the APDU sent and recieved by the SAM and MCU card..
	//returns 1 if erronous and 0 if successful
begin

  case errType of
    0: frmMain.mMsg.SelAttributes.Color := clTeal;      // Notifications
    1: begin                                                    // Error Messages
         frmMain.mMsg.SelAttributes.Color := clRed;
         PrintText := APPTEXT + '>' + GetScardErrMsg(retVal) + ' : ' + printtext;
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

procedure TfrmMain.btnConnectClick(Sender: TObject);
var ReaderLen, ATRLen: DWORD;
    dwState: integer;
    ATRVal: array[0..19] of Byte;
    tmpStr: String;
    indx: integer;
begin
  //SAM Connection ***************************************
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

  //ACOS Connection ****************************************
  if G_ConnActiveMCU then
    begin
      DisplayOut(0, 0, 'Connection is already active.', 'ACOS');
      Exit;
    end;

  DisplayOut(2, 0, 'Invoke SCardConnect', 'ACOS');
  // 1. Connect to selected reader using hContext handle
  //    and obtain valid hCard handle
  G_retCode := SCardConnectA(G_hContext,
                           PChar(cmbCARDReader.Text),
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
    DisplayOut(0, 0, 'Successful connection to ' + cmbCardReader.Text, 'ACOS');

  DisplayOut(2, 0, 'Get Card Status', 'ACOS');
  ATRLen := 32;
  ReaderLen := 0;
  dwState := 0;
  G_retCode := SCardStatusA(G_hCard,
                         PChar(cmbCardReader.Text),
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

  if G_ConnActiveMCU = True and G_ConnActive = True then begin
    tSAMGPIN.Enabled := True;
    tACOSPIN.Enabled := True;
    tACOSNewPIN.Enabled := True;

    btnMA.Enabled := True;
  end;

end;



procedure TfrmMain.tSAMGPINKeyPress(Sender: TObject; var Key: Char);
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

procedure TfrmMain.tACOSNewPINKeyPress(Sender: TObject; var Key: Char);
begin
if Key in ['a'..'z'] then Dec(Key,32);
    if Not(Key in ['0'..'9', 'A'..'F', chr(8), chr(32)]) then
      Key := #0;
end;

procedure TfrmMain.rb3DESClick(Sender: TObject);
begin
  G_AlgoRef := 0;
end;

procedure TfrmMain.rbDESClick(Sender: TObject);
begin
   G_AlgoRef := 0;
end;

procedure TfrmMain.btnMAClick(Sender: TObject);
var tmpstr : String;
    tmpstr2 : String;
    indx : integer;
    ArrIndx2 : integer;
    tmpArr : array [0..42] of byte;
    Challenge : String;
    SN : String;
    RNDt : String;
    Response : String;
begin
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
        tmpStr := tmpStr + Format('%.02X', [(G_RecvBuff[indx])]);

      SN := tmpStr;
    end;

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

    //Submit Issuer PIN (SAM Global PIN)***********************************
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

    //Diversify Kc ************************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $72;
    G_SendBuff[2] := $4;
    G_SendBuff[3] := $82;
    G_SendBuff[4] := $8;
    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 5;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(SN)/2) - 1) + 5 do begin
      tmpstr2 := MidStr(SN,indx,2);
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
    if SendAPDUSAM(tmpArr, G_SendLen, G_RecvLen, G_RecvBuff) = True then
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

    //Diversify Kt ************************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $72;
    G_SendBuff[2] := $3;
    G_SendBuff[3] := $83;
    G_SendBuff[4] := $8;
    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 5;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(SN)/2) - 1) + 5 do begin
      tmpstr2 := MidStr(SN,indx,2);
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
    if SendAPDUSAM(tmpArr, G_SendLen, G_RecvLen, G_RecvBuff) = True then
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

    //Get Challenge ************************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $84;
    G_SendBuff[2] := $0;
    G_SendBuff[3] := $0;
    G_SendBuff[4] := $8;

    G_RecvLen := 10;
    G_SendLen := 5;

    tmpStr := '';
    for indx := 0 to G_SendLen-1 do
      tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);

    DisplayOut(2, 0, tmpstr, 'MCU');
    G_retCode := SendAPDUandDisplay(2, tmpStr);
    if G_retCode = SCARD_S_SUCCESS then
      begin
        Challenge := '';
        for indx := 0 to (G_RecvLen-3) do
          Challenge := Challenge + Format('%.2X', [(G_RecvBuff[Indx])]);
      end
    else
        Exit;

    //'Prepare ACOS authentication *************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $78;

    If G_AlgoRef = 1 Then
        G_SendBuff[2] := $1
    Else
        G_SendBuff[2] := $0;

    G_SendBuff[3] := $0;
    G_SendBuff[4] := $8;

    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 5;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(Challenge)/2) - 1) + 5 do begin
      tmpstr2 := MidStr(Challenge,indx,2);
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

        if (Trim(tmpStr) <> '61 10') then
          begin
            DisplayOut(1, 0, Trim(tmpStr), 'SAM');
            exit;
          end;
      end;

    //'Get Response to get result + RNDt ********************************************
    ClearBuffers();
    G_SendBuff[0] := $00;
    G_SendBuff[1] := $C0;
    G_SendBuff[2] := $0;
    G_SendBuff[3] := $0;
    G_SendBuff[4] := $10;

    tmpstr := '';
    for indx := 0 to 4 do
      tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    G_RecvLen := $12;
    G_SendLen := $5;
    if SendAPDUSAM(G_SendBuff, G_SendLen, G_RecvLen, G_RecvBuff) = True then
      begin
        tmpstr := '';
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

        if (Trim(tmpStr) <> '90 00') then
          begin
            DisplayOut(1, 0, Trim(tmpStr), 'SAM');
            exit;
          end
        else
          begin
            DisplayOut(3, 0, Trim(tmpstr), 'SAM');
            tmpstr := '';

            for indx := 0 to (G_RecvLen-3) do
              tmpStr := tmpStr + Format('%.02X', [(G_RecvBuff[Indx])]);

            RNDt := tmpstr;
          end;
      end;

    //'Authenticate ************************************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $82;
    G_SendBuff[2] := $0;
    G_SendBuff[3] := $0;
    G_SendBuff[4] := $10;

    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 5;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(RNDt)/2) - 1) + 5 do begin
      tmpstr2 := MidStr(RNDt,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    G_SendLen := ArrIndx2;
    tmpStr := '';
    for indx := 0 to G_SendLen -1 do
      tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);

    G_RecvLen := 2;

    DisplayOut(2, 0, tmpstr, 'MCU');
    G_retCode := SendAPDUandDisplay(0, tmpStr);
    if G_retCode <> SCARD_S_SUCCESS then
        Exit
    else begin
        tmpstr := '';
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);
        if (Trim(tmpStr) <> '61 08') then
            exit;
    end;

    //'Get Response to get result **********************************************
    Response := GetMCUResponse($8);

    if Response = '' then
      exit;

    //'Verify ACOS Authentication **********************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $7A;
    G_SendBuff[2] := $0;
    G_SendBuff[3] := $0;
    G_SendBuff[4] := $8;

    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 5;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(Response)/2) - 1) + 5 do begin
      tmpstr2 := MidStr(Response,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    G_SendLen := ArrIndx2;
    tmpStr := '';
    for indx := 0 to G_SendLen -1 do
      tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);

    G_RecvLen := 2;

    DisplayOut(2, 0, tmpstr, 'SAM');
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

    btnSubmit.Enabled := True;

end;

procedure TfrmMain.btnSubmitClick(Sender: TObject);
var tmpstr : String;
    tmpstr2 : String;
    indx : integer;
    ArrIndx2 : integer;
    Response : String;
begin
    //'Encrypt PIN****************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $74;

    If G_AlgoRef = 1 Then
        G_SendBuff[2] := $1
    else
        G_SendBuff[2] := $0;

    G_SendBuff[3] := $1;
    G_SendBuff[4] := $8;
    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 5;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(tACOSPIN.Text)/2) - 1) + 5 do begin
      tmpstr2 := MidStr(tACOSPIN.Text,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    G_SendLen := ArrIndx2;
    tmpStr := '';
    for indx := 0 to G_SendLen -1 do
      tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);

    G_RecvLen := 2;

    DisplayOut(2, 0, tmpstr, 'SAM');
    if SendAPDUSAM(G_SendBuff, G_SendLen, G_RecvLen, G_RecvBuff) = True then
      begin
        tmpstr := '';
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

        if (Trim(tmpStr) <> '61 08') then
          begin
            DisplayOut(1, 0, Trim(tmpStr), 'SAM');
            exit;
          end;
      end;

    //'Get Response to get encrypted PIN ****************************************
    Response := GetSAMResponse(10, $8);

    if Response = '' then
      exit
    else begin
      tmpStr := '';
      for indx := (G_RecvLen-2) to (G_RecvLen-1) do
        tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);
        DisplayOut(3, 0, Trim(tmpStr), 'SAM');
    end;


    //'Submit Encrypted PIN ************************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $20;
    G_SendBuff[2] := $6;  //'PIN
    G_SendBuff[3] := $0;
    G_SendBuff[4] := $8;

    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 5;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(Response)/2) - 1) + 5 do begin
      tmpstr2 := MidStr(Response,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    G_SendLen := ArrIndx2;
    tmpStr := '';
    for indx := 0 to G_SendLen -1 do
      tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);

    G_RecvLen := 2;

    DisplayOut(2, 0, tmpstr, 'MCU');
    G_retCode := SendAPDUandDisplay(0, tmpStr);
    if G_retCode <> SCARD_S_SUCCESS then
        Exit
    else begin
        tmpstr := '';
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

        if (Trim(tmpStr) <> '90 00') then
            exit;
    end;

    btnChangePIN.Enabled := True;
end;

procedure TfrmMain.btnChangePINClick(Sender: TObject);
var tmpstr : String;
    tmpstr2 : String;
    indx : integer;
    ArrIndx2 : integer;
    Response : String;
begin
    //'Decrypt PIN *************************************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $76;

    If G_AlgoRef = 1 Then
      G_SendBuff[2] := $1
    else
      G_SendBuff[2] := $0;

    G_SendBuff[3] := $1;
    G_SendBuff[4] := $8;
    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 5;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(tACOSNewPin.Text)/2) - 1) + 5 do begin
      tmpstr2 := MidStr(tACOSNewPin.Text,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    G_SendLen := ArrIndx2;
    tmpStr := '';
    for indx := 0 to G_SendLen -1 do
      tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);

    G_RecvLen := 2;

    DisplayOut(2, 0, tmpstr, 'SAM');
    if SendAPDUSAM(G_SendBuff, G_SendLen, G_RecvLen, G_RecvBuff) = True then
      begin
        tmpstr := '';
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

        if (Trim(tmpStr) <> '61 08') then
          begin
            DisplayOut(1, 0, Trim(tmpStr), 'SAM');
            exit;
          end;
      end;

    //'Get Response to get decrypted PIN ***********************************
    Response := GetSAMResponse(10, $8);

    if Response = '' then
      exit
    else begin
      tmpStr := '';
      for indx := (G_RecvLen-2) to (G_RecvLen-1) do
        tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);
        DisplayOut(3, 0, Trim(tmpStr), 'SAM');
    end;

    //'Change PIN ***********************************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $24;
    G_SendBuff[2] := $0;
    G_SendBuff[3] := $0;
    G_SendBuff[4] := $8;
    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 5;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(Response)/2) - 1) + 5 do begin
      tmpstr2 := MidStr(Response,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    G_SendLen := ArrIndx2;
    tmpStr := '';
    for indx := 0 to G_SendLen -1 do
      tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);

    G_RecvLen := 2;

    DisplayOut(2, 0, tmpstr, 'MCU');
    G_retCode := SendAPDUandDisplay(0, tmpStr);
    if G_retCode <> SCARD_S_SUCCESS then
        Exit
    else begin
        tmpstr := '';
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);
        if (Trim(tmpStr) <> '90 00') then
            exit;
    end;
end;

procedure TfrmMain.btnInquireAccountClick(Sender: TObject);
var tmpstr : String;
    tmpstr2 : String;
    indx : integer;
    ArrIndx2 : integer;
    tmpArr : array [0..42] of byte;
    SN : String;
    Response : String;
begin
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
          tmpStr := tmpStr + Format('%.02X', [(G_RecvBuff[indx])]);

          SN := tmpStr;
      end;

    //Diversify Kcf ************************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $72;
    G_SendBuff[2] := $2;
    G_SendBuff[3] := $86;
    G_SendBuff[4] := $8;
    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 5;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(SN)/2) - 1) + 5 do begin
      tmpstr2 := MidStr(SN,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    tmpstr := '';
    for indx := 0 to ArrIndx2 -1 do
      tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    G_RecvLen := 2;
    G_SendLen := ArrIndx2;
    if SendAPDUSAM(tmpArr, G_SendLen, G_RecvLen, G_RecvBuff) = True then
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

    //'Inquire Account *****************************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $E4;
    G_SendBuff[2] := $2;
    G_SendBuff[3] := $0;
    G_SendBuff[4] := $4;
    //'4 bytes reference
    G_SendBuff[5] := $AA;
    G_SendBuff[6] := $BB;
    G_SendBuff[7] := $CC;
    G_SendBuff[8] := $DD;

    G_SendLen := 9;
    tmpStr := '';
    for indx := 0 to G_SendLen -1 do
      tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);

    G_RecvLen := 2;

    DisplayOut(2, 0, tmpstr, 'MCU');
    G_retCode := SendAPDUandDisplay(0, tmpStr);
    if G_retCode <> SCARD_S_SUCCESS then
        Exit
    else begin
        tmpstr := '';
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);
        if (Trim(tmpStr) <> '61 19') then
            exit;
    end;

    //'Get Response to get result*******************************************
    Response := GetMCUResponse($19);

    if Response = '' then
      exit;

    //'Verify Inquire Account***********************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $7C;

    if G_AlgoRef = 1 then
      G_SendBuff[2] := $3
    else
      G_SendBuff[2] := $2;

    G_SendBuff[3] := $0;
    G_SendBuff[4] := $1D;
    //'4 bytes reference
    G_SendBuff[5] := $AA;
    G_SendBuff[6] := $BB;
    G_SendBuff[7] := $CC;
    G_SendBuff[8] := $DD;
    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 9;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(Response)/2) - 1) + 5 do begin
      tmpstr2 := MidStr(Response,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
      //Note :  MAC = G_SendBuff[9...12]
      //        Transaction Type = G_SendBuff[13]
      //        Balance = G_SendBuff[14...16]
      //        ATREF = G_SendBuff[17...22]
      //        Max Balance = G_SendBuff[23...25]
      //        TTREFc = G_SendBuff[26...29]
      //        TTREFd = G_SendBuff[30...33]
    end;

    tmpstr := '';
    for indx := 0 to ArrIndx2 -1 do
      tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    G_RecvLen := 2;
    G_SendLen := 34;
    if SendAPDUSAM(tmpArr, G_SendLen, G_RecvLen, G_RecvBuff) = True then
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

    tMaxBal.Text := GetBalance(strToInt('$' + MidStr(Response,29,2)), strToInt('$' + MidStr(Response,31,2)), strToInt('$' + MidStr(Response,33,2)));

    tBal.Text := GetBalance(strToInt('$' + MidStr(Response,11,2)), strToInt('$' + MidStr(Response,13,2)), strToInt('$' + MidStr(Response,15,2)));

end;

procedure TfrmMain.bCreditClick(Sender: TObject);
var tmpstr : String;
    tmpstr2 : String;
    indx : integer;
    ArrIndx2 : integer;
    tmpArr : array [0..42] of byte;
    SN : String;
    Response : String;
begin
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
        tmpStr := tmpStr + Format('%.02X', [(G_RecvBuff[indx])]);

        SN := tmpStr;
    end;

    //Diversify Kcr ************************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $72;
    G_SendBuff[2] := $2;
    G_SendBuff[3] := $85;
    G_SendBuff[4] := $8;
    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 5;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(SN)/2) - 1) + 5 do begin
      tmpstr2 := MidStr(SN,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    tmpstr := '';
    for indx := 0 to ArrIndx2 -1 do
      tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    G_RecvLen := 2;
    G_SendLen := ArrIndx2;
    if SendAPDUSAM(tmpArr, G_SendLen, G_RecvLen, G_RecvBuff) = True then
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

    //'Inquire Account *****************************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $E4;
    G_SendBuff[2] := $1;
    G_SendBuff[3] := $0;
    G_SendBuff[4] := $4;
    //'4 bytes reference
    G_SendBuff[5] := $AA;
    G_SendBuff[6] := $BB;
    G_SendBuff[7] := $CC;
    G_SendBuff[8] := $DD;

    G_SendLen := 9;
    tmpStr := '';
    for indx := 0 to G_SendLen -1 do
      tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);

    G_RecvLen := 2;

    DisplayOut(2, 0, tmpstr, 'MCU');
    G_retCode := SendAPDUandDisplay(0, tmpStr);
    if G_retCode <> SCARD_S_SUCCESS then
        Exit
    else begin
        tmpstr := '';
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);
        if (Trim(tmpStr) <> '61 19') then
            exit;
    end;

    //'Get Response to get result*******************************************
    Response := GetMCUResponse($19);

    if Response = '' then
      exit;

    //'Verify Inquire Account***********************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $7C;

    if G_AlgoRef = 1 then
      G_SendBuff[2] := $3
    else
      G_SendBuff[2] := $2;

    G_SendBuff[3] := $0;
    G_SendBuff[4] := $1D;
    //'4 bytes reference
    G_SendBuff[5] := $AA;
    G_SendBuff[6] := $BB;
    G_SendBuff[7] := $CC;
    G_SendBuff[8] := $DD;
    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 9;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(Response)/2) - 1) + 5 do begin
      tmpstr2 := MidStr(Response,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
      //Note :  MAC = G_SendBuff[9...12]
      //        Transaction Type = G_SendBuff[13]
      //        Balance = G_SendBuff[14...16]
      //        ATREF = G_SendBuff[17...22]
      //        Max Balance = G_SendBuff[23...25]
      //        TTREFc = G_SendBuff[26...29]
      //        TTREFd = G_SendBuff[30...33]
    end;

    tmpstr := '';
    for indx := 0 to ArrIndx2 -1 do
      tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    G_RecvLen := 2;
    G_SendLen := 34;
    if SendAPDUSAM(tmpArr, G_SendLen, G_RecvLen, G_RecvBuff) = True then
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

    //'Prepare ACOS Transaction ***********************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $7E;

    if G_AlgoRef = 1 then
      G_SendBuff[2] := $3
    else
      G_SendBuff[2] := $2;

    G_SendBuff[3] := $E2;
    G_SendBuff[4] := $D;

    //'Amount to Credit
    G_SendBuff[5] := strToInt(tCreditAMT.Text) div 65536;
    G_SendBuff[6] := (strToInt(tCreditAMT.Text) div 256) Mod 65536 Mod 256;
    G_SendBuff[7] := strToInt(tCreditAMT.Text) Mod 256;
    indx := 43;        //Array of textbox increments by 2 characters
    ArrIndx2 := 8;    //Array index increment by 1 only
    while ArrIndx2 <= 11 do begin
      tmpstr2 := MidStr(Response,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
      //Note :  TTREFd = G_SendBuff[8...11]
    end;

    indx := 17;        //Array of textbox increments by 2 characters
    ArrIndx2 := 12;    //Array index increment by 1 only
    while ArrIndx2 <= 17 do begin
      tmpstr2 := MidStr(Response,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
      //Note :  'ATREF = G_SendBuff[12...17]
    end;

    tmpstr := '';
    for indx := 0 to ArrIndx2 -1 do
      tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    G_RecvLen := 2;
    G_SendLen := 18;
    if SendAPDUSAM(tmpArr, G_SendLen, G_RecvLen, G_RecvBuff) = True then
      begin
        tmpstr := '';
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

        if (Trim(tmpStr) <> '61 0B') then
          begin
            DisplayOut(1, 0, Trim(tmpStr), 'SAM');
            exit;
          end;
      end;

    //'Get Response to get result***********************************************
    Response := GetSAMResponse($D, $B);

    if Response = '' then
      exit
    else begin
      tmpStr := '';
      for indx := (G_RecvLen-2) to (G_RecvLen-1) do
        tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

      DisplayOut(3, 0, Trim(tmpStr), 'SAM');
    end;

    //'Credit*******************************************************************
    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 0;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(Response)/2) - 1) do begin
      tmpstr2 := MidStr(Response,indx,2);
      tmpArr[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    if CreditDebit(tmpArr, ArrIndx2 +3, $E2, $0, '') <> 0 then
      exit;

    //'Perform Verify Inquire Account w/ Credit Key and new ammount***************
    //'Inquire Account**********************************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $E4;
    G_SendBuff[2] := $1;
    G_SendBuff[3] := $0;
    G_SendBuff[4] := $4;
    //'4 bytes reference
    G_SendBuff[5] := $AA;
    G_SendBuff[6] := $BB;
    G_SendBuff[7] := $CC;
    G_SendBuff[8] := $DD;

    G_SendLen := 9;
    tmpStr := '';
    for indx := 0 to G_SendLen -1 do
      tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);

    G_RecvLen := 2;

    DisplayOut(2, 0, tmpstr, 'MCU');
    G_retCode := SendAPDUandDisplay(0, tmpStr);
    if G_retCode <> SCARD_S_SUCCESS then
        Exit
    else begin
        tmpstr := '';
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);
        if (Trim(tmpStr) <> '61 19') then
            exit;
    end;

    //'Get Response to get result*******************************************
    Response := GetMCUResponse($19);

    if Response = '' then
      exit;

    //'Verify Inquire Account***********************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $7C;

    if G_AlgoRef = 1 then
      G_SendBuff[2] := $3
    else
      G_SendBuff[2] := $2;

    G_SendBuff[3] := $0;
    G_SendBuff[4] := $1D;
    //'4 bytes reference
    G_SendBuff[5] := $AA;
    G_SendBuff[6] := $BB;
    G_SendBuff[7] := $CC;
    G_SendBuff[8] := $DD;
    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 9;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(Response)/2) - 1) + 5 do begin
      tmpstr2 := MidStr(Response,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
      //Note :  MAC = G_SendBuff[9...12]
      //        Transaction Type = G_SendBuff[13]
      //        Balance = G_SendBuff[14...16]
      //        ATREF = G_SendBuff[17...22]
      //        Max Balance = G_SendBuff[23...25]
      //        TTREFc = G_SendBuff[26...29]
      //        TTREFd = G_SendBuff[30...33]
    end;

    tmpstr := '';
    for indx := 0 to ArrIndx2 -1 do
      tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    G_RecvLen := 2;
    G_SendLen := 34;
    if SendAPDUSAM(tmpArr, G_SendLen, G_RecvLen, G_RecvBuff) = True then
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

    tMaxBal.Text := GetBalance(strToInt('$' + MidStr(Response,29,2)), strToInt('$' + MidStr(Response,31,2)), strToInt('$' + MidStr(Response,33,2)));

    tBal.Text := GetBalance(strToInt('$' + MidStr(Response,11,2)), strToInt('$' + MidStr(Response,13,2)), strToInt('$' + MidStr(Response,15,2)));

end;

procedure TfrmMain.btnDebitAmtClick(Sender: TObject);
var tmpstr : String;
    tmpstr2 : String;
    indx : integer;
    ArrIndx2 : integer;
    tmpArr : array [0..42] of byte;
    SN : String;
    Response : String;
    Response2 : String;
    Bal : Integer;
    NewBal : String;
begin

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
        tmpStr := tmpStr + Format('%.02X', [(G_RecvBuff[indx])]);

      SN := tmpStr;
    end;

    //Diversify Kd ************************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $72;
    G_SendBuff[2] := $2;
    G_SendBuff[3] := $84;
    G_SendBuff[4] := $8;
    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 5;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(SN)/2) - 1) + 5 do begin
      tmpstr2 := MidStr(SN,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    tmpstr := '';
    for indx := 0 to ArrIndx2 -1 do
      tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    G_RecvLen := 2;
    G_SendLen := ArrIndx2;
    if SendAPDUSAM(tmpArr, G_SendLen, G_RecvLen, G_RecvBuff) = True then
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

    //'Inquire Account *****************************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $E4;
    G_SendBuff[2] := $0;
    G_SendBuff[3] := $0;
    G_SendBuff[4] := $4;
    //'4 bytes reference
    G_SendBuff[5] := $AA;
    G_SendBuff[6] := $BB;
    G_SendBuff[7] := $CC;
    G_SendBuff[8] := $DD;

    G_SendLen := 9;
    tmpStr := '';
    for indx := 0 to G_SendLen -1 do
      tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);

    G_RecvLen := 2;

    DisplayOut(2, 0, tmpstr, 'MCU');
    G_retCode := SendAPDUandDisplay(0, tmpStr);
    if G_retCode <> SCARD_S_SUCCESS then
        Exit
    else begin
        tmpstr := '';
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);
        if (Trim(tmpStr) <> '61 19') then
            exit;
    end;

    //'Get Response to get result*******************************************
    Response2 := GetMCUResponse($19);

    if Response2 = '' then
      exit;

    //'Verify Inquire Account***********************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $7C;

    if G_AlgoRef = 1 then
      G_SendBuff[2] := $3
    else
      G_SendBuff[2] := $2;

    G_SendBuff[3] := $0;
    G_SendBuff[4] := $1D;
    //'4 bytes reference
    G_SendBuff[5] := $AA;
    G_SendBuff[6] := $BB;
    G_SendBuff[7] := $CC;
    G_SendBuff[8] := $DD;
    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 9;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(Response2)/2) - 1) + 9 do begin
      tmpstr2 := MidStr(Response2,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
      //Note :  MAC = G_SendBuff[9...12]
      //        Transaction Type = G_SendBuff[13]
      //        Balance = G_SendBuff[14...16]
      //        ATREF = G_SendBuff[17...22]
      //        Max Balance = G_SendBuff[23...25]
      //        TTREFc = G_SendBuff[26...29]
      //        TTREFd = G_SendBuff[30...33]
    end;

    tmpstr := '';
    for indx := 0 to ArrIndx2 -1 do
      tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    G_RecvLen := 2;
    G_SendLen := 34;
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

    Bal := StrToInt(GetBalance(strToInt('$' + MidStr(Response2,11,2)), strToInt('$' + MidStr(Response2,13,2)), strToInt('$' + MidStr(Response2,15,2))));

    //'Prepare ACOS Transaction ***********************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $7E;

    if G_AlgoRef = 1 then
      G_SendBuff[2] := $3
    else
      G_SendBuff[2] := $2;

    G_SendBuff[3] := $E6;
    G_SendBuff[4] := $D;

    //'Amount to Credit
    G_SendBuff[5] := strToInt(tDebitAMT.Text) div 65536;
    G_SendBuff[6] := (strToInt(tDebitAMT.Text) div 256) Mod 65536 Mod 256;
    G_SendBuff[7] := strToInt(tDebitAMT.Text) Mod 256;
    indx := 43;        //Array of textbox increments by 2 characters
    ArrIndx2 := 8;    //Array index increment by 1 only
    while ArrIndx2 <= 11 do begin
      tmpstr2 := MidStr(Response2,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
      //Note :  TTREFd = G_SendBuff[8...11]
    end;

    indx := 17;        //Array of textbox increments by 2 characters
    ArrIndx2 := 12;    //Array index increment by 1 only
    while ArrIndx2 <= 17 do begin
      tmpstr2 := MidStr(Response2,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
      //Note :  'ATREF = G_SendBuff[12...17]
    end;

    tmpstr := '';
    for indx := 0 to ArrIndx2 -1 do
      tmpStr := tmpStr + Format('%.02X ', [(G_SendBuff[Indx])]);

    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    G_RecvLen := 2;
    G_SendLen := 18;
    if SendAPDUSAM(G_SendBuff, G_SendLen, G_RecvLen, G_RecvBuff) = True then
      begin
        tmpstr := '';
        for indx := (G_RecvLen-2) to (G_RecvLen-1) do
          tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

        if (Trim(tmpStr) <> '61 0B') then
          begin
            DisplayOut(1, 0, Trim(tmpStr), 'SAM');
            exit;
          end;
      end;

    //'Get Response to get result***********************************************
    Response := GetSAMResponse($D, $B);

    if Response = '' then
      exit
    else begin
      tmpStr := '';
      for indx := (G_RecvLen-2) to (G_RecvLen-1) do
        tmpStr := tmpStr + Format('%.02X ', [(G_RecvBuff[indx])]);

      DisplayOut(3, 0, Trim(tmpStr), 'SAM');
    end;

    //'Debit and return Debit Certificate****************************************
    indx := 1;        //Array of textbox increments by 3 characters
    ArrIndx2 := 0;    //Array index increment by 1 only
    while ArrIndx2 <= ((Length(Response)/2) - 1) do begin
      tmpstr2 := MidStr(Response,indx,2);
      tmpArr[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
    end;

    if CreditDebit(tmpArr, ArrIndx2 +3, $E6, $1, Response) <> 0 then
      exit;

    //'Get Response to get result***********************************************
    Response := GetMCUResponse($4);

    if Response = '' then
      exit;

    //'Verify Debit Certificate ************************************************
    ClearBuffers();
    G_SendBuff[0] := $80;
    G_SendBuff[1] := $70;
    if G_AlgoRef = 1 then
      G_SendBuff[2] := $3
    else
      G_SendBuff[2] := $2;

    G_SendBuff[3] := $0;
    G_SendBuff[4] := $14;

    indx := 1;        //Array of textbox increments by 2 characters
    ArrIndx2 := 5;    //Array index increment by 1 only
    while ArrIndx2 <= 8 do begin
      tmpstr2 := MidStr(Response,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
      //Note : G_SendBuff[5...8] Only
    end;

    //'Amount last Debited from card
    G_SendBuff[9] := strToInt(tDebitAMT.Text) div 65536;
    G_SendBuff[10] := (strToInt(tDebitAMT.Text) div 256) Mod 65536 Mod 256;
    G_SendBuff[11] := strToInt(tDebitAMT.Text) Mod 256;

    //'Expected New Balance after the Debit
    NewBal := IntToStr(Bal - strToInt(tDebitAMT.Text));

    G_SendBuff[12] := strToInt(NewBal) div 65536;
    G_SendBuff[13] := (strToInt(NewBal) div 256) Mod 65536 Mod 256;
    G_SendBuff[14] := strToInt(NewBal) Mod 256;

    indx := 17;        //Array of textbox increments by 2 characters
    ArrIndx2 := 15;    //Array index increment by 1 only
    while ArrIndx2 <= 20 do begin
      tmpstr2 := MidStr(Response2,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
      //Note :  'ATREF = G_SendBuff[15...20]
    end;

    indx := 43;        //Array of textbox increments by 2 characters
    ArrIndx2 := 21;    //Array index increment by 1 only
    while ArrIndx2 <= 24 do begin
      tmpstr2 := MidStr(Response2,indx,2);
      G_SendBuff[ArrIndx2] := strToInt('$' + tmpstr2);
      ArrIndx2 := ArrIndx2 + 1;
      indx := indx + 2;
      //Note :  TTREFd = G_SendBuff[21...24]
    end;

    G_RecvLen := 2;
    G_SendLen := 25;
    tmpStr := '';
    for indx := 0 to G_SendLen -1 do
      tmpStr := tmpStr + Format('%.02X ',[G_SendBuff[indx]]);
    DisplayOut(2, 0, Trim(tmpStr), 'SAM');
    
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

    tBal.Text := NewBal;
end;

procedure TfrmMain.tCreditAmtKeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then
    if Not (Key in ['0'..'9'])then
      Key := Chr($00);
end;

procedure TfrmMain.tDebitAmtKeyPress(Sender: TObject; var Key: Char);
begin
if Key <> chr($08) then
    if Not (Key in ['0'..'9'])then
      Key := Chr($00);
end;



procedure TfrmMain.FormActivate(Sender: TObject);
begin
  InitMenu();
end;

end.
