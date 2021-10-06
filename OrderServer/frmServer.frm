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
      Left            =   2040
      TabIndex        =   2
      Top             =   840
      Width           =   2175
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
    lblYourIP.Caption = "Your IP:  " & objWinsock.LocalIP

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
    If objWinsock.State <> sckClosed Then
        objWinsock.Close
    End If
    objWinsock.Accept requestID
    txtStatus.Caption = "Status: Connected to client"
    txtMain.SelText = "SENT: " & "C" & vbCrLf
    objWinsock.SendData "C"
End Sub

Private Sub objWinsock_DataArrival(ByVal bytesTotal As Long)
    Dim strData, strData2, strData3 As String
    Dim objDocument As DOMDocument60
    Dim blnSuccess As Boolean
    Dim objNode As IXMLDOMNode
    Dim objNodeList As IXMLDOMNodeList
    Dim strJson As String
    Dim objFieldsToSave As Collection
    Dim objResponse As clsResponse

    Call objWinsock.GetData(strData, vbString)

    txtMain.SelText = "RECEIVED: " & strData & vbCrLf

    strData2 = Left(strData, 1)
    strData = Mid(strData, 2)
    Dim strTemp As String
    Select Case strData2
        Case "T" 'New text data
            strData3 = Left(strData, 1)
            strData = Mid(strData, 2)
            Select Case strData3
                Case "C"
                    If IsNumeric(strData) Then
                        'txtMain.SelText = "Customer ID:     " & strData & vbCrLf
                        txtMain.SelText = "SENT: " & "ROEnter product ID" & vbCrLf
                        objWinsock.SendData "ROEnter product ID"
                    Else
                        'txtMain.SelText = "Customer ID (ERROR):     " & strData & vbCrLf
                        txtMain.SelText = "SENT: " & "ECustomer ID could not be found" & vbCrLf
                        objWinsock.SendData "ECustomer ID could not be found"
                    End If
                Case "O"
                    If Len(strData) > 2 Then
                        If Len(strData) <= 10 Then
                            'txtMain.SelText = "Product ID:     " & strData & vbCrLf
                            txtMain.SelText = "SENT: " & "R" & vbCrLf
                            objWinsock.SendData "R"
                        Else
                            'txtMain.SelText = "Product ID (ERROR):     " & strData & vbCrLf
                            txtMain.SelText = "SENT: " & "EProduct ID is too long" & vbCrLf
                            objWinsock.SendData "EProduct ID is too long"
                        End If
                    Else
                        'txtMain.SelText = "Product ID (ERROR):     " & strData & vbCrLf
                        txtMain.SelText = "SENT: " & "EProduct ID is not long enough" & vbCrLf
                        objWinsock.SendData "EProduct ID is not long enough"
                    End If
            End Select
        Case "S" 'Start request
            txtMain.SelText = "SENT: " & "RCEnter customer ID" & strTemp & vbCrLf
            objWinsock.SendData "RCEnter customer ID" & strTemp
        Case "F" 'Finish request
            Set objDocument = New DOMDocument60
            blnSuccess = objDocument.loadXML(strData)
            If blnSuccess Then
                Set objNodeList = objDocument.selectNodes("/xml/*")
                Set objFieldsToSave = New Collection
                For Each objNode In objNodeList
                    Set objResponse = New clsResponse
                    objResponse.FieldID = objNode.Attributes.getNamedItem("id").Text
                    objResponse.UserResponse = objNode.Text
                    objFieldsToSave.Add objResponse
                Next objNode
                For Each objResponse In objFieldsToSave
                    'txtMain.SelText = "Client wants to use data """ & objResponse.UserResponse & """ for field of id """ & objResponse.FieldID & """" & vbCrLf
                Next objResponse
                'TODO: Attempt to save to database.
                txtMain.SelText = "SENT: " & "F" & vbCrLf
                objWinsock.SendData "F"
            Else
                'txtMain.SelText = "Customer ID (ERROR):     " & strData & vbCrLf
                txtMain.SelText = "SENT: " & "ECould not read data from client" & vbCrLf
                objWinsock.SendData "ECould not read data from client"
            End If
    End Select
End Sub

