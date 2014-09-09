/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              mifareProg.java

  Description:       This sample program outlines the steps on how to
                     transact with Mifare 1K/4K cards using ACR128

  Author:            M.J.E.C. Castillo

  Date:              July 7, 2008

  Revision Trail:   (Date/Author/Description)

======================================================================*/

import java.io.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;


public class mifareProg extends JFrame implements ActionListener, KeyListener{

	//JPCSC Variables
	int retCode;
	boolean connActive; 
	static String VALIDCHARS = "0123456789";
	static String VALIDCHARSHEX = "ABCDEFabcdef0123456789";
	
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
    private JPanel authPanel, binBlkPanel, msgPanel, readerPanel, storeAuthPanel, valBlkPanel;
    private JButton bBinRead, bClear, bConn, bInit, bLoadKey, bReset, bValDec, bValInc;
    private JButton bValRead, bValRes, bValStore, bauth, bBinUpd, bQuit;
    private ButtonGroup bdKeyType, bgKeySource, bgStoreKeys;
    private JComboBox cbreader;
    private JLabel jLabel1, jLabel10, jLabel11, jLabel12, jLabel2, jLabel3, jLabel4;
    private JLabel jLabel5, jLabel6, jLabel7, jLabel8, jLabel9, lblReader;
    private JPanel keyInPanel, keySourcePanel, keyTypePanel, keyValPanel;
    private JTextArea mMsg;
    private JRadioButton rbKeyA, rbKeyB, rbManual, rbNonVol, rbNonVolMem, rbVol, rbVolatile;
    private JScrollPane scrlPaneMsg;
    private JTextField tBinBlk, tBinData, tBinLen, tBlkNo, tKey1, tKey2, tKey3, tKey4, tKey5;
    private JTextField tKey6, tKeyAdd, tKeyIn1, tKeyIn2, tKeyIn3, tKeyIn4, tKeyIn5, tKeyIn6;
    private JTextField tMemAdd, tValAmt, tValBlk, tValSrc, tValTar;
	
	static JacspcscLoader jacs = new JacspcscLoader();
    

