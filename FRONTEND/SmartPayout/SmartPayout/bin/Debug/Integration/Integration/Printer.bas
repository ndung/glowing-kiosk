Attribute VB_Name = "Printer"
Public Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
Public Declare Function GetTickCount Lib "kernel32" () As Long
Global ReceiveCnt As Long
