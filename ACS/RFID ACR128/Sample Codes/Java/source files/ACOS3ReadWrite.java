
/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              ACOS3ReadWrite.java

  Description:       This sample program outlines the steps on how to
                     format the ACOS card and how to  read or write
                     data into it using the PC/SC platform.                 

  Author:            M.J.E.C. Castillo

  Date:              August 20, 2008

  Revision Trail:   (Date/Author/Description)

======================================================================*/

import java.io.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class ACOS3ReadWrite extends JFrame implements ActionListener{

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
	
	//GUI Variables
	private JTextArea Msg;
    private JButton bConn;
    private JButton bFormat;
    private JButton bInit;
    private JButton bRead;
    private JButton bReset;
    private JButton bWrite;
    private JButton bQuit;
    private ButtonGroup bgRecord;
    private JPanel bottomPanel;
    private JPanel buttonPanel;
    private JLabel lblReader;
    private JLabel lblVal;
    private JPanel midPanel;
    private JPanel msgPanel;
    private JScrollPane msgScrollPane;
    private JRadioButton rbaa11;
    private JRadioButton rbbb22;
    private JRadioButton rbcc33;
    private JComboBox rdrName;
    private JPanel recordPanel;
    private JPanel topPanel;
    private JTextField txtVal;
    

    public ACOS3ReadWrite() {
    	
    	this.setTitle("ACOS 3 Read Write");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

   		setSize(650,470);
   		bgRecord = new ButtonGroup();
        topPanel = new JPanel();
        lblReader = new JLabel();
        bInit = new JButton();
        bConn = new JButton();
        bQuit = new JButton();
        bFormat = new JButton();
        midPanel = new JPanel();
        recordPanel = new JPanel();
        rbcc33 = new JRadioButton();
        rbbb22 = new JRadioButton();
        rbaa11 = new JRadioButton();
        buttonPanel = new JPanel();
        bRead = new JButton();
        bWrite = new JButton();
        bottomPanel = new JPanel();
        lblVal = new JLabel();
        txtVal = new JTextField();
        bReset = new JButton();
        msgPanel = new JPanel();
        msgScrollPane = new JScrollPane();
        Msg = new JTextArea();

        lblReader.setText("Select Reader");

        String[] rdrNameDef = {"Please select reader                   "};	
		rdrName = new JComboBox(rdrNameDef);
		rdrName.setSelectedIndex(0);
		
        bInit.setText("Initialize");

        bConn.setText("Connect");

        bFormat.setText("Format Card");

        GroupLayout topPanelLayout = new GroupLayout(topPanel);
        topPanel.setLayout(topPanelLayout);
        topPanelLayout.setHorizontalGroup(
            topPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(topPanelLayout.createSequentialGroup()
                .addGroup(topPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(topPanelLayout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(lblReader, GroupLayout.PREFERRED_SIZE, 82, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(rdrName, 0, 154, Short.MAX_VALUE))
                    .addGroup(GroupLayout.Alignment.TRAILING, topPanelLayout.createSequentialGroup()
                        .addGap(122, 122, 122)
                        .addComponent(bInit, GroupLayout.DEFAULT_SIZE, 125, Short.MAX_VALUE))
                    .addGroup(GroupLayout.Alignment.TRAILING, topPanelLayout.createSequentialGroup()
                        .addGap(122, 122, 122)
                        .addComponent(bConn, GroupLayout.DEFAULT_SIZE, 125, Short.MAX_VALUE))
                    .addGroup(GroupLayout.Alignment.TRAILING, topPanelLayout.createSequentialGroup()
                        .addGap(122, 122, 122)
                        .addComponent(bFormat, GroupLayout.DEFAULT_SIZE, 125, Short.MAX_VALUE)))
                .addContainerGap())
        );
        topPanelLayout.setVerticalGroup(
            topPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(topPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(topPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblReader)
                    .addComponent(rdrName, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addGap(18, 18, 18)
                .addComponent(bInit)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bConn)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(bFormat)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        recordPanel.setBorder(BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));

        bgRecord.add(rbcc33);
        rbcc33.setText("CC 33");
        rbcc33.setBorder(null);

        bgRecord.add(rbbb22);
        rbbb22.setText("BB 22");
        rbbb22.setBorder(null);

        bgRecord.add(rbaa11);
        rbaa11.setText("AA 11");
        rbaa11.setBorder(null);

        GroupLayout recordPanelLayout = new GroupLayout(recordPanel);
        recordPanel.setLayout(recordPanelLayout);
        recordPanelLayout.setHorizontalGroup(
            recordPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(recordPanelLayout.createSequentialGroup()
                .addGap(15, 15, 15)
                .addGroup(recordPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(rbbb22, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, 63, Short.MAX_VALUE)
                    .addGroup(recordPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                        .addComponent(rbcc33)
                        .addComponent(rbaa11)))
                .addContainerGap())
        );
        recordPanelLayout.setVerticalGroup(
            recordPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(recordPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(rbaa11)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbbb22)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbcc33)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        bRead.setText("Read");

        bWrite.setText("Write");

        GroupLayout buttonPanelLayout = new GroupLayout(buttonPanel);
        buttonPanel.setLayout(buttonPanelLayout);
        buttonPanelLayout.setHorizontalGroup(
            buttonPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(buttonPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(buttonPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(bWrite, GroupLayout.DEFAULT_SIZE, 89, Short.MAX_VALUE)
                    .addComponent(bRead, GroupLayout.DEFAULT_SIZE, 89, Short.MAX_VALUE))
                .addContainerGap())
        );
        buttonPanelLayout.setVerticalGroup(
            buttonPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(buttonPanelLayout.createSequentialGroup()
                .addGap(17, 17, 17)
                .addComponent(bRead)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bWrite)
                .addContainerGap(12, Short.MAX_VALUE))
        );

        GroupLayout midPanelLayout = new GroupLayout(midPanel);
        midPanel.setLayout(midPanelLayout);
        midPanelLayout.setHorizontalGroup(
            midPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(midPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(recordPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, 38, Short.MAX_VALUE)
                .addComponent(buttonPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );
        midPanelLayout.setVerticalGroup(
            midPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(GroupLayout.Alignment.TRAILING, midPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(midPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                    .addComponent(buttonPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(recordPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addContainerGap())
        );

        lblVal.setText("String Value of Data");

        bReset.setText("Reset");

        bQuit.setText("Quit");

        javax.swing.GroupLayout bottomPanelLayout = new javax.swing.GroupLayout(bottomPanel);
        bottomPanel.setLayout(bottomPanelLayout);
        bottomPanelLayout.setHorizontalGroup(
            bottomPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(bottomPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(bottomPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(bottomPanelLayout.createSequentialGroup()
                        .addGroup(bottomPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(txtVal, javax.swing.GroupLayout.DEFAULT_SIZE, 231, Short.MAX_VALUE)
                            .addComponent(lblVal, javax.swing.GroupLayout.PREFERRED_SIZE, 109, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addContainerGap())
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, bottomPanelLayout.createSequentialGroup()
                        .addComponent(bReset, javax.swing.GroupLayout.PREFERRED_SIZE, 93, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(18, 18, 18)
                        .addComponent(bQuit, javax.swing.GroupLayout.PREFERRED_SIZE, 93, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(26, 26, 26))))
        );
        bottomPanelLayout.setVerticalGroup(
            bottomPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(bottomPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(lblVal)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(txtVal, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 15, Short.MAX_VALUE)
                .addGroup(bottomPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(bQuit)
                    .addComponent(bReset))
                .addContainerGap())
        );

        Msg.setColumns(20);
        Msg.setRows(5);
        msgScrollPane.setViewportView(Msg);

        GroupLayout msgPanelLayout = new GroupLayout(msgPanel);
        msgPanel.setLayout(msgPanelLayout);
        msgPanelLayout.setHorizontalGroup(
            msgPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGap(0, 340, Short.MAX_VALUE)
            .addGroup(msgPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                .addComponent(msgScrollPane, GroupLayout.DEFAULT_SIZE, 340, Short.MAX_VALUE))
        );
        msgPanelLayout.setVerticalGroup(
            msgPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGap(0, 367, Short.MAX_VALUE)
            .addGroup(msgPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                .addComponent(msgScrollPane, GroupLayout.DEFAULT_SIZE, 367, Short.MAX_VALUE))
        );

        GroupLayout layout = new GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addGroup(GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                        .addComponent(bottomPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED))
                    .addComponent(topPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(midPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(msgPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(msgPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(topPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(midPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addGap(18, 18, 18)
                        .addComponent(bottomPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))))
        );
        
        bInit.addActionListener(this);
        bConn.addActionListener(this);
        bFormat.addActionListener(this);
        bRead.addActionListener(this);
        bWrite.addActionListener(this);
        bReset.addActionListener(this);
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
			{
			
				rdrName.addItem("No PC/SC reader detected");
				
			}
			
			bInit.setEnabled(false);
			bConn.setEnabled(true);
			bReset.setEnabled(true);
			
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
		    
		    connActive = true;
		    bFormat.setEnabled(true);
		    rbaa11.setEnabled(true);
		    rbaa11.setSelected(true);
		    rbbb22.setEnabled(true);
		    rbcc33.setEnabled(true);
		    rbaa11.setEnabled(true);
		    bRead.setEnabled(true);
		    bWrite.setEnabled(true);
			
		}
		
		if(bQuit == e.getSource())
		{
			
			this.dispose();
			
		}
		
		if(bFormat==e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			byte[] tmpArray = new byte[31];
			
			//Send IC Code
			retCode = submitIC();
			if (retCode != ACSModule.SCARD_S_SUCCESS)
					return;
			
			//select FF 02
			retCode = selectFile((byte)0xFF,(byte) 0x2);
			if (retCode != ACSModule.SCARD_S_SUCCESS)
				return;
	
			for(int i=0; i<SendLen; i++)
			{
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
				
			}
			if (tmpStr.indexOf("90 00") < 0)
			{
				
				displayOut(2, 0, "The return string is invalid. Value: " + tmpStr);
			    retCode = INVALID_SW1SW2;
				return;
			}
			
			// 3. Write to FF 02
			//    This will create 3 User files, no Option registers and
			//    Security Option registers defined, Personalization bit
			//    is not set
			  tmpArray[0] =  (byte)0x00;    // 00    Option registers
			  tmpArray[1] =  (byte)0x00;    // 00    Security option register
			  tmpArray[2] =  (byte)0x03;    // 03    No of user files
			  tmpArray[3] =  (byte)0x00;    // 00    Personalization bit
			
			  retCode = writeRecord(0, (byte) 0x00, (byte) 0x04, (byte) 0x04, tmpArray);
			  if (retCode != ACSModule.SCARD_S_SUCCESS)
			    return;
			  
			  displayOut(0, 0, "File FF 02 is updated.");
			  
			  //4. Perform a reset for changes in the ACOS to take effect
			  connActive = false;
			  retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
			  if (retCode != ACSModule.SCARD_S_SUCCESS)
			  {
				  
				  displayOut(0, retCode, "");
				  return;
				  
			  }
			  
			  String rdrcon = (String)rdrName.getSelectedItem();  
			  
			  retCode = jacs.jSCardConnect(hContext, 
						rdrcon, 
						ACSModule.SCARD_SHARE_SHARED,
						ACSModule.SCARD_PROTOCOL_T0 | ACSModule.SCARD_PROTOCOL_T1,
						hCard, 
						PrefProtocols);
			  
			  if (retCode != ACSModule.SCARD_S_SUCCESS)
			  {
				  
				  displayOut(0, retCode, "");
				  return;
				  
			  }
			  
			  displayOut(3, 0, "Card reset is successful.");
			  connActive = true;
			  
			  //select FF 04
			  retCode = selectFile((byte)0xFF, (byte)0x04);
			  if (retCode != ACSModule.SCARD_S_SUCCESS)
				  return;
			  
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
			      displayOut(2, 0, "The return string is invalid. Value: " + tmpStr);
			      retCode = INVALID_SW1SW2;
			      return;
			      
			  }

			  // 6. Send IC Code
			  retCode = submitIC();
			  if (retCode != ACSModule.SCARD_S_SUCCESS)
				  return;

			  // 7. Write to FF 04
			  // 7.1. Write to first record of FF 04
			  tmpArray[0] = (byte)0x0A;    // 10    Record length
			  tmpArray[1] = (byte)0x01;    // 1     No of records
			  tmpArray[2] = (byte)0x00;    // 00    Read security attribute
			  tmpArray[3] = (byte)0x00;    // 00    Write security attribute
			  tmpArray[4] = (byte)0xAA;    // AA    File identifier
			  tmpArray[5] = (byte)0x11;    // 11    File identifier
			  tmpArray[6] = (byte)0x00;    // File Access Flag
			  retCode = writeRecord(0,(byte) 0x00,(byte) 0x07, (byte)0x07, tmpArray);
			  if (retCode != ACSModule.SCARD_S_SUCCESS)
				  return;
			  
			  displayOut(0, 0, "User File AA 11 is defined.");

			  // 7.2. Write to second record of FF 04
			  tmpArray[0] = (byte)0x10;    // 16    Record length
			  tmpArray[1] = (byte)0x01;    // 1     No of records
			  tmpArray[2] = (byte)0x00;    // 00    Read security attribute
			  tmpArray[3] = (byte)0x00;    // 00    Write security attribute
			  tmpArray[4] = (byte)0xBB;    // BB    File identifier
			  tmpArray[5] = (byte)0x22;    // 22    File identifier
			  tmpArray[6] = (byte)0x00;    // File Access Flag
			  retCode = writeRecord(0,(byte) 0x01, (byte)0x07,(byte) 0x07, tmpArray);
			  if (retCode != ACSModule.SCARD_S_SUCCESS)
				  return;
			  
			  displayOut(0, 0, "User File BB 22 is defined.");

			  // 7.3. Write to third record of FF 04
			  tmpArray[0] = (byte)0x20;    // 32    Record length
			  tmpArray[1] = (byte)0x01;    // 1     No of records
			  tmpArray[2] = (byte)0x00;    // 00    Read security attribute
			  tmpArray[3] = (byte)0x00;    // 00    Write security attribute
			  tmpArray[4] = (byte)0xCC;    // CC    File identifier
			  tmpArray[5] = (byte)0x33;    // 33    File identifier
			  tmpArray[6] = (byte)0x00;    // File Access Flag
			  retCode = writeRecord(0, (byte)0x02, (byte)0x07, (byte)0x07, tmpArray);
			  if (retCode != ACSModule.SCARD_S_SUCCESS)
				  return;
			  
			  displayOut(0, 0, "User File CC 33 is defined.");

			  rbaa11.setSelected(true);
			  txtVal.setText("");
		}
		
		if (bRead == e.getSource())
		{
			
			byte hiByte=0, loByte=0, lenByte=0;
			String tmpStr = "", tmpHex="", chkStr="";
			
			//select user file
			if( rbaa11.isSelected())
			{
				
				hiByte = (byte) 0xAA;
				loByte = (byte) 0x11;
				chkStr = "91 00";
				lenByte = (byte) 0x0A;
				
			}
			
			if( rbbb22.isSelected())
			{
				
				hiByte = (byte) 0xBB;
				loByte = (byte) 0x22;
				chkStr = "91 01";
				lenByte = (byte) 0x10;
				
			}
			
			if( rbcc33.isSelected())
			{
				
				hiByte = (byte) 0xCC;
				loByte = (byte) 0x33;
				chkStr = "91 02";
				lenByte = (byte) 0x20;
				
			}
			
			retCode = selectFile(hiByte, loByte);
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i=0; i<2; i++){
				  tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex;  
				  
			  }
			 
			 if (tmpStr.indexOf(chkStr) < 0)
			 {
				 
				  displayOut(2, 0, "The return string is invalid. Value: " + tmpStr);
			      retCode = INVALID_SW1SW2;
			      return;
				 
			 }
			
			 //read first record of user file selected
			 retCode = readRecord((byte)0x00, (byte)lenByte);
			  if (retCode != ACSModule.SCARD_S_SUCCESS)
			    return;
			  
			  tmpStr = "";
			  int indx = 0;
			  while (RecvBuff[indx] != (byte)0x00)
			  {
			
			     // if (indx < txtVal.getText().length())
			        tmpStr  = tmpStr + (char)RecvBuff[indx];
			      
			      indx++;
		
			  }
			  txtVal.setText(tmpStr);
			  displayOut(0, 0, "Data read is displayed in Message Box.");
		}
		
		if(bWrite == e.getSource())
		{
			
			byte hiByte=0, loByte=0;
			String tmpStr="", tmpHex="", chkStr="";
			byte[] tmpArray = new byte[56];
			int tmpInt;
			
			// 1. Validate input
			if (txtVal.getText() =="")
			{
			    txtVal.requestFocus();
			    return;
			}
			
			if (rbaa11.isSelected())
			{
			
				hiByte = (byte) 0xAA;
				loByte = (byte) 0x11;
				chkStr = "91 00";
				txtVal.setColumns(10);
				
			}

			if (rbbb22.isSelected())
			{
			
				hiByte = (byte) 0xBB;
				loByte = (byte) 0x22;
				chkStr = "91 01";
				txtVal.setColumns(16);
				
			}

			if (rbcc33.isSelected())
			{
			
				hiByte = (byte) 0xCC;
				loByte = (byte) 0x33;
				chkStr = "91 02";
				txtVal.setColumns(32);
				
			}
			
			retCode = selectFile(hiByte, loByte);
			if (retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i=0; i<2; i++){
				  tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex;  
				  
			  }
			
			 if (tmpStr.indexOf(chkStr) < 0)
			 {
				 
				  displayOut(2, 0, "The return string is invalid. Value: " + tmpStr);
			      retCode = INVALID_SW1SW2;
			      return;
				 
			 }
			 
			 // 3. Write input data to card

			  tmpStr = txtVal.getText();
			  tmpArray = tmpStr.getBytes();
			 
			  retCode = writeRecord(1, (byte)0x00, (byte)txtVal.getColumns(), (byte)tmpStr.length(), tmpArray);
			  if (retCode != ACSModule.SCARD_S_SUCCESS)
					return;
			  
			  displayOut(0, 0, "Data read from Message Box is written to card.");
		}
		
		if(bReset == e.getSource())
		{
			
			if (connActive)
			{
				
				 retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
				 connActive = false;
				
			}
			
			retCode = jacs.jSCardReleaseContext(hCard);
			initMenu();
			
		}
				
	}
    
	public int readRecord(byte recNo, byte dataLen)
	{
		
		  clearBuffers();
		  SendBuff[0] = (byte)0x80;        // CLA
		  SendBuff[1] = (byte)0xB2;        // INS
		  SendBuff[2] = recNo;             // P1    Record No
		  SendBuff[3] = (byte)0x00;        // P2
		  SendBuff[4] = dataLen;           // P3    Length of data to be read
		  SendLen = 5;
		  RecvLen[0] = SendBuff[4] + 2;
		  String tmpStr = "", tmpHex="";
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
		  
		  for(int i=RecvLen[0]-2; i<RecvLen[0]; i++){
			  tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
			  
		  }

		 if (tmpStr.indexOf("90 00") < 0)
		 {
		      displayOut(2, 0, "The return string is invalid. Value: " + tmpStr);
		      retCode = INVALID_SW1SW2;
		 }
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
	
	public void clearBuffers()
	{
		
		for(int i=0; i<300; i++)
		{
			
			SendBuff[i]=0x00;
			RecvBuff[i]=0x00;
			
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
		rdrName.removeAllItems();
		bInit.setEnabled(true);
		bConn.setEnabled(false);
		bFormat.setEnabled(false);
		bReset.setEnabled(true);
		bRead.setEnabled(false);
		bWrite.setEnabled(false);
		rbaa11.setEnabled(false);
		rbbb22.setEnabled(false);
		rbcc33.setEnabled(false);
		Msg.setText("");
		txtVal.setText("");
		displayOut(0, 0, "Program Ready");
		
	}
    
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new ACOS3ReadWrite().setVisible(true);
            }
        });
    }



}
