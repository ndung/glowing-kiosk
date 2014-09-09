/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              IICSample.java

  Description:       This sample program outlines the steps on how to
                     program IIC memory cards using ACS readers
                     in PC/SC platform.

  Author:	           M.J.E.C.Castillo

  Date:	             September 2, 2008

  Revision Trail:   (Date/Author/Description)
              
======================================================================*/

import java.io.*;
import java.awt.*;
import java.awt.event.*;

import javax.swing.*;

public class IICSample extends JFrame implements ActionListener, KeyListener{

	//JPCSC Variables
	int retCode;
	boolean connActive; 
	
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
	
	//GUI Variables
    private JButton bConnect, bInit, bPageSize, bQuit, bRead, bReset, bWrite;
    private JComboBox cbCardType, cbReader, cbSize;
    private JPanel functionsPanel;
    private JLabel lblAddress, lblCardType, lblData, lblLength, lblReader, lblSize;
    private JTextArea mMsg;
    private JScrollPane scrPaneMsg;
    private JTextField tAdd1, tAdd2, tAdd3, tData, tLen;
    
	static JacspcscLoader jacs = new JacspcscLoader();
    

    public IICSample() {
    	
    	this.setTitle("ICC Sample");
        initComponents();
        initMenu();
    }

    private void initComponents() {

   		setSize(610,415);
   		
		//sets the location of the form at the center of screen
   		Dimension dim = getToolkit().getScreenSize();
   		Rectangle abounds = getBounds();
   		setLocation((dim.width - abounds.width) / 2, (dim.height - abounds.height) / 2);
   		requestFocus();
   		
        lblReader = new JLabel();
        cbReader = new JComboBox();
        bInit = new JButton();
        lblCardType = new JLabel();
        cbCardType = new JComboBox();
        bConnect = new JButton();
        functionsPanel = new JPanel();
        lblSize = new JLabel();
        cbSize = new JComboBox();
        bPageSize = new JButton();
        lblAddress = new JLabel();
        tAdd1 = new JTextField();
        tAdd2 = new JTextField();
        tAdd3 = new JTextField();
        lblLength = new JLabel();
        tLen = new JTextField();
        lblData = new JLabel();
        tData = new JTextField();
        bRead = new JButton();
        bWrite = new JButton();
        scrPaneMsg = new JScrollPane();
        mMsg = new JTextArea();
        bReset = new JButton();
        bQuit = new JButton();
        
        lblReader.setText("Select Reader");

        String[] rdrNameDef = {"Please select reader "};	
		cbReader = new JComboBox(rdrNameDef);
		cbReader.setSelectedIndex(0);
		
        bInit.setText("Initalize");
        lblCardType.setText("Select Card Type");
        cbCardType.setModel(new DefaultComboBoxModel(new String[] { "Card Type" }));
        bConnect.setText("Connect");
        functionsPanel.setBorder(BorderFactory.createTitledBorder("Functions"));
        lblSize.setText("Size:");
        cbSize.setModel(new DefaultComboBoxModel(new String[] { "Size" }));
        bPageSize.setText("Set Page Size");
        lblAddress.setText("Address");
        lblLength.setText("Length");
        lblData.setText("Data");
        bRead.setText("Read");
        bWrite.setText("Write");

        GroupLayout functionsPanelLayout = new GroupLayout(functionsPanel);
        functionsPanel.setLayout(functionsPanelLayout);
        functionsPanelLayout.setHorizontalGroup(
            functionsPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(functionsPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(functionsPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(functionsPanelLayout.createSequentialGroup()
                        .addGap(32, 32, 32)
                        .addComponent(bRead)
                        .addGap(18, 18, 18)
                        .addComponent(bWrite))
                    .addGroup(functionsPanelLayout.createSequentialGroup()
                        .addComponent(lblSize)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(cbSize, GroupLayout.PREFERRED_SIZE, 71, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(bPageSize))
                    .addGroup(functionsPanelLayout.createSequentialGroup()
                        .addGroup(functionsPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                            .addComponent(lblLength)
                            .addComponent(lblData))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addGroup(functionsPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                            .addComponent(tData, GroupLayout.DEFAULT_SIZE, 164, Short.MAX_VALUE)
                            .addComponent(tLen, GroupLayout.PREFERRED_SIZE, 31, GroupLayout.PREFERRED_SIZE)))
                    .addGroup(functionsPanelLayout.createSequentialGroup()
                        .addComponent(lblAddress)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(tAdd1, GroupLayout.PREFERRED_SIZE, 31, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(tAdd2, GroupLayout.PREFERRED_SIZE, 31, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(tAdd3, GroupLayout.PREFERRED_SIZE, 31, GroupLayout.PREFERRED_SIZE)))
                .addContainerGap())
        );
        functionsPanelLayout.setVerticalGroup(
            functionsPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(functionsPanelLayout.createSequentialGroup()
                .addGroup(functionsPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblSize)
                    .addComponent(cbSize, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(bPageSize))
                .addGap(18, 18, 18)
                .addGroup(functionsPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblAddress)
                    .addComponent(tAdd1, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(tAdd2, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(tAdd3, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(functionsPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblLength)
                    .addComponent(tLen, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(functionsPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblData)
                    .addComponent(tData, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(functionsPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(bWrite)
                    .addComponent(bRead))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        scrPaneMsg.setHorizontalScrollBarPolicy(ScrollPaneConstants.HORIZONTAL_SCROLLBAR_NEVER);

        mMsg.setColumns(20);
        mMsg.setRows(5);
        scrPaneMsg.setViewportView(mMsg);

        bReset.setText("Reset");
        bQuit.setText("Quit");

        GroupLayout layout = new GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                    .addGroup(GroupLayout.Alignment.LEADING, layout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(functionsPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                    .addGroup(GroupLayout.Alignment.LEADING, layout.createSequentialGroup()
                        .addGap(20, 20, 20)
                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                            .addComponent(bInit, GroupLayout.PREFERRED_SIZE, 97, GroupLayout.PREFERRED_SIZE)
                            .addComponent(bConnect, GroupLayout.PREFERRED_SIZE, 96, GroupLayout.PREFERRED_SIZE)
                            .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                                .addGroup(layout.createSequentialGroup()
                                    .addComponent(lblReader)
                                    .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                    .addComponent(cbReader, GroupLayout.PREFERRED_SIZE, 158, GroupLayout.PREFERRED_SIZE))
                                .addGroup(layout.createSequentialGroup()
                                    .addComponent(lblCardType)
                                    .addGap(10, 10, 10)
                                    .addComponent(cbCardType, 0, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))))))
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(scrPaneMsg, GroupLayout.PREFERRED_SIZE, 307, GroupLayout.PREFERRED_SIZE))
                    .addGroup(layout.createSequentialGroup()
                        .addGap(46, 46, 46)
                        .addComponent(bReset, GroupLayout.PREFERRED_SIZE, 87, GroupLayout.PREFERRED_SIZE)
                        .addGap(31, 31, 31)
                        .addComponent(bQuit, GroupLayout.PREFERRED_SIZE, 91, GroupLayout.PREFERRED_SIZE)))
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(scrPaneMsg, GroupLayout.PREFERRED_SIZE, 269, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(bReset)
                            .addComponent(bQuit)))
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(lblReader)
                            .addComponent(cbReader, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bInit)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(lblCardType)
                            .addComponent(cbCardType, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(bConnect)
                        .addGap(11, 11, 11)
                        .addComponent(functionsPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        
        mMsg.setLineWrap(true);
        
        bInit.addActionListener(this);
        bQuit.addActionListener(this);
        bReset.addActionListener(this);
        bRead.addActionListener(this);
        bConnect.addActionListener(this);
        bWrite.addActionListener(this);
        bPageSize.addActionListener(this);
        cbCardType.addActionListener(this);
        tData.addKeyListener(this);
           
    }

	public void actionPerformed(ActionEvent e) 
	{
		if(cbCardType == e.getSource())
		{
			
			if(connActive)
			{
				
				retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
				connActive = false;
				
			}
			
		}
		
		if(bInit == e.getSource())
		{
			
			//1. Establish context and obtain hContext handle
			retCode = jacs.jSCardEstablishContext(ACSModule.SCARD_SCOPE_USER, 0, 0, hContext);
		    
			if (retCode != ACSModule.SCARD_S_SUCCESS)
		    {
		    
				mMsg.append("Calling SCardEstablishContext...FAILED\n");
		      	displayOut(1, retCode, "");
		      	
		    }
			
			//2. List PC/SC card readers installed in the system
			retCode = jacs.jSCardListReaders(hContext, 0, szReaders, cchReaders);
      		
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
				cbReader.addItem("No PC/SC reader detected");
			    
			cbReader.setSelectedIndex(0);
			bConnect.setEnabled(true);
			bInit.setEnabled(false);
			bReset.setEnabled(true);
			cbCardType.setEnabled(true);
			cbCardType.setSelectedIndex(1);
			
		}
		
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
		    	displayOut(1, retCode, "");
	    		connActive = false;
	    		return;
		      			    
		    } 
		    else 
		    {	      	      
		      	
		    	displayOut(0, 0, "Successful connection to " + (String)cbReader.getSelectedItem());
		      	
		    }
			
		    connActive=true;
		    cbSize.setEnabled(true);
		    tAdd1.setEnabled(true);
		    tAdd2.setEnabled(true);
		    tAdd3.setEnabled(true);
		    tLen.setEnabled(true);
		    tData.setEnabled(true);
		    bRead.setEnabled(true);
		    bWrite.setEnabled(true);
		    bPageSize.setEnabled(true);
		    cbSize.setSelectedIndex(1);
		    if (cbCardType.getSelectedIndex() == 12)
		    	tAdd1.setEnabled(true);
		    else
		    	tAdd1.setEnabled(false);
		    
		}
		
		if(bPageSize == e.getSource())
		{
			String tmpStr="", tmpHex="";
			clearBuffers();
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0x01;
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)0x00;
			SendBuff[4] = (byte)0x01;
			
			switch(cbSize.getSelectedIndex())
			{
			
				case 1: SendBuff[5] = (byte)0x03;
				case 2: SendBuff[5] = (byte)0x04;
				case 3: SendBuff[5] = (byte)0x05;
				case 4: SendBuff[5] = (byte)0x06;
				case 5: SendBuff[5] = (byte)0x07;
				
			}
			
			SendLen = 6;
			RecvLen[0] = 2;
			
			for(int i =0; i<SendLen; i++)
			{
				
				tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
				
			}
			
			retCode = sendAPDUandDisplay(0, tmpStr);
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
		}
		
		if(bRead== e.getSource())
		{
			String tmpStr="", tmpHex="";
			int indx;
			
			//check for all input fields
			if(cbCardType.getSelectedIndex() == 12)
				indx = 1;
			else
				indx = 0;
			
			if (indx == 1)
			{
				
				if(tAdd1.getText().equals(""))
				{
					tAdd1.requestFocus();
					return;
				}
				
			}
			
			if(tAdd2.getText().equals(""))
			{
				tAdd2.requestFocus();
				return;
			}
			
			if(tAdd3.getText().equals(""))
			{
				tAdd3.requestFocus();
				return;
			}
			
			//read input field and pass data to card
			tData.setText("");
			clearBuffers();
			SendBuff[0] = (byte)0xFF;
			
			if((cbCardType.getSelectedIndex()==12)&&((Integer)Integer.parseInt(tAdd1.getText(), 16)).byteValue()==1)
			{
				SendBuff[1] = (byte)0xB1;
			}
			else
				SendBuff[1] = (byte)0xB0;
			
			SendBuff[2] = (byte)((Integer)Integer.parseInt(tAdd2.getText(), 16)).byteValue();
			SendBuff[3] = (byte)((Integer)Integer.parseInt(tAdd3.getText(), 16)).byteValue();
			SendBuff[4] = (byte)((Integer)Integer.parseInt(tLen.getText(), 16)).byteValue();
			
			SendLen = 5;
			RecvLen[0] = SendBuff[4] + 2;
			
			for(int i =0; i<SendLen; i++)
			{
				
				tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
				
			}
			
			retCode = sendAPDUandDisplay(2, tmpStr);
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			tmpStr = "";
			
			//display data read from card and display into data text box
			for (int i = 0; i< (SendBuff[4] & 0xFF); i++)
				tmpStr = tmpStr + (char)RecvBuff[i];
			
			tData.setText(tmpStr);
			
		}
		
		if(bWrite == e.getSource())
		{
			
			int indx;
			String tmpStr="", tmpHex="";
			
			//check for all input fields
			if(cbCardType.getSelectedIndex() == 12)
				indx = 1;
			else
				indx = 0;
			
			if (indx == 1)
			{
				
				if(tAdd1.getText().equals(""))
				{
					tAdd1.requestFocus();
					return;
				}
				
			}
			
			if(tAdd2.getText().equals(""))
			{
				tAdd2.requestFocus();
				return;
			}
			
			if(tAdd3.getText().equals(""))
			{
				tAdd3.requestFocus();
				return;
			}
			
			if(tData.getText().equals(""))
			{
				tData.requestFocus();
				return;
			}
			
			//read input field and pass data to card
			tmpStr = tData.getText();
			clearBuffers();
			SendBuff[0] = (byte)0xFF;
			
			if((cbCardType.getSelectedIndex()==12)&&((Integer)Integer.parseInt(tAdd1.getText(), 16)).byteValue()==1)
			{
				SendBuff[1] = (byte)0xD1;
			}
			else
				SendBuff[1] = (byte)0xD0;
			
			SendBuff[2] = (byte)((Integer)Integer.parseInt(tAdd2.getText(), 16)).byteValue();
			SendBuff[3] = (byte)((Integer)Integer.parseInt(tAdd3.getText(), 16)).byteValue();
			SendBuff[4] = (byte)((Integer)Integer.parseInt(tLen.getText(), 16)).byteValue();
			
			for(int i =0; i<tmpStr.length(); i++)
			{
				
				if((int)tmpStr.charAt(i)!=0)
					SendBuff[5+i] = (byte)((int)tmpStr.charAt(i));
				else
					SendBuff[5+i] = (byte)0x00;
				
			}
			
			SendLen = 5 + SendBuff[4];
			RecvLen[0] = 2;
			
			tmpStr="";
			
			for(int i =0; i<SendLen; i++)
			{
				
				tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
				
			}
			
			retCode = sendAPDUandDisplay(0, tmpStr);
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
			
			tData.setText("");
			
		}
		
		if(bReset == e.getSource())
		{
			
			//disconnect
			if (connActive){
				
				retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
				connActive= false;
			
			}
		    
			//release context
			retCode = jacs.jSCardReleaseContext(hContext);
			//System.exit(0);
			
			mMsg.setText("");
			initMenu();
			cbReader.removeAllItems();
			cbReader.addItem("Please select reader                   ");
			
		}
		
		if(bQuit == e.getSource())
		{
			
			this.dispose();
			
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
	
	
	public int sendAPDUandDisplay(int sendType, String ApduIn)
	{
		
		ACSModule.SCARD_IO_REQUEST IO_REQ = new ACSModule.SCARD_IO_REQUEST(); 
		ACSModule.SCARD_IO_REQUEST IO_REQ_Recv = new ACSModule.SCARD_IO_REQUEST(); 
		IO_REQ.dwProtocol = PrefProtocols[0];
		IO_REQ.cbPciLength = 8;
		IO_REQ_Recv.dwProtocol = PrefProtocols[0];
		IO_REQ_Recv.cbPciLength = 8;
		
		String tmpStr = "", tmpHex="";
		RecvLen[0] = 262;
		displayOut(2, 0, ApduIn);
		
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
			tmpStr="";
			
			switch(sendType)
			{
			
			//read all data received
			case 0: 
			{
				for(int i =0; i<RecvLen[0]; i++)
				{
				
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex;  
					
				}
				
				break;
			}
			//read ATR after checking SW1/SW2
			case 1:
			{	
				for(int i =RecvLen[0]-2; i<RecvLen[0]; i++)
				{
					
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex;  
					
				}
				
				if(!tmpStr.trim().equals("90 00"))
					displayOut(4, 0, "return Bytes are not Acceptible");
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
			//read data after checking SW1/SW2
			case 2:
			{
				for(int i =RecvLen[0]-2; i<RecvLen[0]; i++)
				{
					
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex;  
					
				}
				
				if(!tmpStr.trim().equals("90 00"))
					displayOut(1, 0, "Return bytes are not acceptable.");
				else
				{
					
					tmpStr="";
					for(int i =0; i<RecvLen[0]-2; i++)
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
					
					mMsg.append("! " + printText);
					mMsg.append(ACSModule.GetScardErrMsg(msgCode) + "\n");
					break;
					
				}
			case 2: mMsg.append("< " + printText + "\n");break;
			case 3: mMsg.append("> " + printText + "\n");break;
			default: mMsg.append("- " + printText + "\n");
		
		}
		
	}
	

	
	public void initMenu()
	{
	
		connActive = false;
		bInit.setEnabled(true);
		bConnect.setEnabled(false);
		bRead.setEnabled(false);
		bWrite.setEnabled(false);
		bReset.setEnabled(false);
		cbCardType.setEnabled(false);
		displayOut(0, 0, "Program Ready");
		cbCardType.addItem("Auto Detect");
		cbCardType.addItem("1Kbit");
		cbCardType.addItem("2Kbit");
		cbCardType.addItem("4Kbit");
		cbCardType.addItem("8Kbit");
		cbCardType.addItem("16Kbit");
		cbCardType.addItem("32Kbit");
		cbCardType.addItem("64Kbit");
		cbCardType.addItem("128Kbit");
		cbCardType.addItem("256Kbit");
		cbCardType.addItem("512Kbit");
		cbCardType.addItem("1024Kbit");
		cbSize.setEnabled(false);
	    tAdd1.setEnabled(false);
	    tAdd2.setEnabled(false);
	    tAdd3.setEnabled(false);
	    tLen.setEnabled(false);
	    tData.setEnabled(false);
	    bRead.setEnabled(false);
	    bWrite.setEnabled(false);
	    cbSize.addItem("8-byte");
	    cbSize.addItem("16-byte");
	    cbSize.addItem("32-byte");
	    cbSize.addItem("64-byte");
	    cbSize.addItem("128-byte");
	    bPageSize.setEnabled(false);
	    tAdd1.setText("");
	    tAdd2.setText("");
	    tAdd3.setText("");
	    tLen.setText("");
	    tData.setText("");
		
	}
	
	public void keyReleased(KeyEvent ke) {
		
	}
	
	public void keyPressed(KeyEvent ke) {
  		//restrict paste actions
		if (ke.getKeyCode() == KeyEvent.VK_V ) 
			ke.setKeyCode(KeyEvent.VK_UNDO );						
  	}
	
	public void keyTyped(KeyEvent ke) 
	{  		
  		Character x = (Character)ke.getKeyChar();
  		char empty = '\r';
  					  
		//Limit character length
		if   (((JTextField)ke.getSource()).getText().length() >= Integer.parseInt(tLen.getText()) ) {
		
  				ke.setKeyChar(empty);
  				return;
  		}			
  			
  		
  				    	
	}
    
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new IICSample().setVisible(true);
            }
        });
    }



}
