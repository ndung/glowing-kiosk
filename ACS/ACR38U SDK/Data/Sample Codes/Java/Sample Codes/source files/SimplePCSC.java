/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              GetATR.java

  Description:       This sample program outlines the steps on using PCSC commands.
                    
  Author:            M.J.E.C. Castillo

  Date:              August 29, 2008

  Revision Trail:   (Date/Author/Description)

======================================================================*/

import java.io.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class SimplePCSC extends JFrame implements ActionListener{

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
    private JButton bClear;
    private JButton bQuit;
    private JButton bSCardBeginTransaction;
    private JButton bSCardConnect;
    private JButton bSCardDisconnect;
    private JButton bSCardEndTransaction;
    private JButton bSCardEstablishContext;
    private JButton bSCardReleaseContext;
    private JButton bSCardStatus;
    private JButton bSCardTransmit;
    private JButton bScardListReaders;
    private JComboBox cbReader;
    private JScrollPane scrPanelMsg;
    private JTextArea mMsg;
    private JLabel lblAPDUInput;
    private JLabel lblInput;
    private JLabel lblReader;
    private JTextField tAPDUInput;
    
	static JacspcscLoader jacs = new JacspcscLoader();
    

    public SimplePCSC() {
    	
    	this.setTitle("Simple PCSC");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

   		setSize(610,415);
        lblReader = new JLabel();
        bSCardEstablishContext = new JButton();
        bScardListReaders = new JButton();
        bSCardConnect = new JButton();
        bSCardBeginTransaction = new JButton();
        bSCardStatus = new JButton();
        bSCardTransmit = new JButton();
        bSCardEndTransaction = new JButton();
        bSCardDisconnect = new JButton();
        bSCardReleaseContext = new JButton();
        lblAPDUInput = new JLabel();
        tAPDUInput = new JTextField();
        lblInput = new JLabel();
        scrPanelMsg = new JScrollPane();
        mMsg = new JTextArea();
        bQuit = new JButton();
        bClear = new JButton();
        
        lblReader.setText("Select Reader");

        String[] rdrNameDef = {"Please select reader "};	
		cbReader = new JComboBox(rdrNameDef);
		cbReader.setSelectedIndex(0);
		
        bSCardEstablishContext.setText("SCardEstablishContext");

        bScardListReaders.setText("SCardListReaders");

        bSCardConnect.setText("SCardConnect");

        bSCardBeginTransaction.setText("SCardBeginTransaction");

        bSCardStatus.setText("SCardStatus");

        bSCardTransmit.setText("SCardTransmit");

        bSCardEndTransaction.setText("SCardEndTransaction");

        bSCardDisconnect.setText("SCardDisconnect");

        bSCardReleaseContext.setText("SCardReleaseContext");

        lblAPDUInput.setText("APDU Input");

        lblInput.setText("(use HEX values only)");

        mMsg.setColumns(20);
        mMsg.setRows(5);
        scrPanelMsg.setViewportView(mMsg);

        bQuit.setText("Quit");

        bClear.setText("Clear");

        GroupLayout layout = new GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(lblReader)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(cbReader, 0, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bScardListReaders, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bSCardEstablishContext, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bSCardConnect, GroupLayout.DEFAULT_SIZE, 145, Short.MAX_VALUE)
                    .addComponent(bSCardBeginTransaction, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bSCardStatus, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bSCardTransmit, GroupLayout.DEFAULT_SIZE, 145, Short.MAX_VALUE)
                    .addComponent(bSCardEndTransaction, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bSCardDisconnect, GroupLayout.DEFAULT_SIZE, 145, Short.MAX_VALUE)
                    .addComponent(bSCardReleaseContext, GroupLayout.DEFAULT_SIZE, 145, Short.MAX_VALUE))
                .addGap(18, 18, 18)
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addGap(20, 20, 20)
                        .addComponent(bClear, GroupLayout.PREFERRED_SIZE, 101, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bQuit, GroupLayout.PREFERRED_SIZE, 107, GroupLayout.PREFERRED_SIZE)
                        .addGap(25, 25, 25))
                    .addComponent(scrPanelMsg, GroupLayout.PREFERRED_SIZE, 268, GroupLayout.PREFERRED_SIZE)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(lblAPDUInput)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                            .addComponent(lblInput)
                            .addComponent(tAPDUInput, GroupLayout.PREFERRED_SIZE, 199, GroupLayout.PREFERRED_SIZE))))
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblReader)
                    .addComponent(lblAPDUInput)
                    .addComponent(tAPDUInput, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(cbReader, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(lblInput)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(scrPanelMsg, GroupLayout.DEFAULT_SIZE, 201, Short.MAX_VALUE))
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(bSCardEstablishContext)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bScardListReaders)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bSCardConnect)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bSCardBeginTransaction)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bSCardStatus)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bSCardTransmit)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bSCardEndTransaction)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bSCardDisconnect)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bSCardReleaseContext)))
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(bClear)))
                    .addGroup(GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bQuit)))
                .addContainerGap())
        );
        
        bSCardEstablishContext.addActionListener(this);
        bQuit.addActionListener(this);
        bClear.addActionListener(this);
        bScardListReaders.addActionListener(this);
        bSCardConnect.addActionListener(this);
        bSCardBeginTransaction.addActionListener(this);
        bSCardStatus.addActionListener(this);
        bSCardTransmit.addActionListener(this);
        bSCardEndTransaction.addActionListener(this);
        bSCardDisconnect.addActionListener(this);
        bSCardReleaseContext.addActionListener(this);
           
    }

	public void actionPerformed(ActionEvent e) 
	{
		
		if(bSCardEstablishContext == e.getSource())
		{
			
			//Establish context and obtain hContext handle
			retCode = jacs.jSCardEstablishContext(ACSModule.SCARD_SCOPE_USER, 0, 0, hContext);
		    
			if (retCode != ACSModule.SCARD_S_SUCCESS)
		    {
		    
				mMsg.append("Calling SCardEstablishContext...FAILED\n");
		      	displayOut(1, retCode, "");
		      	
		    }
			else
			{
				
				mMsg.append("SCardEstablishContext SUCCESS\n");
				
			}
			
			bSCardEstablishContext.setEnabled(false);
			bScardListReaders.setEnabled(true);
			bSCardReleaseContext.setEnabled(true);
			bClear.setEnabled(true);
			
		}
		
		if(bScardListReaders == e.getSource())
		{
			
			//List PC/SC card readers installed in the system
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
			{
			
				cbReader.addItem("No reader detected");
				return;
				
			}
			else
			{
				
				mMsg.append("SCardListReaders SUCCESS\n");
				
			}
			
			bSCardConnect.setEnabled(true);
			
		}
		
		if(bSCardConnect == e.getSource())
		{
			
			String rdrcon = (String)cbReader.getSelectedItem();  	      	      	
		    
		    retCode = jacs.jSCardConnect(hContext, 
		    							rdrcon, 
		    							ACSModule.SCARD_SHARE_EXCLUSIVE,
		    							ACSModule.SCARD_PROTOCOL_T1 | ACSModule.SCARD_PROTOCOL_T0,
		      							hCard, 
		      							PrefProtocols);
		    
		    if (retCode != ACSModule.SCARD_S_SUCCESS)
		    {
		    
		    	displayOut(1, retCode, "");
		    	return;
		    	
		    } 
		    else 
		    {	      	      
		      	
		    	displayOut(0, 0, "Successful connection to " + (String)cbReader.getSelectedItem());
		      	
		    }
		    
		    bScardListReaders.setEnabled(false);
		    bSCardConnect.setEnabled(false);
		    bSCardBeginTransaction.setEnabled(true);
		    bSCardDisconnect.setEnabled(true);
		    bSCardReleaseContext.setEnabled(true);
			
		}
		
		if(bSCardBeginTransaction == e.getSource())
		{
			
			retCode = jacs.jSCardBeginTransaction(hCard);
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
			{
				
				displayOut(1, retCode, "");
				return;
				
			}
			else
			{
				
				displayOut(0, 0, "SCardBeginTransaction SUCCESS");
				
			}
			
			tAPDUInput.setEnabled(true);
			bSCardBeginTransaction.setEnabled(false);
			bSCardStatus.setEnabled(true);
			bSCardTransmit.setEnabled(true);
			bSCardEndTransaction.setEnabled(true);
			bSCardDisconnect.setEnabled(true);
			
		}
		
		if (bSCardStatus == e.getSource())
		{
		
			int tmpWord;
			int[] state = new int[1];
			int[] readerLen = new int[1];
			String tmpStr="", tmpHex="";
			
			state[0]=0;
			readerLen[0]=0;
			for(int i=0; i<128; i++)
				ATRVal[i] = 0;
			
		    tmpWord = 32;
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
		    	return;
		    	
		    }
		    else
		    {
		    
		    	displayOut(0,0, "SCardStatus SUCCESS");
		    	
		    }
		    
		    for(int i =0; i<ATRLen[0]; i++){
				  tmpHex = Integer.toHexString(((Byte)ATRVal[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex;  
			 }
		    
		    displayOut(0, 0, tmpStr);
		    
		    //interpret state returned and display state
		    switch(state[0])
		    {
		    
		    case 0: tmpStr = "SCARD_UNKNOWN";
		    case 1: tmpStr = "SCARD_ABSENT";
		    case 2: tmpStr = "SCARD_PRESENT";
		    case 3: tmpStr = "SCARD_SWALLOWED";
		    case 4: tmpStr = "SCARD_POWERED";
		    case 5: tmpStr = "SCARD_NEGOTIABLE";
		    case 6: tmpStr = "SCARD_SPECIFIC";
		    
		    }
		    
		    displayOut(0, 0, "Reader State: "+tmpStr);
		    
		    //interpret protocol returned and display as active protocol
		    switch(PrefProtocols[0]){
		    
		    case 1: tmpStr = "T=0";
		    case 2: tmpStr = "T=1";
		    
		    }
		    
		    displayOut(0, 0, "Active Protocol: "+tmpStr);
			
		}	 
		
		if(bSCardTransmit == e.getSource())
		{
			
			String tmpStr="", tmpVal="", tmpHex="";
			boolean diFlag=false;
			
			if(tAPDUInput.getText().equals(""))
			{
			
				displayOut(0, 0, "no data input");
				tAPDUInput.requestFocus();
				return;
			
			}
			
			tmpVal = tAPDUInput.getText().trim();
			
			for(int i=0; i<tmpVal.length(); i++)
			{
				
				if(tmpVal.charAt(i) != (char)32)
				{
					tmpStr = tmpStr + tmpVal.charAt(i);
				}
				
			}
			
			if((tmpStr.length() % 2)!=0)
			{
				diFlag = true;
			}
			
			if( tmpStr.length() < 10 )
			{
				
				displayOut(0, 0, "Insufficient data input\n");
				tAPDUInput.requestFocus();
				return;
				
			}
			
			if (diFlag)
			{
				
				displayOut(0, 0, "Invalid data input, uneven number of characters");
				tAPDUInput.requestFocus();
				return;
				
			}
			
			clearBuffers();
			for(int i=0; i<4; i++)
			{
				
				tmpVal= tmpVal + tmpStr.charAt(i*2) + tmpStr.charAt((i*2)+1);
				SendBuff[i] = (byte)((Integer)Integer.parseInt(tmpVal, 16)).byteValue();
				
			}
			
			//if APDU length < 6 then P3 is Le
			if (tmpStr.length()<12)
			{
				
				for(int i=0; i<4; i++)
				{
					
					tmpVal= tmpVal + tmpStr.charAt(i*2) + tmpStr.charAt((i*2)+1);
					SendBuff[i] = (byte)((Integer)Integer.parseInt(tmpVal, 16)).byteValue();
					
				}
				
				SendLen = 5;
				RecvLen[0] = (SendBuff[4] & 0xFF) +2;
				
				tmpStr="";
				for(int i =0; i<4; i++){
					  tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
						
						//For single character hex
						if (tmpHex.length() == 1) 
							tmpHex = "0" + tmpHex;
						
						tmpStr += " " + tmpHex;  
				 }
			    
			    displayOut(0, 0, tmpStr);
			    
			    retCode = sendAPDUandDisplay();
			    if(retCode == ACSModule.SCARD_S_SUCCESS)
			    {
			    	
			    	tmpStr="";
			    	tmpHex="";
					for(int i =0; i<RecvLen[0]; i++){
						  tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
							
							//For single character hex
							if (tmpHex.length() == 1) 
								tmpHex = "0" + tmpHex;
							
							tmpStr += " " + tmpHex;  
					 }
				    
				    displayOut(0, 0, tmpStr);
			    	
			    }
				
			}
			else
			{
				
				for(int i=0; i<4; i++)
				{
					
					tmpVal= tmpVal + tmpStr.charAt(i*2) + tmpStr.charAt((i*2)+1);
					SendBuff[i] = (byte)((Integer)Integer.parseInt(tmpVal, 16)).byteValue();
					
				}
				
				SendLen = (SendBuff[4] & 0xFF) + 5;
				if(tmpStr.length()<(SendLen*2))
				{
					
					displayOut(0, 0, "Invalid data input, insufficient data length");
					tAPDUInput.requestFocus();
					return;
					
				}
				
				for(int i=0; i<SendBuff[4]; i++)
				{
					
					tmpVal= tmpVal + tmpStr.charAt(i*2) + tmpStr.charAt((i*2)+1);
					SendBuff[i] = (byte)((Integer)Integer.parseInt(tmpVal, 16)).byteValue();
					
				}
				
				RecvLen[0] = 2;
				
				tmpStr="";
		    	tmpHex="";
				for(int i =0; i<SendLen; i++){
					  tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
						
						//For single character hex
						if (tmpHex.length() == 1) 
							tmpHex = "0" + tmpHex;
						
						tmpStr += " " + tmpHex;  
				 }
			    
			    displayOut(0, 0, tmpStr);
			    
			    retCode = sendAPDUandDisplay();
			    tmpStr="";
		    	tmpHex="";
				for(int i =0; i<RecvLen[0]; i++){
					  tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
						
						//For single character hex
						if (tmpHex.length() == 1) 
							tmpHex = "0" + tmpHex;
						
						tmpStr += " " + tmpHex;  
				 }
			    
			    displayOut(0, 0, tmpStr);
				
			}
			
		}
		
		if(bSCardEndTransaction == e.getSource())
		{
			
			retCode = jacs.jSCardEndTransaction(hCard, ACSModule.SCARD_LEAVE_CARD);
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
			{
				
				displayOut(1, retCode, "");
				return;
				
			}
			
			bSCardBeginTransaction.setEnabled(false);
			bSCardStatus.setEnabled(false);
			bSCardTransmit.setEnabled(false);
			bSCardEndTransaction.setEnabled(false);
			bSCardDisconnect.setEnabled(true);
			tAPDUInput.setText("");
			tAPDUInput.setEnabled(false);
			
		}
		
		if(bSCardDisconnect == e.getSource())
		{
			
			retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
			{
				
				displayOut(1, retCode, "");
				return;
				
			}
			else
				displayOut(0, 0, "SCardDisconnect SUCCESS");
			
			bScardListReaders.setEnabled(true);
			bSCardConnect.setEnabled(true);
			bSCardBeginTransaction.setEnabled(false);
			bSCardDisconnect.setEnabled(false);
			bSCardReleaseContext.setEnabled(true);
			
		}
		
		if (bSCardReleaseContext == e.getSource())
		{
			
			retCode = jacs.jSCardReleaseContext(hContext);
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
			{
				
				displayOut(1, retCode, "");
				return;
				
			}
			else
				displayOut(0, 0, "SCardRelease Context SUCCESS");
			
			bSCardEstablishContext.setEnabled(true);
			bScardListReaders.setEnabled(false);
			bSCardConnect.setEnabled(false);
			bSCardReleaseContext.setEnabled(false);
			cbReader.removeAllItems();
			
		}
		
		if (bClear == e.getSource())
		{
	
			mMsg.setText("");
			
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
	
	
	public int sendAPDUandDisplay()
	{
		
		ACSModule.SCARD_IO_REQUEST IO_REQ = new ACSModule.SCARD_IO_REQUEST(); 
		ACSModule.SCARD_IO_REQUEST IO_REQ_Recv = new ACSModule.SCARD_IO_REQUEST(); 
		IO_REQ.dwProtocol = PrefProtocols[0];
		IO_REQ.cbPciLength = 8;
		IO_REQ_Recv.dwProtocol = PrefProtocols[0];
		IO_REQ_Recv.cbPciLength = 8;
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
		bSCardEstablishContext.setEnabled(true);
		bScardListReaders.setEnabled(false);
		bSCardConnect.setEnabled(false);
		bSCardBeginTransaction.setEnabled(false);
		bSCardStatus.setEnabled(false);
		bSCardTransmit.setEnabled(false);
		bSCardEndTransaction.setEnabled(false);
		bSCardDisconnect.setEnabled(false);
		bSCardReleaseContext.setEnabled(false);
		bClear.setEnabled(false);
		displayOut(0, 0, "Program Ready");
		tAPDUInput.setText("");
		tAPDUInput.setEnabled(false);
	}
    
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new SimplePCSC().setVisible(true);
            }
        });
    }



}
