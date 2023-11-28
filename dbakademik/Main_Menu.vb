Public Class Main_Menu
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim MahasiswaForm As New Form1()

        Me.Hide()
        MahasiswaForm.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim MataKuliah As New Form2()

        Me.Hide()
        MataKuliah.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        End
    End Sub
End Class