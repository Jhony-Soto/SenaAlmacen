Imports CapaDatos


Public Class CN_administrador
    Dim objAdmin_CD As New CD_administrador

    'valida si ya hay un administrdor en la BD
    Public Function validateAadmin() As Boolean
        Dim res = objAdmin_CD.getAll()
        Dim rows = res.Tables().Count
        If (rows > 0) Then
            Return False ' si ya hay un registro se oculta el btn de registro de login
        Else
            Return True
        End If

    End Function

    'Registrar un usuario
    Public Function saveAdmin(ByVal datos As Array) As Integer
        Dim res = objAdmin_CD.saveAdministrador(datos)
        Return res
    End Function

    'CAmbia el estado del administrador
    Public Sub inactivos_activar_cliente(ByVal id As Integer)
        Dim res = objAdmin_CD.inactivos_activar_cliente(id)
        MsgBox("¡Se volvió a activar el cliente exitosamente!", vbInformation)
    End Sub


    Public Function recuperarContraseña(ByVal datos As Array) As DataTable
        Dim res = objAdmin_CD.recuperarContraseña(datos)
        Return res
    End Function


    'Inicio de session
    Public Sub ValidarLogin(ByVal datos As Array)
        Dim res = objAdmin_CD.ValidarLogin(datos)
        If res.Rows.Count > 0 Then
            For i = 0 To 10
                SESSION(i) = res(0)(i) 'SESSION se encuetra en el modulo llamado VariablesEntorni
            Next

            'Registra el administrador que inicia session
            Dim objLogue As New CD_logueo
            objLogue.inserLogueo({SESSION(0), Date.Now})

        Else
            MsgBox("Datos incorrectos, por favor verifique la información !", vbCritical)
        End If
    End Sub

    'Cerrar session 
    Public Sub cerrarSession()
        Dim objLogeoCD As New CD_logueo
        Dim res = objLogeoCD.idUltimoRegistro
        objLogeoCD.dateFinishSession(res.Rows(0)(0))
    End Sub

End Class
