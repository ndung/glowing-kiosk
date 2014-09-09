/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              AdvDevProg.java

  Description:       This sample program outlines the steps on how to
                     use the functionalities of ACR128 device.
                    
  Author:            M.J.E.C. Castillo

  Date:              July 14, 2008

  Revision Trail:   (Date/Author/Description)

======================================================================*/

import java.io.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;


public class AdvDevProg extends JFrame implements ActionListener, KeyListener{

	//JPCSC Variables
	int retCode;
	boolean connActive, autoDet; 
	static String VALIDCHARS = "0123456789";
	static String VALIDCHARSHEX = "ABCDEFabcdef0123456789";
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
	ACSModule.SCARD_READERSTATE rdrState = new ACSModule.SCARD_READERSTATE();
	
	//GUI Variables
    private JPanel PICCPanel, PPSPanel, antennaPanel, transSetPanel;
    private JButton bClear, bConnect, bGetAS, bGetEH, bGetFWI, bGetPICC, bGetPPS;
    private JButton bGetPSet, bGetReg, bGetTranSet, bInit, bPoll, bRefIS, bReset, bQuit;
    private JButton bSetAS, bSetEH, bSetFWI, bSetPICC, bSetPPS, bSetPSet, bSetReg, bSetTranSet;
    private ButtonGroup bgAntenna, bgCurrSpeed, bgMaxSpeed, bgPICCType, bgRIS;
    private JCheckBox cbFilter;
    private JComboBox cbReader;
    private JPanel currSpeedPanel, errHandPanel, fwiPanel;
    private JLabel lblPICC1, lblPICC10, lblPICC11, lblPICC12, lblPICC2, lblFS, lblFStop, lblFWI, lblMsg;
    private JLabel lblPICC3, lblPICC4, lblPICC5, lblPICC6, lblPICC7, lblPICC8, lblPICC9;
    private JLabel lblPc2Pi, lblPi2Pc, lblReader, lblRecGain, lblRegCurr, lblRegNew, lblSetup;
    private JLabel lblTO, lblTxMode;
    private JTextArea mMsg;
    private JPanel maxSpeedPanel, msgPanel, pollingPanel;
    private JRadioButton rbAntOff, rbAntOn, rbCurr1, rbCurr2, rbCurr3, rbCurr4, rbCurr5;
    private JRadioButton rbMax1, rbMax2, rbMax3, rbMax4, rbMax5, rbRIS1, rbRIS2, rbRIS3;
    private JRadioButton rbType1, rbType2, rbType3;
    private JPanel readerPanel, refISPanel, regPanel;
    private JScrollPane scrPaneMsg;
    private JTextField tFS, tFStop, tFWI, tMsg, tPICC1, tPICC10, tPICC11, tPICC12, tPICC2;
    private JTextField tPICC3, tPICC4, tPICC5, tPICC6, tPICC7, tPICC8, tPICC9, tPc2Pi;
    private JTextField tPi2Pc, tPollTO, tRecGain, tRegNum, tRegVal, tSetup, tTxMode;
	
	static JacspcscLoader jacs = new JacspcscLoader();
    

