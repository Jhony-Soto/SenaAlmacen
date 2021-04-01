Imports System.Text.RegularExpressions 'libreria para validar correos

Public Class email

    Public Function emailValidate(ByVal email As String) As Boolean
        ' retorna true o false   
        Return Regex.IsMatch(email,
               "^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$")
    End Function

    Public Sub SoloNumeros(e)
        If InStr(1, "0123456789" & Chr(8), e.KeyChar) = 0 Then
            e.handled = True

        End If
    End Sub

End Class
