//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              CBinaryFilesinACOS3Dlg.cpp
//
//  Description:       This sample program outlines the steps on how to
//                     implement the binary file support in ACOS3-24K
//
//  Author:	           Wazer Emmanuel R. Benal
//
//  Date:	           June 23, 2008
//
//  Revision Trail:	   (Date/Author/Description)
//
//======================================================================

#include "stdafx.h"
#include "Binary Files in ACOS3.h"
#include "Binary Files in ACOS3Dlg.h"

//Define constants//////////////////////////////////////////////////
#define BLACK RGB(0, 0, 0)
#define RED RGB(255, 0, 0)
#define GREEN RGB(0, 0x99, 0)
#define MAX 262
#define INVALID_SW1SW2 450
////////////////////////////////////////////////////////////////////

//ACOS3 Binary Files Inlude File
#include "WINSCARD.h"

//Global Variables
	SCARDCONTEXT			hContext;
	SCARDHANDLE				hCard;
	unsigned long			dwActProtocol;
	DWORD					dwSend, dwRecv, size = 64;
	SCARD_IO_REQUEST		ioRequest;
	int						retCode;
    char					readerName [256];
	DWORD					SendLen, RecvLen, ByteRet;
	BYTE					SendBuff[262], RecvBuff[262];
	SCARD_IO_REQUEST		IO_REQ;
	unsigned char			HByteArray[16];
	int						reqtype;
	bool					autodet = false, validATS;
	CBinaryFilesinACOS3Dlg	*pThis = NULL;

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

void getBinaryData();
void ClearBuffers();
static CString GetScardErrMsg( int code );
void DisplayOut( CString str, COLORREF color );
void Initializer();
int SubmitIC();
int SendAPDU( int SendType );
int selectFile( BYTE HiAddr, BYTE LoAddr );
int readRecord( BYTE RecNo, BYTE DataLen );
int writeRecord( int casetype, BYTE RecNo, BYTE DataLen, BYTE DataIn[] );
int readBinary( BYTE HiByte, BYTE LoByte, BYTE DataLen );
int writeBinary( int casetype, BYTE HiByte, BYTE LoByte, BYTE maxDataLen, BYTE DataLen, BYTE DataIn[] );
int HexCheck( char data1, char data2 );

//Clears the buffers of any data
void ClearBuffers()
{

	int index;
	
	for( index = 0; index <= 262; index++ )
	{
	
		SendBuff[index] = 0x00;
		RecvBuff[index] = 0x00;
	
	}

}

//Displays the message with the corresponding color
void DisplayOut( CString str, COLORREF color )
{

	int nOldLines = 0,
		nNewLines = 0,
		nScroll = 0;
	long nInsertPoint = 0;
	CHARFORMAT cf;

	//Save number of lines before insertion of new text
	nOldLines = pThis->rbResult.GetLineCount();

	//Initialize character format structure
	cf.cbSize		= sizeof( CHARFORMAT );
	cf.dwMask		= CFM_COLOR;
	cf.dwEffects	= 0;	// To disable CFE_AUTOCOLOR
	cf.crTextColor	= color;

	//Set insertion point to end of text
	nInsertPoint = pThis->rbResult.GetWindowTextLength();
	pThis->rbResult.SetSel( nInsertPoint, -1 );
	
	//Set the character format
	pThis->rbResult.SetSelectionCharFormat( cf );

	//Insert string at the current caret poisiton
	pThis->rbResult.ReplaceSel( str );

	nNewLines = pThis->rbResult.GetLineCount();
	nScroll	= nNewLines - nOldLines;
	pThis->rbResult.LineScroll( 1 );
	
}

//Error checking for inputs that needs to be in hex format
int HexCheck( char data1, char data2 )
{

	int retval = 1;
	bool state1, state2;

	if( data1 == '0' ||
		data1 == '1' ||
		data1 == '2' ||
		data1 == '3' ||
		data1 == '4' ||
		data1 == '5' ||
		data1 == '6' ||
		data1 == '7' ||
		data1 == '8' ||
		data1 == '9' ||
		data1 == 'A' ||
		data1 == 'B' ||
		data1 == 'C' ||
		data1 == 'D' ||
		data1 == 'E' ||
		data1 == 'F' ||
		data1 == 'a' ||
		data1 == 'b' ||
		data1 == 'c' ||
		data1 == 'd' ||
		data1 == 'e' ||
		data1 == 'f' )
	{
	
		state1 = true;
	
	}
	else
	{
	
		state1 = false;
	
	}

	if( data2 == '0' ||
		data2 == '1' ||
		data2 == '2' ||
		data2 == '3' ||
		data2 == '4' ||
		data2 == '5' ||
		data2 == '6' ||
		data2 == '7' ||
		data2 == '8' ||
		data2 == '9' ||
		data2 == 'A' ||
		data2 == 'B' ||
		data2 == 'C' ||
		data2 == 'D' ||
		data2 == 'E' ||
		data2 == 'F' ||
		data1 == 'a' ||
		data1 == 'b' ||
		data1 == 'c' ||
		data1 == 'd' ||
		data1 == 'e' ||
		data1 == 'f' ||
		data2 == NULL )
	{
	
		state2 = true;
	
	}
	else
	{
	
		state2 = false;
	
	}

	if( state1 == true && state2 == true )
	{
	
		retval = 0;
	
	}
	else
	{
	
		retval = 1;
	
	}
				
	return retval;
}

