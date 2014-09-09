/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              ACOS3ReadWriteBinary.java

  Description:       This sample program outlines the steps on how to
                     use the ACOS card for Reading and Writing in Binary
                     process using the PC/SC platform.
                    
  Author:            M.J.E.C. Castillo

  Date:              September 1, 2008

  Revision Trail:   (Date/Author/Description)

======================================================================*/

import java.io.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;


public class deviceProgramming extends JFrame implements ActionListener{

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
	int [] nBytesRet =new int[1];
	byte [] SendBuff = new byte[262];
	byte [] RecvBuff = new byte[262];
	byte [] szReaders = new byte[1024];
	
	//GUI Variables
	private JButton bClear, bConn, bGetBuzz, bGetFW, bGetLed, bInit, bReset, bQuit, bSetBuzz, bSetLed, bSetBuzzDur, bStartBuzz, bStopBuzz;
    private JCheckBox cb1, cb2, cb3, cb4, cb5, cb6, cb7, cb8, cbGreen, cbRed;
    private JTextArea Msg;
    private JLabel lblReader, lblValue;
    private JPanel ledPanel, midPanel, bottomPanel, topPanel, rightPanel, buzzPanel;
    private JScrollPane msgScrollPane;
    private JComboBox rdrName;
    private JTextField txtVal;
	
	static JacspcscLoader jacs = new JacspcscLoader();
    

