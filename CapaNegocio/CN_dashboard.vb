Imports CapaDatos
Public Class CN_dashboard
    Private objeConfig_CD As New CD_configuracion
    Private objPrestamo_CD As New CD_prestamos
    Private arreglo As Array

    Public Function getFechaBackup() As DataTable
        Return objeConfig_CD.getAll().Tables(0)
    End Function

    Public Sub getFechaBackup(ByVal tabla As DataTable)
        'Marcamos el radio button con el tipo de fecha de backup
        Dim tipo_copia = tabla(0)(6)

        'Obenemos el dia el que se se haran las copias
        Dim fecha_copia As Date = tabla(0)(7).date

        Select Case tipo_copia
            Case 1
                'COPIA DE SEGURIDAD SEMANALES
                If Date.Now.DayOfWeek = fecha_copia.DayOfWeek Then
                    If tabla(0)(8) Is DBNull.Value Then
                        generar_copia() 'Est funcion genera la copia de la base de datos
                    Else
                        If tabla(0)(8) = 0 Then
                            generar_copia() 'Est funcion genera la copia de la base de datos
                        End If

                    End If
                Else
                    'Actualizmos el estado de la copia para que cuando sea la fecha estipulada vuelva a generarse
                    arreglo = {tabla(0)(7), 0}
                    objeConfig_CD.updateStateBackup(arreglo)
                End If
                    '*****************************************************************************************************************

                    'COPIA SE SEGURIDAD MENSUALES
            Case 2
                'Validamos que la fecha de la copia sea hoy
                If fecha_copia.AddMonths(1).DayOfWeek = Date.Now.DayOfWeek Then
                    If tabla(0)(8) = 0 Or DBNull.Value Then
                        generar_copia() 'Est funcion genera la copia de la base de datos
                    Else
                        If tabla(0)(8) = 0 Then
                            generar_copia() 'Est funcion genera la copia de la base de datos
                        End If
                    End If
                Else
                    'Actualizmos el estado de la copia para que cuando sea la fecha estipulada vuelva a generarse
                    arreglo = {tabla(0)(7), 0}
                    objeConfig_CD.updateStateBackup(arreglo)
                End If

                    'COPIA DE SEGURIDAD TRIMESTRAL
            Case 3
                'Validamos que la fecha de la copia sea hoy
                If fecha_copia.AddMonths(3).DayOfWeek = Date.Now.DayOfWeek Then
                    If tabla(0)(8) = 0 Or DBNull.Value Then
                        generar_copia() 'Est funcion genera la copia de la base de datos
                    Else
                        If tabla(0)(8) = 0 Then
                            generar_copia() 'Est funcion genera la copia de la base de datos
                        End If
                    End If
                Else
                    'Actualizmos el estado de la copia para que cuando sea la fecha estipulada vuelva a generarse
                    arreglo = {tabla(0)(7), 0}
                    objeConfig_CD.updateStateBackup(arreglo)
                End If

                    'COPIA DE SEGURIDAD SEMESTRAL
            Case 4
                'Validamos que la fecha de la copia sea hoy
                If fecha_copia.AddMonths(6).DayOfWeek = Date.Now.DayOfWeek Then
                    If tabla(0)(8) = 0 Or DBNull.Value Then
                        generar_copia() 'Est funcion genera la copia de la base de datos
                    Else
                        If tabla(0)(8) = 0 Then
                            generar_copia() 'Est funcion genera la copia de la base de datos
                        End If
                    End If
                Else
                    'Actualizmos el estado de la copia para que cuando sea la fecha estipulada vuelva a generarse
                    arreglo = {tabla(0)(7), 0}
                    objeConfig_CD.updateStateBackup(arreglo)
                End If
        End Select
    End Sub

    Public Sub generar_copia()
        'Consultamos la fecha en al cual se hace la copia de seguridad
        Dim tabla = objeConfig_CD.getAll().Tables(0)

        Dim ruta = tabla(0)(9) 'Ruat donde se guardara la copia de segiridad
        Dim nombre_backup = "BackUp_Banco_sena_" & Date.Now.Day & Date.Now.Month & Date.Now.Year & "_" & Now.Hour & "_" & Now.Minute & "_" & Now.Second & ".bak" 'Nomabre de la copia de seguridad
        Dim res = objeConfig_CD.generateBackup("BACKUP DATABASE Banco_Sena TO DISK ='" & ruta & nombre_backup & "'") 'string del procedimiento para generar el backup

        If res = 1 Then
            MsgBox("El sistema  genero la copia de seguridad de la Base de datos correctamente.", vbInformation)
        ElseIf res = 3201 Then
            MsgBox("La ruta especificada no pertenece a la carpeta SQL SERVER para las copias de seguridad" & vbLf & "Por favor vuelva a configurar la ruta.", vbCritical)
        End If


        'Actualizmos la fecha y el estado de la copia con la fecha actual
        arreglo = {Date.Now.Date, 1}
        objeConfig_CD.updateStateBackup(arreglo)

    End Sub

    'Consulta los registros de hace 3 años
    Public Function getRegister3Years() As DataSet
        Dim objeCliente_CD As New CD_cliente
        Return objeCliente_CD.getClientesXfecha(Date.Now.AddYears(-3).ToString("yyyy/MM/dd"))
    End Function

    Public Sub deleteRegister(ByVal datos As DataSet)
        Dim Matriz_clientes(0, 0) As Object
        Dim tablaClientes = datos.Tables(0)

        'Esta matriz contendra todos la informacionde los aprendices
        ReDim Matriz_clientes(tablaClientes.Rows.Count - 1, 6)
        For i = 0 To tablaClientes.Rows.Count - 1
            For j = 0 To 6
                Matriz_clientes(i, j) = tablaClientes(i)(j)
            Next
        Next

        For i = 0 To tablaClientes.Rows.Count - 1
            ''RELIAMOS LA BUSQUEDA Y ELIMINACION DE LOS PRESTAMOS  DEL APRENDIZ
            Dim prestamosCliente = objPrestamo_CD.prestamoXcliente(Matriz_clientes(i, 0))

            'Si tiene prestamos ,eliminamos el detalle del prestamos
            If prestamosCliente.Rows.Count > 0 Then
                'Se elimina del detalle de los prestamos
                For j = 0 To prestamosCliente.Rows.Count - 1
                    objPrestamo_CD.deletePrestamosXcliente(prestamosCliente(j)(0))
                Next
            End If

            '****************************************************************************************************************

            'RELIZAMOS LA BUSQUEDA Y ELIMINACION DE LAS MORAS DEL APRENDIZ
            Dim objMora_CD As New CD_mora
            Dim idMorasCliente = objMora_CD.getIdsMorasXcliente(Matriz_clientes(i, 0))


            If idMorasCliente.Rows.Count > 0 Then
                'Si tiene elementos en mora los eeliminamos 
                For j = 0 To idMorasCliente.Rows.Count - 1
                    Select_1ParametroInt("sp_tbl_mora_elemento_delete_definitivo", idMorasCliente(j)(0))
                Next

                'Eliminamos la mora
                Select_1ParametroInt("sp_tbl_mora_delete_definitivo", Matriz_clientes(i, 0))

            End If
            'Eliminamos el apraniz definitivamente
            Select_1ParametroInt("sp_tbl_cliente_delete_definitivo", Matriz_clientes(i, 0))
        Next


    End Sub
End Class
