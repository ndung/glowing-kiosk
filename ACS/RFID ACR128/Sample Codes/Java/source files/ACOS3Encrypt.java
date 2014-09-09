/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              ACOS3Encrypt.java

  Description:       This sample program outlines the steps on how to
                     use the encryption options in ACOS card using
                     the PC/SC platform.
                 
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

public class ACOS3Encrypt extends JFrame implements ActionListener, KeyListener{

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
	String tmpStr;
	byte tmpByte;
	
	
	static JacspcscLoader jacs = new JacspcscLoader();
	
	private JTextArea Msg;
	private JButton bClear;
	private JButton bConnect;
	private JButton bDisconnect;
	private JButton bInit;
	private JButton bFormat;
	private JButton bSetVal;
	private JButton bSubmit;
	private JButton bQuit;
	private JComboBox cbCode;
	private JComboBox cbReader;
	private ButtonGroup gbEnc;
	private ButtonGroup gbSecure;
	private JScrollPane jScrollPane1;
	private JTextField txtCKey;
	private JTextField txtTKey;
	private JTextField txtSetVal;
	private JLabel lblCardKey;
	private JLabel lblCodeSub;
	private JLabel lblCodeVal;
	private JLabel lblOption;
	private JLabel lblSec;
	private JLabel lblSelect;
	private JLabel lblSelect1;
	private JLabel lblSetVal;
	private JLabel lblTKey;
	private JRadioButton rb3DES;
	private JRadioButton rbDES;
	private JRadioButton rbEnc;
	private JRadioButton rbNotEnc;
	private int txtMaxLength = 8;
	private int txtMaxLength2 = 16;
    

