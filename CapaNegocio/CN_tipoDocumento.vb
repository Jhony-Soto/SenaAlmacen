Imports CapaDatos
Public Class CN_tipoDocumento
    Dim objTipoDocumento As New CD_tipoDocumento

    Function getAll() As DataTable
        Dim res = objTipoDocumento.getAll
        Dim table As New DataTable
        table = res.Tables("data")

        Return table
    End Function
End Class
