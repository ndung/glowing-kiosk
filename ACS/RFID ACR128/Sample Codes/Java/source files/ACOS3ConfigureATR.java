
/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              ACOS3ConfigureATR.java

  Description:       This sample program outlines the steps on how to
                     change the ATR of a smart card using the PC/SC platform.
                     You can also change the Card Baud Rate and the Historical Bytes of the card.

  NOTE:            Historical Bytes valid value must be 0 to 9 and A,B,C,D,E,F only. e.g.(11,99,AE,AA,FF etc)
                   if historical byte is leave blank the profram will assume it as 00.
                   After you update the ATR you have to initialize and connect to the device
                   before you can see the updated ATR.
                     
  Author:            M.J.E.C. Castillo

  Date:              August 26, 2008

  Revision Trail:   (Date/Author/Description)

======================================================================*/

import java.io.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class ACOS3ConfigureATR extends JFrame implements ActionListener, ItemListener, KeyListener{

	//JPCSC Variables
	int retCode;
	boolean connActive; 
	public static final int INVALID_SW1SW2 = -450;
	static String VALIDCHARS = "0123456789abcdefABCDEF";
	int num_historical_byte; 
	String[] ctrbyte = new String [14]; 

	//All variables that requires pass-by-reference calls to functions are
	//declared as 'Array of int' with length 1
	//Java does not process pass-by-ref to int-type variables, thus Array of int was used.
	//int [] ATRLen = new int[1]; 
	int [] hContext = new int[1]; 
	int [] cchReaders = new int[1];
	int [] hCard = new int[1];
	int [] PrefProtocols = new int[1]; 		
	int [] RecvLen = new int[1];
	int SendLen = 0;
	int [] ATRLen = new int[1]; 
	byte [] SendBuff = new byte[300];
	byte [] RecvBuff = new byte[300];
	byte [] ATRVal = new byte[128];
	byte [] szReaders = new byte[1024];
	byte[] tmpAPDU = new byte[35];	
	int [] phCard;
	String mszReaderNames;
	int [] pcchReaderLen;
	int [] pdwState;
	int [] pdwProtocol;
	byte[] pbATR;
	int [] pcbAtrLen;
	
	static JacspcscLoader jacs = new JacspcscLoader();
	
	private JTextArea Msg;
	private JButton bClear;
	private JButton bConnect;
	private JButton bDisconnect;
	private JButton bInit;
	private JButton bReset;
	private JButton bQuit;
	private JButton bUpdate;
	private JComboBox cbNoBytes;
	private JComboBox cbRate;
	private JComboBox cbReader;
	private JLabel jLabel1;
	private JLabel jLabel2;
	private JLabel jLabel3;
	private JLabel jLabel4;
	private JLabel jLabel5;
	private JScrollPane jScrollPane1;
	private JLabel lblSelect;
	private JTextField t1;
	private JTextField t10;
	private JTextField t11;
	private JTextField t12;
	private JTextField t13;
	private JTextField t14;
	private JTextField t15;
	private JTextField t2;
	private JTextField t3;
	private JTextField t4;
	private JTextField t5;
	private JTextField t6;
	private JTextField t7;
	private JTextField t8;
	private JTextField t9;
	private JTextField txtT0;
	private JTextField txtTA;
    

    public ACOS3ConfigureATR() {
    	
    	this.setTitle("ACOS 3 Configure ATR");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

   	  	setSize(550,500);
		lblSelect = new JLabel();
        cbReader = new JComboBox();
        bInit = new JButton();
        bConnect = new JButton();
        bReset = new JButton();
        cbRate = new JComboBox();
        jLabel1 = new JLabel();
        jLabel2 = new JLabel();
        txtTA = new JTextField();
        cbNoBytes = new JComboBox();
        jLabel3 = new JLabel();
        txtT0 = new JTextField();
        jLabel4 = new JLabel();
        jLabel5 = new JLabel();
        t1 = new JTextField();
        t2 = new JTextField();
        t3 = new JTextField();
        t4 = new JTextField();
        t5 = new JTextField();
        t6 = new JTextField();
        t7 = new JTextField();
        t8 = new JTextField();
        t9 = new JTextField();
        t10 = new JTextField();
        t11 = new JTextField();
        t12 = new JTextField();
        t13 = new JTextField();
        t14 = new JTextField();
        t15 = new JTextField();
        bUpdate = new JButton();
        jScrollPane1 = new JScrollPane();
        Msg = new JTextArea();
        bClear = new JButton();
        bQuit = new JButton();
        bDisconnect = new JButton();


        lblSelect.setText("Select Reader");


	    String[] rdrNameDef = {"Please select reader"};	
	    cbReader = new JComboBox(rdrNameDef);
	    cbReader.setSelectedIndex(0);
			

        bInit.setText("Initialize");

        bConnect.setText("Connect");

        bReset.setText("Get ATR");

        cbRate.setModel(new DefaultComboBoxModel(new String[] {"9600", "14400", "28800", "57600" , "115200"}));      

        jLabel1.setText("Card BaudRate");

        jLabel2.setText("TA");

        txtTA.setText("");

        cbNoBytes.setModel(new DefaultComboBoxModel(new String[] {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "0A", "0B", "0C", "0D", "0E", "0F", "FF" }));

        jLabel3.setText("No. of Historical Bytes");

        txtT0.setText("");

        jLabel4.setText("T0");

        jLabel5.setText("Historical Bytes");

        t1.setText("");

        t2.setText("");

        t3.setText("");

        t4.setText("");

        t5.setText("");

        t6.setText("");

        t7.setText("");

        t8.setText("");

        t9.setText("");

        t10.setText("");

        t11.setText("");

        t12.setText("");

        t13.setText("");

        t14.setText("");

        t15.setText("");

        bUpdate.setText("Update ATR");

        Msg.setColumns(20);
        Msg.setRows(5);
        jScrollPane1.setViewportView(Msg);

        bClear.setText("Clear");

        bDisconnect.setText("Disconnect");

        bQuit.setText("Quit");

        GroupLayout layout = new GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addContainerGap()
                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(lblSelect, GroupLayout.PREFERRED_SIZE, 80, javax.swing.GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(cbReader, GroupLayout.PREFERRED_SIZE, 152, GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED))
                            .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                                .addComponent(bConnect,GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                .addComponent(bInit, GroupLayout.DEFAULT_SIZE, 153, Short.MAX_VALUE)
                                .addComponent(bReset, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))))
                    .addGroup(layout.createSequentialGroup()
                        .addGap(27, 27, 27)
                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(jLabel1)
                                .addGap(87, 87, 87)
                                .addComponent(jLabel2))
                            .addGroup(layout.createSequentialGroup()
                                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                                    .addComponent(cbRate,GroupLayout.PREFERRED_SIZE, 110, javax.swing.GroupLayout.PREFERRED_SIZE)
                                    .addComponent(cbNoBytes,GroupLayout.PREFERRED_SIZE, 110,GroupLayout.PREFERRED_SIZE)
                                    .addComponent(jLabel3)
                                    .addComponent(jLabel5))
                                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                                    .addGroup(layout.createSequentialGroup()
                                        .addGap(52, 52, 52)
                                        .addComponent(jLabel4))
                                    .addGroup(layout.createSequentialGroup()
                                        .addGap(33, 33, 33)
                                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                                            .addComponent(txtT0, GroupLayout.PREFERRED_SIZE, 51, GroupLayout.PREFERRED_SIZE)
                                            .addComponent(txtTA, GroupLayout.PREFERRED_SIZE, 51, GroupLayout.PREFERRED_SIZE)))))
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(t1, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(t2, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(t3, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(t4,GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(t5,GroupLayout.PREFERRED_SIZE, 35,GroupLayout.PREFERRED_SIZE))
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(t6,GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(t7,GroupLayout.PREFERRED_SIZE, 35,GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(t8,GroupLayout.PREFERRED_SIZE, 35,GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(t9,GroupLayout.PREFERRED_SIZE, 35,GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(t10,GroupLayout.PREFERRED_SIZE, 35,GroupLayout.PREFERRED_SIZE))
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(t11,GroupLayout.PREFERRED_SIZE, 35,GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(t12, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(t13,GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(t14,GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(t15, GroupLayout.PREFERRED_SIZE, 35,GroupLayout.PREFERRED_SIZE))))
                    .addGroup(layout.createSequentialGroup()
                        .addGap(84, 84, 84)
                        .addComponent(bUpdate)))
                .addGap(18, 18, 18)
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(jScrollPane1, GroupLayout.DEFAULT_SIZE, 265, Short.MAX_VALUE)
                        .addContainerGap())
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                        .addComponent(bDisconnect, GroupLayout.PREFERRED_SIZE, 100,GroupLayout.PREFERRED_SIZE)
                        .addGap(20, 20, 20)
                        .addComponent(bQuit)
                        .addGap(20, 20, 20))
                        .addComponent(bClear)
                        .addGap(35, 35, 35)))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(lblSelect)
                            .addComponent(cbReader, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE,GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(bInit)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bConnect)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bReset)
                        .addGap(21, 21, 21)
                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel1)
                            .addComponent(jLabel2))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(cbRate, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(txtTA, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addGap(10, 10, 10)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel3)
                            .addComponent(jLabel4))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(cbNoBytes, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(txtT0, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addGap(26, 26, 26)
                        .addComponent(jLabel5)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(t1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(t2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(t3, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(t4, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(t5, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(t6, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(t7, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(t8, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(t9, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(t10, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(t11, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(t12, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(t13, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(t14, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(t15, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addGap(18, 18, 18)
                        .addComponent(bUpdate))
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 372, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                        	.addComponent(bQuit)
                        	.addComponent(bDisconnect)
                            .addComponent(bClear)
                            )))
                .addContainerGap(23, Short.MAX_VALUE))
        );
        
        bInit.addActionListener(this);
        bConnect.addActionListener(this);
        bDisconnect.addActionListener(this);
        bClear.addActionListener(this);
        bReset.addActionListener(this);
        bQuit.addActionListener(this);
        bUpdate.addActionListener(this);        
        cbRate.addItemListener(this);        
        cbNoBytes.addItemListener(this);
        txtTA.addKeyListener(this);     
        txtT0.addKeyListener(this); 
        
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
			txtTA.setEnabled(true);
			txtT0.setEnabled(true);
			t1.setEnabled(true);
			t2.setEnabled(true);
			t3.setEnabled(true);
			t4.setEnabled(true);
			t5.setEnabled(true);
			t6.setEnabled(true);
			t7.setEnabled(true);
			t8.setEnabled(true);
			t9.setEnabled(true);
			t10.setEnabled(true);
			t11.setEnabled(true);
			t12.setEnabled(true);
			t13.setEnabled(true);
			t14.setEnabled(true);
			t15.setEnabled(true);
	
			
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
		    bDisconnect.setEnabled(true);
		    bReset.setEnabled(true);
		    bUpdate.setEnabled(true);
	
		    		
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
			Msg.setText("");
			bInit.setEnabled(true);
			bUpdate.setEnabled(false);
			bReset.setEnabled(false);
			initMenu();
		
		} // Disconnect
		
		if (bReset == e.getSource())
		{
			
			int tmpWord;
			int[] state = new int[1];
			int[] readerLen = new int[1];
			String tmpStr;
			
			state[0]=0;
			readerLen[0]=0;
			for(int i=0; i<128; i++)
				ATRVal[i] = 0;
			
		    tmpWord = 33;
		    ATRLen[0] = tmpWord;
		    String rdrcon= (String)cbReader.getSelectedItem();  
		    
		    byte [] tmpReader	= rdrcon.getBytes();
		    byte [] readerName	= new byte[rdrcon.length()+1];
		      
		      for (int i=0; i<rdrcon.length(); i++)
		      	readerName[i] = tmpReader[i];
		      readerName[rdrcon.length()] = 0; //set null terminator
		      
		    retCode = jacs.jSCardStatus(hCard, 
					tmpReader, 
					readerLen, 
					state, 
					PrefProtocols, 
					ATRVal, 
					ATRLen);
		    
		    if (retCode != ACSModule.SCARD_S_SUCCESS)
		    {
		    	
		    	displayOut(1, retCode, "");
		    	
		    }
		    else
		    {
		    	displayOut(0,0,"ScardStatus OK");
		    	
		    	switch (PrefProtocols[0])
		    	{
		    	case 0 : 
				displayOut(0,0,"Active Protocol Undefined");
				break;
				
		    	case 1 :
				displayOut(0,0,"Active Protocol T0");
				break;
				
		    	case 2 : 
		    	displayOut(0,0,"Active Protocol T1");
				break;
		    	}
	
		    	String strHex;
		    	tmpStr = "ATR Length: " + ATRLen[0];
		    	displayOut(3, 0, tmpStr);
		    	tmpStr = "ATR Value: ";
		    	
		    	for(int i=0; i<ATRLen[0]; i++)
		    	{
		    		
		    		//Byte to Hex conversion
					strHex = Integer.toHexString(((Byte)ATRVal[i]).intValue() & 0xFF).toUpperCase();  
					
					//For single character hex
					if (strHex.length() == 1) 
						strHex = "0" + strHex;
					
					tmpStr += " " + strHex;  
					
					//new line every 12 bytes
					if ( ((i+1) % 12 == 0) && ( (i+1) < ATRLen[0] ) )  
						tmpStr += "\n  ";	
					
		    	}
		    	displayOut(3 , 0, tmpStr);
		    	
		    	txtTA.setText(Integer.toHexString(((Byte)ATRVal[1]).intValue() & 0xFF).toUpperCase());
		    	txtT0.setText(Integer.toHexString(((Byte)ATRVal[2]).intValue() & 0xFF).toUpperCase());
		    
				cbNoBytes.setEnabled(true);
				cbRate.setEnabled(true);
			
		    	cbRate.setSelectedIndex(4);
		    	cbNoBytes.setSelectedIndex(14);		    			  		   
		    	
		    } 
		} // Get ATR
		
		if (bUpdate == e.getSource())
		{
			
			String tmpStr = "",  tmpHex=""; 
			int indx;	
			int ctr;
			
			selectFile((byte)0xFF, (byte)0x07);
			if (retCode != ACSModule.SCARD_S_SUCCESS)
			{
			    	
		    	displayOut(1, retCode, "");
			    return;
			    	
			}
			 
			for(int i =0; i<SendLen; i++){
				tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
				  
			  }  
			 
			 submitIC();
			 
			 if (retCode != ACSModule.SCARD_S_SUCCESS)
			 {
			    	
			    displayOut(1, retCode, "");
			    return;
				    	
			 }
			 
			num_historical_byte = cbNoBytes.getSelectedIndex();
			
			tmpAPDU[0] = (byte)Integer.parseInt(txtTA.getText());
				
			if (num_historical_byte == 16) 
			{
				tmpAPDU[1] = (byte)0xFF; //restoring to it original historical bytes
			}
			else
			{
				tmpAPDU[1] = (byte)(num_historical_byte);
				
			}
			ctr = 2;
			
			if (num_historical_byte < 16)
			{
				for (indx = 0; indx< num_historical_byte; indx++)
		             
					if ((ctrbyte[indx]) == null)  
					{
						ctrbyte[indx] = "00";
					}

				for (indx = 0; indx<(num_historical_byte); indx++)
				{
					tmpAPDU[ctr] = (byte)Integer.parseInt(ctrbyte[indx]);

					ctr = ctr + 1;
				}
			}
			
			for (indx = (ctr + 1); indx < 35; indx++)
				tmpAPDU[indx] = 0x00;

			writeRecord((byte)0x00,(byte)0x36, tmpAPDU);
		 

		}
		
		if(bQuit == e.getSource())
		{
			
			this.dispose();
			
		}
				
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
	
	
	public void itemStateChanged(ItemEvent e) 
	{
	    
		if (e.getStateChange() == ItemEvent.SELECTED)
		{
	     
			switch (cbRate.getSelectedIndex())
	    	{
	    	case 0: txtTA.setText("11");
	    		break;
	    	case 1: txtTA.setText("92");
	    		break;
	    	case 2: txtTA.setText("93");
	    		break;
	    	case 3: txtTA.setText("94");
	    		break;
	    	case 4: txtTA.setText("95");
	    		break;
	    			    			   		  
	    	}
		} 
		
		if (e.getStateChange() == ItemEvent.SELECTED)
		{
			switch (cbNoBytes.getSelectedIndex())
			{ 
				case 0 : txtT0.setText("B0");
					break;
				case 1 : txtT0.setText("B1");
					break;
				case 2 : txtT0.setText("B2");
					break;
				case 3 : txtT0.setText("B3");
					break;
				case 4 : txtT0.setText("B4");
					break;
				case 5 : txtT0.setText("B5");
					break;
				case 6 : txtT0.setText("B6");
					break;
				case 7 : txtT0.setText("B7");
					break;
				case 8 : txtT0.setText("B8");
					break;
				case 9 : txtT0.setText("B9");
					break;
				case 10 : txtT0.setText("BA");
					break;
				case 11 :  txtT0.setText("BB");
					break;
				case 12 : txtT0.setText("BC");
					break;
				case 13 :  txtT0.setText("BD");
					break;
				case 14 :  txtT0.setText("BE");
					break;
				case 15 :  txtT0.setText("BF");
					break;
				case 16 :  txtT0.setText("BE");
					break;
			}
			
			switch (cbNoBytes.getSelectedIndex())
			{
			case 0:
			{
				t1.setEnabled(false);
				t2.setEnabled(false);
				t3.setEnabled(false);
				t4.setEnabled(false);
				t5.setEnabled(false);
				t6.setEnabled(false);
				t7.setEnabled(false);
				t8.setEnabled(false);
				t9.setEnabled(false);
				t10.setEnabled(false);
				t11.setEnabled(false);
				t12.setEnabled(false);
				t13.setEnabled(false);
				t14.setEnabled(false);
				t15.setEnabled(false);		
				break;
			}
			case 1:
			{
				t1.setEnabled(true);
				t2.setEnabled(false);
				t3.setEnabled(false);
				t4.setEnabled(false);
				t5.setEnabled(false);
				t6.setEnabled(false);
				t7.setEnabled(false);
				t8.setEnabled(false);
				t9.setEnabled(false);
				t10.setEnabled(false);
				t11.setEnabled(false);
				t12.setEnabled(false);
				t13.setEnabled(false);
				t14.setEnabled(false);
				t15.setEnabled(false);		
				break;
			}
			case 2:
			{
				t1.setEnabled(true);
				t2.setEnabled(true);
				t3.setEnabled(false);
				t4.setEnabled(false);
				t5.setEnabled(false);
				t6.setEnabled(false);
				t7.setEnabled(false);
				t8.setEnabled(false);
				t9.setEnabled(false);
				t10.setEnabled(false);
				t11.setEnabled(false);
				t12.setEnabled(false);
				t13.setEnabled(false);
				t14.setEnabled(false);
				t15.setEnabled(false);		
				break;
			}
				
			case 3:
			{

				t1.setEnabled(true);
				t2.setEnabled(true);
				t3.setEnabled(true);
				t4.setEnabled(false);
				t5.setEnabled(false);
				t6.setEnabled(false);
				t7.setEnabled(false);
				t8.setEnabled(false);
				t9.setEnabled(false);
				t10.setEnabled(false);
				t11.setEnabled(false);
				t12.setEnabled(false);
				t13.setEnabled(false);
				t14.setEnabled(false);
				t15.setEnabled(false);		
				break;
			}
			
			case 4:
			{
				t1.setEnabled(true);
				t2.setEnabled(true);
				t3.setEnabled(true);
				t4.setEnabled(true);
				t5.setEnabled(false);
				t6.setEnabled(false);
				t7.setEnabled(false);
				t8.setEnabled(false);
				t9.setEnabled(false);
				t10.setEnabled(false);
				t11.setEnabled(false);
				t12.setEnabled(false);
				t13.setEnabled(false);
				t14.setEnabled(false);
				t15.setEnabled(false);		
				break;
			}

			case 5:
			{
				t1.setEnabled(true);
				t2.setEnabled(true);
				t3.setEnabled(true);
				t4.setEnabled(true);
				t5.setEnabled(true);
				t6.setEnabled(false);
				t7.setEnabled(false);
				t8.setEnabled(false);
				t9.setEnabled(false);
				t10.setEnabled(false);
				t11.setEnabled(false);
				t12.setEnabled(false);
				t13.setEnabled(false);
				t14.setEnabled(false);
				t15.setEnabled(false);		
				break;
			}
			
			case 6:
			{
				t1.setEnabled(true);
				t2.setEnabled(true);
				t3.setEnabled(true);
				t4.setEnabled(true);
				t5.setEnabled(true);
				t6.setEnabled(true);
				t7.setEnabled(false);
				t8.setEnabled(false);
				t9.setEnabled(false);
				t10.setEnabled(false);
				t11.setEnabled(false);
				t12.setEnabled(false);
				t13.setEnabled(false);
				t14.setEnabled(false);
				t15.setEnabled(false);		
				break;
			}
			
			case 7:
			{

				t1.setEnabled(true);
				t2.setEnabled(true);
				t3.setEnabled(true);
				t4.setEnabled(true);
				t5.setEnabled(true);
				t6.setEnabled(true);
				t7.setEnabled(true);
				t8.setEnabled(false);
				t9.setEnabled(false);
				t10.setEnabled(false);
				t11.setEnabled(false);
				t12.setEnabled(false);
				t13.setEnabled(false);
				t14.setEnabled(false);
				t15.setEnabled(false);		
				break;
			}
			
			case 8:
			{

				t1.setEnabled(true);
				t2.setEnabled(true);
				t3.setEnabled(true);
				t4.setEnabled(true);
				t5.setEnabled(true);
				t6.setEnabled(true);
				t7.setEnabled(true);
				t8.setEnabled(true);
				t9.setEnabled(false);
				t10.setEnabled(false);
				t11.setEnabled(false);
				t12.setEnabled(false);
				t13.setEnabled(false);
				t14.setEnabled(false);
				t15.setEnabled(false);		
				break;
			}
			
			case 9:
			{
				t1.setEnabled(true);
				t2.setEnabled(true);
				t3.setEnabled(true);
				t4.setEnabled(true);
				t5.setEnabled(true);
				t6.setEnabled(true);
				t7.setEnabled(true);
				t8.setEnabled(true);
				t9.setEnabled(true);
				t10.setEnabled(false);
				t11.setEnabled(false);
				t12.setEnabled(false);
				t13.setEnabled(false);
				t14.setEnabled(false);
				t15.setEnabled(false);		
				break;
			}
			
			case 10:
			{
				t1.setEnabled(true);
				t2.setEnabled(true);
				t3.setEnabled(true);
				t4.setEnabled(true);
				t5.setEnabled(true);
				t6.setEnabled(true);
				t7.setEnabled(true);
				t8.setEnabled(true);
				t9.setEnabled(true);
				t10.setEnabled(true);
				t11.setEnabled(false);
				t12.setEnabled(false);
				t13.setEnabled(false);
				t14.setEnabled(false);
				t15.setEnabled(false);		
				break;				
			}
			
			case 11:
			{
				t1.setEnabled(true);
				t2.setEnabled(true);
				t3.setEnabled(true);
				t4.setEnabled(true);
				t5.setEnabled(true);
				t6.setEnabled(true);
				t7.setEnabled(true);
				t8.setEnabled(true);
				t9.setEnabled(true);
				t10.setEnabled(true);
				t11.setEnabled(true);
				t12.setEnabled(false);
				t13.setEnabled(false);
				t14.setEnabled(false);
				t15.setEnabled(false);		
				break;		
			}
		
			case 12:
			{

				t1.setEnabled(true);
				t2.setEnabled(true);
				t3.setEnabled(true);
				t4.setEnabled(true);
				t5.setEnabled(true);
				t6.setEnabled(true);
				t7.setEnabled(true);
				t8.setEnabled(true);
				t9.setEnabled(true);
				t10.setEnabled(true);
				t11.setEnabled(true);
				t12.setEnabled(true);
				t13.setEnabled(false);
				t14.setEnabled(false);
				t15.setEnabled(false);		
				break;		
			}
			
			case 13:
			{

				t1.setEnabled(true);
				t2.setEnabled(true);
				t3.setEnabled(true);
				t4.setEnabled(true);
				t5.setEnabled(true);
				t6.setEnabled(true);
				t7.setEnabled(true);
				t8.setEnabled(true);
				t9.setEnabled(true);
				t10.setEnabled(true);
				t11.setEnabled(true);
				t12.setEnabled(true);
				t13.setEnabled(true);
				t14.setEnabled(false);
				t15.setEnabled(false);		
				break;		
			}
			
			case 14:
			{
				t1.setEnabled(true);
				t2.setEnabled(true);
				t3.setEnabled(true);
				t4.setEnabled(true);
				t5.setEnabled(true);
				t6.setEnabled(true);
				t7.setEnabled(true);
				t8.setEnabled(true);
				t9.setEnabled(true);
				t10.setEnabled(true);
				t11.setEnabled(true);
				t12.setEnabled(true);
				t13.setEnabled(true);
				t14.setEnabled(true);
				t15.setEnabled(false);		
				break;		
			}
			
			case 15:
			{
				t1.setEnabled(true);
				t2.setEnabled(true);
				t3.setEnabled(true);
				t4.setEnabled(true);
				t5.setEnabled(true);
				t6.setEnabled(true);
				t7.setEnabled(true);
				t8.setEnabled(true);
				t9.setEnabled(true);
				t10.setEnabled(true);
				t11.setEnabled(true);
				t12.setEnabled(true);
				t13.setEnabled(true);
				t14.setEnabled(true);
				t15.setEnabled(true);		
				break;		
			}
				
		 } // switch
		}
	
	}
	

	public void keyReleased(KeyEvent ke) { }
	
	public void keyPressed(KeyEvent ke) {
  		//restrict paste actions
		if (ke.getKeyCode() == KeyEvent.VK_V ) 
			ke.setKeyCode(KeyEvent.VK_UNDO );						
  	} 
	
  	public void keyTyped(KeyEvent ke) {  		
  		Character x = (Character)ke.getKeyChar();
  		char empty = '\r';
  		

  		//Check valid characters
  		if (VALIDCHARS.indexOf(x) == -1 ) 
  			ke.setKeyChar(empty);
  					  
		//Limit character length to 2
		if   (((JTextField)ke.getSource()).getText().length() >= 2 ) {
		
			ke.setKeyChar(empty);				
		
		 if (txtTA == ke.getSource())
		 {
		    char [] letters;
			letters = txtTA.getText().toCharArray();


		    if (txtTA.getText().equals("11"))
		    {
		    	cbRate.setSelectedIndex(0);		    	
		    }
		    	
		    if (txtTA.getText().equals("92"))
		    {
		    	cbRate.setSelectedIndex(1);		    	
		    }
		    	
		    if (txtTA.getText().equals("93"))
		    {
		   		cbRate.setSelectedIndex(2);		    	
		   	}
		    	
		   	if (txtTA.getText().equals("94"))
	    	{
	    		cbRate.setSelectedIndex(3);		    	
		    }
		    	
		    if (txtTA.getText().equals("95"))
		    {
		    	cbRate.setSelectedIndex(4);		    	
		    }
		 }
		 
		 if (txtT0 == ke.getSource())
		 {
			char [] letters;
			letters = txtT0.getText().toCharArray();
			
			if (txtT0.getText().equals("B0"))
			{
			    cbNoBytes.setSelectedIndex(0);		    	
			}
			    	
			if (txtT0.getText().equals("B1"))
			{
			    cbNoBytes.setSelectedIndex(1);		    	
			}
						
			if (txtT0.getText().equals("B2"))
			{
			    cbNoBytes.setSelectedIndex(2);		    	
			}

			if (txtT0.getText().equals("B3"))
			{
			    cbNoBytes.setSelectedIndex(3);		    	
			}
			
			if (txtT0.getText().equals("B4"))
			{
			    cbNoBytes.setSelectedIndex(4);		    	
			}
			
			if (txtT0.getText().equals("B5"))
			{
			    cbNoBytes.setSelectedIndex(5);		    	
			}
		
			if (txtT0.getText().equals("B6"))
			{
			    cbNoBytes.setSelectedIndex(6);		    	
			}
			
			if (txtT0.getText().equals("B7"))
			{
			    cbNoBytes.setSelectedIndex(7);		    	
			}
		
			if (txtT0.getText().equals("B8"))
			{
			    cbNoBytes.setSelectedIndex(8);		    	
			}
		
			if (txtT0.getText().equals("B9"))
			{
			    cbNoBytes.setSelectedIndex(9);		    	
			}
			
			if (txtT0.getText().equals("BA"))
			{
			    cbNoBytes.setSelectedIndex(10);		    	
			}
		
			if (txtT0.getText().equals("BB"))
			{
			    cbNoBytes.setSelectedIndex(11);		    	
			}
			
			if (txtT0.getText().equals("BC"))
			{
			    cbNoBytes.setSelectedIndex(12);		    	
			}
		
			if (txtT0.getText().equals("BD"))
			{
			    cbNoBytes.setSelectedIndex(13);		    	
			}
		
			if (txtT0.getText().equals("BF"))
			{
			    cbNoBytes.setSelectedIndex(15);		    	
			}
		 }
		 else 
		 {
				switch (cbNoBytes.getSelectedIndex())
				{ 
				case 16:
					cbNoBytes.setSelectedIndex(16);
				case 14:
					cbNoBytes.setSelectedIndex(14);
				}
			
				t1.setText("");
				t2.setText("");
				t3.setText("");
				t4.setText("");
				t5.setText("");
				t6.setText("");
				t7.setText("");
				t8.setText("");
				t9.setText("");
				t10.setText("");
				t11.setText("");
				t12.setText("");
				t13.setText("");
				t14.setText("");
				t15.setText("");
		 }
			
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
	
	public int writeRecord(byte recNo, byte dataLen, byte[] dataIn)
	{
		 String tmpStr="", tmpHex="";
		
		//write data to card
		 clearBuffers();
		 SendBuff[0] =  (byte) 0x80;          // CLA
		 SendBuff[1] =  (byte) 0xD2;          // INS
		 SendBuff[2] =  recNo;        		  // P1    Record to be written
		 SendBuff[3] =  (byte) 0x00;          // P2
		 SendBuff[4] =  dataLen;   			  // P3    Length
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
  		
	
	public void initMenu()
	{
		cbReader.removeAllItems();
		bInit.setEnabled(true);
		bConnect.setEnabled(false);
		bReset.setEnabled(false);
		bUpdate.setEnabled(false);
		bDisconnect.setEnabled(false);			
		txtTA.setEnabled(false);
		txtT0.setEnabled(false);
		t1.setEnabled(false);
		t2.setEnabled(false);
		t3.setEnabled(false);
		t4.setEnabled(false);
		t5.setEnabled(false);
		t6.setEnabled(false);
		t7.setEnabled(false);
		t8.setEnabled(false);
		t9.setEnabled(false);
		t10.setEnabled(false);
		t11.setEnabled(false);
		t12.setEnabled(false);
		t13.setEnabled(false);
		t14.setEnabled(false);
		t15.setEnabled(false);
		cbNoBytes.setEnabled(false);
		cbRate.setEnabled(false);

		
		Msg.setText("");
		displayOut(0, 0, "Program Ready");
		
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
    
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new ACOS3ConfigureATR().setVisible(true);
            }
        });
    }



}
