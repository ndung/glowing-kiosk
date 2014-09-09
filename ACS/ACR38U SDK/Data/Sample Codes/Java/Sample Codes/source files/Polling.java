/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              Polling.java

  Description:       This sample program outlines the steps on how to
                     use the ACOS card for the Polling.
                    
  Author:            M.J.E.C. Castillo

  Date:              September 1, 2008

  Revision Trail:   (Date/Author/Description)

======================================================================*/

import java.io.*;
import java.sql.Time;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;


public class Polling extends JFrame implements ActionListener{

	//JPCSC Variables
	int retCode, maxLen;
	boolean connActive; 
	public static final int INVALID_SW1SW2 = -450;
	static String VALIDCHARS = "ABCDEFabcdef0123456789";
	
	Timer timer;
	
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
    private JButton bEnd;
    private JButton bInit;
    private JButton bQuit;
    private JButton bReset;
    private JButton bStart;
    private JComboBox cbReader;
    private JLabel lblReader;
    private JTextField tStatus;
	
	static JacspcscLoader jacs = new JacspcscLoader();
    

    public Polling() {
    	
    	this.setTitle("Polling");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

		//GUI Variables
		setSize(270,300);
        lblReader = new JLabel();
        cbReader = new JComboBox();
        bInit = new JButton();
        bReset = new JButton();
        bStart = new JButton();
        bEnd = new JButton();
        bQuit = new JButton();
        tStatus = new JTextField();
        
        lblReader.setText("Select Reader");

		String[] rdrNameDef = {"Please select reader             "};	
		cbReader = new JComboBox(rdrNameDef);
		cbReader.setSelectedIndex(0);
		
        lblReader.setText("Select Reader");

        cbReader.setModel(new DefaultComboBoxModel(new String[] { "Select Reader" }));

        bInit.setText("Initalize");

        bReset.setText("Reset");

        bStart.setText("Start Polling");

        bEnd.setText("End Polling");

        bQuit.setText("Quit");

        tStatus.setBackground(new java.awt.Color(204, 204, 204));
        tStatus.setDisabledTextColor(new java.awt.Color(0, 0, 0));

        GroupLayout layout = new GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addGap(10, 10, 10)
                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                            .addGroup(GroupLayout.Alignment.LEADING, layout.createSequentialGroup()
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(lblReader)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(cbReader, GroupLayout.PREFERRED_SIZE, 144, GroupLayout.PREFERRED_SIZE))
                            .addGroup(layout.createSequentialGroup()
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, 93, Short.MAX_VALUE)
                                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                                    .addComponent(bReset, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                    .addComponent(bInit, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, 122, Short.MAX_VALUE)
                                    .addComponent(bStart, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                    .addComponent(bEnd, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                    .addComponent(bQuit, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)))))
                    .addGroup(layout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(tStatus, GroupLayout.DEFAULT_SIZE, 215, Short.MAX_VALUE)))
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblReader)
                    .addComponent(cbReader, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addGap(18, 18, 18)
                .addComponent(bInit)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bStart)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bEnd)
                .addGap(8, 8, 8)
                .addComponent(bReset)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(bQuit)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(tStatus, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        
        bInit.setMnemonic(KeyEvent.VK_I);
        bReset.setMnemonic(KeyEvent.VK_R);
        bQuit.setMnemonic(KeyEvent.VK_Q);
        bStart.setMnemonic(KeyEvent.VK_S);
        bEnd.setMnemonic(KeyEvent.VK_E);

        bInit.addActionListener(this);
        bReset.addActionListener(this);
        bQuit.addActionListener(this);
        bStart.addActionListener(this);
        bEnd.addActionListener(this);
        
    }

	public void actionPerformed(ActionEvent e) 
	{
		
		if(bInit == e.getSource())
		{
			
			//1. Establish context and obtain hContext handle
			retCode = jacs.jSCardEstablishContext(ACSModule.SCARD_SCOPE_USER, 0, 0, hContext);
		    
			if (retCode != ACSModule.SCARD_S_SUCCESS)
		    {
		    
				tStatus.setText(ACSModule.GetScardErrMsg(retCode));
		      	return;
		      	
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
			bInit.setEnabled(false);
			bReset.setEnabled(true);
			bStart.setEnabled(true);
			tStatus.setText("Ready for card detection polling");
			
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

			initMenu();
			cbReader.removeAllItems();
			cbReader.addItem("Please select reader                   ");
			timer.stop();
			
		}
		
		if(bStart == e.getSource())
		{
			
			tStatus.setText("");
			bStart.setEnabled(false);
			bEnd.setEnabled(true);
			
			timer = new Timer(500, pollTimer);
			timer.start();
			
		}
		
		if(bEnd == e.getSource())
		{
			
			bStart.setEnabled(true);
			bEnd.setEnabled(false);
			timer.stop();
			tStatus.setText("Card detection polling terminated");
			
		}
		
				
	}
	
	//timer for automatic polling
	ActionListener pollTimer = new ActionListener() {
	      public void actionPerformed(ActionEvent evt) {
	    	
	    	  if(checkCard())
	    		  tStatus.setText("Card inserted");
	    	  else
	    		  tStatus.setText("card removed");
		
		}
	};
    

	public boolean checkCard()
	{
	
		ACSModule.SCARD_READERSTATE readerState = new ACSModule.SCARD_READERSTATE();
		
		String rdrcon= (String)cbReader.getSelectedItem();  
		
		readerState.RdrName = rdrcon;
	      
  	    retCode = jacs.jSCardGetStatusChange(hContext, 
  	    						0, 
  	    						readerState, 
  	    						1); 
  	  					
  	  //JOptionPane.showMessageDialog(this, retCode);
  	    if(retCode != ACSModule.SCARD_S_SUCCESS)
  		{
  	    	
  	    	tStatus.setText(ACSModule.GetScardErrMsg(retCode));
  	    	return false;
  	    	
  		}
  	    else
  	    {
  	    	if((readerState.RdrEventState/32)%2!=0)
  	    		return true;
  	    	else
  	    		return false;
  	    	
  	    }
		
		
	}

	
	public void initMenu()
	{
		
		connActive = false;
		bInit.setEnabled(true);
		bReset.setEnabled(false);
		bStart.setEnabled(false);
		bEnd.setEnabled(false);
		tStatus.setText("Program Ready");
		tStatus.setEnabled(false);
		
	}
	
   
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new Polling().setVisible(true);
            }
        });
    }



}
