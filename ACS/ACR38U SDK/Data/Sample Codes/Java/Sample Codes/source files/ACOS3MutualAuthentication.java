/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              mutualAuth.java

  Description:       This sample program outlines the steps on how to
                     use the ACOS card for the Mutual Authentication
                     process using the PC/SC platform.
                    
  Author:            M.J.E.C. Castillo

  Date:              August 26, 2008

  Revision Trail:   (Date/Author/Description)

======================================================================*/

import java.io.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.security.Security;
 
import javax.crypto.Cipher;
import javax.crypto.CipherInputStream;
import javax.crypto.SecretKey;
import javax.crypto.SecretKeyFactory;
import javax.crypto.spec.DESKeySpec;

public class ACOS3MutualAuthentication extends JFrame implements ActionListener, KeyListener{

	//JPCSC Variables
	int retCode;
	boolean connActive; 
	public static final int INVALID_SW1SW2 = -450;
	static String VALIDCHARS = "0123456789abcdefABCDEF";
    private static String algorithm = "DES";
 
	
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
	byte[] cKey = new byte[16]; 
	byte[] tKey = new byte[16]; 
	byte[] tmpArray = new byte[33];
    byte[] CRnd = new byte[8];    			// Card random number
    byte[] TRnd = new byte[8];    			// Terminal random number
	byte[] ReverseKey = new byte[16];      // Reverse of Terminal Key
    byte[] SessionKey = new byte[16];
	byte[] tmpResult = new byte[16];
	String tmpStr;
	byte tmpByte;
	

	static JacspcscLoader jacs = new JacspcscLoader();
		
	private ButtonGroup gbSecure;
	private JTextArea Msg;
	private JButton bClear;
	private JButton bConnect;
	private JButton bDisconnect;
	private JButton bExecMA;
	private JButton bInit;
	private JButton bFormat;
	private JButton bQuit;
	private JComboBox cbReader;
	private JScrollPane jScrollPane1;
	private JLabel lblCardKey;
	private JLabel lblSec;
	private JLabel lblSelect;
	private JLabel lblSelect1;
	private JLabel lblTKey;
	private JRadioButton rb3DES;
	private JRadioButton rbDES;
	private JTextField txtCKey;
	private JTextField txtTKey;
	private int txtMaxLength = 8;
	private int txtMaxLength2 = 16;
    

