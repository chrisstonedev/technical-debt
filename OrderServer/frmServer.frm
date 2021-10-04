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
   Begin VB.CommandButton cmdSend 
      Caption         =   "&Send"
      Default         =   -1  'True
      Height          =   315
      Left            =   5880
      TabIndex        =   3
      Top             =   3840
      Width           =   975
   End
   Begin VB.TextBox txtData 
      Height          =   285
      Left            =   120
      MaxLength       =   100
      TabIndex        =   2
      Top             =   3840
      Width           =   5655
   End
   Begin VB.TextBox txtMain 
      Height          =   1935
      Left            =   360
      MultiLine       =   -1  'True
      TabIndex        =   5
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
      TabIndex        =   6
      Top             =   840
      Width           =   2175
   End
   Begin VB.Label txtStatus 
      Caption         =   "Status: Waiting for connection..."
      Height          =   375
      Left            =   600
      TabIndex        =   4
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
    lblServerNickname.Caption = "Place an order"
End Sub

'Private Sub Form_Unload(Cancel As Integer)
'    objWinsock.Close
'End Sub

Private Sub objWinsock_Close()
    txtMain.SelText = "Disconnected from Client"
    cmdSend.Enabled = False
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
    frmServer.Caption = "Chat Server   [Welcome, " & lblServerNickname.Caption & "!]"
    txtMain.SelText = "Connected to Client"
End Sub

Private Sub objWinsock_DataArrival(ByVal bytesTotal As Long)
    Dim strData, strData2 As String
    Call objWinsock.GetData(strData, vbString)
    strData2 = Left(strData, 1)
    strData = Mid(strData, 2)
    If strData2 = "T" Then
        txtMain.SelText = lblClientNickname.Caption & ":     " & strData & vbCrLf
    End If
    If strData2 = "N" Then
        lblClientNickname.Caption = strData
    End If
End Sub

Private Sub cmdSend_Click()
    txtMain.SelText = lblServerNickname.Caption & ":     " & txtData.Text & vbCrLf
    objWinsock.SendData "T" & txtData.Text
    txtData.Text = ""
End Sub

