/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              mainApplet.java

  Description:       This program enables the user to browse the sample codes for ACR128

  Author:            M.J.E.C. Castillo

  Date:              September 26, 2008

  Revision Trail:   (Date/Author/Description)

======================================================================*/

import java.awt.*;
import java.lang.*;
import java.awt.event.*;
import javax.swing.text.*;
import javax.swing.*;
import java.applet.Applet;
import javax.swing.JApplet;

public class mainApplet extends JApplet implements ActionListener
{
	
	//Variables
	boolean openAcct=false, openReadWrite=false, openMutAuth=false, openEncrypt=false, openOtherPICC=false;
	boolean openCreateFiles=false, openConfATR=false, openPolling=false, openReadWriteBin=false;
	boolean opengetATR = false, openDevProg=false, openAdvDevProg=false, openMifare = false;
	
	//GUI Variables
    private javax.swing.JPanel ACOS3Panel;
    private javax.swing.JButton bAcct;
    private javax.swing.JButton bAdvDevProg;
    private javax.swing.JButton bConfigATR;
    private javax.swing.JButton bCreateFiles;
    private javax.swing.JButton bDevProg;
    private javax.swing.JButton bEncrypt;
    private javax.swing.JButton bGetATR;
    private javax.swing.JButton bMifare;
    private javax.swing.JButton bMutAuth;
    private javax.swing.JButton bOtherPICC;
    private javax.swing.JButton bPolling;
    private javax.swing.JButton bReadWrite;
    private javax.swing.JButton bReadWriteBin;
    
