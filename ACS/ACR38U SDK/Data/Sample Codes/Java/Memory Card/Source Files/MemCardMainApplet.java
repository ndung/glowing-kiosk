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

public class MemCardMainApplet extends JApplet implements ActionListener
{
	
	//Variables
	boolean openIIC=false, openSLE4418=false, openSLE4432=false;
	//GUI Variables
    private JButton bICC;
    private JButton bSLE4432;
    private JButton bSLE4418;
    private JPanel ACOS3Panel;
    
	static IICSample iic;
	static SLE4418 sle4418;
	static SLE4432 sle4432;


	
	public void init() 
   	{
		setSize(200,288);
	
        bSLE4418 = new JButton();
        bICC = new JButton();
        bSLE4432 = new JButton();
        ACOS3Panel = new JPanel();

        bICC.setText("ICC Sample");
        bSLE4432.setText("SLE 4432/4442/5542");
        bSLE4418.setText("SLE 4418/4428");

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
                    .addComponent(bSLE4418, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 139, Short.MAX_VALUE)
                    .addComponent(bSLE4432, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 139, Short.MAX_VALUE)
                    .addComponent(bICC, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 139, Short.MAX_VALUE))
                .addGap(23, 23, 23))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(ACOS3Panel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addGap(60, 60, 60)
                .addComponent(bICC)
                .addGap(18, 18, 18)
                .addComponent(bSLE4432)
                .addGap(18, 18, 18)
                .addComponent(bSLE4418)
                .addContainerGap(55, Short.MAX_VALUE))
        );
        
        bSLE4418.addActionListener(this);
        bICC.addActionListener(this);
        bSLE4432.addActionListener(this);
   		
   	}
	
	public void actionPerformed(ActionEvent e) 
	{
		
		if(bICC == e.getSource())
		{
			closeFrames();
			
			if(openIIC == false)
			{
				iic = new IICSample();
				iic.setVisible(true);	
				openIIC = true;
			}
			else
			{
				iic.dispose();
				iic = new IICSample();
				iic.setVisible(true);	
				openIIC = true;
			}
			
		}
		
		if(bSLE4418 == e.getSource())
		{
			closeFrames();
			
			if(openSLE4418 == false)
			{
				sle4418 = new SLE4418();
				sle4418.setVisible(true);	
				openSLE4418 = true;
			}
			else
			{
				sle4418.dispose();
				sle4418 = new SLE4418();
				sle4418.setVisible(true);	
				openSLE4418 = true;
			}
			
		}
		
		if(bSLE4432 == e.getSource())
		{
			closeFrames();
			
			if(openSLE4432 == false)
			{
				sle4432 = new SLE4432();
				sle4432.setVisible(true);	
				openSLE4432 = true;
			}
			else
			{
				sle4432.dispose();
				sle4432 = new SLE4432();
				sle4432.setVisible(true);	
				openSLE4432 = true;
			}
			
		}
		
	}
	
	public void closeFrames()
	{
		
		if(openIIC==true)
		{
			
			iic.dispose();
			openIIC = false;
			
		}
		
		if(openSLE4418==true)
		{
			
			sle4418.dispose();
			openSLE4418 = false;
			
		}
		
		if(openSLE4432==true)
		{
			
			sle4432.dispose();
			openSLE4432 = false;
			
		}
		
	}
	
	public void start()
	{
	
	}
	
}