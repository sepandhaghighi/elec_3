Public Class Form1
    Public Shared random_number As Double
    Public Shared interval As Integer
    Public Shared index As Integer
    Public Shared out As String
    Dim temp As Double
    Dim low_period As Double
    Dim high_period As Double
    Public Sub init()
        ComboBox2.SelectedItem = ComboBox2.Items(0)
        ComboBox3.SelectedItem = ComboBox3.Items(0)
        ListBox1.SelectedIndex = 0
        ListBox2.SelectedIndex = 0
    End Sub
    Public Sub serial_stop_start(ByVal index As Integer)
        Select Case index
            Case 1
                Timer2.Enabled = True
                Timer1.Enabled = True
                Button2.Text = "Stop"
                'low_period = (1 / Val(TextBox2.Text)) * 1000000
                'high_period = (1 / Val(TextBox1.Text)) * 1000000
                'TextBox1.Enabled = False
                'TextBox2.Enabled = False
                SerialPort1.Write("C")
                SerialPort1.Write(vbCr)
                GroupBox1.Enabled = False
            Case 2
                Button2.Text = "Stop"
                SerialPort1.Write("M")
                SerialPort1.Write(vbCr)
                Timer3.Enabled = True
                Timer1.Enabled = True
                GroupBox1.Enabled = False


            Case Else
                Timer2.Enabled = False
                Timer1.Enabled = False
                Timer3.Enabled = False
                Button2.Text = "Start"
                'TextBox1.Enabled = True
                'TextBox2.Enabled = True
                SerialPort1.Write("S")
                SerialPort1.Write(vbCr)
                GroupBox1.Enabled = True
                SerialPort1.DiscardOutBuffer()
                SerialPort1.DiscardInBuffer()

        End Select
    End Sub
    Public Function chart_type(ByVal index As Integer) As DataVisualization.Charting.SeriesChartType
        Select Case index
            Case 0
                Return DataVisualization.Charting.SeriesChartType.Line
            Case 1
                Return DataVisualization.Charting.SeriesChartType.Spline
            Case 2
                Return DataVisualization.Charting.SeriesChartType.StepLine
            Case 3
                Return DataVisualization.Charting.SeriesChartType.FastLine
            Case 4
                Return DataVisualization.Charting.SeriesChartType.Bar
            Case 5
                Return DataVisualization.Charting.SeriesChartType.Area
            Case 6
                Return DataVisualization.Charting.SeriesChartType.Renko
            Case Else
                Return DataVisualization.Charting.SeriesChartType.Line
        End Select
    End Function
    Public Function chart_pallete(ByVal index As Integer) As DataVisualization.Charting.ChartColorPalette
        Select Case index
            Case 0
                Return DataVisualization.Charting.ChartColorPalette.None
            Case 1
                Return DataVisualization.Charting.ChartColorPalette.Bright
            Case 2
                Return DataVisualization.Charting.ChartColorPalette.Grayscale
            Case 3
                Return DataVisualization.Charting.ChartColorPalette.Excel
            Case 4
                Return DataVisualization.Charting.ChartColorPalette.Light
            Case 5
                Return DataVisualization.Charting.ChartColorPalette.Pastel
            Case 6
                Return DataVisualization.Charting.ChartColorPalette.EarthTones
            Case 7
                Return DataVisualization.Charting.ChartColorPalette.SemiTransparent
            Case 8
                Return DataVisualization.Charting.ChartColorPalette.Fire
            Case Else
                Return DataVisualization.Charting.ChartColorPalette.None

        End Select
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Chart1.Series(0).Points.AddXY(index, random_number)
        index = index + 1
        Label2.Text = random_number
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        On Error GoTo timer2_label

        ' Do
        '    Randomize()
        '   interval = Rnd() * high_period
        'Loop Until interval > low_period

        Randomize()
        random_number = Rnd()
        random_number = random_number * 5
        out = Str(random_number) + ":" + Str(interval)
        SerialPort1.Write(out)
        SerialPort1.Write(vbCr)
        index = index + 1
        Exit Sub
timer2_label:
        Timer1.Enabled = False
        Timer2.Enabled = False
        SerialPort1.Dispose()
        MsgBox("Serial Converter Disconnected")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'If Val(TextBox1.Text) >= Val(TextBox2.Text) Or Val(TextBox2.Text) > 16000000 Or Val(TextBox1.Text) <= 0 Then
        'MsgBox("Please Enter Correct Range For Frequency ")
        If Button2.Text = "Start" And RadioButton1.Checked = True Then

            serial_stop_start(1)
        ElseIf Button2.Text = "Start" And RadioButton2.Checked = True Then

            serial_stop_start(2)
        Else
            serial_stop_start(3)
        End If


    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        init()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        On Error GoTo serial_label
        If Button3.Text = "Connect" Then
            SerialPort1.PortName = ComboBox2.Items(ComboBox2.SelectedIndex).ToString
            SerialPort1.BaudRate = Int(ComboBox3.Items(ComboBox3.SelectedIndex))
            SerialPort1.Open()
            Button2.Enabled = True
            Button3.Text = "Disconnect"
        ElseIf Button3.Text = "Disconnect" And Button2.Text = "Stop" Then
            MsgBox("Please Stop Generator")
        Else

            Button3.Text = "Connect"
            SerialPort1.Close()
            Button2.Enabled = False

        End If

        Exit Sub
serial_label:
        SerialPort1.Dispose()
        MsgBox("Error In Serial Connection")
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Chart1.Series(0).ChartType = chart_type(ListBox1.SelectedIndex)
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        Chart1.Series(0).Palette = chart_pallete(ListBox2.SelectedIndex)
    End Sub

    Private Sub Timer3_Tick_1(sender As Object, e As EventArgs) Handles Timer3.Tick
        On Error GoTo timer3_error

        random_number = Val(SerialPort1.ReadLine)
        random_number = (random_number / 256) * 5
        Exit Sub
timer3_error:
        SerialPort1.Dispose()
        Timer1.Enabled = False
        Timer3.Enabled = False
        MsgBox("Error In Serial Port")
    End Sub
End Class