	static ACOS3Account acct;
	static ACOS3ReadWrite readWrite;
	static ACOS3MutualAuthentication mutAuth;
	static ACOS3Encrypt encrypt;
	static ACOS3CreateFiles createFiles;
	static ACOS3ConfigureATR confATR;
	static polling poll;
	static GetATR atr;
	static deviceProgramming devProg;
	static AdvDevProg advDevProg;
	static ACOS3ReadWriteBinary readWriteBin;
	static otherPICC otherPICC;
	static mifareProg mifare;

	
	public void init() 
   	{
		setSize(260,470);
        bGetATR = new javax.swing.JButton();
        bDevProg = new javax.swing.JButton();
        bAdvDevProg = new javax.swing.JButton();
        bOtherPICC = new javax.swing.JButton();
        bPolling = new javax.swing.JButton();
        bMifare = new javax.swing.JButton();
        ACOS3Panel = new javax.swing.JPanel();
        bConfigATR = new javax.swing.JButton();
        bReadWrite = new javax.swing.JButton();
        bMutAuth = new javax.swing.JButton();
        bEncrypt = new javax.swing.JButton();
        bCreateFiles = new javax.swing.JButton();
        bAcct = new javax.swing.JButton();
        bReadWriteBin = new javax.swing.JButton();

        bGetATR.setText("Get ATR");

        bDevProg.setText("Device Programming");

        bAdvDevProg.setText("Advanced Device Programming");

        bOtherPICC.setText("Other PICC Cards");

        bPolling.setText("Polling");

        bMifare.setText("Mifare 1K 4K");

        ACOS3Panel.setBorder(javax.swing.BorderFactory.createTitledBorder("ACOS 3"));

        bConfigATR.setText("Configure ATR");

        bReadWrite.setText("Read and Write");

        bMutAuth.setText("Mutual Authentication");

        bEncrypt.setText("Encrypt");

        bCreateFiles.setText("Create Files");

        bAcct.setText("Account");

        bReadWriteBin.setText("Read and Write Binary");

        javax.swing.GroupLayout ACOS3PanelLayout = new javax.swing.GroupLayout(ACOS3Panel);
        ACOS3Panel.setLayout(ACOS3PanelLayout);
        ACOS3PanelLayout.setHorizontalGroup(
            ACOS3PanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(ACOS3PanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(ACOS3PanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(bAcct, javax.swing.GroupLayout.DEFAULT_SIZE, 183, Short.MAX_VALUE)
                    .addComponent(bCreateFiles, javax.swing.GroupLayout.DEFAULT_SIZE, 183, Short.MAX_VALUE)
                    .addComponent(bEncrypt, javax.swing.GroupLayout.DEFAULT_SIZE, 183, Short.MAX_VALUE)
                    .addComponent(bMutAuth, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.DEFAULT_SIZE, 183, Short.MAX_VALUE)
                    .addComponent(bReadWrite, javax.swing.GroupLayout.DEFAULT_SIZE, 183, Short.MAX_VALUE)
                    .addComponent(bConfigATR, javax.swing.GroupLayout.DEFAULT_SIZE, 183, Short.MAX_VALUE)
                    .addComponent(bReadWriteBin, javax.swing.GroupLayout.DEFAULT_SIZE, 183, Short.MAX_VALUE))
                .addContainerGap())
        );
        ACOS3PanelLayout.setVerticalGroup(
            ACOS3PanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(ACOS3PanelLayout.createSequentialGroup()
                .addComponent(bAcct)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bCreateFiles)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bEncrypt)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bMutAuth)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bReadWrite)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bConfigATR)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bReadWriteBin)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                .addGap(28, 28, 28)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                    .addComponent(bPolling, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, 183, Short.MAX_VALUE)
                    .addComponent(bOtherPICC, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, 183, Short.MAX_VALUE)
                    .addComponent(bMifare, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, 183, Short.MAX_VALUE)
                    .addComponent(bGetATR, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, 183, Short.MAX_VALUE)
                    .addComponent(bAdvDevProg, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bDevProg, javax.swing.GroupLayout.DEFAULT_SIZE, 183, Short.MAX_VALUE))
                .addGap(24, 24, 24))
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(ACOS3Panel, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(ACOS3Panel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bDevProg)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bAdvDevProg)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bGetATR)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bMifare)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bOtherPICC)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bPolling)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        
        bAcct.addActionListener(this);
        bAdvDevProg.addActionListener(this);
        bConfigATR.addActionListener(this);
        bCreateFiles.addActionListener(this);
        bDevProg.addActionListener(this);
        bEncrypt.addActionListener(this);
        bGetATR.addActionListener(this);
        bMifare.addActionListener(this);
        bMutAuth.addActionListener(this);
        bOtherPICC.addActionListener(this);
        bPolling.addActionListener(this);
        bReadWrite.addActionListener(this);
        bReadWriteBin.addActionListener(this);
   		
   	}
	
	public void actionPerformed(ActionEvent e) 
	{
		
		if(bAcct == e.getSource())
		{
			closeFrames();
			
			if(openAcct == false)
			{
				acct = new ACOS3Account();
				acct.setVisible(true);	
				openAcct = true;
			}
			else
			{
				acct.dispose();
				acct = new ACOS3Account();
				acct.setVisible(true);	
				openAcct = true;
			}
			
		}
		
		if(bReadWrite == e.getSource())
		{
			closeFrames();
			
			if(openReadWrite == false)
			{
				readWrite = new ACOS3ReadWrite();
				readWrite.setVisible(true);	
				openReadWrite = true;
			}
			else
			{
				readWrite.dispose();
				readWrite = new ACOS3ReadWrite();
				readWrite.setVisible(true);	
				openReadWrite = true;
			}
			
		}
		
		if(bMutAuth == e.getSource())
		{
			closeFrames();
			
			if(openMutAuth == false)
			{
				mutAuth = new ACOS3MutualAuthentication();
				mutAuth.setVisible(true);	
				openMutAuth = true;
			}
			else
			{
				mutAuth.dispose();
				mutAuth = new ACOS3MutualAuthentication();
				mutAuth.setVisible(true);	
				openMutAuth = true;
			}
			
		}
		
		if(bEncrypt == e.getSource())
		{
			closeFrames();
			
			if(openEncrypt == false)
			{
				encrypt = new ACOS3Encrypt();
				encrypt.setVisible(true);	
				openEncrypt = true;
			}
			else
			{
				encrypt.dispose();
				encrypt= new ACOS3Encrypt();
				encrypt.setVisible(true);	
				openEncrypt = true;
			}
			
		}
		
		if(bCreateFiles == e.getSource())
		{
			closeFrames();
			
			if(openCreateFiles == false)
			{
				createFiles = new ACOS3CreateFiles();
				createFiles.setVisible(true);	
				openCreateFiles = true;
			}
			else
			{
				createFiles.dispose();
				createFiles= new ACOS3CreateFiles();
				createFiles.setVisible(true);	
				openCreateFiles = true;
			}
			
		}
		
		if(bConfigATR == e.getSource())
		{
			closeFrames();
			
			if(openConfATR == false)
			{
				confATR = new ACOS3ConfigureATR();
				confATR.setVisible(true);	
				openConfATR = true;
			}
			else
			{
				confATR.dispose();
				confATR= new ACOS3ConfigureATR();
				confATR.setVisible(true);	
				openConfATR = true;
			}
			
		}
		
		
		
		if(bPolling == e.getSource())
		{
			closeFrames();
			
			if(openPolling == false)
			{
				poll = new polling();
				poll.setVisible(true);	
				openPolling = true;
			}
			else
			{
				poll.dispose();
				poll = new polling();
				poll.setVisible(true);	
				openPolling = true;
			}
			
		}
		
		if(bGetATR == e.getSource())
		{
			closeFrames();
			
			if(opengetATR == false)
			{
				atr = new GetATR();
				atr.setVisible(true);	
				opengetATR = true;
			}
			else
			{
				atr.dispose();
				atr = new GetATR();
				atr.setVisible(true);	
				opengetATR = true;
			}
			
		}
		
		if(bDevProg == e.getSource())
		{
			closeFrames();
			
			if(openDevProg == false)
			{
				devProg = new deviceProgramming();
				devProg.setVisible(true);	
				openDevProg = true;
			}
			else
			{
				devProg.dispose();
				devProg = new deviceProgramming();
				devProg.setVisible(true);	
				openDevProg = true;
			}
			
		}
		
		if(bAdvDevProg == e.getSource())
		{
			closeFrames();
			
			if(openAdvDevProg == false)
			{
				advDevProg = new AdvDevProg();
				advDevProg.setVisible(true);	
				openAdvDevProg = true;
			}
			else
			{
				advDevProg.dispose();
				advDevProg = new AdvDevProg();
				advDevProg.setVisible(true);	
				openAdvDevProg = true;
			}
			
		}
		
		if(bReadWriteBin == e.getSource())
		{
			closeFrames();
			
			if(openReadWriteBin == false)
			{
				readWriteBin= new ACOS3ReadWriteBinary();
				readWriteBin.setVisible(true);	
				openReadWriteBin = true;
			}
			else
			{
				readWriteBin.dispose();
				readWriteBin = new ACOS3ReadWriteBinary();
				readWriteBin.setVisible(true);	
				openReadWriteBin = true;
			}
			
		}
		
		if(bMifare == e.getSource())
		{
			closeFrames();
			
			if(openMifare == false)
			{
				mifare= new mifareProg();
				mifare.setVisible(true);	
				openMifare = true;
			}
			else
			{
				mifare.dispose();
				mifare = new mifareProg();
				mifare.setVisible(true);	
				openMifare = true;
			}
			
		}
		
		if(bOtherPICC == e.getSource())
		{
			closeFrames();
			
			if(openOtherPICC == false)
			{
				otherPICC= new otherPICC();
				otherPICC.setVisible(true);	
				openOtherPICC = true;
			}
			else
			{
				otherPICC.dispose();
				otherPICC = new otherPICC();
				otherPICC.setVisible(true);	
				openOtherPICC = true;
			}
			
		}
		
	}
	
	public void closeFrames()
	{
		
		if(openAcct==true)
		{
			
			acct.dispose();
			openAcct = false;
			
		}
		
		if(openReadWrite==true)
		{
			
			readWrite.dispose();
			openReadWrite = false;
			
		}
		
		if(openMutAuth==true)
		{
			
			mutAuth.dispose();
			openMutAuth = false;
			
		}
		
		if(openEncrypt==true)
		{
			
			encrypt.dispose();
			openEncrypt = false;
			
		}
		
		if(openCreateFiles==true)
		{
			
			createFiles.dispose();
			openCreateFiles = false;
			
		}
		
		if(openConfATR==true)
		{
			
			confATR.dispose();
			openConfATR = false;
			
		}
		
				
		if(openPolling==true)
		{
			
			poll.dispose();
			openPolling = false;
			
		}
		
		if(opengetATR==true)
		{
			
			atr.dispose();
			opengetATR = false;
			
		}
		
		if(openDevProg==true)
		{
			
			devProg.dispose();
			openDevProg = false;
			
		}
		
		if(openAdvDevProg==true)
		{
			
			advDevProg.dispose();
			openAdvDevProg = false;
			
		}
		
		if(openReadWriteBin==true)
		{
			
			readWriteBin.dispose();
			openReadWriteBin = false;
			
		}
		
		if(openMifare==true)
		{
			
			mifare.dispose();
			openMifare = false;
			
		}
		
		if(openOtherPICC==true)
		{
			
			otherPICC.dispose();
			openOtherPICC = false;
			
		}
		
	}
	
	public void start()
	{
	
	}
	
}