void Initializer()
{

	pThis->cbReader.SetWindowText( "" );
	DisplayOut( "Program Ready\n", GREEN );

	pThis->btnInit.EnableWindow( true );
	pThis->btnClear.EnableWindow( true );
	pThis->btnQuit.EnableWindow( true );
	pThis->btnConnect.EnableWindow( false );
	pThis->btnFormat.EnableWindow( false );
	pThis->btnRead.EnableWindow( false );
	pThis->btnWrite.EnableWindow( false );

	pThis->tbFileID1.EnableWindow( false );
	pThis->tbFileID2.EnableWindow( false );
	pThis->tbLen1.EnableWindow( false );
	pThis->tbLen2.EnableWindow( false );
	pThis->tbID1.EnableWindow( false );
	pThis->tbID2.EnableWindow( false );
	pThis->tbOffset1.EnableWindow( false );
	pThis->tbOffset2.EnableWindow( false );
	pThis->tbLength.EnableWindow( false );
	pThis->tbData.EnableWindow( false );

	pThis->tbFileID1.SetWindowText( "" );
	pThis->tbFileID2.SetWindowText( "" );
	pThis->tbLen1.SetWindowText( "" );
	pThis->tbLen2.SetWindowText( "" );
	pThis->tbID1.SetWindowText( "" );
	pThis->tbID2.SetWindowText( "" );
	pThis->tbOffset1.SetWindowText( "" );
	pThis->tbOffset2.SetWindowText( "" );
	pThis->tbLength.SetWindowText( "" );
	pThis->tbData.SetWindowText( "" );

	pThis->tbFileID1.SetLimitText( 2 );
	pThis->tbFileID2.SetLimitText( 2 );
	pThis->tbLen1.SetLimitText( 3 );
	pThis->tbLen2.SetLimitText( 3 );
	pThis->tbID1.SetLimitText( 2 );
	pThis->tbID2.SetLimitText( 2 );
	pThis->tbOffset1.SetLimitText( 2 );
	pThis->tbOffset2.SetLimitText( 2 );
	pThis->tbLength.SetLimitText( 3 );
	pThis->tbData.SetLimitText( 2 );

}

