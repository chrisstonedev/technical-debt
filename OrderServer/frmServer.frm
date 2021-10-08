VERSION 5.00
Object = "{248DD890-BB45-11CF-9ABC-0080C7E7B78D}#1.0#0"; "MSWINSCK.OCX"
Begin VB.Form frmServer 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Order Server"
   ClientHeight    =   4200
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   7050
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4200
   ScaleWidth      =   7050
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox txtMain 
      Height          =   2295
      Left            =   360
      MultiLine       =   -1  'True
      TabIndex        =   1
      Top             =   1200
      Width           =   6135
   End
   Begin MSWinsockLib.Winsock objWinsock 
      Left            =   120
      Top             =   360
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
   End
   Begin VB.Label lblYourIP 
      Alignment       =   2  'Center
      Height          =   255
      Left            =   480
      TabIndex        =   2
      Top             =   600
      Width           =   3255
   End
   Begin VB.Label txtStatus 
      Caption         =   "Status: Waiting for connection..."
      Height          =   375
      Left            =   600
      TabIndex        =   0
      Top             =   240
      Width           =   4575
   End
End
Attribute VB_Name = "frmServer"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Form_Load()
    lblYourIP.Caption = "Listening on: " & objWinsock.LocalIP & " ; 187"

    Dim intFile As Integer
    Dim strFile As String
    strFile = "DB_FIELDS.TXT"
    intFile = FreeFile
    Dim strLine As String
    On Error Resume Next
    Open strFile For Input As #intFile
    If Err.Number = 53 Then
        If MsgBox("Database files cannot be found. Would you like to create them now?", vbYesNo, "OrderServer") = vbYes Then
            Open strFile For Output As #intFile
            Print #intFile, _
                "CEnter customer ID   " & vbCrLf & _
                "PEnter product ID    "
            Close #intFile
        Else
            End
        End If
    End If

    objWinsock.Close
    objWinsock.LocalPort = CLng(187)
    objWinsock.Listen
End Sub

Private Sub objWinsock_Close()
    txtStatus.Caption = "Status: Disconnected from client"

    objWinsock.Close
    objWinsock.LocalPort = CLng(187)
    objWinsock.Listen
End Sub

Private Sub objWinsock_ConnectionRequest(ByVal requestID As Long)
    Dim intFile As Integer
    Dim strFile As String
    Dim strLine As String
    Dim strTemp As String

    If objWinsock.State <> sckClosed Then
        objWinsock.Close
    End If
    objWinsock.Accept requestID
    txtStatus.Caption = "Status: Connected to client"
    txtMain.SelText = "SENT: " & "C" & vbCrLf
    strFile = "DB_AUDIT.TXT"
    intFile = FreeFile
    Open strFile For Append As #intFile
    Print #intFile, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "ECould not read data from client"
    Close intFile
    objWinsock.SendData "C"
End Sub

