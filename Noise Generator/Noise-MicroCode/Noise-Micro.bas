$regfile = "m8def.dat"
$crystal = 1000000
$baud = 300
Enable Interrupts
Dim Random_number_amp As Integer
Dim ___rseed As Word : ___rseed = 102

Config Portb = Output
Config Lcd = 16 * 2
Config Lcdpin = Pin , Rs = Portb.0 , E = Portb.1 , Db4 = Portb.2 , Db5 = Portb.3 , Db6 = Portb.4 , Db7 = Portb.5
Config Adc = Single , Prescaler = Auto
Config Serialin = Buffered , Size = 30
Config Serialout = Buffered , Size = 30
Declare Sub Change_seed()
Dim A As Integer
Dim T As Double
Dim Temp As String * 1
Dim In1 As String * 20
Dim In_random(2) As String * 7
Dim Count As Integer
Start Adc
Dim Random_number_period As Integer
Dim New_random(2) As Integer
Clear Serialin
Cursor Off
Cls
Start_label:
Print "Start_label"
Input Temp Noecho

If Temp = "M" Then
Goto Micro_generate
Else
Goto Computer_generate
End If


Computer_generate:
Do




Input In1 Noecho

Count = Split(in1 , In_random(1) , ":")
New_random(1) = Val(in_random(1))
New_random(2) = Val(in_random(2))

Portb = New_random(1)




Waitms 15


Loop Until In1 = "S"
Goto Start_label

Micro_generate:
Do
Temp = Inkey()
Call Change_seed()
Random_number_amp = Rnd(256)
Portb = Random_number_amp
Random_number_period = Rnd(10000)
Print Random_number_amp
Waitms 15

Loop Until Temp = "S"
'Goto Start_label

End

Sub Change_seed()

___rseed = ___rseed + 1


End Sub
