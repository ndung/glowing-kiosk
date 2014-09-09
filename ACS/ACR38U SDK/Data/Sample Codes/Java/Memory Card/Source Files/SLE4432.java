/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              SLE4418.java

  Description:       This sample program outlines the steps on how to
                     program SLE4418/4428 memory cards using ACS readers
                     in PC/SC platform.

  Author:	           M.J.E.C.Castillo

  Date:	             September 2, 2008

  Revision Trail:   (Date/Author/Description)
              
======================================================================*/

import java.io.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class SLE4432 extends JFrame implements ActionListener, KeyListener{

	//JPCSC Variables
	int retCode, maxLen;
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
	private JButton bChangeCode, bConnect, bInit, bQuit, bRead;
    private JButton bReadErrCtr, bReset, bSubmitCode, bWrite, bWriteProtect;
    private JComboBox cbReader;
    private JPanel functionsPanel,cardTypePanel;
    private JLabel lblAdd, lblData, lblLen, lblReader;
    private JTextArea mMsg;
    private JRadioButton rb4432, rb4442;
    private JScrollPane scrPanelMsg;
    private JTextField tAdd, tData, tLen;
    private ButtonGroup bgCardType;
    
	static JacspcscLoader jacs = new JacspcscLoader();
    

    public SLE4432() {
    	
    	this.setTitle("SLE 4432/4442/5542");
        initComponents();
        initMenu();
    }

    private void initComponents() {

       setSize(590,370);
   	
	   //sets the location of the form at the center of screen
   	   Dimension dim = getToolkit().getScreenSize();
   	   Rectangle abounds = getBounds();
   	   setLocation((dim.width - abounds.width) / 2, (dim.height - abounds.height) / 2);
   	   requestFocus();
   		
   	   lblReader = new JLabel();
       cbReader = new JComboBox();
       cardTypePanel = new JPanel();
       rb4432 = new JRadioButton();
       rb4442 = new JRadioButton();
       bInit = new JButton();
       bConnect = new JButton();
       functionsPanel = new JPanel();
       lblAdd = new JLabel();
       tAdd = new JTextField();
       lblLen = new JLabel();
       tLen = new JTextField();
       lblData = new JLabel();
       tData = new JTextField();
       bReadErrCtr = new JButton();
       bWriteProtect = new JButton();
       bSubmitCode = new JButton();
       bRead = new JButton();
       bWrite = new JButton();
       bChangeCode = new JButton();
       scrPanelMsg = new JScrollPane();
       mMsg = new JTextArea();
       bReset = new JButton();
       bQuit = new JButton();
       bgCardType = new ButtonGroup();
        
	   lblReader.setText("Select Reader");
	
	   String[] rdrNameDef = {"Select reader "};	
	   cbReader = new JComboBox(rdrNameDef);
	   cbReader.setSelectedIndex(0);
	
	   rb4432.setText("SLE 4432");
	   bgCardType.add(rb4432);
	   rb4442.setText("SLE 4442/5542");
	   bgCardType.add(rb4442); 
	   
	   javax.swing.GroupLayout cardTypePanelLayout = new javax.swing.GroupLayout(cardTypePanel);
	   cardTypePanel.setLayout(cardTypePanelLayout);
	   cardTypePanelLayout.setHorizontalGroup(
	       cardTypePanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
	       .addGroup(cardTypePanelLayout.createSequentialGroup()
	           .addGroup(cardTypePanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
	               .addComponent(rb4432)
	               .addComponent(rb4442))
	           .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
	   );
	   cardTypePanelLayout.setVerticalGroup(
	        cardTypePanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
	        .addGroup(cardTypePanelLayout.createSequentialGroup()
	            .addComponent(rb4432)
	            .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
	            .addComponent(rb4442))
	    );

	    bInit.setText("Initalize");
	    bConnect.setText("Connect");
	    functionsPanel.setBorder(javax.swing.BorderFactory.createTitledBorder("Functions"));
	    lblAdd.setText("Address");
	    lblLen.setText("Length");
	    lblData.setText("Data (string)");
	    bReadErrCtr.setText("Read Err Ctr");
	    bWriteProtect.setText("Write Protect");
	    bSubmitCode.setText("Submit Code");
	    bRead.setText("Read");
	    bWrite.setText("Write");
	    bChangeCode.setText("Change Code");

	    javax.swing.GroupLayout functionsPanelLayout = new javax.swing.GroupLayout(functionsPanel);
	    functionsPanel.setLayout(functionsPanelLayout);
	    functionsPanelLayout.setHorizontalGroup(
	        functionsPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
	        .addGroup(functionsPanelLayout.createSequentialGroup()
	            .addContainerGap()
	            .addGroup(functionsPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
	                .addGroup(functionsPanelLayout.createSequentialGroup()
	                    .addComponent(lblAdd)
	                    .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
	                    .addComponent(tAdd, javax.swing.GroupLayout.PREFERRED_SIZE, 38, javax.swing.GroupLayout.PREFERRED_SIZE)
	                    .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 48, Short.MAX_VALUE)
	                    .addComponent(lblLen)
	                    .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
	                    .addComponent(tLen, javax.swing.GroupLayout.PREFERRED_SIZE, 38, javax.swing.GroupLayout.PREFERRED_SIZE))
	                .addComponent(lblData)
	                .addComponent(tData)
	                .addGroup(functionsPanelLayout.createSequentialGroup()
	                    .addGroup(functionsPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING, false)
	                        .addComponent(bSubmitCode, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
	                        .addComponent(bReadErrCtr, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
	                        .addComponent(bWriteProtect, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
	                    .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
	                    .addGroup(functionsPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
	                        .addComponent(bChangeCode)
	                        .addGroup(functionsPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
	                            .addComponent(bWrite, javax.swing.GroupLayout.DEFAULT_SIZE, 75, Short.MAX_VALUE)
	                            .addComponent(bRead, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)))))
	            .addContainerGap(15, Short.MAX_VALUE))
	    );
	    functionsPanelLayout.setVerticalGroup(
	        functionsPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
	        .addGroup(functionsPanelLayout.createSequentialGroup()
	            .addContainerGap()
	            .addGroup(functionsPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
	                .addComponent(lblAdd)
	                .addComponent(tAdd, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
	                .addComponent(tLen, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
	                .addComponent(lblLen))
	            .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
	            .addComponent(lblData)
	            .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
	            .addComponent(tData, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
	            .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
	            .addGroup(functionsPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
	                .addComponent(bReadErrCtr)
	                .addComponent(bRead))
	            .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
	            .addGroup(functionsPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
	                .addComponent(bWriteProtect)
	                .addComponent(bWrite))
	            .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
	            .addGroup(functionsPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
	                .addComponent(bSubmitCode)
	                .addComponent(bChangeCode))
	            .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
	    );

	    mMsg.setColumns(20);
	    mMsg.setRows(5);
	    scrPanelMsg.setViewportView(mMsg);

	    bReset.setText("Reset");
	    bQuit.setText("Quit");

	    javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
	    getContentPane().setLayout(layout);
	    layout.setHorizontalGroup(
	        layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
	        .addGroup(layout.createSequentialGroup()
	            .addContainerGap()
	            .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
	                .addComponent(functionsPanel, javax.swing.GroupLayout.Alignment.LEADING, 0, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
	                .addGroup(javax.swing.GroupLayout.Alignment.LEADING, layout.createSequentialGroup()
	                    .addComponent(cardTypePanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
	                    .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
	                    .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
	                        .addComponent(bConnect, javax.swing.GroupLayout.DEFAULT_SIZE, 120, Short.MAX_VALUE)
	                        .addComponent(bInit, javax.swing.GroupLayout.DEFAULT_SIZE, 120, Short.MAX_VALUE)))
	                .addGroup(javax.swing.GroupLayout.Alignment.LEADING, layout.createSequentialGroup()
	                    .addComponent(lblReader)
	                    .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
	                    .addComponent(cbReader, 0, 166, Short.MAX_VALUE)))
	            .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
	                .addGroup(layout.createSequentialGroup()
	                    .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
	                    .addComponent(scrPanelMsg, javax.swing.GroupLayout.PREFERRED_SIZE, 265, javax.swing.GroupLayout.PREFERRED_SIZE))
	                .addGroup(layout.createSequentialGroup()
	                    .addGap(38, 38, 38)
	                    .addComponent(bReset, javax.swing.GroupLayout.PREFERRED_SIZE, 83, javax.swing.GroupLayout.PREFERRED_SIZE)
	                    .addGap(18, 18, 18)
	                    .addComponent(bQuit, javax.swing.GroupLayout.PREFERRED_SIZE, 87, javax.swing.GroupLayout.PREFERRED_SIZE)))
	            .addContainerGap())
	    );
	    layout.setVerticalGroup(
	        layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
	        .addGroup(layout.createSequentialGroup()
	            .addContainerGap()
	            .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
	                .addGroup(layout.createSequentialGroup()
	                    .addComponent(scrPanelMsg, javax.swing.GroupLayout.PREFERRED_SIZE, 282, javax.swing.GroupLayout.PREFERRED_SIZE)
	                    .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
	                    .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
	                        .addComponent(bReset)
	                        .addComponent(bQuit)))
	                .addGroup(layout.createSequentialGroup()
	                    .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
	                        .addComponent(lblReader)
	                        .addComponent(cbReader, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
	                    .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
	                    .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
	                        .addComponent(cardTypePanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
	                        .addGroup(layout.createSequentialGroup()
	                            .addComponent(bInit)
	                            .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
	                            .addComponent(bConnect)))
	                    .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
	                    .addComponent(functionsPanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)))
	            .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
	    );
	    
	    mMsg.setLineWrap(true);

	    bInit.addActionListener(this);
	    bQuit.addActionListener(this);
	    bReset.addActionListener(this);
	    bRead.addActionListener(this);
	    bConnect.addActionListener(this);
	    bWrite.addActionListener(this);
	    bReadErrCtr.addActionListener(this);
	    bChangeCode.addActionListener(this);
	    bWriteProtect.addActionListener(this);
	    bSubmitCode.addActionListener(this);
	    rb4432.addActionListener(this);
	    rb4442.addActionListener(this);
           
    }

	public void actionPerformed(ActionEvent e) 
	{
		
		if (bInit==e.getSource())
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
			rb4432.setEnabled(true);
			rb4442.setEnabled(true);
			rb4432.setSelected(true);
			
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
			
		    connActive = true;
		    
		    tLen.setEnabled(true);
		    tData.setEnabled(true);
		    bRead.setEnabled(true);
		    bWrite.setEnabled(true);
			bChangeCode.setEnabled(true);
			bWrite.setEnabled(true);
			bReset.setEnabled(true);
			bWriteProtect.setEnabled(true);
			tLen.setEnabled(true);
			tAdd.setEnabled(true);
			tData.setEnabled(true);
			bReadErrCtr.setEnabled(true);
			bSubmitCode.setEnabled(true);
			
			if(rb4432.isSelected())
		    {
		    	
		    	bSubmitCode.setEnabled(false);
				bReadErrCtr.setEnabled(false);
				bChangeCode.setEnabled(false);
		    	
		    }
		    else
		    {
		    	
		    	bSubmitCode.setEnabled(true);
				bReadErrCtr.setEnabled(true);
				bChangeCode.setEnabled(true);
		    	
		    }
			
		}
		
		if(rb4432 == e.getSource())
		{
			
			tAdd.setText("");
			tData.setText("");
			tLen.setText("");
			tAdd.setEnabled(false);
			tData.setEnabled(false);
			tLen.setEnabled(false);
			bRead.setEnabled(false);
			bWrite.setEnabled(false);
			bChangeCode.setEnabled(false);
			bWriteProtect.setEnabled(false);
			
			if (connActive)
			{
				retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
				connActive = false;
			}
			
			bSubmitCode.setEnabled(false);
			bReadErrCtr.setEnabled(false);
			
		}
		
		if(rb4442 == e.getSource())
		{
			
			tAdd.setText("");
			tData.setText("");
			tLen.setText("");
			tAdd.setEnabled(false);
			tData.setEnabled(false);
			tLen.setEnabled(false);
			bRead.setEnabled(false);
			bWrite.setEnabled(false);
			bChangeCode.setEnabled(false);
			bWriteProtect.setEnabled(false);
			
			if (connActive)
			{
				retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
				connActive = false;
			}
			
			bSubmitCode.setEnabled(false);
			bReadErrCtr.setEnabled(false);
			
		}
		
		if (bRead == e.getSource())
		{
			String tmpStr="", tmpHex="";
			
			if(tAdd.getText().equals(""))
			{
				tAdd.requestFocus();
				return;
			}
			
			if (tLen.getText().equals(""))
			{
				tLen.requestFocus();
				return;
			}
			
			tData.setText("");
			clearBuffers();
			SendBuff[0] = (byte)0xFF;			
			SendBuff[1] = (byte)0xB0;			
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)((Integer)Integer.parseInt(tAdd.getText(), 16)).byteValue();
			SendBuff[4] = (byte)((Integer)Integer.parseInt(tLen.getText(), 16)).byteValue();
			
			SendLen = 5;
			RecvLen[0] = SendBuff[4] + 6;
			
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
			for (int i = 0; i< SendBuff[4]; i++)
				tmpStr = tmpStr + (char)RecvBuff[i];
			
			tData.setText(tmpStr);
						
		}
		
		if (bWrite == e.getSource())
		{
			String tmpStr="", tmpHex="";
			
			if(tAdd.getText().equals(""))
			{
				tAdd.requestFocus();
				return;
			}
			
			if (tLen.getText().equals(""))
			{
				tLen.requestFocus();
				return;
			}

			if (tData.getText().equals(""))
			{
				tData.requestFocus();
				return;
			}
			
			
			tmpStr = tData.getText();
			clearBuffers();
			SendBuff[0] = (byte)0xFF;		
			SendBuff[1] = (byte)0xD0;			
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)((Integer)Integer.parseInt(tAdd.getText(), 16)).byteValue();
			SendBuff[4] = (byte)((Integer)Integer.parseInt(tLen.getText(), 16)).byteValue();
			
			for(int i =0; i<tmpStr.length() -1; i++)
			{
				
				if((int)tmpStr.charAt(i)!=0x00)
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
		
		if (bReadErrCtr == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			
			// 1. Clear all input fields
			ClearFields();

			// 2. Read input fields and pass data to card
			clearBuffers();
			
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0xB1;
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)0x00;
			SendBuff[4] = (byte)0x00;
			
			SendLen = 5;
			RecvLen[0] = 6;
			
			tmpStr = "";
			for(int i =0; i<SendLen; i++)
			{
				
				tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
				
			}
			
			retCode = sendAPDUandDisplay(0, tmpStr);
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
				return;			
		}
		
		if (bWriteProtect == e.getSource())
		{
			String tmpStr="", tmpHex="";
			
			tmpStr = tData.getText();
			clearBuffers();
			SendBuff[0] = (byte)0xFF;		
			SendBuff[1] = (byte)0xD1;			
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)((Integer)Integer.parseInt(tAdd.getText(), 16)).byteValue();
			SendBuff[4] = (byte)((Integer)Integer.parseInt(tLen.getText(), 16)).byteValue();
			
			for(int i =0; i<tmpStr.length(); i++)
			{
				
				if((int)tmpStr.charAt(i)!=0x00)
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
			
			retCode = sendAPDUandDisplay(2, tmpStr);
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
			
			tData.setText("");
			
						
		}
		
		if (bSubmitCode == e.getSource())
		{
			String tmpStr="", tmpHex="", tmpdata="";
						
			tmpdata = tData.getText();
			for(int i =0; i<tmpdata.length(); i++)
			{
				if ((tmpdata.substring(i, 1) != ""))
				{
					tmpStr = tmpStr + tmpdata.substring(i, 1);
				}
					
			}
			
			clearBuffers();
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0x20;
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)0x00;
			SendBuff[4] = (byte)0x03;
			
			for(int i = 0; i <= 2; i++)
			{				
				SendBuff[5+i] = (byte)((int)tmpStr.charAt(i+1));
				
			}
			
			SendLen = SendBuff[4] + 5;
			RecvLen[0] = 6;
			tmpStr = "";
			
			for(int i =0; i<SendLen; i++)
			{
				
				tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
				
			}
			
			
			retCode = sendAPDUandDisplay(2, tmpStr);
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
			
			tData.setText("");
		}
		
		if (bChangeCode == e.getSource())
		{
			
			String tmpStr="", tmpHex="", tmpdata="";
			
			for(int i =0; i<tmpdata.length(); i++)
			{
				if ((tmpdata.substring(i, 1) != ""))
				{
					tmpStr = tmpStr + tmpdata.substring(1,i);
				}
					
			}			

			clearBuffers();

			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0xD2;
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)0x01;
			SendBuff[4] = (byte)0x03;
			
			for(int i = 0; i <= 2; i++)
			{				
				SendBuff[5+i] = (byte)((int)tmpStr.charAt(i+1));
				
			}
			
			SendLen = SendBuff[4] + 5;
			RecvLen[0] = 2;
			tmpStr = "";
			
			for (int i = 0; i < SendLen; i++)
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
			cbReader.addItem("Please select reader ");
			
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
	
	
	private void ClearFields()
	{
		tAdd.setText(""); 
		tLen.setText("");
		tData.setText("");

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
		bReadErrCtr.setEnabled(false);
		bSubmitCode.setEnabled(false);
		bChangeCode.setEnabled(false);
		bWrite.setEnabled(false);
		bReset.setEnabled(false);
		bWriteProtect.setEnabled(false);
		tLen.setEnabled(false);
		tAdd.setEnabled(false);
		tData.setEnabled(false);
		rb4432.setEnabled(false);
		rb4442.setEnabled(false);
		displayOut(0, 0, "Program Ready");
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
                new SLE4432().setVisible(true);
            }
        });
    }



}
