Imports CapaDatos
Public Class CN_configuracion
    Dim objConfiguracion As New CD_configuracion

    Public Function RutaFotos() As DataTable
        Dim res = objConfiguracion.getAll
        Dim ruta = res.Tables(0)
        Return ruta
    End Function

    Public Function centroActual() As Integer
        Dim res = objConfiguracion.centroActual()
        Dim Fila = res.Rows.Count
        'Si no hay ningun registro en la tabla abrimos el formulario para que elija el centro de formacion
        If Fila = 0 Then
            Return 1
        Else
            CODCENTROACTUAL = res(0)(0)
        End If
    End Function

    Public Function cargar_centro(ByVal idCiudad As Integer) As DataTable
        Dim res = objConfiguracion.cargar_centro(idCiudad)
        Return res
    End Function

    Public Function cargar_ciudad(ByVal idDepart As Integer) As DataTable
        Dim res = objConfiguracion.cargar_ciudad(idDepart)
        Return res
    End Function

    Public Function cargar_departamentos() As DataTable
        Dim res = objConfiguracion.cargar_departamentos()
        Return res
    End Function


    Public Function saveCentroActual(ByVal datos As Array) As Integer
        Dim res = objConfiguracion.saveCentroActual(datos)
        If res = 1 Then
            CODCENTRO = datos(0) 'Almacenamos el codigo del centro para que pueda ser accedido desde cualquier objeto en la capa de negocio o presentacion
        End If
        Return res
    End Function

    Public Sub saveRutasConfig(ByVal datos As Array)
        Dim res = objConfiguracion.saveRutasConfig(datos)
        If res = 1 Then
            MsgBox("Se guardaron los datos correctamente", vbInformation)
        ElseIf res = 2627 Then
            MsgBox("El código del elemento ya existe, verifique de nuevo la información.", vbCritical)
        Else
            MsgBox("Ocurrio un error al intentar guardar las rutas de los archivos.", vbCritical)
        End If
    End Sub
End Class
