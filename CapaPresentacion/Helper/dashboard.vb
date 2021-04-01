Public Class dashboard


    Public Function CerrarConfig()
        'Ocultamos el panel de Usuario
        frmDashBoard.pnlConfiguracion.Visible = False
    End Function


    Public Function CerrarPestañas()
        'frmConfigCuenta.Close()
        'frm_configuraciones.Close()
        'frmMovimientos_detalle.Close()
        'frmMovimientos_documento.Close()
        'frm_reservas.Close()
        'frm_detalle_reserva.Close()
        'frm_calendario.Close()
        'frm_insert_update_elemento.Close()
        'frmDashBoard.pnlConfiguracion.Visible = False
        'frm_clientes.Close()
        'frmMovimientos_DuplicarPrestamo.Close()
        'frm_elementos.Close()
        'frmMovimientos_DuplicarMovim.Close()
        'frm_mora.Close()
        'frm_historial_reservas.Close()
        'frmMora_Insert_Update.Close()
        'frmMovimientos_Historial.Close()
        'frm_bajas.Close()
        'frm_administradores.Close()
        'frmMovimientos_PrestamosCliente.Close()
        'frm_sincronizacion.Close()
    End Function

    Public Function BajarNivel(formulario)

        'Funcion para bajar de nivel los form y abrirlos dentro de un panel
        formulario.TopLevel = False
        formulario.Dock = DockStyle.Fill
        frmDashBoard.pnlPrincipal.Controls.Add(formulario)
        formulario.show()
        frmDashBoard.lblFormu.Text = formulario.text
    End Function


End Class
