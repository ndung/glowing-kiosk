'===================================================================================================
'
'   Project Name :  SAM_Sample_Usage
'
'   Company      :  Advanced Card Systems LTD.
'
'   Author       :  Aileen Grace L. Sarte
'
'   Date         :  February 6, 2007
'
'   Description  :  This sample shows how ACOS SAM is use for Mutual Authentication and to perform
'                   encrypted PIN submission. And secure E-Purse Function.
'
'   Initial Step :  1.  Press List Readers.
'                   2.  Choose the SAM reader and ACOS card reader.
'                   3.  Press Connect.
'                   4.  Select Algorithm Reference to use (DES/3DES)
'                   5.  Enter SAM Global PIN. (PIN used in KeyManagement sample, SAM Initialization)
'                   6.  Press Mutual Authentication.
'                   7.  Enter ACOS Card PIN. (PIN used in KeyManagement sample, ACOS Card Initialization)
'                   8.  Press Submit PIN.
'                   9.  If you don't want to change your current PIN go to step 10.
'                       To changed current PIN, enter the desired new PIN and press Change PIN
'                   10. To check current card balance (e-purse) press Inquire Account.
'                   11. To credit amount to the card e-purse enter the amount to credit and press Credit.
'                   12. To dedit amount to the card e-purse enter the amount to dedit and press Dedit.
'
'   NOTE:
'                   Please note that this sample program assumes that the SAM and ACOS card were already
'                   initialized using KeyManagement Sample program.
'
'   Revision     :
'
'               Name                    Date            Brief Description of Revision
'
'=====================================================================================================

