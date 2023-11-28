
Imports System.Data.Odbc
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Public Class Form2
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
        ComboBox1.Text = ""
        ComboBox2.Text = ""

        Call koneksi()
        Da = New OdbcDataAdapter("select * from mata_kuliah", Conn)
        Ds = New DataSet
        Da.Fill(Ds, "mahasiswa")
        DataGridView1.DataSource = Ds.Tables("mahasiswa")
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call koneksi()
        If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Then
            MessageBox.Show("Silahkan mengisi data secara lengkap")
        Else
            Dim InputData As String = "insert into mata_kuliah values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "' ,'" & ComboBox2.Text & "')"
            cmd = New OdbcCommand(InputData, Conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Input Data Berhasil")
            Call KondisiAwal()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New OdbcCommand("select * from mata_kuliah where kode_mk= '" & TextBox1.Text & "'", Conn)
            Rd = cmd.ExecuteReader
            Rd.Read()
            If Rd.HasRows Then
                TextBox2.Text = Rd.Item("nama_mk")
                ComboBox1.Text = Rd.Item("sks")
                ComboBox2.Text = Rd.Item("semester")
            Else
                MsgBox("Data tidak ditemukan")
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Then
            MessageBox.Show("Silahkan mengisi data secara lengkap")
        Else
            Call koneksi()
            Dim EditData As String = "update mata_kuliah Set nama_mk='" & TextBox2.Text & "',sks='" & ComboBox1.Text & "',semester='" & ComboBox2.Text & "' where kode_mk='" & TextBox1.Text & "'"
            cmd = New OdbcCommand(EditData, Conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Edit Data Berhasil")
            Call KondisiAwal()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Or ComboBox2.Text = "" Then
            MessageBox.Show("Silahkan mengisi data secara lengkap")
        Else
            Call koneksi()
            Dim DeleteData As String = "delete from mata_kuliah where kode_mk='" & TextBox1.Text & "'"
            cmd = New OdbcCommand(DeleteData, Conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Delete Data Berhasil")
            Call KondisiAwal()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Main_Menu.Show()
        Me.Hide()
    End Sub
End Class