using System;
using System.Text;
using System.Runtime.InteropServices;

namespace ACOSAccount
{
	/// <summary>
	/// Summary description for Chain3DES.
	/// </summary>
	
	public class Chain3DES
	{

		//============== ENCRYPTION ALGORITHM Constants ================
		public const int ALGO_DES = 0;
		public const int ALGO_3DES = 1;
		public const int ALGO_XOR = 3;
		public const int  DATA_ENCRYPT = 1;
		public const int DATA_DECRYPT = 2;


		/*Note : Block is equal to 8 bytes. So to encrypt/decrypt 8 bytes of data
				 user must use 1 block in the parameter.
				 Example:
				 This code encrypts 8 bytes of data!
				 Dim Data(1 to 8) as byte ' Assume data was entered
				 Dim Key(1 to 8) as byte ' Assume key already exits
				 Chain_DES(Data(1), Key(1), ALGO_3DES , 1 ,DATA_ENCRYPT) */
	
		//=========================================================================
		//      CHAIN_DES PROTOTYPE
		//=========================================================================
       
		[DllImport("chaindes.dll")]
		public static extern int Chain_DES(ref byte Data, ref byte key, short TripleDES, int Blocks, int method);
		[DllImport("chaindes.dll")]
		public static extern int Chain_MAC(ref byte mac, ref byte Data, ref byte key, int Blocks);
		[DllImport("chaindes.dll")]
		public static extern int Chain_MAC2(ref byte mac, ref byte Data, ref byte key, int Blocks);

		public Chain3DES()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}

/*=========================================================================================
'   Author :  Mary Anne C.A. Arana
'   Module :  Chain3DES.cs
'   Company:  Advanced Card Systems Ltd.
'   Date   :  July 22, 2005
'=========================================================================================*/