    public deviceProgramming() {
    	
    	this.setTitle("Device Programming");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

		setSize(715,680);
        topPanel = new JPanel();
        lblReader = new JLabel();
        bInit = new JButton();
        bConn = new JButton();
        bGetFW = new JButton();
        midPanel = new JPanel();
        bGetLed = new JButton();
        bSetLed = new JButton();
        ledPanel = new JPanel();
        cbRed = new JCheckBox();
        cbGreen = new JCheckBox();
        bottomPanel = new JPanel();
        cb1 = new JCheckBox();
        cb2 = new JCheckBox();
        cb3 = new JCheckBox();
        cb4 = new JCheckBox();
        cb5 = new JCheckBox();
        cb6 = new JCheckBox();
        cb7 = new JCheckBox();
        cb8 = new JCheckBox();
        bGetBuzz = new JButton();
        bSetBuzz = new JButton();
        msgScrollPane = new JScrollPane();
        Msg = new JTextArea();
        bClear = new JButton();
        bReset = new JButton();
        bQuit = new JButton();
        rightPanel = new JPanel();
        buzzPanel = new JPanel();
        lblValue = new JLabel();
        txtVal = new JTextField();
        bSetBuzzDur = new JButton();
        bStartBuzz = new JButton();
        bStopBuzz = new JButton();
        
        lblReader.setText("Select Reader");

		String[] rdrNameDef = {"Please select reader                   "};	
		rdrName = new JComboBox(rdrNameDef);
		rdrName.setSelectedIndex(0);
		
	    bInit.setText("Initialize");

        bConn.setText("Connect");

        bGetFW.setText("Get Firmware Version");

        GroupLayout topPanelLayout = new GroupLayout(topPanel);
        topPanel.setLayout(topPanelLayout);
        topPanelLayout.setHorizontalGroup(
            topPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(topPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(topPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(GroupLayout.Alignment.TRAILING, topPanelLayout.createSequentialGroup()
                        .addComponent(lblReader, GroupLayout.PREFERRED_SIZE, 80, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(rdrName, 0, 202, Short.MAX_VALUE))
                    .addGroup(GroupLayout.Alignment.TRAILING, topPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                        .addComponent(bGetFW, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(bConn, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(bInit, GroupLayout.DEFAULT_SIZE, 104, Short.MAX_VALUE)))
                .addContainerGap())
        );
        topPanelLayout.setVerticalGroup(
            topPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(topPanelLayout.createSequentialGroup()
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addGroup(topPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblReader)
                    .addComponent(rdrName, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(bInit)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bConn)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bGetFW))
        );

        bGetLed.setText("GetLED State");
        bSetLed.setText("Set LED State");
        ledPanel.setBorder(BorderFactory.createTitledBorder("LED Setting"));
        cbRed.setText("Red");
        cbGreen.setText("Green");

        GroupLayout ledPanelLayout = new GroupLayout(ledPanel);
        ledPanel.setLayout(ledPanelLayout);
        ledPanelLayout.setHorizontalGroup(
            ledPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(ledPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(cbRed)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, 25, Short.MAX_VALUE)
                .addComponent(cbGreen, GroupLayout.PREFERRED_SIZE, 60, GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );
        ledPanelLayout.setVerticalGroup(
            ledPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(ledPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(ledPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(cbRed)
                    .addComponent(cbGreen))
                .addContainerGap(13, Short.MAX_VALUE))
        );

        GroupLayout midPanelLayout = new GroupLayout(midPanel);
        midPanel.setLayout(midPanelLayout);
        midPanelLayout.setHorizontalGroup(
            midPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(midPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(ledPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(midPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(bSetLed, GroupLayout.DEFAULT_SIZE, 127, Short.MAX_VALUE)
                    .addComponent(bGetLed, GroupLayout.DEFAULT_SIZE, 127, Short.MAX_VALUE))
                .addContainerGap())
        );
        midPanelLayout.setVerticalGroup(
            midPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(midPanelLayout.createSequentialGroup()
                .addGroup(midPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addGroup(midPanelLayout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(bGetLed)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(bSetLed))
                    .addComponent(ledPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        buzzPanel.setBorder(BorderFactory.createTitledBorder("Set Buzzer Duration (x10 mSec)"));
        lblValue.setText("Value");
        bSetBuzzDur.setText("Set Buzzer Duration");
        bStartBuzz.setText("Start Buzzer Tone");
        bStopBuzz.setText("Stop Buzzer Tone");

        GroupLayout buzzPanelLayout = new GroupLayout(buzzPanel);
        buzzPanel.setLayout(buzzPanelLayout);
        buzzPanelLayout.setHorizontalGroup(
            buzzPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(buzzPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(buzzPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                    .addComponent(bStartBuzz, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addGroup(GroupLayout.Alignment.LEADING, buzzPanelLayout.createSequentialGroup()
                        .addComponent(lblValue, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                        .addGap(12, 12, 12)
                        .addComponent(txtVal, GroupLayout.PREFERRED_SIZE, 79, GroupLayout.PREFERRED_SIZE)))
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(buzzPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(bStopBuzz, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bSetBuzzDur, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addContainerGap(20, Short.MAX_VALUE))
        );
        buzzPanelLayout.setVerticalGroup(
            buzzPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(GroupLayout.Alignment.TRAILING, buzzPanelLayout.createSequentialGroup()
                .addGroup(buzzPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(bStartBuzz)
                    .addComponent(bStopBuzz))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addGroup(buzzPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(txtVal, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(lblValue)
                    .addComponent(bSetBuzzDur))
                .addContainerGap())
        );

        bottomPanel.setBorder(BorderFactory.createTitledBorder("LED and Buzzer Setting"));
        cb1.setText("Enable ICC Activation Status LED");
        cb2.setText("Enable PICC Polling Status LED");
        cb3.setText("Enable PICC Activation Status Buzzer");
        cb4.setText("Enable PICC PPS Status Buzzer");
        cb5.setText("Enable Card Insertion/Removal Events Buzzer");
        cb6.setText("Enable RC531 Reset Indication Buzzer");
        cb7.setText("Enable Exclusive Mode Status Buzzer");
        cb8.setText("Enable Card Operation Blinking LED");
        bGetBuzz.setText("Get Buzzer/LED state");
        bSetBuzz.setText("Set Buzzer/LED State");

        GroupLayout bottomPanelLayout = new GroupLayout(bottomPanel);
        bottomPanel.setLayout(bottomPanelLayout);
        bottomPanelLayout.setHorizontalGroup(
            bottomPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(bottomPanelLayout.createSequentialGroup()
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addGroup(bottomPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(cb1)
                    .addComponent(cb2)
                    .addComponent(cb3)
                    .addComponent(cb4)
                    .addComponent(cb5)
                    .addComponent(cb6)
                    .addComponent(cb7)
                    .addComponent(cb8)
                    .addGroup(bottomPanelLayout.createSequentialGroup()
                        .addComponent(bGetBuzz)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bSetBuzz))))
        );
        bottomPanelLayout.setVerticalGroup(
            bottomPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(bottomPanelLayout.createSequentialGroup()
                .addContainerGap(17, Short.MAX_VALUE)
                .addComponent(cb1)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(cb2)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(cb3)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(cb4)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(cb5)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(cb6)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(cb7)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(cb8)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(bottomPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(bGetBuzz)
                    .addComponent(bSetBuzz)))
        );

        bClear.setText("Clear");
        bReset.setText("Reset");
        bQuit.setText("Quit");
        Msg.setColumns(20);
        Msg.setRows(5);
        msgScrollPane.setViewportView(Msg);

        GroupLayout rightPanelLayout = new GroupLayout(rightPanel);
        rightPanel.setLayout(rightPanelLayout);
        rightPanelLayout.setHorizontalGroup(
            rightPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(rightPanelLayout.createSequentialGroup()
                .addGroup(rightPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(rightPanelLayout.createSequentialGroup()
                        .addGap(10, 10, 10)
                        .addComponent(bClear, GroupLayout.PREFERRED_SIZE, 110, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bReset, GroupLayout.PREFERRED_SIZE, 110, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bQuit, GroupLayout.PREFERRED_SIZE, 106, GroupLayout.PREFERRED_SIZE))
                    .addGroup(rightPanelLayout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(msgScrollPane, GroupLayout.PREFERRED_SIZE, 326, GroupLayout.PREFERRED_SIZE)))
                .addContainerGap(14, Short.MAX_VALUE))
        );
        rightPanelLayout.setVerticalGroup(
            rightPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(GroupLayout.Alignment.TRAILING, rightPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(msgScrollPane, GroupLayout.DEFAULT_SIZE, 534, Short.MAX_VALUE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(rightPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(bClear)
                    .addComponent(bReset)
                    .addComponent(bQuit)))
        );

        GroupLayout layout = new GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(buzzPanel, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(topPanel, GroupLayout.Alignment.TRAILING, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(midPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bottomPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rightPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                    .addComponent(rightPanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addGroup(GroupLayout.Alignment.LEADING, layout.createSequentialGroup()
                        .addComponent(topPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(midPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(buzzPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bottomPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        
        bInit.setMnemonic(KeyEvent.VK_I);
        bConn.setMnemonic(KeyEvent.VK_C);
        bGetFW.setMnemonic(KeyEvent.VK_F);
        bGetLed.setMnemonic(KeyEvent.VK_G);
        bSetLed.setMnemonic(KeyEvent.VK_S);
        bGetBuzz.setMnemonic(KeyEvent.VK_U);
        bSetBuzz.setMnemonic(KeyEvent.VK_B);
        bReset.setMnemonic(KeyEvent.VK_R);
        bQuit.setMnemonic(KeyEvent.VK_Q);
        bClear.setMnemonic(KeyEvent.VK_L);
        bSetBuzzDur.setMnemonic(KeyEvent.VK_D);
        bStartBuzz.setMnemonic(KeyEvent.VK_T);
        bStopBuzz.setMnemonic(KeyEvent.VK_P);
        
        bInit.addActionListener(this);
        bConn.addActionListener(this);
        bGetFW.addActionListener(this);
        bGetLed.addActionListener(this);
        bSetLed.addActionListener(this);
        bGetBuzz.addActionListener(this);
        bSetBuzz.addActionListener(this);
        bReset.addActionListener(this);
        bQuit.addActionListener(this);
        bClear.addActionListener(this);
        cbRed.addActionListener(this);
        cbGreen.addActionListener(this);
        bSetBuzzDur.addActionListener(this);
        bStartBuzz.addActionListener(this);
        bStopBuzz.addActionListener(this);
        
        
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
      		
			int offset = 0;
			rdrName.removeAllItems();
			
			for (int i = 0; i < cchReaders[0]-1; i++)
			{
				
			  	if (szReaders[i] == 0x00)
			  	{			  		
			  		
			  		rdrName.addItem(new String(szReaders, offset, i - offset));
			  		offset = i+1;
			  		
			  	}
			}
			
			if (rdrName.getItemCount() == 0)
				rdrName.addItem("No PC/SC reader detected");
			    
			rdrName.setSelectedIndex(0);
			bConn.setEnabled(true);
			bInit.setEnabled(false);
			bClear.setEnabled(true);
			cbGreen.setEnabled(true);
			cbRed.setEnabled(true);
			bReset.setEnabled(true);
			
			//Look for ACR128 SAM and make it the default reader in the combobox
			for (int i = 0; i < cchReaders[0]; i++)
			{
				
				rdrName.setSelectedIndex(i);

				if (((String) rdrName.getSelectedItem()).lastIndexOf("ACR128U SAM")> -1)
					break;
				else
					rdrName.setSelectedIndex(0);
				
			}
			
		}
		
		if(bConn == e.getSource())
		{
			
			if(connActive)
			{
				
				retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
				
			}
			
			String rdrcon = (String)rdrName.getSelectedItem();  	      	      	
		    
		    retCode = jacs.jSCardConnect(hContext, 
		    							rdrcon, 
		    							ACSModule.SCARD_SHARE_SHARED,
		    							ACSModule.SCARD_PROTOCOL_T1 | ACSModule.SCARD_PROTOCOL_T0,
		      							hCard, 
		      							PrefProtocols);
		    
		    if (retCode != ACSModule.SCARD_S_SUCCESS)
		    {
		      	//check if ACR128 SAM is used and use Direct Mode if SAM is not detected
		      	if (((String) rdrName.getSelectedItem()).lastIndexOf("ACR128U SAM")> -1)
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
		    			
		    			displayOut(0, 0, "Successful connection to " + (String)rdrName.getSelectedItem());
		    			
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
		      	
		    	displayOut(0, 0, "Successful connection to " + (String)rdrName.getSelectedItem());
		      	
		    }
			
		    connActive=true;
		    bGetFW.setEnabled(true);
		    bGetLed.setEnabled(true);
		    bSetLed.setEnabled(true);
		    cb1.setEnabled(true);
		    cb2.setEnabled(true);
		    cb3.setEnabled(true);
		    cb4.setEnabled(true);
		    cb5.setEnabled(true);
		    cb6.setEnabled(true);
		    cb7.setEnabled(true);
		    cb8.setEnabled(true);
		    bGetBuzz.setEnabled(true);
		    bSetBuzz.setEnabled(true);
		    bSetBuzzDur.setEnabled(true);
		    txtVal.setEnabled(true);
		    bStartBuzz.setEnabled(true);
		    bStopBuzz.setEnabled(true);
		    		    
		    getLEDState();
		    getBuzzState();
		}
		
		if(bQuit == e.getSource())
		{
			
			this.dispose();
			
		}
		
		if(bGetFW == e.getSource())
		{
			
			String tmpStr="";
			
			clearBuffers();
			SendBuff[0] = (byte) 0x18;
			SendBuff[1] = (byte) 0x00;
			SendLen = 2;
			RecvLen[0] = 35;
			retCode = callCardControl();
			
			if (retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			//Interpret firmware data
			tmpStr = "Firmware Version: ";

			for(int i=5; i< 25; i++)
			{
				
				if (RecvBuff[i] != 0x00)
					tmpStr = tmpStr + (char)RecvBuff[i];
				
			}
			
			displayOut(3, 0, tmpStr);
		}
		
		if(bGetLed == e.getSource())
		{
			
			getLEDState();
			
		}
		
		if(bSetLed == e.getSource())
		{
			
			clearBuffers();
			SendBuff[0] = (byte) 0x29;
			SendBuff[1] = (byte) 0x01;
			
			if (cbRed.isSelected())
			{
				SendBuff[2]= (byte)(SendBuff[2] | 0x01);
				
			}
			if(cbGreen.isSelected())
			{
				
				SendBuff[2]= (byte)(SendBuff[2] | 0x02);
				
			}
			SendLen = 3;
			RecvLen[0] =6;
			retCode = callCardControl();
			
			if (retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
		}
		
		if (bSetBuzzDur == e.getSource())
		{
			
			//checks if input is character or integer
			char [] letters;
			letters = txtVal.getText().toCharArray();

			for(int x = 0; x < letters.length; x++)
				if(Character.isDigit(letters[x]) == false)
					txtVal.setText("1");
				
			//checks if user placed value in textbox
			if (txtVal.getText().isEmpty())
			{
				txtVal.setText("1");
				return;
			}
			
			if(txtVal.getText().length() > 3)
			{
				txtVal.selectAll();
				txtVal.requestFocus();
				return;
			}
			
			if (Integer.parseInt(txtVal.getText()) >255  )
			{
				
				txtVal.setText("255");
				return;
				
			}
			
			clearBuffers();
			SendBuff[0] = (byte) 0x28;
			SendBuff[1] = (byte) 0x01;
			SendBuff[2] = (byte) Integer.parseInt(txtVal.getText());
			SendLen = 3;
			RecvLen[0] = 6;
			retCode = callCardControl();

			if (retCode != ACSModule.SCARD_S_SUCCESS)
				return;
		
		}
		
		if (bGetBuzz == e.getSource())
		{
			
			getBuzzState();
			
		}
		
		if(bSetBuzz == e.getSource())
		{
		
			clearBuffers();
			SendBuff[0] = (byte) 0x21;
			SendBuff[1] = (byte) 0x01;
			SendBuff[2] = (byte) 0x00;
			
			if(cb1.isSelected())
				SendBuff[2] = (byte)(SendBuff[2] | 0x01);
			
			if(cb2.isSelected())
				SendBuff[2] = (byte)(SendBuff[2] | 0x02);
			
			if(cb3.isSelected())
				SendBuff[2] = (byte)(SendBuff[2] | 0x04);
			
			if(cb4.isSelected())
				SendBuff[2] = (byte)(SendBuff[2] | 0x08);
			
			if(cb5.isSelected())
				SendBuff[2] = (byte)(SendBuff[2] | 0x10);
			
			if(cb6.isSelected())
				SendBuff[2] = (byte)(SendBuff[2] | 0x20);
			
			if(cb7.isSelected())
				SendBuff[2] = (byte)(SendBuff[2] | 0x40);
			
			if(cb8.isSelected())
				SendBuff[2] = (byte)(SendBuff[2] | 0x80);
			
			SendLen = 3;
			RecvLen[0] = 6;
			retCode = callCardControl();
			
			if (retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
		}
		
		if(bClear == e.getSource())
		{
			
			Msg.setText("");
			
		}
		
		if(bReset==e.getSource())
		{
			
			//disconnect
			if (connActive){
				
				retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
				connActive= false;
			
			}
		    
			//release context
			retCode = jacs.jSCardReleaseContext(hContext);
			//System.exit(0);
			
			txtVal.setText("");
			Msg.setText("");
			initMenu();
			rdrName.removeAllItems();
			rdrName.addItem("Please select reader                   ");
			
		}
		
		if(bStartBuzz == e.getSource())
		{
			
			clearBuffers();
			SendBuff[0] = (byte)0x28;
			SendBuff[1] = (byte)0x01;
			SendBuff[2] = (byte)0xFF;
			SendLen = 3;
			RecvLen[0] = 6;
			retCode = callCardControl();
			
			if (retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
		}
		
		if(bStopBuzz== e.getSource())
		{
		
			clearBuffers();
			SendBuff[0] = (byte)0x28;
			SendBuff[1] = (byte)0x01;
			SendBuff[2] = (byte)0x00;
			SendLen = 3;
			RecvLen[0] = 6;
			retCode = callCardControl();
			
			if (retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
		}
		
	}
	
	public int callCardControl()
	{
		
		String tmpStr, tmpHex="";
		tmpStr = "SCardControl: ";
		
		for(int i=0; i<SendLen; i++)
		{
			tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
			
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
			
			tmpStr += " " + tmpHex;  
			
		}
		
		displayOut(2, 0, tmpStr);
		
		retCode = jacs.jSCardControl(hCard,
									ACSModule.IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND,
									SendBuff,
									SendLen,
									RecvBuff,
									RecvLen,
									nBytesRet);

		
		if (retCode != ACSModule.SCARD_S_SUCCESS)
		{
			//JOptionPane.showMessageDialog(this, retCode);
			displayOut(1, retCode, "");
			
		}
		else
		{
			
			tmpStr = "";
			
			for(int i =0; i<RecvLen[0]; i++)
			{
				
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
				
			}
			
			displayOut(3, 0, tmpStr.trim());
		}
		
		return retCode;
		
	}
	
	public int getBuzzState()
	{
		
		clearBuffers();
		SendBuff[0]= (byte) 0x21;
		SendBuff[1]= (byte) 0x00;
		SendLen = 2;
		RecvLen[0] = 6;
		retCode = callCardControl();
		
		if(retCode != ACSModule.SCARD_S_SUCCESS)
			return retCode;
		
		//Interpret buzzer State Data
		if((RecvBuff[5] & 0x01)!=0)
		{
			
			displayOut(3, 0, "ICC Activation Status LED is enabled.");
			cb1.setSelected(true);
			
		}
		else
		{
			
			displayOut(3, 0, "ICC Activation Status LED is disabled.");
	        cb1.setSelected(false);
			
		}
		
		if((RecvBuff[5] & 0x02)!=0)
		{
			
			displayOut(3, 0, "PICC Polling Status LED is enabled.");
			cb2.setSelected(true);
			
		}
		else
		{
			
			displayOut(3, 0, "PICC Polling Status LED is disabled.");
	        cb2.setSelected(false);
			
		}
		
		if((RecvBuff[5] & 0x04)!=0)
		{
			
			displayOut(3, 0, "PICC Activation Status Buzzer is enabled.");
			cb3.setSelected(true);
			
		}
		else
		{
			
			displayOut(3, 0, "PICC Activation Status Buzzer is disabled.");
	        cb3.setSelected(false);
			
		}
		
		if((RecvBuff[5] & 0x08)!=0)
		{
			
			displayOut(3, 0, "PICC PPS Status Buzzer is enabled.");
			cb4.setSelected(true);
			
		}
		else
		{
			
			displayOut(3, 0, "PICC PPS Status Buzzer is disabled.");
	        cb4.setSelected(false);
			
		}
		
		if((RecvBuff[5] & 0x10)!=0)
		{
			
			displayOut(3, 0, "Card Insertion and Removal Events Buzzer is enabled.");
			cb5.setSelected(true);
			
		}
		else
		{
			
			displayOut(3, 0, "Card Insertion and Removal Events Buzzer is disabled.");
	        cb5.setSelected(false);
			
		}
		
		if((RecvBuff[5] & 0x20)!=0)
		{
			
			displayOut(3, 0, "RC531 Reset Indication Buzzer is enabled.");
			cb6.setSelected(true);
			
		}
		else
		{
			
			displayOut(3, 0, "RC531 Reset Indication Buzzer is disabled.");
	        cb6.setSelected(false);
			
		}
		
		if((RecvBuff[5] & 0x40)!=0)
		{
			
			displayOut(3, 0, "Exclusive Mode Status Buzzer is enabled.");
			cb7.setSelected(true);
			
		}
		else
		{
			
			displayOut(3, 0, "Exclusive Mode Status Buzzer is disabled.");
	        cb7.setSelected(false);
			
		}
		
		if((RecvBuff[5] & 0x80)!=0)
		{
			
			displayOut(3, 0, "Card Operation Blinking LED is enabled.");
			cb8.setSelected(true);
			
		}
		else
		{
			
			displayOut(3, 0, "Card Operation Blinking LED is disabled.");
	        cb8.setSelected(false);
			
		}
		
		return retCode;
		
	}
	
	public int getLEDState()
	{
		
		clearBuffers();
		SendBuff[0]= (byte) 0x29;
		SendBuff[1]= (byte) 0x00;
		SendLen=2;
		RecvLen[0]=6;
		retCode = callCardControl();
		
		if(retCode != ACSModule.SCARD_S_SUCCESS)
			return retCode;
		
		//interpret LED data
		switch(RecvBuff[5])
		{
		
		case 0: 
			displayOut(3, 0, "Currently connected to SAM reader interface.");
			cbRed.setSelected(true);
			break;
		
		case 1:
			displayOut(3, 0, "No PICC found.");
			cbRed.setSelected(true);
			break;
			
		case 2:
			displayOut(3, 0, "PICC is present but not activated.");
			cbRed.setSelected(true);
			break;
			
		case 3:
			displayOut(3, 0, "PICC is present and activated.");
			cbRed.setSelected(true);
			break;
			
		case 4:
			displayOut(3, 0, "PICC is present and activated.");
			cbRed.setSelected(true);
			break;
			
		case 5:
			displayOut(3, 0, "ICC is present and activated.");
			cbGreen.setSelected(true);
			break;
			
		case 6:
			displayOut(3, 0, "ICC is absent or not activated.");
			cbGreen.setSelected(true);
			break;
			
		case 7:
			displayOut(3, 0, "ICC is operating.");
			cbGreen.setSelected(true);
			break;
			
		}
		
		if ((RecvBuff[5] & 0x02)!= 0)
			cbGreen.setSelected(true);
		else
			cbGreen.setSelected(false);
		
		if ((RecvBuff[5] & 0x01)!= 0)
			cbRed.setSelected(true);
		else
			cbRed.setSelected(false);
		
		return retCode;
		
	}
	
	public void clearBuffers()
	{
		
		for(int i=0; i<262; i++)
		{
			
			SendBuff[i]=(byte) 0x00;
			RecvBuff[i]= (byte) 0x00;
			
		}
		
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
	

	
	public void initMenu()
	{
	
		connActive = false;
		bConn.setEnabled(false);
		bInit.setEnabled(true);
		bGetFW.setEnabled(false);
		bGetLed.setEnabled(false);
		bSetLed.setEnabled(false);
		bGetBuzz.setEnabled(false);
		bSetBuzz.setEnabled(false);
		bSetBuzzDur.setEnabled(false);
		txtVal.setEnabled(false);
		cbGreen.setEnabled(false);
		cbRed.setEnabled(false);
		cb1.setEnabled(false);
		cb2.setEnabled(false);
		cb3.setEnabled(false);
		cb4.setEnabled(false);
		cb5.setEnabled(false);
		cb6.setEnabled(false);
		cb7.setEnabled(false);
		cb8.setEnabled(false);
		bStartBuzz.setEnabled(false);
		bStopBuzz.setEnabled(false);
		bReset.setEnabled(false);
		displayOut(0, 0, "Program Ready");
		
	}
    
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new deviceProgramming().setVisible(true);
            }
        });
    }



}
