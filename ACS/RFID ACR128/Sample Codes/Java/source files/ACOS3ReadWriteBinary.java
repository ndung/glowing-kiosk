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


public class ACOS3ReadWriteBinary extends JFrame implements ActionListener, KeyListener{

	//JPCSC Variables
	int retCode, maxLen;
	boolean connActive; 
	public static final int INVALID_SW1SW2 = -450;
	static String VALIDCHARS = "ABCDEFabcdef0123456789";
	
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
	int reqType;

	
	//GUI Variables
    private JButton bClear;
    private JButton bConnect;
    private JButton bFormat;
    private JButton bInit;
    private JButton bRead;
    private JButton bReset;
    private JButton bQuit;
    private JButton bWrite;
    private JPanel binaryPanel;
    private JComboBox cbReader;
    private JPanel crdFormatPanel;
    private JLabel lblData;
    private JLabel lblFileID;
    private JLabel lblID;
    private JLabel lblLen;
    private JLabel lblLength;
    private JLabel lblOffset;
    private JLabel lblReader;
    private JTextArea mData;
    private JTextArea mMsg;
    private JPanel msgPanel;
    private JPanel rdrPanel;
    private JScrollPane scrPaneData;
    private JScrollPane scrPaneMsg;
    private JTextField tFID1;
    private JTextField tFID2;
    private JTextField tFileID1;
    private JTextField tFileID2;
    private JTextField tFileLen1;
    private JTextField tFileLen2;
    private JTextField tLen;
    private JTextField tOffset1;
    private JTextField tOffset2;
	
	static JacspcscLoader jacs = new JacspcscLoader();
    

