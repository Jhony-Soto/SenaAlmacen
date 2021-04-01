Imports CapaDatos
Public Class CN_prestamos
    Dim objPrestamos_CD As New CD_prestamos


    Public Function prestamosActivos() As DataTable
        Dim fecha_actual As Date = Date.Now.Date 'fecha actual
        Dim res = objPrestamos_CD.prestamosActivos(fecha_actual)
        Return res.Tables(0)
    End Function
End Class
