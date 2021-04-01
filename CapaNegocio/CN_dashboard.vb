﻿Imports CapaDatos
Public Class CN_dashboard
    Private objeConfig_CD As New CD_configuracion

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
End Class
