/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              ACOS3Account.java

  Description:       This sample program outlines the steps on how to
                     use the Account File functionalities of ACOS
                     using the PC/SC platform.
                     
  Author:            M.J.E.C. Castillo

  Date:              August 20, 2008

  Revision Trail:   (Date/Author/Description)

======================================================================*/

import java.io.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.security.Security;
import java.util.Arrays;
 
import javax.crypto.Cipher;
import javax.crypto.CipherInputStream;
import javax.crypto.SecretKey;
import javax.crypto.SecretKeyFactory;
import javax.crypto.spec.DESKeySpec;

public class ACOS3Account extends JFrame implements ActionListener, KeyListener{

	//JPCSC Variables
	int retCode, maxLen;
	boolean connActive; 
	public static final int INVALID_SW1SW2 = -450;
	private static String algorithm = "DES";
	static String VALIDCHARS = "0123456789";
	
	//All variables that requires pass-by-reference calls to functions are
	//declared as 'Array of int' with length 1
	//Java does not process pass-by-ref to int-type variables, thus Array of int was used.
	int [] hContext = new int[1]; 
	int [] cchReaders = new int[1];
	int [] hCard = new int[1];
	int [] PrefProtocols = new int[1]; 		
	int [] RecvLen = new int[1];
	int SendLen = 0;
	byte [] SendBuff = new byte[262];
	byte [] RecvBuff = new byte[262];
	byte [] szReaders = new byte[1024];
	
	//GUI Variables
    private JButton bBalance, bQuit, bClear, bConnect, bCredit, bDebit, bFormat, bInit, bReset, bRevoke;
    private JCheckBox cbDebitCert;
    private JComboBox cbReader;
    private JPanel desPanel, acctPanel, msgPanel, readerPanel, secKeyPanel;
    private JLabel lblAmount, lblCertify, lblCredit, lblDebit, lblReader, lblRecoke;
    private JTextArea mMsg;
    private JRadioButton rb3Des, rbDes;
    private JScrollPane scrPaneMsg;
    private JTextField tAmt, tCertify, tCredit, tDebit, tRevoke;
    private ButtonGroup bgDes;
	
	static JacspcscLoader jacs = new JacspcscLoader();
    

