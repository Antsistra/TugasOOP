
Imports System.Data.Odbc

Public Class Form1
    Dim Conn As OdbcConnection
    Dim cmd As OdbcCommand
    Dim Ds As DataSet
    Dim Da As OdbcDataAdapter
    Dim Rd As OdbcDataReader
    Dim MyDB As String

    Sub koneksi()
        MyDB = "Driver={MySQL ODBC 8.0 UNICODE Driver};Database=akademikdb;server=localhost;uid=root"
        Conn = New OdbcConnection(MyDB)
        If Conn.State = ConnectionState.Closed Then Conn.Open()
    End Sub

    Sub KondisiAwal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox2.Text = ""
        TextBox4.Text = ""
        ComboBox1.Text = ""
        ComboBox2.Text = ""
        Button1.Text = "Input"
        Button2.Text = "Edit"
        Button3.Text = "Delete"

        Call koneksi()
        Da = New OdbcDataAdapter("select * from mahasiswa", Conn)
        Ds = New DataSet
        Da.Fill(Ds, "mahasiswa")
        DataGridView1.DataSource = Ds.Tables("mahasiswa")
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call koneksi()
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Or ComboBox2.Text = "" Then
            MessageBox.Show("Silahkan mengisi data secara lengkap")
        Else
            Dim InputData As String = "insert into mahasiswa values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "', '" & ComboBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "' )"
            cmd = New OdbcCommand(InputData, Conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Input Data Berhasil")
            Call KondisiAwal()
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Main_Menu.Show()
        Me.Hide()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New OdbcCommand("select * from mahasiswa where nim = '" & TextBox1.Text & "'", Conn)
            Rd = cmd.ExecuteReader
            Rd.Read()
            If Rd.HasRows Then
                TextBox2.Text = Rd.Item("nama")
                ComboBox1.Text = Rd.Item("jekel")
                ComboBox2.Text = Rd.Item("prodi")
                TextBox3.Text = Rd.Item("alamat")
                TextBox4.Text = Rd.Item("nohp")
            Else
                MsgBox("Data tidak ditemukan")
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Or ComboBox2.Text = "" Then
            MessageBox.Show("Silahkan mengisi data secara lengkap")
        Else
            Call koneksi()
            Dim EditData As String = "update mahasiswa Set nama='" & TextBox2.Text & "',jekel='" & ComboBox1.Text & "',prodi='" & ComboBox2.Text & "',alamat='" & TextBox3.Text & "',nohp='" & TextBox4.Text & "' where nim='" & TextBox1.Text & "'"
            cmd = New OdbcCommand(EditData, Conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Edit Data Berhasil")
            Call KondisiAwal()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Or ComboBox2.Text = "" Then
            MessageBox.Show("Silahkan mengisi data secara lengkap")
        Else
            Call koneksi()
            Dim DeleteData As String = "delete from mahasiswa where nim='" & TextBox1.Text & "'"
            cmd = New OdbcCommand(DeleteData, Conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Delete Data Berhasil")
            Call KondisiAwal()
        End If
    End Sub
End Class