//SmartCard Error Handler
static CString GetScardErrMsg(int code)
{
	switch( code )
	{
	// Smartcard Reader interface errors
	case SCARD_E_CANCELLED:
		return ("The action was canceled by an SCardCancel request.\n");
		break;
	case SCARD_E_CANT_DISPOSE:
		return ("The system could not dispose of the media in the requested manner.\n");
		break;
	case SCARD_E_CARD_UNSUPPORTED:
		return ("The smart card does not meet minimal requirements for support.\n");
		break;
	case SCARD_E_DUPLICATE_READER:
		return ("The reader driver didn't produce a unique reader name.\n");
		break;
	case SCARD_E_INSUFFICIENT_BUFFER:
		return ("The data buffer for returned data is too small for the returned data.\n");
		break;
	case SCARD_E_INVALID_ATR:
		return ("An ATR string obtained from the registry is not a valid ATR string.\n");
		break;
	case SCARD_E_INVALID_HANDLE:
		return ("The supplied handle was invalid.\n");
		break;
	case SCARD_E_INVALID_PARAMETER:
		return ("One or more of the supplied parameters could not be properly interpreted.\n");
		break;
	case SCARD_E_INVALID_TARGET:
		return ("Registry startup information is missing or invalid.\n");
		break;
	case SCARD_E_INVALID_VALUE:
		return ("One or more of the supplied parameter values could not be properly interpreted.\n");
		break;
	case SCARD_E_NOT_READY:
		return ("The reader or card is not ready to accept commands.\n");
		break;
	case SCARD_E_NOT_TRANSACTED:
		return ("An attempt was made to end a non-existent transaction.\n");
		break;
	case SCARD_E_NO_MEMORY:
		return ("Not enough memory available to complete this command.\n");
		break;
	case SCARD_E_NO_SERVICE:
		return ("The smart card resource manager is not running.\n");
		break;
	case SCARD_E_NO_SMARTCARD:
		return ("The operation requires a smart card, but no smart card is currently in the device.\n");
		break;
	case SCARD_E_PCI_TOO_SMALL:
		return ("The PCI receive buffer was too small.\n");
		break;
	case SCARD_E_PROTO_MISMATCH:
		return ("The requested protocols are incompatible with the protocol currently in use with the card.\n");
		break;
	case SCARD_E_READER_UNAVAILABLE:
		return ("The specified reader is not currently available for use.\n");
		break;
	case SCARD_E_READER_UNSUPPORTED:
		return ("The reader driver does not meet minimal requirements for support.\n");
		break;
	case SCARD_E_SERVICE_STOPPED:
		return ("The smart card resource manager has shut down.\n");
		break;
	case SCARD_E_SHARING_VIOLATION:
		return ("The smart card cannot be accessed because of other outstanding connections.\n");
		break;
	case SCARD_E_SYSTEM_CANCELLED:
		return ("The action was canceled by the system, presumably to log off or shut down.\n");
		break;
	case SCARD_E_TIMEOUT:
		return ("The user-specified timeout value has expired.\n");
		break;
	case SCARD_E_UNKNOWN_CARD:
		return ("The specified smart card name is not recognized.\n");
		break;
	case SCARD_E_UNKNOWN_READER:
		return ("The specified reader name is not recognized.\n");
		break;
	case SCARD_F_COMM_ERROR:
		return ("An internal communications error has been detected.\n");
		break;
	case SCARD_F_INTERNAL_ERROR:
		return ("An internal consistency check failed.\n");
		break;
	case SCARD_F_UNKNOWN_ERROR:
		return ("An internal error has been detected, but the source is unknown.\n");
		break;
	case SCARD_F_WAITED_TOO_LONG:
		return ("An internal consistency timer has expired.\n");
		break;
	case SCARD_W_REMOVED_CARD:
		return ("The smart card has been removed and no further communication is possible.\n");
		break;
	case SCARD_W_RESET_CARD:
		return ("The smart card has been reset, so any shared state information is invalid.\n");
		break;
	case SCARD_W_UNPOWERED_CARD:
		return ("Power has been removed from the smart card and no further communication is possible.\n");
		break;
	case SCARD_W_UNRESPONSIVE_CARD:
		return ("The smart card is not responding to a reset.\n");
		break;
	case SCARD_W_UNSUPPORTED_CARD:
		return ("The reader cannot communicate with the card due to ATR string configuration conflicts.\n");
		break;
	}
	return ("Error is not documented.\n");
}

//Transmits APDU to Card
int SendAPDU( int SendType )
{

	char tempstr[32767];
	int index;

	ioRequest.dwProtocol = dwActProtocol;
	ioRequest.cbPciLength = sizeof( SCARD_IO_REQUEST );
	
	//Display APDU In
	sprintf( tempstr, "< " );
	for( index = 0; index != SendLen; index++ )
	{
	
		sprintf( tempstr, "%s%02X ", tempstr, SendBuff[index] );
	
	}
	sprintf( tempstr, "%s\n", tempstr );
	DisplayOut( tempstr, BLACK );

	retCode = SCardTransmit( hCard,
							 &ioRequest,
							 SendBuff,
							 SendLen,
							 NULL,
							 RecvBuff,
							 &RecvLen );

	if( retCode != SCARD_S_SUCCESS )
	{
	
		DisplayOut( GetScardErrMsg( retCode ), RED );
		return retCode;
	
	}
	else
	{
	
		sprintf( tempstr, "> " );
		switch( SendType )
		{
			
			case 0:	//Display SW1/SW2 value
				for( index = RecvLen - 2; index != RecvLen; index++ )
				{
				
					sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
				
				}
				if( strcmp( tempstr, "> 90 00 " ) != 0 )
				{
				
					DisplayOut( "Return bytes are not acceptable\n", RED );
				
				}
				break;
			case 1:	//Display ATR after checking SW1/SW2
				for( index = RecvLen - 2; index != RecvLen; index++ )
				{
				
					sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
				
				}
				if( strcmp( tempstr, "> 90 00 " ) != 0 )
				{
				
					DisplayOut( "Return bytes are not acceptable\n", RED );
				
				}
				else
				{
				
					sprintf( tempstr, "ATR : " );
					for( index = 0; index != RecvLen - 2; index++ )
					{
					
						sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
					
					}
				
				}
				break;
			case 2: //Display all data
				for( index = 0; index != RecvLen; index++ )
				{
				
					sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
				
				}
				break;
		}
		sprintf( tempstr, "%s\n", tempstr );
		DisplayOut( tempstr, BLACK );
	
	}

	return retCode;

}