Public Class Form1
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_ListReaders As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbSLT As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSAM As System.Windows.Forms.ComboBox
    Friend WithEvents cmd_Connect As System.Windows.Forms.Button
    Friend WithEvents rb_DES As System.Windows.Forms.RadioButton
    Friend WithEvents rb_3DES As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSAMGPIN As System.Windows.Forms.TextBox
    Friend WithEvents cmd_MutualAuth As System.Windows.Forms.Button
    Friend WithEvents txt_PIN As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmd_SubmitPIN As System.Windows.Forms.Button
    Friend WithEvents txt_New_PIN As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmd_Change_PIN As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtMaxBalance As System.Windows.Forms.TextBox
    Friend WithEvents txtInqAmount As System.Windows.Forms.TextBox
    Friend WithEvents cmdInqAccount As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmdCredit As System.Windows.Forms.Button
    Friend WithEvents txtCreditAmount As System.Windows.Forms.TextBox
    Friend WithEvents cmdDebit As System.Windows.Forms.Button
    Friend WithEvents txtDebitAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lst_Log As System.Windows.Forms.ListBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txt_New_PIN = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmd_Change_PIN = New System.Windows.Forms.Button
        Me.cmd_SubmitPIN = New System.Windows.Forms.Button
        Me.txt_PIN = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmd_MutualAuth = New System.Windows.Forms.Button
        Me.txtSAMGPIN = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.rb_3DES = New System.Windows.Forms.RadioButton
        Me.rb_DES = New System.Windows.Forms.RadioButton
        Me.cmd_Connect = New System.Windows.Forms.Button
        Me.cmbSAM = New System.Windows.Forms.ComboBox
        Me.cmbSLT = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmd_ListReaders = New System.Windows.Forms.Button
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmdDebit = New System.Windows.Forms.Button
        Me.txtDebitAmount = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.cmdCredit = New System.Windows.Forms.Button
        Me.txtCreditAmount = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.cmdInqAccount = New System.Windows.Forms.Button
        Me.txtInqAmount = New System.Windows.Forms.TextBox
        Me.txtMaxBalance = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
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
        Me.TabControl1.Location = New System.Drawing.Point(8, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(254, 503)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(246, 477)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Security"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txt_New_PIN)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmd_Change_PIN)
        Me.GroupBox1.Controls.Add(Me.cmd_SubmitPIN)
        Me.GroupBox1.Controls.Add(Me.txt_PIN)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmd_MutualAuth)
        Me.GroupBox1.Controls.Add(Me.txtSAMGPIN)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.rb_3DES)
        Me.GroupBox1.Controls.Add(Me.rb_DES)
        Me.GroupBox1.Controls.Add(Me.cmd_Connect)
        Me.GroupBox1.Controls.Add(Me.cmbSAM)
        Me.GroupBox1.Controls.Add(Me.cmbSLT)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmd_ListReaders)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(232, 464)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txt_New_PIN
        '
        Me.txt_New_PIN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_New_PIN.Location = New System.Drawing.Point(110, 386)
        Me.txt_New_PIN.MaxLength = 16
        Me.txt_New_PIN.Name = "txt_New_PIN"
        Me.txt_New_PIN.Size = New System.Drawing.Size(109, 20)
        Me.txt_New_PIN.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(15, 391)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 16)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "ACOS New PIN"
        '
        'cmd_Change_PIN
        '
        Me.cmd_Change_PIN.Enabled = False
        Me.cmd_Change_PIN.Location = New System.Drawing.Point(20, 414)
        Me.cmd_Change_PIN.Name = "cmd_Change_PIN"
        Me.cmd_Change_PIN.Size = New System.Drawing.Size(128, 32)
        Me.cmd_Change_PIN.TabIndex = 12
        Me.cmd_Change_PIN.Text = "Change PIN (ciphered)"
        '
        'cmd_SubmitPIN
        '
        Me.cmd_SubmitPIN.Enabled = False
        Me.cmd_SubmitPIN.Location = New System.Drawing.Point(19, 340)
        Me.cmd_SubmitPIN.Name = "cmd_SubmitPIN"
        Me.cmd_SubmitPIN.Size = New System.Drawing.Size(128, 32)
        Me.cmd_SubmitPIN.TabIndex = 10
        Me.cmd_SubmitPIN.Text = "Submit PIN (ciphered)"
        '
        'txt_PIN
        '
        Me.txt_PIN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_PIN.Location = New System.Drawing.Point(110, 313)
        Me.txt_PIN.MaxLength = 16
        Me.txt_PIN.Name = "txt_PIN"
        Me.txt_PIN.Size = New System.Drawing.Size(109, 20)
        Me.txt_PIN.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(15, 316)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 16)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "ACOS Card PIN"
        '
        'cmd_MutualAuth
        '
        Me.cmd_MutualAuth.Enabled = False
        Me.cmd_MutualAuth.Location = New System.Drawing.Point(18, 268)
        Me.cmd_MutualAuth.Name = "cmd_MutualAuth"
        Me.cmd_MutualAuth.Size = New System.Drawing.Size(128, 32)
        Me.cmd_MutualAuth.TabIndex = 8
        Me.cmd_MutualAuth.Text = "Mutual Authentication"
        '
        'txtSAMGPIN
        '
        Me.txtSAMGPIN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSAMGPIN.Location = New System.Drawing.Point(109, 239)
        Me.txtSAMGPIN.MaxLength = 16
        Me.txtSAMGPIN.Name = "txtSAMGPIN"
        Me.txtSAMGPIN.Size = New System.Drawing.Size(109, 20)
        Me.txtSAMGPIN.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(11, 243)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 17)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "SAM GLOBAL PIN"
        '
        'rb_3DES
        '
        Me.rb_3DES.Location = New System.Drawing.Point(87, 202)
        Me.rb_3DES.Name = "rb_3DES"
        Me.rb_3DES.Size = New System.Drawing.Size(57, 24)
        Me.rb_3DES.TabIndex = 6
        Me.rb_3DES.Text = "3 DES"
        '
        'rb_DES
        '
        Me.rb_DES.Location = New System.Drawing.Point(22, 202)
        Me.rb_DES.Name = "rb_DES"
        Me.rb_DES.Size = New System.Drawing.Size(57, 24)
        Me.rb_DES.TabIndex = 5
        Me.rb_DES.Text = "DES"
        '
        'cmd_Connect
        '
        Me.cmd_Connect.Enabled = False
        Me.cmd_Connect.Location = New System.Drawing.Point(19, 159)
        Me.cmd_Connect.Name = "cmd_Connect"
        Me.cmd_Connect.Size = New System.Drawing.Size(128, 32)
        Me.cmd_Connect.TabIndex = 4
        Me.cmd_Connect.Text = "Connect"
        '
        'cmbSAM
        '
        Me.cmbSAM.Location = New System.Drawing.Point(12, 128)
        Me.cmbSAM.Name = "cmbSAM"
        Me.cmbSAM.Size = New System.Drawing.Size(208, 21)
        Me.cmbSAM.TabIndex = 3
        '
        'cmbSLT
        '
        Me.cmbSLT.Location = New System.Drawing.Point(12, 76)
        Me.cmbSLT.Name = "cmbSLT"
        Me.cmbSLT.Size = New System.Drawing.Size(208, 21)
        Me.cmbSLT.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 110)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "SAM Reader"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Card Reader"
        '
        'cmd_ListReaders
        '
        Me.cmd_ListReaders.Location = New System.Drawing.Point(20, 16)
        Me.cmd_ListReaders.Name = "cmd_ListReaders"
        Me.cmd_ListReaders.Size = New System.Drawing.Size(128, 32)
        Me.cmd_ListReaders.TabIndex = 1
        Me.cmd_ListReaders.Text = "List Readers"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(246, 477)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Account"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdDebit)
        Me.GroupBox2.Controls.Add(Me.txtDebitAmount)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.cmdCredit)
        Me.GroupBox2.Controls.Add(Me.txtCreditAmount)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.cmdInqAccount)
        Me.GroupBox2.Controls.Add(Me.txtInqAmount)
        Me.GroupBox2.Controls.Add(Me.txtMaxBalance)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 7)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(232, 464)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'cmdDebit
        '
        Me.cmdDebit.Location = New System.Drawing.Point(17, 402)
        Me.cmdDebit.Name = "cmdDebit"
        Me.cmdDebit.Size = New System.Drawing.Size(128, 32)
        Me.cmdDebit.TabIndex = 18
        Me.cmdDebit.Text = "Debit"
        '
        'txtDebitAmount
        '
        Me.txtDebitAmount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDebitAmount.Location = New System.Drawing.Point(16, 371)
        Me.txtDebitAmount.MaxLength = 7
        Me.txtDebitAmount.Name = "txtDebitAmount"
        Me.txtDebitAmount.Size = New System.Drawing.Size(200, 20)
        Me.txtDebitAmount.TabIndex = 17
        Me.txtDebitAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(16, 352)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(100, 16)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Debit Amount"
        '
        'cmdCredit
        '
        Me.cmdCredit.Location = New System.Drawing.Point(17, 296)
        Me.cmdCredit.Name = "cmdCredit"
        Me.cmdCredit.Size = New System.Drawing.Size(128, 32)
        Me.cmdCredit.TabIndex = 16
        Me.cmdCredit.Text = "Credit"
        '
        'txtCreditAmount
        '
        Me.txtCreditAmount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCreditAmount.Location = New System.Drawing.Point(16, 264)
        Me.txtCreditAmount.MaxLength = 7
        Me.txtCreditAmount.Name = "txtCreditAmount"
        Me.txtCreditAmount.Size = New System.Drawing.Size(200, 20)
        Me.txtCreditAmount.TabIndex = 15
        Me.txtCreditAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(16, 243)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(100, 16)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Credit Amount"
        '
        'cmdInqAccount
        '
        Me.cmdInqAccount.Location = New System.Drawing.Point(16, 154)
        Me.cmdInqAccount.Name = "cmdInqAccount"
        Me.cmdInqAccount.Size = New System.Drawing.Size(128, 32)
        Me.cmdInqAccount.TabIndex = 14
        Me.cmdInqAccount.Text = "Inquire Account"
        '
        'txtInqAmount
        '
        Me.txtInqAmount.Enabled = False
        Me.txtInqAmount.Location = New System.Drawing.Point(16, 123)
        Me.txtInqAmount.Name = "txtInqAmount"
        Me.txtInqAmount.Size = New System.Drawing.Size(200, 20)
        Me.txtInqAmount.TabIndex = 3
        Me.txtInqAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtMaxBalance
        '
        Me.txtMaxBalance.Enabled = False
        Me.txtMaxBalance.Location = New System.Drawing.Point(16, 58)
        Me.txtMaxBalance.Name = "txtMaxBalance"
        Me.txtMaxBalance.Size = New System.Drawing.Size(200, 20)
        Me.txtMaxBalance.TabIndex = 2
        Me.txtMaxBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(16, 103)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(100, 16)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Balance"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(16, 36)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 16)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Max Balance"
        '
        'lst_Log
        '
        Me.lst_Log.Location = New System.Drawing.Point(272, 24)
        Me.lst_Log.Name = "lst_Log"
        Me.lst_Log.Size = New System.Drawing.Size(473, 485)
        Me.lst_Log.TabIndex = 13
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(752, 519)
        Me.Controls.Add(Me.lst_Log)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SAM Sample Usage"
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
    Dim G_List_SAM, G_List_SLT As String


    Private Sub cmd_ListReaders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ListReaders.Click

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

        If (G_retCode <> ModWinsCard.SCARD_S_SUCCESS) Then
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
        LoadListToControl(cmbSLT, sReaderList)

        If cmbSAM.Items.Count > 0 And cmbSLT.Items.Count > 0 Then
            cmbSAM.SelectedIndex = 0
            cmbSLT.SelectedIndex = 0
            cmd_Connect.Enabled = True
        Else
            lst_Log.Items.Add("No Reader Found")
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


    Private Sub cmd_Connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Connect.Click

        'Establish Reader Connection

        'Get Reader Name
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

        cmd_MutualAuth.Enabled = True

    End Sub


    Private Sub cmbSLT_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSLT.SelectedIndexChanged
        'Gets the selected reader
        G_List_SLT = cmbSLT.SelectedItem
    End Sub


    Private Sub cmbSAM_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSAM.SelectedIndexChanged
        'Gets the selected SAM reader
        G_List_SAM = cmbSAM.SelectedItem
    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Initialize object
        rb_DES.Checked = True
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


    Private Sub cmd_MutualAuth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_MutualAuth.Click

        'Variable Declaration
        Dim SendBuff() As Byte
        Dim RecvBuff() As Byte
        Dim RecvLen As Integer
        Dim SN, tempStr As String
        Dim i As Integer

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
        Else
            Exit Sub
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

                SN = Replace(SN, " ", "")

            End If
        Else
            Exit Sub

        End If

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
        Else
            Exit Sub
        End If


        'Submit Issuer PIN (SAM Global PIN)
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

        'Diversify Kc
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H72
        SendBuff(2) = &H4
        SendBuff(3) = &H82
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(SN, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(SN, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(SN, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(SN, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(SN, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(SN, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(SN, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(SN, 15, 2))


        lst_Log.Items.Add("SAM < 80 72 04 82 08")
        lst_Log.Items.Add("    <<" & Mid(SN, 1, 2) & " " _
                                 & Mid(SN, 3, 2) & " " _
                                 & Mid(SN, 5, 2) & " " _
                                 & Mid(SN, 7, 2) & " " _
                                 & Mid(SN, 9, 2) & " " _
                                 & Mid(SN, 11, 2) & " " _
                                 & Mid(SN, 13, 2) & " " _
                                 & Mid(SN, 15, 2))

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

        'Diversify Kt
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H72
        SendBuff(2) = &H3
        SendBuff(3) = &H83
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(SN, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(SN, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(SN, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(SN, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(SN, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(SN, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(SN, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(SN, 15, 2))


        lst_Log.Items.Add("SAM < 80 72 03 83 08")
        lst_Log.Items.Add("    <<" & Mid(SN, 1, 2) & " " _
                                 & Mid(SN, 3, 2) & " " _
                                 & Mid(SN, 5, 2) & " " _
                                 & Mid(SN, 7, 2) & " " _
                                 & Mid(SN, 9, 2) & " " _
                                 & Mid(SN, 11, 2) & " " _
                                 & Mid(SN, 13, 2) & " " _
                                 & Mid(SN, 15, 2))

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

        'Get Challenge
        ReDim SendBuff(4)
        ReDim RecvBuff(9)

        SendBuff(0) = &H80
        SendBuff(1) = &H84
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8

        lst_Log.Items.Add("MCU < 80 84 00 00 08")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 10

        If Send_APDU_SLT(SendBuff, 5, RecvLen, RecvBuff) = True Then

            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve RNDc
                tempStr = ""

                For i = 0 To 7
                    tempStr = tempStr & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i

                lst_Log.Items.Add("MCU >> " & tempStr)
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                tempStr = Replace(tempStr, " ", "")

            End If

        Else
            Exit Sub
        End If

        'Prepare ACOS authentication
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H78

        If rb_DES.Checked = True Then
            SendBuff(2) = &H1
        ElseIf rb_3DES.Checked = True Then
            SendBuff(2) = &H0
        End If

        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(tempStr, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(tempStr, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(tempStr, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(tempStr, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(tempStr, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(tempStr, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(tempStr, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(tempStr, 15, 2))


        lst_Log.Items.Add("SAM < 80 78 " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 00 08")
        lst_Log.Items.Add("    <<" & Mid(tempStr, 1, 2) & " " _
                                 & Mid(tempStr, 3, 2) & " " _
                                 & Mid(tempStr, 5, 2) & " " _
                                 & Mid(tempStr, 7, 2) & " " _
                                 & Mid(tempStr, 9, 2) & " " _
                                 & Mid(tempStr, 11, 2) & " " _
                                 & Mid(tempStr, 13, 2) & " " _
                                 & Mid(tempStr, 15, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6110" Then
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

        'Get Response to get result + RNDt
        ReDim SendBuff(4)
        ReDim RecvBuff(17)

        SendBuff(0) = &H0
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H10

        lst_Log.Items.Add("SAM < 00 C0 00 00 10")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = &H12

        If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then

            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(16)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(17)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(16)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(17)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve Result + RNDt
                tempStr = ""

                For i = 0 To 15
                    tempStr = tempStr & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i

                lst_Log.Items.Add("SAM >> " & tempStr)
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(16)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(17)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                tempStr = Replace(tempStr, " ", "")

            End If

        Else
            Exit Sub
        End If

        'Authenticate
        ReDim SendBuff(20)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H82
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H10
        SendBuff(5) = CInt("&H" & Mid(tempStr, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(tempStr, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(tempStr, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(tempStr, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(tempStr, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(tempStr, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(tempStr, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(tempStr, 15, 2))
        SendBuff(13) = CInt("&H" & Mid(tempStr, 17, 2))
        SendBuff(14) = CInt("&H" & Mid(tempStr, 19, 2))
        SendBuff(15) = CInt("&H" & Mid(tempStr, 21, 2))
        SendBuff(16) = CInt("&H" & Mid(tempStr, 23, 2))
        SendBuff(17) = CInt("&H" & Mid(tempStr, 25, 2))
        SendBuff(18) = CInt("&H" & Mid(tempStr, 27, 2))
        SendBuff(19) = CInt("&H" & Mid(tempStr, 29, 2))
        SendBuff(20) = CInt("&H" & Mid(tempStr, 31, 2))


        lst_Log.Items.Add("MCU < 80 82 00 00 10")
        lst_Log.Items.Add("    <<" & Mid(tempStr, 1, 2) & " " _
                                 & Mid(tempStr, 3, 2) & " " _
                                 & Mid(tempStr, 5, 2) & " " _
                                 & Mid(tempStr, 7, 2) & " " _
                                 & Mid(tempStr, 9, 2) & " " _
                                 & Mid(tempStr, 11, 2) & " " _
                                 & Mid(tempStr, 13, 2) & " " _
                                 & Mid(tempStr, 15, 2) & " " _
                                 & Mid(tempStr, 17, 2) & " " _
                                 & Mid(tempStr, 19, 2) & " " _
                                 & Mid(tempStr, 21, 2) & " " _
                                 & Mid(tempStr, 23, 2) & " " _
                                 & Mid(tempStr, 25, 2) & " " _
                                 & Mid(tempStr, 27, 2) & " " _
                                 & Mid(tempStr, 29, 2) & " " _
                                 & Mid(tempStr, 31, 2))


        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 21, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
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

        'Get Response to get result
        ReDim SendBuff(4)
        ReDim RecvBuff(9)

        SendBuff(0) = &H80
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8

        lst_Log.Items.Add("MCU < 80 C0 00 00 08")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 10

        If Send_APDU_SLT(SendBuff, 5, RecvLen, RecvBuff) = True Then

            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve Result
                tempStr = ""

                For i = 0 To 7
                    tempStr = tempStr & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i

                lst_Log.Items.Add("MCU >> " & tempStr)
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                tempStr = Replace(tempStr, " ", "")

            End If

        Else
            Exit Sub
        End If


        'Verify ACOS Authentication 
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H7A
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(tempStr, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(tempStr, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(tempStr, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(tempStr, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(tempStr, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(tempStr, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(tempStr, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(tempStr, 15, 2))


        lst_Log.Items.Add("SAM < 80 7A 00 00 08")
        lst_Log.Items.Add("    <<" & Mid(tempStr, 1, 2) & " " _
                                 & Mid(tempStr, 3, 2) & " " _
                                 & Mid(tempStr, 5, 2) & " " _
                                 & Mid(tempStr, 7, 2) & " " _
                                 & Mid(tempStr, 9, 2) & " " _
                                 & Mid(tempStr, 11, 2) & " " _
                                 & Mid(tempStr, 13, 2) & " " _
                                 & Mid(tempStr, 15, 2))

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

        cmd_SubmitPIN.Enabled = True

    End Sub


    Private Sub cmd_SubmitPIN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_SubmitPIN.Click

        'Variable Declaration
        Dim SendBuff() As Byte
        Dim RecvBuff() As Byte
        Dim RecvLen As Integer
        Dim SN, tempStr As String
        Dim i As Integer

        'Encrypt PIN
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H74

        If rb_DES.Checked = True Then
            SendBuff(2) = &H1
        ElseIf rb_3DES.Checked = True Then
            SendBuff(2) = &H0
        End If

        SendBuff(3) = &H1
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(txt_PIN.Text, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(txt_PIN.Text, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(txt_PIN.Text, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(txt_PIN.Text, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(txt_PIN.Text, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(txt_PIN.Text, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(txt_PIN.Text, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(txt_PIN.Text, 15, 2))


        lst_Log.Items.Add("SAM < 80 74 " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 01 08")
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


        'Get Response to get encrypted PIN
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
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(16)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(17)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve encrypted PIN
                tempStr = ""

                For i = 0 To 7
                    tempStr = tempStr & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i

                lst_Log.Items.Add("SAM >> " & tempStr)
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                tempStr = Replace(tempStr, " ", "")

            End If

        Else
            Exit Sub
        End If

        'Submit Encrypted PIN
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H20
        SendBuff(2) = &H6  'PIN
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(tempStr, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(tempStr, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(tempStr, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(tempStr, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(tempStr, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(tempStr, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(tempStr, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(tempStr, 15, 2))


        lst_Log.Items.Add("MCU < 80 20 06 00 08 ")
        lst_Log.Items.Add("    <<" & Mid(tempStr, 1, 2) & " " _
                                 & Mid(tempStr, 3, 2) & " " _
                                 & Mid(tempStr, 5, 2) & " " _
                                 & Mid(tempStr, 7, 2) & " " _
                                 & Mid(tempStr, 9, 2) & " " _
                                 & Mid(tempStr, 11, 2) & " " _
                                 & Mid(tempStr, 13, 2) & " " _
                                 & Mid(tempStr, 15, 2))

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

        cmd_Change_PIN.Enabled = True

    End Sub


    Private Sub cmd_Change_PIN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Change_PIN.Click

        'Variable Declaration
        Dim SendBuff() As Byte
        Dim RecvBuff() As Byte
        Dim RecvLen As Integer
        Dim SN, tempStr As String
        Dim i As Integer

        'Decrypt PIN
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H76

        If rb_DES.Checked = True Then
            SendBuff(2) = &H1
        ElseIf rb_3DES.Checked = True Then
            SendBuff(2) = &H0
        End If

        SendBuff(3) = &H1
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(txt_New_PIN.Text, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(txt_New_PIN.Text, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(txt_New_PIN.Text, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(txt_New_PIN.Text, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(txt_New_PIN.Text, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(txt_New_PIN.Text, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(txt_New_PIN.Text, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(txt_New_PIN.Text, 15, 2))


        lst_Log.Items.Add("SAM < 80 76 " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 01 08")
        lst_Log.Items.Add("    <<" & Mid(txt_New_PIN.Text, 1, 2) & " " _
                                 & Mid(txt_New_PIN.Text, 3, 2) & " " _
                                 & Mid(txt_New_PIN.Text, 5, 2) & " " _
                                 & Mid(txt_New_PIN.Text, 7, 2) & " " _
                                 & Mid(txt_New_PIN.Text, 9, 2) & " " _
                                 & Mid(txt_New_PIN.Text, 11, 2) & " " _
                                 & Mid(txt_New_PIN.Text, 13, 2) & " " _
                                 & Mid(txt_New_PIN.Text, 15, 2))

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

        'Get Response to get decrypted PIN
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
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(16)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(17)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve decrypted PIN
                tempStr = ""

                For i = 0 To 7
                    tempStr = tempStr & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i

                lst_Log.Items.Add("SAM >> " & tempStr)
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(9)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                tempStr = Replace(tempStr, " ", "")

            End If

        Else
            Exit Sub
        End If

        'Change PIN
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H24
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(tempStr, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(tempStr, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(tempStr, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(tempStr, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(tempStr, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(tempStr, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(tempStr, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(tempStr, 15, 2))


        lst_Log.Items.Add("MCU < 80 24 00 00 08 ")
        lst_Log.Items.Add("    <<" & Mid(tempStr, 1, 2) & " " _
                                 & Mid(tempStr, 3, 2) & " " _
                                 & Mid(tempStr, 5, 2) & " " _
                                 & Mid(tempStr, 7, 2) & " " _
                                 & Mid(tempStr, 9, 2) & " " _
                                 & Mid(tempStr, 11, 2) & " " _
                                 & Mid(tempStr, 13, 2) & " " _
                                 & Mid(tempStr, 15, 2))

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



    End Sub


    Private Sub cmdInqAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInqAccount.Click

        'Variable Declaration
        Dim SendBuff() As Byte
        Dim RecvBuff() As Byte
        Dim RecvLen As Integer
        Dim SN, tempStr As String
        Dim i As Integer

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
        Else
            Exit Sub
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

                SN = Replace(SN, " ", "")

            End If
        Else
            Exit Sub

        End If

        'Diversify Kcf
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H72
        SendBuff(2) = &H2
        SendBuff(3) = &H86
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(SN, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(SN, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(SN, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(SN, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(SN, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(SN, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(SN, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(SN, 15, 2))


        lst_Log.Items.Add("SAM < 80 72 02 86 08")
        lst_Log.Items.Add("    <<" & Mid(SN, 1, 2) & " " _
                                 & Mid(SN, 3, 2) & " " _
                                 & Mid(SN, 5, 2) & " " _
                                 & Mid(SN, 7, 2) & " " _
                                 & Mid(SN, 9, 2) & " " _
                                 & Mid(SN, 11, 2) & " " _
                                 & Mid(SN, 13, 2) & " " _
                                 & Mid(SN, 15, 2))

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

        'Inquire Account
        ReDim SendBuff(8)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HE4
        SendBuff(2) = &H2
        SendBuff(3) = &H0
        SendBuff(4) = &H4

        '4 bytes reference
        SendBuff(5) = &HAA
        SendBuff(6) = &HBB
        SendBuff(7) = &HCC
        SendBuff(8) = &HDD

        lst_Log.Items.Add("MCU < 80 E4 02 00 04")
        lst_Log.Items.Add("    <<AA BB CC DD")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6119" Then
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

        'Get Response to get result
        ReDim SendBuff(4)
        ReDim RecvBuff(26)

        SendBuff(0) = &H80
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H19

        lst_Log.Items.Add("MCU < 80 C0 00 00 19")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 27

        If Send_APDU_SLT(SendBuff, 5, RecvLen, RecvBuff) = True Then

            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(25)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(26)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(25)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(26)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve Result
                tempStr = ""

                For i = 0 To 24
                    tempStr = tempStr & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i

                lst_Log.Items.Add("MCU >> " & tempStr)
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(25)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(26)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                tempStr = Replace(tempStr, " ", "")

            End If

        Else
            Exit Sub
        End If

        'Verify Inquire Account
        ReDim SendBuff(33)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H7C

        If rb_3DES.Checked = True Then
            SendBuff(2) = &H2
        ElseIf rb_DES.Checked = True Then
            SendBuff(2) = &H3
        End If

        SendBuff(3) = &H0
        SendBuff(4) = &H1D
        'Ref
        SendBuff(5) = &HAA
        SendBuff(6) = &HBB
        SendBuff(7) = &HCC
        SendBuff(8) = &HDD
        'MAC
        SendBuff(9) = CInt("&H" & Mid(tempStr, 1, 2))
        SendBuff(10) = CInt("&H" & Mid(tempStr, 3, 2))
        SendBuff(11) = CInt("&H" & Mid(tempStr, 5, 2))
        SendBuff(12) = CInt("&H" & Mid(tempStr, 7, 2))

        'Transaction Type
        SendBuff(13) = CInt("&H" & Mid(tempStr, 9, 2))

        'Balance
        SendBuff(14) = CInt("&H" & Mid(tempStr, 11, 2))
        SendBuff(15) = CInt("&H" & Mid(tempStr, 13, 2))
        SendBuff(16) = CInt("&H" & Mid(tempStr, 15, 2))

        'ATREF
        SendBuff(17) = CInt("&H" & Mid(tempStr, 17, 2))
        SendBuff(18) = CInt("&H" & Mid(tempStr, 19, 2))
        SendBuff(19) = CInt("&H" & Mid(tempStr, 21, 2))
        SendBuff(20) = CInt("&H" & Mid(tempStr, 23, 2))
        SendBuff(21) = CInt("&H" & Mid(tempStr, 25, 2))
        SendBuff(22) = CInt("&H" & Mid(tempStr, 27, 2))

        'Max Balance
        SendBuff(23) = CInt("&H" & Mid(tempStr, 29, 2))
        SendBuff(24) = CInt("&H" & Mid(tempStr, 31, 2))
        SendBuff(25) = CInt("&H" & Mid(tempStr, 33, 2))

        'TTREFc
        SendBuff(26) = CInt("&H" & Mid(tempStr, 35, 2))
        SendBuff(27) = CInt("&H" & Mid(tempStr, 37, 2))
        SendBuff(28) = CInt("&H" & Mid(tempStr, 39, 2))
        SendBuff(29) = CInt("&H" & Mid(tempStr, 41, 2))

        'TTREFd
        SendBuff(30) = CInt("&H" & Mid(tempStr, 43, 2))
        SendBuff(31) = CInt("&H" & Mid(tempStr, 45, 2))
        SendBuff(32) = CInt("&H" & Mid(tempStr, 47, 2))
        SendBuff(33) = CInt("&H" & Mid(tempStr, 49, 2))


        lst_Log.Items.Add("SAM < 80 7C " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 00 1D")
        lst_Log.Items.Add("    <<AA BB CC DD " & Mid(tempStr, 1, 2) & " " _
                                 & Mid(tempStr, 3, 2) & " " _
                                 & Mid(tempStr, 5, 2) & " " _
                                 & Mid(tempStr, 7, 2) & " " _
                                 & Mid(tempStr, 9, 2) & " " _
                                 & Mid(tempStr, 11, 2) & " " _
                                 & Mid(tempStr, 13, 2) & " " _
                                 & Mid(tempStr, 15, 2) & " " _
                                 & Mid(tempStr, 17, 2) & " " _
                                 & Mid(tempStr, 19, 2) & " " _
                                 & Mid(tempStr, 21, 2) & " " _
                                 & Mid(tempStr, 23, 2))


        lst_Log.Items.Add("    <<" & Mid(tempStr, 25, 2) & " " _
                                 & Mid(tempStr, 27, 2) & " " _
                                 & Mid(tempStr, 29, 2) & " " _
                                 & Mid(tempStr, 31, 2) & " " _
                                 & Mid(tempStr, 33, 2) & " " _
                                 & Mid(tempStr, 35, 2) & " " _
                                 & Mid(tempStr, 37, 2) & " " _
                                 & Mid(tempStr, 39, 2) & " " _
                                 & Mid(tempStr, 41, 2) & " " _
                                 & Mid(tempStr, 43, 2) & " " _
                                 & Mid(tempStr, 45, 2) & " " _
                                 & Mid(tempStr, 47, 2) & " " _
                                 & Mid(tempStr, 49, 2))


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
        Else
            Exit Sub
        End If

        txtMaxBalance.Text = Get_Balance(CInt("&H" & Mid(tempStr, 29, 2)), CInt("&H" & Mid(tempStr, 31, 2)), CInt("&H" & Mid(tempStr, 33, 2)))

        txtInqAmount.Text = Get_Balance(CInt("&H" & Mid(tempStr, 11, 2)), CInt("&H" & Mid(tempStr, 13, 2)), CInt("&H" & Mid(tempStr, 15, 2)))

    End Sub


    Private Function Get_Balance(ByVal Data1 As Byte, ByVal Data2 As Byte, ByVal Data3 As Byte) As String

        Dim TotalBalance As Integer

        'Get Total Balance
        TotalBalance = Data1 * 65536

        TotalBalance = TotalBalance + (Data2 * CLng(256))

        TotalBalance = TotalBalance + Data3

        Get_Balance = CStr(TotalBalance)

    End Function


    Private Sub cmdCredit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCredit.Click

        'Variable Declaration
        Dim SendBuff() As Byte
        Dim RecvBuff() As Byte
        Dim RecvLen As Integer
        Dim SN, tempStr As String
        Dim i As Integer

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
        Else
            Exit Sub
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

                SN = Replace(SN, " ", "")

            End If
        Else
            Exit Sub

        End If


        'Diversify Kcr
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H72
        SendBuff(2) = &H2
        SendBuff(3) = &H85
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(SN, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(SN, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(SN, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(SN, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(SN, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(SN, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(SN, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(SN, 15, 2))


        lst_Log.Items.Add("SAM < 80 72 02 85 08")
        lst_Log.Items.Add("    <<" & Mid(SN, 1, 2) & " " _
                                 & Mid(SN, 3, 2) & " " _
                                 & Mid(SN, 5, 2) & " " _
                                 & Mid(SN, 7, 2) & " " _
                                 & Mid(SN, 9, 2) & " " _
                                 & Mid(SN, 11, 2) & " " _
                                 & Mid(SN, 13, 2) & " " _
                                 & Mid(SN, 15, 2))

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

        'Inquire Account
        ReDim SendBuff(8)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HE4
        SendBuff(2) = &H1
        SendBuff(3) = &H0
        SendBuff(4) = &H4

        '4 bytes reference
        SendBuff(5) = &HAA
        SendBuff(6) = &HBB
        SendBuff(7) = &HCC
        SendBuff(8) = &HDD

        lst_Log.Items.Add("MCU < 80 E4 01 00 04")
        lst_Log.Items.Add("    <<AA BB CC DD")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6119" Then
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

        'Get Response to get result
        ReDim SendBuff(4)
        ReDim RecvBuff(26)

        SendBuff(0) = &H80
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H19

        lst_Log.Items.Add("MCU < 80 C0 00 00 19")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 27

        If Send_APDU_SLT(SendBuff, 5, RecvLen, RecvBuff) = True Then

            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(25)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(26)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(25)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(26)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve Result
                tempStr = ""

                For i = 0 To 24
                    tempStr = tempStr & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i

                lst_Log.Items.Add("MCU >> " & tempStr)
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(25)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(26)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                tempStr = Replace(tempStr, " ", "")

            End If

        Else
            Exit Sub
        End If

        'Verify Inquire Account
        ReDim SendBuff(33)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H7C

        If rb_3DES.Checked = True Then
            SendBuff(2) = &H2
        ElseIf rb_DES.Checked = True Then
            SendBuff(2) = &H3
        End If

        SendBuff(3) = &H0
        SendBuff(4) = &H1D
        'Ref
        SendBuff(5) = &HAA
        SendBuff(6) = &HBB
        SendBuff(7) = &HCC
        SendBuff(8) = &HDD
        'MAC
        SendBuff(9) = CInt("&H" & Mid(tempStr, 1, 2))
        SendBuff(10) = CInt("&H" & Mid(tempStr, 3, 2))
        SendBuff(11) = CInt("&H" & Mid(tempStr, 5, 2))
        SendBuff(12) = CInt("&H" & Mid(tempStr, 7, 2))

        'Transaction Type
        SendBuff(13) = CInt("&H" & Mid(tempStr, 9, 2))

        'Balance
        SendBuff(14) = CInt("&H" & Mid(tempStr, 11, 2))
        SendBuff(15) = CInt("&H" & Mid(tempStr, 13, 2))
        SendBuff(16) = CInt("&H" & Mid(tempStr, 15, 2))

        'ATREF
        SendBuff(17) = CInt("&H" & Mid(tempStr, 17, 2))
        SendBuff(18) = CInt("&H" & Mid(tempStr, 19, 2))
        SendBuff(19) = CInt("&H" & Mid(tempStr, 21, 2))
        SendBuff(20) = CInt("&H" & Mid(tempStr, 23, 2))
        SendBuff(21) = CInt("&H" & Mid(tempStr, 25, 2))
        SendBuff(22) = CInt("&H" & Mid(tempStr, 27, 2))

        'Max Balance
        SendBuff(23) = CInt("&H" & Mid(tempStr, 29, 2))
        SendBuff(24) = CInt("&H" & Mid(tempStr, 31, 2))
        SendBuff(25) = CInt("&H" & Mid(tempStr, 33, 2))

        'TTREFc
        SendBuff(26) = CInt("&H" & Mid(tempStr, 35, 2))
        SendBuff(27) = CInt("&H" & Mid(tempStr, 37, 2))
        SendBuff(28) = CInt("&H" & Mid(tempStr, 39, 2))
        SendBuff(29) = CInt("&H" & Mid(tempStr, 41, 2))

        'TTREFd
        SendBuff(30) = CInt("&H" & Mid(tempStr, 43, 2))
        SendBuff(31) = CInt("&H" & Mid(tempStr, 45, 2))
        SendBuff(32) = CInt("&H" & Mid(tempStr, 47, 2))
        SendBuff(33) = CInt("&H" & Mid(tempStr, 49, 2))


        lst_Log.Items.Add("SAM < 80 7C " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 00 1D")
        lst_Log.Items.Add("    <<AA BB CC DD " & Mid(tempStr, 1, 2) & " " _
                                 & Mid(tempStr, 3, 2) & " " _
                                 & Mid(tempStr, 5, 2) & " " _
                                 & Mid(tempStr, 7, 2) & " " _
                                 & Mid(tempStr, 9, 2) & " " _
                                 & Mid(tempStr, 11, 2) & " " _
                                 & Mid(tempStr, 13, 2) & " " _
                                 & Mid(tempStr, 15, 2) & " " _
                                 & Mid(tempStr, 17, 2) & " " _
                                 & Mid(tempStr, 19, 2) & " " _
                                 & Mid(tempStr, 21, 2) & " " _
                                 & Mid(tempStr, 23, 2))


        lst_Log.Items.Add("    <<" & Mid(tempStr, 25, 2) & " " _
                                 & Mid(tempStr, 27, 2) & " " _
                                 & Mid(tempStr, 29, 2) & " " _
                                 & Mid(tempStr, 31, 2) & " " _
                                 & Mid(tempStr, 33, 2) & " " _
                                 & Mid(tempStr, 35, 2) & " " _
                                 & Mid(tempStr, 37, 2) & " " _
                                 & Mid(tempStr, 39, 2) & " " _
                                 & Mid(tempStr, 41, 2) & " " _
                                 & Mid(tempStr, 43, 2) & " " _
                                 & Mid(tempStr, 45, 2) & " " _
                                 & Mid(tempStr, 47, 2) & " " _
                                 & Mid(tempStr, 49, 2))


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
        Else
            Exit Sub
        End If

        txtMaxBalance.Text = Get_Balance(CInt("&H" & Mid(tempStr, 29, 2)), CInt("&H" & Mid(tempStr, 31, 2)), CInt("&H" & Mid(tempStr, 33, 2)))

        txtInqAmount.Text = Get_Balance(CInt("&H" & Mid(tempStr, 11, 2)), CInt("&H" & Mid(tempStr, 13, 2)), CInt("&H" & Mid(tempStr, 15, 2)))

        'Prepare ACOS Transaction
        ReDim SendBuff(17)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H7E

        If rb_3DES.Checked = True Then
            SendBuff(2) = &H2
        ElseIf rb_DES.Checked = True Then
            SendBuff(2) = &H3
        End If

        SendBuff(3) = &HE2
        SendBuff(4) = &HD

        'Amount to Credit
        SendBuff(5) = CLng(txtCreditAmount.Text) \ 65536
        SendBuff(6) = (CLng(txtCreditAmount.Text) \ 256) Mod 65536 Mod 256
        SendBuff(7) = CLng(txtCreditAmount.Text) Mod 256

        'TTREFd
        SendBuff(8) = CInt("&H" & Mid(tempStr, 43, 2))
        SendBuff(9) = CInt("&H" & Mid(tempStr, 45, 2))
        SendBuff(10) = CInt("&H" & Mid(tempStr, 47, 2))
        SendBuff(11) = CInt("&H" & Mid(tempStr, 49, 2))

        'ATREF
        SendBuff(12) = CInt("&H" & Mid(tempStr, 17, 2))
        SendBuff(13) = CInt("&H" & Mid(tempStr, 19, 2))
        SendBuff(14) = CInt("&H" & Mid(tempStr, 21, 2))
        SendBuff(15) = CInt("&H" & Mid(tempStr, 23, 2))
        SendBuff(16) = CInt("&H" & Mid(tempStr, 25, 2))
        SendBuff(17) = CInt("&H" & Mid(tempStr, 27, 2))



        lst_Log.Items.Add("SAM < 80 7E " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " E2 0D")
        lst_Log.Items.Add("    <<" & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(5)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(6)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(7)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(8)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(9)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(10)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(11)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(12)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(13)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(14)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(15)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(16)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(17)), 2))


        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 18, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "610B" Then
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

        'Get Response to get result
        ReDim SendBuff(4)
        ReDim RecvBuff(12)

        SendBuff(0) = &H0
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &HB

        lst_Log.Items.Add("SAM < 00 C0 00 00 0B")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = &HD

        If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then

            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(11)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(12)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(11)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(12)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve Result
                tempStr = ""

                For i = 0 To 10
                    tempStr = tempStr & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i

                lst_Log.Items.Add("SAM >> " & tempStr)
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(11)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(12)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                tempStr = Replace(tempStr, " ", "")

            End If

        Else
            Exit Sub
        End If

        'Credit
        ReDim SendBuff(15)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HE2
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &HB
        SendBuff(5) = CInt("&H" & Mid(tempStr, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(tempStr, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(tempStr, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(tempStr, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(tempStr, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(tempStr, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(tempStr, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(tempStr, 15, 2))
        SendBuff(13) = CInt("&H" & Mid(tempStr, 17, 2))
        SendBuff(14) = CInt("&H" & Mid(tempStr, 19, 2))
        SendBuff(15) = CInt("&H" & Mid(tempStr, 21, 2))


        lst_Log.Items.Add("MCU < 80 E2 00 00 0B ")
        lst_Log.Items.Add("    <<" & Mid(tempStr, 1, 2) & " " _
                                 & Mid(tempStr, 3, 2) & " " _
                                 & Mid(tempStr, 5, 2) & " " _
                                 & Mid(tempStr, 7, 2) & " " _
                                 & Mid(tempStr, 9, 2) & " " _
                                 & Mid(tempStr, 11, 2) & " " _
                                 & Mid(tempStr, 13, 2) & " " _
                                 & Mid(tempStr, 15, 2) & " " _
                                 & Mid(tempStr, 17, 2) & " " _
                                 & Mid(tempStr, 19, 2) & " " _
                                 & Mid(tempStr, 21, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 16, RecvLen, RecvBuff) = True Then
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

        'Perform Verify Inquire Account w/ Credit Key and new ammount

        'Inquire Account
        ReDim SendBuff(8)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HE4
        SendBuff(2) = &H1
        SendBuff(3) = &H0
        SendBuff(4) = &H4

        '4 bytes reference
        SendBuff(5) = &HAA
        SendBuff(6) = &HBB
        SendBuff(7) = &HCC
        SendBuff(8) = &HDD

        lst_Log.Items.Add("MCU < 80 E4 01 00 04")
        lst_Log.Items.Add("    <<AA BB CC DD")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6119" Then
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

        'Get Response to get result
        ReDim SendBuff(4)
        ReDim RecvBuff(26)

        SendBuff(0) = &H80
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H19

        lst_Log.Items.Add("MCU < 80 C0 00 00 19")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 27

        If Send_APDU_SLT(SendBuff, 5, RecvLen, RecvBuff) = True Then

            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(25)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(26)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(25)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(26)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve Result
                tempStr = ""

                For i = 0 To 24
                    tempStr = tempStr & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i

                lst_Log.Items.Add("MCU >> " & tempStr)
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(25)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(26)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                tempStr = Replace(tempStr, " ", "")

            End If

        Else
            Exit Sub
        End If

        'Verify Inquire Account
        ReDim SendBuff(33)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H7C

        If rb_3DES.Checked = True Then
            SendBuff(2) = &H2
        ElseIf rb_DES.Checked = True Then
            SendBuff(2) = &H3
        End If

        SendBuff(3) = &H0
        SendBuff(4) = &H1D
        'Ref
        SendBuff(5) = &HAA
        SendBuff(6) = &HBB
        SendBuff(7) = &HCC
        SendBuff(8) = &HDD
        'MAC
        SendBuff(9) = CInt("&H" & Mid(tempStr, 1, 2))
        SendBuff(10) = CInt("&H" & Mid(tempStr, 3, 2))
        SendBuff(11) = CInt("&H" & Mid(tempStr, 5, 2))
        SendBuff(12) = CInt("&H" & Mid(tempStr, 7, 2))

        'Transaction Type
        SendBuff(13) = CInt("&H" & Mid(tempStr, 9, 2))

        'Balance
        SendBuff(14) = CInt("&H" & Mid(tempStr, 11, 2))
        SendBuff(15) = CInt("&H" & Mid(tempStr, 13, 2))
        SendBuff(16) = CInt("&H" & Mid(tempStr, 15, 2))

        'ATREF
        SendBuff(17) = CInt("&H" & Mid(tempStr, 17, 2))
        SendBuff(18) = CInt("&H" & Mid(tempStr, 19, 2))
        SendBuff(19) = CInt("&H" & Mid(tempStr, 21, 2))
        SendBuff(20) = CInt("&H" & Mid(tempStr, 23, 2))
        SendBuff(21) = CInt("&H" & Mid(tempStr, 25, 2))
        SendBuff(22) = CInt("&H" & Mid(tempStr, 27, 2))

        'Max Balance
        SendBuff(23) = CInt("&H" & Mid(tempStr, 29, 2))
        SendBuff(24) = CInt("&H" & Mid(tempStr, 31, 2))
        SendBuff(25) = CInt("&H" & Mid(tempStr, 33, 2))

        'TTREFc
        SendBuff(26) = CInt("&H" & Mid(tempStr, 35, 2))
        SendBuff(27) = CInt("&H" & Mid(tempStr, 37, 2))
        SendBuff(28) = CInt("&H" & Mid(tempStr, 39, 2))
        SendBuff(29) = CInt("&H" & Mid(tempStr, 41, 2))

        'TTREFd
        SendBuff(30) = CInt("&H" & Mid(tempStr, 43, 2))
        SendBuff(31) = CInt("&H" & Mid(tempStr, 45, 2))
        SendBuff(32) = CInt("&H" & Mid(tempStr, 47, 2))
        SendBuff(33) = CInt("&H" & Mid(tempStr, 49, 2))


        lst_Log.Items.Add("SAM < 80 7C " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 00 1D")
        lst_Log.Items.Add("    <<AA BB CC DD " & Mid(tempStr, 1, 2) & " " _
                                 & Mid(tempStr, 3, 2) & " " _
                                 & Mid(tempStr, 5, 2) & " " _
                                 & Mid(tempStr, 7, 2) & " " _
                                 & Mid(tempStr, 9, 2) & " " _
                                 & Mid(tempStr, 11, 2) & " " _
                                 & Mid(tempStr, 13, 2) & " " _
                                 & Mid(tempStr, 15, 2) & " " _
                                 & Mid(tempStr, 17, 2) & " " _
                                 & Mid(tempStr, 19, 2) & " " _
                                 & Mid(tempStr, 21, 2) & " " _
                                 & Mid(tempStr, 23, 2))


        lst_Log.Items.Add("    <<" & Mid(tempStr, 25, 2) & " " _
                                 & Mid(tempStr, 27, 2) & " " _
                                 & Mid(tempStr, 29, 2) & " " _
                                 & Mid(tempStr, 31, 2) & " " _
                                 & Mid(tempStr, 33, 2) & " " _
                                 & Mid(tempStr, 35, 2) & " " _
                                 & Mid(tempStr, 37, 2) & " " _
                                 & Mid(tempStr, 39, 2) & " " _
                                 & Mid(tempStr, 41, 2) & " " _
                                 & Mid(tempStr, 43, 2) & " " _
                                 & Mid(tempStr, 45, 2) & " " _
                                 & Mid(tempStr, 47, 2) & " " _
                                 & Mid(tempStr, 49, 2))


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
        Else
            Exit Sub
        End If

    End Sub


    Private Sub cmdDebit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDebit.Click

        'Variable Declaration

        Dim SendBuff() As Byte
        Dim RecvBuff() As Byte
        Dim RecvLen As Integer
        Dim SN, tempStr, tempStr2 As String
        Dim i As Integer
        Dim Bal, NewBal As Integer

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
        Else
            Exit Sub
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

                SN = Replace(SN, " ", "")

            End If
        Else
            Exit Sub

        End If

        'Diversify Kd
        ReDim SendBuff(12)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H72
        SendBuff(2) = &H2
        SendBuff(3) = &H84
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(SN, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(SN, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(SN, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(SN, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(SN, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(SN, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(SN, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(SN, 15, 2))


        lst_Log.Items.Add("SAM < 80 72 02 84 08")
        lst_Log.Items.Add("    <<" & Mid(SN, 1, 2) & " " _
                                 & Mid(SN, 3, 2) & " " _
                                 & Mid(SN, 5, 2) & " " _
                                 & Mid(SN, 7, 2) & " " _
                                 & Mid(SN, 9, 2) & " " _
                                 & Mid(SN, 11, 2) & " " _
                                 & Mid(SN, 13, 2) & " " _
                                 & Mid(SN, 15, 2))

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


        'Inquire Account
        ReDim SendBuff(8)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HE4
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H4

        '4 bytes reference
        SendBuff(5) = &HAA
        SendBuff(6) = &HBB
        SendBuff(7) = &HCC
        SendBuff(8) = &HDD

        lst_Log.Items.Add("MCU < 80 E4 00 00 04")
        lst_Log.Items.Add("    <<AA BB CC DD")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6119" Then
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

        'Get Response to get result
        ReDim SendBuff(4)
        ReDim RecvBuff(26)

        SendBuff(0) = &H80
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H19

        lst_Log.Items.Add("MCU < 80 C0 00 00 19")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 27

        If Send_APDU_SLT(SendBuff, 5, RecvLen, RecvBuff) = True Then

            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(25)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(26)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(25)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(26)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve Result
                tempStr2 = ""

                For i = 0 To 24
                    tempStr2 = tempStr2 & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i

                lst_Log.Items.Add("MCU >> " & tempStr2)
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(25)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(26)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                tempStr2 = Replace(tempStr2, " ", "")

            End If

        Else
            Exit Sub
        End If

        'Verify Inquire Account
        ReDim SendBuff(33)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H7C

        If rb_3DES.Checked = True Then
            SendBuff(2) = &H2
        ElseIf rb_DES.Checked = True Then
            SendBuff(2) = &H3
        End If

        SendBuff(3) = &H0
        SendBuff(4) = &H1D
        'Ref
        SendBuff(5) = &HAA
        SendBuff(6) = &HBB
        SendBuff(7) = &HCC
        SendBuff(8) = &HDD
        'MAC
        SendBuff(9) = CInt("&H" & Mid(tempStr2, 1, 2))
        SendBuff(10) = CInt("&H" & Mid(tempStr2, 3, 2))
        SendBuff(11) = CInt("&H" & Mid(tempStr2, 5, 2))
        SendBuff(12) = CInt("&H" & Mid(tempStr2, 7, 2))

        'Transaction Type
        SendBuff(13) = CInt("&H" & Mid(tempStr2, 9, 2))

        'Balance
        SendBuff(14) = CInt("&H" & Mid(tempStr2, 11, 2))
        SendBuff(15) = CInt("&H" & Mid(tempStr2, 13, 2))
        SendBuff(16) = CInt("&H" & Mid(tempStr2, 15, 2))

        'ATREF
        SendBuff(17) = CInt("&H" & Mid(tempStr2, 17, 2))
        SendBuff(18) = CInt("&H" & Mid(tempStr2, 19, 2))
        SendBuff(19) = CInt("&H" & Mid(tempStr2, 21, 2))
        SendBuff(20) = CInt("&H" & Mid(tempStr2, 23, 2))
        SendBuff(21) = CInt("&H" & Mid(tempStr2, 25, 2))
        SendBuff(22) = CInt("&H" & Mid(tempStr2, 27, 2))

        'Max Balance
        SendBuff(23) = CInt("&H" & Mid(tempStr2, 29, 2))
        SendBuff(24) = CInt("&H" & Mid(tempStr2, 31, 2))
        SendBuff(25) = CInt("&H" & Mid(tempStr2, 33, 2))

        'TTREFc
        SendBuff(26) = CInt("&H" & Mid(tempStr2, 35, 2))
        SendBuff(27) = CInt("&H" & Mid(tempStr2, 37, 2))
        SendBuff(28) = CInt("&H" & Mid(tempStr2, 39, 2))
        SendBuff(29) = CInt("&H" & Mid(tempStr2, 41, 2))

        'TTREFd
        SendBuff(30) = CInt("&H" & Mid(tempStr2, 43, 2))
        SendBuff(31) = CInt("&H" & Mid(tempStr2, 45, 2))
        SendBuff(32) = CInt("&H" & Mid(tempStr2, 47, 2))
        SendBuff(33) = CInt("&H" & Mid(tempStr2, 49, 2))


        lst_Log.Items.Add("SAM < 80 7C " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " 00 1D")
        lst_Log.Items.Add("    <<AA BB CC DD " & Mid(tempStr2, 1, 2) & " " _
                                 & Mid(tempStr2, 3, 2) & " " _
                                 & Mid(tempStr2, 5, 2) & " " _
                                 & Mid(tempStr2, 7, 2) & " " _
                                 & Mid(tempStr2, 9, 2) & " " _
                                 & Mid(tempStr2, 11, 2) & " " _
                                 & Mid(tempStr2, 13, 2) & " " _
                                 & Mid(tempStr2, 15, 2) & " " _
                                 & Mid(tempStr2, 17, 2) & " " _
                                 & Mid(tempStr2, 19, 2) & " " _
                                 & Mid(tempStr2, 21, 2) & " " _
                                 & Mid(tempStr2, 23, 2))


        lst_Log.Items.Add("    <<" & Mid(tempStr2, 25, 2) & " " _
                                 & Mid(tempStr2, 27, 2) & " " _
                                 & Mid(tempStr2, 29, 2) & " " _
                                 & Mid(tempStr2, 31, 2) & " " _
                                 & Mid(tempStr2, 33, 2) & " " _
                                 & Mid(tempStr2, 35, 2) & " " _
                                 & Mid(tempStr2, 37, 2) & " " _
                                 & Mid(tempStr2, 39, 2) & " " _
                                 & Mid(tempStr2, 41, 2) & " " _
                                 & Mid(tempStr2, 43, 2) & " " _
                                 & Mid(tempStr2, 45, 2) & " " _
                                 & Mid(tempStr2, 47, 2) & " " _
                                 & Mid(tempStr2, 49, 2))


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
        Else
            Exit Sub
        End If

        Bal = CLng(Get_Balance(CInt("&H" & Mid(tempStr2, 11, 2)), CInt("&H" & Mid(tempStr2, 13, 2)), CInt("&H" & Mid(tempStr2, 15, 2))))

        'Prepare ACOS Transaction
        ReDim SendBuff(17)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H7E

        If rb_3DES.Checked = True Then
            SendBuff(2) = &H2
        ElseIf rb_DES.Checked = True Then
            SendBuff(2) = &H3
        End If

        SendBuff(3) = &HE6
        SendBuff(4) = &HD

        'Amount to Debit
        SendBuff(5) = CLng(txtDebitAmount.Text) \ 65536
        SendBuff(6) = (CLng(txtDebitAmount.Text) \ CLng(256)) Mod 65536 Mod CLng(256)
        SendBuff(7) = CLng(txtDebitAmount.Text) Mod CLng(256)

        'TTREFd
        SendBuff(8) = CInt("&H" & Mid(tempStr2, 43, 2))
        SendBuff(9) = CInt("&H" & Mid(tempStr2, 45, 2))
        SendBuff(10) = CInt("&H" & Mid(tempStr2, 47, 2))
        SendBuff(11) = CInt("&H" & Mid(tempStr2, 49, 2))

        'ATREF
        SendBuff(12) = CInt("&H" & Mid(tempStr2, 17, 2))
        SendBuff(13) = CInt("&H" & Mid(tempStr2, 19, 2))
        SendBuff(14) = CInt("&H" & Mid(tempStr2, 21, 2))
        SendBuff(15) = CInt("&H" & Mid(tempStr2, 23, 2))
        SendBuff(16) = CInt("&H" & Mid(tempStr2, 25, 2))
        SendBuff(17) = CInt("&H" & Mid(tempStr2, 27, 2))

        lst_Log.Items.Add("SAM < 80 7E " & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(2)), 2) & " E6 0D")
        lst_Log.Items.Add("    <<" & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(5)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(6)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(7)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(8)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(9)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(10)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(11)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(12)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(13)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(14)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(15)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(16)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(17)), 2))


        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 18, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "610B" Then
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


        'Get Response to get result
        ReDim SendBuff(4)
        ReDim RecvBuff(12)

        SendBuff(0) = &H0
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &HB

        lst_Log.Items.Add("SAM < 00 C0 00 00 0B")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = &HD

        If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then

            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(11)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(12)), 2) <> "9000" Then
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(11)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(12)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve Result
                tempStr = ""

                For i = 0 To 10
                    tempStr = tempStr & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i

                lst_Log.Items.Add("SAM >> " & tempStr)
                lst_Log.Items.Add("SAM > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(11)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(12)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                tempStr = Replace(tempStr, " ", "")

            End If

        Else
            Exit Sub
        End If

        'Debit and return Debit Certificate
        ReDim SendBuff(15)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &HE6
        SendBuff(2) = &H1
        SendBuff(3) = &H0
        SendBuff(4) = &HB
        SendBuff(5) = CInt("&H" & Mid(tempStr, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(tempStr, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(tempStr, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(tempStr, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(tempStr, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(tempStr, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(tempStr, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(tempStr, 15, 2))
        SendBuff(13) = CInt("&H" & Mid(tempStr, 17, 2))
        SendBuff(14) = CInt("&H" & Mid(tempStr, 19, 2))
        SendBuff(15) = CInt("&H" & Mid(tempStr, 21, 2))


        lst_Log.Items.Add("MCU < 80 E6 01 00 0B ")
        lst_Log.Items.Add("    <<" & Mid(tempStr, 1, 2) & " " _
                                 & Mid(tempStr, 3, 2) & " " _
                                 & Mid(tempStr, 5, 2) & " " _
                                 & Mid(tempStr, 7, 2) & " " _
                                 & Mid(tempStr, 9, 2) & " " _
                                 & Mid(tempStr, 11, 2) & " " _
                                 & Mid(tempStr, 13, 2) & " " _
                                 & Mid(tempStr, 15, 2) & " " _
                                 & Mid(tempStr, 17, 2) & " " _
                                 & Mid(tempStr, 19, 2) & " " _
                                 & Mid(tempStr, 21, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 16, RecvLen, RecvBuff) = True Then
            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) <> "6104" Then

                If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2) = "6A86" Then
                    lst_Log.Items.Add("Debit Certificate Not Supported By ACOS2 card or lower")
                    lst_Log.Items.Add("Change P1 = 0 to perform Debit without returning debit certificate")
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                    Debit_ACOS2(tempStr)
                Else
                    lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                    lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                End If

                Exit Sub
            Else
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(1)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
            End If
        Else
            Exit Sub
        End If

        'Get Response to get result
        ReDim SendBuff(4)
        ReDim RecvBuff(5)

        SendBuff(0) = &H80
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H4

        lst_Log.Items.Add("MCU < 80 C0 00 00 04")
        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 6

        If Send_APDU_SLT(SendBuff, 5, RecvLen, RecvBuff) = True Then

            If Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(4)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(5)), 2) <> "9000" Then
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(4)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(5)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1
                Exit Sub
            Else
                'Retrieve Result
                tempStr = ""

                For i = 0 To 3
                    tempStr = tempStr & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i

                lst_Log.Items.Add("MCU >> " & tempStr)
                lst_Log.Items.Add("MCU > " & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(4)), 2) & Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(5)), 2))
                lst_Log.SelectedIndex = lst_Log.Items.Count - 1

                tempStr = Replace(tempStr, " ", "")

            End If

        Else
            Exit Sub
        End If

        'Verify Debit Certificate
        ReDim SendBuff(24)
        ReDim RecvBuff(1)

        SendBuff(0) = &H80
        SendBuff(1) = &H70
        If rb_3DES.Checked = True Then
            SendBuff(2) = &H2
        ElseIf rb_DES.Checked = True Then
            SendBuff(2) = &H3
        End If
        SendBuff(3) = &H0
        SendBuff(4) = &H14
        SendBuff(5) = CInt("&H" & Mid(tempStr, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(tempStr, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(tempStr, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(tempStr, 7, 2))

        'Amount last Debited from card
        SendBuff(9) = CLng(txtDebitAmount.Text) \ 65536
        SendBuff(10) = (CLng(txtDebitAmount.Text) \ CLng(256)) Mod 65536 Mod CLng(256)
        SendBuff(11) = CLng(txtDebitAmount.Text) Mod CLng(256)

        'Expected New Balance after the Debit
        NewBal = Bal - CLng(txtDebitAmount.Text)

        SendBuff(12) = CLng(NewBal) \ 65536
        SendBuff(13) = (CLng(NewBal) \ CLng(256)) Mod 65536 Mod CLng(256)
        SendBuff(14) = CLng(NewBal) Mod CLng(256)

        'ATREF
        SendBuff(15) = CInt("&H" & Mid(tempStr2, 17, 2))
        SendBuff(16) = CInt("&H" & Mid(tempStr2, 19, 2))
        SendBuff(17) = CInt("&H" & Mid(tempStr2, 21, 2))
        SendBuff(18) = CInt("&H" & Mid(tempStr2, 23, 2))
        SendBuff(19) = CInt("&H" & Mid(tempStr2, 25, 2))
        SendBuff(20) = CInt("&H" & Mid(tempStr2, 27, 2))

        'TTREFd
        SendBuff(21) = CInt("&H" & Mid(tempStr2, 43, 2))
        SendBuff(22) = CInt("&H" & Mid(tempStr2, 45, 2))
        SendBuff(23) = CInt("&H" & Mid(tempStr2, 47, 2))
        SendBuff(24) = CInt("&H" & Mid(tempStr2, 49, 2))


        lst_Log.Items.Add("SAM < 80 70 02 00 14 ")
        lst_Log.Items.Add("    <<" & Mid(tempStr, 1, 2) & " " _
                                 & Mid(tempStr, 3, 2) & " " _
                                 & Mid(tempStr, 5, 2) & " " _
                                 & Mid(tempStr, 7, 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(9)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(10)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(11)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(12)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(13)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(14)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(15)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(16)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(17)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(18)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(19)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(20)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(21)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(22)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(23)), 2) & " " _
                                 & Microsoft.VisualBasic.Right("00" & Hex(SendBuff(24)), 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SAM(SendBuff, 25, RecvLen, RecvBuff) = True Then
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


    Private Sub Debit_ACOS2(ByVal tempStr As String)

        'Debit without returning debit certificate for ACOS2 or lower
        Dim RecvLen As Integer
        Dim SendBuff(15) As Byte
        Dim RecvBuff(1) As Byte

        SendBuff(0) = &H80
        SendBuff(1) = &HE6
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &HB
        SendBuff(5) = CInt("&H" & Mid(tempStr, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(tempStr, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(tempStr, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(tempStr, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(tempStr, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(tempStr, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(tempStr, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(tempStr, 15, 2))
        SendBuff(13) = CInt("&H" & Mid(tempStr, 17, 2))
        SendBuff(14) = CInt("&H" & Mid(tempStr, 19, 2))
        SendBuff(15) = CInt("&H" & Mid(tempStr, 21, 2))


        lst_Log.Items.Add("MCU < 80 E6 00 00 0B ")
        lst_Log.Items.Add("    <<" & Mid(tempStr, 1, 2) & " " _
                                 & Mid(tempStr, 3, 2) & " " _
                                 & Mid(tempStr, 5, 2) & " " _
                                 & Mid(tempStr, 7, 2) & " " _
                                 & Mid(tempStr, 9, 2) & " " _
                                 & Mid(tempStr, 11, 2) & " " _
                                 & Mid(tempStr, 13, 2) & " " _
                                 & Mid(tempStr, 15, 2) & " " _
                                 & Mid(tempStr, 17, 2) & " " _
                                 & Mid(tempStr, 19, 2) & " " _
                                 & Mid(tempStr, 21, 2))

        lst_Log.SelectedIndex = lst_Log.Items.Count - 1

        RecvLen = 2

        If Send_APDU_SLT(SendBuff, 16, RecvLen, RecvBuff) = True Then
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
        'Verify Key Input
        e.Handled = KeyVerify(Asc(e.KeyChar))
    End Sub


    Private Sub txt_PIN_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_PIN.KeyPress
        'Verify Key Input
        e.Handled = KeyVerify(Asc(e.KeyChar))
    End Sub


    Private Sub txt_New_PIN_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_New_PIN.KeyPress
        'Verify Key Input
        e.Handled = KeyVerify(Asc(e.KeyChar))
    End Sub


    Private Sub txtCreditAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCreditAmount.KeyPress

        'Verify Key Input
        If (Asc(e.KeyChar) >= 48 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub


    Private Sub txtDebitAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDebitAmount.KeyPress

        'Verify Key Input
        If (Asc(e.KeyChar) >= 48 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub

End Class
