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
      TabIndex        =   4
      Top             =   3840
      Width           =   975
   End
   Begin VB.TextBox txtData 
      Height          =   285
      Left            =   120
      MaxLength       =   100
      TabIndex        =   3
      Top             =   3840
      Width           =   5655
   End
   Begin VB.TextBox txtMain 
      BackColor       =   &H00FFFFFF&
      Height          =   3615
      Left            =   120
      Locked          =   -1  'True
      MultiLine       =   -1  'True
      ScrollBars      =   2  'Vertical
      TabIndex        =   0
      Top             =   120
      Width           =   6855
   End
   Begin MSWinsockLib.Winsock objWinsock 
      Left            =   120
      Top             =   360
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
   End
   Begin VB.Label lblClientNickname 
      Height          =   135
      Left            =   480
      TabIndex        =   2
      Top             =   4320
      Visible         =   0   'False
      Width           =   255
   End
   Begin VB.Label lblServerNickname 
      Height          =   135
      Left            =   240
      TabIndex        =   1
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

Private Sub objWinsock_Close()
    txtMain.SelText = "Disconnected from Client"
    cmdSend.Enabled = False
End Sub

Private Sub objWinsock_ConnectionRequest(ByVal requestID As Long)
    If objWinsock.State <> sckClosed Then
        objWinsock.Close
    End If
    objWinsock.Accept requestID
    Me.Show
    Unload frmStartup
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