    public mifareProg() {
    	
    	this.setTitle("Mifare 1K 4K");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

		setSize(800,730);
        bgStoreKeys = new ButtonGroup();
        bgKeySource = new ButtonGroup();
        bdKeyType = new ButtonGroup();
        readerPanel = new JPanel();
        lblReader = new JLabel();
        bInit = new JButton();
        bConn = new JButton();
        storeAuthPanel = new JPanel();
        rbNonVolMem = new JRadioButton();
        rbVol = new JRadioButton();
        jLabel1 = new JLabel();
        jLabel2 = new JLabel();
        bLoadKey = new JButton();
        keyValPanel = new JPanel();
        tKey1 = new JTextField();
        tKey2 = new JTextField();
        tKey3 = new JTextField();
        tKey4 = new JTextField();
        tKey5 = new JTextField();
        tKey6 = new JTextField();
        tMemAdd = new JTextField();
        authPanel = new JPanel();
        keySourcePanel = new JPanel();
        rbManual = new JRadioButton();
        rbVolatile = new JRadioButton();
        rbNonVol = new JRadioButton();
        jLabel3 = new JLabel();
        jLabel4 = new JLabel();
        jLabel5 = new JLabel();
        keyTypePanel = new JPanel();
        rbKeyA = new JRadioButton();
        rbKeyB = new JRadioButton();
        bauth = new JButton();
        keyInPanel = new JPanel();
        tKeyIn1 = new JTextField();
        tKeyIn2 = new JTextField();
        tKeyIn3 = new JTextField();
        tKeyIn4 = new JTextField();
        tKeyIn5 = new JTextField();
        tKeyIn6 = new JTextField();
        tKeyAdd = new JTextField();
        tBlkNo = new JTextField();
        binBlkPanel = new JPanel();
        jLabel6 = new JLabel();
        jLabel7 = new JLabel();
        jLabel8 = new JLabel();
        tBinBlk = new JTextField();
        tBinLen = new JTextField();
        tBinData = new JTextField();
        bBinRead = new JButton();
        bBinUpd = new JButton();
        valBlkPanel = new JPanel();
        jLabel9 = new JLabel();
        tValAmt = new JTextField();
        jLabel10 = new JLabel();
        tValBlk = new JTextField();
        jLabel11 = new JLabel();
        tValSrc = new JTextField();
        jLabel12 = new JLabel();
        tValTar = new JTextField();
        bValStore = new JButton();
        bValInc = new JButton();
        bValDec = new JButton();
        bValRead = new JButton();
        bValRes = new JButton();
        msgPanel = new JPanel();
        scrlPaneMsg = new JScrollPane();
        mMsg = new JTextArea();
        bClear = new JButton();
        bReset = new JButton();
        bQuit = new JButton();
        
        lblReader.setText("Select Reader");

		String[] rdrNameDef = {"Please select reader                   "};	
		cbreader = new JComboBox(rdrNameDef);
		cbreader.setSelectedIndex(0);
		
        bInit.setText("Initialize");
        bConn.setText("Connect");

        GroupLayout readerPanelLayout = new GroupLayout(readerPanel);
        readerPanel.setLayout(readerPanelLayout);
        readerPanelLayout.setHorizontalGroup(
            readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(readerPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(readerPanelLayout.createSequentialGroup()
                        .addComponent(lblReader)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(cbreader, 0, 207, Short.MAX_VALUE))
                    .addComponent(bInit, GroupLayout.Alignment.TRAILING, GroupLayout.PREFERRED_SIZE, 110, GroupLayout.PREFERRED_SIZE)
                    .addComponent(bConn, GroupLayout.Alignment.TRAILING, GroupLayout.PREFERRED_SIZE, 109, GroupLayout.PREFERRED_SIZE))
                .addContainerGap())
        );
        readerPanelLayout.setVerticalGroup(
            readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(readerPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(readerPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblReader)
                    .addComponent(cbreader, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bInit)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bConn)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        storeAuthPanel.setBorder(BorderFactory.createTitledBorder("Store Authentication Keys to Device"));
        bgStoreKeys.add(rbNonVolMem);
        rbNonVolMem.setText("Non-volatile Memory");
        bgStoreKeys.add(rbVol);
        rbVol.setText("Volatile Memory");
        jLabel1.setText("key Store No.");
        jLabel2.setText("Key Value Input");
        bLoadKey.setText("Load Key");

        GroupLayout keyValPanelLayout = new GroupLayout(keyValPanel);
        keyValPanel.setLayout(keyValPanelLayout);
        keyValPanelLayout.setHorizontalGroup(
            keyValPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(keyValPanelLayout.createSequentialGroup()
                .addComponent(tKey1, GroupLayout.PREFERRED_SIZE, 29, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tKey2, GroupLayout.PREFERRED_SIZE, 31, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tKey3, GroupLayout.PREFERRED_SIZE, 30, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tKey4, GroupLayout.PREFERRED_SIZE, 30, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tKey5, GroupLayout.PREFERRED_SIZE, 30, GroupLayout.PREFERRED_SIZE)
                .addGap(8, 8, 8)
                .addComponent(tKey6, GroupLayout.PREFERRED_SIZE, 30, GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );
        keyValPanelLayout.setVerticalGroup(
            keyValPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(keyValPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                .addComponent(tKey1, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addComponent(tKey2, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addComponent(tKey3, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addComponent(tKey4, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addComponent(tKey5, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addComponent(tKey6, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
        );

        GroupLayout storeAuthPanelLayout = new GroupLayout(storeAuthPanel);
        storeAuthPanel.setLayout(storeAuthPanelLayout);
        storeAuthPanelLayout.setHorizontalGroup(
            storeAuthPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(storeAuthPanelLayout.createSequentialGroup()
                .addGroup(storeAuthPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(storeAuthPanelLayout.createSequentialGroup()
                        .addGroup(storeAuthPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                            .addComponent(jLabel1)
                            .addComponent(jLabel2))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(storeAuthPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                            .addComponent(tMemAdd, GroupLayout.PREFERRED_SIZE, 29, GroupLayout.PREFERRED_SIZE)
                            .addComponent(keyValPanel, GroupLayout.PREFERRED_SIZE, 211, GroupLayout.PREFERRED_SIZE)))
                    .addGroup(storeAuthPanelLayout.createSequentialGroup()
                        .addComponent(rbNonVolMem)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(rbVol))
                    .addGroup(storeAuthPanelLayout.createSequentialGroup()
                        .addGap(101, 101, 101)
                        .addComponent(bLoadKey, GroupLayout.PREFERRED_SIZE, 115, GroupLayout.PREFERRED_SIZE)))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        storeAuthPanelLayout.setVerticalGroup(
            storeAuthPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(storeAuthPanelLayout.createSequentialGroup()
                .addGroup(storeAuthPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(rbVol)
                    .addComponent(rbNonVolMem))
                .addGap(7, 7, 7)
                .addGroup(storeAuthPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel1)
                    .addComponent(tMemAdd, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(storeAuthPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                    .addComponent(jLabel2)
                    .addComponent(keyValPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bLoadKey)
                .addContainerGap(17, Short.MAX_VALUE))
        );

        authPanel.setBorder(BorderFactory.createTitledBorder("Authentication Function"));

        keySourcePanel.setBorder(BorderFactory.createTitledBorder("Key Source"));
        bgKeySource.add(rbManual);
        rbManual.setText("Manual");
        bgKeySource.add(rbVolatile);
        rbVolatile.setText("Volatile Memory");
        bgKeySource.add(rbNonVol);
        rbNonVol.setText("Non-Volatile Memory");

        GroupLayout keySourcePanelLayout = new GroupLayout(keySourcePanel);
        keySourcePanel.setLayout(keySourcePanelLayout);
        keySourcePanelLayout.setHorizontalGroup(
            keySourcePanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(keySourcePanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(keySourcePanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(rbManual)
                    .addComponent(rbVolatile)
                    .addComponent(rbNonVol))
                .addContainerGap(12, Short.MAX_VALUE))
        );
        keySourcePanelLayout.setVerticalGroup(
            keySourcePanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(keySourcePanelLayout.createSequentialGroup()
                .addComponent(rbManual)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(rbVolatile)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbNonVol))
        );

        jLabel3.setText("Block No. (Dec)");
        jLabel4.setText("Key Store No.");
        jLabel5.setText("Key Input Value");

        keyTypePanel.setBorder(BorderFactory.createTitledBorder("Key Type"));
        bdKeyType.add(rbKeyA);
        rbKeyA.setText("Key A");
        bdKeyType.add(rbKeyB);
        rbKeyB.setText("Key B");

        GroupLayout keyTypePanelLayout = new GroupLayout(keyTypePanel);
        keyTypePanel.setLayout(keyTypePanelLayout);
        keyTypePanelLayout.setHorizontalGroup(
            keyTypePanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(keyTypePanelLayout.createSequentialGroup()
                .addGap(20, 20, 20)
                .addGroup(keyTypePanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                    .addComponent(rbKeyB)
                    .addComponent(rbKeyA))
                .addContainerGap(29, Short.MAX_VALUE))
        );
        keyTypePanelLayout.setVerticalGroup(
            keyTypePanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(keyTypePanelLayout.createSequentialGroup()
                .addComponent(rbKeyA)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, 18, Short.MAX_VALUE)
                .addComponent(rbKeyB)
                .addContainerGap())
        );

        bauth.setText("Authenticate");

        GroupLayout keyInPanelLayout = new GroupLayout(keyInPanel);
        keyInPanel.setLayout(keyInPanelLayout);
        keyInPanelLayout.setHorizontalGroup(
            keyInPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(keyInPanelLayout.createSequentialGroup()
                .addComponent(tKeyIn1, GroupLayout.PREFERRED_SIZE, 29, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tKeyIn2, GroupLayout.PREFERRED_SIZE, 31, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tKeyIn3, GroupLayout.PREFERRED_SIZE, 30, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tKeyIn4, GroupLayout.PREFERRED_SIZE, 30, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tKeyIn5, GroupLayout.PREFERRED_SIZE, 30, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tKeyIn6, GroupLayout.PREFERRED_SIZE, 30, GroupLayout.PREFERRED_SIZE))
        );
        keyInPanelLayout.setVerticalGroup(
            keyInPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(keyInPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                .addComponent(tKeyIn1, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addComponent(tKeyIn2, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addComponent(tKeyIn3, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addComponent(tKeyIn4, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addComponent(tKeyIn5, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addComponent(tKeyIn6, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
        );

        GroupLayout authPanelLayout = new GroupLayout(authPanel);
        authPanel.setLayout(authPanelLayout);
        authPanelLayout.setHorizontalGroup(
            authPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(authPanelLayout.createSequentialGroup()
                .addGroup(authPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(authPanelLayout.createSequentialGroup()
                        .addComponent(keySourcePanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(keyTypePanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                    .addGroup(authPanelLayout.createSequentialGroup()
                        .addComponent(jLabel5)
                        .addGroup(authPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                            .addGroup(authPanelLayout.createSequentialGroup()
                                .addGap(14, 14, 14)
                                .addComponent(bauth, GroupLayout.PREFERRED_SIZE, 130, GroupLayout.PREFERRED_SIZE))
                            .addGroup(authPanelLayout.createSequentialGroup()
                                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(keyInPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))))
                    .addGroup(authPanelLayout.createSequentialGroup()
                        .addComponent(jLabel3)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(tBlkNo, GroupLayout.PREFERRED_SIZE, 29, GroupLayout.PREFERRED_SIZE))
                    .addGroup(authPanelLayout.createSequentialGroup()
                        .addComponent(jLabel4)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(tKeyAdd, GroupLayout.PREFERRED_SIZE, 29, GroupLayout.PREFERRED_SIZE)))
                .addContainerGap())
        );
        authPanelLayout.setVerticalGroup(
            authPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(authPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(authPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(keySourcePanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(keyTypePanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(authPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel3, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(tBlkNo, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addGap(5, 5, 5)
                .addGroup(authPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel4)
                    .addComponent(tKeyAdd, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(authPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(jLabel5)
                    .addComponent(keyInPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bauth)
                .addGap(18, 18, 18))
        );

        binBlkPanel.setBorder(BorderFactory.createTitledBorder("Binary Block Function"));

        jLabel6.setText("Start Block (Dec)");
        jLabel7.setText("Length");
        jLabel8.setText("Data (text)");
        bBinRead.setText("Read Block");
        bBinUpd.setText("Update Block");

        GroupLayout binBlkPanelLayout = new GroupLayout(binBlkPanel);
        binBlkPanel.setLayout(binBlkPanelLayout);
        binBlkPanelLayout.setHorizontalGroup(
            binBlkPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(binBlkPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(binBlkPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(tBinData, GroupLayout.DEFAULT_SIZE, 281, Short.MAX_VALUE)
                    .addGroup(binBlkPanelLayout.createSequentialGroup()
                        .addGap(25, 25, 25)
                        .addComponent(bBinRead, GroupLayout.PREFERRED_SIZE, 112, GroupLayout.PREFERRED_SIZE)
                        .addGap(18, 18, 18)
                        .addComponent(bBinUpd, GroupLayout.PREFERRED_SIZE, 120, GroupLayout.PREFERRED_SIZE))
                    .addGroup(binBlkPanelLayout.createSequentialGroup()
                        .addComponent(jLabel6)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(tBinBlk, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                        .addGap(26, 26, 26)
                        .addComponent(jLabel7)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(tBinLen, GroupLayout.PREFERRED_SIZE, 42, GroupLayout.PREFERRED_SIZE))
                    .addComponent(jLabel8))
                .addContainerGap())
        );
        binBlkPanelLayout.setVerticalGroup(
            binBlkPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(binBlkPanelLayout.createSequentialGroup()
                .addGroup(binBlkPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel6)
                    .addComponent(jLabel7)
                    .addComponent(tBinBlk, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(tBinLen, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jLabel8)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tBinData, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(binBlkPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(bBinUpd)
                    .addComponent(bBinRead))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        valBlkPanel.setBorder(BorderFactory.createTitledBorder("Value Block Function"));

        jLabel9.setText("Value Amount");
        jLabel10.setText("Block No.");
        jLabel11.setText("Source Block");
        jLabel12.setText("Target Block");
        bValStore.setText("Store Value");
        bValInc.setText("Increment");
        bValDec.setText("Decrement");
        bValRead.setText("Read Value");
        bValRes.setText("Restore Value");

        GroupLayout valBlkPanelLayout = new GroupLayout(valBlkPanel);
        valBlkPanel.setLayout(valBlkPanelLayout);
        valBlkPanelLayout.setHorizontalGroup(
            valBlkPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(valBlkPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(valBlkPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(valBlkPanelLayout.createSequentialGroup()
                        .addGroup(valBlkPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                            .addComponent(jLabel9)
                            .addComponent(jLabel10))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(valBlkPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                            .addComponent(tValAmt, GroupLayout.PREFERRED_SIZE, 104, GroupLayout.PREFERRED_SIZE)
                            .addGroup(valBlkPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                                .addComponent(tValSrc, GroupLayout.Alignment.LEADING, 0, 0, Short.MAX_VALUE)
                                .addComponent(tValBlk, GroupLayout.Alignment.LEADING, GroupLayout.PREFERRED_SIZE, 32, GroupLayout.PREFERRED_SIZE))))
                    .addComponent(jLabel11)
                    .addGroup(valBlkPanelLayout.createSequentialGroup()
                        .addComponent(jLabel12)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(tValTar, GroupLayout.PREFERRED_SIZE, 32, GroupLayout.PREFERRED_SIZE)))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(valBlkPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                    .addComponent(bValRes, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bValRead, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bValDec, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bValInc, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bValStore, GroupLayout.Alignment.LEADING, GroupLayout.PREFERRED_SIZE, 120, GroupLayout.PREFERRED_SIZE))
                .addContainerGap(30, Short.MAX_VALUE))
        );
        valBlkPanelLayout.setVerticalGroup(
            valBlkPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(valBlkPanelLayout.createSequentialGroup()
                .addGroup(valBlkPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel9)
                    .addComponent(tValAmt, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(bValStore))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(valBlkPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel10)
                    .addComponent(tValBlk, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(bValInc))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(valBlkPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel11)
                    .addComponent(tValSrc, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(bValDec))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(valBlkPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel12)
                    .addComponent(bValRead)
                    .addComponent(tValTar, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bValRes)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        mMsg.setColumns(20);
        mMsg.setRows(5);
        scrlPaneMsg.setViewportView(mMsg);

        bClear.setText("Clear");
        bReset.setText("Reset");
        bQuit.setText("Quit");

        GroupLayout msgPanelLayout = new GroupLayout(msgPanel);
        msgPanel.setLayout(msgPanelLayout);
        msgPanelLayout.setHorizontalGroup(
            msgPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(msgPanelLayout.createSequentialGroup()
                .addGroup(msgPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(msgPanelLayout.createSequentialGroup()
                        .addGap(14, 14, 14)
                        .addComponent(bClear, GroupLayout.PREFERRED_SIZE, 119, GroupLayout.PREFERRED_SIZE)
                        .addGap(18, 18, 18)
                        .addComponent(bReset, GroupLayout.PREFERRED_SIZE, 119, GroupLayout.PREFERRED_SIZE)
                        .addGap(18, 18, 18)
                        .addComponent(bQuit, GroupLayout.PREFERRED_SIZE, 118, GroupLayout.PREFERRED_SIZE))
                    .addGroup(GroupLayout.Alignment.TRAILING, msgPanelLayout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(scrlPaneMsg, GroupLayout.DEFAULT_SIZE, 318, Short.MAX_VALUE)))
                .addContainerGap())
        );
        msgPanelLayout.setVerticalGroup(
            msgPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(msgPanelLayout.createSequentialGroup()
                .addGap(6, 6, 6)
                .addComponent(scrlPaneMsg, GroupLayout.PREFERRED_SIZE, 419, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(msgPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(bQuit)
                    .addComponent(bReset)
                    .addComponent(bClear))
                .addContainerGap(21, Short.MAX_VALUE))
        );

        GroupLayout layout = new GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(readerPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                        .addComponent(authPanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 313, Short.MAX_VALUE)
                        .addComponent(binBlkPanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(storeAuthPanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)))
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(valBlkPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(msgPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(readerPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(storeAuthPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addGap(1, 1, 1)
                        .addComponent(authPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(binBlkPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                    .addGroup(layout.createSequentialGroup()
                        .addGap(8, 8, 8)
                        .addComponent(valBlkPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(msgPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        
        bInit.setMnemonic(KeyEvent.VK_I);
        bConn.setMnemonic(KeyEvent.VK_C);
        bReset.setMnemonic(KeyEvent.VK_R);
        bQuit.setMnemonic(KeyEvent.VK_Q);
        bClear.setMnemonic(KeyEvent.VK_L);
        bLoadKey.setMnemonic(KeyEvent.VK_L);
        bauth.setMnemonic(KeyEvent.VK_A);
        bBinRead.setMnemonic(KeyEvent.VK_E);
        bBinUpd.setMnemonic(KeyEvent.VK_U);
        bValStore.setMnemonic(KeyEvent.VK_S);
        bValInc.setMnemonic(KeyEvent.VK_I);
        bValDec.setMnemonic(KeyEvent.VK_D);
        bValRead.setMnemonic(KeyEvent.VK_E);
        bValRes.setMnemonic(KeyEvent.VK_T);

        bInit.addActionListener(this);
        bConn.addActionListener(this);
        bReset.addActionListener(this);
        bQuit.addActionListener(this);
        bClear.addActionListener(this);
        bLoadKey.addActionListener(this);
        bauth.addActionListener(this);
        bBinRead.addActionListener(this);
        bBinUpd.addActionListener(this);
        bValStore.addActionListener(this);
        bValInc.addActionListener(this);
        bValDec.addActionListener(this);
        bValRead.addActionListener(this);
        bValRes.addActionListener(this);
        rbManual.addActionListener(this);
        rbVolatile.addActionListener(this);
        rbNonVol.addActionListener(this);
        rbVol.addActionListener(this);
        rbNonVolMem.addActionListener(this);

        tMemAdd.addKeyListener(this);
        tKey1.addKeyListener(this);
        tKey2.addKeyListener(this);
        tKey3.addKeyListener(this);
        tKey4.addKeyListener(this);
        tKey5.addKeyListener(this);
        tKey6.addKeyListener(this);
        tBlkNo.addKeyListener(this);
        tKeyAdd.addKeyListener(this);
        tKeyIn1.addKeyListener(this);
        tKeyIn2.addKeyListener(this);
        tKeyIn3.addKeyListener(this);
        tKeyIn4.addKeyListener(this);
        tKeyIn5.addKeyListener(this);
        tKeyIn6.addKeyListener(this);
        tBinBlk.addKeyListener(this);
        tBinLen.addKeyListener(this);
        tValAmt.addKeyListener(this);
        tValBlk.addKeyListener(this);
        tValSrc.addKeyListener(this);
        tValTar.addKeyListener(this);
        
        
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
			cbreader.removeAllItems();
			
			for (int i = 0; i < cchReaders[0]-1; i++)
			{
				
			  	if (szReaders[i] == 0x00)
			  	{			  		
			  		
			  		cbreader.addItem(new String(szReaders, offset, i - offset));
			  		offset = i+1;
			  		
			  	}
			}
			
			if (cbreader.getItemCount() == 0)
				cbreader.addItem("No PC/SC reader detected");
			    
			cbreader.setSelectedIndex(0);
			bConn.setEnabled(true);
			bInit.setEnabled(false);
			bReset.setEnabled(true);
			
			//Look for ACR128 PICC and make it the default reader in the combobox
			for (int i = 0; i < cchReaders[0]; i++)
			{
				
				cbreader.setSelectedIndex(i);
				
				if (((String) cbreader.getSelectedItem()).lastIndexOf("ACR128U PICC")> -1)
					break;
				else
					cbreader.setSelectedIndex(0);
				
			}
			
		}
		
		if(bConn == e.getSource())
		{
			
			if(connActive)
			{
				
				retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
				
			}
			
			String rdrcon = (String)cbreader.getSelectedItem();  	      	      	
		    
		    retCode = jacs.jSCardConnect(hContext, 
		    							rdrcon, 
		    							ACSModule.SCARD_SHARE_SHARED,
		    							ACSModule.SCARD_PROTOCOL_T1 | ACSModule.SCARD_PROTOCOL_T0,
		      							hCard, 
		      							PrefProtocols);
		    
		    if (retCode != ACSModule.SCARD_S_SUCCESS)
		    {
		      	//check if ACR128 SAM is used and use Direct Mode if SAM is not detected
		      	if (((String) cbreader.getSelectedItem()).lastIndexOf("ACR128U SAM")> -1)
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
		    			
		    			displayOut(0, 0, "Successful connection to " + (String)cbreader.getSelectedItem());
		    			
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
		      	
		    	displayOut(0, 0, "Successful connection to " + (String)cbreader.getSelectedItem());
		      	
		    }
			
		    connActive=true;
		    rbKeyA.setSelected(true);
		    rbManual.setSelected(true);
			rbNonVolMem.setEnabled(true);
			rbVol.setEnabled(true);
			rbManual.setEnabled(true);
			rbNonVol.setEnabled(true);
			rbVolatile.setEnabled(true);
			rbKeyA.setEnabled(true);
			rbKeyB.setEnabled(true);
			tKey1.setEnabled(true);
			tKey2.setEnabled(true);
			tKey3.setEnabled(true);
			tKey4.setEnabled(true);
			tKey5.setEnabled(true);
			tKey6.setEnabled(true);
			tKeyIn1.setEnabled(true);
			tKeyIn2.setEnabled(true);
			tKeyIn3.setEnabled(true);
			tKeyIn4.setEnabled(true);
			tKeyIn5.setEnabled(true);
			tKeyIn6.setEnabled(true);
			tBlkNo.setEnabled(true);
			tKeyAdd.setEnabled(false);
			tMemAdd.setEnabled(true);
			tBinBlk.setEnabled(true);
			tBinLen.setEnabled(true);
			tBinData.setEnabled(true);
			tValAmt.setEnabled(true);
			tValBlk.setEnabled(true);
			tValSrc.setEnabled(true);
			tValTar.setEnabled(true);
			bLoadKey.setEnabled(true);
			bauth.setEnabled(true);
			bBinRead.setEnabled(true);
			bBinUpd.setEnabled(true);
			bValStore.setEnabled(true);
			bValInc.setEnabled(true);
			bValDec.setEnabled(true);
			bValRead.setEnabled(true);
			bValRes.setEnabled(true);

		}
		
		if(bClear == e.getSource())
		{
			
			mMsg.setText("");
			
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
			cbreader.removeAllItems();
			cbreader.addItem("Please select reader                   ");
			
		}
		
		if(bQuit == e.getSource())
		{
			
			this.dispose();
			
		}
		
		if(bLoadKey==e.getSource())
		{
			//check input
			if (!rbNonVolMem.isSelected())
			{
				if(!rbVol.isSelected()){
					rbNonVolMem.requestFocus();
					return;
				}
			}
			
			if (rbNonVolMem.isSelected())
			{
				
				if (tMemAdd.getText().equals(""))
				{

					tMemAdd.requestFocus();
					return;
					
				}
				
				if(((Integer)Integer.parseInt(tMemAdd.getText(), 16)).byteValue()>0x1F)
				{
					tMemAdd.setText("1F");
					return;
				}
			}
			
			if(tKey1.getText().equals(""))
			{

				tKey1.requestFocus();
				return;
				
			}
			
			if(tKey2.getText().equals(""))
			{
	
				tKey2.requestFocus();
				return;
				
			}
				
			if(tKey3.getText().equals(""))
			{
	
				tKey3.requestFocus();
				return;
				
			}

			if(tKey4.getText().equals(""))
			{
	
				tKey4.requestFocus();
				return;
				
			}

			if(tKey5.getText().equals(""))
			{

				tKey5.requestFocus();
				return;
				
			}
			
			if(tKey6.getText().equals(""))
			{

				tKey6.requestFocus();
				return;
				
			}
			
			clearBuffers();
			SendBuff[0] = (byte) 0xFF;
			SendBuff[1] = (byte) 0x82;
			
			if (rbNonVolMem.isSelected())
			{
				
				SendBuff[2] = (byte) 0x20;
				SendBuff[3]= (byte) ((Integer)Integer.parseInt(tMemAdd.getText(), 16)).byteValue();
				
			}
			else
			{
				
				SendBuff[2] = (byte) 0x00;
				SendBuff[3] = (byte) 0x20;
				
			}
			
			SendBuff[4] = (byte)0x06;
			SendBuff[5] = (byte)((Integer)Integer.parseInt(tKey1.getText(), 16)).byteValue();
			SendBuff[6] = (byte)((Integer)Integer.parseInt(tKey2.getText(), 16)).byteValue();
			SendBuff[7] = (byte)((Integer)Integer.parseInt(tKey3.getText(), 16)).byteValue();
			SendBuff[8] = (byte)((Integer)Integer.parseInt(tKey4.getText(), 16)).byteValue();
			SendBuff[9] = (byte)((Integer)Integer.parseInt(tKey5.getText(), 16)).byteValue();
			SendBuff[10] = (byte)((Integer)Integer.parseInt(tKey6.getText(), 16)).byteValue();
			
			SendLen = 0x0B;
			RecvLen[0] = 0x02;
			
			retCode = sendAPDUandDisplay(0);
			
			if (retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
		
		}
		
		if(bauth==e.getSource())
		{
			
			//validate input
			if(tBlkNo.getText().equals(""))
			{
				
				tBlkNo.selectAll();
				tBlkNo.requestFocus();
				return;
				
			}
			
			if(Integer.parseInt(tBlkNo.getText())>319)
			{
				
				tBlkNo.setText("319");
				return;
				
			}
			
			if (rbManual.isSelected())
			{
				
				if (tKeyIn1.getText().equals(""))
				{
					tKeyIn1.selectAll();
					tKeyIn1.requestFocus();
					return;
				}
				
				if (tKeyIn2.getText().equals(""))
				{
	
					tKeyIn2.requestFocus();
					return;
					
				}
	
				if (tKeyIn3.getText().equals(""))
				{
		
					tKeyIn3.requestFocus();
					return;
					
				}
	
				if (tKeyIn4.getText().equals(""))
				{

					tKeyIn4.requestFocus();
					return;
					
				}
	
				if (tKeyIn5.getText().equals(""))
				{
			
					tKeyIn5.requestFocus();
					return;
					
				}
	
				if (tKeyIn6.getText().equals(""))
				{
			
					tKeyIn6.requestFocus();
					return;
				}
				
	
			}
			
			if(rbNonVol.isSelected())
			{
				if(tKeyAdd.getText().equals(""))
				{
		
					tKeyAdd.requestFocus();
					return;
					
				}
				
				if (((Integer)Integer.parseInt(tKeyAdd.getText(), 16))>0x1F)
				{
					tKeyAdd.setText("1F");
					return;
				}
				
			}
			
			if(rbManual.isSelected())
			{
				
				//store value in volatile memory
				clearBuffers();
				SendBuff[0] = (byte)0xFF;
				SendBuff[1] = (byte)0x82;
				SendBuff[2] = (byte)0x00;
				SendBuff[3] = (byte)0x20;
				SendBuff[4] = (byte)0x06;
				SendBuff[5] = (byte)((Integer)Integer.parseInt(tKeyIn1.getText(), 16)).byteValue();
				SendBuff[6] = (byte)((Integer)Integer.parseInt(tKeyIn2.getText(), 16)).byteValue();
				SendBuff[7] = (byte)((Integer)Integer.parseInt(tKeyIn3.getText(), 16)).byteValue();
				SendBuff[8] = (byte)((Integer)Integer.parseInt(tKeyIn4.getText(), 16)).byteValue();
				SendBuff[9] = (byte)((Integer)Integer.parseInt(tKeyIn5.getText(), 16)).byteValue();
				SendBuff[10] = (byte)((Integer)Integer.parseInt(tKeyIn6.getText(), 16)).byteValue();
				SendLen = 0x0B;
				RecvLen[0] = 0x02;
				
				retCode = sendAPDUandDisplay(0);
				
				if(retCode!= ACSModule.SCARD_S_SUCCESS)
					return;
				
				//use volatile memory to authenticate
				clearBuffers();
				SendBuff[0] = (byte)0xFF;
				SendBuff[2] = (byte)0x00;
				SendBuff[1] = (byte)0x86;
				SendBuff[3] = (byte)0x00;
				SendBuff[4] = (byte)0x05;
				SendBuff[5] = (byte)0x01;
				SendBuff[6] = (byte)0x00;
				SendBuff[7] = (byte)((Integer)Integer.parseInt(tBlkNo.getText(), 16)).byteValue();
				
				if(rbKeyA.isSelected())
					SendBuff[8] = (byte)0x60;
				else
					SendBuff[8] = (byte)0x61;
				
				SendBuff[9] = (byte)0x20;
				
			}
			else
			{
				
				clearBuffers();
				SendBuff[0] = (byte)0xFF;
				SendBuff[2] = (byte)0x00;
				SendBuff[1] = (byte)0x86;
				SendBuff[3] = (byte)0x00;
				SendBuff[4] = (byte)0x05;
				SendBuff[5] = (byte)0x01;
				SendBuff[6] = (byte)0x00;
				SendBuff[7] = (byte)((Integer)Integer.parseInt(tBlkNo.getText(), 16)).byteValue();
				
				if(rbKeyA.isSelected())
					SendBuff[8] = (byte)0x60;
				else
					SendBuff[8] = (byte)0x61;
				
				if(rbVolatile.isSelected())
					SendBuff[9] = (byte)0x20;
				else
					SendBuff[9] = (byte)((Integer)Integer.parseInt(tKeyAdd.getText(), 16)).byteValue();
				
			}
			
			SendLen = 0x0A;
			RecvLen[0] = 0x02;
			
			retCode = sendAPDUandDisplay(0);
			
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
			
		}
		
		if(bBinRead==e.getSource())
		{
			
			String tmpStr="";
			//validate input
			tBinData.setText("");
			
			if(tBinBlk.getText().equals(""))
			{
				tBinBlk.selectAll();
				tBinBlk.requestFocus();
				return;
			}

			if(tBinLen.getText().equals(""))
			{
				tBinLen.selectAll();
				tBinLen.requestFocus();
				return;
			}

			if (((Integer)Integer.parseInt(tBinBlk.getText(), 16))>319)
			{
				tBinBlk.setText("319");
				return;
			}

			clearBuffers();
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0xB0;
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)Integer.parseInt(tBinBlk.getText());
			SendBuff[4] = (byte)Integer.parseInt(tBinLen.getText());
	
			SendLen = 0x05;
			RecvLen[0] = Integer.parseInt(tBinLen.getText()) + 2;
			
			retCode = sendAPDUandDisplay(2);
			
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
			
			//display data in text format
			for(int i =0; i<RecvLen[0]-1; i++)
				if(RecvBuff[i] <= 0)
					tmpStr = tmpStr + " ";
				else
					tmpStr = tmpStr + (char)RecvBuff[i];
			
			tBinData.setText(tmpStr);
			
		}
		
		if(bBinUpd==e.getSource())
		{
			
			String tmpStr="";
			
			//validate input
			if (tBinData.getText().equals(""))
			{
				tBinData.requestFocus();
				return;
			}
			
			if(tBinBlk.getText().equals(""))
			{
	
				tBinBlk.requestFocus();
				return;
				
			}
			
			if (((Integer)Integer.parseInt(tBinBlk.getText(), 16))>319)
				tBinBlk.setText("319");
			
			if(tBinLen.getText().equals(""))
			{

				tBinLen.requestFocus();
				return;
				
			}
	
			tmpStr = tBinData.getText();
			
			clearBuffers();
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0xD6;
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)Integer.parseInt(tBinBlk.getText());
			SendBuff[4] = (byte)Integer.parseInt(tBinLen.getText());
			
			for(int i=0; i<tmpStr.length(); i++)
				SendBuff[i+5] = (byte)tmpStr.charAt(i);
			
			SendLen = SendBuff[4] + 5;
			RecvLen[0] = 0x02;
			
			retCode = sendAPDUandDisplay(2);
			
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
			
		}
		
		if(bValStore==e.getSource())
		{
			
			int amount;
			
			//validate input
			if (tValAmt.getText().equals(""))
			{
			
				tValAmt.requestFocus();
				return;
				
			}
		
			if(Integer.parseInt(tValAmt.getText())>2147483647)
			{
				
				tValAmt.setText("2147483647");
				tValAmt.requestFocus();
				return;
				
			}
			
			if(tValBlk.getText().equals(""))
			{
		
				tValBlk.requestFocus();
				return;
				
			}
		
			if(Integer.parseInt(tValBlk.getText())>255)
			{
				tValBlk.setText("255");
				tValBlk.requestFocus();
				return;
				
			}
			
			tValSrc.setText("");
			tValTar.setText("");
			
			amount = Integer.parseInt(tValAmt.getText());
			
			clearBuffers();
			SendBuff[0]=(byte)0xFF;
			SendBuff[1]=(byte)0xD7;
			SendBuff[2]=(byte)0x00;
			SendBuff[3]=(byte)Integer.parseInt(tValBlk.getText());
			SendBuff[4]=(byte)0x05;
			SendBuff[5]=(byte)0x00; 
			SendBuff[6]=(byte)((amount>>24) & 0xFF);
			SendBuff[7]=(byte)((amount>>16) & 0xFF);
			SendBuff[8]=(byte)((amount>>8) & 0xFF);
			SendBuff[9]=(byte)(amount & 0xFF);
			
			SendLen = SendBuff[4] + 5;
			RecvLen[0] = 0x02;
			
			retCode = sendAPDUandDisplay(2);
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
		}
		
		if (bValInc == e.getSource())
		{
			
			int amount;
			
			//validate input
			if (tValAmt.getText().equals(""))
			{

				tValAmt.requestFocus();
				return;
				
			}
	
			if(Integer.parseInt(tValAmt.getText())>2147483647)
			{
				
				tValAmt.setText("2147483647");
				tValAmt.requestFocus();
				return;
				
			}
			
			if(tValBlk.getText().equals(""))
			{
	
				tValBlk.requestFocus();
				return;
				
			}
			
			if(Integer.parseInt(tValBlk.getText())>255)
			{
				tValBlk.setText("255");
				tValBlk.requestFocus();
				return;
				
			}
			
			tValSrc.setText("");
			tValTar.setText("");
			
			amount = Integer.parseInt(tValAmt.getText());
			
			clearBuffers();
			SendBuff[0]=(byte)0xFF;
			SendBuff[1]=(byte)0xD7;
			SendBuff[2]=(byte)0x00;
			SendBuff[3]=(byte)Integer.parseInt(tValBlk.getText());
			SendBuff[4]=(byte)0x05;
			SendBuff[5]=(byte)0x01; 
			SendBuff[6]=(byte)((amount>>24) & 0xFF);
			SendBuff[7]=(byte)((amount>>16) & 0xFF);
			SendBuff[8]=(byte)((amount>>8) & 0xFF);
			SendBuff[9]=(byte)(amount & 0xFF);
			
			SendLen = 0x0A;
			RecvLen[0] = 0x02;
			
			retCode = sendAPDUandDisplay(2);
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
		}
		
		if(bValDec==e.getSource())
		{
			
			int amount;
			//validate input
			if (tValAmt.getText().equals(""))
			{
			
				tValAmt.requestFocus();
				return;
				
			}

			if(Integer.parseInt(tValAmt.getText())>2147483647)
			{
				
				tValAmt.setText("2147483647");
				tValAmt.requestFocus();
				return;
				
			}
			
			if(tValBlk.getText().equals(""))
			{
			
				tValBlk.requestFocus();
				return;
				
			}
	
			if(Integer.parseInt(tValBlk.getText())>255)
			{
				tValBlk.setText("255");
				tValBlk.requestFocus();
				return;
				
			}
			
			tValSrc.setText("");
			tValTar.setText("");
			
			amount = Integer.parseInt(tValAmt.getText());
			
			clearBuffers();
			SendBuff[0]=(byte)0xFF;
			SendBuff[1]=(byte)0xD7;
			SendBuff[2]=(byte)0x00;
			SendBuff[3]=(byte)Integer.parseInt(tValBlk.getText());
			SendBuff[4]=(byte)0x05;
			SendBuff[5]=(byte)0x02; 
			SendBuff[6]=(byte)((amount>>24) & 0xFF);
			SendBuff[7]=(byte)((amount>>16) & 0xFF);
			SendBuff[8]=(byte)((amount>>8) & 0xFF);
			SendBuff[9]=(byte)(amount & 0xFF);
			
			SendLen = SendBuff[4] + 5;
			RecvLen[0] = 0x02;
			
			retCode = sendAPDUandDisplay(2);
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
		}
		
		if(bValRead==e.getSource())
		{
			
			int amount=0;
			//validate input
			if(tValBlk.getText().equals(""))
			{
				
				tValBlk.requestFocus();
				return;
				
			}
			
			if(Integer.parseInt(tValBlk.getText())>255)
			{
				
				tValBlk.setText("255");
				tValBlk.requestFocus();
				return;
				
			}
			
			tValAmt.setText("");
			tValSrc.setText("");
			tValTar.setText("");
			
			clearBuffers();
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0xB1;
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)Integer.parseInt(tValBlk.getText());
			SendBuff[4] = (byte)0x00;
			
			SendLen = 0x05;
			RecvLen[0] = 0x06;
			
			retCode = sendAPDUandDisplay(2);
			if (retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			amount = (RecvBuff[3] & 0xFF);
			amount = amount + (RecvBuff[2] * 256);
			amount = amount + (RecvBuff[1] * 256 * 256);
			amount = amount + (RecvBuff[0] * 256 * 256 *256);
//			JOptionPane.showMessageDialog(this,RecvBuff[2] * 256);
			tValAmt.setText(""+amount);
			
		}
		
		if(bValRes == e.getSource())
		{
			
			//validate input
			if(tValSrc.getText().equals(""))
			{
				
				tValSrc.requestFocus();
				return;
				
			}
			
			if(tValTar.getText().equals(""))
			{
				
				tValTar.requestFocus();
				return;
				
			}
	
			if(Integer.parseInt(tValSrc.getText())>255)
			{
				
				tValSrc.setText("255");
				tValSrc.requestFocus();
				return;
				
			}
			
			if(Integer.parseInt(tValTar.getText())>255)
			{
				
				tValTar.setText("255");
				tValTar.requestFocus();
				return;
				
			}
			
			tValAmt.setText("");
			tValBlk.setText("");
			
			clearBuffers();
			SendBuff[0] = (byte) 0xFF;
			SendBuff[1] = (byte) 0xD7;
			SendBuff[2] = (byte) 0x00;
			SendBuff[3] = (byte) Integer.parseInt(tValSrc.getText());
			SendBuff[4] = (byte) 0x02;
			SendBuff[5] = (byte) 0x03;
			SendBuff[6] = (byte) Integer.parseInt(tValTar.getText());
			
			SendLen = 0x07;
			RecvLen[0] = 0x02;
			
			retCode = sendAPDUandDisplay(2);
			
			if(retCode!=ACSModule.SCARD_S_SUCCESS)
				return;
			
		}
		
		if(rbManual == e.getSource())
		{
			
			tKeyAdd.setText("");
			tKeyAdd.setEnabled(false);
			tBlkNo.setEnabled(true);
			tKeyIn1.setEnabled(true);
			tKeyIn2.setEnabled(true);
			tKeyIn3.setEnabled(true);
			tKeyIn4.setEnabled(true);
			tKeyIn5.setEnabled(true);
			tKeyIn6.setEnabled(true);
			
		}
		
		if(rbVolatile==e.getSource())
		{
			
			tKeyAdd.setText("");
			tKeyAdd.setEnabled(false);
			tBlkNo.setEnabled(true);
			tKeyIn1.setEnabled(false);
			tKeyIn2.setEnabled(false);
			tKeyIn3.setEnabled(false);
			tKeyIn4.setEnabled(false);
			tKeyIn5.setEnabled(false);
			tKeyIn6.setEnabled(false);
			
		}
		
		if(rbNonVol == e.getSource())
		{
			
			tKeyAdd.setText("");
			tKeyAdd.setEnabled(true);
			tBlkNo.setEnabled(true);
			tKeyIn1.setEnabled(false);
			tKeyIn2.setEnabled(false);
			tKeyIn3.setEnabled(false);
			tKeyIn4.setEnabled(false);
			tKeyIn5.setEnabled(false);
			tKeyIn6.setEnabled(false);
			
		}
		
		if(rbVol == e.getSource())
		{
			
			tMemAdd.setText("");
			tMemAdd.setEnabled(false);
			
		}
		
		if(rbNonVolMem == e.getSource())
		{
			
			tMemAdd.setText("");
			tMemAdd.setEnabled(true);
			
		}
				
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
		bConn.setEnabled(false);
		bInit.setEnabled(true);
		bReset.setEnabled(false);
		mMsg.setText("");
		rbNonVolMem.setEnabled(false);
		rbVol.setEnabled(false);
		tMemAdd.setText("");
		tKey1.setText("");
		tKey2.setText("");
		tKey3.setText("");
		tKey4.setText("");
		tKey5.setText("");
		tKey6.setText("");
		tBlkNo.setText("");
		tKeyAdd.setText("");
		tKeyIn1.setText("");
		tKeyIn2.setText("");
		tKeyIn3.setText("");
		tKeyIn4.setText("");
		tKeyIn5.setText("");
		tKeyIn6.setText("");
		rbManual.setEnabled(false);
		rbNonVol.setEnabled(false);
		rbVolatile.setEnabled(false);
		rbKeyA.setEnabled(false);
		rbKeyB.setEnabled(false);
		tBinBlk.setText("");
		tBinLen.setText("");
		tBinData.setText("");
		tValAmt.setText("");
		tValBlk.setText("");
		tValSrc.setText("");
		tValTar.setText("");
		tKey1.setEnabled(false);
		tKey2.setEnabled(false);
		tKey3.setEnabled(false);
		tKey4.setEnabled(false);
		tKey5.setEnabled(false);
		tKey6.setEnabled(false);
		tKeyIn1.setEnabled(false);
		tKeyIn2.setEnabled(false);
		tKeyIn3.setEnabled(false);
		tKeyIn4.setEnabled(false);
		tKeyIn5.setEnabled(false);
		tKeyIn6.setEnabled(false);
		tBlkNo.setEnabled(false);
		tKeyAdd.setEnabled(false);
		tMemAdd.setEnabled(false);
		tBinBlk.setEnabled(false);
		tBinLen.setEnabled(false);
		tBinData.setEnabled(false);
		tValAmt.setEnabled(false);
		tValBlk.setEnabled(false);
		tValSrc.setEnabled(false);
		tValTar.setEnabled(false);
		bLoadKey.setEnabled(false);
		bauth.setEnabled(false);
		bBinRead.setEnabled(false);
		bBinUpd.setEnabled(false);
		bValStore.setEnabled(false);
		bValInc.setEnabled(false);
		bValDec.setEnabled(false);
		bValRead.setEnabled(false);
		bValRes.setEnabled(false);
		rbNonVolMem.setSelected(false);
		rbVol.setSelected(false);
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
  		if(tBlkNo.isFocusOwner() || tBinBlk.isFocusOwner() || tBinLen.isFocusOwner() || tValAmt.isFocusOwner() || tValSrc.isFocusOwner() || tValBlk.isFocusOwner() || tValTar.isFocusOwner())
  		{	
  		
  			if (VALIDCHARS.indexOf(x) == -1 ) 
  				ke.setKeyChar(empty);
  			
  		}
  		else
  		{
  			
  			if (VALIDCHARSHEX.indexOf(x) == -1 ) 
  				ke.setKeyChar(empty);
  			
  		}
  					  
		//Limit character length
  		if(tBlkNo.isFocusOwner() || tBinBlk.isFocusOwner() || tValBlk.isFocusOwner() || tValSrc.isFocusOwner() || tValTar.isFocusOwner())
  		{
  			
  			if   (((JTextField)ke.getSource()).getText().length() >= 3 ) 
  			{
		
  				ke.setKeyChar(empty);	
  				return;
  				
  			}
  			
  		}
  		else if(tValAmt.isFocusOwner())
  		{
  			
  			if   (((JTextField)ke.getSource()).getText().length() >= 10 ) 
  			{
		
  				ke.setKeyChar(empty);	
  				return;
  				
  			}
  			
  		}
  		else
  		{
  			
  			if   (((JTextField)ke.getSource()).getText().length() >= 2 ) 
  			{
		
  				ke.setKeyChar(empty);	
  				return;
  				
  			}
  			
  		}
  			
  		    	
	}
    
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new mifareProg().setVisible(true);
            }
        });
    }



}