int SubmitIC()
{

	int index;
	char tempstr[MAX], tempstr2[MAX];

	ClearBuffers();
	SendBuff[0] = 0x80;			//CLA
	SendBuff[1] = 0x20;			//INS
	SendBuff[2] = 0x07;			//P1
	SendBuff[3] = 0x00;			//P2
	SendBuff[4] = 0x08;			//P3
	SendBuff[5] = 0x41;			//A
	SendBuff[6] = 0x43;			//C
	SendBuff[7] = 0x4F;			//O
	SendBuff[8] = 0x53;			//S
	SendBuff[9] = 0x54;			//T
	SendBuff[10] = 0x45;		//E
	SendBuff[11] = 0x53;		//S
	SendBuff[12] = 0x54;		//T

	SendLen = 0x0D;
	RecvLen = 0x02;

	retCode = SendAPDU( 0 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return retCode;
	
	}

	sprintf( tempstr, "> " );
	for( index = 0; index < 2; index++ )
	{
	
		sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
	
	}

	if( strcmp( tempstr, "> 90 00 " ) != 0 )
	{
	
		sprintf( tempstr2, "The return string is invalid. Value: %s\n", tempstr );
		DisplayOut( tempstr2, RED );
		retCode = INVALID_SW1SW2;
		return retCode;
	
	}
	
	return retCode;
}

int selectFile( BYTE HiAddr, BYTE LoAddr )
{

	char tempstr[MAX];
	int index;

	ClearBuffers();
	SendBuff[0] = 0x80;			//CLA
	SendBuff[1] = 0xA4;			//INS
	SendBuff[2] = 0x00;			//P1
	SendBuff[3] = 0x00;			//P2
	SendBuff[4] = 0x02;			//P3
	SendBuff[5] = HiAddr;		//Value of High Byte
	SendBuff[6] = LoAddr;		//Value of Low Byte

	SendLen = 0x07;
	RecvLen = 0x02;

	retCode = SendAPDU( 2 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return retCode;
	
	}

	return retCode;

}
int readRecord( BYTE RecNo, BYTE DataLen )
{

	char tempstr[MAX], tempstr2[MAX];
	int index;

	ClearBuffers();
	SendBuff[0] = 0x80;			//CLA
	SendBuff[1] = 0xB2;			//INS
	SendBuff[2] = RecNo;		//P1 Record No
	SendBuff[3] = 0x00;			//P2
	SendBuff[4] = DataLen;		//P3 Length of data to be read

	SendLen = 0x05;
	RecvLen = SendBuff[4] + 2;

	retCode = SendAPDU( 0 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return retCode;
	
	}

	sprintf( tempstr, "> " );
	for( index = RecvLen - 2; index != RecvLen; index++ )
	{
	
		sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
	
	}

	if( strcmp( tempstr, "> 90 00 " ) != 0 )
	{
	
		sprintf( tempstr2, "The return string is invalid. Value: %s\n", tempstr );
		DisplayOut( tempstr2, RED );
		retCode = INVALID_SW1SW2;
		return retCode;
	
	}
	
	return retCode;

}

int writeRecord( int casetype, BYTE RecNo, BYTE maxDataLen, BYTE DataLen, BYTE DataIn[] )
{

	char tempstr[MAX], tempstr2[MAX];
	int index;

	if( casetype == 1 ) //If card data is to be erased before writing new data
	{
		//Re-initialize card values to 0x00
		ClearBuffers();
		SendBuff[0] = 0x80;			//CLA
		SendBuff[1] = 0xD2;			//INS
		SendBuff[2] = RecNo;		//P1 Record to be written
		SendBuff[3] = 0x00;			//P2
		SendBuff[4] = maxDataLen;	//P3 Length

		for( index = 0; index != maxDataLen; index++ )
		{
		
			SendBuff[index + 5] = 0x00;
		
		}

		SendLen = maxDataLen + 5;
		RecvLen = 0x02;

		retCode = SendAPDU( 0 );
		if( retCode != SCARD_S_SUCCESS )
		{
		
			return retCode;
		
		}
		
		sprintf( tempstr, "> " );
		for( index = 0; index < 2; index++ )
		{
		
			sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
		
		}
		if( strcmp( tempstr, "> 90 00 " ) != 0 )
		{
		
			sprintf( tempstr2, "The return string is invalid. Value: %s\n", tempstr );
			DisplayOut( tempstr2, RED );
			retCode = INVALID_SW1SW2;
			return retCode;

		}

	}
	
	//Write data to card
	ClearBuffers();
	SendBuff[0] = 0x80;			//CLA
	SendBuff[1] = 0xD2;			//INS
	SendBuff[2] = RecNo;		//P1
	SendBuff[3] = 0x00;			//P2
	SendBuff[4] = DataLen;		//P3 Length
	
	for( index = 0; index != DataLen; index++ )
	{
	
		SendBuff[index + 5 ] = DataIn[index];
	
	}
	SendLen = DataLen + 5;
	RecvLen = 0x02;

	retCode = SendAPDU( 0 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return retCode;
	
	}

	sprintf( tempstr, "> " );
	for( index = 0; index < 2; index++ )
	{
	
		sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
	
	}
	if( strcmp( tempstr, "> 90 00 " ) != 0 )
	{
	
		sprintf( tempstr2, "The return string is invalid. Value: %s\n", tempstr );
		DisplayOut( tempstr2, RED );
		retCode = INVALID_SW1SW2;
		return retCode;
	
	}

	return retCode;
}

