//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              GetATR.dpr
//
//  Description:       This sample program outlines the steps on how to
//                     get ATR from cards using ACR128
//
//  Author:	           Jose Isagani R. Mission
//
//  Date:	             May 19, 2008
//
//  Revision Trail:   (Date/Author/Description)
//
//======================================================================
unit GetATRMain;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ACSModule, StdCtrls, ComCtrls;

const MAX_BUFFER_LEN    = 256;
const INVALID_SW1SW2    = -450;

type
  TMainGetATR = class(TForm)
    Label1: TLabel;
    cbReader: TComboBox;
    bInit: TButton;
    bConnect: TButton;
    bGetAtr: TButton;
    mMsg: TRichEdit;
    bReset: TButton;
    bQuit: TButton;
    bClear: TButton;
    procedure bInitClick(Sender: TObject);
    procedure bConnectClick(Sender: TObject);
    procedure bResetClick(Sender: TObject);
    procedure bQuitClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure bGetAtrClick(Sender: TObject);
    procedure bClearClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  MainGetATR: TMainGetATR;
  hContext            : SCARDCONTEXT;
  hCard               : SCARDCONTEXT;
  ioRequest           : SCARD_IO_REQUEST;
  retCode             : Integer;
  dwActProtocol, BufferLen  : DWORD;
  SendBuff, RecvBuff  : array [0..262] of Byte;
  ATRVal              : array [0..128] of byte;
  ATRLen              : ^DWORD;
  SendLen, RecvLen, nBytesRet: DWORD;
  Buffer              : array [0..MAX_BUFFER_LEN] of char;
  connActive          : Boolean;

procedure InitMenu();
procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
procedure EnableButtons();
procedure ClearBuffers();
procedure InterpretATR();
function CallCardControl(): integer;
function SendAPDUandDisplay(SendType: integer): integer;

implementation

{$R *.dfm}
procedure InitMenu();
begin

  connActive := False;
  MainGetATR.cbReader.Clear;
  MainGetATR.mMsg.Clear;
  DisplayOut(0, 0, 'Program ready');
  MainGetATR.bConnect.Enabled := False;
  MainGetATR.bGetAtr.Enabled := False;
  MainGetATR.bReset.Enabled := False;
  MainGetATR.bInit.Enabled := True;

end;

procedure displayOut(errType: Integer; retVal: Integer; PrintText: String);
begin

  case errType of
    0: MainGetATR.mMsg.SelAttributes.Color := clTeal;      // Notifications
    1: begin                                                // Error Messages
         MainGetATR.mMsg.SelAttributes.Color := clRed;
         PrintText := GetScardErrMsg(retVal);
       end;
    2: begin
         MainGetATR.mMsg.SelAttributes.Color := clBlack;
         PrintText := '< ' + PrintText;                      // Input data
       end;
    3: begin
         MainGetATR.mMsg.SelAttributes.Color := clBlack;
         PrintText := '> ' + PrintText;                      // Output data
       end;
    4: MainGetATR.mMsg.SelAttributes.Color := clRed;        // For ACOS1 error
  end;
  MainGetATR.mMsg.Lines.Add(PrintText);
  MainGetATR.mMsg.SelAttributes.Color := clBlack;
  MainGetATR.mMsg.SetFocus;

end;

procedure EnableButtons();
begin

  MainGetATR.bInit.Enabled := False;
  MainGetATR.bConnect.Enabled := True;
  MainGetATR.bGetAtr.Enabled := True;
  MainGetATR.bReset.Enabled := True;

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

procedure InterpretATR();
var RIDVal, sATRStr, lATRStr, cardName: String;
    indx: Integer;
