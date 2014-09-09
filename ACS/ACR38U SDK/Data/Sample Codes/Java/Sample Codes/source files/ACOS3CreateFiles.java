
/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              ACOS3CreateFiles.java

  Description:       This sample program outlines the steps on how to
                     create user-defined files in ACOS smart card
                     using the PC/SC platform.
                     
  Author:            M.J.E.C. Castillo

  Date:              August 26, 2008

  Revision Trail:   (Date/Author/Description)

======================================================================*/

import java.io.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class ACOS3CreateFiles extends JFrame implements ActionListener{

	//JPCSC Variables
	int retCode;
	boolean connActive; 
	public static final int INVALID_SW1SW2 = -450;

	//All variables that requires pass-by-reference calls to functions are
	//declared as 'Array of int' with length 1
	//Java does not process pass-by-ref to int-type variables, thus Array of int was used.
	int [] ATRLen = new int[1]; 
	int [] hContext = new int[1]; 
	int [] cchReaders = new int[1];
	int [] hCard = new int[1];
	int [] PrefProtocols = new int[1]; 		
	int [] RecvLen = new int[1];
	int SendLen = 0;
	byte [] SendBuff = new byte[300];
	byte [] RecvBuff = new byte[300];
	byte [] ATRVal = new byte[128];
	byte [] szReaders = new byte[1024];
	
	
	static JacspcscLoader jacs = new JacspcscLoader();
	
	 private JButton bClear;
	 private JButton bConnect;
	 private JButton bCreate;
	 private JButton bQuit;
	 private JButton bDisconnect;
	 private JButton bInit;
	 private JTextArea Msg;
	 private JComboBox cbReader;
	 private JLabel jLabel1;
	 private JScrollPane msgScrollPane;
    

    public ACOS3CreateFiles() {
    	
    	this.setTitle("ACOS 3 Create Files");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

   	  	setSize(470,320);
   	  	bInit = new JButton();
   	  	bConnect = new JButton();
   	  	bCreate = new JButton();
   	  	bDisconnect = new JButton();
   	  	bClear = new JButton();
   	  	bQuit = new JButton();
   	  	Msg = new JTextArea();
   	  	msgScrollPane = new JScrollPane();
   	  	cbReader = new JComboBox();
   	  	jLabel1 = new JLabel();
   	  	jLabel1.setText("Select Reader");
   	  	Msg.setColumns(20);
   	  	Msg.setRows(5);
   	  	msgScrollPane.setViewportView(Msg);

  
      
   	  	String[] rdrNameDef = {"Please select reader"};	
   	  	cbReader = new JComboBox(rdrNameDef);
   	  	cbReader.setSelectedIndex(0);
		
        bInit.setText("Initialize");

        bConnect.setText("Connect");
        
        bCreate.setText("Create Files");

        bDisconnect.setText("Disconnect");
        
        bClear.setText("Clear");

        bQuit.setText("Quit");

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jLabel1)
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                            .addComponent(bClear, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, 120, Short.MAX_VALUE)
                            .addComponent(bDisconnect, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, 120, Short.MAX_VALUE)
                            .addComponent(bInit, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, 120, Short.MAX_VALUE)
                            .addComponent(bConnect, javax.swing.GroupLayout.DEFAULT_SIZE, 120, Short.MAX_VALUE)
                            .addComponent(bCreate, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, 120, Short.MAX_VALUE)
                            .addComponent(bQuit, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, 120, Short.MAX_VALUE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)))
                .addGap(24, 24, 24)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(msgScrollPane, javax.swing.GroupLayout.PREFERRED_SIZE, 306, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(cbReader, javax.swing.GroupLayout.PREFERRED_SIZE, 145, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(16, 16, 16))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGap(26, 26, 26)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel1)
                    .addComponent(cbReader, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(bInit)
                        .addGap(13, 13, 13)
                        .addComponent(bConnect)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(bCreate, javax.swing.GroupLayout.PREFERRED_SIZE, 23, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(bDisconnect)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bClear)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bQuit))
                    .addComponent(msgScrollPane, javax.swing.GroupLayout.PREFERRED_SIZE, 183, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addContainerGap())
        );
        
        bInit.addActionListener(this);
        bConnect.addActionListener(this);
        bDisconnect.addActionListener(this);
        bClear.addActionListener(this);
        bCreate.addActionListener(this);
        bQuit.addActionListener(this);
        
    }

	public void actionPerformed(ActionEvent e) 
	{
		
		if(bInit == e.getSource())
		{
			
			//1. Establish context and obtain hContext handle
			retCode = jacs.jSCardEstablishContext(ACSModule.SCARD_SCOPE_USER, 0, 0, hContext);
		    
			if (retCode != ACSModule.SCARD_S_SUCCESS)
		    {
		    
				Msg.append("Calling SCardEstablishContext...FAILED\n");
		      	displayOut(1, retCode, "");
		      	
		    }
			
			//2. List PC/SC card readers installed in the system
			retCode = jacs.jSCardListReaders(hContext, 0, szReaders, cchReaders);
      		
			//String DataOut = "";
			int offset = 0;
			cbReader.removeAllItems();
			
			for (int i = 0; i < cchReaders[0]-1; i++)
			{
				
			  	if (szReaders[i] == 0x00)
			  	{			  		
			  		
			  		cbReader.addItem(new String(szReaders, offset, i - offset));
			  		offset = i+1;
			  		
			  	}
			}
			
			if (cbReader.getItemCount() == 0)
			{
			
				cbReader.addItem("No PC/SC reader detected");
				
			}
			
			bInit.setEnabled(false);
			bConnect.setEnabled(true);
		
			
		} // Initialize
		
		if(bConnect == e.getSource())
		{
			
			if(connActive)
			{
				
				retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
				
			}
			
			String rdrcon = (String)cbReader.getSelectedItem();  	      	      	
		    
		    retCode = jacs.jSCardConnect(hContext, 
		    							rdrcon, 
		    							ACSModule.SCARD_SHARE_SHARED,
		    							ACSModule.SCARD_PROTOCOL_T1 | ACSModule.SCARD_PROTOCOL_T0,
		      							hCard, 
		      							PrefProtocols);
		    

		    if (retCode != ACSModule.SCARD_S_SUCCESS)
		    {
		      	//check if ACR128 SAM is used and use Direct Mode if SAM is not detected
		      	if (((String) cbReader.getSelectedItem()).lastIndexOf("ACR128U SAM")> -1)
				{
					
		    		retCode = jacs.jSCardConnect(hContext, 
		    									rdrcon, 
		    									ACSModule.SCARD_SHARE_DIRECT,
		    									0,
		    									hCard, 
		    									PrefProtocols);
		    		
		    		if (retCode != ACSModule.SCARD_S_SUCCESS)
				    {
		    			
		    			displayOut(1, retCode, "");
		    			connActive = false;
		    			return;
		    			
				    }
		    		else
		    		{
		    			
		    			displayOut(0, 0, "Successful connection to " + (String)cbReader.getSelectedItem());
		    			
		    		}
					
				}
		      	else{
		      		
		      		displayOut(1, retCode, "");
	    			connActive = false;
	    			return;
		      		
		      	}
		    
		    } 
		    else 
		    {	      	      
		      	
		    	displayOut(0, 0, "Successful connection to " + (String)cbReader.getSelectedItem());
		      	
		    }
		    
		    connActive = true;
		    bCreate.setEnabled(true);
		    bDisconnect.setEnabled(true);
		    		
		} // Connect
		
		if (bClear == e.getSource())
		{
			Msg.setText("");	
		}
		
		if (bDisconnect == e.getSource())
		{
			if (connActive)
			{
				
				 retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
				 connActive = false;
				
			}
			
			retCode = jacs.jSCardReleaseContext(hCard);
			bCreate.setEnabled(false);
			Msg.setText("");
		
		} // Disconnect
		
		if (bCreate == e.getSource())
		{
			byte[] tmpArray = new byte[56];
			
			submitIC();
			selectFile((byte)0xFF, (byte)0x2);
			
			tmpArray[0] = (byte)0x0;      // 00    Option registers
			tmpArray[1] = (byte)0x0;      // 00    Security option register
			tmpArray[2] = (byte)0x3;      // 03    No of user files
			tmpArray[3] = (byte)0x0;      // 00    Personalization bit
			displayOut(0, 0, "FF 02 is updated");
		
			
			 retCode = writeRecord((byte)0x00, (byte)0x00, (byte)0x04, (byte)0x04, tmpArray);
			 
			 if(retCode!= ACSModule.SCARD_S_SUCCESS)
					return;
			 
			displayOut(0, 0, "Select FF 04");
			selectFile((byte)0xFF, (byte)0x4);
			 
			 if(retCode!= ACSModule.SCARD_S_SUCCESS)
					return;
			 			
			submitIC();
			
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
					return;
			 			

			// Write to FF 04
			//  Write to first record of FF 04
			tmpArray[0] = (byte)0x05;       // 5     Record length
			tmpArray[1] = (byte)0x03;       // 3     No of records
			tmpArray[2] = (byte)0x00;       // 00    Read security attribute
			tmpArray[3] = (byte)0x00;       // 00    Write security attribute
			tmpArray[4] = (byte)0xAA;       // AA    File identifier
			tmpArray[5] = (byte)0x11;       // 11    File identifier
            tmpArray[6] = (byte)0x00;
            
			writeRecord((byte)0x00, (byte)0x00, (byte)0x06, (byte)0x06, tmpArray);
			 
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
					return;
			 
			displayOut(0, 0, "User File AA 11 is defined");
			 
			//  Write to second record of FF 04
			tmpArray[0] = (byte)0x0A;       // 10    Record length
			tmpArray[1] = (byte)0x02;       // 2     No of records
			tmpArray[2] = (byte)0x00;       // 00    Read security attribute
			tmpArray[3] = (byte)0x00;       // 00    Write security attribute
			tmpArray[4] = (byte)0xBB;       // BB    File identifier
			tmpArray[5] = (byte)0x22;       // 22    File identifier
            tmpArray[6] = (byte)0x00;

			writeRecord((byte)0x00, (byte)0x01, (byte)0x06, (byte)0x06, tmpArray);				
			 
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
					return;
			 
			displayOut(0, 0, "User File BB 22 is defined");
			
			//  Write to third record of FF 04
			tmpArray[0] = (byte)0x06;       // 6     Record length
			tmpArray[1] = (byte)0x04;       // 4     No of records
			tmpArray[2] = (byte)0x00;       // 00    Read security attribute
			tmpArray[3] = (byte)0x00;       // 00    Write security attribute
			tmpArray[4] = (byte)0xCC;       // CC    File identifier
			tmpArray[5] = (byte)0x33;       // 33    File identifier
            tmpArray[6] = (byte)0x00;

			writeRecord((byte)0x00, (byte)0x02, (byte)0x06, (byte)0x06, tmpArray);
			
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
		 
			displayOut(0, 0, "User File CC 33 is defined");
			

			//  Select 3 User Files created previously for validation
			// Select User File AA 11
			selectFile((byte)0xAA, (byte)0x11);

			if(retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
			
			displayOut(0, 0, "User File AA 11 is selected");
			
			selectFile((byte)0xBB, (byte)0x22);
			
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
			
			displayOut(0, 0, "User File BB 22 is selected");
			
			selectFile((byte)0xCC, (byte)0x33);
			
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
			
			displayOut(0, 0, "User File CC 33 is selected");
			
		}
		
		if(bQuit == e.getSource())
		{
			
			this.dispose();
			
		}
				
	}
    
	public int writeRecord(int caseType, byte recNo, byte maxDataLen, byte dataLen, byte[] dataIn)
	{
		 String tmpStr="", tmpHex="";
		// If card data is to be erased before writing new data
		if (caseType == 1)
		{
			
			clearBuffers();
		    SendBuff[0] =  (byte) 0x80;          // CLA
		    SendBuff[1] =  (byte) 0xD2;          // INS
		    SendBuff[2] =  recNo;        		 // P1    Record to be written
		    SendBuff[3] =  (byte) 0x00;          // P2
		    SendBuff[4] =  maxDataLen;   		 // P3    Length
			
		    for(int i=0; i<maxDataLen; i++)
		    	SendBuff[i+5] = (byte) 0x00;
		    	
		    SendLen = maxDataLen +5;
		    RecvLen[0] = 2;
		    

		 for(int i=0; i<SendLen; i++)
		 {
			tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
				
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
				
			tmpStr += " " + tmpHex;  
				
		 }
		 retCode = sendAPDUandDisplay(0, tmpStr);
		 
		 if (retCode != ACSModule.SCARD_S_SUCCESS)
		 {
			 
			 return retCode;
			 
		 }
		 
		 tmpStr = "";
		 for(int i=0; i<2; i++){
			  tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
			  
		  }
		 
		 if (tmpStr.indexOf("90 00") < 0)
		  {
			
			  displayOut(0, 0, "Return string is invalid. Value: " + tmpStr);
			  return INVALID_SW1SW2;
		
		  }
		    
		}
		
		//write data to card
		 clearBuffers();
		 SendBuff[0] =  (byte) 0x80;          // CLA
		 SendBuff[1] =  (byte) 0xD2;          // INS
		 SendBuff[2] =  recNo;        		 // P1    Record to be written
		 SendBuff[3] =  (byte) 0x00;          // P2
		 SendBuff[4] =  dataLen;   			 // P3    Length
		 for (int i =0; i<dataLen; i++)
			 SendBuff[i+5] = dataIn[i];
		 
		 SendLen = dataLen+5;
		 RecvLen[0] = 2;
		 
		 tmpStr = "";
		 for(int i=0; i<SendLen; i++)
		 {
			tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
				
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
				
			tmpStr += " " + tmpHex;  
				
		 }
		 retCode = sendAPDUandDisplay(0, tmpStr);
		 if (retCode != ACSModule.SCARD_S_SUCCESS)
			 return retCode;
		
		 tmpStr = "";
		 for(int i=0; i<2; i++){
			  tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
			  
		  }
		 
		 if (tmpStr.indexOf("90 00") < 0)
		  {
			
			  displayOut(0, 0, "Return string is invalid. Value: " + tmpStr);
			  return INVALID_SW1SW2;
		
		  }
		 
		 return retCode;
		 
	}	
	
	public int selectFile(byte hiAddr, byte loAddr)
	{
		
		String tmpStr="", tmpHex = "";
		
		clearBuffers();
		SendBuff[0]=(byte) 0x80;
		SendBuff[1]=(byte) 0xA4;
		SendBuff[2]=(byte) 0x00;
		SendBuff[3]=(byte) 0x00;
		SendBuff[4]=(byte) 0x02;
		SendBuff[5]=(byte) hiAddr;
		SendBuff[6]=(byte) loAddr;
		SendLen = 7;
		RecvLen[0] = 2;
		
		for(int i=0; i<SendLen; i++)
		{
			tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
			
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
			
			tmpStr += " " + tmpHex;  
			
		}
		
		retCode = sendAPDUandDisplay(0, tmpStr);
		if (retCode != ACSModule.SCARD_S_SUCCESS)
		{
			return retCode;
			
		}
		
		return retCode;
	}
	
	
	public int submitIC()
	{
		
		  clearBuffers();
		  SendBuff[0] = (byte)0x80;        // CLA
		  SendBuff[1] = (byte)0x20;        // INS
		  SendBuff[2] = (byte)0x07;        // P1
		  SendBuff[3] = (byte)0x0;         // P2
		  SendBuff[4] = (byte)0x08;        // P3
		  SendBuff[5] = (byte)0x41;        // A
		  SendBuff[6] = (byte)0x43;        // C
		  SendBuff[7] = (byte)0x4F;        // O
		  SendBuff[8] = (byte)0x53;        // S
		  SendBuff[9] = (byte)0x54;        // T
		  SendBuff[10] = (byte)0x45;       // E
		  SendBuff[11] = (byte)0x53;       // S
		  SendBuff[12] = (byte)0x54;       // T
		  
		  SendLen = 13;
		  RecvLen[0] = 2;
		  String tmpStr ="", tmpHex="";
		  
		  for(int i =0; i<SendLen; i++){
			  tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
		  }
		  
		  retCode = sendAPDUandDisplay(0, tmpStr);
		  
		  if (retCode != ACSModule.SCARD_S_SUCCESS)
			  return retCode;
		  
		  tmpStr="";
		  for(int i=0; i<2; i++){
			  tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
			  
		  }

		  if (tmpStr.indexOf("90 00") < 0)
		  {
			
			  displayOut(0, 0, "Return string is invalid. Value: " + tmpStr);
			  return INVALID_SW1SW2;
		
		  }
		  
		return retCode;
		
	}
	
	public int sendAPDUandDisplay(int sendType, String ApduIn)
	{
		
		ACSModule.SCARD_IO_REQUEST IO_REQ = new ACSModule.SCARD_IO_REQUEST(); 
		ACSModule.SCARD_IO_REQUEST IO_REQ_Recv = new ACSModule.SCARD_IO_REQUEST(); 
		IO_REQ.dwProtocol = PrefProtocols[0];
		IO_REQ.cbPciLength = 8;
		IO_REQ_Recv.dwProtocol = PrefProtocols[0];
		IO_REQ_Recv.cbPciLength = 8;
		displayOut(2, 0, ApduIn);
		String tmpStr = "", tmpHex="";
		RecvLen[0] = 262;
		
		retCode = jacs.jSCardTransmit(hCard, 
									 IO_REQ, 
									 SendBuff, 
									 SendLen, 
									 null, 
									 RecvBuff, 
									 RecvLen);
		
		if (retCode != ACSModule.SCARD_S_SUCCESS)
		{
			
			displayOut(1, retCode, "");
			return retCode;
			
		}
		else
		{
			
			switch(sendType)
			{
			
			case 0: 
				for(int i =0; i<RecvLen[0]; i++)
				{
				
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex;  
					
				}
				
				break;
				
			case 1:
				for(int i =RecvLen[0]-1; i<RecvLen[0]; i++)
				{
					
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex;  
					
				}
				
				if (tmpStr.indexOf("90 00") < 0)
				{
					
					displayOut(1, 0, "Return bytes are not acceptable.");
					
				}
				else
				{
					
					tmpStr = "ATR: ";
					for (int i=0; i<RecvLen[0]-2; i++)
					{
						
						tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
						
						//For single character hex
						if (tmpHex.length() == 1) 
							tmpHex = "0" + tmpHex;
						
						tmpStr += " " + tmpHex;  
						
					}
					
				}
				break;
				
			case 2:
				for(int i =RecvLen[0]-1; i<RecvLen[0]; i++)
				{
					
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex;  
					
				}
				
				if (tmpStr.indexOf("90 00") < 0)
				{
					
					displayOut(1, 0, "Return bytes are not acceptable.");
					
				}
				else
				{
					
					tmpStr = "ATR: ";
					for (int i=0; i<RecvLen[0]-2; i++)
					{
						
						tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
						
						//For single character hex
						if (tmpHex.length() == 1) 
							tmpHex = "0" + tmpHex;
						
						tmpStr += " " + tmpHex;  
						
					}
					
				}
				break;
			}
			
			displayOut(3, 0, tmpStr);
			
		}
		
		return retCode;
	}

	public void displayOut(int mType, int msgCode, String printText)
	{

		switch(mType)
		{
		
			case 1: 
				{
					
					Msg.append("! " + printText);
					Msg.append(ACSModule.GetScardErrMsg(msgCode) + "\n");
					break;
					
				}
			case 2: Msg.append("< " + printText + "\n");break;
			case 3: Msg.append("> " + printText + "\n");break;
			default: Msg.append("- " + printText + "\n");
		
		}
		
	}
	
	public void clearBuffers()
	{
		
		for(int i=0; i<300; i++)
		{
			
			SendBuff[i]=0x00;
			RecvBuff[i]=0x00;
			
		}
		
	}
	
	public void initMenu()
	{
		cbReader.removeAllItems();
		bInit.setEnabled(true);
		bConnect.setEnabled(false);
		bCreate.setEnabled(false);
		bDisconnect.setEnabled(false);			
		Msg.setText("");
		displayOut(0, 0, "Program Ready");
		
	}
    
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new ACOS3CreateFiles().setVisible(true);
            }
        });
    }



}