int readBinary( BYTE HiByte, BYTE LoByte, BYTE DataLen )
{

	char tempstr[MAX];
	int index;

	ClearBuffers();
	SendBuff[0] = 0x80;
	SendBuff[1] = 0xB0;
	SendBuff[2] = HiByte;
	SendBuff[3] = LoByte;
	SendBuff[4] = DataLen;

	SendLen = 0x05;
	RecvLen = SendBuff[4] + 2;

	retCode = SendAPDU( 2 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return retCode;
	
	}

	return retCode;

}

int writeBinary( int casetype, BYTE HiByte, BYTE LoByte, BYTE DataLen, BYTE DataIn[] )
{

	char tempstr[MAX], tempstr2[MAX];
	int index;

	if( casetype == 1 ) //If card data is to be erased before writing new data
	{
		//Re-initialize card values to 0x00
		ClearBuffers();
		SendBuff[0] = 0x80;			//CLA
		SendBuff[1] = 0xD0;			//INS
		SendBuff[2] = HiByte;		//P1 High Byte Address
		SendBuff[3] = LoByte;		//P2 Low Byte Address
		SendBuff[4] = DataLen;		//P3 Length of data to be read

		for( index = 0; index != DataLen; index++ )
		{
		
			SendBuff[index + 5] = 0x00;
		
		}

		SendLen = DataLen + 5;
		RecvLen = 0x02;

		retCode = SendAPDU( 0 );
		if( retCode != SCARD_S_SUCCESS )
		{
		
			return retCode;
		
		}
	
		sprintf( tempstr, "> " );
		for( index = 0; index < 2; index++ )
		{
		
			sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
		
		}
		if( strcmp( tempstr, "> 90 00 " ) != 0 )
		{
		
			sprintf( tempstr2, "The return string is invalid. Value: %s\n", tempstr );
			DisplayOut( tempstr2, RED );
			retCode = INVALID_SW1SW2;
			return retCode;
		
		}

	}
	
	//Write data to card
	ClearBuffers();
	SendBuff[0] = 0x80;			//CLA
	SendBuff[1] = 0xD0;			//INS
	SendBuff[2] = HiByte;		//P1 High Byte Address
	SendBuff[3] = LoByte;		//P2 Low Byte Address
	SendBuff[4] = DataLen;		//P3 Length of data to be read

	for( index = 0; index != DataLen; index++ )
	{
	
		SendBuff[index + 5] = DataIn[index];
	
	}

	SendLen = DataLen + 5;
	RecvLen = 0x02;

	retCode = SendAPDU( 0 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return retCode;
	
	}

	sprintf( tempstr, "> " );
	for( index = 0; index < 2; index++ )
	{
	
		sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
	
	}
	if( strcmp( tempstr, "> 90 00 " ) != 0 )
	{
		
		sprintf( tempstr2, "The return string is invalid. Value: %s\n", tempstr );
		DisplayOut( tempstr2, RED );
		retCode = INVALID_SW1SW2;
		return retCode;
		
	}

	return retCode;
	
}

void getBinaryData()
{

	char tempstr[MAX], tempstr2[MAX], holder[2];
	int index, templen;

	//Send IC Code
	retCode = SubmitIC();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		DisplayOut( "Insert ACOS3-24K card on contact card reader\n", RED );
	
	}

	//Select FF 04
	retCode = selectFile( 0xFF, 0x04 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	sprintf( tempstr, "> " );
	for( index = 0; index < 2; index++ )
	{
	
		sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
	
	}
	if( strcmp( tempstr, "> 90 00 " ) != 0 )
	{
		
		sprintf( tempstr2, "The return string is invalid. Value: %s\n", tempstr );
		DisplayOut( tempstr2, RED );
		retCode = INVALID_SW1SW2;
		return;
		
	}

	//Read first record
	retCode = readRecord( 0x00, 0x07 );
	if( retCode != SCARD_S_SUCCESS )
	{
		
		DisplayOut( "Card may not have been formatted yet\n", RED );
		return;
	
	}

	//Provide parameter to data input box
	sprintf( holder, "%02X", RecvBuff[4] );
	pThis->tbID1.SetWindowText( holder );
	sprintf( holder, "%02X", RecvBuff[5] );
	pThis->tbID2.SetWindowText( holder );
	templen = RecvBuff[1];
	templen = templen + ( RecvBuff[0] * 256 );
	pThis->tbData.SetLimitText( templen );

}
/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
		// No message handlers
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CBinaryFilesinACOS3Dlg dialog