begin

  // 4. Interpret ATR and guess card
  // 4.1. Mifare cards using ISO 14443 Part 3 Supplemental Document
  if integer(ATRLen) > 14 then begin
    RIDVal := '';
    sATRStr := '';
    lATRStr := '';
    for indx := 7 to 11 do
      RIDVal := RIDVal + Format('%.02X',[ATRVal[indx]]);
    for indx := 0 to 4 do
      if ((indx = 1) and (ATRVal[indx] shr 4 = 8)) then begin
        lATRStr := lATRStr + '8X';
        sATRStr := sATRStr + '8X';
      end
      else begin
        if indx = 4 then
          lATRStr := lATRStr + Format('%.02X',[ATRVal[indx]])
        else begin
          lATRStr := lATRStr + Format('%.02X',[ATRVal[indx]]);
          sATRStr := sATRStr + Format('%.02X',[ATRVal[indx]]);
        end;
      end;
    if (RIDVal = 'A000000306') then begin
      cardName := '';
      case ATRVal[12] of
        0: cardName := 'No card information';
        1: cardName := 'ISO 14443 A, Part1 Card Type';
        2: cardName := 'ISO 14443 A, Part2 Card Type';
        3: cardName := 'ISO 14443 A, Part3 Card Type';
        5: cardName := 'ISO 14443 B, Part1 Card Type';
        6: cardName := 'ISO 14443 B, Part2 Card Type';
        7: cardName := 'ISO 14443 B, Part3 Card Type';
        9: cardName := 'ISO 15693, Part1 Card Type';
        10: cardName := 'ISO 15693, Part2 Card Type';
        11: cardName := 'ISO 15693, Part3 Card Type';
        12: cardName := 'ISO 15693, Part4 Card Type';
        13: cardName := 'Contact Card (7816-10) IIC Card Type';
        14: cardName := 'Contact Card (7816-10) Extended IIC Card Type';
        15: cardName := 'Contact Card (7816-10) 2WBP Card Type';
        16: cardName := 'Contact Card (7816-10) 3WBP Card Type';
      else
        cardName := 'Undefined card';
      end;
      if (ATRVal[12] = $03) then
        if ATRVal[13] = $00 then
          case ATRVal[14] of
            $01: cardName := cardName + ': Mifare Standard 1K';
            $02: cardName := cardName + ': Mifare Standard 4K';
            $03: cardName := cardName + ': Mifare Ultra light';
            $04: cardName := cardName + ': SLE55R_XXXX';
            $06: cardName := cardName + ': SR176';
            $07: cardName := cardName + ': SRI X4K';
            $08: cardName := cardName + ': AT88RF020';
            $09: cardName := cardName + ': AT88SC0204CRF';
            $0A: cardName := cardName + ': AT88SC0808CRF';
            $0B: cardName := cardName + ': AT88SC1616CRF';
            $0C: cardName := cardName + ': AT88SC3216CRF';
            $0D: cardName := cardName + ': AT88SC6416CRF';
            $0E: cardName := cardName + ': SRF55V10P';
            $0F: cardName := cardName + ': SRF55V02P';
            $10: cardName := cardName + ': SRF55V10S';
            $11: cardName := cardName + ': SRF55V02S';
            $12: cardName := cardName + ': TAG IT';
            $13: cardName := cardName + ': LRI512';
            $14: cardName := cardName + ': ICODESLI';
            $15: cardName := cardName + ': TEMPSENS';
            $16: cardName := cardName + ': I.CODE1';
            $17: cardName := cardName + ': PicoPass 2K';
            $18: cardName := cardName + ': PicoPass 2KS';
            $19: cardName := cardName + ': PicoPass 16K';
            $1A: cardName := cardName + ': PicoPass 16KS';
            $1B: cardName := cardName + ': PicoPass 16K(8x2)';
            $1C: cardName := cardName + ': PicoPass 16KS(8x2)';

            $1D: cardName := cardName + ': PicoPass 32KS(16+16)';
            $1E: cardName := cardName + ': PicoPass 32KS(16+8x2)';
            $1F: cardName := cardName + ': PicoPass 32KS(8x2+16)';
            $20: cardName := cardName + ': PicoPass 32KS(8x2+8x2)';
            $21: cardName := cardName + ': LRI64';
            $22: cardName := cardName + ': I.CODE UID';
            $23: cardName := cardName + ': I.CODE EPC';
            $24: cardName := cardName + ': LRI12';
            $25: cardName := cardName + ': LRI128';
            $26: cardName := cardName + ': Mifare Mini';
          end
        else
          if ATRVal[13] = $FF then
            case ATRVal[14] of
              $09: cardName := cardName + ': Mifare Mini';
            end;
      DisplayOut(3, 0, cardName + ' is detected.');
      Exit;
    end;
   end;

  // 4.2. Mifare DESFire card using ISO 14443 Part 4
  if integer(ATRLen) = 11 then begin
    RIDVal := '';
    for indx := 4 to 9 do
      RIDVal := RIDVal + Format('%.02X',[ATRVal[indx]]);
    if (RIDVal = '067577810280') then
      DisplayOut(3, 0, 'Mifare DESFire is detected.');
  end;

  // 4.3. Other cards using ISO 14443 Part 4
  if integer(ATRLen) = 17 then begin
    RIDVal := '';
    for indx := 4 to 15 do
      RIDVal := RIDVal + Format('%.02X',[ATRVal[indx]]);
    if (RIDVal = '50122345561253544E3381C3') then
      DisplayOut(3, 0, 'ST19XRC8E is detected.');
  end;

  // 4.4. Other cards using ISO 14443 Type A or B
  if lATRStr = '3B8X800150' then
    DisplayOut(3, 0, 'ISO 14443B is detected.')
  else
    if sATRStr = '3B8X8001' then
      DisplayOut(3, 0, 'ISO 14443A is detected.');

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

