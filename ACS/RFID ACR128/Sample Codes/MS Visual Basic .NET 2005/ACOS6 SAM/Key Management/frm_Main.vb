'=======================================================================================
'
'   Project Name :  KeyManagement
' 
'   Company      :  Advanced Card System LTD.
'
'   Author       :  Aileen Grace L. Sarte
'
'   Date         :  January 23, 2007
'
'   Description  :  This sample shows how to initialize an ACOS SAM and use it to generate keys
'                   for ACOS card, based from the card serial number.
'
'   Initial Step :  1.  Press List Readers (SAM Initialization).
'                   2.  Choose the SAM reader where you insert your SAM card.
'                   3.  Press Connect.
'                   4.  Enter 8 bytes global PIN (hex format).
'                   5.  If you haven't initialize the SAM card yet, select algorithm to use (DES/3DES).
'                          otherwise proceed to step 8 to initialize keys of new ACOS card.
'                   6.  Enter 16 bytes Master Key to be use for key generation of
'                          each type of ACOS Keys (e.g IC, PIN, Card Key....).
'                   7.  Press Initialize SAM button.
'                   8.  Press ACOS Card Initialization Tab.
'                   9.  Press List Readers (ACOS Card Initialization).
'                   10. Choose the slot reader where you insert your ACOS card.
'                   11. Press Connect.
'                   12. Select Algorithm Reference to use (DES/3DES)
'                   13. Press Generate Keys.
'                   14. Press Save Keys.
'
'   NOTE:
'                   Please note that once the ACOS card have been initialize or the IC was changed
'                   from it's default key (0x41 0x43 0x4F 0x53 0x54 0x45 0x53 0x54) it is not possible to
'                   save keys unless you change it's IC key back to default value.
'
'   Revision     :
'
'               Name                    Date            Brief Description of Revision
'
'=====================================================================================================

Imports System
Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices

Public Class frm_Main
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents lst_Log As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cmbSAM As System.Windows.Forms.ComboBox
    Friend WithEvents cmd_ListReaders_SAM As System.Windows.Forms.Button
    Friend WithEvents txtSAMGPIN As System.Windows.Forms.TextBox
    Friend WithEvents cmd_Connect_SAM As System.Windows.Forms.Button
    Friend WithEvents txt_IC As System.Windows.Forms.TextBox
    Friend WithEvents txtKrd As System.Windows.Forms.TextBox
    Friend WithEvents txtKcf As System.Windows.Forms.TextBox
    Friend WithEvents txtKcr As System.Windows.Forms.TextBox
    Friend WithEvents txtKd As System.Windows.Forms.TextBox
    Friend WithEvents txtKt As System.Windows.Forms.TextBox
    Friend WithEvents txtKc As System.Windows.Forms.TextBox
    Friend WithEvents cmd_InitSAM As System.Windows.Forms.Button
    Friend WithEvents cmbSLT As System.Windows.Forms.ComboBox
    Friend WithEvents cmd_Connect_SLT As System.Windows.Forms.Button
    Friend WithEvents cmd_ListReaders_SLT As System.Windows.Forms.Button
    Friend WithEvents lbl_CardSN As System.Windows.Forms.Label
    Friend WithEvents lbl_IC As System.Windows.Forms.Label
    Friend WithEvents lbl_Kc As System.Windows.Forms.Label
    Friend WithEvents lbl_Kd As System.Windows.Forms.Label
    Friend WithEvents lbl_Kcr As System.Windows.Forms.Label
    Friend WithEvents lbl_Kcf As System.Windows.Forms.Label
    Friend WithEvents lbl_Krd As System.Windows.Forms.Label
    Friend WithEvents cmd_GenerateKey As System.Windows.Forms.Button
    Friend WithEvents cmd_SaveKeys As System.Windows.Forms.Button
    Friend WithEvents rb_DES As System.Windows.Forms.RadioButton
    Friend WithEvents rb_3DES As System.Windows.Forms.RadioButton
    Friend WithEvents lbl_r_kc As System.Windows.Forms.Label
    Friend WithEvents lbl_Kt As System.Windows.Forms.Label
    Friend WithEvents lbl_r_Kt As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txt_PIN As System.Windows.Forms.TextBox
    Friend WithEvents lbl_r_Kd As System.Windows.Forms.Label
    Friend WithEvents lbl_r_Kcr As System.Windows.Forms.Label
    Friend WithEvents lbl_r_Kcf As System.Windows.Forms.Label
    Friend WithEvents lbl_r_Krd As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Main))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtKrd = New System.Windows.Forms.TextBox
        Me.txtKcf = New System.Windows.Forms.TextBox
        Me.txtKcr = New System.Windows.Forms.TextBox
        Me.txtKd = New System.Windows.Forms.TextBox
        Me.txtKt = New System.Windows.Forms.TextBox
        Me.txtKc = New System.Windows.Forms.TextBox
        Me.txt_IC = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtSAMGPIN = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmd_InitSAM = New System.Windows.Forms.Button
        Me.cmd_Connect_SAM = New System.Windows.Forms.Button
        Me.cmd_ListReaders_SAM = New System.Windows.Forms.Button
        Me.cmbSAM = New System.Windows.Forms.ComboBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lbl_r_Krd = New System.Windows.Forms.Label
        Me.lbl_r_Kcf = New System.Windows.Forms.Label
        Me.lbl_r_Kcr = New System.Windows.Forms.Label
        Me.lbl_r_Kd = New System.Windows.Forms.Label
        Me.txt_PIN = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.lbl_r_Kt = New System.Windows.Forms.Label
        Me.lbl_Kt = New System.Windows.Forms.Label
        Me.lbl_r_kc = New System.Windows.Forms.Label
        Me.rb_3DES = New System.Windows.Forms.RadioButton
        Me.rb_DES = New System.Windows.Forms.RadioButton
        Me.lbl_Krd = New System.Windows.Forms.Label
        Me.lbl_Kcf = New System.Windows.Forms.Label
        Me.lbl_Kcr = New System.Windows.Forms.Label
        Me.lbl_Kd = New System.Windows.Forms.Label
        Me.lbl_Kc = New System.Windows.Forms.Label
        Me.lbl_IC = New System.Windows.Forms.Label
        Me.lbl_CardSN = New System.Windows.Forms.Label
        Me.cmd_GenerateKey = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.cmd_SaveKeys = New System.Windows.Forms.Button
        Me.cmd_Connect_SLT = New System.Windows.Forms.Button
        Me.cmd_ListReaders_SLT = New System.Windows.Forms.Button
        Me.cmbSLT = New System.Windows.Forms.ComboBox
        Me.lst_Log = New System.Windows.Forms.ListBox
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(6, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(372, 507)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(364, 481)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "SAM Initialization"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtKrd)
        Me.GroupBox1.Controls.Add(Me.txtKcf)
        Me.GroupBox1.Controls.Add(Me.txtKcr)
        Me.GroupBox1.Controls.Add(Me.txtKd)
        Me.GroupBox1.Controls.Add(Me.txtKt)
        Me.GroupBox1.Controls.Add(Me.txtKc)
        Me.GroupBox1.Controls.Add(Me.txt_IC)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtSAMGPIN)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmd_InitSAM)
        Me.GroupBox1.Controls.Add(Me.cmd_Connect_SAM)
        Me.GroupBox1.Controls.Add(Me.cmd_ListReaders_SAM)
        Me.GroupBox1.Controls.Add(Me.cmbSAM)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(352, 464)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(24, 169)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(312, 24)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "---------------------------SAM Master Keys---------------------------"
        '
        'txtKrd
        '
        Me.txtKrd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtKrd.Enabled = False
        Me.txtKrd.Location = New System.Drawing.Point(125, 386)
        Me.txtKrd.MaxLength = 32
        Me.txtKrd.Name = "txtKrd"
        Me.txtKrd.Size = New System.Drawing.Size(204, 21)
        Me.txtKrd.TabIndex = 11
        '
        'txtKcf
        '
        Me.txtKcf.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtKcf.Enabled = False
        Me.txtKcf.Location = New System.Drawing.Point(125, 353)
        Me.txtKcf.MaxLength = 32
        Me.txtKcf.Name = "txtKcf"
        Me.txtKcf.Size = New System.Drawing.Size(204, 21)
        Me.txtKcf.TabIndex = 10
        '
        'txtKcr
        '
        Me.txtKcr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtKcr.Enabled = False
        Me.txtKcr.Location = New System.Drawing.Point(125, 324)
        Me.txtKcr.MaxLength = 32
        Me.txtKcr.Name = "txtKcr"
        Me.txtKcr.Size = New System.Drawing.Size(204, 21)
        Me.txtKcr.TabIndex = 9
        '
        'txtKd
        '
        Me.txtKd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtKd.Enabled = False
        Me.txtKd.Location = New System.Drawing.Point(125, 295)
        Me.txtKd.MaxLength = 32
        Me.txtKd.Name = "txtKd"
        Me.txtKd.Size = New System.Drawing.Size(204, 21)
        Me.txtKd.TabIndex = 8
        '
        'txtKt
        '
        Me.txtKt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtKt.Enabled = False
        Me.txtKt.Location = New System.Drawing.Point(125, 264)
        Me.txtKt.MaxLength = 32
        Me.txtKt.Name = "txtKt"
        Me.txtKt.Size = New System.Drawing.Size(204, 21)
        Me.txtKt.TabIndex = 7
        '
        'txtKc
        '
        Me.txtKc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtKc.Enabled = False
        Me.txtKc.Location = New System.Drawing.Point(125, 233)
        Me.txtKc.MaxLength = 32
        Me.txtKc.Name = "txtKc"
        Me.txtKc.Size = New System.Drawing.Size(204, 21)
        Me.txtKc.TabIndex = 6
        '
        'txt_IC
        '
        Me.txt_IC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_IC.Enabled = False
        Me.txt_IC.Location = New System.Drawing.Point(125, 201)
        Me.txt_IC.MaxLength = 32
        Me.txt_IC.Name = "txt_IC"
        Me.txt_IC.Size = New System.Drawing.Size(204, 21)
        Me.txt_IC.TabIndex = 5
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(21, 358)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(100, 18)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Certify Key (Kcf)"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(21, 389)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(100, 27)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Revoke Debit Key (Krd)"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(21, 329)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(100, 15)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Credit Key (Kcr)"
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(21, 301)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(100, 19)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Debit Key (Kd)"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(21, 238)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 18)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Card Key (Kc)"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(21, 270)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 18)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Terminal Key (Kt)"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(21, 205)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 23)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Issuer Code (IC)"
        '
        'txtSAMGPIN
        '
        Me.txtSAMGPIN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSAMGPIN.Enabled = False
        Me.txtSAMGPIN.Location = New System.Drawing.Point(127, 125)
        Me.txtSAMGPIN.MaxLength = 16
        Me.txtSAMGPIN.Name = "txtSAMGPIN"
        Me.txtSAMGPIN.Size = New System.Drawing.Size(112, 21)
        Me.txtSAMGPIN.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(24, 125)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 23)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "SAM GLOBAL PIN"
        '
        'cmd_InitSAM
        '
        Me.cmd_InitSAM.Enabled = False
        Me.cmd_InitSAM.Location = New System.Drawing.Point(239, 425)
        Me.cmd_InitSAM.Name = "cmd_InitSAM"
        Me.cmd_InitSAM.Size = New System.Drawing.Size(88, 23)
        Me.cmd_InitSAM.TabIndex = 12
        Me.cmd_InitSAM.Text = "Initialize SAM"
        '
        'cmd_Connect_SAM
        '
        Me.cmd_Connect_SAM.Enabled = False
        Me.cmd_Connect_SAM.Location = New System.Drawing.Point(231, 72)
        Me.cmd_Connect_SAM.Name = "cmd_Connect_SAM"
        Me.cmd_Connect_SAM.Size = New System.Drawing.Size(96, 23)
        Me.cmd_Connect_SAM.TabIndex = 3
        Me.cmd_Connect_SAM.Text = "Connect"
        '
        'cmd_ListReaders_SAM
        '
        Me.cmd_ListReaders_SAM.Location = New System.Drawing.Point(230, 40)
        Me.cmd_ListReaders_SAM.Name = "cmd_ListReaders_SAM"
        Me.cmd_ListReaders_SAM.Size = New System.Drawing.Size(97, 23)
        Me.cmd_ListReaders_SAM.TabIndex = 1
        Me.cmd_ListReaders_SAM.Text = "List Readers"
        '
        'cmbSAM
        '
        Me.cmbSAM.Location = New System.Drawing.Point(24, 40)
        Me.cmbSAM.Name = "cmbSAM"
        Me.cmbSAM.Size = New System.Drawing.Size(184, 21)
        Me.cmbSAM.TabIndex = 2
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(364, 481)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "ACOS Card Initialization"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lbl_r_Krd)
        Me.GroupBox2.Controls.Add(Me.lbl_r_Kcf)
        Me.GroupBox2.Controls.Add(Me.lbl_r_Kcr)
        Me.GroupBox2.Controls.Add(Me.lbl_r_Kd)
        Me.GroupBox2.Controls.Add(Me.txt_PIN)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.lbl_r_Kt)
        Me.GroupBox2.Controls.Add(Me.lbl_Kt)
        Me.GroupBox2.Controls.Add(Me.lbl_r_kc)
        Me.GroupBox2.Controls.Add(Me.rb_3DES)
        Me.GroupBox2.Controls.Add(Me.rb_DES)
        Me.GroupBox2.Controls.Add(Me.lbl_Krd)
        Me.GroupBox2.Controls.Add(Me.lbl_Kcf)
        Me.GroupBox2.Controls.Add(Me.lbl_Kcr)
        Me.GroupBox2.Controls.Add(Me.lbl_Kd)
        Me.GroupBox2.Controls.Add(Me.lbl_Kc)
        Me.GroupBox2.Controls.Add(Me.lbl_IC)
        Me.GroupBox2.Controls.Add(Me.lbl_CardSN)
        Me.GroupBox2.Controls.Add(Me.cmd_GenerateKey)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.cmd_SaveKeys)
        Me.GroupBox2.Controls.Add(Me.cmd_Connect_SLT)
        Me.GroupBox2.Controls.Add(Me.cmd_ListReaders_SLT)
        Me.GroupBox2.Controls.Add(Me.cmbSLT)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(352, 464)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Generated Keys"
        '
        'lbl_r_Krd
        '
        Me.lbl_r_Krd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_r_Krd.Location = New System.Drawing.Point(228, 384)
        Me.lbl_r_Krd.Name = "lbl_r_Krd"
        Me.lbl_r_Krd.Size = New System.Drawing.Size(112, 23)
        Me.lbl_r_Krd.TabIndex = 34
        Me.lbl_r_Krd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_r_Kcf
        '
        Me.lbl_r_Kcf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_r_Kcf.Location = New System.Drawing.Point(228, 352)
        Me.lbl_r_Kcf.Name = "lbl_r_Kcf"
        Me.lbl_r_Kcf.Size = New System.Drawing.Size(112, 23)
        Me.lbl_r_Kcf.TabIndex = 33
        Me.lbl_r_Kcf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_r_Kcr
        '
        Me.lbl_r_Kcr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_r_Kcr.Location = New System.Drawing.Point(228, 320)
        Me.lbl_r_Kcr.Name = "lbl_r_Kcr"
        Me.lbl_r_Kcr.Size = New System.Drawing.Size(112, 23)
        Me.lbl_r_Kcr.TabIndex = 32
        Me.lbl_r_Kcr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_r_Kd
        '
        Me.lbl_r_Kd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_r_Kd.Location = New System.Drawing.Point(228, 288)
        Me.lbl_r_Kd.Name = "lbl_r_Kd"
        Me.lbl_r_Kd.Size = New System.Drawing.Size(112, 23)
        Me.lbl_r_Kd.TabIndex = 31
        Me.lbl_r_Kd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_PIN
        '
        Me.txt_PIN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_PIN.Location = New System.Drawing.Point(115, 133)
        Me.txt_PIN.MaxLength = 16
        Me.txt_PIN.Name = "txt_PIN"
        Me.txt_PIN.Size = New System.Drawing.Size(112, 21)
        Me.txt_PIN.TabIndex = 17
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(13, 141)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(88, 14)
        Me.Label16.TabIndex = 66
        Me.Label16.Text = "ACOS Card PIN"
        '
        'lbl_r_Kt
        '
        Me.lbl_r_Kt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_r_Kt.Location = New System.Drawing.Point(228, 256)
        Me.lbl_r_Kt.Name = "lbl_r_Kt"
        Me.lbl_r_Kt.Size = New System.Drawing.Size(112, 23)
        Me.lbl_r_Kt.TabIndex = 30
        Me.lbl_r_Kt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Kt
        '
        Me.lbl_Kt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Kt.Location = New System.Drawing.Point(116, 256)
        Me.lbl_Kt.Name = "lbl_Kt"
        Me.lbl_Kt.Size = New System.Drawing.Size(112, 23)
        Me.lbl_Kt.TabIndex = 24
        Me.lbl_Kt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_r_kc
        '
        Me.lbl_r_kc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_r_kc.Location = New System.Drawing.Point(228, 222)
        Me.lbl_r_kc.Name = "lbl_r_kc"
        Me.lbl_r_kc.Size = New System.Drawing.Size(112, 23)
        Me.lbl_r_kc.TabIndex = 29
        Me.lbl_r_kc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'rb_3DES
        '
        Me.rb_3DES.Location = New System.Drawing.Point(221, 160)
        Me.rb_3DES.Name = "rb_3DES"
        Me.rb_3DES.Size = New System.Drawing.Size(104, 24)
        Me.rb_3DES.TabIndex = 19
        Me.rb_3DES.Text = "3 DES"
        '
        'rb_DES
        '
        Me.rb_DES.Location = New System.Drawing.Point(117, 160)
        Me.rb_DES.Name = "rb_DES"
        Me.rb_DES.Size = New System.Drawing.Size(104, 24)
        Me.rb_DES.TabIndex = 18
        Me.rb_DES.Text = "DES"
        '
        'lbl_Krd
        '
        Me.lbl_Krd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Krd.Location = New System.Drawing.Point(116, 384)
        Me.lbl_Krd.Name = "lbl_Krd"
        Me.lbl_Krd.Size = New System.Drawing.Size(112, 23)
        Me.lbl_Krd.TabIndex = 28
        Me.lbl_Krd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Kcf
        '
        Me.lbl_Kcf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Kcf.Location = New System.Drawing.Point(116, 352)
        Me.lbl_Kcf.Name = "lbl_Kcf"
        Me.lbl_Kcf.Size = New System.Drawing.Size(112, 23)
        Me.lbl_Kcf.TabIndex = 27
        Me.lbl_Kcf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Kcr
        '
        Me.lbl_Kcr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Kcr.Location = New System.Drawing.Point(116, 320)
        Me.lbl_Kcr.Name = "lbl_Kcr"
        Me.lbl_Kcr.Size = New System.Drawing.Size(112, 23)
        Me.lbl_Kcr.TabIndex = 26
        Me.lbl_Kcr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Kd
        '
        Me.lbl_Kd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Kd.Location = New System.Drawing.Point(116, 288)
        Me.lbl_Kd.Name = "lbl_Kd"
        Me.lbl_Kd.Size = New System.Drawing.Size(112, 23)
        Me.lbl_Kd.TabIndex = 25
        Me.lbl_Kd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Kc
        '
        Me.lbl_Kc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Kc.Location = New System.Drawing.Point(116, 222)
        Me.lbl_Kc.Name = "lbl_Kc"
        Me.lbl_Kc.Size = New System.Drawing.Size(112, 23)
        Me.lbl_Kc.TabIndex = 23
        Me.lbl_Kc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_IC
        '
        Me.lbl_IC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_IC.Location = New System.Drawing.Point(116, 188)
        Me.lbl_IC.Name = "lbl_IC"
        Me.lbl_IC.Size = New System.Drawing.Size(223, 23)
        Me.lbl_IC.TabIndex = 22
        Me.lbl_IC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_CardSN
        '
        Me.lbl_CardSN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_CardSN.Location = New System.Drawing.Point(116, 100)
        Me.lbl_CardSN.Name = "lbl_CardSN"
        Me.lbl_CardSN.Size = New System.Drawing.Size(223, 23)
        Me.lbl_CardSN.TabIndex = 16
        Me.lbl_CardSN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmd_GenerateKey
        '
        Me.cmd_GenerateKey.Enabled = False
        Me.cmd_GenerateKey.Location = New System.Drawing.Point(156, 427)
        Me.cmd_GenerateKey.Name = "cmd_GenerateKey"
        Me.cmd_GenerateKey.Size = New System.Drawing.Size(88, 23)
        Me.cmd_GenerateKey.TabIndex = 20
        Me.cmd_GenerateKey.Text = "Generate Key"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(12, 356)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(93, 18)
        Me.Label6.TabIndex = 41
        Me.Label6.Text = "Certify Key (Kcf)"
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(12, 386)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(93, 27)
        Me.Label11.TabIndex = 40
        Me.Label11.Text = "Revoke Debit Key (Krd)"
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(12, 324)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(85, 15)
        Me.Label12.TabIndex = 39
        Me.Label12.Text = "Credit Key (Kcr)"
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(12, 291)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(77, 19)
        Me.Label13.TabIndex = 38
        Me.Label13.Text = "Debit Key (Kd)"
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(12, 226)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(77, 17)
        Me.Label14.TabIndex = 37
        Me.Label14.Text = "Card Key (Kc)"
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(12, 257)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(93, 23)
        Me.Label15.TabIndex = 36
        Me.Label15.Text = "Terminal Key (Kt)"
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(12, 196)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(93, 23)
        Me.Label17.TabIndex = 34
        Me.Label17.Text = "Issuer Code (IC)"
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(12, 103)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(108, 23)
        Me.Label18.TabIndex = 30
        Me.Label18.Text = "Card Serial Number"
        '
        'cmd_SaveKeys
        '
        Me.cmd_SaveKeys.Enabled = False
        Me.cmd_SaveKeys.Location = New System.Drawing.Point(251, 427)
        Me.cmd_SaveKeys.Name = "cmd_SaveKeys"
        Me.cmd_SaveKeys.Size = New System.Drawing.Size(88, 23)
        Me.cmd_SaveKeys.TabIndex = 21
        Me.cmd_SaveKeys.Text = "Save Keys"
        '
        'cmd_Connect_SLT
        '
        Me.cmd_Connect_SLT.Location = New System.Drawing.Point(234, 61)
        Me.cmd_Connect_SLT.Name = "cmd_Connect_SLT"
        Me.cmd_Connect_SLT.Size = New System.Drawing.Size(96, 23)
        Me.cmd_Connect_SLT.TabIndex = 15
        Me.cmd_Connect_SLT.Text = "Connect"
        '
        'cmd_ListReaders_SLT
        '
        Me.cmd_ListReaders_SLT.Location = New System.Drawing.Point(233, 29)
        Me.cmd_ListReaders_SLT.Name = "cmd_ListReaders_SLT"
        Me.cmd_ListReaders_SLT.Size = New System.Drawing.Size(97, 23)
        Me.cmd_ListReaders_SLT.TabIndex = 13
        Me.cmd_ListReaders_SLT.Text = "List Readers"
        '
        'cmbSLT
        '
        Me.cmbSLT.Location = New System.Drawing.Point(27, 29)
        Me.cmbSLT.Name = "cmbSLT"
        Me.cmbSLT.Size = New System.Drawing.Size(184, 21)
        Me.cmbSLT.TabIndex = 14
        '
        'lst_Log
        '
        Me.lst_Log.Location = New System.Drawing.Point(385, 15)
        Me.lst_Log.Name = "lst_Log"
        Me.lst_Log.Size = New System.Drawing.Size(369, 498)
        Me.lst_Log.TabIndex = 1
        '
        'frm_Main
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(760, 525)
        Me.Controls.Add(Me.lst_Log)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Key Management Sample"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim G_retCode, G_hContext, G_hCard, G_hCardSAM, G_Protocol As Integer
    Dim G_DevName, G_DevNameSAM As String
    Dim G_ioRequest As SCARD_IO_REQUEST
    Dim G_AlgoRef, G_TLV_LEN As Byte
    Dim G_List_SAM, G_List_SLT As String


    Private Sub frm_Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Initialization
        rb_DES.Checked = True
        G_AlgoRef = 1

    End Sub


    Private Sub cmd_ListReaders_SAM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ListReaders_SAM.Click

        'Initialize List of Available Readers

        Dim sReaderList As String
        Dim ReaderCount As Integer
        Dim sReaderGroup As String
        Dim ctr As Integer

        cmbSAM.Items.Clear()

        For ctr = 0 To 255
            sReaderList = sReaderList + vbNullChar
        Next

        ReaderCount = 255

        ' 1. Establish context and obtain G_hContext handle
        G_retCode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, G_hContext)

        If G_retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            lst_Log.Items.Add("SAM SCardEstablisG_hContext Error: " & GetScardErrMsg(G_retCode))
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            Exit Sub
        Else
            lst_Log.Items.Add("SAM SCardEstablisG_hContext Success")
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
        End If

        ' 2. List PC/SC card readers installed in the system
        G_retCode = ModWinsCard.SCardListReaders(G_hContext, "", sReaderList, ReaderCount)

        If G_retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            lst_Log.Items.Add("SAM SCardListReaders Error: " & GetScardErrMsg(G_retCode))
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            Exit Sub
        Else
            lst_Log.Items.Add("SAM SCardListReaders Success")
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
        End If

        'Load Available Readers
        LoadListToControl(cmbSAM, sReaderList)

        If cmbSAM.Items.Count > 0 Then
            cmbSAM.SelectedIndex = 0
            cmd_Connect_SAM.Enabled = True
        Else
            lst_Log.Items.Add("SAM Reader Not Found")
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
        End If

    End Sub


    Public Sub LoadListToControl(ByVal Ctrl As ComboBox, ByVal ReaderList As String)

        Dim sTemp As String
        Dim indx As Integer

        indx = 1
        sTemp = ""
        Ctrl.Items.Clear()

        While (Mid(ReaderList, indx, 1) <> vbNullChar)

            While (Mid(ReaderList, indx, 1) <> vbNullChar)
                sTemp = sTemp + Mid(ReaderList, indx, 1)
                indx = indx + 1
            End While
            indx = indx + 1

            Ctrl.Items.Add(sTemp)

            sTemp = ""
        End While

    End Sub


    ' Routines for ErrorCodes
    Public Function GetScardErrMsg(ByVal ReturnCode As Integer) As String

        Select Case ReturnCode
            Case ModWinsCard.SCARD_E_CANCELLED
                GetScardErrMsg = "The action was canceled by an SCardCancel request."
            Case ModWinsCard.SCARD_E_CANT_DISPOSE
                GetScardErrMsg = "The system could not dispose of the media in the requested manner."
            Case ModWinsCard.SCARD_E_CARD_UNSUPPORTED
                GetScardErrMsg = "The smart card does not meet minimal requirements for support."
            Case ModWinsCard.SCARD_E_DUPLICATE_READER
                GetScardErrMsg = "The reader driver didn't produce a unique reader name."
            Case ModWinsCard.SCARD_E_INSUFFICIENT_BUFFER
                GetScardErrMsg = "The data buffer for returned data is too small for the returned data."
            Case ModWinsCard.SCARD_E_INVALID_ATR
                GetScardErrMsg = "An ATR string obtained from the registry is not a valid ATR string."
            Case ModWinsCard.SCARD_E_INVALID_HANDLE
                GetScardErrMsg = "The supplied handle was invalid."
            Case ModWinsCard.SCARD_E_INVALID_PARAMETER
                GetScardErrMsg = "One or more of the supplied parameters could not be properly interpreted."
            Case ModWinsCard.SCARD_E_INVALID_TARGET
                GetScardErrMsg = "Registry startup information is missing or invalid."
            Case ModWinsCard.SCARD_E_INVALID_VALUE
                GetScardErrMsg = "One or more of the supplied parameter values could not be properly interpreted."
            Case ModWinsCard.SCARD_E_NOT_READY
                GetScardErrMsg = "The reader or card is not ready to accept commands."
            Case ModWinsCard.SCARD_E_NOT_TRANSACTED
                GetScardErrMsg = "An attempt was made to end a non-existent transaction."
            Case ModWinsCard.SCARD_E_NO_MEMORY
                GetScardErrMsg = "Not enough memory available to complete this command."
            Case ModWinsCard.SCARD_E_NO_SERVICE
                GetScardErrMsg = "The smart card resource manager is not running."
            Case ModWinsCard.SCARD_E_NO_SMARTCARD
                GetScardErrMsg = "The operation requires a smart card, but no smart card is currently in the device."
            Case ModWinsCard.SCARD_E_PCI_TOO_SMALL
                GetScardErrMsg = "The PCI receive buffer was too small."
            Case ModWinsCard.SCARD_E_PROTO_MISMATCH
                GetScardErrMsg = "The requested protocols are incompatible with the protocol currently in use with the card."
            Case ModWinsCard.SCARD_E_READER_UNAVAILABLE
                GetScardErrMsg = "The specified reader is not currently available for use."
            Case ModWinsCard.SCARD_E_READER_UNSUPPORTED
                GetScardErrMsg = "The reader driver does not meet minimal requirements for support."
            Case ModWinsCard.SCARD_E_SERVICE_STOPPED
                GetScardErrMsg = "The smart card resource manager has shut down."
            Case ModWinsCard.SCARD_E_SHARING_VIOLATION
                GetScardErrMsg = "The smart card cannot be accessed because of other outstanding connections."
            Case ModWinsCard.SCARD_E_SYSTEM_CANCELLED
                GetScardErrMsg = "The action was canceled by the system, presumably to log off or shut down."
            Case ModWinsCard.SCARD_E_TIMEOUT
                GetScardErrMsg = "The user-specified timeout value has expired."
            Case ModWinsCard.SCARD_E_UNKNOWN_CARD
                GetScardErrMsg = "The specified smart card name is not recognized."
            Case ModWinsCard.SCARD_E_UNKNOWN_READER
                GetScardErrMsg = "The specified reader name is not recognized."
            Case ModWinsCard.SCARD_F_COMM_ERROR
                GetScardErrMsg = "An internal communications error has been detected."
            Case ModWinsCard.SCARD_F_INTERNAL_ERROR
                GetScardErrMsg = "An internal consistency check failed."
            Case ModWinsCard.SCARD_F_UNKNOWN_ERROR
                GetScardErrMsg = "An internal error has been detected, but the source is unknown."
            Case ModWinsCard.SCARD_F_WAITED_TOO_Integer
                GetScardErrMsg = "An internal consistency timer has expired."
            Case ModWinsCard.SCARD_S_SUCCESS
                GetScardErrMsg = "No error was encountered."
            Case ModWinsCard.SCARD_W_REMOVED_CARD
                GetScardErrMsg = "The smart card has been removed, so that further communication is not possible."
            Case ModWinsCard.SCARD_W_RESET_CARD
                GetScardErrMsg = "The smart card has been reset, so any shared state information is invalid."
            Case ModWinsCard.SCARD_W_UNPOWERED_CARD
                GetScardErrMsg = "Power has been removed from the smart card, so that further communication is not possible."
            Case ModWinsCard.SCARD_W_UNRESPONSIVE_CARD
                GetScardErrMsg = "The smart card is not responding to a reset."
            Case ModWinsCard.SCARD_W_UNSUPPORTED_CARD
                GetScardErrMsg = "The reader cannot communicate with the card, due to ATR string configuration conflicts."
            Case Else
                GetScardErrMsg = "?"
        End Select
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

    End Function


    Private Sub cmd_ListReaders_SLT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ListReaders_SLT.Click

        'Initialize List of Available Readers

        Dim sReaderList As String
        Dim ReaderCount As Integer
        Dim sReaderGroup As String
        Dim ctr As Integer

        For ctr = 0 To 255
            sReaderList = sReaderList + vbNullChar
        Next

        cmbSAM.Items.Clear()

        ReaderCount = 255

        ' 1. Establish context and obtain G_hContext handle
        G_retCode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, G_hContext)

        If G_retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            lst_Log.Items.Add("SCardEstablisG_hContext Error: " & GetScardErrMsg(G_retCode))
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            Exit Sub
        Else
            lst_Log.Items.Add("SCardEstablisG_hContext Success")
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
        End If

        ' 2. List PC/SC card readers installed in the system
        G_retCode = ModWinsCard.SCardListReaders(G_hContext, "", sReaderList, ReaderCount)

        If G_retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            lst_Log.Items.Add("SCardListReaders Error: " & GetScardErrMsg(G_retCode))
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            Exit Sub
        Else
            lst_Log.Items.Add("SCardListReaders Success")
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
        End If

        'Load Available Readers
        LoadListToControl(cmbSLT, sReaderList)

        If cmbSLT.Items.Count > 0 Then
            cmbSLT.SelectedIndex = 0
            cmd_Connect_SLT.Enabled = True
        Else
            lst_Log.Items.Add("Reader Not Found")
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
        End If

    End Sub


    Private Sub cmd_Connect_SAM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Connect_SAM.Click

        'Establish SAM Reader Connection

        'Get Reader Name
        G_DevNameSAM = G_List_SAM

        'Disconnect
        G_retCode = ModWinsCard.SCardDisconnect(G_hCardSAM, ModWinsCard.SCARD_UNPOWER_CARD)

        'Connect
        G_retCode = ModWinsCard.SCardConnect(G_hContext, _
                                             G_DevNameSAM, _
                                             ModWinsCard.SCARD_SHARE_EXCLUSIVE, _
                                             ModWinsCard.SCARD_PROTOCOL_T0 Or ModWinsCard.SCARD_PROTOCOL_T1, _
                                             G_hCardSAM, _
                                             G_Protocol)

        If G_retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            lst_Log.Items.Add("SAM SCardConnect Error: " & GetScardErrMsg(G_retCode))
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            Exit Sub
        Else
            lst_Log.Items.Add("SAM SCardConnect Success")
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
        End If

        'Enable PIN and Master Keys Inputbox
        txtSAMGPIN.Enabled = True

        rb_DES.Enabled = True
        rb_3DES.Enabled = True

        txt_IC.Enabled = True
        txtKc.Enabled = True
        txtKcf.Enabled = True
        txtKcr.Enabled = True
        txtKd.Enabled = True
        txtKrd.Enabled = True
        txtKt.Enabled = True

        cmd_InitSAM.Enabled = True

    End Sub


    Private Sub cmd_Connect_SLT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Connect_SLT.Click

        'Establish Reader Connection

        'Get Reader name
        G_DevName = G_List_SLT

        'Disconnect
        G_retCode = ModWinsCard.SCardDisconnect(G_hCard, ModWinsCard.SCARD_UNPOWER_CARD)

        'Connect
        G_retCode = ModWinsCard.SCardConnect(G_hContext, _
                                             G_DevName, _
                                             ModWinsCard.SCARD_SHARE_EXCLUSIVE, _
                                             ModWinsCard.SCARD_PROTOCOL_T0 Or ModWinsCard.SCARD_PROTOCOL_T1, _
                                             G_hCard, _
                                             G_Protocol)


        If G_retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            lst_Log.Items.Add("SCardConnect Error: " & GetScardErrMsg(G_retCode))
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            Exit Sub
        Else
            lst_Log.Items.Add("SCardConnect Success")
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
        End If

        'Enable buttons
        cmd_GenerateKey.Enabled = True
        cmd_SaveKeys.Enabled = True

    End Sub


    Private Sub cmd_InitSAM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_InitSAM.Click

        Dim SendBuff() As Byte
        Dim RecvBuff() As Byte
        Dim RecvLen As Integer

        'Check if null and check length if correct
        If txtSAMGPIN.Text = "" Then
            lst_Log.Items.Add("Input Required")
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            txtSAMGPIN.Focus()
            Exit Sub
        End If

        If Len(txtSAMGPIN.Text) <> 16 Then
            lst_Log.Items.Add("Invalid Input Length")
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            txtSAMGPIN.Focus()
            Exit Sub
        End If

        If Not CheckInput(txt_IC) Then Exit Sub
        If Not CheckInput(txtKc) Then Exit Sub
        If Not CheckInput(txtKt) Then Exit Sub
        If Not CheckInput(txtKd) Then Exit Sub
        If Not CheckInput(txtKcr) Then Exit Sub
        If Not CheckInput(txtKcf) Then Exit Sub
        If Not CheckInput(txtKrd) Then Exit Sub


        'Clear Card's EEPROM
        ReDim SendBuff(4)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H30
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H0

        lst_Log.Items.Add("SAM < 80 30 00 00 00")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = False Then
            lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            Exit Sub
        Else
            lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
        End If

        'Reset
        cmd_Connect_SAM_Click(sender, e)

        'Create MF
        ReDim SendBuff(18)
        ReDim RecvBuff(1)

        SendBuff(0) = &H0
        SendBuff(1) = &HE0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &HE
        SendBuff(5) = &H62
        SendBuff(6) = &HC
        SendBuff(7) = &H80
        SendBuff(8) = &H2
        SendBuff(9) = &H2C
        SendBuff(10) = &H0
        SendBuff(11) = &H82
        SendBuff(12) = &H2
        SendBuff(13) = &H3F
        SendBuff(14) = &HFF
        SendBuff(15) = &H83
        SendBuff(16) = &H2
        SendBuff(17) = &H3F
        SendBuff(18) = &H0

        lst_Log.Items.Add("SAM < 00 E0 00 00 0E")
        lst_Log.Items.Add("    <<62 0C 80 02 2C 00 82 02 02 3F FF 83 02 3F 00")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 19, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If

        'Create EF1 to store PIN's
        'FDB=0C MRL=0A NOR=01 READ=NONE WRITE=IC
        ReDim SendBuff(31)
        ReDim RecvBuff(1)

        SendBuff(0) = &H0
        SendBuff(1) = &HE0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H1B
        SendBuff(5) = &H62
        SendBuff(6) = &H19
        SendBuff(7) = &H83
        SendBuff(8) = &H2
        SendBuff(9) = &HFF
        SendBuff(10) = &HA
        SendBuff(11) = &H88
        SendBuff(12) = &H1
        SendBuff(13) = &H1
        SendBuff(14) = &H82
        SendBuff(15) = &H6
        SendBuff(16) = &HC
        SendBuff(17) = &H0
        SendBuff(18) = &H0
        SendBuff(19) = &HA
        SendBuff(20) = &H0
        SendBuff(21) = &H1
        SendBuff(22) = &H8C
        SendBuff(23) = &H8
        SendBuff(24) = &H7F
        SendBuff(25) = &HFF
        SendBuff(26) = &HFF
        SendBuff(27) = &HFF
        SendBuff(28) = &HFF
        SendBuff(29) = &H27
        SendBuff(30) = &H27
        SendBuff(31) = &HFF

        lst_Log.Items.Add("SAM < 00 E0 00 00 1B")
        lst_Log.Items.Add("    <<62 19 83 02 FF 0A 88 01 01 82 06 0C 00 00 0A 00")
        lst_Log.Items.Add("    <<01 8C 08 7F FF FF FF FF 27 27 FF")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 32, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        End If

        'Set Global PIN's
        ReDim SendBuff(14)
        ReDim RecvBuff(1)

        SendBuff(0) = &H0
        SendBuff(1) = &HDC
        SendBuff(2) = &H1
        SendBuff(3) = &H4
        SendBuff(4) = &HA
        SendBuff(5) = &H1
        SendBuff(6) = &H88
        SendBuff(7) = CInt("&H" & Mid(txtSAMGPIN.Text, 1, 2))
        SendBuff(8) = CInt("&H" & Mid(txtSAMGPIN.Text, 3, 2))
        SendBuff(9) = CInt("&H" & Mid(txtSAMGPIN.Text, 5, 2))
        SendBuff(10) = CInt("&H" & Mid(txtSAMGPIN.Text, 7, 2))
        SendBuff(11) = CInt("&H" & Mid(txtSAMGPIN.Text, 9, 2))
        SendBuff(12) = CInt("&H" & Mid(txtSAMGPIN.Text, 11, 2))
        SendBuff(13) = CInt("&H" & Mid(txtSAMGPIN.Text, 13, 2))
        SendBuff(14) = CInt("&H" & Mid(txtSAMGPIN.Text, 15, 2))

        lst_Log.Items.Add("SAM < 00 DC 01 04 0A")
        lst_Log.Items.Add("    <<01 88 " & Mid(txtSAMGPIN.Text, 1, 2) & " " _
                                       & Mid(txtSAMGPIN.Text, 3, 2) & " " _
                                       & Mid(txtSAMGPIN.Text, 5, 2) & " " _
                                       & Mid(txtSAMGPIN.Text, 7, 2) & " " _
                                       & Mid(txtSAMGPIN.Text, 9, 2) & " " _
                                       & Mid(txtSAMGPIN.Text, 11, 2) & " " _
                                       & Mid(txtSAMGPIN.Text, 13, 2) & " " _
                                       & Mid(txtSAMGPIN.Text, 15, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 15, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If

        'Create Next DF DRT01: 1100

        ReDim SendBuff(47)
        ReDim RecvBuff(1)

        SendBuff(0) = &H0
        SendBuff(1) = &HE0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H2B
        SendBuff(5) = &H62
        SendBuff(6) = &H29
        SendBuff(7) = &H82
        SendBuff(8) = &H1
        SendBuff(9) = &H38
        SendBuff(10) = &H83
        SendBuff(11) = &H2
        SendBuff(12) = &H11
        SendBuff(13) = &H0
        SendBuff(14) = &H8A
        SendBuff(15) = &H1
        SendBuff(16) = &H1
        SendBuff(17) = &H8C
        SendBuff(18) = &H8
        SendBuff(19) = &H7F
        SendBuff(20) = &H3
        SendBuff(21) = &H3
        SendBuff(22) = &H3
        SendBuff(23) = &H3
        SendBuff(24) = &H3
        SendBuff(25) = &H3
        SendBuff(26) = &H3
        SendBuff(27) = &H8D
        SendBuff(28) = &H2
        SendBuff(29) = &H41
        SendBuff(30) = &H3
        SendBuff(31) = &H80
        SendBuff(32) = &H2
        SendBuff(33) = &H3
        SendBuff(34) = &H20
        SendBuff(35) = &HAB
        SendBuff(36) = &HB
        SendBuff(37) = &H84
        SendBuff(38) = &H1
        SendBuff(39) = &H88
        SendBuff(40) = &HA4
        SendBuff(41) = &H6
        SendBuff(42) = &H83
        SendBuff(43) = &H1
        SendBuff(44) = &H81
        SendBuff(45) = &H95
        SendBuff(46) = &H1
        SendBuff(47) = &HFF

        lst_Log.Items.Add("SAM < 00 E0 00 00 2B")
        lst_Log.Items.Add("    <<62 29 82 01 38 83 02 11 00 8A 01 01 8C 08 7F 03")
        lst_Log.Items.Add("    <<03 03 03 03 03 03 8D 02 41 03 20 AB 0B 84 01 88")
        lst_Log.Items.Add("    <<A4 06 83 01 81 95 01 FF")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 48, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If

        'Create Key File EF2 1101
        'MRL=16 NOR=08

        ReDim SendBuff(33)
        ReDim RecvBuff(1)

        SendBuff(0) = &H0
        SendBuff(1) = &HE0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H1D
        SendBuff(5) = &H62
        SendBuff(6) = &H1B
        SendBuff(7) = &H82
        SendBuff(8) = &H5
        SendBuff(9) = &HC
        SendBuff(10) = &H41
        SendBuff(11) = &H0
        SendBuff(12) = &H16
        SendBuff(13) = &H8
        SendBuff(14) = &H83
        SendBuff(15) = &H2
        SendBuff(16) = &H11
        SendBuff(17) = &H1
        SendBuff(18) = &H88
        SendBuff(19) = &H1
        SendBuff(20) = &H2
        SendBuff(21) = &H8A
        SendBuff(22) = &H1
        SendBuff(23) = &H1
        SendBuff(24) = &H8C
        SendBuff(25) = &H8
        SendBuff(26) = &H7F
        SendBuff(27) = &H3
        SendBuff(28) = &H3
        SendBuff(29) = &H3
        SendBuff(30) = &H3
        SendBuff(31) = &H3
        SendBuff(32) = &H3
        SendBuff(33) = &H3

        lst_Log.Items.Add("SAM < 00 E0 00 00 1D")
        lst_Log.Items.Add("    <<62 1B 82 05 0C 41 00 16 08 83 02 11 01 88 01 02")
        lst_Log.Items.Add("    <<8A 01 01 8C 08 7F 03 03 03 03 03 03 03")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 34, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        End If


        'Append Record To EF2, Define 8 Key Records in EF2 - Master Keys
        '1st Master key, key ID=81, key type=03, int/ext authenticate, usage counter = FF FF
        ReDim SendBuff(26)
        ReDim RecvBuff(1)

        SendBuff(0) = &H0
        SendBuff(1) = &HE2
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H16
        SendBuff(5) = &H81 'Key ID
        SendBuff(6) = &H3
        SendBuff(7) = &HFF
        SendBuff(8) = &HFF
        SendBuff(9) = &H88
        SendBuff(10) = &H0
        SendBuff(11) = CInt("&H" & Mid(txt_IC.Text, 1, 2))
        SendBuff(12) = CInt("&H" & Mid(txt_IC.Text, 3, 2))
        SendBuff(13) = CInt("&H" & Mid(txt_IC.Text, 5, 2))
        SendBuff(14) = CInt("&H" & Mid(txt_IC.Text, 7, 2))
        SendBuff(15) = CInt("&H" & Mid(txt_IC.Text, 9, 2))
        SendBuff(16) = CInt("&H" & Mid(txt_IC.Text, 11, 2))
        SendBuff(17) = CInt("&H" & Mid(txt_IC.Text, 13, 2))
        SendBuff(18) = CInt("&H" & Mid(txt_IC.Text, 15, 2))
        SendBuff(19) = CInt("&H" & Mid(txt_IC.Text, 17, 2))
        SendBuff(20) = CInt("&H" & Mid(txt_IC.Text, 19, 2))
        SendBuff(21) = CInt("&H" & Mid(txt_IC.Text, 21, 2))
        SendBuff(22) = CInt("&H" & Mid(txt_IC.Text, 23, 2))
        SendBuff(23) = CInt("&H" & Mid(txt_IC.Text, 25, 2))
        SendBuff(24) = CInt("&H" & Mid(txt_IC.Text, 27, 2))
        SendBuff(25) = CInt("&H" & Mid(txt_IC.Text, 29, 2))
        SendBuff(26) = CInt("&H" & Mid(txt_IC.Text, 31, 2))

        lst_Log.Items.Add("SAM < 00 E2 00 00 16")
        lst_Log.Items.Add("    <<81 03 FF FF 88 00 " _
                                & Mid(txt_IC.Text, 1, 2) & " " _
                                & Mid(txt_IC.Text, 3, 2) & " " _
                                & Mid(txt_IC.Text, 5, 2) & " " _
                                & Mid(txt_IC.Text, 7, 2) & " " _
                                & Mid(txt_IC.Text, 9, 2) & " " _
                                & Mid(txt_IC.Text, 11, 2) & " " _
                                & Mid(txt_IC.Text, 13, 2) & " " _
                                & Mid(txt_IC.Text, 15, 2) & " " _
                                & Mid(txt_IC.Text, 17, 2) & " " _
                                & Mid(txt_IC.Text, 19, 2))

        lst_Log.Items.Add("    <<" & Mid(txt_IC.Text, 21, 2) & " " _
                                   & Mid(txt_IC.Text, 23, 2) & " " _
                                   & Mid(txt_IC.Text, 25, 2) & " " _
                                   & Mid(txt_IC.Text, 27, 2) & " " _
                                   & Mid(txt_IC.Text, 29, 2) & " " _
                                   & Mid(txt_IC.Text, 31, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 27, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        '2nd Master key, key ID=82, key type=03, int/ext authenticate, usage counter = FF FF
        ReDim SendBuff(26)
        ReDim RecvBuff(1)

        SendBuff(0) = &H0
        SendBuff(1) = &HE2
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H16
        SendBuff(5) = &H82 'Key ID
        SendBuff(6) = &H3
        SendBuff(7) = &HFF
        SendBuff(8) = &HFF
        SendBuff(9) = &H88
        SendBuff(10) = &H0
        SendBuff(11) = CInt("&H" & Mid(txtKc.Text, 1, 2))
        SendBuff(12) = CInt("&H" & Mid(txtKc.Text, 3, 2))
        SendBuff(13) = CInt("&H" & Mid(txtKc.Text, 5, 2))
        SendBuff(14) = CInt("&H" & Mid(txtKc.Text, 7, 2))
        SendBuff(15) = CInt("&H" & Mid(txtKc.Text, 9, 2))
        SendBuff(16) = CInt("&H" & Mid(txtKc.Text, 11, 2))
        SendBuff(17) = CInt("&H" & Mid(txtKc.Text, 13, 2))
        SendBuff(18) = CInt("&H" & Mid(txtKc.Text, 15, 2))
        SendBuff(19) = CInt("&H" & Mid(txtKc.Text, 17, 2))
        SendBuff(20) = CInt("&H" & Mid(txtKc.Text, 19, 2))
        SendBuff(21) = CInt("&H" & Mid(txtKc.Text, 21, 2))
        SendBuff(22) = CInt("&H" & Mid(txtKc.Text, 23, 2))
        SendBuff(23) = CInt("&H" & Mid(txtKc.Text, 25, 2))
        SendBuff(24) = CInt("&H" & Mid(txtKc.Text, 27, 2))
        SendBuff(25) = CInt("&H" & Mid(txtKc.Text, 29, 2))
        SendBuff(26) = CInt("&H" & Mid(txtKc.Text, 31, 2))

        lst_Log.Items.Add("SAM < 00 E2 00 00 16")
        lst_Log.Items.Add("    <<82 03 FF FF 88 00 " _
                               & Mid(txtKc.Text, 1, 2) & " " _
                               & Mid(txtKc.Text, 3, 2) & " " _
                               & Mid(txtKc.Text, 5, 2) & " " _
                               & Mid(txtKc.Text, 7, 2) & " " _
                               & Mid(txtKc.Text, 9, 2) & " " _
                               & Mid(txtKc.Text, 11, 2) & " " _
                               & Mid(txtKc.Text, 13, 2) & " " _
                               & Mid(txtKc.Text, 15, 2) & " " _
                               & Mid(txtKc.Text, 17, 2) & " " _
                               & Mid(txtKc.Text, 19, 2))

        lst_Log.Items.Add("    <<" & Mid(txtKc.Text, 21, 2) & " " _
                                   & Mid(txtKc.Text, 23, 2) & " " _
                                   & Mid(txtKc.Text, 25, 2) & " " _
                                   & Mid(txtKc.Text, 27, 2) & " " _
                                   & Mid(txtKc.Text, 29, 2) & " " _
                                   & Mid(txtKc.Text, 31, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 27, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        '3rd Master key, key ID=83, key type=03, int/ext authenticate, usage counter = FF FF
        ReDim SendBuff(26)
        ReDim RecvBuff(1)

        SendBuff(0) = &H0
        SendBuff(1) = &HE2
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H16
        SendBuff(5) = &H83 'Key ID
        SendBuff(6) = &H3
        SendBuff(7) = &HFF
        SendBuff(8) = &HFF
        SendBuff(9) = &H88
        SendBuff(10) = &H0
        SendBuff(11) = CInt("&H" & Mid(txtKt.Text, 1, 2))
        SendBuff(12) = CInt("&H" & Mid(txtKt.Text, 3, 2))
        SendBuff(13) = CInt("&H" & Mid(txtKt.Text, 5, 2))
        SendBuff(14) = CInt("&H" & Mid(txtKt.Text, 7, 2))
        SendBuff(15) = CInt("&H" & Mid(txtKt.Text, 9, 2))
        SendBuff(16) = CInt("&H" & Mid(txtKt.Text, 11, 2))
        SendBuff(17) = CInt("&H" & Mid(txtKt.Text, 13, 2))
        SendBuff(18) = CInt("&H" & Mid(txtKt.Text, 15, 2))
        SendBuff(19) = CInt("&H" & Mid(txtKt.Text, 17, 2))
        SendBuff(20) = CInt("&H" & Mid(txtKt.Text, 19, 2))
        SendBuff(21) = CInt("&H" & Mid(txtKt.Text, 21, 2))
        SendBuff(22) = CInt("&H" & Mid(txtKt.Text, 23, 2))
        SendBuff(23) = CInt("&H" & Mid(txtKt.Text, 25, 2))
        SendBuff(24) = CInt("&H" & Mid(txtKt.Text, 27, 2))
        SendBuff(25) = CInt("&H" & Mid(txtKt.Text, 29, 2))
        SendBuff(26) = CInt("&H" & Mid(txtKt.Text, 31, 2))

        lst_Log.Items.Add("SAM < 00 E2 00 00 16")
        lst_Log.Items.Add("    <<83 03 FF FF 88 00 " _
                                         & Mid(txtKt.Text, 1, 2) & " " _
                                         & Mid(txtKt.Text, 3, 2) & " " _
                                         & Mid(txtKt.Text, 5, 2) & " " _
                                         & Mid(txtKt.Text, 7, 2) & " " _
                                         & Mid(txtKt.Text, 9, 2) & " " _
                                         & Mid(txtKt.Text, 11, 2) & " " _
                                         & Mid(txtKt.Text, 13, 2) & " " _
                                         & Mid(txtKt.Text, 15, 2) & " " _
                                         & Mid(txtKt.Text, 17, 2) & " " _
                                         & Mid(txtKt.Text, 19, 2))

        lst_Log.Items.Add("    <<" & Mid(txtKt.Text, 21, 2) & " " _
                                         & Mid(txtKt.Text, 23, 2) & " " _
                                         & Mid(txtKt.Text, 25, 2) & " " _
                                         & Mid(txtKt.Text, 27, 2) & " " _
                                         & Mid(txtKt.Text, 29, 2) & " " _
                                         & Mid(txtKt.Text, 31, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 27, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        '4th Master key, key ID=84, key type=03, int/ext authenticate, usage counter = FF FF
        ReDim SendBuff(26)
        ReDim RecvBuff(1)


        SendBuff(0) = &H0
        SendBuff(1) = &HE2
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H16
        SendBuff(5) = &H84 'Key ID
        SendBuff(6) = &H3
        SendBuff(7) = &HFF
        SendBuff(8) = &HFF
        SendBuff(9) = &H88
        SendBuff(10) = &H0
        SendBuff(11) = CInt("&H" & Mid(txtKd.Text, 1, 2))
        SendBuff(12) = CInt("&H" & Mid(txtKd.Text, 3, 2))
        SendBuff(13) = CInt("&H" & Mid(txtKd.Text, 5, 2))
        SendBuff(14) = CInt("&H" & Mid(txtKd.Text, 7, 2))
        SendBuff(15) = CInt("&H" & Mid(txtKd.Text, 9, 2))
        SendBuff(16) = CInt("&H" & Mid(txtKd.Text, 11, 2))
        SendBuff(17) = CInt("&H" & Mid(txtKd.Text, 13, 2))
        SendBuff(18) = CInt("&H" & Mid(txtKd.Text, 15, 2))
        SendBuff(19) = CInt("&H" & Mid(txtKd.Text, 17, 2))
        SendBuff(20) = CInt("&H" & Mid(txtKd.Text, 19, 2))
        SendBuff(21) = CInt("&H" & Mid(txtKd.Text, 21, 2))
        SendBuff(22) = CInt("&H" & Mid(txtKd.Text, 23, 2))
        SendBuff(23) = CInt("&H" & Mid(txtKd.Text, 25, 2))
        SendBuff(24) = CInt("&H" & Mid(txtKd.Text, 27, 2))
        SendBuff(25) = CInt("&H" & Mid(txtKd.Text, 29, 2))
        SendBuff(26) = CInt("&H" & Mid(txtKd.Text, 31, 2))

        lst_Log.Items.Add("SAM < 00 E2 00 00 16")
        lst_Log.Items.Add("    <<84 03 FF FF 88 00 " _
                                         & Mid(txtKd.Text, 1, 2) & " " _
                                         & Mid(txtKd.Text, 3, 2) & " " _
                                         & Mid(txtKd.Text, 5, 2) & " " _
                                         & Mid(txtKd.Text, 7, 2) & " " _
                                         & Mid(txtKd.Text, 9, 2) & " " _
                                         & Mid(txtKd.Text, 11, 2) & " " _
                                         & Mid(txtKd.Text, 13, 2) & " " _
                                         & Mid(txtKd.Text, 15, 2) & " " _
                                         & Mid(txtKd.Text, 17, 2) & " " _
                                         & Mid(txtKd.Text, 19, 2))

        lst_Log.Items.Add("    <<" & Mid(txtKd.Text, 21, 2) & " " _
                                         & Mid(txtKd.Text, 23, 2) & " " _
                                         & Mid(txtKd.Text, 25, 2) & " " _
                                         & Mid(txtKd.Text, 27, 2) & " " _
                                         & Mid(txtKd.Text, 29, 2) & " " _
                                         & Mid(txtKd.Text, 31, 2))


        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 27, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        '5th Master key, key ID=85, key type=03, int/ext authenticate, usage counter = FF FF
        ReDim SendBuff(26)
        ReDim RecvBuff(1)

        SendBuff(0) = &H0
        SendBuff(1) = &HE2
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H16
        SendBuff(5) = &H85 'Key ID
        SendBuff(6) = &H3
        SendBuff(7) = &HFF
        SendBuff(8) = &HFF
        SendBuff(9) = &H88
        SendBuff(10) = &H0
        SendBuff(11) = CInt("&H" & Mid(txtKcr.Text, 1, 2))
        SendBuff(12) = CInt("&H" & Mid(txtKcr.Text, 3, 2))
        SendBuff(13) = CInt("&H" & Mid(txtKcr.Text, 5, 2))
        SendBuff(14) = CInt("&H" & Mid(txtKcr.Text, 7, 2))
        SendBuff(15) = CInt("&H" & Mid(txtKcr.Text, 9, 2))
        SendBuff(16) = CInt("&H" & Mid(txtKcr.Text, 11, 2))
        SendBuff(17) = CInt("&H" & Mid(txtKcr.Text, 13, 2))
        SendBuff(18) = CInt("&H" & Mid(txtKcr.Text, 15, 2))
        SendBuff(19) = CInt("&H" & Mid(txtKcr.Text, 17, 2))
        SendBuff(20) = CInt("&H" & Mid(txtKcr.Text, 19, 2))
        SendBuff(21) = CInt("&H" & Mid(txtKcr.Text, 21, 2))
        SendBuff(22) = CInt("&H" & Mid(txtKcr.Text, 23, 2))
        SendBuff(23) = CInt("&H" & Mid(txtKcr.Text, 25, 2))
        SendBuff(24) = CInt("&H" & Mid(txtKcr.Text, 27, 2))
        SendBuff(25) = CInt("&H" & Mid(txtKcr.Text, 29, 2))
        SendBuff(26) = CInt("&H" & Mid(txtKcr.Text, 31, 2))

        lst_Log.Items.Add("SAM < 00 E2 00 00 16")
        lst_Log.Items.Add("    <<85 03 FF FF 88 00 " _
                                         & Mid(txtKcr.Text, 1, 2) & " " _
                                         & Mid(txtKcr.Text, 3, 2) & " " _
                                         & Mid(txtKcr.Text, 5, 2) & " " _
                                         & Mid(txtKcr.Text, 7, 2) & " " _
                                         & Mid(txtKcr.Text, 9, 2) & " " _
                                         & Mid(txtKcr.Text, 11, 2) & " " _
                                         & Mid(txtKcr.Text, 13, 2) & " " _
                                         & Mid(txtKcr.Text, 15, 2) & " " _
                                         & Mid(txtKcr.Text, 17, 2) & " " _
                                         & Mid(txtKcr.Text, 19, 2))

        lst_Log.Items.Add("    <<" & Mid(txtKcr.Text, 21, 2) & " " _
                                         & Mid(txtKcr.Text, 23, 2) & " " _
                                         & Mid(txtKcr.Text, 25, 2) & " " _
                                         & Mid(txtKcr.Text, 27, 2) & " " _
                                         & Mid(txtKcr.Text, 29, 2) & " " _
                                         & Mid(txtKcr.Text, 31, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 27, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        '6th Master key, key ID=86, key type=03, int/ext authenticate, usage counter = FF FF
        ReDim SendBuff(26)
        ReDim RecvBuff(1)


        SendBuff(0) = &H0
        SendBuff(1) = &HE2
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H16
        SendBuff(5) = &H86 'Key ID
        SendBuff(6) = &H3
        SendBuff(7) = &HFF
        SendBuff(8) = &HFF
        SendBuff(9) = &H88
        SendBuff(10) = &H0
        SendBuff(11) = CInt("&H" & Mid(txtKcf.Text, 1, 2))
        SendBuff(12) = CInt("&H" & Mid(txtKcf.Text, 3, 2))
        SendBuff(13) = CInt("&H" & Mid(txtKcf.Text, 5, 2))
        SendBuff(14) = CInt("&H" & Mid(txtKcf.Text, 7, 2))
        SendBuff(15) = CInt("&H" & Mid(txtKcf.Text, 9, 2))
        SendBuff(16) = CInt("&H" & Mid(txtKcf.Text, 11, 2))
        SendBuff(17) = CInt("&H" & Mid(txtKcf.Text, 13, 2))
        SendBuff(18) = CInt("&H" & Mid(txtKcf.Text, 15, 2))
        SendBuff(19) = CInt("&H" & Mid(txtKcf.Text, 17, 2))
        SendBuff(20) = CInt("&H" & Mid(txtKcf.Text, 19, 2))
        SendBuff(21) = CInt("&H" & Mid(txtKcf.Text, 21, 2))
        SendBuff(22) = CInt("&H" & Mid(txtKcf.Text, 23, 2))
        SendBuff(23) = CInt("&H" & Mid(txtKcf.Text, 25, 2))
        SendBuff(24) = CInt("&H" & Mid(txtKcf.Text, 27, 2))
        SendBuff(25) = CInt("&H" & Mid(txtKcf.Text, 29, 2))
        SendBuff(26) = CInt("&H" & Mid(txtKcf.Text, 31, 2))

        lst_Log.Items.Add("SAM < 00 E2 00 00 16")
        lst_Log.Items.Add("    <<86 03 FF FF 88 00 " _
                                         & Mid(txtKcf.Text, 1, 2) & " " _
                                         & Mid(txtKcf.Text, 3, 2) & " " _
                                         & Mid(txtKcf.Text, 5, 2) & " " _
                                         & Mid(txtKcf.Text, 7, 2) & " " _
                                         & Mid(txtKcf.Text, 9, 2) & " " _
                                         & Mid(txtKcf.Text, 11, 2) & " " _
                                         & Mid(txtKcf.Text, 13, 2) & " " _
                                         & Mid(txtKcf.Text, 15, 2) & " " _
                                         & Mid(txtKcf.Text, 17, 2) & " " _
                                         & Mid(txtKcf.Text, 19, 2))

        lst_Log.Items.Add("    <<" & Mid(txtKcf.Text, 21, 2) & " " _
                                         & Mid(txtKcf.Text, 23, 2) & " " _
                                         & Mid(txtKcf.Text, 25, 2) & " " _
                                         & Mid(txtKcf.Text, 27, 2) & " " _
                                         & Mid(txtKcf.Text, 29, 2) & " " _
                                         & Mid(txtKcf.Text, 31, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 27, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        '7th Master key, key ID=87, key type=03, int/ext authenticate, usage counter = FF FF
        ReDim SendBuff(26)
        ReDim RecvBuff(1)


        SendBuff(0) = &H0
        SendBuff(1) = &HE2
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H16
        SendBuff(5) = &H87 'Key ID
        SendBuff(6) = &H3
        SendBuff(7) = &HFF
        SendBuff(8) = &HFF
        SendBuff(9) = &H88
        SendBuff(10) = &H0
        SendBuff(11) = CInt("&H" & Mid(txtKrd.Text, 1, 2))
        SendBuff(12) = CInt("&H" & Mid(txtKrd.Text, 3, 2))
        SendBuff(13) = CInt("&H" & Mid(txtKrd.Text, 5, 2))
        SendBuff(14) = CInt("&H" & Mid(txtKrd.Text, 7, 2))
        SendBuff(15) = CInt("&H" & Mid(txtKrd.Text, 9, 2))
        SendBuff(16) = CInt("&H" & Mid(txtKrd.Text, 11, 2))
        SendBuff(17) = CInt("&H" & Mid(txtKrd.Text, 13, 2))
        SendBuff(18) = CInt("&H" & Mid(txtKrd.Text, 15, 2))
        SendBuff(19) = CInt("&H" & Mid(txtKrd.Text, 17, 2))
        SendBuff(20) = CInt("&H" & Mid(txtKrd.Text, 19, 2))
        SendBuff(21) = CInt("&H" & Mid(txtKrd.Text, 21, 2))
        SendBuff(22) = CInt("&H" & Mid(txtKrd.Text, 23, 2))
        SendBuff(23) = CInt("&H" & Mid(txtKrd.Text, 25, 2))
        SendBuff(24) = CInt("&H" & Mid(txtKrd.Text, 27, 2))
        SendBuff(25) = CInt("&H" & Mid(txtKrd.Text, 29, 2))
        SendBuff(26) = CInt("&H" & Mid(txtKrd.Text, 31, 2))

        lst_Log.Items.Add("SAM < 00 E2 00 00 16")
        lst_Log.Items.Add("    <<87 03 FF FF 88 00 " _
                                         & Mid(txtKrd.Text, 1, 2) & " " _
                                         & Mid(txtKrd.Text, 3, 2) & " " _
                                         & Mid(txtKrd.Text, 5, 2) & " " _
                                         & Mid(txtKrd.Text, 7, 2) & " " _
                                         & Mid(txtKrd.Text, 9, 2) & " " _
                                         & Mid(txtKrd.Text, 11, 2) & " " _
                                         & Mid(txtKrd.Text, 13, 2) & " " _
                                         & Mid(txtKrd.Text, 15, 2) & " " _
                                         & Mid(txtKrd.Text, 17, 2) & " " _
                                         & Mid(txtKrd.Text, 19, 2))

        lst_Log.Items.Add("    <<" & Mid(txtKrd.Text, 21, 2) & " " _
                                         & Mid(txtKrd.Text, 23, 2) & " " _
                                         & Mid(txtKrd.Text, 25, 2) & " " _
                                         & Mid(txtKrd.Text, 27, 2) & " " _
                                         & Mid(txtKrd.Text, 29, 2) & " " _
                                         & Mid(txtKrd.Text, 31, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 27, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If

    End Sub


    Public Function Send_APDU_SAM(ByRef SendBuff() As Byte, ByVal SendLen As Integer, ByRef RecvLen As Integer, ByRef RecvBuff() As Byte) As Boolean

        'Send APDU to SAM Reader

        G_ioRequest.dwProtocol = G_Protocol
        G_ioRequest.cbPciLength = Len(G_ioRequest)

        G_retCode = ModWinsCard.SCardTransmit(G_hCardSAM, _
                                              G_ioRequest, _
                                              SendBuff(0), _
                                              SendLen, _
                                              G_ioRequest, _
                                              RecvBuff(0), _
                                              RecvLen)

        If G_retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            lst_Log.Items.Add("SAM SCardTransmit Error: " & GetScardErrMsg(G_retCode))
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            Exit Function
        End If

        Send_APDU_SAM = True

    End Function


    Private Sub cmd_GenerateKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_GenerateKey.Click

        'Variable Declaration
        Dim SendBuff() As Byte
        Dim RecvBuff() As Byte
        Dim RecvLen As Integer
        Dim SN, GeneratedKey As String
        Dim i As Integer

        'Select Issuer DF
        ReDim SendBuff(6)
        ReDim RecvBuff(1)

        SendBuff(0) = &H0
        SendBuff(1) = &HA4
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H2
        SendBuff(5) = &H11
        SendBuff(6) = &H0

        lst_Log.Items.Add("SAM < 00 A4 00 00 02")
        lst_Log.Items.Add("    <<11 00")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 7, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "612D" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        End If

        'Submit Issuer PIN
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H0
        SendBuff(1) = &H20
        SendBuff(2) = &H0
        SendBuff(3) = &H1
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(txtSAMGPIN.Text, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(txtSAMGPIN.Text, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(txtSAMGPIN.Text, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(txtSAMGPIN.Text, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(txtSAMGPIN.Text, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(txtSAMGPIN.Text, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(txtSAMGPIN.Text, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(txtSAMGPIN.Text, 15, 2))

        lst_Log.Items.Add("SAM < 00 20 00 01 08")
        lst_Log.Items.Add("    <<" & Mid(txtSAMGPIN.Text, 1, 2) _
                                 & Mid(txtSAMGPIN.Text, 3, 2) _
                                 & Mid(txtSAMGPIN.Text, 5, 2) _
                                 & Mid(txtSAMGPIN.Text, 7, 2) _
                                 & Mid(txtSAMGPIN.Text, 9, 2) _
                                 & Mid(txtSAMGPIN.Text, 11, 2) _
                                 & Mid(txtSAMGPIN.Text, 13, 2) _
                                 & Mid(txtSAMGPIN.Text, 15, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If

        'Get Card Serial Number
        'Select FF00
        ReDim SendBuff(6)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HA4
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H2
        SendBuff(5) = &HFF
        SendBuff(6) = &H0

        lst_Log.Items.Add("MCU < 80 A4 00 00 02")
        lst_Log.Items.Add("    <<FF 00")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 7, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        End If

        'Read FF 00 to retrieve card serial number
        ReDim SendBuff(4)
        ReDim RecvBuff(9)

        SendBuff(0) = &H80
        SendBuff(1) = &HB2
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8

        lst_Log.Items.Add("MCU < 80 B2 00 00 08")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 10

        If Send_APDU_SLT(SendBuff, 5, RecvLen, RecvBuff) = True Then

            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve Card Serial Number
                SN = ""

                For i = 0 To 7
                    SN = SN & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                lst_Log.Items.Add("MCU >> " & SN)
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                lbl_CardSN.Text = Replace(SN, " ", "")

            End If
        Else
            Exit Sub

        End If


        'Generate Key
        'Generate IC Using 1st SAM Master Key (KeyID=81)
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H88
        SendBuff(2) = &H0
        SendBuff(3) = &H81 'KeyID
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_CardSN.Text, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_CardSN.Text, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_CardSN.Text, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_CardSN.Text, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_CardSN.Text, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_CardSN.Text, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_CardSN.Text, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_CardSN.Text, 15, 2))

        lst_Log.Items.Add("SAM < 80 88 00 81 08")
        lst_Log.Items.Add("    <<" & Mid(lbl_CardSN.Text, 1, 2) _
                                 & Mid(lbl_CardSN.Text, 3, 2) _
                                 & Mid(lbl_CardSN.Text, 5, 2) _
                                 & Mid(lbl_CardSN.Text, 7, 2) _
                                 & Mid(lbl_CardSN.Text, 9, 2) _
                                 & Mid(lbl_CardSN.Text, 11, 2) _
                                 & Mid(lbl_CardSN.Text, 13, 2) _
                                 & Mid(lbl_CardSN.Text, 15, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub

        End If

        'Get Response to Retrieve Generated Key
        ReDim SendBuff(4)
        ReDim RecvBuff(9)

        SendBuff(0) = &H0
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8

        lst_Log.Items.Add("SAM < 00 C0 00 00 08")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 10

        If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then

            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve Generated Key
                GeneratedKey = ""

                For i = 0 To 7
                    GeneratedKey = GeneratedKey & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                lst_Log.Items.Add("SAM >> " & GeneratedKey)
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                lbl_IC.Text = Replace(GeneratedKey, " ", "")

            End If
        Else
            Exit Sub

        End If

        'Generate Card Key (Kc) Using 2nd SAM Master Key (KeyID=82)
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H88
        SendBuff(2) = &H0
        SendBuff(3) = &H82 'KeyID
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_CardSN.Text, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_CardSN.Text, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_CardSN.Text, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_CardSN.Text, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_CardSN.Text, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_CardSN.Text, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_CardSN.Text, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_CardSN.Text, 15, 2))

        lst_Log.Items.Add("SAM < 80 88 00 82 08")
        lst_Log.Items.Add("    <<" & Mid(lbl_CardSN.Text, 1, 2) _
                                 & Mid(lbl_CardSN.Text, 3, 2) _
                                 & Mid(lbl_CardSN.Text, 5, 2) _
                                 & Mid(lbl_CardSN.Text, 7, 2) _
                                 & Mid(lbl_CardSN.Text, 9, 2) _
                                 & Mid(lbl_CardSN.Text, 11, 2) _
                                 & Mid(lbl_CardSN.Text, 13, 2) _
                                 & Mid(lbl_CardSN.Text, 15, 2))
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Get Response to Retrieve Generated Key
        ReDim SendBuff(4)
        ReDim RecvBuff(9)

        SendBuff(0) = &H0
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8

        lst_Log.Items.Add("SAM < 00 C0 00 00 08")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 10

        If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then

            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve Generated Key
                GeneratedKey = ""

                For i = 0 To 7
                    GeneratedKey = GeneratedKey & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                lst_Log.Items.Add("SAM >> " & GeneratedKey)
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                lbl_Kc.Text = Replace(GeneratedKey, " ", "")

            End If

        Else
            Exit Sub

        End If


        'If Algorithm Reference = 3DES then Generate Right Half of Card Key (Kc) Using 2nd SAM Master Key (KeyID=82)
        If G_AlgoRef = 0 Then

            ReDim SendBuff(12)
            ReDim RecvBuff(1)


            SendBuff(0) = &H80
            SendBuff(1) = &H88
            SendBuff(2) = &H0
            SendBuff(3) = &H82 'KeyID
            SendBuff(4) = &H8

            'compliment the card serial number to generate right half key for 3DES algorithm
            SendBuff(5) = (CInt("&H" & Mid(lbl_CardSN.Text, 1, 2)) Xor &HFF)
            SendBuff(6) = (CInt("&H" & Mid(lbl_CardSN.Text, 3, 2)) Xor &HFF)
            SendBuff(7) = (CInt("&H" & Mid(lbl_CardSN.Text, 5, 2)) Xor &HFF)
            SendBuff(8) = (CInt("&H" & Mid(lbl_CardSN.Text, 7, 2)) Xor &HFF)
            SendBuff(9) = (CInt("&H" & Mid(lbl_CardSN.Text, 9, 2)) Xor &HFF)
            SendBuff(10) = (CInt("&H" & Mid(lbl_CardSN.Text, 11, 2)) Xor &HFF)
            SendBuff(11) = (CInt("&H" & Mid(lbl_CardSN.Text, 13, 2)) Xor &HFF)
            SendBuff(12) = (CInt("&H" & Mid(lbl_CardSN.Text, 15, 2)) Xor &HFF)

            lst_Log.Items.Add("SAM < 80 88 00 82 08")
            lst_Log.Items.Add("    <<" & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(5)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(6)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(7)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(8)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(9)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(10)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(11)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(12)), 2))
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1

            RecvLen = 2

            If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Exit Sub
                Else
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                End If
            Else
                Exit Sub
            End If

            'Get Response to Retrieve Generated Key
            ReDim SendBuff(4)
            ReDim RecvBuff(9)

            SendBuff(0) = &H0
            SendBuff(1) = &HC0
            SendBuff(2) = &H0
            SendBuff(3) = &H0
            SendBuff(4) = &H8

            lst_Log.Items.Add("SAM < 00 C0 00 00 08")
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1

            RecvLen = 10

            If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then

                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Exit Sub
                Else
                    'Retrieve Generated Key
                    GeneratedKey = ""

                    For i = 0 To 7
                        GeneratedKey = GeneratedKey & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                    Next i
                    lst_Log.Items.Add("SAM >> " & GeneratedKey)
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                    lbl_r_kc.Text = Replace(GeneratedKey, " ", "")

                End If

            Else
                Exit Sub
            End If

        Else

            lbl_r_kc.Text = ""

        End If


        'Generate Terminal Key (Kt) Using 3rd SAM Master Key (KeyID=83)
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H88
        SendBuff(2) = &H0
        SendBuff(3) = &H83 'KeyID
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_CardSN.Text, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_CardSN.Text, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_CardSN.Text, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_CardSN.Text, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_CardSN.Text, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_CardSN.Text, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_CardSN.Text, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_CardSN.Text, 15, 2))

        lst_Log.Items.Add("SAM < 80 88 00 83 08")
        lst_Log.Items.Add("    <<" & Mid(lbl_CardSN.Text, 1, 2) _
                                 & Mid(lbl_CardSN.Text, 3, 2) _
                                 & Mid(lbl_CardSN.Text, 5, 2) _
                                 & Mid(lbl_CardSN.Text, 7, 2) _
                                 & Mid(lbl_CardSN.Text, 9, 2) _
                                 & Mid(lbl_CardSN.Text, 11, 2) _
                                 & Mid(lbl_CardSN.Text, 13, 2) _
                                 & Mid(lbl_CardSN.Text, 15, 2))
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Get Response to Retrieve Generated Key
        ReDim SendBuff(4)
        ReDim RecvBuff(9)

        SendBuff(0) = &H0
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8

        lst_Log.Items.Add("SAM < 00 C0 00 00 08")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 10

        If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then

            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve Generated Key
                GeneratedKey = ""

                For i = 0 To 7
                    GeneratedKey = GeneratedKey & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                lst_Log.Items.Add("SAM >> " & GeneratedKey)
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                lbl_Kt.Text = Replace(GeneratedKey, " ", "")

            End If
        Else
            Exit Sub

        End If


        'If Algorithm Reference = 3DES then Generate Right Half of Terminal Key (Kt) Using 3rd SAM Master Key (KeyID=83)
        If G_AlgoRef = 0 Then

            ReDim SendBuff(12)
            ReDim RecvBuff(1)

            SendBuff(0) = &H80
            SendBuff(1) = &H88
            SendBuff(2) = &H0
            SendBuff(3) = &H83 'KeyID
            SendBuff(4) = &H8

            'compliment the card serial number to generate right half key for 3DES algorithm
            SendBuff(5) = (CInt("&H" & Mid(lbl_CardSN.Text, 1, 2)) Xor &HFF)
            SendBuff(6) = (CInt("&H" & Mid(lbl_CardSN.Text, 3, 2)) Xor &HFF)
            SendBuff(7) = (CInt("&H" & Mid(lbl_CardSN.Text, 5, 2)) Xor &HFF)
            SendBuff(8) = (CInt("&H" & Mid(lbl_CardSN.Text, 7, 2)) Xor &HFF)
            SendBuff(9) = (CInt("&H" & Mid(lbl_CardSN.Text, 9, 2)) Xor &HFF)
            SendBuff(10) = (CInt("&H" & Mid(lbl_CardSN.Text, 11, 2)) Xor &HFF)
            SendBuff(11) = (CInt("&H" & Mid(lbl_CardSN.Text, 13, 2)) Xor &HFF)
            SendBuff(12) = (CInt("&H" & Mid(lbl_CardSN.Text, 15, 2)) Xor &HFF)

            lst_Log.Items.Add("SAM < 80 88 00 83 08")
            lst_Log.Items.Add("    <<" & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(5)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(6)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(7)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(8)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(9)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(10)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(11)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(12)), 2))
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1

            RecvLen = 2

            If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Exit Sub
                Else
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                End If
            Else
                Exit Sub
            End If


            'Get Response to Retrieve Generated Key
            ReDim SendBuff(4)
            ReDim RecvBuff(9)

            SendBuff(0) = &H0
            SendBuff(1) = &HC0
            SendBuff(2) = &H0
            SendBuff(3) = &H0
            SendBuff(4) = &H8

            lst_Log.Items.Add("SAM < 00 C0 00 00 08")
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1

            RecvLen = 10

            If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then

                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Exit Sub
                Else
                    'Retrieve Generated Key
                    GeneratedKey = ""

                    For i = 0 To 7
                        GeneratedKey = GeneratedKey & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                    Next i
                    lst_Log.Items.Add("SAM >> " & GeneratedKey)
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                    lbl_r_Kt.Text = Replace(GeneratedKey, " ", "")

                End If
            Else
                Exit Sub

            End If
        Else

            lbl_r_Kt.Text = ""
        End If


        'Generate Debit Key (Kd) Using 4th SAM Master Key (KeyID=84)
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H88
        SendBuff(2) = &H0
        SendBuff(3) = &H84 'KeyID
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_CardSN.Text, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_CardSN.Text, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_CardSN.Text, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_CardSN.Text, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_CardSN.Text, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_CardSN.Text, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_CardSN.Text, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_CardSN.Text, 15, 2))

        lst_Log.Items.Add("SAM < 80 88 00 84 08")
        lst_Log.Items.Add("    <<" & Mid(lbl_CardSN.Text, 1, 2) _
                                 & Mid(lbl_CardSN.Text, 3, 2) _
                                 & Mid(lbl_CardSN.Text, 5, 2) _
                                 & Mid(lbl_CardSN.Text, 7, 2) _
                                 & Mid(lbl_CardSN.Text, 9, 2) _
                                 & Mid(lbl_CardSN.Text, 11, 2) _
                                 & Mid(lbl_CardSN.Text, 13, 2) _
                                 & Mid(lbl_CardSN.Text, 15, 2))
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Get Response to Retrieve Generated Key
        ReDim SendBuff(4)
        ReDim RecvBuff(9)

        SendBuff(0) = &H0
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8

        lst_Log.Items.Add("SAM < 00 C0 00 00 08")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 10

        If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then

            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve Generated Key
                GeneratedKey = ""

                For i = 0 To 7
                    GeneratedKey = GeneratedKey & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                lst_Log.Items.Add("SAM >> " & GeneratedKey)
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                lbl_Kd.Text = Replace(GeneratedKey, " ", "")

            End If
        Else
            Exit Sub

        End If


        'If Algorithm Reference = 3DES then Generate Right Half of Debit Key (Kd) Using 4th SAM Master Key (KeyID=84)
        If G_AlgoRef = 0 Then

            ReDim SendBuff(12)
            ReDim RecvBuff(1)

            SendBuff(0) = &H80
            SendBuff(1) = &H88
            SendBuff(2) = &H0
            SendBuff(3) = &H84 'KeyID
            SendBuff(4) = &H8

            'compliment the card serial number to generate right half key for 3DES algorithm
            SendBuff(5) = (CInt("&H" & Mid(lbl_CardSN.Text, 1, 2)) Xor &HFF)
            SendBuff(6) = (CInt("&H" & Mid(lbl_CardSN.Text, 3, 2)) Xor &HFF)
            SendBuff(7) = (CInt("&H" & Mid(lbl_CardSN.Text, 5, 2)) Xor &HFF)
            SendBuff(8) = (CInt("&H" & Mid(lbl_CardSN.Text, 7, 2)) Xor &HFF)
            SendBuff(9) = (CInt("&H" & Mid(lbl_CardSN.Text, 9, 2)) Xor &HFF)
            SendBuff(10) = (CInt("&H" & Mid(lbl_CardSN.Text, 11, 2)) Xor &HFF)
            SendBuff(11) = (CInt("&H" & Mid(lbl_CardSN.Text, 13, 2)) Xor &HFF)
            SendBuff(12) = (CInt("&H" & Mid(lbl_CardSN.Text, 15, 2)) Xor &HFF)

            lst_Log.Items.Add("SAM < 80 88 00 84 08")
            lst_Log.Items.Add("    <<" & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(5)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(6)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(7)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(8)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(9)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(10)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(11)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(12)), 2))
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1

            RecvLen = 2

            If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Exit Sub
                Else
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                End If
            Else
                Exit Sub
            End If

            'Get Response to Retrieve Generated Key
            ReDim SendBuff(4)
            ReDim RecvBuff(9)

            SendBuff(0) = &H0
            SendBuff(1) = &HC0
            SendBuff(2) = &H0
            SendBuff(3) = &H0
            SendBuff(4) = &H8

            lst_Log.Items.Add("SAM < 00 C0 00 00 08")
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1

            RecvLen = 10

            If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then

                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Exit Sub
                Else
                    'Retrieve Generated Key
                    GeneratedKey = ""

                    For i = 0 To 7
                        GeneratedKey = GeneratedKey & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                    Next i
                    lst_Log.Items.Add("SAM >> " & GeneratedKey)
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                    lbl_r_Kd.Text = Replace(GeneratedKey, " ", "")

                End If

            Else
                Exit Sub
            End If

        Else

            lbl_r_Kd.Text = ""

        End If


        'Generate Credit Key (Kcr) Using 5th SAM Master Key (KeyID=85)
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H88
        SendBuff(2) = &H0
        SendBuff(3) = &H85 'KeyID
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_CardSN.Text, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_CardSN.Text, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_CardSN.Text, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_CardSN.Text, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_CardSN.Text, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_CardSN.Text, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_CardSN.Text, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_CardSN.Text, 15, 2))

        lst_Log.Items.Add("SAM < 80 88 00 85 08")
        lst_Log.Items.Add("    <<" & Mid(lbl_CardSN.Text, 1, 2) _
                                 & Mid(lbl_CardSN.Text, 3, 2) _
                                 & Mid(lbl_CardSN.Text, 5, 2) _
                                 & Mid(lbl_CardSN.Text, 7, 2) _
                                 & Mid(lbl_CardSN.Text, 9, 2) _
                                 & Mid(lbl_CardSN.Text, 11, 2) _
                                 & Mid(lbl_CardSN.Text, 13, 2) _
                                 & Mid(lbl_CardSN.Text, 15, 2))
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Get Response to Retrieve Generated Key
        ReDim SendBuff(4)
        ReDim RecvBuff(9)

        SendBuff(0) = &H0
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8

        lst_Log.Items.Add("SAM < 00 C0 00 00 08")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 10

        If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then

            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve Generated Key
                GeneratedKey = ""

                For i = 0 To 7
                    GeneratedKey = GeneratedKey & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                lst_Log.Items.Add("SAM >> " & GeneratedKey)
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                lbl_Kcr.Text = Replace(GeneratedKey, " ", "")

            End If
        Else
            Exit Sub

        End If

        'If Algorithm Reference = 3DES then Generate Right Half of Credit Key (Kcr) Using 5th SAM Master Key (KeyID=85)
        If G_AlgoRef = 0 Then

            ReDim SendBuff(12)
            ReDim RecvBuff(1)

            SendBuff(0) = &H80
            SendBuff(1) = &H88
            SendBuff(2) = &H0
            SendBuff(3) = &H85 'KeyID
            SendBuff(4) = &H8

            'compliment the card serial number to generate right half key for 3DES algorithm
            SendBuff(5) = (CInt("&H" & Mid(lbl_CardSN.Text, 1, 2)) Xor &HFF)
            SendBuff(6) = (CInt("&H" & Mid(lbl_CardSN.Text, 3, 2)) Xor &HFF)
            SendBuff(7) = (CInt("&H" & Mid(lbl_CardSN.Text, 5, 2)) Xor &HFF)
            SendBuff(8) = (CInt("&H" & Mid(lbl_CardSN.Text, 7, 2)) Xor &HFF)
            SendBuff(9) = (CInt("&H" & Mid(lbl_CardSN.Text, 9, 2)) Xor &HFF)
            SendBuff(10) = (CInt("&H" & Mid(lbl_CardSN.Text, 11, 2)) Xor &HFF)
            SendBuff(11) = (CInt("&H" & Mid(lbl_CardSN.Text, 13, 2)) Xor &HFF)
            SendBuff(12) = (CInt("&H" & Mid(lbl_CardSN.Text, 15, 2)) Xor &HFF)

            lst_Log.Items.Add("SAM < 80 88 00 85 08")
            lst_Log.Items.Add("    <<" & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(5)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(6)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(7)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(8)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(9)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(10)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(11)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(12)), 2))
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1

            RecvLen = 2

            If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Exit Sub
                Else
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                End If
            Else
                Exit Sub
            End If


            'Get Response to Retrieve Generated Key
            ReDim SendBuff(4)
            ReDim RecvBuff(9)

            SendBuff(0) = &H0
            SendBuff(1) = &HC0
            SendBuff(2) = &H0
            SendBuff(3) = &H0
            SendBuff(4) = &H8

            lst_Log.Items.Add("SAM < 00 C0 00 00 08")
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1

            RecvLen = 10

            If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then

                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Exit Sub
                Else
                    'Retrieve Generated Key
                    GeneratedKey = ""

                    For i = 0 To 7
                        GeneratedKey = GeneratedKey & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                    Next i
                    lst_Log.Items.Add("SAM >> " & GeneratedKey)
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                    lbl_r_Kcr.Text = Replace(GeneratedKey, " ", "")

                End If

            Else
                Exit Sub
            End If

        Else

            lbl_r_Kcr.Text = ""

        End If


        'Generate Certify Key (Kcf) Using 6th SAM Master Key (KeyID=86)
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H88
        SendBuff(2) = &H0
        SendBuff(3) = &H86 'KeyID
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_CardSN.Text, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_CardSN.Text, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_CardSN.Text, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_CardSN.Text, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_CardSN.Text, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_CardSN.Text, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_CardSN.Text, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_CardSN.Text, 15, 2))

        lst_Log.Items.Add("SAM < 80 88 00 86 08 ")
        lst_Log.Items.Add("    <<" & Mid(lbl_CardSN.Text, 1, 2) _
                                 & Mid(lbl_CardSN.Text, 3, 2) _
                                 & Mid(lbl_CardSN.Text, 5, 2) _
                                 & Mid(lbl_CardSN.Text, 7, 2) _
                                 & Mid(lbl_CardSN.Text, 9, 2) _
                                 & Mid(lbl_CardSN.Text, 11, 2) _
                                 & Mid(lbl_CardSN.Text, 13, 2) _
                                 & Mid(lbl_CardSN.Text, 15, 2))
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Get Response to Retrieve Generated Key
        ReDim SendBuff(4)
        ReDim RecvBuff(9)

        SendBuff(0) = &H0
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8

        lst_Log.Items.Add("SAM < 00 C0 00 00 08")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 10

        If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then

            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve Generated Key
                GeneratedKey = ""

                For i = 0 To 7
                    GeneratedKey = GeneratedKey & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                lst_Log.Items.Add("SAM >> " & GeneratedKey)
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                lbl_Kcf.Text = Replace(GeneratedKey, " ", "")

            End If
        Else
            Exit Sub

        End If

        'If Algorithm Reference = 3DES then Generate Right Half of Certify Key (Kcf) Using 6th SAM Master Key (KeyID=86)
        If G_AlgoRef = 0 Then

            ReDim SendBuff(12)
            ReDim RecvBuff(1)

            SendBuff(0) = &H80
            SendBuff(1) = &H88
            SendBuff(2) = &H0
            SendBuff(3) = &H86 'KeyID
            SendBuff(4) = &H8

            'compliment the card serial number to generate right half key for 3DES algorithm
            SendBuff(5) = (CInt("&H" & Mid(lbl_CardSN.Text, 1, 2)) Xor &HFF)
            SendBuff(6) = (CInt("&H" & Mid(lbl_CardSN.Text, 3, 2)) Xor &HFF)
            SendBuff(7) = (CInt("&H" & Mid(lbl_CardSN.Text, 5, 2)) Xor &HFF)
            SendBuff(8) = (CInt("&H" & Mid(lbl_CardSN.Text, 7, 2)) Xor &HFF)
            SendBuff(9) = (CInt("&H" & Mid(lbl_CardSN.Text, 9, 2)) Xor &HFF)
            SendBuff(10) = (CInt("&H" & Mid(lbl_CardSN.Text, 11, 2)) Xor &HFF)
            SendBuff(11) = (CInt("&H" & Mid(lbl_CardSN.Text, 13, 2)) Xor &HFF)
            SendBuff(12) = (CInt("&H" & Mid(lbl_CardSN.Text, 15, 2)) Xor &HFF)

            lst_Log.Items.Add("SAM < 80 88 00 86 08")
            lst_Log.Items.Add("    <<" & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(5)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(6)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(7)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(8)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(9)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(10)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(11)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(12)), 2))
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1

            RecvLen = 2

            If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Exit Sub
                Else
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                End If
            Else
                Exit Sub
            End If


            'Get Response to Retrieve Generated Key
            ReDim SendBuff(4)
            ReDim RecvBuff(9)

            SendBuff(0) = &H0
            SendBuff(1) = &HC0
            SendBuff(2) = &H0
            SendBuff(3) = &H0
            SendBuff(4) = &H8

            lst_Log.Items.Add("SAM < 00 C0 00 00 08")
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1

            RecvLen = 10

            If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then

                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Exit Sub
                Else
                    'Retrieve Generated Key
                    GeneratedKey = ""

                    For i = 0 To 7
                        GeneratedKey = GeneratedKey & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                    Next i
                    lst_Log.Items.Add("SAM >> " & GeneratedKey)
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                    lbl_r_Kcf.Text = Replace(GeneratedKey, " ", "")

                End If

            Else
                Exit Sub
            End If

        Else

            lbl_r_Kcf.Text = ""

        End If

        'Generate Revoke Debit Key (Krd) Using 7th SAM Master Key (KeyID=87)
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H88
        SendBuff(2) = &H0
        SendBuff(3) = &H87 'KeyID
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_CardSN.Text, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_CardSN.Text, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_CardSN.Text, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_CardSN.Text, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_CardSN.Text, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_CardSN.Text, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_CardSN.Text, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_CardSN.Text, 15, 2))

        lst_Log.Items.Add("SAM < 80 88 00 87 08")
        lst_Log.Items.Add("    <<" & Mid(lbl_CardSN.Text, 1, 2) _
                                 & Mid(lbl_CardSN.Text, 3, 2) _
                                 & Mid(lbl_CardSN.Text, 5, 2) _
                                 & Mid(lbl_CardSN.Text, 7, 2) _
                                 & Mid(lbl_CardSN.Text, 9, 2) _
                                 & Mid(lbl_CardSN.Text, 11, 2) _
                                 & Mid(lbl_CardSN.Text, 13, 2) _
                                 & Mid(lbl_CardSN.Text, 15, 2))
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Get Response to Retrieve Generated Key
        ReDim SendBuff(4)
        ReDim RecvBuff(9)

        SendBuff(0) = &H0
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8

        lst_Log.Items.Add("SAM < 00 C0 00 00 08")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 10

        If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then

            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve Generated Key
                GeneratedKey = ""

                For i = 0 To 7
                    GeneratedKey = GeneratedKey & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                lst_Log.Items.Add("SAM >> " & GeneratedKey)
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                lbl_Krd.Text = Replace(GeneratedKey, " ", "")

            End If
        Else
            Exit Sub

        End If


        'If Algorithm Reference = 3DES then Generate Right Half of Revoke Debit Key (Krd) Using 7th SAM Master Key (KeyID=87)
        If G_AlgoRef = 0 Then

            ReDim SendBuff(12)
            ReDim RecvBuff(1)

            SendBuff(0) = &H80
            SendBuff(1) = &H88
            SendBuff(2) = &H0
            SendBuff(3) = &H87 'KeyID
            SendBuff(4) = &H8

            'compliment the card serial number to generate right half key for 3DES algorithm
            SendBuff(5) = (CInt("&H" & Mid(lbl_CardSN.Text, 1, 2)) Xor &HFF)
            SendBuff(6) = (CInt("&H" & Mid(lbl_CardSN.Text, 3, 2)) Xor &HFF)
            SendBuff(7) = (CInt("&H" & Mid(lbl_CardSN.Text, 5, 2)) Xor &HFF)
            SendBuff(8) = (CInt("&H" & Mid(lbl_CardSN.Text, 7, 2)) Xor &HFF)
            SendBuff(9) = (CInt("&H" & Mid(lbl_CardSN.Text, 9, 2)) Xor &HFF)
            SendBuff(10) = (CInt("&H" & Mid(lbl_CardSN.Text, 11, 2)) Xor &HFF)
            SendBuff(11) = (CInt("&H" & Mid(lbl_CardSN.Text, 13, 2)) Xor &HFF)
            SendBuff(12) = (CInt("&H" & Mid(lbl_CardSN.Text, 15, 2)) Xor &HFF)

            lst_Log.Items.Add("SAM < 80 88 00 87 08")
            lst_Log.Items.Add("    <<" & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(5)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(6)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(7)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(8)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(9)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(10)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(11)), 2) & " " _
                                     & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(12)), 2))
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1

            RecvLen = 2

            If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Exit Sub
                Else
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                End If
            Else
                Exit Sub
            End If


            'Get Response to Retrieve Generated Key
            ReDim SendBuff(4)
            ReDim RecvBuff(9)

            SendBuff(0) = &H0
            SendBuff(1) = &HC0
            SendBuff(2) = &H0
            SendBuff(3) = &H0
            SendBuff(4) = &H8

            lst_Log.Items.Add("SAM < 00 C0 00 00 08")
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1

            RecvLen = 10

            If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then

                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Exit Sub
                Else
                    'Retrieve Generated Key
                    GeneratedKey = ""

                    For i = 0 To 7
                        GeneratedKey = GeneratedKey & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                    Next i
                    lst_Log.Items.Add("SAM >> " & GeneratedKey)
                    lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                    lbl_r_Krd.Text = Replace(GeneratedKey, " ", "")

                End If

            Else
                Exit Sub
            End If

        Else

            lbl_r_Krd.Text = ""

        End If

    End Sub


    Public Function Send_APDU_SLT(ByRef SendBuff() As Byte, ByVal SendLen As Integer, ByRef RecvLen As Integer, ByRef RecvBuff() As Byte) As Boolean

        'Send APDU to Slot Reader

        G_ioRequest.dwProtocol = G_Protocol
        G_ioRequest.cbPciLength = Len(G_ioRequest)

        G_retCode = ModWinsCard.SCardTransmit(G_hCard, _
                                  G_ioRequest, _
                                  SendBuff(0), _
                                  SendLen, _
                                  G_ioRequest, _
                                  RecvBuff(0), _
                                  RecvLen)

        If G_retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            lst_Log.Items.Add("SCardTransmit Error: " & GetScardErrMsg(G_retCode))
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            Exit Function
        End If

        Send_APDU_SLT = True

    End Function


    Private Sub cmd_SaveKeys_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_SaveKeys.Click

        'Variable Declaration
        Dim SendBuff() As Byte
        Dim RecvBuff() As Byte
        Dim RecvLen As Integer

        'Check User PIN
        If Not CheckInput(txt_PIN, 16) Then Exit Sub

        'Update Personalization File (FF02)

        'Select FF02
        ReDim SendBuff(6)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HA4
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H2
        SendBuff(5) = &HFF
        SendBuff(6) = &H2

        lst_Log.Items.Add("MCU < 80 A4 00 00 02")
        lst_Log.Items.Add("    <<FF 02")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 7, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Submit Default IC
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H20
        SendBuff(2) = &H7
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = &H41
        SendBuff(6) = &H43
        SendBuff(7) = &H4F
        SendBuff(8) = &H53
        SendBuff(9) = &H54
        SendBuff(10) = &H45
        SendBuff(11) = &H53
        SendBuff(12) = &H54

        lst_Log.Items.Add("MCU < 80 20 07 00 08")
        lst_Log.Items.Add("    <<41 43 4F 53 54 45 53 54")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Update FF02 record 0 
        ReDim SendBuff(8)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H4

        If G_AlgoRef = 0 Then
            SendBuff(5) = &HFF ' INQ_AUT, TRNS_AUT, REV_DEB, DEB_PIN, DEB_MAC, Account, 3-DES, PIN_ALT Enabled
        Else
            SendBuff(5) = &HFD ' INQ_AUT, TRNS_AUT, REV_DEB, DEB_PIN, DEB_MAC, Account, 3-DES, PIN_ALT Enabled
        End If

        SendBuff(6) = &H40 'PIN was encrypted and the PIN should be submitted in the submit code command must be encrypted with the current session key.
        SendBuff(7) = &H0 'No User File Created
        SendBuff(8) = &H0

        lst_Log.Items.Add("MCU < 80 D2 00 00 04")
        lst_Log.Items.Add("    <<" & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(5)), 2) & " 40 00 00")

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Reset
        cmd_Connect_SLT_Click(sender, e)


        'Update Card Keys (FF03)

        'Select FF03
        ReDim SendBuff(6)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HA4
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H2
        SendBuff(5) = &HFF
        SendBuff(6) = &H3

        lst_Log.Items.Add("MCU < 80 A4 00 00 02")
        lst_Log.Items.Add("    <<FF 03")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 7, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Submit Default IC
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H20
        SendBuff(2) = &H7
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = &H41
        SendBuff(6) = &H43
        SendBuff(7) = &H4F
        SendBuff(8) = &H53
        SendBuff(9) = &H54
        SendBuff(10) = &H45
        SendBuff(11) = &H53
        SendBuff(12) = &H54

        lst_Log.Items.Add("MCU < 80 20 07 00 08")
        lst_Log.Items.Add("    <<41 43 4F 53 54 45 53 54")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Update FF03 record 0 (IC)
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_IC.Text, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_IC.Text, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_IC.Text, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_IC.Text, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_IC.Text, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_IC.Text, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_IC.Text, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_IC.Text, 15, 2))


        lst_Log.Items.Add("MCU < 80 D2 00 00 08")
        lst_Log.Items.Add("    <<" & Mid(lbl_IC.Text, 1, 2) & " " _
                                 & Mid(lbl_IC.Text, 3, 2) & " " _
                                 & Mid(lbl_IC.Text, 5, 2) & " " _
                                 & Mid(lbl_IC.Text, 7, 2) & " " _
                                 & Mid(lbl_IC.Text, 9, 2) & " " _
                                 & Mid(lbl_IC.Text, 11, 2) & " " _
                                 & Mid(lbl_IC.Text, 13, 2) & " " _
                                 & Mid(lbl_IC.Text, 15, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Update FF03 record 1 (PIN)
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &H1
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(txt_PIN.Text, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(txt_PIN.Text, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(txt_PIN.Text, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(txt_PIN.Text, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(txt_PIN.Text, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(txt_PIN.Text, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(txt_PIN.Text, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(txt_PIN.Text, 15, 2))


        lst_Log.Items.Add("MCU < 80 D2 01 00 08")
        lst_Log.Items.Add("    <<" & Mid(txt_PIN.Text, 1, 2) & " " _
                                 & Mid(txt_PIN.Text, 3, 2) & " " _
                                 & Mid(txt_PIN.Text, 5, 2) & " " _
                                 & Mid(txt_PIN.Text, 7, 2) & " " _
                                 & Mid(txt_PIN.Text, 9, 2) & " " _
                                 & Mid(txt_PIN.Text, 11, 2) & " " _
                                 & Mid(txt_PIN.Text, 13, 2) & " " _
                                 & Mid(txt_PIN.Text, 15, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Update FF03 record 2 (Kc)
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &H2
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_Kc.Text, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_Kc.Text, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_Kc.Text, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_Kc.Text, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_Kc.Text, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_Kc.Text, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_Kc.Text, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_Kc.Text, 15, 2))


        lst_Log.Items.Add("MCU < 80 D2 02 00 08")
        lst_Log.Items.Add("    <<" & Mid(lbl_Kc.Text, 1, 2) & " " _
                                 & Mid(lbl_Kc.Text, 3, 2) & " " _
                                 & Mid(lbl_Kc.Text, 5, 2) & " " _
                                 & Mid(lbl_Kc.Text, 7, 2) & " " _
                                 & Mid(lbl_Kc.Text, 9, 2) & " " _
                                 & Mid(lbl_Kc.Text, 11, 2) & " " _
                                 & Mid(lbl_Kc.Text, 13, 2) & " " _
                                 & Mid(lbl_Kc.Text, 15, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        If G_AlgoRef = 0 Then
            'If Algorithm Reference = 3DES Update FF03 record 0x0C Right Half (Kc)
            ReDim SendBuff(12)
            ReDim RecvBuff(1)

            SendBuff(0) = &H80
            SendBuff(1) = &HD2
            SendBuff(2) = &HC
            SendBuff(3) = &H0
            SendBuff(4) = &H8
            SendBuff(5) = CInt("&H" & Mid(lbl_r_kc.Text, 1, 2))
            SendBuff(6) = CInt("&H" & Mid(lbl_r_kc.Text, 3, 2))
            SendBuff(7) = CInt("&H" & Mid(lbl_r_kc.Text, 5, 2))
            SendBuff(8) = CInt("&H" & Mid(lbl_r_kc.Text, 7, 2))
            SendBuff(9) = CInt("&H" & Mid(lbl_r_kc.Text, 9, 2))
            SendBuff(10) = CInt("&H" & Mid(lbl_r_kc.Text, 11, 2))
            SendBuff(11) = CInt("&H" & Mid(lbl_r_kc.Text, 13, 2))
            SendBuff(12) = CInt("&H" & Mid(lbl_r_kc.Text, 15, 2))


            lst_Log.Items.Add("MCU < 80 D2 0C 00 08")
            lst_Log.Items.Add("    <<" & Mid(lbl_r_kc.Text, 1, 2) & " " _
                                     & Mid(lbl_r_kc.Text, 3, 2) & " " _
                                     & Mid(lbl_r_kc.Text, 5, 2) & " " _
                                     & Mid(lbl_r_kc.Text, 7, 2) & " " _
                                     & Mid(lbl_r_kc.Text, 9, 2) & " " _
                                     & Mid(lbl_r_kc.Text, 11, 2) & " " _
                                     & Mid(lbl_r_kc.Text, 13, 2) & " " _
                                     & Mid(lbl_r_kc.Text, 15, 2))

            lst_Log.SelectedIndex = lst_Log.Items.Count - 1

            RecvLen = 2

            If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                    lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Exit Sub
                Else
                    lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                End If
            Else
                Exit Sub
            End If
        End If


        'Update FF03 record 3 (Kt)
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &H3
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_Kt.Text, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_Kt.Text, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_Kt.Text, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_Kt.Text, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_Kt.Text, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_Kt.Text, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_Kt.Text, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_Kt.Text, 15, 2))


        lst_Log.Items.Add("MCU < 80 D2 03 00 08")
        lst_Log.Items.Add("    <<" & Mid(lbl_Kt.Text, 1, 2) & " " _
                                 & Mid(lbl_Kt.Text, 3, 2) & " " _
                                 & Mid(lbl_Kt.Text, 5, 2) & " " _
                                 & Mid(lbl_Kt.Text, 7, 2) & " " _
                                 & Mid(lbl_Kt.Text, 9, 2) & " " _
                                 & Mid(lbl_Kt.Text, 11, 2) & " " _
                                 & Mid(lbl_Kt.Text, 13, 2) & " " _
                                 & Mid(lbl_Kt.Text, 15, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        If G_AlgoRef = 0 Then
            'If Algorithm Reference = 3DES Update FF03 record 0x0D Right Half (Kt)
            ReDim SendBuff(12)
            ReDim RecvBuff(1)

            SendBuff(0) = &H80
            SendBuff(1) = &HD2
            SendBuff(2) = &HD
            SendBuff(3) = &H0
            SendBuff(4) = &H8
            SendBuff(5) = CInt("&H" & Mid(lbl_r_Kt.Text, 1, 2))
            SendBuff(6) = CInt("&H" & Mid(lbl_r_Kt.Text, 3, 2))
            SendBuff(7) = CInt("&H" & Mid(lbl_r_Kt.Text, 5, 2))
            SendBuff(8) = CInt("&H" & Mid(lbl_r_Kt.Text, 7, 2))
            SendBuff(9) = CInt("&H" & Mid(lbl_r_Kt.Text, 9, 2))
            SendBuff(10) = CInt("&H" & Mid(lbl_r_Kt.Text, 11, 2))
            SendBuff(11) = CInt("&H" & Mid(lbl_r_Kt.Text, 13, 2))
            SendBuff(12) = CInt("&H" & Mid(lbl_r_Kt.Text, 15, 2))


            lst_Log.Items.Add("MCU < 80 D2 0D 00 08")
            lst_Log.Items.Add("    <<" & Mid(lbl_r_Kt.Text, 1, 2) & " " _
                                     & Mid(lbl_r_Kt.Text, 3, 2) & " " _
                                     & Mid(lbl_r_Kt.Text, 5, 2) & " " _
                                     & Mid(lbl_r_Kt.Text, 7, 2) & " " _
                                     & Mid(lbl_r_Kt.Text, 9, 2) & " " _
                                     & Mid(lbl_r_Kt.Text, 11, 2) & " " _
                                     & Mid(lbl_r_Kt.Text, 13, 2) & " " _
                                     & Mid(lbl_r_Kt.Text, 15, 2))

            lst_Log.SelectedIndex = lst_Log.Items.Count - 1

            RecvLen = 2

            If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                    lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Exit Sub
                Else
                    lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                End If
            Else
                Exit Sub
            End If
        End If


        'Select FF06
        ReDim SendBuff(6)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HA4
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H2
        SendBuff(5) = &HFF
        SendBuff(6) = &H6

        lst_Log.Items.Add("MCU < 80 A4 00 00 02")
        lst_Log.Items.Add("    <<FF 06")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 7, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Update FF06 record 0 (Kd)
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        If G_AlgoRef = 0 Then
            SendBuff(2) = &H4
        Else
            SendBuff(2) = &H0
        End If
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_Kd.Text, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_Kd.Text, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_Kd.Text, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_Kd.Text, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_Kd.Text, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_Kd.Text, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_Kd.Text, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_Kd.Text, 15, 2))


        lst_Log.Items.Add("MCU < 80 D2 " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 00 08")
        lst_Log.Items.Add("    <<" & Mid(lbl_Kd.Text, 1, 2) & " " _
                                 & Mid(lbl_Kd.Text, 3, 2) & " " _
                                 & Mid(lbl_Kd.Text, 5, 2) & " " _
                                 & Mid(lbl_Kd.Text, 7, 2) & " " _
                                 & Mid(lbl_Kd.Text, 9, 2) & " " _
                                 & Mid(lbl_Kd.Text, 11, 2) & " " _
                                 & Mid(lbl_Kd.Text, 13, 2) & " " _
                                 & Mid(lbl_Kd.Text, 15, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Update FF06 record 1 (Kcr)
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        If G_AlgoRef = 0 Then
            SendBuff(2) = &H5
        Else
            SendBuff(2) = &H1
        End If
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_Kcr.Text, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_Kcr.Text, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_Kcr.Text, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_Kcr.Text, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_Kcr.Text, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_Kcr.Text, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_Kcr.Text, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_Kcr.Text, 15, 2))


        lst_Log.Items.Add("MCU < 80 D2 " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 00 08")
        lst_Log.Items.Add("    <<" & Mid(lbl_Kcr.Text, 1, 2) & " " _
                                 & Mid(lbl_Kcr.Text, 3, 2) & " " _
                                 & Mid(lbl_Kcr.Text, 5, 2) & " " _
                                 & Mid(lbl_Kcr.Text, 7, 2) & " " _
                                 & Mid(lbl_Kcr.Text, 9, 2) & " " _
                                 & Mid(lbl_Kcr.Text, 11, 2) & " " _
                                 & Mid(lbl_Kcr.Text, 13, 2) & " " _
                                 & Mid(lbl_Kcr.Text, 15, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Update FF06 record 2 (Kcf)
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        If G_AlgoRef = 0 Then
            SendBuff(2) = &H6
        Else
            SendBuff(2) = &H2
        End If
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_Kcf.Text, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_Kcf.Text, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_Kcf.Text, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_Kcf.Text, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_Kcf.Text, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_Kcf.Text, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_Kcf.Text, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_Kcf.Text, 15, 2))


        lst_Log.Items.Add("MCU < 80 D2 " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 00 08")
        lst_Log.Items.Add("    <<" & Mid(lbl_Kcf.Text, 1, 2) & " " _
                                 & Mid(lbl_Kcf.Text, 3, 2) & " " _
                                 & Mid(lbl_Kcf.Text, 5, 2) & " " _
                                 & Mid(lbl_Kcf.Text, 7, 2) & " " _
                                 & Mid(lbl_Kcf.Text, 9, 2) & " " _
                                 & Mid(lbl_Kcf.Text, 11, 2) & " " _
                                 & Mid(lbl_Kcf.Text, 13, 2) & " " _
                                 & Mid(lbl_Kcf.Text, 15, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Update FF06 record 3 (Krd)
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        If G_AlgoRef = 0 Then
            SendBuff(2) = &H7
        Else
            SendBuff(2) = &H3
        End If
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_Krd.Text, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_Krd.Text, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_Krd.Text, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_Krd.Text, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_Krd.Text, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_Krd.Text, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_Krd.Text, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_Krd.Text, 15, 2))


        lst_Log.Items.Add("MCU < 80 D2 " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 00 08")
        lst_Log.Items.Add("    <<" & Mid(lbl_Krd.Text, 1, 2) & " " _
                                 & Mid(lbl_Krd.Text, 3, 2) & " " _
                                 & Mid(lbl_Krd.Text, 5, 2) & " " _
                                 & Mid(lbl_Krd.Text, 7, 2) & " " _
                                 & Mid(lbl_Krd.Text, 9, 2) & " " _
                                 & Mid(lbl_Krd.Text, 11, 2) & " " _
                                 & Mid(lbl_Krd.Text, 13, 2) & " " _
                                 & Mid(lbl_Krd.Text, 15, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'If Algorithm Reference = 3DES then update Right Half of the Keys
        If G_AlgoRef = 0 Then


            'Update FF06 record 0 (Kd) right half
            ReDim SendBuff(12)
            ReDim RecvBuff(1)

            SendBuff(0) = &H80
            SendBuff(1) = &HD2
            SendBuff(2) = &H0
            SendBuff(3) = &H0
            SendBuff(4) = &H8
            SendBuff(5) = CInt("&H" & Mid(lbl_r_Kd.Text, 1, 2))
            SendBuff(6) = CInt("&H" & Mid(lbl_r_Kd.Text, 3, 2))
            SendBuff(7) = CInt("&H" & Mid(lbl_r_Kd.Text, 5, 2))
            SendBuff(8) = CInt("&H" & Mid(lbl_r_Kd.Text, 7, 2))
            SendBuff(9) = CInt("&H" & Mid(lbl_r_Kd.Text, 9, 2))
            SendBuff(10) = CInt("&H" & Mid(lbl_r_Kd.Text, 11, 2))
            SendBuff(11) = CInt("&H" & Mid(lbl_r_Kd.Text, 13, 2))
            SendBuff(12) = CInt("&H" & Mid(lbl_r_Kd.Text, 15, 2))


            lst_Log.Items.Add("MCU < 80 D2 00 00 08")
            lst_Log.Items.Add("    <<" & Mid(lbl_r_Kd.Text, 1, 2) & " " _
                                     & Mid(lbl_r_Kd.Text, 3, 2) & " " _
                                     & Mid(lbl_r_Kd.Text, 5, 2) & " " _
                                     & Mid(lbl_r_Kd.Text, 7, 2) & " " _
                                     & Mid(lbl_r_Kd.Text, 9, 2) & " " _
                                     & Mid(lbl_r_Kd.Text, 11, 2) & " " _
                                     & Mid(lbl_r_Kd.Text, 13, 2) & " " _
                                     & Mid(lbl_r_Kd.Text, 15, 2))

            lst_Log.SelectedIndex = lst_Log.Items.Count - 1

            RecvLen = 2

            If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                    lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Exit Sub
                Else
                    lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                End If
            Else
                Exit Sub
            End If


            'Update FF06 record 1 (Kcr) right half
            ReDim SendBuff(12)
            ReDim RecvBuff(1)

            SendBuff(0) = &H80
            SendBuff(1) = &HD2
            SendBuff(2) = &H1
            SendBuff(3) = &H0
            SendBuff(4) = &H8
            SendBuff(5) = CInt("&H" & Mid(lbl_r_Kcr.Text, 1, 2))
            SendBuff(6) = CInt("&H" & Mid(lbl_r_Kcr.Text, 3, 2))
            SendBuff(7) = CInt("&H" & Mid(lbl_r_Kcr.Text, 5, 2))
            SendBuff(8) = CInt("&H" & Mid(lbl_r_Kcr.Text, 7, 2))
            SendBuff(9) = CInt("&H" & Mid(lbl_r_Kcr.Text, 9, 2))
            SendBuff(10) = CInt("&H" & Mid(lbl_r_Kcr.Text, 11, 2))
            SendBuff(11) = CInt("&H" & Mid(lbl_r_Kcr.Text, 13, 2))
            SendBuff(12) = CInt("&H" & Mid(lbl_r_Kcr.Text, 15, 2))


            lst_Log.Items.Add("MCU < 80 D2 01 00 08")
            lst_Log.Items.Add("    <<" & Mid(lbl_r_Kcr.Text, 1, 2) & " " _
                                     & Mid(lbl_r_Kcr.Text, 3, 2) & " " _
                                     & Mid(lbl_r_Kcr.Text, 5, 2) & " " _
                                     & Mid(lbl_r_Kcr.Text, 7, 2) & " " _
                                     & Mid(lbl_r_Kcr.Text, 9, 2) & " " _
                                     & Mid(lbl_r_Kcr.Text, 11, 2) & " " _
                                     & Mid(lbl_r_Kcr.Text, 13, 2) & " " _
                                     & Mid(lbl_r_Kcr.Text, 15, 2))

            lst_Log.SelectedIndex = lst_Log.Items.Count - 1

            RecvLen = 2

            If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                    lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Exit Sub
                Else
                    lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                End If
            Else
                Exit Sub
            End If


            'Update FF06 record 2 (Kcf) right half
            ReDim SendBuff(12)
            ReDim RecvBuff(1)

            SendBuff(0) = &H80
            SendBuff(1) = &HD2
            SendBuff(2) = &H2
            SendBuff(3) = &H0
            SendBuff(4) = &H8
            SendBuff(5) = CInt("&H" & Mid(lbl_r_Kcf.Text, 1, 2))
            SendBuff(6) = CInt("&H" & Mid(lbl_r_Kcf.Text, 3, 2))
            SendBuff(7) = CInt("&H" & Mid(lbl_r_Kcf.Text, 5, 2))
            SendBuff(8) = CInt("&H" & Mid(lbl_r_Kcf.Text, 7, 2))
            SendBuff(9) = CInt("&H" & Mid(lbl_r_Kcf.Text, 9, 2))
            SendBuff(10) = CInt("&H" & Mid(lbl_r_Kcf.Text, 11, 2))
            SendBuff(11) = CInt("&H" & Mid(lbl_r_Kcf.Text, 13, 2))
            SendBuff(12) = CInt("&H" & Mid(lbl_r_Kcf.Text, 15, 2))


            lst_Log.Items.Add("MCU < 80 D2 02 00 08")
            lst_Log.Items.Add("    <<" & Mid(lbl_r_Kcf.Text, 1, 2) & " " _
                                     & Mid(lbl_r_Kcf.Text, 3, 2) & " " _
                                     & Mid(lbl_r_Kcf.Text, 5, 2) & " " _
                                     & Mid(lbl_r_Kcf.Text, 7, 2) & " " _
                                     & Mid(lbl_r_Kcf.Text, 9, 2) & " " _
                                     & Mid(lbl_r_Kcf.Text, 11, 2) & " " _
                                     & Mid(lbl_r_Kcf.Text, 13, 2) & " " _
                                     & Mid(lbl_r_Kcf.Text, 15, 2))

            lst_Log.SelectedIndex = lst_Log.Items.Count - 1

            RecvLen = 2

            If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                    lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Exit Sub
                Else
                    lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                End If
            Else
                Exit Sub
            End If


            'Update FF06 record 3 (Krd) right half
            ReDim SendBuff(12)
            ReDim RecvBuff(1)

            SendBuff(0) = &H80
            SendBuff(1) = &HD2
            SendBuff(2) = &H3
            SendBuff(3) = &H0
            SendBuff(4) = &H8
            SendBuff(5) = CInt("&H" & Mid(lbl_r_Krd.Text, 1, 2))
            SendBuff(6) = CInt("&H" & Mid(lbl_r_Krd.Text, 3, 2))
            SendBuff(7) = CInt("&H" & Mid(lbl_r_Krd.Text, 5, 2))
            SendBuff(8) = CInt("&H" & Mid(lbl_r_Krd.Text, 7, 2))
            SendBuff(9) = CInt("&H" & Mid(lbl_r_Krd.Text, 9, 2))
            SendBuff(10) = CInt("&H" & Mid(lbl_r_Krd.Text, 11, 2))
            SendBuff(11) = CInt("&H" & Mid(lbl_r_Krd.Text, 13, 2))
            SendBuff(12) = CInt("&H" & Mid(lbl_r_Krd.Text, 15, 2))

            lst_Log.Items.Add("MCU < 80 D2 03 00 08")
            lst_Log.Items.Add("    <<" & Mid(lbl_r_Krd.Text, 1, 2) & " " _
                                      & Mid(lbl_r_Krd.Text, 3, 2) & " " _
                                      & Mid(lbl_r_Krd.Text, 5, 2) & " " _
                                      & Mid(lbl_r_Krd.Text, 7, 2) & " " _
                                      & Mid(lbl_r_Krd.Text, 9, 2) & " " _
                                      & Mid(lbl_r_Krd.Text, 11, 2) & " " _
                                      & Mid(lbl_r_Krd.Text, 13, 2) & " " _
                                      & Mid(lbl_r_Krd.Text, 15, 2))

            lst_Log.SelectedIndex = lst_Log.Items.Count - 1

            RecvLen = 2

            If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                    lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Exit Sub
                Else
                    lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                End If
            Else
                Exit Sub
            End If

        End If

        'Select FF05
        ReDim SendBuff(6)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HA4
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H2
        SendBuff(5) = &HFF
        SendBuff(6) = &H5

        lst_Log.Items.Add("MCU < 80 A4 00 00 02")
        lst_Log.Items.Add("    <<FF 05")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 7, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Initialize FF05 Account File
        ReDim SendBuff(8)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H4
        SendBuff(5) = &H0
        SendBuff(6) = &H0
        SendBuff(7) = &H0
        SendBuff(8) = &H0


        lst_Log.Items.Add("MCU < 80 D2 " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 00 04")
        lst_Log.Items.Add("    <<00 00 00 00")

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &H1
        SendBuff(3) = &H0
        SendBuff(4) = &H4
        SendBuff(5) = &H0
        SendBuff(6) = &H0
        SendBuff(7) = &H1
        SendBuff(8) = &H0

        lst_Log.Items.Add("MCU < 80 D2 " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 00 04")
        lst_Log.Items.Add("    <<00 00 01 00")

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &H2
        SendBuff(3) = &H0
        SendBuff(4) = &H4
        SendBuff(5) = &H0
        SendBuff(6) = &H0
        SendBuff(7) = &H0
        SendBuff(8) = &H0

        lst_Log.Items.Add("MCU < 80 D2 " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 00 04")
        lst_Log.Items.Add("    <<00 00 00 00")

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &H3
        SendBuff(3) = &H0
        SendBuff(4) = &H4
        SendBuff(5) = &H0
        SendBuff(6) = &H0
        SendBuff(7) = &H1
        SendBuff(8) = &H0

        lst_Log.Items.Add("MCU < 80 D2 " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 00 04")
        lst_Log.Items.Add("    <<00 00 01 00")

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        'Set Max Balance to 98 96 7F = 9,999,999
        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &H4
        SendBuff(3) = &H0
        SendBuff(4) = &H4
        SendBuff(5) = &H98
        SendBuff(6) = &H96
        SendBuff(7) = &H7F
        SendBuff(8) = &H0

        lst_Log.Items.Add("MCU < 80 D2 " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 00 04")
        lst_Log.Items.Add("    <<98 96 7F 00")

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &H5
        SendBuff(3) = &H0
        SendBuff(4) = &H4
        SendBuff(5) = &H0
        SendBuff(6) = &H0
        SendBuff(7) = &H0
        SendBuff(8) = &H0

        lst_Log.Items.Add("MCU < 80 D2 " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 00 04")
        lst_Log.Items.Add("    <<00 00 00 00")

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &H6
        SendBuff(3) = &H0
        SendBuff(4) = &H4
        SendBuff(5) = &H0
        SendBuff(6) = &H0
        SendBuff(7) = &H0
        SendBuff(8) = &H0

        lst_Log.Items.Add("MCU < 80 D2 " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 00 04")
        lst_Log.Items.Add("    <<00 00 00 00")

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If


        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &H7
        SendBuff(3) = &H0
        SendBuff(4) = &H4
        SendBuff(5) = &H0
        SendBuff(6) = &H0
        SendBuff(7) = &H0
        SendBuff(8) = &H0

        lst_Log.Items.Add("MCU < 80 D2 " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 00 04")
        lst_Log.Items.Add("    <<00 00 00 00")

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If

    End Sub


    Public Function CheckInput(ByRef TextInput As TextBox, Optional ByVal ValidLen As Byte = 32) As Boolean

        'Check if null and check length if correct
        If TextInput.Text = "" Then
            lst_Log.Items.Add("Input Required")
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            TextInput.Focus()
            Exit Function
        End If

        If Len(TextInput.Text) <> ValidLen Then
            lst_Log.Items.Add("Invalid Input Length")
            lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            TextInput.Focus()
            Exit Function
        End If

        CheckInput = True

    End Function


    Private Sub rb_DES_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rb_DES.Click

        'Set Algo Ref to DES
        G_AlgoRef = 1

    End Sub


    Private Sub rb_3DES_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rb_3DES.Click

        'Set Algo Ref to 3DES
        G_AlgoRef = 0

    End Sub


    Public Function KeyVerify(ByVal key As Integer) As Boolean

        '=========================================
        '  Routine for Verifying the Inputed  
        '  Character 0-9 and A-F
        '=========================================

        If key >= 48 And key <= 57 Then
            KeyVerify = False
        ElseIf key >= 65 And key <= 70 Then
            KeyVerify = False
        ElseIf key >= 97 And key <= 102 Then
            KeyVerify = False
        ElseIf key = 8 Then
            KeyVerify = False
        Else
            KeyVerify = True
        End If

    End Function


    Private Sub txtSAMGPIN_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSAMGPIN.KeyPress
        'Verify Input
        e.Handled = KeyVerify(Asc(e.KeyChar))
    End Sub


    Private Sub txt_IC_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_IC.KeyPress
        'Verify Input
        e.Handled = KeyVerify(Asc(e.KeyChar))
    End Sub


    Private Sub txtKc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKc.KeyPress
        'Verify Input
        e.Handled = KeyVerify(Asc(e.KeyChar))
    End Sub


    Private Sub txtKt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKt.KeyPress
        'Verify Input
        e.Handled = KeyVerify(Asc(e.KeyChar))
    End Sub


    Private Sub txtKd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKd.KeyPress
        'Verify Input
        e.Handled = KeyVerify(Asc(e.KeyChar))
    End Sub


    Private Sub txtKcr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKcr.KeyPress
        'Verify Input
        e.Handled = KeyVerify(Asc(e.KeyChar))
    End Sub


    Private Sub txtKcf_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKcf.KeyPress
        'Verify Input
        e.Handled = KeyVerify(Asc(e.KeyChar))
    End Sub


    Private Sub txtKrd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKrd.KeyPress
        'Verify Input
        e.Handled = KeyVerify(Asc(e.KeyChar))
    End Sub


    Private Sub txt_PIN_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_PIN.KeyPress
        'Verify Input
        e.Handled = KeyVerify(Asc(e.KeyChar))
    End Sub


    Private Sub cmbSAM_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSAM.SelectedIndexChanged
        'Gets the selected SAM reader
        G_List_SAM = cmbSAM.SelectedItem
    End Sub

   
    
    Private Sub cmbSLT_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSLT.SelectedIndexChanged
        'Gets the selected reader
        G_List_SLT = cmbSLT.SelectedItem
    End Sub

End Class