CBinaryFilesinACOS3Dlg::CBinaryFilesinACOS3Dlg(CWnd* pParent /*=NULL*/)
	: CDialog(CBinaryFilesinACOS3Dlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CBinaryFilesinACOS3Dlg)
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIconBig   = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hIconSmall = AfxGetApp()->LoadIcon(IDR_SMALLICON);
}

void CBinaryFilesinACOS3Dlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CBinaryFilesinACOS3Dlg)
	DDX_Control(pDX, IDC_EDIT9, tbLength);
	DDX_Control(pDX, IDC_BUTTON5, btnWrite);
	DDX_Control(pDX, IDC_BUTTON4, btnRead);
	DDX_Control(pDX, IDC_BUTTON8, btnQuit);
	DDX_Control(pDX, IDC_BUTTON7, btnReset);
	DDX_Control(pDX, IDC_BUTTON6, btnClear);
	DDX_Control(pDX, IDC_RICHEDIT1, rbResult);
	DDX_Control(pDX, IDC_EDIT11, tbData);
	DDX_Control(pDX, IDC_EDIT7, tbOffset1);
	DDX_Control(pDX, IDC_EDIT8, tbOffset2);
	DDX_Control(pDX, IDC_EDIT6, tbID2);
	DDX_Control(pDX, IDC_EDIT5, tbID1);
	DDX_Control(pDX, IDC_EDIT4, tbLen2);
	DDX_Control(pDX, IDC_EDIT3, tbLen1);
	DDX_Control(pDX, IDC_EDIT2, tbFileID2);
	DDX_Control(pDX, IDC_EDIT1, tbFileID1);
	DDX_Control(pDX, IDC_COMBO1, cbReader);
	DDX_Control(pDX, IDC_BUTTON3, btnFormat);
	DDX_Control(pDX, IDC_BUTTON2, btnConnect);
	DDX_Control(pDX, IDC_BUTTON1, btnInit);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CBinaryFilesinACOS3Dlg, CDialog)
	//{{AFX_MSG_MAP(CBinaryFilesinACOS3Dlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, OnInit)
	ON_BN_CLICKED(IDC_BUTTON2, OnConnect)
	ON_BN_CLICKED(IDC_BUTTON6, OnClear)
	ON_BN_CLICKED(IDC_BUTTON7, OnReset)
	ON_BN_CLICKED(IDC_BUTTON8, OnQuit)
	ON_BN_CLICKED(IDC_BUTTON3, OnFormat)
	ON_BN_CLICKED(IDC_BUTTON4, OnRead)
	ON_BN_CLICKED(IDC_BUTTON5, OnWrite)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CBinaryFilesinACOS3Dlg message handlers

BOOL CBinaryFilesinACOS3Dlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIconBig,	TRUE);		// Set big icon
	SetIcon(m_hIconSmall,	FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	pThis = this;
	Initializer();
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CBinaryFilesinACOS3Dlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CBinaryFilesinACOS3Dlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIconSmall);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CBinaryFilesinACOS3Dlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIconSmall;
}

void CBinaryFilesinACOS3Dlg::OnInit() 
{
	
	//Establish Context
	retCode = SCardEstablishContext( SCARD_SCOPE_USER,
									 NULL,
									 NULL,
									 &hContext );
	
	if( retCode != SCARD_S_SUCCESS )
	{
		rbResult.SetWindowText( GetScardErrMsg( retCode ) );
		return;
	
	}

	//List PC/SC Card Readers
	size = 256;
	retCode = SCardListReaders( hContext,
								NULL,
								readerName,
								&size );

	if( retCode != SCARD_S_SUCCESS )
	{
	
		rbResult.SetWindowText( GetScardErrMsg( retCode ) );
		return;
	
	}

	if( readerName == NULL )
	{
	
		rbResult.SetWindowText( GetScardErrMsg( retCode ) );
		return;
	
	}
	//Put readernames inside the combobox
	cbReader.ResetContent();
	char *p = readerName;
	while ( *p )
	{
    	for (int i=0;p[i];i++);
	      i++;
	    if ( *p != 0 )
		{
     		cbReader.AddString( p );
		}
		p = &p[i];
	}
	
	cbReader.SetCurSel( 0 );
	DisplayOut( "Select reader, insert mcu card and connect\n", GREEN );

	btnInit.EnableWindow( false );
	btnConnect.EnableWindow( true );

}

