/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              pollingMain.java

  Description:       This sample program outlines the steps on how to
                     execute card detection polling functions using ACR128

  Author:            M.J.E.C. Castillo

  Date:              July 3, 2008

  Revision Trail:   (Date/Author/Description)

======================================================================*/

import java.io.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;


public class polling extends JFrame implements ActionListener{

	//JPCSC Variables
	int retCode, pollCase;
	boolean connActive, autoDet, dualPoll; 
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
	
	//GUI Variables
    private JButton bAutoDet, bClear, bQuit, bConn, bInit, bManDet, bReadCurr, bReadPoll, bReset, bSetCurr, bSetPoll;
    private ButtonGroup bgConf, bgCurr, bgPollInt;
    private JPanel bottomPanel, confPanel, currModePanel, midPanel, msgPanel, pollIntPanel, pollOptPanel, rdrPanel, statPanel;
    private JCheckBox cbPollOpt1,cbPollOpt2, cbPollOpt3, cbPollOpt4, cbPollOpt5, cbPollOpt6;
    private JComboBox cbReader;
    private JLabel lblReader;
    private JTextArea mMsg;
    private JRadioButton rbBoth, rbEither, rbExAct, rbExNotAct, rbPollOpt1, rbPollOpt2, rbPollOpt3, rbPollOpt4;
    private JScrollPane scrlMsg;
    private JTextField tStat1, tStat2, tStat3, tStat4;
    // End of variables declaration
	
	static JacspcscLoader jacs = new JacspcscLoader();
    

