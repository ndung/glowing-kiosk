//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              MainConfigureATR.frm
//
//  Description:       This sample program outlines the steps on how to
//                     change the ATR of a smart card using the PC/SC platform.
//                     You can also change the Card Baud Rate and the Historical Bytes of the card.
//
// NOTE: After you update the ATR you have to initialize and connect to the device
//       before you can see the updated ATR.
//       Historical Bytes valid value must be 0 to 9 and A,B,C,D,E,F only. e.g.(11,99,AE,AA,FF etc)
//			 If historical byte is leave blank the program will assume it as 00. This number and letter are hex value.
//
//Author:            Fernando G. Robles
//
//Date:              November 10, 2005
//
//Revision Trail:   (Date/Author/Description)

//=====================================================================

unit GetATR;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ACSModule, ComCtrls;

Const MAX_BUFFER_LEN    = 256;
Const INVALID_SW1SW2    = -450;

type
  TMainConfigureATR = class(TForm)
    Label1: TLabel;
    cbReader: TComboBox;
    bConnect: TButton;
    bATR: TButton;
    bReset: TButton;
    bQuit: TButton;
    bInit: TButton;
    bUpdate: TButton;
    mMsg: TRichEdit;
    cbo_baud: TComboBox;
    cbo_byte: TComboBox;
    Edit1: TEdit; // No. of Historical Bytes
    Edit2: TEdit; // Card Baud Rate (2nd character only)
    Edit3: TEdit; // Historical Byte
    Edit4: TEdit; // Historical Byte
    Edit5: TEdit; // Historical Byte
    Edit6: TEdit; // Historical Byte
    Edit7: TEdit; // Historical Byte
    Edit8: TEdit; // Historical Byte
    Edit9: TEdit; // Historical Byte
    Edit10: TEdit; // Historical Byte
    Edit11: TEdit; // Historical Byte
    Edit12: TEdit; // Historical Byte
    Edit13: TEdit; // Historical Byte
    Edit14: TEdit; // Historical Byte
    Edit15: TEdit; // Historical Byte
    Edit16: TEdit; // Historical Byte
    Edit17: TEdit; // Historical Byte
    Label4: TLabel;
    Label5: TLabel;
    Label6: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    procedure bQuitClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure bInitClick(Sender: TObject);
    procedure bConnectClick(Sender: TObject);
    procedure bResetClick(Sender: TObject);
    procedure bATRClick(Sender: TObject);
    procedure cbReaderChange(Sender: TObject);
    procedure Edit2Change(Sender: TObject);
    procedure Edit1Change(Sender: TObject);
    procedure cbo_baudChange(Sender: TObject);
    procedure cbo_byteChange(Sender: TObject);
    procedure cbo_baudClick(Sender: TObject);
    procedure bUpdateClick(Sender: TObject);
    procedure Edit3Change(Sender: TObject);
    procedure Edit4Change(Sender: TObject);
    procedure Edit5Change(Sender: TObject);
    procedure Edit6Change(Sender: TObject);
    procedure Edit7Change(Sender: TObject);
    procedure Edit8Change(Sender: TObject);
    procedure Edit9Change(Sender: TObject);
    procedure Edit10Change(Sender: TObject);
    procedure Edit11Change(Sender: TObject);
    procedure Edit12Change(Sender: TObject);
    procedure Edit13Change(Sender: TObject);
    procedure Edit14Change(Sender: TObject);
    procedure Edit15Change(Sender: TObject);
    procedure Edit16Change(Sender: TObject);
    procedure Edit17Change(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  MainConfigureATR  : TMainConfigureATR;
  hContext    : SCARDCONTEXT;
  hCard       : SCARDCONTEXT;
  ioRequest   : SCARD_IO_REQUEST;
  retCode     : Integer;
  dwActProtocol, BufferLen  : DWORD;
  SendBuff, RecvBuff        : array [0..262] of Byte;
  HByteArray        : array [0..14] of string;
  SendLen, RecvLen          : DWORD;
  Buffer      : array [0..MAX_BUFFER_LEN] of char;
  ConnActive   : Boolean;

procedure InitMenu();
procedure AddButtons();
procedure DisplayOut(mType: integer; PrintText: string);
procedure ClearBuffers();
function SendAPDUandDisplay(SendType: integer; ApduIn: string): integer;
function SubmitIC(): integer;
function selectFile(HiAddr: Byte; LoAddr: Byte): integer;
function writeRecord(RecNo: Byte; DataLen: Byte; DataIn:array of Byte): integer;

implementation

{$R *.dfm}

procedure InitMenu();
begin

  //Initialize all objects and form.

  MainConfigureATR.cbReader.Clear;
  MainConfigureATR.mMsg.Clear;
  DisplayOut(0, 'Program ready');
  MainConfigureATR.cbReader.Enabled := False;
  MainConfigureATR.bInit.Enabled := True;
  MainConfigureATR.bConnect.Enabled := False;
  MainConfigureATR.bATR.Enabled := False;
  MainConfigureATR.bReset.Enabled := False;
  MainConfigureATR.cbo_baud.Enabled := false;
  MainConfigureATR.cbo_byte.Enabled := false;
  MainConfigureATR.bUpdate.Enabled := false;

  MainConfigureATR.cbo_baud.ItemIndex   := -1;
  MainConfigureATR.cbo_byte.ItemIndex   := -1;
  MainConfigureATR.edit1.Text:= '';
  MainConfigureATR.edit2.Text:= '';





end;

procedure AddButtons();
begin

  MainConfigureATR.cbReader.Enabled := True;
  MainConfigureATR.bInit.Enabled := False;
  MainConfigureATR.bConnect.Enabled := True;
  MainConfigureATR.bReset.Enabled := True;

end;

procedure DisplayOut(mType: integer; PrintText: string);
begin

  //Function that display all messages to the RichEdit object

  case mType of
    0: MainConfigureATR.mMsg.SelAttributes.Color := clTeal;      // Notifications
    1: MainConfigureATR.mMsg.SelAttributes.Color := clRed;       // Error Messages
    2..3: MainConfigureATR.mMsg.SelAttributes.Color := clBlack;
  end;
  case mType of
    2: begin
       PrintText := '< ' + PrintText;                      // Input data
       end;
    3: begin
       PrintText := '> ' + PrintText;                      // Output data
       end;
  end;
  MainConfigureATR.mMsg.Lines.Add(PrintText);
  MainConfigureATR.mMsg.SelAttributes.Color := clBlack;

end;

procedure TMainConfigureATR.bQuitClick(Sender: TObject);
begin

  if ConnActive then
    begin
      retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
      ConnActive := False;
    end;
  retCode := SCardReleaseContext(hCard);
  Application.Terminate;

end;

procedure TMainConfigureATR.FormActivate(Sender: TObject);
begin

  InitMenu();

end;

procedure TMainConfigureATR.bInitClick(Sender: TObject);
begin

  //Initialize Function

  // 1. Establish context and obtain hContext handle
  retCode := SCardEstablishContext(SCARD_SCOPE_USER,
                                   nil,
                                   nil,
                                   @hContext);
  if retCode <> SCARD_S_SUCCESS then
    begin
      DisplayOut(1, GetScardErrMsg(retCode));
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
      DisplayOut(1, GetScardErrMsg(retCode));
      Exit;
    end
  else
    DisplayOut(0, 'Select reader, insert mcu card and connect.');

  MainConfigureATR.cbReader.Clear;;
  LoadListToControl(MainConfigureATR.cbReader,@buffer,bufferLen);
  MainConfigureATR.cbReader.ItemIndex := 0;
  MainConfigureATR.cbo_baud.ItemIndex := -1;
  MainConfigureATR.cbo_byte.ItemIndex := -1;
  AddButtons();

end;

procedure TMainConfigureATR.bConnectClick(Sender: TObject);
begin

  if ConnActive then
  begin
    DisplayOut(0, 'Connection is already active.');
    Exit;
  end;

  DisplayOut(2, 'Invoke SCardConnect');
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
      DisplayOut(1, GetScardErrMsg(retCode));
      ConnActive := False;
      Exit;
    end
  else
    DisplayOut(0, 'Successful connection to ' + cbReader.Text);

  ConnActive := True;
  MainConfigureATR.bATR.Enabled := True;

end;

procedure TMainConfigureATR.bResetClick(Sender: TObject);
begin

  if ConnActive then
    begin
      retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
      ConnActive := False;
    end;

  retCode := SCardReleaseContext(hCard);
  InitMenu();

end;

procedure TMainConfigureATR.bATRClick(Sender: TObject);
var ReaderLen, dwState, ATRLen: ^DWORD;
    tmpStr: String;
    ATRVal: array [0..128] of byte;
    indx: Integer;
    tmpWord: DWORD;
begin

  //Get ATR Function.

  DisplayOut(2, 'Invoke SCardStatus');
  // 1. Invoke SCardStatus using hCard handle
  //    and valid reader name
  tmpWord := 32;
  ATRLen := @tmpWord;
  ReaderLen := 0;
  dwState := 0;
  retCode := SCardStatusA(hCard,
                         PChar(cbReader.Text),
                         @ReaderLen,
                         @dwState,
                         @dwActProtocol,
                         @ATRVal,
                         @ATRLen);
  if retCode <> SCARD_S_SUCCESS then
    begin
      DisplayOut(1, GetScardErrMsg(retCode));
      ConnActive := False;
      Exit;
    end;

  // 2. Format ATRVal returned and display string as ATR value
  tmpStr := '';
  for indx := 0 to integer(ATRLen)-1 do
    begin
      tmpStr := tmpStr + Format('%.02X ',[ATRVal[indx]]);
    end;
  DisplayOut(3, Format('ATR Value: %s',[tmpStr]));


     edit1.Text := Format('%.02X ',[ATRVal[1]]);
     edit2.Text := Format('%.02X ',[ATRVal[2]]);



  // 3. Interpret dwActProtocol returned and display as active protocol
  tmpStr := '';
  case integer(dwActProtocol) of
    1: tmpStr := 'T=0';
    2: tmpStr := 'T=1';
    else
      tmpStr := 'No protocol is defined.';
    end;
  DisplayOut(3, Format('Active Protocol: %s',[tmpStr]));

  cbo_baud.Enabled := true;
  cbo_byte.Enabled := true;
  bUpdate.Enabled := true;
end;

procedure TMainConfigureATR.cbReaderChange(Sender: TObject);
begin

  bATR.Enabled := False;
  if ConnActive then
  begin
    retCode := SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
    ConnActive := False;
  end;

end;

procedure TMainConfigureATR.Edit2Change(Sender: TObject);
begin

      //edit2.text is the value of card baud rate written on the card.

      if trim(edit2.Text) = '11' then
      begin
        cbo_baud.ItemIndex := 0;
      end
      else
      if trim(edit2.Text) = '92' then
      begin
        cbo_baud.ItemIndex := 1;
      end
      else
      if trim(edit2.Text) = '93' then
      begin
        cbo_baud.ItemIndex := 2;
      end
      else
      if trim(edit2.Text) = '94' then
      begin
        cbo_baud.ItemIndex := 3;
      end
      else
      if trim(edit2.Text) = '95' then
      begin
        cbo_baud.ItemIndex := 4;
      end;


end;

procedure TMainConfigureATR.Edit1Change(Sender: TObject);
begin

    // The 2nd character of edit1.text is the Number of
    //historical bytes to be written on the ACOS card.

    if trim(edit1.Text) = 'B0' then
      begin
        cbo_byte.ItemIndex := 0;
      end
      else
    if trim(edit1.Text) = 'B1' then
      begin
        cbo_byte.ItemIndex := 1;
      end
      else
    if trim(edit1.Text) = 'B2' then
      begin
        cbo_byte.ItemIndex := 2;
      end
      else
    if trim(edit1.Text) = 'B3' then
      begin
        cbo_byte.ItemIndex := 3;
      end
      else
    if trim(edit1.Text) = 'B4' then
      begin
        cbo_byte.ItemIndex := 4;
      end
      else
    if trim(edit1.Text) = 'B5' then
      begin
        cbo_byte.ItemIndex := 5;
      end
      else
    if trim(edit1.Text) = 'B6' then
      begin
        cbo_byte.ItemIndex := 6;
      end
      else
    if trim(edit1.Text) = 'B7' then
      begin
        cbo_byte.ItemIndex := 7;
      end
      else
    if trim(edit1.Text) = 'B8' then
      begin
        cbo_byte.ItemIndex := 8;
      end
      else
    if trim(edit1.Text) = 'B9' then
      begin
        cbo_byte.ItemIndex := 9;
      end
      else
    if trim(edit1.Text) = 'BA' then
      begin
        cbo_byte.ItemIndex := 10;
      end
      else
    if trim(edit1.Text) = 'BB' then
      begin
        cbo_byte.ItemIndex := 11;
      end
      else
    if trim(edit1.Text) = 'BC' then
      begin
        cbo_byte.ItemIndex := 12;
      end
      else
    if trim(edit1.Text) = 'BD' then
      begin
        cbo_byte.ItemIndex := 13;
      end
      else
    if trim(edit1.Text) = 'BF' then
      begin
        cbo_byte.ItemIndex := 15;
      end
    else
      begin
        if cbo_byte.ItemIndex = 16 then
            cbo_byte.ItemIndex := 16
        else
        if trim(edit1.Text) = 'BE' then
            cbo_byte.ItemIndex := 14;
      end;

    //Edit3.text to Edit17.text contains the historical bytes to be written
    //in the ACOS card.

    Edit3.Text := '';
    Edit4.Text := '';
    Edit5.Text := '';
    Edit6.Text := '';
    Edit7.Text := '';
    Edit8.Text := '';
    Edit9.Text := '';
    Edit10.Text := '';
    Edit11.Text := '';
    Edit12.Text := '';
    Edit13.Text := '';
    Edit14.Text := '';
    Edit15.Text := '';
    Edit16.Text := '';
    Edit17.Text := '';

    Edit3.Color := clWhite;
    Edit4.Color := clWhite;
    Edit5.Color := clWhite;
    Edit6.Color := clWhite;
    Edit7.Color := clWhite;
    Edit8.Color := clWhite;
    Edit9.Color := clWhite;
    Edit10.Color := clWhite;
    Edit11.Color := clWhite;
    Edit12.Color := clWhite;
    Edit13.Color := clWhite;
    Edit14.Color := clWhite;
    Edit15.Color := clWhite;
    Edit16.Color := clWhite;
    Edit17.Color := clWhite;

  if cbo_byte.ItemIndex = 0 then
    begin
      Edit3.Enabled := false;
      Edit4.Enabled := false;
      Edit5.Enabled := false;
      Edit6.Enabled := false;
      Edit7.Enabled := false;
      Edit8.Enabled := false;
      Edit9.Enabled := false;
      Edit10.Enabled := false;
      Edit11.Enabled := false;
      Edit12.Enabled := false;
      Edit13.Enabled := false;
      Edit14.Enabled := false;
      Edit15.Enabled := false;
      Edit16.Enabled := false;
      Edit17.Enabled := false;


      Edit3.Color := clSilver;
      Edit4.Color := clSilver;
      Edit5.Color := clSilver;
      Edit6.Color := clSilver;
      Edit7.Color := clSilver;
      Edit8.Color := clSilver;
      Edit9.Color := clSilver;
      Edit10.Color := clSilver;
      Edit11.Color := clSilver;
      Edit12.Color := clSilver;
      Edit13.Color := clSilver;
      Edit14.Color := clSilver;
      Edit15.Color := clSilver;
      Edit16.Color := clSilver;
      Edit17.Color := clSilver;
    end
  else
  if cbo_byte.ItemIndex = 1 then
    begin

      Edit3.Enabled := true;
      Edit4.Enabled := false;
      Edit5.Enabled := false;
      Edit6.Enabled := false;
      Edit7.Enabled := false;
      Edit8.Enabled := false;
      Edit9.Enabled := false;
      Edit10.Enabled := false;
      Edit11.Enabled := false;
      Edit12.Enabled := false;
      Edit13.Enabled := false;
      Edit14.Enabled := false;
      Edit15.Enabled := false;
      Edit16.Enabled := false;
      Edit17.Enabled := false;


      Edit3.Color := clWhite;
      Edit4.Color := clSilver;
      Edit5.Color := clSilver;
      Edit6.Color := clSilver;
      Edit7.Color := clSilver;
      Edit8.Color := clSilver;
      Edit9.Color := clSilver;
      Edit10.Color := clSilver;
      Edit11.Color := clSilver;
      Edit12.Color := clSilver;
      Edit13.Color := clSilver;
      Edit14.Color := clSilver;
      Edit15.Color := clSilver;
      Edit16.Color := clSilver;
      Edit17.Color := clSilver;

    end
  else
  if cbo_byte.ItemIndex = 2 then
    begin
      Edit3.Enabled := true;
      Edit4.Enabled := true;
      Edit5.Enabled := false;
      Edit6.Enabled := false;
      Edit7.Enabled := false;
      Edit8.Enabled := false;
      Edit9.Enabled := false;
      Edit10.Enabled := false;
      Edit11.Enabled := false;
      Edit12.Enabled := false;
      Edit13.Enabled := false;
      Edit14.Enabled := false;
      Edit15.Enabled := false;
      Edit16.Enabled := false;
      Edit17.Enabled := false;


      Edit3.Color := clWhite;
      Edit4.Color := clWhite;
      Edit5.Color := clSilver;
      Edit6.Color := clSilver;
      Edit7.Color := clSilver;
      Edit8.Color := clSilver;
      Edit9.Color := clSilver;
      Edit10.Color := clSilver;
      Edit11.Color := clSilver;
      Edit12.Color := clSilver;
      Edit13.Color := clSilver;
      Edit14.Color := clSilver;
      Edit15.Color := clSilver;
      Edit16.Color := clSilver;
      Edit17.Color := clSilver;

    end
  else
  if cbo_byte.ItemIndex = 3 then
    begin
      Edit3.Enabled := true;
      Edit4.Enabled := true;
      Edit5.Enabled := true;
      Edit6.Enabled := false;
      Edit7.Enabled := false;
      Edit8.Enabled := false;
      Edit9.Enabled := false;
      Edit10.Enabled := false;
      Edit11.Enabled := false;
      Edit12.Enabled := false;
      Edit13.Enabled := false;
      Edit14.Enabled := false;
      Edit15.Enabled := false;
      Edit16.Enabled := false;
      Edit17.Enabled := false;


      Edit3.Color := clWhite;
      Edit4.Color := clWhite;
      Edit5.Color := clWhite;
      Edit6.Color := clSilver;
      Edit7.Color := clSilver;
      Edit8.Color := clSilver;
      Edit9.Color := clSilver;
      Edit10.Color := clSilver;
      Edit11.Color := clSilver;
      Edit12.Color := clSilver;
      Edit13.Color := clSilver;
      Edit14.Color := clSilver;
      Edit15.Color := clSilver;
      Edit16.Color := clSilver;
      Edit17.Color := clSilver;
    end
  else
  if cbo_byte.ItemIndex = 4 then
    begin
      Edit3.Enabled := true;
      Edit4.Enabled := true;
      Edit5.Enabled := true;
      Edit6.Enabled := true;
      Edit7.Enabled := false;
      Edit8.Enabled := false;
      Edit9.Enabled := false;
      Edit10.Enabled := false;
      Edit11.Enabled := false;
      Edit12.Enabled := false;
      Edit13.Enabled := false;
      Edit14.Enabled := false;
      Edit15.Enabled := false;
      Edit16.Enabled := false;
      Edit17.Enabled := false;


      Edit3.Color := clWhite;
      Edit4.Color := clWhite;
      Edit5.Color := clWhite;
      Edit6.Color := clWhite;
      Edit7.Color := clSilver;
      Edit8.Color := clSilver;
      Edit9.Color := clSilver;
      Edit10.Color := clSilver;
      Edit11.Color := clSilver;
      Edit12.Color := clSilver;
      Edit13.Color := clSilver;
      Edit14.Color := clSilver;
      Edit15.Color := clSilver;
      Edit16.Color := clSilver;
      Edit17.Color := clSilver;

    end
  else
  if cbo_byte.ItemIndex = 5 then
    begin
      Edit3.Enabled := true;
      Edit4.Enabled := true;
      Edit5.Enabled := true;
      Edit6.Enabled := true;
      Edit7.Enabled := true;
      Edit8.Enabled := false;
      Edit9.Enabled := false;
      Edit10.Enabled := false;
      Edit11.Enabled := false;
      Edit12.Enabled := false;
      Edit13.Enabled := false;
      Edit14.Enabled := false;
      Edit15.Enabled := false;
      Edit16.Enabled := false;
      Edit17.Enabled := false;


      Edit3.Color := clWhite;
      Edit4.Color := clWhite;
      Edit5.Color := clWhite;
      Edit6.Color := clWhite;
      Edit7.Color := clWhite;
      Edit8.Color := clSilver;
      Edit9.Color := clSilver;
      Edit10.Color := clSilver;
      Edit11.Color := clSilver;
      Edit12.Color := clSilver;
      Edit13.Color := clSilver;
      Edit14.Color := clSilver;
      Edit15.Color := clSilver;
      Edit16.Color := clSilver;
      Edit17.Color := clSilver;
    end
  else
  if cbo_byte.ItemIndex = 6 then
    begin
      Edit3.Enabled := true;
      Edit4.Enabled := true;
      Edit5.Enabled := true;
      Edit6.Enabled := true;
      Edit7.Enabled := true;
      Edit8.Enabled := true;
      Edit9.Enabled := false;
      Edit10.Enabled := false;
      Edit11.Enabled := false;
      Edit12.Enabled := false;
      Edit13.Enabled := false;
      Edit14.Enabled := false;
      Edit15.Enabled := false;
      Edit16.Enabled := false;
      Edit17.Enabled := false;


      Edit3.Color := clWhite;
      Edit4.Color := clWhite;
      Edit5.Color := clWhite;
      Edit6.Color := clWhite;
      Edit7.Color := clWhite;
      Edit8.Color := clWhite;
      Edit9.Color := clSilver;
      Edit10.Color := clSilver;
      Edit11.Color := clSilver;
      Edit12.Color := clSilver;
      Edit13.Color := clSilver;
      Edit14.Color := clSilver;
      Edit15.Color := clSilver;
      Edit16.Color := clSilver;
      Edit17.Color := clSilver;

    end
  else
  if cbo_byte.ItemIndex = 7 then
    begin
      Edit3.Enabled := true;
      Edit4.Enabled := true;
      Edit5.Enabled := true;
      Edit6.Enabled := true;
      Edit7.Enabled := true;
      Edit8.Enabled := true;
      Edit9.Enabled := true;
      Edit10.Enabled := false;
      Edit11.Enabled := false;
      Edit12.Enabled := false;
      Edit13.Enabled := false;
      Edit14.Enabled := false;
      Edit15.Enabled := false;
      Edit16.Enabled := false;
      Edit17.Enabled := false;


      Edit3.Color := clWhite;
      Edit4.Color := clWhite;
      Edit5.Color := clWhite;
      Edit6.Color := clWhite;
      Edit7.Color := clWhite;
      Edit8.Color := clWhite;
      Edit9.Color := clWhite;
      Edit10.Color := clSilver;
      Edit11.Color := clSilver;
      Edit12.Color := clSilver;
      Edit13.Color := clSilver;
      Edit14.Color := clSilver;
      Edit15.Color := clSilver;
      Edit16.Color := clSilver;
      Edit17.Color := clSilver;
    end
  else
  if cbo_byte.ItemIndex = 8 then
    begin
      Edit3.Enabled := true;
      Edit4.Enabled := true;
      Edit5.Enabled := true;
      Edit6.Enabled := true;
      Edit7.Enabled := true;
      Edit8.Enabled := true;
      Edit9.Enabled := true;
      Edit10.Enabled := true;
      Edit11.Enabled := false;
      Edit12.Enabled := false;
      Edit13.Enabled := false;
      Edit14.Enabled := false;
      Edit15.Enabled := false;
      Edit16.Enabled := false;
      Edit17.Enabled := false;


      Edit3.Color := clWhite;
      Edit4.Color := clWhite;
      Edit5.Color := clWhite;
      Edit6.Color := clWhite;
      Edit7.Color := clWhite;
      Edit8.Color := clWhite;
      Edit9.Color := clWhite;
      Edit10.Color := clWhite;
      Edit11.Color := clSilver;
      Edit12.Color := clSilver;
      Edit13.Color := clSilver;
      Edit14.Color := clSilver;
      Edit15.Color := clSilver;
      Edit16.Color := clSilver;
      Edit17.Color := clSilver;
    end
  else
  if cbo_byte.ItemIndex = 9 then
    begin
      Edit3.Enabled := true;
      Edit4.Enabled := true;
      Edit5.Enabled := true;
      Edit6.Enabled := true;
      Edit7.Enabled := true;
      Edit8.Enabled := true;
      Edit9.Enabled := true;
      Edit10.Enabled := true;
      Edit11.Enabled := true;
      Edit12.Enabled := false;
      Edit13.Enabled := false;
      Edit14.Enabled := false;
      Edit15.Enabled := false;
      Edit16.Enabled := false;
      Edit17.Enabled := false;


      Edit3.Color := clWhite;
      Edit4.Color := clWhite;
      Edit5.Color := clWhite;
      Edit6.Color := clWhite;
      Edit7.Color := clWhite;
      Edit8.Color := clWhite;
      Edit9.Color := clWhite;
      Edit10.Color := clWhite;
      Edit11.Color := clWhite;
      Edit12.Color := clSilver;
      Edit13.Color := clSilver;
      Edit14.Color := clSilver;
      Edit15.Color := clSilver;
      Edit16.Color := clSilver;
      Edit17.Color := clSilver;
    end
  else
  if cbo_byte.ItemIndex = 10 then
    begin
      Edit3.Enabled := true;
      Edit4.Enabled := true;
      Edit5.Enabled := true;
      Edit6.Enabled := true;
      Edit7.Enabled := true;
      Edit8.Enabled := true;
      Edit9.Enabled := true;
      Edit10.Enabled := true;
      Edit11.Enabled := true;
      Edit12.Enabled := true;
      Edit13.Enabled := false;
      Edit14.Enabled := false;
      Edit15.Enabled := false;
      Edit16.Enabled := false;
      Edit17.Enabled := false;


      Edit3.Color := clWhite;
      Edit4.Color := clWhite;
      Edit5.Color := clWhite;
      Edit6.Color := clWhite;
      Edit7.Color := clWhite;
      Edit8.Color := clWhite;
      Edit9.Color := clWhite;
      Edit10.Color := clWhite;
      Edit11.Color := clWhite;
      Edit12.Color := clWhite;
      Edit13.Color := clSilver;
      Edit14.Color := clSilver;
      Edit15.Color := clSilver;
      Edit16.Color := clSilver;
      Edit17.Color := clSilver;
    end
  else
  if cbo_byte.ItemIndex = 11 then
    begin
      Edit3.Enabled := true;
      Edit4.Enabled := true;
      Edit5.Enabled := true;
      Edit6.Enabled := true;
      Edit7.Enabled := true;
      Edit8.Enabled := true;
      Edit9.Enabled := true;
      Edit10.Enabled := true;
      Edit11.Enabled := true;
      Edit12.Enabled := true;
      Edit13.Enabled := true;
      Edit14.Enabled := false;
      Edit15.Enabled := false;
      Edit16.Enabled := false;
      Edit17.Enabled := false;


      Edit3.Color := clWhite;
      Edit4.Color := clWhite;
      Edit5.Color := clWhite;
      Edit6.Color := clWhite;
      Edit7.Color := clWhite;
      Edit8.Color := clWhite;
      Edit9.Color := clWhite;
      Edit10.Color := clWhite;
      Edit11.Color := clWhite;
      Edit12.Color := clWhite;
      Edit13.Color := clWhite;
      Edit14.Color := clSilver;
      Edit15.Color := clSilver;
      Edit16.Color := clSilver;
      Edit17.Color := clSilver;
    end
  else
  if cbo_byte.ItemIndex = 12 then
    begin
      Edit3.Enabled := true;
      Edit4.Enabled := true;
      Edit5.Enabled := true;
      Edit6.Enabled := true;
      Edit7.Enabled := true;
      Edit8.Enabled := true;
      Edit9.Enabled := true;
      Edit10.Enabled := true;
      Edit11.Enabled := true;
      Edit12.Enabled := true;
      Edit13.Enabled := true;
      Edit14.Enabled := true;
      Edit15.Enabled := false;
      Edit16.Enabled := false;
      Edit17.Enabled := false;


      Edit3.Color := clWhite;
      Edit4.Color := clWhite;
      Edit5.Color := clWhite;
      Edit6.Color := clWhite;
      Edit7.Color := clWhite;
      Edit8.Color := clWhite;
      Edit9.Color := clWhite;
      Edit10.Color := clWhite;
      Edit11.Color := clWhite;
      Edit12.Color := clWhite;
      Edit13.Color := clWhite;
      Edit14.Color := clWhite;
      Edit15.Color := clSilver;
      Edit16.Color := clSilver;
      Edit17.Color := clSilver;
    end
  else
  if cbo_byte.ItemIndex = 13 then
    begin
      Edit3.Enabled := true;
      Edit4.Enabled := true;
      Edit5.Enabled := true;
      Edit6.Enabled := true;
      Edit7.Enabled := true;
      Edit8.Enabled := true;
      Edit9.Enabled := true;
      Edit10.Enabled := true;
      Edit11.Enabled := true;
      Edit12.Enabled := true;
      Edit13.Enabled := true;
      Edit14.Enabled := true;
      Edit15.Enabled := true;
      Edit16.Enabled := false;
      Edit17.Enabled := false;


      Edit3.Color := clWhite;
      Edit4.Color := clWhite;
      Edit5.Color := clWhite;
      Edit6.Color := clWhite;
      Edit7.Color := clWhite;
      Edit8.Color := clWhite;
      Edit9.Color := clWhite;
      Edit10.Color := clWhite;
      Edit11.Color := clWhite;
      Edit12.Color := clWhite;
      Edit13.Color := clWhite;
      Edit14.Color := clWhite;
      Edit15.Color := clWhite;
      Edit16.Color := clSilver;
      Edit17.Color := clSilver;
    end
  else
  if cbo_byte.ItemIndex = 14 then
    begin
        Edit3.Enabled := true;
      Edit4.Enabled := true;
      Edit5.Enabled := true;
      Edit6.Enabled := true;
      Edit7.Enabled := true;
      Edit8.Enabled := true;
      Edit9.Enabled := true;
      Edit10.Enabled := true;
      Edit11.Enabled := true;
      Edit12.Enabled := true;
      Edit13.Enabled := true;
      Edit14.Enabled := true;
      Edit15.Enabled := true;
      Edit16.Enabled := true;
      Edit17.Enabled := false;


      Edit3.Color := clWhite;
      Edit4.Color := clWhite;
      Edit5.Color := clWhite;
      Edit6.Color := clWhite;
      Edit7.Color := clWhite;
      Edit8.Color := clWhite;
      Edit9.Color := clWhite;
      Edit10.Color := clWhite;
      Edit11.Color := clWhite;
      Edit12.Color := clWhite;
      Edit13.Color := clWhite;
      Edit14.Color := clWhite;
      Edit15.Color := clWhite;
      Edit16.Color := clWhite;
      Edit17.Color := clSilver;
    end
  else
  if cbo_byte.ItemIndex = 15 then
    begin
      Edit3.Enabled := true;
      Edit4.Enabled := true;
      Edit5.Enabled := true;
      Edit6.Enabled := true;
      Edit7.Enabled := true;
      Edit8.Enabled := true;
      Edit9.Enabled := true;
      Edit10.Enabled := true;
      Edit11.Enabled := true;
      Edit12.Enabled := true;
      Edit13.Enabled := true;
      Edit14.Enabled := true;
      Edit15.Enabled := true;
      Edit16.Enabled := true;
      Edit17.Enabled := true;


      Edit3.Color := clWhite;
      Edit4.Color := clWhite;
      Edit5.Color := clWhite;
      Edit6.Color := clWhite;
      Edit7.Color := clWhite;
      Edit8.Color := clWhite;
      Edit9.Color := clWhite;
      Edit10.Color := clWhite;
      Edit11.Color := clWhite;
      Edit12.Color := clWhite;
      Edit13.Color := clWhite;
      Edit14.Color := clWhite;
      Edit15.Color := clWhite;
      Edit16.Color := clWhite;
      Edit17.Color := clWhite;
    end
  else
  if cbo_byte.ItemIndex = 16 then
    begin
      Edit3.Enabled := false;
      Edit4.Enabled := false;
      Edit5.Enabled := false;
      Edit6.Enabled := false;
      Edit7.Enabled := false;
      Edit8.Enabled := false;
      Edit9.Enabled := false;
      Edit10.Enabled := false;
      Edit11.Enabled := false;
      Edit12.Enabled := false;
      Edit13.Enabled := false;
      Edit14.Enabled := false;
      Edit15.Enabled := false;
      Edit16.Enabled := false;
      Edit17.Enabled := false;


      Edit3.Color := clSilver;
      Edit4.Color := clSilver;
      Edit5.Color := clSilver;
      Edit6.Color := clSilver;
      Edit7.Color := clSilver;
      Edit8.Color := clSilver;
      Edit9.Color := clSilver;
      Edit10.Color := clSilver;
      Edit11.Color := clSilver;
      Edit12.Color := clSilver;
      Edit13.Color := clSilver;
      Edit14.Color := clSilver;
      Edit15.Color := clSilver;
      Edit16.Color := clSilver;
      Edit17.Color := clSilver;

    end;



end;

procedure TMainConfigureATR.cbo_baudChange(Sender: TObject);
begin
  case cbo_baud.ItemIndex  of
    0: Edit2.Text := '11';
    1: Edit2.Text := '92';
    2: Edit2.Text := '93';
    3: Edit2.Text := '94';
    4: Edit2.Text := '95';

  end;
end;

procedure TMainConfigureATR.cbo_byteChange(Sender: TObject);
begin
    case cbo_byte.ItemIndex  of
    0: Edit1.Text := 'B0';
    1: Edit1.Text := 'B1';
    2: Edit1.Text := 'B2';
    3: Edit1.Text := 'B3';
    4: Edit1.Text := 'B4';
    5: Edit1.Text := 'B5';
    6: Edit1.Text := 'B6';
    7: Edit1.Text := 'B7';
    8: Edit1.Text := 'B8';
    9: Edit1.Text := 'B9';
    10: Edit1.Text := 'BA';
    11: Edit1.Text := 'BB';
    12: Edit1.Text := 'BC';
    13: Edit1.Text := 'BD';
    14: Edit1.Text := 'BE';
    15: Edit1.Text := 'BF';
    16: Edit1.Text := 'BE';

  end;






end;

procedure TMainConfigureATR.cbo_baudClick(Sender: TObject);
begin
  case cbo_baud.ItemIndex  of
    0: Edit2.Text := '11';
    1: Edit2.Text := '92';
    2: Edit2.Text := '93';
    3: Edit2.Text := '94';
    4: Edit2.Text := '95';

  end;
end;

function selectFile(HiAddr: Byte; LoAddr: Byte): integer;
// Select File function.
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

function writeRecord(RecNo: Byte; DataLen: Byte; DataIn:array of Byte): integer;
//Write Function
var indx: integer;
    tmpStr: String;
begin

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
//  if ACOSError(RecvBuff[0], RecvBuff[1]) then
//    begin
//      retCode := INVALID_SW1SW2;
//      writeRecord := retCode;
//      Exit;
//    end;
  if tmpStr <> '90 00 ' then
    begin
      displayOut(3, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      writeRecord := retCode;
      Exit;
    end;
  writeRecord := retCode;

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

function SendAPDUandDisplay(SendType: integer; ApduIn: string): integer;
//Function that execute APDU commands.
var tmpStr: string;
    indx: integer;
begin



  ioRequest.dwProtocol := dwActProtocol;
  ioRequest.cbPciLength := sizeof(SCARD_IO_REQUEST);
  DisplayOut(2, ApduIn);
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
//      DisplayOut(1, atos(retCode));
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
           DisplayOut(1, 'Return bytes are not acceptable.')
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
           DisplayOut(1,  'Return bytes are not acceptable.')
         else
           begin
             tmpStr := '';
             for indx := 0 to (RecvLen-3) do
               tmpStr := tmpStr + Format('%.2X ', [(RecvBuff[Indx])]);
           end;
         end;

      end;
      DisplayOut(3, Trim(tmpStr));
    end;
  SendAPDUandDisplay := retCode;

end;

function SubmitIC(): integer; //Submit IC Function
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
      displayOut(2, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      SubmitIC := retCode;
      Exit;
    end;
  SubmitIC := retCode;

end;

procedure TMainConfigureATR.bUpdateClick(Sender: TObject);
var tmpStr: String;
    indx: integer;
    num_historical_byte: integer;
    ctr: integer;
    tmpAPDU: array[0..35] of Byte;
    str_temp: String;
begin

  //Select File FF 07
  retCode := selectFile($FF, $07);
  if retCode <> SCARD_S_SUCCESS then
    Exit;
  tmpStr := '';
  for indx := 0 to 1 do
    tmpStr  := tmpStr + Format('%.2x',[RecvBuff[indx]]) + ' ';
  if tmpStr <> '90 00 ' then
    begin
      displayOut(2, 'The return string is invalid. Value: ' + tmpStr);
      retCode := INVALID_SW1SW2;
      Exit;
    end;


  // Submit IC Code
  retCode := SubmitIC();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

  num_historical_byte := cbo_byte.ItemIndex;

  tmpAPDU[0] := strtoint('$' + trim(Edit2.Text));    //Card Baud Rate parameter.

  If num_historical_byte = 16 Then
    tmpAPDU[1] := $FF             //Restoring to it original historical bytes
  Else
    tmpAPDU[1] := num_historical_byte;


  ctr := 2;

  If num_historical_byte < 16 Then
  begin

      for indx := 0 to (num_historical_byte-1) do
      begin
        if trim(hbytearray[indx]) = '' then
          hbytearray[indx] := '00';


      end;

      for indx := 0 to (num_historical_byte-1) do
      begin
        tmpAPDU[ctr] := strtoint('$' + hbytearray[indx]);
        ctr := ctr + 1;
      end;


  end;
  for indx := ctr to 35 do
    tmpAPDU[indx] := $00;

  retCode := writeRecord(0, 36, tmpAPDU);

end;

procedure TMainConfigureATR.Edit3Change(Sender: TObject);
begin
   hbytearray[0] := edit3.text;
end;

procedure TMainConfigureATR.Edit4Change(Sender: TObject);
begin
   hbytearray[1] := edit4.text;
end;

procedure TMainConfigureATR.Edit5Change(Sender: TObject);
begin
  hbytearray[2] := edit5.text;
end;

procedure TMainConfigureATR.Edit6Change(Sender: TObject);
begin
  hbytearray[3] := edit6.text;
end;

procedure TMainConfigureATR.Edit7Change(Sender: TObject);
begin
    hbytearray[4] := edit7.text;
end;

procedure TMainConfigureATR.Edit8Change(Sender: TObject);
begin
    hbytearray[5] := edit8.text;
end;

procedure TMainConfigureATR.Edit9Change(Sender: TObject);
begin
    hbytearray[6] := edit9.text;
end;

procedure TMainConfigureATR.Edit10Change(Sender: TObject);
begin
    hbytearray[7] := edit10.text;
end;

procedure TMainConfigureATR.Edit11Change(Sender: TObject);
begin
    hbytearray[8] := edit11.text;
end;

procedure TMainConfigureATR.Edit12Change(Sender: TObject);
begin
    hbytearray[9] := edit12.text;
end;

procedure TMainConfigureATR.Edit13Change(Sender: TObject);
begin
    hbytearray[10] := edit13.text;
end;

procedure TMainConfigureATR.Edit14Change(Sender: TObject);
begin
    hbytearray[11] := edit14.text;
end;

procedure TMainConfigureATR.Edit15Change(Sender: TObject);
begin
    hbytearray[12] := edit15.text;
end;

procedure TMainConfigureATR.Edit16Change(Sender: TObject);
begin
    hbytearray[13] := edit16.text;
end;

procedure TMainConfigureATR.Edit17Change(Sender: TObject);
begin
    hbytearray[14] := edit17.text;
end;

end.