void CBinaryFilesinACOS3Dlg::OnConnect() 
{

	DWORD Protocol = 1;
	char buffer1[100];
	char buffer2[100];


	cbReader.GetLBText( cbReader.GetCurSel(), readerName );
	
	DisplayOut( "< Invoke SCardConnect\n", BLACK );
	//Connect to selected reader
	retCode = SCardConnect( hContext,
							readerName,
							SCARD_SHARE_EXCLUSIVE,
							Protocol,
							&hCard,
							&Protocol );

	if( retCode != SCARD_S_SUCCESS )
	{
	
		//Failed to connect to reader
		DisplayOut( GetScardErrMsg( retCode ), RED );
		return;
		
	}

	//Successful connection to reader
	IO_REQ.dwProtocol = Protocol;
	IO_REQ.cbPciLength = sizeof( SCARD_IO_REQUEST );

	cbReader.GetLBText( cbReader.GetCurSel(), buffer2 );
	sprintf( buffer1, "%s %s \n", "Successful connection to ", buffer2 );
	DisplayOut( buffer1, GREEN );
	getBinaryData();

	
	pThis->btnFormat.EnableWindow( true );
	pThis->btnRead.EnableWindow( true );
	pThis->btnWrite.EnableWindow( true );

	pThis->tbFileID1.EnableWindow( true );
	pThis->tbFileID2.EnableWindow( true );
	pThis->tbLen1.EnableWindow( true );
	pThis->tbLen2.EnableWindow( true );
	pThis->tbID1.EnableWindow( true );
	pThis->tbID2.EnableWindow( true );
	pThis->tbOffset1.EnableWindow( true );
	pThis->tbOffset2.EnableWindow( true );
	pThis->tbLength.EnableWindow( true );
	pThis->tbData.EnableWindow( true );

}

void CBinaryFilesinACOS3Dlg::OnClear() 
{

	rbResult.SetWindowText( "" );

}

void CBinaryFilesinACOS3Dlg::OnReset() 
{
	
	rbResult.SetWindowText( "" );
	retCode = SCardDisconnect( hCard, SCARD_UNPOWER_CARD );
	retCode = SCardReleaseContext( hContext );
	Initializer();
	
}

void CBinaryFilesinACOS3Dlg::OnQuit() 
{
	
	retCode = SCardDisconnect( hCard, SCARD_UNPOWER_CARD );
	retCode = SCardReleaseContext( hContext );
	CDialog::OnCancel();
	
}

void CBinaryFilesinACOS3Dlg::OnFormat() 
{

	char tempstr[MAX], tempstr2[MAX], holder[4];
	int index, tempval;
	BYTE temparr[32];

	tbFileID1.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbFileID1.SetFocus();
		return;
	
	}

	tbFileID2.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbFileID2.SetFocus();
		return;
	
	}

	tbLen2.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbLen2.SetFocus();
		return;
	
	}

	//Send IC Code
	retCode = SubmitIC();
	if( retCode != SCARD_S_SUCCESS )
	{
		
		DisplayOut( "Insert ACOS3-24K card on contact card reader\n", RED );
		return;
	
	}

	//Select FF 02
	retCode = selectFile( 0xFF, 0x02 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}
	sprintf( tempstr, "> " );
	for( index = 0; index < 2; index++ )
	{
	
		sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
	
	}
	if( strcmp( tempstr, "> 90 00 " ) != 0 )
	{
	
		sprintf( tempstr2, "The return string is invalid. Value: %s\n", tempstr );
		DisplayOut( tempstr, RED );
		retCode = INVALID_SW1SW2;
		return;

	}

	//Write to FF 02
	//This will create 1 binary file, no Option registers and
	//Security Option registers defined, Personalization bit
	//is not set
	temparr[0] = 0x00;
	temparr[1] = 0x00;
	temparr[2] = 0x01;
	temparr[3] = 0x00;
	retCode = writeRecord( 0, 0x00, 0x04, 0x04, temparr );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}
	DisplayOut( "File FF 02 is updated\n", GREEN );

	//Perform a reset for changes in the ACOS3 to take effect
	retCode = SCardDisconnect( hCard, SCARD_UNPOWER_CARD );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		DisplayOut( GetScardErrMsg( retCode ), RED );
		return;

	}

	retCode = SCardConnect( hContext,
							readerName,
							SCARD_SHARE_SHARED,
							SCARD_PROTOCOL_T0 || SCARD_PROTOCOL_T1,
							&hCard,
							&dwActProtocol );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		DisplayOut( GetScardErrMsg( retCode ), RED );
		return;

	}

	DisplayOut( "Card reset is successful\n", GREEN );

	//Select FF 04
	retCode = selectFile( 0xFF, 0x04 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	sprintf( tempstr, "> " );
	for( index = 0; index < 2; index++ )
	{
	
		sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
	
	}
	if( strcmp( tempstr, "> 90 00 " ) != 0 )
	{
	
		sprintf( tempstr, "The return string is invalid. Value: %s\n", tempstr );
		DisplayOut( tempstr, RED );
		retCode = INVALID_SW1SW2;
		return;
	
	}

	//Send IC Code
	retCode = SubmitIC();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	//Write to FF 04
	tbLen1.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	if( strcmp( holder, "" ) == 0 )
	{
	
		temparr[0] = 0x00;
	
	}
	else
	{
	
		temparr[0] = tempval;
	
	}

	tbLen2.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	temparr[1] = tempval;

	temparr[2] = 0x00;
	temparr[3] = 0x00;

	tbFileID1.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	temparr[4] = tempval;

	tbFileID2.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	temparr[5] = tempval;

	temparr[6] = 0x80;

	retCode = writeRecord( 0, 0x00, 0x07, 0x07, temparr );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}
	
	sprintf( tempstr, "" );
	tbFileID1.GetWindowText( holder, 4 );
	sprintf( tempstr, holder );
	tbFileID2.GetWindowText( holder, 4 );
	sprintf( tempstr, "%s%s", tempstr, holder );
	sprintf( tempstr2, "User file %s is defined\n", tempstr );
	DisplayOut( tempstr2, GREEN );

}	

