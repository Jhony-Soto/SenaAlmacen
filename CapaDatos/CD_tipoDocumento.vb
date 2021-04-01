Imports System.Data.SqlClient
Public Class CD_tipoDocumento
    Dim objConexion As New BDConexion 'Clase de la conexion
    Dim dataAdapter As SqlDataAdapter
    Dim conexion As SqlConnection 'Atributo con la conexion.


    Function getAll() As DataSet
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tbl_tipo_documento_select", conexion)
            Dim dataset As New DataSet
            dataAdapter.Fill(dataset, "data")
            Return dataset
        Catch ex As Exception
            Throw ex
        Finally
            conexion.Dispose()
            dataAdapter.Dispose()
        End Try

    End Function

End Class