    public ACOS3Account() {
    	
    	this.setTitle("ACOS 3 Account");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

		setSize(620,550);
		bgDes = new ButtonGroup();
	    readerPanel = new JPanel();
        lblReader = new JLabel();
        bInit = new JButton();
        bConnect = new JButton();
        bFormat = new JButton();
        desPanel = new JPanel();
        rbDes = new JRadioButton();
        rb3Des = new JRadioButton();
        secKeyPanel = new JPanel();
        lblCredit = new JLabel();
        lblDebit = new JLabel();
        lblCertify = new JLabel();
        lblRecoke = new JLabel();
        tCredit = new JTextField();
        tDebit = new JTextField();
        tCertify = new JTextField();
        tRevoke = new JTextField();
        acctPanel = new JPanel();
        lblAmount = new JLabel();
        tAmt = new JTextField();
        cbDebitCert = new JCheckBox();
        bCredit = new JButton();
        bDebit = new JButton();
        bBalance = new JButton();
        bRevoke = new JButton();
        msgPanel = new JPanel();
        scrPaneMsg = new JScrollPane();
        mMsg = new JTextArea();
        bClear = new JButton();
        bReset = new JButton();
        bQuit = new JButton();
        
        lblReader.setText("Select Reader");

		String[] rdrNameDef = {"Please select reader  "};	
		cbReader = new JComboBox(rdrNameDef);
		cbReader.setSelectedIndex(0);
		
        bInit.setText("Initialize");
        bConnect.setText("Connect");
        bFormat.setText("Format Card");
        desPanel.setBorder(BorderFactory.createTitledBorder("Security Option"));
        rbDes.setText("DES");
        rb3Des.setText("3-DES");
        bgDes.add(rbDes);
        bgDes.add(rb3Des);

        GroupLayout desPanelLayout = new GroupLayout(desPanel);
        desPanel.setLayout(desPanelLayout);
        desPanelLayout.setHorizontalGroup(
            desPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(desPanelLayout.createSequentialGroup()
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addGroup(desPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(rbDes, GroupLayout.PREFERRED_SIZE, 81, GroupLayout.PREFERRED_SIZE)
                    .addComponent(rb3Des)))
        );
        desPanelLayout.setVerticalGroup(
            desPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(desPanelLayout.createSequentialGroup()
                .addComponent(rbDes)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(rb3Des)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        GroupLayout readerPanelLayout = new GroupLayout(readerPanel);
        readerPanel.setLayout(readerPanelLayout);
        readerPanelLayout.setHorizontalGroup(
            readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(readerPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(readerPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                    .addGroup(GroupLayout.Alignment.LEADING, readerPanelLayout.createSequentialGroup()
                        .addComponent(lblReader)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(cbReader, GroupLayout.PREFERRED_SIZE, 144, GroupLayout.PREFERRED_SIZE))
                    .addGroup(GroupLayout.Alignment.LEADING, readerPanelLayout.createSequentialGroup()
                        .addGap(8, 8, 8)
                        .addComponent(desPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addGroup(readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                            .addComponent(bFormat, GroupLayout.DEFAULT_SIZE, 107, Short.MAX_VALUE)
                            .addComponent(bConnect, GroupLayout.DEFAULT_SIZE, 107, Short.MAX_VALUE)
                            .addComponent(bInit, GroupLayout.DEFAULT_SIZE, 107, Short.MAX_VALUE))))
                .addGap(15, 15, 15))
        );
        readerPanelLayout.setVerticalGroup(
            readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(readerPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(readerPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblReader)
                    .addComponent(cbReader, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addGap(22, 22, 22)
                .addGroup(readerPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                    .addGroup(readerPanelLayout.createSequentialGroup()
                        .addComponent(bInit)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(bConnect)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bFormat))
                    .addComponent(desPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addContainerGap())
        );

        acctPanel.setBorder(BorderFactory.createTitledBorder("Account Functions"));

        lblAmount.setText("Amount");
        cbDebitCert.setText("Require Debit Certificate");
        bCredit.setText("Credit");
        bDebit.setText("Debit");
        bBalance.setText("Inquire Balance");
        bRevoke.setText("Revoke Debit");


        GroupLayout acctPanelLayout = new GroupLayout(acctPanel);
        acctPanel.setLayout(acctPanelLayout);
        acctPanelLayout.setHorizontalGroup(
            acctPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(acctPanelLayout.createSequentialGroup()
                .addGroup(acctPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(acctPanelLayout.createSequentialGroup()
                        .addGap(25, 25, 25)
                        .addComponent(lblAmount)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(tAmt, GroupLayout.PREFERRED_SIZE, 130, GroupLayout.PREFERRED_SIZE))
                    .addGroup(acctPanelLayout.createSequentialGroup()
                        .addContainerGap()
                        .addGroup(acctPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                            .addGroup(acctPanelLayout.createSequentialGroup()
                                .addGroup(acctPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                                    .addComponent(bDebit, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                    .addComponent(bCredit, GroupLayout.DEFAULT_SIZE, 102, Short.MAX_VALUE))
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addGroup(acctPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                                    .addComponent(bRevoke, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                    .addComponent(bBalance, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)))
                            .addComponent(cbDebitCert))))
                .addContainerGap())
        );
        acctPanelLayout.setVerticalGroup(
            acctPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(acctPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(acctPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblAmount)
                    .addComponent(tAmt, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(cbDebitCert)
                .addGap(12, 12, 12)
                .addGroup(acctPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(bCredit)
                    .addComponent(bBalance))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(acctPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(bDebit)
                    .addComponent(bRevoke))
                .addContainerGap(14, Short.MAX_VALUE))
        );

        scrPaneMsg.setHorizontalScrollBarPolicy(ScrollPaneConstants.HORIZONTAL_SCROLLBAR_NEVER);

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
                        .addComponent(bClear, javax.swing.GroupLayout.PREFERRED_SIZE, 97, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bReset, javax.swing.GroupLayout.PREFERRED_SIZE, 96, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(bQuit, javax.swing.GroupLayout.PREFERRED_SIZE, 96, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addComponent(scrPaneMsg, javax.swing.GroupLayout.DEFAULT_SIZE, 301, Short.MAX_VALUE))
                .addContainerGap())
        );
        msgPanelLayout.setVerticalGroup(
            msgPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, msgPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(scrPaneMsg, javax.swing.GroupLayout.DEFAULT_SIZE, 408, Short.MAX_VALUE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(msgPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(bClear)
                    .addComponent(bReset)
                    .addComponent(bQuit))
                .addContainerGap())
        );

        secKeyPanel.setBorder(BorderFactory.createTitledBorder("Security Keys"));

        lblCredit.setText("Credit");
        lblDebit.setText("Debit");
        lblCertify.setText("Certify");
        lblRecoke.setText("Revoke Debit");


        GroupLayout secKeyPanelLayout = new GroupLayout(secKeyPanel);
        secKeyPanel.setLayout(secKeyPanelLayout);
        secKeyPanelLayout.setHorizontalGroup(
            secKeyPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(secKeyPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(secKeyPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(lblCredit)
                    .addComponent(lblDebit)
                    .addComponent(lblCertify)
                    .addComponent(lblRecoke))
                .addGap(30, 30, 30)
                .addGroup(secKeyPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(tRevoke)
                    .addComponent(tDebit, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, 77, Short.MAX_VALUE)
                    .addComponent(tCertify)
                    .addComponent(tCredit, GroupLayout.PREFERRED_SIZE, 94, GroupLayout.PREFERRED_SIZE))
                .addContainerGap(39, Short.MAX_VALUE))
        );
        secKeyPanelLayout.setVerticalGroup(
            secKeyPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(secKeyPanelLayout.createSequentialGroup()
                .addGroup(secKeyPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblCredit)
                    .addComponent(tCredit, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(secKeyPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblDebit)
                    .addComponent(tDebit, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(secKeyPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblCertify)
                    .addComponent(tCertify, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(secKeyPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblRecoke)
                    .addComponent(tRevoke, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        GroupLayout layout = new GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(readerPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(secKeyPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(acctPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(msgPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGap(11, 11, 11)
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                    .addComponent(msgPanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addGroup(GroupLayout.Alignment.LEADING, layout.createSequentialGroup()
                        .addComponent(readerPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(secKeyPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(acctPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
		
        mMsg.setLineWrap(true);
        
        bInit.setMnemonic(KeyEvent.VK_I);
        bConnect.setMnemonic(KeyEvent.VK_C);
        bReset.setMnemonic(KeyEvent.VK_R);
        bClear.setMnemonic(KeyEvent.VK_L);
        bFormat.setMnemonic(KeyEvent.VK_F);
        bCredit.setMnemonic(KeyEvent.VK_R);
        bDebit.setMnemonic(KeyEvent.VK_D);
        bBalance.setMnemonic(KeyEvent.VK_B);
        bRevoke.setMnemonic(KeyEvent.VK_V);
        bQuit.setMnemonic(KeyEvent.VK_Q);

        bInit.addActionListener(this);
        bConnect.addActionListener(this);
        bReset.addActionListener(this);
        bClear.addActionListener(this);
        bFormat.addActionListener(this);
        bCredit.addActionListener(this);
        bDebit.addActionListener(this);
        bBalance.addActionListener(this);
        bQuit.addActionListener(this);
        bRevoke.addActionListener(this);
        rbDes.addActionListener(this);
        rb3Des.addActionListener(this);
        tCredit.addKeyListener(this);
        tDebit.addKeyListener(this);
        tRevoke.addKeyListener(this);
        tCertify.addKeyListener(this);
        tAmt.addKeyListener(this);
        
        
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
			bFormat.setEnabled(true);
			bCredit.setEnabled(true);
			bDebit.setEnabled(true);
			bRevoke.setEnabled(true);
			bBalance.setEnabled(true);
			tCredit.setEnabled(true);
			tDebit.setEnabled(true);
			tCertify.setEnabled(true);
			tRevoke.setEnabled(true);
			tAmt.setEnabled(true);
			cbDebitCert.setEnabled(true);
			rbDes.setEnabled(true);
			rb3Des.setEnabled(true);
			rbDes.setSelected(true);
			maxLen=8;
			
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
			
			if(!validTemplate())
				return;
			
			if(!checkACOS())
			{
				displayOut(0, 0, "Please insert an ACOS card.");
				return;
			}
			
			//submit issuer code
			retCode = submitIC();
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			//select FF 02
			retCode = selectFile((byte)0xFF, (byte)0x02);
			if(retCode != ACSModule.SCARD_S_SUCCESS)
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
		    	displayOut(0, 0, "Return string is invalid. Value: " + tmpStr);
		    	return;
		    }
		    
		    // 5. Write to FF 02
		    //    This step will define the Option registers,
		    //    Security Option registers and Personalization bit
		    //    are not set
		    if(rbDes.isSelected())
		    	tmpArray[0] = (byte)0x29;
		    else
		    	tmpArray[0] = (byte)0x2B;
		    
		    tmpArray[1] = (byte)0x00;
		    tmpArray[2] = (byte)0x03;
		    tmpArray[3] = (byte)0x00;
		    retCode = writeRecord(0, (byte)0x00, (byte)0x04, (byte)0x04, tmpArray);
		    if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
		    
		    displayOut(0, 0, "FF 02 is updated");
		    
		    //6. Perform a reset for changes in the ACOS to take effect
		    retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
		    connActive = false;
		    
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
		    	return;
		    }
		    
		    displayOut(0, 0, "Account files are enabled.");
		    connActive = true;
		    
		    //7. Submit Issuer Code to write into FF 05 and FF 06
		    retCode = submitIC();
		    if(retCode != ACSModule.SCARD_S_SUCCESS)
		    	return;
		    
		    // 8. Select FF 05
		    retCode = selectFile((byte)0xFF, (byte)0x05);
		    if(retCode != ACSModule.SCARD_S_SUCCESS)
		    	return;
		    
		    tmpStr = "";
		    
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
		    	return;
		    }
		    
		    //9. Write to FF 05
		    // 9.1. Record 00
		    tmpArray[0] = (byte)0x00;
		    tmpArray[1] = (byte)0x00;
		    tmpArray[2] = (byte)0x00;
		    tmpArray[3] = (byte)0x00;
		    retCode = writeRecord(0, (byte)0x00, (byte)0x04, (byte)0x04, tmpArray);
		    if(retCode != ACSModule.SCARD_S_SUCCESS)
		    	return;
		    
		    //9.2.Record 01
		    tmpArray[0] = (byte)0x00;
		    tmpArray[1] = (byte)0x00;
		    tmpArray[2] = (byte)0x01;
		    tmpArray[3] = (byte)0x00;
		    retCode = writeRecord(0, (byte)0x01, (byte)0x04, (byte)0x04, tmpArray);
		    if(retCode != ACSModule.SCARD_S_SUCCESS)
		    	return;
		    
		    //9.3. Record 02
		    tmpArray[0] = (byte)0x00;
		    tmpArray[1] = (byte)0x00;
		    tmpArray[2] = (byte)0x00;
		    tmpArray[3] = (byte)0x00;
		    retCode = writeRecord(0, (byte)0x02, (byte)0x04, (byte)0x04, tmpArray);
		    if(retCode != ACSModule.SCARD_S_SUCCESS)
		    	return;
		    
		    //9.4. Record 03
		    tmpArray[0] = (byte)0x00;
		    tmpArray[1] = (byte)0x00;
		    tmpArray[2] = (byte)0x01;
		    tmpArray[3] = (byte)0x00;
		    retCode = writeRecord(0, (byte)0x03, (byte)0x04, (byte)0x04, tmpArray);
		    if(retCode != ACSModule.SCARD_S_SUCCESS)
		    	return;
		    
		    //9.5. Record 04
		    tmpArray[0] = (byte)0xFF;
		    tmpArray[1] = (byte)0xFF;
		    tmpArray[2] = (byte)0xFF;
		    tmpArray[3] = (byte)0x00;
		    retCode = writeRecord(0, (byte)0x04, (byte)0x04, (byte)0x04, tmpArray);
		    if(retCode != ACSModule.SCARD_S_SUCCESS)
		    	return;
		    
		    //9.6. record 05
		    tmpArray[0] = (byte)0x00;
		    tmpArray[1] = (byte)0x00;
		    tmpArray[2] = (byte)0x00;
		    tmpArray[3] = (byte)0x00;
		    retCode = writeRecord(0, (byte)0x05, (byte)0x04, (byte)0x04, tmpArray);
		    if(retCode != ACSModule.SCARD_S_SUCCESS)
		    	return;
		    
		    //9.7. record 06
		    tmpArray[0] = (byte)0x00;
		    tmpArray[1] = (byte)0x00;
		    tmpArray[2] = (byte)0x00;
		    tmpArray[3] = (byte)0x00;
		    retCode = writeRecord(0, (byte)0x06, (byte)0x04, (byte)0x04, tmpArray);
		    if(retCode != ACSModule.SCARD_S_SUCCESS)
		    	return;
		    
		    //9.8. record 07
		    tmpArray[0] = (byte)0x00;
		    tmpArray[1] = (byte)0x00;
		    tmpArray[2] = (byte)0x00;
		    tmpArray[3] = (byte)0x00;
		    retCode = writeRecord(0, (byte)0x07, (byte)0x04, (byte)0x04, tmpArray);
		    if(retCode != ACSModule.SCARD_S_SUCCESS)
		    	return;
		    
		    //10. select FF 06
		    retCode = selectFile((byte)0xFF, (byte)0x06);
		    if(retCode != ACSModule.SCARD_S_SUCCESS)
		    	return;
		    
		    tmpStr = "";
		    
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
		    	return;
		    }
		    
		    int tmpInt;
		    //write to FF 05
		    if(rbDes.isSelected())
		    {
		    	
		    	//11a.1. Record 00 for Debit key
		    	tmpStr = tDebit.getText();
		    	
		    	for(int i = 0; i< 8; i++)
		    	{
		    		tmpInt=(int) tmpStr.charAt(i);
		    		tmpArray[i] = (byte)tmpInt; 
		    	}
		    	
		    	 retCode = writeRecord(0, (byte)0x00, (byte)0x08, (byte)0x08, tmpArray);
				 if(retCode != ACSModule.SCARD_S_SUCCESS)
				   	return;
				 
				 //11a.2. Record 01 for Credit key
				 tmpStr = tCredit.getText();
				 for(int i = 0; i< 8; i++)
			    	{
			    		tmpInt=(int) tmpStr.charAt(i);
			    		tmpArray[i] = (byte)tmpInt; 
			    	}
			    	
			    	 retCode = writeRecord(0, (byte)0x01, (byte)0x08, (byte)0x08, tmpArray);
					 if(retCode != ACSModule.SCARD_S_SUCCESS)
					   	return;

				//11a.3. Record 01 for Certify key
				 tmpStr = tCertify.getText();
				 for(int i = 0; i< 8; i++)
		    	 {
			   		tmpInt=(int) tmpStr.charAt(i);
		    		tmpArray[i] = (byte)tmpInt; 
		    	 }
				    	
			   	 retCode = writeRecord(0, (byte)0x02, (byte)0x08, (byte)0x08, tmpArray);
				 if(retCode != ACSModule.SCARD_S_SUCCESS)
					   	return;
				 
				 //11a.2. Record 01 for Revoke Debit key
				 tmpStr = tRevoke.getText();
				 for(int i = 0; i< 8; i++)
			    	{
			    		tmpInt=(int) tmpStr.charAt(i);
			    		tmpArray[i] = (byte)tmpInt; 
			    	}
			    	
			    	 retCode = writeRecord(0, (byte)0x03, (byte)0x08, (byte)0x08, tmpArray);
					 if(retCode != ACSModule.SCARD_S_SUCCESS)
					   	return;
		    	
		    }
		    else
		    {
		    	
		    	// 11b.1. Record 04 for Left half of Debit key
		    	 tmpStr = tDebit.getText();
				 for(int i = 0; i< 8; i++)
		    	 {
			   		tmpInt=(int) tmpStr.charAt(i);
		    		tmpArray[i] = (byte)tmpInt; 
		    	 }
		    	
				 retCode = writeRecord(0, (byte)0x04, (byte)0x08, (byte)0x08, tmpArray);
				 if(retCode != ACSModule.SCARD_S_SUCCESS)
					   	return;
				 
				// 11b.2. Record 00 for Right half of Debit key
				 for(int i = 8; i< 16; i++)
		    	 {
			   		tmpInt=(int) tmpStr.charAt(i);
		    		tmpArray[i-8] = (byte)tmpInt; 
		    	 }
		    	
				 retCode = writeRecord(0, (byte)0x00, (byte)0x08, (byte)0x08, tmpArray);
				 if(retCode != ACSModule.SCARD_S_SUCCESS)
					   	return;
				 
				// 11b.3. Record 05 for Left half of Credit key
		    	 tmpStr = tCredit.getText();
				 for(int i = 0; i< 8; i++)
		    	 {
			   		tmpInt=(int) tmpStr.charAt(i);
		    		tmpArray[i] = (byte)tmpInt; 
		    	 }
		    	
				 retCode = writeRecord(0, (byte)0x05, (byte)0x08, (byte)0x08, tmpArray);
				 if(retCode != ACSModule.SCARD_S_SUCCESS)
					   	return;
				 
				// 11b.4. Record 01 for Right half of Credit key
				 for(int i = 8; i< 16; i++)
		    	 {
			   		tmpInt=(int) tmpStr.charAt(i);
		    		tmpArray[i-8] = (byte)tmpInt; 
		    	 }
		    	
				 retCode = writeRecord(0, (byte)0x01, (byte)0x08, (byte)0x08, tmpArray);
				 if(retCode != ACSModule.SCARD_S_SUCCESS)
					   	return;
				 
				// 11b.5. Record 06 for Left half of Certify key
		    	 tmpStr = tCertify.getText();
				 for(int i = 0; i< 8; i++)
		    	 {
			   		tmpInt=(int) tmpStr.charAt(i);
		    		tmpArray[i] = (byte)tmpInt; 
		    	 }
		    	
				 retCode = writeRecord(0, (byte)0x06, (byte)0x08, (byte)0x08, tmpArray);
				 if(retCode != ACSModule.SCARD_S_SUCCESS)
					   	return;
				 
				// 11b.6. Record 02 for Right half of Certify key
				 for(int i = 8; i< 16; i++)
		    	 {
			   		tmpInt=(int) tmpStr.charAt(i);
		    		tmpArray[i-8] = (byte)tmpInt; 
		    	 }
		    	
				 retCode = writeRecord(0, (byte)0x02, (byte)0x08, (byte)0x08, tmpArray);
				 if(retCode != ACSModule.SCARD_S_SUCCESS)
					   	return;
				 
				// 11b.7. Record 04 for Left half of Revoke Debit key
		    	 tmpStr = tRevoke.getText();
				 for(int i = 0; i< 8; i++)
		    	 {
			   		tmpInt=(int) tmpStr.charAt(i);
		    		tmpArray[i] = (byte)tmpInt; 
		    	 }
		    	
				 retCode = writeRecord(0, (byte)0x07, (byte)0x08, (byte)0x08, tmpArray);
				 if(retCode != ACSModule.SCARD_S_SUCCESS)
					   	return;
				 
				// 11b.8. Record 00 for Right half of Revoke Debit key
				 for(int i = 8; i< 16; i++)
		    	 {
					 
			   		tmpInt=(int) tmpStr.charAt(i);
		    		tmpArray[i-8] = (byte)tmpInt; 
		    		
		    	 }
		    	
				 retCode = writeRecord(0, (byte)0x03, (byte)0x08, (byte)0x08, tmpArray);
				 if(retCode != ACSModule.SCARD_S_SUCCESS)
					   	return;
				 
		    }
		    
		    tCredit.setText("");
		    tDebit.setText("");
		    tCertify.setText("");
		    tRevoke.setText("");
		    displayOut(0, 0, "FF 06 is updated");
		    
		}
		
		if(bCredit == e.getSource())
		{
			String tmpStr="";
			byte[] tmpArray = new byte[32];
			byte[] TTREFc = new byte[4];
			byte[] ATREF = new byte[6];
			byte[] tmpKey = new byte[16];
			int amount, tmpVal;
			//1. Check if Credit key and valid Transaction value are provided
			if(tCredit.getText().length() < maxLen)
			{
				
				tCredit.selectAll();
				tCredit.requestFocus();
				return;
				
			}
			
			if(tAmt.getText().equals("")||(Integer.parseInt(tAmt.getText())>16777215))
			{
				
				tAmt.setText("16777215");
				tAmt.selectAll();
				tAmt.requestFocus();
				return;
				
			}
			
			//2. Check if card inserted is an ACOS card
			if(!checkACOS())
			{
				
				displayOut(0, 0, "Please insert an ACOS card.");
				return;
				
			}
			
			displayOut(0, 0, "ACOS card is detected.");
			
			//  3. Issue INQUIRE ACCOUNT command using any arbitrary data and Credit key
			//     Arbitrary data is 1111h
			for(int i=0; i< 4; i++)
				tmpArray[i] = (byte)0x01;
			
			retCode = inquireAccount((byte)0x02, tmpArray);
			if(retCode != ACSModule.SCARD_S_SUCCESS)
			   	return;
			
			//4. Issue GET RESPONSE command with Le = 19h or 25 bytes
			retCode = getResponse((byte)0x19);
			if(retCode != ACSModule.SCARD_S_SUCCESS)
			   	return;

			// 5. Store ACOS card values for TTREFc and ATREF
			for(int i=0; i<4; i++)
				TTREFc[i] = RecvBuff[i + 17];
			
			for (int i=0; i<6; i++)
				ATREF[i] = RecvBuff[i+8];
			
			//6. Prepare MAC data block: E2 + AMT + TTREFc + ATREF + 00 + 00
			//   use tmpArray as the data block
			amount = Integer.parseInt(tAmt.getText());
			tmpArray[0] = (byte)0xE2;
			tmpVal = (int) amount/256;
			tmpVal = (int) tmpVal/256;
			tmpArray[1] = (byte)(tmpVal % 256);
			tmpVal = (int) amount/256;
			tmpArray[2] = (byte)(tmpVal % 256);
			tmpArray[3] = (byte)(amount % 256);
			
			for(int i=0; i<4; i++)
				tmpArray[i+4] = TTREFc[i];
			
			for(int i=0; i<6; i++)
				tmpArray[i+8] = ATREF[i];
			
			tmpArray[13] = (byte)((tmpArray[13] & 0xFF)+1);
			tmpArray[14] = (byte)0x00;
			tmpArray[15] = (byte)0x00;
			
			//7. Generate applicable MAC values, MAC result will be stored in tmpArray
			tmpStr= tCredit.getText();
			int tmpInt;
			for(int i=0; i<tmpStr.length(); i++)
			{
				tmpInt = (int)tmpStr.charAt(i);
				tmpKey[i] = (byte)tmpInt;
			}
			
			if(rbDes.isSelected())
				mac(tmpArray, tmpKey);
			else
				TripleMAC(tmpArray, tmpKey);
			
			//8. Format Credit command data and execute credit command
			//   Using tmpArray, the first four bytes are carried over
			tmpVal = (int)amount/256;
			tmpVal = (int)tmpVal/256;
			tmpArray[4] = (byte)(tmpVal % 256);
			tmpVal = (int) amount/256;
			tmpArray[5] = (byte)(tmpVal % 256);
			tmpArray[6] = (byte)(amount % 256);
			
			for (int i=0; i<4; i++)
				tmpArray[7 + i] = ATREF[i];
			
			retCode = creditAmount(tmpArray);
			if(retCode != ACSModule.SCARD_S_SUCCESS)
			   	return;
			
			displayOut(3, 0, "Credit transaction completed");
			tCredit.setText("");
			tDebit.setText("");
			tRevoke.setText("");
			tCertify.setText("");
			tAmt.setText("");
			
		}
		
		if(bDebit == e.getSource())
		{
			
			boolean b;
			byte[] tmpArray = new byte[32]; 
			byte[] tmpKey = new byte[16];
			byte[] TTREFd = new byte[4];
			byte[] ATREF = new byte[6];
			int[] tmpBalance = new int[4];
			int newBalance, amt, tmpVal;
			String tmpStr="";
			
			//1. Check if Debit key and valid Transaction value are provided
			if(tDebit.getText().length() != maxLen)
			{
				
				tDebit.selectAll();
				tDebit.requestFocus();
				return;
				
			}
			
			
			if(tAmt.getText().equals("")||(Integer.parseInt(tAmt.getText())>16777215))
			{
				
				tAmt.setText("16777215");
				tAmt.selectAll();
				tAmt.requestFocus();
				return;
				
			}
			
			//2. Check if card inserted is an ACOS card
			if(!checkACOS())
			{
			
				displayOut(0, 0, "Please insert an ACOS card.");
				return;
				
			}
			
			displayOut(0, 0, "ACOS card is detected.");
			
			//3. Issue INQUIRE ACCOUNT command using any arbitrary data and Credit key
			//    Arbitrary data is 1111h
			for(int i=0; i<4; i++)
				tmpArray[i] = (byte)0x01;
			
			retCode = inquireAccount((byte)0x02, tmpArray);
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			//4. Issue GET RESPONSE command with Le = 19h or 25 bytes
			retCode = getResponse((byte)0x19);
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			tmpBalance[1] = (RecvBuff[7] & 0xFF);
			tmpBalance[2] = (RecvBuff[6] & 0xFF);
			tmpBalance[2] = (tmpBalance[2] *256);
			tmpBalance[3] = (RecvBuff[5] & 0xFF);
			tmpBalance[3] = (tmpBalance[3] * 256);
			tmpBalance[3] = (tmpBalance[3] * 256);
			tmpBalance[0] =  tmpBalance[1] + tmpBalance[2] + tmpBalance[3];
			
			//5. Store ACOS card values for TTREFd and ATREF
			for(int i=0; i<4; i++)
				TTREFd[i] = RecvBuff[i + 21];
			
			for(int i=0; i<6; i++)
				ATREF[i] = RecvBuff[i + 8];
			
			//6. Prepare MAC data block: E6 + AMT + TTREFd + ATREF + 00 + 00
			//    use tmpArray as the data block
			amt = Integer.parseInt(tAmt.getText());
			tmpArray[0] = (byte)0xE6;
			tmpVal = (int)(amt / 256);
			tmpVal = (int)(tmpVal / 256);
			tmpArray[1] = (byte)(tmpVal % 256);
			tmpVal = (int)(amt/256);
			tmpArray[2] = (byte)(tmpVal % 256);
			tmpArray[3] = (byte)(amt % 256);
			
			for(int i=0; i<4; i++)
				tmpArray[i + 4] = TTREFd[i];
			
			for(int i=0; i<6; i++)
				tmpArray[i + 8] = ATREF[i];
			
			tmpArray[13] = (byte) (tmpArray[13] + 1);
			tmpArray[14] = (byte) 0x00;
			tmpArray[15] = (byte)0x00;
			
			//7. Generate applicable MAC values, MAC result will be stored in tmpArray
			tmpStr = tDebit.getText();
			for(int i=0; i<tmpStr.length(); i++)
				tmpKey[i] = (byte)((int)tmpStr.charAt(i));
			
			if(rbDes.isSelected())
				mac(tmpArray, tmpKey);
			else
				TripleMAC(tmpArray, tmpKey);
			
			//8. Format Debit command data and execute debit command
			//    Using tmpArray, the first four bytes are carried over
			tmpVal = (int)(amt/256);
			tmpVal = (int)(tmpVal/256);
			tmpArray[4] = (byte)(tmpVal % 256);
			tmpVal = (int)(amt/256);
			tmpArray[5] = (byte)(tmpVal % 256);
			tmpArray[6] = (byte)(amt % 256);
			
			for(int i=0; i<6; i++)
				tmpArray[i+7] = ATREF[i];
			
			if(!cbDebitCert.isSelected())
			{	
				
				retCode = debitAmount(tmpArray);
				if(retCode != ACSModule.SCARD_S_SUCCESS)
					return;
				
			}
			else
			{
			
				retCode = debitAmountwithDBC(tmpArray);
				if(retCode != ACSModule.SCARD_S_SUCCESS)
					return;
				
				retCode= getResponse((byte)0x04);
				if (retCode!= ACSModule.SCARD_S_SUCCESS)
					return;
				
				//Prepare MAC data block: 01 + New Balance + ATC + TTREFD + 00 + 00 + 00
			    //   use tmpArray as the data block
				amt = Integer.parseInt(tAmt.getText());
				newBalance = tmpBalance[0] - amt;
				tmpArray[0] = (byte)0x01;
				
				tmpVal = (int)(newBalance / 256);
				tmpVal = (int)(tmpVal / 256);
				tmpArray[1] = (byte)(tmpVal % 256);
				tmpVal = (int)(newBalance / 256);
				tmpArray[2] = (byte)(tmpVal % 256);
				tmpArray[3] = (byte)(newBalance % 256);
				
				tmpVal = (int)(amt / 256);
				tmpVal = (int)(tmpVal / 256);
				tmpArray[4] = (byte)(tmpVal % 256);
				tmpVal = (int)(amt / 256);
				tmpArray[5] = (byte)(tmpVal % 256);
				tmpArray[6] = (byte)(amt % 256);
				tmpArray[7] = ATREF[4];
				tmpArray[8] = (byte)(ATREF[5] + 1);    
				
				for(int i=0; i<4; i++)
					tmpArray[i+9] = TTREFd[i];
				
				tmpArray[13] = (byte)0x00;
				tmpArray[14] = (byte)0x00;
				tmpArray[15] = (byte)0x00;
				
				//Generate applicable MAC values, MAC result will be stored in tmpArray
				tmpStr = tDebit.getText();
				for(int i=0; i<tmpStr.length(); i++)
					tmpKey[i] = (byte)((int)tmpStr.charAt(i));
				
				if(rbDes.isSelected())
					mac(tmpArray, tmpKey);
				else
					TripleMAC(tmpArray, tmpKey);
				
				b = Arrays.equals(RecvBuff, tmpArray);
				if(b)
				{
					displayOut(0, 0, "Debit Certificate Failed.");
					cbDebitCert.setSelected(false);
					return;
						
				}
				
				
				displayOut(3, 0, "Debit Certificate Verified.");
				
			}
			
			displayOut(3, 0, "Debit transaction completed");
			tCredit.setText("");
			tDebit.setText("");
			tCertify.setText("");
			tRevoke.setText("");
			tAmt.setText("");
			
		}
		
		if(bBalance == e.getSource())
		{
			
			String tmpStr="";
			byte[] tmpArray = new byte[32];
			int[] tmpBalance = new int[4];
			byte[] tmpKey = new byte[16];
			byte lastTran;
			byte[] TTREFc = new byte[4];
			byte[] TTREFd = new byte[4];
			byte[] ATREF = new byte[6];
			
			//1. Check if Certify key is provided
			if(tCertify.getText().length() != maxLen)
			{
				
				tCertify.selectAll();
				tCertify.requestFocus();
				return;
				
			}
			
			//2. Check if card inserted is an ACOS card
			if(!checkACOS())
			{
				displayOut(0, 0, "Please insert an ACOS card.");
				return;
			}
			
			displayOut(0, 0, "ACOS card is detected.");
			
			//3. Issue INQUIRE ACCOUNT command using any arbitrary data and Certify key
			//    Arbitrary data is 1111h
			for(int i=0; i<4; i++)
				tmpArray[i] = (byte)0x01;
			
			retCode = inquireAccount((byte)0x02, tmpArray);
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			//4. Issue GET RESPONSE command with Le = 19h or 25 bytes
			retCode = getResponse((byte)0x19);
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			//5. Check integrity of data returned by card
			// 5.1. Build MAC input data
			// 5.1.1. Extract the info from ACOS card in Dataout
			lastTran = RecvBuff[4];
		    tmpBalance[1] = (RecvBuff[7] & 0xFF);
		    tmpBalance[2] = (RecvBuff[6] & 0xFF);
		    tmpBalance[2] = tmpBalance[2] * 256;
		    tmpBalance[3] = (RecvBuff[5] & 0xFF);
		    tmpBalance[3] = tmpBalance[3] * 256;
		    tmpBalance[3] = tmpBalance[3] * 256;
		    tmpBalance[0] = tmpBalance[1] + tmpBalance[2] + tmpBalance[3];
		    
		    for(int i=0; i<4; i++)
		    	TTREFc[i] = RecvBuff[i+17];
		    
		    for(int i=0; i<4; i++)
		    	TTREFd[i] = RecvBuff[i+21];
		    
		    for(int i=0; i<6; i++)
		    	ATREF[i] = RecvBuff[i+8];
		    
		    // 5.1.2. Move data from ACOS card as input to MAC calculations
		    tmpArray[4] = RecvBuff[4];
		    for(int i=0; i<3; i++)
		    	tmpArray[i+5] = RecvBuff[i+5];
		    
		    for(int i=0; i<6; i++)
		    	tmpArray[i+8] = RecvBuff[i+8];
		 
		    tmpArray[14] = (byte)0x00;
		    tmpArray[15] = (byte)0x00;
		    
		    for(int i=0; i<4; i++)
		    	tmpArray[i+16] = TTREFc[i];
		    
		    for(int i=0; i<4; i++)
		    	tmpArray[i+20] = TTREFd[i];
		    
		    //5.2. Generate applicable MAC values
		    tmpStr = tCertify.getText();
		    for(int i=0; i<tmpStr.length(); i++)
		    	tmpKey[i] = (byte)((int)tmpStr.charAt(i));
		    
		    if(rbDes.isSelected())
		    	mac(tmpArray, tmpKey);
		    else
		    	TripleMAC(tmpArray, tmpKey);
		    
		    //5.3. Compare MAC values
		    for(int i=0; i<4; i++)
		    {
		    	
		    	if(tmpArray[i] != RecvBuff[i])
		    	{
		    		displayOut(4, 0, "MAC is incorrect, data integrity is jeopardized.");
		    		break;
		    		
		    	}
		    	
		    }
		    
		    //6. Display relevant data from ACOS card
		    switch(lastTran)
		    {
		    
		    case 1: tmpStr="DEBIT"; break;
		    case 2: tmpStr="REVOKE DEBIT"; break;
		    case 3: tmpStr="CREDIT"; break;
		    default: tmpStr = "NOT DEFINED";
		    
		    }
		    
		    displayOut(3, 0, "Last transaction is " + tmpStr + ".");
		    tCredit.setText("");
		    tDebit.setText("");
		    tCertify.setText("");
		    tRevoke.setText("");
		    tAmt.setText(""+tmpBalance[0]);
		    
			
		}
		
		if(bRevoke == e.getSource())
		{
			
			String tmpStr="";
			byte[] tmpArray = new byte[32];
			int amt, tmpVal;
			byte[] tmpKey = new byte[16];
			byte[] TTREFd = new byte[4];
			byte[] ATREF = new byte[6];
			
			//1. Check if Debit key and valid Transaction value are provided
			if(tRevoke.getText().length() != maxLen)
			{
				
				tRevoke.selectAll();
				tRevoke.requestFocus();
				return;
				
			}
			
			
			if(tAmt.getText().equals("") || (Integer.parseInt(tAmt.getText())>16777215))
			{
				tAmt.setText("16777215");
				tAmt.selectAll();
				tAmt.requestFocus();
				return;
			}
			
			//2. Check if card inserted is an ACOS card
			if(!checkACOS())
			{
			
				displayOut(0, 0, "Please insert an ACOS card.");
				return;
				
			}
			
			displayOut(0, 0, "ACOS card is detected.");
			
			//3. Issue INQUIRE ACCOUNT command using any arbitrary data and Credit key
			//    Arbitrary data is 1111h
			for(int i=0; i<4; i++)
				tmpArray[i] = (byte)0x01;
			
			retCode = inquireAccount((byte)0x02, tmpArray);
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			//4. Issue GET RESPONSE command with Le = 19h or 25 bytes
			retCode = getResponse((byte)0x19);
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			//5. Store ACOS card values for TTREFd and ATREF
			for(int i=0; i<4; i++)
				TTREFd[i] = RecvBuff[i+21];
			
			for(int i=0; i<6; i++)
				ATREF[i] = RecvBuff[i+8];
			
			//6. Prepare MAC data block: E8 + AMT + TTREFd + ATREF + 00 + 00
			//    use tmpArray as the data block
			amt = Integer.parseInt(tAmt.getText());
			tmpArray[0] = (byte)0xE8;
			tmpVal = (int)(amt / 256);
			tmpVal = (int)(tmpVal / 256);
			tmpArray[1] = (byte)(tmpVal % 256);
			tmpVal = (int)(amt/256);
			tmpArray[2] = (byte)(tmpVal % 256);
			tmpArray[3] = (byte)(amt % 256);
			
			for(int i=0; i<4; i++)
				tmpArray[i + 4] = TTREFd[i];
			
			for(int i=0; i<6; i++)
				tmpArray[i + 8] = ATREF[i];
			
			tmpArray[13] = (byte) ((tmpArray[13] & 0xFF) + 1);
			tmpArray[14] = (byte) 0x00;
			tmpArray[15] = (byte)0x00;
			
			//7. Generate applicable MAC values, MAC result will be stored in tmpArray
			tmpStr = tRevoke.getText();
			for(int i=0; i<tmpStr.length(); i++)
				tmpKey[i] = (byte)((int)tmpStr.charAt(i));
			
			if(rbDes.isSelected())
				mac(tmpArray, tmpKey);
			else
				TripleMAC(tmpArray, tmpKey);
			
			//8. Execute Revoke Debit command data and execute credit command
			//    Using tmpArray, the first four bytes storing the MAC value are carried over
			retCode = revokeDebit(tmpArray);
			if(retCode!=ACSModule.SCARD_S_SUCCESS)
				return;
			
			displayOut(3, 0, "Revoke Debit transaction completed");
			tCredit.setText("");
			tDebit.setText("");
			tRevoke.setText("");
			tCertify.setText("");
			tAmt.setText("");
			
		}
		
		if(rbDes == e.getSource())
		{ 
			
			tCredit.setText("");
			tDebit.setText("");
			tCertify.setText("");
			tRevoke.setText("");
			tAmt.setText("");
			maxLen = 8;
			
		}

		if(rb3Des == e.getSource())
		{
			
			tCredit.setText("");
			tDebit.setText("");
			tCertify.setText("");
			tRevoke.setText("");
			tAmt.setText("");
			maxLen = 16;
			
		}
				
	}
    
	public int revokeDebit(byte[] revDebData)
	{
		
		String tmpStr="", tmpHex="";
		clearBuffers();
		SendBuff[0] = (byte)0x80;
		SendBuff[1] = (byte)0xE8;
		SendBuff[2] = (byte)0x00;
		SendBuff[3] = (byte)0x00;
		SendBuff[4] = (byte)0x04;
		
		for(int i=0; i<5; i++)
			SendBuff[i+5] = revDebData[i];
		
		SendLen = (SendBuff[4] & 0xFF)+5;
		RecvLen[0] = 0x02;
		
		for(int i=0; i<SendLen; i++)
		 {
			tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
				
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
				
			tmpStr += " " + tmpHex;  
				
		 }
		
		retCode = sendAPDUandDisplay(0, tmpStr);
		if(retCode != ACSModule.SCARD_S_SUCCESS)
			return retCode;
		
		tmpStr="";
		for(int i=0; i<2; i++)
		 {
			tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
				
			tmpStr += " " + tmpHex;  
				
		 }
		
		if(ACOSError(RecvBuff[0], RecvBuff[1]))
			return INVALID_SW1SW2;
		
		if(!tmpStr.trim().equals("90 00"))
		{
			
			displayOut(4, 0, "REVOKE DEBIT command failed.");
		    displayOut(0, 0, "Return string is invalid. Value: " + tmpStr);
		    return INVALID_SW1SW2;
		    
		}
		
		return retCode;
		
	}
	
	public int debitAmountwithDBC(byte[] debitData)
	{
		
		String tmpStr="", tmpHex="";

		clearBuffers();
		SendBuff[0] = (byte)0x80;
		SendBuff[1] = (byte)0xE6;
		SendBuff[2] = (byte)0x01;
		SendBuff[3] = (byte)0x00;
		SendBuff[4] = (byte)0x0B;
		
		for(int i=0; i<12; i++)
			SendBuff[i+5] = debitData[i];
		
		SendLen = (SendBuff[4] & 0xFF)+5;
		RecvLen[0] = 0x02;
		
		for(int i=0; i<SendLen; i++)
		 {
			tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
				
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
				
			tmpStr += " " + tmpHex;  
				
		 }
		
		retCode = sendAPDUandDisplay(0, tmpStr);
		if(retCode != ACSModule.SCARD_S_SUCCESS)
			return retCode;
		
		tmpStr="";
		for(int i=0; i<2; i++)
		 {
			tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
				
			tmpStr += " " + tmpHex;  
				
		 }
		
		if(ACOSError(RecvBuff[0], RecvBuff[1]))
			return INVALID_SW1SW2;

		if(!tmpStr.trim().equals("61 04"))
		{
			
			displayOut(4, 0, "DEBIT AMOUNT command failed.");
		    displayOut(0, 0, "Return string is invalid. Value: " + tmpStr);
		    return INVALID_SW1SW2;
		    
		}
		
		return retCode;
		
	}
	
	public int debitAmount(byte[] debitData)
	{
		
		String tmpStr="", tmpHex="";
		
		clearBuffers();
		SendBuff[0] = (byte)0x80;
		SendBuff[1] = (byte)0xE6;
		SendBuff[2] = (byte)0x00;
		SendBuff[3] = (byte)0x00;
		SendBuff[4] = (byte)0x0B;
		
		for(int i=0; i<12; i++)
			SendBuff[i+5] = debitData[i];
		
		SendLen = (SendBuff[4] & 0xFF)+5;
		RecvLen[0] = 0x02;
		
		for(int i=0; i<SendLen; i++)
		 {
			tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
				
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
				
			tmpStr += " " + tmpHex;  
				
		 }
		
		retCode = sendAPDUandDisplay(0, tmpStr);
		if(retCode != ACSModule.SCARD_S_SUCCESS)
			return retCode;
		
		tmpStr="";
		for(int i=0; i<2; i++)
		 {
			tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
				
			tmpStr += " " + tmpHex;  
				
		 }
		
		if(ACOSError(RecvBuff[0], RecvBuff[1]))
			return INVALID_SW1SW2;
		
		if(!tmpStr.trim().equals("90 00"))
		{
			
			displayOut(4, 0, "DEBIT AMOUNT command failed.");
		    displayOut(0, 0, "Return string is invalid. Value: " + tmpStr);
		    return INVALID_SW1SW2;
		    
		}
		
		return retCode;
		
	}
	
	public int creditAmount(byte[] creditData)
	{
		
		String tmpStr="", tmpHex="";
		
		clearBuffers();
		SendBuff[0] = (byte)0x80;
		SendBuff[1] = (byte)0xE2;
		SendBuff[2] = (byte)0x00;
		SendBuff[3] = (byte)0x00;
		SendBuff[4] = (byte)0x0B;
		
		for(int i=0; i<12; i++)
			SendBuff[i+5] = creditData[i];
		
		SendLen = (SendBuff[4] & 0xFF)+5;
		RecvLen[0] = 0x02;
		
		for(int i=0; i<SendLen; i++)
		 {
			tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
				
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
				
			tmpStr += " " + tmpHex;  
				
		 }
		
		retCode = sendAPDUandDisplay(0, tmpStr);
		if(retCode != ACSModule.SCARD_S_SUCCESS)
			return retCode;
		
		tmpStr="";
		for(int i=0; i<2; i++)
		 {
			tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
				
			tmpStr += " " + tmpHex;  
				
		 }
		
		if(ACOSError(RecvBuff[0], RecvBuff[1]))
			return INVALID_SW1SW2;
		
		if(!tmpStr.trim().equals("90 00"))
		{
			
			displayOut(4, 0, "CREDIT AMOUNT command failed.");
		    displayOut(0, 0, "Return string is invalid. Value: " + tmpStr);
		    return INVALID_SW1SW2;
		    
		}
		
		return retCode;
		
	}
	
	public static void DES(byte Data[], byte key[])
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
	            //System.out.println(new String(encryptedContents));
	    
	            for(int i=0;i<8;i++)
	            {
	            	Data[i] = encryptedContents[i];
	        
	            }
	            
	        	//End Encrypt 
	            
	        	//Decrypt
	            for(int i =0; i<8; i++)
	    		{
	    			keyTemp[i] = key[i+8];
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
	
	public int getResponse(byte LE)
	{
		
		String tmpStr="", tmpHex="";
		
		clearBuffers();
		SendBuff[0] = (byte)0x80;
		SendBuff[1] = (byte)0xC0;
		SendBuff[2] = (byte)0x00;
		SendBuff[3] = (byte)0x00;
		SendBuff[4] =  LE;
		SendLen = 5;
		RecvLen[0] = LE + 2;
		
		for(int i=0; i<SendLen; i++)
		 {
			tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
				
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
				
			tmpStr += " " + tmpHex;  
				
		 }
		
		retCode = sendAPDUandDisplay(0, tmpStr);
		if(retCode != ACSModule.SCARD_S_SUCCESS)
			return retCode;
		
		tmpStr="";
		for(int i=0; i<2; i++)
		 {
			tmpHex = Integer.toHexString(((Byte)RecvBuff[i + SendBuff[4]]).intValue() & 0xFF).toUpperCase();
				
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
				
			tmpStr += " " + tmpHex;  
				
		 }
		
		if (ACOSError(RecvBuff[SendBuff[4]], RecvBuff[SendBuff[4]+1]))
			return INVALID_SW1SW2;
		
		if(!tmpStr.trim().equals("90 00"))
		{
			displayOut(4, 0, "GET RESPONSE command failed.");
		    displayOut(0, 0, "Return string is invalid. Value: " + tmpStr);
		    return INVALID_SW1SW2;
		}
		
		return retCode;
		
	}
	
	public int inquireAccount(byte keyNo, byte[] dataIn)
	{
		String tmpStr="", tmpHex="";
		
		clearBuffers();
		SendBuff[0] = (byte)0x80;
		SendBuff[1] = (byte)0xE4;
		SendBuff[2] = keyNo;
		SendBuff[3] = (byte)0x00;
		SendBuff[4] = (byte)0x04;
		
		for(int i=0; i<4; i++)
			SendBuff[i + 5] = dataIn[i];
		
		SendLen = 0x04 + 5;
		RecvLen[0] = 0x02;
		
		for(int i=0; i<SendLen; i++)
		 {
			tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
				
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
				
			tmpStr += " " + tmpHex;  
				
		 }
		
		retCode = sendAPDUandDisplay(0, tmpStr);
		if(retCode != ACSModule.SCARD_S_SUCCESS)
			return retCode;
		
		tmpStr="";
		for(int i=0; i<2; i++)
		 {
			tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
				
			tmpStr += " " + tmpHex;  
				
		 }
		
		if(ACOSError(RecvBuff[0], RecvBuff[1]))
			return INVALID_SW1SW2;
		
		if(!tmpStr.trim().equals("61 19"))
		{
		    displayOut(4, 0, "INQUIRE ACCOUNT command failed.");
		    displayOut(0, 0, "Return string is invalid. Value: " + tmpStr);
		    return INVALID_SW1SW2;
		}
		
		return retCode;
		
	}
	
	public boolean ACOSError(byte SW1, byte SW2)
	{
		
		if((SW1 == (byte)0x62)&&(SW2 == (byte)0x81))
		{
			displayOut(4, 0, "Account data may be corrupted.");
			return true;
		}
		
		if(SW1 == (byte)0x63)
		{
			displayOut(4, 0, "MAC cryptographic checksum is wrong.");
			return true;
		}
		
		if((SW1 == (byte)0x69)&&(SW2 == (byte)0x66))
		{
			displayOut(4, 0, "Command not available or option bit not set.");
			return true;
		}
		
		if((SW1 == (byte)0x69)&&(SW2 == (byte)0x82))
		{
			displayOut(4, 0, "Security status not satisfied. Secret code, IC or PIN not submitted.");
			return true;
		}
		
		if((SW1 == (byte)0x69)&&(SW2 == (byte)0x83))
		{
			displayOut(4, 0, "The specified code is locked.");
			return true;
		}
		
		if((SW1 == (byte)0x69)&&(SW2 == (byte)0x85))
		{
			displayOut(4, 0, "Preceding transaction was not DEBIT or mutual authentication has not been completed.");
			return true;
		}
		
		if((SW1 == (byte)0x69)&&(SW2 == (byte)0xF0))
		{
			displayOut(4, 0, "Data in account is inconsistent. No access unless in Issuer mode.");
			return true;
		}
		
		if((SW1 == (byte)0x6A)&&(SW2 == (byte)0x82))
		{
			displayOut(4, 0, "Account does not exist.");
			return true;
		}
		
		if((SW1 == (byte)0x6A)&&(SW2 == (byte)0x83))
		{
			displayOut(4, 0, "Record not found or file too short.");
			return true;
		}
		
		if((SW1 == (byte)0x6A)&&(SW2 == (byte)0x86))
		{
			displayOut(4, 0, "P1 or P2 is incorrect.");
			return true;
		}
		
		if((SW1 == (byte)0x6B)&&(SW2 == (byte)0x20))
		{
			displayOut(4, 0, "Invalid amount in DEBIT/CREDIT command.");
			return true;
		}
		
		if(SW1 == (byte)0x6C)
		{
			displayOut(4, 0, "Issue GET RESPONSE with P3 = " + Integer.toHexString(SW2) + " to get response data.");
			return true;
		}
		
		if(SW1 == (byte)0x6E)
		{
			displayOut(4, 0, "Unknown INS.");
			return true;
		}
		
		if(SW1 == (byte)0x6F)
		{
			displayOut(4, 0, "Unknown CLA.");
			return true;
		}
		
		if((SW1 == (byte)0x6F)&&(SW2 == (byte)0x10))
		{
			displayOut(4, 0, "Account Transaction Counter at maximum. No more transaction possible.");
			return true;
		}
		
		return false;
		
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

	public boolean validTemplate()
	{
		if(tCredit.getText().length() < maxLen)
		{
			tCredit.selectAll();
			tCredit.requestFocus();
			return false;
		}
		
		if(tDebit.getText().length() < maxLen)
		{
			tDebit.selectAll();
			tDebit.requestFocus();
			return false;
		}
		
		if(tCertify.getText().length() < maxLen)
		{
			tCertify.selectAll();
			tCertify.requestFocus();
			return false;
		}
		
		if(tRevoke.getText().length() < maxLen)
		{
			tRevoke.selectAll();
			tRevoke.requestFocus();
			return false;
		}
		
		return true;
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
		  
		  tmpStr="";
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
		
		String tmpStr="", tmpHex="";
		
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
		String tmpStr="", tmpHex="";
		
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
			
			displayOut(0, 0, "Return string is invalid. Value: " + tmpStr);
			retCode = INVALID_SW1SW2;
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
		bFormat.setEnabled(false);
		bCredit.setEnabled(false);
		bDebit.setEnabled(false);
		bRevoke.setEnabled(false);
		bBalance.setEnabled(false);
		tCredit.setEnabled(false);
		tDebit.setEnabled(false);
		tCertify.setEnabled(false);
		tRevoke.setEnabled(false);
		tAmt.setEnabled(false);
		cbDebitCert.setEnabled(false);
		rbDes.setEnabled(false);
		rb3Des.setEnabled(false);
		tCredit.setText("");
		tDebit.setText("");
		tRevoke.setText("");
		tCertify.setText("");
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
  		if(tAmt.isFocusOwner())
  		{	
  			if (VALIDCHARS.indexOf(x) == -1 ) 
  				ke.setKeyChar(empty);
  		}
  					  
		//Limit character length
			
  	  	
  			if(rbDes.isSelected())
  			{ 
  		
  				if   (((JTextField)ke.getSource()).getText().length() >= maxLen ) {
		
  				ke.setKeyChar(empty);	
  				
  				return;
  				}			
  			}
  		
  			if(rb3Des.isSelected())
  			{ 
  		
  				if   (((JTextField)ke.getSource()).getText().length() >= maxLen ) 
  				{
		
  					ke.setKeyChar(empty);
  					return;
  				}			
  			}  		    	
	}
    
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new ACOS3Account().setVisible(true);
            }
        });
    }



}
