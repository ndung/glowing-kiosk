Attribute VB_Name = "Utilities"
Option Explicit

'set the maximum logsize to 2 Mb
Const MaxLogSize = 2000000

Public Function RightJustify(ByVal myString As String, width As Integer) As String

    Dim leftPadding As String
    Dim i As Integer

    For i = 1 To width - Len(myString)
        leftPadding = leftPadding + "0"
    Next

   RightJustify = leftPadding + Left(myString, width)

End Function

Public Function LeftJustify(myString As String, width As Integer) As String

    Dim rightPadding As String
    Dim i As Integer

    For i = 1 To width - Len(myString)
        rightPadding = rightPadding + Chr(32)
    Next

   LeftJustify = Left(myString, width) + rightPadding

End Function

Public Function StringStartsWith(ByVal strValue As String, _
  CheckFor As String, Optional CompareType As VbCompareMethod _
   = vbBinaryCompare) As Boolean
   
'Determines if a string starts with the same characters as
'CheckFor string

'True if starts with CheckFor, false otherwise
'Case sensitive by default.  If you want non-case sensitive, set
'last parameter to vbTextCompare
    
    'Examples:
    'MsgBox StringStartsWith("Test", "TE") 'false
    'MsgBox StringStartsWith("Test", "TE", vbTextCompare) 'True
    
  Dim sCompare As String
  Dim lLen As Long
   
  lLen = Len(CheckFor)
  If lLen > Len(strValue) Then Exit Function
  sCompare = Left(strValue, lLen)
  StringStartsWith = StrComp(sCompare, CheckFor, CompareType) = 0

End Function

Public Sub WriteLog(sLogEntry As String)
   Const ForReading = 1, ForWriting = 2, ForAppending = 8
   Dim sLogFile As String, sLogPath As String, iLogSize As Long
   Dim fso, f
   
On Error GoTo ErrHandler

   'Set the path and filename of the log
   sLogPath = App.Path & "\" & App.EXEName
   sLogFile = sLogPath & ".log"
   
   Set fso = CreateObject("Scripting.FileSystemObject")
   Set f = fso.OpenTextFile(sLogFile, ForAppending, True)
   
   'Get the size of the log to check if it's getting unwieldly
   iLogSize = GetLogSize(sLogFile)
   If iLogSize > MaxLogSize Then
   
        'If too big, back it up to to retain some sort of history
        fso.CopyFile sLogFile, (sLogPath & ".old"), True
        Set f = Nothing
        fso.DeleteFile sLogFile
        'And start with a clean log-file
        Set f = fso.OpenTextFile(sLogFile, ForAppending, True)
        
   End If
    
   'Append the log-entry to the file together with time and date
   f.WriteLine Now() & vbTab & sLogEntry
   
ErrHandler:
    Exit Sub
End Sub

Private Function GetLogSize(filespec As String) As Long
'Returns the size of a file in bytes. If the file does not
'exist, it returns -1.

   Dim fso, f
   Set fso = CreateObject("Scripting.FileSystemObject")
   
   If (fso.FileExists(filespec)) Then
        Set f = fso.GetFile(filespec)
        GetLogSize = f.size
   Else
        GetLogSize = -1
   End If
End Function