    public AdvDevProg() {
    	
    	this.setTitle("Advanced Device Programming");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

		setSize(1200,700);
        bgAntenna = new ButtonGroup();
        bgPICCType = new ButtonGroup();
        bgMaxSpeed = new ButtonGroup();
        bgCurrSpeed = new ButtonGroup();
        bgRIS = new ButtonGroup();
        readerPanel = new JPanel();
        lblReader = new JLabel();
        bInit = new JButton();
        bConnect = new JButton();
        fwiPanel = new JPanel();
        tFWI = new JTextField();
        tPollTO = new JTextField();
        tFS = new JTextField();
        lblFWI = new JLabel();
        lblTO = new JLabel();
        lblFS = new JLabel();
        bGetFWI = new JButton();
        bSetFWI = new JButton();
        antennaPanel = new JPanel();
        rbAntOn = new JRadioButton();
        rbAntOff = new JRadioButton();
        bGetAS = new JButton();
        bSetAS = new JButton();
        transSetPanel = new JPanel();
        tFStop = new JTextField();
        tSetup = new JTextField();
        cbFilter = new JCheckBox();
        tRecGain = new JTextField();
        tTxMode = new JTextField();
        lblFStop = new JLabel();
        lblSetup = new JLabel();
        lblRecGain = new JLabel();
        lblTxMode = new JLabel();
        bGetTranSet = new JButton();
        bSetTranSet = new JButton();
        errHandPanel = new JPanel();
        tPc2Pi = new JTextField();
        tPi2Pc = new JTextField();
        lblPc2Pi = new JLabel();
        lblPi2Pc = new JLabel();
        bGetEH = new JButton();
        bSetEH = new JButton();
        PICCPanel = new JPanel();
        tPICC1 = new JTextField();
        tPICC2 = new JTextField();
        tPICC3 = new JTextField();
        tPICC4 = new JTextField();
        tPICC5 = new JTextField();
        tPICC6 = new JTextField();
        tPICC7 = new JTextField();
        tPICC8 = new JTextField();
        tPICC9 = new JTextField();
        tPICC10 = new JTextField();
        tPICC11 = new JTextField();
        tPICC12 = new JTextField();
        lblPICC1 = new JLabel();
        lblPICC2 = new JLabel();
        lblPICC3 = new JLabel();
        lblPICC4 = new JLabel();
        lblPICC5 = new JLabel();
        lblPICC6 = new JLabel();
        lblPICC7 = new JLabel();
        lblPICC8 = new JLabel();
        lblPICC9 = new JLabel();
        lblPICC10 = new JLabel();
        lblPICC11 = new JLabel();
        lblPICC12 = new JLabel();
        bGetPICC = new JButton();
        bSetPICC = new JButton();
        pollingPanel = new JPanel();
        rbType1 = new JRadioButton();
        rbType2 = new JRadioButton();
        rbType3 = new JRadioButton();
        bGetPSet = new JButton();
        bSetPSet = new JButton();
        bPoll = new JButton();
        lblMsg = new JLabel();
        tMsg = new JTextField();
        PPSPanel = new JPanel();
        maxSpeedPanel = new JPanel();
        rbMax1 = new JRadioButton();
        rbMax2 = new JRadioButton();
        rbMax3 = new JRadioButton();
        rbMax4 = new JRadioButton();
        rbMax5 = new JRadioButton();
        currSpeedPanel = new JPanel();
        rbCurr1 = new JRadioButton();
        rbCurr2 = new JRadioButton();
        rbCurr3 = new JRadioButton();
        rbCurr4 = new JRadioButton();
        rbCurr5 = new JRadioButton();
        bGetPPS = new JButton();
        bSetPPS = new JButton();
        regPanel = new JPanel();
        tRegNum = new JTextField();
        tRegVal = new JTextField();
        lblRegCurr = new JLabel();
        lblRegNew = new JLabel();
        bGetReg = new JButton();
        bSetReg = new JButton();
        refISPanel = new JPanel();
        rbRIS1 = new JRadioButton();
        rbRIS2 = new JRadioButton();
        rbRIS3 = new JRadioButton();
        bRefIS = new JButton();
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

        bInit.setText("Initialize");
        bConnect.setText("Connect");

        GroupLayout readerPanelLayout = new GroupLayout(readerPanel);
        readerPanel.setLayout(readerPanelLayout);
        readerPanelLayout.setHorizontalGroup(
            readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(readerPanelLayout.createSequentialGroup()
                .addGroup(readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(readerPanelLayout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(lblReader)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(cbReader, 0, 252, Short.MAX_VALUE))
                    .addGroup(readerPanelLayout.createSequentialGroup()
                        .addGap(173, 173, 173)
                        .addGroup(readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                            .addComponent(bConnect, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, 160, Short.MAX_VALUE)
                            .addComponent(bInit, GroupLayout.DEFAULT_SIZE, 160, Short.MAX_VALUE))))
                .addContainerGap())
        );
        readerPanelLayout.setVerticalGroup(
            readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(readerPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(readerPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblReader)
                    .addComponent(cbReader, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bInit)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bConnect)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        fwiPanel.setBorder(BorderFactory.createTitledBorder("FWI, Polling Timeout and Transmit Frame Size"));
        lblFWI.setText("FWI Value");
        lblTO.setText("Polling Timeout");
        lblFS.setText("Transmit Frame Size");
        bGetFWI.setText("Get Current Values");
        bSetFWI.setText("Set Options");

        GroupLayout fwiPanelLayout = new GroupLayout(fwiPanel);
        fwiPanel.setLayout(fwiPanelLayout);
        fwiPanelLayout.setHorizontalGroup(
            fwiPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(fwiPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(fwiPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                    .addComponent(tFS, GroupLayout.Alignment.LEADING, 0, 0, Short.MAX_VALUE)
                    .addComponent(tPollTO, GroupLayout.Alignment.LEADING, 0, 0, Short.MAX_VALUE)
                    .addComponent(tFWI, GroupLayout.Alignment.LEADING, GroupLayout.PREFERRED_SIZE, 33, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(fwiPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(lblFWI)
                    .addComponent(lblTO)
                    .addComponent(lblFS, GroupLayout.PREFERRED_SIZE, 120, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, 25, Short.MAX_VALUE)
                .addGroup(fwiPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                    .addComponent(bSetFWI, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bGetFWI, GroupLayout.DEFAULT_SIZE, 145, Short.MAX_VALUE))
                .addContainerGap())
        );
        fwiPanelLayout.setVerticalGroup(
            fwiPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(fwiPanelLayout.createSequentialGroup()
                .addGroup(fwiPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(fwiPanelLayout.createSequentialGroup()
                        .addGap(3, 3, 3)
                        .addComponent(bGetFWI)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bSetFWI))
                    .addGroup(fwiPanelLayout.createSequentialGroup()
                        .addGroup(fwiPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(tFWI, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                            .addComponent(lblFWI))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(fwiPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(tPollTO, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                            .addComponent(lblTO))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(fwiPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(tFS, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                            .addComponent(lblFS))))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        antennaPanel.setBorder(BorderFactory.createTitledBorder("Antenna Field Setting"));
        bgAntenna.add(rbAntOn);
        rbAntOn.setText("Antenna ON");
        bgAntenna.add(rbAntOff);
        rbAntOff.setText("Antenna OFF");
        bGetAS.setText("Get Current Settings");
        bSetAS.setText("Set Antenna Option");

        GroupLayout antennaPanelLayout = new GroupLayout(antennaPanel);
        antennaPanel.setLayout(antennaPanelLayout);
        antennaPanelLayout.setHorizontalGroup(
            antennaPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(antennaPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(antennaPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(antennaPanelLayout.createSequentialGroup()
                        .addComponent(rbAntOn, GroupLayout.DEFAULT_SIZE, 151, Short.MAX_VALUE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED))
                    .addGroup(antennaPanelLayout.createSequentialGroup()
                        .addComponent(rbAntOff)
                        .addGap(41, 41, 41)))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(antennaPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                    .addComponent(bSetAS, GroupLayout.DEFAULT_SIZE, 158, Short.MAX_VALUE)
                    .addComponent(bGetAS, GroupLayout.DEFAULT_SIZE, 158, Short.MAX_VALUE))
                .addContainerGap())
        );
        antennaPanelLayout.setVerticalGroup(
            antennaPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(antennaPanelLayout.createSequentialGroup()
                .addGroup(antennaPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(rbAntOn)
                    .addComponent(bGetAS))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(antennaPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(rbAntOff)
                    .addComponent(bSetAS))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        transSetPanel.setBorder(BorderFactory.createTitledBorder("Tranceiver Settings"));
        cbFilter.setText("      LP Filter (On/Off)");
        lblFStop.setText("Field Stop Time (x5 ms)");
        lblSetup.setText("Setup time (x10 ms)");
        lblRecGain.setText("Receiver Gain");
        lblTxMode.setText("TX Mode");
        bGetTranSet.setText("Get Current Setting");
        bSetTranSet.setText("Set Tranceiver Options");

        GroupLayout transSetPanelLayout = new GroupLayout(transSetPanel);
        transSetPanel.setLayout(transSetPanelLayout);
        transSetPanelLayout.setHorizontalGroup(
            transSetPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(transSetPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(transSetPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(transSetPanelLayout.createSequentialGroup()
                        .addGroup(transSetPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                            .addComponent(tSetup, GroupLayout.Alignment.LEADING, 0, 0, Short.MAX_VALUE)
                            .addComponent(tFStop, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 34, Short.MAX_VALUE))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(transSetPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                            .addComponent(lblSetup, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(lblFStop, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)))
                    .addGroup(transSetPanelLayout.createSequentialGroup()
                        .addGroup(transSetPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                            .addGroup(GroupLayout.Alignment.LEADING, transSetPanelLayout.createSequentialGroup()
                                .addGroup(transSetPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                                    .addComponent(tTxMode, GroupLayout.Alignment.LEADING, 0, 0, Short.MAX_VALUE)
                                    .addComponent(tRecGain, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 34, Short.MAX_VALUE))
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addGroup(transSetPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                                    .addComponent(lblTxMode, GroupLayout.PREFERRED_SIZE, 89, GroupLayout.PREFERRED_SIZE)
                                    .addComponent(lblRecGain, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)))
                            .addComponent(cbFilter, GroupLayout.Alignment.LEADING, GroupLayout.PREFERRED_SIZE, 138, GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(transSetPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                            .addComponent(bGetTranSet, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(bSetTranSet, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        transSetPanelLayout.setVerticalGroup(
            transSetPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(transSetPanelLayout.createSequentialGroup()
                .addGroup(transSetPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(tFStop, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(lblFStop))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(transSetPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(tSetup, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(lblSetup))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(transSetPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(transSetPanelLayout.createSequentialGroup()
                        .addComponent(cbFilter)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(transSetPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(tRecGain, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                            .addComponent(lblRecGain))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(transSetPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(tTxMode, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                            .addComponent(lblTxMode)))
                    .addGroup(transSetPanelLayout.createSequentialGroup()
                        .addComponent(bGetTranSet)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bSetTranSet)))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        errHandPanel.setBorder(BorderFactory.createTitledBorder("PICC T=CL Data Exchange Error Handling"));

        lblPc2Pi.setText("PCD to PICC");
        lblPi2Pc.setText("PICC to PCD");
        bGetEH.setText("Get Current Value");
        bSetEH.setText("Set Error Handling");

        GroupLayout errHandPanelLayout = new GroupLayout(errHandPanel);
        errHandPanel.setLayout(errHandPanelLayout);
        errHandPanelLayout.setHorizontalGroup(
            errHandPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(errHandPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(errHandPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                    .addComponent(tPi2Pc, GroupLayout.Alignment.LEADING, 0, 0, Short.MAX_VALUE)
                    .addComponent(tPc2Pi, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 36, Short.MAX_VALUE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(errHandPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(lblPi2Pc, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(lblPc2Pi, GroupLayout.DEFAULT_SIZE, 77, Short.MAX_VALUE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(errHandPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(bGetEH, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bSetEH, GroupLayout.DEFAULT_SIZE, 136, Short.MAX_VALUE))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        errHandPanelLayout.setVerticalGroup(
            errHandPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(errHandPanelLayout.createSequentialGroup()
                .addGroup(errHandPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(tPc2Pi, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(lblPc2Pi)
                    .addComponent(bGetEH))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(errHandPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(errHandPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                        .addComponent(tPi2Pc, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addComponent(lblPi2Pc))
                    .addComponent(bSetEH))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        PICCPanel.setBorder(BorderFactory.createTitledBorder("PICC Settings"));

        lblPICC1.setText("MOD_B1");
        lblPICC2.setText("COND_B1");
        lblPICC3.setText("RX_B1");
        lblPICC4.setText("MOD_B2");
        lblPICC5.setText("COND_B2");
        lblPICC6.setText("RX_B2");
        lblPICC7.setText("MOD_A1");
        lblPICC8.setText("COND_A1");
        lblPICC9.setText("RX_A1");
        lblPICC10.setText("MOD_A2");
        lblPICC11.setText("COND_A2");
        lblPICC12.setText("RX_A2");
        bGetPICC.setText("Get PICC Setting");
        bSetPICC.setText("Set PICC Option");

        GroupLayout PICCPanelLayout = new GroupLayout(PICCPanel);
        PICCPanel.setLayout(PICCPanelLayout);
        PICCPanelLayout.setHorizontalGroup(
            PICCPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(PICCPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(PICCPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                    .addComponent(tPICC6, GroupLayout.Alignment.LEADING, 0, 0, Short.MAX_VALUE)
                    .addComponent(tPICC5, GroupLayout.Alignment.LEADING, 0, 0, Short.MAX_VALUE)
                    .addComponent(tPICC4, GroupLayout.Alignment.LEADING, 0, 0, Short.MAX_VALUE)
                    .addComponent(tPICC3, GroupLayout.Alignment.LEADING, 0, 0, Short.MAX_VALUE)
                    .addComponent(tPICC2, GroupLayout.Alignment.LEADING, 0, 0, Short.MAX_VALUE)
                    .addComponent(tPICC1, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 34, Short.MAX_VALUE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(PICCPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(lblPICC1, GroupLayout.PREFERRED_SIZE, 68, GroupLayout.PREFERRED_SIZE)
                    .addGroup(PICCPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                        .addComponent(lblPICC4, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(lblPICC3, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(lblPICC2, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 54, Short.MAX_VALUE)
                        .addComponent(lblPICC5, GroupLayout.Alignment.LEADING, 0, 0, Short.MAX_VALUE)
                        .addComponent(lblPICC6, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(PICCPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(tPICC12, 0, 0, Short.MAX_VALUE)
                    .addComponent(tPICC11, 0, 0, Short.MAX_VALUE)
                    .addComponent(tPICC10, 0, 0, Short.MAX_VALUE)
                    .addComponent(tPICC9, 0, 0, Short.MAX_VALUE)
                    .addComponent(tPICC8, 0, 0, Short.MAX_VALUE)
                    .addComponent(tPICC7, GroupLayout.DEFAULT_SIZE, 36, Short.MAX_VALUE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(PICCPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(lblPICC8, GroupLayout.DEFAULT_SIZE, 52, Short.MAX_VALUE)
                    .addGroup(PICCPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                        .addComponent(lblPICC12, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(lblPICC11, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(lblPICC7, GroupLayout.DEFAULT_SIZE, 52, Short.MAX_VALUE))
                    .addComponent(lblPICC10, GroupLayout.DEFAULT_SIZE, 52, Short.MAX_VALUE)
                    .addComponent(lblPICC9, GroupLayout.DEFAULT_SIZE, 52, Short.MAX_VALUE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(PICCPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(bSetPICC, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bGetPICC, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addContainerGap())
        );
        PICCPanelLayout.setVerticalGroup(
            PICCPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(PICCPanelLayout.createSequentialGroup()
                .addGroup(PICCPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(tPICC1, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(tPICC7, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(lblPICC1)
                    .addComponent(lblPICC7))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(PICCPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(tPICC2, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(tPICC8, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(lblPICC2)
                    .addComponent(lblPICC8))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(PICCPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(PICCPanelLayout.createSequentialGroup()
                        .addGroup(PICCPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                            .addGroup(PICCPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                                .addComponent(tPICC3, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                                .addComponent(tPICC9, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                                .addComponent(lblPICC3))
                            .addComponent(lblPICC9))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(PICCPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(tPICC4, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                            .addComponent(tPICC10, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                            .addComponent(lblPICC4)
                            .addComponent(lblPICC10))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(PICCPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(tPICC5, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                            .addComponent(tPICC11, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                            .addComponent(lblPICC11)
                            .addComponent(lblPICC5, GroupLayout.PREFERRED_SIZE, 14, GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(PICCPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(tPICC6, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                            .addComponent(tPICC12, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                            .addComponent(lblPICC12)
                            .addComponent(lblPICC6)))
                    .addGroup(PICCPanelLayout.createSequentialGroup()
                        .addComponent(bGetPICC)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(bSetPICC)))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        pollingPanel.setBorder(BorderFactory.createTitledBorder("Polling for Specific PICC Types"));
        bgPICCType.add(rbType1);
        rbType1.setText("ISO14443 Type A only");
        bgPICCType.add(rbType2);
        rbType2.setText("ISO14443 Type B only");
        bgPICCType.add(rbType3);
        rbType3.setText("ISO14443 Types A/B");
        bGetPSet.setText("Get Current Setting");
        bSetPSet.setText("Set PICC Type");
        bPoll.setText("Start Auto Detection");
        lblMsg.setText("Message");

        GroupLayout pollingPanelLayout = new GroupLayout(pollingPanel);
        pollingPanel.setLayout(pollingPanelLayout);
        pollingPanelLayout.setHorizontalGroup(
            pollingPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(pollingPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(pollingPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(pollingPanelLayout.createSequentialGroup()
                        .addComponent(lblMsg)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(tMsg, GroupLayout.DEFAULT_SIZE, 281, Short.MAX_VALUE))
                    .addGroup(pollingPanelLayout.createSequentialGroup()
                        .addGroup(pollingPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                            .addGroup(pollingPanelLayout.createSequentialGroup()
                                .addComponent(rbType1, GroupLayout.PREFERRED_SIZE, 150, GroupLayout.PREFERRED_SIZE)
                                .addGap(18, 18, 18))
                            .addComponent(rbType2, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, 192, Short.MAX_VALUE)
                            .addComponent(rbType3, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, 192, Short.MAX_VALUE))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(pollingPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                            .addComponent(bSetPSet, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(bGetPSet, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addGroup(pollingPanelLayout.createSequentialGroup()
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(bPoll, GroupLayout.PREFERRED_SIZE, 150, GroupLayout.PREFERRED_SIZE)))))
                .addContainerGap())
        );
        pollingPanelLayout.setVerticalGroup(
            pollingPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(pollingPanelLayout.createSequentialGroup()
                .addGroup(pollingPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(pollingPanelLayout.createSequentialGroup()
                        .addComponent(rbType1)
                        .addGap(2, 2, 2)
                        .addComponent(rbType2)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(rbType3))
                    .addGroup(pollingPanelLayout.createSequentialGroup()
                        .addComponent(bGetPSet)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bSetPSet)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bPoll)))
                .addGap(18, 18, 18)
                .addGroup(pollingPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblMsg)
                    .addComponent(tMsg, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        PPSPanel.setBorder(BorderFactory.createTitledBorder("PPS Setting (Communication Speed)"));

        maxSpeedPanel.setBorder(BorderFactory.createTitledBorder("Maximum Speed"));
        bgMaxSpeed.add(rbMax1);
        rbMax1.setText("106 kbps");
        bgMaxSpeed.add(rbMax2);
        rbMax2.setText("212 kbps");
        bgMaxSpeed.add(rbMax3);
        rbMax3.setText("424 kbps");
        bgMaxSpeed.add(rbMax4);
        rbMax4.setText("848 kbps");
        bgMaxSpeed.add(rbMax5);
        rbMax5.setText("No Auto PPS");

        GroupLayout maxSpeedPanelLayout = new GroupLayout(maxSpeedPanel);
        maxSpeedPanel.setLayout(maxSpeedPanelLayout);
        maxSpeedPanelLayout.setHorizontalGroup(
            maxSpeedPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(maxSpeedPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(maxSpeedPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(maxSpeedPanelLayout.createSequentialGroup()
                        .addComponent(rbMax2, GroupLayout.DEFAULT_SIZE, 114, Short.MAX_VALUE)
                        .addContainerGap())
                    .addGroup(maxSpeedPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                        .addComponent(rbMax5, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(rbMax4, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(rbMax3, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(rbMax1, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 103, Short.MAX_VALUE))))
        );
        maxSpeedPanelLayout.setVerticalGroup(
            maxSpeedPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(maxSpeedPanelLayout.createSequentialGroup()
                .addComponent(rbMax1)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbMax2)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbMax3)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbMax4)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(rbMax5)
                .addContainerGap())
        );

        currSpeedPanel.setBorder(BorderFactory.createTitledBorder("Current Speed"));
        bgCurrSpeed.add(rbCurr1);
        rbCurr1.setText("106 kbps");
        bgCurrSpeed.add(rbCurr2);
        rbCurr2.setText("212 kbps");
        bgCurrSpeed.add(rbCurr3);
        rbCurr3.setText("424 kbps");
        bgCurrSpeed.add(rbCurr4);
        rbCurr4.setText("848 kbps");
        bgCurrSpeed.add(rbCurr5);
        rbCurr5.setText("No Auto PPS");

        GroupLayout currSpeedPanelLayout = new GroupLayout(currSpeedPanel);
        currSpeedPanel.setLayout(currSpeedPanelLayout);
        currSpeedPanelLayout.setHorizontalGroup(
            currSpeedPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(currSpeedPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(currSpeedPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(rbCurr1)
                    .addComponent(rbCurr2)
                    .addComponent(rbCurr3)
                    .addComponent(rbCurr4)
                    .addComponent(rbCurr5))
                .addContainerGap(36, Short.MAX_VALUE))
        );
        currSpeedPanelLayout.setVerticalGroup(
            currSpeedPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(currSpeedPanelLayout.createSequentialGroup()
                .addComponent(rbCurr1)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbCurr2)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbCurr3)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbCurr4)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(rbCurr5)
                .addContainerGap())
        );

        bGetPPS.setText("Get Current Setting");
        bSetPPS.setText("Set PPS Value");

        GroupLayout PPSPanelLayout = new GroupLayout(PPSPanel);
        PPSPanel.setLayout(PPSPanelLayout);
        PPSPanelLayout.setHorizontalGroup(
            PPSPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(PPSPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(PPSPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(PPSPanelLayout.createSequentialGroup()
                        .addComponent(maxSpeedPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addGap(38, 38, 38))
                    .addGroup(PPSPanelLayout.createSequentialGroup()
                        .addComponent(bGetPPS,  GroupLayout.PREFERRED_SIZE, 150, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)))
                .addGroup(PPSPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(currSpeedPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(bSetPPS, GroupLayout.PREFERRED_SIZE, 150, GroupLayout.PREFERRED_SIZE))
                .addContainerGap())
        );
        PPSPanelLayout.setVerticalGroup(
            PPSPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(PPSPanelLayout.createSequentialGroup()
                .addGroup(PPSPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(maxSpeedPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(currSpeedPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(PPSPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(bSetPPS)
                    .addComponent(bGetPPS))
                .addContainerGap())
        );

        regPanel.setBorder(BorderFactory.createTitledBorder("RC531 Register Setting"));
        lblRegCurr.setText("Register Number");
        lblRegNew.setText("Register Value");
        bGetReg.setText("Get Current Value");
        bSetReg.setText("Set New Value");

        GroupLayout regPanelLayout = new GroupLayout(regPanel);
        regPanel.setLayout(regPanelLayout);
        regPanelLayout.setHorizontalGroup(
            regPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(regPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(regPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                    .addComponent(tRegVal, GroupLayout.Alignment.LEADING, 0, 0, Short.MAX_VALUE)
                    .addComponent(tRegNum, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 34, Short.MAX_VALUE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(regPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(lblRegNew, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(lblRegCurr, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, 14, Short.MAX_VALUE)
                .addGroup(regPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                    .addComponent(bSetReg, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bGetReg, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addContainerGap())
        );
        regPanelLayout.setVerticalGroup(
            regPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(regPanelLayout.createSequentialGroup()
                .addGroup(regPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(regPanelLayout.createSequentialGroup()
                        .addGroup(regPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(tRegNum, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                            .addComponent(lblRegCurr))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(regPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                            .addComponent(tRegVal, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                            .addComponent(lblRegNew)))
                    .addGroup(regPanelLayout.createSequentialGroup()
                        .addComponent(bGetReg)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bSetReg)))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        refISPanel.setBorder(BorderFactory.createTitledBorder("Refresh Interface Status"));
        bgRIS.add(rbRIS1);
        rbRIS1.setText("ICC Interface");
        bgRIS.add(rbRIS2);
        rbRIS2.setText("PICC Interface");
        bgRIS.add(rbRIS3);
        rbRIS3.setText("SAM Interface");
        bRefIS.setText("Refresh Interface");

        GroupLayout refISPanelLayout = new GroupLayout(refISPanel);
        refISPanel.setLayout(refISPanelLayout);
        refISPanelLayout.setHorizontalGroup(
            refISPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(refISPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(refISPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(refISPanelLayout.createSequentialGroup()
                        .addComponent(rbRIS2)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, 68, Short.MAX_VALUE)
                        .addComponent(bRefIS))
                    .addComponent(rbRIS1)
                    .addComponent(rbRIS3))
                .addContainerGap())
        );
        refISPanelLayout.setVerticalGroup(
            refISPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(refISPanelLayout.createSequentialGroup()
                .addComponent(rbRIS1)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(refISPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(rbRIS2)
                    .addComponent(bRefIS))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbRIS3)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        mMsg.setColumns(20);
        mMsg.setRows(5);
        scrPaneMsg.setViewportView(mMsg);

        bClear.setText("Clear");
        bReset.setText("Reset");
        bQuit.setText("Quit");

        GroupLayout msgPanelLayout = new GroupLayout(msgPanel);
        msgPanel.setLayout(msgPanelLayout);
        msgPanelLayout.setHorizontalGroup(
            msgPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(msgPanelLayout.createSequentialGroup()
                .addGap(18, 18, 18)
                .addComponent(bClear, GroupLayout.PREFERRED_SIZE, 102, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(bReset, GroupLayout.PREFERRED_SIZE, 102, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(bQuit, GroupLayout.PREFERRED_SIZE, 101, GroupLayout.PREFERRED_SIZE))
            .addComponent(scrPaneMsg, GroupLayout.PREFERRED_SIZE, 355, GroupLayout.PREFERRED_SIZE)
        );
        msgPanelLayout.setVerticalGroup(
            msgPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(GroupLayout.Alignment.TRAILING, msgPanelLayout.createSequentialGroup()
                .addComponent(scrPaneMsg, GroupLayout.DEFAULT_SIZE, 250, Short.MAX_VALUE)
                .addGap(18, 18, 18)
                .addGroup(msgPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(bClear)
                    .addComponent(bReset)
                    .addComponent(bQuit))
                .addContainerGap())
        );

        GroupLayout layout = new GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(antennaPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(readerPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(fwiPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(transSetPanel, 0, 289, Short.MAX_VALUE)
                    .addComponent(errHandPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(pollingPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(PICCPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(PPSPanel, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addGap(6, 6, 6)
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                        .addComponent(refISPanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(regPanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                    .addComponent(msgPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addGap(17, 17, 17))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                    .addGroup(GroupLayout.Alignment.LEADING, layout.createSequentialGroup()
                        .addGap(22, 22, 22)
                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                            .addComponent(PICCPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(regPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                                .addGap(11, 11, 11)
                                .addComponent(refISPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(msgPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                    .addGroup(GroupLayout.Alignment.LEADING, layout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(readerPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(pollingPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                                .addComponent(PPSPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(fwiPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                                .addComponent(antennaPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                                .addComponent(transSetPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                                .addComponent(errHandPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)))))
                .addContainerGap())
        );
		
        bInit.setMnemonic(KeyEvent.VK_I);
        bConnect.setMnemonic(KeyEvent.VK_C);
        bReset.setMnemonic(KeyEvent.VK_R);
        bQuit.setMnemonic(KeyEvent.VK_Q);
        bClear.setMnemonic(KeyEvent.VK_L);
        bGetFWI.setMnemonic(KeyEvent.VK_G);
        bSetFWI.setMnemonic(KeyEvent.VK_S);
        bGetAS.setMnemonic(KeyEvent.VK_U);
        bSetAS.setMnemonic(KeyEvent.VK_A);
        bGetTranSet.setMnemonic(KeyEvent.VK_E);
        bSetTranSet.setMnemonic(KeyEvent.VK_T);
        bGetEH.setMnemonic(KeyEvent.VK_V);
        bSetEH.setMnemonic(KeyEvent.VK_H);
        bGetPICC.setMnemonic(KeyEvent.VK_P);
        bSetPICC.setMnemonic(KeyEvent.VK_O);
        bGetPSet.setMnemonic(KeyEvent.VK_N);
        bSetPSet.setMnemonic(KeyEvent.VK_T);
        bPoll.setMnemonic(KeyEvent.VK_D);

        bInit.addActionListener(this);
        bConnect.addActionListener(this);
        bReset.addActionListener(this);
        bQuit.addActionListener(this);
        bClear.addActionListener(this);
        bGetFWI.addActionListener(this);
        bSetFWI.addActionListener(this);
        bGetAS.addActionListener(this);
        bSetAS.addActionListener(this);
        bGetTranSet.addActionListener(this);
        bSetTranSet.addActionListener(this);
        bGetEH.addActionListener(this);
        bSetEH.addActionListener(this);
        bGetPICC.addActionListener(this);
        bSetPICC.addActionListener(this);
        bGetPSet.addActionListener(this);
        bSetPSet.addActionListener(this);
        bPoll.addActionListener(this);
        bGetPPS.addActionListener(this);
        bSetPPS.addActionListener(this);
        bGetReg.addActionListener(this);
        bSetReg.addActionListener(this);
        bRefIS.addActionListener(this);
        
        tFWI.addKeyListener(this);
        tSetup.addKeyListener(this);
        tFS.addKeyListener(this);
        tPollTO.addKeyListener(this);
        tFStop.addKeyListener(this);
        tRecGain.addKeyListener(this);
        tTxMode.addKeyListener(this);
        tPc2Pi.addKeyListener(this);
        tPi2Pc.addKeyListener(this);
        tPICC1.addKeyListener(this);
        tPICC2.addKeyListener(this);
        tPICC3.addKeyListener(this);
        tPICC4.addKeyListener(this);
        tPICC5.addKeyListener(this);
        tPICC6.addKeyListener(this);
        tPICC7.addKeyListener(this);
        tPICC8.addKeyListener(this);
        tPICC9.addKeyListener(this);
        tPICC10.addKeyListener(this);
        tPICC11.addKeyListener(this);
        tPICC12.addKeyListener(this);
        tRegNum.addKeyListener(this);
        tRegVal.addKeyListener(this);
        
        
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
			
			//Look for ACR128 SAM and make it the default reader in the combobox
			for (int i = 0; i < cchReaders[0]; i++)
			{
				
				cbReader.setSelectedIndex(i);
				
				if (((String) cbReader.getSelectedItem()).lastIndexOf("ACR128U SAM")> -1)
					break;
				else
					cbReader.setSelectedIndex(0);
				
			}
			
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
			tFWI.setEnabled(true);
			tPollTO.setEnabled(true);
			tFS.setEnabled(true);
			bGetFWI.setEnabled(true);
			bSetFWI.setEnabled(true);
			rbAntOn.setEnabled(true);
			rbAntOff.setEnabled(true);
			bGetAS.setEnabled(true);
			bSetAS.setEnabled(true);
			tFStop.setEnabled(true);
			tSetup.setEnabled(true);
			cbFilter.setEnabled(true);
			tRecGain.setEnabled(true);
			tTxMode.setEnabled(true);
			bGetTranSet.setEnabled(true);
			bSetTranSet.setEnabled(true);
			tPc2Pi.setEnabled(true);
			tPi2Pc.setEnabled(true);
			bGetEH.setEnabled(true);
			bSetEH.setEnabled(true);
			tPICC1.setEnabled(true);
			tPICC2.setEnabled(true);
			tPICC3.setEnabled(true);
			tPICC4.setEnabled(true);
			tPICC5.setEnabled(true);
			tPICC6.setEnabled(true);
			tPICC7.setEnabled(true);
			tPICC8.setEnabled(true);
			tPICC9.setEnabled(true);
			tPICC10.setEnabled(true);
			tPICC11.setEnabled(true);
			tPICC12.setEnabled(true);
			bGetPICC.setEnabled(true);
			bSetPICC.setEnabled(true);
			rbType1.setEnabled(true);
			rbType2.setEnabled(true);
			rbType3.setEnabled(true);
			bGetPSet.setEnabled(true);
			bSetPSet.setEnabled(true);
			bPoll.setEnabled(true);
			tMsg.setEnabled(true);
			rbMax1.setEnabled(true);
			rbMax2.setEnabled(true);
			rbMax3.setEnabled(true);
			rbMax4.setEnabled(true);
			rbMax5.setEnabled(true);
			rbCurr1.setEnabled(true);
			rbCurr2.setEnabled(true);
			rbCurr3.setEnabled(true);
			rbCurr4.setEnabled(true);
			rbCurr5.setEnabled(true);
			bGetPPS.setEnabled(true);
			bSetPPS.setEnabled(true);
			tRegNum.setEnabled(true);
			tRegVal.setEnabled(true);
			bGetReg.setEnabled(true);
			bSetReg.setEnabled(true);
			rbRIS1.setEnabled(true);
			rbRIS2.setEnabled(true);
			rbRIS3.setEnabled(true);
			bRefIS.setEnabled(true);
			rbType3.setSelected(true);
			rbRIS3.setSelected(true);
			
		}
		
		if(bClear == e.getSource())
		{
			
			mMsg.setText("");
			
		}
		
		if(bQuit==e.getSource())
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
			timer.stop();
			
		}
		
		if(bGetFWI == e.getSource())
		{
			
			String tmpStr = "", tmpHex="";
			
			clearBuffers();
			SendBuff[0] = (byte) 0x1F;
			SendBuff[1] = (byte) 0x00;
			SendLen = 2; 
			RecvLen[0] = 8;
			
			retCode = callCardControl();
			
			if(retCode!=ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i=0; i<5; i++)
			{
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += tmpHex;  
				
			}
			
			if(tmpStr.equals("E100000003"))
			{
				
				//interpret response data
				tFWI.setText(Integer.toHexString(((Byte)RecvBuff[5]).intValue() & 0xFF).toUpperCase());
				tPollTO.setText(Integer.toHexString(((Byte)RecvBuff[6]).intValue() & 0xFF).toUpperCase());
				tFS.setText(Integer.toHexString(((Byte)RecvBuff[7]).intValue() & 0xFF).toUpperCase());
				
			}
			else
			{
				
				tFWI.setText("");
				tPollTO.setText("");
				tFS.setText("");
				displayOut(3, 0, "Invalid Response");
				
			}
			
		}
		
		if(bSetFWI == e.getSource())
		{
			
			String tmpStr="", tmpHex = "";
			
			if(tFWI.getText().equals(""))
			{
		
				tFWI.requestFocus();
				return;
				
			}
			
			if(tPollTO.getText().equals(""))
			{

				tPollTO.requestFocus();
				return;
				
			}
			
			if(tFS.getText().equals(""))
			{

				tFS.requestFocus();
				return;
				
			}
			
			clearBuffers();
			SendBuff[0] = (byte)0x1F;
			SendBuff[1] = (byte)0x03;
			SendBuff[2] = (byte)((Integer)Integer.parseInt(tFWI.getText(), 16)).byteValue();
			SendBuff[3] = (byte)((Integer)Integer.parseInt(tPollTO.getText(), 16)).byteValue();
			SendBuff[4] = (byte)((Integer)Integer.parseInt(tFS.getText(), 16)).byteValue();
			SendLen = 5;
			RecvLen[0]= 8;
			retCode = callCardControl();
			
			if (retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i=0; i<5; i++)
			{
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += tmpHex;  
				
			}
			
			if(!tmpStr.equals("E100000003"))
				displayOut(3, 0, "Invalid response");
			
		}
		
		if(bGetAS == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			
			clearBuffers();
			SendBuff[0] = (byte)0x25;
			SendBuff[1] = (byte)0x00;
			SendLen = 2;
			RecvLen[0] = 6;
			retCode = callCardControl();
			
			if(retCode!=ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i=0; i<5; i++)
			{
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += tmpHex;  
				
			}
			
			if(tmpStr.equals("E100000001"))
			{
				
				//interpret response data
				if(RecvBuff[5] == 0)
					rbAntOff.setSelected(true);
				else
					rbAntOn.setSelected(true);
				
			}
			else
			{
				
				rbAntOff.setSelected(false);
				rbAntOn.setSelected(false);
				displayOut(3, 0, "Invalid response");
				
			}
			
		}
		
		if(bSetAS == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			
			readPollingOptions();
			if((RecvBuff[5] & 0x01) != 0)
			{
				displayOut(0,0, "Turn off automatic PICC polling in the device before using this function.");
				return;
			}
			
			clearBuffers();
			SendBuff[0] = (byte)0x25;
			SendBuff[1] = (byte)0x01;
			
			if(rbAntOff.isSelected())
				SendBuff[2] = (byte)0x00;
			else
			{
				rbAntOff.requestFocus();
				return;
			}
			
			SendLen = 3;
			RecvLen[0] = 6;
			retCode = callCardControl();
			
			if (retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i=0; i<5; i++)
			{
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += tmpHex;  
				
			}
			
			if(!tmpStr.equals("E100000001"))
				displayOut(3, 0, "Invalid Response");
			
		}
		
		if(bGetTranSet==e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			int tmpVal;
			
			clearBuffers();
			SendBuff[0] = (byte)0x20;
			SendBuff[1] = (byte)0x01;
			SendLen = 2;
			RecvLen[0] = 9;
			retCode = callCardControl();
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i=0; i<6; i++)
			{
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += tmpHex;  
				
			}

			if(tmpStr.equals("E10000000406"))
			{
				
				//interpret response data
				tmpVal = RecvBuff[6] >> 4;
				tFStop.setText(""+tmpVal);
				tmpVal = RecvBuff[6] & 0x0F;
				tSetup.setText(""+tmpVal);
				
				if((RecvBuff[7] & 0x04) != 0)
					cbFilter.setSelected(true);
				else
					cbFilter.setSelected(false);
				
				tmpVal = RecvBuff[7] & 0x03;
				tRecGain.setText(""+tmpVal);
				tTxMode.setText(Integer.toHexString(((Byte)RecvBuff[8]).intValue() & 0xFF).toUpperCase());
				
			}
			else
			{
				
				tFStop.setText("");
				tSetup.setText("");
				cbFilter.setSelected(false);
				tRecGain.setText("");
				tTxMode.setText("");
				displayOut(3, 0, "Invalid Response");
				
			}
			
		}
		
		if(bSetTranSet == e.getSource())
		{
			
			if(tFStop.getText().equals(""))
			{

				tFStop.requestFocus();
				return;
				
			}
			
			if(tSetup.getText().equals(""))
			{

				tSetup.requestFocus();
				return;
				
			}
			
			if(tRecGain.getText().equals(""))
			{

				tRecGain.requestFocus();
				return;
				
			}
			
			if(tTxMode.getText().equals(""))
			{

				tTxMode.requestFocus();
				return;
				
			}
			
			if(Integer.parseInt(tFStop.getText())>15)
			{
				
				tFStop.setText("15");
				tFStop.requestFocus();
				return;
				
			}
			
			if(Integer.parseInt(tSetup.getText())>15)
			{
				
				tSetup.setText("15");
				tSetup.requestFocus();
				return;
				
			}
			
			if(Integer.parseInt(tRecGain.getText())>3)
			{
				
				tRecGain.setText("3");
				tRecGain.requestFocus();
				return;
				
			}
			
			clearBuffers();
			SendBuff[0] = (byte)0x20;
			SendBuff[1] = (byte)0x04;
			SendBuff[2] = (byte)0x06;
			SendBuff[3] = (byte)(Integer.parseInt(tFStop.getText()) >> 4);
			SendBuff[3] = (byte)(Integer.parseInt(tSetup.getText()) + SendBuff[3]);
			
			if(cbFilter.isSelected())
				SendBuff[4] = (byte)0x04;
			
			SendBuff[4] = (byte)(SendBuff[4] + Integer.parseInt(tRecGain.getText()));
			SendBuff[5] = (byte)((Integer)Integer.parseInt(tTxMode.getText(), 16)).byteValue();
			SendLen = 6;
			RecvLen[0] = 5;
			retCode = callCardControl();
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			if((RecvBuff[0] & 0xFF) != 0xE1)
				displayOut(3, 0, "Invalid Response");
			
		}
		
		if(bGetEH == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			int tmpVal=0;
			
			clearBuffers();
			SendBuff[0] = (byte) 0x2C;
			SendBuff[1] = (byte) 0x00;
			SendLen = 2;
			RecvLen[0] = 7;
			retCode = callCardControl();
			
			if (retCode!=ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i=0; i<5; i++)
			{
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += tmpHex;  
				
			}
			
			if((tmpStr.equals("E100000002"))&&((RecvBuff[6] & 0xFF) == 0x7F))
			{
				
				//interpret response data
				tmpVal = RecvBuff[5] >> 4;
				tPc2Pi.setText(""+tmpVal);
				tmpVal = RecvBuff[5] & 0x03;
				tPi2Pc.setText(""+tmpVal);
				
			}
			else
			{
				
				tPc2Pi.setText("");
				tPi2Pc.setText("");
				displayOut(3, 0, "Invalid Response");
				
			}
			
		}
		
		if(bSetEH == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			
			if(tPc2Pi.getText().equals(""))
			{

				tPc2Pi.requestFocus();
				return;
				
			}
			
			if(tPi2Pc.getText().equals(""))
			{

				tPi2Pc.requestFocus();
				return;
				
			}
			
			if(Integer.parseInt(tPc2Pi.getText())>3)
			{
				
				tPc2Pi.setText("3");
				tPc2Pi.requestFocus();
				return;
				
			}
			
			if(Integer.parseInt(tPi2Pc.getText())>3)
			{
				
				tPi2Pc.setText("3");
				tPi2Pc.requestFocus();
				return;
				
			}
			
			clearBuffers();
			SendBuff[0] = (byte)0x2C;
			SendBuff[1] = (byte)0x02;
			SendBuff[2] = (byte)(Integer.parseInt(tPc2Pi.getText()) << 4);
			SendBuff[2] = (byte)(SendBuff[2] + Integer.parseInt(tPi2Pc.getText()));
			SendBuff[3] = (byte)0x7F;
			SendLen = 4;
			RecvLen[0] = 7;
			retCode = callCardControl();
			
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i=0; i<5; i++)
			{
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += tmpHex;  
				
			}
			
			if(!tmpStr.equals("E100000002"))
				displayOut(3, 0, "Invalid Response");
		}
		
		if(bGetPICC == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			
			clearBuffers();
			SendBuff[0] = (byte)0x2A;
			SendBuff[1] = (byte)0x00;
			SendLen = 2;
			RecvLen[0] = 17;
			retCode = callCardControl();
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i=0; i<5; i++)
			{
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += tmpHex;  
				
			}
			
			if (tmpStr.equals("E10000000C"))
			{
				
				//interpret response data
				tPICC1.setText(Integer.toHexString(((Byte)RecvBuff[5]).intValue() & 0xFF).toUpperCase());
				tPICC2.setText(Integer.toHexString(((Byte)RecvBuff[6]).intValue() & 0xFF).toUpperCase());
				tPICC3.setText(Integer.toHexString(((Byte)RecvBuff[7]).intValue() & 0xFF).toUpperCase());
				tPICC4.setText(Integer.toHexString(((Byte)RecvBuff[8]).intValue() & 0xFF).toUpperCase());
				tPICC5.setText(Integer.toHexString(((Byte)RecvBuff[9]).intValue() & 0xFF).toUpperCase());
				tPICC6.setText(Integer.toHexString(((Byte)RecvBuff[10]).intValue() & 0xFF).toUpperCase());
				tPICC7.setText(Integer.toHexString(((Byte)RecvBuff[11]).intValue() & 0xFF).toUpperCase());
				tPICC8.setText(Integer.toHexString(((Byte)RecvBuff[12]).intValue() & 0xFF).toUpperCase());
				tPICC9.setText(Integer.toHexString(((Byte)RecvBuff[13]).intValue() & 0xFF).toUpperCase());
				tPICC10.setText(Integer.toHexString(((Byte)RecvBuff[14]).intValue() & 0xFF).toUpperCase());
				tPICC11.setText(Integer.toHexString(((Byte)RecvBuff[15]).intValue() & 0xFF).toUpperCase());
				tPICC12.setText(Integer.toHexString(((Byte)RecvBuff[16]).intValue() & 0xFF).toUpperCase());
				
			}
			else
			{
				tPICC1.setText("");
				tPICC2.setText("");
				tPICC3.setText("");
				tPICC4.setText("");
				tPICC5.setText("");
				tPICC6.setText("");
				tPICC7.setText("");
				tPICC8.setText("");
				tPICC9.setText("");
				tPICC10.setText("");
				tPICC11.setText("");
				tPICC12.setText("");
				displayOut(3, 0, "Invalid Response");
				
			}
			
		}
		
		if(bSetPICC == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			
			if(tPICC1.getText().equals(""))
			{

				tPICC1.requestFocus();
				return;
				
			}
			
			if(tPICC2.getText().equals(""))
			{

				tPICC2.requestFocus();
				return;
				
			}
			
			if(tPICC3.getText().equals(""))
			{

				tPICC3.requestFocus();
				return;
				
			}
			
			if(tPICC4.getText().equals(""))
			{

				tPICC4.requestFocus();
				return;
				
			}
			
			if(tPICC5.getText().equals(""))
			{

				tPICC5.requestFocus();
				return;
				
			}
			
			if(tPICC6.getText().equals(""))
			{

				tPICC6.requestFocus();
				return;
				
			}
			
			if(tPICC7.getText().equals(""))
			{
	
				tPICC7.requestFocus();
				return;
				
			}
			
			if(tPICC8.getText().equals(""))
			{
	
				tPICC8.requestFocus();
				return;
				
			}
			
			if(tPICC9.getText().equals(""))
			{

				tPICC9.requestFocus();
				return;
				
			}
			
			if(tPICC10.getText().equals(""))
			{
				tPICC10.selectAll();
				tPICC10.requestFocus();
				return;
			}
			
			if(tPICC11.getText().equals(""))
			{
				tPICC11.selectAll();
				tPICC11.requestFocus();
				return;
			}
			
			if(tPICC12.getText().equals(""))
			{
				tPICC12.selectAll();
				tPICC12.requestFocus();
				return;
			}
			
			clearBuffers();
			SendBuff[0] = (byte)0x2A;
			SendBuff[1] = (byte)0x0C;
			SendBuff[2] = (byte)((Integer)Integer.parseInt(tPICC1.getText(), 16)).byteValue();
			SendBuff[3] = (byte)((Integer)Integer.parseInt(tPICC2.getText(), 16)).byteValue();
			SendBuff[4] = (byte)((Integer)Integer.parseInt(tPICC3.getText(), 16)).byteValue();
			SendBuff[5] = (byte)((Integer)Integer.parseInt(tPICC4.getText(), 16)).byteValue();
			SendBuff[6] = (byte)((Integer)Integer.parseInt(tPICC5.getText(), 16)).byteValue();
			SendBuff[7] = (byte)((Integer)Integer.parseInt(tPICC6.getText(), 16)).byteValue();
			SendBuff[8] = (byte)((Integer)Integer.parseInt(tPICC7.getText(), 16)).byteValue();
			SendBuff[9] = (byte)((Integer)Integer.parseInt(tPICC8.getText(), 16)).byteValue();
			SendBuff[10] = (byte)((Integer)Integer.parseInt(tPICC9.getText(), 16)).byteValue();
			SendBuff[11] = (byte)((Integer)Integer.parseInt(tPICC10.getText(), 16)).byteValue();
			SendBuff[12] = (byte)((Integer)Integer.parseInt(tPICC11.getText(), 16)).byteValue();
			SendBuff[13] = (byte)((Integer)Integer.parseInt(tPICC12.getText(), 16)).byteValue();
			SendLen = 14;
			RecvLen[0] = 17;
			retCode = callCardControl();
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i=0; i<5; i++)
			{
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += tmpHex;  
				
			}
			
			if(!tmpStr.equals("E10000000C"))
				displayOut(3, 0, "Invalid Response");
			
		}
		
		if(bGetPSet == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			
			clearBuffers();
			SendBuff[0] = (byte)0x20;
			SendBuff[1] = (byte)0x00;
			SendBuff[3] = (byte)0xFF;
			SendLen = 4;
			RecvLen[0] = 6;
			retCode = callCardControl();
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i=0; i<5; i++)
			{
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += tmpHex;  
				
			}
			
			if(!tmpStr.equals("E100000001"))
			{
				tMsg.setText("Invalid Card Detected");
				return;
			}
			
			//interpret status
			switch(RecvBuff[5] & 0xFF)
			{
				case 1: rbType1.setSelected(true);break;
				case 2: rbType2.setSelected(true);break;
				case 3: rbType3.setSelected(true); break;
			}
			
		}
		
		if(bSetPSet == e.getSource())
		{
			
			if(rbType1.isSelected())
				reqType=1;
			else
				if(rbType2.isSelected())
					reqType = 2;
				else
					if(rbType3.isSelected())
						reqType=3;
					else
					{
						rbType1.requestFocus();
						return;
					}
			
			clearBuffers();
			SendBuff[0] = (byte)0x20;
			SendBuff[1] = (byte)0x02;
			
			switch(reqType)
			{
			
				case 1: SendBuff[2] = (byte)0x01; break;
				case 2: SendBuff[2] = (byte)0x02; break;
				case 3: SendBuff[2] = (byte)0x03; break;
			
			}
			
			SendBuff[3] = (byte)0xFF;
			SendLen = 4;
			RecvLen[0] = 5;
			retCode = callCardControl();
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
		}
		
		if(bGetPPS == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			
			clearBuffers();
			SendBuff[0] = (byte)0x24;
			SendBuff[1] = (byte)0x00;
			SendLen = 2;
			RecvLen[0] = 7;
			retCode = callCardControl();
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i=0; i<5; i++)
			{
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += tmpHex;  
				
			}
			
			if(tmpStr.equals("E100000002"))
			{
				
				//interpret response data
				switch(RecvBuff[5] & 0xFF)
				{
					
					case 0: rbMax1.setSelected(true); break;
					case 1: rbMax2.setSelected(true); break;
					case 2: rbMax3.setSelected(true); break;
					case 3: rbMax4.setSelected(true); break;
					default: rbMax5.setSelected(true); break;
				
				}
				
				switch (RecvBuff[6] & 0xFF)
				{
					case 0:rbCurr1.setSelected(true);break;
					case 1:rbCurr2.setSelected(true);break;
					case 2:rbCurr3.setSelected(true);break;
					case 3:rbCurr4.setSelected(true);break;
					default:rbCurr5.setSelected(true);break;
				}
				
			}
			else
			{
				
				rbMax1.setSelected(false);
				rbMax2.setSelected(false);
				rbMax3.setSelected(false);
				rbMax4.setSelected(false);
				rbMax5.setSelected(false);
				rbCurr1.setSelected(false);
				rbCurr2.setSelected(false);
				rbCurr3.setSelected(false);
				rbCurr4.setSelected(false);
				rbCurr5.setSelected(false);
				displayOut(3, 0, "Invalid response");
				
			}
			
		}
		
		if(bSetPPS == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			
			if((!rbMax1.isSelected())&&(!rbMax2.isSelected())&&(!rbMax3.isSelected())&&(!rbMax4.isSelected())&&(!rbMax5.isSelected()))
				rbMax3.setSelected(true);
			
			if((!rbCurr1.isSelected())&&(!rbCurr2.isSelected())&&(!rbCurr3.isSelected())&&(!rbCurr4.isSelected())&&(!rbCurr5.isSelected()))
				rbCurr3.setSelected(true);
			
			clearBuffers();
			SendBuff[0] = (byte)0x24;
			SendBuff[1] = (byte)0x01;
			
			if(rbMax1.isSelected())
				SendBuff[2] = (byte)0x00;
			
			if(rbMax2.isSelected())
				SendBuff[2] = (byte)0x01;
			
			if(rbMax3.isSelected())
				SendBuff[2] = (byte)0x02;
			
			if(rbMax4.isSelected())
				SendBuff[2] = (byte)0x03;
			
			if(rbMax5.isSelected())
				SendBuff[2] = (byte)0xFF;
			
			SendLen = 3;
			RecvLen[0] = 7;
			retCode = callCardControl();
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i=0; i<5; i++)
			{
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += tmpHex;  
				
			}
			
			if(!tmpStr.equals("E100000002"))
				displayOut(3, 0, "Invalid response");
			
		}
		
		if(bGetReg == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			
			if(tRegNum.getText().equals(""))
			{
				tRegNum.requestFocus();
				return;
			}
			
			clearBuffers();
			SendBuff[0] = (byte)0x019;
			SendBuff[2] = (byte)0x01;
			SendBuff[3] = (byte)((Integer)Integer.parseInt(tRegNum.getText(), 16)).byteValue();
			SendLen = 3;
			RecvLen[0] = 6;
			retCode = callCardControl();
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i=0; i<5; i++)
			{
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += tmpHex;  
				
			}
			
			if(tmpStr.equals("E100000001"))
			{
				
				//interpret response data
				tRegVal.setText(Integer.toHexString(((Byte)RecvBuff[5]).intValue() & 0xFF).toUpperCase());
				
			}
			else
			{
				
				tRegNum.setText("");
				displayOut(3, 0, "Invalid response");
				
			}
			
		}
		
		if(bSetReg == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			
			if(tRegNum.getText().equals(""))
			{

				tRegNum.requestFocus();
				return;
				
			}
			
			if(tRegVal.getText().equals(""))
			{
				
				tRegVal.requestFocus();
				return;
				
			}
			
			clearBuffers();
			SendBuff[0] = (byte)0x1A;
			SendBuff[1] = (byte)0x02;
			SendBuff[2] = (byte)((Integer)Integer.parseInt(tRegNum.getText(), 16)).byteValue();
			SendBuff[3] = (byte)((Integer)Integer.parseInt(tRegVal.getText(), 16)).byteValue();
			SendLen = 4;
			RecvLen[0] = 6;
			retCode = callCardControl();
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(int i=0; i<5; i++)
			{
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += tmpHex;  
				
			}
			
			if(tmpStr.equals("E100000001"))
				tRegVal.setText(Integer.toHexString(((Byte)RecvBuff[5]).intValue() & 0xFF).toUpperCase());
			else
			{
				tRegNum.setText("");
				tRegVal.setText("");
				displayOut(3, 0, "Invalid response");
			}
			
		}
		
		if(bRefIS == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			int i=0;
			
			if(connActive)
				retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
			
			while(((String) cbReader.getSelectedItem()).lastIndexOf("ACR128U SAM")== -1)
			{
				
				if (i == cbReader.getItemCount())
				{
					displayOut(0, 0, "Cannot find ACR128 SAM reader.");
					connActive = false;
					return;
				}
				
				i++;
				cbReader.setSelectedIndex(i);
				
			}
			
			String rdrcon = (String)cbReader.getSelectedItem();  	 
			
			//1. For SAM Refresh, connect to SAM Interface in direct mode
			if(rbRIS1.isSelected())
			{
				
				retCode = jacs.jSCardConnect(hContext, 
											rdrcon, 
											ACSModule.SCARD_SHARE_DIRECT,
											0,
											hCard, 
											PrefProtocols);
				
				if(retCode != ACSModule.SCARD_S_SUCCESS)
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
			else
			{	
				//2. For other interfaces, connect to SAM Interface in direct or shared mode
				retCode = jacs.jSCardConnect(hContext, 
											rdrcon, 
											ACSModule.SCARD_SHARE_SHARED | ACSModule.SCARD_SHARE_DIRECT,
											0,
											hCard, 
											PrefProtocols);
				
				if(retCode != ACSModule.SCARD_S_SUCCESS)
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
			
			clearBuffers();
			SendBuff[0] = (byte)0x2D;
			SendBuff[1] = (byte)0x01;
			
			if(rbRIS1.isSelected())
				SendBuff[2] = (byte)0x01;
			
			if(rbRIS2.isSelected())
				SendBuff[2] = (byte)0x02;
			
			if(rbRIS3.isSelected())
				SendBuff[2] = (byte)0x04;
			
			SendLen = 3;
			RecvLen[0] = 6;
			retCode = callCardControl();
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			for(i=0; i<5; i++)
			{
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += tmpHex;  
				
			}
			
			if(!tmpStr.equals("E100000001"))
			{
				
				displayOut(3, 0, "Invalid response");
				return;
				
			}
			
			//For SAM interface, disconnect and connect to SAM Interface in direct or shared mode
			if(rbRIS3.isSelected())
			{
				
				if(connActive)
					retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
				
				while(((String) cbReader.getSelectedItem()).lastIndexOf("ACR128U SAM")== -1)
				{
					
					if (i == cbReader.getItemCount())
					{
						displayOut(0, 0, "Cannot find ACR128 SAM reader.");
						connActive = false;
						return;
					}
					
					i++;
					cbReader.setSelectedIndex(i);
					
				}
				
				//String rdrcon = (String)cbReader.getSelectedItem();  
				
				retCode = jacs.jSCardConnect(hContext, 
											 rdrcon, 
											 ACSModule.SCARD_SHARE_SHARED | ACSModule.SCARD_SHARE_DIRECT,
											 0,
											 hCard, 
											 PrefProtocols);
				
				if(retCode != ACSModule.SCARD_S_SUCCESS)
				{
					displayOut(1, retCode, "");
					connActive = false;
					return;
				}
				
			}
			else
				displayOut(0, 0, "Successful connection to " + (String)cbReader.getSelectedItem());
			
		}
		
		if(bPoll == e.getSource())
		{
			
			if(autoDet)
			{
				
				autoDet = false;
				bPoll.setText("Start Auto Detection");
				tMsg.setText("Polling Stopped...");
				timer.stop();
				return;
				
			}
			
			tMsg.setText("Polling Started...");
			autoDet = true;
			bPoll.setText("End Auto Detection");
			timer = new Timer(500, pollTimer);
			timer.start();
			
		}
				
	}

	ActionListener pollTimer = new ActionListener() {
	      public void actionPerformed(ActionEvent evt) 
	      {
	    	
	    	  String tmpStr="";
	    	  
	    	  int i=0;
				
				while(((String) cbReader.getSelectedItem()).lastIndexOf("ACR128U PICC")== -1)
				{
					
					if (i == cbReader.getItemCount())
					{
						displayOut(0, 0, "Cannot find ACR128 PICC reader.");
						timer.stop();
						return;
					}
					
					i++;
					cbReader.setSelectedIndex(i);
					
				}
				
				String rdrcon = (String)cbReader.getSelectedItem();  
				rdrState.RdrName = rdrcon;
				
				retCode = jacs.jSCardGetStatusChange(hContext, 
													 0, 
													 rdrState, 
													 1);

				if(retCode == ACSModule.SCARD_S_SUCCESS)
				{
					
					if((rdrState.RdrEventState & ACSModule.SCARD_STATE_PRESENT) !=0)
					{
						
						switch(reqType)
						{
						
							case 1: tmpStr = "ISO14443 Type A card"; break;
							case 2: tmpStr = "ISO14443 Type B card"; break;
							default: tmpStr = "ISO14443 card"; break;
						
						}
						
						tMsg.setText(tmpStr+" is detected");
						
					}
					else
						tMsg.setText("No card within range");
					
				}
	    	  
	      }
	      
	};
	
	public void readPollingOptions()
	{
		
		clearBuffers();
		SendBuff[0] = (byte)0x23;
		SendBuff[1] = (byte)0x00;
		SendLen = 2;
		RecvLen[0] = 6;
		retCode = callCardControl();
		
		if(retCode != ACSModule.SCARD_S_SUCCESS)
			return;
		
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
		tFWI.setText("");
		tPollTO.setText("");
		tFS.setText("");
		tFWI.setEnabled(false);
		tPollTO.setEnabled(false);
		tFS.setEnabled(false);
		bGetFWI.setEnabled(false);
		bSetFWI.setEnabled(false);
		rbAntOn.setEnabled(false);
		rbAntOff.setEnabled(false);
		bGetAS.setEnabled(false);
		bSetAS.setEnabled(false);
		tFStop.setEnabled(false);
		tFStop.setText("");
		tSetup.setEnabled(false);
		tSetup.setText("");
		cbFilter.setEnabled(false);
		tRecGain.setEnabled(false);
		tRecGain.setText("");
		tTxMode.setEnabled(false);
		tTxMode.setText("");
		bGetTranSet.setEnabled(false);
		bSetTranSet.setEnabled(false);
		tPc2Pi.setText("");
		tPc2Pi.setEnabled(false);
		tPi2Pc.setText("");
		tPi2Pc.setEnabled(false);
		bGetEH.setEnabled(false);
		bSetEH.setEnabled(false);
		tPICC1.setText("");
		tPICC2.setText("");
		tPICC3.setText("");
		tPICC4.setText("");
		tPICC5.setText("");
		tPICC6.setText("");
		tPICC7.setText("");
		tPICC8.setText("");
		tPICC9.setText("");
		tPICC10.setText("");
		tPICC11.setText("");
		tPICC12.setText("");
		tPICC1.setEnabled(false);
		tPICC2.setEnabled(false);
		tPICC3.setEnabled(false);
		tPICC4.setEnabled(false);
		tPICC5.setEnabled(false);
		tPICC6.setEnabled(false);
		tPICC7.setEnabled(false);
		tPICC8.setEnabled(false);
		tPICC9.setEnabled(false);
		tPICC10.setEnabled(false);
		tPICC11.setEnabled(false);
		tPICC12.setEnabled(false);
		bGetPICC.setEnabled(false);
		bSetPICC.setEnabled(false);
		rbType1.setEnabled(false);
		rbType2.setEnabled(false);
		rbType3.setEnabled(false);
		bGetPSet.setEnabled(false);
		bSetPSet.setEnabled(false);
		bPoll.setEnabled(false);
		tMsg.setText("");
		tMsg.setEnabled(false);
		rbMax1.setEnabled(false);
		rbMax2.setEnabled(false);
		rbMax3.setEnabled(false);
		rbMax4.setEnabled(false);
		rbMax5.setEnabled(false);
		rbCurr1.setEnabled(false);
		rbCurr2.setEnabled(false);
		rbCurr3.setEnabled(false);
		rbCurr4.setEnabled(false);
		rbCurr5.setEnabled(false);
		bGetPPS.setEnabled(false);
		bSetPPS.setEnabled(false);
		tRegNum.setText("");
		tRegNum.setEnabled(false);
		tRegVal.setText("");
		tRegVal.setEnabled(false);
		bGetReg.setEnabled(false);
		bSetReg.setEnabled(false);
		rbRIS1.setEnabled(false);
		rbRIS2.setEnabled(false);
		rbRIS3.setEnabled(false);
		bRefIS.setEnabled(false);
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
  		if(tFStop.isFocusOwner() || tSetup.isFocusOwner() || tRecGain.isFocusOwner() || tPc2Pi.isFocusOwner() || tPi2Pc.isFocusOwner())
  		{
  			
  			if (VALIDCHARS.indexOf(x) == -1 ) 
  				ke.setKeyChar(empty);		
	  			
  		}
  		else
  		{
  			
  			//Check valid characters
  			if (VALIDCHARSHEX.indexOf(x) == -1 ) 
  				ke.setKeyChar(empty);
  			
  		}
  			
  		//Limit character length
	  	if(tPc2Pi.isFocusOwner() || tPi2Pc.isFocusOwner())
	  	{
  			if(((JTextField)ke.getSource()).getText().length() >= 1 ) 
	  		{
	  			
		  		ke.setKeyChar(empty);	
		  		return;
	  			
	  		}
	  	}
		else
		{
				
			if(((JTextField)ke.getSource()).getText().length() >= 2 ) 
  			{
  			
	  			ke.setKeyChar(empty);	
	  			return;
  			
  			}	
				
		}
  	    	
	}
    
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new AdvDevProg().setVisible(true);
            }
        });
    }



}
