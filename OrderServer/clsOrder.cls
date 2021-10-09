VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsOrder"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private m_strLineBuffer As String * 52

Public Property Get CUS() As String
    CUS = Mid(m_strLineBuffer, 1, 10)
End Property

Public Property Let CUS(ByVal strCUS As String)
    m_strLineBuffer = Left(strCUS & Space(10), 10) & Mid(m_strLineBuffer, 11)
End Property

Public Property Get DAT() As String
    DAT = Mid(m_strLineBuffer, 11, 19)
End Property

Public Property Let DAT(ByVal strDAT As String)
    m_strLineBuffer = Left(m_strLineBuffer, 10) & Left(strDAT & Space(19), 19) & Mid(m_strLineBuffer, 30)
End Property

Public Property Get PRC() As Double
    PRC = Mid(m_strLineBuffer, 30, 8)
End Property

Public Property Let PRC(ByVal dblPRC As Double)
    m_strLineBuffer = Left(m_strLineBuffer, 29) & Left(CStr(dblPRC) & Space(8), 8) & Mid(m_strLineBuffer, 38)
End Property

Public Property Get PRD() As String
    PRD = Mid(m_strLineBuffer, 38, 10)
End Property

Public Property Let PRD(ByVal strPRD As String)
    m_strLineBuffer = Left(m_strLineBuffer, 37) & Left(strPRD & Space(10), 10) & Mid(m_strLineBuffer, 48)
End Property

Public Property Get QTY() As Integer
    QTY = Mid(m_strLineBuffer, 48, 5)
End Property

Public Property Let QTY(ByVal intQTY As Integer)
    m_strLineBuffer = Left(m_strLineBuffer, 47) & Left(CStr(intQTY) & Space(5), 5) & Mid(m_strLineBuffer, 53)
End Property

Private Sub Class_Initialize()
    m_strLineBuffer = ""
    DAT = Format(Now(), "yyyy-mm-dd hh:nn:ss")
End Sub

Public Sub FromDatabaseFormat(ByVal strLineBuffer As String)
    m_strLineBuffer = strLineBuffer
End Sub

Public Function ToDatabaseFormat() As String
    ToDatabaseFormat = m_strLineBuffer
End Function
