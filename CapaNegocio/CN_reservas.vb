Imports CapaDatos
Public Class CN_reservas
    Dim objReservas_CD As New CD_reservas

    Public Function reservasActivas() As DataTable
        Dim fecha_actual As Date = Date.Now.Date 'fecha actual
        Return objReservas_CD.reservasActivas(fecha_actual)
    End Function
End Class
