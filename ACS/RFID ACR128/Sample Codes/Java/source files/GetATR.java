/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              GetATR.java

  Description:       This sample program outlines the steps on how to
                     get ATR from cards using ACR128

  Author:            M.J.E.C. Castillo

  Date:              July 14, 2008

  Revision Trail:   (Date/Author/Description)

======================================================================*/

import java.io.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;


public class GetATR extends JFrame implements ActionListener{

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
	int RecvLen = 0;
	int SendLen = 0;
	byte [] SendBuff = new byte[300];
	byte [] RecvBuff = new byte[300];
	byte [] ATRVal = new byte[128];
	byte [] szReaders = new byte[1024];
	
	//GUI Variables
	private JButton bClear;
    private JButton bConn;
    private JButton bGetATR;
    private JButton bInit;
    private JButton bQuit;
    private JButton bReset;
    private JLabel lblReader;
    private JPanel menuPanel;
    private JTextArea Msg;
    private JPanel msgPanel;
    private JScrollPane msgScrollPane;
    private JComboBox rdrName;
    private JPanel readerpanel;
	static JacspcscLoader jacs = new JacspcscLoader();
    

    public GetATR() {
    	
    	this.setTitle("Get ATR");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

   		setSize(700,400);
   		readerpanel = new JPanel();
        lblReader = new JLabel();
        bInit = new JButton();
        bConn = new JButton();
        bGetATR = new JButton();
        menuPanel = new JPanel();
        bClear = new JButton();
        bReset = new JButton();
        bQuit = new JButton();
        msgPanel = new JPanel();
        msgScrollPane = new JScrollPane();
        Msg = new JTextArea();
        
        lblReader.setText("Select Reader");

        String[] rdrNameDef = {"Please select reader                   "};	
		rdrName = new JComboBox(rdrNameDef);
		rdrName.setSelectedIndex(0);
		
        bInit.setText("Initialize");

        bConn.setText("Connect");

        bGetATR.setText("Get ATR");

        GroupLayout readerpanelLayout = new GroupLayout(readerpanel);
        readerpanel.setLayout(readerpanelLayout);
        readerpanelLayout.setHorizontalGroup(
            readerpanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(readerpanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(readerpanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(GroupLayout.Alignment.TRAILING, readerpanelLayout.createSequentialGroup()
                        .addComponent(lblReader)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(rdrName, 0, 175, Short.MAX_VALUE))
                    .addGroup(GroupLayout.Alignment.TRAILING, readerpanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                        .addComponent(bGetATR, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(bConn, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(bInit, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, 125, Short.MAX_VALUE)))
                .addContainerGap())
        );
        readerpanelLayout.setVerticalGroup(
            readerpanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(readerpanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(readerpanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblReader)
                    .addComponent(rdrName, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(bInit)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bConn)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bGetATR)
                .addContainerGap(18, Short.MAX_VALUE))
        );

        bClear.setText("Clear Screen");

        bReset.setText("Reset");
        
        bQuit.setText("Quit");

        GroupLayout menuPanelLayout = new GroupLayout(menuPanel);
        menuPanel.setLayout(menuPanelLayout);
        menuPanelLayout.setHorizontalGroup(
            menuPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(GroupLayout.Alignment.TRAILING, menuPanelLayout.createSequentialGroup()
                .addContainerGap(132, Short.MAX_VALUE)
                .addGroup(menuPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(bClear, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bReset, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bQuit, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, 124, Short.MAX_VALUE))
                .addContainerGap())
        );
        menuPanelLayout.setVerticalGroup(
            menuPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(GroupLayout.Alignment.TRAILING, menuPanelLayout.createSequentialGroup()
                .addContainerGap(56, Short.MAX_VALUE)
                .addComponent(bClear)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bReset)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bQuit)
                .addContainerGap())
        );

        Msg.setColumns(20);
        Msg.setRows(5);
        msgScrollPane.setViewportView(Msg);

        GroupLayout msgPanelLayout = new GroupLayout(msgPanel);
        msgPanel.setLayout(msgPanelLayout);
        msgPanelLayout.setHorizontalGroup(
            msgPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addComponent(msgScrollPane, GroupLayout.DEFAULT_SIZE, 272, Short.MAX_VALUE)
        );
        msgPanelLayout.setVerticalGroup(
            msgPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addComponent(msgScrollPane, GroupLayout.DEFAULT_SIZE, 273, Short.MAX_VALUE)
        );

        GroupLayout layout = new GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                    .addComponent(menuPanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(readerpanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(msgPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(msgPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(readerpanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(menuPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)))
                .addContainerGap())
        );
        
        bInit.addActionListener(this);
        bConn.addActionListener(this);
        bGetATR.addActionListener(this);
        bClear.addActionListener(this);
        bReset.addActionListener(this);
        bQuit.addActionListener(this);
        
        
    }

