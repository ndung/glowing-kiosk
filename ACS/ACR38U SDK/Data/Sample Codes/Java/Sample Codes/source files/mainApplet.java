/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              mainApplet.java

  Description:       This program enables the user to browse the sample codes for ACR100F

  Author:            M.J.E.C. Castillo

  Date:              August 29, 2008

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
	boolean openAcct=false, openReadWrite=false, openMutAuth=false, openEncrypt=false;
	boolean openCreateFiles=false, openConfATR=false, openGetATR=false, openSimplePCSC=false;
	boolean openReadWriteBin=false, openPolling=false;
	//GUI Variables
    private JPanel ACOS3Panel;
    private JButton bAcct;
    private JButton bGetATR;
    private JButton bConfigATR;
    private JButton bCreateFiles;
    private JButton bSimplePCSC;
    private JButton bEncrypt;
    private JButton bMutAuth;
    private JButton bReadWrite;
    private JButton bPolling;
    private JButton bReadWriteBin;
    
	static ACOS3Account acct;
	static ACOS3ReadWrite readWrite;
	static ACOS3MutualAuthentication mutAuth;
	static ACOS3Encrypt encrypt;
	static ACOS3CreateFiles createFiles;
	static ACOS3ConfigureATR confATR;
	static GetATR getATR;
	static SimplePCSC simplePCSC;
	static ACOS3ReadWriteBinary readWriteBin;
	static Polling polling;

	
	public void init() 
   	{
		setSize(215,390);
        ACOS3Panel = new JPanel();
        bConfigATR = new JButton();
        bReadWrite = new JButton();
        bMutAuth = new JButton();
        bEncrypt = new JButton();
        bCreateFiles = new JButton();
        bAcct = new JButton();
        bSimplePCSC = new JButton();
        bGetATR = new JButton();
        bPolling = new JButton();
        bReadWriteBin = new JButton();


        ACOS3Panel.setBorder(BorderFactory.createTitledBorder("ACOS 3"));

        bConfigATR.setText("Configure ATR");

        bReadWrite.setText("Read and Write");

        bMutAuth.setText("Mutual Authentication");

        bEncrypt.setText("Encrypt");

        bCreateFiles.setText("Create Files");

        bAcct.setText("Account");

        bReadWriteBin.setText("Read/Write Binary");

        GroupLayout ACOS3PanelLayout = new GroupLayout(ACOS3Panel);
        ACOS3Panel.setLayout(ACOS3PanelLayout);
        ACOS3PanelLayout.setHorizontalGroup(
            ACOS3PanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(ACOS3PanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(ACOS3PanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(bAcct, GroupLayout.DEFAULT_SIZE, 139, Short.MAX_VALUE)
                    .addComponent(bCreateFiles, GroupLayout.DEFAULT_SIZE, 139, Short.MAX_VALUE)
                    .addComponent(bEncrypt, GroupLayout.DEFAULT_SIZE, 139, Short.MAX_VALUE)
                    .addComponent(bMutAuth, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bReadWrite, GroupLayout.DEFAULT_SIZE, 139, Short.MAX_VALUE)
                    .addComponent(bConfigATR, GroupLayout.DEFAULT_SIZE, 139, Short.MAX_VALUE)
                    .addComponent(bReadWriteBin, GroupLayout.DEFAULT_SIZE, 139, Short.MAX_VALUE))
                .addContainerGap())
        );
        ACOS3PanelLayout.setVerticalGroup(
            ACOS3PanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(ACOS3PanelLayout.createSequentialGroup()
                .addComponent(bAcct)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bCreateFiles)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bEncrypt)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bMutAuth)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bReadWrite)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bConfigATR)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bReadWriteBin)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        bSimplePCSC.setText("Simple PCSC");

        bGetATR.setText("Get ATR");

        bPolling.setText("Polling");

        GroupLayout layout = new GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(ACOS3Panel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
            .addGroup(GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                .addGap(29, 29, 29)
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                    .addComponent(bPolling, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 139, Short.MAX_VALUE)
                    .addComponent(bGetATR, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 139, Short.MAX_VALUE)
                    .addComponent(bSimplePCSC, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 139, Short.MAX_VALUE))
                .addGap(23, 23, 23))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(ACOS3Panel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bSimplePCSC)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bGetATR)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bPolling)
                .addContainerGap(55, Short.MAX_VALUE))
        );
        
        bAcct.addActionListener(this);
        bReadWrite.addActionListener(this);
        bMutAuth.addActionListener(this);
        bEncrypt.addActionListener(this);
        bCreateFiles.addActionListener(this);
        bConfigATR.addActionListener(this);
        bGetATR.addActionListener(this);
        bSimplePCSC.addActionListener(this);
        bReadWriteBin.addActionListener(this);
        bPolling.addActionListener(this);
   		
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
		
		if(bGetATR == e.getSource())
		{
			closeFrames();
			
			if(openGetATR == false)
			{
				getATR= new GetATR();
				getATR.setVisible(true);	
				openGetATR = true;
			}
			else
			{
				getATR.dispose();
				getATR= new GetATR();
				getATR.setVisible(true);	
				openGetATR = true;
			}
			
		}
		
		if(bSimplePCSC == e.getSource())
		{
			closeFrames();
			
			if(openSimplePCSC == false)
			{
				simplePCSC= new SimplePCSC();
				simplePCSC.setVisible(true);	
				openSimplePCSC = true;
			}
			else
			{
				simplePCSC.dispose();
				simplePCSC= new SimplePCSC();
				simplePCSC.setVisible(true);	
				openSimplePCSC = true;
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
				readWriteBin= new ACOS3ReadWriteBinary();
				readWriteBin.setVisible(true);	
				openReadWriteBin = true;
			}
			
		}
		
		if(bPolling == e.getSource())
		{
			closeFrames();
			
			if(openPolling == false)
			{
				polling= new Polling();
				polling.setVisible(true);	
				openPolling = true;
			}
			else
			{
				polling.dispose();
				polling= new Polling();
				polling.setVisible(true);	
				openPolling = true;
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
		
		if(openGetATR==true)
		{
			
			getATR.dispose();
			openGetATR = false;
			
		}
		
		if(openSimplePCSC==true)
		{
			
			simplePCSC.dispose();
			openSimplePCSC = false;
			
		}
		
		if(openReadWriteBin==true)
		{
			
			readWriteBin.dispose();
			openReadWriteBin = false;
			
		}
		
		if(openPolling==true)
		{
			
			polling.dispose();
			openPolling = false;
			
		}
		
	}
	
	public void start()
	{
	
	}
	
}