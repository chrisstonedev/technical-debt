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
      Height          =   1935
      Left            =   360
      MultiLine       =   -1  'True
      TabIndex        =   3
      Top             =   1200
      Width           =   2895
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
      TabIndex        =   4
      Top             =   840
      Width           =   2175
   End
   Begin VB.Label txtStatus 
      Caption         =   "Status: Waiting for connection..."
      Height          =   375
      Left            =   600
      TabIndex        =   2
      Top             =   240
      Width           =   4575
   End
   Begin VB.Label lblClientNickname 
      Height          =   135
      Left            =   480
      TabIndex        =   1
      Top             =   4320
      Visible         =   0   'False
      Width           =   255
   End
   Begin VB.Label lblServerNickname 
      Height          =   135
      Left            =   240
      TabIndex        =   0
      Top             =   4320
      Visible         =   0   'False
      Width           =   255
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
    lblServerNickname.Caption = "IAmServer"
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
    objWinsock.SendData "C" & lblServerNickname.Caption
End Sub

Private Sub objWinsock_DataArrival(ByVal bytesTotal As Long)
    Dim strData, strData2, strData3 As String
    Call objWinsock.GetData(strData, vbString)
    strData2 = Left(strData, 1)
    strData = Mid(strData, 2)
    Select Case strData2
        Case "T" 'New text data
            strData3 = Left(strData, 1)
            strData = Mid(strData, 2)
            Select Case strData3
                Case "C"
                    txtMain.SelText = "Customer ID:     " & strData & vbCrLf
                    objWinsock.SendData "ROEnter product ID"
                Case "O"
                    txtMain.SelText = "Product ID:     " & strData & vbCrLf
                    objWinsock.SendData "F"
            End Select
        Case "N" 'New connection
            lblClientNickname.Caption = strData
        Case "S" 'Start request
            objWinsock.SendData "RCEnter customer ID"
    End Select
End Sub