    public ACOS3MutualAuthentication() {
    	
    	this.setTitle("ACOS 3 Mutual Authentication");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

		//GUI Variables
    	setSize(620,350);
		cbReader = new JComboBox();
		gbSecure = new ButtonGroup();
	    lblSelect = new JLabel();
	    bInit = new JButton();
	    bConnect = new JButton();
	    lblSec = new JLabel();
	    rbDES = new JRadioButton();
	    rb3DES = new JRadioButton();
	    txtCKey = new JTextField();
	    txtTKey = new JTextField();
	    bFormat = new JButton();
	    lblCardKey = new JLabel();
	    lblTKey = new JLabel();
	    bExecMA = new JButton();
	    jScrollPane1 = new JScrollPane();
	    Msg = new JTextArea();
	    bClear = new JButton();
	    bDisconnect = new JButton();
	    bQuit = new JButton();
	    lblSelect1 = new JLabel();
	    
	    
	    lblSelect.setText("Select Reader");

	    String[] rdrNameDef = {"Please select reader                   "};	
		cbReader = new JComboBox(rdrNameDef);
		cbReader.setSelectedIndex(0);
		
	    bInit.setText("Initialize");

	    bConnect.setText("Connect");
	    
	    bConnect.setText("Connect");

        lblSec.setText("Security Option");

        gbSecure.add(rbDES);
        rbDES.setText("DES");

        gbSecure.add(rb3DES);
        rb3DES.setText("3DES");
        
        bFormat.setText("Format");

        lblCardKey.setText("Card Key");

        lblTKey.setText("Terminal Key");

        bExecMA.setText("Execute MA");

        Msg.setColumns(20);
        Msg.setRows(5);
        jScrollPane1.setViewportView(Msg);

        bClear.setText("Clear");

        bDisconnect.setText("Disconnect");

        lblSelect1.setText("Result");
	    
        bQuit.setText("Quit");

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                                .addGroup(layout.createSequentialGroup()
                                    .addComponent(lblSelect, javax.swing.GroupLayout.PREFERRED_SIZE, 80, javax.swing.GroupLayout.PREFERRED_SIZE)
                                    .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                    .addComponent(cbReader, javax.swing.GroupLayout.PREFERRED_SIZE, 152, javax.swing.GroupLayout.PREFERRED_SIZE))
                                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                                    .addComponent(bConnect, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                    .addComponent(bInit, javax.swing.GroupLayout.DEFAULT_SIZE, 153, Short.MAX_VALUE)))
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(lblSec)
                                .addGap(22, 22, 22)
                                .addComponent(rbDES)
                                .addGap(18, 18, 18)
                                .addComponent(rb3DES)))
                        .addGap(18, 18, 18))
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(lblCardKey)
                            .addComponent(lblTKey))
                        .addGap(19, 19, 19)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(txtTKey, javax.swing.GroupLayout.DEFAULT_SIZE, 152, Short.MAX_VALUE)
                            .addComponent(txtCKey, javax.swing.GroupLayout.DEFAULT_SIZE, 152, Short.MAX_VALUE)
                            .addComponent(bFormat, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.PREFERRED_SIZE, 87, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(bExecMA, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.DEFAULT_SIZE, 152, Short.MAX_VALUE))
                        .addGap(9, 9, 9)))
                .addGap(8, 8, 8)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(bClear, javax.swing.GroupLayout.PREFERRED_SIZE, 82, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(bDisconnect, javax.swing.GroupLayout.DEFAULT_SIZE, 101, Short.MAX_VALUE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(bQuit, javax.swing.GroupLayout.PREFERRED_SIZE, 96, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 299, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(lblSelect1, javax.swing.GroupLayout.PREFERRED_SIZE, 67, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(cbReader, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(lblSelect))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bInit)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bConnect)
                        .addGap(18, 18, 18)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(lblSec)
                            .addComponent(rb3DES)
                            .addComponent(rbDES))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(lblCardKey)
                            .addComponent(txtCKey, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(lblTKey)
                            .addComponent(txtTKey, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bFormat)
                        .addGap(18, 18, 18)
                        .addComponent(bExecMA))
                    .addGroup(layout.createSequentialGroup()
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(lblSelect1)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 213, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(bQuit)
                            .addComponent(bDisconnect)
                            .addComponent(bClear))))
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        
        
        bInit.addActionListener(this);
        bConnect.addActionListener(this);
        bDisconnect.addActionListener(this);
        bClear.addActionListener(this);
        bFormat.addActionListener(this);
        bQuit.addActionListener(this);
        bExecMA.addActionListener(this);
        rbDES.addActionListener(this);
        rb3DES.addActionListener(this);    
        txtCKey.addKeyListener(this);
        txtTKey.addKeyListener(this);
        
        
    }

	public void actionPerformed(ActionEvent e) 
	{
		
		if (bInit==e.getSource())
		{
			//1. Establish context and obtain hContext handle
			retCode = jacs.jSCardEstablishContext(ACSModule.SCARD_SCOPE_USER, 0, 0, hContext);
		    
			if (retCode != ACSModule.SCARD_S_SUCCESS)
		    {
		    
				Msg.append("Calling SCardEstablishContext...FAILED\n");
		      	//displayOut(1, retCode, "");
		      	
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
			
			bConnect.setEnabled(true);
			
			
		} // Init
		
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
		    bFormat.setEnabled(true);
			rbDES.setEnabled(true);
			rb3DES.setEnabled(true);
			txtCKey.setEnabled(true);
			txtTKey.setEnabled(true);
		
		} // Connect
		
		if (bClear == e.getSource())
		{
			Msg.setText("");	
		}
		
		if(bQuit == e.getSource())
		{
			
			this.dispose();
			
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
			txtCKey.setText("");
			txtTKey.setText("");
			initMenu();
		
		} // Disconnect
		
		if (rbDES == e.getSource())
		{
			txtCKey.setText("");
			txtTKey.setText("");
			txtCKey.requestFocus();						
			
		}
		
		if (rb3DES == e.getSource())
		{
			txtCKey.setText("");
			txtTKey.setText("");
			txtCKey.requestFocus();			
			
		}
		
		
		if (bFormat == e.getSource())
		{
				if (rbDES.isSelected())
				{
			
					if (txtCKey.getText().length() < txtMaxLength)
					{
						txtCKey.requestFocus(); 
						return;
			
					} 
			
					if (txtTKey.getText().length() < txtMaxLength)
					{
						txtTKey.requestFocus(); 
						return;
					}
				
				}
	  		
				else
				{
					if (txtCKey.getText().length() < txtMaxLength2)
					{
						txtCKey.requestFocus(); 
						return;
			
					} 
			
					if (txtTKey.getText().length() < txtMaxLength2)
					{
						txtTKey.requestFocus(); 
						return;
					}
				}
				
				submitIC();
				if (retCode != ACSModule.SCARD_S_SUCCESS)
				{
					return;
					
				}
				
			
				// Select FF 02
	        	selectFile((byte)0xFF, (byte)0x02);
	        	
	        	if (retCode != ACSModule.SCARD_S_SUCCESS)
				{
					return;
					
				}
	        	
	        	if (rbDES.isSelected())
	        	{
	        		tmpArray[0] = 0x00;		
	        	}
	        	else
	        	{
	        		tmpArray[0] = 0x02;  
	        	}
	        	tmpArray[1] = 0x00;             // 00    Security option register
				tmpArray[2] = 0x03;             // 00    No of user files
				tmpArray[3] = 0x00;             // 00    Personalization bit

				writeRecord((byte)0x00, (byte)0x00, (byte)0x04, (byte)0x04,tmpArray);

				if (retCode != ACSModule.SCARD_S_SUCCESS)
				{
					return;
					
				}
				
				displayOut(0,0,"FF 02 is updated");
				
				if (retCode != ACSModule.SCARD_S_SUCCESS)
				{
					return;
					
				}
				
				displayOut(0,0,"Account files are enabled");
				
				// Submit Issuer Code to write into FF 03
				submitIC();
				
				if (retCode != ACSModule.SCARD_S_SUCCESS)
				{
					return;
					
				}
				
			//  Select FF 03
				selectFile((byte)0xFF, (byte)0x03);
				if (retCode != ACSModule.SCARD_S_SUCCESS)
				{
					return;
					
				}
				
				if (rbDES.isSelected())
				{
					int indx, tmpInt;
					tmpStr = txtCKey.getText();
					
					for (indx=0; indx<8; indx++)
					{
					    tmpInt = (int)tmpStr.charAt(indx);
					    tmpArray[indx] = (byte) tmpInt;
					}
				
					writeRecord((byte)0x00, (byte)0x02, (byte)0x08, (byte)0x08, tmpArray);
					if (retCode != ACSModule.SCARD_S_SUCCESS)
					{
						return;
						
					}
				//  Record 03 for Terminal key
					tmpStr = txtTKey.getText();
					for (indx=0; indx<8; indx++)
					{
					    tmpInt = (int)tmpStr.charAt(indx);
					    tmpArray[indx] = (byte) tmpInt;
					}
					writeRecord((byte)0x00, (byte)0x03, (byte)0x08, (byte)0x08,tmpArray);
					
					if (retCode != ACSModule.SCARD_S_SUCCESS)
					{
						return;
						
					}
									
				}
				
				else if (rb3DES.isSelected())
				{
					int indx, tmpInt;
					tmpStr = txtCKey.getText();
					
					for (indx=0; indx<8; indx++)
					{
					    tmpInt = (int)tmpStr.charAt(indx);
					    tmpArray[indx] = (byte) tmpInt;
					}
					
					writeRecord((byte)0x00, (byte)0x02, (byte)0x08, (byte)0x08,tmpArray);


					// Record 12 for Right half of Card key
					for (indx=8; indx<16; indx++)
					{
					    tmpInt = (int)tmpStr.charAt(indx);
					    tmpArray[indx-8] = (byte) tmpInt;
					}
					writeRecord((byte)0x00, (byte)0x0C, (byte)0x08, (byte)0x08,tmpArray);
				
					if (retCode != ACSModule.SCARD_S_SUCCESS)
					{
						return;
						
					}
					
					
					//  Record 03 for Terminal key
						tmpStr = txtTKey.getText();
						for (indx=0; indx<8; indx++)
						{
						    tmpInt = (int)tmpStr.charAt(indx);
						    tmpArray[indx] = (byte) tmpInt;
						}
						writeRecord((byte)0x00, (byte)0x03, (byte)0x08, (byte)0x08,tmpArray);
						
						if (retCode != ACSModule.SCARD_S_SUCCESS)
						{
							return;
							
						}
						

						//  Record 13 for Right half of Terminal key					
						for (indx=8; indx<16; indx++)
						{
						    tmpInt = (int)tmpStr.charAt(indx);
						    tmpArray[indx-8] = (byte) tmpInt;
						    
						}
						writeRecord((byte)0x00, (byte)0x0D, (byte)0x08, (byte)0x08,tmpArray);
						if (retCode != ACSModule.SCARD_S_SUCCESS)
						{
							return;
							
						}
						displayOut(0,0,"FF 03 is updated");
						
				}																			
			
		} // Format
		
		if (bExecMA == e.getSource())
		{

			if (rbDES.isSelected())
			{
		
				if (txtCKey.getText().length() < txtMaxLength)
				{
					txtCKey.requestFocus(); 
					return;
		
				} 
		
				if (txtTKey.getText().length() < txtMaxLength)
				{
					txtTKey.requestFocus(); 
					return;
				}
			
			}
  		
			else
			{
				if (txtCKey.getText().length() < txtMaxLength2)
				{
					txtCKey.requestFocus(); 
					return;
		
				} 
		
				if (txtTKey.getText().length() < txtMaxLength2)
				{
					return;
				}
			}
			displayOut(0,0,"Success");
			retCode = StartSession();
			if (retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			// Retrieve Terminal Key from Input Template
			int indx, tmpInt;
			tmpStr = " ";
			tmpStr = txtTKey.getText();
			for(int i=0; i<txtTKey.getText().length(); i++)
				tKey[i] = (byte)((int)tmpStr.charAt(i));
		
			
			//  Encrypt Random No (CRnd) with Terminal Key (tKey)
			//    tmpArray will hold the 8-byte Enrypted number
			for (indx=0; indx<8; indx++)
				tmpArray[indx] = CRnd[indx];
			
			if (rbDES.isSelected()) 
			{
				for (indx=0; indx<8; indx++)
				{
				    tmpInt = (int)tmpStr.charAt(indx);
				    tKey[indx] = (byte) tmpInt;
				}
				
				DES(tmpArray, tKey);
					
			}
			else
			{
				for (indx=0; indx<16; indx++)
				{
				    tmpInt = (int)tmpStr.charAt(indx);
				    tKey[indx] = (byte) tmpInt;
				}
				
				TripleDES(tmpArray,tKey);
			}
			
			//  Issue Authenticate command using 8-byte Encrypted No (tmpArray)
			//    and Random Terminal number (TRnd)
			for (indx=0; indx<8; indx++)
				tmpArray[indx + 8] = TRnd[indx];

			Authenticate(tmpArray);
			if (retCode != ACSModule.SCARD_S_SUCCESS)
				return;
		
			// Get 8-byte result of card-side authentication
			// and save to tmpResult
            retCode = GetResponse();
            
        	for (indx=0; indx<8; indx++)
                tmpResult[indx] = RecvBuff[indx];
        	
        		//displayOut(0,0,"RecvBuff :" + hexBytesToString(RecvBuff,0,8));
        	
        	
        	if (retCode != ACSModule.SCARD_S_SUCCESS)
				return;
        	/*  Terminal-side authentication process
            '  Retrieve Card Key from Input Template */
			tmpStr = "";
			tmpStr = txtCKey.getText();
			for (indx=0; indx<8; indx++)
			{
			    tmpInt = (int)tmpStr.charAt(indx);
			    cKey[indx] = (byte) tmpInt;
			}
			
		//  Compute for Session Key
			if (rbDES.isSelected()) 
			{
				/*  for single DES
				' prepare SessionKey
				' SessionKey = DES (DES(RNDc, KC) XOR RNDt, KT) */

				// calculate DES(cRnd,cKey)
				for (indx=0; indx<8; indx++)
					tmpArray[indx] = CRnd[indx];

				DES(tmpArray, cKey);
				
				// XOR the result with tRnd
				for (indx=0; indx<8; indx++)
					tmpArray[indx] = tmpArray[indx] ^= TRnd[indx];
            
				// DES the result with tKey
				DES(tmpArray,tKey);

				// temp now holds the SessionKey
				for (indx=0; indx<8; indx++)
					SessionKey[indx] = tmpArray[indx];
					
			
			}
			else
			{

				/*  for triple DES
				' prepare SessionKey
				' Left half SessionKey =  3DES (3DES (CRnd, cKey), tKey)
				' Right half SessionKey = 3DES (TRnd, REV (tKey))
				' tmpArray = 3DES (CRnd, cKey) */

				/*for (indx=0; indx<8; indx++)
					tmpArray[indx] = CRnd[indx];
				//displayOut(0,0,"Rand Array :" + hexBytesToString(tmpArray,0,8));
				
			TripleDES(tmpArray, cKey);
				// tmpArray = 3DES (tmpArray, tKey)
			
			TripleDES(tmpArray,tKey);*/
				
				// calculate DES(cRnd,cKey)
				for (indx=0; indx<8; indx++)
					tmpArray[indx] = CRnd[indx];

				TripleDES(tmpArray, cKey);
				// XOR the result with tRnd
				for (indx=0; indx<8; indx++)
					tmpArray[indx] = tmpArray[indx] ^= TRnd[indx];
            
				// 3DES the result with tKey
				
				TripleDES(tmpArray,tKey);
				

				
				
				// tmpArray holds the left half of SessionKey
				for (indx=0; indx<8;indx++)
					SessionKey[indx] = tmpArray[indx];
				//displayOut(0,0,"Session :" + hexBytesToString(SessionKey,0,8));

				/* compute ReverseKey of tKey
				' just swap its left side with right side
				' ReverseKey = right half of tKey + left half of tKey */
				for (indx=0;indx<8; indx++)
					ReverseKey[indx] = tKey[8 + indx];
				//displayOut(0,0,"Reverse :" + hexBytesToString(ReverseKey,0,8));
           
				for (indx=0;indx<8;indx++)
					ReverseKey[8 + indx] = tKey[indx];


				// compute tmpArray = 3DES (TRnd, ReverseKey)
				for (indx=0; indx<8; indx++)
					tmpArray[indx] = TRnd[indx];


				TripleDES(tmpArray, ReverseKey);

				// tmpArray holds the right half of SessionKey
				for (indx=0; indx<8; indx++)
					SessionKey[indx + 8] = tmpArray[indx];	
			}
			
			// compute DES (TRnd, SessionKey)
			for (indx=0; indx<8;indx++)
				tmpArray[indx] = TRnd[indx];
			
	
			
			if (rbDES.isSelected()) 
			{ 
				DES(tmpArray,SessionKey);
			}
			else 
			{   
				TripleDES(tmpArray,SessionKey);
			}
			
		//	displayOut(0,0,"Session KEy :" + hexBytesToString(SessionKey,0,8));

			// Compare Card-side and Terminal-side authentication results		
			
			//displayOut(0,0,"Result :" + hexBytesToString(tmpResult,0,8));
			//displayOut(0,0,"Array :" + hexBytesToString(tmpArray, 0, 8));
			
			if (rbDES.isSelected())
			{
			for (indx=0; indx<8;indx++)
				if (tmpResult[indx] != tmpArray[indx]) 
				{				
					displayOut(0,0,"Mutual Authentication failed.");
					return;
				}
			
				
			displayOut(0,0,"Mutual Authentication is successful.");
			}
			
			if (rb3DES.isSelected())
			{
			for (indx=0; indx<8;indx++)
				if (RecvBuff[indx] != tmpResult[indx]) 
				{				
					displayOut(0,0,"Mutual Authentication failed.");
					return;
				}
			
				
			displayOut(0,0,"Mutual Authentication is successful.");
			}
			
		}
				
	}
    
	private String hexBytesToString(byte[] bytes, int start, int length){
		
		String strHex, tmpStr = "";		
		for (int indx=0 ; indx < length ; indx++ ){			
			
			//Byte to Hex conversion
			strHex = Integer.toHexString(((Byte)bytes[indx+start]).intValue() & 0xFF); 
			
			//For single character hex
			if (strHex.length() == 1) 
				strHex = "0" + strHex;
			
			tmpStr += " " + strHex;  			
			
			//new line every 8 bytes
			if ( ((indx+1) % 8 == 0) && ( (indx+1) < length ) )  
				tmpStr += "\n ";							

		}		
		return tmpStr.toUpperCase();
	}
	
	private int GetResponse()
	{
		 clearBuffers();
		 SendBuff[0] = (byte)0x80;        // CLA
		 SendBuff[1] = (byte)0xC0;        // INS
		 SendBuff[2] = (byte)0x00;        // P1
		 SendBuff[3] = (byte)0x00;         // P2
		 SendBuff[4] = (byte)0x08;        // P3
		 SendLen = 5;
		 RecvLen[0] = 10;
		 		
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
		  for(int i =0; i<2; i++){
			  tmpHex = Integer.toHexString(((Byte)RecvBuff[i+(SendBuff[4] & 0xFF)]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
		  }

		  if(!tmpStr.trim().equals("90 00"))
		  {
			  displayOut(4, 0, "GET RESPONSE command failed.");
			  displayOut(0, 0, "Return string is invalid. Value: " + tmpStr);
			  return INVALID_SW1SW2;
		  }
		  
		  return retCode;
		 
	}
	
	private void Authenticate(byte[] DataIn) 
	{
		 clearBuffers();
		 
		  
		 SendBuff[0] = (byte)0x80;        // CLA
		 SendBuff[1] = (byte)0x82;        // INS
		 SendBuff[2] = (byte)0x00;        // P1
		 SendBuff[3] = (byte)0x00;         // P2
		 SendBuff[4] = (byte)0x10;        // P3
		 SendLen = 5+(SendBuff[4] & 0xFF);
		 
		 int indx;
		 
		 
		 for (indx=0; indx<16; indx++)
			SendBuff[indx+5] = DataIn[indx];
		 
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
			  return;									 
		
	}
	
	public void DES(byte Data[], byte key[])
	{
		byte[] keyTemp = new byte[8];
		for(int i =0; i<8; i++)
		{
			keyTemp[i] = key[i];
		}
        try {
            
            DESKeySpec desKeySpec = new DESKeySpec(keyTemp);
            SecretKeyFactory keyFactory = SecretKeyFactory.getInstance(algorithm);
            SecretKey secretKey = keyFactory.generateSecret(desKeySpec);                  
           
            Cipher encryptCipher = Cipher.getInstance(algorithm);
            encryptCipher.init(Cipher.ENCRYPT_MODE, secretKey);
           
            byte encryptedContents[] = process(Data, encryptCipher);
    
            for(int i=0;i<8;i++)
            {
            	Data[i] = encryptedContents[i];
        
            }
    		//displayOut(0,0,"Data :" + hexBytesToString(Data,0,8));
           
        } catch (Exception e) {
            e.printStackTrace();
        }
        
	}
	
	
	/*public void DES2(byte Data[], byte key[])
	{
		byte[] keyTemp = new byte[8];
		  for(int i =0; i<8; i++)
  		{
  			keyTemp[i] = key[i+8];
  		}
		  try
		  {
  		
          DESKeySpec desKeySpec = new DESKeySpec(keyTemp);
          SecretKeyFactory keyFactory = SecretKeyFactory.getInstance(algorithm);
          SecretKey secretKey2 = keyFactory.generateSecret(desKeySpec);                  
         
          Cipher encryptCipher = Cipher.getInstance(algorithm);
          encryptCipher.init(Cipher.DECRYPT_MODE, secretKey2);
          
          byte decryptedContents[] = process(Data, encryptCipher);
      
          for(int i=0;i<8;i++)
          {
          	Data[i] = decryptedContents[i];
      
          }
      	displayOut(0,0,"Data2 :" + hexBytesToString(Data,0,8));
          
           
        } catch (Exception e) {
            e.printStackTrace();
        }
        
	}*/
	
	
	
	/*Triple DES Algo
	 * Data: 8 bytes
	 * Key:  16 bytes
	 *ENCRYPT:
	 * 1. DES(Data,Key,ENCRYPT)
	 * 2. DES(Data,Key[8],DECRYPT)
	 * 3. DES(Data,Key,ENCRYPT)
	 * 
	 * DECRYPT:
	 * 1. DES(Data,Key,DECRYPT)
	 * 2. DES(Data,Key[8],ENCRYPT)
	 * 3. DES(Data,Key,DECRYPT)
	 * */
	

	public void TripleDES(byte Data[], byte key[])
	{
		byte[] keyTemp = new byte[16];		
		
		
        try {
        	
        	for(int i =0; i<8; i++)
    		{
    			keyTemp[i] = key[i];
    		}
        	
        	//Encrypt        	           
            DESKeySpec desKeySpec = new DESKeySpec(keyTemp);
            SecretKeyFactory keyFactory = SecretKeyFactory.getInstance(algorithm);
            SecretKey secretKey = keyFactory.generateSecret(desKeySpec);                  
           
            Cipher encryptCipher = Cipher.getInstance(algorithm);
            encryptCipher.init(Cipher.ENCRYPT_MODE, secretKey);
           
            byte encryptedContents[] = process(Data, encryptCipher);
            //System.out.println(new String(encryptedContents));
    
            for(int i=0;i<8;i++)
            {
            	Data[i] = encryptedContents[i];
        
            }
  
            
        	//End Encrypt 
            
        	//Decrypt
            for(int i =0; i<8; i++)
    		{
    			keyTemp[i+8] = key[i];
    		}
    		
            DESKeySpec desKeySpec2 = new DESKeySpec(keyTemp);
            SecretKeyFactory keyFactory2 = SecretKeyFactory.getInstance(algorithm);
            SecretKey secretKey2 = keyFactory2.generateSecret(desKeySpec2);                  
           
            Cipher encryptCipher2 = Cipher.getInstance(algorithm);
            encryptCipher2.init(Cipher.DECRYPT_MODE, secretKey2);
            
            byte decryptedContents[] = process(encryptedContents, encryptCipher2);
        
            for(int i=0;i<8;i++)
            {
            	Data[i] = decryptedContents[i];
        
            }
         
            
        	//End Decrypt
            
        	//Encrypt
            for(int i =0; i<8; i++)
    		{
    			keyTemp[i] = key[i];
    		}
            
            DESKeySpec desKeySpec3 = new DESKeySpec(keyTemp);
            SecretKeyFactory keyFactory3 = SecretKeyFactory.getInstance(algorithm);
            SecretKey secretKey3 = keyFactory3.generateSecret(desKeySpec3);                  
           
            Cipher encryptCipher3 = Cipher.getInstance(algorithm);
            encryptCipher3.init(Cipher.ENCRYPT_MODE, secretKey3);
           
            byte encryptedContents2[] = process(Data, encryptCipher3);
            //System.out.println(new String(encryptedContents2));
    
            for(int i=0;i<8;i++)
            {
            	Data[i] = encryptedContents2[i];
        
            }
            
          	
          	
        } catch (Exception e) {
            e.printStackTrace();
        }
                                   
	}
	
	 private static byte[] process(byte processMe[], Cipher cipher) throws Exception 
	 {
	        // Create the input stream to be used for encryption
	        ByteArrayInputStream in = new ByteArrayInputStream(processMe);
	       
	        // Now actually encrypt the data and put it into a
	        // ByteArrayOutputStream so we can pull it out easily.
	        CipherInputStream processStream = new CipherInputStream(in, cipher);
	        ByteArrayOutputStream resultStream = new ByteArrayOutputStream();
	        int whatWasRead = 0;
	        while ((whatWasRead = processStream.read()) != -1) {
	            resultStream.write(whatWasRead);
	        }
	       
	        return resultStream.toByteArray();
	 }
	
	/*
	
	
	// MAC as defined in ACOS manual
	// receives 8-byte Key and 16-byte Data
	// result is stored in Data
	public void mac(byte[] Data, byte[]  key)
	{

		DES(Data[0],key[0]);
		for (i=0; i<8; i++)
			Data[i] = Data[i] ^= Data [i + 8];
		
		DES(Data[0], key[0]);

	}
	
	// Triple MAC as defined in ACOS manual
	// receives 16-byte Key and 16-byte Data
	// result is stored in Data
	public void TripleMAC(byte[] Data, byte[] key)		
	{
		int i;
		
		TripleDES(Data[0], key[0]);

		for (i=0; i<8; i++)
			Data[i] = Data[i] ^= Data[i + 8];

		TripleDES(Data[0], key[0]);
	} 
		
	*/
	
	private int StartSession() 
	{
		
		 clearBuffers();
		 SendBuff[0] = (byte)0x80;        // CLA
		 SendBuff[1] = (byte)0x84;        // INS
		 SendBuff[2] = (byte)0x00;        // P1
		 SendBuff[3] = (byte)0x00;         // P2
		 SendBuff[4] = (byte)0x08;        // P3
		 SendLen = 5;
		 RecvLen[0] = 10;
		  
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
		  for(int i =0; i<2; i++){
			  tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
		  }
		  
		  if(tmpStr.trim().equals("90 00"))
		  {
		    displayOut(0, 0, "Return string is invalid. Value: " + tmpStr);
		    return INVALID_SW1SW2;
		  }
		  
		  int indx;
		  for (indx=0; indx<8; indx++)
				CRnd[indx] = RecvBuff[indx];
		  
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
  		
	public int submitIC()
	{
		
		  clearBuffers();
		  SendBuff[0] = (byte)0x80;        // CLA
		  SendBuff[1] = (byte)0x20;        // INS
		  SendBuff[2] = (byte)0x07;        // P1
		  SendBuff[3] = (byte)0x00;         // P2
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
				
				tmpStr += " " + tmpHex;  
			  
		  }

		  if (tmpStr.indexOf("90 00") < 0)
		  {
			
			  displayOut(0, 0, "Return string is invalid. Value: " + tmpStr);
			  return INVALID_SW1SW2;
		
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
			
  	  	
  			if(rbDES.isSelected())
  			{ 
  		
  				if   (((JTextField)ke.getSource()).getText().length() >= 8 ) {
		
  				ke.setKeyChar(empty);	
  				
  				return;
  				}			
  			}
  		
  			if(rb3DES.isSelected())
  			{ 
  		
  				if   (((JTextField)ke.getSource()).getText().length() >= 16 ) 
  				{
		
  					ke.setKeyChar(empty);
  					return;
  				}			
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
		
		Msg.setText("");
		displayOut(0, 0, "Program Ready");
		cbReader.removeAllItems();
		bInit.setEnabled(true);
		bConnect.setEnabled(false);		
		bFormat.setEnabled(false);
		bDisconnect.setEnabled(true);		
		rbDES.setEnabled(false);
		rb3DES.setEnabled(false);
		txtCKey.setEnabled(false);
		txtTKey.setEnabled(false);
		rbDES.setSelected(true);
				
	}
    
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new ACOS3MutualAuthentication().setVisible(true);
            }
        });
    }



}
