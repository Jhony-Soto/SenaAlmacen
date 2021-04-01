Imports System.ComponentModel
Imports System.Windows.Forms.MouseEventArgs
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Imports CapaNegocio


Public Class frmDashBoard
    Dim click As MouseEventArgs
    Dim Ruta, arreglo, tipo_copia, Matriz_clientes(0, 0), contador As Object
    Dim Sw As Byte

    Dim objDashboard_CN As New CN_dashboard
    Dim helperDashboard As New dashboard


    Private Sub btnResponsive_Click(sender As Object, e As EventArgs) Handles btnResponsive.Click
        'Este codigo es para  el menu responsive del dasboar
        If ptbSena.Visible = True Then
            ptbSena.Visible = False
            pnlMenu.Size = New Size(53, 645)

            btnMovimientos.Size = New Size(38, 42)
            btnMovimientos.Location = New Point(7, 264)

            btnReservas.Size = New Size(38, 42)
            btnReservas.Location = New Point(7, 327)

            btnMora.Size = New Size(38, 42)
            btnMora.Location = New Point(7, 390)

            btnElementos.Size = New Size(38, 42)
            btnElementos.Location = New Point(7, 453)

            btnUsuarios.Size = New Size(38, 42)
            btnUsuarios.Location = New Point(7, 516)

            btnAdministradores.Size = New Size(38, 42)
            btnAdministradores.Location = New Point(7, 579)


            btnResponsive.Location = New Point(7, 4)

        Else


            pnlMenu.Size = New Size(222, 645)

            btnMovimientos.Size = New Size(195, 42)
            btnMovimientos.Location = New Point(12, 264)

            btnReservas.Size = New Size(195, 42)
            btnReservas.Location = New Point(12, 327)

            btnMora.Size = New Size(195, 42)
            btnMora.Location = New Point(12, 390)

            btnElementos.Size = New Size(195, 42)
            btnElementos.Location = New Point(12, 453)

            btnUsuarios.Size = New Size(195, 42)
            btnUsuarios.Location = New Point(12, 516)

            btnAdministradores.Size = New Size(195, 42)
            btnAdministradores.Location = New Point(12, 579)

            ptbSena.Visible = True

            btnResponsive.Location = New Point(184, 4)

        End If
    End Sub

    Private Sub ptbSesion_Click(sender As Object, e As EventArgs) Handles ptbSesion.Click
        'Este codigo es para abrir y cerrar el panel de Sesion
        If pnlConfiguracion.Visible = False Then
            Timer1.Start()
            pnlConfiguracion.Visible = True
            pnlConfiguracion.BringToFront()

        Else
            Timer1.Stop()
            pnlConfiguracion.Visible = False

        End If
    End Sub


    Private Sub btnConfigurarCuenta_Click(sender As Object, e As EventArgs) Handles btnConfigurarCuenta.Click
        helperDashboard.CerrarConfig() 'cerrramos el apanel de usuario
        helperDashboard.CerrarPestañas()
        helperDashboard.BajarNivel(frmConfigCuenta) 'enviamos a la funcion el formulario que queremos bajar de nivel
        Sw = 0
    End Sub

    Public Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click

        Dim objeAdmini_CN As New CN_administrador
        objeAdmini_CN.cerrarSession()
        Me.Close() 'cerramos este formulario
        frm_login.Show() 'abrimos el login
        Sw = 0
    End Sub

    Function restaurar_colores()
        btnMovimientos.BaseColor = Color.FromArgb(37, 136, 120)
        btnReservas.BaseColor = Color.FromArgb(37, 136, 120)
        btnMora.BaseColor = Color.FromArgb(37, 136, 120)
        btnElementos.BaseColor = Color.FromArgb(37, 136, 120)
        btnUsuarios.BaseColor = Color.FromArgb(37, 136, 120)
        btnAdministradores.BaseColor = Color.FromArgb(37, 136, 120)
        btnSincronizar.BaseColor = Color.FromArgb(37, 136, 120)
    End Function

    Function validar_backup()

        'Validamos si la copia de seguridad del sistema se debe hacer hoy
        'Consultamos la fecha en al cual se hace la copia de seguridad
        Dim tabla = objDashboard_CN.getFechaBackup

        'CUando aun no se ha configurado la fecha del backup
        If tabla(0)(6) Is DBNull.Value Then
            MsgBox("Aun no se ha configurado las fechas ni la ruta donde el sistema guardar la copia de seguridad de la base de datos.", vbCritical)
            'Cargamos el formuilario de configuraciones
            Cursor = Cursors.WaitCursor
            helperDashboard.CerrarPestañas()
            helperDashboard.CerrarConfig()
            helperDashboard.BajarNivel(frm_configuraciones)

            With frm_configuraciones
                .btn_backup_Click(Nothing, Nothing)
            End With
            Cursor = Cursors.Default
        Else

            'metodo para realizar la copia de seguridad
            objDashboard_CN.getFechaBackup(tabla)

        End If


    End Function


    Private Sub Eliminar_registros_de_3_años()

        'Esta funcion reliza un busqueda de aprendices que tengan estado 0 y que su registro se halla echo de 3 años atras
        'para eliminarlos y refrscar la bse de datos ,estos aprendices no tienen ningun tipo de deuda con el banco ,tambien se descargara un reporte con los aprenices que se eliminaron
        'asi poder revisar si se desea.
        Dim datos = objDashboard_CN.getRegister3Years()

        Dim tabla = datos.Tables(0)
        'Si hay aprendices ,cargamos el datagrid parar exportarlo como excel
        If tabla.Rows.Count > 0 Then

            'Cragamos el datagrid con los aprendices eliminados
            dgexportar.DataSource = datos
            dgexportar.DataMember = "datos"

            objDashboard_CN.deleteRegister(datos)

            'Esta matriz contendra todos la informacionde los aprendices
            ReDim Matriz_clientes(tabla.Rows.Count - 1, 6)
            For i = 0 To tabla.Rows.Count - 1
                For j = 0 To 6
                    Matriz_clientes(i, j) = tabla(i)(j)
                Next
            Next


            MsgBox("ATENCIÓN :  Se limpiara los registros de la base de datos  ya que tiene 3 años de antiguedad." & vbLf &
                   "Igualmente se abrira un reporte con los datos para que puedan ser gurdados y consultados.", vbInformation)

            MsgBox("¡Esto tardara unos segundos!", vbInformation)

            Cursor = Cursors.WaitCursor

            For i = 0 To tabla.Rows.Count - 1
                ''RELIAMOS LA BUSQUEDA Y ELIMINACION DE LOS PRESTAMOS  DEL APRENDIZ
                datos = Select_1ParametroInt("sp_tbl_prestamosXcliente_eliminar_Datos", Matriz_clientes(i, 0))
                tabla = New DataTable
                tabla = datos.tables("datos")

                'Si tiene prestamos ,eliminamos el detalle del prestamos
                If tabla.Rows.Count > 0 Then
                    'Se elimina del detalle de los prestamos
                    For j = 0 To tabla.Rows.Count - 1
                        Select_1ParametroInt("sp_tbl_prestamo_elemento_delete_datos", tabla(j)(0))
                    Next

                    'Eliminamos todso los prestamos de aprendiz
                    Select_1ParametroInt("sp_tbl_prestamos_delete", Matriz_clientes(i, 0))

                End If

                '****************************************************************************************************************

                'RELIAMOS LA BUSQUEDA Y ELIMINACION DE LAS MORAS DEL APRENDIZ
                datos = Mora_SelectPK(Matriz_clientes(i, 0), "sp_tbl_mora_Consultar", 0)
                tabla = New DataTable
                tabla = datos.tables("datos")


                If tabla.Rows.Count > 0 Then
                    'Si tiene elementos en mora los eeliminamos 
                    For j = 0 To tabla.Rows.Count - 1
                        Select_1ParametroInt("sp_tbl_mora_elemento_delete_definitivo", tabla(j)(0))
                    Next

                    'Eliminamos la mora
                    Select_1ParametroInt("sp_tbl_mora_delete_definitivo", Matriz_clientes(i, 0))

                End If
                'Eliminamos el apraniz definitivamente
                Select_1ParametroInt("sp_tbl_cliente_delete_definitivo", Matriz_clientes(i, 0))
            Next

            'Abrimos reporte con los clientes eliminados
            descargar_plantilla_bajas(dgexportar, "REPORTE DE CLIENTES ELIMINADOS DE LA BASE DE DATOS")


        End If

        'RELIAMOS LA BUSQUEDA Y ELIMINACION DE LAS RESERVAS
        datos = Select_1ParametroInt("sp_tbl_reservas_delete", Date.Now.AddYears(-3).ToString("yyyy/MM/dd"))
        tabla = New DataTable
        tabla = datos.tables("datos")

        If tabla.Rows.Count > 0 Then
            For i = 0 To tabla.Rows.Count - 1

                'ELIMINAMOS LOS REGISTROS EN LA TABLA RESERVA ELEMENTO 
                Select_1ParametroInt("sp_tbl_reserva_elemento_delete_definitivo", tabla(i)(0))


                'ELIMINAMOS LOS REGISTROS DE LA TABLA RESERVAS
                Select_1ParametroInt("sp_tbl_reserva_delete_definitivo", tabla(i)(0))
            Next
            'Cargamos el datagrid con los datos a exportar
            dgexportar.DataSource = datos
            dgexportar.DataMember = "datos"

            descargar_plantilla_bajas(dgexportar, "REPORTE DE RESERVAS  ELIMINADAS DE LA BASE DE DATOS")
        End If

        Cursor = Cursors.Default
    End Sub

    Private Sub frmDashBoard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        validar_reserva_hoy() 'Busca las reservas que hay para el dia de hoy

        validar_backup() 'Valida si hoy es el dia elegido para crear el BackUp 

        btnMovimientos.BaseColor = Color.FromArgb(26, 92, 81)



        'Esta funcion me valida si hay registro me hace tres 3 atras ,para eliminarlos creear un reporte de ellos
        Eliminar_registros_de_3_años()

        'Inicializamos el taimer para validar las reservas de hoy
        timer_validar_reserva.Start()

        lbladmin.Text = SESSION(1) & " " & SESSION(2) 'Esta variable SESSION  tiene toda la informacion del administrador que se logeo

        'Validar si la imagen existe

        CargarImg(ptbSesion, SESSION(7), "del administrador")


        BajarNivel(frmMovimientos_documento)

        For Each control As Control In Controls
            AddHandler control.MouseClick, AddressOf frmDashBoard_Click
        Next
    End Sub

    Private Sub frmDashBoard_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        If pnlConfiguracion.Visible = True Then
            pnlConfiguracion.Visible = False
        End If
    End Sub

    Private Sub frmDashBoard_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'String con el procedimiento almacenado
        datos = CargarDatos("sp_tbl_logeo_order_by")
        tabla = New DataTable
        tabla = datos.tables("datos")

        tbl_logeo_update(tabla.Rows(0)(0))
        CerrarPestañas()
        frm_login.Show() 'abrimos el login
    End Sub

    Private Sub btnConfiguracionGeneral_Click(sender As Object, e As EventArgs) Handles btnConfiguracionGeneral.Click
        CerrarPestañas()

        'Bajamos el nivel del formulario configuracion para quese muestre en el panel principal
        BajarNivel(frm_configuraciones)
        Sw = 0
    End Sub

    Private Sub btnMovimientos_Click(sender As Object, e As EventArgs) Handles btnMovimientos.Click
        restaurar_colores()
        btnMovimientos.BaseColor = Color.FromArgb(26, 92, 81)
        'Cargamos el formulario 
        CerrarPestañas()
        CerrarConfig()
        BajarNivel(frmMovimientos_documento)

    End Sub

    Private Sub btnReservas_Click(sender As Object, e As EventArgs) Handles btnReservas.Click
        restaurar_colores()
        btnReservas.BaseColor = Color.FromArgb(26, 92, 81)
        'Cargamos el formulario 

        CerrarPestañas()
        BajarNivel(frm_calendario)

    End Sub

    'Cierra el panel de configuracion cuando está abierto
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If MouseButtons = MouseButtons.Left And pnlConfiguracion.Visible = True And Sw = 0 Then
            Me.pnlConfiguracion.Visible = False
            Timer1.Stop()
        Else
            If pnlConfiguracion.Visible = True Then
                Timer1.Start()

            End If
        End If
    End Sub

    Private Sub pnlConfiguracion_MouseHover(sender As Object, e As EventArgs) Handles pnlConfiguracion.MouseHover
        Timer1.Stop()
        Sw = 1
    End Sub

    Private Sub btnConfiguracionGeneral_MouseHover(sender As Object, e As EventArgs) Handles btnConfiguracionGeneral.MouseHover
        Timer1.Stop()
        Sw = 1
    End Sub

    Private Sub btnConfigurarCuenta_MouseHover(sender As Object, e As EventArgs) Handles btnConfigurarCuenta.MouseHover
        Sw = 1
        Timer1.Stop()
    End Sub

    Private Sub btnCerrarSesion_MouseHover(sender As Object, e As EventArgs) Handles btnCerrarSesion.MouseHover
        Sw = 1
        Timer1.Stop()
    End Sub

    Private Sub btnUsuarios_Click(sender As Object, e As EventArgs) Handles btnUsuarios.Click

        restaurar_colores()
        btnUsuarios.BaseColor = Color.FromArgb(26, 92, 81)
        'Cargamos el formulario 
        Cursor = Cursors.WaitCursor
        CerrarPestañas()
        CerrarConfig()
        BajarNivel(frm_clientes)
        Cursor = Cursors.Default
    End Sub

    Private Sub timer_validar_reserva_Tick(sender As Object, e As EventArgs) Handles timer_validar_reserva.Tick
        frm_sincronizacion.validacion_de_sincronizacion() 'Validamos el estado de sincronizacion de los datos
        validar_reserva_hoy()

    End Sub

    Private Sub btnElementos_Click(sender As Object, e As EventArgs) Handles btnElementos.Click
        restaurar_colores()
        btnElementos.BaseColor = Color.FromArgb(26, 92, 81)
        'Cargamos el formulario 

        Cursor = Cursors.WaitCursor
        CerrarPestañas()
        CerrarConfig()
        BajarNivel(frm_elementos)
        Cursor = Cursors.Default
    End Sub

    Private Sub Timer_conexion_internet_Tick(sender As Object, e As EventArgs) Handles Timer_conexion_internet.Tick
        contador += 1

        If contador = 10 Then
            Timer_conexion_internet.Stop()
            btnSincronizar.BaseColor = Color.FromArgb(26, 92, 81)
            btnSincronizar.Text = "Sincronizar"
            contador = 0
        End If
    End Sub

    Private Sub btnSincronizar_Click(sender As Object, e As EventArgs) Handles btnSincronizar.Click
        restaurar_colores()
        btnSincronizar.BaseColor = Color.FromArgb(26, 92, 81)
        ''Cargamos el formulario
        Cursor = Cursors.WaitCursor
        If EstadoConexion() = True Then
            CerrarPestañas()
            CerrarConfig()
            BajarNivel(frm_sincronizacion)
            pnlMenu.Enabled = False
        Else

            btnSincronizar.BaseColor = Color.Red
            btnSincronizar.Text = "Sin conexion"
            Timer_conexion_internet.Start()

        End If
        Cursor = Cursors.Default

    End Sub

    Private Sub btnMora_Click(sender As Object, e As EventArgs) Handles btnMora.Click
        restaurar_colores()
        btnMora.BaseColor = Color.FromArgb(26, 92, 81)
        'Cargamos el formulario 
        CerrarPestañas()
        CerrarConfig()
        BajarNivel(frm_mora)
    End Sub

    Private Sub btnAdministradores_Click(sender As Object, e As EventArgs) Handles btnAdministradores.Click
        restaurar_colores()
        btnAdministradores.BaseColor = Color.FromArgb(26, 92, 81)
        'Cargamos el formulario 
        CerrarPestañas()
        CerrarConfig()
        BajarNivel(frm_administradores)

    End Sub

End Class