    public ACOS3ReadWriteBinary() {
    	
    	this.setTitle("ACOS 3 Read Write Binary");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

		//GUI Variables
		setSize(670,515);
        rdrPanel = new JPanel();
        lblReader = new JLabel();
        bInit = new JButton();
        bConnect = new JButton();
        crdFormatPanel = new JPanel();
        lblFileID = new JLabel();
        lblLength = new JLabel();
        tFileID1 = new JTextField();
        tFileID2 = new JTextField();
        tFileLen1 = new JTextField();
        tFileLen2 = new JTextField();
        bFormat = new JButton();
        binaryPanel = new JPanel();
        lblID = new JLabel();
        lblOffset = new JLabel();
        tFID1 = new JTextField();
        tFID2 = new JTextField();
        tOffset1 = new JTextField();
        tOffset2 = new JTextField();
        lblLen = new JLabel();
        tLen = new JTextField();
        lblData = new JLabel();
        scrPaneData = new JScrollPane();
        mData = new JTextArea();
        bRead = new JButton();
        bWrite = new JButton();
        msgPanel = new JPanel();
        scrPaneMsg = new JScrollPane();
        mMsg = new JTextArea();
        bClear = new JButton();
        bReset = new JButton();
        bQuit = new JButton();
        
        lblReader.setText("Select Reader");

		String[] rdrNameDef = {"Please select reader             "};	
		cbReader = new JComboBox(rdrNameDef);
		cbReader.setSelectedIndex(0);
		
        bInit.setText("Initalize");

        bConnect.setText("Connect");

        GroupLayout rdrPanelLayout = new GroupLayout(rdrPanel);
        rdrPanel.setLayout(rdrPanelLayout);
        rdrPanelLayout.setHorizontalGroup(
            rdrPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(rdrPanelLayout.createSequentialGroup()
                .addGroup(rdrPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(rdrPanelLayout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(lblReader)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(cbReader, 0, 198, Short.MAX_VALUE))
                    .addGroup(GroupLayout.Alignment.TRAILING, rdrPanelLayout.createSequentialGroup()
                        .addGap(154, 154, 154)
                        .addComponent(bConnect, GroupLayout.DEFAULT_SIZE, 125, Short.MAX_VALUE))
                    .addGroup(GroupLayout.Alignment.TRAILING, rdrPanelLayout.createSequentialGroup()
                        .addGap(154, 154, 154)
                        .addComponent(bInit, GroupLayout.DEFAULT_SIZE, 125, Short.MAX_VALUE)))
                .addContainerGap())
        );
        rdrPanelLayout.setVerticalGroup(
            rdrPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(rdrPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(rdrPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblReader)
                    .addComponent(cbReader, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bInit)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bConnect)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        crdFormatPanel.setBorder(BorderFactory.createTitledBorder("Card Format Routine"));

        lblFileID.setText("File ID");

        lblLength.setText("Length");

        bFormat.setText("Format Card");

        GroupLayout crdFormatPanelLayout = new GroupLayout(crdFormatPanel);
        crdFormatPanel.setLayout(crdFormatPanelLayout);
        crdFormatPanelLayout.setHorizontalGroup(
            crdFormatPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(crdFormatPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(crdFormatPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                    .addComponent(lblLength)
                    .addComponent(lblFileID))
                .addGap(18, 18, 18)
                .addGroup(crdFormatPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(tFileLen1, 0, 0, Short.MAX_VALUE)
                    .addComponent(tFileID1, GroupLayout.DEFAULT_SIZE, 34, Short.MAX_VALUE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(crdFormatPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(tFileLen2)
                    .addComponent(tFileID2, GroupLayout.DEFAULT_SIZE, 34, Short.MAX_VALUE))
                .addGap(18, 18, 18)
                .addComponent(bFormat, GroupLayout.PREFERRED_SIZE, 114, GroupLayout.PREFERRED_SIZE)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        crdFormatPanelLayout.setVerticalGroup(
            crdFormatPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(crdFormatPanelLayout.createSequentialGroup()
                .addGroup(crdFormatPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(crdFormatPanelLayout.createSequentialGroup()
                        .addGroup(crdFormatPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(lblFileID)
                            .addComponent(tFileID1, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                            .addComponent(tFileID2, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(crdFormatPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                            .addComponent(lblLength)
                            .addGroup(crdFormatPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                                .addComponent(tFileLen1, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                                .addComponent(tFileLen2, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))))
                    .addGroup(crdFormatPanelLayout.createSequentialGroup()
                        .addGap(11, 11, 11)
                        .addComponent(bFormat)))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        binaryPanel.setBorder(BorderFactory.createTitledBorder("Read and Write to Binary File"));

        lblID.setText("File ID");

        lblOffset.setText("File Offset");

        lblLen.setText("Length");

        lblData.setText("Data");

        scrPaneData.setVerticalScrollBarPolicy(ScrollPaneConstants.VERTICAL_SCROLLBAR_NEVER);

        mData.setColumns(20);
        mData.setRows(5);
        scrPaneData.setViewportView(mData);

        bRead.setText("Read Binary");

        bWrite.setText("Write Binary");

        GroupLayout binaryPanelLayout = new GroupLayout(binaryPanel);
        binaryPanel.setLayout(binaryPanelLayout);
        binaryPanelLayout.setHorizontalGroup(
            binaryPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(binaryPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(binaryPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(binaryPanelLayout.createSequentialGroup()
                        .addGroup(binaryPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                            .addComponent(lblID)
                            .addComponent(lblOffset))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(binaryPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                            .addGroup(binaryPanelLayout.createSequentialGroup()
                                .addComponent(tFID1, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(tFID2, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE))
                            .addGroup(binaryPanelLayout.createSequentialGroup()
                                .addComponent(tOffset1, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(tOffset2, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                                .addGap(26, 26, 26)
                                .addComponent(lblLen)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(tLen, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE))))
                    .addComponent(lblData)
                    .addComponent(scrPaneData, GroupLayout.PREFERRED_SIZE, 259, GroupLayout.PREFERRED_SIZE))
                .addContainerGap(8, Short.MAX_VALUE))
            .addGroup(GroupLayout.Alignment.TRAILING, binaryPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(binaryPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(bRead, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bWrite, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, 108, Short.MAX_VALUE))
                .addContainerGap())
        );
        binaryPanelLayout.setVerticalGroup(
            binaryPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(binaryPanelLayout.createSequentialGroup()
                .addGroup(binaryPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblID)
                    .addComponent(tFID1, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(tFID2, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(binaryPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblOffset)
                    .addComponent(tOffset1, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(tOffset2, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(lblLen)
                    .addComponent(tLen, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(lblData)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(scrPaneData, GroupLayout.PREFERRED_SIZE, 57, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bRead)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bWrite)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        mMsg.setColumns(20);
        mMsg.setRows(5);
        scrPaneMsg.setViewportView(mMsg);

        bClear.setText("Clear");

        bReset.setText("Reset");
        
        bQuit.setText("Quit");

        javax.swing.GroupLayout msgPanelLayout = new javax.swing.GroupLayout(msgPanel);
        msgPanel.setLayout(msgPanelLayout);
        msgPanelLayout.setHorizontalGroup(
            msgPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(msgPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(msgPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(msgPanelLayout.createSequentialGroup()
                        .addComponent(bClear, javax.swing.GroupLayout.PREFERRED_SIZE, 101, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bReset, javax.swing.GroupLayout.PREFERRED_SIZE, 95, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(bQuit, javax.swing.GroupLayout.PREFERRED_SIZE, 95, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addComponent(scrPaneMsg, javax.swing.GroupLayout.PREFERRED_SIZE, 309, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addContainerGap(19, Short.MAX_VALUE))
        );
        msgPanelLayout.setVerticalGroup(
            msgPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(msgPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(scrPaneMsg, javax.swing.GroupLayout.PREFERRED_SIZE, 393, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(msgPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(bClear)
                    .addComponent(bReset)
                    .addComponent(bQuit))
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        GroupLayout layout = new GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                    .addComponent(rdrPanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(binaryPanel, GroupLayout.Alignment.LEADING, 0, 289, Short.MAX_VALUE)
                    .addComponent(crdFormatPanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(msgPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(msgPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(rdrPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(crdFormatPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addGap(18, 18, 18)
                        .addComponent(binaryPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
		
        mMsg.setLineWrap(true);
        mData.setLineWrap(true);
        
        bInit.setMnemonic(KeyEvent.VK_I);
        bConnect.setMnemonic(KeyEvent.VK_C);
        bReset.setMnemonic(KeyEvent.VK_R);
        bClear.setMnemonic(KeyEvent.VK_L);
        bFormat.setMnemonic(KeyEvent.VK_F);
        bRead.setMnemonic(KeyEvent.VK_R);
        bWrite.setMnemonic(KeyEvent.VK_W);
        bQuit.setMnemonic(KeyEvent.VK_Q);

        bInit.addActionListener(this);
        bConnect.addActionListener(this);
        bReset.addActionListener(this);
        bClear.addActionListener(this);
        bFormat.addActionListener(this);
        bRead.addActionListener(this);
        bWrite.addActionListener(this);
        bQuit.addActionListener(this);
        
        tFileID1.addKeyListener(this);
        tFileID2.addKeyListener(this);
        tFileLen1.addKeyListener(this);
        tFileLen2.addKeyListener(this);
        tFID1.addKeyListener(this);
        tFID2.addKeyListener(this);
        tOffset1.addKeyListener(this);
        tOffset2.addKeyListener(this);
        tLen.addKeyListener(this);
        
        
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
			
		    connActive=true;
			tFileID1.setEnabled(true);
			tFileID2.setEnabled(true);
			tFileLen1.setEnabled(true);
			tFileLen2.setEnabled(true);
			bFormat.setEnabled(true);
			tFID1.setEnabled(true);
			tFID2.setEnabled(true);
			tOffset1.setEnabled(true);
			tOffset2.setEnabled(true);
			tLen.setEnabled(true);
			mData.setEnabled(true);
			bRead.setEnabled(true);
			bWrite.setEnabled(true);
			getBinaryData();
			
		}
		
		if(bClear == e.getSource())
		{
			
			mMsg.setText("");
			
		}
		
		if(bQuit == e.getSource())
		{
			
			this.dispose();
			
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
			
			mMsg.setText("");
			initMenu();
			cbReader.removeAllItems();
			cbReader.addItem("Please select reader                   ");
			
		}
		
		if(bFormat == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			byte[] tmpArray = new byte[31];
			
			//validate input
			if(tFileID1.getText().equals(""))
			{

				tFileID1.requestFocus();
				return;
				
			}
	
			if(tFileID2.getText().equals(""))
			{
				
				tFileID2.requestFocus();
				return;
				
			}
	
			if(tFileLen2.getText().equals(""))
			{
			
				tFileLen2.requestFocus();
				return;
				
			}
		
			//send IC code
			retCode = submitIC();
			
			if (retCode != ACSModule.SCARD_S_SUCCESS)
			{
				displayOut(4, 0, "Insert ACOS3-24K card on contact card reader.");
				return;
			}
			
			//select FF 02
			retCode = selectFile((byte)0xFF, (byte)0x02);
			
			if (retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i =0; i<2; i++){
				  tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex;  
			  }
			
			if(!tmpStr.trim().equals("90 00"))
			{
				displayOut(2, 0, "The return string is invalid. Value: " + tmpStr);
				retCode = INVALID_SW1SW2;
				return;
			}
			
			//3. Write to FF 02
		    //   This will create 1 binary file, no Option registers and
		    //   Security Option registers defined, Personalization bit
			tmpArray[0] = (byte)0x00;
			tmpArray[1] = (byte)0x00;
			tmpArray[2] = (byte)0x01;
			tmpArray[3] = (byte)0x00;
			
			retCode = writeRecord(0, (byte)0x00, (byte)0x04, (byte)0x04, tmpArray);
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			displayOut(0, 0, "File FF 02 is updated.");
			
			//4. perform a reset for changes in ACOS3 to take effect
			connActive = false;
			retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
			{
				displayOut(0, retCode, "");
				return;
			}
			
			String rdrcon = (String)cbReader.getSelectedItem();  	      	      	
		    
		    retCode = jacs.jSCardConnect(hContext, 
		    							rdrcon, 
		    							ACSModule.SCARD_SHARE_SHARED,
		    							ACSModule.SCARD_PROTOCOL_T1 | ACSModule.SCARD_PROTOCOL_T0,
		      							hCard, 
		      							PrefProtocols);
			
		    if(retCode != ACSModule.SCARD_S_SUCCESS)
			{
				displayOut(0, retCode, "");
				return;
			}
		    
		    displayOut(3, 0, "Card reset is successful.");
		    connActive = true;
		    
		    //5. select FF 04
		    retCode = selectFile((byte)0xFF, (byte)0x04);
		    if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
		    
			tmpStr="";
			
		    for(int i =0; i<2; i++){
				  tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex;  
			  }
			
			if(!tmpStr.trim().equals("90 00"))
			{
				displayOut(2, 0, "The return string is invalid. Value: " + tmpStr);
				retCode = INVALID_SW1SW2;
				return;
			}
			
			//6. send IC code
			retCode = submitIC();
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
		    //write to FF 04
		    //7.1. Write to first record of FF 04
			if(tFileLen1.getText().equals(""))
				tmpArray[0] = (byte)0x00;
			else
				tmpArray[0] = (byte)((Integer)Integer.parseInt(tFileLen1.getText(), 16)).byteValue();
			
			tmpArray[1] = (byte)((Integer)Integer.parseInt(tFileLen2.getText(), 16)).byteValue();
			tmpArray[2] = (byte) 0x00;
			tmpArray[3] = (byte) 0x00;
			tmpArray[4] = (byte)((Integer)Integer.parseInt(tFileID1.getText(), 16)).byteValue();
			tmpArray[5] = (byte)((Integer)Integer.parseInt(tFileID2.getText(), 16)).byteValue();
			tmpArray[6] = (byte)0x80;
			
			retCode = writeRecord(0, (byte)0x00, (byte)0x07, (byte)0x07, tmpArray);
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			tmpStr = "";
			tmpStr = tFileID1.getText() + " " + tFileID2.getText();
			displayOut(0, 0, "User File " + tmpStr + " is defined.");
		    
		}
		
		if(bRead == e.getSource())
		{
			
			byte fileID1, fileID2, hiByte, loByte;
			int tmpLen;
			String tmpStr="", tmpHex="";
			
			//validate input
			if(tFID2.getText().equals(""))
			{
				
				tFID2.requestFocus();
				return;
				
			}
			
			if(tOffset2.getText().equals(""))
			{
				tOffset2.selectAll();
				tOffset2.requestFocus();
				return;
			}
			
			if(tLen.getText().equals(""))
			{
				
				tLen.requestFocus();
				return;
				
			}
			
			clearBuffers();
			fileID1 = (byte)((Integer)Integer.parseInt(tFID1.getText(), 16)).byteValue();
			fileID2 = (byte)((Integer)Integer.parseInt(tFID2.getText(), 16)).byteValue();
			
			if(tOffset1.getText().equals(""))
				hiByte = (byte) 0x00;
			else
				hiByte = ((Integer)Integer.parseInt(tOffset1.getText(), 16)).byteValue();
			
			loByte = (byte)((Integer)Integer.parseInt(tOffset2.getText(), 16)).byteValue();
			tmpLen = ((Integer)Integer.parseInt(tLen.getText(), 16)).byteValue();
			
			//select user file
			retCode = selectFile(fileID1, fileID2);
			
			if (retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i=0; i<2; i++)
			{
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
				
			}
			
			if(!tmpStr.trim().equals("91 00"))
			{
	
				displayOut(2, 0, "The return string is invalid. Value: " + tmpStr);
				retCode = INVALID_SW1SW2;
				return;
				
			}
			
			//read binary
			retCode = readBinary(hiByte, loByte, (byte)tmpLen);
			
			if (retCode!=ACSModule.SCARD_S_SUCCESS)
			{
				displayOut(4, 0, "Card may not have been formatted yet.");
				return;
			}
			
			tmpStr="";
			int i=0;
			
			while((RecvBuff[i] & 0xFF) != 0x00)
			{
				
				if(i < maxLen)
					tmpStr = tmpStr + (char)(RecvBuff[i] & 0xFF);
				
				i++;
				
			}
			
			mData.setText(tmpStr);
			
		}
		
		if(bWrite == e.getSource())
		{
			
			int tmpLen;
			String tmpStr="", tmpHex="";
			byte hiByte, loByte, fileID1, fileID2;
			byte[] tmpArray = new byte[255];
			
			//validate input
			if(tFID1.getText().equals(""))
			{
			
				tFID1.requestFocus();
				return;
				
			}
			
			if(tFID2.getText().equals(""))
			{
				tFID2.selectAll();
				tFID2.requestFocus();
				return;
			}
		
			if(tOffset2.getText().equals(""))
			{
				
				tOffset2.requestFocus();
				return;
				
			}
			
			if(tLen.getText().equals(""))
			{
			
				tLen.requestFocus();
				return;
				
			}
			
			if(mData.getText().equals(""))
			{
				
				mData.requestFocus();
				return;
				
			}
			
			clearBuffers();
			fileID1 = (byte)((Integer)Integer.parseInt(tFID1.getText(), 16)).byteValue();
			fileID2 = (byte)((Integer)Integer.parseInt(tFID2.getText(), 16)).byteValue();
			
			if(tOffset1.getText().equals(""))
				hiByte = (byte)0x00;
			else
				hiByte = (byte)((Integer)Integer.parseInt(tOffset1.getText(), 16)).byteValue();
			
			loByte = (byte)((Integer)Integer.parseInt(tOffset2.getText(), 16)).byteValue();
			tmpLen = ((Integer)Integer.parseInt(tLen.getText(), 16)).byteValue();
			
			//select user file
			retCode = selectFile(fileID1, fileID2);
			if(retCode!=ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i=0; i<2; i++)
			 {
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
					
				tmpStr += " " + tmpHex;  
					
			 }
			
			if(!tmpStr.trim().equals("91 00"))
			{
				displayOut(2, 0, "The return string is invalid. Value: " + tmpStr);
				retCode = INVALID_SW1SW2;
				return;
			}
			
			//write input data to card
			tmpStr= mData.getText();
			int tmpInt;
			
			for(int i = 0 ; i<tmpStr.length(); i++)
			{
				tmpInt = (int)tmpStr.charAt(i);
				tmpArray[i] = (byte) tmpInt;
			}
			
			retCode = writeBinary(1, (byte) hiByte, (byte)loByte, (byte)tmpLen, tmpArray);
			if(retCode!=ACSModule.SCARD_S_SUCCESS)
				return;
			
		}
				
	}
    
	public void getBinaryData()
	{
		String tmpStr="", tmpHex="";
		int tmpLen;
		
		//1. send IC code
		retCode = submitIC();
		if (retCode != ACSModule.SCARD_S_SUCCESS)
		{
			displayOut(4, 0, "Insert ACOS3-24K card on contact card reader.");
			return;
		}
		
		//select FF 04
		retCode = selectFile((byte)0xFF, (byte)0x04);
		if (retCode != ACSModule.SCARD_S_SUCCESS)
			return;
		
		for(int i=0; i<2; i++)
		 {
			tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
				
			tmpStr += " " + tmpHex;  
				
		 }
		
		if(!tmpStr.trim().equals("90 00"))
		{
			displayOut(2, 0, "The return string is invalid. Value: " + tmpStr);
			retCode = INVALID_SW1SW2;
			return;
		}
		
		//read first record
		readRecord((byte)0x00, (byte)0x07);
		if (retCode != ACSModule.SCARD_S_SUCCESS)
		{
			displayOut(4, 0, "Card may not have been formatted yet.");
			return;
		}
		
		//provide paramenter for data input box
		tFID1.setText(Integer.toHexString(((Byte)RecvBuff[4]).intValue() & 0xFF).toUpperCase());
		tFID2.setText(Integer.toHexString(((Byte)RecvBuff[5]).intValue() & 0xFF).toUpperCase());
		tmpLen = RecvBuff[1] & 0xFF;
		tmpLen = tmpLen + ((RecvBuff[0] & 0xFF) * 256);
		maxLen = tmpLen;

		
	}
	
	public int readRecord(byte recNo, byte dataLen)
	{
		  String tmpStr = "", tmpHex="";
		  
		  clearBuffers();
		  SendBuff[0] = (byte)0x80;        // CLA
		  SendBuff[1] = (byte)0xB2;        // INS
		  SendBuff[2] = recNo;             // P1    Record No
		  SendBuff[3] = (byte)0x00;        // P2
		  SendBuff[4] = dataLen;           // P3    Length of data to be read
		  SendLen = 5;
		  RecvLen[0] = (SendBuff[4] & 0xFF) + 2;
		  
		  retCode = sendAPDUandDisplay(0);
		  if (retCode != ACSModule.SCARD_S_SUCCESS)
		      return retCode;
		  
		  for(int i=0; i<2; i++){
			  tmpHex = Integer.toHexString(((Byte)RecvBuff[i + dataLen]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
			  
		  }

		 if (!tmpStr.trim().equals("90 00"))
		 {
		      displayOut(2, 0, "The return string is invalid. Value: " + tmpStr);
		      retCode = INVALID_SW1SW2;
		 }
		 
		 return retCode;
		
	}
	
	public int writeBinary(int caseType, byte hiByte, byte loByte, byte dataLen, byte[] dataIn)
	{
		String tmpStr="", tmpHex="";
		
		//If card data is to be erased before writing new data
		if(caseType == 1){
			//reinitialize card value to 0x00
			clearBuffers();
			SendBuff[0] = (byte)0x80;
			SendBuff[1] = (byte)0xD0;
			SendBuff[2] = hiByte;
			SendBuff[3] = loByte;
			SendBuff[4] = dataLen;
			
			for(int i=0; i< (dataLen & 0xFF); i++)
				SendBuff[i+5] = (byte)0x00;
			
			SendLen= (dataLen & 0xFF)+5;
			RecvLen[0] = 0x02;
			
			retCode = sendAPDUandDisplay(2);
			if(retCode!=ACSModule.SCARD_S_SUCCESS)
				return retCode;
			
			for(int i=0; i<2; i++){
				  tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex;  
				  
			  }
			
			if(!tmpStr.trim().equals("90 00"))
			{
				
				displayOut(3, 0, "The return string is invalid. Value: " + tmpStr);
				retCode = INVALID_SW1SW2;
				return retCode;
				
			}
			
		}
		
		//write data to card
		clearBuffers();
		SendBuff[0] = (byte) 0x80;
		SendBuff[1] = (byte) 0xD0;
		SendBuff[2] = hiByte;
		SendBuff[3] = loByte;
		SendBuff[4] = dataLen;
		
		for(int i=0 ; i< (dataLen & 0xFF); i++)
			SendBuff[i + 5] = dataIn[i];
		
		SendLen = (dataLen & 0xFF) + 5;
		RecvLen[0] = 0x02;
		
		retCode = sendAPDUandDisplay(0);
		if(retCode != ACSModule.SCARD_S_SUCCESS)
			return retCode;
		
		tmpStr="";
		for(int i=0; i<2; i++){
			  tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
			  
		  }
		
		if(!tmpStr.trim().equals("90 00"))
		{
			displayOut(3, 0, "The return string is invalid. Value: " + tmpStr);
			retCode = INVALID_SW1SW2;
			return retCode;
		}	
		
		return retCode;
		
	}
	
	public int readBinary(byte hiByte, byte loByte, byte dataLen)
	{
		
		clearBuffers();
		SendBuff[0] = (byte)0x80;
		SendBuff[1] = (byte)0xB0;
		SendBuff[2] = hiByte;
		SendBuff[3] = loByte;
		SendBuff[4] = dataLen;
		
		SendLen = 0x05;
		RecvLen[0] = (dataLen & 0xFF) +2;
		
		retCode = sendAPDUandDisplay(0);
		if (retCode != ACSModule.SCARD_S_SUCCESS)
			return retCode;
		
		return retCode;
		
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
		 retCode = sendAPDUandDisplay(0);
		 
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
		 retCode = sendAPDUandDisplay(0);
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
		
		retCode = sendAPDUandDisplay(2);
		if (retCode != ACSModule.SCARD_S_SUCCESS)
		{
			return retCode;
			
		}
		
		return retCode;
	}
	
	public int submitIC()
	{
		
		clearBuffers();
		SendBuff[0] = (byte) 0x80;
		SendBuff[1] = (byte) 0x20;
		SendBuff[2] = (byte) 0x07;
		SendBuff[3] = (byte) 0x00;
		SendBuff[4] = (byte) 0x08;
		SendBuff[5] = (byte) 0x41;
		SendBuff[6] = (byte) 0x43;
		SendBuff[7] = (byte) 0x4F;
		SendBuff[8] = (byte) 0x53;
		SendBuff[9] = (byte) 0x54;
		SendBuff[10] = (byte) 0x45;
		SendBuff[11] = (byte) 0x53;
		SendBuff[12] = (byte) 0x54;
		
		SendLen = 0x0D;
		RecvLen[0] = 0x02;
		
		retCode = sendAPDUandDisplay(0);
		
		if (retCode != ACSModule.SCARD_S_SUCCESS)
			return retCode;
		
		String tmpStr="", tmpHex="";
		
		for(int i =0; i<2; i++){
			  tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
		  }
		
		if(!tmpStr.trim().equals("90 00"))
		{
			
			displayOut(0, 0, "Return string is invalid. Value: " + tmpStr);
			retCode = INVALID_SW1SW2;
			return retCode;
			
		}
		
		return retCode;
		
	}

	
	public int sendAPDUandDisplay(int sendType)
	{
		
		ACSModule.SCARD_IO_REQUEST IO_REQ = new ACSModule.SCARD_IO_REQUEST(); 
		ACSModule.SCARD_IO_REQUEST IO_REQ_Recv = new ACSModule.SCARD_IO_REQUEST(); 
		IO_REQ.dwProtocol = PrefProtocols[0];
		IO_REQ.cbPciLength = 8;
		IO_REQ_Recv.dwProtocol = PrefProtocols[0];
		IO_REQ_Recv.cbPciLength = 8;
		
		String tmpStr = "", tmpHex="";
		
		for(int i =0; i<SendLen; i++)
		{
			
			tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
			//JOptionPane.showMessageDialog(this, SendBuff[4]);
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
			
			tmpStr += " " + tmpHex;  
			
		}
		
		displayOut(2, 0, tmpStr);
		
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
			
			//display SW1/SW2 value
			case 0: 
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
				
				break;
			}
			//Display ATR after checking SW1/SW2
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
			//Display all Data
			case 2:
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
			}
			
			displayOut(3, 0, tmpStr);
			
		}
		
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
		bConnect.setEnabled(false);
		bInit.setEnabled(true);
		bReset.setEnabled(false);
		mMsg.setText("");
		tFileID1.setText("");
		tFileID1.setEnabled(false);
		tFileID2.setText("");
		tFileID2.setEnabled(false);
		tFileLen1.setText("");
		tFileLen1.setEnabled(false);
		tFileLen2.setText("");
		tFileLen2.setEnabled(false);
		bFormat.setEnabled(false);
		tFID1.setText("");
		tFID1.setEnabled(false);
		tFID2.setText("");
		tFID2.setEnabled(false);
		tOffset1.setText("");
		tOffset1.setEnabled(false);
		tOffset2.setText("");
		tOffset2.setEnabled(false);
		tLen.setText("");
		tLen.setEnabled(false);
		mData.setText("");
		mData.setEnabled(false);
		bRead.setEnabled(false);
		bWrite.setEnabled(false);
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
  		

  		//Check valid characters
  		if (VALIDCHARS.indexOf(x) == -1 ) 
  			ke.setKeyChar(empty);
  		
  					  
		//Limit character length
  		if(((JTextField)ke.getSource()).getText().length() >= 2 ) 
  		{
		
  				ke.setKeyChar(empty);
  				return;
  				
  		}			
  			
 		    	
	}
    
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new ACOS3ReadWriteBinary().setVisible(true);
            }
        });
    }



}
