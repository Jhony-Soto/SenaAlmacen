Imports System.Data.SqlClient
Imports System.Data
Public Class BDConexion
    Dim ConexionSQL As SqlConnection 'Atributo que contendra la conexion sql


    Public Function Conectar() As SqlConnection
        ConexionSQL = New SqlConnection("Data source=DESKTOP-7GVHR1M\SQLEXPRESS;Initial Catalog=Banco_Sena;integrated security=true")
        Return ConexionSQL
    End Function





End Class
