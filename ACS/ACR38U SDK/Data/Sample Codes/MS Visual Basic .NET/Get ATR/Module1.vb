Module Module1

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
End Module
