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

public class SLE4418 extends JFrame implements ActionListener, KeyListener{

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
    private JButton bConnect;
    private JButton bInit;
    private JButton bQuit;
    private JButton bRead;
    private JButton bReadErrCtr;
    private JButton bReadwoProtect;
    private JButton bReset;
    private JButton bSubmitCode;
    private JButton bWrite;
    private JButton bWriteProtect;
    private JPanel cardTypePanel;
    private JComboBox cbReader;
    private JPanel functionsPanel;
    private JLabel lblAdd;
    private JLabel lblData;
    private JLabel lblLen;
    private JLabel lblReader;
    private JTextArea mMsg;
    private JRadioButton rb4418;
    private JRadioButton rb4428;
    private JScrollPane scrPanelMsg;
    private JTextField tAdd1;
    private JTextField tAdd2;
    private JTextField tData;
    private JTextField tLen;
    private ButtonGroup bgCardType;
    
	static JacspcscLoader jacs = new JacspcscLoader();
    

    public SLE4418() {
    	
    	this.setTitle("SLE 4418/4428");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

   		setSize(590,370);
        lblReader = new JLabel();
        cbReader = new JComboBox();
        cardTypePanel = new JPanel();
        rb4418 = new JRadioButton();
        rb4428 = new JRadioButton();
        bInit = new JButton();
        bConnect = new JButton();
        functionsPanel = new JPanel();
        lblAdd = new JLabel();
        tAdd1 = new JTextField();
        lblLen = new JLabel();
        tLen = new JTextField();
        lblData = new JLabel();
        tData = new JTextField();
        bReadwoProtect = new JButton();
        bWriteProtect = new JButton();
        bSubmitCode = new JButton();
        bRead = new JButton();
        bWrite = new JButton();
        bReadErrCtr = new JButton();
        tAdd2 = new JTextField();
        scrPanelMsg = new JScrollPane();
        mMsg = new JTextArea();
        bReset = new JButton();
        bQuit = new JButton();
        bgCardType = new ButtonGroup();
        
        lblReader.setText("Select Reader");

        String[] rdrNameDef = {"Select reader "};	
		cbReader = new JComboBox(rdrNameDef);
		cbReader.setSelectedIndex(0);
		
        cardTypePanel.setBorder(BorderFactory.createTitledBorder("Card Type"));

        rb4418.setText("SLE 4418");
        bgCardType.add(rb4418);
        rb4428.setText("SLE 4428");
        bgCardType.add(rb4428);
        GroupLayout cardTypePanelLayout = new GroupLayout(cardTypePanel);
        cardTypePanel.setLayout(cardTypePanelLayout);
        cardTypePanelLayout.setHorizontalGroup(
            cardTypePanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(cardTypePanelLayout.createSequentialGroup()
                .addGroup(cardTypePanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(rb4418)
                    .addComponent(rb4428))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        cardTypePanelLayout.setVerticalGroup(
            cardTypePanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(cardTypePanelLayout.createSequentialGroup()
                .addComponent(rb4418)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rb4428))
        );

        bInit.setText("Initalize");

        bConnect.setText("Connect");

        functionsPanel.setBorder(BorderFactory.createTitledBorder("Functions"));

        lblAdd.setText("Address");

        lblLen.setText("Length");

        lblData.setText("Data (string)");

        bReadwoProtect.setText("Read w/o Prot");

        bWriteProtect.setText("Write Protect");

        bSubmitCode.setText("Submit Code");

        bRead.setText("Read w/ Prot");

        bWrite.setText("Write");

        bReadErrCtr.setText("Read Err Ctr");

        GroupLayout functionsPanelLayout = new GroupLayout(functionsPanel);
        functionsPanel.setLayout(functionsPanelLayout);
        functionsPanelLayout.setHorizontalGroup(
            functionsPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(functionsPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(functionsPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(functionsPanelLayout.createSequentialGroup()
                        .addComponent(lblAdd)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(tAdd1, GroupLayout.PREFERRED_SIZE, 38, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(tAdd2, GroupLayout.PREFERRED_SIZE, 38, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(lblLen)
                        .addGap(18, 18, 18)
                        .addComponent(tLen, GroupLayout.PREFERRED_SIZE, 38, GroupLayout.PREFERRED_SIZE))
                    .addComponent(lblData)
                    .addGroup(GroupLayout.Alignment.TRAILING, functionsPanelLayout.createSequentialGroup()
                        .addGroup(functionsPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                            .addComponent(tData, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 224, Short.MAX_VALUE)
                            .addGroup(functionsPanelLayout.createSequentialGroup()
                                .addGroup(functionsPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                                    .addComponent(bSubmitCode, GroupLayout.DEFAULT_SIZE, 107, Short.MAX_VALUE)
                                    .addComponent(bWrite, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, 107, Short.MAX_VALUE)
                                    .addComponent(bReadwoProtect, GroupLayout.PREFERRED_SIZE, 120, GroupLayout.PREFERRED_SIZE))
                                .addGap(18, 18, 18)
                                .addGroup(functionsPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                                    .addComponent(bWriteProtect, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                    .addComponent(bReadErrCtr, GroupLayout.DEFAULT_SIZE, 99, Short.MAX_VALUE)
                                    .addComponent(bRead, GroupLayout.DEFAULT_SIZE, 99, Short.MAX_VALUE))))
                        .addGap(63, 63, 63)))
                .addContainerGap())
        );
        functionsPanelLayout.setVerticalGroup(
            functionsPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(functionsPanelLayout.createSequentialGroup()
                .addGroup(functionsPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblAdd)
                    .addComponent(tAdd1, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(tAdd2, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(lblLen)
                    .addComponent(tLen, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(lblData)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tData, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(functionsPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(functionsPanelLayout.createSequentialGroup()
                        .addComponent(bReadwoProtect)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bWrite)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bSubmitCode))
                    .addGroup(functionsPanelLayout.createSequentialGroup()
                        .addComponent(bRead)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bWriteProtect)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bReadErrCtr)))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        mMsg.setColumns(20);
        mMsg.setRows(5);
        scrPanelMsg.setViewportView(mMsg);

        bReset.setText("Reset");

        bQuit.setText("Quit");

        GroupLayout layout = new GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(functionsPanel, GroupLayout.PREFERRED_SIZE, 280, GroupLayout.PREFERRED_SIZE)
                    .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                        .addGroup(GroupLayout.Alignment.LEADING, layout.createSequentialGroup()
                            .addComponent(lblReader)
                            .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                            .addComponent(cbReader, 0, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                        .addGroup(GroupLayout.Alignment.LEADING, layout.createSequentialGroup()
                            .addComponent(cardTypePanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                            .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                            .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                                .addComponent(bConnect, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                .addComponent(bInit, GroupLayout.DEFAULT_SIZE, 154, Short.MAX_VALUE)))))
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(scrPanelMsg, GroupLayout.PREFERRED_SIZE, 265, GroupLayout.PREFERRED_SIZE))
                    .addGroup(layout.createSequentialGroup()
                        .addGap(50, 50, 50)
                        .addComponent(bReset, GroupLayout.PREFERRED_SIZE, 83, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bQuit, GroupLayout.PREFERRED_SIZE, 87, GroupLayout.PREFERRED_SIZE)))
                .addContainerGap(14, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(lblReader)
                            .addComponent(cbReader, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                            .addComponent(cardTypePanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(bInit)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(bConnect)))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(functionsPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(scrPanelMsg, GroupLayout.PREFERRED_SIZE, 271, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(bReset)
                            .addComponent(bQuit))))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        
        bInit.addActionListener(this);
        bQuit.addActionListener(this);
        bReset.addActionListener(this);
        bRead.addActionListener(this);
        bConnect.addActionListener(this);
        bWrite.addActionListener(this);
        bWriteProtect.addActionListener(this);
        bReadErrCtr.addActionListener(this);
        bReadwoProtect.addActionListener(this);
        bSubmitCode.addActionListener(this);
        rb4418.addActionListener(this);
        rb4428.addActionListener(this);
        tData.addKeyListener(this);
           
    }

	public void actionPerformed(ActionEvent e) 
	{
		
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
			rb4418.setSelected(true);
			rb4418.setEnabled(true);
			rb4428.setEnabled(true);
			
		}
		
		if(bConnect == e.getSource())
		{
			int i;
			String tempstr="", tmpHex="";		
		
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
		    
		    clearBuffers();
		    SendBuff[0] = (byte)0xFF;
		    SendBuff[1] = (byte)0xA4;
		    SendBuff[2] = (byte)0x00;
		    SendBuff[3] = (byte)0x00;
		    SendBuff[4] = (byte)0x01;
		    SendBuff[5] = (byte)0x05;
		    
		    SendLen = 6;
		    RecvLen[0] = 255;
		    
		    for(i =0; i<SendLen; i++)
			{
				
				tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tempstr += " " + tmpHex;  
				
			}
			
			retCode = sendAPDUandDisplay(0, tempstr);
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
		
		    connActive=true;
		    tAdd1.setEnabled(true);
		    tAdd2.setEnabled(true);
		    tLen.setEnabled(true);
		    tData.setEnabled(true);
		    bRead.setEnabled(true);
		    bWrite.setEnabled(true);
		    bReadErrCtr.setEnabled(true);
		    bSubmitCode.setEnabled(true);
		    bReadwoProtect.setEnabled(true);
		    bWriteProtect.setEnabled(true);
		    
		    if(rb4418.isSelected())
		    {
		    	
		    	bSubmitCode.setEnabled(false);
				bReadErrCtr.setEnabled(false);
		    	
		    }
		    else
		    {
		    	
		    	bSubmitCode.setEnabled(true);
				bReadErrCtr.setEnabled(true);
		    	
		    }
		    
		}
		
		if(bReadwoProtect==e.getSource())
		{
			
			if(tAdd1.getText().equals(""))
			{
				
				tAdd1.setText("0");
				tAdd1.requestFocus();
				return;
				
			}
			
			if(tAdd2.getText().equals(""))
			{
				
				tAdd2.requestFocus();
				return;
				
			}
			
			if(tLen.getText().equals(""))
			{
				
				tLen.requestFocus();
				return;
				
			}
			
			//send all data to card
			clearBuffers();
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0xB0;
			SendBuff[2] = (byte)((Integer)Integer.parseInt(tAdd1.getText(), 16)).byteValue();
			SendBuff[3] = (byte)((Integer)Integer.parseInt(tAdd2.getText(), 16)).byteValue();
			SendBuff[4] = (byte)((Integer)Integer.parseInt(tLen.getText(), 16)).byteValue();
			
			SendLen = 5;
			RecvLen[0] = 2;
			
			String tmpStr="", tmpHex="";
			
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
			
			//display data read from card
			tmpStr="";
			for (int i = 0; i< (SendBuff[4] & 0xFF); i++)
				tmpStr = tmpStr + (char)RecvBuff[i];
			
			tData.setText(tmpStr);
			
		}
		
		if(bRead == e.getSource())
		{
			int protLen;
			
			if(tAdd1.getText().equals(""))
			{
				
				tAdd1.setText("0");
				tAdd1.requestFocus();
				return;
				
			}
			
			if(tAdd2.getText().equals(""))
			{
				
				tAdd2.requestFocus();
				return;
				
			}
			
			if(tLen.getText().equals(""))
			{
				
				tLen.requestFocus();
				return;
				
			}
			
			//send all data to card
			clearBuffers();
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0xB2;
			SendBuff[2] = (byte)((Integer)Integer.parseInt(tAdd1.getText(), 16)).byteValue();
			SendBuff[3] = (byte)((Integer)Integer.parseInt(tAdd2.getText(), 16)).byteValue();
			SendBuff[4] = (byte)((Integer)Integer.parseInt(tLen.getText(), 16)).byteValue();
			
			SendLen = 5;
			protLen = 1+(SendBuff[4] /8);
			RecvLen[0] = SendBuff[4] + protLen + 2;
			
			String tmpStr="", tmpHex="";
			
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
			
			//display data read from card
			tmpStr="";
			for (int i = 0; i< (SendBuff[4] & 0xFF); i++)
				tmpStr = tmpStr + (char)RecvBuff[i];
			
			tData.setText(tmpStr);
			
		}
		
		if(bWrite == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			
			//check for all input fields
			if(tAdd1.getText().equals(""))
			{
				tAdd1.setText("0");
				tAdd1.requestFocus();
				return;
			}
			
			if(tAdd2.getText().equals(""))
			{
				tAdd2.requestFocus();
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
			SendBuff[1] = (byte)0xD0;
			SendBuff[2] = (byte)((Integer)Integer.parseInt(tAdd1.getText(), 16)).byteValue();
			SendBuff[3] = (byte)((Integer)Integer.parseInt(tAdd2.getText(), 16)).byteValue();
			SendBuff[4] = (byte)((Integer)Integer.parseInt(tLen.getText(), 16)).byteValue();
			
			for(int i =0; i<tmpStr.length(); i++)
			{
				SendBuff[5+i] = (byte)((int)tmpStr.charAt(i));
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
		
		if(bWriteProtect==e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			
			//check for all input fields
			if(tAdd1.getText().equals(""))
			{
				tAdd1.setText("0");
				tAdd1.requestFocus();
				return;
			}
			
			if(tAdd2.getText().equals(""))
			{
				tAdd2.requestFocus();
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
			SendBuff[1] = (byte)0xD1;
			SendBuff[2] = (byte)((Integer)Integer.parseInt(tAdd1.getText(), 16)).byteValue();
			SendBuff[3] = (byte)((Integer)Integer.parseInt(tAdd2.getText(), 16)).byteValue();
			SendBuff[4] = (byte)((Integer)Integer.parseInt(tLen.getText(), 16)).byteValue();
			
			for(int i =0; i<tmpStr.length(); i++)
			{
				SendBuff[5+i] = (byte)((int)tmpStr.charAt(i));
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
		
		if(bSubmitCode == e.getSource())
		{
			String tmpStr="", tmpHex="";
			//verify input
			tAdd1.setText("");
			tAdd2.setText("");
			tLen.setText("");
			tData.setText(tData.getText().toUpperCase());
			if(tData.getText().equals(""))
			{
				tData.requestFocus();
				return;
			}
			
			tmpStr = tData.getText().trim();
			
			if(tmpStr.length() != 4)
			{
				tData.selectAll();
				tData.requestFocus();
				return;
			}
			
			//send all input to card
			clearBuffers();
			tmpStr = tData.getText().trim();
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0x20;
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)0x00;
			SendBuff[4] = (byte)0x02;
			
			for(int i =0; i<1 -1; i++)
			{
				tmpStr = ""+tmpStr.charAt(i*2)+1+tmpStr.charAt(i*2)+2;
				SendBuff[5] = (byte)((Integer)Integer.parseInt(tmpStr, 16)).byteValue();
			}
			
			SendLen = 5+ SendBuff[4];
			RecvLen[0] = 5;
			
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
		
		if(bReadErrCtr == e.getSource())
		{
			String tmpStr="", tmpHex="";
			
			tAdd1.setText("");
			tAdd2.setText("");
			tLen.setText("");
			tData.setText("");
			
			clearBuffers();
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0xB1;
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)0x00;
			SendBuff[4] = (byte)0x03;
			SendLen = 5;
			RecvLen[0] = 5;
			
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
			cbReader.addItem("Select reader");
			
		}
		
		if(rb4428 == e.getSource())
		{
			
			tAdd1.setText("");
			tAdd2.setText("");
			tData.setText("");
			tLen.setText("");
			tAdd1.setEnabled(false);
			tAdd2.setEnabled(false);
			tData.setEnabled(false);
			tLen.setEnabled(false);
			bReadwoProtect.setEnabled(false);
			bRead.setEnabled(false);
			bWrite.setEnabled(false);
			bWriteProtect.setEnabled(false);
			
			if (connActive)
			{
				retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
				connActive = false;
			}
			
			bSubmitCode.setEnabled(false);
			bReadErrCtr.setEnabled(false);
			
		}
		
		if(rb4418 == e.getSource())
		{
			
			tAdd1.setText("");
			tAdd2.setText("");
			tData.setText("");
			tLen.setText("");
			tAdd1.setEnabled(false);
			tAdd2.setEnabled(false);
			tData.setEnabled(false);
			tLen.setEnabled(false);
			bReadwoProtect.setEnabled(false);
			bRead.setEnabled(false);
			bWrite.setEnabled(false);
			bWriteProtect.setEnabled(false);
			
			if (connActive)
			{
				retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
				connActive = false;
			}
			
			bSubmitCode.setEnabled(false);
			bReadErrCtr.setEnabled(false);
			
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
		displayOut(0, 0, "Program Ready");
	    tAdd1.setEnabled(false);
	    tAdd2.setEnabled(false);
	    tLen.setEnabled(false);
	    tData.setEnabled(false);
	    bRead.setEnabled(false);
	    bWrite.setEnabled(false);
	    bReadErrCtr.setEnabled(false);
	    bSubmitCode.setEnabled(false);
	    bReadwoProtect.setEnabled(false);
	    bWriteProtect.setEnabled(false);
	    rb4418.setEnabled(false);
	    rb4428.setEnabled(false);
		
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
                new SLE4418().setVisible(true);
            }
        });
    }



}