void CBinaryFilesinACOS3Dlg::OnRead() 
{
	
	char tempstr[MAX], tempdata[MAX], holder[4];
	int index, tempval, templen;
	BYTE FileID1, FileID2, HiByte, LoByte;

	tbID1.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbID1.SetFocus();
		return;
	
	}
	tbID2.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbID2.SetFocus();
		return;
	
	}
	tbOffset2.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbOffset2.SetFocus();
		return;
	
	}
	tbLength.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbLength.SetFocus();
		return;
	
	}

	ClearBuffers();
	
	tbID1.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	FileID1 = tempval;

	tbID2.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	FileID2 = tempval;

	tbOffset1.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
		
		tbOffset1.SetWindowText( "00" );
		HiByte = 0x00;
	
	}
	else
	{
	
		HiByte = tempval;
	
	}
	
	tbOffset2.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	LoByte = tempval;

	tbLength.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	templen = tempval;

	//Select User File
	retCode = selectFile( FileID1, FileID2 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}
	sprintf( tempstr, "> " );
	for( index = 0; index < 2; index++ )
	{
	
		sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
	
	}
	if( strcmp( tempstr, "> 91 00 " ) != 0 )
	{
	
		sprintf( tempstr, "The return string is invalid. Value: %s\n", tempstr );
		DisplayOut( tempstr, RED );
		retCode = INVALID_SW1SW2;
		return;

	}

	//Read Binary
	retCode = readBinary( HiByte, LoByte, templen );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		DisplayOut( "Card may not have been formatted yet\n", RED );
		return;
	
	}

	sprintf( tempstr, "" );
	index = 0;
	tempval = tbData.GetLimitText();
	while( RecvBuff[index] != 0x00 )
	{
	
		if( index < tempval )
		{
		
			sprintf( tempstr, "%s%c", tempstr, RecvBuff[index] );
			index++;
		
		}
	
	}

	tbData.SetWindowText( tempstr );

}

void CBinaryFilesinACOS3Dlg::OnWrite() 
{
	
	char tempstr[MAX], tempdata[MAX], holder[4];
	int index, tempval, templen;
	BYTE FileID1, FileID2, HiByte, LoByte;
	BYTE temparr[256];

	tbID1.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbID1.SetFocus();
		return;
	
	}
	tbID2.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbID2.SetFocus();
		return;
	
	}
	tbOffset2.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0  || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbOffset2.SetFocus();
		return;
	
	}
	tbLength.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0  || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tbLength.SetFocus();
		return;
	
	}
	tbData.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tbData.SetFocus();
		return;
	
	}
	
	ClearBuffers();
	
	tbID1.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	FileID1 = tempval;

	tbID2.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	FileID2 = tempval;

	tbOffset1.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
		
		tbOffset1.SetWindowText( "00" );
		HiByte = 0x00;
	
	}
	else
	{
	
		HiByte = tempval;
	
	}
	
	tbOffset2.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	LoByte = tempval;

	tbLength.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	templen = tempval;

	//Select User File
	retCode = selectFile( FileID1, FileID2 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}
	sprintf( tempstr, "> " );
	for( index = 0; index < 2; index++ )
	{
	
		sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
	
	}
	if( strcmp( tempstr, "> 91 00 " ) != 0 )
	{
	
		sprintf( tempstr, "The return string is invalid. Value: %s\n", tempstr );
		DisplayOut( tempstr, RED );
		retCode = INVALID_SW1SW2;
		return;

	}

	//Write Data to card
	tbData.GetWindowText( tempdata, MAX );
	for( index = 0; index != strlen( tempdata ); index++ )
	{
	
		temparr[index] = (char)tempdata[index];
	
	}
	retCode = writeBinary( 1, HiByte, LoByte, templen, temparr );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

}
