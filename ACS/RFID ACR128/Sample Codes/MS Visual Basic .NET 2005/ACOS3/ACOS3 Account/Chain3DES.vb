Public Class Chain3Des
    '============== ENCRYPTION ALGORITHM Constants ================
    Public Const ALGO_DES = 0
    Public Const ALGO_3DES = 1
    Public Const ALGO_XOR = 3
    Public Const DATA_ENCRYPT = 1
    Public Const DATA_DECRYPT = 2



    'Note : Block is equal to 8 bytes. So to encrypt/decrypt 8 bytes of data
    '       user must use 1 block in the parameter.
    '       Example:
    '        'This code encrypts 8 bytes of data!
    '        Dim Data(1 to 8) as byte ' Assume data was entered
    '        Dim Key(1 to 8) as byte ' Assume key already exits
    '        Chain_DES(Data(1), Key(1), ALGO_3DES , 1 ,DATA_ENCRYPT)
    '
    '=========================================================================
    '       CHAIN_DES PROTOTYPE
    '=========================================================================

    Declare Function Chain_DES Lib "chaindes.dll" (ByRef Data As Byte, ByRef key As Byte, ByVal TripleDES As Short, ByVal Blocks As Integer, ByVal method As Integer) As Integer
    Declare Function Chain_MAC Lib "chaindes.dll" (ByRef mac As Byte, ByRef Data As Byte, ByRef key As Byte, ByVal Blocks As Integer) As Integer
    Declare Function Chain_MAC2 Lib "chaindes.dll" (ByRef mac As Byte, ByRef Data As Byte, ByRef key As Byte, ByVal Blocks As Integer) As Integer

End Class

'=========================================================================================
'   Author :  Mary Anne C.A. Arana
'   Module :  Chain3DES.vb
'   Company:  Advanced Card Systems Ltd.
'   Date   :  July 13, 2005
'=========================================================================================