    public polling() {
    	
    	this.setTitle("Polling");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

		setSize(810,750);
		bgConf = new ButtonGroup();
        bgCurr = new ButtonGroup();
        bgPollInt = new ButtonGroup();
        rdrPanel = new JPanel();
        lblReader = new JLabel();
        cbReader = new JComboBox();
        bInit = new JButton();
        bQuit = new JButton();
        bConn = new JButton();
        midPanel = new JPanel();
        confPanel = new JPanel();
        rbBoth = new JRadioButton();
        rbEither = new JRadioButton();
        currModePanel = new JPanel();
        rbExNotAct = new JRadioButton();
        rbExAct = new JRadioButton();
        bReadCurr = new JButton();
        bSetCurr = new JButton();
        bottomPanel = new JPanel();
        pollOptPanel = new JPanel();
        cbPollOpt1 = new JCheckBox();
        cbPollOpt2 = new JCheckBox();
        cbPollOpt3 = new JCheckBox();
        cbPollOpt4 = new JCheckBox();
        cbPollOpt5 = new JCheckBox();
        cbPollOpt6 = new JCheckBox();
        pollIntPanel = new JPanel();
        rbPollOpt1 = new JRadioButton();
        rbPollOpt2 = new JRadioButton();
        rbPollOpt3 = new JRadioButton();
        rbPollOpt4 = new JRadioButton();
        bReadPoll = new JButton();
        bSetPoll = new JButton();
        bManDet = new JButton();
        bAutoDet = new JButton();
        msgPanel = new JPanel();
        scrlMsg = new JScrollPane();
        mMsg = new JTextArea();
        bClear = new JButton();
        bReset = new JButton();
        statPanel = new JPanel();
        tStat1 = new JTextField();
        tStat2 = new JTextField();
        tStat3 = new JTextField();
        tStat4 = new JTextField();
        
        lblReader.setText("Select Reader");

		String[] rdrNameDef = {"Please select reader                   "};	
		cbReader = new JComboBox(rdrNameDef);
		cbReader.setSelectedIndex(0);
		
        lblReader.setText("Select Reader");
        bInit.setText("Initalize");
        bConn.setText("Connect");

        GroupLayout rdrPanelLayout = new GroupLayout(rdrPanel);
        rdrPanel.setLayout(rdrPanelLayout);
        rdrPanelLayout.setHorizontalGroup(
            rdrPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(rdrPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(rdrPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(GroupLayout.Alignment.TRAILING, rdrPanelLayout.createSequentialGroup()
                        .addComponent(bInit, GroupLayout.PREFERRED_SIZE, 117, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bConn, GroupLayout.PREFERRED_SIZE, 118, GroupLayout.PREFERRED_SIZE))
                    .addGroup(rdrPanelLayout.createSequentialGroup()
                        .addComponent(lblReader)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(cbReader, 0, 230, Short.MAX_VALUE)))
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
                .addGroup(rdrPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(bConn)
                    .addComponent(bInit))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        confPanel.setBorder(BorderFactory.createTitledBorder("Configuration Setting"));
        bgConf.add(rbBoth);
        rbBoth.setText("Both ICC_PICC interfaces can be activated");
        bgConf.add(rbEither);
        rbEither.setText("Either ICC or PICC interface can be activated");

        GroupLayout confPanelLayout = new GroupLayout(confPanel);
        confPanel.setLayout(confPanelLayout);
        confPanelLayout.setHorizontalGroup(
            confPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(GroupLayout.Alignment.TRAILING, confPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(confPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                    .addComponent(rbEither, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 277, Short.MAX_VALUE)
                    .addComponent(rbBoth, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 277, Short.MAX_VALUE))
                .addContainerGap())
        );
        confPanelLayout.setVerticalGroup(
            confPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(confPanelLayout.createSequentialGroup()
                .addComponent(rbBoth)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbEither)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        currModePanel.setBorder(BorderFactory.createTitledBorder("Current Mode"));
        bgCurr.add(rbExNotAct);
        rbExNotAct.setText("Exclusive Mode is not Active");
        bgCurr.add(rbExAct);
        rbExAct.setText("Exclusive Mode is Active");

        GroupLayout currModePanelLayout = new GroupLayout(currModePanel);
        currModePanel.setLayout(currModePanelLayout);
        currModePanelLayout.setHorizontalGroup(
            currModePanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(GroupLayout.Alignment.TRAILING, currModePanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(currModePanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                    .addComponent(rbExAct, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 277, Short.MAX_VALUE)
                    .addComponent(rbExNotAct, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 277, Short.MAX_VALUE))
                .addContainerGap())
        );
        currModePanelLayout.setVerticalGroup(
            currModePanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(currModePanelLayout.createSequentialGroup()
                .addComponent(rbExNotAct)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbExAct)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        bReadCurr.setText("Read Current Mode");
        bSetCurr.setText("Set Mode Configuration");

        GroupLayout midPanelLayout = new GroupLayout(midPanel);
        midPanel.setLayout(midPanelLayout);
        midPanelLayout.setHorizontalGroup(
            midPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(midPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(midPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(currModePanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(confPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addGroup(midPanelLayout.createSequentialGroup()
                        .addComponent(bReadCurr, GroupLayout.PREFERRED_SIZE, 145, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bSetCurr, GroupLayout.DEFAULT_SIZE, 159, Short.MAX_VALUE)))
                .addContainerGap())
        );
        midPanelLayout.setVerticalGroup(
            midPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(midPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(confPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(currModePanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(midPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(bReadCurr)
                    .addComponent(bSetCurr))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        pollOptPanel.setBorder(BorderFactory.createTitledBorder("Contactless Polling Option"));
        cbPollOpt1.setText("Automatic PICC Polling");
        cbPollOpt2.setText("Turn off antenna field if no PICC within range");
        cbPollOpt3.setText("Turn off antenna field if PICC is inactive");
        cbPollOpt4.setText("Activate the PICC when detected");
        cbPollOpt5.setText("Test Mode");
        cbPollOpt6.setText("Enforce ISO 14443A Part4");

        GroupLayout pollOptPanelLayout = new GroupLayout(pollOptPanel);
        pollOptPanel.setLayout(pollOptPanelLayout);
        pollOptPanelLayout.setHorizontalGroup(
            pollOptPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(pollOptPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(pollOptPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(cbPollOpt2, GroupLayout.DEFAULT_SIZE, 277, Short.MAX_VALUE)
                    .addComponent(cbPollOpt1, GroupLayout.DEFAULT_SIZE, 277, Short.MAX_VALUE)
                    .addComponent(cbPollOpt3, GroupLayout.DEFAULT_SIZE, 277, Short.MAX_VALUE)
                    .addComponent(cbPollOpt4, GroupLayout.DEFAULT_SIZE, 277, Short.MAX_VALUE)
                    .addComponent(cbPollOpt5, GroupLayout.DEFAULT_SIZE, 277, Short.MAX_VALUE)
                    .addComponent(cbPollOpt6, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, 277, Short.MAX_VALUE))
                .addContainerGap())
        );
        pollOptPanelLayout.setVerticalGroup(
            pollOptPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(pollOptPanelLayout.createSequentialGroup()
                .addComponent(cbPollOpt1)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(cbPollOpt2)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(cbPollOpt3)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(cbPollOpt4)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(cbPollOpt5)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(cbPollOpt6))
        );

        pollIntPanel.setBorder(BorderFactory.createTitledBorder("Poll Interval"));
        bgPollInt.add(rbPollOpt1);
        rbPollOpt1.setText("250 msec");
        bgPollInt.add(rbPollOpt2);
        rbPollOpt2.setText("500 msec");
        bgPollInt.add(rbPollOpt3);
        rbPollOpt3.setText("1 sec");
        bgPollInt.add(rbPollOpt4);
        rbPollOpt4.setText("2.5 sec");

        GroupLayout pollIntPanelLayout = new GroupLayout(pollIntPanel);
        pollIntPanel.setLayout(pollIntPanelLayout);
        pollIntPanelLayout.setHorizontalGroup(
            pollIntPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(pollIntPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(pollIntPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(rbPollOpt2, GroupLayout.DEFAULT_SIZE, 94, Short.MAX_VALUE)
                    .addComponent(rbPollOpt1, GroupLayout.DEFAULT_SIZE, 94, Short.MAX_VALUE)
                    .addComponent(rbPollOpt3, GroupLayout.DEFAULT_SIZE, 94, Short.MAX_VALUE)
                    .addComponent(rbPollOpt4, GroupLayout.DEFAULT_SIZE, 94, Short.MAX_VALUE))
                .addContainerGap())
        );
        pollIntPanelLayout.setVerticalGroup(
            pollIntPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(pollIntPanelLayout.createSequentialGroup()
                .addComponent(rbPollOpt1)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbPollOpt2)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbPollOpt3)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbPollOpt4))
        );

        bReadPoll.setText("Read Polling Options");
        bSetPoll.setText("Set Polling Options");
        bManDet.setText("Manual Card Detection");
        bAutoDet.setText("Start Auto Detection");

        GroupLayout bottomPanelLayout = new GroupLayout(bottomPanel);
        bottomPanel.setLayout(bottomPanelLayout);
        bottomPanelLayout.setHorizontalGroup(
            bottomPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(bottomPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(bottomPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(bottomPanelLayout.createSequentialGroup()
                        .addComponent(pollIntPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(bottomPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                            .addComponent(bSetPoll, GroupLayout.DEFAULT_SIZE, 177, Short.MAX_VALUE)
                            .addComponent(bReadPoll, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 177, Short.MAX_VALUE)))
                    .addComponent(pollOptPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addGroup(bottomPanelLayout.createSequentialGroup()
                        .addComponent(bManDet)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bAutoDet, GroupLayout.DEFAULT_SIZE, 152, Short.MAX_VALUE)))
                .addContainerGap())
        );
        bottomPanelLayout.setVerticalGroup(
            bottomPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(bottomPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(pollOptPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addGroup(bottomPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(bottomPanelLayout.createSequentialGroup()
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(pollIntPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                    .addGroup(bottomPanelLayout.createSequentialGroup()
                        .addGap(14, 14, 14)
                        .addComponent(bReadPoll)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bSetPoll)))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(bottomPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(bManDet)
                    .addComponent(bAutoDet))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        mMsg.setColumns(20);
        mMsg.setRows(5);
        scrlMsg.setViewportView(mMsg);
        bClear.setText("Clear");
        bReset.setText("Reset");
        bQuit.setText("Quit");

        GroupLayout msgPanelLayout = new GroupLayout(msgPanel);
        msgPanel.setLayout(msgPanelLayout);
        msgPanelLayout.setHorizontalGroup(
            msgPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(GroupLayout.Alignment.TRAILING, msgPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(msgPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                    .addComponent(scrlMsg, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 389, Short.MAX_VALUE)
                    .addGroup(msgPanelLayout.createSequentialGroup()
                        .addComponent(bClear, GroupLayout.PREFERRED_SIZE, 120, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, 28, Short.MAX_VALUE)
                        .addComponent(bReset, GroupLayout.PREFERRED_SIZE, 120, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, 28, Short.MAX_VALUE)
                        .addComponent(bQuit, GroupLayout.PREFERRED_SIZE, 120, GroupLayout.PREFERRED_SIZE)))
                .addContainerGap())
        );
        msgPanelLayout.setVerticalGroup(
            msgPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(GroupLayout.Alignment.TRAILING, msgPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(scrlMsg, GroupLayout.DEFAULT_SIZE, 583, Short.MAX_VALUE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(msgPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(bQuit)
                    .addComponent(bReset)
                    .addComponent(bClear))
                .addContainerGap())
        );

        tStat1.setBackground(new java.awt.Color(204, 204, 204));
        tStat2.setBackground(new java.awt.Color(204, 204, 204));
        tStat3.setBackground(new java.awt.Color(204, 204, 204));
        tStat4.setBackground(new java.awt.Color(204, 204, 204));

        GroupLayout statPanelLayout = new GroupLayout(statPanel);
        statPanel.setLayout(statPanelLayout);
        statPanelLayout.setHorizontalGroup(
            statPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(GroupLayout.Alignment.TRAILING, statPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(tStat1, GroupLayout.DEFAULT_SIZE, 131, Short.MAX_VALUE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tStat2, GroupLayout.PREFERRED_SIZE, 210, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tStat3, GroupLayout.PREFERRED_SIZE, 126, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tStat4, GroupLayout.PREFERRED_SIZE, 231, GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );
        statPanelLayout.setVerticalGroup(
            statPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(statPanelLayout.createSequentialGroup()
                .addGroup(statPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(tStat1, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(tStat2, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(tStat3, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(tStat4, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        GroupLayout layout = new GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(statPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                            .addComponent(bottomPanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addGroup(GroupLayout.Alignment.LEADING, layout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                                .addComponent(rdrPanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                .addComponent(midPanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(msgPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)))
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                    .addComponent(msgPanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addGroup(GroupLayout.Alignment.LEADING, layout.createSequentialGroup()
                        .addComponent(rdrPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(midPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bottomPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(statPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
        );
		
        bInit.addActionListener(this); 
        bConn.addActionListener(this);
        bClear.addActionListener(this);
        bReset.addActionListener(this);
        bReadCurr.addActionListener(this);
        bSetCurr.addActionListener(this);
        bReadPoll.addActionListener(this);
        bSetPoll.addActionListener(this);
        bManDet.addActionListener(this);
        bAutoDet.addActionListener(this);
        rbBoth.addActionListener(this);
        rbEither.addActionListener(this);
        rbExNotAct.addActionListener(this);
        rbExAct.addActionListener(this);
        rbPollOpt1.addActionListener(this);
        rbPollOpt2.addActionListener(this);
        rbPollOpt3.addActionListener(this);
        rbPollOpt4.addActionListener(this);
        cbPollOpt1.addActionListener(this);
        cbPollOpt2.addActionListener(this);
        cbPollOpt3.addActionListener(this);
        cbPollOpt4.addActionListener(this);
        cbPollOpt5.addActionListener(this);
        cbPollOpt6.addActionListener(this);
        bQuit.addActionListener(this);
   		
        bInit.setMnemonic(KeyEvent.VK_I);
        bConn.setMnemonic(KeyEvent.VK_C);
        bClear.setMnemonic(KeyEvent.VK_L);
        bReset.setMnemonic(KeyEvent.VK_R);
        bQuit.setMnemonic(KeyEvent.VK_Q);
        bReadCurr.setMnemonic(KeyEvent.VK_M);
        bSetCurr.setMnemonic(KeyEvent.VK_S);
        bReadPoll.setMnemonic(KeyEvent.VK_P);
        bSetPoll.setMnemonic(KeyEvent.VK_O);
        bManDet.setMnemonic(KeyEvent.VK_D);
        bAutoDet.setMnemonic(KeyEvent.VK_A);
        
        
    }

	public void actionPerformed(ActionEvent e) 
	{
		
		//Initialize button
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
		
			
			//Look for ACR128 SAM and make it the default reader in the combobox
			for (int i = 0; i < cchReaders[0]-1; i++)
			{
				
				cbReader.setSelectedIndex(i);
				
				if (((String) cbReader.getSelectedItem()).lastIndexOf("ACR128U SAM")> -1)
					break;
				
			}
			
			addButton(0);
			
		}
		
		//Connect button
		if(bConn == e.getSource())
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

		}
		
		//Clear button
		if(bClear == e.getSource())
		{
			
			mMsg.setText("");
			
		}
		
		if(bQuit == e.getSource())
		{
			
			this.dispose();
			
		}
		
		//Reset Button
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
			timer.stop();
			bAutoDet.setText("Start Auto Detection");
			mMsg.setText("");
			initMenu();
			cbReader.removeAllItems();
			cbReader.addItem("Please select reader                   ");
			
		}
		
		//Read Current Mode button
		if(bReadCurr == e.getSource())
		{
			
			getExMode(1);
			
		}
		
		//Set Mode Configuration button
		if(bSetCurr == e.getSource())
		{
			
			clearBuffers();
			SendBuff[0] = (byte)0x2B;
			SendBuff[1] = (byte)0x01;
			
			if(rbBoth.isSelected())
				SendBuff[2] = (byte)0x00;
			else
				SendBuff[2] = (byte)0x01;
			
			SendLen = 3;
			RecvLen[0]=7;
			retCode = callCardControl();
			
			if (retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
			
		}
		
		//Read Polling Option button
		if(bReadPoll == e.getSource())
		{
			
			readPollingOptions(1);
			
		}
		
		//Set Polling Option button
		if(bSetPoll == e.getSource())
		{
			
			clearBuffers();
			SendBuff[0] = (byte)0x23;
			SendBuff[1] = (byte)0x01;
			
			if(cbPollOpt1.isSelected())
				SendBuff[2] = (byte)(SendBuff[2] | 0x01);
			
			if(cbPollOpt2.isSelected())
				SendBuff[2] = (byte)(SendBuff[2] | 0x02);
			
			if(cbPollOpt3.isSelected())
				SendBuff[2] = (byte)(SendBuff[2] | 0x04);
			
			if(cbPollOpt4.isSelected())
				SendBuff[2] = (byte)(SendBuff[2] | 0x08);
			
			if(rbPollOpt2.isSelected())
				SendBuff[2] = (byte)(SendBuff[2] | 0x10); 
			
			if(rbPollOpt3.isSelected())
				SendBuff[2] = (byte)(SendBuff[2] | 0x20);
			
			if(rbPollOpt4.isSelected())
			{
				
				SendBuff[2] = (byte)(SendBuff[2] | 0x10);
				SendBuff[2] = (byte)(SendBuff[2] | 0x20);
				
			}
			
			if(cbPollOpt5.isSelected())
				SendBuff[2] = (byte)(SendBuff[2] | 0x40);
			
			if(cbPollOpt6.isSelected())
				SendBuff[2] = (byte)(SendBuff[2] | 0x80);
			
			SendLen = 3;
			RecvLen[0] = 7;
			retCode = callCardControl();
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
		}
		
		//Manual Card Detection button
		if(bManDet == e.getSource())
		{
			
			//Always use a valid connection for Card Control commands
			retCode = callCardConnect(1);
			
			if (retCode!=ACSModule.SCARD_S_SUCCESS)
				return;
			
			readPollingOptions(0);
			
			if ((RecvBuff[5]&0x01)!=0)
				displayOut(0, 0, "Turn off automatic PICC polling in the device before using this function.");
			else
			{
				
				clearBuffers();
				SendBuff[0] = (byte)0x22;
				SendBuff[1] = (byte)0x01;
				SendBuff[2] = (byte)0x0A;
				SendLen = 3;
				RecvLen[0] = 6;
				callCardControl();
				
				if (retCode!=ACSModule.SCARD_S_SUCCESS)
					return;
				
				if((RecvBuff[5]&0x01) != 0)
					displayOut(6, 0, "No card within range.");
				else
					displayOut(6, 0, "Card is detected.");
				
			}
			
		}
		
		//Start Auto Detection Button
		if (bAutoDet == e.getSource())
		{
			
			 // pollCase legend
			 // 1 = Both ICC and PICC can poll, but only one at a time
			 // 2 = Only ICC can poll
			 // 3 = Both reader can be polled
			//JOptionPane.showMessageDialog(this, cbReader.getItemCount());
			if (autoDet)
			{
				
				autoDet = false;
				bAutoDet.setText("Start Auto Detection");
				//stop timer
				displayOut(5, 0, "");
				displayOut(6, 0, "");
				return;
				
			}
			//Always use a valid connection for Card Control commands
			retCode = callCardConnect(1);
			
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
			{
				
				bAutoDet.setText("Start Auto Detection");
				autoDet = false;
				return;
				
			}
			
			getExMode(0);
			
			//Either ICC or PICC can be polled at any one time
			if (RecvBuff[5]!=0)
			{
				
				readPollingOptions(0);
				
				//auto PICC polling is enabled
				if((RecvBuff[5] & 0x01)!=0)
					pollCase=1;	//Either ICC and PICC can be polled
				else
					pollCase=2;	//Only ICC can be polled
				
			}
			else	//Both ICC and PICC can be enabled at the same time
			{
				
				readPollingOptions(0);
				
				//auto polling is enabled
				if((RecvBuff[5]&0x01)!=0)
					pollCase=3;	//Both ICC and PICC can be polled
				else
					pollCase=2; //only ICC can be polled
				
			}
			
			switch(pollCase)
			{
			
			case 1: displayOut(0, 0, "Either reader can detect cards, but not both."); break;
			case 2: displayOut(0, 0, "Automatic PICC polling is disabled, only ICC can detect card."); break;
			case 3: displayOut(0, 0, "Both ICC and PICC readers can automatically detect card."); break;
			
			}
			
			autoDet = true;
			bAutoDet.setText("End Auto Detection");
			timer = new Timer(500, pollTimer);			
			timer.start();
			
			
		}
		
	}
	
	//timer for automatic polling
	ActionListener pollTimer = new ActionListener() {
	      public void actionPerformed(ActionEvent evt) {
		
			switch(pollCase)
			{
			
			//Automatic PICC polling is disabled, only ICC can detect card
			case 2:
			{
				//connect to ICC reader
				displayOut(6, 0, "Auto polling is disabled");
	
				int i=0;
				cbReader.setSelectedIndex(i);
				while(((String) cbReader.getSelectedItem()).lastIndexOf("ACR128U ICC")> -1)
				{
				
					if (i==cbReader.getItemCount())
					{
						displayOut(0, 0, "Cannot find ACR128 ICC reader.");
						timer.stop();
					}
					i++;
					cbReader.setSelectedIndex(i);
				}
				
				ACSModule.SCARD_READERSTATE rdrState = new ACSModule.SCARD_READERSTATE();
				rdrState.RdrName = (String)cbReader.getSelectedItem();
				retCode = jacs.jSCardGetStatusChange(hContext, 
													 0, 
													 rdrState, 
													 1);
				
				if (retCode!= ACSModule.SCARD_S_SUCCESS)
				{
					
					displayOut(1, retCode, "");
					timer.stop();
					return;
					
				}
				
				if((rdrState.RdrEventState & ACSModule.SCARD_STATE_PRESENT)!=0)
					displayOut(5, 0, "Card is inserted.");
				else
					displayOut(5, 0, "Card is removed.");
				
				break;
			}
			
			//Both ICC and PICC readers can automatically detect card
			case 1:
			case 3:
			{
				
				//attempt to poll ICC reader
				displayOut(6, 0, "Auto polling is disabled");
	
				int i=0;
				cbReader.setSelectedIndex(i);
				while(((String) cbReader.getSelectedItem()).lastIndexOf("ACR128U ICC")== -1)
				{
				
					if (i==cbReader.getItemCount())
					{
						displayOut(0, 0, "Cannot find ACR128 ICC reader.");
						timer.stop();
					}
					i++;
					cbReader.setSelectedIndex(i);
				}
				
				ACSModule.SCARD_READERSTATE rdrState = new ACSModule.SCARD_READERSTATE();
				rdrState.RdrName = (String)cbReader.getSelectedItem();
				retCode = jacs.jSCardGetStatusChange(hContext, 
													 0, 
													 rdrState, 
													 1);
				
				if (retCode== ACSModule.SCARD_S_SUCCESS)
				{
				
					if((rdrState.RdrEventState & ACSModule.SCARD_STATE_PRESENT)!=0)
						displayOut(5, 0, "Card is inserted.");
					else
						displayOut(5, 0, "Card is removed.");
				
				}
				//attempt to poll PICC reader
				
				i=0;
				cbReader.setSelectedIndex(i);
				while(((String) cbReader.getSelectedItem()).lastIndexOf("ACR128U PICC")== -1)
				{
					
					if (i==cbReader.getItemCount())
					{
						displayOut(0, 0, "Cannot find ACR128 PICC reader.");
						timer.stop();
					}
					i++;
					cbReader.setSelectedIndex(i);
				}
		
				rdrState.RdrName = (String)cbReader.getSelectedItem();
				retCode = jacs.jSCardGetStatusChange(hContext, 
													 0, 
													 rdrState, 
													 1);
				
				if (retCode== ACSModule.SCARD_S_SUCCESS)
				{
							
					if((rdrState.RdrEventState & ACSModule.SCARD_STATE_PRESENT)!=0)
						displayOut(6, 0, "Card is detected.");
					else
						displayOut(6, 0, "No card within Range.");
				
				}
				break;
				
			}
			
			
			}
		}
	};
	
	public int callCardConnect(int reqType)
	{
		
		if (connActive)
			retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
		
		//shared connection
		String rdrcon = (String)cbReader.getSelectedItem();
		retCode = jacs.jSCardConnect(hContext, 
									 rdrcon, 
									 ACSModule.SCARD_SHARE_SHARED, 
									 ACSModule.SCARD_PROTOCOL_T0 | ACSModule.SCARD_PROTOCOL_T1, 
									 hCard, 
									 PrefProtocols);
		
		if(retCode != ACSModule.SCARD_S_SUCCESS)
		{
			
			//connect to SAM
			if (reqType == 1)
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
		    			return retCode;
		    			
				    }
		    		else
		    		{
		    			
		    			displayOut(0, 0, "Successful connection to " + (String)cbReader.getSelectedItem());
		    			
		    		}
					
				}
		      	else{
		      		
		      		displayOut(1, retCode, "");
	    			connActive = false;
	    			return retCode;
		      		
		      	}
				
			}
			
		}
		else
		{
			
			displayOut(0, 0, "Successful connection to " + (String)cbReader.getSelectedItem());
			return retCode;
			
		}
		return retCode;
	}
	
	public void readPollingOptions(int reqType)
	{
		
		clearBuffers();
		SendBuff[0] = (byte)0x23;
		SendBuff[1] = (byte)0x00;
		SendLen = 2;
		RecvLen[0] = 6;
		retCode = callCardControl();
		
		if(retCode != ACSModule.SCARD_S_SUCCESS)
			return;
		
		if (reqType == 1)
		{
		
			//interpret PICC polling setting
			if ((RecvBuff[5] & 0x01)!=0)
			{
				
				displayOut(3, 0, "Automatic PICC polling is enabled.");
				cbPollOpt1.setSelected(true);
				
			}
			else
			{
				
				displayOut(3, 0, "Automatic PICC polling is disabled.");
				cbPollOpt1.setSelected(false);
				
			}
			
			if ((RecvBuff[5] & 0x02)!=0)
			{
				
				displayOut(3, 0, "Antenna off when no PICC found is enabled.");
				cbPollOpt2.setSelected(true);
				
			}
			else
			{
				
				displayOut(3, 0, "Antenna off when no PICC found is disabled.");
				cbPollOpt2.setSelected(false);
				
			}
			
			if ((RecvBuff[5] & 0x04)!=0)
			{
				
				displayOut(3, 0, "Antenna off when PICC is inactive is enabled.");
				cbPollOpt3.setSelected(true);
				
			}
			else
			{
				
				displayOut(3, 0, "Antenna off when PICC is inactive is disabled.");
				cbPollOpt3.setSelected(false);
				
			}
			
			if ((RecvBuff[5] & 0x08)!=0)
			{
				
				displayOut(3, 0, "Activate PICC when detected is enabled.");
				cbPollOpt4.setSelected(true);
				
			}
			else
			{
				
				displayOut(3, 0, "Activate PICC when detected is disabled.");
				cbPollOpt4.setSelected(false);
				
			}
			
			if (((RecvBuff[5] & 0x10)==0)&&((RecvBuff[5] & 0x20)==0))
			{
				
				displayOut(3, 0, "Poll interval is 250 msec.");
				rbPollOpt1.setSelected(true);
			
			}
			
			if (((RecvBuff[5] & 0x10)!=0)&&((RecvBuff[5] & 0x20)==0))
			{
				
				displayOut(3, 0, "Poll interval is 500 msec.");
				rbPollOpt2.setSelected(true);
			
			}

			if (((RecvBuff[5] & 0x10)==0)&&((RecvBuff[5] & 0x20)!=0))
			{
				
				displayOut(3, 0, "Poll interval is 1 sec.");
				rbPollOpt3.setSelected(true);
			
			}
			
			if (((RecvBuff[5] & 0x10)!=0)&&((RecvBuff[5] & 0x20)!=0))
			{
				
				displayOut(3, 0, "Poll interval is 2.5 sec.");
				rbPollOpt4.setSelected(true);
			
			}
			
			if ((RecvBuff[5] & 0x40)!=0)
			{
				
				displayOut(3, 0, "Test Mode is enabled.");
				cbPollOpt5.setSelected(true);
				
			}
			else
			{
				
				displayOut(3, 0, "Test Mode is disabled.");
				cbPollOpt5.setSelected(false);
				
			}
			
			if ((RecvBuff[5] & 0x80)!=0)
			{
				
				displayOut(3, 0, "ISO14443A Part4 is enforced.");
				cbPollOpt6.setSelected(true);
				
			}
			else
			{
				
				displayOut(3, 0, "ISO14443A Part4 is not enforced.");
				cbPollOpt6.setSelected(false);
				
			}
			
		}
		
	}
	
	public void getExMode(int reqType)
	{
		
		String tmpStr, tmpHex;
		
		clearBuffers();
		SendBuff[0] = (byte)0x2B;
		SendBuff[1] = (byte) 0x00;
		SendLen = 2;
		RecvLen[0] = 7;
		retCode = callCardControl();
		
		if (retCode != ACSModule.SCARD_S_SUCCESS)
			return;
		
		//Interpret Configuration Setting and Current Mode
		tmpStr = "";
		
		for(int i=0; i<5; i++)
		{
			tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
			
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
			
			tmpStr += tmpHex;  
			
		}
		if (tmpStr.equals("E100000002"))
		{
			
			if (reqType == 1)
			{
				if (RecvBuff[5] == 0)
					rbBoth.setSelected(true);
				else
					rbEither.setSelected(true);
				
				if (RecvBuff[6] == 0)
					rbExNotAct.setSelected(true);
				else
					rbExAct.setSelected(true);
				
			}
			
		}
		else
			displayOut(4, 0, "Wrong return values from device.");
	}
	
	public void addButton(int reqType)
	{
		
		switch(reqType){
		
		case 0:
		{
			bInit.setEnabled(false);
			bConn.setEnabled(true);
			bReset.setEnabled(true);
		}
		case 1:
		{
			
			currModePanel.setEnabled(true);
			rbExNotAct.setEnabled(true);
			rbExAct.setEnabled(true);
			bSetCurr.setEnabled(true);
			bReadCurr.setEnabled(true); 
			pollOptPanel.setEnabled(true);
			cbPollOpt1.setEnabled(true);
			cbPollOpt2.setEnabled(true);
			cbPollOpt3.setEnabled(true);
			cbPollOpt4.setEnabled(true);
			cbPollOpt5.setEnabled(true);
			cbPollOpt6.setEnabled(true);
			pollIntPanel.setEnabled(true);
			rbPollOpt1.setEnabled(true);
			rbPollOpt2.setEnabled(true);
			rbPollOpt3.setEnabled(true);
			rbPollOpt4.setEnabled(true);
			bReadPoll.setEnabled(true);
			bSetPoll.setEnabled(true);
			bManDet.setEnabled(true);
			bAutoDet.setEnabled(true);
			rbBoth.setEnabled(true);
			rbEither.setEnabled(true);
			
		}
		
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
			case 5: tStat2.setText(printText);break;
			case 6: tStat4.setText(printText);break;
			default: mMsg.append("- " + printText + "\n");
		
		}
		
	}
	

	
	public void initMenu()
	{
	
		connActive = false;
		displayOut(0, 0, "Program Ready");
		autoDet = false;
		dualPoll = false;
		bConn.setEnabled(false);
		bReset.setEnabled(false);
		bInit.setEnabled(true);
		confPanel.setEnabled(false);
		currModePanel.setEnabled(false);
		bSetCurr.setEnabled(false);
		bReadCurr.setEnabled(false);
		pollOptPanel.setEnabled(false);
		pollIntPanel.setEnabled(false);
		bSetPoll.setEnabled(false);
		bReadPoll.setEnabled(false);
		bManDet.setEnabled(false);
		bAutoDet.setEnabled(false);
		tStat1.setText("ICC Reader Status");
		tStat1.setEditable(false);
		tStat3.setText("PICC Reader Status");
		tStat3.setEditable(false);
		tStat2.setEditable(false);
		tStat4.setEditable(false);
		rbBoth.setEnabled(false);
		rbEither.setEnabled(false);
		rbExAct.setEnabled(false);
		rbExNotAct.setEnabled(false);
		rbPollOpt1.setEnabled(false);
		rbPollOpt2.setEnabled(false);
		rbPollOpt3.setEnabled(false);
		rbPollOpt4.setEnabled(false);
		cbPollOpt1.setEnabled(false);
		cbPollOpt2.setEnabled(false);
		cbPollOpt3.setEnabled(false);
		cbPollOpt4.setEnabled(false);
		cbPollOpt5.setEnabled(false);
		cbPollOpt6.setEnabled(false);
		rbExNotAct.setEnabled(false);
		rbExAct.setEnabled(false);
		
	}
    
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new polling().setVisible(true);
            }
        });
    }



}