procedure TMainGetATR.bInitClick(Sender: TObject);
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

procedure TMainGetATR.bConnectClick(Sender: TObject);
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

end;

procedure TMainGetATR.bResetClick(Sender: TObject);
begin

  if connActive then
    retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
  retCode := SCardReleaseContext(hCard);
  InitMenu();

end;

procedure TMainGetATR.bQuitClick(Sender: TObject);
begin

  Application.Terminate;

end;

procedure TMainGetATR.FormActivate(Sender: TObject);
begin
  InitMenu();
end;

procedure TMainGetATR.bGetAtrClick(Sender: TObject);
var ReaderLen, dwState: ^DWORD;
    tmpStr: String;
    indx: Integer;
    tmpWord: DWORD;
begin

  DisplayOut(0, 0, 'Invoke SCardStatus');
  // 1. Invoke SCardStatus using hCard handle
  //    and valid reader name
  tmpWord := 32;
  ATRLen := @tmpWord;
  retCode := SCardStatusA(hCard,
                         PChar(cbReader.Text),
                         @ReaderLen,
                         @dwState,
                         @dwActProtocol,
                         @ATRVal,
                         @ATRLen);
  if retCode <> SCARD_S_SUCCESS then
    DisplayOut(1, retCode, '')
  else begin
    // 2. Format ATRVal returned and display string as ATR value
    tmpStr := 'ATR Length: ' + InttoStr(integer(ATRLen));
    DisplayOut(3, 0, tmpStr);
    tmpStr := 'ATR Value: ';
    for indx := 0 to integer(ATRLen)-1 do
      tmpStr := tmpStr + Format('%.02X ',[ATRVal[indx]]);
    DisplayOut(3, 0, tmpStr);

    // 3. Interpret dwActProtocol returned and display as active protocol
    tmpStr := 'Active Protocol: ';
    case integer(dwActProtocol) of
      1: tmpStr := tmpStr + 'T=0';
      2: begin
           if AnsiPos('ACR128U PICC', cbReader.Text) <> 0 then
             tmpStr := 'T=CL'
           else
             tmpStr := 'T=1';
         end;
    else
      tmpStr := 'No protocol is defined.';
    end;
    DisplayOut(3, 0, tmpStr);

    InterpretATR();
    
  end;

end;

procedure TMainGetATR.bClearClick(Sender: TObject);
begin

  mMsg.Clear;
  
end;

end.
