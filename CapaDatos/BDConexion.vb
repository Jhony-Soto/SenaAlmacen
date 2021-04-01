Imports System.Data.SqlClient
Imports System.Data
Public Class BDConexion
    Dim conexionSQL As SqlConnection 'Atributo que contendra la conexion sql


    Public Function conectar() As SqlConnection
        conexionSQL = New SqlConnection("Data source=DESKTOP-7GVHR1M\SQLEXPRESS;Initial Catalog=Banco_Sena;integrated security=true")
        Return conexionSQL
    End Function

End Class