Private Sub objWinsock_DataArrival(ByVal bytesTotal As Long)
    Dim strData, strData2, strData3 As String
    Dim objDoc As DOMDocument60
    Dim blnSuccess As Boolean
    Dim objNode As IXMLDOMNode
    Dim objNodes As IXMLDOMNodeList
    Dim strJson As String
    Dim objFld As Collection
    Dim objRes As clsResponse
    Dim intFile As Integer
    Dim strFile As String
    Dim strLine As String
    Dim strTemp As String
    Dim strFF As String
    Dim intFF As Integer
    Dim strFFLn As String

    On Error GoTo Log_Error

    Call objWinsock.GetData(strData, vbString)

    strFile = "DB_AUDIT.TXT"
    strFF = "DB_FIELDS.TXT"
    intFile = FreeFile
    Open strFile For Append As #intFile
    txtMain.SelText = "RECEIVED: " & strData & vbCrLf
    Print #intFile, "R" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & strData

    strData2 = Left(strData, 1)
    strData = Mid(strData, 2)
    Select Case strData2
        Case "T"
            strData3 = Left(strData, 1)
            strData = Mid(strData, 2)
            Dim blnTmp As Boolean
            Select Case strData3
                Case "C"
                    If IsNumeric(strData) Then
                        blnTmp = True
                    Else
                        txtMain.SelText = "SENT: " & "ECustomer ID could not be found" & vbCrLf
                        Print #intFile, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "ECustomer ID could not be found"
                        objWinsock.SendData "ECustomer ID could not be found"
                    End If
                Case "P"
                    If Len(strData) > 2 Then
                        If Len(strData) <= 10 Then
                            blnTmp = True
                        Else
                            txtMain.SelText = "SENT: " & "EProduct ID is too long" & vbCrLf
                            Print #intFile, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "EProduct ID is too long"
                            objWinsock.SendData "EProduct ID is too long"
                        End If
                    Else
                        txtMain.SelText = "SENT: " & "EProduct ID is not long enough" & vbCrLf
                        Print #intFile, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "EProduct ID is not long enough"
                        objWinsock.SendData "EProduct ID is not long enough"
                    End If
            End Select

            Dim blnNxt As Boolean
            Dim strLn As String

            intFF = FreeFile
            If blnTmp Then
                strFF = "DB_FIELDS.TXT"
                Open strFF For Input As #intFF
                Do While Not EOF(intFF)
                    Line Input #intFF, strFFLn
                    If blnNxt Then
                        strLn = strFFLn
                        blnNxt = False
                    End If
                    If Left(strFFLn, 1) = strData3 Then
                        blnNxt = True
                    End If
                Loop
                Close #intFF

                Dim strFN As String
                Select Case Left(strLn, 1)
                    Case "C"
                        strFN = "Customer  "
                    Case "P"
                        strFN = "Product   "
                    Case "Q"
                        strFN = "Quantity  "
                    Case "R"
                        strFN = "Price     "
                End Select
                strLn = Left(strLn, 1) & strFN & Mid(strLn, 2)

                txtMain.SelText = "SENT: " & "R" & strLn & vbCrLf
                Print #intFile, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "R" & strLn
                objWinsock.SendData "R" & strLn
            End If
        Case "S"
            intFF = FreeFile
            Open strFF For Input As #intFF
            Line Input #intFF, strFFLn
            Close #intFF

            Dim strFFN As String
            Select Case Left(strFFLn, 1)
                Case "C"
                    strFFN = "Customer  "
                Case "P"
                    strFFN = "Product   "
                Case "Q"
                    strFFN = "Quantity  "
                Case "R"
                    strFFN = "Price     "
            End Select
            strFFLn = Left(strFFLn, 1) & strFFN & Mid(strFFLn, 2)
            txtMain.SelText = "SENT: " & "R" & strFFLn & vbCrLf
            Print #intFile, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "R" & strFFLn
            objWinsock.SendData "R" & strFFLn
        Case "F"
            Set objDoc = New DOMDocument60
            blnSuccess = objDoc.loadXML(strData)
            If blnSuccess Then
                Set objNodes = objDoc.selectNodes("/xml/*")
                Set objFld = New Collection
                For Each objNode In objNodes
                    Set objRes = New clsResponse
                    objRes.FieldID = objNode.Attributes.getNamedItem("id").Text
                    objRes.UserResponse = objNode.Text
                    objFld.Add objRes
                Next objNode

                Dim objOrder As clsOrder
                Set objOrder = New clsOrder
                
                For Each objRes In objFld
                    Select Case objRes.FieldID
                        Case "C"
                            objOrder.Customer = objRes.UserResponse
                        Case "P"
                            objOrder.Product = objRes.UserResponse
                        Case "Q"
                            objOrder.Quantity = CInt(objRes.UserResponse)
                        Case "R"
                            objOrder.Price = CDbl(objRes.UserResponse)
                    End Select
                Next objRes

                Dim intOrdersFile As Integer
                Dim strOrdersFile As String

                strOrdersFile = "DB_ORDERS.TXT"
                intOrdersFile = FreeFile
                Open strOrdersFile For Append As #intOrdersFile
                Print #intOrdersFile, objOrder.ToDatabaseFormat
                Close intOrdersFile

                txtMain.SelText = "SENT: " & "F" & vbCrLf
                Print #intFile, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "F"
                objWinsock.SendData "F"
            Else
                txtMain.SelText = "SENT: " & "ECould not read data from client" & vbCrLf
                Print #intFile, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "ECould not read data from client"
                objWinsock.SendData "ECould not read data from client"
            End If
    End Select

Done:

    Close #intFile
    Exit Sub

Log_Error:
    Dim ErrorNumber As Long
    Dim ErrorDescription As String
    ErrorNumber = Err.Number
    ErrorDescription = Err.Description

    On Error Resume Next
    Print #intFile, "E" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & CStr(ErrorNumber) & ": " & ErrorDescription
    If Err.Number > 0 Then
        MsgBox "Serious error alert!" & vbCrLf & CStr(ErrorNumber) & ": " & ErrorDescription, vbCritical, "Order Server"
    End If
    
    GoTo Done
End Sub

