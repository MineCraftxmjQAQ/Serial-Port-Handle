Imports System.IO.Ports
Imports System.Runtime.InteropServices
Public Class Form1
    <DllImport("user32", EntryPoint:="mouse_event", CharSet:=CharSet.Unicode, SetLastError:=True)>'鼠标事件函数
    Public Shared Function mouse_event(dwFlags As Integer, dx As Integer, dy As Integer, dwData As Integer, dwExtraInfo As Integer) As Integer
    End Function
    <DllImport("user32", EntryPoint:="keybd_event", CharSet:=CharSet.Unicode, SetLastError:=True)>'键盘事件函数
    Public Shared Function keybd_event(bVk As Byte, bScan As Byte, dwFlags As UInt32, dwExtraInfo As UInt32) As Integer
    End Function
    Const MOUSEEVENTF_MOVE = &H1 '模拟鼠标移动
    Const MOUSEEVENTF_LEFTDOWN = &H2 '模拟鼠标左键按下
    Const MOUSEEVENTF_LEFTUP = &H4 '模拟鼠标左键抬起
    Const MOUSEEVENTF_RIGHTDOWN = &H8 '模拟鼠标右键按下
    Const MOUSEEVENTF_RIGHTUP = &H10 '模拟鼠标右键抬起
    Const MOUSEEVENTF_MIDDLEDOWN = &H20 '模拟鼠标中键按下
    Const MOUSEEVENTF_MIDDLEUP = &H40 '模拟鼠标中键抬起
    Const MOUSEEVENTF_WHEEL = &H800 '模拟鼠标滚轮旋转
    Const MOUSEEVENTF_HWHEEL = &H1000 '模拟鼠标滚轮倾斜
    Private comm As New SerialPort() '串口对象
    Dim ports As String() = SerialPort.GetPortNames() '串口名数组
    '键盘映射表
    Dim KEYCODE() As Byte = {65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 8, 9, 12, 13, 16, 17, 18, 20, 27, 32, 33, 34, 35, 36, 37, 38, 39, 40, 45, 46, 144, 186, 187, 188, 189, 190, 191, 192, 219, 220, 221, 222, 175, 174, 179, 173}
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim i As Integer
        For i = 0 To ports.Length - 1
            ComboBox1.Items.Add(ports(i)) 'COM口列表
        Next i
        '控件初始化
        If ComboBox1.Items.Count = 0 Then
            Button1.Enabled = False
        End If
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = 6
        ComboBox3.SelectedIndex = 3
        ComboBox4.SelectedIndex = 0
        ComboBox5.SelectedIndex = 0
        ComboBox6.SelectedIndex = 0
        ComboBox7.SelectedIndex = 0
        ComboBox8.SelectedIndex = 0
        ComboBox9.SelectedIndex = 2
        ComboBox10.SelectedIndex = 0
        ComboBox11.SelectedIndex = 1
        ComboBox12.SelectedIndex = 2
        ComboBox13.SelectedIndex = 3
        ComboBox14.SelectedIndex = 0
        ComboBox15.SelectedIndex = 1
        ComboBox16.SelectedIndex = 2
        ComboBox17.SelectedIndex = 3
        ComboBox18.SelectedIndex = 4
        ComboBox19.SelectedIndex = 5
        ComboBox20.SelectedIndex = 6
        ComboBox21.SelectedIndex = 7
        ComboBox22.SelectedIndex = 8
        ComboBox23.SelectedIndex = 9
        ComboBox24.SelectedIndex = 10
        ComboBox25.SelectedIndex = 11
        ComboBox26.SelectedIndex = 12
        ComboBox27.SelectedIndex = 13
        ComboBox28.SelectedIndex = 14
        ComboBox29.SelectedIndex = 15
        ComboBox30.SelectedIndex = 16
        ComboBox31.SelectedIndex = 17
        ComboBox32.SelectedIndex = 18
        ComboBox33.SelectedIndex = 19
        ComboBox34.SelectedIndex = 20
        ComboBox35.SelectedIndex = 21
        ComboBox36.SelectedIndex = 22
        ComboBox37.SelectedIndex = 23
        ComboBox38.SelectedIndex = 24
        ComboBox39.SelectedIndex = 25
        ComboBox40.SelectedIndex = 26
        ComboBox41.SelectedIndex = 27
        ComboBox42.SelectedIndex = 28
        ComboBox43.SelectedIndex = 29
        ComboBox44.SelectedIndex = 30
        ComboBox45.SelectedIndex = 31
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.SelectedIndex < 0 Then
            MsgBox("错误:尚未选择串口,请在选择串口后再打开它", 0 + vbCritical + vbSystemModal, "尚未选择串口")
            Exit Sub
        End If
        If StrComp(Button1.Text, "打开串口") = 0 Then
            Call OpenSerialPort()
            AddHandler comm.DataReceived, AddressOf SerialPort_DataReceived '打开串口数据接收子线程,链接事件处理过程
        ElseIf StrComp(Button1.Text, "关闭串口") = 0 Then
            Call CloseSerialPort()
        End If
    End Sub
    Private Sub ComboBox1_DropDown(sender As Object, e As EventArgs) Handles ComboBox1.DropDown '串口下拉框刷新事件
        ports = SerialPort.GetPortNames
        ComboBox1.Items.Clear()
        ComboBox1.Items.AddRange(ports)
    End Sub
    Private Sub OpenSerialPort()
        Try
            Dim sb As StopBits() = {StopBits.One, StopBits.OnePointFive, StopBits.Two} '停止位参数
            Dim pt As Parity() = {Parity.None, Parity.Odd, Parity.Even} '校验位参数
            comm.PortName = ports(ComboBox1.SelectedIndex) '串口名
            comm.BaudRate = Val(ComboBox2.SelectedItem.ToString) '串口波特率
            comm.DataBits = Val(ComboBox3.SelectedItem.ToString) '数据位
            comm.StopBits = sb(ComboBox4.SelectedIndex) '停止位
            comm.Parity = pt(ComboBox5.SelectedIndex) '校验位
            comm.Open()
            Button1.Text = "关闭串口"
            ComboBox1.Enabled = False
            ComboBox2.Enabled = False
            ComboBox3.Enabled = False
            ComboBox4.Enabled = False
            ComboBox5.Enabled = False
        Catch ex As Exception
            MsgBox("错误:串口打开失败,请检查硬件连接", 0 + vbCritical + vbSystemModal, "串口打开失败")
        End Try
    End Sub
    Private Sub CloseSerialPort()
        comm.Close()
        Button1.Text = "打开串口"
        ComboBox1.Enabled = True
        ComboBox2.Enabled = True
        ComboBox3.Enabled = True
        ComboBox4.Enabled = True
        ComboBox5.Enabled = True
    End Sub
    Private Sub SerialPort_DataReceived(sender As Object, e As SerialDataReceivedEventArgs)
        Dim R_X As Integer, R_Y As Integer, L_X As Integer, L_Y As Integer, btn As Integer
        Dim temp As String = comm.ReadLine()
        Dim combo6 As Integer, combo7 As Integer, bar1 As Integer, bar2 As Integer, R_Mapping As Integer, L_Mapping As Integer, keynum As Integer
        R_X = Val(Mid(temp, 1, 4)) - 1000 '右摇杆X轴
        R_Y = Val(Mid(temp, 6, 4)) - 1000 '右摇杆Y轴
        L_X = Val(Mid(temp, 11, 4)) - 1000 '左摇杆X轴
        L_Y = Val(Mid(temp, 16, 4)) - 1000 '左摇杆Y轴
        btn = Val(Mid(temp, 21, 2)) '按键
        combo6 = ComboBox6.Invoke(New Func(Of Integer)(Function() As Integer
                                                           Return ComboBox6.SelectedIndex
                                                       End Function)) '右摇杆极性选择
        If combo6 = 1 Then
            R_X = -R_X
        ElseIf combo6 = 2 Then
            R_Y = -R_Y
        ElseIf combo6 = 3 Then
            R_X = -R_X
            R_Y = -R_Y
        End If
        combo7 = ComboBox7.Invoke(New Func(Of Integer)(Function() As Integer
                                                           Return ComboBox7.SelectedIndex
                                                       End Function)) '左摇杆极性选择
        If combo7 = 1 Then
            L_X = -L_X
        ElseIf combo7 = 2 Then
            L_Y = -L_Y
        ElseIf combo7 = 3 Then
            L_X = -L_X
            L_Y = -L_Y
        End If
        bar1 = HScrollBar1.Invoke(New Func(Of Integer)(Function() As Integer
                                                           Return HScrollBar1.Value
                                                       End Function)) '右摇杆灵敏度控制
        R_X /= bar1
        R_Y /= bar1
        bar2 = HScrollBar2.Invoke(New Func(Of Integer)(Function() As Integer
                                                           Return HScrollBar2.Value
                                                       End Function)) '左摇杆灵敏度控制
        L_X /= bar2
        L_Y /= bar2
        R_Mapping = ComboBox8.Invoke(New Func(Of Integer)(Function() As Integer
                                                              Return ComboBox8.SelectedIndex
                                                          End Function)) '右摇杆映射
        If R_Mapping = 0 Then '鼠标光标
            mouse_event(MOUSEEVENTF_MOVE, R_X, R_Y, 0, 0)
        ElseIf R_Mapping = 1 Then '鼠标滚轮(X轴映射)
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, R_X, 0)
        ElseIf R_Mapping = 2 Then '鼠标滚轮(Y轴映射)
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, R_Y, 0)
        ElseIf R_Mapping = 3 Then 'WASD
            If R_X > 0 Then 'D
                keybd_event(68, 0, 0, 0)
                keybd_event(68, 0, 2, 0)
            ElseIf R_X < 0 Then 'A
                keybd_event(65, 0, 0, 0)
                keybd_event(65, 0, 2, 0)
            End If
            If R_Y > 0 Then 'W
                keybd_event(87, 0, 0, 0)
                keybd_event(87, 0, 2, 0)
            ElseIf R_Y < 0 Then 'S
                keybd_event(83, 0, 0, 0)
                keybd_event(83, 0, 2, 0)
            End If
        ElseIf R_Mapping = 4 Then '方向键
            If R_X > 0 Then '向右
                keybd_event(39, 0, 0, 0)
                keybd_event(39, 0, 2, 0)
            ElseIf R_X < 0 Then '向左
                keybd_event(37, 0, 0, 0)
                keybd_event(37, 0, 2, 0)
            End If
            If R_Y > 0 Then '向上
                keybd_event(38, 0, 0, 0)
                keybd_event(38, 0, 2, 0)
            ElseIf R_Y < 0 Then '向下
                keybd_event(40, 0, 0, 0)
                keybd_event(40, 0, 2, 0)
            End If
        End If
        L_Mapping = ComboBox9.Invoke(New Func(Of Integer)(Function() As Integer
                                                              Return ComboBox9.SelectedIndex
                                                          End Function)) '左摇杆映射
        If L_Mapping = 0 Then '鼠标光标
            mouse_event(MOUSEEVENTF_MOVE, L_X, L_Y, 0, 0)
        ElseIf L_Mapping = 1 Then '鼠标滚轮(X轴映射)
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, L_X, 0)
        ElseIf L_Mapping = 2 Then '鼠标滚轮(Y轴映射)
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, L_Y, 0)
        ElseIf L_Mapping = 3 Then 'WASD
            If L_X > 0 Then 'D
                keybd_event(68, 0, 0, 0)
                keybd_event(68, 0, 2, 0)
            ElseIf L_X < 0 Then 'A
                keybd_event(65, 0, 0, 0)
                keybd_event(65, 0, 2, 0)
            End If
            If L_Y > 0 Then 'W
                keybd_event(87, 0, 0, 0)
                keybd_event(87, 0, 2, 0)
            ElseIf L_Y < 0 Then 'S
                keybd_event(83, 0, 0, 0)
                keybd_event(83, 0, 2, 0)
            End If
        ElseIf L_Mapping = 4 Then '方向键
            If L_X > 0 Then '向右
                keybd_event(39, 0, 0, 0)
                keybd_event(39, 0, 2, 0)
            ElseIf L_X < 0 Then '向左
                keybd_event(37, 0, 0, 0)
                keybd_event(37, 0, 2, 0)
            End If
            If L_Y > 0 Then '向上
                keybd_event(38, 0, 0, 0)
                keybd_event(38, 0, 2, 0)
            ElseIf L_Y < 0 Then '向下
                keybd_event(40, 0, 0, 0)
                keybd_event(40, 0, 2, 0)
            End If
        End If
        If btn = 1 Then '矩阵键盘01短按
            keynum = ComboBox14.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox14.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 2 Then '矩阵键盘02短按
            keynum = ComboBox16.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox16.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 3 Then '矩阵键盘03短按
            keynum = ComboBox18.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox18.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 4 Then '矩阵键盘04短按
            keynum = ComboBox20.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox20.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 5 Then '矩阵键盘05短按
            keynum = ComboBox22.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox22.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 6 Then '矩阵键盘06短按
            keynum = ComboBox24.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox24.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 7 Then '矩阵键盘07短按
            keynum = ComboBox26.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox26.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 8 Then '矩阵键盘08短按
            keynum = ComboBox28.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox28.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 9 Then '矩阵键盘09短按
            keynum = ComboBox30.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox30.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 10 Then '矩阵键盘10
            keynum = ComboBox32.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox32.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 11 Then '矩阵键盘11短按
            keynum = ComboBox34.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox34.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 12 Then '矩阵键盘12短按
            keynum = ComboBox36.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox36.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 13 Then '矩阵键盘13短按
            keynum = ComboBox38.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox38.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 14 Then '矩阵键盘14短按
            keynum = ComboBox40.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox40.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 15 Then '矩阵键盘15短按
            keynum = ComboBox42.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox42.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 16 Then '矩阵键盘16短按
            keynum = ComboBox44.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox44.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 17 Then '右摇杆短按
            keynum = ComboBox10.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox10.SelectedIndex
                                                            End Function))
            If keynum = 0 Then
                mouse_event(MOUSEEVENTF_LEFTDOWN Or MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
            ElseIf keynum = 1 Then
                mouse_event(MOUSEEVENTF_RIGHTDOWN Or MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            ElseIf keynum = 2 Then
                mouse_event(MOUSEEVENTF_MIDDLEDOWN Or MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0)
            ElseIf keynum = 3 Then
                mouse_event(MOUSEEVENTF_HWHEEL, 0, 0, 0, 0)
            End If
        ElseIf btn = 18 Then '左摇杆短按
            keynum = ComboBox12.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox12.SelectedIndex
                                                            End Function))
            If keynum = 0 Then
                mouse_event(MOUSEEVENTF_LEFTDOWN Or MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
            ElseIf keynum = 1 Then
                mouse_event(MOUSEEVENTF_RIGHTDOWN Or MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            ElseIf keynum = 2 Then
                mouse_event(MOUSEEVENTF_MIDDLEDOWN Or MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0)
            ElseIf keynum = 3 Then
                mouse_event(MOUSEEVENTF_HWHEEL, 0, 0, 0, 0)
            End If
        ElseIf btn = 33 Then '矩阵键盘01长按
            keynum = ComboBox15.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox15.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 34 Then '矩阵键盘02长按
            keynum = ComboBox17.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox17.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 35 Then '矩阵键盘03长按
            keynum = ComboBox19.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox19.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 36 Then '矩阵键盘04长按
            keynum = ComboBox21.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox21.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 37 Then '矩阵键盘05长按
            keynum = ComboBox23.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox23.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 38 Then '矩阵键盘06长按
            keynum = ComboBox25.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox25.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 39 Then '矩阵键盘07长按
            keynum = ComboBox27.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox27.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 40 Then '矩阵键盘08长按
            keynum = ComboBox29.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox29.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 41 Then '矩阵键盘09长按
            keynum = ComboBox31.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox31.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 42 Then '矩阵键盘10长按
            keynum = ComboBox33.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox33.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 43 Then '矩阵键盘11长按
            keynum = ComboBox35.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox35.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 44 Then '矩阵键盘12长按
            keynum = ComboBox37.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox37.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 45 Then '矩阵键盘13长按
            keynum = ComboBox39.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox39.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 46 Then '矩阵键盘14长按
            keynum = ComboBox41.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox41.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 47 Then '矩阵键盘15长按
            keynum = ComboBox43.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox43.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 48 Then '矩阵键盘16长按
            keynum = ComboBox45.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox45.SelectedIndex
                                                            End Function))
            keybd_event(KEYCODE(keynum), 0, 0, 0)
            keybd_event(KEYCODE(keynum), 0, 2, 0)
        ElseIf btn = 49 Then '右摇杆长按
            keynum = ComboBox11.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox11.SelectedIndex
                                                            End Function))
            If keynum = 0 Then
                mouse_event(MOUSEEVENTF_LEFTDOWN Or MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
            ElseIf keynum = 1 Then
                mouse_event(MOUSEEVENTF_RIGHTDOWN Or MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            ElseIf keynum = 2 Then
                mouse_event(MOUSEEVENTF_MIDDLEDOWN Or MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0)
            ElseIf keynum = 3 Then
                mouse_event(MOUSEEVENTF_HWHEEL, 0, 0, 0, 0)
            End If
        ElseIf btn = 50 Then '左摇杆长按
            keynum = ComboBox13.Invoke(New Func(Of Integer)(Function() As Integer
                                                                Return ComboBox13.SelectedIndex
                                                            End Function))
            If keynum = 0 Then
                mouse_event(MOUSEEVENTF_LEFTDOWN Or MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
            ElseIf keynum = 1 Then
                mouse_event(MOUSEEVENTF_RIGHTDOWN Or MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            ElseIf keynum = 2 Then
                mouse_event(MOUSEEVENTF_MIDDLEDOWN Or MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0)
            ElseIf keynum = 3 Then
                mouse_event(MOUSEEVENTF_HWHEEL, 0, 0, 0, 0)
            End If
        End If
    End Sub
End Class