    public ACOS3Encrypt() {
    	
    	this.setTitle("ACOS 3 Encrypt");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

    	setSize(620,530);
        gbEnc = new ButtonGroup();
        gbSecure = new ButtonGroup();
        cbReader = new JComboBox();
        bInit = new JButton();
        bConnect = new JButton();
        bQuit = new JButton();
        lblSelect = new JLabel();
        lblOption = new JLabel();
        rbNotEnc = new JRadioButton();
        rbEnc = new JRadioButton();
        lblSec = new JLabel();
        rbDES = new JRadioButton();
        rb3DES = new JRadioButton();
        lblCardKey = new JLabel();
        lblTKey = new JLabel();
        txtCKey = new JTextField();
        txtTKey = new JTextField();
        bFormat = new JButton();
        lblCodeSub = new JLabel();
        lblCodeVal = new JLabel();
        lblSetVal = new JLabel();
        cbCode = new JComboBox();
        txtSetVal = new JTextField();
        bSetVal = new JButton();
        bSubmit = new JButton();
        lblSelect1 = new JLabel();
        jScrollPane1 = new JScrollPane();
        Msg = new JTextArea();
        bClear = new JButton();
        bDisconnect = new JButton();
        
        lblSelect.setText("Select Reader");

        String[] rdrNameDef = {"Please select reader                   "};	
    	cbReader = new JComboBox(rdrNameDef);
    	cbReader.setSelectedIndex(0);
    	
        bInit.setText("Initialize");

        bConnect.setText("Connect");
        
        bFormat.setText("Format");
        
        bSubmit.setText("Submit");
        
        bSetVal.setText("Set Value");
     
        bClear.setText("Clear");

        bDisconnect.setText("Disconnect");
     
        lblSelect.setText("Select Reader");

        lblOption.setText("Encrypt Option");
        
        lblSelect1.setText("Result");
        
        Msg.setColumns(20);
        Msg.setRows(5);
        jScrollPane1.setViewportView(Msg);

        gbEnc.add(rbNotEnc);
        rbNotEnc.setText("Not Encrypted");

        gbEnc.add(rbEnc);
        rbEnc.setText("Encrypted");

        lblSec.setText("Security Option");
        
        lblCodeSub.setText("Code Submission");

        lblCodeVal.setText("Code Value");

        lblSetVal.setText("Set Value");

        cbCode.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "PIN Code", "Application Code 1", "Application Code 2", "Application Code 3", "Application Code 4", "Application Code 5" }));

        gbSecure.add(rbDES);
        rbDES.setText("DES");  

        gbSecure.add(rb3DES);
        rb3DES.setText("3DES");

        lblCardKey.setText("Card Key");

        lblTKey.setText("Terminal Key");
        bQuit.setText("Quit");

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                    .addGroup(layout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(bFormat, javax.swing.GroupLayout.PREFERRED_SIZE, 99, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                        .addGroup(layout.createSequentialGroup()
                            .addGap(3, 3, 3)
                            .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                                    .addComponent(bConnect, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                    .addComponent(bInit, javax.swing.GroupLayout.DEFAULT_SIZE, 153, Short.MAX_VALUE))
                                .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                                    .addComponent(lblSelect, javax.swing.GroupLayout.PREFERRED_SIZE, 80, javax.swing.GroupLayout.PREFERRED_SIZE)
                                    .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                    .addComponent(cbReader, javax.swing.GroupLayout.PREFERRED_SIZE, 152, javax.swing.GroupLayout.PREFERRED_SIZE))
                                .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                                    .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                        .addGroup(layout.createSequentialGroup()
                                            .addGap(20, 20, 20)
                                            .addComponent(lblOption))
                                        .addGroup(layout.createSequentialGroup()
                                            .addGap(10, 10, 10)
                                            .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                                .addComponent(rbEnc)
                                                .addComponent(rbNotEnc))))
                                    .addGap(58, 58, 58)
                                    .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                        .addComponent(rb3DES)
                                        .addComponent(rbDES)
                                        .addComponent(lblSec)))))
                        .addGroup(layout.createSequentialGroup()
                            .addGap(22, 22, 22)
                            .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                .addGroup(layout.createSequentialGroup()
                                    .addComponent(lblTKey)
                                    .addGap(18, 18, 18)
                                    .addComponent(txtTKey, javax.swing.GroupLayout.DEFAULT_SIZE, 143, Short.MAX_VALUE))
                                .addGroup(layout.createSequentialGroup()
                                    .addComponent(lblCardKey)
                                    .addGap(36, 36, 36)
                                    .addComponent(txtCKey, javax.swing.GroupLayout.DEFAULT_SIZE, 142, Short.MAX_VALUE))
                                .addComponent(lblCodeSub)
                                .addGroup(layout.createSequentialGroup()
                                    .addComponent(lblCodeVal)
                                    .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                    .addComponent(cbCode, 0, 158, Short.MAX_VALUE))
                                .addGroup(layout.createSequentialGroup()
                                    .addComponent(lblSetVal)
                                    .addGap(18, 18, 18)
                                    .addComponent(txtSetVal, javax.swing.GroupLayout.DEFAULT_SIZE, 159, Short.MAX_VALUE))
                                .addGroup(layout.createSequentialGroup()
                                    .addComponent(bSetVal, javax.swing.GroupLayout.PREFERRED_SIZE, 111, javax.swing.GroupLayout.PREFERRED_SIZE)
                                    .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                    .addComponent(bSubmit, javax.swing.GroupLayout.DEFAULT_SIZE, 101, Short.MAX_VALUE))))))
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addGroup(layout.createSequentialGroup()
                        .addGap(20, 20, 20)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(lblSelect1, javax.swing.GroupLayout.PREFERRED_SIZE, 67, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 326, javax.swing.GroupLayout.PREFERRED_SIZE)))
                    .addGroup(layout.createSequentialGroup()
                        .addGap(37, 37, 37)
                        .addComponent(bClear, javax.swing.GroupLayout.PREFERRED_SIZE, 74, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(18, 18, 18)
                        .addComponent(bDisconnect)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(bQuit, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)))
                .addGap(20, 20, 20))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addGap(19, 19, 19)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(cbReader, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(lblSelect))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bInit)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bConnect)
                        .addGap(32, 32, 32)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(lblOption)
                            .addComponent(lblSec))
                        .addGap(10, 10, 10)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(rbNotEnc)
                            .addComponent(rbDES))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(rbEnc)
                            .addComponent(rb3DES))
                        .addGap(18, 18, 18)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(lblCardKey)
                            .addComponent(txtTKey, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(lblTKey)
                            .addComponent(txtCKey, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bFormat)
                        .addGap(18, 18, 18)
                        .addComponent(lblCodeSub)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(lblCodeVal)
                            .addComponent(cbCode, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(lblSetVal)
                            .addComponent(txtSetVal, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addGap(18, 18, 18)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(bSetVal)
                            .addComponent(bSubmit)))
                    .addGroup(layout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(lblSelect1)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 357, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(18, 18, 18)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(bClear)
                            .addComponent(bDisconnect)
                            .addComponent(bQuit))))
                .addContainerGap(29, Short.MAX_VALUE))
        );
        
        bInit.addActionListener(this);
        bConnect.addActionListener(this);
        bDisconnect.addActionListener(this);
        bClear.addActionListener(this);
        bSetVal.addActionListener(this);
        bFormat.addActionListener(this);
        bQuit.addActionListener(this);
        bSubmit.addActionListener(this);
        rbEnc.addActionListener(this);
        rbNotEnc.addActionListener(this);
        txtCKey.addKeyListener(this);
        txtTKey.addKeyListener(this);
        txtSetVal.addKeyListener(this);
        rbDES.addActionListener(this);
        rb3DES.addActionListener(this);
        
        
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
		    bSetVal.setEnabled(true);
			bSubmit.setEnabled(true);
			bFormat.setEnabled(true);
			rbEnc.setEnabled(true);
			rbNotEnc.setEnabled(true);
			rbDES.setEnabled(true);
			rb3DES.setEnabled(true);
			txtCKey.setEnabled(true);
			txtTKey.setEnabled(true);
			txtSetVal.setEnabled(true);
			cbCode.setEnabled(true);
			rbNotEnc.setSelected(true);
			rbDES.setSelected(true);
			rbDES.setEnabled(false);
			rb3DES.setEnabled(false);
					    		
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
			txtCKey.setText("");
			txtTKey.setText("");
			txtSetVal.setText("");			
			bInit.setEnabled(true);		
			initMenu();		
		
		} // Disconnect
		
		if(bQuit == e.getSource())
		{
			
			this.dispose();
			
		}
		
		if (rbNotEnc == e.getSource())
		{
			rbDES.setEnabled(false);
			rb3DES.setEnabled(false);		
			txtCKey.requestFocus();	
			txtCKey.setText("");
			txtTKey.setText("");
			txtSetVal.setText("");
		
		}			
		
		if (rbEnc == e.getSource())
		{
			rbDES.setEnabled(true);
			rb3DES.setEnabled(true);
			rbDES.setSelected(true);
			txtCKey.setText("");
			txtTKey.setText("");
			txtCKey.requestFocus();
			txtSetVal.setText("");
		}
		
		if (rbDES == e.getSource())
		{
			rbEnc.setSelected(true);			
			txtCKey.setText("");
			txtTKey.setText("");
			txtCKey.requestFocus();				
			txtSetVal.setText("");
			
		}
		
		if (rb3DES == e.getSource())
		{
			rbEnc.setSelected(true);
			txtCKey.setText("");
			txtTKey.setText("");
			txtCKey.requestFocus();		
			txtSetVal.setText("");
			
		}
		
		if (bFormat == e.getSource())
		{	
			if(!checkACOS())
			{
				
				displayOut(0, 0, "Please insert an ACOS card.");
				return;
				
			}
			
			displayOut(0, 0, "ACOS card is detected.");
				
			if (rbEnc.isSelected())
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
	  		}
			
			if (rbNotEnc.isSelected())
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
        	
        	if (rbEnc.isSelected())
        	{
        		tmpArray[0] = 0x00;		
        	}
        	else
        	{
        		tmpArray[0] = 0x02;  
        	}
        	
        	if (rbNotEnc.isSelected())
        	{
        		tmpArray[1] = 0x00;     
        		        		
        	}
        	else
			{  
				tmpArray[1] = 0x7E;        // Encryption on all codes, except IC, enabled		
			}	
        	
        	tmpArray[2] = 0x03;			  // 00    No of user files
			tmpArray[3] = 0x00;           // 00    Personalization bit
			
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
			
			displayOut(0,0,"Account files are enabled.");
			
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
			
			if (rb3DES.isSelected())
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
			
			
		  }// format
		
		if (bSetVal == e.getSource())
		{
			if (txtSetVal.getText().length() < txtMaxLength)
			{
				txtSetVal.requestFocus(); 
				return;

			} 							

			submitIC();
			
			if (retCode != ACSModule.SCARD_S_SUCCESS)
			{
				return;
				
			}
			 selectFile((byte)0xFF, (byte)0x03);
			 if (retCode != ACSModule.SCARD_S_SUCCESS)
			 {
				 return;
					
			 }
			 
			 int indx, tmpInt;
			 tmpStr = txtSetVal.getText();
			 for (indx=0; indx<8; indx++)
			 {
				   	tmpInt = (int)tmpStr.charAt(indx);
				   	tmpArray[indx] = (byte) tmpInt;
			 }
			 
			 switch (cbCode.getSelectedIndex())
			 {
			 case 0:
					tmpByte = (byte)0x01;
					tmpStr = "PIN Code";
					break;
			 
			 case 1:
					tmpByte = (byte)0x05;
					tmpStr = "Application Code 1";
					break;
			 
			 case 2:
				 	tmpByte = (byte)0x06;
					tmpStr = "Application Code 2";
					break;
			 
			 case 3 :
					tmpByte = (byte)0x07;
					tmpStr = "Application Code 3";
					break;
			 
			 case 4 :
					tmpByte = (byte)0x08;
					tmpStr = "Application Code 4";
					break;
			 
			 case 5:
					tmpByte = (byte)0x09;
					tmpStr = "Application Code 5";
					return;
				 			
			 }
			 
			 // Write to corresponding record in FF 03
			 writeRecord((byte)0x00, tmpByte, (byte)0x08, (byte)0x08, tmpArray);
			 		
			 if (retCode != ACSModule.SCARD_S_SUCCESS)
			 {
				return;				
			 }
			 displayOut(0,0,tmpStr + " submitted successfully.");
			 		
		} // Set Value
		
		if (bSubmit == e.getSource())
		{
			
			if (rbEnc.isSelected())
			{
				GetSessionKey();
				 if (retCode != ACSModule.SCARD_S_SUCCESS)
				 {
					return;				
				 }
				 
				 int indx, tmpInt;
				 tmpStr = "";
				 tmpStr = txtSetVal.getText();
				 for (indx=0; indx<8; indx++)
				 {
					   	tmpInt = (int)tmpStr.charAt(indx);
					   	tmpArray[indx] = (byte) tmpInt;
				 }
				 
				 if (rbDES.isSelected())
				 {
					 DES(tmpArray, SessionKey);
				 }
				 else
				 {
					 TripleDES(tmpArray, SessionKey);
				 }											
			}
			
			switch (cbCode.getSelectedIndex())
			{
			case 0:
				tmpByte = (byte)0x06;
				tmpStr = "PIN Code";
				break;
			case 1:
				tmpByte = (byte)0x01;
				tmpStr = "Application Code 1";
				break;
			case 2:
				tmpByte = (byte)0x02;
				tmpStr = "Application Code 2";
				break;
			case 3 :
				tmpByte = (byte)0x03;
				tmpStr = "Application Code 3";
				break;
			case 4 :
				tmpByte = (byte)0x04;
				tmpStr = "Application Code 4";
				break;
			case 5:
				tmpByte = (byte)0x05;
				tmpStr = "Application Code 5";
				return;
			}
			 SubmitCode(tmpByte, tmpArray);
			 
			 if (retCode != ACSModule.SCARD_S_SUCCESS)
			 {
				return;				
			 }
			 displayOut(0,0,tmpStr + " Submitted successfully.");
			 			 
		} // Submit
				
	}
    
	public boolean checkACOS()
	{
		String tmpStr="", tmpHex="";
		//reconnect reader
		if(connActive)
		{	
			retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
			connActive = false;
		}
		
		String rdrcon = (String)cbReader.getSelectedItem();  	      	      	
	    
	    retCode = jacs.jSCardConnect(hContext, 
	    							rdrcon, 
	    							ACSModule.SCARD_SHARE_EXCLUSIVE,
	    							ACSModule.SCARD_PROTOCOL_T1 | ACSModule.SCARD_PROTOCOL_T0,
	      							hCard, 
	      							PrefProtocols);
	    
	    if(retCode != ACSModule.SCARD_S_SUCCESS)
	    {
	    	displayOut(1, retCode, "");
	    	connActive = false;
	    	return false;
	    }
	    
	    connActive = true;
	    
	    //check for file FF 00
	    retCode = selectFile((byte)0xFF, (byte)0x00);
	    if(retCode != ACSModule.SCARD_S_SUCCESS)
	    	return false;
	    
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
	    	displayOut(0, 0, "Return string is invalid. Value: " + tmpStr);
	    	return false;
	    }
	    
	    //check for file FF 01
	    retCode = selectFile((byte)0xFF, (byte)0x01);
	    if(retCode != ACSModule.SCARD_S_SUCCESS)
	    	return false;
	    
	    tmpStr="";
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
	    	displayOut(0, 0, "Return string is invalid. Value: " + tmpStr);
	    	return false;
	    }
		
	    //check for file FF 02
	    retCode = selectFile((byte)0xFF, (byte)0x02);
	    if(retCode != ACSModule.SCARD_S_SUCCESS)
	    	return false;
	    
	    tmpStr="";
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
	    	displayOut(0, 0, "Return string is invalid. Value: " + tmpStr);
	    	return false;
	    }
	    
		return true;
		
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
    		
           
        } catch (Exception e) {
            e.printStackTrace();
        }
        
	}
	

	 public static void TripleDES(byte Data[], byte key[])
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
	
	
	private void SubmitCode(byte CodeType, byte[] DataIn)
	{
		 int indx; 
			
		  clearBuffers();
		  SendBuff[0] = (byte)0x80;        	// CLA
		  SendBuff[1] = (byte)0x20;        	// INS
		  SendBuff[2] = CodeType;        	// P1
		  SendBuff[3] = (byte)0x00;         // P2
		  SendBuff[4] = (byte)0x08;        	// P3
		  SendLen = 5+(SendBuff[4] & 0xFF);
		  
			 
		  for(indx=0; indx<8; indx++)
			SendBuff[5+indx] = DataIn[indx];		  		 
		
		  //SendLen = 5;	
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
	private void GetSessionKey()
	{
		int indx, tmpInt;
		
		tmpStr = txtCKey.getText();
		for (indx=0; indx<8; indx++)
		{
		    tmpInt = (int)tmpStr.charAt(indx);
		    cKey[indx] = (byte) tmpInt;
		}
		
		tmpStr = txtTKey.getText();
		for (indx=0; indx<8; indx++)
		{
		    tmpInt = (int)tmpStr.charAt(indx);
		    tKey[indx] = (byte) tmpInt;
		}
		
		StartSession();
		  
		if (retCode != ACSModule.SCARD_S_SUCCESS)
			return;
		

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
		
		tmpStr = "";
		tmpStr = txtCKey.getText();
		for (indx=0; indx<8; indx++)
		{
		    tmpInt = (int)tmpStr.charAt(indx);
		    cKey[indx] = (byte) tmpInt;
		}
		
		
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

			/* compute ReverseKey of tKey
			' just swap its left side with right side
			' ReverseKey = right half of tKey + left half of tKey */
			for (indx=0;indx<8; indx++)
				ReverseKey[indx] = tKey[8 + indx];
       
			
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
	
	private void StartSession() 
	{
		
		 clearBuffers();
		 SendBuff[0] = (byte)0x80;        // CLA
		 SendBuff[1] = (byte)0x84;        // INS
		 SendBuff[2] = (byte)0x00;        // P1
		 SendBuff[3] = (byte)0x00;         // P2
		 SendBuff[4] = (byte)0x08;        // P3
		 SendLen = 5;
		  
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
		  
		  int indx;
		  for (indx=0; indx<8; indx++)
				CRnd[indx] = RecvBuff[indx];
		 
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
  		
	
	// MAC as defined in ACOS manual
	// receives 8-byte Key and 16-byte Data
	// result is stored in Data
	public void mac(byte[] Data, byte[]  key)
	{
		int i;   

		DES(Data,key);
		for (i=0; i<8; i++)
			Data[i] = Data[i] ^= Data [i + 8];
		
		DES(Data, key);

	}
	
	// Triple MAC as defined in ACOS manual
	// receives 16-byte Key and 16-byte Data
	// result is stored in Data
	public void TripleMAC(byte[] Data, byte[] key)		
	{
		int i;
		
		TripleDES(Data, key);

		for (i=0; i<8; i++)
			Data[i] = Data[i] ^= Data[i + 8];

		TripleDES(Data, key);
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
			
  		
  		if (rbEnc.isSelected())
  		{
  		
  			if(rbDES.isSelected())
  			{ 
  		
  				if   (((JTextField)ke.getSource()).getText().length() >= 8 ) {
		
  				ke.setKeyChar(empty);	
  				
  				return;
  				}			
  			}
  			else if(rb3DES.isSelected())
  			{ 
  		
  				if   (((JTextField)ke.getSource()).getText().length() >= 16 ) 
  				{
		
  					ke.setKeyChar(empty);
  					return;
  				}			
  			}
  			else
  			{
  				if  (((JTextField)ke.getSource()).getText().length() >= 8 ) {
  		  			
  					ke.setKeyChar(empty);	
  					return;
  				}	
  			}
  		}
  		
  		
  		else if (rbNotEnc.isSelected()){
  			
  			if  (((JTextField)ke.getSource()).getText().length() >= 8 ) {
  			
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
		bSetVal.setEnabled(false);
		bSubmit.setEnabled(false);
		bFormat.setEnabled(false);
		bDisconnect.setEnabled(true);
		rbEnc.setEnabled(false);
		rbNotEnc.setEnabled(false);
		rbDES.setEnabled(false);
		rb3DES.setEnabled(false);
		txtCKey.setEnabled(false);
		txtTKey.setEnabled(false);
		txtSetVal.setEnabled(false);
		cbCode.setEnabled(false);		
		
	}
    
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new ACOS3Encrypt().setVisible(true);
            }
        });
    }



}