	public void actionPerformed(ActionEvent e) 
	{
		
		if (bReset == e.getSource()) 
		{
			
			//disconnect
			if (connActive){
				
				retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
				connActive= false;
			
			}
		    
			//release context
			retCode = jacs.jSCardReleaseContext(hContext);
			//System.exit(0);
			
			Msg.setText("");
			initMenu();
			rdrName.removeAllItems();
			rdrName.addItem("Please select reader                   ");
			
		}
		
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
			{
			
				rdrName.addItem("No PC/SC reader detected");
				
			}
			      	
			rdrName.setSelectedIndex(0);
			bConn.setEnabled(true);
			bGetATR.setEnabled(true);
			bInit.setEnabled(false);
			bReset.setEnabled(true);
			
			//Look for ACR128 SAM and make it the default reader in the combobox
			for (int i = 0; i < cchReaders[0]-1; i++)
			{
				
				rdrName.setSelectedIndex(i);
				
				if (((String) rdrName.getSelectedItem()).lastIndexOf("ACR128U SAM")> -1)
				{
					
					break;
					
				}
				
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
			
		}
		
		if(bGetATR == e.getSource())
		{
		
			int tmpWord;
			int[] state = new int[1];
			int[] readerLen = new int[1];
			String tmpStr;

			displayOut(0, 0, "Invoke SCardStatus");
			//1. Invoke SCardStatus using hCard handle
		    //   and valid reader name
			state[0]=0;
			readerLen[0]=0;
			for(int i=0; i<128; i++)
				ATRVal[i] = 0;
			
		    tmpWord = 32;
		    ATRLen[0] = tmpWord;
		    String rdrcon= (String)rdrName.getSelectedItem();  
		    
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
		    
   
		   // Msg.append(""+retCode);
		    
		    
		    if (retCode != ACSModule.SCARD_S_SUCCESS)
		    {
		    	
		    	displayOut(1, retCode, "");
		    	
		    }
		    else
		    {
		    	
		    	//2. Format ATRVal returned and display string as ATR value
		    	//tmpStr = "ATR Length: " & CInt(ATRLen)
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
		    	
		    	//3. Interpret dwActProtocol returned and display as active protocol
		    	tmpStr = "Active Protocol: ";
		    	
		    	switch(PrefProtocols[0])
		    	{
		    	
		    		case 1: tmpStr = tmpStr + "T=0"; break;
		    		case 2:
		    		{
		    			if (((String) rdrName.getSelectedItem()).lastIndexOf("ACR128U PICC")> -1)
		    				tmpStr = tmpStr + "T=CL"; 
		    			else
		    				tmpStr = tmpStr + "T=1"; 
		    			
		    			break;
		    		}
		    		default: tmpStr = "No protocol is defined."; break;
		    	
		    	}
		    	
		    	displayOut(3, 0, tmpStr);
		    	interpretATR();
		    	
		    }
		    
		}
		
		if (bClear == e.getSource())
		{
	
			Msg.setText("");
			
		}
		
		if(bQuit == e.getSource())
		{
			
			this.dispose();
			
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
	
	public void interpretATR()
	{
		
		String RIDVal, cardName;
		cardName= "";
		
		//4. Interpret ATR and guess card
	    // 4.1. Mifare cards using ISO 14443 Part 3 Supplemental Document
		if (ATRLen[0]>14){
			
			RIDVal = "";
			for(int i=7; i<11; i++)
			{
				
				RIDVal = RIDVal + Integer.toHexString(((Byte)ATRVal[i]).intValue() & 0xFF);
				
			}
			
				if (RIDVal=="A000000306")
				{
				
					cardName = "";
					
					switch(ATRVal[12])
					{
					
						case 0: cardName = "No card information"; break;
						case 1: cardName = "ISO 14443 A, Part1 Card Type"; break;
						case 2: cardName = "ISO 14443 A, Part2 Card Type"; break;
						case 3: cardName = "ISO 14443 A, Part3 Card Type"; break;
						case 5: cardName = "ISO 14443 B, Part1 Card Type"; break;
						case 6: cardName = "ISO 14443 B, Part2 Card Type"; break;
						case 7: cardName = "ISO 14443 B, Part3 Card Type"; break;
						case 9: cardName = "ISO 15693, Part1 Card Type"; break;
						case 10: cardName = "ISO 15693, Part2 Card Type"; break;
						case 11: cardName = "ISO 15693, Part3 Card Type"; break;
						case 12: cardName = "ISO 15693, Part4 Card Type"; break;
						case 13: cardName = "Contact Card (7816-10) IIC Card Type"; break;
						case 14: cardName = "Contact Card (7816-10) Extended IIC Card Type"; break;
						case 15: cardName = "Contact Card (7816-10) 2WBP Card Type"; break;
						case 16: cardName = "Contact Card (7816-10) 3WBP Card Type"; break;
						default: cardName = "Undefined card"; break;
					
					}
					
				}
				//Msg.append(""+Integer.toHexString(((Byte)ATRVal[12]).intValue() & 0xFF));
				if (Integer.toHexString(((Byte)ATRVal[12]).intValue() & 0xFF)=="3")
				{
					
					if(Integer.toHexString(((Byte)ATRVal[13]).intValue() & 0xFF)=="0")
					{
					
						switch(ATRVal[14])
						{
						
							case 0x01: cardName = cardName + ": Mifare Standard 1K"; break;
	                        case 0x02: cardName = cardName + ": Mifare Standard 4K"; break;
	                        case 0x03: cardName = cardName + ": Mifare Ultra light"; break;
	                        case 0x04: cardName = cardName + ": SLE55R_XXXX"; break;
	                        case 0x06: cardName = cardName + ": SR176"; break;
	                        case 0x07: cardName = cardName + ": SRI X4K"; break;
	                        case 0x08: cardName = cardName + ": AT88RF020"; break;
	                        case 0x09: cardName = cardName + ": AT88SC0204CRF"; break;
	                        case 0x0A: cardName = cardName + ": AT88SC0808CRF"; break;
	                        case 0x0B: cardName = cardName + ": AT88SC1616CRF"; break;
                            case 0x0C: cardName = cardName + ": AT88SC3216CRF"; break;
	                        case 0x0D: cardName = cardName + ": AT88SC6416CRF"; break;
                            case 0x0E: cardName = cardName + ": SRF55V10P"; break;
                            case 0x0F: cardName = cardName + ": SRF55V02P"; break;
                            case 0x10: cardName = cardName + ": SRF55V10S"; break;
                            case 0x11: cardName = cardName + ": SRF55V02S"; break;
                            case 0x12: cardName = cardName + ": TAG IT"; break;
                            case 0x13: cardName = cardName + ": LRI512"; break;
                            case 0x14: cardName = cardName + ": ICODESLI"; break;
                            case 0x15: cardName = cardName + ": TEMPSENS"; break;
                            case 0x16: cardName = cardName + ": I.CODE1"; break;
                            case 0x17: cardName = cardName + ": PicoPass 2K"; break;
                            case 0x18: cardName = cardName + ": PicoPass 2KS"; break;
                            case 0x19: cardName = cardName + ": PicoPass 16K"; break;
                            case 0x1A: cardName = cardName + ": PicoPass 16KS"; break;
	                        case 0x1B: cardName = cardName + ": PicoPass 16K(8x2)";break;
	                        case 0x1C: cardName = cardName + ": PicoPass 16KS(8x2)";break;
	                        case 0x1D: cardName = cardName + ": PicoPass 32KS(16+16)";break;
	                        case 0x1E: cardName = cardName + ": PicoPass 32KS(16+8x2)";break;
	                        case 0x1F: cardName = cardName + ": PicoPass 32KS(8x2+16)";break;
	                        case 0x20: cardName = cardName + ": PicoPass 32KS(8x2+8x2)";break;
	                        case 0x21: cardName = cardName + ": LRI64";break;
	                        case 0x22: cardName = cardName + ": I.CODE UID";break;
	                        case 0x23: cardName = cardName + ": I.CODE EPC";break;
	                        case 0x24: cardName = cardName + ": LRI12";break;
	                        case 0x25: cardName = cardName + ": LRI128";break;
	                        case 0x26: cardName = cardName + ": Mifare Mini";break;
						
						}
						
					}
					else
					{
						
						if (ATRVal[13]==0xFF)
						{
							
							switch(ATRVal[14])
							{
							
								case 9: cardName = cardName + ": Mifare Mini"; break;
							
							}
							
						}
						
					}
					
					displayOut(3, 0, cardName + " is detected.");
					
				}
		
		}
		
		if(ATRLen[0] ==11)
		{
			
			RIDVal = "";
			
			for(int i=4; i<9; i++)
			{
				
				RIDVal = RIDVal + Integer.toHexString(((Byte)ATRVal[i]).intValue() & 0xFF);
				
			}
			
			if (RIDVal.equals("067577810280"))
			{
				
				displayOut(3, 0, "Mifare DESFire is detected.");
				
			}
			
		}
		
		if(ATRLen[0] ==17)
		{
			
RIDVal = "";
			
			for(int i=4; i<15; i++)
			{
				
				RIDVal = RIDVal + Integer.toHexString(((Byte)ATRVal[i]).intValue() & 0xFF);
				
			}
			
			if (RIDVal.equals("50122345561253544E3381C3"))
			{
				
				displayOut(3, 0, "ST19XRC8E is detected.");
				
			}
			
		}
		
		
	}
	
	public void initMenu()
	{
	
		connActive = false;
		bConn.setEnabled(false);
		bGetATR.setEnabled(false);
		bReset.setEnabled(false);
		bInit.setEnabled(true);
		displayOut(0, 0, "Program Ready");
		
	}
    
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new GetATR().setVisible(true);
            }
        });
    }



}
